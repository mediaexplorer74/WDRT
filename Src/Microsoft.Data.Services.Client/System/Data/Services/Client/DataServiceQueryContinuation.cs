using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x0200005A RID: 90
	[DebuggerDisplay("{NextLinkUri}")]
	public abstract class DataServiceQueryContinuation
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000DF5F File Offset: 0x0000C15F
		internal DataServiceQueryContinuation(Uri nextLinkUri, ProjectionPlan plan)
		{
			this.nextLinkUri = nextLinkUri;
			this.plan = plan;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000DF75 File Offset: 0x0000C175
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000DF7D File Offset: 0x0000C17D
		public Uri NextLinkUri
		{
			get
			{
				return this.nextLinkUri;
			}
			internal set
			{
				this.nextLinkUri = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600030D RID: 781
		internal abstract Type ElementType { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000DF86 File Offset: 0x0000C186
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000DF8E File Offset: 0x0000C18E
		internal ProjectionPlan Plan
		{
			get
			{
				return this.plan;
			}
			set
			{
				this.plan = value;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000DF97 File Offset: 0x0000C197
		public override string ToString()
		{
			return this.NextLinkUri.ToString();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		internal static DataServiceQueryContinuation Create(Uri nextLinkUri, ProjectionPlan plan)
		{
			if (nextLinkUri == null)
			{
				return null;
			}
			IEnumerable<ConstructorInfo> instanceConstructors = typeof(DataServiceQueryContinuation<>).MakeGenericType(new Type[] { plan.ProjectedType }).GetInstanceConstructors(false);
			object obj = Util.ConstructorInvoke(instanceConstructors.Single<ConstructorInfo>(), new object[] { nextLinkUri, plan });
			return (DataServiceQueryContinuation)obj;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E008 File Offset: 0x0000C208
		internal QueryComponents CreateQueryComponents()
		{
			return new QueryComponents(this.NextLinkUri, Util.DataServiceVersionEmpty, this.Plan.LastSegmentType, null, null);
		}

		// Token: 0x04000277 RID: 631
		private Uri nextLinkUri;

		// Token: 0x04000278 RID: 632
		private ProjectionPlan plan;
	}
}
