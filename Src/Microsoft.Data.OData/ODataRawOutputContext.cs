using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001CC RID: 460
	internal sealed class ODataRawOutputContext : ODataOutputContext
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x000327D8 File Offset: 0x000309D8
		internal ODataRawOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				this.encoding = encoding;
				if (synchronous)
				{
					this.outputStream = messageStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(messageStream);
					this.outputStream = this.asynchronousOutputStream;
				}
			}
			catch
			{
				messageStream.Dispose();
				throw;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00032844 File Offset: 0x00030A44
		internal Stream OutputStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0003284C File Offset: 0x00030A4C
		internal TextWriter TextWriter
		{
			get
			{
				return this.rawValueWriter.TextWriter;
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00032859 File Offset: 0x00030A59
		internal void Flush()
		{
			if (this.rawValueWriter != null)
			{
				this.rawValueWriter.Flush();
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003289B File Offset: 0x00030A9B
		internal Task FlushAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				if (this.rawValueWriter != null)
				{
					this.rawValueWriter.Flush();
				}
				return this.asynchronousOutputStream.FlushAsync();
			}).FollowOnSuccessWithTask((Task asyncBufferedStreamFlushTask) => this.messageOutputStream.FlushAsync());
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000328BF File Offset: 0x00030ABF
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteInStreamErrorForRawValues);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000328DE File Offset: 0x00030ADE
		internal override Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteInStreamErrorForRawValues);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000328FD File Offset: 0x00030AFD
		internal override ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			return this.CreateODataBatchWriterImplementation(batchBoundary);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00032924 File Offset: 0x00030B24
		internal override Task<ODataBatchWriter> CreateODataBatchWriterAsync(string batchBoundary)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchWriter>(() => this.CreateODataBatchWriterImplementation(batchBoundary));
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00032956 File Offset: 0x00030B56
		internal override void WriteValue(object value)
		{
			this.WriteValueImplementation(value);
			this.Flush();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003298C File Offset: 0x00030B8C
		internal override Task WriteValueAsync(object value)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteValueImplementation(value);
				return this.FlushAsync();
			});
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000329BE File Offset: 0x00030BBE
		internal void InitializeRawValueWriter()
		{
			this.rawValueWriter = new RawValueWriter(base.MessageWriterSettings, this.outputStream, this.encoding);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000329DD File Offset: 0x00030BDD
		internal void CloseWriter()
		{
			this.rawValueWriter.Dispose();
			this.rawValueWriter = null;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000329F1 File Offset: 0x00030BF1
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00032A0C File Offset: 0x00030C0C
		internal void FlushBuffers()
		{
			if (this.asynchronousOutputStream != null)
			{
				this.asynchronousOutputStream.FlushSync();
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00032A21 File Offset: 0x00030C21
		internal Task FlushBuffersAsync()
		{
			if (this.asynchronousOutputStream != null)
			{
				return this.asynchronousOutputStream.FlushAsync();
			}
			return TaskUtils.CompletedTask;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00032A3C File Offset: 0x00030C3C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			try
			{
				if (this.messageOutputStream != null)
				{
					if (this.rawValueWriter != null)
					{
						this.rawValueWriter.Flush();
					}
					if (this.asynchronousOutputStream != null)
					{
						this.asynchronousOutputStream.FlushSync();
						this.asynchronousOutputStream.Dispose();
					}
					this.messageOutputStream.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.asynchronousOutputStream = null;
				this.outputStream = null;
				this.rawValueWriter = null;
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00032AC4 File Offset: 0x00030CC4
		private void WriteValueImplementation(object value)
		{
			byte[] array = value as byte[];
			if (array != null)
			{
				this.OutputStream.Write(array, 0, array.Length);
				return;
			}
			this.InitializeRawValueWriter();
			this.rawValueWriter.Start();
			this.rawValueWriter.WriteRawValue(value);
			this.rawValueWriter.End();
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00032B14 File Offset: 0x00030D14
		private ODataBatchWriter CreateODataBatchWriterImplementation(string batchBoundary)
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataBatchWriter odataBatchWriter = new ODataBatchWriter(this, batchBoundary);
			this.outputInStreamErrorListener = odataBatchWriter;
			return odataBatchWriter;
		}

		// Token: 0x040004B5 RID: 1205
		private Encoding encoding;

		// Token: 0x040004B6 RID: 1206
		private Stream messageOutputStream;

		// Token: 0x040004B7 RID: 1207
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x040004B8 RID: 1208
		private Stream outputStream;

		// Token: 0x040004B9 RID: 1209
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x040004BA RID: 1210
		private RawValueWriter rawValueWriter;
	}
}
