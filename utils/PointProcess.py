import numpy as np
import math
from mpl_toolkits.mplot3d import Axes3D
from matplotlib.ticker import FuncFormatter
import matplotlib.pyplot as plt

def readPoints(path, errorPath):

    images = []
    poses = []
    with open(path) as f:
        next(f)  # skip the 3 header lines
        next(f)
        next(f)
        for line in f:
            fname, p0, p1, p2, p3, p4, p5, p6 = line.split()
            p0 = float(p0)
            p1 = float(p1)
            p2 = float(p2)
            p3 = float(p3)
            p4 = float(p4)
            p5 = float(p5)
            p6 = float(p6)
            images.append(fname)
            poses.append((p0, p1, p2, p3, p4, p5, p6))

    predict = []
    with open(errorPath) as f:
        next(f)  # skip the 3 header lines
        next(f)
        next(f)
        for line in f:
            fname, p0, p1, p2, p3, p4, p5, p6, Error_X, theta = line.split()
            p0 = float(p0)
            p1 = float(p1)
            p2 = float(p2)
            p3 = float(p3)
            p4 = float(p4)
            p5 = float(p5)
            p6 = float(p6)
            predict.append((p0, p1, p2, p3, p4, p5, p6))
    return (images, poses, predict)

def point_error(images, poses, predict, seq = None):
    results = np.zeros((len(poses), 4))
    for i in range(len(poses)):
        pose_q = np.asarray(poses[i][3:7])
        pose_x = np.asarray(poses[i][0:3])
        predict_q = np.asarray(predict[i][3:7])
        predict_x = np.asarray(predict[i][0:3])

        #Compute Individual Sample Error
        q1 = pose_q / np.linalg.norm(pose_q)
        q2 = predict_q / np.linalg.norm(predict_q)
        d = abs(np.sum(np.multiply(q1,q2)))
        theta = 2 * np.arccos(d) * 180/math.pi

        error_vertical = np.linalg.norm(pose_x[1]-predict_x[1])
        error_horizontal = np.linalg.norm(pose_x[::2] - predict_x[::2])

        error_x = np.linalg.norm(pose_x - predict_x)

        results[i,:] = [error_x, error_horizontal, error_vertical, theta]
        #print ('Iteration:  ', i, '  Error Horizontal (m):  ', error_horizontal, '  Error Vertical (m):  ', error_vertical)

    median_result = np.median(results, axis=0)
    print('Median error ', median_result[0], 'm  and Error Vertical: ', median_result[2], 'm.', median_result[3], 'degrees.')

    images = np.asarray(images)
    images = images[results[:, 0].argsort()]
    error_sort = results[results[:, 0].argsort()]
    print('\n')
    if False:
    #if True:
        for i in range(20):
            print(images[i], error_sort[i, 0])
        print('\n')
    if seq == None:
        pass
    else:
        for i in range(len(seq) - 1):
            median_result = np.median(results[seq[i]: seq[i + 1], :], axis=0)
            print('dataset', i, ': Median error ', median_result[0], 'm  and ', median_result[3], 'degrees.')

def randrange(n, vmin, vmax):
    '''
    Helper function to make an array of random numbers having shape (n, )
    with each number distributed Uniform(vmin, vmax).
    '''
    return (vmax - vmin) * np.random.rand(n) + vmin

def example():

    fig = plt.figure()
    ax = fig.add_subplot(111, projection='3d')

    n = 100

    # For each set of style and range settings, plot n random points in the box
    # defined by x in [23, 32], y in [0, 100], z in [zlow, zhigh].
    for c, m, zlow, zhigh in [('r', 'o', -50, -25), ('b', '^', -30, -5)]:
        xs = randrange(n, 23, 32)
        ys = randrange(n, 0, 100)
        zs = randrange(n, zlow, zhigh)
        ax.scatter(xs, ys, zs, c=c, marker=m)

    ax.set_xlabel('X Label')
    ax.set_ylabel('Y Label')
    ax.set_zlabel('Z Label')

    plt.show()

