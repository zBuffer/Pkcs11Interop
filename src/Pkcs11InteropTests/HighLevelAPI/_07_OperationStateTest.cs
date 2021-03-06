/*
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

using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// GetOperationState and SetOperationState tests.
    /// </summary>
    [TestFixture()]
    public class _07_OperationStateTest
    {
        /// <summary>
        /// Basic GetOperationState and SetOperationState test.
        /// </summary>
        [Test()]
        public void _01_BasicOperationStateTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Get operation state
                    byte[] state = session.GetOperationState();

                    // Do something interesting with operation state
                    Assert.IsNotNull(state);

                    // Let's set state so the test is complete
                    // Note that CK_INVALID_HANDLE is passed in as encryptionKey and authenticationKey
                    session.SetOperationState(state, new ObjectHandle(), new ObjectHandle());
                }
            }
        }
    }
}

