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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Resulting key handles and initialization vectors after performing a DeriveKey method with the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    public class CkSsl3KeyMatOut : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkSsl3KeyMatOut
        /// </summary>
        private HighLevelAPI4.MechanismParams.CkSsl3KeyMatOut _params4 = null;

        /// <summary>
        /// Platform specific CkSsl3KeyMatOut
        /// </summary>
        private HighLevelAPI8.MechanismParams.CkSsl3KeyMatOut _params8 = null;

        /// <summary>
        /// Key handle for the resulting Client MAC Secret key
        /// </summary>
        public ObjectHandle ClientMacSecret
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? new ObjectHandle(_params4.ClientMacSecret) : new ObjectHandle(_params8.ClientMacSecret);
            }
        }

        /// <summary>
        /// Key handle for the resulting Server MAC Secret key
        /// </summary>
        public ObjectHandle ServerMacSecret
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? new ObjectHandle(_params4.ServerMacSecret) : new ObjectHandle(_params8.ServerMacSecret);
            }
        }

        /// <summary>
        /// Key handle for the resulting Client Secret key
        /// </summary>
        public ObjectHandle ClientKey
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? new ObjectHandle(_params4.ClientKey) : new ObjectHandle(_params8.ClientKey);
            }
        }

        /// <summary>
        /// Key handle for the resulting Server Secret key
        /// </summary>
        public ObjectHandle ServerKey
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? new ObjectHandle(_params4.ServerKey) : new ObjectHandle(_params8.ServerKey);
            }
        }

        /// <summary>
        /// Initialization vector (IV) created for the client
        /// </summary>
        public byte[] IVClient
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? _params4.IVClient : _params8.IVClient;
            }
        }

        /// <summary>
        /// Initialization vector (IV) created for the server
        /// </summary>
        public byte[] IVServer
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? _params4.IVServer : _params8.IVServer;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkSsl3KeyMatOut class.
        /// </summary>
        /// <param name='ckSsl3KeyMatOut'>Platform specific CkSsl3KeyMatOut</param>
        internal CkSsl3KeyMatOut(HighLevelAPI4.MechanismParams.CkSsl3KeyMatOut ckSsl3KeyMatOut)
        {
            if (ckSsl3KeyMatOut == null)
                throw new ArgumentNullException("ckSsl3KeyMatOut");

            _params4 = ckSsl3KeyMatOut;
        }

        /// <summary>
        /// Initializes a new instance of the CkSsl3KeyMatOut class.
        /// </summary>
        /// <param name='ckSsl3KeyMatOut'>Platform specific CkSsl3KeyMatOut</param>
        internal CkSsl3KeyMatOut(HighLevelAPI8.MechanismParams.CkSsl3KeyMatOut ckSsl3KeyMatOut)
        {
            if (ckSsl3KeyMatOut == null)
                throw new ArgumentNullException("ckSsl3KeyMatOut");

            _params8 = ckSsl3KeyMatOut;
        }

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
                    if (_params4 != null)
                    {
                        _params4.Dispose();
                        _params4 = null;
                    }

                    if (_params8 != null)
                    {
                        _params8.Dispose();
                        _params8 = null;
                    }
                }
                
                // Dispose unmanaged objects

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSsl3KeyMatOut()
        {
            Dispose(false);
        }
        
        #endregion
    }
}

