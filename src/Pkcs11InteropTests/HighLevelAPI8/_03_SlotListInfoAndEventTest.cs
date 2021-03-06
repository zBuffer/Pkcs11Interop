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

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI8;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
{
    /// <summary>
    /// GetSlotList, GetSlotInfo and WaitForSlotEvent tests.
    /// </summary>
    [TestFixture()]
    public class _03_SlotListInfoAndEventTest
    {
        /// <summary>
        /// GetSlotList test.
        /// </summary>
        [Test()]
        public void _01_SlotListTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Get list of available slots
                List<Slot> slots = pkcs11.GetSlotList(false);

                // Do something interesting with slots
                Assert.IsNotNull(slots);
                Assert.IsTrue(slots.Count > 0);
            }
        }
        
        /// <summary>
        /// GetSlotInfo test.
        /// </summary>
        [Test()]
        public void _02_BasicSlotListAndInfoTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Get list of available slots
                List<Slot> slots = pkcs11.GetSlotList(false);
                
                // Do something interesting with slots
                Assert.IsNotNull(slots);
                Assert.IsTrue(slots.Count > 0);

                // Analyze first slot
                SlotInfo slotInfo = slots[0].GetSlotInfo();

                // Do something interesting with slot info
                Assert.IsNotNull(slotInfo.ManufacturerId);
            }
        }
        
        /// <summary>
        /// WaitForSlotEvent test.
        /// </summary>
        [Test()]
        public void _03_WaitForSlotEventTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Wait for a slot event
                bool eventOccured = false;
                ulong slotId = 0;
                pkcs11.WaitForSlotEvent(true, out eventOccured, out slotId);
                Assert.IsFalse(eventOccured);
            }
        }
    }
}

