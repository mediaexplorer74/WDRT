using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the visibility a property has to the design-time serializer.</summary>
	// Token: 0x02000542 RID: 1346
	[ComVisible(true)]
	public enum DesignerSerializationVisibility
	{
		/// <summary>The code generator does not produce code for the object.</summary>
		// Token: 0x04002978 RID: 10616
		Hidden,
		/// <summary>The code generator produces code for the object.</summary>
		// Token: 0x04002979 RID: 10617
		Visible,
		/// <summary>The code generator produces code for the contents of the object, rather than for the object itself.</summary>
		// Token: 0x0400297A RID: 10618
		Content
	}
}
