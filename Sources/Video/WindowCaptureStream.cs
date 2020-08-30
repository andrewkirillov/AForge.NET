using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace AForge.Video
{
    /// <summary>
    /// Window capture video source.
    /// </summary>
    /// 
    /// <remarks><para>The video source constantly captures the target window.</para>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// // set NewFrame event handler
    /// stream.NewFrame += new NewFrameEventHandler( video_NewFrame );
    /// 
    /// // start the video source
    /// stream.Start( );
    /// 
    /// // ...
    /// // signal to stop
    /// stream.SignalToStop( );
    /// // ...
    /// 
    /// private void video_NewFrame( object sender, NewFrameEventArgs eventArgs )
    /// {
    ///     // get new frame
    ///     Bitmap bitmap = eventArgs.Frame;
    ///     // process the frame
    /// }
    /// </code>
    /// </remarks>
    /// 
    public class WindowCaptureStream : IVideoSource
    {
        private IntPtr hwnd;
        private Rectangle region;

        // frame interval in milliseconds
        private int frameInterval = 100;

        private Thread thread = null;
        private ManualResetEvent stopEvent = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowCaptureStream"/> class.
        /// </summary>
        /// 
        /// <param name="hWnd">Window to capture.</param>
        /// 
        public WindowCaptureStream(IntPtr hWnd)
        {
            hwnd = hWnd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowCaptureStream"/> class.
        /// </summary>
        /// 
        /// <param name="hWnd">Window to capture.</param>
        /// <param name="region">Window's rectangle to capture</param>
        /// 
        public WindowCaptureStream(IntPtr hWnd, Rectangle region) : this(hWnd)
        {
            this.region = region;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowCaptureStream"/> class.
        /// </summary>
        /// 
        /// <param name="hWnd">Window to capture.</param>
        /// <param name="region">Window's rectangle to capture</param>
        /// <param name="frameInterval">Time interval between making screen shots, ms.</param>
        /// 
        public WindowCaptureStream(IntPtr hWnd, Rectangle region, int frameInterval) : this(hWnd, region)
        {
            this.frameInterval = frameInterval;
        }

        /// <summary>
        /// Received bytes count.
        /// </summary>
        /// 
        /// <remarks><para><note>The property is not implemented for this video source and always returns 0.</note></para></remarks>
        /// 
        public long BytesReceived { get { return 0; } }

        private int framesReceived;
        /// <summary>
        /// Received frames count.
        /// </summary>
        /// 
        /// <remarks>Number of frames the video source provided from the moment of the last
        /// access to the property.
        /// </remarks>
        /// 
        public int FramesReceived
        {
            get
            {
                int frames = framesReceived;
                framesReceived = 0;
                return frames;
            }
        }

        /// <summary>
        /// State of the video source.
        /// </summary>
        /// 
        /// <remarks>Current state of video source object - running or not.</remarks>
        /// 
        public bool IsRunning
        {
            get
            {
                if (thread != null)
                {
                    // check thread status
                    if (thread.Join(0) == false)
                        return true;

                    // the thread is not running, free resources
                    Free();
                }
                return false;
            }
        }

        /// <summary>
        /// Video source.
        /// </summary>
        /// 
        public string Source { get { return "Window Capture"; } }

        /// <summary>
        /// New frame event.
        /// </summary>
        /// 
        /// <remarks><para>Notifies clients about new available frame from video source.</para>
        /// 
        /// <para><note>Since video source may have multiple clients, each client is responsible for
        /// making a copy (cloning) of the passed video frame, because the video source disposes its
        /// own original copy after notifying of clients.</note></para>
        /// </remarks>
        /// 
        public event NewFrameEventHandler NewFrame;

        /// <summary>
        /// Video playing finished event.
        /// </summary>
        /// 
        /// <remarks><para>This event is used to notify clients that the video playing has finished.</para>
        /// </remarks>
        /// 
        public event PlayingFinishedEventHandler PlayingFinished;

        /// <summary>
        /// Video source error event.
        /// </summary>
        /// 
        /// <remarks>This event is used to notify clients about any type of errors occurred in
        /// video source object, for example internal exceptions.</remarks>
        /// 
        public event VideoSourceErrorEventHandler VideoSourceError;

        /// <summary>
        /// Signal video source to stop its work.
        /// </summary>
        /// 
        /// <remarks>Signals video source to stop its background thread, stop to
        /// provide new frames and free resources.</remarks>
        /// 
        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent.Set();
            }
        }

        /// <summary>
        /// Start video source.
        /// </summary>
        /// 
        /// <remarks>Starts video source and return execution to caller. Video source
        /// object creates background thread and notifies about new frames with the
        /// help of <see cref="NewFrame"/> event.</remarks>
        /// 
        /// <exception cref="ArgumentException">Video source is not specified.</exception>
        /// 
        public void Start()
        {
            if (!IsRunning)
            {
                framesReceived = 0;

                // create events
                stopEvent = new ManualResetEvent(false);

                // create and start new thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Name = Source; // mainly for debugging
                thread.Start();
            }
        }

        /// <summary>
        /// Stop video source.
        /// </summary>
        /// 
        /// <remarks><para>Stops video source aborting its thread.</para>
        /// 
        /// <para><note>Since the method aborts background thread, its usage is highly not preferred
        /// and should be done only if there are no other options. The correct way of stopping camera
        /// is <see cref="SignalToStop">signaling it stop</see> and then
        /// <see cref="WaitForStop">waiting</see> for background thread's completion.</note></para>
        /// </remarks>
        /// 
        public void Stop()
        {
            if (this.IsRunning)
            {
                //stopEvent.Set();
                //thread.Abort();
                SignalToStop();
                WaitForStop();
            }
        }

        /// <summary>
        /// Wait for video source has stopped.
        /// </summary>
        /// 
        /// <remarks>Waits for source stopping after it was signalled to stop using
        /// <see cref="SignalToStop"/> method.</remarks>
        /// 
        public void WaitForStop()
        {
            if (thread != null)
            {
                // wait for thread stop
                thread.Join();

                Free();
            }
        }

        /// <summary>
        /// Free resource.
        /// </summary>
        /// 
        private void Free()
        {
            thread = null;

            // release events
            stopEvent.Close();
            stopEvent = null;
        }

        private void WorkerThread()
        {
            int width = region.Width;
            int height = region.Height;
            int x = region.Location.X;
            int y = region.Location.Y;

            Bitmap screenshot = new Bitmap(width, height);

            var fromHwnd = Graphics.FromHwnd(hwnd);
            var fromImage = Graphics.FromImage(screenshot);

            // download start time and duration
            DateTime start;
            TimeSpan span;

            while (!stopEvent.WaitOne(0, false))
            {
                // set dowbload start time
                start = DateTime.Now;

                try
                {
                    IntPtr hdc_screen = fromHwnd.GetHdc();
                    IntPtr hdc_screenshot = fromImage.GetHdc();

                    BitBlt(hdc_screenshot, 0, 0, width, height, hdc_screen, x, y, (int)CopyPixelOperation.SourceCopy);

                    fromImage.ReleaseHdc(hdc_screenshot);
                    fromHwnd.ReleaseHdc(hdc_screen);

                    NewFrame?.Invoke(this, new NewFrameEventArgs(screenshot));

                    // wait for a while ?
                    if (frameInterval > 0)
                    {
                        // get download duration
                        span = DateTime.Now.Subtract(start);

                        // miliseconds to sleep
                        int msec = frameInterval - (int)span.TotalMilliseconds;

                        if ((msec > 0) && (stopEvent.WaitOne(msec, false)))
                            break;
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception exception)
                {
                    // provide information to clients
                    VideoSourceError?.Invoke(this, new VideoSourceErrorEventArgs(exception.Message));
                    // wait for a while before the next try
                    Thread.Sleep(250);
                }

                // need to stop ?
                if (stopEvent.WaitOne(0, false))
                    break;
            }

            screenshot.Dispose();
            fromHwnd.Dispose();
            fromImage.Dispose();

            PlayingFinished?.Invoke(this, ReasonToFinishPlaying.StoppedByUser);
        }

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);
    }
}
