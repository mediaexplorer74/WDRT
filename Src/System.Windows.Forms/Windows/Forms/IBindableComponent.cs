using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Enables a non-control component to emulate the data-binding behavior of a Windows Forms control.</summary>
	// Token: 0x02000286 RID: 646
	public interface IBindableComponent : IComponent, IDisposable
	{
		/// <summary>Gets the collection of data-binding objects for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002987 RID: 10631
		ControlBindingsCollection DataBindings { get; }

		/// <summary>Gets or sets the collection of currency managers for the <see cref="T:System.Windows.Forms.IBindableComponent" />.</summary>
		/// <returns>The collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</returns>
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002988 RID: 10632
		// (set) Token: 0x06002989 RID: 10633
		BindingContext BindingContext { get; set; }
	}
}
