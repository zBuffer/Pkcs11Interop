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

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Imported native methods
    /// </summary>
    internal static class NativeMethods
    {
        #region Windows
        
        /// <summary>
        /// Loads the specified module into the address space of the calling process.
        /// </summary>
        /// <param name="lpFileName">The name of the module.</param>
        /// <returns>If the function succeeds, the return value is a handle to the module. If the function fails, the return value is NULL.</returns>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// </summary>
        /// <param name="hModule">A handle to the loaded library module.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="hModule">A handle to the DLL module that contains the function or variable.</param>
        /// <param name="lpProcName">The function or variable name, or the function's ordinal value.</param>
        /// <returns>If the function succeeds, the return value is the address of the exported function or variable. If the function fails, the return value is NULL.</returns>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        #endregion
         
        #region Unix
        
        /// <summary>
        /// Lazy function call binding
        /// </summary>
        internal const int RTLD_LAZY = 0x00001;
        
        /// <summary>
        /// Immediate function call binding 
        /// </summary>
        internal const int RTLD_NOW = 0x00002;
        
        /// <summary>
        /// Human readable string describing the most recent error that occurred from dlopen(), dlsym() or dlclose() since the last call to dlerror().
        /// </summary>
        /// <returns>Human readable string describing the most recent error or NULL if no errors have occurred since initialization or since it was last called.</returns>
        [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlerror();
        
        /// <summary>
        /// Loads the dynamic library
        /// </summary>
        /// <param name='filename'>Library filename.</param>
        /// <param name='flag'>RTLD_LAZY for lazy function call binding or RTLD_NOW immediate function call binding.</param>
        /// <returns>Handle for the dynamic library if successful, IntPtr.Zero otherwise.</returns>
        [DllImport("libdl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr dlopen(string filename, int flag);

        /// <summary>
        /// Decrements the reference count on the dynamic library handle. If the reference count drops to zero and no other loaded libraries use symbols in it, then the dynamic library is unloaded.
        /// </summary>
        /// <param name='handle'>Handle for the dynamic library.</param>
        /// <returns>Returns 0 on success, and nonzero on error.</returns>
        [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int dlclose(IntPtr handle);
        
        /// <summary>
        /// Returns the address where the symbol is loaded into memory.
        /// </summary>
        /// <param name='handle'>Handle for the dynamic library.</param>
        /// <param name='symbol'>Name of symbol that should be addressed.</param>
        /// <returns>Returns 0 on success, and nonzero on error.</returns>
        [DllImport("libdl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        
        #endregion
    }
}
