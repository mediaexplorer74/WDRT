using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000201 RID: 513
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseSocketAndEvent : SafeCloseSocket
	{
		// Token: 0x06001345 RID: 4933 RVA: 0x00065C08 File Offset: 0x00063E08
		internal SafeCloseSocketAndEvent()
		{
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00065C10 File Offset: 0x00063E10
		protected override bool ReleaseHandle()
		{
			bool flag = base.ReleaseHandle();
			this.DeleteEvent();
			return flag;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00065C2C File Offset: 0x00063E2C
		internal static SafeCloseSocketAndEvent CreateWSASocketWithEvent(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, bool autoReset, bool signaled)
		{
			SafeCloseSocketAndEvent safeCloseSocketAndEvent = new SafeCloseSocketAndEvent();
			SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(addressFamily, socketType, protocolType), safeCloseSocketAndEvent);
			if (safeCloseSocketAndEvent.IsInvalid)
			{
				throw new SocketException();
			}
			safeCloseSocketAndEvent.waitHandle = new AutoResetEvent(false);
			SafeCloseSocketAndEvent.CompleteInitialization(safeCloseSocketAndEvent);
			return safeCloseSocketAndEvent;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00065C70 File Offset: 0x00063E70
		internal static void CompleteInitialization(SafeCloseSocketAndEvent socketAndEventHandle)
		{
			SafeWaitHandle safeWaitHandle = socketAndEventHandle.waitHandle.SafeWaitHandle;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeWaitHandle.DangerousAddRef(ref flag);
			}
			catch
			{
				if (flag)
				{
					safeWaitHandle.DangerousRelease();
					socketAndEventHandle.waitHandle = null;
					flag = false;
				}
			}
			finally
			{
				if (flag)
				{
					safeWaitHandle.Dispose();
				}
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00065CD8 File Offset: 0x00063ED8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void DeleteEvent()
		{
			try
			{
				if (this.waitHandle != null)
				{
					this.waitHandle.SafeWaitHandle.DangerousRelease();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00065D14 File Offset: 0x00063F14
		internal WaitHandle GetEventHandle()
		{
			return this.waitHandle;
		}

		// Token: 0x04001545 RID: 5445
		private AutoResetEvent waitHandle;
	}
}
