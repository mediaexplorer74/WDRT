using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x020000C1 RID: 193
	internal abstract class ResourceExpression : Expression
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x00018944 File Offset: 0x00016B44
		internal ResourceExpression(Expression source, Type type, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
		{
			this.source = source;
			this.type = type;
			this.expandPaths = expandPaths ?? new List<string>();
			this.countOption = countOption;
			this.customQueryOptions = customQueryOptions ?? new Dictionary<ConstantExpression, ConstantExpression>(ReferenceEqualityComparer<ConstantExpression>.Instance);
			this.projection = projection;
			this.ResourceTypeAs = resourceTypeAs;
			this.uriVersion = uriVersion ?? Util.DataServiceVersion1;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000189B4 File Offset: 0x00016BB4
		public override Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000633 RID: 1587
		internal abstract ResourceExpression CreateCloneWithNewType(Type type);

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000634 RID: 1588
		internal abstract bool HasQueryOptions { get; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000635 RID: 1589
		internal abstract Type ResourceType { get; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000189BC File Offset: 0x00016BBC
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x000189C4 File Offset: 0x00016BC4
		internal Type ResourceTypeAs { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x000189CD File Offset: 0x00016BCD
		internal Version UriVersion
		{
			get
			{
				return this.uriVersion;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000639 RID: 1593
		internal abstract bool IsSingleton { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000189D5 File Offset: 0x00016BD5
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x000189DD File Offset: 0x00016BDD
		internal virtual List<string> ExpandPaths
		{
			get
			{
				return this.expandPaths;
			}
			set
			{
				this.expandPaths = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x000189E6 File Offset: 0x00016BE6
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x000189EE File Offset: 0x00016BEE
		internal virtual CountOption CountOption
		{
			get
			{
				return this.countOption;
			}
			set
			{
				this.countOption = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x000189F7 File Offset: 0x00016BF7
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x000189FF File Offset: 0x00016BFF
		internal virtual Dictionary<ConstantExpression, ConstantExpression> CustomQueryOptions
		{
			get
			{
				return this.customQueryOptions;
			}
			set
			{
				this.customQueryOptions = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00018A08 File Offset: 0x00016C08
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00018A10 File Offset: 0x00016C10
		internal ProjectionQueryOptionExpression Projection
		{
			get
			{
				return this.projection;
			}
			set
			{
				this.projection = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00018A19 File Offset: 0x00016C19
		internal Expression Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00018A21 File Offset: 0x00016C21
		internal InputReferenceExpression CreateReference()
		{
			if (this.inputRef == null)
			{
				this.inputRef = new InputReferenceExpression(this);
			}
			return this.inputRef;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00018A3D File Offset: 0x00016C3D
		internal void RaiseUriVersion(Version newVersion)
		{
			WebUtil.RaiseVersion(ref this.uriVersion, newVersion);
		}

		// Token: 0x040003F5 RID: 1013
		protected readonly Expression source;

		// Token: 0x040003F6 RID: 1014
		protected InputReferenceExpression inputRef;

		// Token: 0x040003F7 RID: 1015
		private Type type;

		// Token: 0x040003F8 RID: 1016
		private List<string> expandPaths;

		// Token: 0x040003F9 RID: 1017
		private CountOption countOption;

		// Token: 0x040003FA RID: 1018
		private Dictionary<ConstantExpression, ConstantExpression> customQueryOptions;

		// Token: 0x040003FB RID: 1019
		private ProjectionQueryOptionExpression projection;

		// Token: 0x040003FC RID: 1020
		private Version uriVersion;
	}
}
