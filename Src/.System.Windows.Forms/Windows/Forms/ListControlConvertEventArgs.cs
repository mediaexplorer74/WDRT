using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListControl.Format" /> event.</summary>
	// Token: 0x020002CD RID: 717
	public class ListControlConvertEventArgs : ConvertEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListControlConvertEventArgs" /> class with the specified object, type, and list item.</summary>
		/// <param name="value">The value displayed in the <see cref="T:System.Windows.Forms.ListControl" />.</param>
		/// <param name="desiredType">The <see cref="T:System.Type" /> for the displayed item.</param>
		/// <param name="listItem">The data source item to be displayed in the <see cref="T:System.Windows.Forms.ListControl" />.</param>
		// Token: 0x06002CA9 RID: 11433 RVA: 0x000C8737 File Offset: 0x000C6937
		public ListControlConvertEventArgs(object value, Type desiredType, object listItem)
			: base(value, desiredType)
		{
			this.listItem = listItem;
		}

		/// <summary>Gets a data source item.</summary>
		/// <returns>The <see cref="T:System.Object" /> that represents an item in the data source.</returns>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000C8748 File Offset: 0x000C6948
		public object ListItem
		{
			get
			{
				return this.listItem;
			}
		}

		// Token: 0x0400128B RID: 4747
		private object listItem;
	}
}
