﻿using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MModRect : DlibObject
    {

        #region Constructors

        public MModRect()
        {
            this.NativePtr = NativeMethods.mmod_rect_new();
        }

        internal MModRect(IntPtr ptr, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public double DetectionConfidence
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_rect_get_detection_confidence(this.NativePtr, out var confidence);
                return confidence;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_rect_set_detection_confidence(this.NativePtr, value);
            }
        }

        public bool Ignore
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_rect_get_ignore(this.NativePtr, out var ignore);
                return ignore;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_rect_set_ignore(this.NativePtr, value);
            }
        }

        public IntPtr Label
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_rect_get_label(this.NativePtr, out var label);

                // ToDo
                //return new StdString(label);
                return label;
            }
            set
            {
                this.ThrowIfDisposed();
                if (value == IntPtr.Zero)
                    throw new ArgumentNullException();

                //value.ThrowIfDisposed();
                //NativeMethods.mmod_rect_set_label(this.NativePtr, value.NativePtr);
            }
        }

        public Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_rect_get_rect(this.NativePtr, out var rect);
                return new Rectangle(rect);
            }
            set
            {
                this.ThrowIfDisposed();

                using (var native = value.ToNative())
                    NativeMethods.mmod_rect_set_rect(this.NativePtr, native.NativePtr);
            }
        }

        #endregion

        #region Methods

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.mmod_rect_delete(this.NativePtr);
        }

        #endregion

        #region Operators

        public static implicit operator Rectangle(MModRect val)
        {
            return val.Rect;
        }

        #endregion

        #endregion

    }

}