def visualization_point(poses, predict):

    poses = np.asarray(poses)
    predict = np.asarray(predict)

    fig = plt.figure()
    ax = fig.add_subplot(111, projection='3d')

    ax.scatter(poses[:, 0], poses[:, 2], -poses[:, 1], c='r', marker='o')
    ax.scatter(predict[:, 0], predict[:, 2], -predict[:, 1], c='b', marker='^')

    ax.set_xlabel('X Label')
    ax.set_ylabel('Y Label')
    ax.set_zlabel('Z Label')

    plt.show()

def show_error():

    #train_seq = [0, 1532 - 1, 2752 - 1, 3647 - 1, 3878 - 1, 5365 - 1]#, 8380 - 1]
    #test_seq = [0, 760, 1103, 1285, 1388, 1918]#, 4841]
    #train_seq = [0, 1220, 2707]
    test_seq = [0, 343, 873]
    #train_seq = [0, 319, 921]
    #test_seq = [0, 66, 180]

    print('\nKingsCollege')
    #if False:
    if True:
        test_path = '.../KingsCollege/dataset_test.txt'
        test_error_path = '.../KingsCollege/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict)

    print('\nStMarysChurch')
    if True:
        test_path = '.../StMarysChurch/dataset_test.txt'
        test_error_path = '.../StMarysChurch/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict)

    print('\nHorizontal_2_Scenes_close')
    #if False:
    if True:
        test_path = '.../Horizontal_2_Scenes_close/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes_close/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nHorizontal_2_Scenes')
    #if False:
    if True:
        test_path = '.../Horizontal_2_Scenes/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nHorizontal_2_Scenes_far')
    #if False:
    if True:
        test_path = '.../Horizontal_2_Scenes_far/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes_far/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nVertical_2_Scenes_close')
    #if False:
    if True:
        test_path = '.../Vertical_2_Scenes_close/dataset_test.txt'
        test_error_path = '.../Vertical_2_Scenes_close/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nVertical_2_Scenes')
    #if False:
    if True:
        test_path = '.../Vertical_2_Scenes/dataset_test.txt'
        test_error_path = '.../Vertical_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nVertical_2_Scenes_far')
    #if False:
    if True:
        test_path = '.../Vertical_2_Scenes_far/dataset_test.txt'
        test_error_path = '.../Vertical_2_Scenes_far/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nScaled_2_Scenes_0.5')
    #if False:
    if True:
        test_path = '.../Scaled_2_Scenes/dataset_test.txt'
        test_error_path = '.../Scaled_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nScaled_2_Scenes_1')
    # if False:
    if True:
        test_path = '.../Horizontal_2_Scenes/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

    print('\nScaled_2_Scenes_2')
    #if False:
    if True:
        test_path = '.../Scaled_2_Scenes_2/dataset_test.txt'
        test_error_path = '.../Scaled_2_Scenes_2/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        point_error(images, poses, predict, test_seq)

def show_3d_visualization():

    test_path = '.../data/dataset_test.txt'
    test_error_path = '.../data/dataset_test_predict.txt'

    #test_path = '.../Scaled_2_Scenes/dataset_test.txt'
    #test_error_path = '.../Scaled_2_Scenes/dataset_test_predict.txt'

    images, poses, predict = readPoints(test_path, test_error_path)
    visualization_point(poses, predict)

    train_path = '.../data/dataset_train.txt'
    train_error_path = '.../data/dataset_train_predict.txt'

    #train_path = '.../Scaled_2_Scenes/dataset_train.txt'
    #train_error_path = '.../Scaled_2_Scenes/dataset_train_predict.txt'

    images, poses, predict = readPoints(train_path, train_error_path)
    visualization_point(poses, predict)


def vertical_error(poses, predict):
    poses = np.asarray(poses)
    predict = np.asarray(predict)

    N = poses.shape[0]

    k = np.abs(poses[:, 1] - predict[:, 1]) < 0.3
    print(np.sum(k) / N)

