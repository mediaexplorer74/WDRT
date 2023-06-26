using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a simple implementation of the <see cref="T:System.ComponentModel.Design.IServiceContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x020005FE RID: 1534
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ServiceContainer : IServiceContainer, IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class.</summary>
		// Token: 0x06003869 RID: 14441 RVA: 0x000F0935 File Offset: 0x000EEB35
		public ServiceContainer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class using the specified parent service provider.</summary>
		/// <param name="parentProvider">A parent service provider.</param>
		// Token: 0x0600386A RID: 14442 RVA: 0x000F093D File Offset: 0x000EEB3D
		public ServiceContainer(IServiceProvider parentProvider)
		{
			this.parentProvider = parentProvider;
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000F094C File Offset: 0x000EEB4C
		private IServiceContainer Container
		{
			get
			{
				IServiceContainer serviceContainer = null;
				if (this.parentProvider != null)
				{
					serviceContainer = (IServiceContainer)this.parentProvider.GetService(typeof(IServiceContainer));
				}
				return serviceContainer;
			}
		}

		/// <summary>Gets the default services implemented directly by <see cref="T:System.ComponentModel.Design.ServiceContainer" />.</summary>
		/// <returns>The default services.</returns>
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000F097F File Offset: 0x000EEB7F
		protected virtual Type[] DefaultServices
		{
			get
			{
				return ServiceContainer._defaultServices;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x000F0986 File Offset: 0x000EEB86
		private ServiceContainer.ServiceCollection<object> Services
		{
			get
			{
				if (this.services == null)
				{
					this.services = new ServiceContainer.ServiceCollection<object>();
				}
				return this.services;
			}
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="serviceInstance">An instance of the service to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x0600386E RID: 14446 RVA: 0x000F09A1 File Offset: 0x000EEBA1
		public void AddService(Type serviceType, object serviceInstance)
		{
			this.AddService(serviceType, serviceInstance, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter.</param>
		/// <param name="promote">
		///   <see langword="true" /> if this service should be added to any parent service containers; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x0600386F RID: 14447 RVA: 0x000F09AC File Offset: 0x000EEBAC
		public virtual void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, serviceInstance, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			if (!(serviceInstance is ServiceCreatorCallback) && !serviceInstance.GetType().IsCOMObject && !serviceType.IsAssignableFrom(serviceInstance.GetType()))
			{
				throw new ArgumentException(SR.GetString("ErrorInvalidServiceInstance", new object[] { serviceType.FullName }));
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.GetString("ErrorServiceExists", new object[] { serviceType.FullName }), "serviceType");
			}
			this.Services[serviceType] = serviceInstance;
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06003870 RID: 14448 RVA: 0x000F0A73 File Offset: 0x000EEC73
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.AddService(serviceType, callback, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add.</param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested.</param>
		/// <param name="promote">
		///   <see langword="true" /> if this service should be added to any parent service containers; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06003871 RID: 14449 RVA: 0x000F0A80 File Offset: 0x000EEC80
		public virtual void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, callback, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.GetString("ErrorServiceExists", new object[] { serviceType.FullName }), "serviceType");
			}
			this.Services[serviceType] = callback;
		}

		/// <summary>Disposes this service container.</summary>
		// Token: 0x06003872 RID: 14450 RVA: 0x000F0B05 File Offset: 0x000EED05
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Disposes this service container.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> is in the process of being disposed of; otherwise, <see langword="false" />.</param>
		// Token: 0x06003873 RID: 14451 RVA: 0x000F0B10 File Offset: 0x000EED10
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServiceContainer.ServiceCollection<object> serviceCollection = this.services;
				this.services = null;
				if (serviceCollection != null)
				{
					foreach (object obj in serviceCollection.Values)
					{
						if (obj is IDisposable)
						{
							((IDisposable)obj).Dispose();
						}
					}
				}
			}
		}

		/// <summary>Gets the requested service.</summary>
		/// <param name="serviceType">The type of service to retrieve.</param>
		/// <returns>An instance of the service if it could be found, or <see langword="null" /> if it could not be found.</returns>
		// Token: 0x06003874 RID: 14452 RVA: 0x000F0B84 File Offset: 0x000EED84
		public virtual object GetService(Type serviceType)
		{
			object obj = null;
			Type[] defaultServices = this.DefaultServices;
			for (int i = 0; i < defaultServices.Length; i++)
			{
				if (serviceType.IsEquivalentTo(defaultServices[i]))
				{
					obj = this;
					break;
				}
			}
			if (obj == null)
			{
				this.Services.TryGetValue(serviceType, out obj);
			}
			if (obj is ServiceCreatorCallback)
			{
				obj = ((ServiceCreatorCallback)obj)(this, serviceType);
				if (obj != null && !obj.GetType().IsCOMObject && !serviceType.IsAssignableFrom(obj.GetType()))
				{
					obj = null;
				}
				this.Services[serviceType] = obj;
			}
			if (obj == null && this.parentProvider != null)
			{
				obj = this.parentProvider.GetService(serviceType);
			}
			return obj;
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is <see langword="null" />.</exception>
		// Token: 0x06003875 RID: 14453 RVA: 0x000F0C25 File Offset: 0x000EEE25
		public void RemoveService(Type serviceType)
		{
			this.RemoveService(serviceType, false);
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove.</param>
		/// <param name="promote">
		///   <see langword="true" /> if this service should be removed from any parent service containers; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is <see langword="null" />.</exception>
		// Token: 0x06003876 RID: 14454 RVA: 0x000F0C30 File Offset: 0x000EEE30
		public virtual void RemoveService(Type serviceType, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.RemoveService(serviceType, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			this.Services.Remove(serviceType);
		}

		// Token: 0x04002B0B RID: 11019
		private ServiceContainer.ServiceCollection<object> services;

		// Token: 0x04002B0C RID: 11020
		private IServiceProvider parentProvider;

		// Token: 0x04002B0D RID: 11021
		private static Type[] _defaultServices = new Type[]
		{
			typeof(IServiceContainer),
			typeof(ServiceContainer)
		};

		// Token: 0x04002B0E RID: 11022
		private static TraceSwitch TRACESERVICE = new TraceSwitch("TRACESERVICE", "ServiceProvider: Trace service provider requests.");

		// Token: 0x020008AC RID: 2220
		private sealed class ServiceCollection<T> : Dictionary<Type, T>
		{
			// Token: 0x060045F7 RID: 17911 RVA: 0x00123D23 File Offset: 0x00121F23
			public ServiceCollection()
				: base(ServiceContainer.ServiceCollection<T>.serviceTypeComparer)
			{
			}

			// Token: 0x040037E7 RID: 14311
			private static ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer serviceTypeComparer = new ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer();

			// Token: 0x02000936 RID: 2358
			private sealed class EmbeddedTypeAwareTypeComparer : IEqualityComparer<Type>
			{
				// Token: 0x060046C1 RID: 18113 RVA: 0x0012729E File Offset: 0x0012549E
				public bool Equals(Type x, Type y)
				{
					return x.IsEquivalentTo(y);
				}

				// Token: 0x060046C2 RID: 18114 RVA: 0x001272A7 File Offset: 0x001254A7
				public int GetHashCode(Type obj)
				{
					return obj.FullName.GetHashCode();
				}
			}
		}
	}
}
