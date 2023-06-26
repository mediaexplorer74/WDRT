using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property should be registered at runtime through the <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> method.</summary>
	// Token: 0x02000815 RID: 2069
	[ComVisible(true)]
	public interface IDynamicProperty
	{
		/// <summary>Gets the name of the dynamic property.</summary>
		/// <returns>The name of the dynamic property.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060058F7 RID: 22775
		string Name
		{
			[SecurityCritical]
			get;
		}
	}
}
