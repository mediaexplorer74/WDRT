using System;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyTabChanged" /> event of a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	// Token: 0x02000332 RID: 818
	[ComVisible(true)]
	public class PropertyTabChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyTabChangedEventArgs" /> class.</summary>
		/// <param name="oldTab">The Previously selected property tab.</param>
		/// <param name="newTab">The newly selected property tab.</param>
		// Token: 0x06003558 RID: 13656 RVA: 0x000F25A3 File Offset: 0x000F07A3
		public PropertyTabChangedEventArgs(PropertyTab oldTab, PropertyTab newTab)
		{
			this.oldTab = oldTab;
			this.newTab = newTab;
		}

		/// <summary>Gets the old <see cref="T:System.Windows.Forms.Design.PropertyTab" /> selected.</summary>
		/// <returns>The old <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that was selected.</returns>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000F25B9 File Offset: 0x000F07B9
		public PropertyTab OldTab
		{
			get
			{
				return this.oldTab;
			}
		}

		/// <summary>Gets the new <see cref="T:System.Windows.Forms.Design.PropertyTab" /> selected.</summary>
		/// <returns>The newly selected <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</returns>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000F25C1 File Offset: 0x000F07C1
		public PropertyTab NewTab
		{
			get
			{
				return this.newTab;
			}
		}

		// Token: 0x04001F44 RID: 8004
		private PropertyTab oldTab;

		// Token: 0x04001F45 RID: 8005
		private PropertyTab newTab;
	}
}
