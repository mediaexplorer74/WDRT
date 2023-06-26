using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a callback mechanism that can create an instance of a service on demand.</summary>
	/// <param name="container">The service container that requested the creation of the service.</param>
	/// <param name="serviceType">The type of service to create.</param>
	/// <returns>The service specified by <paramref name="serviceType" />, or <see langword="null" /> if the service could not be created.</returns>
	// Token: 0x020005FD RID: 1533
	// (Invoke) Token: 0x06003866 RID: 14438
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate object ServiceCreatorCallback(IServiceContainer container, Type serviceType);
}
