using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Encapsulates zero or more components.</summary>
	// Token: 0x0200052E RID: 1326
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Container : IContainer, IDisposable
	{
		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.ComponentModel.Container" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06003215 RID: 12821 RVA: 0x000E07C8 File Offset: 0x000DE9C8
		~Container()
		{
			this.Dispose(false);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" />. The component is unnamed.</summary>
		/// <param name="component">The component to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is <see langword="null" />.</exception>
		// Token: 0x06003216 RID: 12822 RVA: 0x000E07F8 File Offset: 0x000DE9F8
		public virtual void Add(IComponent component)
		{
			this.Add(component, null);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" /> and assigns it a name.</summary>
		/// <param name="component">The component to add.</param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.  
		///  -or-  
		///  <see langword="null" />, which leaves the component unnamed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x06003217 RID: 12823 RVA: 0x000E0804 File Offset: 0x000DEA04
		public virtual void Add(IComponent component, string name)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site == null || site.Container != this)
					{
						if (this.sites == null)
						{
							this.sites = new ISite[4];
						}
						else
						{
							this.ValidateName(component, name);
							if (this.sites.Length == this.siteCount)
							{
								ISite[] array = new ISite[this.siteCount * 2];
								Array.Copy(this.sites, 0, array, 0, this.siteCount);
								this.sites = array;
							}
						}
						if (site != null)
						{
							site.Container.Remove(component);
						}
						ISite site2 = this.CreateSite(component, name);
						ISite[] array2 = this.sites;
						int num = this.siteCount;
						this.siteCount = num + 1;
						array2[num] = site2;
						component.Site = site2;
						this.components = null;
					}
				}
			}
		}

		/// <summary>Creates a site <see cref="T:System.ComponentModel.ISite" /> for the given <see cref="T:System.ComponentModel.IComponent" /> and assigns the given name to the site.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for.</param>
		/// <param name="name">The name to assign to <paramref name="component" />, or <see langword="null" /> to skip the name assignment.</param>
		/// <returns>The newly created site.</returns>
		// Token: 0x06003218 RID: 12824 RVA: 0x000E08FC File Offset: 0x000DEAFC
		protected virtual ISite CreateSite(IComponent component, string name)
		{
			return new Container.Site(component, this, name);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Container" />.</summary>
		// Token: 0x06003219 RID: 12825 RVA: 0x000E0906 File Offset: 0x000DEB06
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Container" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600321A RID: 12826 RVA: 0x000E0918 File Offset: 0x000DEB18
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				object obj = this.syncObj;
				lock (obj)
				{
					while (this.siteCount > 0)
					{
						ISite[] array = this.sites;
						int num = this.siteCount - 1;
						this.siteCount = num;
						ISite site = array[num];
						site.Component.Site = null;
						site.Component.Dispose();
					}
					this.sites = null;
					this.components = null;
				}
			}
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve.</param>
		/// <returns>An <see cref="T:System.Object" /> implementing the requested service, or <see langword="null" /> if the service cannot be resolved.</returns>
		// Token: 0x0600321B RID: 12827 RVA: 0x000E09A0 File Offset: 0x000DEBA0
		protected virtual object GetService(Type service)
		{
			if (!(service == typeof(IContainer)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>A collection that contains the components in the <see cref="T:System.ComponentModel.Container" />.</returns>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000E09B8 File Offset: 0x000DEBB8
		public virtual ComponentCollection Components
		{
			get
			{
				object obj = this.syncObj;
				ComponentCollection componentCollection2;
				lock (obj)
				{
					if (this.components == null)
					{
						IComponent[] array = new IComponent[this.siteCount];
						for (int i = 0; i < this.siteCount; i++)
						{
							array[i] = this.sites[i].Component;
						}
						this.components = new ComponentCollection(array);
						if (this.filter == null && this.checkedFilter)
						{
							this.checkedFilter = false;
						}
					}
					if (!this.checkedFilter)
					{
						this.filter = this.GetService(typeof(ContainerFilterService)) as ContainerFilterService;
						this.checkedFilter = true;
					}
					if (this.filter != null)
					{
						ComponentCollection componentCollection = this.filter.FilterComponents(this.components);
						if (componentCollection != null)
						{
							this.components = componentCollection;
						}
					}
					componentCollection2 = this.components;
				}
				return componentCollection2;
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="component">The component to remove.</param>
		// Token: 0x0600321D RID: 12829 RVA: 0x000E0AA8 File Offset: 0x000DECA8
		public virtual void Remove(IComponent component)
		{
			this.Remove(component, false);
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000E0AB4 File Offset: 0x000DECB4
		private void Remove(IComponent component, bool preserveSite)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null && site.Container == this)
					{
						if (!preserveSite)
						{
							component.Site = null;
						}
						for (int i = 0; i < this.siteCount; i++)
						{
							if (this.sites[i] == site)
							{
								this.siteCount--;
								Array.Copy(this.sites, i + 1, this.sites, i, this.siteCount - i);
								this.sites[this.siteCount] = null;
								this.components = null;
								break;
							}
						}
					}
				}
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" /> without setting <see cref="P:System.ComponentModel.IComponent.Site" /> to <see langword="null" />.</summary>
		/// <param name="component">The component to remove.</param>
		// Token: 0x0600321F RID: 12831 RVA: 0x000E0B74 File Offset: 0x000DED74
		protected void RemoveWithoutUnsiting(IComponent component)
		{
			this.Remove(component, true);
		}

		/// <summary>Determines whether the component name is unique for this container.</summary>
		/// <param name="component">The named component.</param>
		/// <param name="name">The component name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x06003220 RID: 12832 RVA: 0x000E0B80 File Offset: 0x000DED80
		protected virtual void ValidateName(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (name != null)
			{
				for (int i = 0; i < Math.Min(this.siteCount, this.sites.Length); i++)
				{
					ISite site = this.sites[i];
					if (site != null && site.Name != null && string.Equals(site.Name, name, StringComparison.OrdinalIgnoreCase) && site.Component != component)
					{
						InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(site.Component)[typeof(InheritanceAttribute)];
						if (inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
						{
							throw new ArgumentException(SR.GetString("DuplicateComponentName", new object[] { name }));
						}
					}
				}
			}
		}

		// Token: 0x0400294B RID: 10571
		private ISite[] sites;

		// Token: 0x0400294C RID: 10572
		private int siteCount;

		// Token: 0x0400294D RID: 10573
		private ComponentCollection components;

		// Token: 0x0400294E RID: 10574
		private ContainerFilterService filter;

		// Token: 0x0400294F RID: 10575
		private bool checkedFilter;

		// Token: 0x04002950 RID: 10576
		private object syncObj = new object();

		// Token: 0x0200088E RID: 2190
		private class Site : ISite, IServiceProvider
		{
			// Token: 0x06004564 RID: 17764 RVA: 0x001210E8 File Offset: 0x0011F2E8
			internal Site(IComponent component, Container container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000FB1 RID: 4017
			// (get) Token: 0x06004565 RID: 17765 RVA: 0x00121105 File Offset: 0x0011F305
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000FB2 RID: 4018
			// (get) Token: 0x06004566 RID: 17766 RVA: 0x0012110D File Offset: 0x0011F30D
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x06004567 RID: 17767 RVA: 0x00121115 File Offset: 0x0011F315
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x17000FB3 RID: 4019
			// (get) Token: 0x06004568 RID: 17768 RVA: 0x00121137 File Offset: 0x0011F337
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000FB4 RID: 4020
			// (get) Token: 0x06004569 RID: 17769 RVA: 0x0012113A File Offset: 0x0011F33A
			// (set) Token: 0x0600456A RID: 17770 RVA: 0x00121142 File Offset: 0x0011F342
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (value == null || this.name == null || !value.Equals(this.name))
					{
						this.container.ValidateName(this.component, value);
						this.name = value;
					}
				}
			}

			// Token: 0x040037A4 RID: 14244
			private IComponent component;

			// Token: 0x040037A5 RID: 14245
			private Container container;

			// Token: 0x040037A6 RID: 14246
			private string name;
		}
	}
}
