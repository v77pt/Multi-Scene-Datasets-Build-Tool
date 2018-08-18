from matplotlib.lines import Line2D
import matplotlib.pyplot as plt
import numpy as np



def readPoints(path, errorPath):
    file = open(path)
    arrayOfLines = file.readlines()

    index = 3

    cameraData = arrayOfLines[index:]

    #data = np.zeros([len(cameraData), 4], dtype=np.float32)

    data = np.zeros([len(cameraData), 5], dtype=np.object_)

    for i in range(len(cameraData)):
        temp = cameraData[i].split()
        #data[i, 0] = np.float32(temp[1])
        #data[i, 1] = -np.float32(temp[3])

        data[i, 0] = temp[1]
        data[i, 1] = temp[3]
    file.close()

    errorfile = open(errorPath)
    errorarray = errorfile.readlines()

    index = 3

    errorData = errorarray[index:]

    for i in range(len(errorData)):
        temp = errorData[i].split()
        #data[i, 2] = np.float32(temp[1])
        #data[i, 3] = -np.float32(temp[3])

        data[i, 2] = temp[1]
        data[i, 3] = temp[3]

        data[i, 4] = temp[0]
    errorfile.close()

    data = data[data[:, 4].argsort(), :]
    #print(data)
    return data[:, :-1].astype(np.float32)


def processPoints(points):
    # deal with positions

    #fix the coordinate
    points[:, 1] = -points[:, 1]
    points[:, 3] = -points[:, 3]

    points[:, 0] = -points[:, 0]
    points[:, 2] = -points[:, 2]

    #Exponentially Weighted Moving Average
    #if True:
    if False:
        for i in range(1, points.shape[0]):
            points[i, 2] = points[i - 1, 2] * 0.8 + points[i, 2] * 0.2
            points[i, 3] = points[i - 1, 3] * 0.8 + points[i, 3] * 0.2

    xmin = np.min(points[:, 0])
    ymin = np.min(points[:, 1])

    print('xmin: ', xmin)
    print('ymin: ', ymin)

    points[:, 0] = points[:, 0] + 400  # - xmin
    points[:, 1] = points[:, 1] + 650  # - ymin

    points[points[:, 0] > 1000, 0] = 100
    points[points[:, 1] > 1000, 1] = 100
    points[points[:, 2] > 1000, 2] = 100
    points[points[:, 3] > 1000, 3] = 100


    # deal with errors
    errorXmin = np.min(points[:, 2])
    errorQmin = np.min(points[:, 3])

    points[:, 2] = points[:, 2] + 400
    points[:, 3] = points[:, 3] + 650

    return points


def drawTrainAndTest1(train, test):
    fig, ax = plt.subplots(figsize=(8, 10))

    plt.xlim(0, 800)
    plt.ylim(0, 1000)
    if True:
    #if False:
        for i in range(train.shape[0]):
            line = [(train[i, 0], train[i, 1]), (test[i, 0], test[i, 1])]
            (line_xs, line_ys) = zip(*line)
            ax.add_line(Line2D(line_xs, line_ys, linewidth=1, color='blue'))
    #if True:
    if False:
        for i in range(train.shape[0]-1):
            line = [(train[i, 0], train[i, 1]), (train[i + 1, 0], train[i + 1, 1])]
            (line_xs, line_ys) = zip(*line)
            ax.add_line(Line2D(line_xs, line_ys, linewidth=1, color='blue'))
    #if True:
    if False:
        for i in range(test.shape[0]-1):
            line = [(test[i, 0], test[i, 1]), (test[i + 1, 0], test[i + 1, 1])]
            (line_xs, line_ys) = zip(*line)
            ax.add_line(Line2D(line_xs, line_ys, linewidth=1, color='blue'))

    ax.scatter(train[:, 0], train[:, 1], c='r', s=1, alpha=0.5)
    ax.scatter(test[:, 0], test[:, 1], c='b', s=1, alpha=0.5)

    ax.set_xlabel(r'x', fontsize=15)
    ax.set_ylabel(r'y', fontsize=15)
    ax.set_title('data set')

    ax.grid(True)
    fig.tight_layout()

    plt.show()



if __name__ == '__main__':

    test_path = '.../dataset_test.txt'
    test_error_path = '.../dataset_test_predict.txt'

    test_data = readPoints(test_path, test_error_path)

    test_data = processPoints(test_data)

    drawTrainAndTest1(test_data, test_data[:, 2:])