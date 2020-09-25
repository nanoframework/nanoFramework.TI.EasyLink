//
// Copyright (c) .NET Foundation and Contributors
// Portions Copyright (c) 2015-2019 Texas Instruments Incorporated
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;

namespace nanoFramework.TI.EasyLink
{
    /// <summary>
    /// The CC13xx/CC26xx EasyLink API is a simple abstraction layer on top of the CC13xx/CC26xx RF Driver and is intended as a starting point for developers creating a proprietor Sub1G protocol.
    /// </summary>
    /// <remarks>
    /// The EasyLink layer does not support any regional RF conformance such as 'Listen Before Talk' required for the license free frequency band. 
    /// Customers need to add support for the regional conformance that their product requires under the EasyLink API. 
    /// </remarks>
    public sealed class EasyLinkController : IDisposable
    {
        // this is used as the lock object 
        // a lock is required because multiple threads can access the SerialDevice
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly object _syncLock;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _disposed = false;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // this is a flag to signal that the Initialize() method has been successfully called
        private bool _initialized = false;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly PhyType _phyType;

#pragma warning disable S2933 // this field is updated in a method and can't be readonly
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private ArrayList _addressFilter;
#pragma warning restore S2933 // Fields that are only assigned in the constructor should be "readonly"

        /// <summary>
        /// 
        /// </summary>
        public PhyType PhyType => _phyType;

        /// <summary>
        /// Provides information if the EasyLink layer has been successfully initialized by calling <see cref="Initialize"/>.
        /// </summary>
        /// <returns><see langword="true"/> if the EasyLink has been successfully initialized.</returns>
        public bool IsInitialized => _initialized;

        /// <summary>
        /// Absolute radio time.
        /// This can be used for monitoring or Tx and Rx events using the AbsoluteTime field from <see cref="TransmitPacket"/> or <see cref="ReceivedPacket"/>.
        /// </summary>
        /// <returns>Absolute radio time.</returns>
        public extern uint AbsoluteTime
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// RSSI value of an ongoing radio operation.
        /// It is useful in receiver test modes to detect the presence of both modulated and unmodulated carrier waves.
        /// </summary>
        /// <returns>Signed RSSI value (dBm)e.</returns>
        /// <remarks>
        /// If no RSSI is available the return value is -128.
        /// </remarks>
        public extern sbyte Rssi
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// Gets the radio frequency in units of kHz.
        /// The returned frequency is value set in the Frequency Synthesizer and may not be exactly the same that was set.
        /// </summary>
        /// <remarks>
        /// This value does not include any offsets for deviations due to factors such as temperature and hence this API
        /// should not be used to get an accurate measure of frequency.
        /// </remarks>
        public extern uint Frequency
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// Gets the Tx Power in dBm.
        /// </summary>
        /// <remarks>
        /// This value does not include any offsets for deviations due to factors such as temperature and hence this API
        /// should not be used to get an accurate measure of frequency.
        /// </remarks>
        public extern sbyte RfPower
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// Gets the IEEE address.
        /// </summary>
        public extern byte[] IeeeAddress
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// Initializes the radio with specified Phy settings.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <remarks>
        /// </remarks>
        public EasyLinkController(PhyType phyTYpe = PhyType._50kbps2gfsk)
        {
            _phyType = phyTYpe;

            _addressFilter = new ArrayList();

            _syncLock = new object();
        }

        /// <summary>
        /// Initializes the EsayLink layer.
        /// </summary>
        /// <returns>The operation result.</returns>
        public Status Initialize()
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return Status.Success;
                }
                else
                {
                    byte initResult = InitNative();
                    if (initResult == (byte)Status.Success)
                    {
                        _initialized = true;

                        return Status.Success;
                    }
                    else
                    {
                        return (Status)initResult;
                    }
                }
            }
        }

        /// <summary>
        /// Sends a Packet with blocking call. This method blocks execution of the current thread until the packet transmission in complete.
        /// </summary>
        /// <param name="packet">The <see cref="TransmitPacket"/> to be transmitted.</param>
        /// <returns>The operation result.</returns>
        public Status Transmit(TransmitPacket packet)
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return (Status)TransmitNative(packet, Timeout.Infinite, 0);
                }
                else
                {
                    return Status.ConfigurationError;
                }
            }
        }

        /// <summary>
        /// Sends a Packet. This method blocks execution of the current thread until the packet transmission in complete.
        /// </summary>
        /// <param name="packet">The <see cref="TransmitPacket"/> to be transmitted.</param>
        /// <param name="timeout">The timeout value (in milliseconds) for the transmission operation to complete successfully.</param>
        /// <param name="dueTime"> The amount of time to delay before starting the transmission, in milliseconds. 
        /// Specify zero (0) to start the timer immediately.
        /// </param>
        /// <returns>The operation result.</returns>
        public Status Transmit(TransmitPacket packet, int timeout, int dueTime = 0)
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return (Status)TransmitNative(packet, timeout, dueTime);
                }
                else
                {
                    return Status.ConfigurationError;
                }
            }
        }

        /// <summary>
        /// Waits for packet to be received.
        /// This method blocks execution of the current thread until a packet is received.
        /// </summary>
        /// <param name="packet">The received packet.</param>
        /// <returns>The operation result.</returns>
        public Status Receive(out ReceivedPacket packet)
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return (Status)ReceiveNative(out packet, Timeout.Infinite);
                }
                else
                {
                    packet = null;

                    return Status.ConfigurationError;
                }
            }
        }

        /// <summary>
        /// Waits for packet to be received.
        /// This method blocks execution of the current thread until a packet is received.
        /// If no packet is receive before the <paramref name="timeout"/> expires an <see cref="Exception"/> is thrown.
        /// </summary>
        /// <param name="packet">The received packet.</param>
        /// <param name="timeout">The timeout value for the reception operation to complete successfully.</param>
        /// <returns>The operation result.</returns>
        public Status Receive(out ReceivedPacket packet, int timeout)
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return (Status)ReceiveNative(out packet, timeout);
                }
                else
                {
                    packet = null;

                    return Status.ConfigurationError;
                }
            }
        }

        /// <summary>
        /// Sets advanced configuration options.
        /// </summary>
        /// <param name="option">The control option to be set.</param>
        /// <param name="value">The value to set the control option to.</param>
        /// <returns>The operation result.</returns>
        public Status SetConfiguration(ControlOption option, uint value)
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return (Status)SetConfigurationNative(option, value);
                }
                else
                {
                    return Status.ConfigurationError;
                }
            }
        }

        /// <summary>
        /// Gets the current value of an advanced configuration option.
        /// </summary>
        /// <param name="option">The control option to get.</param>
        /// <returns>The control option value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
