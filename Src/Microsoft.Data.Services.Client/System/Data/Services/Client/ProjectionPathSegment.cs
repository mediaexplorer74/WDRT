using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x02000080 RID: 128
	[DebuggerDisplay("Segment {ProjectionType} {Member}")]
	internal class ProjectionPathSegment
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00011E0A File Offset: 0x0001000A
		internal ProjectionPathSegment(ProjectionPath startPath, string member, Type projectionType)
		{
			this.Member = member;
			this.StartPath = startPath;
			this.ProjectionType = projectionType;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00011E28 File Offset: 0x00010028
		internal ProjectionPathSegment(ProjectionPath startPath, MemberExpression memberExpression)
		{
			this.StartPath = startPath;
			Expression expression = ResourceBinder.StripTo<Expression>(memberExpression.Expression);
			this.Member = memberExpression.Member.Name;
			this.ProjectionType = memberExpression.Type;
			this.SourceTypeAs = ((expression.NodeType == ExpressionType.TypeAs) ? expression.Type : null);
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00011E84 File Offset: 0x00010084
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00011E8C File Offset: 0x0001008C
		internal string Member { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00011E95 File Offset: 0x00010095
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00011E9D File Offset: 0x0001009D
		internal Type ProjectionType { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00011EA6 File Offset: 0x000100A6
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00011EAE File Offset: 0x000100AE
		internal Type SourceTypeAs { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00011EB7 File Offset: 0x000100B7
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00011EBF File Offset: 0x000100BF
		internal ProjectionPath StartPath { get; private set; }
	}
}
