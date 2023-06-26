using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000453 RID: 1107
	[SuppressUnmanagedCodeSecurity]
	internal abstract class CAPISafe : CAPINative
	{
		// Token: 0x060028ED RID: 10477
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern uint FormatMessage([In] uint dwFlags, [In] IntPtr lpSource, [In] uint dwMessageId, [In] uint dwLanguageId, [In] [Out] StringBuilder lpBuffer, [In] uint nSize, [In] IntPtr Arguments);

		// Token: 0x060028EE RID: 10478
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FreeLibrary([In] IntPtr hModule);

		// Token: 0x060028EF RID: 10479
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetProcAddress([In] IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

		// Token: 0x060028F0 RID: 10480
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeLocalAllocHandle LocalAlloc([In] uint uFlags, [In] IntPtr sizetdwBytes);

		// Token: 0x060028F1 RID: 10481
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "LoadLibraryA", SetLastError = true)]
		internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] [In] string lpFileName);

		// Token: 0x060028F2 RID: 10482
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertControlStore([In] SafeCertStoreHandle hCertStore, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara);

		// Token: 0x060028F3 RID: 10483
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeCertContextHandle CertCreateCertificateContext([In] uint dwCertEncodingType, [In] SafeLocalAllocHandle pbCertEncoded, [In] uint cbCertEncoded);

		// Token: 0x060028F4 RID: 10484
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeCertContextHandle CertDuplicateCertificateContext([In] IntPtr pCertContext);

		// Token: 0x060028F5 RID: 10485
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeCertContextHandle CertDuplicateCertificateContext([In] SafeCertContextHandle pCertContext);

		// Token: 0x060028F6 RID: 10486
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeX509ChainHandle CertDuplicateCertificateChain([In] IntPtr pChainContext);

		// Token: 0x060028F7 RID: 10487
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeX509ChainHandle CertDuplicateCertificateChain([In] SafeX509ChainHandle pChainContext);

		// Token: 0x060028F8 RID: 10488
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeCertStoreHandle CertDuplicateStore([In] IntPtr hCertStore);

		// Token: 0x060028F9 RID: 10489
		[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr CertFindExtension([MarshalAs(UnmanagedType.LPStr)] [In] string pszObjId, [In] uint cExtensions, [In] IntPtr rgExtensions);

		// Token: 0x060028FA RID: 10490
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertFreeCertificateContext([In] IntPtr pCertContext);

		// Token: 0x060028FB RID: 10491
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertGetCertificateChain([In] IntPtr hChainEngine, [In] SafeCertContextHandle pCertContext, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME pTime, [In] SafeCertStoreHandle hAdditionalStore, [In] ref CAPIBase.CERT_CHAIN_PARA pChainPara, [In] uint dwFlags, [In] IntPtr pvReserved, [In] [Out] ref SafeX509ChainHandle ppChainContext);

		// Token: 0x060028FC RID: 10492
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertGetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] [Out] SafeLocalAllocHandle pvData, [In] [Out] ref uint pcbData);

		// Token: 0x060028FD RID: 10493
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertGetIntendedKeyUsage([In] uint dwCertEncodingType, [In] IntPtr pCertInfo, [In] IntPtr pbKeyUsage, [In] [Out] uint cbKeyUsage);

		// Token: 0x060028FE RID: 10494
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern uint CertGetNameStringW([In] SafeCertContextHandle pCertContext, [In] uint dwType, [In] uint dwFlags, [In] IntPtr pvTypePara, [In] [Out] SafeLocalAllocHandle pszNameString, [In] uint cchNameString);

		// Token: 0x060028FF RID: 10495
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern uint CertGetPublicKeyLength([In] uint dwCertEncodingType, [In] IntPtr pPublicKey);

		// Token: 0x06002900 RID: 10496
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertGetValidUsages([In] uint cCerts, [In] IntPtr rghCerts, [Out] IntPtr cNumOIDs, [In] [Out] SafeLocalAllocHandle rghOIDs, [In] [Out] IntPtr pcbOIDs);

		// Token: 0x06002901 RID: 10497
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern uint CertNameToStrW([In] uint dwCertEncodingType, [In] IntPtr pName, [In] uint dwStrType, [In] [Out] SafeLocalAllocHandle psz, [In] uint csz);

		// Token: 0x06002902 RID: 10498
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertSerializeCertificateStoreElement([In] SafeCertContextHandle pCertContext, [In] uint dwFlags, [In] [Out] SafeLocalAllocHandle pbElement, [In] [Out] IntPtr pcbElement);

		// Token: 0x06002903 RID: 10499
		[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertStrToNameW([In] uint dwCertEncodingType, [In] string pszX500, [In] uint dwStrType, [In] IntPtr pvReserved, [In] [Out] IntPtr pbEncoded, [In] [Out] ref uint pcbEncoded, [In] [Out] IntPtr ppszError);

		// Token: 0x06002904 RID: 10500
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int CertVerifyTimeValidity([In] [Out] ref System.Runtime.InteropServices.ComTypes.FILETIME pTimeToVerify, [In] IntPtr pCertInfo);

		// Token: 0x06002905 RID: 10501
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertVerifyCertificateChainPolicy([In] IntPtr pszPolicyOID, [In] SafeX509ChainHandle pChainContext, [In] ref CAPIBase.CERT_CHAIN_POLICY_PARA pPolicyPara, [In] [Out] ref CAPIBase.CERT_CHAIN_POLICY_STATUS pPolicyStatus);

		// Token: 0x06002906 RID: 10502
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptAcquireCertificatePrivateKey([In] SafeCertContextHandle pCert, [In] uint dwFlags, [In] IntPtr pvReserved, [In] [Out] ref SafeCryptProvHandle phCryptProv, [In] [Out] ref uint pdwKeySpec, [In] [Out] ref bool pfCallerFreeProv);

		// Token: 0x06002907 RID: 10503
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] IntPtr pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In] [Out] SafeLocalAllocHandle pvStructInfo, [In] [Out] IntPtr pcbStructInfo);

		// Token: 0x06002908 RID: 10504
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] byte[] pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In] [Out] SafeLocalAllocHandle pvStructInfo, [In] [Out] IntPtr pcbStructInfo);

		// Token: 0x06002909 RID: 10505
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptEncodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] IntPtr pvStructInfo, [In] [Out] SafeLocalAllocHandle pbEncoded, [In] [Out] IntPtr pcbEncoded);

		// Token: 0x0600290A RID: 10506
		[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptEncodeObject([In] uint dwCertEncodingType, [MarshalAs(UnmanagedType.LPStr)] [In] string lpszStructType, [In] IntPtr pvStructInfo, [In] [Out] SafeLocalAllocHandle pbEncoded, [In] [Out] IntPtr pcbEncoded);

		// Token: 0x0600290B RID: 10507
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] OidGroup dwGroupId);

		// Token: 0x0600290C RID: 10508
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] OidGroup dwGroupId);

		// Token: 0x0600290D RID: 10509
		[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptFormatObject([In] uint dwCertEncodingType, [In] uint dwFormatType, [In] uint dwFormatStrType, [In] IntPtr pFormatStruct, [MarshalAs(UnmanagedType.LPStr)] [In] string lpszStructType, [In] byte[] pbEncoded, [In] uint cbEncoded, [In] [Out] SafeLocalAllocHandle pbFormat, [In] [Out] IntPtr pcbFormat);

		// Token: 0x0600290E RID: 10510
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptFormatObject([In] uint dwCertEncodingType, [In] uint dwFormatType, [In] uint dwFormatStrType, [In] IntPtr pFormatStruct, [In] IntPtr lpszStructType, [In] byte[] pbEncoded, [In] uint cbEncoded, [In] [Out] SafeLocalAllocHandle pbFormat, [In] [Out] IntPtr pcbFormat);

		// Token: 0x0600290F RID: 10511
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptGetProvParam([In] SafeCryptProvHandle hProv, [In] uint dwParam, [In] IntPtr pbData, [In] IntPtr pdwDataLen, [In] uint dwFlags);

		// Token: 0x06002910 RID: 10512
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptHashCertificate([In] IntPtr hCryptProv, [In] uint Algid, [In] uint dwFlags, [In] IntPtr pbEncoded, [In] uint cbEncoded, [Out] IntPtr pbComputedHash, [In] [Out] IntPtr pcbComputedHash);

		// Token: 0x06002911 RID: 10513
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptHashPublicKeyInfo([In] IntPtr hCryptProv, [In] uint Algid, [In] uint dwFlags, [In] uint dwCertEncodingType, [In] IntPtr pInfo, [Out] IntPtr pbComputedHash, [In] [Out] IntPtr pcbComputedHash);

		// Token: 0x06002912 RID: 10514
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptMsgGetParam([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwParamType, [In] uint dwIndex, [In] [Out] IntPtr pvData, [In] [Out] IntPtr pcbData);

		// Token: 0x06002913 RID: 10515
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptMsgGetParam([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwParamType, [In] uint dwIndex, [In] [Out] SafeLocalAllocHandle pvData, [In] [Out] IntPtr pcbData);

		// Token: 0x06002914 RID: 10516
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeCryptMsgHandle CryptMsgOpenToDecode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr hCryptProv, [In] IntPtr pRecipientInfo, [In] IntPtr pStreamInfo);

		// Token: 0x06002915 RID: 10517
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptMsgUpdate([In] SafeCryptMsgHandle hCryptMsg, [In] byte[] pbData, [In] uint cbData, [In] bool fFinal);

		// Token: 0x06002916 RID: 10518
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptMsgUpdate([In] SafeCryptMsgHandle hCryptMsg, [In] IntPtr pbData, [In] uint cbData, [In] bool fFinal);

		// Token: 0x06002917 RID: 10519
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptMsgVerifyCountersignatureEncoded([In] IntPtr hCryptProv, [In] uint dwEncodingType, [In] IntPtr pbSignerInfo, [In] uint cbSignerInfo, [In] IntPtr pbSignerInfoCountersignature, [In] uint cbSignerInfoCountersignature, [In] IntPtr pciCountersigner);

		// Token: 0x06002918 RID: 10520
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern void SetLastError(uint dwErrorCode);

		// Token: 0x06002919 RID: 10521
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x0600291A RID: 10522
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern void ZeroMemory(IntPtr handle, uint length);

		// Token: 0x0600291B RID: 10523
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern int LsaNtStatusToWinError([In] int status);
	}
}
