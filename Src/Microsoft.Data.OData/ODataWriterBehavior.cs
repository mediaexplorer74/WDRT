using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001F2 RID: 498
	internal sealed class ODataWriterBehavior
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x00036D5B File Offset: 0x00034F5B
		private ODataWriterBehavior(ODataBehaviorKind formatBehaviorKind, ODataBehaviorKind apiBehaviorKind, bool usesV1Provider, bool allowNullValuesForNonNullablePrimitiveTypes, bool allowDuplicatePropertyNames, string odataNamespace, string typeScheme)
		{
			this.formatBehaviorKind = formatBehaviorKind;
			this.apiBehaviorKind = apiBehaviorKind;
			this.usesV1Provider = usesV1Provider;
			this.allowNullValuesForNonNullablePrimitiveTypes = allowNullValuesForNonNullablePrimitiveTypes;
			this.allowDuplicatePropertyNames = allowDuplicatePropertyNames;
			this.odataNamespace = odataNamespace;
			this.typeScheme = typeScheme;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00036D98 File Offset: 0x00034F98
		internal static ODataWriterBehavior DefaultBehavior
		{
			get
			{
				return ODataWriterBehavior.defaultWriterBehavior;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00036D9F File Offset: 0x00034F9F
		internal string ODataTypeScheme
		{
			get
			{
				return this.typeScheme;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00036DA7 File Offset: 0x00034FA7
		internal string ODataNamespace
		{
			get
			{
				return this.odataNamespace;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00036DAF File Offset: 0x00034FAF
		internal bool UseV1ProviderBehavior
		{
			get
			{
				return this.usesV1Provider;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00036DB7 File Offset: 0x00034FB7
		internal bool AllowNullValuesForNonNullablePrimitiveTypes
		{
			get
			{
				return this.allowNullValuesForNonNullablePrimitiveTypes;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00036DBF File Offset: 0x00034FBF
		internal bool AllowDuplicatePropertyNames
		{
			get
			{
				return this.allowDuplicatePropertyNames;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00036DC7 File Offset: 0x00034FC7
		internal ODataBehaviorKind FormatBehaviorKind
		{
			get
			{
				return this.formatBehaviorKind;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00036DCF File Offset: 0x00034FCF
		internal ODataBehaviorKind ApiBehaviorKind
		{
			get
			{
				return this.apiBehaviorKind;
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00036DD7 File Offset: 0x00034FD7
		internal static ODataWriterBehavior CreateWcfDataServicesClientBehavior(string odataNamespace, string typeScheme)
		{
			return new ODataWriterBehavior(ODataBehaviorKind.WcfDataServicesClient, ODataBehaviorKind.WcfDataServicesClient, false, false, false, odataNamespace, typeScheme);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00036DE5 File Offset: 0x00034FE5
		internal static ODataWriterBehavior CreateWcfDataServicesServerBehavior(bool usesV1Provider)
		{
			return new ODataWriterBehavior(ODataBehaviorKind.WcfDataServicesServer, ODataBehaviorKind.WcfDataServicesServer, usesV1Provider, true, true, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00036DFB File Offset: 0x00034FFB
		internal void UseDefaultFormatBehavior()
		{
			this.formatBehaviorKind = ODataBehaviorKind.Default;
			this.usesV1Provider = false;
			this.allowNullValuesForNonNullablePrimitiveTypes = false;
			this.allowDuplicatePropertyNames = false;
			this.odataNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices";
			this.typeScheme = "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme";
		}

		// Token: 0x0400055F RID: 1375
		private static readonly ODataWriterBehavior defaultWriterBehavior = new ODataWriterBehavior(ODataBehaviorKind.Default, ODataBehaviorKind.Default, false, false, false, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");

		// Token: 0x04000560 RID: 1376
		private readonly ODataBehaviorKind apiBehaviorKind;

		// Token: 0x04000561 RID: 1377
		private bool usesV1Provider;

		// Token: 0x04000562 RID: 1378
		private bool allowNullValuesForNonNullablePrimitiveTypes;

		// Token: 0x04000563 RID: 1379
		private bool allowDuplicatePropertyNames;

		// Token: 0x04000564 RID: 1380
		private string typeScheme;

		// Token: 0x04000565 RID: 1381
		private string odataNamespace;

		// Token: 0x04000566 RID: 1382
		private ODataBehaviorKind formatBehaviorKind;
	}
}
