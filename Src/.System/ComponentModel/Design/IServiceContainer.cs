using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a container for services.</summary>
	// Token: 0x020005F6 RID: 1526
	[ComVisible(true)]
	public interface IServiceContainer : IServiceProvider
	{
		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter.</param>
		// Token: 0x0600383F RID: 14399
		void AddService(Type serviceType, object serviceInstance);

		/// <summary>Adds the specified service to the service container, and optionally promotes the service to any parent service containers.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter.</param>
		/// <param name="promote">
		///   <see langword="true" /> to promote this request to any parent service containers; otherwise, <see langword="false" />.</param>
		// Token: 0x06003840 RID: 14400
		void AddService(Type serviceType, object serviceInstance, bool promote);

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested.</param>
		// Token: 0x06003841 RID: 14401
		void AddService(Type serviceType, ServiceCreatorCallback callback);

		/// <summary>Adds the specified service to the service container, and optionally promotes the service to parent service containers.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested.</param>
		/// <param name="promote">
		///   <see langword="true" /> to promote this request to any parent service containers; otherwise, <see langword="false" />.</param>
		// Token: 0x06003842 RID: 14402
		void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote);

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove.</param>
		// Token: 0x06003843 RID: 14403
		void RemoveService(Type serviceType);

		/// <summary>Removes the specified service type from the service container, and optionally promotes the service to parent service containers.</summary>
		/// <param name="serviceType">The type of service to remove.</param>
		/// <param name="promote">
		///   <see langword="true" /> to promote this request to any parent service containers; otherwise, <see langword="false" />.</param>
		// Token: 0x06003844 RID: 14404
		void RemoveService(Type serviceType, bool promote);
	}
}
