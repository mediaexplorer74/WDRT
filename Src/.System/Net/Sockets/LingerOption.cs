using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies whether a <see cref="T:System.Net.Sockets.Socket" /> will remain connected after a call to the <see cref="M:System.Net.Sockets.Socket.Close" /> or <see cref="M:System.Net.Sockets.TcpClient.Close" /> methods and the length of time it will remain connected, if data remains to be sent.</summary>
	// Token: 0x0200036D RID: 877
	public class LingerOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class.</summary>
		/// <param name="enable">
		///   <see langword="true" /> to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called; otherwise, <see langword="false" />.</param>
		/// <param name="seconds">The number of seconds to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called.</param>
		// Token: 0x06001FC3 RID: 8131 RVA: 0x00094C00 File Offset: 0x00092E00
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		/// <summary>Gets or sets a value that indicates whether to linger after the <see cref="T:System.Net.Sockets.Socket" /> is closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> should linger after <see cref="M:System.Net.Sockets.Socket.Close" /> is called; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00094C16 File Offset: 0x00092E16
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x00094C1E File Offset: 0x00092E1E
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the amount of time to remain connected after calling the <see cref="M:System.Net.Sockets.Socket.Close" /> method if data remains to be sent.</summary>
		/// <returns>The amount of time, in seconds, to remain connected after calling <see cref="M:System.Net.Sockets.Socket.Close" />.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00094C27 File Offset: 0x00092E27
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x00094C2F File Offset: 0x00092E2F
		public int LingerTime
		{
			get
			{
				return this.lingerTime;
			}
			set
			{
				this.lingerTime = value;
			}
		}

		// Token: 0x04001DCB RID: 7627
		private bool enabled;

		// Token: 0x04001DCC RID: 7628
		private int lingerTime;
	}
}
