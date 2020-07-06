using System;
using System.Collections.Generic;
using System.Text;

namespace AForge.Video.DirectShow
{

    /// <summary>
    /// From VideoProcAmpProperty
    /// </summary>
    public enum VideoProcAmpProperty
    {
        /// <summary>
        /// 
        /// </summary>
        Brightness,

        /// <summary>
        /// 
        /// </summary>
        Contrast,

        /// <summary>
        /// 
        /// </summary>
        Hue,

        /// <summary>
        /// 
        /// </summary>
        Saturation,

        /// <summary>
        /// 
        /// </summary>
        Sharpness,

        /// <summary>
        /// 
        /// </summary>
        Gamma,

        /// <summary>
        /// 
        /// </summary>
        ColorEnable,

        /// <summary>
        /// 
        /// </summary>
        WhiteBalance,

        /// <summary>
        /// 
        /// </summary>
        BacklightCompensation,

        /// <summary>
        /// 
        /// </summary>
        Gain
    }
    
    /// <summary>
    /// From VideoProcAmpFlags
    /// </summary>
    [Flags]
    public enum VideoProcAmpFlags
    {
        /// <summary>
        /// No control flag.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Auto control Flag.
        /// </summary>
        Auto = 0x0001,
        /// <summary>
        /// Manual control Flag.
        /// </summary>
        Manual = 0x0002
    }
}
