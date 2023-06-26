using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000037 RID: 55
	internal static class LocalAppContextSwitches
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00010C0A File Offset: 0x0000EE0A
		public static bool MemberDescriptorEqualsReturnsFalseIfEquivalent
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.MemberDescriptorEqualsReturnsFalseIfEquivalent", ref LocalAppContextSwitches._memberDescriptorEqualsReturnsFalseIfEquivalent);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00010C1B File Offset: 0x0000EE1B
		public static bool DontEnableStrictRFC3986ReservedCharacterSets
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Uri.DontEnableStrictRFC3986ReservedCharacterSets", ref LocalAppContextSwitches._dontEnableStrictRFC3986ReservedCharacterSets);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public static bool DontKeepUnicodeBidiFormattingCharacters
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Uri.DontKeepUnicodeBidiFormattingCharacters", ref LocalAppContextSwitches._dontKeepUnicodeBidiFormattingCharacters);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00010C3D File Offset: 0x0000EE3D
		public static bool DisableTempFileCollectionDirectoryFeature
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.DisableTempFileCollectionDirectoryFeature", ref LocalAppContextSwitches._disableTempFileCollectionDirectoryFeature);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00010C4E File Offset: 0x0000EE4E
		public static bool DisableEventLogRegistryKeysFiltering
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Diagnostics.EventLog.DisableEventLogRegistryKeysFiltering", ref LocalAppContextSwitches._disableEventLogRegistryKeysFiltering);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00010C5F File Offset: 0x0000EE5F
		public static bool DontEnableSchUseStrongCrypto
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Net.DontEnableSchUseStrongCrypto", ref LocalAppContextSwitches._dontEnableSchUseStrongCrypto);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00010C70 File Offset: 0x0000EE70
		public static bool AllocateOverlappedOnDemand
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Net.WebSockets.HttpListenerAsyncEventArgs.AllocateOverlappedOnDemand", ref LocalAppContextSwitches._allocateOverlappedOnDemand);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00010C81 File Offset: 0x0000EE81
		public static bool DontEnableSchSendAuxRecord
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Net.DontEnableSchSendAuxRecord", ref LocalAppContextSwitches._dontEnableSchSendAuxRecord);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00010C92 File Offset: 0x0000EE92
		public static bool DontEnableSystemDefaultTlsVersions
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Net.DontEnableSystemDefaultTlsVersions", ref LocalAppContextSwitches._dontEnableSystemSystemDefaultTlsVersions);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00010CA3 File Offset: 0x0000EEA3
		public static bool DontEnableTlsAlerts
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Net.DontEnableTlsAlerts", ref LocalAppContextSwitches._dontEnableTlsAlerts);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00010CB4 File Offset: 0x0000EEB4
		public static bool DontEnableTls13
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Net.DontEnableTls13", ref LocalAppContextSwitches._dontEnableTls13);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00010CC5 File Offset: 0x0000EEC5
		public static bool DontCheckCertificateEKUs
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Net.DontCheckCertificateEKUs", ref LocalAppContextSwitches._dontCheckCertificateEKUs);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00010CD6 File Offset: 0x0000EED6
		public static bool DontCheckCertificateRevocation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("System.Net.Security.SslStream.AuthenticateAsClient.DontCheckCertificateRevocation", ref LocalAppContextSwitches._dontCheckCertificateRevocation);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00010CE7 File Offset: 0x0000EEE7
		public static bool DoNotCatchSerialStreamThreadExceptions
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IO.Ports.DoNotCatchSerialStreamThreadExceptions", ref LocalAppContextSwitches._doNotCatchSerialStreamThreadExceptions);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00010CF8 File Offset: 0x0000EEF8
		public static bool DoNotValidateX509KeyStorageFlags
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Security.Cryptography.X509Cerificates.X509Certificate2Collection.DoNotValidateX509KeyStorageFlags", ref LocalAppContextSwitches._doNotValidateX509KeyStorageFlags);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00010D09 File Offset: 0x0000EF09
		public static bool DoNotUseNativeZipLibraryForDecompression
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IO.Compression.DoNotUseNativeZipLibraryForDecompression", ref LocalAppContextSwitches._doNotUseNativeZipLibraryForDecompression);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00010D1A File Offset: 0x0000EF1A
		public static bool UseLegacyTimeoutCheck
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Text.RegularExpressions.UseLegacyTimeoutCheck", ref LocalAppContextSwitches._useLegacyTimeoutCheck);
			}
		}

		// Token: 0x04000396 RID: 918
		private static int _memberDescriptorEqualsReturnsFalseIfEquivalent;

		// Token: 0x04000397 RID: 919
		internal const string MemberDescriptorEqualsReturnsFalseIfEquivalentName = "Switch.System.MemberDescriptorEqualsReturnsFalseIfEquivalent";

		// Token: 0x04000398 RID: 920
		private static int _dontEnableStrictRFC3986ReservedCharacterSets;

		// Token: 0x04000399 RID: 921
		internal const string DontEnableStrictRFC3986ReservedCharacterSetsName = "Switch.System.Uri.DontEnableStrictRFC3986ReservedCharacterSets";

		// Token: 0x0400039A RID: 922
		private static int _dontKeepUnicodeBidiFormattingCharacters;

		// Token: 0x0400039B RID: 923
		internal const string DontKeepUnicodeBidiFormattingCharactersName = "Switch.System.Uri.DontKeepUnicodeBidiFormattingCharacters";

		// Token: 0x0400039C RID: 924
		private static int _disableTempFileCollectionDirectoryFeature;

		// Token: 0x0400039D RID: 925
		internal const string DisableTempFileCollectionDirectoryFeatureName = "Switch.System.DisableTempFileCollectionDirectoryFeature";

		// Token: 0x0400039E RID: 926
		private static int _disableEventLogRegistryKeysFiltering;

		// Token: 0x0400039F RID: 927
		private const string DisableEventLogRegistryKeysFilteringName = "Switch.System.Diagnostics.EventLog.DisableEventLogRegistryKeysFiltering";

		// Token: 0x040003A0 RID: 928
		private static int _dontEnableSchUseStrongCrypto;

		// Token: 0x040003A1 RID: 929
		internal const string DontEnableSchUseStrongCryptoName = "Switch.System.Net.DontEnableSchUseStrongCrypto";

		// Token: 0x040003A2 RID: 930
		private static int _allocateOverlappedOnDemand;

		// Token: 0x040003A3 RID: 931
		internal const string AllocateOverlappedOnDemandName = "Switch.System.Net.WebSockets.HttpListenerAsyncEventArgs.AllocateOverlappedOnDemand";

		// Token: 0x040003A4 RID: 932
		private static int _dontEnableSchSendAuxRecord;

		// Token: 0x040003A5 RID: 933
		internal const string DontEnableSchSendAuxRecordName = "Switch.System.Net.DontEnableSchSendAuxRecord";

		// Token: 0x040003A6 RID: 934
		private static int _dontEnableSystemSystemDefaultTlsVersions;

		// Token: 0x040003A7 RID: 935
		internal const string DontEnableSystemDefaultTlsVersionsName = "Switch.System.Net.DontEnableSystemDefaultTlsVersions";

		// Token: 0x040003A8 RID: 936
		private static int _dontEnableTlsAlerts;

		// Token: 0x040003A9 RID: 937
		internal const string DontEnableTlsAlertsName = "Switch.System.Net.DontEnableTlsAlerts";

		// Token: 0x040003AA RID: 938
		private static int _dontEnableTls13;

		// Token: 0x040003AB RID: 939
		internal const string DontEnableTls13Name = "Switch.System.Net.DontEnableTls13";

		// Token: 0x040003AC RID: 940
		private static int _dontCheckCertificateEKUs;

		// Token: 0x040003AD RID: 941
		internal const string DontCheckCertificateEKUsName = "Switch.System.Net.DontCheckCertificateEKUs";

		// Token: 0x040003AE RID: 942
		private static int _dontCheckCertificateRevocation;

		// Token: 0x040003AF RID: 943
		internal const string DontCheckCertificateRevocationName = "System.Net.Security.SslStream.AuthenticateAsClient.DontCheckCertificateRevocation";

		// Token: 0x040003B0 RID: 944
		private static int _doNotCatchSerialStreamThreadExceptions;

		// Token: 0x040003B1 RID: 945
		internal const string DoNotCatchSerialStreamThreadExceptionsName = "Switch.System.IO.Ports.DoNotCatchSerialStreamThreadExceptions";

		// Token: 0x040003B2 RID: 946
		private static int _doNotValidateX509KeyStorageFlags;

		// Token: 0x040003B3 RID: 947
		internal const string DoNotValidateX509KeyStorageFlagsName = "Switch.System.Security.Cryptography.X509Cerificates.X509Certificate2Collection.DoNotValidateX509KeyStorageFlags";

		// Token: 0x040003B4 RID: 948
		private static int _doNotUseNativeZipLibraryForDecompression;

		// Token: 0x040003B5 RID: 949
		internal const string DoNotUseNativeZipLibraryForDecompressionName = "Switch.System.IO.Compression.DoNotUseNativeZipLibraryForDecompression";

		// Token: 0x040003B6 RID: 950
		private static int _useLegacyTimeoutCheck;

		// Token: 0x040003B7 RID: 951
		internal const string UseLegacyTimeoutCheckName = "Switch.System.Text.RegularExpressions.UseLegacyTimeoutCheck";
	}
}
