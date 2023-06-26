using System;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000205 RID: 517
	internal sealed class SafeUnlockUrlCacheEntryFile : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x00065E94 File Offset: 0x00064094
		private SafeUnlockUrlCacheEntryFile(string keyString)
			: base(true)
		{
			this.m_KeyString = keyString;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00065EA4 File Offset: 0x000640A4
		protected unsafe override bool ReleaseHandle()
		{
			fixed (string keyString = this.m_KeyString)
			{
				char* ptr = keyString;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				UnsafeNclNativeMethods.SafeNetHandles.UnlockUrlCacheEntryFileW(ptr, 0);
			}
			base.SetHandle(IntPtr.Zero);
			this.m_KeyString = null;
			return true;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00065EE4 File Offset: 0x000640E4
		internal unsafe static _WinInetCache.Status GetAndLockFile(string key, byte* entryPtr, ref int entryBufSize, out SafeUnlockUrlCacheEntryFile handle)
		{
			if (ValidationHelper.IsBlankString(key))
			{
				throw new ArgumentNullException("key");
			}
			handle = new SafeUnlockUrlCacheEntryFile(key);
			char* ptr = key;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return SafeUnlockUrlCacheEntryFile.MustRunGetAndLockFile(ptr, entryPtr, ref entryBufSize, handle);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00065F28 File Offset: 0x00064128
		private unsafe static _WinInetCache.Status MustRunGetAndLockFile(char* key, byte* entryPtr, ref int entryBufSize, SafeUnlockUrlCacheEntryFile handle)
		{
			_WinInetCache.Status status = _WinInetCache.Status.Success;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (!UnsafeNclNativeMethods.SafeNetHandles.RetrieveUrlCacheEntryFileW(key, entryPtr, ref entryBufSize, 0))
				{
					status = (_WinInetCache.Status)Marshal.GetLastWin32Error();
					handle.SetHandleAsInvalid();
				}
				else
				{
					handle.SetHandle((IntPtr)1);
				}
			}
			return status;
		}

		// Token: 0x04001549 RID: 5449
		private string m_KeyString;
	}
}
