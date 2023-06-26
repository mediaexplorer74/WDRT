using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200023C RID: 572
	internal static class CapiNative
	{
		// Token: 0x06002093 RID: 8339 RVA: 0x000725A0 File Offset: 0x000707A0
		[SecurityCritical]
		internal static SafeCspHandle AcquireCsp(string keyContainer, string providerName, CapiNative.ProviderType providerType, CapiNative.CryptAcquireContextFlags flags)
		{
			if ((flags & CapiNative.CryptAcquireContextFlags.VerifyContext) == CapiNative.CryptAcquireContextFlags.VerifyContext && (flags & CapiNative.CryptAcquireContextFlags.MachineKeyset) == CapiNative.CryptAcquireContextFlags.MachineKeyset)
			{
				flags &= ~CapiNative.CryptAcquireContextFlags.MachineKeyset;
			}
			SafeCspHandle safeCspHandle = null;
			if (!CapiNative.UnsafeNativeMethods.CryptAcquireContext(out safeCspHandle, keyContainer, providerName, providerType, flags))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return safeCspHandle;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000725E4 File Offset: 0x000707E4
		[SecurityCritical]
		internal static SafeCspHashHandle CreateHashAlgorithm(SafeCspHandle cspHandle, CapiNative.AlgorithmID algorithm)
		{
			SafeCspHashHandle safeCspHashHandle = null;
			if (!CapiNative.UnsafeNativeMethods.CryptCreateHash(cspHandle, algorithm, IntPtr.Zero, 0, out safeCspHashHandle))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return safeCspHashHandle;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00072610 File Offset: 0x00070810
		[SecurityCritical]
		internal static void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer)
		{
			if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, buffer.Length, buffer))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0007262C File Offset: 0x0007082C
		[SecurityCritical]
		internal unsafe static void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer, int offset, int count)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, count, ptr2))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0007265C File Offset: 0x0007085C
		[SecurityCritical]
		internal static int GetHashPropertyInt32(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
		{
			byte[] hashProperty = CapiNative.GetHashProperty(hashHandle, property);
			if (hashProperty.Length != 4)
			{
				return 0;
			}
			return BitConverter.ToInt32(hashProperty, 0);
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00072680 File Offset: 0x00070880
		[SecurityCritical]
		internal static byte[] GetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
		{
			int num = 0;
			byte[] array = null;
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, array, ref num, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, array, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000726D4 File Offset: 0x000708D4
		[SecurityCritical]
		internal static int GetKeyPropertyInt32(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
		{
			byte[] keyProperty = CapiNative.GetKeyProperty(keyHandle, property);
			if (keyProperty.Length != 4)
			{
				return 0;
			}
			return BitConverter.ToInt32(keyProperty, 0);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000726F8 File Offset: 0x000708F8
		[SecurityCritical]
		internal static byte[] GetKeyProperty(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
		{
			int num = 0;
			byte[] array = null;
			if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, array, ref num, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, array, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0007274B File Offset: 0x0007094B
		[SecurityCritical]
		internal static void SetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property, byte[] value)
		{
			if (!CapiNative.UnsafeNativeMethods.CryptSetHashParam(hashHandle, property, value, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00072764 File Offset: 0x00070964
		[SecurityCritical]
		internal static bool VerifySignature(SafeCspHandle cspHandle, SafeCspKeyHandle keyHandle, CapiNative.AlgorithmID signatureAlgorithm, CapiNative.AlgorithmID hashAlgorithm, byte[] hashValue, byte[] signature)
		{
			byte[] array = new byte[signature.Length];
			Array.Copy(signature, array, array.Length);
			Array.Reverse(array);
			bool flag;
			using (SafeCspHashHandle safeCspHashHandle = CapiNative.CreateHashAlgorithm(cspHandle, hashAlgorithm))
			{
				if (hashValue.Length != CapiNative.GetHashPropertyInt32(safeCspHashHandle, CapiNative.HashProperty.HashSize))
				{
					throw new CryptographicException(-2146893822);
				}
				CapiNative.SetHashProperty(safeCspHashHandle, CapiNative.HashProperty.HashValue, hashValue);
				if (CapiNative.UnsafeNativeMethods.CryptVerifySignature(safeCspHashHandle, array, array.Length, keyHandle, null, 0))
				{
					flag = true;
				}
				else
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != -2146893818)
					{
						throw new CryptographicException(lastWin32Error);
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x02000B32 RID: 2866
		internal enum AlgorithmClass
		{
			// Token: 0x04003366 RID: 13158
			Any,
			// Token: 0x04003367 RID: 13159
			Signature = 8192,
			// Token: 0x04003368 RID: 13160
			Hash = 32768,
			// Token: 0x04003369 RID: 13161
			KeyExchange = 40960
		}

		// Token: 0x02000B33 RID: 2867
		internal enum AlgorithmType
		{
			// Token: 0x0400336B RID: 13163
			Any,
			// Token: 0x0400336C RID: 13164
			Rsa = 1024
		}

		// Token: 0x02000B34 RID: 2868
		internal enum AlgorithmSubId
		{
			// Token: 0x0400336E RID: 13166
			Any,
			// Token: 0x0400336F RID: 13167
			RsaAny = 0,
			// Token: 0x04003370 RID: 13168
			Sha1 = 4,
			// Token: 0x04003371 RID: 13169
			Sha256 = 12,
			// Token: 0x04003372 RID: 13170
			Sha384,
			// Token: 0x04003373 RID: 13171
			Sha512
		}

		// Token: 0x02000B35 RID: 2869
		internal enum AlgorithmID
		{
			// Token: 0x04003375 RID: 13173
			None,
			// Token: 0x04003376 RID: 13174
			RsaSign = 9216,
			// Token: 0x04003377 RID: 13175
			RsaKeyExchange = 41984,
			// Token: 0x04003378 RID: 13176
			Sha1 = 32772,
			// Token: 0x04003379 RID: 13177
			Sha256 = 32780,
			// Token: 0x0400337A RID: 13178
			Sha384,
			// Token: 0x0400337B RID: 13179
			Sha512
		}

		// Token: 0x02000B36 RID: 2870
		[Flags]
		internal enum CryptAcquireContextFlags
		{
			// Token: 0x0400337D RID: 13181
			None = 0,
			// Token: 0x0400337E RID: 13182
			NewKeyset = 8,
			// Token: 0x0400337F RID: 13183
			DeleteKeyset = 16,
			// Token: 0x04003380 RID: 13184
			MachineKeyset = 32,
			// Token: 0x04003381 RID: 13185
			Silent = 64,
			// Token: 0x04003382 RID: 13186
			VerifyContext = -268435456
		}

		// Token: 0x02000B37 RID: 2871
		internal enum ErrorCode
		{
			// Token: 0x04003384 RID: 13188
			Ok,
			// Token: 0x04003385 RID: 13189
			MoreData = 234,
			// Token: 0x04003386 RID: 13190
			BadHash = -2146893822,
			// Token: 0x04003387 RID: 13191
			BadData = -2146893819,
			// Token: 0x04003388 RID: 13192
			BadSignature,
			// Token: 0x04003389 RID: 13193
			NoKey = -2146893811
		}

		// Token: 0x02000B38 RID: 2872
		internal enum HashProperty
		{
			// Token: 0x0400338B RID: 13195
			None,
			// Token: 0x0400338C RID: 13196
			HashValue = 2,
			// Token: 0x0400338D RID: 13197
			HashSize = 4
		}

		// Token: 0x02000B39 RID: 2873
		[Flags]
		internal enum KeyGenerationFlags
		{
			// Token: 0x0400338F RID: 13199
			None = 0,
			// Token: 0x04003390 RID: 13200
			Exportable = 1,
			// Token: 0x04003391 RID: 13201
			UserProtected = 2,
			// Token: 0x04003392 RID: 13202
			Archivable = 16384
		}

		// Token: 0x02000B3A RID: 2874
		internal enum KeyProperty
		{
			// Token: 0x04003394 RID: 13204
			None,
			// Token: 0x04003395 RID: 13205
			AlgorithmID = 7,
			// Token: 0x04003396 RID: 13206
			KeyLength = 9
		}

		// Token: 0x02000B3B RID: 2875
		internal enum KeySpec
		{
			// Token: 0x04003398 RID: 13208
			KeyExchange = 1,
			// Token: 0x04003399 RID: 13209
			Signature
		}

		// Token: 0x02000B3C RID: 2876
		internal static class ProviderNames
		{
			// Token: 0x0400339A RID: 13210
			internal const string MicrosoftEnhanced = "Microsoft Enhanced Cryptographic Provider v1.0";
		}

		// Token: 0x02000B3D RID: 2877
		internal enum ProviderType
		{
			// Token: 0x0400339C RID: 13212
			RsaFull = 1
		}

		// Token: 0x02000B3E RID: 2878
		[SecurityCritical]
		internal static class UnsafeNativeMethods
		{
			// Token: 0x06006B8F RID: 27535
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptAcquireContext(out SafeCspHandle phProv, string pszContainer, string pszProvider, CapiNative.ProviderType dwProvType, CapiNative.CryptAcquireContextFlags dwFlags);

			// Token: 0x06006B90 RID: 27536
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptCreateHash(SafeCspHandle hProv, CapiNative.AlgorithmID Algid, IntPtr hKey, int dwFlags, out SafeCspHashHandle phHash);

			// Token: 0x06006B91 RID: 27537
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGenKey(SafeCspHandle hProv, int Algid, uint dwFlags, out SafeCspKeyHandle phKey);

			// Token: 0x06006B92 RID: 27538
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbBuffer);

			// Token: 0x06006B93 RID: 27539
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal unsafe static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, byte* pbBuffer);

			// Token: 0x06006B94 RID: 27540
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06006B95 RID: 27541
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGetKeyParam(SafeCspKeyHandle hKey, CapiNative.KeyProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06006B96 RID: 27542
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptImportKey(SafeCspHandle hProv, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbData, int pdwDataLen, IntPtr hPubKey, CapiNative.KeyGenerationFlags dwFlags, out SafeCspKeyHandle phKey);

			// Token: 0x06006B97 RID: 27543
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptSetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbData, int dwFlags);

			// Token: 0x06006B98 RID: 27544
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptVerifySignature(SafeCspHashHandle hHash, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSignature, int dwSigLen, SafeCspKeyHandle hPubKey, string sDescription, int dwFlags);
		}
	}
}
