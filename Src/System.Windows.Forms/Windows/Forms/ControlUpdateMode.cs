using System;

namespace System.Windows.Forms
{
	/// <summary>Determines when changes to a data source value get propagated to the corresponding data-bound control property.</summary>
	// Token: 0x02000170 RID: 368
	public enum ControlUpdateMode
	{
		/// <summary>The bound control is updated when the data source value changes, or the data source position changes.</summary>
		// Token: 0x04000929 RID: 2345
		OnPropertyChanged,
		/// <summary>The bound control is never updated when a data source value changes. The data source is write-only.</summary>
		// Token: 0x0400092A RID: 2346
		Never
	}
}
