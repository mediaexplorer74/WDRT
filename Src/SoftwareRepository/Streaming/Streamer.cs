using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000018 RID: 24
	public abstract class Streamer : IDisposable
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003D8C File Offset: 0x00001F8C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetStreamInternal", Justification = "Spelled correctly.")]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Can throw.")]
		public Stream GetStream()
		{
			bool flag = this.Stream != null;
			Stream stream;
			if (flag)
			{
				stream = this.Stream;
			}
			else
			{
				Stream streamInternal = this.GetStreamInternal();
				bool flag2 = streamInternal == null;
				if (flag2)
				{
					throw new InvalidOperationException("GetStreamInternal() returned null");
				}
				bool flag3 = !streamInternal.CanWrite;
				if (flag3)
				{
					throw new InvalidOperationException("Stream returned by GetStreamInternal() must support writing");
				}
				bool flag4 = !streamInternal.CanSeek;
				if (flag4)
				{
					throw new InvalidOperationException("Stream returned by GetStreamInternal() must support seeking");
				}
				stream = (this.Stream = streamInternal);
			}
			return stream;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003E12 File Offset: 0x00002012
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003E24 File Offset: 0x00002024
		public virtual void SetMetadata(byte[] metadata)
		{
			this.Metadata = metadata;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003E30 File Offset: 0x00002030
		public virtual byte[] GetMetadata()
		{
			return this.Metadata;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003E48 File Offset: 0x00002048
		public virtual void ClearMetadata()
		{
			this.Metadata = null;
		}

		// Token: 0x060000A7 RID: 167
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Implementors can employ arbitrarily complex logic.")]
		protected abstract Stream GetStreamInternal();

		// Token: 0x060000A8 RID: 168 RVA: 0x00003E54 File Offset: 0x00002054
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				bool flag = this.Stream != null;
				if (flag)
				{
					this.Stream.Dispose();
					this.Stream = null;
				}
			}
		}

		// Token: 0x04000066 RID: 102
		private Stream Stream = null;

		// Token: 0x04000067 RID: 103
		private byte[] Metadata = null;

		// Token: 0x04000068 RID: 104
		public string DownloadPath = null;
	}
}
