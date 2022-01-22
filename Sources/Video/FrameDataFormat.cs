// AForge Video Library
// AForge.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright © AForge.NET, 2005-2011
// contacts@aforgenet.com
//

namespace AForge.Video
{
    using System.Collections.Generic;
    using ImagingPixelFormat = System.Drawing.Imaging.PixelFormat;

    /// <summary>
    /// Format for image data representation.
    /// </summary>
    /// 
    public enum FrameDataFormat
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// RGB, 1 bit per pixel (bpp), palettized.
        /// </summary>
        RGB1,
        /// <summary>
        /// RGB, 4 bpp, palettized.
        /// </summary>
        RGB4,
        /// <summary>
        /// RGB, 8 bpp.
        /// </summary>
        RGB8,
        /// <summary>
        /// RGB 555, 16 bpp.
        /// </summary>
        RGB555,
        /// <summary>
        /// RGB 565, 16 bpp.
        /// </summary>
        RGB565,
        /// <summary>
        /// RGB, 24 bpp.
        /// </summary>
        RGB24,
        /// <summary>
        /// RGB, 32 bpp, no alpha channel.
        /// </summary>
        RGB32,

        /// <summary>
        /// YUV 4:2:2.
        /// </summary>
        YUYV,
        /// <summary>
        /// YUV 4:2:2.
        /// </summary>
        UYVY,
        /// <summary>
        /// YUV 4:2:2.
        /// </summary>
        YUY2,
        /// <summary>
        /// YUV 4:2:2.
        /// </summary>
        IYUV,

        /// <summary>
        /// Consumer DV.
        /// </summary>
        DVSD,
        /// <summary>
        /// Motion JPEG.
        /// </summary>
        MJPG
    }

    /// <summary>
    /// Some internal utilities for handling frame data formats.
    /// </summary>
    /// 
    public static class FrameDataFormatUtils
    {
        private static readonly Dictionary<ImagingPixelFormat, FrameDataFormat> lookup;
        private static readonly Dictionary<FrameDataFormat, ImagingPixelFormat> reverseLookup;

        static FrameDataFormatUtils()
        {
            lookup = new Dictionary<ImagingPixelFormat, FrameDataFormat>()
            {
                { ImagingPixelFormat.Format1bppIndexed, FrameDataFormat.RGB1 },
                { ImagingPixelFormat.Format4bppIndexed, FrameDataFormat.RGB4 },
                { ImagingPixelFormat.Format16bppRgb555, FrameDataFormat.RGB555 },
                { ImagingPixelFormat.Format16bppRgb565, FrameDataFormat.RGB565 },
                { ImagingPixelFormat.Format8bppIndexed, FrameDataFormat.RGB8 },
                { ImagingPixelFormat.Format24bppRgb,    FrameDataFormat.RGB24 },
                { ImagingPixelFormat.Format32bppArgb,   FrameDataFormat.RGB32 },
            };

            reverseLookup = new Dictionary<FrameDataFormat, ImagingPixelFormat>();

            foreach (KeyValuePair<ImagingPixelFormat, FrameDataFormat> pair in lookup)
            {
                reverseLookup.Add(pair.Value, pair.Key);
            }
        }

        /// <summary>
        /// Converts System.Drawing.Imaging.PixelFormat to AForge.Video.ImageDataFormat.
        /// </summary>
        /// 
        /// <param name="pixelFormat">Pixel format.</param>
        /// 
        /// <returns>Returns corresponding AForge.Video.ImageDataFormat if possible. Otherwise it returns <b>ImageDataFormat.Unknown</b>.</returns>
        /// 
        public static FrameDataFormat FromPixelFormat(ImagingPixelFormat pixelFormat)
        {
            FrameDataFormat imageDataFormat;

            if (lookup.TryGetValue(pixelFormat, out imageDataFormat))
                return imageDataFormat;
            else
                return FrameDataFormat.Unknown;
        }

        /// <summary>
        /// Converts AForge.Video.ImageDataFormat to System.Drawing.Imaging.PixelFormat.
        /// </summary>
        /// 
        /// <param name="dataFormat">Data format.</param>
        /// 
        /// <returns>Returns corresponding System.Drawing.Imaging.PixelFormat if possible. Otherwise it returns <b>PixelFormat.Undefined</b>.</returns>
        /// 
        public static ImagingPixelFormat ToPixelFormat(FrameDataFormat dataFormat)
        {
            ImagingPixelFormat pixelFormat;

            if (reverseLookup.TryGetValue(dataFormat, out pixelFormat))
                return pixelFormat;
            else
                return ImagingPixelFormat.Undefined;
        }
    }
}
