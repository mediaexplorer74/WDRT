using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000200 RID: 512
	[SuppressUnmanagedCodeSecurity]
	internal class SafeCloseSocket : SafeHandleMinusOneIsInvalid
	{
		// Token: 0x0600133B RID: 4923 RVA: 0x00065A82 File Offset: 0x00063C82
		protected SafeCloseSocket()
			: base(true)
		{
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00065A8B File Offset: 0x00063C8B
		public override bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return base.IsClosed || base.IsInvalid;
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00065A9D File Offset: 0x00063C9D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void SetInnerSocket(SafeCloseSocket.InnerSafeCloseSocket socket)
		{
			this.m_InnerSocket = socket;
			base.SetHandle(socket.DangerousGetHandle());
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00065AB4 File Offset: 0x00063CB4
		private static SafeCloseSocket CreateSocket(SafeCloseSocket.InnerSafeCloseSocket socket)
		{
			SafeCloseSocket safeCloseSocket = new SafeCloseSocket();
			SafeCloseSocket.CreateSocket(socket, safeCloseSocket);
			return safeCloseSocket;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00065AD0 File Offset: 0x00063CD0
		protected static void CreateSocket(SafeCloseSocket.InnerSafeCloseSocket socket, SafeCloseSocket target)
		{
			if (socket != null && socket.IsInvalid)
			{
				target.SetHandleAsInvalid();
				return;
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				socket.DangerousAddRef(ref flag);
			}
			catch
			{
				if (flag)
				{
					socket.DangerousRelease();
					flag = false;
				}
			}
			finally
			{
				if (flag)
				{
					target.SetInnerSocket(socket);
					socket.Close();
				}
				else
				{
					target.SetHandleAsInvalid();
				}
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00065B44 File Offset: 0x00063D44
		internal unsafe static SafeCloseSocket CreateWSASocket(byte* pinnedBuffer)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(pinnedBuffer));
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00065B51 File Offset: 0x00063D51
		internal static SafeCloseSocket CreateWSASocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(addressFamily, socketType, protocolType));
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00065B60 File Offset: 0x00063D60
		internal static SafeCloseSocket Accept(SafeCloseSocket socketHandle, byte[] socketAddress, ref int socketAddressSize)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.Accept(socketHandle, socketAddress, ref socketAddressSize));
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00065B70 File Offset: 0x00063D70
		protected override bool ReleaseHandle()
		{
			this.m_Released = true;
			SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = ((this.m_InnerSocket == null) ? null : Interlocked.Exchange<SafeCloseSocket.InnerSafeCloseSocket>(ref this.m_InnerSocket, null));
			if (innerSafeCloseSocket != null)
			{
				innerSafeCloseSocket.DangerousRelease();
			}
			return true;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00065BA8 File Offset: 0x00063DA8
		internal void CloseAsIs()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = ((this.m_InnerSocket == null) ? null : Interlocked.Exchange<SafeCloseSocket.InnerSafeCloseSocket>(ref this.m_InnerSocket, null));
				base.Close();
				if (innerSafeCloseSocket != null)
				{
					while (!this.m_Released)
					{
						Thread.SpinWait(1);
					}
					innerSafeCloseSocket.BlockingRelease();
				}
			}
		}

		// Token: 0x04001543 RID: 5443
		private SafeCloseSocket.InnerSafeCloseSocket m_InnerSocket;

		// Token: 0x04001544 RID: 5444
		private volatile bool m_Released;

		// Token: 0x02000756 RID: 1878
		internal class InnerSafeCloseSocket : SafeHandleMinusOneIsInvalid
		{
			// Token: 0x060041E3 RID: 16867 RVA: 0x0011189E File Offset: 0x0010FA9E
			protected InnerSafeCloseSocket()
				: base(true)
			{
			}

			// Token: 0x17000F10 RID: 3856
			// (get) Token: 0x060041E4 RID: 16868 RVA: 0x001118A7 File Offset: 0x0010FAA7
			public override bool IsInvalid
			{
				[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
				get
				{
					return base.IsClosed || base.IsInvalid;
				}
			}

			// Token: 0x060041E5 RID: 16869 RVA: 0x001118BC File Offset: 0x0010FABC
			protected override bool ReleaseHandle()
			{
				SocketError socketError;
				if (this.m_Blockable)
				{
					socketError = UnsafeNclNativeMethods.SafeNetHandles.closesocket(this.handle);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
					if (socketError != SocketError.WouldBlock)
					{
						return socketError == SocketError.Success;
					}
					int num = 0;
					socketError = UnsafeNclNativeMethods.SafeNetHandles.ioctlsocket(this.handle, -2147195266, ref num);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
					if (socketError == SocketError.InvalidArgument)
					{
						socketError = UnsafeNclNativeMethods.SafeNetHandles.WSAEventSelect(this.handle, IntPtr.Zero, AsyncEventBits.FdNone);
						socketError = UnsafeNclNativeMethods.SafeNetHandles.ioctlsocket(this.handle, -2147195266, ref num);
					}
					if (socketError == SocketError.Success)
					{
						socketError = UnsafeNclNativeMethods.SafeNetHandles.closesocket(this.handle);
						if (socketError == SocketError.SocketError)
						{
							socketError = (SocketError)Marshal.GetLastWin32Error();
						}
						if (socketError != SocketError.WouldBlock)
						{
							return socketError == SocketError.Success;
						}
					}
				}
				Linger linger;
				linger.OnOff = 1;
				linger.Time = 0;
				socketError = UnsafeNclNativeMethods.SafeNetHandles.setsockopt(this.handle, SocketOptionLevel.Socket, SocketOptionName.Linger, ref linger, 4);
				if (socketError == SocketError.SocketError)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
				if (socketError != SocketError.Success && socketError != SocketError.InvalidArgument && socketError != SocketError.ProtocolOption)
				{
					return false;
				}
				socketError = UnsafeNclNativeMethods.SafeNetHandles.closesocket(this.handle);
				return socketError == SocketError.Success;
			}

			// Token: 0x060041E6 RID: 16870 RVA: 0x001119CB File Offset: 0x0010FBCB
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void BlockingRelease()
			{
				this.m_Blockable = true;
				base.DangerousRelease();
			}

			// Token: 0x060041E7 RID: 16871 RVA: 0x001119DC File Offset: 0x0010FBDC
			internal unsafe static SafeCloseSocket.InnerSafeCloseSocket CreateWSASocket(byte* pinnedBuffer)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.OSSOCK.WSASocket(AddressFamily.Unknown, SocketType.Unknown, ProtocolType.Unknown, pinnedBuffer, 0U, SocketConstructorFlags.WSA_FLAG_OVERLAPPED);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x060041E8 RID: 16872 RVA: 0x00111A04 File Offset: 0x0010FC04
			internal static SafeCloseSocket.InnerSafeCloseSocket CreateWSASocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.OSSOCK.WSASocket(addressFamily, socketType, protocolType, IntPtr.Zero, 0U, SocketConstructorFlags.WSA_FLAG_OVERLAPPED);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x060041E9 RID: 16873 RVA: 0x00111A30 File Offset: 0x0010FC30
			internal static SafeCloseSocket.InnerSafeCloseSocket Accept(SafeCloseSocket socketHandle, byte[] socketAddress, ref int socketAddressSize)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.SafeNetHandles.accept(socketHandle.DangerousGetHandle(), socketAddress, ref socketAddressSize);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x040031FE RID: 12798
			private static readonly byte[] tempBuffer = new byte[1];

			// Token: 0x040031FF RID: 12799
			private bool m_Blockable;
		}
	}
}
