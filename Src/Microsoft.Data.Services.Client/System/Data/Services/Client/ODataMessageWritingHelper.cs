using System;
using System.Xml;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000078 RID: 120
	internal class ODataMessageWritingHelper
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x00011332 File Offset: 0x0000F532
		internal ODataMessageWritingHelper(RequestInfo requestInfo)
		{
			this.requestInfo = requestInfo;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011344 File Offset: 0x0000F544
		internal ODataMessageWriterSettings CreateSettings(Func<ODataEntry, XmlWriter, XmlWriter> startEntryXmlCustomizationCallback, Action<ODataEntry, XmlWriter, XmlWriter> endEntryXmlCustomizationCallback, bool isBatchPartRequest)
		{
			ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
			{
				CheckCharacters = false,
				Indent = false,
				DisableMessageStreamDisposal = !isBatchPartRequest
			};
			CommonUtil.SetDefaultMessageQuotas(odataMessageWriterSettings.MessageQuotas);
			if (!this.requestInfo.HasWritingEventHandlers)
			{
				startEntryXmlCustomizationCallback = null;
				endEntryXmlCustomizationCallback = null;
			}
			odataMessageWriterSettings.EnableWcfDataServicesClientBehavior(startEntryXmlCustomizationCallback, endEntryXmlCustomizationCallback, this.requestInfo.DataNamespace, this.requestInfo.TypeScheme.AbsoluteUri);
			this.requestInfo.Configurations.RequestPipeline.ExecuteWriterSettingsConfiguration(odataMessageWriterSettings);
			return odataMessageWriterSettings;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000113C9 File Offset: 0x0000F5C9
		internal ODataMessageWriter CreateWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings writerSettings, bool isParameterPayload)
		{
			this.requestInfo.Context.Format.ValidateCanWriteRequestFormat(requestMessage, isParameterPayload);
			return new ODataMessageWriter(requestMessage, writerSettings, this.requestInfo.Model);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000113F4 File Offset: 0x0000F5F4
		internal ODataRequestMessageWrapper CreateRequestMessage(BuildingRequestEventArgs requestMessageArgs)
		{
			return ODataRequestMessageWrapper.CreateRequestMessageWrapper(requestMessageArgs, this.requestInfo);
		}

		// Token: 0x040002BF RID: 703
		private readonly RequestInfo requestInfo;
	}
}
