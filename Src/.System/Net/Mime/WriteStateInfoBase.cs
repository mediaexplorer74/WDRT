using System;

namespace System.Net.Mime
{
	// Token: 0x02000253 RID: 595
	internal class WriteStateInfoBase
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x00074FCC File Offset: 0x000731CC
		internal WriteStateInfoBase()
		{
			this.buffer = new byte[1024];
			this._header = new byte[0];
			this._footer = new byte[0];
			this._maxLineLength = EncodedStreamFactory.DefaultMaxLineLength;
			this._currentLineLength = 0;
			this._currentBufferUsed = 0;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00075020 File Offset: 0x00073220
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength)
			: this(bufferSize, header, footer, maxLineLength, 0)
		{
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0007502E File Offset: 0x0007322E
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
		{
			this.buffer = new byte[bufferSize];
			this._header = header;
			this._footer = footer;
			this._maxLineLength = maxLineLength;
			this._currentLineLength = mimeHeaderLength;
			this._currentBufferUsed = 0;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00075067 File Offset: 0x00073267
		internal int FooterLength
		{
			get
			{
				return this._footer.Length;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00075071 File Offset: 0x00073271
		internal byte[] Footer
		{
			get
			{
				return this._footer;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00075079 File Offset: 0x00073279
		internal byte[] Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00075081 File Offset: 0x00073281
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00075089 File Offset: 0x00073289
		internal int Length
		{
			get
			{
				return this._currentBufferUsed;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00075091 File Offset: 0x00073291
		internal int CurrentLineLength
		{
			get
			{
				return this._currentLineLength;
			}
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0007509C File Offset: 0x0007329C
		private void EnsureSpaceInBuffer(int moreBytes)
		{
			int num = this.Buffer.Length;
			while (this._currentBufferUsed + moreBytes >= num)
			{
				num *= 2;
			}
			if (num > this.Buffer.Length)
			{
				byte[] array = new byte[num];
				this.buffer.CopyTo(array, 0);
				this.buffer = array;
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000750EC File Offset: 0x000732EC
		internal void Append(byte aByte)
		{
			this.EnsureSpaceInBuffer(1);
			byte[] array = this.Buffer;
			int currentBufferUsed = this._currentBufferUsed;
			this._currentBufferUsed = currentBufferUsed + 1;
			array[currentBufferUsed] = aByte;
			this._currentLineLength++;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00075127 File Offset: 0x00073327
		internal void Append(params byte[] bytes)
		{
			this.EnsureSpaceInBuffer(bytes.Length);
			bytes.CopyTo(this.buffer, this.Length);
			this._currentLineLength += bytes.Length;
			this._currentBufferUsed += bytes.Length;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00075164 File Offset: 0x00073364
		internal void AppendCRLF(bool includeSpace)
		{
			this.AppendFooter();
			this.Append(new byte[] { 13, 10 });
			this._currentLineLength = 0;
			if (includeSpace)
			{
				this.Append(32);
			}
			this.AppendHeader();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0007519A File Offset: 0x0007339A
		internal void AppendHeader()
		{
			if (this.Header != null && this.Header.Length != 0)
			{
				this.Append(this.Header);
			}
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000751B9 File Offset: 0x000733B9
		internal void AppendFooter()
		{
			if (this.Footer != null && this.Footer.Length != 0)
			{
				this.Append(this.Footer);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x000751D8 File Offset: 0x000733D8
		internal int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000751E0 File Offset: 0x000733E0
		internal void Reset()
		{
			this._currentBufferUsed = 0;
			this._currentLineLength = 0;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000751F0 File Offset: 0x000733F0
		internal void BufferFlushed()
		{
			this._currentBufferUsed = 0;
		}

		// Token: 0x0400174F RID: 5967
		protected byte[] _header;

		// Token: 0x04001750 RID: 5968
		protected byte[] _footer;

		// Token: 0x04001751 RID: 5969
		protected int _maxLineLength;

		// Token: 0x04001752 RID: 5970
		protected byte[] buffer;

		// Token: 0x04001753 RID: 5971
		protected int _currentLineLength;

		// Token: 0x04001754 RID: 5972
		protected int _currentBufferUsed;

		// Token: 0x04001755 RID: 5973
		protected const int defaultBufferSize = 1024;
	}
}
