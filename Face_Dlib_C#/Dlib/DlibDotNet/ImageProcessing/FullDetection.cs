﻿using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class FullDetection : DlibObject
    {

        #region Constructors

        internal FullDetection(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public double DetectionConfidence
        {
            get
            {
                this.ThrowIfDisposed();
                var detectionConfidence = NativeMethods.full_detection_get_detection_confidence(this.NativePtr);
                return detectionConfidence;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.full_detection_set_detection_confidence(this.NativePtr, value);
            }
        }

        public FullObjectDetection Rect
        {
            get
            {
                this.ThrowIfDisposed();
                var rect = NativeMethods.full_detection_get_rect(this.NativePtr);
                return new FullObjectDetection(rect);
            }
            set
            {
                this.ThrowIfDisposed();

                if (value == null)
                    throw new ArgumentException(nameof(value));

                NativeMethods.full_detection_set_rect(this.NativePtr, value.NativePtr);
            }
        }

        public ulong WeightIndex
        {
            get
            {
                this.ThrowIfDisposed();
                var weightIndex = NativeMethods.full_detection_get_weight_index(this.NativePtr);
                return weightIndex;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.full_detection_set_weight_index(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.full_detection_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }
}
