using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.IComponent" /> interface and enables object sharing between applications.</summary>
	// Token: 0x0200052A RID: 1322
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DesignerCategory("Component")]
	public class Component : MarshalByRefObject, IComponent, IDisposable
	{
		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.ComponentModel.Component" /> is reclaimed by garbage collection.</summary>
		// Token: 0x060031F8 RID: 12792 RVA: 0x000E0134 File Offset: 0x000DE334
		~Component()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a value indicating whether the component can raise an event.</summary>
		/// <returns>
		///   <see langword="true" /> if the component can raise events; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000E0164 File Offset: 0x000DE364
		protected virtual bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000E0167 File Offset: 0x000DE367
		internal bool CanRaiseEventsInternal
		{
			get
			{
				return this.CanRaiseEvents;
			}
		}

		/// <summary>Occurs when the component is disposed by a call to the <see cref="M:System.ComponentModel.Component.Dispose" /> method.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060031FB RID: 12795 RVA: 0x000E016F File Offset: 0x000DE36F
		// (remove) Token: 0x060031FC RID: 12796 RVA: 0x000E0182 File Offset: 0x000DE382
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(Component.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(Component.EventDisposed, value);
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000E0195 File Offset: 0x000DE395
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList(this);
				}
				return this.events;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000E01B1 File Offset: 0x000DE3B1
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x000E01B9 File Offset: 0x000DE3B9
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

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.</summary>
		// Token: 0x06003200 RID: 12800 RVA: 0x000E01C2 File Offset: 0x000DE3C2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003201 RID: 12801 RVA: 0x000E01D4 File Offset: 0x000DE3D4
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
						EventHandler eventHandler = (EventHandler)this.events[Component.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000E0260 File Offset: 0x000DE460
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IContainer Container
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

		/// <summary>Returns an object that represents a service provided by the <see cref="T:System.ComponentModel.Component" /> or by its <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="service">A service provided by the <see cref="T:System.ComponentModel.Component" />.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
		// Token: 0x06003203 RID: 12803 RVA: 0x000E0280 File Offset: 0x000DE480
		protected virtual object GetService(Type service)
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.GetService(service);
			}
			return null;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.ComponentModel.Component" /> is currently in design mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Component" /> is in design mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000E02A0 File Offset: 0x000DE4A0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06003205 RID: 12805 RVA: 0x000E02C0 File Offset: 0x000DE4C0
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x04002946 RID: 10566
		private static readonly object EventDisposed = new object();

		// Token: 0x04002947 RID: 10567
		private ISite site;

		// Token: 0x04002948 RID: 10568
		private EventHandlerList events;
	}
}
