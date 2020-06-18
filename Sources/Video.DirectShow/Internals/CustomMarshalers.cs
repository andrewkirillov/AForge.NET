// AForge Direct Show Library
// AForge.NET framework
//
// Copyright © Andrew Kirillov, 2020
// andrew.kirillov@gmail.com
//
// directshow.net library
// directshownet.sourceforge.net
//

namespace AForge.Video.DirectShow.Internals
{
    using System;
    using System.Runtime.InteropServices;

    // Custom marshaller used to interop different Direct Show APIs
    abstract internal class DSMarshaler : ICustomMarshaler
    {
        protected string cookie;
        protected object obj;

        public DSMarshaler( string cookie )
        {
            this.cookie = cookie;
        }

        // Called just before invoking the COM method. The returned IntPtr is what goes on the stack
        // for the COM call. The input arg is the parameter that was passed to the method.
        virtual public IntPtr MarshalManagedToNative( object managedObj )
        {
            this.obj = managedObj;

            // create an appropriately sized buffer, blank it, and send it to the marshaler to make the COM call
            int    size = GetNativeDataSize( ) + 3;
            IntPtr ptr  = Marshal.AllocCoTaskMem( size );

            for ( int x = 0; x < size / 4; x++ )
            {
                Marshal.WriteInt32( ptr, x * 4, 0 );
            }

            return ptr;
        }

        // Called just after invoking the COM method. The IntPtr is the same one that just got returned
        // from MarshalManagedToNative. The return value is unused.
        virtual public object MarshalNativeToManaged( IntPtr ptrNativeData )
        {
            return this.obj;
        }

        // Release the  buffer
        virtual public void CleanUpNativeData( IntPtr ptrNativeData )
        {
            if (ptrNativeData != IntPtr.Zero )
            {
                Marshal.FreeCoTaskMem( ptrNativeData );
            }
        }

        // Release the managed object
        virtual public void CleanUpManagedData( object managedObj )
        {
            this.obj = null;
        }

        // This routine is (apparently) never called by the marshaler.  However it can be useful.
        abstract public int GetNativeDataSize( );

        // GetInstance is called by the marshaler in preparation to doing custom marshaling.  The (optional)
        // cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.

        // It is commented out in this abstract class, but MUST be implemented in derived classes
        // public static ICustomMarshaler GetInstance(string cookie)
    }

    // Custom marshaller used for IEnumMediaTypes.Nex() as C# does not correctly marshal arrays of pointers
    internal class EMTMarshaler : DSMarshaler
    {
        public EMTMarshaler( string cookie ) : base( cookie )
        {
        }

        // Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        // from MarshalManagedToNative.  The return value is unused.
        override public object MarshalNativeToManaged( IntPtr ptrNativeData )
        {
            AMMediaType[] emt = this.obj as AMMediaType[];

            for ( int x = 0; x < emt.Length; x++ )
            {
                // copy in the value, and advance the pointer
                IntPtr ptr = Marshal.ReadIntPtr( ptrNativeData, x * IntPtr.Size );
                if ( ptr != IntPtr.Zero)
                {
                    emt[x] = (AMMediaType) Marshal.PtrToStructure( ptr, typeof( AMMediaType ) );
                }
                else
                {
                    emt[x] = null;
                }
            }

            return null;
        }

        // The number of bytes to marshal out
        override public int GetNativeDataSize( )
        {
            // get the array size
            int len = ( (Array) this.obj ).Length;

            // multiply that times the size of a pointer
            int size = len * IntPtr.Size;

            return size;
        }

        // This method is called by interop to create the custom marshaler.  The (optional)
        // cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.
        public static ICustomMarshaler GetInstance( string cookie )
        {
            return new EMTMarshaler( cookie );
        }
    }

}