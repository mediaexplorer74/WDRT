using System;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>Encapsulates the information that is necessary to duplicate a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x02000375 RID: 885
	[Serializable]
	public struct SocketInformation
	{
		/// <summary>Gets or sets the protocol information for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" />.</returns>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00095DA6 File Offset: 0x00093FA6
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x00095DAE File Offset: 0x00093FAE
		public byte[] ProtocolInformation
		{
			get
			{
				return this.protocolInformation;
			}
			set
			{
				this.protocolInformation = value;
			}
		}

		/// <summary>Gets or sets the options for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketInformationOptions" /> instance.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00095DB7 File Offset: 0x00093FB7
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x00095DBF File Offset: 0x00093FBF
		public SocketInformationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x00095DC8 File Offset: 0x00093FC8
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x00095DD5 File Offset: 0x00093FD5
		internal bool IsNonBlocking
		{
			get
			{
				return (this.options & SocketInformationOptions.NonBlocking) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.NonBlocking;
					return;
				}
				this.options &= ~SocketInformationOptions.NonBlocking;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x00095DF8 File Offset: 0x00093FF8
		// (set) Token: 0x06002010 RID: 8208 RVA: 0x00095E05 File Offset: 0x00094005
		internal bool IsConnected
		{
			get
			{
				return (this.options & SocketInformationOptions.Connected) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Connected;
					return;
				}
				this.options &= ~SocketInformationOptions.Connected;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x00095E28 File Offset: 0x00094028
		// (set) Token: 0x06002012 RID: 8210 RVA: 0x00095E35 File Offset: 0x00094035
		internal bool IsListening
		{
			get
			{
				return (this.options & SocketInformationOptions.Listening) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Listening;
					return;
				}
				this.options &= ~SocketInformationOptions.Listening;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x00095E58 File Offset: 0x00094058
		// (set) Token: 0x06002014 RID: 8212 RVA: 0x00095E65 File Offset: 0x00094065
		internal bool UseOnlyOverlappedIO
		{
			get
			{
				return (this.options & SocketInformationOptions.UseOnlyOverlappedIO) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.UseOnlyOverlappedIO;
					return;
				}
				this.options &= ~SocketInformationOptions.UseOnlyOverlappedIO;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00095E88 File Offset: 0x00094088
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x00095E90 File Offset: 0x00094090
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
			set
			{
				this.remoteEndPoint = value;
			}
		}

		// Token: 0x04001E1D RID: 7709
		private byte[] protocolInformation;

		// Token: 0x04001E1E RID: 7710
		private SocketInformationOptions options;

		// Token: 0x04001E1F RID: 7711
		[OptionalField]
		private EndPoint remoteEndPoint;
	}
}
