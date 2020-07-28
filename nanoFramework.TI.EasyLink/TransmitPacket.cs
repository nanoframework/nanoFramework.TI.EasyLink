//
// Copyright (c) 2019 The nanoFramework project contributors
// Portions Copyright (c) 2015-2019 Texas Instruments Incorporated
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.TI.EasyLink
{
    /////////////////////////////////////////////////////////////////////////////////////////////
    // !!! KEEP IN SYNC WITH with EasyLink_TxPacket in SDK @ source\ti\easylink\EasyLink.h !!! //
    /////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Class with definitions and data of a packet to be transmited.
    /// </summary>
    public class TransmitPacket
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly byte[] _address;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly byte[] _payload;

        /// <summary>
        /// Destination address.
        /// </summary>
        public byte[] DestinationAddress { get => _address; }

        /// <summary>
        /// Payload data.
        /// </summary>
        public byte[] Payload { get => _payload; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        public TransmitPacket(byte[] address, byte[] data)
        {
            _address = address;
            _payload = data;
        }
    }
}
