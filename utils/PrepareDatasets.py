import pandas as pd
import numpy as np
import re
import datetime

def NVMtoOFF(path, name):
    print('convert dataset: ', name)
    starttime = datetime.datetime.now()

    nvmFile = open(path)
    arrayOfLines = nvmFile.readlines()

    index = 2
    ncam = int(arrayOfLines[index])

    print('Num of Camera:')
    print(ncam)

    index += ncam + 2
    npoint = int(arrayOfLines[index])

    print('Num of Point:')
    print(npoint)

    index += 1

    pointData = arrayOfLines[index: index + npoint]

    point_parameter_num = 6

    print('saving off file.')

    f = open('Datasets/OffFiles/%s.off' % name, 'w')
    f.write('COFF\n%d 0 0' % npoint)

    for data in pointData:
        temp = data.split()[:point_parameter_num]
        f.write('\n%s %s %s %s %s %s 255' % (temp[0], temp[1], temp[2], temp[3], temp[4], temp[5]))

    f.close()

    endtime = datetime.datetime.now()
    print('done.')
    print('cost time: ', (endtime - starttime).seconds, 's')
    # return PointDataValue


def CameraDataToCSV(path, save_path):
    print('convert camera label to csv file: ', save_path)
    starttime = datetime.datetime.now()

    cameraDataFile = open(path)
    arrayOfLines = cameraDataFile.readlines()

    index = 3
    CameraData = arrayOfLines[index:]
    print('Num of Camera:')
    print(len(CameraData))

    camera_parameter_num = 8
    CameraDataValue = np.ones([1, camera_parameter_num])

    for data in CameraData:
        data = data.strip('\n')
        temp = np.array(re.split('[\t ]', data))
        print(temp)
        temp = temp[:8]
        temp = temp.reshape([1, camera_parameter_num])
        CameraDataValue = np.concatenate((CameraDataValue, temp), axis=0)

    CameraDataValue = CameraDataValue[1:, :]

    print('saving txt file.')
    A = CameraDataValue
    dataframe = pd.DataFrame(
        {'FileName': A[:, 0], 'X': A[:, 1], 'Y': A[:, 2], 'Z': A[:, 3], 'W': A[:, 4], 'P': A[:, 5], 'Q': A[:, 6],
         'R': A[:, 7]})
    dataframe.to_csv(save_path, index=False, sep='\t')

    endtime = datetime.datetime.now()
    print('done.')
    print('cost time: ', (endtime - starttime).seconds, 's')


def CSVToCameraData(path, save_path):
    print('convert csv file to camera label: ', save_path)
    starttime = datetime.datetime.now()

    cameraCSV = pd.read_csv(path, sep='\t')
    nCamera = len(cameraCSV)
    print('Num of Camera:')
    print(nCamera)

    CameraDataValue = np.concatenate((np.array(cameraCSV['FileName']).reshape([nCamera, 1]),
                                      np.array(cameraCSV['X']).reshape([nCamera, 1])), axis=1)
    CameraDataValue = np.concatenate((CameraDataValue, np.array(cameraCSV['Y']).reshape([nCamera, 1])), axis=1)
    CameraDataValue = np.concatenate((CameraDataValue, np.array(cameraCSV['Z']).reshape([nCamera, 1])), axis=1)
    CameraDataValue = np.concatenate((CameraDataValue, np.array(cameraCSV['W']).reshape([nCamera, 1])), axis=1)
    CameraDataValue = np.concatenate((CameraDataValue, np.array(cameraCSV['P']).reshape([nCamera, 1])), axis=1)
    CameraDataValue = np.concatenate((CameraDataValue, np.array(cameraCSV['Q']).reshape([nCamera, 1])), axis=1)
    CameraDataValue = np.concatenate((CameraDataValue, np.array(cameraCSV['R']).reshape([nCamera, 1])), axis=1)

    print('saving txt file.')
    np.savetxt(save_path, CameraDataValue, fmt='%s',
               header='Visual Landmark Dataset V1\nImageFile, Camera Position [X Y Z W P Q R]')

    endtime = datetime.datetime.now()
    print('done.')
    print('cost time: ', (endtime - starttime).seconds, 's')


def AddPath(path, dataPath, savePath):
    print('AddPath: ', path)
    starttime = datetime.datetime.now()

    file = open(path)
    arrayOfLines = file.readlines()

    f = open(savePath, 'w')
    f.write('Visual Landmark Dataset V1\nImageFile, Camera Position [X Y Z W P Q R]\n\n')

    cameraData = arrayOfLines[3:]
    for i in cameraData:
        f.write(dataPath + i)

    f.close()

    endtime = datetime.datetime.now()
    print('done.')
    print('cost time: ', (endtime - starttime).seconds, 's')


def CombineDatasets(filePath, dataPath, savePath):
    file = open(filePath)
    arrayOfLines = file.readlines()
    index = 3

    saveFile = open(savePath, 'a')

    for i in range(index, len(arrayOfLines)):
        saveFile.write(dataPath + arrayOfLines[i])


def convert_data():
    starttime = datetime.datetime.now()

    datasetName = [
        #'GreatCourt',
        'KingsCollege',
        #'OldHospital',
        #'ShopFacade',
        'StMarysChurch'#,
        #'Street',
        #'EngineeringBuilding',
        #'OlympicGym'
    ]

    # dataset_tpyeName = ['dataset_train.txt', 'dataset_test.txt']
    # NVM_FileName = 'reconstruction.nvm'
    newdataset_tpyeName = ['dataset_train_new.txt', 'dataset_test_new.txt']
    csv_tpyeName = ['_csv_dataset_train_new.txt', '_csv_dataset_test_new.txt']


    workspaceROOT = 'D:/PythonWorkSpace/posenet/PoseNet_Sep/Vertical_2_Scenes_close/'
    datasetROOT = workspaceROOT + 'Datasets/'

    for name in datasetName:
        for typeName in csv_tpyeName:
            path = datasetROOT + 'CSVFiles/' + name + typeName
            save_path = datasetROOT + 'LabelFiles/' + name + typeName[4:]
            CSVToCameraData(path, save_path)


    for i in range(len(datasetName)):
        for typename in newdataset_tpyeName:
            filePath = datasetROOT + 'LabelFiles/' + datasetName[i] + '_' + typename
            dataPath = 'D:/_git/posenet/PoseNet_Dataset/' + datasetName[i] + '/'
            savePath = workspaceROOT + typename
            CombineDatasets(filePath, dataPath, savePath)

    endtime = datetime.datetime.now()
    print('whole time used: ', (endtime - starttime).seconds, 's')

if __name__ == '__main__':
    path = 'D:/PythonWorkSpace/posenet/PoseNet_Sep/cau/dataset_test_predict.txt'
    savepath = 'D:/PythonWorkSpace/posenet/PoseNet_Sep/cau/dataset_predict_csv.csv'
    CameraDataToCSV(path, savepath)