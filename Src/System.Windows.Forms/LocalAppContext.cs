using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000009 RID: 9
	internal static class LocalAppContext
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000025D0 File Offset: 0x000007D0
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000025D7 File Offset: 0x000007D7
		private static bool DisableCaching { get; set; }

		// Token: 0x06000011 RID: 17 RVA: 0x000025DF File Offset: 0x000007DF
		static LocalAppContext()
		{
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002614 File Offset: 0x00000814
		public static bool IsSwitchEnabled(string switchName)
		{
			bool flag;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out flag))
			{
				return flag;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002640 File Offset: 0x00000840
		private static bool IsSwitchEnabledLocal(string switchName)
		{
			Dictionary<string, bool> dictionary = LocalAppContext.s_switchMap;
			bool flag3;
			bool flag2;
			lock (dictionary)
			{
				flag2 = LocalAppContext.s_switchMap.TryGetValue(switchName, out flag3);
			}
			return flag2 && flag3;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002690 File Offset: 0x00000890
		private static bool SetupDelegate()
		{
			Type type = typeof(object).Assembly.GetType("System.AppContext");
			if (type == null)
			{
				return false;
			}
			MethodInfo method = type.GetMethod("TryGetSwitch", BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				typeof(string),
				typeof(bool).MakeByRefType()
			}, null);
			if (method == null)
			{
				return false;
			}
			LocalAppContext.TryGetSwitchFromCentralAppContext = (LocalAppContext.TryGetSwitchDelegate)Delegate.CreateDelegate(typeof(LocalAppContext.TryGetSwitchDelegate), method);
			return true;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000271D File Offset: 0x0000091D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002734 File Offset: 0x00000934
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			if (LocalAppContext.DisableCaching)
			{
				return LocalAppContext.IsSwitchEnabled(switchName);
			}
			bool flag = LocalAppContext.IsSwitchEnabled(switchName);
			switchValue = (flag ? 1 : (-1));
			return flag;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002760 File Offset: 0x00000960
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x04000087 RID: 135
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x04000088 RID: 136
		private static bool s_canForwardCalls = LocalAppContext.SetupDelegate();

		// Token: 0x04000089 RID: 137
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x0400008A RID: 138
		private static readonly object s_syncLock = new object();

		// Token: 0x02000516 RID: 1302
		// (Invoke) Token: 0x0600553D RID: 21821
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
