using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates the result of a completed binding operation.</summary>
	// Token: 0x02000132 RID: 306
	public enum BindingCompleteState
	{
		/// <summary>An indication that the binding operation completed successfully.</summary>
		// Token: 0x040006B0 RID: 1712
		Success,
		/// <summary>An indication that the binding operation failed with a data error.</summary>
		// Token: 0x040006B1 RID: 1713
		DataError,
		/// <summary>An indication that the binding operation failed with an exception.</summary>
		// Token: 0x040006B2 RID: 1714
		Exception
	}
}
