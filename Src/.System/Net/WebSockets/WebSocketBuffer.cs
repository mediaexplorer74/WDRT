using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Net.WebSockets
{
	// Token: 0x02000231 RID: 561
	internal class WebSocketBuffer : IDisposable
	{
		// Token: 0x060014E5 RID: 5349 RVA: 0x0006D738 File Offset: 0x0006B938
		private WebSocketBuffer(ArraySegment<byte> internalBuffer, int receiveBufferSize, int sendBufferSize)
		{
			this.m_ReceiveBufferSize = receiveBufferSize;
			this.m_SendBufferSize = sendBufferSize;
			this.m_InternalBuffer = internalBuffer;
			this.m_GCHandle = GCHandle.Alloc(internalBuffer.Array, GCHandleType.Pinned);
			int num = this.m_ReceiveBufferSize + this.m_SendBufferSize + 144;
			this.m_StartAddress = Marshal.UnsafeAddrOfPinnedArrayElement(internalBuffer.Array, internalBuffer.Offset).ToInt64();
			this.m_EndAddress = this.m_StartAddress + (long)num;
			this.m_NativeBuffer = new ArraySegment<byte>(internalBuffer.Array, internalBuffer.Offset, num);
			this.m_PayloadBuffer = new ArraySegment<byte>(internalBuffer.Array, this.m_NativeBuffer.Offset + this.m_NativeBuffer.Count, this.m_ReceiveBufferSize);
			this.m_PropertyBuffer = new ArraySegment<byte>(internalBuffer.Array, this.m_PayloadBuffer.Offset + this.m_PayloadBuffer.Count, WebSocketBuffer.s_PropertyBufferSize);
			this.m_SendBufferState = 0;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0006D840 File Offset: 0x0006BA40
		internal static int SizeOfUInt
		{
			get
			{
				return WebSocketBuffer.s_SizeOfUInt;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0006D847 File Offset: 0x0006BA47
		public int ReceiveBufferSize
		{
			get
			{
				return this.m_ReceiveBufferSize;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0006D84F File Offset: 0x0006BA4F
		public int SendBufferSize
		{
			get
			{
				return this.m_SendBufferSize;
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0006D857 File Offset: 0x0006BA57
		internal static WebSocketBuffer CreateClientBuffer(ArraySegment<byte> internalBuffer, int receiveBufferSize, int sendBufferSize)
		{
			return new WebSocketBuffer(internalBuffer, receiveBufferSize, WebSocketBuffer.GetNativeSendBufferSize(sendBufferSize, false));
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0006D868 File Offset: 0x0006BA68
		internal static WebSocketBuffer CreateServerBuffer(ArraySegment<byte> internalBuffer, int receiveBufferSize)
		{
			int nativeSendBufferSize = WebSocketBuffer.GetNativeSendBufferSize(16, true);
			return new WebSocketBuffer(internalBuffer, receiveBufferSize, nativeSendBufferSize);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0006D886 File Offset: 0x0006BA86
		public void Dispose(WebSocketState webSocketState)
		{
			if (Interlocked.CompareExchange(ref this.m_StateWhenDisposing, (int)webSocketState, -2147483648) != -2147483648)
			{
				return;
			}
			this.CleanUp();
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0006D8A7 File Offset: 0x0006BAA7
		public void Dispose()
		{
			this.Dispose(WebSocketState.None);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0006D8B0 File Offset: 0x0006BAB0
		internal WebSocketProtocolComponent.Property[] CreateProperties(bool useZeroMaskingKey)
		{
			this.ThrowIfDisposed();
			IntPtr intPtr = this.m_GCHandle.AddrOfPinnedObject();
			int num = this.m_PropertyBuffer.Offset;
			Marshal.WriteInt32(intPtr, num, this.m_ReceiveBufferSize);
			num += WebSocketBuffer.s_SizeOfUInt;
			Marshal.WriteInt32(intPtr, num, this.m_SendBufferSize);
			num += WebSocketBuffer.s_SizeOfUInt;
			Marshal.WriteIntPtr(intPtr, num, intPtr + this.m_InternalBuffer.Offset);
			num += IntPtr.Size;
			Marshal.WriteInt32(intPtr, num, useZeroMaskingKey ? 1 : 0);
			int num2 = (useZeroMaskingKey ? 4 : 3);
			WebSocketProtocolComponent.Property[] array = new WebSocketProtocolComponent.Property[num2];
			num = this.m_PropertyBuffer.Offset;
			array[0] = new WebSocketProtocolComponent.Property
			{
				Type = WebSocketProtocolComponent.PropertyType.ReceiveBufferSize,
				PropertySize = (uint)WebSocketBuffer.s_SizeOfUInt,
				PropertyData = IntPtr.Add(intPtr, num)
			};
			num += WebSocketBuffer.s_SizeOfUInt;
			array[1] = new WebSocketProtocolComponent.Property
			{
				Type = WebSocketProtocolComponent.PropertyType.SendBufferSize,
				PropertySize = (uint)WebSocketBuffer.s_SizeOfUInt,
				PropertyData = IntPtr.Add(intPtr, num)
			};
			num += WebSocketBuffer.s_SizeOfUInt;
			array[2] = new WebSocketProtocolComponent.Property
			{
				Type = WebSocketProtocolComponent.PropertyType.AllocatedBuffer,
				PropertySize = (uint)this.m_NativeBuffer.Count,
				PropertyData = IntPtr.Add(intPtr, num)
			};
			num += IntPtr.Size;
			if (useZeroMaskingKey)
			{
				array[3] = new WebSocketProtocolComponent.Property
				{
					Type = WebSocketProtocolComponent.PropertyType.DisableMasking,
					PropertySize = (uint)WebSocketBuffer.s_SizeOfBool,
					PropertyData = IntPtr.Add(intPtr, num)
				};
			}
			return array;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0006DA50 File Offset: 0x0006BC50
		internal void PinSendBuffer(ArraySegment<byte> payload, out bool bufferHasBeenPinned)
		{
			bufferHasBeenPinned = false;
			WebSocketHelpers.ValidateBuffer(payload.Array, payload.Offset, payload.Count);
			int num = Interlocked.Exchange(ref this.m_SendBufferState, 1);
			if (num != 0)
			{
				throw new AccessViolationException();
			}
			this.m_PinnedSendBuffer = payload;
			this.m_PinnedSendBufferHandle = GCHandle.Alloc(this.m_PinnedSendBuffer.Array, GCHandleType.Pinned);
			bufferHasBeenPinned = true;
			this.m_PinnedSendBufferStartAddress = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_PinnedSendBuffer.Array, this.m_PinnedSendBuffer.Offset).ToInt64();
			this.m_PinnedSendBufferEndAddress = this.m_PinnedSendBufferStartAddress + (long)this.m_PinnedSendBuffer.Count;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0006DAF3 File Offset: 0x0006BCF3
		internal IntPtr ConvertPinnedSendPayloadToNative(ArraySegment<byte> payload)
		{
			return this.ConvertPinnedSendPayloadToNative(payload.Array, payload.Offset, payload.Count);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0006DB10 File Offset: 0x0006BD10
		internal IntPtr ConvertPinnedSendPayloadToNative(byte[] buffer, int offset, int count)
		{
			if (!this.IsPinnedSendPayloadBuffer(buffer, offset, count))
			{
				throw new AccessViolationException();
			}
			return new IntPtr(this.m_PinnedSendBufferStartAddress + (long)offset - (long)this.m_PinnedSendBuffer.Offset);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0006DB40 File Offset: 0x0006BD40
		internal ArraySegment<byte> ConvertPinnedSendPayloadFromNative(WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType)
		{
			if (!this.IsPinnedSendPayloadBuffer(buffer, bufferType))
			{
				throw new AccessViolationException();
			}
			IntPtr intPtr;
			uint num;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out num);
			int num2 = (int)(intPtr.ToInt64() - this.m_PinnedSendBufferStartAddress);
			return new ArraySegment<byte>(this.m_PinnedSendBuffer.Array, this.m_PinnedSendBuffer.Offset + num2, (int)num);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0006DB98 File Offset: 0x0006BD98
		private bool IsPinnedSendPayloadBuffer(byte[] buffer, int offset, int count)
		{
			return this.m_SendBufferState == 1 && (buffer == this.m_PinnedSendBuffer.Array && offset >= this.m_PinnedSendBuffer.Offset) && offset + count <= this.m_PinnedSendBuffer.Offset + this.m_PinnedSendBuffer.Count;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0006DBF0 File Offset: 0x0006BDF0
		internal bool IsPinnedSendPayloadBuffer(WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType)
		{
			if (this.m_SendBufferState != 1)
			{
				return false;
			}
			IntPtr intPtr;
			uint num;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out num);
			long num2 = intPtr.ToInt64();
			long num3 = num2 + (long)((ulong)num);
			return num2 >= this.m_PinnedSendBufferStartAddress && num3 >= this.m_PinnedSendBufferStartAddress && num2 <= this.m_PinnedSendBufferEndAddress && num3 <= this.m_PinnedSendBufferEndAddress;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0006DC4C File Offset: 0x0006BE4C
		internal void ReleasePinnedSendBuffer()
		{
			int num = Interlocked.Exchange(ref this.m_SendBufferState, 0);
			if (num != 1)
			{
				return;
			}
			if (this.m_PinnedSendBufferHandle.IsAllocated)
			{
				this.m_PinnedSendBufferHandle.Free();
			}
			this.m_PinnedSendBuffer = WebSocketHelpers.EmptyPayload;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0006DC90 File Offset: 0x0006BE90
		internal void BufferPayload(ArraySegment<byte> payload, int unconsumedDataOffset, WebSocketMessageType messageType, bool endOfMessage)
		{
			this.ThrowIfDisposed();
			int num = payload.Count - unconsumedDataOffset;
			Buffer.BlockCopy(payload.Array, payload.Offset + unconsumedDataOffset, this.m_PayloadBuffer.Array, this.m_PayloadBuffer.Offset, num);
			this.m_BufferedPayloadReceiveResult = new WebSocketReceiveResult(num, messageType, endOfMessage);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0006DCF0 File Offset: 0x0006BEF0
		internal bool ReceiveFromBufferedPayload(ArraySegment<byte> buffer, out WebSocketReceiveResult receiveResult)
		{
			this.ThrowIfDisposed();
			int num = Math.Min(buffer.Count, this.m_BufferedPayloadReceiveResult.Count);
			receiveResult = this.m_BufferedPayloadReceiveResult.Copy(num);
			Buffer.BlockCopy(this.m_PayloadBuffer.Array, this.m_PayloadBuffer.Offset + this.m_PayloadOffset, buffer.Array, buffer.Offset, num);
			bool flag;
			if (this.m_BufferedPayloadReceiveResult.Count == 0)
			{
				this.m_PayloadOffset = 0;
				this.m_BufferedPayloadReceiveResult = null;
				flag = false;
			}
			else
			{
				this.m_PayloadOffset += num;
				flag = true;
			}
			return flag;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0006DDA0 File Offset: 0x0006BFA0
		internal ArraySegment<byte> ConvertNativeBuffer(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType)
		{
			this.ThrowIfDisposed();
			IntPtr intPtr;
			uint num;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out num);
			if (intPtr == IntPtr.Zero)
			{
				return WebSocketHelpers.EmptyPayload;
			}
			if (this.IsNativeBuffer(intPtr, num))
			{
				return new ArraySegment<byte>(this.m_InternalBuffer.Array, this.GetOffset(intPtr), (int)num);
			}
			throw new AccessViolationException();
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0006DDFC File Offset: 0x0006BFFC
		internal void ConvertCloseBuffer(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer buffer, out WebSocketCloseStatus closeStatus, out string reason)
		{
			this.ThrowIfDisposed();
			closeStatus = (WebSocketCloseStatus)buffer.CloseStatus.CloseStatus;
			IntPtr intPtr;
			uint num;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, (WebSocketProtocolComponent.BufferType)2147483652U, out intPtr, out num);
			if (intPtr == IntPtr.Zero)
			{
				reason = null;
				return;
			}
			if (this.IsNativeBuffer(intPtr, num))
			{
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(this.m_InternalBuffer.Array, this.GetOffset(intPtr), (int)num);
				reason = Encoding.UTF8.GetString(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				return;
			}
			throw new AccessViolationException();
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0006DE90 File Offset: 0x0006C090
		internal void ValidateNativeBuffers(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount)
		{
			this.ThrowIfDisposed();
			if ((ulong)dataBufferCount > (ulong)((long)dataBuffers.Length))
			{
				throw new AccessViolationException();
			}
			int num = dataBuffers.Length;
			bool flag = action == WebSocketProtocolComponent.Action.IndicateSendComplete || action == WebSocketProtocolComponent.Action.SendToNetwork;
			if (flag)
			{
				num = (int)dataBufferCount;
			}
			bool flag2 = false;
			for (int i = 0; i < num; i++)
			{
				WebSocketProtocolComponent.Buffer buffer = dataBuffers[i];
				IntPtr intPtr;
				uint num2;
				WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out num2);
				if (!(intPtr == IntPtr.Zero))
				{
					flag2 = true;
					bool flag3 = this.IsPinnedSendPayloadBuffer(buffer, bufferType);
					if ((ulong)num2 > (ulong)((long)this.GetMaxBufferSize()) && (!flag || !flag3))
					{
						throw new AccessViolationException();
					}
					if (!flag3 && !this.IsNativeBuffer(intPtr, num2))
					{
						throw new AccessViolationException();
					}
				}
			}
			if (flag2 || action == WebSocketProtocolComponent.Action.NoAction || action != WebSocketProtocolComponent.Action.IndicateReceiveComplete)
			{
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0006DF43 File Offset: 0x0006C143
		private static int GetNativeSendBufferSize(int sendBufferSize, bool isServerBuffer)
		{
			if (!isServerBuffer)
			{
				return sendBufferSize;
			}
			return 16;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0006DF4C File Offset: 0x0006C14C
		internal static void UnwrapWebSocketBuffer(WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType, out IntPtr bufferData, out uint bufferLength)
		{
			bufferData = IntPtr.Zero;
			bufferLength = 0U;
			if (bufferType != WebSocketProtocolComponent.BufferType.None)
			{
				switch (bufferType)
				{
				case (WebSocketProtocolComponent.BufferType)2147483648U:
				case (WebSocketProtocolComponent.BufferType)2147483649U:
				case (WebSocketProtocolComponent.BufferType)2147483650U:
				case (WebSocketProtocolComponent.BufferType)2147483651U:
				case (WebSocketProtocolComponent.BufferType)2147483653U:
				case (WebSocketProtocolComponent.BufferType)2147483654U:
					break;
				case (WebSocketProtocolComponent.BufferType)2147483652U:
					bufferData = buffer.CloseStatus.ReasonData;
					bufferLength = buffer.CloseStatus.ReasonLength;
					return;
				default:
					return;
				}
			}
			bufferData = buffer.Data.BufferData;
			bufferLength = buffer.Data.BufferLength;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0006DFC4 File Offset: 0x0006C1C4
		private void ThrowIfDisposed()
		{
			int stateWhenDisposing = this.m_StateWhenDisposing;
			if (stateWhenDisposing == -2147483648)
			{
				return;
			}
			if (stateWhenDisposing - 5 > 1)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
			{
				typeof(WebSocketBase),
				this.m_StateWhenDisposing
			}));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0006E02C File Offset: 0x0006C22C
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		private void ValidateBufferedPayload()
		{
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0006E030 File Offset: 0x0006C230
		private int GetOffset(IntPtr pBuffer)
		{
			return (int)(pBuffer.ToInt64() - this.m_StartAddress + (long)this.m_InternalBuffer.Offset);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0006E05E File Offset: 0x0006C25E
		private int GetMaxBufferSize()
		{
			return Math.Max(this.m_ReceiveBufferSize, this.m_SendBufferSize);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0006E074 File Offset: 0x0006C274
		internal bool IsInternalBuffer(byte[] buffer, int offset, int count)
		{
			return buffer == this.m_NativeBuffer.Array && offset >= this.m_NativeBuffer.Offset && offset + count <= this.m_NativeBuffer.Offset + this.m_NativeBuffer.Count;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0006E0CC File Offset: 0x0006C2CC
		internal IntPtr ToIntPtr(int offset)
		{
			return new IntPtr(this.m_StartAddress + (long)offset - (long)this.m_InternalBuffer.Offset);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0006E0F8 File Offset: 0x0006C2F8
		private bool IsNativeBuffer(IntPtr pBuffer, uint bufferSize)
		{
			long num = pBuffer.ToInt64();
			long num2 = (long)((ulong)bufferSize + (ulong)num);
			return num >= this.m_StartAddress && num <= this.m_EndAddress && num2 >= this.m_StartAddress && num2 <= this.m_EndAddress;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0006E13C File Offset: 0x0006C33C
		private void CleanUp()
		{
			if (this.m_GCHandle.IsAllocated)
			{
				this.m_GCHandle.Free();
			}
			this.ReleasePinnedSendBuffer();
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0006E170 File Offset: 0x0006C370
		internal static ArraySegment<byte> CreateInternalBufferArraySegment(int receiveBufferSize, int sendBufferSize, bool isServerBuffer)
		{
			int internalBufferSize = WebSocketBuffer.GetInternalBufferSize(receiveBufferSize, sendBufferSize, isServerBuffer);
			return new ArraySegment<byte>(new byte[internalBufferSize]);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0006E194 File Offset: 0x0006C394
		internal static void Validate(int count, int receiveBufferSize, int sendBufferSize, bool isServerBuffer)
		{
			int internalBufferSize = WebSocketBuffer.GetInternalBufferSize(receiveBufferSize, sendBufferSize, isServerBuffer);
			if (count < internalBufferSize)
			{
				throw new ArgumentOutOfRangeException("internalBuffer", SR.GetString("net_WebSockets_ArgumentOutOfRange_InternalBuffer", new object[] { internalBufferSize }));
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0006E1D4 File Offset: 0x0006C3D4
		private static int GetInternalBufferSize(int receiveBufferSize, int sendBufferSize, bool isServerBuffer)
		{
			int nativeSendBufferSize = WebSocketBuffer.GetNativeSendBufferSize(sendBufferSize, isServerBuffer);
			return 2 * receiveBufferSize + nativeSendBufferSize + 144 + WebSocketBuffer.s_PropertyBufferSize;
		}

		// Token: 0x0400165C RID: 5724
		private const int NativeOverheadBufferSize = 144;

		// Token: 0x0400165D RID: 5725
		internal const int MinSendBufferSize = 16;

		// Token: 0x0400165E RID: 5726
		internal const int MinReceiveBufferSize = 256;

		// Token: 0x0400165F RID: 5727
		internal const int MaxBufferSize = 65536;

		// Token: 0x04001660 RID: 5728
		private static readonly int s_SizeOfUInt = Marshal.SizeOf(typeof(uint));

		// Token: 0x04001661 RID: 5729
		private static readonly int s_SizeOfBool = Marshal.SizeOf(typeof(bool));

		// Token: 0x04001662 RID: 5730
		private static readonly int s_PropertyBufferSize = 2 * WebSocketBuffer.s_SizeOfUInt + WebSocketBuffer.s_SizeOfBool + IntPtr.Size;

		// Token: 0x04001663 RID: 5731
		private readonly int m_ReceiveBufferSize;

		// Token: 0x04001664 RID: 5732
		private readonly long m_StartAddress;

		// Token: 0x04001665 RID: 5733
		private readonly long m_EndAddress;

		// Token: 0x04001666 RID: 5734
		private readonly GCHandle m_GCHandle;

		// Token: 0x04001667 RID: 5735
		private readonly ArraySegment<byte> m_InternalBuffer;

		// Token: 0x04001668 RID: 5736
		private readonly ArraySegment<byte> m_NativeBuffer;

		// Token: 0x04001669 RID: 5737
		private readonly ArraySegment<byte> m_PayloadBuffer;

		// Token: 0x0400166A RID: 5738
		private readonly ArraySegment<byte> m_PropertyBuffer;

		// Token: 0x0400166B RID: 5739
		private readonly int m_SendBufferSize;

		// Token: 0x0400166C RID: 5740
		private volatile int m_PayloadOffset;

		// Token: 0x0400166D RID: 5741
		private volatile WebSocketReceiveResult m_BufferedPayloadReceiveResult;

		// Token: 0x0400166E RID: 5742
		private long m_PinnedSendBufferStartAddress;

		// Token: 0x0400166F RID: 5743
		private long m_PinnedSendBufferEndAddress;

		// Token: 0x04001670 RID: 5744
		private ArraySegment<byte> m_PinnedSendBuffer;

		// Token: 0x04001671 RID: 5745
		private GCHandle m_PinnedSendBufferHandle;

		// Token: 0x04001672 RID: 5746
		private int m_StateWhenDisposing = int.MinValue;

		// Token: 0x04001673 RID: 5747
		private int m_SendBufferState;

		// Token: 0x02000778 RID: 1912
		private static class SendBufferState
		{
			// Token: 0x040032A8 RID: 12968
			public const int None = 0;

			// Token: 0x040032A9 RID: 12969
			public const int SendPayloadSpecified = 1;
		}
	}
}
