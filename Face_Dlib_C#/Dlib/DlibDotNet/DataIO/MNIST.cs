﻿using System;
using System.IO;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void LoadMNISTDataset(string folderPath,
                                            out Matrix<byte>[] trainingImages,
                                            out uint[] trainingLabels,
                                            out Matrix<byte>[] testingImages,
                                            out uint[] testingLabels)
        {
            if (folderPath == null)
                throw new ArgumentNullException(nameof(folderPath));
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException();

            trainingImages = null;
            trainingLabels = null;
            testingImages = null;
            testingLabels = null;

            var str = Dlib.Encoding.GetBytes(folderPath);

            NativeMethods.load_mnist_dataset(str, 
                                             out var retTrainingImages,
                                             out var retTrainingLabels, 
                                             out var retTestingImages,
                                             out var retTestingLabels);

            using (var tmp = new StdVector<Matrix<byte>>(retTrainingImages))
                trainingImages = tmp.ToArray();
            using (var tmp = new StdVector<uint>(retTrainingLabels))
                trainingLabels = tmp.ToArray();
            using (var tmp = new StdVector<Matrix<byte>>(retTestingImages))
                testingImages = tmp.ToArray();
            using (var tmp = new StdVector<uint>(retTestingLabels))
                testingLabels = tmp.ToArray();
        }
        
        #endregion

    }

}