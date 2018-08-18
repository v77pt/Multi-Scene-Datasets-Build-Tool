# Import the converted model's class
import numpy as np
import random
import os
import tensorflow as tf
from posenet import GoogLeNet as PoseNet
#import cv2
from PIL import Image
from tqdm import tqdm

import re

batch_size = 64 #75
max_iterations = 15000 #30000

#Your data root
directory = '.../'
dataset = 'dataset_train.txt'
dataset_test = 'dataset_test.txt'

inner = 'centeredCrop/'

image_mean_path = '.../image_mean.npz'

class datasource(object):
    def __init__(self, images, poses):
        self.images = images
        self.poses = poses

def centeredCrop(img, output_side_length):
    height, width, depth = img.shape
    height_offset = (new_height - output_side_length) // 2
    width_offset = (new_width - output_side_length) // 2
    cropped_img = img[height_offset:height_offset + output_side_length,
                        width_offset:width_offset + output_side_length]
    return cropped_img

def preprocess(images):
    images_out = [] #final result
    #Resize and crop and compute mean!
    images_cropped = []
    for i in tqdm(range(len(images))):
        X = Image.open(images[i])
        X = np.array(X.resize((455, 256))).astype("uint8")
        X = centeredCrop(X, 224)
        images_cropped.append(X)
    #compute images mean
    N = 0

    mean = np.zeros((1, 3, 224, 224))
    
    if not os.path.exists(image_mean_path):

        for X in tqdm(images_cropped):
            mean[0][0] += X[:,:,0]
            mean[0][1] += X[:,:,1]
            mean[0][2] += X[:,:,2]
            N += 1
        mean[0] /= N

        np.savez(image_mean_path, mean)
        print('create image mean')

    else:

        mean = np.load(image_mean_path)['arr_0']
        print('load image mean')

    #Subtract mean from all images
    for X in tqdm(images_cropped):
        X = np.transpose(X,(2,0,1))
        X = X - mean
        X = np.squeeze(X)
        X = np.transpose(X, (1,2,0))
        images_out.append(X)
    return images_out

def get_image(images):
    images_out = [] #final result
    #Resize and crop and compute mean!
    images_cropped = []

    for i in tqdm(range(len(images))):
        X = Image.open(images[i][:32] + inner + images[i][32:])
        X = np.array(X).astype("uint8")
        images_cropped.append(X)

        #compute images mean
    N = 0

    mean = np.zeros((1, 3, 224, 224))
    
    if not os.path.exists(image_mean_path):

        for X in tqdm(images_cropped):
            mean[0][0] += X[:,:,0]
            mean[0][1] += X[:,:,1]
            mean[0][2] += X[:,:,2]
            N += 1
        mean[0] /= N

        np.savez(image_mean_path, mean)
        print('create image mean')

    else:

        mean = np.load(image_mean_path)['arr_0']
        print('load image mean')

    #Subtract mean from all images
    for X in tqdm(images_cropped):
        X = np.transpose(X,(2,0,1))
        X = X - mean
        X = np.squeeze(X)
        X = np.transpose(X, (1,2,0))
        images_out.append(X)
    return images_out


def get_data(datapath):
    poses = []
    images = []

    with open(datapath) as f:
        next(f)  # skip the 3 header lines
        next(f)
        next(f)
        for line in f:
            fname, p0,p1,p2,p3,p4,p5,p6 = line.split()
            p0 = float(p0)
            p1 = float(p1)
            p2 = float(p2)
            p3 = float(p3)
            p4 = float(p4)
            p5 = float(p5)
            p6 = float(p6)
            poses.append((p0,p1,p2,p3,p4,p5,p6))
            images.append(fname)
    images = get_image(images)
    return datasource(images, poses)

def gen_data(source):
    while True:
        indices = list(range(len(source.images)))
        random.shuffle(indices)
        for i in indices:
            image = source.images[i]
            pose_x = source.poses[i][0:3]
            pose_q = source.poses[i][3:7]
            yield image, pose_x, pose_q

def gen_data_batch(source):
    data_gen = gen_data(source)
    while True:
        image_batch = []
        pose_x_batch = []
        pose_q_batch = []
        for _ in range(batch_size):
            image, pose_x, pose_q = next(data_gen)
            image_batch.append(image)
            pose_x_batch.append(pose_x)
            pose_q_batch.append(pose_q)
        yield np.array(image_batch), np.array(pose_x_batch), np.array(pose_q_batch)


