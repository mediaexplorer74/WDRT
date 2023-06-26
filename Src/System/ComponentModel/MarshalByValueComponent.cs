using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Implements <see cref="T:System.ComponentModel.IComponent" /> and provides the base implementation for remotable components that are marshaled by value (a copy of the serialized object is passed).</summary>
	// Token: 0x0200058C RID: 1420
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerCategory("Component")]
	[TypeConverter(typeof(ComponentConverter))]
	public class MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{
		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06003452 RID: 13394 RVA: 0x000E4700 File Offset: 0x000E2900
		~MarshalByValueComponent()
		{
			this.Dispose(false);
		}

		/// <summary>Adds an event handler to listen to the <see cref="E:System.ComponentModel.MarshalByValueComponent.Disposed" /> event on the component.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06003453 RID: 13395 RVA: 0x000E4730 File Offset: 0x000E2930
		// (remove) Token: 0x06003454 RID: 13396 RVA: 0x000E4743 File Offset: 0x000E2943
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(MarshalByValueComponent.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(MarshalByValueComponent.EventDisposed, value);
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000E4756 File Offset: 0x000E2956
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		/// <summary>Gets or sets the site of the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.ISite" /> interface that represents the site of the component.</returns>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000E4771 File Offset: 0x000E2971
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x000E4779 File Offset: 0x000E2979
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" />.</summary>
		// Token: 0x06003458 RID: 13400 RVA: 0x000E4782 File Offset: 0x000E2982
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003459 RID: 13401 RVA: 0x000E4794 File Offset: 0x000E2994
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.site != null && this.site.Container != null)
					{
						this.site.Container.Remove(this);
					}
					if (this.events != null)
					{
						EventHandler eventHandler = (EventHandler)this.events[MarshalByValueComponent.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		/// <summary>Gets the container for the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.IContainer" /> interface that represents the component's container, or <see langword="null" /> if the component does not have a site.</returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000E4820 File Offset: 0x000E2A20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IContainer Container
		{
			get
			{
				ISite site = this.site;
				if (site != null)
				{
					return site.Container;
				}
				return null;
			}
		}

		/// <summary>Gets the implementer of the <see cref="T:System.IServiceProvider" />.</summary>
		/// <param name="service">A <see cref="T:System.Type" /> that represents the type of service you want.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the implementer of the <see cref="T:System.IServiceProvider" />.</returns>
		// Token: 0x0600345B RID: 13403 RVA: 0x000E483F File Offset: 0x000E2A3F
		public virtual object GetService(Type service)
		{
			if (this.site != null)
			{
				return this.site.GetService(service);
			}
			return null;
		}

		/// <summary>Gets a value indicating whether the component is currently in design mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the component is in design mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000E4858 File Offset: 0x000E2A58
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any.  
		///  <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x0600345D RID: 13405 RVA: 0x000E4878 File Offset: 0x000E2A78
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x040029DD RID: 10717
		private static readonly object EventDisposed = new object();

		// Token: 0x040029DE RID: 10718
		private ISite site;

		// Token: 0x040029DF RID: 10719
		private EventHandlerList events;
	}
}
