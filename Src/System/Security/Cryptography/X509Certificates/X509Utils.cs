using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000481 RID: 1153
	internal class X509Utils
	{
		// Token: 0x06002A9D RID: 10909 RVA: 0x000C2376 File Offset: 0x000C0576
		private X509Utils()
		{
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000C237E File Offset: 0x000C057E
		internal static bool IsCertRdnCharString(uint dwValueType)
		{
			return (dwValueType & 255U) >= 3U;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000C2390 File Offset: 0x000C0590
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

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000C23E4 File Offset: 0x000C05E4
		internal static uint MapKeyStorageFlags(X509KeyStorageFlags keyStorageFlags)
		{
			if (LocalAppContextSwitches.DoNotValidateX509KeyStorageFlags)
			{
				keyStorageFlags &= X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet;
			}
			if ((keyStorageFlags & (X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet)) != keyStorageFlags)
			{
				throw new ArgumentException(SR.GetString("Arg_EnumIllegalVal", new object[] { (int)keyStorageFlags }), "keyStorageFlags");
			}
			X509KeyStorageFlags x509KeyStorageFlags = keyStorageFlags & (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet);
			if (x509KeyStorageFlags == (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet))
			{
				throw new ArgumentException(SR.GetString("Cryptography_X509_InvalidFlagCombination", new object[] { x509KeyStorageFlags }), "keyStorageFlags");
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

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000C249C File Offset: 0x000C069C
		internal static uint MapX509StoreFlags(StoreLocation storeLocation, OpenFlags flags)
		{
			uint num = 0U;
			uint num2 = (uint)(flags & (OpenFlags.ReadWrite | OpenFlags.MaxAllowed));
			if (num2 != 0U)
			{
				if (num2 == 2U)
				{
					num |= 4096U;
				}
			}
			else
			{
				num |= 32768U;
			}
			if ((flags & OpenFlags.OpenExistingOnly) == OpenFlags.OpenExistingOnly)
			{
				num |= 16384U;
			}
			if ((flags & OpenFlags.IncludeArchived) == OpenFlags.IncludeArchived)
			{
				num |= 512U;
			}
			if (storeLocation == StoreLocation.LocalMachine)
			{
				num |= 131072U;
			}
			else if (storeLocation == StoreLocation.CurrentUser)
			{
				num |= 65536U;
			}
			return num;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000C2504 File Offset: 0x000C0704
		internal static uint MapNameType(X509NameType nameType)
		{
			uint num;
			switch (nameType)
			{
			case X509NameType.SimpleName:
				num = 4U;
				break;
			case X509NameType.EmailName:
				num = 1U;
				break;
			case X509NameType.UpnName:
				num = 8U;
				break;
			case X509NameType.DnsName:
			case X509NameType.DnsFromAlternativeName:
				num = 6U;
				break;
			case X509NameType.UrlName:
				num = 7U;
				break;
			default:
				throw new ArgumentException(SR.GetString("Argument_InvalidNameType"));
			}
			return num;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000C2558 File Offset: 0x000C0758
		internal static uint MapRevocationFlags(X509RevocationMode revocationMode, X509RevocationFlag revocationFlag)
		{
			uint num = 0U;
			if (revocationMode == X509RevocationMode.NoCheck)
			{
				return num;
			}
			if (revocationMode == X509RevocationMode.Offline)
			{
				num |= 2147483648U;
			}
			if (revocationFlag == X509RevocationFlag.EndCertificateOnly)
			{
				num |= 268435456U;
			}
			else if (revocationFlag == X509RevocationFlag.EntireChain)
			{
				num |= 536870912U;
			}
			else
			{
				num |= 1073741824U;
			}
			return num;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000C259C File Offset: 0x000C079C
		internal static string EncodeHexString(byte[] sArray)
		{
			return X509Utils.EncodeHexString(sArray, 0U, (uint)sArray.Length);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000C25A8 File Offset: 0x000C07A8
		internal static string EncodeHexString(byte[] sArray, uint start, uint end)
		{
			string text = null;
			if (sArray != null)
			{
				char[] array = new char[(end - start) * 2U];
				uint num = start;
				uint num2 = 0U;
				while (num < end)
				{
					uint num3 = (uint)((sArray[(int)num] & 240) >> 4);
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
					num3 = (uint)(sArray[(int)num] & 15);
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
					num += 1U;
				}
				text = new string(array);
			}
			return text;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000C2610 File Offset: 0x000C0810
		internal static string EncodeHexStringFromInt(byte[] sArray, uint start, uint end)
		{
			string text = null;
			if (sArray != null)
			{
				char[] array = new char[(end - start) * 2U];
				uint num = end;
				uint num2 = 0U;
				while (num-- > start)
				{
					uint num3 = (uint)(sArray[(int)num] & 240) >> 4;
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
					num3 = (uint)(sArray[(int)num] & 15);
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
				}
				text = new string(array);
			}
			return text;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000C2677 File Offset: 0x000C0877
		internal static byte HexToByte(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (byte)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (byte)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (byte)(val - 'A' + '\n');
			}
			return byte.MaxValue;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000C26B4 File Offset: 0x000C08B4
		internal static uint AlignedLength(uint length)
		{
			return (length + 7U) & 4294967288U;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000C26BC File Offset: 0x000C08BC
		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return X509Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000C26CC File Offset: 0x000C08CC
		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000C2738 File Offset: 0x000C0938
		internal static byte[] DecodeHexString(string s)
		{
			string text = X509Utils.DiscardWhiteSpaces(s);
			uint num = (uint)(text.Length / 2);
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num))
			{
				array[num3] = (byte)(((int)X509Utils.HexToByte(text[num2]) << 4) | (int)X509Utils.HexToByte(text[num2 + 1]));
				num2 += 2;
				num3++;
			}
			return array;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000C2798 File Offset: 0x000C0998
		internal static int GetHexArraySize(byte[] hex)
		{
			int num = hex.Length;
			while (num-- > 0 && hex[num] == 0)
			{
			}
			return num + 1;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000C27BC File Offset: 0x000C09BC
		internal static SafeLocalAllocHandle ByteToPtr(byte[] managed)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(managed.Length));
			Marshal.Copy(managed, 0, safeLocalAllocHandle.DangerousGetHandle(), managed.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000C27EC File Offset: 0x000C09EC
		internal unsafe static void memcpy(IntPtr source, IntPtr dest, uint size)
		{
			for (uint num = 0U; num < size; num += 1U)
			{
				*(UIntPtr)((long)dest + (long)((ulong)num)) = Marshal.ReadByte(new IntPtr((long)source + (long)((ulong)num)));
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000C2824 File Offset: 0x000C0A24
		internal static byte[] PtrToByte(IntPtr unmanaged, uint size)
		{
			byte[] array = new byte[size];
			Marshal.Copy(unmanaged, array, 0, array.Length);
			return array;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000C2844 File Offset: 0x000C0A44
		internal unsafe static bool MemEqual(byte* pbBuf1, uint cbBuf1, byte* pbBuf2, uint cbBuf2)
		{
			if (cbBuf1 != cbBuf2)
			{
				return false;
			}
			while (cbBuf1-- > 0U)
			{
				if (*(pbBuf1++) != *(pbBuf2++))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000C2868 File Offset: 0x000C0A68
		internal static SafeLocalAllocHandle StringToAnsiPtr(string s)
		{
			byte[] array = new byte[s.Length + 1];
			Encoding.ASCII.GetBytes(s, 0, s.Length, array, 0);
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(array.Length));
			Marshal.Copy(array, 0, safeLocalAllocHandle.DangerousGetHandle(), array.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000C28B8 File Offset: 0x000C0AB8
		internal static SafeLocalAllocHandle StringToUniPtr(string s)
		{
			byte[] array = new byte[2 * (s.Length + 1)];
			Encoding.Unicode.GetBytes(s, 0, s.Length, array, 0);
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(array.Length));
			Marshal.Copy(array, 0, safeLocalAllocHandle.DangerousGetHandle(), array.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000C290C File Offset: 0x000C0B0C
		internal static SafeCertStoreHandle ExportToMemoryStore(X509Certificate2Collection collection)
		{
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(2L), 65537U, IntPtr.Zero, 8704U, null);
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			foreach (X509Certificate2 x509Certificate in collection)
			{
				if (!CAPI.CertAddCertificateLinkToStore(safeCertStoreHandle, x509Certificate.CertContext, 4U, SafeCertContextHandle.InvalidHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return safeCertStoreHandle;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000C299C File Offset: 0x000C0B9C
		internal static uint OidToAlgId(string value)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = X509Utils.StringToAnsiPtr(value);
			CAPIBase.CRYPT_OID_INFO crypt_OID_INFO = CAPI.CryptFindOIDInfo(1U, safeLocalAllocHandle, OidGroup.All);
			return crypt_OID_INFO.Algid;
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000C29C0 File Offset: 0x000C0BC0
		internal static string FindOidInfo(uint keyType, string keyValue, OidGroup oidGroup)
		{
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			if (keyValue.Length == 0)
			{
				return null;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			string text;
			try
			{
				if (keyType != 1U)
				{
					if (keyType == 2U)
					{
						safeLocalAllocHandle = X509Utils.StringToUniPtr(keyValue);
					}
				}
				else
				{
					safeLocalAllocHandle = X509Utils.StringToAnsiPtr(keyValue);
				}
				CAPIBase.CRYPT_OID_INFO crypt_OID_INFO = CAPI.CryptFindOIDInfo(keyType, safeLocalAllocHandle, oidGroup);
				if (keyType == 1U)
				{
					text = crypt_OID_INFO.pwszName;
				}
				else
				{
					text = crypt_OID_INFO.pszOID;
				}
			}
			finally
			{
				safeLocalAllocHandle.Dispose();
			}
			return text;
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000C2A3C File Offset: 0x000C0C3C
		internal static string FindOidInfoWithFallback(uint key, string value, OidGroup group)
		{
			string text = X509Utils.FindOidInfo(key, value, group);
			if (text == null && group != OidGroup.All)
			{
				text = X509Utils.FindOidInfo(key, value, OidGroup.All);
			}
			return text;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000C2A64 File Offset: 0x000C0C64
		internal static void ValidateOidValue(string keyValue)
		{
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			int length = keyValue.Length;
			if (length >= 2)
			{
				char c = keyValue[0];
				if ((c == '0' || c == '1' || c == '2') && keyValue[1] == '.' && keyValue[length - 1] != '.')
				{
					bool flag = false;
					for (int i = 1; i < length; i++)
					{
						if (!char.IsDigit(keyValue[i]))
						{
							if (keyValue[i] != '.' || keyValue[i + 1] == '.')
							{
								goto IL_82;
							}
							flag = true;
						}
					}
					if (flag)
					{
						return;
					}
				}
			}
			IL_82:
			throw new ArgumentException(SR.GetString("Argument_InvalidOidValue"));
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000C2B04 File Offset: 0x000C0D04
		internal static SafeLocalAllocHandle CopyOidsToUnmanagedMemory(OidCollection oids)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (oids == null || oids.Count == 0)
			{
				return safeLocalAllocHandle;
			}
			List<string> list = new List<string>();
			foreach (Oid oid in oids)
			{
				list.Add(oid.Value);
			}
			IntPtr zero = IntPtr.Zero;
			int num;
			int num2;
			checked
			{
				num = list.Count * Marshal.SizeOf(typeof(IntPtr));
				num2 = 0;
				foreach (string text in list)
				{
					num2 += text.Length + 1;
				}
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)(checked((uint)num + (uint)num2)))));
			checked
			{
				zero = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + unchecked((long)num));
			}
			for (int i = 0; i < list.Count; i++)
			{
				Marshal.WriteIntPtr(new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(IntPtr)))), zero);
				byte[] bytes = Encoding.ASCII.GetBytes(list[i]);
				Marshal.Copy(bytes, 0, zero, bytes.Length);
				zero = new IntPtr((long)zero + (long)list[i].Length + 1L);
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000C2C68 File Offset: 0x000C0E68
		internal static X509Certificate2Collection GetCertificates(SafeCertStoreHandle safeCertStoreHandle)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			IntPtr intPtr = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, IntPtr.Zero);
			while (intPtr != IntPtr.Zero)
			{
				X509Certificate2 x509Certificate = new X509Certificate2(intPtr);
				x509Certificate2Collection.Add(x509Certificate);
				intPtr = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, intPtr);
			}
			return x509Certificate2Collection;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000C2CB0 File Offset: 0x000C0EB0
		internal unsafe static int VerifyCertificate(SafeCertContextHandle pCertContext, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, X509Certificate2Collection extraStore, IntPtr pszPolicy, IntPtr pdwErrorStatus)
		{
			if (pCertContext == null || pCertContext.IsInvalid)
			{
				throw new ArgumentException("pCertContext");
			}
			CAPIBase.CERT_CHAIN_POLICY_PARA cert_CHAIN_POLICY_PARA = new CAPIBase.CERT_CHAIN_POLICY_PARA(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_PARA)));
			CAPIBase.CERT_CHAIN_POLICY_STATUS cert_CHAIN_POLICY_STATUS = new CAPIBase.CERT_CHAIN_POLICY_STATUS(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_STATUS)));
			SafeX509ChainHandle invalidHandle = SafeX509ChainHandle.InvalidHandle;
			int num = X509Chain.BuildChain(new IntPtr(0L), pCertContext, extraStore, applicationPolicy, certificatePolicy, revocationMode, revocationFlag, verificationTime, timeout, ref invalidHandle);
			if (num != 0)
			{
				return num;
			}
			if (!CAPISafe.CertVerifyCertificateChainPolicy(pszPolicy, invalidHandle, ref cert_CHAIN_POLICY_PARA, ref cert_CHAIN_POLICY_STATUS))
			{
				return Marshal.GetHRForLastWin32Error();
			}
			if (pdwErrorStatus != IntPtr.Zero)
			{
				*(int*)(void*)pdwErrorStatus = (int)cert_CHAIN_POLICY_STATUS.dwError;
			}
			if (cert_CHAIN_POLICY_STATUS.dwError != 0U)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000C2D60 File Offset: 0x000C0F60
		internal static string GetSystemErrorString(int hr)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			uint num = CAPISafe.FormatMessage(4608U, IntPtr.Zero, (uint)hr, 0U, stringBuilder, (uint)stringBuilder.Capacity, IntPtr.Zero);
			if (num != 0U)
			{
				return stringBuilder.ToString();
			}
			return SR.GetString("Unknown_Error");
		}

		// Token: 0x04002644 RID: 9796
		private static readonly char[] hexValues = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
