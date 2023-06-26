using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for a set of technologies that designer hosts support.</summary>
	// Token: 0x02000601 RID: 1537
	[ComVisible(true)]
	public enum ViewTechnology
	{
		/// <summary>Represents a mode in which the view object is passed directly to the development environment.</summary>
		// Token: 0x04002B56 RID: 11094
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Passthrough,
		/// <summary>Represents a mode in which a Windows Forms control object provides the display for the root designer.</summary>
		// Token: 0x04002B57 RID: 11095
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		WindowsForms,
		/// <summary>Specifies the default view technology support.</summary>
		// Token: 0x04002B58 RID: 11096
		Default
	}
}
