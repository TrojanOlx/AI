﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void load_mnist_dataset(byte[] folderPath,
                                                     out IntPtr training_images,
                                                     out IntPtr training_labels,
                                                     out IntPtr testing_images,
                                                     out IntPtr testing_labels);

    }

}