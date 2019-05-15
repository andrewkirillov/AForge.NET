// AForge Direct Show Library
// AForge.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright © AForge.NET, 2009-2013
// contacts@aforgenet.com
//

namespace AForge.Video.DirectShow.Internals
{
    using System;
    using System.Runtime.InteropServices;
    using System.Drawing;

    // PIN_DIRECTION

    /// <summary>
    /// This enumeration indicates a pin's direction.
    /// </summary>
    /// 
    [ComVisible( false )]
    internal enum PinDirection
    {
        /// <summary>
        /// Input pin.
        /// </summary>
        Input,

        /// <summary>
        /// Output pin.
        /// </summary>
        Output
    }

    // AM_MEDIA_TYPE

    /// <summary>
    /// The structure describes the format of a media sample.
    /// </summary>
    /// 
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential )]
    internal class AMMediaType : IDisposable
    {
        /// <summary>
        /// Globally unique identifier (GUID) that specifies the major type of the media sample.
        /// </summary>
        public Guid MajorType;

        /// <summary>
        /// GUID that specifies the subtype of the media sample.
        /// </summary>
        public Guid SubType;

        /// <summary>
        /// If <b>true</b>, samples are of a fixed size.
        /// </summary>
        [MarshalAs( UnmanagedType.Bool )]
        public bool FixedSizeSamples = true;

        /// <summary>
        /// If <b>true</b>, samples are compressed using temporal (interframe) compression.
        /// </summary>
        [MarshalAs( UnmanagedType.Bool )]
        public bool TemporalCompression;

        /// <summary>
        /// Size of the sample in bytes. For compressed data, the value can be zero.
        /// </summary>
        public int SampleSize = 1;

        /// <summary>
        /// GUID that specifies the structure used for the format block.
        /// </summary>
        public Guid FormatType;

        /// <summary>
        /// Not used.
        /// </summary>
        public IntPtr unkPtr;

        /// <summary>
        /// Size of the format block, in bytes.
        /// </summary>
        public int FormatSize;

        /// <summary>
        /// Pointer to the format block.
        /// </summary>
        public IntPtr FormatPtr;

        /// <summary>
        /// Destroys the instance of the <see cref="AMMediaType"/> class.
        /// </summary>
        /// 
        ~AMMediaType( )
        {
            Dispose( false );
        }

        /// <summary>
        /// Dispose the object.
        /// </summary>
        ///
        public void Dispose( )
        {
            Dispose( true );
            // remove me from the Finalization queue 
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Dispose the object
        /// </summary>
        /// 
        /// <param name="disposing">Indicates if disposing was initiated manually.</param>
        /// 
        protected virtual void Dispose( bool disposing )
        {
            if ( ( FormatSize != 0 ) && ( FormatPtr != IntPtr.Zero ) )
            {
                Marshal.FreeCoTaskMem( FormatPtr );
                FormatSize = 0;
            }

            if ( unkPtr != IntPtr.Zero )
            {
                Marshal.Release( unkPtr );
                unkPtr = IntPtr.Zero;
            }
        }
    }


    // PIN_INFO

    /// <summary>
    /// The structure contains information about a pin.
    /// </summary>
    /// 
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode )]
    internal struct PinInfo
    {
        /// <summary>
        /// Owning filter.
        /// </summary>
        public IBaseFilter Filter;

        /// <summary>
        /// Direction of the pin.
        /// </summary>
        public PinDirection Direction;

        /// <summary>
        /// Name of the pin.
        /// </summary>
        [MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
        public string Name;
    }

    // FILTER_INFO
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode )]
    internal struct FilterInfo
    {
        /// <summary>
        /// Filter's name.
        /// </summary>
        [MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
        public string Name;

        /// <summary>
        /// Owning graph.
        /// </summary>
        public IFilterGraph FilterGraph;
    }

    // VIDEOINFOHEADER

    /// <summary>
    /// The structure describes the bitmap and color information for a video image.
    /// </summary>
    /// 
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential )]
    internal struct VideoInfoHeader
    {
        /// <summary>
        /// <see cref="RECT"/> structure that specifies the source video window.
        /// </summary>
        public RECT SrcRect;

        /// <summary>
        /// <see cref="RECT"/> structure that specifies the destination video window.
        /// </summary>
        public RECT TargetRect;

        /// <summary>
        /// Approximate data rate of the video stream, in bits per second.
        /// </summary>
        public int BitRate;

        /// <summary>
        /// Data error rate, in bit errors per second.
        /// </summary>
        public int BitErrorRate;

        /// <summary>
        /// The desired average display time of the video frames, in 100-nanosecond units.
        /// </summary>
        public long AverageTimePerFrame;

        /// <summary>
        /// <see cref="BitmapInfoHeader"/> structure that contains color and dimension information for the video image bitmap.
        /// </summary>
        public BitmapInfoHeader BmiHeader;
    }

    // VIDEOINFOHEADER2

    /// <summary>
    /// The structure describes the bitmap and color information for a video image (v2).
    /// </summary>
    /// 
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential )]
    internal struct VideoInfoHeader2
    {
        /// <summary>
        /// <see cref="RECT"/> structure that specifies the source video window.
        /// </summary>
        public RECT SrcRect;

        /// <summary>
        /// <see cref="RECT"/> structure that specifies the destination video window.
        /// </summary>
        public RECT TargetRect;

        /// <summary>
        /// Approximate data rate of the video stream, in bits per second.
        /// </summary>
        public int BitRate;

        /// <summary>
        /// Data error rate, in bit errors per second.
        /// </summary>
        public int BitErrorRate;

        /// <summary>
        /// The desired average display time of the video frames, in 100-nanosecond units.
        /// </summary>
        public long AverageTimePerFrame;

        /// <summary>
        /// Flags that specify how the video is interlaced.
        /// </summary>
        public int InterlaceFlags;

        /// <summary>
        /// Flag set to indicate that the duplication of the stream should be restricted.
        /// </summary>
        public int CopyProtectFlags;

        /// <summary>
        /// The X dimension of picture aspect ratio.
        /// </summary>
        public int PictAspectRatioX;

        /// <summary>
        /// The Y dimension of picture aspect ratio.
        /// </summary>
        public int PictAspectRatioY;

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        public int Reserved1;

        /// <summary>
        /// Reserved for future use. 
        /// </summary>
        public int Reserved2;

        /// <summary>
        /// <see cref="BitmapInfoHeader"/> structure that contains color and dimension information for the video image bitmap.
        /// </summary>
        public BitmapInfoHeader BmiHeader;
    }

    /// <summary>
    /// The structure contains information about the dimensions and color format of a device-independent bitmap (DIB).
    /// </summary>
    /// 
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential, Pack = 2 )]
    internal struct BitmapInfoHeader
    {
        /// <summary>
        /// Specifies the number of bytes required by the structure.
        /// </summary>
        public int Size;

        /// <summary>
        /// Specifies the width of the bitmap.
        /// </summary>
        public int Width;

        /// <summary>
        /// Specifies the height of the bitmap, in pixels.
        /// </summary>
        public int Height;

        /// <summary>
        /// Specifies the number of planes for the target device. This value must be set to 1.
        /// </summary>
        public short Planes;

        /// <summary>
        /// Specifies the number of bits per pixel.
        /// </summary>
        public short BitCount;

        /// <summary>
        /// If the bitmap is compressed, this member is a <b>FOURCC</b> the specifies the compression.
        /// </summary>
        public int Compression;

        /// <summary>
        /// Specifies the size, in bytes, of the image.
        /// </summary>
        public int ImageSize;

        /// <summary>
        /// Specifies the horizontal resolution, in pixels per meter, of the target device for the bitmap.
        /// </summary>
        public int XPelsPerMeter;

        /// <summary>
        /// Specifies the vertical resolution, in pixels per meter, of the target device for the bitmap.
        /// </summary>
        public int YPelsPerMeter;

        /// <summary>
        /// Specifies the number of color indices in the color table that are actually used by the bitmap.
        /// </summary>
        public int ColorsUsed;

        /// <summary>
        /// Specifies the number of color indices that are considered important for displaying the bitmap.
        /// </summary>
        public int ColorsImportant;
    }

    // RECT

    /// <summary>
    /// The structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
    /// </summary>
    /// 
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential )]
    internal struct RECT
    {
        /// <summary>
        /// Specifies the x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Left;

        /// <summary>
        /// Specifies the y-coordinate of the upper-left corner of the rectangle. 
        /// </summary>
        public int Top;

        /// <summary>
        /// Specifies the x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Right;

        /// <summary>
        /// Specifies the y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Bottom;
    }

    // CAUUID

    /// <summary>
    /// The CAUUID structure is a Counted Array of UUID or GUID types.
    /// </summary>
    /// 
    [ComVisible( false ),
    StructLayout( LayoutKind.Sequential )]
    internal struct CAUUID
    {
        /// <summary>
        /// Size of the array pointed to by <b>pElems</b>.
        /// </summary>
        public int cElems;

        /// <summary>
        /// Pointer to an array of UUID values, each of which specifies UUID.
        /// </summary>
        public IntPtr pElems;

        /// <summary>
        /// Performs manual marshaling of <b>pElems</b> to retrieve an array of Guid objects.
        /// </summary>
        /// 
        /// <returns>A managed representation of <b>pElems</b>.</returns>
        /// 
        public Guid[] ToGuidArray( )
        {
            Guid[] retval = new Guid[cElems];

            for ( int i = 0; i < cElems; i++ )
            {
                IntPtr ptr = new IntPtr( pElems.ToInt64( ) + i * Marshal.SizeOf( typeof( Guid ) ) );
                retval[i] = (Guid) Marshal.PtrToStructure( ptr, typeof( Guid ) );
            }

            return retval;
        }
    }

    /// <summary>
    /// Enumeration of DirectShow event codes.
    /// </summary>
    internal enum DsEvCode
    {
        None,
        Complete   = 0x01,      // EC_COMPLETE
		ErrorAbort = 0x03,      // EC_ErrorAbort
        DeviceLost = 0x1F,      // EC_DEVICE_LOST
        //(...) not yet interested in other events
    }
	
	
	internal class DsEvError
	{
		public static Dictionary<uint, string> Map = new Dictionary<uint, string>()
		{
			//VFW_S_NO_MORE_ITEMS
			{0x00040103, "Reached the end of the list; no more items in the list. (Filter developers: The CBasePin::GetMediaType method is expected to return this value.)"},
			//VFW_S_DUPLICATE_NAME
			{0x0004022D,"An attempt to add a filter with a duplicate name succeeded with a modified name."},
			//VFW_S_STATE_INTERMEDIATE
			{0x00040237,"The state transition is not complete."},
			//VFW_S_PARTIAL_RENDER
			{0x00040242,"Some of the streams are in an unsupported format."},
			//VFW_S_SOME_DATA_IGNORED
			{0x00040245,"The file contained some property settings that were not used."},
			//VFW_S_CONNECTIONS_DEFERRED
			{0x00040246,"Some connections failed and were deferred."},
			//VFW_S_RESOURCE_NOT_NEEDED
			{0x00040250,"The resource specified is no longer needed."},
			//VFW_S_MEDIA_TYPE_IGNORED
			{0x00040254,"A GraphEdit (.grf) file was loaded successfully, but at least two pins were connected using a different media type than the media type stored in the GraphEdit file."},
			//VFW_S_VIDEO_NOT_RENDERED
			{0x00040257,"Cannot play back the video stream: could not find a suitable renderer."},
			//VFW_S_AUDIO_NOT_RENDERED
			{0x00040258,"Cannot play back the audio stream: could not find a suitable renderer."},
			//VFW_S_RPZA
			{0x0004025A,"Cannot play back the video stream: format 'RPZA' is not supported."},
			//VFW_S_ESTIMATED
			{0x00040260,"The value returned had to be estimated. Its accuracy can't be guaranteed."},
			//VFW_S_RESERVED
			{0x00040263,"This success code is reserved for internal purposes within DirectShow."},
			//VFW_S_STREAM_OFF
			{0x00040267,"The stream was turned off."},
			//VFW_S_CANT_CUE
			{0x00040268,"The filter is active, but cannot deliver data. See IMediaFilter::GetState."},
			//VFW_S_NOPREVIEWPIN
			{0x0004027E,"Preview was rendered throught the Smart Tee filter, because the capture filter does not have a preview pin."},
			//VFW_S_DVD_NON_ONE_SEQUENTIAL
			{0x00040280,"The current title is not a sequential set of chapters (PGC), so the timing information might not be continuous."},
			//VFW_S_DVD_CHANNEL_CONTENTS_NOT_AVAILABLE
			{0x0004028C,"The audio stream does not contain enough information to determine the contents of each channel."},
			//VFW_S_DVD_NOT_ACCURATE
			{0x0004028D,"The seek operation on the DVD was not frame accurate."},
			//VFW_E_INVALIDMEDIATYPE
			{0x80040200,"The specified media type is invalid."},
			//VFW_E_INVALIDSUBTYPE
			{0x80040201,"The specified media subtype is invalid."},
			//VFW_E_NEED_OWNER
			{0x80040202,"This object can only be created as an aggregated object."},
			//VFW_E_ENUM_OUT_OF_SYNC
			{0x80040203,"The state of the enumerated object has changed and is now inconsistent with the state of the enumerator. Discard any data obtained from previous calls to the enumerator and then update the enumerator by calling the enumerator's Reset method."},
			//VFW_E_ALREADY_CONNECTED
			{0x80040204,"At least one of the pins involved in the operation is already connected."},
			//VFW_E_FILTER_ACTIVE
			{0x80040205,"This operation cannot be performed because the filter is active."},
			//VFW_E_NO_TYPES
			{0x80040206,"One of the specified pins supports no media types."},
			//VFW_E_NO_ACCEPTABLE_TYPES
			{0x80040207,"There is no common media type between these pins."},
			//VFW_E_INVALID_DIRECTION
			{0x80040208,"Two pins of the same direction cannot be connected."},
			//VFW_E_NOT_CONNECTED
			{0x80040209,"The operation cannot be performed because the pins are not connected."},
			//VFW_E_NO_ALLOCATOR
			{0x8004020A,"No sample buffer allocator is available."},
			//VFW_E_RUNTIME_ERROR
			{0x8004020B,"A run-time error occurred."},
			//VFW_E_BUFFER_NOTSET
			{0x8004020C,"No buffer space has been set."},
			//VFW_E_BUFFER_OVERFLOW
			{0x8004020D,"The buffer is not big enough."},
			//VFW_E_BADALIGN
			{0x8004020E,"An invalid alignment was specified."},
			//VFW_E_ALREADY_COMMITTED
			{0x8004020F,"The allocator was not committed. See IMemAllocator::Commit."},
			//VFW_E_BUFFERS_OUTSTANDING
			{0x80040210,"One or more buffers are still active."},
			//VFW_E_NOT_COMMITTED
			{0x80040211,"Cannot allocate a sample when the allocator is not active."},
			//VFW_E_SIZENOTSET
			{0x80040212,"Cannot allocate memory because no size has been set."},
			//VFW_E_NO_CLOCK
			{0x80040213,"Cannot lock for synchronization because no clock has been defined."},
			//VFW_E_NO_SINK
			{0x80040214,"Quality messages could not be sent because no quality sink has been defined."},
			//VFW_E_NO_INTERFACE
			{0x80040215,"A required interface has not been implemented."},
			//VFW_E_NOT_FOUND
			{0x80040216,"An object or name was not found."},
			//VFW_E_CANNOT_CONNECT
			{0x80040217,"No combination of intermediate filters could be found to make the connection."},
			//VFW_E_CANNOT_RENDER
			{0x80040218,"No combination of filters could be found to render the stream."},
			//VFW_E_CHANGING_FORMAT
			{0x80040219,"Could not change formats dynamically."},
			//VFW_E_NO_COLOR_KEY_SET
			{0x8004021A,"No color key has been set."},
			//VFW_E_NOT_OVERLAY_CONNECTION
			{0x8004021B,"Current pin connection is not using the IOverlay transport."},
			//VFW_E_NOT_SAMPLE_CONNECTION
			{0x8004021C,"Current pin connection is not using the IMemInputPin transport."},
			//VFW_E_PALETTE_SET
			{0x8004021D,"Setting a color key would conflict with the palette already set."},
			//VFW_E_COLOR_KEY_SET
			{0x8004021E,"Setting a palette would conflict with the color key already set."},
			//VFW_E_NO_COLOR_KEY_FOUND
			{0x8004021F,"No matching color key is available."},
			//VFW_E_NO_PALETTE_AVAILABLE
			{0x80040220,"No palette is available."},
			//VFW_E_NO_DISPLAY_PALETTE
			{0x80040221,"Display does not use a palette."},
			//VFW_E_TOO_MANY_COLORS
			{0x80040222,"Too many colors for the current display settings."},
			//VFW_E_STATE_CHANGED
			{0x80040223,"The state changed while waiting to process the sample."},
			//VFW_E_NOT_STOPPED
			{0x80040224,"The operation could not be performed because the filter is not stopped."},
			//VFW_E_NOT_PAUSED
			{0x80040225,"The operation could not be performed because the filter is not paused."},
			//VFW_E_NOT_RUNNING
			{0x80040226,"The operation could not be performed because the filter is not running."},
			//VFW_E_WRONG_STATE
			{0x80040227,"The operation could not be performed because the filter is in the wrong state."},
			//VFW_E_START_TIME_AFTER_END
			{0x80040228,"The sample start time is after the sample end time."},
			//VFW_E_INVALID_RECT
			{0x80040229,"The supplied rectangle is invalid."},
			//VFW_E_TYPE_NOT_ACCEPTED
			{0x8004022A,"This pin cannot use the supplied media type."},
			//VFW_E_SAMPLE_REJECTED
			{0x8004022B,"This sample cannot be rendered."},
			//VFW_E_SAMPLE_REJECTED_EOS
			{0x8004022C,"This sample cannot be rendered because the end of the stream has been reached."},
			//VFW_E_DUPLICATE_NAME
			{0x8004022D,"An attempt to add a filter with a duplicate name failed."},
			//VFW_E_TIMEOUT
			{0x8004022E,"A time-out has expired."},
			//VFW_E_INVALID_FILE_FORMAT
			{0x8004022F,"The file format is invalid."},
			//VFW_E_ENUM_OUT_OF_RANGE
			{0x80040230,"The list has already been exhausted."},
			//VFW_E_CIRCULAR_GRAPH
			{0x80040231,"The filter graph is circular."},
			//VFW_E_NOT_ALLOWED_TO_SAVE
			{0x80040232,"Updates are not allowed in this state."},
			//VFW_E_TIME_ALREADY_PASSED
			{0x80040233,"An attempt was made to queue a command for a time in the past."},
			//VFW_E_ALREADY_CANCELLED
			{0x80040234,"The queued command was already canceled."},
			//VFW_E_CORRUPT_GRAPH_FILE
			{0x80040235,"Cannot render the file because it is corrupt."},
			//VFW_E_ADVISE_ALREADY_SET
			{0x80040236,"An IOverlay advise link already exists."},
			//VFW_E_NO_MODEX_AVAILABLE
			{0x80040238,"No full-screen modes are available."},
			//VFW_E_NO_ADVISE_SET
			{0x80040239,"This advise cannot be canceled because it was not successfully set."},
			//VFW_E_NO_FULLSCREEN
			{0x8004023A,"Full-screen mode is not available."},
			//VFW_E_IN_FULLSCREEN_MODE
			{0x8004023B,"Cannot call IVideoWindow methods while in full-screen mode."},
			//VFW_E_UNKNOWN_FILE_TYPE
			{0x80040240,"The media type of this file is not recognized."},
			//VFW_E_CANNOT_LOAD_SOURCE_FILTER
			{0x80040241,"The source filter for this file could not be loaded."},
			//VFW_E_FILE_TOO_SHORT
			{0x80040243,"A file appeared to be incomplete."},
			//VFW_E_INVALID_FILE_VERSION
			{0x80040244,"The file's version number is invalid."},
			//VFW_E_INVALID_CLSID
			{0x80040247,"This file is corrupt: it contains an invalid class identifier."},
			//VFW_E_INVALID_MEDIA_TYPE
			{0x80040248,"This file is corrupt: it contains an invalid media type."},
			//VFW_E_SAMPLE_TIME_NOT_SET
			{0x80040249,"No time stamp has been set for this sample."},
			//VFW_E_MEDIA_TIME_NOT_SET
			{0x80040251,"No media time was set for this sample."},
			//VFW_E_NO_TIME_FORMAT_SET
			{0x80040252,"No media time format was selected."},
			//VFW_E_MONO_AUDIO_HW
			{0x80040253,"Cannot change balance because audio device is monoaural only."},
			//VFW_E_NO_DECOMPRESSOR
			{0x80040255,"Cannot play back the video stream: could not find a suitable decompressor."},
			//VFW_E_NO_AUDIO_HARDWARE
			{0x80040256,"Cannot play back the audio stream: no audio hardware is available, or the hardware is not supported."},
			//VFW_E_RPZA
			{0x80040259,"Cannot play back the video stream: format 'RPZA' is not supported."},
			//VFW_E_PROCESSOR_NOT_SUITABLE
			{0x8004025B,"DirectShow cannot play MPEG movies on this processor."},
			//VFW_E_UNSUPPORTED_AUDIO
			{0x8004025C,"Cannot play back the audio stream: the audio format is not supported."},
			//VFW_E_UNSUPPORTED_VIDEO
			{0x8004025D,"Cannot play back the video stream: the video format is not supported."},
			//VFW_E_MPEG_NOT_CONSTRAINED
			{0x8004025E,"DirectShow cannot play this video stream because it falls outside the constrained standard."},
			//VFW_E_NOT_IN_GRAPH
			{0x8004025F,"Cannot perform the requested function on an object that is not in the filter graph."},
			//VFW_E_NO_TIME_FORMAT
			{0x80040261,"Cannot access the time format on an object."},
			//VFW_E_READ_ONLY
			{0x80040262,"Could not make the connection because the stream is read-only and the filter alters the data."},
			//VFW_E_BUFFER_UNDERFLOW
			{0x80040264,"The buffer is not full enough."},
			//VFW_E_UNSUPPORTED_STREAM
			{0x80040265,"Cannot play back the file: the format is not supported."},
			//VFW_E_NO_TRANSPORT
			{0x80040266,"Pins cannot connect because they don't support the same transport. For example, the upstream filter might require the IAsyncReader interface, while the downstream filter requires IMemInputPin."},
			//VFW_E_BAD_VIDEOCD
			{0x80040269,"The Video CD can't be read correctly by the device or the data is corrupt."},
			//VFW_S_NO_STOP_TIME
			{0x80040270,"The sample had a start time but not a stop time. In this case, the stop time that is returned is set to the start time plus one."},
			//VFW_E_OUT_OF_VIDEO_MEMORY
			{0x80040271,"There is not enough video memory at this display resolution and number of colors. Reducing resolution might help."},
			//VFW_E_VP_NEGOTIATION_FAILED
			{0x80040272,"The video port connection negotiation process has failed."},
			//VFW_E_DDRAW_CAPS_NOT_SUITABLE
			{0x80040273,"Either DirectDraw has not been installed or the video card capabilities are not suitable. Make sure the display is not in 16-color mode."},
			//VFW_E_NO_VP_HARDWARE
			{0x80040274,"No video port hardware is available, or the hardware is not responding."},
			//VFW_E_NO_CAPTURE_HARDWARE
			{0x80040275,"No capture hardware is available, or the hardware is not responding."},
			//VFW_E_DVD_OPERATION_INHIBITED
			{0x80040276,"This user operation is prohibited by DVD content at this time."},
			//VFW_E_DVD_INVALIDDOMAIN
			{0x80040277,"This operation is not permitted in the current domain."},
			//VFW_E_DVD_NO_BUTTON
			{0x80040278,"Requested button is not available."},
			//VFW_E_DVD_GRAPHNOTREADY
			{0x80040279,"DVD-Video playback graph has not been built yet."},
			//VFW_E_DVD_RENDERFAIL
			{0x8004027A,"DVD-Video playback graph building failed."},
			//VFW_E_DVD_DECNOTENOUGH
			{0x8004027B,"DVD-Video playback graph could not be built due to insufficient decoders."},
			//VFW_E_DDRAW_VERSION_NOT_SUITABLE
			{0x8004027C,"The DirectDraw version number is not suitable. Make sure to install DirectX 5 or higher."},
			//VFW_E_COPYPROT_FAILED
			{0x8004027D,"Copy protection could not be enabled."},
			//VFW_E_TIME_EXPIRED
			{0x8004027F,"Seek command timed out."},
			//VFW_E_DVD_WRONG_SPEED
			{0x80040281,"The operation cannot be performed at the current playback speed."},
			//VFW_E_DVD_MENU_DOES_NOT_EXIST
			{0x80040282,"The specified DVD menu does not exist."},
			//VFW_E_DVD_CMD_CANCELLED
			{0x80040283,"The specified command was canceled or no longer exists."},
			//VFW_E_DVD_STATE_WRONG_VERSION
			{0x80040284,"The DVD state information contains the wrong version number."},
			//VFW_E_DVD_STATE_CORRUPT
			{0x80040285,"The DVD state information is corrupted."},
			//VFW_E_DVD_STATE_WRONG_DISC
			{0x80040286,"The DVD state information is from another disc and not the current disc."},
			//VFW_E_DVD_INCOMPATIBLE_REGION
			{0x80040287,"The region is not compatible with the drive."},
			//VFW_E_DVD_NO_ATTRIBUTES
			{0x80040288,"The requested attributes do not exist."},
			//VFW_E_DVD_NO_GOUP_PGC
			{0x80040289,"The operation cannot be performed because no GoUp program chain (PGC) is available."},
			//VFW_E_DVD_LOW_PARENTAL_LEVEL
			{0x8004028A,"The operation is prohibited because the parental level is too low."},
			//VFW_E_DVD_NOT_IN_KARAOKE_MODE
			{0x8004028B,"The DVD Navigator is not in karaoke mode."},
			//VFW_E_FRAME_STEP_UNSUPPORTED
			{0x8004028E,"Frame stepping is not supported."},
			//VFW_E_DVD_STREAM_DISABLED
			{0x8004028F,"The requested stream is disabled."},
			//VFW_E_DVD_TITLE_UNKNOWN
			{0x80040290,"The operation requires a title number, but there is no current title. This error can occur when the DVD Navigator is not in the Title domain or the Video Title Set Menu (VTSM) domain."},
			//VFW_E_DVD_INVALID_DISC
			{0x80040291,"The specified path is not a valid DVD disc."},
			//VFW_E_DVD_NO_RESUME_INFORMATION
			{0x80040292,"The Resume operation could not be completed, because there is no resume information."},
			//VFW_E_PIN_ALREADY_BLOCKED_ON_THIS_THREAD
			{0x80040293,"Pin is already blocked on the calling thread."},
			//VFW_E_PIN_ALREADY_BLOCKED
			{0x80040294,"Pin is already blocked on another thread."},
			//VFW_E_CERTIFICATION_FAILURE
			{0x80040295,"Use of this filter is restricted by a software key. The application must unlock the filter."},
			//VFW_E_VMR_NOT_IN_MIXER_MODE
			{0x80040296,"The Video Mixing Renderer (VMR) is not in mixing mode. Call IVMRFilterConfig::SetNumberOfStreams (VMR-7) or IVMRFilterConfig9::SetNumberOfStreams (VMR-9)."},
			//VFW_E_VMR_NO_AP_SUPPLIED
			{0x80040297,"The application has not yet provided the VMR filter with a valid allocator-presenter object."},
			//VFW_E_VMR_NO_DEINTERLACE_HW
			{0x80040298,"The VMR could not find any de-interlacing hardware on the current display device."},
			//VFW_E_VMR_NO_PROCAMP_HW
			{0x80040299,"The VMR could not find any hardware that supports ProcAmp controls on the current display device."},
			//VFW_E_DVD_VMR9_INCOMPATIBLEDEC
			{0x8004029A,"The hardware decoder uses video port extensions (VPE), which are not compatible with the VMR-9 filter."},
			//VFW_E_NO_COPP_HW
			{0x8004029B,"The current display device does not support Content Output Protection Protocol (COPP); or the VMR has not connected to a display device yet."},
			//VFW_E_BAD_KEY
			{0x800403F2,"A registry entry is corrupt."},
			//VFW_E_DVD_NONBLOCKING
			{0x8004029C,"The DVD navigator cannot complete the requested operation, because another operation is still pending."},
			//VFW_E_DVD_TOO_MANY_RENDERERS_IN_FILTER_GRAPH
			{0x8004029D,"The DVD Navigator cannot build the DVD playback graph because the graph contains more than one video renderer."},
			//VFW_E_DVD_NON_EVR_RENDERER_IN_FILTER_GRAPH
			{0x8004029E,"The DVD Navigator cannot add the Enhanced Video Renderer (EVR) filter to the filter graph because the graph already contains a video renderer."},
			//VFW_E_DVD_RESOLUTION_ERROR
			{0x8004029F,"DVD Video output is not at a proper resolution."},
			//VFW_E_CODECAPI_LINEAR_RANGE
			{0x80040310,"The specified codec parameter has a linear range, not an enumerated list."},
			//VFW_E_CODECAPI_ENUMERATED
			{0x80040311,"The specified codec parameter has an enumerated range of values, not a linear range."},
			//VFW_E_CODECAPI_NO_DEFAULT
			{0x80040313,"The specified codec parameter does not have a default value."},
			//VFW_E_CODECAPI_NO_CURRENT_VALUE
			{0x80040314,"The specified codec parameter does not have a current value."},
			//E_PROP_ID_UNSUPPORTED
			{0x80070490,"The specified property identifier is not supported."},
			//Access denied
			{ 0x80070005,"error (access denied)"},
			//E_PROP_SET_UNSUPPORTED
			{0x80070492,"The specified property set is not supported."},
			//S_WARN_OUTPUTRESET
			{0x00009DD4,"The rendering portion of the graph was deleted. The application must rebuild it."},
			//E_NOTINTREE
			{0x80040400,"The object is not contained in the timeline."},
			//E_RENDER_ENGINE_IS_BROKEN
			{0x80040401,"Operation failed because project was not rendered successfully."},
			//E_MUST_INIT_RENDERER
			{0x80040402,"Render engine has not been initialized."},
			//E_NOTDETERMINED
			{0x80040403,"Cannot determine requested value."},
			//E_NO_TIMELINE
			{0x80040404,"E_NO_TIMELINE"},

		};
	}

    [Flags, ComVisible( false )]
    internal enum AnalogVideoStandard
    {
        None        = 0x00000000,   // This is a digital sensor
        NTSC_M      = 0x00000001,   //        75 IRE Setup
        NTSC_M_J    = 0x00000002,   // Japan,  0 IRE Setup
        NTSC_433    = 0x00000004,
        PAL_B       = 0x00000010,
        PAL_D       = 0x00000020,
        PAL_G       = 0x00000040,
        PAL_H       = 0x00000080,
        PAL_I       = 0x00000100,
        PAL_M       = 0x00000200,
        PAL_N       = 0x00000400,
        PAL_60      = 0x00000800,
        SECAM_B     = 0x00001000,
        SECAM_D     = 0x00002000,
        SECAM_G     = 0x00004000,
        SECAM_H     = 0x00008000,
        SECAM_K     = 0x00010000,
        SECAM_K1    = 0x00020000,
        SECAM_L     = 0x00040000,
        SECAM_L1    = 0x00080000,
        PAL_N_COMBO = 0x00100000    // Argentina
    }

    [Flags, ComVisible( false )]
    internal enum VideoControlFlags
    {
        FlipHorizontal        = 0x0001,
        FlipVertical          = 0x0002,
        ExternalTriggerEnable = 0x0004,
        Trigger               = 0x0008
    }

    [StructLayout( LayoutKind.Sequential ), ComVisible( false )]
    internal class VideoStreamConfigCaps		// VIDEO_STREAM_CONFIG_CAPS
    {
        public Guid                 Guid;
        public AnalogVideoStandard  VideoStandard;
        public Size                 InputSize;
        public Size                 MinCroppingSize;
        public Size                 MaxCroppingSize;
        public int                  CropGranularityX;
        public int                  CropGranularityY;
        public int                  CropAlignX;
        public int                  CropAlignY;
        public Size                 MinOutputSize;
        public Size                 MaxOutputSize;
        public int                  OutputGranularityX;
        public int                  OutputGranularityY;
        public int                  StretchTapsX;
        public int                  StretchTapsY;
        public int                  ShrinkTapsX;
        public int                  ShrinkTapsY;
        public long                 MinFrameInterval;
        public long                 MaxFrameInterval;
        public int                  MinBitsPerSecond;
        public int                  MaxBitsPerSecond;
    }

    /// <summary>
    /// Specifies a filter's state or the state of the filter graph.
    /// </summary>
    internal enum FilterState
    {
        /// <summary>
        /// Stopped. The filter is not processing data.
        /// </summary>
        State_Stopped,

        /// <summary>
        /// Paused. The filter is processing data, but not rendering it.
        /// </summary>
        State_Paused,

        /// <summary>
        /// Running. The filter is processing and rendering data.
        /// </summary>
        State_Running
    }
}
