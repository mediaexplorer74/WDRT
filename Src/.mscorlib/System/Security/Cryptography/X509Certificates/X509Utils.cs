using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002AB RID: 683
	internal static class X509Utils
	{
		// Token: 0x06002424 RID: 9252 RVA: 0x00083C2D File Offset: 0x00081E2D
		private static bool OidGroupWillNotUseActiveDirectory(OidGroup group)
		{
			return group == OidGroup.HashAlgorithm || group == OidGroup.EncryptionAlgorithm || group == OidGroup.PublicKeyAlgorithm || group == OidGroup.SignatureAlgorithm || group == OidGroup.Attribute || group == OidGroup.ExtensionOrAttribute || group == OidGroup.KeyDerivationFunction;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00083C50 File Offset: 0x00081E50
		[SecurityCritical]
		private static CRYPT_OID_INFO FindOidInfo(OidKeyType keyType, string key, OidGroup group)
		{
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			CRYPT_OID_INFO crypt_OID_INFO;
			try
			{
				if (keyType == OidKeyType.Oid)
				{
					intPtr = Marshal.StringToCoTaskMemAnsi(key);
				}
				else
				{
					intPtr = Marshal.StringToCoTaskMemUni(key);
				}
				if (!X509Utils.OidGroupWillNotUseActiveDirectory(group))
				{
					OidGroup oidGroup = group | OidGroup.DisableSearchDS;
					IntPtr intPtr2 = X509Utils.CryptFindOIDInfo(keyType, intPtr, oidGroup);
					if (intPtr2 != IntPtr.Zero)
					{
						return (CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr2, typeof(CRYPT_OID_INFO));
					}
				}
				IntPtr intPtr3 = X509Utils.CryptFindOIDInfo(keyType, intPtr, group);
				if (intPtr3 != IntPtr.Zero)
				{
					crypt_OID_INFO = (CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr3, typeof(CRYPT_OID_INFO));
				}
				else
				{
					if (group != OidGroup.AllGroups)
					{
						IntPtr intPtr4 = X509Utils.CryptFindOIDInfo(keyType, intPtr, OidGroup.AllGroups);
						if (intPtr4 != IntPtr.Zero)
						{
							return (CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr4, typeof(CRYPT_OID_INFO));
						}
					}
					crypt_OID_INFO = default(CRYPT_OID_INFO);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return crypt_OID_INFO;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00083D58 File Offset: 0x00081F58
		[SecuritySafeCritical]
		internal static int GetAlgIdFromOid(string oid, OidGroup oidGroup)
		{
			if (string.Equals(oid, "2.16.840.1.101.3.4.2.1", StringComparison.Ordinal))
			{
				return 32780;
			}
			if (string.Equals(oid, "2.16.840.1.101.3.4.2.2", StringComparison.Ordinal))
			{
				return 32781;
			}
			if (string.Equals(oid, "2.16.840.1.101.3.4.2.3", StringComparison.Ordinal))
			{
				return 32782;
			}
			return X509Utils.FindOidInfo(OidKeyType.Oid, oid, oidGroup).AlgId;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00083DB0 File Offset: 0x00081FB0
		[SecuritySafeCritical]
		internal static string GetFriendlyNameFromOid(string oid, OidGroup oidGroup)
		{
			CRYPT_OID_INFO crypt_OID_INFO = X509Utils.FindOidInfo(OidKeyType.Oid, oid, oidGroup);
			return crypt_OID_INFO.pwszName;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00083DCC File Offset: 0x00081FCC
		[SecuritySafeCritical]
		internal static string GetOidFromFriendlyName(string friendlyName, OidGroup oidGroup)
		{
			CRYPT_OID_INFO crypt_OID_INFO = X509Utils.FindOidInfo(OidKeyType.Name, friendlyName, oidGroup);
			return crypt_OID_INFO.pszOID;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00083DE8 File Offset: 0x00081FE8
		internal static int NameOrOidToAlgId(string oid, OidGroup oidGroup)
		{
			if (oid == null)
			{
				return 32772;
			}
			string text = CryptoConfig.MapNameToOID(oid, oidGroup);
			if (text == null)
			{
				text = oid;
			}
			int algIdFromOid = X509Utils.GetAlgIdFromOid(text, oidGroup);
			if (algIdFromOid == 0 || algIdFromOid == -1)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidOID"));
			}
			return algIdFromOid;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00083E2C File Offset: 0x0008202C
		internal static X509ContentType MapContentType(uint contentType)
		{
			switch (contentType)
			{
			case 1U:
				return X509ContentType.Cert;
			case 4U:
				return X509ContentType.SerializedStore;
			case 5U:
				return X509ContentType.SerializedCert;
			case 8U:
			case 9U:
				return X509ContentType.Pkcs7;
			case 10U:
				return X509ContentType.Authenticode;
			case 12U:
				return X509ContentType.Pfx;
			}
			return X509ContentType.Unknown;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00083E80 File Offset: 0x00082080
		internal static uint MapKeyStorageFlags(X509KeyStorageFlags keyStorageFlags)
		{
			if ((keyStorageFlags & (X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet)) != keyStorageFlags)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "keyStorageFlags");
			}
			X509KeyStorageFlags x509KeyStorageFlags = keyStorageFlags & (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet);
			if (x509KeyStorageFlags == (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet))
			{
				throw new ArgumentException(Environment.GetResourceString("Cryptography_X509_InvalidFlagCombination", new object[] { x509KeyStorageFlags }), "keyStorageFlags");
			}
			uint num = 0U;
			if ((keyStorageFlags & X509KeyStorageFlags.UserKeySet) == X509KeyStorageFlags.UserKeySet)
			{
				num |= 4096U;
			}
			else if ((keyStorageFlags & X509KeyStorageFlags.MachineKeySet) == X509KeyStorageFlags.MachineKeySet)
			{
				num |= 32U;
			}
			if ((keyStorageFlags & X509KeyStorageFlags.Exportable) == X509KeyStorageFlags.Exportable)
			{
				num |= 1U;
			}
			if ((keyStorageFlags & X509KeyStorageFlags.UserProtected) == X509KeyStorageFlags.UserProtected)
			{
				num |= 2U;
			}
			if ((keyStorageFlags & X509KeyStorageFlags.EphemeralKeySet) == X509KeyStorageFlags.EphemeralKeySet)
			{
				num |= 33280U;
			}
			return num;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00083F1C File Offset: 0x0008211C
		[SecurityCritical]
		internal static SafeCertStoreHandle ExportCertToMemoryStore(X509Certificate certificate)
		{
			SafeCertStoreHandle invalidHandle = SafeCertStoreHandle.InvalidHandle;
			X509Utils.OpenX509Store(2U, 8704U, null, invalidHandle);
			X509Utils._AddCertificateToStore(invalidHandle, certificate.CertContext);
			return invalidHandle;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00083F4C File Offset: 0x0008214C
		[SecurityCritical]
		internal static IntPtr PasswordToHGlobalUni(object password)
		{
			if (password != null)
			{
				string text = password as string;
				if (text != null)
				{
					return Marshal.StringToHGlobalUni(text);
				}
				SecureString secureString = password as SecureString;
				if (secureString != null)
				{
					return Marshal.SecureStringToGlobalAllocUnicode(secureString);
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600242E RID: 9262
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32")]
		private static extern IntPtr CryptFindOIDInfo(OidKeyType dwKeyType, IntPtr pvKey, OidGroup dwGroupId);

		// Token: 0x0600242F RID: 9263
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void _AddCertificateToStore(SafeCertStoreHandle safeCertStoreHandle, SafeCertContextHandle safeCertContext);

		// Token: 0x06002430 RID: 9264
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _DuplicateCertContext(IntPtr handle, ref SafeCertContextHandle safeCertContext);

		// Token: 0x06002431 RID: 9265
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] _ExportCertificatesToBlob(SafeCertStoreHandle safeCertStoreHandle, X509ContentType contentType, IntPtr password);

		// Token: 0x06002432 RID: 9266
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] _GetCertRawData(SafeCertContextHandle safeCertContext);

		// Token: 0x06002433 RID: 9267
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void _GetDateNotAfter(SafeCertContextHandle safeCertContext, ref Win32Native.FILE_TIME fileTime);

		// Token: 0x06002434 RID: 9268
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void _GetDateNotBefore(SafeCertContextHandle safeCertContext, ref Win32Native.FILE_TIME fileTime);

		// Token: 0x06002435 RID: 9269
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string _GetIssuerName(SafeCertContextHandle safeCertContext, bool legacyV1Mode);

		// Token: 0x06002436 RID: 9270
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string _GetPublicKeyOid(SafeCertContextHandle safeCertContext);

		// Token: 0x06002437 RID: 9271
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] _GetPublicKeyParameters(SafeCertContextHandle safeCertContext);

		// Token: 0x06002438 RID: 9272
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] _GetPublicKeyValue(SafeCertContextHandle safeCertContext);

		// Token: 0x06002439 RID: 9273
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string _GetSubjectInfo(SafeCertContextHandle safeCertContext, uint displayType, bool legacyV1Mode);

		// Token: 0x0600243A RID: 9274
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] _GetSerialNumber(SafeCertContextHandle safeCertContext);

		// Token: 0x0600243B RID: 9275
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] _GetThumbprint(SafeCertContextHandle safeCertContext);

		// Token: 0x0600243C RID: 9276
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _LoadCertFromBlob(byte[] rawData, IntPtr password, uint dwFlags, bool persistKeySet, ref SafeCertContextHandle pCertCtx);

		// Token: 0x0600243D RID: 9277
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _LoadCertFromFile(string fileName, IntPtr password, uint dwFlags, bool persistKeySet, ref SafeCertContextHandle pCertCtx);

		// Token: 0x0600243E RID: 9278
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _OpenX509Store(uint storeType, uint flags, string storeName, ref SafeCertStoreHandle safeCertStoreHandle);

		// Token: 0x0600243F RID: 9279
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint _QueryCertBlobType(byte[] rawData);

		// Token: 0x06002440 RID: 9280
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint _QueryCertFileType(string fileName);

		// Token: 0x06002441 RID: 9281 RVA: 0x00083F83 File Offset: 0x00082183
		[SecurityCritical]
		internal static void DuplicateCertContext(IntPtr handle, SafeCertContextHandle safeCertContext)
		{
			X509Utils._DuplicateCertContext(handle, ref safeCertContext);
			if (!safeCertContext.IsInvalid)
			{
				GC.ReRegisterForFinalize(safeCertContext);
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00083F9B File Offset: 0x0008219B
		[SecurityCritical]
		internal static void LoadCertFromBlob(byte[] rawData, IntPtr password, uint dwFlags, bool persistKeySet, SafeCertContextHandle pCertCtx)
		{
			X509Utils._LoadCertFromBlob(rawData, password, dwFlags, persistKeySet, ref pCertCtx);
			if (!pCertCtx.IsInvalid)
			{
				GC.ReRegisterForFinalize(pCertCtx);
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00083FB8 File Offset: 0x000821B8
		[SecurityCritical]
		internal static void LoadCertFromFile(string fileName, IntPtr password, uint dwFlags, bool persistKeySet, SafeCertContextHandle pCertCtx)
		{
			X509Utils._LoadCertFromFile(fileName, password, dwFlags, persistKeySet, ref pCertCtx);
			if (!pCertCtx.IsInvalid)
			{
				GC.ReRegisterForFinalize(pCertCtx);
			}
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00083FD5 File Offset: 0x000821D5
		[SecurityCritical]
		private static void OpenX509Store(uint storeType, uint flags, string storeName, SafeCertStoreHandle safeCertStoreHandle)
		{
			X509Utils._OpenX509Store(storeType, flags, storeName, ref safeCertStoreHandle);
			if (!safeCertStoreHandle.IsInvalid)
			{
				GC.ReRegisterForFinalize(safeCertStoreHandle);
			}
		}
	}
}
