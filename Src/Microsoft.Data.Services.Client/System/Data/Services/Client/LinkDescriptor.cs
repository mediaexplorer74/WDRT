using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Diagnostics;

namespace System.Data.Services.Client
{
	// Token: 0x02000102 RID: 258
	[DebuggerDisplay("State = {state}")]
	public sealed class LinkDescriptor : Descriptor
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x000235EB File Offset: 0x000217EB
		internal LinkDescriptor(object source, string sourceProperty, object target, ClientEdmModel model)
			: this(source, sourceProperty, target, EntityStates.Unchanged)
		{
			this.IsSourcePropertyCollection = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(source.GetType())).GetProperty(sourceProperty, false).IsEntityCollection;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002361D File Offset: 0x0002181D
		internal LinkDescriptor(object source, string sourceProperty, object target, EntityStates state)
			: base(state)
		{
			this.source = source;
			this.sourceProperty = sourceProperty;
			this.target = target;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0002363C File Offset: 0x0002183C
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00023644 File Offset: 0x00021844
		public object Target
		{
			get
			{
				return this.target;
			}
			internal set
			{
				this.target = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0002364D File Offset: 0x0002184D
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x00023655 File Offset: 0x00021855
		public object Source
		{
			get
			{
				return this.source;
			}
			internal set
			{
				this.source = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0002365E File Offset: 0x0002185E
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00023666 File Offset: 0x00021866
		public string SourceProperty
		{
			get
			{
				return this.sourceProperty;
			}
			internal set
			{
				this.sourceProperty = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0002366F File Offset: 0x0002186F
		internal override DescriptorKind DescriptorKind
		{
			get
			{
				return DescriptorKind.Link;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00023672 File Offset: 0x00021872
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0002367A File Offset: 0x0002187A
		internal bool IsSourcePropertyCollection { get; set; }

		// Token: 0x0600087A RID: 2170 RVA: 0x00023683 File Offset: 0x00021883
		internal override void ClearChanges()
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00023685 File Offset: 0x00021885
		internal bool IsEquivalent(object src, string srcPropName, object targ)
		{
			return this.source == src && this.target == targ && this.sourceProperty == srcPropName;
		}

		// Token: 0x040004F7 RID: 1271
		internal static readonly IEqualityComparer<LinkDescriptor> EquivalenceComparer = new LinkDescriptor.Equivalent();

		// Token: 0x040004F8 RID: 1272
		private object source;

		// Token: 0x040004F9 RID: 1273
		private string sourceProperty;

		// Token: 0x040004FA RID: 1274
		private object target;

		// Token: 0x02000103 RID: 259
		private sealed class Equivalent : IEqualityComparer<LinkDescriptor>
		{
			// Token: 0x0600087D RID: 2173 RVA: 0x000236B3 File Offset: 0x000218B3
			public bool Equals(LinkDescriptor x, LinkDescriptor y)
			{
				return x != null && y != null && x.IsEquivalent(y.source, y.sourceProperty, y.target);
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x000236D5 File Offset: 0x000218D5
			public int GetHashCode(LinkDescriptor obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.Source.GetHashCode() ^ ((obj.Target != null) ? obj.Target.GetHashCode() : 0) ^ obj.SourceProperty.GetHashCode();
			}
		}
	}
}
