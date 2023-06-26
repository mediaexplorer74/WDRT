using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyValueChanged" /> event of a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	// Token: 0x02000334 RID: 820
	[ComVisible(true)]
	public class PropertyValueChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyValueChangedEventArgs" /> class.</summary>
		/// <param name="changedItem">The item in the grid that changed.</param>
		/// <param name="oldValue">The old property value.</param>
		// Token: 0x0600355F RID: 13663 RVA: 0x000F25C9 File Offset: 0x000F07C9
		public PropertyValueChangedEventArgs(GridItem changedItem, object oldValue)
		{
			this.changedItem = changedItem;
			this.oldValue = oldValue;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> that was changed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridItem" /> in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</returns>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000F25DF File Offset: 0x000F07DF
		public GridItem ChangedItem
		{
			get
			{
				return this.changedItem;
			}
		}

		/// <summary>The value of the grid item before it was changed.</summary>
		/// <returns>A object representing the old value of the property.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000F25E7 File Offset: 0x000F07E7
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		// Token: 0x04001F46 RID: 8006
		private readonly GridItem changedItem;

		// Token: 0x04001F47 RID: 8007
		private object oldValue;
	}
}
