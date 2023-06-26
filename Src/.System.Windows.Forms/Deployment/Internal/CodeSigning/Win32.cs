using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x0200000A RID: 10
	internal static class Win32
	{
		// Token: 0x06000018 RID: 24
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetProcessHeap();

		// Token: 0x06000019 RID: 25
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool HeapFree([In] IntPtr hHeap, [In] uint dwFlags, [In] IntPtr lpMem);

		// Token: 0x0600001A RID: 26
		[DllImport("clr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int CertTimestampAuthenticodeLicense([In] ref Win32.CRYPT_DATA_BLOB pSignedLicenseBlob, [In] string pwszTimestampURI, [In] [Out] ref Win32.CRYPT_DATA_BLOB pTimestampSignatureBlob);

		// Token: 0x0600001B RID: 27
		[DllImport("clr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int CertVerifyAuthenticodeLicense([In] ref Win32.CRYPT_DATA_BLOB pLicenseBlob, [In] uint dwFlags, [In] [Out] ref Win32.AXL_SIGNER_INFO pSignerInfo, [In] [Out] ref Win32.AXL_TIMESTAMPER_INFO pTimestamperInfo);

		// Token: 0x0600001C RID: 28
		[DllImport("clr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int CertFreeAuthenticodeSignerInfo([In] ref Win32.AXL_SIGNER_INFO pSignerInfo);

		// Token: 0x0600001D RID: 29
		[DllImport("clr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int CertFreeAuthenticodeTimestamperInfo([In] ref Win32.AXL_TIMESTAMPER_INFO pTimestamperInfo);

		// Token: 0x0600001E RID: 30
		[DllImport("clr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int _AxlGetIssuerPublicKeyHash([In] IntPtr pCertContext, [In] [Out] ref IntPtr ppwszPublicKeyHash);

		// Token: 0x0600001F RID: 31
		[DllImport("clr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int _AxlRSAKeyValueToPublicKeyToken([In] ref Win32.CRYPT_DATA_BLOB pModulusBlob, [In] ref Win32.CRYPT_DATA_BLOB pExponentBlob, [In] [Out] ref IntPtr ppwszPublicKeyToken);

		// Token: 0x06000020 RID: 32
		[DllImport("clr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int _AxlPublicKeyBlobToPublicKeyToken([In] ref Win32.CRYPT_DATA_BLOB pCspPublicKeyBlob, [In] [Out] ref IntPtr ppwszPublicKeyToken);

		// Token: 0x06000021 RID: 33
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("crypt32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptRetrieveTimeStamp([MarshalAs(UnmanagedType.LPWStr)] [In] string wszUrl, [In] uint dwRetrievalFlags, [In] int dwTimeout, [MarshalAs(UnmanagedType.LPStr)] [In] string pszHashId, [In] [Out] ref Win32.CRYPT_TIMESTAMP_PARA pPara, [In] byte[] pbData, [In] int cbData, [In] [Out] ref IntPtr ppTsContext, [In] [Out] ref IntPtr ppTsSigner, [In] [Out] ref IntPtr phStore);

		// Token: 0x06000022 RID: 34
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("crypt32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptVerifyTimeStampSignature([In] byte[] pbTSContentInfo, [In] int cbTSContentInfo, [In] byte[] pbData, [In] int cbData, [In] IntPtr hAdditionalStore, [In] [Out] ref IntPtr ppTsContext, [In] [Out] ref IntPtr ppTsSigner, [In] [Out] ref IntPtr phStore);

		// Token: 0x06000023 RID: 35
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("crypt32.dll", SetLastError = true)]
		internal static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x06000024 RID: 36
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("crypt32.dll", SetLastError = true)]
		internal static extern bool CertCloseStore(IntPtr pCertContext, int dwFlags);

		// Token: 0x06000025 RID: 37
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("crypt32.dll")]
		internal static extern void CryptMemFree(IntPtr pv);

		// Token: 0x0400008C RID: 140
		internal const string CRYPT32 = "crypt32.dll";

		// Token: 0x0400008D RID: 141
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x0400008E RID: 142
		internal const string MSCORWKS = "clr.dll";

		// Token: 0x0400008F RID: 143
		internal const int S_OK = 0;

		// Token: 0x04000090 RID: 144
		internal const int NTE_BAD_HASH = -2146893822;

		// Token: 0x04000091 RID: 145
		internal const int NTE_BAD_KEY = -2146893821;

		// Token: 0x04000092 RID: 146
		internal const int TRUST_E_SYSTEM_ERROR = -2146869247;

		// Token: 0x04000093 RID: 147
		internal const int TRUST_E_NO_SIGNER_CERT = -2146869246;

		// Token: 0x04000094 RID: 148
		internal const int TRUST_E_COUNTER_SIGNER = -2146869245;

		// Token: 0x04000095 RID: 149
		internal const int TRUST_E_CERT_SIGNATURE = -2146869244;

		// Token: 0x04000096 RID: 150
		internal const int TRUST_E_TIME_STAMP = -2146869243;

		// Token: 0x04000097 RID: 151
		internal const int TRUST_E_BAD_DIGEST = -2146869232;

		// Token: 0x04000098 RID: 152
		internal const int TRUST_E_BASIC_CONSTRAINTS = -2146869223;

		// Token: 0x04000099 RID: 153
		internal const int TRUST_E_FINANCIAL_CRITERIA = -2146869218;

		// Token: 0x0400009A RID: 154
		internal const int TRUST_E_PROVIDER_UNKNOWN = -2146762751;

		// Token: 0x0400009B RID: 155
		internal const int TRUST_E_ACTION_UNKNOWN = -2146762750;

		// Token: 0x0400009C RID: 156
		internal const int TRUST_E_SUBJECT_FORM_UNKNOWN = -2146762749;

		// Token: 0x0400009D RID: 157
		internal const int TRUST_E_SUBJECT_NOT_TRUSTED = -2146762748;

		// Token: 0x0400009E RID: 158
		internal const int TRUST_E_NOSIGNATURE = -2146762496;

		// Token: 0x0400009F RID: 159
		internal const int CERT_E_UNTRUSTEDROOT = -2146762487;

		// Token: 0x040000A0 RID: 160
		internal const int TRUST_E_FAIL = -2146762485;

		// Token: 0x040000A1 RID: 161
		internal const int TRUST_E_EXPLICIT_DISTRUST = -2146762479;

		// Token: 0x040000A2 RID: 162
		internal const int CERT_E_CHAINING = -2146762486;

		// Token: 0x040000A3 RID: 163
		internal const int AXL_REVOCATION_NO_CHECK = 1;

		// Token: 0x040000A4 RID: 164
		internal const int AXL_REVOCATION_CHECK_END_CERT_ONLY = 2;

		// Token: 0x040000A5 RID: 165
		internal const int AXL_REVOCATION_CHECK_ENTIRE_CHAIN = 4;

		// Token: 0x040000A6 RID: 166
		internal const int AXL_URL_CACHE_ONLY_RETRIEVAL = 8;

		// Token: 0x040000A7 RID: 167
		internal const int AXL_LIFETIME_SIGNING = 16;

		// Token: 0x040000A8 RID: 168
		internal const int AXL_TRUST_MICROSOFT_ROOT_ONLY = 32;

		// Token: 0x040000A9 RID: 169
		internal const int WTPF_IGNOREREVOKATION = 512;

		// Token: 0x040000AA RID: 170
		internal const int WTPF_IGNOREREVOCATIONONTS = 131072;

		// Token: 0x040000AB RID: 171
		internal const string szOID_KP_LIFETIME_SIGNING = "1.3.6.1.4.1.311.10.3.13";

		// Token: 0x040000AC RID: 172
		internal const string szOID_RSA_signingTime = "1.2.840.113549.1.9.5";

		// Token: 0x040000AD RID: 173
		internal const string szOID_OIWSEC_sha1 = "1.3.14.3.2.26";

		// Token: 0x040000AE RID: 174
		internal const string szOID_NIST_sha256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x040000AF RID: 175
		internal const string szOID_RSA_messageDigest = "1.2.840.113549.1.9.4";

		// Token: 0x040000B0 RID: 176
		internal const string szOID_PKIX_KP_TIMESTAMP_SIGNING = "1.3.6.1.5.5.7.3.8";

		// Token: 0x02000517 RID: 1303
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_DATA_BLOB
		{
			// Token: 0x0400376A RID: 14186
			internal uint cbData;

			// Token: 0x0400376B RID: 14187
			internal IntPtr pbData;
		}

		// Token: 0x02000518 RID: 1304
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct AXL_SIGNER_INFO
		{
			// Token: 0x0400376C RID: 14188
			internal uint cbSize;

			// Token: 0x0400376D RID: 14189
			internal uint dwError;

			// Token: 0x0400376E RID: 14190
			internal uint algHash;

			// Token: 0x0400376F RID: 14191
			internal IntPtr pwszHash;

			// Token: 0x04003770 RID: 14192
			internal IntPtr pwszDescription;

			// Token: 0x04003771 RID: 14193
			internal IntPtr pwszDescriptionUrl;

			// Token: 0x04003772 RID: 14194
			internal IntPtr pChainContext;
		}

		// Token: 0x02000519 RID: 1305
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct AXL_TIMESTAMPER_INFO
		{
			// Token: 0x04003773 RID: 14195
			internal uint cbSize;

			// Token: 0x04003774 RID: 14196
			internal uint dwError;

			// Token: 0x04003775 RID: 14197
			internal uint algHash;

			// Token: 0x04003776 RID: 14198
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftTimestamp;

			// Token: 0x04003777 RID: 14199
			internal IntPtr pChainContext;
		}

		// Token: 0x0200051A RID: 1306
		internal struct CRYPT_TIMESTAMP_CONTEXT
		{
			// Token: 0x04003778 RID: 14200
			internal uint cbEncoded;

			// Token: 0x04003779 RID: 14201
			internal IntPtr pbEncoded;

			// Token: 0x0400377A RID: 14202
			internal IntPtr pTimeStamp;
		}

		// Token: 0x0200051B RID: 1307
		internal struct CRYPT_TIMESTAMP_INFO
		{
			// Token: 0x0400377B RID: 14203
			internal int dwVersion;

			// Token: 0x0400377C RID: 14204
			internal IntPtr pszTSAPolicyId;

			// Token: 0x0400377D RID: 14205
			internal Win32.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;

			// Token: 0x0400377E RID: 14206
			internal Win32.CRYPTOAPI_BLOB HashedMessage;

			// Token: 0x0400377F RID: 14207
			internal Win32.CRYPTOAPI_BLOB SerialNumber;

			// Token: 0x04003780 RID: 14208
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftTime;

			// Token: 0x04003781 RID: 14209
			internal IntPtr pvAccuracy;

			// Token: 0x04003782 RID: 14210
			[MarshalAs(UnmanagedType.Bool)]
			internal bool fOrdering;

			// Token: 0x04003783 RID: 14211
			internal Win32.CRYPTOAPI_BLOB Nonce;

			// Token: 0x04003784 RID: 14212
			internal Win32.CRYPTOAPI_BLOB Tsa;

			// Token: 0x04003785 RID: 14213
			internal int cExtension;

			// Token: 0x04003786 RID: 14214
			internal IntPtr rgExtension;
		}

		// Token: 0x0200051C RID: 1308
		internal struct CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x04003787 RID: 14215
			internal IntPtr pszOid;

			// Token: 0x04003788 RID: 14216
			internal Win32.CRYPTOAPI_BLOB Parameters;
		}

		// Token: 0x0200051D RID: 1309
		internal struct CRYPTOAPI_BLOB
		{
			// Token: 0x04003789 RID: 14217
			internal uint cbData;

			// Token: 0x0400378A RID: 14218
			internal IntPtr pbData;
		}

		// Token: 0x0200051E RID: 1310
		internal struct CRYPT_TIMESTAMP_PARA
		{
			// Token: 0x0400378B RID: 14219
			internal IntPtr pszTSAPolicyId;

			// Token: 0x0400378C RID: 14220
			internal bool fRequestCerts;

			// Token: 0x0400378D RID: 14221
			internal Win32.CRYPTOAPI_BLOB Nonce;

			// Token: 0x0400378E RID: 14222
			internal int cExtension;

			// Token: 0x0400378F RID: 14223
			internal IntPtr rgExtension;
		}
	}
}
