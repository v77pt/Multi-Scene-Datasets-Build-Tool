import numpy as np
from PIL import Image
from tqdm import tqdm

import os
import re

def compute_image_mean(path, file_name, output):
    images = []
    
    for name in file_name:
        with open(path + name) as f:
            next(f)
            next(f)
            next(f)
            for line in f:
                fname, p0,p1,p2,p3,p4,p5,p6 = line.split()
                images.append(fname)
    preprocess(images, path, output)

def centeredCrop(img, output_side_length):
    height, width, depth = img.shape
    height_offset = (height - output_side_length) // 2
    width_offset = (width - output_side_length) // 2
    cropped_img = img[height_offset:height_offset + output_side_length,
                        width_offset:width_offset + output_side_length]
    
    #print(height_offset,height_offset + output_side_length,
    #                    width_offset,width_offset + output_side_length)
    return cropped_img

def preprocess(images, path, output):
    images_out = [] #final result
    #Resize and crop and compute mean!
    images_cropped = []
    for i in tqdm(range(len(images))):
        #X = cv2.imread(images[i])
        #X = cv2.resize(X, (455, 256))
        X = Image.open(images[i])
        
        X = X.resize((455, 256))
        
        save_path = make_path(images[i], 'resize/')
        X.save(save_path)
        
        X = np.array(X).astype("uint8")
        X = centeredCrop(X, 224)
        
        save_path = make_path(images[i], 'centeredCrop/')
        Image.fromarray(X).save(save_path)
        
        images_cropped.append(X)
    
    #compute images mean
    N = 0
    
    mean = np.zeros((1, 3, 224, 224))
    
    for X in tqdm(images_cropped):
        mean[0][0] += X[:,:,0]
        mean[0][1] += X[:,:,1]
        mean[0][2] += X[:,:,2]
        N += 1
    mean[0] /= N

    np.savez(output, mean)
    print('create image mean')
    
def make_path(path, inner):
    
    path = path[:32] + inner + path[32:]
    
    #isExists = os.path.exists(path[:-12]) || os.path.exists(path[:-23])
    #if not isExists:
    #    os.makedirs(path[:-14])
    
    return path

if __name__ == '__main__':

    # root directory
    directory = '.../data/'
    dataset = ['dataset_train.txt',
               'dataset_test.txt']

    save_path = 'D:/PythonWorkSpace/posenet/PoseNet_Sep/image_mean.npz'

    compute_image_mean(directory, dataset, save_path)