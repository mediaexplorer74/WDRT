using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	/// <summary>Represents the possible values for a top-level node on a foreign machine.</summary>
	// Token: 0x02000011 RID: 17
	[ComVisible(true)]
	[Serializable]
	public enum RegistryHive
	{
		/// <summary>Represents the HKEY_CLASSES_ROOT base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x04000179 RID: 377
		ClassesRoot = -2147483648,
		/// <summary>Represents the HKEY_CURRENT_USER base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x0400017A RID: 378
		CurrentUser,
		/// <summary>Represents the HKEY_LOCAL_MACHINE base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x0400017B RID: 379
		LocalMachine,
		/// <summary>Represents the HKEY_USERS base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x0400017C RID: 380
		Users,
		/// <summary>Represents the HKEY_PERFORMANCE_DATA base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x0400017D RID: 381
		PerformanceData,
		/// <summary>Represents the HKEY_CURRENT_CONFIG base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x0400017E RID: 382
		CurrentConfig,
		/// <summary>Represents the HKEY_DYN_DATA base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x0400017F RID: 383
		DynData
	}
}
