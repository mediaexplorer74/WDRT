using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property is interested in participating in activation and might not have provided a message sink.</summary>
	// Token: 0x0200080C RID: 2060
	[ComVisible(true)]
	public interface IContextPropertyActivator
	{
		/// <summary>Indicates whether it is all right to activate the object type indicated in the <paramref name="msg" /> parameter.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</param>
		/// <returns>A Boolean value indicating whether the requested type can be activated.</returns>
		// Token: 0x060058DB RID: 22747
		[SecurityCritical]
		bool IsOKToActivate(IConstructionCallMessage msg);

		/// <summary>Called on each client context property that has this interface, before the construction request leaves the client.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</param>
		// Token: 0x060058DC RID: 22748
		[SecurityCritical]
		void CollectFromClientContext(IConstructionCallMessage msg);

		/// <summary>Called on each client context property that has this interface, when the construction request returns to the client from the server.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</param>
		/// <returns>
		///   <see langword="true" /> if successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060058DD RID: 22749
		[SecurityCritical]
		bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

		/// <summary>Called on each server context property that has this interface, before the construction response leaves the server for the client.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />.</param>
		// Token: 0x060058DE RID: 22750
		[SecurityCritical]
		void CollectFromServerContext(IConstructionReturnMessage msg);

		/// <summary>Called on each client context property that has this interface, when the construction request returns to the client from the server.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />.</param>
		/// <returns>
		///   <see langword="true" /> if successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060058DF RID: 22751
		[SecurityCritical]
		bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);
	}
}
