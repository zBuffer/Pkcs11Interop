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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI8;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
{
    /// <summary>
    /// Pkcs11 construct, dispose, initialize and finalize tests.
    /// </summary>
    [TestFixture()]
    public class _01_InitializeTest
    {
        /// <summary>
        /// Basic construct and dispose test.
        /// </summary>
        [Test()]
        public void _01_BasicPkcs11DisposeTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            // Unmanaged PKCS#11 library is loaded by the constructor of Pkcs11 class.
            // Every PKCS#11 library needs to be initialized with C_Initialize method
            // which is also called automatically by the constructor of Pkcs11 class.
            Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false);
            
            // Do something  interesting
            
            // Unmanaged PKCS#11 library is unloaded by Dispose() method.
            // C_Finalize should be the last call made by an application and it
            // is also called automatically by Dispose() method.
            pkcs11.Dispose();
        }
        
        /// <summary>
        /// Using statement test.
        /// </summary>
        [Test()]
        public void _02_UsingPkcs11DisposeTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            // Pkcs11 class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Do something interesting
            }
        }

        /// <summary>
        /// Example for single-threaded applications.
        /// </summary>
        [Test()]
        public void _03_SingleThreadedInitializeTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            // If an application will not be accessing PKCS#11 library from multiple threads
            // simultaneously, it should specify "false" as a value of "useOsLocking" parameter.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Do something interesting
            }
        }
        
        /// <summary>
        /// Example for multi-threaded applications.
        /// </summary>
        [Test()]
        public void _04_MultiThreadedInitializeTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            // If an application will be accessing PKCS#11 library from multiple threads
            // simultaneously, it should specify "true" as a value of "useOsLocking" parameter.
            // PKCS#11 library will use the native operation system threading model for locking.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, true))
            {
                // Do something interesting
            }
        }

        /// <summary>
        /// Example for libraries that support C_GetFunctionList()
        /// </summary>
        [Test()]
        public void _05_Pkcs11WithGetFunctionListTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            // Before an application can perform any cryptographic operations with Cryptoki library 
            // it has to obtain function pointers for all the Cryptoki API routines present in the library.
            // This can be done either via C_GetFunctionList() function or via platform specific native 
            // function - GetProcAddress() on Windows and dlsym() on Unix.
            // The most simple constructor of Net.Pkcs11Interop.HighLevelAPI8.Pkcs11 class uses 
            // C_GetFunctionList() approach but Pkcs11Interop also provides an alternative constructor 
            // that can specify which approach should be used.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, true, true))
            {
                // Do something interesting
            }
        }

        /// <summary>
        /// Example for libraries that do not support C_GetFunctionList()
        /// </summary>
        [Test()]
        public void _06_Pkcs11WithoutGetFunctionListTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            // Before an application can perform any cryptographic operations with Cryptoki library 
            // it has to obtain function pointers for all the Cryptoki API routines present in the library.
            // This can be done either via C_GetFunctionList() function or via platform specific native 
            // function - GetProcAddress() on Windows and dlsym() on Unix.
            // The most simple constructor of Net.Pkcs11Interop.HighLevelAPI8.Pkcs11 class uses 
            // C_GetFunctionList() approach but Pkcs11Interop also provides an alternative constructor 
            // that can specify which approach should be used.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, true, false))
            {
                // Do something interesting
            }
        }
    }
}

