using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	/// <summary>Defines the type of normalization to perform.</summary>
	// Token: 0x02000A78 RID: 2680
	[ComVisible(true)]
	public enum NormalizationForm
	{
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x04002EE5 RID: 12005
		FormC = 1,
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition.</summary>
		// Token: 0x04002EE6 RID: 12006
		FormD,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x04002EE7 RID: 12007
		FormKC = 5,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition.</summary>
		// Token: 0x04002EE8 RID: 12008
		FormKD
	}
}
