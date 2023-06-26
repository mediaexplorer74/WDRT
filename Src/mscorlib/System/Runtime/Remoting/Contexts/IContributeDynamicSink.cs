using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property will be registered at runtime through the <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> method.</summary>
	// Token: 0x02000811 RID: 2065
	[ComVisible(true)]
	public interface IContributeDynamicSink
	{
		/// <summary>Returns the message sink that will be notified of call start and finish events through the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicMessageSink" /> interface.</summary>
		/// <returns>A dynamic sink that exposes the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicMessageSink" /> interface.</returns>
		// Token: 0x060058F3 RID: 22771
		[SecurityCritical]
		IDynamicMessageSink GetDynamicSink();
	}
}
