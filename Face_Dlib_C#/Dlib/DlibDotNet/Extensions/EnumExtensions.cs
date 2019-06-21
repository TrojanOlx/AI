﻿using System;

namespace DlibDotNet.Extensions
{

    internal static class EnumExtensions
    {

        internal static NativeMethods.Array2DType ToNativeArray2DType(this ImageTypes imageType)
        {
            switch (imageType)
            {
                case ImageTypes.RgbPixel:
                    return NativeMethods.Array2DType.RgbPixel;
                case ImageTypes.RgbAlphaPixel:
                    return NativeMethods.Array2DType.RgbAlphaPixel;
                case ImageTypes.UInt8:
                    return NativeMethods.Array2DType.UInt8;
                case ImageTypes.UInt16:
                    return NativeMethods.Array2DType.UInt16;
                case ImageTypes.UInt32:
                    return NativeMethods.Array2DType.UInt32;
                case ImageTypes.Int8:
                    return NativeMethods.Array2DType.Int8;
                case ImageTypes.Int16:
                    return NativeMethods.Array2DType.Int16;
                case ImageTypes.Int32:
                    return NativeMethods.Array2DType.Int32;
                case ImageTypes.HsiPixel:
                    return NativeMethods.Array2DType.HsiPixel;
                case ImageTypes.Float:
                    return NativeMethods.Array2DType.Float;
                case ImageTypes.Double:
                    return NativeMethods.Array2DType.Double;
                case ImageTypes.Matrix:
                    return NativeMethods.Array2DType.Matrix;
                default:
                    throw new ArgumentOutOfRangeException(nameof(imageType), imageType, null);
            }
        }

        internal static NativeMethods.MatrixElementType ToNativeMatrixElementType(this MatrixElementTypes matrixElementTypes)
        {
            switch (matrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    return NativeMethods.MatrixElementType.UInt8;
                case MatrixElementTypes.UInt16:
                    return NativeMethods.MatrixElementType.UInt16;
                case MatrixElementTypes.UInt32:
                    return NativeMethods.MatrixElementType.UInt32;
                case MatrixElementTypes.UInt64:
                    return NativeMethods.MatrixElementType.UInt64;
                case MatrixElementTypes.Int8:
                    return NativeMethods.MatrixElementType.Int8;
                case MatrixElementTypes.Int16:
                    return NativeMethods.MatrixElementType.Int16;
                case MatrixElementTypes.Int32:
                    return NativeMethods.MatrixElementType.Int32;
                case MatrixElementTypes.Int64:
                    return NativeMethods.MatrixElementType.Int64;
                case MatrixElementTypes.Float:
                    return NativeMethods.MatrixElementType.Float;
                case MatrixElementTypes.Double:
                    return NativeMethods.MatrixElementType.Double;
                case MatrixElementTypes.RgbPixel:
                    return NativeMethods.MatrixElementType.RgbPixel;
                case MatrixElementTypes.RgbAlphaPixel:
                    return NativeMethods.MatrixElementType.RgbAlphaPixel;
                case MatrixElementTypes.HsiPixel:
                    return NativeMethods.MatrixElementType.HsiPixel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matrixElementTypes), matrixElementTypes, null);
            }
        }

        internal static NativeMethods.VectorElementType ToNativeVectorElementType(this VectorElementTypes types)
        {
            switch (types)
            {
                case VectorElementTypes.UInt8:
                    return NativeMethods.VectorElementType.UInt8;
                case VectorElementTypes.UInt16:
                    return NativeMethods.VectorElementType.UInt16;
                case VectorElementTypes.UInt32:
                    return NativeMethods.VectorElementType.UInt32;
                case VectorElementTypes.Int8:
                    return NativeMethods.VectorElementType.Int8;
                case VectorElementTypes.Int16:
                    return NativeMethods.VectorElementType.Int16;
                case VectorElementTypes.Int32:
                    return NativeMethods.VectorElementType.Int32;
                case VectorElementTypes.Float:
                    return NativeMethods.VectorElementType.Float;
                case VectorElementTypes.Double:
                    return NativeMethods.VectorElementType.Double;
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }

        internal static ImageTypes ToImageTypes(this MatrixElementTypes matrixElementTypes)
        {
            switch (matrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    return ImageTypes.UInt8;
                case MatrixElementTypes.UInt16:
                    return ImageTypes.UInt16;
                //case MatrixElementTypes.UInt32:
                //    return ImageTypes.UInt32;
                //case MatrixElementTypes.Int8:
                //    return ImageTypes.Int8;
                //case MatrixElementTypes.Int16:
                //    return ImageTypes.Int16;
                case MatrixElementTypes.Int32:
                    return ImageTypes.Int32;
                case MatrixElementTypes.Float:
                    return ImageTypes.Float;
                case MatrixElementTypes.Double:
                    return ImageTypes.Double;
                case MatrixElementTypes.RgbPixel:
                    return ImageTypes.RgbPixel;
                case MatrixElementTypes.RgbAlphaPixel:
                    return ImageTypes.HsiPixel;
                case MatrixElementTypes.HsiPixel:
                    return ImageTypes.HsiPixel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matrixElementTypes), matrixElementTypes, null);
            }
        }

        internal static NativeMethods.InterpolationTypes ToNativeInterpolationTypes(this InterpolationTypes interpolationTypes)
        {
            switch (interpolationTypes)
            {
                case InterpolationTypes.NearestNeighbor:
                    return NativeMethods.InterpolationTypes.NearestNeighbor;
                case InterpolationTypes.Bilinear:
                    return NativeMethods.InterpolationTypes.Bilinear;
                case InterpolationTypes.Quadratic:
                    return NativeMethods.InterpolationTypes.Quadratic;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interpolationTypes), interpolationTypes, null);
            }
        }

        internal static NativeMethods.PointMappingTypes GetNativePointMappingTypes(this PointTransformBase pointTransform)
        {
            if (pointTransform is PointRotator)
                return NativeMethods.PointMappingTypes.Rotator;
            if (pointTransform is PointTransform)
                return NativeMethods.PointMappingTypes.Transform;
            if (pointTransform is PointTransformAffine)
                return NativeMethods.PointMappingTypes.TransformAffine;
            if (pointTransform is PointTransformProjective)
                return NativeMethods.PointMappingTypes.TransformProjective;

            throw new ArgumentOutOfRangeException(nameof(pointTransform));
        }

        internal static NativeMethods.MlpKernelType ToNativeMlpKernelType(this MultilayerPerceptronKernelType type)
        {
            switch (type)
            {
                case MultilayerPerceptronKernelType.Kernel1:
                    return NativeMethods.MlpKernelType.Kernel1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        internal static NativeMethods.RunningStatsType ToRunningStatsType(this RunningStatsType type)
        {
            switch (type)
            {
                case RunningStatsType.Float:
                    return NativeMethods.RunningStatsType.Float;
                case RunningStatsType.Double:
                    return NativeMethods.RunningStatsType.Double;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        internal static NativeMethods.ImagePixelType ToImagePixelType(this ImagePixelFormat type)
        {
            switch (type)
            {
                case ImagePixelFormat.Bgr:
                    return NativeMethods.ImagePixelType.Bgr;
                case ImagePixelFormat.Bgra:
                    return NativeMethods.ImagePixelType.Bgra;
                case ImagePixelFormat.Rgb:
                    return NativeMethods.ImagePixelType.Rgb;
                case ImagePixelFormat.Rgba:
                    return NativeMethods.ImagePixelType.Rgba;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }

}
