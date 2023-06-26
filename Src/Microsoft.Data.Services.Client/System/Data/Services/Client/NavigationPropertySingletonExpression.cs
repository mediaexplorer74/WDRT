using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000D6 RID: 214
	internal class NavigationPropertySingletonExpression : ResourceExpression
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0001C824 File Offset: 0x0001AA24
		internal NavigationPropertySingletonExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
			: base(source, type, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion)
		{
			this.memberExpression = memberExpression;
			this.resourceType = resourceType;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001C854 File Offset: 0x0001AA54
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10002;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001C85B File Offset: 0x0001AA5B
		internal MemberExpression MemberExpression
		{
			get
			{
				return (MemberExpression)this.memberExpression;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001C868 File Offset: 0x0001AA68
		internal override Type ResourceType
		{
			get
			{
				return this.resourceType;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001C870 File Offset: 0x0001AA70
		internal override bool IsSingleton
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001C873 File Offset: 0x0001AA73
		internal override bool HasQueryOptions
		{
			get
			{
				return this.ExpandPaths.Count > 0 || this.CountOption == CountOption.InlineAll || this.CustomQueryOptions.Count > 0 || base.Projection != null;
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001C8BC File Offset: 0x0001AABC
		internal override ResourceExpression CreateCloneWithNewType(Type type)
		{
			return new NavigationPropertySingletonExpression(type, this.source, this.MemberExpression, TypeSystem.GetElementType(type), this.ExpandPaths.ToList<string>(), this.CountOption, this.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), base.Projection, base.ResourceTypeAs, base.UriVersion);
		}

		// Token: 0x04000434 RID: 1076
		private readonly Expression memberExpression;

		// Token: 0x04000435 RID: 1077
		private readonly Type resourceType;
	}
}
