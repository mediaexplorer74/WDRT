using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000177 RID: 375
	internal abstract class ODataJsonOutputContextBase : ODataOutputContext
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002375D File Offset: 0x0002195D
		protected internal ODataJsonOutputContextBase(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, messageWriterSettings, false, true, model, null)
		{
			this.textWriter = textWriter;
			this.jsonWriter = new JsonWriter(this.textWriter, messageWriterSettings.Indent, format);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002378C File Offset: 0x0002198C
		protected internal ODataJsonOutputContextBase(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				Stream stream;
				if (synchronous)
				{
					stream = messageStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(messageStream);
					stream = this.asynchronousOutputStream;
				}
				this.textWriter = new StreamWriter(stream, encoding);
				this.jsonWriter = new JsonWriter(this.textWriter, messageWriterSettings.Indent, format);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00023818 File Offset: 0x00021A18
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonWriter;
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00023820 File Offset: 0x00021A20
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002383B File Offset: 0x00021A3B
		internal void Flush()
		{
			this.jsonWriter.Flush();
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002386D File Offset: 0x00021A6D
		internal Task FlushAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.jsonWriter.Flush();
				return this.asynchronousOutputStream.FlushAsync();
			}).FollowOnSuccessWithTask((Task asyncBufferedStreamFlushTask) => this.messageOutputStream.FlushAsync());
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00023894 File Offset: 0x00021A94
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			try
			{
				if (this.messageOutputStream != null)
				{
					this.jsonWriter.Flush();
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
				this.textWriter = null;
				this.jsonWriter = null;
			}
		}

		// Token: 0x040003F4 RID: 1012
		protected IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x040003F5 RID: 1013
		private Stream messageOutputStream;

		// Token: 0x040003F6 RID: 1014
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x040003F7 RID: 1015
		private TextWriter textWriter;

		// Token: 0x040003F8 RID: 1016
		private IJsonWriter jsonWriter;
	}
}
