using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides display instructions for the debugger.</summary>
	// Token: 0x020003EA RID: 1002
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public enum DebuggerBrowsableState
	{
		/// <summary>Never show the element.</summary>
		// Token: 0x040016AC RID: 5804
		[__DynamicallyInvokable]
		Never,
		/// <summary>Show the element as collapsed.</summary>
		// Token: 0x040016AD RID: 5805
		[__DynamicallyInvokable]
		Collapsed = 2,
		/// <summary>Do not display the root element; display the child elements if the element is a collection or array of items.</summary>
		// Token: 0x040016AE RID: 5806
		[__DynamicallyInvokable]
		RootHidden
	}
}
