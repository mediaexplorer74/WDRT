using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel in a <see cref="T:System.Windows.Forms.StatusBar" /> control. Although the <see cref="T:System.Windows.Forms.StatusStrip" /> control replaces and adds functionality to the <see cref="T:System.Windows.Forms.StatusBar" /> control of previous versions, <see cref="T:System.Windows.Forms.StatusBar" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x02000376 RID: 886
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	public class StatusBarPanel : Component, ISupportInitialize
	{
		/// <summary>Gets or sets the alignment of text and icons within the status bar panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration.</exception>
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x00100C4D File Offset: 0x000FEE4D
		// (set) Token: 0x06003A28 RID: 14888 RVA: 0x00100C55 File Offset: 0x000FEE55
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[Localizable(true)]
		[SRDescription("StatusBarPanelAlignmentDescr")]
		public HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.Realize();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the status bar panel is automatically resized.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> enumeration.</exception>
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x00100C93 File Offset: 0x000FEE93
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x00100C9B File Offset: 0x000FEE9B
		[SRCategory("CatAppearance")]
		[DefaultValue(StatusBarPanelAutoSize.None)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("StatusBarPanelAutoSizeDescr")]
		public StatusBarPanelAutoSize AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StatusBarPanelAutoSize));
				}
				if (this.autoSize != value)
				{
					this.autoSize = value;
					this.UpdateSize();
				}
			}
		}

		/// <summary>Gets or sets the border style of the status bar panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelBorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelBorderStyle.Sunken" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelBorderStyle" /> enumeration.</exception>
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x00100CD9 File Offset: 0x000FEED9
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x00100CE4 File Offset: 0x000FEEE4
		[SRCategory("CatAppearance")]
		[DefaultValue(StatusBarPanelBorderStyle.Sunken)]
		[DispId(-504)]
		[SRDescription("StatusBarPanelBorderStyleDescr")]
		public StatusBarPanelBorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StatusBarPanelBorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					this.Realize();
					if (this.Created)
					{
						this.parent.Invalidate();
					}
				}
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x00100D40 File Offset: 0x000FEF40
		internal bool Created
		{
			get
			{
				return this.parent != null && this.parent.ArePanelsRealized();
			}
		}

		/// <summary>Gets or sets the icon to display within the status bar panel.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon to display in the panel.</returns>
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x00100D57 File Offset: 0x000FEF57
		// (set) Token: 0x06003A2F RID: 14895 RVA: 0x00100D60 File Offset: 0x000FEF60
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("StatusBarPanelIconDescr")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value != null && (value.Height > SystemInformation.SmallIconSize.Height || value.Width > SystemInformation.SmallIconSize.Width))
				{
					this.icon = new Icon(value, SystemInformation.SmallIconSize);
				}
				else
				{
					this.icon = value;
				}
				if (this.Created)
				{
					IntPtr intPtr = ((this.icon == null) ? IntPtr.Zero : this.icon.Handle);
					this.parent.SendMessage(1039, (IntPtr)this.GetIndex(), intPtr);
				}
				this.UpdateSize();
				if (this.Created)
				{
					this.parent.Invalidate();
				}
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x00100E0D File Offset: 0x000FF00D
		// (set) Token: 0x06003A31 RID: 14897 RVA: 0x00100E15 File Offset: 0x000FF015
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		/// <summary>Gets or sets the minimum allowed width of the status bar panel within the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>The minimum width, in pixels, of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		/// <exception cref="T:System.ArgumentException">A value less than 0 is assigned to the property.</exception>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x00100E1E File Offset: 0x000FF01E
		// (set) Token: 0x06003A33 RID: 14899 RVA: 0x00100E28 File Offset: 0x000FF028
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("StatusBarPanelMinWidthDescr")]
		public int MinWidth
		{
			get
			{
				return this.minWidth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MinWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MinWidth",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value != this.minWidth)
				{
					this.minWidth = value;
					this.UpdateSize();
					if (this.minWidth > this.Width)
					{
						this.Width = value;
					}
				}
			}
		}

		/// <summary>Gets or sets the name to apply to the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x00100EA7 File Offset: 0x000FF0A7
		// (set) Token: 0x06003A35 RID: 14901 RVA: 0x00100EB5 File Offset: 0x000FF0B5
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("StatusBarPanelNameDescr")]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, this.name);
			}
			set
			{
				this.name = value;
				if (this.Site != null)
				{
					this.Site.Name = this.name;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.StatusBar" /> control that hosts the status bar panel.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.StatusBar" /> that contains the panel.</returns>
		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x00100ED7 File Offset: 0x000FF0D7
		[Browsable(false)]
		public StatusBar Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (set) Token: 0x06003A37 RID: 14903 RVA: 0x00100EDF File Offset: 0x000FF0DF
		internal StatusBar ParentInternal
		{
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x00100EE8 File Offset: 0x000FF0E8
		// (set) Token: 0x06003A39 RID: 14905 RVA: 0x00100EF0 File Offset: 0x000FF0F0
		internal int Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		/// <summary>Gets or sets the style of the status bar panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelStyle" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelStyle.Text" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelStyle" /> enumeration.</exception>
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x00100EF9 File Offset: 0x000FF0F9
		// (set) Token: 0x06003A3B RID: 14907 RVA: 0x00100F04 File Offset: 0x000FF104
		[SRCategory("CatAppearance")]
		[DefaultValue(StatusBarPanelStyle.Text)]
		[SRDescription("StatusBarPanelStyleDescr")]
		public StatusBarPanelStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StatusBarPanelStyle));
				}
				if (this.style != value)
				{
					this.style = value;
					this.Realize();
					if (this.Created)
					{
						this.parent.Invalidate();
					}
				}
			}
		}

		/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x00100F60 File Offset: 0x000FF160
		// (set) Token: 0x06003A3D RID: 14909 RVA: 0x00100F68 File Offset: 0x000FF168
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		/// <summary>Gets or sets the text of the status bar panel.</summary>
		/// <returns>The text displayed in the panel.</returns>
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x00100F71 File Offset: 0x000FF171
		// (set) Token: 0x06003A3F RID: 14911 RVA: 0x00100F87 File Offset: 0x000FF187
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("StatusBarPanelTextDescr")]
		public string Text
		{
			get
			{
				if (this.text == null)
				{
					return "";
				}
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.Text.Equals(value))
				{
					if (value.Length == 0)
					{
						this.text = null;
					}
					else
					{
						this.text = value;
					}
					this.Realize();
					this.UpdateSize();
				}
			}
		}

		/// <summary>Gets or sets ToolTip text associated with the status bar panel.</summary>
		/// <returns>The ToolTip text for the panel.</returns>
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x00100FC5 File Offset: 0x000FF1C5
		// (set) Token: 0x06003A41 RID: 14913 RVA: 0x00100FDC File Offset: 0x000FF1DC
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("StatusBarPanelToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				if (this.toolTipText == null)
				{
					return "";
				}
				return this.toolTipText;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.ToolTipText.Equals(value))
				{
					if (value.Length == 0)
					{
						this.toolTipText = null;
					}
					else
					{
						this.toolTipText = value;
					}
					if (this.Created)
					{
						this.parent.UpdateTooltip(this);
					}
				}
			}
		}

		/// <summary>Gets or sets the width of the status bar panel within the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>The width, in pixels, of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
		/// <exception cref="T:System.ArgumentException">The width specified is less than the value of the <see cref="P:System.Windows.Forms.StatusBarPanel.MinWidth" /> property.</exception>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x0010102D File Offset: 0x000FF22D
		// (set) Token: 0x06003A43 RID: 14915 RVA: 0x00101035 File Offset: 0x000FF235
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(100)]
		[SRDescription("StatusBarPanelWidthDescr")]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (!this.initializing && value < this.minWidth)
				{
					throw new ArgumentOutOfRangeException("Width", SR.GetString("WidthGreaterThanMinWidth"));
				}
				this.width = value;
				this.UpdateSize();
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		// Token: 0x06003A44 RID: 14916 RVA: 0x0010106A File Offset: 0x000FF26A
		public void BeginInit()
		{
			this.initializing = true;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.StatusBarPanel" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003A45 RID: 14917 RVA: 0x00101074 File Offset: 0x000FF274
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.parent != null)
			{
				int num = this.GetIndex();
				if (num != -1)
				{
					this.parent.Panels.RemoveAt(num);
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		// Token: 0x06003A46 RID: 14918 RVA: 0x001010AF File Offset: 0x000FF2AF
		public void EndInit()
		{
			this.initializing = false;
			if (this.Width < this.MinWidth)
			{
				this.Width = this.MinWidth;
			}
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x001010D4 File Offset: 0x000FF2D4
		internal int GetContentsWidth(bool newPanel)
		{
			string text;
			if (newPanel)
			{
				if (this.text == null)
				{
					text = "";
				}
				else
				{
					text = this.text;
				}
			}
			else
			{
				text = this.Text;
			}
			Graphics graphics = this.parent.CreateGraphicsInternal();
			Size size = Size.Ceiling(graphics.MeasureString(text, this.parent.Font));
			if (this.icon != null)
			{
				size.Width += this.icon.Size.Width + 5;
			}
			graphics.Dispose();
			int num = size.Width + SystemInformation.BorderSize.Width * 2 + 6 + 2;
			return Math.Max(num, this.minWidth);
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x00100E0D File Offset: 0x000FF00D
		private int GetIndex()
		{
			return this.index;
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x00101184 File Offset: 0x000FF384
		internal void Realize()
		{
			if (this.Created)
			{
				int num = 0;
				string text;
				if (this.text == null)
				{
					text = "";
				}
				else
				{
					text = this.text;
				}
				HorizontalAlignment horizontalAlignment = this.alignment;
				if (this.parent.RightToLeft == RightToLeft.Yes)
				{
					if (horizontalAlignment != HorizontalAlignment.Left)
					{
						if (horizontalAlignment == HorizontalAlignment.Right)
						{
							horizontalAlignment = HorizontalAlignment.Left;
						}
					}
					else
					{
						horizontalAlignment = HorizontalAlignment.Right;
					}
				}
				string text2;
				if (horizontalAlignment != HorizontalAlignment.Right)
				{
					if (horizontalAlignment == HorizontalAlignment.Center)
					{
						text2 = "\t" + text;
					}
					else
					{
						text2 = text;
					}
				}
				else
				{
					text2 = "\t\t" + text;
				}
				switch (this.borderStyle)
				{
				case StatusBarPanelBorderStyle.None:
					num |= 256;
					break;
				case StatusBarPanelBorderStyle.Raised:
					num |= 512;
					break;
				}
				StatusBarPanelStyle statusBarPanelStyle = this.style;
				if (statusBarPanelStyle != StatusBarPanelStyle.Text && statusBarPanelStyle == StatusBarPanelStyle.OwnerDraw)
				{
					num |= 4096;
				}
				int num2 = this.GetIndex() | num;
				if (this.parent.RightToLeft == RightToLeft.Yes)
				{
					num2 |= 1024;
				}
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(this.parent, this.parent.Handle), NativeMethods.SB_SETTEXT, (IntPtr)num2, text2) == 0)
				{
					throw new InvalidOperationException(SR.GetString("UnableToSetPanelText"));
				}
				if (this.icon != null && this.style != StatusBarPanelStyle.OwnerDraw)
				{
					this.parent.SendMessage(1039, (IntPtr)this.GetIndex(), this.icon.Handle);
				}
				else
				{
					this.parent.SendMessage(1039, (IntPtr)this.GetIndex(), IntPtr.Zero);
				}
				if (this.style == StatusBarPanelStyle.OwnerDraw)
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					int num3 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.parent, this.parent.Handle), 1034, (IntPtr)this.GetIndex(), ref rect);
					if (num3 != 0)
					{
						this.parent.Invalidate(Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom));
					}
				}
			}
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x0010137F File Offset: 0x000FF57F
		private void UpdateSize()
		{
			if (this.autoSize == StatusBarPanelAutoSize.Contents)
			{
				this.ApplyContentSizing();
				return;
			}
			if (this.Created)
			{
				this.parent.DirtyLayout();
				this.parent.PerformLayout();
			}
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x001013B0 File Offset: 0x000FF5B0
		private void ApplyContentSizing()
		{
			if (this.autoSize == StatusBarPanelAutoSize.Contents && this.parent != null)
			{
				int contentsWidth = this.GetContentsWidth(false);
				if (contentsWidth != this.Width)
				{
					this.Width = contentsWidth;
					if (this.Created)
					{
						this.parent.DirtyLayout();
						this.parent.PerformLayout();
					}
				}
			}
		}

		/// <summary>Retrieves a string that contains information about the panel.</summary>
		/// <returns>Returns a string that contains the class name for the control and the text it contains.</returns>
		// Token: 0x06003A4C RID: 14924 RVA: 0x00101404 File Offset: 0x000FF604
		public override string ToString()
		{
			return "StatusBarPanel: {" + this.Text + "}";
		}

		// Token: 0x040022DE RID: 8926
		private const int DEFAULTWIDTH = 100;

		// Token: 0x040022DF RID: 8927
		private const int DEFAULTMINWIDTH = 10;

		// Token: 0x040022E0 RID: 8928
		private const int PANELTEXTINSET = 3;

		// Token: 0x040022E1 RID: 8929
		private const int PANELGAP = 2;

		// Token: 0x040022E2 RID: 8930
		private string text = "";

		// Token: 0x040022E3 RID: 8931
		private string name = "";

		// Token: 0x040022E4 RID: 8932
		private string toolTipText = "";

		// Token: 0x040022E5 RID: 8933
		private Icon icon;

		// Token: 0x040022E6 RID: 8934
		private HorizontalAlignment alignment;

		// Token: 0x040022E7 RID: 8935
		private StatusBarPanelBorderStyle borderStyle = StatusBarPanelBorderStyle.Sunken;

		// Token: 0x040022E8 RID: 8936
		private StatusBarPanelStyle style = StatusBarPanelStyle.Text;

		// Token: 0x040022E9 RID: 8937
		private StatusBar parent;

		// Token: 0x040022EA RID: 8938
		private int width = 100;

		// Token: 0x040022EB RID: 8939
		private int right;

		// Token: 0x040022EC RID: 8940
		private int minWidth = 10;

		// Token: 0x040022ED RID: 8941
		private int index;

		// Token: 0x040022EE RID: 8942
		private StatusBarPanelAutoSize autoSize = StatusBarPanelAutoSize.None;

		// Token: 0x040022EF RID: 8943
		private bool initializing;

		// Token: 0x040022F0 RID: 8944
		private object userData;
	}
}
