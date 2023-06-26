using System;
using System.Text;

namespace System.IO
{
	// Token: 0x0200019F RID: 415
	internal class ReadLinesIterator : Iterator<string>
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x00054977 File Offset: 0x00052B77
		private ReadLinesIterator(string path, Encoding encoding, StreamReader reader)
		{
			this._path = path;
			this._encoding = encoding;
			this._reader = reader;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00054994 File Offset: 0x00052B94
		public override bool MoveNext()
		{
			if (this._reader != null)
			{
				this.current = this._reader.ReadLine();
				if (this.current != null)
				{
					return true;
				}
				base.Dispose();
			}
			return false;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000549C0 File Offset: 0x00052BC0
		protected override Iterator<string> Clone()
		{
			return ReadLinesIterator.CreateIterator(this._path, this._encoding, this._reader);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000549DC File Offset: 0x00052BDC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._reader != null)
				{
					this._reader.Dispose();
				}
			}
			finally
			{
				this._reader = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00054A20 File Offset: 0x00052C20
		internal static ReadLinesIterator CreateIterator(string path, Encoding encoding)
		{
			return ReadLinesIterator.CreateIterator(path, encoding, null);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00054A2A File Offset: 0x00052C2A
		private static ReadLinesIterator CreateIterator(string path, Encoding encoding, StreamReader reader)
		{
			return new ReadLinesIterator(path, encoding, reader ?? new StreamReader(path, encoding));
		}

		// Token: 0x040008EC RID: 2284
		private readonly string _path;

		// Token: 0x040008ED RID: 2285
		private readonly Encoding _encoding;

		// Token: 0x040008EE RID: 2286
		private StreamReader _reader;
	}
}
