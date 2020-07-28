//
// Copyright (c) 2019 The nanoFramework project contributors
// Portions Copyright (c) 2015-2019 Texas Instruments Incorporated
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.TI.EasyLink
{
    /// <summary>
    /// EasyLink Status and error codes.
    /// </summary>
    public enum Status : byte
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // !!! KEEP IN SYNC WITH with EasyLink_Status in SDK @ source\ti\easylink\EasyLink.h !!! //
        ///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Operation successful.
        /// </summary>
        Success,

        /// <summary>
        /// Configuration error.
        /// </summary>
        ConfigurationError,

        /// <summary>
        /// Parameter error.
        /// </summary>
        ParameterError,

        /// <summary>
        /// Memory error
        /// </summary>
        MemoryError,

        /// <summary>
        /// Command error.
        /// </summary>
        CommandError,

        /// <summary>
        /// Transmit error.
        /// </summary>
        TxError,

        /// <summary>
        /// Receive error.
        /// </summary>
        RxError,

        /// <summary>
        /// Receive timeout.
        /// </summary>
        RxTimeout,

        /// <summary>
        /// Receive buffer error.
        /// </summary>
        RxBufferError,

        /// <summary>
        /// Busy error.
        /// </summary>
        Busy,

        /// <summary>
        /// Command stopped or aborted.
        /// </summary>
        Aborted,
    }
}
