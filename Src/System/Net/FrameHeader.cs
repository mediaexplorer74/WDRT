using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200021B RID: 539
	internal class FrameHeader
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x00068FBC File Offset: 0x000671BC
		public FrameHeader()
		{
			this._MessageId = 22;
			this._MajorV = 1;
			this._MinorV = 0;
			this._PayloadSize = -1;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00068FE1 File Offset: 0x000671E1
		public FrameHeader(int messageId, int majorV, int minorV)
		{
			this._MessageId = messageId;
			this._MajorV = majorV;
			this._MinorV = minorV;
			this._PayloadSize = -1;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00069005 File Offset: 0x00067205
		public int Size
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00069008 File Offset: 0x00067208
		public int MaxMessageSize
		{
			get
			{
				return 65535;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0006900F File Offset: 0x0006720F
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x00069017 File Offset: 0x00067217
		public int MessageId
		{
			get
			{
				return this._MessageId;
			}
			set
			{
				this._MessageId = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00069020 File Offset: 0x00067220
		public int MajorV
		{
			get
			{
				return this._MajorV;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00069028 File Offset: 0x00067228
		public int MinorV
		{
			get
			{
				return this._MinorV;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00069030 File Offset: 0x00067230
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x00069038 File Offset: 0x00067238
		public int PayloadSize
		{
			get
			{
				return this._PayloadSize;
			}
			set
			{
				if (value > this.MaxMessageSize)
				{
					throw new ArgumentException(SR.GetString("net_frame_max_size", new object[]
					{
						this.MaxMessageSize.ToString(NumberFormatInfo.InvariantInfo),
						value.ToString(NumberFormatInfo.InvariantInfo)
					}), "PayloadSize");
				}
				this._PayloadSize = value;
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00069098 File Offset: 0x00067298
		public void CopyTo(byte[] dest, int start)
		{
			dest[start++] = (byte)this._MessageId;
			dest[start++] = (byte)this._MajorV;
			dest[start++] = (byte)this._MinorV;
			dest[start++] = (byte)((this._PayloadSize >> 8) & 255);
			dest[start] = (byte)(this._PayloadSize & 255);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000690FC File Offset: 0x000672FC
		public void CopyFrom(byte[] bytes, int start, FrameHeader verifier)
		{
			this._MessageId = (int)bytes[start++];
			this._MajorV = (int)bytes[start++];
			this._MinorV = (int)bytes[start++];
			this._PayloadSize = ((int)bytes[start++] << 8) | (int)bytes[start];
			if (verifier.MessageId != -1 && this.MessageId != verifier.MessageId)
			{
				throw new InvalidOperationException(SR.GetString("net_io_header_id", new object[] { "MessageId", this.MessageId, verifier.MessageId }));
			}
			if (verifier.MajorV != -1 && this.MajorV != verifier.MajorV)
			{
				throw new InvalidOperationException(SR.GetString("net_io_header_id", new object[] { "MajorV", this.MajorV, verifier.MajorV }));
			}
			if (verifier.MinorV != -1 && this.MinorV != verifier.MinorV)
			{
				throw new InvalidOperationException(SR.GetString("net_io_header_id", new object[] { "MinorV", this.MinorV, verifier.MinorV }));
			}
		}

		// Token: 0x040015CC RID: 5580
		public const int IgnoreValue = -1;

		// Token: 0x040015CD RID: 5581
		public const int HandshakeDoneId = 20;

		// Token: 0x040015CE RID: 5582
		public const int HandshakeErrId = 21;

		// Token: 0x040015CF RID: 5583
		public const int HandshakeId = 22;

		// Token: 0x040015D0 RID: 5584
		public const int DefaultMajorV = 1;

		// Token: 0x040015D1 RID: 5585
		public const int DefaultMinorV = 0;

		// Token: 0x040015D2 RID: 5586
		private int _MessageId;

		// Token: 0x040015D3 RID: 5587
		private int _MajorV;

		// Token: 0x040015D4 RID: 5588
		private int _MinorV;

		// Token: 0x040015D5 RID: 5589
		private int _PayloadSize;
	}
}
