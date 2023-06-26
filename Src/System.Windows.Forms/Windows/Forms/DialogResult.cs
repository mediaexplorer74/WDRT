using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies identifiers to indicate the return value of a dialog box.</summary>
	// Token: 0x0200022C RID: 556
	[ComVisible(true)]
	public enum DialogResult
	{
		/// <summary>
		///   <see langword="Nothing" /> is returned from the dialog box. This means that the modal dialog continues running.</summary>
		// Token: 0x04000EDD RID: 3805
		None,
		/// <summary>The dialog box return value is <see langword="OK" /> (usually sent from a button labeled OK).</summary>
		// Token: 0x04000EDE RID: 3806
		OK,
		/// <summary>The dialog box return value is <see langword="Cancel" /> (usually sent from a button labeled Cancel).</summary>
		// Token: 0x04000EDF RID: 3807
		Cancel,
		/// <summary>The dialog box return value is <see langword="Abort" /> (usually sent from a button labeled Abort).</summary>
		// Token: 0x04000EE0 RID: 3808
		Abort,
		/// <summary>The dialog box return value is <see langword="Retry" /> (usually sent from a button labeled Retry).</summary>
		// Token: 0x04000EE1 RID: 3809
		Retry,
		/// <summary>The dialog box return value is <see langword="Ignore" /> (usually sent from a button labeled Ignore).</summary>
		// Token: 0x04000EE2 RID: 3810
		Ignore,
		/// <summary>The dialog box return value is <see langword="Yes" /> (usually sent from a button labeled Yes).</summary>
		// Token: 0x04000EE3 RID: 3811
		Yes,
		/// <summary>The dialog box return value is <see langword="No" /> (usually sent from a button labeled No).</summary>
		// Token: 0x04000EE4 RID: 3812
		No
	}
}
