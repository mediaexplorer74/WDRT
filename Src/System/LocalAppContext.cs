using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000063 RID: 99
	internal static class LocalAppContext
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001E732 File Offset: 0x0001C932
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0001E739 File Offset: 0x0001C939
		private static bool DisableCaching { get; set; }

		// Token: 0x06000449 RID: 1097 RVA: 0x0001E741 File Offset: 0x0001C941
		static LocalAppContext()
		{
			System.AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001E778 File Offset: 0x0001C978
		public static bool IsSwitchEnabled(string switchName)
		{
			bool flag;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out flag))
			{
				return flag;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001E7A4 File Offset: 0x0001C9A4
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

		// Token: 0x0600044C RID: 1100 RVA: 0x0001E7F4 File Offset: 0x0001C9F4
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

		// Token: 0x0600044D RID: 1101 RVA: 0x0001E881 File Offset: 0x0001CA81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001E898 File Offset: 0x0001CA98
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

		// Token: 0x0600044F RID: 1103 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x0400052B RID: 1323
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x0400052C RID: 1324
		private static bool s_canForwardCalls = LocalAppContext.SetupDelegate();

		// Token: 0x0400052D RID: 1325
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x0400052E RID: 1326
		private static readonly object s_syncLock = new object();

		// Token: 0x020006E4 RID: 1764
		// (Invoke) Token: 0x06004021 RID: 16417
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
