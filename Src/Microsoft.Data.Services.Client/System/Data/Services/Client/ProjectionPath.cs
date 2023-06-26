using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Services.Client
{
	// Token: 0x0200007D RID: 125
	[DebuggerDisplay("{ToString()}")]
	internal class ProjectionPath : List<ProjectionPathSegment>
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00011856 File Offset: 0x0000FA56
		internal ProjectionPath()
		{
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001185E File Offset: 0x0000FA5E
		internal ProjectionPath(ParameterExpression root, Expression expectedRootType, Expression rootEntry)
		{
			this.Root = root;
			this.RootEntry = rootEntry;
			this.ExpectedRootType = expectedRootType;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001187C File Offset: 0x0000FA7C
		internal ProjectionPath(ParameterExpression root, Expression expectedRootType, Expression rootEntry, IEnumerable<Expression> members)
			: this(root, expectedRootType, rootEntry)
		{
			foreach (Expression expression in members)
			{
				base.Add(new ProjectionPathSegment(this, (MemberExpression)expression));
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000118DC File Offset: 0x0000FADC
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x000118E4 File Offset: 0x0000FAE4
		internal ParameterExpression Root { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000118ED File Offset: 0x0000FAED
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x000118F5 File Offset: 0x0000FAF5
		internal Expression RootEntry { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000118FE File Offset: 0x0000FAFE
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00011906 File Offset: 0x0000FB06
		internal Expression ExpectedRootType { get; private set; }

		// Token: 0x0600043C RID: 1084 RVA: 0x00011910 File Offset: 0x0000FB10
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Root.ToString());
			stringBuilder.Append("->");
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].SourceTypeAs != null)
				{
					stringBuilder.Insert(0, "(");
					stringBuilder.Append(" as " + base[i].SourceTypeAs.Name + ")");
				}
				if (i > 0)
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append((base[i].Member == null) ? "*" : base[i].Member);
			}
			return stringBuilder.ToString();
		}
	}
}
