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
	// Token: 0x02000201 RID: 513
	internal sealed class ODataMetadataInputContext : ODataInputContext
	{
		// Token: 0x06000F92 RID: 3986 RVA: 0x00037FE8 File Offset: 0x000361E8
		internal ODataMetadataInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			try
			{
				this.baseXmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, encoding, messageReaderSettings);
				this.xmlReader = new BufferingXmlReader(this.baseXmlReader, null, messageReaderSettings.BaseUri, false, messageReaderSettings.MessageQuotas.MaxNestingDepth, messageReaderSettings.ReaderBehavior.ODataNamespace);
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

		// Token: 0x06000F93 RID: 3987 RVA: 0x00038088 File Offset: 0x00036288
		internal override IEdmModel ReadMetadataDocument()
		{
			return this.ReadMetadataDocumentImplementation();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00038090 File Offset: 0x00036290
		protected override void DisposeImplementation()
		{
			try
			{
				if (this.baseXmlReader != null)
				{
					((IDisposable)this.baseXmlReader).Dispose();
				}
			}
			finally
			{
				this.baseXmlReader = null;
				this.xmlReader = null;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000380D4 File Offset: 0x000362D4
		private IEdmModel ReadMetadataDocumentImplementation()
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!EdmxReader.TryParse(this.xmlReader, out edmModel, out enumerable))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (EdmError edmError in enumerable)
				{
					stringBuilder.AppendLine(edmError.ToString());
				}
				throw new ODataException(Strings.ODataMetadataInputContext_ErrorReadingMetadata(stringBuilder.ToString()));
			}
			edmModel.LoadODataAnnotations(base.MessageReaderSettings.MessageQuotas.MaxEntityPropertyMappingsPerType);
			return edmModel;
		}

		// Token: 0x04000588 RID: 1416
		private XmlReader baseXmlReader;

		// Token: 0x04000589 RID: 1417
		private BufferingXmlReader xmlReader;
	}
}
