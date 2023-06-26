using System;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000077 RID: 119
	internal class ODataMessageReadingHelper
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0001121C File Offset: 0x0000F41C
		internal ODataMessageReadingHelper(ResponseInfo responseInfo)
		{
			this.responseInfo = responseInfo;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001122C File Offset: 0x0000F42C
		internal ODataMessageReaderSettings CreateSettings(Func<ODataEntry, XmlReader, Uri, XmlReader> entryXmlCustomizer)
		{
			ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings();
			if (!this.responseInfo.ResponsePipeline.HasAtomReadingEntityHandlers)
			{
				entryXmlCustomizer = null;
			}
			Func<IEdmType, string, IEdmType> func = new Func<IEdmType, string, IEdmType>(this.responseInfo.TypeResolver.ResolveWireTypeName);
			if (this.responseInfo.Context.Format.ServiceModel != null)
			{
				func = null;
			}
			odataMessageReaderSettings.EnableWcfDataServicesClientBehavior(func, this.responseInfo.DataNamespace, UriUtil.UriToString(this.responseInfo.TypeScheme), entryXmlCustomizer);
			odataMessageReaderSettings.BaseUri = this.responseInfo.BaseUriResolver.BaseUriOrNull;
			odataMessageReaderSettings.MaxProtocolVersion = CommonUtil.ConvertToODataVersion(this.responseInfo.MaxProtocolVersion);
			odataMessageReaderSettings.UndeclaredPropertyBehaviorKinds = this.responseInfo.UndeclaredPropertyBehaviorKinds | ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty;
			CommonUtil.SetDefaultMessageQuotas(odataMessageReaderSettings.MessageQuotas);
			this.responseInfo.ResponsePipeline.ExecuteReaderSettingsConfiguration(odataMessageReaderSettings);
			return odataMessageReaderSettings;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00011303 File Offset: 0x0000F503
		internal ODataMessageReader CreateReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings)
		{
			this.responseInfo.Context.Format.ValidateCanReadResponseFormat(responseMessage);
			return new ODataMessageReader(responseMessage, settings, this.responseInfo.TypeResolver.ReaderModel);
		}

		// Token: 0x040002BE RID: 702
		private readonly ResponseInfo responseInfo;
	}
}
