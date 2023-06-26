using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.INestedContainer" /> interface, which enables containers to have an owning component.</summary>
	// Token: 0x02000592 RID: 1426
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class NestedContainer : Container, INestedContainer, IContainer, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NestedContainer" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is <see langword="null" />.</exception>
		// Token: 0x060034F2 RID: 13554 RVA: 0x000E6ED1 File Offset: 0x000E50D1
		public NestedContainer(IComponent owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._owner.Disposed += this.OnOwnerDisposed;
		}

		/// <summary>Gets the owning component for this nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</returns>
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000E6F05 File Offset: 0x000E5105
		public IComponent Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Gets the name of the owning component.</summary>
		/// <returns>The name of the owning component.</returns>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000E6F10 File Offset: 0x000E5110
		protected virtual string OwnerName
		{
			get
			{
				string text = null;
				if (this._owner != null && this._owner.Site != null)
				{
					INestedSite nestedSite = this._owner.Site as INestedSite;
					if (nestedSite != null)
					{
						text = nestedSite.FullName;
					}
					else
					{
						text = this._owner.Site.Name;
					}
				}
				return text;
			}
		}

		/// <summary>Creates a site for the component within the container.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for.</param>
		/// <param name="name">The name to assign to <paramref name="component" />, or <see langword="null" /> to skip the name assignment.</param>
		/// <returns>The newly created <see cref="T:System.ComponentModel.ISite" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is <see langword="null" />.</exception>
		// Token: 0x060034F5 RID: 13557 RVA: 0x000E6F63 File Offset: 0x000E5163
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new NestedContainer.Site(component, this, name);
		}

		/// <summary>Releases the resources used by the nested container.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060034F6 RID: 13558 RVA: 0x000E6F7B File Offset: 0x000E517B
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._owner.Disposed -= this.OnOwnerDisposed;
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve.</param>
		/// <returns>An <see cref="T:System.Object" /> that implements the requested service, or <see langword="null" /> if the service cannot be resolved.</returns>
		// Token: 0x060034F7 RID: 13559 RVA: 0x000E6F9E File Offset: 0x000E519E
		protected override object GetService(Type service)
		{
			if (service == typeof(INestedContainer))
			{
				return this;
			}
			return base.GetService(service);
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000E6FBB File Offset: 0x000E51BB
		private void OnOwnerDisposed(object sender, EventArgs e)
		{
			base.Dispose();
		}

		// Token: 0x04002A1D RID: 10781
		private IComponent _owner;

		// Token: 0x02000897 RID: 2199
		private class Site : INestedSite, ISite, IServiceProvider
		{
			// Token: 0x0600457B RID: 17787 RVA: 0x001225C2 File Offset: 0x001207C2
			internal Site(IComponent component, NestedContainer container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000FB6 RID: 4022
			// (get) Token: 0x0600457C RID: 17788 RVA: 0x001225DF File Offset: 0x001207DF
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000FB7 RID: 4023
			// (get) Token: 0x0600457D RID: 17789 RVA: 0x001225E7 File Offset: 0x001207E7
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x0600457E RID: 17790 RVA: 0x001225EF File Offset: 0x001207EF
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x17000FB8 RID: 4024
			// (get) Token: 0x0600457F RID: 17791 RVA: 0x00122614 File Offset: 0x00120814
			public bool DesignMode
			{
				get
				{
					IComponent owner = this.container.Owner;
					return owner != null && owner.Site != null && owner.Site.DesignMode;
				}
			}

			// Token: 0x17000FB9 RID: 4025
			// (get) Token: 0x06004580 RID: 17792 RVA: 0x00122648 File Offset: 0x00120848
			public string FullName
			{
				get
				{
					if (this.name != null)
					{
						string ownerName = this.container.OwnerName;
						string text = this.name;
						if (ownerName != null)
						{
							text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { ownerName, text });
						}
						return text;
					}
					return this.name;
				}
			}

			// Token: 0x17000FBA RID: 4026
			// (get) Token: 0x06004581 RID: 17793 RVA: 0x00122699 File Offset: 0x00120899
			// (set) Token: 0x06004582 RID: 17794 RVA: 0x001226A1 File Offset: 0x001208A1
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

			// Token: 0x040037C3 RID: 14275
			private IComponent component;

			// Token: 0x040037C4 RID: 14276
			private NestedContainer container;

			// Token: 0x040037C5 RID: 14277
			private string name;
		}
	}
}
