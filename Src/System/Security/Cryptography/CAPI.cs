using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000456 RID: 1110
	internal sealed class CAPI : CAPIMethods
	{
		// Token: 0x06002936 RID: 10550 RVA: 0x000BB468 File Offset: 0x000B9668
		internal static byte[] BlobToByteArray(IntPtr pBlob)
		{
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPIBase.CRYPTOAPI_BLOB)Marshal.PtrToStructure(pBlob, typeof(CAPIBase.CRYPTOAPI_BLOB));
			if (cryptoapi_BLOB.cbData == 0U)
			{
				return new byte[0];
			}
			return CAPI.BlobToByteArray(cryptoapi_BLOB);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000BB4A0 File Offset: 0x000B96A0
		internal static byte[] BlobToByteArray(CAPIBase.CRYPTOAPI_BLOB blob)
		{
			if (blob.cbData == 0U)
			{
				return new byte[0];
			}
			byte[] array = new byte[blob.cbData];
			Marshal.Copy(blob.pbData, array, 0, array.Length);
			return array;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000BB4DC File Offset: 0x000B96DC
		internal unsafe static bool DecodeObject(IntPtr pszStructType, IntPtr pbEncoded, uint cbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
		{
			decodedValue = SafeLocalAllocHandle.InvalidHandle;
			cbDecodedValue = 0U;
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			decodedValue = safeLocalAllocHandle;
			cbDecodedValue = num;
			return true;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000BB54C File Offset: 0x000B974C
		internal unsafe static bool DecodeObject(IntPtr pszStructType, byte[] pbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
		{
			decodedValue = SafeLocalAllocHandle.InvalidHandle;
			cbDecodedValue = 0U;
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			decodedValue = safeLocalAllocHandle;
			cbDecodedValue = num;
			return true;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000BB5BC File Offset: 0x000B97BC
		internal unsafe static bool EncodeObject(IntPtr lpszStructType, IntPtr pvStructInfo, out byte[] encodedData)
		{
			encodedData = new byte[0];
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			encodedData = new byte[num];
			Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), encodedData, 0, (int)num);
			safeLocalAllocHandle.Dispose();
			return true;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000BB638 File Offset: 0x000B9838
		internal unsafe static bool EncodeObject(string lpszStructType, IntPtr pvStructInfo, out byte[] encodedData)
		{
			encodedData = new byte[0];
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			encodedData = new byte[num];
			Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), encodedData, 0, (int)num);
			safeLocalAllocHandle.Dispose();
			return true;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000BB6B4 File Offset: 0x000B98B4
		internal unsafe static string GetCertNameInfo([In] SafeCertContextHandle safeCertContext, [In] uint dwFlags, [In] uint dwDisplayType)
		{
			if (safeCertContext == null)
			{
				throw new ArgumentNullException("pCertContext");
			}
			if (safeCertContext.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "safeCertContext");
			}
			uint num = 33554435U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (dwDisplayType == 3U)
			{
				safeLocalAllocHandle = System.Security.Cryptography.X509Certificates.X509Utils.StringToAnsiPtr("2.5.4.3");
			}
			SafeLocalAllocHandle safeLocalAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
			uint num2 = CAPISafe.CertGetNameStringW(safeCertContext, dwDisplayType, dwFlags, (dwDisplayType == 3U) ? safeLocalAllocHandle.DangerousGetHandle() : new IntPtr((void*)(&num)), safeLocalAllocHandle2, 0U);
			if (num2 == 0U)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			safeLocalAllocHandle2 = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)(2U * num2))));
			if (CAPISafe.CertGetNameStringW(safeCertContext, dwDisplayType, dwFlags, (dwDisplayType == 3U) ? safeLocalAllocHandle.DangerousGetHandle() : new IntPtr((void*)(&num)), safeLocalAllocHandle2, num2) == 0U)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			string text = Marshal.PtrToStringUni(safeLocalAllocHandle2.DangerousGetHandle());
			safeLocalAllocHandle2.Dispose();
			safeLocalAllocHandle.Dispose();
			return text;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000BB794 File Offset: 0x000B9994
		internal new static SafeLocalAllocHandle LocalAlloc(uint uFlags, IntPtr sizetdwBytes)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = CAPISafe.LocalAlloc(uFlags, sizetdwBytes);
			if (safeLocalAllocHandle == null || safeLocalAllocHandle.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000BB7BC File Offset: 0x000B99BC
		internal new static bool CryptAcquireContext([In] [Out] ref SafeCryptProvHandle hCryptProv, [MarshalAs(UnmanagedType.LPStr)] [In] string pwszContainer, [MarshalAs(UnmanagedType.LPStr)] [In] string pwszProvider, [In] uint dwProvType, [In] uint dwFlags)
		{
			CspParameters cspParameters = new CspParameters();
			cspParameters.ProviderName = pwszProvider;
			cspParameters.KeyContainerName = pwszContainer;
			cspParameters.ProviderType = (int)dwProvType;
			cspParameters.KeyNumber = -1;
			cspParameters.Flags = (((dwFlags & 32U) == 32U) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(cspParameters, KeyContainerPermissionFlags.Open);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			bool flag = CAPIUnsafe.CryptAcquireContext(ref hCryptProv, pwszContainer, pwszProvider, dwProvType, dwFlags);
			if (!flag && Marshal.GetLastWin32Error() == -2146893802)
			{
				flag = CAPIUnsafe.CryptAcquireContext(ref hCryptProv, pwszContainer, pwszProvider, dwProvType, dwFlags | 8U);
			}
			return flag;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000BB854 File Offset: 0x000B9A54
		internal static bool CryptAcquireContext(ref SafeCryptProvHandle hCryptProv, IntPtr pwszContainer, IntPtr pwszProvider, uint dwProvType, uint dwFlags)
		{
			string text = null;
			if (pwszContainer != IntPtr.Zero)
			{
				text = Marshal.PtrToStringUni(pwszContainer);
			}
			string text2 = null;
			if (pwszProvider != IntPtr.Zero)
			{
				text2 = Marshal.PtrToStringUni(pwszProvider);
			}
			return CAPI.CryptAcquireContext(ref hCryptProv, text, text2, dwProvType, dwFlags);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000BB898 File Offset: 0x000B9A98
		internal new static CAPIBase.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] OidGroup dwGroupId)
		{
			if (pvKey == IntPtr.Zero)
			{
				throw new ArgumentNullException("pvKey");
			}
			CAPIBase.CRYPT_OID_INFO crypt_OID_INFO = new CAPIBase.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CAPIBase.CRYPT_OID_INFO)));
			IntPtr intPtr = CAPISafe.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
			if (intPtr != IntPtr.Zero)
			{
				crypt_OID_INFO = (CAPIBase.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr, typeof(CAPIBase.CRYPT_OID_INFO));
			}
			return crypt_OID_INFO;
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000BB900 File Offset: 0x000B9B00
		internal new static CAPIBase.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] OidGroup dwGroupId)
		{
			if (pvKey == null)
			{
				throw new ArgumentNullException("pvKey");
			}
			if (pvKey.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "pvKey");
			}
			CAPIBase.CRYPT_OID_INFO crypt_OID_INFO = new CAPIBase.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CAPIBase.CRYPT_OID_INFO)));
			IntPtr intPtr = CAPISafe.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
			if (intPtr != IntPtr.Zero)
			{
				crypt_OID_INFO = (CAPIBase.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr, typeof(CAPIBase.CRYPT_OID_INFO));
			}
			return crypt_OID_INFO;
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000BB97C File Offset: 0x000B9B7C
		internal unsafe static string CryptFormatObject([In] uint dwCertEncodingType, [In] uint dwFormatStrType, [In] string lpszStructType, [In] byte[] rawData)
		{
			if (rawData == null)
			{
				throw new ArgumentNullException("rawData");
			}
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPISafe.CryptFormatObject(dwCertEncodingType, 0U, dwFormatStrType, IntPtr.Zero, lpszStructType, rawData, (uint)rawData.Length, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return System.Security.Cryptography.X509Certificates.X509Utils.EncodeHexString(rawData);
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPISafe.CryptFormatObject(dwCertEncodingType, 0U, dwFormatStrType, IntPtr.Zero, lpszStructType, rawData, (uint)rawData.Length, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return System.Security.Cryptography.X509Certificates.X509Utils.EncodeHexString(rawData);
			}
			string text = Marshal.PtrToStringUni(safeLocalAllocHandle.DangerousGetHandle());
			safeLocalAllocHandle.Dispose();
			return text;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000BBA08 File Offset: 0x000B9C08
		internal unsafe static string CryptFormatObject([In] uint dwCertEncodingType, [In] uint dwFormatStrType, [In] IntPtr lpszStructType, [In] byte[] rawData)
		{
			if (rawData == null)
			{
				throw new ArgumentNullException("rawData");
			}
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPISafe.CryptFormatObject(dwCertEncodingType, 0U, dwFormatStrType, IntPtr.Zero, lpszStructType, rawData, (uint)rawData.Length, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return System.Security.Cryptography.X509Certificates.X509Utils.EncodeHexString(rawData);
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPISafe.CryptFormatObject(dwCertEncodingType, 0U, dwFormatStrType, IntPtr.Zero, lpszStructType, rawData, (uint)rawData.Length, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return System.Security.Cryptography.X509Certificates.X509Utils.EncodeHexString(rawData);
			}
			string text = Marshal.PtrToStringUni(safeLocalAllocHandle.DangerousGetHandle());
			safeLocalAllocHandle.Dispose();
			return text;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000BBA94 File Offset: 0x000B9C94
		internal new static bool CryptMsgControl([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara)
		{
			return CAPIUnsafe.CryptMsgControl(hCryptMsg, dwFlags, dwCtrlType, pvCtrlPara);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000BBA9F File Offset: 0x000B9C9F
		internal new static bool CryptMsgCountersign([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwIndex, [In] uint cCountersigners, [In] IntPtr rgCountersigners)
		{
			return CAPIUnsafe.CryptMsgCountersign(hCryptMsg, dwIndex, cCountersigners, rgCountersigners);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000BBAAA File Offset: 0x000B9CAA
		internal new static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] IntPtr pszInnerContentObjID, [In] IntPtr pStreamInfo)
		{
			return CAPIUnsafe.CryptMsgOpenToEncode(dwMsgEncodingType, dwFlags, dwMsgType, pvMsgEncodeInfo, pszInnerContentObjID, pStreamInfo);
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000BBAB9 File Offset: 0x000B9CB9
		internal new static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] string pszInnerContentObjID, [In] IntPtr pStreamInfo)
		{
			return CAPIUnsafe.CryptMsgOpenToEncode(dwMsgEncodingType, dwFlags, dwMsgType, pvMsgEncodeInfo, pszInnerContentObjID, pStreamInfo);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000BBAC8 File Offset: 0x000B9CC8
		internal new static bool CertSetCertificateContextProperty([In] IntPtr pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] IntPtr pvData)
		{
			if (pvData == IntPtr.Zero)
			{
				throw new ArgumentNullException("pvData");
			}
			if (dwPropId != 19U && dwPropId != 11U && dwPropId != 125U && dwPropId != 2U)
			{
				throw new ArgumentException(System.SR.GetString("Security_InvalidValue"), "dwFlags");
			}
			if (dwPropId == 19U || dwPropId == 11U || dwPropId == 2U)
			{
				new PermissionSet(PermissionState.Unrestricted).Demand();
			}
			return CAPIUnsafe.CertSetCertificateContextProperty(pCertContext, dwPropId, dwFlags, pvData);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000BBB38 File Offset: 0x000B9D38
		internal new static bool CertSetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] IntPtr pvData)
		{
			if (pvData == IntPtr.Zero)
			{
				throw new ArgumentNullException("pvData");
			}
			if (dwPropId != 19U && dwPropId != 11U && dwPropId != 125U && dwPropId != 2U)
			{
				throw new ArgumentException(System.SR.GetString("Security_InvalidValue"), "dwFlags");
			}
			if (dwPropId == 19U || dwPropId == 11U || dwPropId == 2U)
			{
				new PermissionSet(PermissionState.Unrestricted).Demand();
			}
			return CAPIUnsafe.CertSetCertificateContextProperty(pCertContext, dwPropId, dwFlags, pvData);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000BBBA8 File Offset: 0x000B9DA8
		internal new static bool CertSetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] SafeLocalAllocHandle safeLocalAllocHandle)
		{
			if (pCertContext == null)
			{
				throw new ArgumentNullException("pCertContext");
			}
			if (pCertContext.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "pCertContext");
			}
			if (dwPropId != 19U && dwPropId != 11U && dwPropId != 125U && dwPropId != 2U)
			{
				throw new ArgumentException(System.SR.GetString("Security_InvalidValue"), "dwFlags");
			}
			if (dwPropId == 19U || dwPropId == 11U || dwPropId == 2U)
			{
				new PermissionSet(PermissionState.Unrestricted).Demand();
			}
			return CAPIUnsafe.CertSetCertificateContextProperty(pCertContext, dwPropId, dwFlags, safeLocalAllocHandle);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000BBC2A File Offset: 0x000B9E2A
		internal new static SafeCertContextHandle CertDuplicateCertificateContext([In] IntPtr pCertContext)
		{
			if (pCertContext == IntPtr.Zero)
			{
				return SafeCertContextHandle.InvalidHandle;
			}
			return CAPISafe.CertDuplicateCertificateContext(pCertContext);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000BBC45 File Offset: 0x000B9E45
		internal new static SafeCertContextHandle CertDuplicateCertificateContext([In] SafeCertContextHandle pCertContext)
		{
			if (pCertContext == null || pCertContext.IsInvalid)
			{
				return SafeCertContextHandle.InvalidHandle;
			}
			return CAPISafe.CertDuplicateCertificateContext(pCertContext);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000BBC60 File Offset: 0x000B9E60
		internal new static IntPtr CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pPrevCertContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (pPrevCertContext == IntPtr.Zero)
			{
				StorePermission storePermission = new StorePermission(StorePermissionFlags.EnumerateCertificates);
				storePermission.Demand();
			}
			IntPtr intPtr = CAPIUnsafe.CertEnumCertificatesInStore(hCertStore, pPrevCertContext);
			if (intPtr == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != -2146885628)
				{
					CAPISafe.CertFreeCertificateContext(intPtr);
					throw new CryptographicException(lastWin32Error);
				}
			}
			return intPtr;
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000BBCE8 File Offset: 0x000B9EE8
		internal new static SafeCertContextHandle CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pPrevCertContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (pPrevCertContext.IsInvalid)
			{
				StorePermission storePermission = new StorePermission(StorePermissionFlags.EnumerateCertificates);
				storePermission.Demand();
			}
			SafeCertContextHandle safeCertContextHandle = CAPIUnsafe.CertEnumCertificatesInStore(hCertStore, pPrevCertContext);
			if (safeCertContextHandle == null || safeCertContextHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != -2146885628)
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return safeCertContextHandle;
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000BBD68 File Offset: 0x000B9F68
		internal unsafe static bool CryptQueryObject([In] uint dwObjectType, [In] object pvObject, [In] uint dwExpectedContentTypeFlags, [In] uint dwExpectedFormatTypeFlags, [In] uint dwFlags, [Out] IntPtr pdwMsgAndCertEncodingType, [Out] IntPtr pdwContentType, [Out] IntPtr pdwFormatType, [In] [Out] IntPtr phCertStore, [In] [Out] IntPtr phMsg, [In] [Out] IntPtr ppvContext)
		{
			bool flag = false;
			GCHandle gchandle = GCHandle.Alloc(pvObject, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			try
			{
				if (pvObject == null)
				{
					throw new ArgumentNullException("pvObject");
				}
				if (dwObjectType == 1U)
				{
					string fullPath = Path.GetFullPath((string)pvObject);
					new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
				}
				else
				{
					CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB;
					cryptoapi_BLOB.cbData = (uint)((byte[])pvObject).Length;
					cryptoapi_BLOB.pbData = intPtr;
					intPtr = new IntPtr((void*)(&cryptoapi_BLOB));
				}
				flag = CAPIUnsafe.CryptQueryObject(dwObjectType, intPtr, dwExpectedContentTypeFlags, dwExpectedFormatTypeFlags, dwFlags, pdwMsgAndCertEncodingType, pdwContentType, pdwFormatType, phCertStore, phMsg, ppvContext);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return flag;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000BBE14 File Offset: 0x000BA014
		internal unsafe static bool CryptQueryObject([In] uint dwObjectType, [In] object pvObject, [In] uint dwExpectedContentTypeFlags, [In] uint dwExpectedFormatTypeFlags, [In] uint dwFlags, [Out] IntPtr pdwMsgAndCertEncodingType, [Out] IntPtr pdwContentType, [Out] IntPtr pdwFormatType, [In] [Out] ref SafeCertStoreHandle phCertStore, [In] [Out] IntPtr phMsg, [In] [Out] IntPtr ppvContext)
		{
			bool flag = false;
			GCHandle gchandle = GCHandle.Alloc(pvObject, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			try
			{
				if (pvObject == null)
				{
					throw new ArgumentNullException("pvObject");
				}
				if (dwObjectType == 1U)
				{
					string fullPath = Path.GetFullPath((string)pvObject);
					new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
				}
				else
				{
					CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB;
					cryptoapi_BLOB.cbData = (uint)((byte[])pvObject).Length;
					cryptoapi_BLOB.pbData = intPtr;
					intPtr = new IntPtr((void*)(&cryptoapi_BLOB));
				}
				flag = CAPIUnsafe.CryptQueryObject(dwObjectType, intPtr, dwExpectedContentTypeFlags, dwExpectedFormatTypeFlags, dwFlags, pdwMsgAndCertEncodingType, pdwContentType, pdwFormatType, ref phCertStore, phMsg, ppvContext);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return flag;
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000BBEC0 File Offset: 0x000BA0C0
		internal unsafe static SafeCertStoreHandle PFXImportCertStore([In] uint dwObjectType, [In] object pvObject, [In] string szPassword, [In] uint dwFlags, [In] bool persistKeyContainers)
		{
			if (pvObject == null)
			{
				throw new ArgumentNullException("pvObject");
			}
			byte[] array;
			if (dwObjectType == 1U)
			{
				array = File.ReadAllBytes((string)pvObject);
			}
			else
			{
				array = (byte[])pvObject;
			}
			if (persistKeyContainers && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Create);
				keyContainerPermission.Demand();
			}
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			try
			{
				CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB;
				cryptoapi_BLOB.cbData = (uint)array.Length;
				cryptoapi_BLOB.pbData = intPtr;
				safeCertStoreHandle = CAPIUnsafe.PFXImportCertStore(new IntPtr((void*)(&cryptoapi_BLOB)), szPassword, dwFlags);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			if (!safeCertStoreHandle.IsInvalid && !persistKeyContainers)
			{
				IntPtr intPtr2 = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, IntPtr.Zero);
				while (intPtr2 != IntPtr.Zero)
				{
					CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB2 = default(CAPIBase.CRYPTOAPI_BLOB);
					if (!CAPI.CertSetCertificateContextProperty(intPtr2, 125U, 1073741824U, new IntPtr((void*)(&cryptoapi_BLOB2))))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					intPtr2 = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, intPtr2);
				}
			}
			return safeCertStoreHandle;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000BBFCC File Offset: 0x000BA1CC
		internal new static bool CertAddCertificateContextToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (pCertContext == null)
			{
				throw new ArgumentNullException("pCertContext");
			}
			if (pCertContext.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "pCertContext");
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AddToStore);
			storePermission.Demand();
			return CAPIUnsafe.CertAddCertificateContextToStore(hCertStore, pCertContext, dwAddDisposition, ppStoreContext);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000BC048 File Offset: 0x000BA248
		internal new static bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (pCertContext == null)
			{
				throw new ArgumentNullException("pCertContext");
			}
			if (pCertContext.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "pCertContext");
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AddToStore);
			storePermission.Demand();
			return CAPIUnsafe.CertAddCertificateLinkToStore(hCertStore, pCertContext, dwAddDisposition, ppStoreContext);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000BC0C4 File Offset: 0x000BA2C4
		internal new static bool CertDeleteCertificateFromStore([In] SafeCertContextHandle pCertContext)
		{
			if (pCertContext == null)
			{
				throw new ArgumentNullException("pCertContext");
			}
			if (pCertContext.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "pCertContext");
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.RemoveFromStore);
			storePermission.Demand();
			return CAPIUnsafe.CertDeleteCertificateFromStore(pCertContext);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000BC110 File Offset: 0x000BA310
		internal new static SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara)
		{
			if (lpszStoreProvider != new IntPtr(2L) && lpszStoreProvider != new IntPtr(10L))
			{
				throw new ArgumentException(System.SR.GetString("Security_InvalidValue"), "lpszStoreProvider");
			}
			if (((dwFlags & 131072U) == 131072U || (dwFlags & 524288U) == 524288U || (dwFlags & 589824U) == 589824U) && pvPara != null && pvPara.StartsWith("\\\\", StringComparison.Ordinal))
			{
				new PermissionSet(PermissionState.Unrestricted).Demand();
			}
			if ((dwFlags & 16U) == 16U)
			{
				StorePermission storePermission = new StorePermission(StorePermissionFlags.DeleteStore);
				storePermission.Demand();
			}
			else
			{
				StorePermission storePermission2 = new StorePermission(StorePermissionFlags.OpenStore);
				storePermission2.Demand();
			}
			if ((dwFlags & 8192U) == 8192U)
			{
				StorePermission storePermission3 = new StorePermission(StorePermissionFlags.CreateStore);
				storePermission3.Demand();
			}
			if ((dwFlags & 16384U) == 0U)
			{
				StorePermission storePermission4 = new StorePermission(StorePermissionFlags.CreateStore);
				storePermission4.Demand();
			}
			return CAPIUnsafe.CertOpenStore(lpszStoreProvider, dwMsgAndCertEncodingType, hCryptProv, dwFlags | 4U, pvPara);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000BC1FC File Offset: 0x000BA3FC
		internal new static SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] IntPtr pvFindPara, [In] SafeCertContextHandle pPrevCertContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "hCertStore");
			}
			return CAPIUnsafe.CertFindCertificateInStore(hCertStore, dwCertEncodingType, dwFindFlags, dwFindType, pvFindPara, pPrevCertContext);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000BC238 File Offset: 0x000BA438
		internal new static bool PFXExportCertStore([In] SafeCertStoreHandle hCertStore, [In] [Out] IntPtr pPFX, [In] string szPassword, [In] uint dwFlags)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Export);
				keyContainerPermission.Demand();
			}
			return CAPIUnsafe.PFXExportCertStore(hCertStore, pPFX, szPassword, dwFlags);
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000BC290 File Offset: 0x000BA490
		internal new static bool CertSaveStore([In] SafeCertStoreHandle hCertStore, [In] uint dwMsgAndCertEncodingType, [In] uint dwSaveAs, [In] uint dwSaveTo, [In] [Out] IntPtr pvSaveToPara, [In] uint dwFlags)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_InvalidHandle"), "hCertStore");
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.EnumerateCertificates);
			storePermission.Demand();
			if (dwSaveTo == 3U || dwSaveTo == 4U)
			{
				throw new ArgumentException(System.SR.GetString("Security_InvalidValue"), "pvSaveToPara");
			}
			return CAPIUnsafe.CertSaveStore(hCertStore, dwMsgAndCertEncodingType, dwSaveAs, dwSaveTo, pvSaveToPara, dwFlags);
		}
	}
}
