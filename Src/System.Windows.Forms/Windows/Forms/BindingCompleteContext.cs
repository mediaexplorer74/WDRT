using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction of the binding operation.</summary>
	// Token: 0x0200012F RID: 303
	public enum BindingCompleteContext
	{
		/// <summary>An indication that the control property value is being updated from the data source.</summary>
		// Token: 0x040006A8 RID: 1704
		ControlUpdate,
		/// <summary>An indication that the data source value is being updated from the control property.</summary>
		// Token: 0x040006A9 RID: 1705
		DataSourceUpdate
	}
}
