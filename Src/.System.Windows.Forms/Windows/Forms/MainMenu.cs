using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents the menu structure of a form. Although <see cref="T:System.Windows.Forms.MenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MainMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.MainMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x020002E4 RID: 740
	[ToolboxItemFilter("System.Windows.Forms.MainMenu")]
	public class MainMenu : Menu
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class without any specified menu items.</summary>
		// Token: 0x06002EB9 RID: 11961 RVA: 0x000D32C0 File Offset: 0x000D14C0
		public MainMenu()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class with the specified container.</summary>
		/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> representing the container of the <see cref="T:System.Windows.Forms.MainMenu" />.</param>
		// Token: 0x06002EBA RID: 11962 RVA: 0x000D32D0 File Offset: 0x000D14D0
		public MainMenu(IContainer container)
			: this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that will be added to the <see cref="T:System.Windows.Forms.MainMenu" />.</param>
		// Token: 0x06002EBB RID: 11963 RVA: 0x000D32ED File Offset: 0x000D14ED
		public MainMenu(MenuItem[] items)
			: base(items)
		{
		}

		/// <summary>Occurs when the main menu collapses.</summary>
		// Token: 0x14000221 RID: 545
		// (add) Token: 0x06002EBC RID: 11964 RVA: 0x000D32FD File Offset: 0x000D14FD
		// (remove) Token: 0x06002EBD RID: 11965 RVA: 0x000D3316 File Offset: 0x000D1516
		[SRDescription("MainMenuCollapseDescr")]
		public event EventHandler Collapse
		{
			add
			{
				this.onCollapse = (EventHandler)Delegate.Combine(this.onCollapse, value);
			}
			remove
			{
				this.onCollapse = (EventHandler)Delegate.Remove(this.onCollapse, value);
			}
		}

		/// <summary>Gets or sets whether the text displayed by the control is displayed from right to left.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a valid member of the <see cref="T:System.Windows.Forms.RightToLeft" /> enumeration.</exception>
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x000D332F File Offset: 0x000D152F
		// (set) Token: 0x06002EBF RID: 11967 RVA: 0x000D3358 File Offset: 0x000D1558
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		[SRDescription("MenuRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				if (RightToLeft.Inherit != this.rightToLeft)
				{
					return this.rightToLeft;
				}
				if (this.form != null)
				{
					return this.form.RightToLeft;
				}
				return RightToLeft.Inherit;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("RightToLeft", (int)value, typeof(RightToLeft));
				}
				if (this.rightToLeft != value)
				{
					this.rightToLeft = value;
					base.UpdateRtl(value == RightToLeft.Yes);
				}
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x000D33A5 File Offset: 0x000D15A5
		internal override bool RenderIsRightToLeft
		{
			get
			{
				return this.RightToLeft == RightToLeft.Yes && (this.form == null || !this.form.IsMirrored);
			}
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.MainMenu" /> that is a duplicate of the current <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the cloned menu.</returns>
		// Token: 0x06002EC1 RID: 11969 RVA: 0x000D33CC File Offset: 0x000D15CC
		public virtual MainMenu CloneMenu()
		{
			MainMenu mainMenu = new MainMenu();
			mainMenu.CloneMenu(this);
			return mainMenu;
		}

		/// <summary>Creates a new handle to the Menu.</summary>
		/// <returns>A handle to the menu if the method succeeds; otherwise, <see langword="null" />.</returns>
		// Token: 0x06002EC2 RID: 11970 RVA: 0x000D33E7 File Offset: 0x000D15E7
		protected override IntPtr CreateMenuHandle()
		{
			return UnsafeNativeMethods.CreateMenu();
		}

		/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000D33EE File Offset: 0x000D15EE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.form != null && (this.ownerForm == null || this.form == this.ownerForm))
			{
				this.form.Menu = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Form" /> that contains this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that is the container for this control. Returns <see langword="null" /> if the <see cref="T:System.Windows.Forms.MainMenu" /> is not currently hosted on a form.</returns>
		// Token: 0x06002EC4 RID: 11972 RVA: 0x000D3424 File Offset: 0x000D1624
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public Form GetForm()
		{
			return this.form;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000D3424 File Offset: 0x000D1624
		internal Form GetFormUnsafe()
		{
			return this.form;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000D342C File Offset: 0x000D162C
		internal override void ItemsChanged(int change)
		{
			base.ItemsChanged(change);
			if (this.form != null)
			{
				this.form.MenuChanged(change, this);
			}
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000D344A File Offset: 0x000D164A
		internal virtual void ItemsChanged(int change, Menu menu)
		{
			if (this.form != null)
			{
				this.form.MenuChanged(change, menu);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MainMenu.Collapse" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002EC8 RID: 11976 RVA: 0x000D3461 File Offset: 0x000D1661
		protected internal virtual void OnCollapse(EventArgs e)
		{
			if (this.onCollapse != null)
			{
				this.onCollapse(this, e);
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000D3478 File Offset: 0x000D1678
		internal virtual bool ShouldSerializeRightToLeft()
		{
			return RightToLeft.Inherit != this.RightToLeft;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MainMenu" />.</returns>
		// Token: 0x06002ECA RID: 11978 RVA: 0x000D3486 File Offset: 0x000D1686
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0400135E RID: 4958
		internal Form form;

		// Token: 0x0400135F RID: 4959
		internal Form ownerForm;

		// Token: 0x04001360 RID: 4960
		private RightToLeft rightToLeft = RightToLeft.Inherit;

		// Token: 0x04001361 RID: 4961
		private EventHandler onCollapse;
	}
}
