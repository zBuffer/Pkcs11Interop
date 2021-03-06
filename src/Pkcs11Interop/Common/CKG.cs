﻿/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Mask generation functions
    /// </summary>
    public enum CKG : uint
    {
        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-1 digest algorithm
        /// </summary>
        CKG_MGF1_SHA1 = 0x00000001,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-256 digest algorithm
        /// </summary>
        CKG_MGF1_SHA256 = 0x00000002,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-384 digest algorithm
        /// </summary>
        CKG_MGF1_SHA384 = 0x00000003,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-512 digest algorithm
        /// </summary>
        CKG_MGF1_SHA512 = 0x00000004,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-224 digest algorithm
        /// </summary>
        CKG_MGF1_SHA224 = 0x00000005
    }
}
