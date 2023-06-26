using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security.Util
{
	// Token: 0x02000378 RID: 888
	internal static class Config
	{
		// Token: 0x06002C33 RID: 11315 RVA: 0x000A5BCC File Offset: 0x000A3DCC
		[SecurityCritical]
		private static void GetFileLocales()
		{
			if (Config.m_machineConfig == null)
			{
				string text = null;
				Config.GetMachineDirectory(JitHelpers.GetStringHandleOnStack(ref text));
				Config.m_machineConfig = text;
			}
			if (Config.m_userConfig == null)
			{
				string text2 = null;
				Config.GetUserDirectory(JitHelpers.GetStringHandleOnStack(ref text2));
				Config.m_userConfig = text2;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000A5C17 File Offset: 0x000A3E17
		internal static string MachineDirectory
		{
			[SecurityCritical]
			get
			{
				Config.GetFileLocales();
				return Config.m_machineConfig;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000A5C25 File Offset: 0x000A3E25
		internal static string UserDirectory
		{
			[SecurityCritical]
			get
			{
				Config.GetFileLocales();
				return Config.m_userConfig;
			}
		}

		// Token: 0x06002C36 RID: 11318
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int SaveDataByte(string path, [In] byte[] data, int length);

		// Token: 0x06002C37 RID: 11319
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool RecoverData(ConfigId id);

		// Token: 0x06002C38 RID: 11320
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetQuickCache(ConfigId id, QuickCacheEntryType quickCacheFlags);

		// Token: 0x06002C39 RID: 11321
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool GetCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, ObjectHandleOnStack retData);

		// Token: 0x06002C3A RID: 11322 RVA: 0x000A5C34 File Offset: 0x000A3E34
		[SecurityCritical]
		internal static bool GetCacheEntry(ConfigId id, int numKey, byte[] key, out byte[] data)
		{
			byte[] array = null;
			bool cacheEntry = Config.GetCacheEntry(id, numKey, key, key.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			data = array;
			return cacheEntry;
		}

		// Token: 0x06002C3B RID: 11323
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, byte[] data, int dataLength);

		// Token: 0x06002C3C RID: 11324 RVA: 0x000A5C5A File Offset: 0x000A3E5A
		[SecurityCritical]
		internal static void AddCacheEntry(ConfigId id, int numKey, byte[] key, byte[] data)
		{
			Config.AddCacheEntry(id, numKey, key, key.Length, data, data.Length);
		}

		// Token: 0x06002C3D RID: 11325
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void ResetCacheData(ConfigId id);

		// Token: 0x06002C3E RID: 11326
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMachineDirectory(StringHandleOnStack retDirectory);

		// Token: 0x06002C3F RID: 11327
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetUserDirectory(StringHandleOnStack retDirectory);

		// Token: 0x06002C40 RID: 11328
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool WriteToEventLog(string message);

		// Token: 0x040011CB RID: 4555
		private static volatile string m_machineConfig;

		// Token: 0x040011CC RID: 4556
		private static volatile string m_userConfig;
	}
}
