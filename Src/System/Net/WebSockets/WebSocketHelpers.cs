using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000237 RID: 567
	internal static class WebSocketHelpers
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0006E9A5 File Offset: 0x0006CBA5
		internal static ArraySegment<byte> EmptyPayload
		{
			get
			{
				return WebSocketHelpers.s_EmptyPayload;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0006E9AC File Offset: 0x0006CBAC
		internal static Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(HttpListenerContext context, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			WebSocketHelpers.ValidateOptions(subProtocol, receiveBufferSize, 16, keepAliveInterval);
			WebSocketHelpers.ValidateArraySegment<byte>(internalBuffer, "internalBuffer");
			WebSocketBuffer.Validate(internalBuffer.Count, receiveBufferSize, 16, true);
			return WebSocketHelpers.AcceptWebSocketAsyncCore(context, subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0006E9E0 File Offset: 0x0006CBE0
		private static async Task<HttpListenerWebSocketContext> AcceptWebSocketAsyncCore(HttpListenerContext context, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			HttpListenerWebSocketContext httpListenerWebSocketContext = null;
			if (Logging.On)
			{
				Logging.Enter(Logging.WebSockets, context, "AcceptWebSocketAsync", "");
			}
			try
			{
				HttpListenerResponse response = context.Response;
				HttpListenerRequest request = context.Request;
				WebSocketHelpers.ValidateWebSocketHeaders(context);
				string secWebSocketVersion = request.Headers["Sec-WebSocket-Version"];
				string origin = request.Headers["Origin"];
				List<string> secWebSocketProtocols = new List<string>();
				string text;
				bool flag = WebSocketHelpers.ProcessWebSocketProtocolHeader(request.Headers["Sec-WebSocket-Protocol"], subProtocol, out text);
				if (flag)
				{
					secWebSocketProtocols.Add(text);
					response.Headers.Add("Sec-WebSocket-Protocol", text);
				}
				string secWebSocketKey = request.Headers["Sec-WebSocket-Key"];
				string secWebSocketAcceptString = WebSocketHelpers.GetSecWebSocketAcceptString(secWebSocketKey);
				response.Headers.Add("Connection", "Upgrade");
				response.Headers.Add("Upgrade", "websocket");
				response.Headers.Add("Sec-WebSocket-Accept", secWebSocketAcceptString);
				response.StatusCode = 101;
				response.ComputeCoreHeaders();
				ulong num = WebSocketHelpers.SendWebSocketHeaders(response);
				if (num != 0UL)
				{
					throw new WebSocketException((int)num, SR.GetString("net_WebSockets_NativeSendResponseHeaders", new object[] { "AcceptWebSocketAsync", num }));
				}
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.WebSockets, string.Format("{0} = {1}", "Origin", origin));
					Logging.PrintInfo(Logging.WebSockets, string.Format("{0} = {1}", "Sec-WebSocket-Version", secWebSocketVersion));
					Logging.PrintInfo(Logging.WebSockets, string.Format("{0} = {1}", "Sec-WebSocket-Key", secWebSocketKey));
					Logging.PrintInfo(Logging.WebSockets, string.Format("{0} = {1}", "Sec-WebSocket-Accept", secWebSocketAcceptString));
					Logging.PrintInfo(Logging.WebSockets, string.Format("Request  {0} = {1}", "Sec-WebSocket-Protocol", request.Headers["Sec-WebSocket-Protocol"]));
					Logging.PrintInfo(Logging.WebSockets, string.Format("Response {0} = {1}", "Sec-WebSocket-Protocol", text));
				}
				await response.OutputStream.FlushAsync().SuppressContextFlow();
				HttpResponseStream httpResponseStream = response.OutputStream as HttpResponseStream;
				((HttpResponseStream)response.OutputStream).SwitchToOpaqueMode();
				HttpRequestStream httpRequestStream = new HttpRequestStream(context);
				httpRequestStream.SwitchToOpaqueMode();
				WebSocketHttpListenerDuplexStream webSocketHttpListenerDuplexStream = new WebSocketHttpListenerDuplexStream(httpRequestStream, httpResponseStream, context);
				WebSocket webSocket = WebSocket.CreateServerWebSocket(webSocketHttpListenerDuplexStream, subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer);
				httpListenerWebSocketContext = new HttpListenerWebSocketContext(request.Url, request.Headers, request.Cookies, context.User, request.IsAuthenticated, request.IsLocal, request.IsSecureConnection, origin, secWebSocketProtocols.AsReadOnly(), secWebSocketVersion, secWebSocketKey, webSocket);
				if (Logging.On)
				{
					Logging.Associate(Logging.WebSockets, context, httpListenerWebSocketContext);
					Logging.Associate(Logging.WebSockets, httpListenerWebSocketContext, webSocket);
				}
				response = null;
				request = null;
				secWebSocketVersion = null;
				origin = null;
				secWebSocketProtocols = null;
				secWebSocketKey = null;
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.WebSockets, context, "AcceptWebSocketAsync", ex);
				}
				throw;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.WebSockets, context, "AcceptWebSocketAsync", "");
				}
			}
			return httpListenerWebSocketContext;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0006EA44 File Offset: 0x0006CC44
		internal static string GetSecWebSocketAcceptString(string secWebSocketKey)
		{
			string text2;
			using (SHA1 sha = SHA1.Create())
			{
				string text = secWebSocketKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				text2 = Convert.ToBase64String(sha.ComputeHash(bytes));
			}
			return text2;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0006EA9C File Offset: 0x0006CC9C
		internal static string GetTraceMsgForParameters(int offset, int count, CancellationToken cancellationToken)
		{
			return string.Format(CultureInfo.InvariantCulture, "offset: {0}, count: {1}, cancellationToken.CanBeCanceled: {2}", new object[] { offset, count, cancellationToken.CanBeCanceled });
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0006EAD4 File Offset: 0x0006CCD4
		internal static bool ProcessWebSocketProtocolHeader(string clientSecWebSocketProtocol, string subProtocol, out string acceptProtocol)
		{
			acceptProtocol = string.Empty;
			if (string.IsNullOrEmpty(clientSecWebSocketProtocol))
			{
				if (subProtocol != null)
				{
					throw new WebSocketException(WebSocketError.UnsupportedProtocol, SR.GetString("net_WebSockets_ClientAcceptingNoProtocols", new object[] { subProtocol }));
				}
				return false;
			}
			else
			{
				if (subProtocol == null)
				{
					return true;
				}
				string[] array = clientSecWebSocketProtocol.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				acceptProtocol = subProtocol;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim();
					if (string.Compare(acceptProtocol, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				throw new WebSocketException(WebSocketError.UnsupportedProtocol, SR.GetString("net_WebSockets_AcceptUnsupportedProtocol", new object[] { clientSecWebSocketProtocol, subProtocol }));
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0006EB6D File Offset: 0x0006CD6D
		internal static ConfiguredTaskAwaitable SuppressContextFlow(this Task task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0006EB76 File Offset: 0x0006CD76
		internal static ConfiguredTaskAwaitable<T> SuppressContextFlow<T>(this Task<T> task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0006EB7F File Offset: 0x0006CD7F
		internal static void ValidateBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0006EBBB File Offset: 0x0006CDBB
		private static ulong SendWebSocketHeaders(HttpListenerResponse response)
		{
			return (ulong)response.SendHeaders(null, null, UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA | UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_BUFFER_DATA | UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_OPAQUE, true);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0006EBCC File Offset: 0x0006CDCC
		private static void ValidateWebSocketHeaders(HttpListenerContext context)
		{
			WebSocketHelpers.EnsureHttpSysSupportsWebSockets();
			if (!context.Request.IsWebSocketRequest)
			{
				throw new WebSocketException(WebSocketError.NotAWebSocket, SR.GetString("net_WebSockets_AcceptNotAWebSocket", new object[]
				{
					"ValidateWebSocketHeaders",
					"Connection",
					"Upgrade",
					"websocket",
					context.Request.Headers["Upgrade"]
				}));
			}
			string text = context.Request.Headers["Sec-WebSocket-Version"];
			if (string.IsNullOrEmpty(text))
			{
				throw new WebSocketException(WebSocketError.HeaderError, SR.GetString("net_WebSockets_AcceptHeaderNotFound", new object[] { "ValidateWebSocketHeaders", "Sec-WebSocket-Version" }));
			}
			if (string.Compare(text, WebSocketProtocolComponent.SupportedVersion, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new WebSocketException(WebSocketError.UnsupportedVersion, SR.GetString("net_WebSockets_AcceptUnsupportedWebSocketVersion", new object[]
				{
					"ValidateWebSocketHeaders",
					text,
					WebSocketProtocolComponent.SupportedVersion
				}));
			}
			if (string.IsNullOrWhiteSpace(context.Request.Headers["Sec-WebSocket-Key"]))
			{
				throw new WebSocketException(WebSocketError.HeaderError, SR.GetString("net_WebSockets_AcceptHeaderNotFound", new object[] { "ValidateWebSocketHeaders", "Sec-WebSocket-Key" }));
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0006ECFC File Offset: 0x0006CEFC
		internal static void PrepareWebRequest(ref HttpWebRequest request)
		{
			request.Connection = "Upgrade";
			request.Headers["Upgrade"] = "websocket";
			byte[] array = new byte[16];
			Random random = WebSocketHelpers.s_KeyGenerator;
			lock (random)
			{
				WebSocketHelpers.s_KeyGenerator.NextBytes(array);
			}
			request.Headers["Sec-WebSocket-Key"] = Convert.ToBase64String(array);
			if (WebSocketProtocolComponent.IsSupported)
			{
				request.Headers["Sec-WebSocket-Version"] = WebSocketProtocolComponent.SupportedVersion;
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0006EDA0 File Offset: 0x0006CFA0
		internal static void ValidateSubprotocol(string subProtocol)
		{
			if (string.IsNullOrWhiteSpace(subProtocol))
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidEmptySubProtocol"), "subProtocol");
			}
			char[] array = subProtocol.ToCharArray();
			string text = null;
			foreach (char c in array)
			{
				if (c < '!' || c > '~')
				{
					text = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[] { (int)c });
					break;
				}
				if (!char.IsLetterOrDigit(c) && "()<>@,;:\\\"/[]?={} ".IndexOf(c) >= 0)
				{
					text = c.ToString();
					break;
				}
			}
			if (text != null)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidCharInProtocolString", new object[] { subProtocol, text }), "subProtocol");
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0006EE58 File Offset: 0x0006D058
		internal static void ValidateCloseStatus(WebSocketCloseStatus closeStatus, string statusDescription)
		{
			if (closeStatus == WebSocketCloseStatus.Empty && !string.IsNullOrEmpty(statusDescription))
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_ReasonNotNull", new object[]
				{
					statusDescription,
					WebSocketCloseStatus.Empty
				}), "statusDescription");
			}
			if ((closeStatus >= (WebSocketCloseStatus)0 && closeStatus <= (WebSocketCloseStatus)999) || closeStatus == (WebSocketCloseStatus)1006 || closeStatus == (WebSocketCloseStatus)1015)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidCloseStatusCode", new object[] { (int)closeStatus }), "closeStatus");
			}
			int num = 0;
			if (!string.IsNullOrEmpty(statusDescription))
			{
				num = Encoding.UTF8.GetByteCount(statusDescription);
			}
			if (num > 123)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidCloseStatusDescription", new object[] { statusDescription, 123 }), "statusDescription");
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0006EF28 File Offset: 0x0006D128
		internal static void ValidateOptions(string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval)
		{
			if (subProtocol != null)
			{
				WebSocketHelpers.ValidateSubprotocol(subProtocol);
			}
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			if (keepAliveInterval < Timeout.InfiniteTimeSpan)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[] { Timeout.InfiniteTimeSpan.ToString() }));
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0006EF8C File Offset: 0x0006D18C
		internal static void ValidateBufferSizes(int receiveBufferSize, int sendBufferSize)
		{
			if (receiveBufferSize < 256)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[] { 256 }));
			}
			if (sendBufferSize < 16)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[] { 16 }));
			}
			if (receiveBufferSize > 65536)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooBig", new object[] { "receiveBufferSize", receiveBufferSize, 65536 }));
			}
			if (sendBufferSize > 65536)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooBig", new object[] { "sendBufferSize", sendBufferSize, 65536 }));
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0006F090 File Offset: 0x0006D290
		internal static void ValidateInnerStream(Stream innerStream)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead)
			{
				throw new ArgumentException(SR.GetString("NotReadableStream"), "innerStream");
			}
			if (!innerStream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("NotWriteableStream"), "innerStream");
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0006F0E5 File Offset: 0x0006D2E5
		internal static void ThrowIfConnectionAborted(Stream connection, bool read)
		{
			if ((!read && !connection.CanWrite) || (read && !connection.CanRead))
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely);
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0006F104 File Offset: 0x0006D304
		internal static void ThrowPlatformNotSupportedException_WSPC()
		{
			throw new PlatformNotSupportedException(SR.GetString("net_WebSockets_UnsupportedPlatform"));
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0006F115 File Offset: 0x0006D315
		private static void ThrowPlatformNotSupportedException_HTTPSYS()
		{
			throw new PlatformNotSupportedException(SR.GetString("net_WebSockets_UnsupportedPlatform"));
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0006F128 File Offset: 0x0006D328
		internal static void ValidateArraySegment<T>(ArraySegment<T> arraySegment, string parameterName)
		{
			if (arraySegment.Array == null)
			{
				throw new ArgumentNullException(parameterName + ".Array");
			}
			if (arraySegment.Offset < 0 || arraySegment.Offset > arraySegment.Array.Length)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Offset");
			}
			if (arraySegment.Count < 0 || arraySegment.Count > arraySegment.Array.Length - arraySegment.Offset)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Count");
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0006F1B1 File Offset: 0x0006D3B1
		private static void EnsureHttpSysSupportsWebSockets()
		{
			if (!WebSocketHelpers.s_HttpSysSupportsWebSockets)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_HTTPSYS();
			}
		}

		// Token: 0x04001695 RID: 5781
		internal const string SecWebSocketKeyGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		// Token: 0x04001696 RID: 5782
		internal const string WebSocketUpgradeToken = "websocket";

		// Token: 0x04001697 RID: 5783
		internal const int DefaultReceiveBufferSize = 16384;

		// Token: 0x04001698 RID: 5784
		internal const int DefaultClientSendBufferSize = 16384;

		// Token: 0x04001699 RID: 5785
		internal const int MaxControlFramePayloadLength = 123;

		// Token: 0x0400169A RID: 5786
		internal const int ClientTcpCloseTimeout = 1000;

		// Token: 0x0400169B RID: 5787
		private const int CloseStatusCodeAbort = 1006;

		// Token: 0x0400169C RID: 5788
		private const int CloseStatusCodeFailedTLSHandshake = 1015;

		// Token: 0x0400169D RID: 5789
		private const int InvalidCloseStatusCodesFrom = 0;

		// Token: 0x0400169E RID: 5790
		private const int InvalidCloseStatusCodesTo = 999;

		// Token: 0x0400169F RID: 5791
		private const string Separators = "()<>@,;:\\\"/[]?={} ";

		// Token: 0x040016A0 RID: 5792
		private static readonly ArraySegment<byte> s_EmptyPayload = new ArraySegment<byte>(new byte[0], 0, 0);

		// Token: 0x040016A1 RID: 5793
		private static readonly Random s_KeyGenerator = new Random();

		// Token: 0x040016A2 RID: 5794
		private static volatile bool s_HttpSysSupportsWebSockets = ComNetOS.IsWin8orLater;

		// Token: 0x0200077F RID: 1919
		internal static class MethodNames
		{
			// Token: 0x040032E3 RID: 13027
			internal const string AcceptWebSocketAsync = "AcceptWebSocketAsync";

			// Token: 0x040032E4 RID: 13028
			internal const string ValidateWebSocketHeaders = "ValidateWebSocketHeaders";
		}
	}
}
