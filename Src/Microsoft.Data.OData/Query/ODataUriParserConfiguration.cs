using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200000C RID: 12
	internal sealed class ODataUriParserConfiguration
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000029B0 File Offset: 0x00000BB0
		public ODataUriParserConfiguration(IEdmModel model, Uri serviceRoot)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (serviceRoot != null && !serviceRoot.IsAbsoluteUri)
			{
				throw new ArgumentException(Strings.UriParser_UriMustBeAbsolute(serviceRoot), "serviceRoot");
			}
			this.model = model;
			this.serviceRoot = serviceRoot;
			this.Settings = new ODataUriParserSettings();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A14 File Offset: 0x00000C14
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002A1C File Offset: 0x00000C1C
		public ODataUriParserSettings Settings { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A25 File Offset: 0x00000C25
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A2D File Offset: 0x00000C2D
		public Uri ServiceRoot
		{
			get
			{
				return this.serviceRoot;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A35 File Offset: 0x00000C35
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002A3D File Offset: 0x00000C3D
		public ODataUrlConventions UrlConventions
		{
			get
			{
				return this.urlConventions;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<ODataUrlConventions>(value, "UrlConventions");
				this.urlConventions = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A51 File Offset: 0x00000C51
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002A59 File Offset: 0x00000C59
		public Func<string, BatchReferenceSegment> BatchReferenceCallback { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A62 File Offset: 0x00000C62
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A6A File Offset: 0x00000C6A
		public Func<string, string> FunctionParameterAliasCallback { get; set; }

		// Token: 0x04000013 RID: 19
		private readonly IEdmModel model;

		// Token: 0x04000014 RID: 20
		private readonly Uri serviceRoot;

		// Token: 0x04000015 RID: 21
		private ODataUrlConventions urlConventions = ODataUrlConventions.Default;
	}
}
