using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x02000725 RID: 1829
	public static class CompatibilitySwitch
	{
		// Token: 0x06005181 RID: 20865 RVA: 0x00120892 File Offset: 0x0011EA92
		[SecurityCritical]
		public static bool IsEnabled(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, true);
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0012089B File Offset: 0x0011EA9B
		[SecurityCritical]
		public static string GetValue(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, true);
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x001208A4 File Offset: 0x0011EAA4
		[SecurityCritical]
		internal static bool IsEnabledInternal(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, false);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x001208AD File Offset: 0x0011EAAD
		[SecurityCritical]
		internal static string GetValueInternal(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, false);
		}

		// Token: 0x06005185 RID: 20869
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetAppContextOverridesInternalCall();

		// Token: 0x06005186 RID: 20870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnabledInternalCall(string compatibilitySwitchName, bool onlyDB);

		// Token: 0x06005187 RID: 20871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetValueInternalCall(string compatibilitySwitchName, bool onlyDB);
	}
}
