using System;

namespace System.Windows.Forms
{
	// Token: 0x0200032D RID: 813
	internal class PropertyGridToolStrip : ToolStrip
	{
		// Token: 0x0600351E RID: 13598 RVA: 0x000F1582 File Offset: 0x000EF782
		public PropertyGridToolStrip(PropertyGrid parentPropertyGrid)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool SupportsUiaProviders
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000F1591 File Offset: 0x000EF791
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new PropertyGridToolStripAccessibleObject(this, this._parentPropertyGrid);
		}

		// Token: 0x04001F36 RID: 7990
		private PropertyGrid _parentPropertyGrid;
	}
}
