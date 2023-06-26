using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000454 RID: 1108
	[SuppressUnmanagedCodeSecurity]
	internal abstract class CAPIUnsafe : CAPISafe
	{
		// Token: 0x0600291D RID: 10525
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "CryptAcquireContextA")]
		protected internal static extern bool CryptAcquireContext([In] [Out] ref SafeCryptProvHandle hCryptProv, [MarshalAs(UnmanagedType.LPStr)] [In] string pszContainer, [MarshalAs(UnmanagedType.LPStr)] [In] string pszProvider, [In] uint dwProvType, [In] uint dwFlags);

		// Token: 0x0600291E RID: 10526
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertAddCertificateContextToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext);

		// Token: 0x0600291F RID: 10527
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext);

		// Token: 0x06002920 RID: 10528
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertDeleteCertificateFromStore([In] SafeCertContextHandle pCertContext);

		// Token: 0x06002921 RID: 10529
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern IntPtr CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pPrevCertContext);

		// Token: 0x06002922 RID: 10530
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCertContextHandle CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pPrevCertContext);

		// Token: 0x06002923 RID: 10531
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] IntPtr pvFindPara, [In] SafeCertContextHandle pPrevCertContext);

		// Token: 0x06002924 RID: 10532
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		protected internal static extern SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara);

		// Token: 0x06002925 RID: 10533
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSaveStore([In] SafeCertStoreHandle hCertStore, [In] uint dwMsgAndCertEncodingType, [In] uint dwSaveAs, [In] uint dwSaveTo, [In] [Out] IntPtr pvSaveToPara, [In] uint dwFlags);

		// Token: 0x06002926 RID: 10534
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSetCertificateContextProperty([In] IntPtr pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] IntPtr pvData);

		// Token: 0x06002927 RID: 10535
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] IntPtr pvData);

		// Token: 0x06002928 RID: 10536
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] SafeLocalAllocHandle safeLocalAllocHandle);

		// Token: 0x06002929 RID: 10537
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCertContextHandle CertCreateSelfSignCertificate([In] SafeCryptProvHandle hProv, [In] IntPtr pSubjectIssuerBlob, [In] uint dwFlags, [In] IntPtr pKeyProvInfo, [In] IntPtr pSignatureAlgorithm, [In] IntPtr pStartTime, [In] IntPtr pEndTime, [In] IntPtr pExtensions);

		// Token: 0x0600292A RID: 10538
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptMsgControl([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara);

		// Token: 0x0600292B RID: 10539
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptMsgCountersign([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwIndex, [In] uint cCountersigners, [In] IntPtr rgCountersigners);

		// Token: 0x0600292C RID: 10540
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] IntPtr pszInnerContentObjID, [In] IntPtr pStreamInfo);

		// Token: 0x0600292D RID: 10541
		[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [MarshalAs(UnmanagedType.LPStr)] [In] string pszInnerContentObjID, [In] IntPtr pStreamInfo);

		// Token: 0x0600292E RID: 10542
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptQueryObject([In] uint dwObjectType, [In] IntPtr pvObject, [In] uint dwExpectedContentTypeFlags, [In] uint dwExpectedFormatTypeFlags, [In] uint dwFlags, [Out] IntPtr pdwMsgAndCertEncodingType, [Out] IntPtr pdwContentType, [Out] IntPtr pdwFormatType, [In] [Out] IntPtr phCertStore, [In] [Out] IntPtr phMsg, [In] [Out] IntPtr ppvContext);

		// Token: 0x0600292F RID: 10543
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptQueryObject([In] uint dwObjectType, [In] IntPtr pvObject, [In] uint dwExpectedContentTypeFlags, [In] uint dwExpectedFormatTypeFlags, [In] uint dwFlags, [Out] IntPtr pdwMsgAndCertEncodingType, [Out] IntPtr pdwContentType, [Out] IntPtr pdwFormatType, [In] [Out] ref SafeCertStoreHandle phCertStore, [In] [Out] IntPtr phMsg, [In] [Out] IntPtr ppvContext);

		// Token: 0x06002930 RID: 10544
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptProtectData([In] IntPtr pDataIn, [In] string szDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob);

		// Token: 0x06002931 RID: 10545
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptUnprotectData([In] IntPtr pDataIn, [In] IntPtr ppszDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob);

		// Token: 0x06002932 RID: 10546
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		protected internal static extern bool PFXExportCertStore([In] SafeCertStoreHandle hStore, [In] [Out] IntPtr pPFX, [In] string szPassword, [In] uint dwFlags);

		// Token: 0x06002933 RID: 10547
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		protected internal static extern SafeCertStoreHandle PFXImportCertStore([In] IntPtr pPFX, [In] string szPassword, [In] uint dwFlags);
	}
}
