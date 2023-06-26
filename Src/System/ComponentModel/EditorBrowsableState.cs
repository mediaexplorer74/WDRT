using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the browsable state of a property or method from within an editor.</summary>
	// Token: 0x0200054C RID: 1356
	[global::__DynamicallyInvokable]
	public enum EditorBrowsableState
	{
		/// <summary>The property or method is always browsable from within an editor.</summary>
		// Token: 0x04002991 RID: 10641
		[global::__DynamicallyInvokable]
		Always,
		/// <summary>The property or method is never browsable from within an editor.</summary>
		// Token: 0x04002992 RID: 10642
		[global::__DynamicallyInvokable]
		Never,
		/// <summary>The property or method is a feature that only advanced users should see. An editor can either show or hide such properties.</summary>
		// Token: 0x04002993 RID: 10643
		[global::__DynamicallyInvokable]
		Advanced
	}
}
