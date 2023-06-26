using System;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C3 RID: 451
	internal class ODataVerboseJsonSerializer : ODataSerializer
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x0003106D File Offset: 0x0002F26D
		internal ODataVerboseJsonSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003107D File Offset: 0x0002F27D
		internal ODataVerboseJsonOutputContext VerboseJsonOutputContext
		{
			get
			{
				return this.verboseJsonOutputContext;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00031085 File Offset: 0x0002F285
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.verboseJsonOutputContext.JsonWriter;
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00031092 File Offset: 0x0002F292
		internal void WritePayloadStart()
		{
			this.WritePayloadStart(false);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003109B File Offset: 0x0002F29B
		internal void WritePayloadStart(bool disableResponseWrapper)
		{
			ODataJsonWriterUtils.StartJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
			if (base.WritingResponse && !disableResponseWrapper)
			{
				this.JsonWriter.StartObjectScope();
				this.JsonWriter.WriteDataWrapper();
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000310CF File Offset: 0x0002F2CF
		internal void WritePayloadEnd()
		{
			this.WritePayloadEnd(false);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000310D8 File Offset: 0x0002F2D8
		internal void WritePayloadEnd(bool disableResponseWrapper)
		{
			if (base.WritingResponse && !disableResponseWrapper)
			{
				this.JsonWriter.EndObjectScope();
			}
			ODataJsonWriterUtils.EndJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00031101 File Offset: 0x0002F301
		internal void WriteTopLevelPayload(Action payloadWriterAction)
		{
			this.WriteTopLevelPayload(payloadWriterAction, false);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003110B File Offset: 0x0002F30B
		internal void WriteTopLevelPayload(Action payloadWriterAction, bool disableResponseWrapper)
		{
			this.WritePayloadStart(disableResponseWrapper);
			payloadWriterAction();
			this.WritePayloadEnd(disableResponseWrapper);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00031164 File Offset: 0x0002F364
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WriteTopLevelPayload(delegate
			{
				ODataJsonWriterUtils.WriteError(this.VerboseJsonOutputContext.JsonWriter, null, error, includeDebugInformation, this.MessageWriterSettings.MessageQuotas.MaxNestingDepth, false);
			}, true);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003119F File Offset: 0x0002F39F
		internal string UriToAbsoluteUriString(Uri uri)
		{
			return this.UriToUriString(uri, true);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x000311A9 File Offset: 0x0002F3A9
		internal string UriToUriString(Uri uri, bool makeAbsolute)
		{
			return ODataJsonWriterUtils.UriToUriString(this.verboseJsonOutputContext, uri, makeAbsolute);
		}

		// Token: 0x040004AF RID: 1199
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;
	}
}
