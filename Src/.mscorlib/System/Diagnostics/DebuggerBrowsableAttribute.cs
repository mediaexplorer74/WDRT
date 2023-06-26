using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines if and how a member is displayed in the debugger variable windows. This class cannot be inherited.</summary>
	// Token: 0x020003EB RID: 1003
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerBrowsableAttribute" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values that specifies how to display the member.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="state" /> is not one of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values.</exception>
		// Token: 0x06003328 RID: 13096 RVA: 0x000C6422 File Offset: 0x000C4622
		[__DynamicallyInvokable]
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
			{
				throw new ArgumentOutOfRangeException("state");
			}
			this.state = state;
		}

		/// <summary>Gets the display state for the attribute.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000C6444 File Offset: 0x000C4644
		[__DynamicallyInvokable]
		public DebuggerBrowsableState State
		{
			[__DynamicallyInvokable]
			get
			{
				return this.state;
			}
		}

		// Token: 0x040016AF RID: 5807
		private DebuggerBrowsableState state;
	}
}