def show_vertical_accuracy():
    if True:
        test_path = '.../KingsCollege/dataset_test.txt'
        test_error_path = '.../KingsCollege/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)

        print('KingsCollege vertical distance: ', -np.min(poses, 0)[1] + np.max(poses, 0)[1])

        poses = np.asarray(poses)
        predict = np.asarray(predict)

        N1 = poses.shape[0]

        k1 = np.abs(poses[:, 1] - predict[:, 1]) < 0.3

        print(np.sum(k1)/N1)

        test_path = '.../StMarysChurch/dataset_test.txt'
        test_error_path = '.../StMarysChurch/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        #print(np.min(poses, 0)[1])
        #print(np.max(poses, 0)[1])

        print('StMarysChurch vertical distance: ', -np.min(poses, 0)[1] + np.max(poses, 0)[1])

        poses = np.asarray(poses)
        predict = np.asarray(predict)

        N2 = poses.shape[0]

        k2 = np.abs(poses[:, 1] - predict[:, 1]) < 0.3

        print(np.sum(k2)/N2)

        print('total accuracy: ', (np.sum(k1) + np.sum(k2))/(N1 + N2))

    if True:

        print('Horizontal_2_Scenes_close')
        test_path = '.../Horizontal_2_Scenes_close/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes_close/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Horizontal_2_Scenes_mid')
        test_path = '.../Horizontal_2_Scenes/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Horizontal_2_Scenes_far')
        test_path = '.../Horizontal_2_Scenes_far/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes_far/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Vertical_2_Scenes_close')
        test_path = '.../Vertical_2_Scenes_close/dataset_test.txt'
        test_error_path = '.../Vertical_2_Scenes_close/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Vertical_2_Scenes_mid')
        test_path = '.../Vertical_2_Scenes/dataset_test.txt'
        test_error_path = '.../Vertical_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Vertical_2_Scenes_far')
        test_path = '.../Vertical_2_Scenes_far/dataset_test.txt'
        test_error_path = '.../Vertical_2_Scenes_far/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Scaled_2_Scenes_close')
        test_path = '.../Scaled_2_Scenes/dataset_test.txt'
        test_error_path = '.../Scaled_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Scaled_2_Scenes_mid')
        test_path = '.../Horizontal_2_Scenes/dataset_test.txt'
        test_error_path = '.../Horizontal_2_Scenes/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

    if True:

        print('Scaled_2_Scenes_far')
        test_path = '.../Scaled_2_Scenes_2/dataset_test.txt'
        test_error_path = '.../Scaled_2_Scenes_2/dataset_test_predict.txt'

        images, poses, predict = readPoints(test_path, test_error_path)
        vertical_error(poses, predict)

def millions(x, pos):
    'The two args are the value and tick position'
    return '%1.1f' % (x * 100) + '%'

def plot_bar_x():
    # this is for plotting purpose

    label = ['baseline',
             'h_close',
             'h_mid',
             'h_far',
             'v_close',
             'v_mid',
             'v_far',
             's_close',
             's_mid',
             's_far']

    accuracy = [
        0.806414662085,
        0.792668957617,
        0.762886597938,
        0.797250859107,
        0.751431844215,
        0.289805269187,
        0.17067583047,
        0.782359679267,
        0.762886597938,
        0.616265750286
    ]

    index = np.arange(len(label))

    fig, ax = plt.subplots()

    for i, v in enumerate(accuracy):
        ax.text(v/2, i, '%1.1f' % (v * 100) + '%',fontsize = 12, color='tab:blue', fontweight='bold', horizontalalignment='center', verticalalignment='center')
        print(i, v)

    ax.barh(index, accuracy, 0.75, color = 'burlywood')

    formatter = FuncFormatter(millions)
    ax.xaxis.set_major_formatter(formatter)
    #plt.xlabel('Genre', fontsize=5)
    #plt.ylabel('No of Movies', fontsize=5)
    plt.yticks(index, label, fontsize=10, rotation=0)
    plt.title('prediction accuracy on vertical direction')
    plt.show()

if __name__ == '__main__':
    #show_error()
    #show_3d_visualization()
    #show_vertical_accuracy()
    #plot_bar_x()