#pragma warning disable S4200 // It's OK to have native methods unwrapped in nanoFramework
        public extern uint GetConfiguration(ControlOption option);
#pragma warning restore S4200 // Native methods should be wrapped

        /// <summary>
        /// Add an address to the receive address filter.
        /// Any packet received for an address that is not in the filter will be discarded.
        /// Addresses are entered as <see cref="byte"/> array.
        /// </summary>
        /// <remarks>
        /// If the address is already on the list it won't be added.
        /// </remarks>
        public void AddAddressToFilter(byte[] address)
        {
            lock (_addressFilter)
            {
                if (!_addressFilter.Contains(address))
                {
                    _addressFilter.Add(address);

                    UpdateRxAddressFilterNative();
                }
            }
        }

        /// <summary>
        /// Removes an address from the receive address filter.
        /// </summary>
        /// <remarks>
        /// If the address is not on the filter list no error will be returned.
        /// </remarks>
        public void RemoveAddressFromFilter(byte[] address)
        {
            if (_addressFilter.Contains(address))
            {
                _addressFilter.Remove(address);

                UpdateRxAddressFilterNative();
            }
        }

        /// <summary>
        /// Sets the radio frequency in units of kHz.
        /// When setting the radio frequency the value will be rounded to the nearest frequency supported by the frequency synthesizer.
        /// </summary>
        /// <remarks>
        /// In order to set the frequency the EasyLink layer has to have been previously initialized with <see cref="Initialize"/>.
        /// </remarks>
        public Status SetFrequency(uint frequency)
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return (Status)SetFrequencyNative(frequency);
                }
                else
                {
                    return Status.ConfigurationError;
                }
            }
        }

        /// <summary>
        /// Sets the Tx Power in dBm.
        /// Accepted values range from -20 to 20 dBm, depending on the platform.
        /// All platforms other than the CC1352P: Value of -10 dBm or values in the range of 0-14 dBm are accepted. 
        /// Values above 14 are set to 14 dBm while those below 0 are set to -10 dBm.
        /// CC1352P Default PA: -20 to 14 dBm. Values above 14 dBm will be set to 14 dBm, while values below -20 dBm will cause a configuration error.
        /// CC1352P High PA: 14 to 20 dBm. Values above 20 dBm will be set to 20 dBm, while values below 14 dBm will cause a configuration error.
        /// </summary>
        /// <remarks>
        /// In order to set the Tx Power the EasyLink layer has to have been previously initialized with <see cref="Initialize"/>.
        /// The PA mode is chosen at build time, run-time switching from high PA to default PA (or vice versa) is not supported.
        /// </remarks>
        public Status SetRfPower(sbyte rfPower)
        {
            lock (_syncLock)
            {
                if (_initialized)
                {
                    return (Status)SetRfPowerNative(rfPower);
                }
                else
                {
                    return Status.ConfigurationError;
                }
            }
        }

        #region IDisposable Support

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _initialized = false;

                DisposeNative();

                _disposed = true;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ~EasyLinkController()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Dispose(false);
        }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Dispose()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Native calls

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void DisposeNative();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void UpdateRxAddressFilterNative();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte InitNative();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte ReceiveNative(out ReceivedPacket packet, int timeout);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte SetConfigurationNative(ControlOption option, uint value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte SetFrequencyNative(uint frequency);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte SetRfPowerNative(sbyte rfPower);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte TransmitNative(TransmitPacket packet, int timeout, int dueTime);

        #endregion
    }
}
