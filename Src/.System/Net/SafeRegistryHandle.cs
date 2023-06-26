using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000206 RID: 518
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x00065F78 File Offset: 0x00064178
		private SafeRegistryHandle()
			: base(true)
		{
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00065F81 File Offset: 0x00064181
		internal static uint RegOpenKeyEx(IntPtr key, string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenKeyEx(key, subKey, ulOptions, samDesired, out resultSubKey);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00065F8E File Offset: 0x0006418E
		internal uint RegOpenKeyEx(string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenKeyEx(this, subKey, ulOptions, samDesired, out resultSubKey);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00065F9B File Offset: 0x0006419B
		internal uint RegCloseKey()
		{
			base.Close();
			return this.resClose;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00065FAC File Offset: 0x000641AC
		internal uint QueryValue(string name, out object data)
		{
			data = null;
			byte[] array = null;
			uint num = 0U;
			uint num3;
			uint num2;
			for (;;)
			{
				num2 = UnsafeNclNativeMethods.RegistryHelper.RegQueryValueEx(this, name, IntPtr.Zero, out num3, array, ref num);
				if (num2 != 234U && (array != null || num2 != 0U))
				{
					break;
				}
				array = new byte[num];
			}
			if (num2 != 0U)
			{
				return num2;
			}
			if (num3 == 3U)
			{
				if ((ulong)num != (ulong)((long)array.Length))
				{
					byte[] array2 = array;
					array = new byte[num];
					Buffer.BlockCopy(array2, 0, array, 0, (int)num);
				}
				data = array;
				return 0U;
			}
			return 50U;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00066016 File Offset: 0x00064216
		internal uint RegNotifyChangeKeyValue(bool watchSubTree, uint notifyFilter, SafeWaitHandle regEvent, bool async)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegNotifyChangeKeyValue(this, watchSubTree, notifyFilter, regEvent, async);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00066023 File Offset: 0x00064223
		internal static uint RegOpenCurrentUser(uint samDesired, out SafeRegistryHandle resultKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenCurrentUser(samDesired, out resultKey);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0006602C File Offset: 0x0006422C
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				this.resClose = UnsafeNclNativeMethods.RegistryHelper.RegCloseKey(this.handle);
			}
			base.SetHandleAsInvalid();
			return true;
		}

		// Token: 0x0400154A RID: 5450
		private uint resClose;
	}
}
