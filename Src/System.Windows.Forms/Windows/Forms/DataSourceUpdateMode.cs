using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies when a data source is updated when changes occur in the bound control.</summary>
	// Token: 0x02000223 RID: 547
	public enum DataSourceUpdateMode
	{
		/// <summary>Data source is updated when the control property is validated,</summary>
		// Token: 0x04000EA5 RID: 3749
		OnValidation,
		/// <summary>Data source is updated whenever the value of the control property changes.</summary>
		// Token: 0x04000EA6 RID: 3750
		OnPropertyChanged,
		/// <summary>Data source is never updated and values entered into the control are not parsed, validated or re-formatted.</summary>
		// Token: 0x04000EA7 RID: 3751
		Never
	}
}
