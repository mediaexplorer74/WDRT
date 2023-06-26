using System;

namespace System.Net.WebSockets
{
	/// <summary>An instance of this class represents the result of performing a single ReceiveAsync operation on a WebSocket.</summary>
	// Token: 0x0200023B RID: 571
	public class WebSocketReceiveResult
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		// Token: 0x0600159C RID: 5532 RVA: 0x00070588 File Offset: 0x0006E788
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
			: this(count, messageType, endOfMessage, null, null)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		/// <param name="closeStatus">Indicates the <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" /> of the connection.</param>
		/// <param name="closeStatusDescription">The description of <paramref name="closeStatus" />.</param>
		// Token: 0x0600159D RID: 5533 RVA: 0x000705A8 File Offset: 0x0006E7A8
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeStatusDescription)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Count = count;
			this.EndOfMessage = endOfMessage;
			this.MessageType = messageType;
			this.CloseStatus = closeStatus;
			this.CloseStatusDescription = closeStatusDescription;
		}

		/// <summary>Indicates the number of bytes that the WebSocket received.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x000705E4 File Offset: 0x0006E7E4
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x000705EC File Offset: 0x0006E7EC
		public int Count { get; private set; }

		/// <summary>Indicates whether the message has been received completely.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000705F5 File Offset: 0x0006E7F5
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x000705FD File Offset: 0x0006E7FD
		public bool EndOfMessage { get; private set; }

		/// <summary>Indicates whether the current message is a UTF-8 message or a binary message.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketMessageType" />.</returns>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00070606 File Offset: 0x0006E806
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x0007060E File Offset: 0x0006E80E
		public WebSocketMessageType MessageType { get; private set; }

		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00070617 File Offset: 0x0006E817
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x0007061F File Offset: 0x0006E81F
		public WebSocketCloseStatus? CloseStatus { get; private set; }

		/// <summary>Returns the optional description that describes why the close handshake has been initiated by the remote endpoint.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00070628 File Offset: 0x0006E828
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x00070630 File Offset: 0x0006E830
		public string CloseStatusDescription { get; private set; }

		// Token: 0x060015A8 RID: 5544 RVA: 0x00070639 File Offset: 0x0006E839
		internal WebSocketReceiveResult Copy(int count)
		{
			this.Count -= count;
			return new WebSocketReceiveResult(count, this.MessageType, this.Count == 0 && this.EndOfMessage, this.CloseStatus, this.CloseStatusDescription);
		}
	}
}
