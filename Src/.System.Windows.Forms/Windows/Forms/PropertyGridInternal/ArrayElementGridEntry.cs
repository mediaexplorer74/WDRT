using System;
using System.Globalization;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004F9 RID: 1273
	internal class ArrayElementGridEntry : GridEntry
	{
		// Token: 0x0600537F RID: 21375 RVA: 0x0015DDF1 File Offset: 0x0015BFF1
		public ArrayElementGridEntry(PropertyGrid ownerGrid, GridEntry peParent, int index)
			: base(ownerGrid, peParent)
		{
			this.index = index;
			this.SetFlag(256, (peParent.Flags & 256) != 0 || peParent.ForceReadOnly);
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06005380 RID: 21376 RVA: 0x00016041 File Offset: 0x00014241
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.ArrayValue;
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x0015DE24 File Offset: 0x0015C024
		public override bool IsValueEditable
		{
			get
			{
				return this.ParentGridEntry.IsValueEditable;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06005382 RID: 21378 RVA: 0x0015DE31 File Offset: 0x0015C031
		public override string PropertyLabel
		{
			get
			{
				return "[" + this.index.ToString(CultureInfo.CurrentCulture) + "]";
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x0015DE52 File Offset: 0x0015C052
		public override Type PropertyType
		{
			get
			{
				return this.parentPE.PropertyType.GetElementType();
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06005384 RID: 21380 RVA: 0x0015DE64 File Offset: 0x0015C064
		// (set) Token: 0x06005385 RID: 21381 RVA: 0x0015DE8C File Offset: 0x0015C08C
		public override object PropertyValue
		{
			get
			{
				object valueOwner = this.GetValueOwner();
				return ((Array)valueOwner).GetValue(this.index);
			}
			set
			{
				object valueOwner = this.GetValueOwner();
				((Array)valueOwner).SetValue(value, this.index);
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06005386 RID: 21382 RVA: 0x0015DEB2 File Offset: 0x0015C0B2
		public override bool ShouldRenderReadOnly
		{
			get
			{
				return this.ParentGridEntry.ShouldRenderReadOnly;
			}
		}

		// Token: 0x040036B0 RID: 14000
		protected int index;
	}
}
