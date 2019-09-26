//
// Copyright (c) 2019 The nanoFramework project contributors
// Portions Copyright (c) 2015-2019 Texas Instruments Incorporated
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.TI.EasyLink
{
    /// <summary>
    /// Phy settings to initialize the radio.
    /// </summary>
    /// <remarks>
    /// The support for a particular Phy configuration is target dependent.
    /// If the request type is not available an <see cref="System.NotSupportedException"/> will be thrown.
    /// </remarks>
    public enum PhyType : byte
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // !!! KEEP IN SYNC WITH with EasyLink_PhyType in SDK @ source\ti\easylink\EasyLink. !!! //
        ///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Customer Phy specific settings.
        /// </summary>
        Custom = 0,

        /// <summary>
        /// Sub1G 50kbps data rate, IEEE 802.15.4g GFSK
        /// </summary>
        _50kbps2gfsk = 1,

        /// <summary>
        /// Sub1G 625bps data rate, Long Range Mode
        /// </summary>
        _625bpsLrm = 2,

        /// <summary>
        /// 2.4Ghz 200kbps data rate, IEEE 802.15.4g GFSK.
        /// </summary>
        _2_4_200kbps2gfsk = 3,

        /// <summary>
        /// SimpleLink Long Range (5 kbps).
        /// </summary>
        _5kbpsSlLr = 4,

        /// <summary>
        /// 2.4Ghz 100kbps data rate, IEEE 802.15.4g GFSK.
        /// </summary>
        _2_4_100kbps2gfs = 5,

        /// <summary>
        /// 2.4Ghz 250kbps data rate, IEEE 802.15.4g GFSK.
        /// </summary>
        _2_4_250kbps2gfsk = 6,

        /// <summary>
        /// 200kbps data rate, IEEE 802.15.4g GFSK.
        /// </summary>
        _200kbps2gfsk = 7,

    }
}
