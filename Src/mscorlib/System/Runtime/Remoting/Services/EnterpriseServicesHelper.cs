using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Services
{
	/// <summary>Provides APIs that are needed for communication and operation with unmanaged classes outside of the <see cref="T:System.AppDomain" />. This class cannot be inherited.</summary>
	// Token: 0x02000803 RID: 2051
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class EnterpriseServicesHelper
	{
		/// <summary>Wraps the specified <see langword="IUnknown" /> COM interface with a Runtime Callable Wrapper (RCW).</summary>
		/// <param name="punk">A pointer to the <see langword="IUnknown" /> COM interface to wrap.</param>
		/// <returns>The RCW where the specified <see langword="IUnknown" /> is wrapped.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x06005886 RID: 22662 RVA: 0x00139634 File Offset: 0x00137834
		[SecurityCritical]
		public static object WrapIUnknownWithComObject(IntPtr punk)
		{
			return Marshal.InternalWrapIUnknownWithComObject(punk);
		}

		/// <summary>Constructs a <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> from the specified <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="ctorMsg">A construction call to the object from which the new <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> instance is returning.</param>
		/// <param name="retObj">A <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the object that is constructed with the construction call in <paramref name="ctorMsg" />.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> returned from the construction call that is specified in the <paramref name="ctorMsg" /> parameter.</returns>
		// Token: 0x06005887 RID: 22663 RVA: 0x0013963C File Offset: 0x0013783C
		[ComVisible(true)]
		public static IConstructionReturnMessage CreateConstructionReturnMessage(IConstructionCallMessage ctorMsg, MarshalByRefObject retObj)
		{
			return new ConstructorReturnMessage(retObj, null, 0, null, ctorMsg);
		}

		/// <summary>Switches a COM Callable Wrapper (CCW) from one instance of a class to another instance of the same class.</summary>
		/// <param name="oldcp">A proxy that represents the old instance of a class that is referenced by a CCW.</param>
		/// <param name="newcp">A proxy that represents the new instance of a class that is referenced by a CCW.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have UnmanagedCode permission.</exception>
		// Token: 0x06005888 RID: 22664 RVA: 0x00139658 File Offset: 0x00137858
		[SecurityCritical]
		public static void SwitchWrappers(RealProxy oldcp, RealProxy newcp)
		{
			object transparentProxy = oldcp.GetTransparentProxy();
			object transparentProxy2 = newcp.GetTransparentProxy();
			IntPtr serverContextForProxy = RemotingServices.GetServerContextForProxy(transparentProxy);
			IntPtr serverContextForProxy2 = RemotingServices.GetServerContextForProxy(transparentProxy2);
			Marshal.InternalSwitchCCW(transparentProxy, transparentProxy2);
		}
	}
}
