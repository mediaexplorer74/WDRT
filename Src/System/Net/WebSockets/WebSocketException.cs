using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.WebSockets
{
	/// <summary>Represents an exception that occurred when performing an operation on a WebSocket connection.</summary>
	// Token: 0x02000236 RID: 566
	[Serializable]
	public sealed class WebSocketException : Win32Exception
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		// Token: 0x06001528 RID: 5416 RVA: 0x0006E749 File Offset: 0x0006C949
		public WebSocketException()
			: this(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		// Token: 0x06001529 RID: 5417 RVA: 0x0006E756 File Offset: 0x0006C956
		public WebSocketException(WebSocketError error)
			: this(error, WebSocketException.GetErrorMessage(error))
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x0600152A RID: 5418 RVA: 0x0006E765 File Offset: 0x0006C965
		public WebSocketException(WebSocketError error, string message)
			: base(message)
		{
			this.m_WebSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x0600152B RID: 5419 RVA: 0x0006E775 File Offset: 0x0006C975
		public WebSocketException(WebSocketError error, Exception innerException)
			: this(error, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x0600152C RID: 5420 RVA: 0x0006E785 File Offset: 0x0006C985
		public WebSocketException(WebSocketError error, string message, Exception innerException)
			: base(message, innerException)
		{
			this.m_WebSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		// Token: 0x0600152D RID: 5421 RVA: 0x0006E796 File Offset: 0x0006C996
		public WebSocketException(int nativeError)
			: base(nativeError)
		{
			this.m_WebSocketErrorCode = ((!WebSocketProtocolComponent.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x0600152E RID: 5422 RVA: 0x0006E7B8 File Offset: 0x0006C9B8
		public WebSocketException(int nativeError, string message)
			: base(nativeError, message)
		{
			this.m_WebSocketErrorCode = ((!WebSocketProtocolComponent.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x0600152F RID: 5423 RVA: 0x0006E7DB File Offset: 0x0006C9DB
		public WebSocketException(int nativeError, Exception innerException)
			: base(SR.GetString("net_WebSockets_Generic"), innerException)
		{
			this.m_WebSocketErrorCode = ((!WebSocketProtocolComponent.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		// Token: 0x06001530 RID: 5424 RVA: 0x0006E807 File Offset: 0x0006CA07
		public WebSocketException(WebSocketError error, int nativeError)
			: this(error, nativeError, WebSocketException.GetErrorMessage(error))
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x06001531 RID: 5425 RVA: 0x0006E817 File Offset: 0x0006CA17
		public WebSocketException(WebSocketError error, int nativeError, string message)
			: base(message)
		{
			this.m_WebSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x06001532 RID: 5426 RVA: 0x0006E82E File Offset: 0x0006CA2E
		public WebSocketException(WebSocketError error, int nativeError, Exception innerException)
			: this(error, nativeError, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x06001533 RID: 5427 RVA: 0x0006E83F File Offset: 0x0006CA3F
		public WebSocketException(WebSocketError error, int nativeError, string message, Exception innerException)
			: base(message, innerException)
		{
			this.m_WebSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		// Token: 0x06001534 RID: 5428 RVA: 0x0006E858 File Offset: 0x0006CA58
		public WebSocketException(string message)
			: base(message)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x06001535 RID: 5429 RVA: 0x0006E861 File Offset: 0x0006CA61
		public WebSocketException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0006E86B File Offset: 0x0006CA6B
		private WebSocketException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>The native error code for the exception that occurred.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0006E875 File Offset: 0x0006CA75
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Returns a WebSocketError indicating the type of error that occurred.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketError" />.</returns>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0006E87D File Offset: 0x0006CA7D
		public WebSocketError WebSocketErrorCode
		{
			get
			{
				return this.m_WebSocketErrorCode;
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0006E888 File Offset: 0x0006CA88
		private static string GetErrorMessage(WebSocketError error)
		{
			switch (error)
			{
			case WebSocketError.InvalidMessageType:
				return SR.GetString("net_WebSockets_InvalidMessageType_Generic", new object[]
				{
					typeof(WebSocket).Name + "CloseAsync",
					typeof(WebSocket).Name + "CloseOutputAsync"
				});
			case WebSocketError.Faulted:
				return SR.GetString("net_Websockets_WebSocketBaseFaulted");
			case WebSocketError.NotAWebSocket:
				return SR.GetString("net_WebSockets_NotAWebSocket_Generic");
			case WebSocketError.UnsupportedVersion:
				return SR.GetString("net_WebSockets_UnsupportedWebSocketVersion_Generic");
			case WebSocketError.UnsupportedProtocol:
				return SR.GetString("net_WebSockets_UnsupportedProtocol_Generic");
			case WebSocketError.HeaderError:
				return SR.GetString("net_WebSockets_HeaderError_Generic");
			case WebSocketError.ConnectionClosedPrematurely:
				return SR.GetString("net_WebSockets_ConnectionClosedPrematurely_Generic");
			case WebSocketError.InvalidState:
				return SR.GetString("net_WebSockets_InvalidState_Generic");
			}
			return SR.GetString("net_WebSockets_Generic");
		}

		/// <summary>Sets the SerializationInfo object with the file name and line number where the exception occurred.</summary>
		/// <param name="info">A SerializationInfo object.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600153A RID: 5434 RVA: 0x0006E966 File Offset: 0x0006CB66
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("WebSocketErrorCode", this.m_WebSocketErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0006E994 File Offset: 0x0006CB94
		private void SetErrorCodeOnError(int nativeError)
		{
			if (!WebSocketProtocolComponent.Succeeded(nativeError))
			{
				base.HResult = nativeError;
			}
		}

		// Token: 0x04001694 RID: 5780
		private WebSocketError m_WebSocketErrorCode;
	}
}
