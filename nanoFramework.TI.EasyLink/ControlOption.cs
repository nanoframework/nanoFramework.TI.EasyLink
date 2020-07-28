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
    public enum ControlOption : byte
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////
        // !!! KEEP IN SYNC WITH with EasyLink_CtrlOption in SDK @ source\ti\easylink\EasyLink.h !!! //
        ///////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Set the number of bytes in Address for both address filter and Tx/Rx operations.
        /// </summary>
        AddressSize,

        /// <summary>
        /// Set a timeout value for inactivity on the radio.
        /// </summary>
        /// <remarks>
        ///If the radio stays idle for this amount of time it is automatically powered down.
        /// </remarks>
        IdleTimeout,

        /// <summary> 
        /// Set Multi-client mode for applications that will use multiple RF clients.
        /// </summary>
        /// <remarks>
        /// Must be set before calling <see cref="EasyLinkController.Initialize"/>.
        /// </remarks>
        MultiClientMode,

        /// <summary>
        /// Relative time in ticks from asynchronous Rx start to TimeOut.
        /// A value of 0 means no timeout.
        /// </summary>
        AsyncRxTimeout,

        /// <summary>
        /// Enable/Disable Test mode for Tone.
        /// </summary>
        TestTone,

        /// <summary>
        /// Enable/Disable Test mode for Signal.
        /// </summary>
        TestSignal,

        /// <summary>
        /// Enable/Disable Rx Test mode for Tone.
        /// </summary>
        RxTestTone,
    }
}
