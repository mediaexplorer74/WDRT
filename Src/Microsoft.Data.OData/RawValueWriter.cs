using System;
using System.IO;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015E RID: 350
	internal sealed class RawValueWriter : IDisposable
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		internal RawValueWriter(ODataMessageWriterSettings settings, Stream stream, Encoding encoding)
		{
			this.settings = settings;
			this.stream = stream;
			this.encoding = encoding;
			this.InitializeTextWriter();
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001DC1F File Offset: 0x0001BE1F
		internal TextWriter TextWriter
		{
			get
			{
				return this.textWriter;
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001DC27 File Offset: 0x0001BE27
		public void Dispose()
		{
			this.textWriter.Dispose();
			this.textWriter = null;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001DC3B File Offset: 0x0001BE3B
		internal void Start()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(this.settings.JsonPCallback);
				this.textWriter.Write("(");
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001DC70 File Offset: 0x0001BE70
		internal void End()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(")");
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001DC90 File Offset: 0x0001BE90
		internal void WriteRawValue(object value)
		{
			string text;
			if (!AtomValueUtils.TryConvertPrimitiveToString(value, out text))
			{
				throw new ODataException(Strings.ODataUtils_CannotConvertValueToRawPrimitive(value.GetType().FullName));
			}
			this.textWriter.Write(text);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001DCC9 File Offset: 0x0001BEC9
		internal void Flush()
		{
			if (this.TextWriter != null)
			{
				this.TextWriter.Flush();
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
		private void InitializeTextWriter()
		{
			Stream stream;
			if (MessageStreamWrapper.IsNonDisposingStream(this.stream) || this.stream is AsyncBufferedStream)
			{
				stream = this.stream;
			}
			else
			{
				stream = MessageStreamWrapper.CreateNonDisposingStream(this.stream);
			}
			this.textWriter = new StreamWriter(stream, this.encoding);
		}

		// Token: 0x04000382 RID: 898
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x04000383 RID: 899
		private readonly Stream stream;

		// Token: 0x04000384 RID: 900
		private readonly Encoding encoding;

		// Token: 0x04000385 RID: 901
		private TextWriter textWriter;
	}
}
