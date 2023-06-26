using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Hosts custom controls or Windows Forms controls.</summary>
	// Token: 0x020003B6 RID: 950
	public class ToolStripControlHost : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class that hosts the specified control.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> hosted by this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">The control referred to by the <paramref name="c" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003F5F RID: 16223 RVA: 0x00111814 File Offset: 0x0010FA14
		public ToolStripControlHost(Control c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "ControlCannotBeNull");
			}
			this.control = c;
			this.SyncControlParent();
			c.Visible = true;
			this.SetBounds(c.Bounds);
			Rectangle bounds = this.Bounds;
			CommonProperties.UpdateSpecifiedBounds(c, bounds.X, bounds.Y, bounds.Width, bounds.Height);
			if (AccessibilityImprovements.Level3)
			{
				c.ToolStripControlHost = this;
			}
			this.OnSubscribeControlEvents(c);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class that hosts the specified control and that has the specified name.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> hosted by this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripControlHost" />.</param>
		// Token: 0x06003F60 RID: 16224 RVA: 0x001118A0 File Offset: 0x0010FAA0
		public ToolStripControlHost(Control c, string name)
			: this(c)
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x001118B0 File Offset: 0x0010FAB0
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x001118BD File Offset: 0x0010FABD
		public override Color BackColor
		{
			get
			{
				return this.Control.BackColor;
			}
			set
			{
				this.Control.BackColor = value;
			}
		}

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x001118CB File Offset: 0x0010FACB
		// (set) Token: 0x06003F64 RID: 16228 RVA: 0x001118D8 File Offset: 0x0010FAD8
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		[DefaultValue(null)]
		public override Image BackgroundImage
		{
			get
			{
				return this.Control.BackgroundImage;
			}
			set
			{
				this.Control.BackgroundImage = value;
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see langword="ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" />:  
		///
		/// <see cref="F:System.Windows.Forms.ImageLayout.Center" /><see cref="F:System.Windows.Forms.ImageLayout.None" /><see cref="F:System.Windows.Forms.ImageLayout.Stretch" /><see cref="F:System.Windows.Forms.ImageLayout.Tile" /> (default)  
		///
		/// <see cref="F:System.Windows.Forms.ImageLayout.Zoom" /></returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x001118E6 File Offset: 0x0010FAE6
		// (set) Token: 0x06003F66 RID: 16230 RVA: 0x001118F3 File Offset: 0x0010FAF3
		[SRCategory("CatAppearance")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return this.Control.BackgroundImageLayout;
			}
			set
			{
				this.Control.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets a value indicating whether the control can be selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can be selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x00111901 File Offset: 0x0010FB01
		public override bool CanSelect
		{
			get
			{
				return this.control != null && (base.DesignMode || this.Control.CanSelect);
			}
		}

		/// <summary>Gets or sets a value indicating whether the hosted control causes and raises validation events on other controls when the hosted control receives focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the hosted control causes and raises validation events on other controls when the hosted control receives focus; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x00111922 File Offset: 0x0010FB22
		// (set) Token: 0x06003F69 RID: 16233 RVA: 0x0011192F File Offset: 0x0010FB2F
		[SRCategory("CatFocus")]
		[DefaultValue(true)]
		[SRDescription("ControlCausesValidationDescr")]
		public bool CausesValidation
		{
			get
			{
				return this.Control.CausesValidation;
			}
			set
			{
				this.Control.CausesValidation = value;
			}
		}

		/// <summary>Gets or sets the alignment of the control on the form.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleCenter" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.ToolStripControlHost.ControlAlign" /> property is set to a value that is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x0011193D File Offset: 0x0010FB3D
		// (set) Token: 0x06003F6B RID: 16235 RVA: 0x00111945 File Offset: 0x0010FB45
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Browsable(false)]
		public ContentAlignment ControlAlign
		{
			get
			{
				return this.controlAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.controlAlign != value)
				{
					this.controlAlign = value;
					this.OnBoundsChanged();
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</returns>
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x0011197B File Offset: 0x0010FB7B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x00111983 File Offset: 0x0010FB83
		internal AccessibleObject ControlAccessibilityObject
		{
			get
			{
				Control control = this.Control;
				if (control == null)
				{
					return null;
				}
				return control.AccessibilityObject;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x00111996 File Offset: 0x0010FB96
		protected override Size DefaultSize
		{
			get
			{
				if (this.Control != null)
				{
					return this.Control.Size;
				}
				return base.DefaultSize;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The display style of the object.</returns>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x001119B2 File Offset: 0x0010FBB2
		// (set) Token: 0x06003F70 RID: 16240 RVA: 0x001119BA File Offset: 0x0010FBBA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return base.DisplayStyle;
			}
			set
			{
				base.DisplayStyle = value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400030D RID: 781
		// (add) Token: 0x06003F71 RID: 16241 RVA: 0x001119C3 File Offset: 0x0010FBC3
		// (remove) Token: 0x06003F72 RID: 16242 RVA: 0x001119D6 File Offset: 0x0010FBD6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DisplayStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDisplayStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDisplayStyleChanged, value);
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if double clicking is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x001119E9 File Offset: 0x0010FBE9
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x001119F1 File Offset: 0x0010FBF1
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool DoubleClickEnabled
		{
			get
			{
				return base.DoubleClickEnabled;
			}
			set
			{
				base.DoubleClickEnabled = value;
			}
		}

		/// <summary>Gets or sets the font to be used on the hosted control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> for the hosted control.</returns>
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x001119FA File Offset: 0x0010FBFA
		// (set) Token: 0x06003F76 RID: 16246 RVA: 0x00111A07 File Offset: 0x0010FC07
		public override Font Font
		{
			get
			{
				return this.Control.Font;
			}
			set
			{
				this.Control.Font = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x00111A15 File Offset: 0x0010FC15
		// (set) Token: 0x06003F78 RID: 16248 RVA: 0x00111A22 File Offset: 0x0010FC22
		public override bool Enabled
		{
			get
			{
				return this.Control.Enabled;
			}
			set
			{
				this.Control.Enabled = value;
			}
		}

		/// <summary>Occurs when the hosted control is entered.</summary>
		// Token: 0x1400030E RID: 782
		// (add) Token: 0x06003F79 RID: 16249 RVA: 0x00111A30 File Offset: 0x0010FC30
		// (remove) Token: 0x06003F7A RID: 16250 RVA: 0x00111A43 File Offset: 0x0010FC43
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnEnterDescr")]
		public event EventHandler Enter
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventEnter, value);
			}
		}

		/// <summary>Gets a value indicating whether the control has input focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the control has input focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x00111A56 File Offset: 0x0010FC56
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public virtual bool Focused
		{
			get
			{
				return this.Control.Focused;
			}
		}

		/// <summary>Gets or sets the foreground color of the hosted control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of the hosted control.</returns>
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x00111A63 File Offset: 0x0010FC63
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x00111A70 File Offset: 0x0010FC70
		public override Color ForeColor
		{
			get
			{
				return this.Control.ForeColor;
			}
			set
			{
				this.Control.ForeColor = value;
			}
		}

		/// <summary>Occurs when the hosted control receives focus.</summary>
		// Token: 0x1400030F RID: 783
		// (add) Token: 0x06003F7E RID: 16254 RVA: 0x00111A7E File Offset: 0x0010FC7E
		// (remove) Token: 0x06003F7F RID: 16255 RVA: 0x00111A91 File Offset: 0x0010FC91
		[SRCategory("CatFocus")]
		[SRDescription("ToolStripItemOnGotFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler GotFocus
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventGotFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventGotFocus, value);
			}
		}

		/// <summary>The image associated with the object.</summary>
		/// <returns>The image of the hosted control.</returns>
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x00111AA4 File Offset: 0x0010FCA4
		// (set) Token: 0x06003F81 RID: 16257 RVA: 0x00111AAC File Offset: 0x0010FCAC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if an image on a ToolStripItem is automatically resized to fit in a container; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x00111AB5 File Offset: 0x0010FCB5
		// (set) Token: 0x06003F83 RID: 16259 RVA: 0x00111ABD File Offset: 0x0010FCBD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return base.ImageScaling;
			}
			set
			{
				base.ImageScaling = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The transparent color of the image.</returns>
		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x00111AC6 File Offset: 0x0010FCC6
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x00111ACE File Offset: 0x0010FCCE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ImageTransparentColor
		{
			get
			{
				return base.ImageTransparentColor;
			}
			set
			{
				base.ImageTransparentColor = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The image alignment for the object.</returns>
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x00111AD7 File Offset: 0x0010FCD7
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x00111ADF File Offset: 0x0010FCDF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
			set
			{
				base.ImageAlign = value;
			}
		}

		/// <summary>Occurs when the input focus leaves the hosted control.</summary>
		// Token: 0x14000310 RID: 784
		// (add) Token: 0x06003F88 RID: 16264 RVA: 0x00111AE8 File Offset: 0x0010FCE8
		// (remove) Token: 0x06003F89 RID: 16265 RVA: 0x00111AFB File Offset: 0x0010FCFB
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLeaveDescr")]
		public event EventHandler Leave
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventLeave, value);
			}
		}

		/// <summary>Occurs when the hosted control loses focus.</summary>
		// Token: 0x14000311 RID: 785
		// (add) Token: 0x06003F8A RID: 16266 RVA: 0x00111B0E File Offset: 0x0010FD0E
		// (remove) Token: 0x06003F8B RID: 16267 RVA: 0x00111B21 File Offset: 0x0010FD21
		[SRCategory("CatFocus")]
		[SRDescription("ToolStripItemOnLostFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler LostFocus
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventLostFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventLostFocus, value);
			}
		}

		/// <summary>Occurs when a key is pressed and held down while the hosted control has focus.</summary>
		// Token: 0x14000312 RID: 786
		// (add) Token: 0x06003F8C RID: 16268 RVA: 0x00111B34 File Offset: 0x0010FD34
		// (remove) Token: 0x06003F8D RID: 16269 RVA: 0x00111B47 File Offset: 0x0010FD47
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyDownDescr")]
		public event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventKeyDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventKeyDown, value);
			}
		}

		/// <summary>Occurs when a key is pressed while the hosted control has focus.</summary>
		// Token: 0x14000313 RID: 787
		// (add) Token: 0x06003F8E RID: 16270 RVA: 0x00111B5A File Offset: 0x0010FD5A
		// (remove) Token: 0x06003F8F RID: 16271 RVA: 0x00111B6D File Offset: 0x0010FD6D
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyPressDescr")]
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventKeyPress, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventKeyPress, value);
			}
		}

		/// <summary>Occurs when a key is released while the hosted control has focus.</summary>
		// Token: 0x14000314 RID: 788
		// (add) Token: 0x06003F90 RID: 16272 RVA: 0x00111B80 File Offset: 0x0010FD80
		// (remove) Token: 0x06003F91 RID: 16273 RVA: 0x00111B93 File Offset: 0x0010FD93
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyUpDescr")]
		public event KeyEventHandler KeyUp
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventKeyUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventKeyUp, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x00111BA6 File Offset: 0x0010FDA6
		// (set) Token: 0x06003F93 RID: 16275 RVA: 0x00111BC2 File Offset: 0x0010FDC2
		public override RightToLeft RightToLeft
		{
			get
			{
				if (this.control != null)
				{
					return this.control.RightToLeft;
				}
				return base.RightToLeft;
			}
			set
			{
				if (this.control != null)
				{
					this.control.RightToLeft = value;
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if the image is mirrored; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x00111BD8 File Offset: 0x0010FDD8
		// (set) Token: 0x06003F95 RID: 16277 RVA: 0x00111BE0 File Offset: 0x0010FDE0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		/// <summary>Gets a value indicating whether the item is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x00111BE9 File Offset: 0x0010FDE9
		public override bool Selected
		{
			get
			{
				return this.Control != null && this.Control.Focused;
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x00111C00 File Offset: 0x0010FE00
		// (set) Token: 0x06003F98 RID: 16280 RVA: 0x00111C08 File Offset: 0x0010FE08
		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				Rectangle rectangle = Rectangle.Empty;
				if (this.control != null)
				{
					rectangle = this.control.Bounds;
					rectangle.Size = value;
					CommonProperties.UpdateSpecifiedBounds(this.control, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
				}
				base.Size = value;
				if (this.control != null)
				{
					Rectangle bounds = this.control.Bounds;
					if (bounds != rectangle)
					{
						CommonProperties.UpdateSpecifiedBounds(this.control, bounds.X, bounds.Y, bounds.Width, bounds.Height);
					}
				}
			}
		}

		/// <summary>Gets or sets the site of the hosted control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the control.</returns>
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x0003192A File Offset: 0x0002FB2A
		// (set) Token: 0x06003F9A RID: 16282 RVA: 0x00111CA9 File Offset: 0x0010FEA9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (value != null)
				{
					this.Control.Site = new ToolStripControlHost.StubSite(this.Control, this);
					return;
				}
				this.Control.Site = null;
			}
		}

		/// <summary>Gets or sets the text to be displayed on the hosted control.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the text.</returns>
		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x00111CD9 File Offset: 0x0010FED9
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x00111CE6 File Offset: 0x0010FEE6
		[DefaultValue("")]
		public override string Text
		{
			get
			{
				return this.Control.Text;
			}
			set
			{
				this.Control.Text = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The text alignment property for the object.</returns>
		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x00111CF4 File Offset: 0x0010FEF4
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x00111CFC File Offset: 0x0010FEFC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The text direction of the tool strip.</returns>
		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x00111D05 File Offset: 0x0010FF05
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x00111D0D File Offset: 0x0010FF0D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
			set
			{
				base.TextDirection = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The relation of a text image with the object.</returns>
		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x00111D16 File Offset: 0x0010FF16
		// (set) Token: 0x06003FA2 RID: 16290 RVA: 0x00111D1E File Offset: 0x0010FF1E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
			set
			{
				base.TextImageRelation = value;
			}
		}

		/// <summary>Occurs while the hosted control is validating.</summary>
		// Token: 0x14000315 RID: 789
		// (add) Token: 0x06003FA3 RID: 16291 RVA: 0x00111D27 File Offset: 0x0010FF27
		// (remove) Token: 0x06003FA4 RID: 16292 RVA: 0x00111D3A File Offset: 0x0010FF3A
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatingDescr")]
		public event CancelEventHandler Validating
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventValidating, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventValidating, value);
			}
		}

		/// <summary>Occurs after the hosted control has been successfully validated.</summary>
		// Token: 0x14000316 RID: 790
		// (add) Token: 0x06003FA5 RID: 16293 RVA: 0x00111D4D File Offset: 0x0010FF4D
		// (remove) Token: 0x06003FA6 RID: 16294 RVA: 0x00111D60 File Offset: 0x0010FF60
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatedDescr")]
		public event EventHandler Validated
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventValidated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventValidated, value);
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06003FA7 RID: 16295 RVA: 0x00111D73 File Offset: 0x0010FF73
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return base.CreateAccessibilityInstance();
			}
			return this.Control.AccessibilityObject;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003FA8 RID: 16296 RVA: 0x00111D8E File Offset: 0x0010FF8E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.Control != null)
			{
				this.OnUnsubscribeControlEvents(this.Control);
				this.Control.Dispose();
				this.control = null;
			}
		}

		/// <summary>Gives the focus to a control.</summary>
		// Token: 0x06003FA9 RID: 16297 RVA: 0x00111DC0 File Offset: 0x0010FFC0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Focus()
		{
			this.Control.Focus();
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06003FAA RID: 16298 RVA: 0x00111DD0 File Offset: 0x0010FFD0
		public override Size GetPreferredSize(Size constrainingSize)
		{
			if (this.control != null)
			{
				return this.Control.GetPreferredSize(constrainingSize - this.Padding.Size) + this.Padding.Size;
			}
			return base.GetPreferredSize(constrainingSize);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00111E1F File Offset: 0x0011001F
		private void HandleClick(object sender, EventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x00111E28 File Offset: 0x00110028
		private void HandleBackColorChanged(object sender, EventArgs e)
		{
			this.OnBackColorChanged(e);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x00111E31 File Offset: 0x00110031
		private void HandleDoubleClick(object sender, EventArgs e)
		{
			this.OnDoubleClick(e);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x00111E3A File Offset: 0x0011003A
		private void HandleDragDrop(object sender, DragEventArgs e)
		{
			this.OnDragDrop(e);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x00111E43 File Offset: 0x00110043
		private void HandleDragEnter(object sender, DragEventArgs e)
		{
			this.OnDragEnter(e);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x00111E4C File Offset: 0x0011004C
		private void HandleDragLeave(object sender, EventArgs e)
		{
			this.OnDragLeave(e);
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x00111E55 File Offset: 0x00110055
		private void HandleDragOver(object sender, DragEventArgs e)
		{
			this.OnDragOver(e);
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x00111E5E File Offset: 0x0011005E
		private void HandleEnter(object sender, EventArgs e)
		{
			this.OnEnter(e);
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x00111E67 File Offset: 0x00110067
		private void HandleEnabledChanged(object sender, EventArgs e)
		{
			this.OnEnabledChanged(e);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x00111E70 File Offset: 0x00110070
		private void HandleForeColorChanged(object sender, EventArgs e)
		{
			this.OnForeColorChanged(e);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x00111E79 File Offset: 0x00110079
		private void HandleGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			this.OnGiveFeedback(e);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x00111E82 File Offset: 0x00110082
		private void HandleGotFocus(object sender, EventArgs e)
		{
			this.OnGotFocus(e);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x00111E8B File Offset: 0x0011008B
		private void HandleLocationChanged(object sender, EventArgs e)
		{
			this.OnLocationChanged(e);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x00111E94 File Offset: 0x00110094
		private void HandleLostFocus(object sender, EventArgs e)
		{
			this.OnLostFocus(e);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x00111E9D File Offset: 0x0011009D
		private void HandleKeyDown(object sender, KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x00111EA6 File Offset: 0x001100A6
		private void HandleKeyPress(object sender, KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x00111EAF File Offset: 0x001100AF
		private void HandleKeyUp(object sender, KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x00111EB8 File Offset: 0x001100B8
		private void HandleLeave(object sender, EventArgs e)
		{
			this.OnLeave(e);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x00111EC1 File Offset: 0x001100C1
		private void HandleMouseDown(object sender, MouseEventArgs e)
		{
			this.OnMouseDown(e);
			base.RaiseMouseEvent(ToolStripItem.EventMouseDown, e);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x00111ED6 File Offset: 0x001100D6
		private void HandleMouseEnter(object sender, EventArgs e)
		{
			this.OnMouseEnter(e);
			base.RaiseEvent(ToolStripItem.EventMouseEnter, e);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x00111EEB File Offset: 0x001100EB
		private void HandleMouseLeave(object sender, EventArgs e)
		{
			this.OnMouseLeave(e);
			base.RaiseEvent(ToolStripItem.EventMouseLeave, e);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x00111F00 File Offset: 0x00110100
		private void HandleMouseHover(object sender, EventArgs e)
		{
			this.OnMouseHover(e);
			base.RaiseEvent(ToolStripItem.EventMouseHover, e);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x00111F15 File Offset: 0x00110115
		private void HandleMouseMove(object sender, MouseEventArgs e)
		{
			this.OnMouseMove(e);
			base.RaiseMouseEvent(ToolStripItem.EventMouseMove, e);
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x00111F2A File Offset: 0x0011012A
		private void HandleMouseUp(object sender, MouseEventArgs e)
		{
			this.OnMouseUp(e);
			base.RaiseMouseEvent(ToolStripItem.EventMouseUp, e);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x00111F3F File Offset: 0x0011013F
		private void HandlePaint(object sender, PaintEventArgs e)
		{
			this.OnPaint(e);
			base.RaisePaintEvent(ToolStripItem.EventPaint, e);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x00111F54 File Offset: 0x00110154
		private void HandleQueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
		{
			QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)base.Events[ToolStripItem.EventQueryAccessibilityHelp];
			if (queryAccessibilityHelpEventHandler != null)
			{
				queryAccessibilityHelpEventHandler(this, e);
			}
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x00111F82 File Offset: 0x00110182
		private void HandleQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			this.OnQueryContinueDrag(e);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x00111F8B File Offset: 0x0011018B
		private void HandleRightToLeftChanged(object sender, EventArgs e)
		{
			this.OnRightToLeftChanged(e);
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x00111F94 File Offset: 0x00110194
		private void HandleResize(object sender, EventArgs e)
		{
			if (this.suspendSyncSizeCount == 0)
			{
				this.OnHostedControlResize(e);
			}
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x00111FA5 File Offset: 0x001101A5
		private void HandleTextChanged(object sender, EventArgs e)
		{
			this.OnTextChanged(e);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x00111FB0 File Offset: 0x001101B0
		private void HandleControlVisibleChanged(object sender, EventArgs e)
		{
			bool participatesInLayout = ((IArrangedElement)this.Control).ParticipatesInLayout;
			bool participatesInLayout2 = ((IArrangedElement)this).ParticipatesInLayout;
			if (participatesInLayout2 != participatesInLayout)
			{
				base.Visible = this.Control.Visible;
			}
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x00111FE5 File Offset: 0x001101E5
		private void HandleValidating(object sender, CancelEventArgs e)
		{
			this.OnValidating(e);
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x00111FEE File Offset: 0x001101EE
		private void HandleValidated(object sender, EventArgs e)
		{
			this.OnValidated(e);
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x00111FF7 File Offset: 0x001101F7
		internal override void OnAccessibleDescriptionChanged(EventArgs e)
		{
			this.Control.AccessibleDescription = base.AccessibleDescription;
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0011200A File Offset: 0x0011020A
		internal override void OnAccessibleNameChanged(EventArgs e)
		{
			this.Control.AccessibleName = base.AccessibleName;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0011201D File Offset: 0x0011021D
		internal override void OnAccessibleDefaultActionDescriptionChanged(EventArgs e)
		{
			this.Control.AccessibleDefaultActionDescription = base.AccessibleDefaultActionDescription;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x00112030 File Offset: 0x00110230
		internal override void OnAccessibleRoleChanged(EventArgs e)
		{
			this.Control.AccessibleRole = base.AccessibleRole;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD0 RID: 16336 RVA: 0x00112043 File Offset: 0x00110243
		protected virtual void OnEnter(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventEnter, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD1 RID: 16337 RVA: 0x00112051 File Offset: 0x00110251
		protected virtual void OnGotFocus(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventGotFocus, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Leave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD2 RID: 16338 RVA: 0x0011205F File Offset: 0x0011025F
		protected virtual void OnLeave(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventLeave, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.LostFocus" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD3 RID: 16339 RVA: 0x0011206D File Offset: 0x0011026D
		protected virtual void OnLostFocus(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventLostFocus, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x0011207B File Offset: 0x0011027B
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
			base.RaiseKeyEvent(ToolStripControlHost.EventKeyDown, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD5 RID: 16341 RVA: 0x00112089 File Offset: 0x00110289
		protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			base.RaiseKeyPressEvent(ToolStripControlHost.EventKeyPress, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD6 RID: 16342 RVA: 0x00112097 File Offset: 0x00110297
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
			base.RaiseKeyEvent(ToolStripControlHost.EventKeyUp, e);
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Bounds" /> property changes.</summary>
		// Token: 0x06003FD7 RID: 16343 RVA: 0x001120A8 File Offset: 0x001102A8
		protected override void OnBoundsChanged()
		{
			if (this.control != null)
			{
				this.SuspendSizeSync();
				IArrangedElement arrangedElement = this.control;
				if (arrangedElement == null)
				{
					return;
				}
				Size size = LayoutUtils.DeflateRect(this.Bounds, this.Padding).Size;
				Rectangle rectangle = LayoutUtils.Align(size, this.Bounds, this.ControlAlign);
				arrangedElement.SetBounds(rectangle, BoundsSpecified.None);
				if (rectangle != this.control.Bounds)
				{
					rectangle = LayoutUtils.Align(this.control.Size, this.Bounds, this.ControlAlign);
					arrangedElement.SetBounds(rectangle, BoundsSpecified.None);
				}
				this.ResumeSizeSync();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD8 RID: 16344 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnPaint(PaintEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD9 RID: 16345 RVA: 0x000070A6 File Offset: 0x000052A6
		protected internal override void OnLayout(LayoutEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="oldParent">The original parent of the item.</param>
		/// <param name="newParent">The new parent of the item.</param>
		// Token: 0x06003FDA RID: 16346 RVA: 0x00112144 File Offset: 0x00110344
		protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		{
			if (oldParent != null && base.Owner == null && newParent == null && this.Control != null)
			{
				WindowsFormsUtils.ReadOnlyControlCollection controlCollection = ToolStripControlHost.GetControlCollection(this.Control.ParentInternal as ToolStrip);
				if (controlCollection != null)
				{
					controlCollection.RemoveInternal(this.Control);
				}
			}
			else
			{
				this.SyncControlParent();
			}
			base.OnParentChanged(oldParent, newParent);
		}

		/// <summary>Subscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x06003FDB RID: 16347 RVA: 0x0011219C File Offset: 0x0011039C
		protected virtual void OnSubscribeControlEvents(Control control)
		{
			if (control != null)
			{
				control.Click += this.HandleClick;
				control.BackColorChanged += this.HandleBackColorChanged;
				control.DoubleClick += this.HandleDoubleClick;
				control.DragDrop += this.HandleDragDrop;
				control.DragEnter += this.HandleDragEnter;
				control.DragLeave += this.HandleDragLeave;
				control.DragOver += this.HandleDragOver;
				control.Enter += this.HandleEnter;
				control.EnabledChanged += this.HandleEnabledChanged;
				control.ForeColorChanged += this.HandleForeColorChanged;
				control.GiveFeedback += this.HandleGiveFeedback;
				control.GotFocus += this.HandleGotFocus;
				control.Leave += this.HandleLeave;
				control.LocationChanged += this.HandleLocationChanged;
				control.LostFocus += this.HandleLostFocus;
				control.KeyDown += this.HandleKeyDown;
				control.KeyPress += this.HandleKeyPress;
				control.KeyUp += this.HandleKeyUp;
				control.MouseDown += this.HandleMouseDown;
				control.MouseEnter += this.HandleMouseEnter;
				control.MouseHover += this.HandleMouseHover;
				control.MouseLeave += this.HandleMouseLeave;
				control.MouseMove += this.HandleMouseMove;
				control.MouseUp += this.HandleMouseUp;
				control.Paint += this.HandlePaint;
				control.QueryAccessibilityHelp += this.HandleQueryAccessibilityHelp;
				control.QueryContinueDrag += this.HandleQueryContinueDrag;
				control.Resize += this.HandleResize;
				control.RightToLeftChanged += this.HandleRightToLeftChanged;
				control.TextChanged += this.HandleTextChanged;
				control.VisibleChanged += this.HandleControlVisibleChanged;
				control.Validating += this.HandleValidating;
				control.Validated += this.HandleValidated;
			}
		}

		/// <summary>Unsubscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x06003FDC RID: 16348 RVA: 0x00112404 File Offset: 0x00110604
		protected virtual void OnUnsubscribeControlEvents(Control control)
		{
			if (control != null)
			{
				control.Click -= this.HandleClick;
				control.BackColorChanged -= this.HandleBackColorChanged;
				control.DoubleClick -= this.HandleDoubleClick;
				control.DragDrop -= this.HandleDragDrop;
				control.DragEnter -= this.HandleDragEnter;
				control.DragLeave -= this.HandleDragLeave;
				control.DragOver -= this.HandleDragOver;
				control.Enter -= this.HandleEnter;
				control.EnabledChanged -= this.HandleEnabledChanged;
				control.ForeColorChanged -= this.HandleForeColorChanged;
				control.GiveFeedback -= this.HandleGiveFeedback;
				control.GotFocus -= this.HandleGotFocus;
				control.Leave -= this.HandleLeave;
				control.LocationChanged -= this.HandleLocationChanged;
				control.LostFocus -= this.HandleLostFocus;
				control.KeyDown -= this.HandleKeyDown;
				control.KeyPress -= this.HandleKeyPress;
				control.KeyUp -= this.HandleKeyUp;
				control.MouseDown -= this.HandleMouseDown;
				control.MouseEnter -= this.HandleMouseEnter;
				control.MouseHover -= this.HandleMouseHover;
				control.MouseLeave -= this.HandleMouseLeave;
				control.MouseMove -= this.HandleMouseMove;
				control.MouseUp -= this.HandleMouseUp;
				control.Paint -= this.HandlePaint;
				control.QueryAccessibilityHelp -= this.HandleQueryAccessibilityHelp;
				control.QueryContinueDrag -= this.HandleQueryContinueDrag;
				control.Resize -= this.HandleResize;
				control.RightToLeftChanged -= this.HandleRightToLeftChanged;
				control.TextChanged -= this.HandleTextChanged;
				control.VisibleChanged -= this.HandleControlVisibleChanged;
				control.Validating -= this.HandleValidating;
				control.Validated -= this.HandleValidated;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FDD RID: 16349 RVA: 0x00112669 File Offset: 0x00110869
		protected virtual void OnValidating(CancelEventArgs e)
		{
			base.RaiseCancelEvent(ToolStripControlHost.EventValidating, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Validated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003FDE RID: 16350 RVA: 0x00112677 File Offset: 0x00110877
		protected virtual void OnValidated(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventValidated, e);
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x00112688 File Offset: 0x00110888
		private static WindowsFormsUtils.ReadOnlyControlCollection GetControlCollection(ToolStrip toolStrip)
		{
			return (toolStrip != null) ? ((WindowsFormsUtils.ReadOnlyControlCollection)toolStrip.Controls) : null;
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x001126A8 File Offset: 0x001108A8
		private void SyncControlParent()
		{
			WindowsFormsUtils.ReadOnlyControlCollection controlCollection = ToolStripControlHost.GetControlCollection(base.ParentInternal);
			if (controlCollection != null)
			{
				controlCollection.AddInternal(this.Control);
			}
		}

		/// <summary>Synchronizes the resizing of the control host with the resizing of the hosted control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE1 RID: 16353 RVA: 0x001126D0 File Offset: 0x001108D0
		protected virtual void OnHostedControlResize(EventArgs e)
		{
			this.Size = this.Control.Size;
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x06003FE2 RID: 16354 RVA: 0x0001180C File Offset: 0x0000FA0C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return false;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003FE3 RID: 16355 RVA: 0x001126E3 File Offset: 0x001108E3
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.control != null)
			{
				return this.control.ProcessMnemonic(charCode);
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the item; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003FE4 RID: 16356 RVA: 0x0001180C File Offset: 0x0000FA0C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			return false;
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visible state.</summary>
		/// <param name="visible">
		///   <see langword="true" /> to make the ToolStripItem visible; otherwise, <see langword="false" />.</param>
		// Token: 0x06003FE5 RID: 16357 RVA: 0x00112704 File Offset: 0x00110904
		protected override void SetVisibleCore(bool visible)
		{
			if (this.inSetVisibleCore)
			{
				return;
			}
			this.inSetVisibleCore = true;
			this.Control.SuspendLayout();
			try
			{
				this.Control.Visible = visible;
			}
			finally
			{
				this.Control.ResumeLayout(false);
				base.SetVisibleCore(visible);
				this.inSetVisibleCore = false;
			}
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003FE6 RID: 16358 RVA: 0x00112768 File Offset: 0x00110968
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetBackColor()
		{
			this.Control.ResetBackColor();
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003FE7 RID: 16359 RVA: 0x00112775 File Offset: 0x00110975
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetForeColor()
		{
			this.Control.ResetForeColor();
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x00112782 File Offset: 0x00110982
		private void SuspendSizeSync()
		{
			this.suspendSyncSizeCount++;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x00112792 File Offset: 0x00110992
		private void ResumeSizeSync()
		{
			this.suspendSyncSizeCount--;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x001127A2 File Offset: 0x001109A2
		internal override bool ShouldSerializeBackColor()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeBackColor();
			}
			return base.ShouldSerializeBackColor();
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x001127BE File Offset: 0x001109BE
		internal override bool ShouldSerializeForeColor()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeForeColor();
			}
			return base.ShouldSerializeForeColor();
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x001127DA File Offset: 0x001109DA
		internal override bool ShouldSerializeFont()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeFont();
			}
			return base.ShouldSerializeFont();
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x001127F6 File Offset: 0x001109F6
		internal override bool ShouldSerializeRightToLeft()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeRightToLeft();
			}
			return base.ShouldSerializeRightToLeft();
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x00112812 File Offset: 0x00110A12
		internal override void OnKeyboardToolTipHook(ToolTip toolTip)
		{
			base.OnKeyboardToolTipHook(toolTip);
			KeyboardToolTipStateMachine.Instance.Hook(this.Control, toolTip);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x0011282C File Offset: 0x00110A2C
		internal override void OnKeyboardToolTipUnhook(ToolTip toolTip)
		{
			base.OnKeyboardToolTipUnhook(toolTip);
			KeyboardToolTipStateMachine.Instance.Unhook(this.Control, toolTip);
		}

		// Token: 0x040024A1 RID: 9377
		private Control control;

		// Token: 0x040024A2 RID: 9378
		private int suspendSyncSizeCount;

		// Token: 0x040024A3 RID: 9379
		private ContentAlignment controlAlign = ContentAlignment.MiddleCenter;

		// Token: 0x040024A4 RID: 9380
		private bool inSetVisibleCore;

		// Token: 0x040024A5 RID: 9381
		internal static readonly object EventGotFocus = new object();

		// Token: 0x040024A6 RID: 9382
		internal static readonly object EventLostFocus = new object();

		// Token: 0x040024A7 RID: 9383
		internal static readonly object EventKeyDown = new object();

		// Token: 0x040024A8 RID: 9384
		internal static readonly object EventKeyPress = new object();

		// Token: 0x040024A9 RID: 9385
		internal static readonly object EventKeyUp = new object();

		// Token: 0x040024AA RID: 9386
		internal static readonly object EventEnter = new object();

		// Token: 0x040024AB RID: 9387
		internal static readonly object EventLeave = new object();

		// Token: 0x040024AC RID: 9388
		internal static readonly object EventValidated = new object();

		// Token: 0x040024AD RID: 9389
		internal static readonly object EventValidating = new object();

		// Token: 0x020007FD RID: 2045
		private class StubSite : ISite, IServiceProvider, IDictionaryService
		{
			// Token: 0x06006EA7 RID: 28327 RVA: 0x001952D8 File Offset: 0x001934D8
			public StubSite(Component control, Component host)
			{
				this.comp = control;
				this.owner = host;
			}

			// Token: 0x17001830 RID: 6192
			// (get) Token: 0x06006EA8 RID: 28328 RVA: 0x001952EE File Offset: 0x001934EE
			IComponent ISite.Component
			{
				get
				{
					return this.comp;
				}
			}

			// Token: 0x17001831 RID: 6193
			// (get) Token: 0x06006EA9 RID: 28329 RVA: 0x001952F6 File Offset: 0x001934F6
			IContainer ISite.Container
			{
				get
				{
					return this.owner.Site.Container;
				}
			}

			// Token: 0x17001832 RID: 6194
			// (get) Token: 0x06006EAA RID: 28330 RVA: 0x00195308 File Offset: 0x00193508
			bool ISite.DesignMode
			{
				get
				{
					return this.owner.Site.DesignMode;
				}
			}

			// Token: 0x17001833 RID: 6195
			// (get) Token: 0x06006EAB RID: 28331 RVA: 0x0019531A File Offset: 0x0019351A
			// (set) Token: 0x06006EAC RID: 28332 RVA: 0x0019532C File Offset: 0x0019352C
			string ISite.Name
			{
				get
				{
					return this.owner.Site.Name;
				}
				set
				{
					this.owner.Site.Name = value;
				}
			}

			// Token: 0x06006EAD RID: 28333 RVA: 0x00195340 File Offset: 0x00193540
			object IServiceProvider.GetService(Type service)
			{
				if (service == null)
				{
					throw new ArgumentNullException("service");
				}
				if (service == typeof(IDictionaryService))
				{
					return this;
				}
				if (this.owner.Site != null)
				{
					return this.owner.Site.GetService(service);
				}
				return null;
			}

			// Token: 0x06006EAE RID: 28334 RVA: 0x00195398 File Offset: 0x00193598
			object IDictionaryService.GetKey(object value)
			{
				if (this._dictionary != null)
				{
					foreach (object obj in this._dictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						object value2 = dictionaryEntry.Value;
						if (value != null && value.Equals(value2))
						{
							return dictionaryEntry.Key;
						}
					}
				}
				return null;
			}

			// Token: 0x06006EAF RID: 28335 RVA: 0x00195418 File Offset: 0x00193618
			object IDictionaryService.GetValue(object key)
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				return null;
			}

			// Token: 0x06006EB0 RID: 28336 RVA: 0x00195430 File Offset: 0x00193630
			void IDictionaryService.SetValue(object key, object value)
			{
				if (this._dictionary == null)
				{
					this._dictionary = new Hashtable();
				}
				if (value == null)
				{
					this._dictionary.Remove(key);
					return;
				}
				this._dictionary[key] = value;
			}

			// Token: 0x040042EA RID: 17130
			private Hashtable _dictionary;

			// Token: 0x040042EB RID: 17131
			private IComponent comp;

			// Token: 0x040042EC RID: 17132
			private IComponent owner;
		}
	}
}
