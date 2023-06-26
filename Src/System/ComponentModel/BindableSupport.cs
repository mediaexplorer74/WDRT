using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values to indicate whether a property can be bound to a data element or another property.</summary>
	// Token: 0x0200051A RID: 1306
	public enum BindableSupport
	{
		/// <summary>The property is not bindable at design time.</summary>
		// Token: 0x04002913 RID: 10515
		No,
		/// <summary>The property is bindable at design time.</summary>
		// Token: 0x04002914 RID: 10516
		Yes,
		/// <summary>The property is set to the default.</summary>
		// Token: 0x04002915 RID: 10517
		Default
	}
}
