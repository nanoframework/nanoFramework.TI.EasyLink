//
// Copyright (c) 2019 The nanoFramework project contributors
// Portions Copyright (c) 2015-2019 Texas Instruments Incorporated
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.TI.EasyLink
{
    ////////////////////////////////////////////////////////////////////////////////////////
    // !!! KEEP IN SYNC WITH EasyLink_RxPacket in SDK @ source\ti\easylink\EasyLink.h !!! //
    ////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Class with definitions and data of a received packet.
    /// </summary>
    public class ReceivedPacket
    {
#pragma warning disable S3459 // These fields are assigned in native code
#pragma warning disable IDE0044 // Add readonly modifier
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte[] _address;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte[] _payload;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private sbyte _rssi;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)] private uint _absoluteTime;
        private uint _rxTimeout;

#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore S3459 // Unassigned members should be removed

        /// <summary>
        /// Destination address.
        /// </summary>
        public byte[] DestinationAddress => _address;

        /// <summary>
        /// Payload data.
        /// </summary>
        public byte[] Payload => _payload;

        /// <summary>
        /// RSSI of received packet.
        /// </summary>
        public sbyte Rssi => _rssi;

        /// <summary>
        /// Absolute radio time that the packet was received.
        /// </summary>
        public uint AbsoluteTime => _absoluteTime;

        /// <summary>
        /// Relative radio time from Rx start to Rx Timeout,
        /// or absolute time that packet was Rx'ed when returned.
        /// A value of 0 means no timeout.
        /// </summary>
        public uint RxTimeout => _rxTimeout;
    }
}
