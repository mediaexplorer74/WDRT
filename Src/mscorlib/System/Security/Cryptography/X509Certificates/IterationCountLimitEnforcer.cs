using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002AF RID: 687
	internal static class IterationCountLimitEnforcer
	{
		// Token: 0x0600248A RID: 9354 RVA: 0x00084C08 File Offset: 0x00082E08
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void EnforceIterationCountLimit(byte[] pkcs12, bool readingFromFile, bool passwordProvided)
		{
			IterationCountLimitEnforcer.Impl.EnforceIterationCountLimit(pkcs12, readingFromFile, passwordProvided);
		}

		// Token: 0x02000B45 RID: 2885
		private static class Impl
		{
			// Token: 0x06006BB3 RID: 27571 RVA: 0x00175F68 File Offset: 0x00174168
			[SecuritySafeCritical]
			[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
			private static long ReadSecuritySwitch()
			{
				long num = 0L;
				string environmentVariable = Environment.GetEnvironmentVariable("COMPlus_Pkcs12UnspecifiedPasswordIterationLimit");
				if (environmentVariable != null && long.TryParse(environmentVariable, out num))
				{
					return num;
				}
				if (IterationCountLimitEnforcer.Impl.ReadSettingsFromRegistry(Registry.CurrentUser, ref num))
				{
					return num;
				}
				if (IterationCountLimitEnforcer.Impl.ReadSettingsFromRegistry(Registry.LocalMachine, ref num))
				{
					return num;
				}
				return 600000L;
			}

			// Token: 0x06006BB4 RID: 27572 RVA: 0x00175FB8 File Offset: 0x001741B8
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
			private static bool ReadSettingsFromRegistry(RegistryKey regKey, ref long value)
			{
				try
				{
					using (RegistryKey registryKey = regKey.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework", false))
					{
						if (registryKey != null)
						{
							object value2 = registryKey.GetValue("Pkcs12UnspecifiedPasswordIterationLimit");
							if (value2 != null)
							{
								value = Convert.ToInt64(value2, CultureInfo.InvariantCulture);
								return true;
							}
						}
					}
				}
				catch
				{
				}
				return false;
			}

			// Token: 0x06006BB5 RID: 27573 RVA: 0x00176028 File Offset: 0x00174228
			internal static void EnforceIterationCountLimit(byte[] pkcs12, bool readingFromFile, bool passwordProvided)
			{
				if (readingFromFile || passwordProvided)
				{
					return;
				}
				long num = IterationCountLimitEnforcer.Impl.s_pkcs12UnspecifiedPasswordIterationLimit;
				if (num == -1L)
				{
					return;
				}
				if (num < 0L)
				{
					num = 600000L;
				}
				checked
				{
					try
					{
						try
						{
							KdfWorkLimiter.SetIterationLimit((ulong)num);
							ulong iterationCount = IterationCountLimitEnforcer.Impl.GetIterationCount(pkcs12);
							if (iterationCount > (ulong)num || KdfWorkLimiter.WasWorkLimitExceeded())
							{
								throw new CryptographicException();
							}
						}
						finally
						{
							KdfWorkLimiter.ResetIterationLimit();
						}
					}
					catch (Exception ex)
					{
						throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"), ex);
					}
				}
			}

			// Token: 0x06006BB6 RID: 27574 RVA: 0x001760AC File Offset: 0x001742AC
			private static ulong GetIterationCount(byte[] pkcs12)
			{
				ReadOnlyMemory<byte> readOnlyMemory = new ReadOnlyMemory<byte>(pkcs12);
				AsnValueReader asnValueReader = new AsnValueReader(pkcs12, AsnEncodingRules.BER);
				PfxAsn pfxAsn;
				PfxAsn.Decode(ref asnValueReader, readOnlyMemory, out pfxAsn);
				return pfxAsn.CountTotalIterations();
			}

			// Token: 0x040033C8 RID: 13256
			private const long DefaultPkcs12UnspecifiedPasswordIterationLimit = 600000L;

			// Token: 0x040033C9 RID: 13257
			private static long s_pkcs12UnspecifiedPasswordIterationLimit = IterationCountLimitEnforcer.Impl.ReadSecuritySwitch();
		}
	}
}
