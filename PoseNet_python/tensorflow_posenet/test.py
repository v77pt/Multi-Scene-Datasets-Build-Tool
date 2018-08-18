# Import the converted model's class
import numpy as np
import random
import os
import tensorflow as tf
from tensorflow.python.ops import rnn, rnn_cell
from posenet import GoogLeNet as PoseNet
#import cv2
from PIL import Image
from tqdm import tqdm
import math

batch_size = 1
max_iterations = 30000

#Your data root
directory = '.../'
dataset = ['dataset_train.txt', 'dataset_test.txt']
dataset_predict = '_predict.txt'

inner = 'centeredCrop/'
image_mean_path = '.../image_mean.npz'

predicted_x = None
predicted_q = None

class datasource(object):
    def __init__(self, fname, images, poses):
        self.fname = fname
        self.images = images
        self.poses = poses

class vecsource(object):
    def __init__(self, vecs, poses):
        self.vecs = vecs
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
        #X = cv2.imread(images[i])
        #X = cv2.resize(X, (455, 256))
        X = Image.open(images[i])
        X = np.array(X.resize((455, 256))).astype("uint8")
        X = centeredCrop(X, 224)
        images_cropped.append(X)
    #compute images mean
    N = 0

    mean = np.zeros((1, 3, 224, 224))
    if not os.path.exists('image_mean.npz'):
        print('does not find image mean')
        quit()
    else:
        mean = np.load('image_mean.npz')['arr_0']
        print('load image mean')

    #Subtract mean from all images
    for X in tqdm(images_cropped):
        X = np.transpose(X,(2,0,1))
        X = X - mean
        X = np.squeeze(X)
        X = np.transpose(X, (1,2,0))
        Y = np.expand_dims(X, axis=0)
        images_out.append(Y)
    return images_out

def get_image(images):
    images_out = [] #final result
    #Resize and crop and compute mean!
    images_cropped = []

    for i in tqdm(range(len(images))):
        #X = cv2.imread(images[i])
        #X = cv2.resize(X, (455, 256))
        X = Image.open(images[i][:32] + inner + images[i][32:])
        X = np.array(X).astype("uint8")
        images_cropped.append(X)

        #compute images mean
    N = 0

    mean = np.zeros((1, 3, 224, 224))
    
    if not os.path.exists(image_mean_path):
        print('does not find image mean')
        quit()
    else:
        mean = np.load(image_mean_path)['arr_0']
        print('load image mean')

    #Subtract mean from all images
    for X in tqdm(images_cropped):
        X = np.transpose(X,(2,0,1))
        X = X - mean
        X = np.squeeze(X)
        X = np.transpose(X, (1,2,0))
        Y = np.expand_dims(X, axis=0)
        images_out.append(Y)
    return images_out


def get_data(datapath):
    poses = []
    images = []
    fileName = []
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
            #images.append(directory+fname)
            images.append(fname)
            fileName.append(fname)
    #images = preprocess(images)
    images = get_image(fileName)
    return datasource(fileName, images, poses)

def gen_data(source):
    while True:
        indices = range(len(source.images))
        random.shuffle(indices)
        for i in indices:
            fname = source.fname[i]
            image = source.images[i]
            pose_x = source.poses[i][0:3]
            pose_q = source.poses[i][3:7]
            yield fname, image, pose_x, pose_q

def gen_data_batch(source):
    data_gen = gen_data(source)
    while True:
        fname_batch = []
        image_batch = []
        pose_x_batch = []
        pose_q_batch = []
        for _ in range(batch_size):
            fname, image, pose_x, pose_q = next(data_gen)
            fname_batch.append(fname)
            image_batch.append(image)
            pose_x_batch.append(pose_x)
            pose_q_batch.append(pose_q)
        yield fname_batch, np.array(image_batch), np.array(pose_x_batch), np.array(pose_q_batch)

def inference():

    image = tf.placeholder(tf.float32, [1, 224, 224, 3])
    

    predicted_X = np.zeros([1,3])
    predicted_Q = np.zeros([1,4])

    net = PoseNet({'data': image})

    p3_x = net.layers['cls3_fc_pose_xyz']
    p3_q = net.layers['cls3_fc_pose_wpqr']

    #init = tf.initialize_all_variables()
    init = tf.global_variables_initializer()
    outputFile = "PoseNet.ckpt"

    saver = tf.train.Saver()

    with tf.Session() as sess:
        # Load the data
        sess.run(init)
        saver.restore(sess, directory + "PoseNet.ckpt")#path + 'PoseNet.ckpt')

        for index in range(len(dataset)):

            f = open(directory + dataset[index][:-4] + dataset_predict,'w')
            f.write('Visual Landmark Dataset V1\nImageFile, Camera Position [X Y Z W P Q R]\n')

            datasource = get_data(directory + dataset[index])
            results = np.zeros((len(datasource.images),2))

            #data_gen = gen_data_batch(datasource)
            for i in range(len(datasource.images)):
                np_image = datasource.images[i]
                feed = {image: np_image}
                pose_q= np.asarray(datasource.poses[i][3:7])
                pose_x= np.asarray(datasource.poses[i][0:3])
                predicted_x, predicted_q = sess.run([p3_x, p3_q], feed_dict=feed)
                predicted_q = np.squeeze(predicted_q).reshape([1,4])
                predicted_x = np.squeeze(predicted_x).reshape([1,3])

                #Compute Individual Sample Error
                q1 = pose_q / np.linalg.norm(pose_q)
                q2 = predicted_q / np.linalg.norm(predicted_q)
                d = abs(np.sum(np.multiply(q1,q2)))
                theta = 2 * np.arccos(d) * 180/math.pi
                error_x = np.linalg.norm(pose_x-predicted_x)
                results[i,:] = [error_x,theta]
                f.write('\n%s %s %s %s %s %s %s %s %s %s' % 
                    (datasource.fname[i],predicted_x[0,0],predicted_x[0,1],predicted_x[0,2],predicted_q[0,0],predicted_q[0,1],predicted_q[0,2],predicted_q[0,3], error_x, theta))
            
            f.close()

            median_result = np.median(results,axis=0)
            print ('Median error ', median_result[0], 'm  and ', median_result[1], 'degrees.')

if __name__ == '__main__':
    main()