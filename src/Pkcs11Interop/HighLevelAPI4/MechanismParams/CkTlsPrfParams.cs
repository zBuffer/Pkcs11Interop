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

using System;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI4.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_TLS_PRF mechanism
    /// </summary>
    public class CkTlsPrfParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI4.MechanismParams.CK_TLS_PRF_PARAMS _lowLevelStruct = new LowLevelAPI4.MechanismParams.CK_TLS_PRF_PARAMS();

        /// <summary>
        /// Output of the operation
        /// </summary>
        public byte[] Output
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                int uintSize = Common.UnmanagedMemory.SizeOf(typeof(uint));
                byte[] outputLenBytes = Common.UnmanagedMemory.Read(_lowLevelStruct.OutputLen, uintSize);
                uint outputLen = Common.ConvertUtils.BytesToUInt(outputLenBytes);
                return Common.UnmanagedMemory.Read(_lowLevelStruct.Output, Convert.ToInt32(outputLen));
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkTlsPrfParams class.
        /// </summary>
        /// <param name='seed'>Input seed</param>
        /// <param name='label'>Identifying label</param>
        /// <param name='outputLen'>Length in bytes that the output to be created shall have</param>
        public CkTlsPrfParams(byte[] seed, byte[] label, uint outputLen)
        {
            _lowLevelStruct.Seed = IntPtr.Zero;
            _lowLevelStruct.SeedLen = 0;
            _lowLevelStruct.Label = IntPtr.Zero;
            _lowLevelStruct.LabelLen = 0;
            _lowLevelStruct.Output = IntPtr.Zero;
            _lowLevelStruct.OutputLen = IntPtr.Zero;

            if (seed != null)
            {
                _lowLevelStruct.Seed = Common.UnmanagedMemory.Allocate(seed.Length);
                Common.UnmanagedMemory.Write(_lowLevelStruct.Seed, seed);
                _lowLevelStruct.SeedLen = Convert.ToUInt32(seed.Length);
            }
            
            if (label != null)
            {
                _lowLevelStruct.Label = Common.UnmanagedMemory.Allocate(label.Length);
                Common.UnmanagedMemory.Write(_lowLevelStruct.Label, label);
                _lowLevelStruct.LabelLen = Convert.ToUInt32(label.Length);
            }

            if (outputLen < 1)
                throw new ArgumentException("Value has to be positive number", "outputLen");

            _lowLevelStruct.Output = Common.UnmanagedMemory.Allocate(Convert.ToInt32(outputLen));

            byte[] outputLenBytes = Common.ConvertUtils.UIntToBytes(outputLen);
            _lowLevelStruct.OutputLen = Common.UnmanagedMemory.Allocate(outputLenBytes.Length);
            Common.UnmanagedMemory.Write(_lowLevelStruct.OutputLen, outputLenBytes);
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _lowLevelStruct;
        }
        
        #endregion
        
        #region IDisposable
        
        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects
                }
                
                // Dispose unmanaged objects
                Common.UnmanagedMemory.Free(ref _lowLevelStruct.Seed);
                _lowLevelStruct.SeedLen = 0;
                Common.UnmanagedMemory.Free(ref _lowLevelStruct.Label);
                _lowLevelStruct.LabelLen = 0;
                Common.UnmanagedMemory.Free(ref _lowLevelStruct.Output);
                Common.UnmanagedMemory.Free(ref _lowLevelStruct.OutputLen);

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkTlsPrfParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
