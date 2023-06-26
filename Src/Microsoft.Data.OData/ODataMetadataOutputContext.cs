using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.OData.Atom;

namespace Microsoft.Data.OData
{
	// Token: 0x020001CB RID: 459
	internal sealed class ODataMetadataOutputContext : ODataOutputContext
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x00032668 File Offset: 0x00030868
		internal ODataMetadataOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				this.xmlWriter = ODataAtomWriterUtils.CreateXmlWriter(messageStream, messageWriterSettings, encoding);
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

		// Token: 0x06000E4A RID: 3658 RVA: 0x000326C4 File Offset: 0x000308C4
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000326D1 File Offset: 0x000308D1
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			ODataAtomWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
			this.Flush();
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000326F8 File Offset: 0x000308F8
		internal override void WriteMetadataDocument()
		{
			base.Model.SaveODataAnnotations();
			IEnumerable<EdmError> enumerable;
			if (!EdmxWriter.TryWriteEdmx(base.Model, this.xmlWriter, EdmxTarget.OData, out enumerable))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (EdmError edmError in enumerable)
				{
					stringBuilder.AppendLine(edmError.ToString());
				}
				throw new ODataException(Strings.ODataMetadataOutputContext_ErrorWritingMetadata(stringBuilder.ToString()));
			}
			this.Flush();
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00032784 File Offset: 0x00030984
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			try
			{
				if (this.xmlWriter != null)
				{
					this.xmlWriter.Flush();
					this.messageOutputStream.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.xmlWriter = null;
			}
		}

		// Token: 0x040004B3 RID: 1203
		private Stream messageOutputStream;

		// Token: 0x040004B4 RID: 1204
		private XmlWriter xmlWriter;
	}
}
