using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Net.WebSockets
{
	// Token: 0x0200023A RID: 570
	internal static class WebSocketProtocolComponent
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x0006FBFC File Offset: 0x0006DDFC
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		static WebSocketProtocolComponent()
		{
			if (!WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketProtocolComponent.s_SupportedVersion = WebSocketProtocolComponent.GetSupportedVersion();
				WebSocketProtocolComponent.s_ServerFakeRequestHeaders = new WebSocketProtocolComponent.HttpHeader[]
				{
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Connection",
						NameLength = (uint)"Connection".Length,
						Value = "Upgrade",
						ValueLength = (uint)"Upgrade".Length
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Upgrade",
						NameLength = (uint)"Upgrade".Length,
						Value = "websocket",
						ValueLength = (uint)"websocket".Length
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Host",
						NameLength = (uint)"Host".Length,
						Value = string.Empty,
						ValueLength = 0U
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Sec-WebSocket-Version",
						NameLength = (uint)"Sec-WebSocket-Version".Length,
						Value = WebSocketProtocolComponent.s_SupportedVersion,
						ValueLength = (uint)WebSocketProtocolComponent.s_SupportedVersion.Length
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Sec-WebSocket-Key",
						NameLength = (uint)"Sec-WebSocket-Key".Length,
						Value = WebSocketProtocolComponent.s_DummyWebsocketKeyBase64,
						ValueLength = (uint)WebSocketProtocolComponent.s_DummyWebsocketKeyBase64.Length
					}
				};
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0006FE69 File Offset: 0x0006E069
		internal static string SupportedVersion
		{
			get
			{
				if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
				{
					WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
				}
				return WebSocketProtocolComponent.s_SupportedVersion;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0006FE81 File Offset: 0x0006E081
		internal static bool IsSupported
		{
			get
			{
				return !WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid;
			}
		}

		// Token: 0x0600157C RID: 5500
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketCreateClientHandle", ExactSpelling = true)]
		private static extern int WebSocketCreateClientHandle_Raw([In] WebSocketProtocolComponent.Property[] properties, [In] uint propertyCount, out SafeWebSocketHandle webSocketHandle);

		// Token: 0x0600157D RID: 5501
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketBeginClientHandshake", ExactSpelling = true)]
		private static extern int WebSocketBeginClientHandshake_Raw([In] SafeHandle webSocketHandle, [In] IntPtr subProtocols, [In] uint subProtocolCount, [In] IntPtr extensions, [In] uint extensionCount, [In] WebSocketProtocolComponent.HttpHeader[] initialHeaders, [In] uint initialHeaderCount, out IntPtr additionalHeadersPtr, out uint additionalHeaderCount);

		// Token: 0x0600157E RID: 5502
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketEndClientHandshake", ExactSpelling = true)]
		private static extern int WebSocketEndClientHandshake_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.HttpHeader[] responseHeaders, [In] uint responseHeaderCount, [In] [Out] IntPtr selectedExtensions, [In] IntPtr selectedExtensionCount, [In] IntPtr selectedSubProtocol);

		// Token: 0x0600157F RID: 5503
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketBeginServerHandshake", ExactSpelling = true)]
		private static extern int WebSocketBeginServerHandshake_Raw([In] SafeHandle webSocketHandle, [In] IntPtr subProtocol, [In] IntPtr extensions, [In] uint extensionCount, [In] WebSocketProtocolComponent.HttpHeader[] requestHeaders, [In] uint requestHeaderCount, out IntPtr responseHeadersPtr, out uint responseHeaderCount);

		// Token: 0x06001580 RID: 5504
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketEndServerHandshake", ExactSpelling = true)]
		private static extern int WebSocketEndServerHandshake_Raw([In] SafeHandle webSocketHandle);

		// Token: 0x06001581 RID: 5505
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketCreateServerHandle", ExactSpelling = true)]
		private static extern int WebSocketCreateServerHandle_Raw([In] WebSocketProtocolComponent.Property[] properties, [In] uint propertyCount, out SafeWebSocketHandle webSocketHandle);

		// Token: 0x06001582 RID: 5506
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketAbortHandle", ExactSpelling = true)]
		private static extern void WebSocketAbortHandle_Raw([In] SafeHandle webSocketHandle);

		// Token: 0x06001583 RID: 5507
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketDeleteHandle", ExactSpelling = true)]
		private static extern void WebSocketDeleteHandle_Raw([In] IntPtr webSocketHandle);

		// Token: 0x06001584 RID: 5508
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketSend", ExactSpelling = true)]
		private static extern int WebSocketSend_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.BufferType bufferType, [In] ref WebSocketProtocolComponent.Buffer buffer, [In] IntPtr applicationContext);

		// Token: 0x06001585 RID: 5509
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketSend", ExactSpelling = true)]
		private static extern int WebSocketSendWithoutBody_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.BufferType bufferType, [In] IntPtr buffer, [In] IntPtr applicationContext);

		// Token: 0x06001586 RID: 5510
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketReceive", ExactSpelling = true)]
		private static extern int WebSocketReceive_Raw([In] SafeHandle webSocketHandle, [In] IntPtr buffers, [In] IntPtr applicationContext);

		// Token: 0x06001587 RID: 5511
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketGetAction", ExactSpelling = true)]
		private static extern int WebSocketGetAction_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.ActionQueue actionQueue, [In] [Out] WebSocketProtocolComponent.Buffer[] dataBuffers, [In] [Out] ref uint dataBufferCount, out WebSocketProtocolComponent.Action action, out WebSocketProtocolComponent.BufferType bufferType, out IntPtr applicationContext, out IntPtr actionContext);

		// Token: 0x06001588 RID: 5512
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketCompleteAction", ExactSpelling = true)]
		private static extern void WebSocketCompleteAction_Raw([In] SafeHandle webSocketHandle, [In] IntPtr actionContext, [In] uint bytesTransferred);

		// Token: 0x06001589 RID: 5513
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketGetGlobalProperty", ExactSpelling = true)]
		private static extern int WebSocketGetGlobalProperty_Raw([In] WebSocketProtocolComponent.PropertyType property, [In] [Out] ref uint value, [In] [Out] ref uint size);

		// Token: 0x0600158A RID: 5514 RVA: 0x0006FE90 File Offset: 0x0006E090
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static string GetSupportedVersion()
		{
			if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			SafeWebSocketHandle safeWebSocketHandle = null;
			string text2;
			try
			{
				int num = WebSocketProtocolComponent.WebSocketCreateClientHandle_Raw(null, 0U, out safeWebSocketHandle);
				WebSocketProtocolComponent.ThrowOnError(num);
				if (safeWebSocketHandle == null || safeWebSocketHandle.IsInvalid)
				{
					WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
				}
				IntPtr intPtr;
				uint num2;
				num = WebSocketProtocolComponent.WebSocketBeginClientHandshake_Raw(safeWebSocketHandle, IntPtr.Zero, 0U, IntPtr.Zero, 0U, WebSocketProtocolComponent.s_InitialClientRequestHeaders, (uint)WebSocketProtocolComponent.s_InitialClientRequestHeaders.Length, out intPtr, out num2);
				WebSocketProtocolComponent.ThrowOnError(num);
				WebSocketProtocolComponent.HttpHeader[] array = WebSocketProtocolComponent.MarshalHttpHeaders(intPtr, (int)num2);
				string text = null;
				foreach (WebSocketProtocolComponent.HttpHeader httpHeader in array)
				{
					if (string.Compare(httpHeader.Name, "Sec-WebSocket-Version", StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = httpHeader.Value;
						break;
					}
				}
				text2 = text;
			}
			finally
			{
				if (safeWebSocketHandle != null)
				{
					safeWebSocketHandle.Dispose();
				}
			}
			return text2;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0006FF68 File Offset: 0x0006E168
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketCreateClientHandle(WebSocketProtocolComponent.Property[] properties, out SafeWebSocketHandle webSocketHandle)
		{
			uint num = (uint)((properties == null) ? 0 : properties.Length);
			if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			int num2 = WebSocketProtocolComponent.WebSocketCreateClientHandle_Raw(properties, num, out webSocketHandle);
			WebSocketProtocolComponent.ThrowOnError(num2);
			if (webSocketHandle == null || webSocketHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			IntPtr intPtr;
			uint num3;
			num2 = WebSocketProtocolComponent.WebSocketBeginClientHandshake_Raw(webSocketHandle, IntPtr.Zero, 0U, IntPtr.Zero, 0U, WebSocketProtocolComponent.s_InitialClientRequestHeaders, (uint)WebSocketProtocolComponent.s_InitialClientRequestHeaders.Length, out intPtr, out num3);
			WebSocketProtocolComponent.ThrowOnError(num2);
			WebSocketProtocolComponent.HttpHeader[] array = WebSocketProtocolComponent.MarshalHttpHeaders(intPtr, (int)num3);
			string text = null;
			foreach (WebSocketProtocolComponent.HttpHeader httpHeader in array)
			{
				if (string.Compare(httpHeader.Name, "Sec-WebSocket-Key", StringComparison.OrdinalIgnoreCase) == 0)
				{
					text = httpHeader.Value;
					break;
				}
			}
			string secWebSocketAcceptString = WebSocketHelpers.GetSecWebSocketAcceptString(text);
			WebSocketProtocolComponent.HttpHeader[] array3 = new WebSocketProtocolComponent.HttpHeader[]
			{
				new WebSocketProtocolComponent.HttpHeader
				{
					Name = "Connection",
					NameLength = (uint)"Connection".Length,
					Value = "Upgrade",
					ValueLength = (uint)"Upgrade".Length
				},
				new WebSocketProtocolComponent.HttpHeader
				{
					Name = "Upgrade",
					NameLength = (uint)"Upgrade".Length,
					Value = "websocket",
					ValueLength = (uint)"websocket".Length
				},
				new WebSocketProtocolComponent.HttpHeader
				{
					Name = "Sec-WebSocket-Accept",
					NameLength = (uint)"Sec-WebSocket-Accept".Length,
					Value = secWebSocketAcceptString,
					ValueLength = (uint)secWebSocketAcceptString.Length
				}
			};
			num2 = WebSocketProtocolComponent.WebSocketEndClientHandshake_Raw(webSocketHandle, array3, (uint)array3.Length, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			WebSocketProtocolComponent.ThrowOnError(num2);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00070138 File Offset: 0x0006E338
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketCreateServerHandle(WebSocketProtocolComponent.Property[] properties, int propertyCount, out SafeWebSocketHandle webSocketHandle)
		{
			if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			int num = WebSocketProtocolComponent.WebSocketCreateServerHandle_Raw(properties, (uint)propertyCount, out webSocketHandle);
			WebSocketProtocolComponent.ThrowOnError(num);
			if (webSocketHandle == null || webSocketHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			IntPtr intPtr;
			uint num2;
			num = WebSocketProtocolComponent.WebSocketBeginServerHandshake_Raw(webSocketHandle, IntPtr.Zero, IntPtr.Zero, 0U, WebSocketProtocolComponent.s_ServerFakeRequestHeaders, (uint)WebSocketProtocolComponent.s_ServerFakeRequestHeaders.Length, out intPtr, out num2);
			WebSocketProtocolComponent.ThrowOnError(num);
			WebSocketProtocolComponent.HttpHeader[] array = WebSocketProtocolComponent.MarshalHttpHeaders(intPtr, (int)num2);
			num = WebSocketProtocolComponent.WebSocketEndServerHandshake_Raw(webSocketHandle);
			WebSocketProtocolComponent.ThrowOnError(num);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000701B6 File Offset: 0x0006E3B6
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketAbortHandle(SafeHandle webSocketHandle)
		{
			WebSocketProtocolComponent.WebSocketAbortHandle_Raw(webSocketHandle);
			WebSocketProtocolComponent.DrainActionQueue(webSocketHandle, WebSocketProtocolComponent.ActionQueue.Send);
			WebSocketProtocolComponent.DrainActionQueue(webSocketHandle, WebSocketProtocolComponent.ActionQueue.Receive);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000701CC File Offset: 0x0006E3CC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketDeleteHandle(IntPtr webSocketPtr)
		{
			WebSocketProtocolComponent.WebSocketDeleteHandle_Raw(webSocketPtr);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000701D4 File Offset: 0x0006E3D4
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketSend(WebSocketBase webSocket, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Buffer buffer)
		{
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int num;
			try
			{
				num = WebSocketProtocolComponent.WebSocketSend_Raw(webSocket.SessionHandle, bufferType, ref buffer, IntPtr.Zero);
			}
			catch (ObjectDisposedException ex)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, ex);
			}
			WebSocketProtocolComponent.ThrowOnError(num);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0007021C File Offset: 0x0006E41C
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketSendWithoutBody(WebSocketBase webSocket, WebSocketProtocolComponent.BufferType bufferType)
		{
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int num;
			try
			{
				num = WebSocketProtocolComponent.WebSocketSendWithoutBody_Raw(webSocket.SessionHandle, bufferType, IntPtr.Zero, IntPtr.Zero);
			}
			catch (ObjectDisposedException ex)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, ex);
			}
			WebSocketProtocolComponent.ThrowOnError(num);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00070268 File Offset: 0x0006E468
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketReceive(WebSocketBase webSocket)
		{
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int num;
			try
			{
				num = WebSocketProtocolComponent.WebSocketReceive_Raw(webSocket.SessionHandle, IntPtr.Zero, IntPtr.Zero);
			}
			catch (ObjectDisposedException ex)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, ex);
			}
			WebSocketProtocolComponent.ThrowOnError(num);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x000702B4 File Offset: 0x0006E4B4
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketGetAction(WebSocketBase webSocket, WebSocketProtocolComponent.ActionQueue actionQueue, WebSocketProtocolComponent.Buffer[] dataBuffers, ref uint dataBufferCount, out WebSocketProtocolComponent.Action action, out WebSocketProtocolComponent.BufferType bufferType, out IntPtr actionContext)
		{
			action = WebSocketProtocolComponent.Action.NoAction;
			bufferType = WebSocketProtocolComponent.BufferType.None;
			actionContext = IntPtr.Zero;
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int num;
			try
			{
				IntPtr intPtr;
				num = WebSocketProtocolComponent.WebSocketGetAction_Raw(webSocket.SessionHandle, actionQueue, dataBuffers, ref dataBufferCount, out action, out bufferType, out intPtr, out actionContext);
			}
			catch (ObjectDisposedException ex)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, ex);
			}
			WebSocketProtocolComponent.ThrowOnError(num);
			webSocket.ValidateNativeBuffers(action, bufferType, dataBuffers, dataBufferCount);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00070320 File Offset: 0x0006E520
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketCompleteAction(WebSocketBase webSocket, IntPtr actionContext, int bytesTransferred)
		{
			if (webSocket.SessionHandle.IsClosed)
			{
				return;
			}
			try
			{
				WebSocketProtocolComponent.WebSocketCompleteAction_Raw(webSocket.SessionHandle, actionContext, (uint)bytesTransferred);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00070360 File Offset: 0x0006E560
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static TimeSpan WebSocketGetDefaultKeepAliveInterval()
		{
			uint num = 0U;
			uint num2 = 4U;
			int num3 = WebSocketProtocolComponent.WebSocketGetGlobalProperty_Raw(WebSocketProtocolComponent.PropertyType.KeepAliveInterval, ref num, ref num2);
			if (!WebSocketProtocolComponent.Succeeded(num3))
			{
				return Timeout.InfiniteTimeSpan;
			}
			return TimeSpan.FromMilliseconds(num);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00070394 File Offset: 0x0006E594
		private static void DrainActionQueue(SafeHandle webSocketHandle, WebSocketProtocolComponent.ActionQueue actionQueue)
		{
			for (;;)
			{
				WebSocketProtocolComponent.Buffer[] array = new WebSocketProtocolComponent.Buffer[1];
				uint num = 1U;
				WebSocketProtocolComponent.Action action;
				WebSocketProtocolComponent.BufferType bufferType;
				IntPtr intPtr;
				IntPtr intPtr2;
				int num2 = WebSocketProtocolComponent.WebSocketGetAction_Raw(webSocketHandle, actionQueue, array, ref num, out action, out bufferType, out intPtr, out intPtr2);
				if (!WebSocketProtocolComponent.Succeeded(num2))
				{
					break;
				}
				if (action == WebSocketProtocolComponent.Action.NoAction)
				{
					return;
				}
				WebSocketProtocolComponent.WebSocketCompleteAction_Raw(webSocketHandle, intPtr2, 0U);
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x000703D8 File Offset: 0x0006E5D8
		private static void MarshalAndVerifyHttpHeader(IntPtr httpHeaderPtr, ref WebSocketProtocolComponent.HttpHeader httpHeader)
		{
			IntPtr intPtr = Marshal.ReadIntPtr(httpHeaderPtr);
			IntPtr intPtr2 = IntPtr.Add(httpHeaderPtr, IntPtr.Size);
			int num = Marshal.ReadInt32(intPtr2);
			if (intPtr != IntPtr.Zero)
			{
				httpHeader.Name = Marshal.PtrToStringAnsi(intPtr, num);
			}
			if ((httpHeader.Name == null && num != 0) || (httpHeader.Name != null && num != httpHeader.Name.Length))
			{
				throw new AccessViolationException();
			}
			int num2 = 2 * IntPtr.Size;
			int num3 = 3 * IntPtr.Size;
			IntPtr intPtr3 = Marshal.ReadIntPtr(IntPtr.Add(httpHeaderPtr, num2));
			intPtr2 = IntPtr.Add(httpHeaderPtr, num3);
			num = Marshal.ReadInt32(intPtr2);
			httpHeader.Value = Marshal.PtrToStringAnsi(intPtr3, num);
			if ((httpHeader.Value == null && num != 0) || (httpHeader.Value != null && num != httpHeader.Value.Length))
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000704A4 File Offset: 0x0006E6A4
		private static WebSocketProtocolComponent.HttpHeader[] MarshalHttpHeaders(IntPtr nativeHeadersPtr, int nativeHeaderCount)
		{
			WebSocketProtocolComponent.HttpHeader[] array = new WebSocketProtocolComponent.HttpHeader[nativeHeaderCount];
			int num = 4 * IntPtr.Size;
			for (int i = 0; i < nativeHeaderCount; i++)
			{
				int num2 = num * i;
				IntPtr intPtr = IntPtr.Add(nativeHeadersPtr, num2);
				WebSocketProtocolComponent.MarshalAndVerifyHttpHeader(intPtr, ref array[i]);
			}
			return array;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000704E8 File Offset: 0x0006E6E8
		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x000704F1 File Offset: 0x0006E6F1
		private static void ThrowOnError(int errorCode)
		{
			if (WebSocketProtocolComponent.Succeeded(errorCode))
			{
				return;
			}
			throw new WebSocketException(errorCode);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00070504 File Offset: 0x0006E704
		private static void ThrowIfSessionHandleClosed(WebSocketBase webSocket)
		{
			if (webSocket.SessionHandle.IsClosed)
			{
				throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
				{
					webSocket.GetType().FullName,
					webSocket.State
				}));
			}
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00070552 File Offset: 0x0006E752
		private static WebSocketException ConvertObjectDisposedException(WebSocketBase webSocket, ObjectDisposedException innerException)
		{
			return new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
			{
				webSocket.GetType().FullName,
				webSocket.State
			}), innerException);
		}

		// Token: 0x040016B5 RID: 5813
		private const string WEBSOCKET = "websocket.dll";

		// Token: 0x040016B6 RID: 5814
		private static readonly string s_DllFileName = Path.Combine(Environment.SystemDirectory, "websocket.dll");

		// Token: 0x040016B7 RID: 5815
		private static readonly string s_DummyWebsocketKeyBase64 = Convert.ToBase64String(new byte[16]);

		// Token: 0x040016B8 RID: 5816
		private static readonly SafeLoadLibrary s_WebSocketDllHandle = SafeLoadLibrary.LoadLibraryEx(WebSocketProtocolComponent.s_DllFileName);

		// Token: 0x040016B9 RID: 5817
		private static readonly string s_SupportedVersion;

		// Token: 0x040016BA RID: 5818
		private static readonly WebSocketProtocolComponent.HttpHeader[] s_InitialClientRequestHeaders = new WebSocketProtocolComponent.HttpHeader[]
		{
			new WebSocketProtocolComponent.HttpHeader
			{
				Name = "Connection",
				NameLength = (uint)"Connection".Length,
				Value = "Upgrade",
				ValueLength = (uint)"Upgrade".Length
			},
			new WebSocketProtocolComponent.HttpHeader
			{
				Name = "Upgrade",
				NameLength = (uint)"Upgrade".Length,
				Value = "websocket",
				ValueLength = (uint)"websocket".Length
			}
		};

		// Token: 0x040016BB RID: 5819
		private static readonly WebSocketProtocolComponent.HttpHeader[] s_ServerFakeRequestHeaders;

		// Token: 0x02000787 RID: 1927
		internal static class Errors
		{
			// Token: 0x04003334 RID: 13108
			internal const int E_INVALID_OPERATION = -2147483568;

			// Token: 0x04003335 RID: 13109
			internal const int E_INVALID_PROTOCOL_OPERATION = -2147483567;

			// Token: 0x04003336 RID: 13110
			internal const int E_INVALID_PROTOCOL_FORMAT = -2147483566;

			// Token: 0x04003337 RID: 13111
			internal const int E_NUMERIC_OVERFLOW = -2147483565;

			// Token: 0x04003338 RID: 13112
			internal const int E_FAIL = -2147467259;
		}

		// Token: 0x02000788 RID: 1928
		internal enum Action
		{
			// Token: 0x0400333A RID: 13114
			NoAction,
			// Token: 0x0400333B RID: 13115
			SendToNetwork,
			// Token: 0x0400333C RID: 13116
			IndicateSendComplete,
			// Token: 0x0400333D RID: 13117
			ReceiveFromNetwork,
			// Token: 0x0400333E RID: 13118
			IndicateReceiveComplete
		}

		// Token: 0x02000789 RID: 1929
		internal enum BufferType : uint
		{
			// Token: 0x04003340 RID: 13120
			None,
			// Token: 0x04003341 RID: 13121
			UTF8Message = 2147483648U,
			// Token: 0x04003342 RID: 13122
			UTF8Fragment,
			// Token: 0x04003343 RID: 13123
			BinaryMessage,
			// Token: 0x04003344 RID: 13124
			BinaryFragment,
			// Token: 0x04003345 RID: 13125
			Close,
			// Token: 0x04003346 RID: 13126
			PingPong,
			// Token: 0x04003347 RID: 13127
			UnsolicitedPong
		}

		// Token: 0x0200078A RID: 1930
		internal enum PropertyType
		{
			// Token: 0x04003349 RID: 13129
			ReceiveBufferSize,
			// Token: 0x0400334A RID: 13130
			SendBufferSize,
			// Token: 0x0400334B RID: 13131
			DisableMasking,
			// Token: 0x0400334C RID: 13132
			AllocatedBuffer,
			// Token: 0x0400334D RID: 13133
			DisableUtf8Verification,
			// Token: 0x0400334E RID: 13134
			KeepAliveInterval
		}

		// Token: 0x0200078B RID: 1931
		internal enum ActionQueue
		{
			// Token: 0x04003350 RID: 13136
			Send = 1,
			// Token: 0x04003351 RID: 13137
			Receive
		}

		// Token: 0x0200078C RID: 1932
		internal struct Property
		{
			// Token: 0x04003352 RID: 13138
			internal WebSocketProtocolComponent.PropertyType Type;

			// Token: 0x04003353 RID: 13139
			internal IntPtr PropertyData;

			// Token: 0x04003354 RID: 13140
			internal uint PropertySize;
		}

		// Token: 0x0200078D RID: 1933
		[StructLayout(LayoutKind.Explicit)]
		internal struct Buffer
		{
			// Token: 0x04003355 RID: 13141
			[FieldOffset(0)]
			internal WebSocketProtocolComponent.DataBuffer Data;

			// Token: 0x04003356 RID: 13142
			[FieldOffset(0)]
			internal WebSocketProtocolComponent.CloseBuffer CloseStatus;
		}

		// Token: 0x0200078E RID: 1934
		internal struct DataBuffer
		{
			// Token: 0x04003357 RID: 13143
			internal IntPtr BufferData;

			// Token: 0x04003358 RID: 13144
			internal uint BufferLength;
		}

		// Token: 0x0200078F RID: 1935
		internal struct CloseBuffer
		{
			// Token: 0x04003359 RID: 13145
			internal IntPtr ReasonData;

			// Token: 0x0400335A RID: 13146
			internal uint ReasonLength;

			// Token: 0x0400335B RID: 13147
			internal ushort CloseStatus;
		}

		// Token: 0x02000790 RID: 1936
		internal struct HttpHeader
		{
			// Token: 0x0400335C RID: 13148
			[MarshalAs(UnmanagedType.LPStr)]
			internal string Name;

			// Token: 0x0400335D RID: 13149
			internal uint NameLength;

			// Token: 0x0400335E RID: 13150
			[MarshalAs(UnmanagedType.LPStr)]
			internal string Value;

			// Token: 0x0400335F RID: 13151
			internal uint ValueLength;
		}
	}
}
