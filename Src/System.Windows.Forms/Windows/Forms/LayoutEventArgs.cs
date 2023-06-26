using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Layout" /> event. This class cannot be inherited.</summary>
	// Token: 0x020002BC RID: 700
	public sealed class LayoutEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutEventArgs" /> class with the specified component and property affected.</summary>
		/// <param name="affectedComponent">The <see cref="T:System.ComponentModel.Component" /> affected by the layout change.</param>
		/// <param name="affectedProperty">The property affected by the layout change.</param>
		// Token: 0x06002B22 RID: 11042 RVA: 0x000C20D4 File Offset: 0x000C02D4
		public LayoutEventArgs(IComponent affectedComponent, string affectedProperty)
		{
			this.affectedComponent = affectedComponent;
			this.affectedProperty = affectedProperty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutEventArgs" /> class with the specified control and property affected.</summary>
		/// <param name="affectedControl">The <see cref="T:System.Windows.Forms.Control" /> affected by the layout change.</param>
		/// <param name="affectedProperty">The property affected by the layout change.</param>
		// Token: 0x06002B23 RID: 11043 RVA: 0x000C20EA File Offset: 0x000C02EA
		public LayoutEventArgs(Control affectedControl, string affectedProperty)
			: this(affectedControl, affectedProperty)
		{
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Component" /> affected by the layout change.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> representing the <see cref="T:System.ComponentModel.Component" /> affected by the layout change.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000C20F4 File Offset: 0x000C02F4
		public IComponent AffectedComponent
		{
			get
			{
				return this.affectedComponent;
			}
		}

		/// <summary>Gets the child control affected by the change.</summary>
		/// <returns>The child <see cref="T:System.Windows.Forms.Control" /> affected by the change.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000C20FC File Offset: 0x000C02FC
		public Control AffectedControl
		{
			get
			{
				return this.affectedComponent as Control;
			}
		}

		/// <summary>Gets the property affected by the change.</summary>
		/// <returns>The property affected by the change.</returns>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000C2109 File Offset: 0x000C0309
		public string AffectedProperty
		{
			get
			{
				return this.affectedProperty;
			}
		}

		// Token: 0x0400121C RID: 4636
		private readonly IComponent affectedComponent;

		// Token: 0x0400121D RID: 4637
		private readonly string affectedProperty;
	}
}
