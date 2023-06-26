using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001F1 RID: 497
	internal sealed class ODataReaderBehavior
	{
		// Token: 0x06000F3C RID: 3900 RVA: 0x00036C5B File Offset: 0x00034E5B
		private ODataReaderBehavior(ODataBehaviorKind formatBehaviorKind, ODataBehaviorKind apiBehaviorKind, bool allowDuplicatePropertyNames, bool usesV1Provider, Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme)
		{
			this.formatBehaviorKind = formatBehaviorKind;
			this.apiBehaviorKind = apiBehaviorKind;
			this.allowDuplicatePropertyNames = allowDuplicatePropertyNames;
			this.usesV1Provider = usesV1Provider;
			this.typeResolver = typeResolver;
			this.odataNamespace = odataNamespace;
			this.typeScheme = typeScheme;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00036C98 File Offset: 0x00034E98
		internal static ODataReaderBehavior DefaultBehavior
		{
			get
			{
				return ODataReaderBehavior.defaultReaderBehavior;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00036C9F File Offset: 0x00034E9F
		internal string ODataTypeScheme
		{
			get
			{
				return this.typeScheme;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00036CA7 File Offset: 0x00034EA7
		internal string ODataNamespace
		{
			get
			{
				return this.odataNamespace;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00036CAF File Offset: 0x00034EAF
		internal bool AllowDuplicatePropertyNames
		{
			get
			{
				return this.allowDuplicatePropertyNames;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00036CB7 File Offset: 0x00034EB7
		internal bool UseV1ProviderBehavior
		{
			get
			{
				return this.usesV1Provider;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00036CBF File Offset: 0x00034EBF
		internal Func<IEdmType, string, IEdmType> TypeResolver
		{
			get
			{
				return this.typeResolver;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00036CC7 File Offset: 0x00034EC7
		internal ODataBehaviorKind FormatBehaviorKind
		{
			get
			{
				return this.formatBehaviorKind;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00036CCF File Offset: 0x00034ECF
		internal ODataBehaviorKind ApiBehaviorKind
		{
			get
			{
				return this.apiBehaviorKind;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00036CD7 File Offset: 0x00034ED7
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00036CDF File Offset: 0x00034EDF
		internal Func<IEdmEntityType, bool> OperationsBoundToEntityTypeMustBeContainerQualified
		{
			get
			{
				return this.operationsBoundToEntityTypeMustBeContainerQualified;
			}
			set
			{
				this.operationsBoundToEntityTypeMustBeContainerQualified = value;
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00036CE8 File Offset: 0x00034EE8
		internal static ODataReaderBehavior CreateWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme)
		{
			return new ODataReaderBehavior(ODataBehaviorKind.WcfDataServicesClient, ODataBehaviorKind.WcfDataServicesClient, true, false, typeResolver, odataNamespace, typeScheme);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00036CF6 File Offset: 0x00034EF6
		internal static ODataReaderBehavior CreateWcfDataServicesServerBehavior(bool usesV1Provider)
		{
			return new ODataReaderBehavior(ODataBehaviorKind.WcfDataServicesServer, ODataBehaviorKind.WcfDataServicesServer, true, usesV1Provider, null, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00036D0C File Offset: 0x00034F0C
		internal void ResetFormatBehavior()
		{
			this.formatBehaviorKind = ODataBehaviorKind.Default;
			this.allowDuplicatePropertyNames = false;
			this.usesV1Provider = false;
			this.odataNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices";
			this.typeScheme = "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme";
			this.operationsBoundToEntityTypeMustBeContainerQualified = null;
		}

		// Token: 0x04000556 RID: 1366
		private static readonly ODataReaderBehavior defaultReaderBehavior = new ODataReaderBehavior(ODataBehaviorKind.Default, ODataBehaviorKind.Default, false, false, null, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");

		// Token: 0x04000557 RID: 1367
		private readonly ODataBehaviorKind apiBehaviorKind;

		// Token: 0x04000558 RID: 1368
		private readonly Func<IEdmType, string, IEdmType> typeResolver;

		// Token: 0x04000559 RID: 1369
		private bool allowDuplicatePropertyNames;

		// Token: 0x0400055A RID: 1370
		private bool usesV1Provider;

		// Token: 0x0400055B RID: 1371
		private string typeScheme;

		// Token: 0x0400055C RID: 1372
		private string odataNamespace;

		// Token: 0x0400055D RID: 1373
		private ODataBehaviorKind formatBehaviorKind;

		// Token: 0x0400055E RID: 1374
		private Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified;
	}
}
