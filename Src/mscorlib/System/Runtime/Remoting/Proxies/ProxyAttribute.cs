using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Proxies
{
	/// <summary>Indicates that an object type requires a custom proxy.</summary>
	// Token: 0x020007FB RID: 2043
	[SecurityCritical]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ProxyAttribute : Attribute, IContextAttribute
	{
		/// <summary>Creates either an uninitialized <see cref="T:System.MarshalByRefObject" /> or a transparent proxy, depending on whether the specified type can exist in the current context.</summary>
		/// <param name="serverType">The object type to create an instance of.</param>
		/// <returns>An uninitialized <see cref="T:System.MarshalByRefObject" /> or a transparent proxy.</returns>
		// Token: 0x06005843 RID: 22595 RVA: 0x00138248 File Offset: 0x00136448
		[SecurityCritical]
		public virtual MarshalByRefObject CreateInstance(Type serverType)
		{
			if (serverType == null)
			{
				throw new ArgumentNullException("serverType");
			}
			RuntimeType runtimeType = serverType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			if (!serverType.IsContextful)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
			}
			if (serverType.IsAbstract)
			{
				throw new RemotingException(Environment.GetResourceString("Acc_CreateAbst"));
			}
			return this.CreateInstanceInternal(runtimeType);
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001382C0 File Offset: 0x001364C0
		internal MarshalByRefObject CreateInstanceInternal(RuntimeType serverType)
		{
			return ActivationServices.CreateInstance(serverType);
		}

		/// <summary>Creates an instance of a remoting proxy for a remote object described by the specified <see cref="T:System.Runtime.Remoting.ObjRef" />, and located on the server.</summary>
		/// <param name="objRef">The object reference to the remote object for which to create a proxy.</param>
		/// <param name="serverType">The type of the server where the remote object is located.</param>
		/// <param name="serverObject">The server object.</param>
		/// <param name="serverContext">The context in which the server object is located.</param>
		/// <returns>The new instance of remoting proxy for the remote object that is described in the specified <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		// Token: 0x06005845 RID: 22597 RVA: 0x001382C8 File Offset: 0x001364C8
		[SecurityCritical]
		public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
		{
			RemotingProxy remotingProxy = new RemotingProxy(serverType);
			if (serverContext != null)
			{
				RealProxy.SetStubData(remotingProxy, serverContext.InternalContextID);
			}
			if (objRef != null && objRef.GetServerIdentity().IsAllocated)
			{
				remotingProxy.SetSrvInfo(objRef.GetServerIdentity(), objRef.GetDomainID());
			}
			remotingProxy.Initialized = true;
			if (!serverType.IsContextful && !serverType.IsMarshalByRef && serverContext != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
			}
			return remotingProxy;
		}

		/// <summary>Checks the specified context.</summary>
		/// <param name="ctx">The context to be verified.</param>
		/// <param name="msg">The message for the remote call.</param>
		/// <returns>The specified context.</returns>
		// Token: 0x06005846 RID: 22598 RVA: 0x00138345 File Offset: 0x00136545
		[SecurityCritical]
		[ComVisible(true)]
		public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}

		/// <summary>Gets properties for a new context.</summary>
		/// <param name="msg">The message for which the context is to be retrieved.</param>
		// Token: 0x06005847 RID: 22599 RVA: 0x00138348 File Offset: 0x00136548
		[SecurityCritical]
		[ComVisible(true)]
		public void GetPropertiesForNewContext(IConstructionCallMessage msg)
		{
		}
	}
}
