// AForge Direct Show Library
// AForge.NET framework
//
// Copyright © Andrew Kirillov, 2020
// andrew.kirillov@gmail.com
//

namespace AForge.Video.DirectShow.Internals
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// This interface enumerates a pin's preferred media types.
    /// </summary>
    /// 
    [ComImport,
    Guid( "89C31040-846B-11CE-97D3-00AA0055595A" ),
    InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
    internal interface IEnumMediaTypes
    {
        /// <summary>
        /// Retrieves a specified number of media types.
        /// </summary>
        /// 
        /// <param name="cMediaTypes">The number of media types to retrieve.</param>
        /// <param name="mediaTypes">Array of media types to fill in.</param>
        /// <param name="typesFetched">Receives the number of media types returned.</param>
        /// 
        /// <returns>Return's <b>HRESULT</b> error code.</returns>
        /// 
        [PreserveSig]
        int Next( [In] int cMediaTypes,
            [In, Out, MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( EMTMarshaler ), SizeParamIndex = 0 )] AMMediaType[] mediaTypes,
            [Out] out int typesFetched );

        /// <summary>
        /// Skips over a specified number of media types.
        /// </summary>
        /// 
        /// <param name="cMediaTypes">Number of media types to skip.</param>
        /// 
        /// <returns>Return's <b>HRESULT</b> error code.</returns>
        /// 
        [PreserveSig]
        int Skip( [In] int cMediaTypes );

        /// <summary>
        /// Resets the enumeration sequence to the beginning.
        /// </summary>
        /// 
        /// <returns>Return's <b>HRESULT</b> error code.</returns>
        /// 
        [PreserveSig]
        int Reset( );

        /// <summary>
        /// makes a copy of the enumerator.
        /// </summary>
        /// 
        /// <param name="enumMediaTypes">Clone of the enumerator</param>
        /// 
        /// <returns>Return's <b>HRESULT</b> error code.</returns>
        /// 
        [PreserveSig]
        int Clone( [Out] out IEnumPins enumMediaTypes );
    }
}