def main():
    random.seed(27)

    images = tf.placeholder(tf.float32, [batch_size, 224, 224, 3])
    poses_x = tf.placeholder(tf.float32, [batch_size, 3])
    poses_q = tf.placeholder(tf.float32, [batch_size, 4])
    

    net = PoseNet({'data': images})

    p1_x = net.layers['cls1_fc_pose_xyz']
    p1_q = net.layers['cls1_fc_pose_wpqr']
    p2_x = net.layers['cls2_fc_pose_xyz']
    p2_q = net.layers['cls2_fc_pose_wpqr']
    p3_x = net.layers['cls3_fc_pose_xyz']
    p3_q = net.layers['cls3_fc_pose_wpqr']

    l1_x = tf.sqrt(tf.reduce_sum(tf.square(tf.subtract(p1_x, poses_x)))) * 0.3
    l1_q = tf.sqrt(tf.reduce_sum(tf.square(tf.subtract(p1_q, poses_q)))) * 225
    l2_x = tf.sqrt(tf.reduce_sum(tf.square(tf.subtract(p2_x, poses_x)))) * 0.3
    l2_q = tf.sqrt(tf.reduce_sum(tf.square(tf.subtract(p2_q, poses_q)))) * 225
    l3_x = tf.sqrt(tf.reduce_sum(tf.square(tf.subtract(p3_x, poses_x)))) * 1
    l3_q = tf.sqrt(tf.reduce_sum(tf.square(tf.subtract(p3_q, poses_q)))) * 500

    WEIGHT_DECAY_FACTOR = 0.005

    # Create your variables
    #weights = tf.get_variable('weights', collections=['variables'])

    with tf.variable_scope('weights_norm') as scope:
        weights_norm = tf.reduce_sum(
            input_tensor = WEIGHT_DECAY_FACTOR*tf.stack(
                [tf.nn.l2_loss(i) for i in tf.get_collection('weights')]
            ),
            name='weights_norm'
        )

    # Add the weight decay loss to another collection called losses
    #tf.add_to_collection('losses', weights_norm)

    # To calculate your total loss
    #tf.add_n(tf.get_collection('losses'), name='total_loss')

    loss = l1_x + l1_q + l2_x + l2_q + l3_x + l3_q
    losses = loss + weights_norm
    opt = tf.train.AdamOptimizer(learning_rate=0.0001, beta1=0.9, beta2=0.999, epsilon=0.00000001, use_locking=False, name='Adam').minimize(losses)

    # Set GPU options
    gpu_options = tf.GPUOptions(per_process_gpu_memory_fraction=0.6833)

    init = tf.global_variables_initializer()
    saver = tf.train.Saver()
    outputFile = directory + "PoseNet.ckpt"

    #f = open('D:/PythonWorkSpace/posenet/PoseNet_AllDatasets_Closer/practiceCurves.txt','w')


    with tf.Session(config=tf.ConfigProto(gpu_options=gpu_options)) as sess:
        # Load the data
        sess.run(init)
        net.load('.../weights/posenet.npy', sess)
        #saver.restore(sess, directory + "PoseNet.ckpt")

        datasource = get_data(directory+dataset)
        datasource_test = get_data(directory+dataset_test)

        data_gen = gen_data_batch(datasource)
        data_gen_test = gen_data_batch(datasource_test)

        for i in range(max_iterations):
            np_images, np_poses_x, np_poses_q = next(data_gen)
            feed = {images: np_images, poses_x: np_poses_x, poses_q: np_poses_q}

            sess.run(opt, feed_dict=feed)
            
            if i % 500 == 0:
                np_loss = sess.run(loss, feed_dict=feed)

                np_images_test, np_poses_x_test, np_poses_q_test = next(data_gen_test)
                feed_test = {images: np_images_test, poses_x: np_poses_x_test, poses_q: np_poses_q_test}

                np_loss_test = sess.run(loss, feed_dict=feed_test)

                print("iteration: " + str(i) + "\n\t" + "Loss is: " + str(np_loss) + "\n\t" + "test Loss is: " + str(np_loss_test))
                f = open(directory + 'practiceCurves.txt','a')
                f.write(str(i) + " " + str(np_loss) + " " + str(np_loss_test) + "\n")
                f.close()
            if i % 2000 == 0:
                saver.save(sess, outputFile, global_step=i)
                print("Intermediate file saved at: " + outputFile)
        saver.save(sess, outputFile)
        print("Intermediate file saved at: " + outputFile)

        #f.close()

if __name__ == '__main__':
    main()