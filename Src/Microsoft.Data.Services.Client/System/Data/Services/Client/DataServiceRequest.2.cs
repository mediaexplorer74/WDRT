using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200010C RID: 268
	public sealed class DataServiceRequest<TElement> : DataServiceRequest
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x000248C6 File Offset: 0x00022AC6
		public DataServiceRequest(Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			this.requestUri = requestUri;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000248E1 File Offset: 0x00022AE1
		internal DataServiceRequest(Uri requestUri, QueryComponents queryComponents, ProjectionPlan plan)
			: this(requestUri)
		{
			this.queryComponents = queryComponents;
			this.plan = plan;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x000248F8 File Offset: 0x00022AF8
		public override Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00024904 File Offset: 0x00022B04
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0002490C File Offset: 0x00022B0C
		public override Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
			internal set
			{
				this.requestUri = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00024915 File Offset: 0x00022B15
		internal override ProjectionPlan Plan
		{
			get
			{
				return this.plan;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002491D File Offset: 0x00022B1D
		public override string ToString()
		{
			return this.requestUri.ToString();
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002492C File Offset: 0x00022B2C
		internal override QueryComponents QueryComponents(ClientEdmModel model)
		{
			if (this.queryComponents == null)
			{
				Type type = typeof(TElement);
				type = ((PrimitiveType.IsKnownType(type) || WebUtil.IsCLRTypeCollection(type, model)) ? type : TypeSystem.GetElementType(type));
				this.queryComponents = new QueryComponents(this.requestUri, Util.DataServiceVersionEmpty, type, null, null);
			}
			return this.queryComponents;
		}

		// Token: 0x04000510 RID: 1296
		private readonly ProjectionPlan plan;

		// Token: 0x04000511 RID: 1297
		private Uri requestUri;

		// Token: 0x04000512 RID: 1298
		private QueryComponents queryComponents;
	}
}
