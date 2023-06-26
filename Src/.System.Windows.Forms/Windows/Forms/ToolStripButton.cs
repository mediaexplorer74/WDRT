using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a selectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that can contain text and images.</summary>
	// Token: 0x020003B4 RID: 948
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripButton : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class.</summary>
		// Token: 0x06003EEA RID: 16106 RVA: 0x00110DB3 File Offset: 0x0010EFB3
		public ToolStripButton()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003EEB RID: 16107 RVA: 0x00110DC9 File Offset: 0x0010EFC9
		public ToolStripButton(string text)
			: base(text, null, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified image.</summary>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003EEC RID: 16108 RVA: 0x00110DE2 File Offset: 0x0010EFE2
		public ToolStripButton(Image image)
			: base(null, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text and image.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003EED RID: 16109 RVA: 0x00110DFB File Offset: 0x0010EFFB
		public ToolStripButton(string text, Image image)
			: base(text, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		// Token: 0x06003EEE RID: 16110 RVA: 0x00110E14 File Offset: 0x0010F014
		public ToolStripButton(string text, Image image, EventHandler onClick)
			: base(text, image, onClick)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class with the specified name that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06003EEF RID: 16111 RVA: 0x00110E2D File Offset: 0x0010F02D
		public ToolStripButton(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.Initialize();
		}

		/// <summary>Gets or sets a value indicating whether default or custom <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</summary>
		/// <returns>
		///   <see langword="true" /> if default <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x00110E48 File Offset: 0x0010F048
		// (set) Token: 0x06003EF1 RID: 16113 RVA: 0x00110E50 File Offset: 0x0010F050
		[DefaultValue(true)]
		public new bool AutoToolTip
		{
			get
			{
				return base.AutoToolTip;
			}
			set
			{
				base.AutoToolTip = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003EF2 RID: 16114 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool CanSelect
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003EF3 RID: 16115 RVA: 0x00110E59 File Offset: 0x0010F059
		// (set) Token: 0x06003EF4 RID: 16116 RVA: 0x00110E61 File Offset: 0x0010F061
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripButtonCheckOnClickDescr")]
		public bool CheckOnClick
		{
			get
			{
				return this.checkOnClick;
			}
			set
			{
				this.checkOnClick = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed or not pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed in or not pressed in; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x00110E6A File Offset: 0x0010F06A
		// (set) Token: 0x06003EF6 RID: 16118 RVA: 0x00110E75 File Offset: 0x0010F075
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripButtonCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.checkState > CheckState.Unchecked;
			}
			set
			{
				if (value != this.Checked)
				{
					this.CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
					base.InvokePaint();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> is in the pressed or not pressed state by default, or is in an indeterminate state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values. The default is <see langword="Unchecked" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.CheckState" /> values.</exception>
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003EF7 RID: 16119 RVA: 0x00110E93 File Offset: 0x0010F093
		// (set) Token: 0x06003EF8 RID: 16120 RVA: 0x00110E9C File Offset: 0x0010F09C
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		[SRDescription("CheckBoxCheckStateDescr")]
		public CheckState CheckState
		{
			get
			{
				return this.checkState;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
				}
				if (value != this.checkState)
				{
					this.checkState = value;
					base.Invalidate();
					this.OnCheckedChanged(EventArgs.Empty);
					this.OnCheckStateChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripButton.Checked" /> property changes.</summary>
		// Token: 0x14000305 RID: 773
		// (add) Token: 0x06003EF9 RID: 16121 RVA: 0x00110EFB File Offset: 0x0010F0FB
		// (remove) Token: 0x06003EFA RID: 16122 RVA: 0x00110F0E File Offset: 0x0010F10E
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripButton.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripButton.EventCheckedChanged, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripButton.CheckState" /> property changes.</summary>
		// Token: 0x14000306 RID: 774
		// (add) Token: 0x06003EFB RID: 16123 RVA: 0x00110F21 File Offset: 0x0010F121
		// (remove) Token: 0x06003EFC RID: 16124 RVA: 0x00110F34 File Offset: 0x0010F134
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripButton.EventCheckStateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripButton.EventCheckStateChanged, value);
			}
		}

		/// <summary>Gets a value indicating whether to display the ToolTip that is defined as the default.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x00012E4E File Offset: 0x0001104E
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x00110F47 File Offset: 0x0010F147
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x00110F4F File Offset: 0x0010F14F
		internal override int DeviceDpi
		{
			get
			{
				return base.DeviceDpi;
			}
			set
			{
				if (base.DeviceDpi != value)
				{
					base.DeviceDpi = value;
					this.standardButtonWidth = DpiHelper.LogicalToDeviceUnits(23, this.DeviceDpi);
				}
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripButton" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripButton" />.</returns>
		// Token: 0x06003F00 RID: 16128 RVA: 0x00110F74 File Offset: 0x0010F174
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripButton.ToolStripButtonAccessibleObject(this);
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripButton" /> can be fitted.</summary>
		/// <param name="constrainingSize">The specified area for a <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06003F01 RID: 16129 RVA: 0x00110F7C File Offset: 0x0010F17C
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			preferredSize.Width = Math.Max(preferredSize.Width, this.standardButtonWidth);
			return preferredSize;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x00110FAB File Offset: 0x0010F1AB
		private void Initialize()
		{
			base.SupportsSpaceKey = true;
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.standardButtonWidth = DpiHelper.LogicalToDeviceUnitsX(23);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F03 RID: 16131 RVA: 0x00110FC8 File Offset: 0x0010F1C8
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F04 RID: 16132 RVA: 0x00110FF8 File Offset: 0x0010F1F8
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange);
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.EventCheckStateChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F05 RID: 16133 RVA: 0x00111034 File Offset: 0x0010F234
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				renderer.DrawButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, base.InternalLayout.ImageRectangle)
					{
						ShiftOnPress = true
					});
				}
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
				{
					renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, base.InternalLayout.TextRectangle, this.ForeColor, this.Font, base.InternalLayout.TextFormat));
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F06 RID: 16134 RVA: 0x001110DB File Offset: 0x0010F2DB
		protected override void OnClick(EventArgs e)
		{
			if (this.checkOnClick)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
		}

		// Token: 0x04002491 RID: 9361
		private CheckState checkState;

		// Token: 0x04002492 RID: 9362
		private bool checkOnClick;

		// Token: 0x04002493 RID: 9363
		private const int STANDARD_BUTTON_WIDTH = 23;

		// Token: 0x04002494 RID: 9364
		private int standardButtonWidth = 23;

		// Token: 0x04002495 RID: 9365
		private static readonly object EventCheckedChanged = new object();

		// Token: 0x04002496 RID: 9366
		private static readonly object EventCheckStateChanged = new object();

		// Token: 0x020007FA RID: 2042
		[ComVisible(true)]
		internal class ToolStripButtonAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006E94 RID: 28308 RVA: 0x001950D5 File Offset: 0x001932D5
			public ToolStripButtonAccessibleObject(ToolStripButton ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006E95 RID: 28309 RVA: 0x001950E5 File Offset: 0x001932E5
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001828 RID: 6184
			// (get) Token: 0x06006E96 RID: 28310 RVA: 0x00195108 File Offset: 0x00193308
			public override AccessibleRole Role
			{
				get
				{
					if (this.ownerItem.CheckOnClick && AccessibilityImprovements.Level1)
					{
						return AccessibleRole.CheckButton;
					}
					return base.Role;
				}
			}

			// Token: 0x17001829 RID: 6185
			// (get) Token: 0x06006E97 RID: 28311 RVA: 0x00195128 File Offset: 0x00193328
			public override AccessibleStates State
			{
				get
				{
					if (this.ownerItem.Enabled && this.ownerItem.Checked)
					{
						return base.State | AccessibleStates.Checked;
					}
					if (AccessibilityImprovements.Level1 && !this.ownerItem.Enabled && this.ownerItem.Selected)
					{
						return base.State | AccessibleStates.Focused;
					}
					return base.State;
				}
			}

			// Token: 0x040042E7 RID: 17127
			private ToolStripButton ownerItem;
		}
	}
}
