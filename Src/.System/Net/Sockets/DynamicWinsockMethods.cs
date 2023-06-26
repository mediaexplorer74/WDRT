using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x02000390 RID: 912
	internal sealed class DynamicWinsockMethods
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x000A374C File Offset: 0x000A194C
		public static DynamicWinsockMethods GetMethods(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			List<DynamicWinsockMethods> list = DynamicWinsockMethods.s_MethodTable;
			DynamicWinsockMethods dynamicWinsockMethods2;
			lock (list)
			{
				DynamicWinsockMethods dynamicWinsockMethods;
				for (int i = 0; i < DynamicWinsockMethods.s_MethodTable.Count; i++)
				{
					dynamicWinsockMethods = DynamicWinsockMethods.s_MethodTable[i];
					if (dynamicWinsockMethods.addressFamily == addressFamily && dynamicWinsockMethods.socketType == socketType && dynamicWinsockMethods.protocolType == protocolType)
					{
						return dynamicWinsockMethods;
					}
				}
				dynamicWinsockMethods = new DynamicWinsockMethods(addressFamily, socketType, protocolType);
				DynamicWinsockMethods.s_MethodTable.Add(dynamicWinsockMethods);
				dynamicWinsockMethods2 = dynamicWinsockMethods;
			}
			return dynamicWinsockMethods2;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000A37E0 File Offset: 0x000A19E0
		private DynamicWinsockMethods(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			this.addressFamily = addressFamily;
			this.socketType = socketType;
			this.protocolType = protocolType;
			this.lockObject = new object();
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000A3808 File Offset: 0x000A1A08
		public T GetDelegate<T>(SafeCloseSocket socketHandle) where T : class
		{
			if (typeof(T) == typeof(AcceptExDelegate))
			{
				this.EnsureAcceptEx(socketHandle);
				return (T)((object)this.acceptEx);
			}
			if (typeof(T) == typeof(GetAcceptExSockaddrsDelegate))
			{
				this.EnsureGetAcceptExSockaddrs(socketHandle);
				return (T)((object)this.getAcceptExSockaddrs);
			}
			if (typeof(T) == typeof(ConnectExDelegate))
			{
				this.EnsureConnectEx(socketHandle);
				return (T)((object)this.connectEx);
			}
			if (typeof(T) == typeof(DisconnectExDelegate))
			{
				this.EnsureDisconnectEx(socketHandle);
				return (T)((object)this.disconnectEx);
			}
			if (typeof(T) == typeof(DisconnectExDelegate_Blocking))
			{
				this.EnsureDisconnectEx(socketHandle);
				return (T)((object)this.disconnectEx_Blocking);
			}
			if (typeof(T) == typeof(WSARecvMsgDelegate))
			{
				this.EnsureWSARecvMsg(socketHandle);
				return (T)((object)this.recvMsg);
			}
			if (typeof(T) == typeof(WSARecvMsgDelegate_Blocking))
			{
				this.EnsureWSARecvMsg(socketHandle);
				return (T)((object)this.recvMsg_Blocking);
			}
			if (typeof(T) == typeof(TransmitPacketsDelegate))
			{
				this.EnsureTransmitPackets(socketHandle);
				return (T)((object)this.transmitPackets);
			}
			return default(T);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000A3990 File Offset: 0x000A1B90
		private unsafe IntPtr LoadDynamicFunctionPointer(SafeCloseSocket socketHandle, ref Guid guid)
		{
			IntPtr zero = IntPtr.Zero;
			int num;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl(socketHandle, -939524090, ref guid, sizeof(Guid), out zero, sizeof(IntPtr), out num, IntPtr.Zero, IntPtr.Zero);
			if (socketError != SocketError.Success)
			{
				throw new SocketException();
			}
			return zero;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000A39D4 File Offset: 0x000A1BD4
		private void EnsureAcceptEx(SafeCloseSocket socketHandle)
		{
			if (this.acceptEx == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.acceptEx == null)
					{
						Guid guid = new Guid("{0xb5367df1,0xcbac,0x11cf,{0x95, 0xca, 0x00, 0x80, 0x5f, 0x48, 0xa1, 0x92}}");
						IntPtr intPtr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.acceptEx = (AcceptExDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(AcceptExDelegate));
					}
				}
			}
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000A3A50 File Offset: 0x000A1C50
		private void EnsureGetAcceptExSockaddrs(SafeCloseSocket socketHandle)
		{
			if (this.getAcceptExSockaddrs == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.getAcceptExSockaddrs == null)
					{
						Guid guid = new Guid("{0xb5367df2,0xcbac,0x11cf,{0x95, 0xca, 0x00, 0x80, 0x5f, 0x48, 0xa1, 0x92}}");
						IntPtr intPtr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.getAcceptExSockaddrs = (GetAcceptExSockaddrsDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetAcceptExSockaddrsDelegate));
					}
				}
			}
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000A3ACC File Offset: 0x000A1CCC
		private void EnsureConnectEx(SafeCloseSocket socketHandle)
		{
			if (this.connectEx == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.connectEx == null)
					{
						Guid guid = new Guid("{0x25a207b9,0x0ddf3,0x4660,{0x8e,0xe9,0x76,0xe5,0x8c,0x74,0x06,0x3e}}");
						IntPtr intPtr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.connectEx = (ConnectExDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ConnectExDelegate));
					}
				}
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000A3B48 File Offset: 0x000A1D48
		private void EnsureDisconnectEx(SafeCloseSocket socketHandle)
		{
			if (this.disconnectEx == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.disconnectEx == null)
					{
						Guid guid = new Guid("{0x7fda2e11,0x8630,0x436f,{0xa0, 0x31, 0xf5, 0x36, 0xa6, 0xee, 0xc1, 0x57}}");
						IntPtr intPtr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.disconnectEx = (DisconnectExDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(DisconnectExDelegate));
						this.disconnectEx_Blocking = (DisconnectExDelegate_Blocking)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(DisconnectExDelegate_Blocking));
					}
				}
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000A3BE0 File Offset: 0x000A1DE0
		private void EnsureWSARecvMsg(SafeCloseSocket socketHandle)
		{
			if (this.recvMsg == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.recvMsg == null)
					{
						Guid guid = new Guid("{0xf689d7c8,0x6f1f,0x436b,{0x8a,0x53,0xe5,0x4f,0xe3,0x51,0xc3,0x22}}");
						IntPtr intPtr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.recvMsg = (WSARecvMsgDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(WSARecvMsgDelegate));
						this.recvMsg_Blocking = (WSARecvMsgDelegate_Blocking)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(WSARecvMsgDelegate_Blocking));
					}
				}
			}
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000A3C78 File Offset: 0x000A1E78
		private void EnsureTransmitPackets(SafeCloseSocket socketHandle)
		{
			if (this.transmitPackets == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.transmitPackets == null)
					{
						Guid guid = new Guid("{0xd9689da0,0x1f90,0x11d3,{0x99,0x71,0x00,0xc0,0x4f,0x68,0xc8,0x76}}");
						IntPtr intPtr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.transmitPackets = (TransmitPacketsDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(TransmitPacketsDelegate));
					}
				}
			}
		}

		// Token: 0x04001F55 RID: 8021
		private static List<DynamicWinsockMethods> s_MethodTable = new List<DynamicWinsockMethods>();

		// Token: 0x04001F56 RID: 8022
		private AddressFamily addressFamily;

		// Token: 0x04001F57 RID: 8023
		private SocketType socketType;

		// Token: 0x04001F58 RID: 8024
		private ProtocolType protocolType;

		// Token: 0x04001F59 RID: 8025
		private object lockObject;

		// Token: 0x04001F5A RID: 8026
		private AcceptExDelegate acceptEx;

		// Token: 0x04001F5B RID: 8027
		private GetAcceptExSockaddrsDelegate getAcceptExSockaddrs;

		// Token: 0x04001F5C RID: 8028
		private ConnectExDelegate connectEx;

		// Token: 0x04001F5D RID: 8029
		private TransmitPacketsDelegate transmitPackets;

		// Token: 0x04001F5E RID: 8030
		private DisconnectExDelegate disconnectEx;

		// Token: 0x04001F5F RID: 8031
		private DisconnectExDelegate_Blocking disconnectEx_Blocking;

		// Token: 0x04001F60 RID: 8032
		private WSARecvMsgDelegate recvMsg;

		// Token: 0x04001F61 RID: 8033
		private WSARecvMsgDelegate_Blocking recvMsg_Blocking;
	}
}
