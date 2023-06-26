using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows toolbar button. Although <see cref="T:System.Windows.Forms.ToolStripButton" /> replaces and extends the <see cref="T:System.Windows.Forms.ToolBarButton" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBarButton" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x020003A9 RID: 937
	[Designer("System.Windows.Forms.Design.ToolBarButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class ToolBarButton : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButton" /> class.</summary>
		// Token: 0x06003D68 RID: 15720 RVA: 0x0010AB29 File Offset: 0x00108D29
		public ToolBarButton()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButton" /> class and displays the assigned text on the button.</summary>
		/// <param name="text">The text to display on the new <see cref="T:System.Windows.Forms.ToolBarButton" />.</param>
		// Token: 0x06003D69 RID: 15721 RVA: 0x0010AB59 File Offset: 0x00108D59
		public ToolBarButton(string text)
		{
			this.Text = text;
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x0010AB90 File Offset: 0x00108D90
		internal ToolBarButton.ToolBarButtonImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ToolBarButton.ToolBarButtonImageIndexer(this);
				}
				return this.imageIndexer;
			}
		}

		/// <summary>Gets or sets the menu to be displayed in the drop-down toolbar button.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> to be displayed in the drop-down toolbar button. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned object is not a <see cref="T:System.Windows.Forms.ContextMenu" />.</exception>
		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003D6B RID: 15723 RVA: 0x0010ABAC File Offset: 0x00108DAC
		// (set) Token: 0x06003D6C RID: 15724 RVA: 0x0010ABB4 File Offset: 0x00108DB4
		[DefaultValue(null)]
		[TypeConverter(typeof(ReferenceConverter))]
		[SRDescription("ToolBarButtonMenuDescr")]
		public Menu DropDownMenu
		{
			get
			{
				return this.dropDownMenu;
			}
			set
			{
				if (value != null && !(value is ContextMenu))
				{
					throw new ArgumentException(SR.GetString("ToolBarButtonInvalidDropDownMenuType"));
				}
				this.dropDownMenu = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the button is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the button is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x0010ABD8 File Offset: 0x00108DD8
		// (set) Token: 0x06003D6E RID: 15726 RVA: 0x0010ABE0 File Offset: 0x00108DE0
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonEnabledDescr")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					this.enabled = value;
					if (this.parent != null && this.parent.IsHandleCreated)
					{
						this.parent.SendMessage(1025, this.FindButtonIndex(), this.enabled ? 1 : 0);
					}
				}
			}
		}

		/// <summary>Gets or sets the index value of the image assigned to the button.</summary>
		/// <returns>The index value of the <see cref="T:System.Drawing.Image" /> assigned to the toolbar button. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than -1.</exception>
		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x0010AC35 File Offset: 0x00108E35
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x0010AC44 File Offset: 0x00108E44
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonImageIndexDescr")]
		public int ImageIndex
		{
			get
			{
				return this.ImageIndexer.Index;
			}
			set
			{
				if (this.ImageIndexer.Index != value)
				{
					if (value < -1)
					{
						throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"ImageIndex",
							value.ToString(CultureInfo.CurrentCulture),
							-1
						}));
					}
					this.ImageIndexer.Index = value;
					this.UpdateButton(false);
				}
			}
		}

		/// <summary>Gets or sets the name of the image assigned to the button.</summary>
		/// <returns>The name of the <see cref="T:System.Drawing.Image" /> assigned to the toolbar button.</returns>
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x0010ACB1 File Offset: 0x00108EB1
		// (set) Token: 0x06003D72 RID: 15730 RVA: 0x0010ACBE File Offset: 0x00108EBE
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ToolBarButtonImageIndexDescr")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				if (this.ImageIndexer.Key != value)
				{
					this.ImageIndexer.Key = value;
					this.UpdateButton(false);
				}
			}
		}

		/// <summary>The name of the button.</summary>
		/// <returns>The name of the button.</returns>
		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x0010ACE6 File Offset: 0x00108EE6
		// (set) Token: 0x06003D74 RID: 15732 RVA: 0x0010ACF4 File Offset: 0x00108EF4
		[Browsable(false)]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, this.name);
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.name = null;
				}
				else
				{
					this.name = value;
				}
				if (this.Site != null)
				{
					this.Site.Name = this.name;
				}
			}
		}

		/// <summary>Gets the toolbar control that the toolbar button is assigned to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolBar" /> control that the <see cref="T:System.Windows.Forms.ToolBarButton" /> is assigned to.</returns>
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x0010AD2A File Offset: 0x00108F2A
		[Browsable(false)]
		public ToolBar Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is partially pushed.</summary>
		/// <returns>
		///   <see langword="true" /> if a toggle-style toolbar button is partially pushed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x0010AD34 File Offset: 0x00108F34
		// (set) Token: 0x06003D77 RID: 15735 RVA: 0x0010AD91 File Offset: 0x00108F91
		[DefaultValue(false)]
		[SRDescription("ToolBarButtonPartialPushDescr")]
		public bool PartialPush
		{
			get
			{
				if (this.parent == null || !this.parent.IsHandleCreated)
				{
					return this.partialPush;
				}
				if ((int)this.parent.SendMessage(1037, this.FindButtonIndex(), 0) != 0)
				{
					this.partialPush = true;
				}
				else
				{
					this.partialPush = false;
				}
				return this.partialPush;
			}
			set
			{
				if (this.partialPush != value)
				{
					this.partialPush = value;
					this.UpdateButton(false);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is currently in the pushed state.</summary>
		/// <returns>
		///   <see langword="true" /> if a toggle-style toolbar button is currently in the pushed state; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x0010ADAA File Offset: 0x00108FAA
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x0010ADCE File Offset: 0x00108FCE
		[DefaultValue(false)]
		[SRDescription("ToolBarButtonPushedDescr")]
		public bool Pushed
		{
			get
			{
				if (this.parent == null || !this.parent.IsHandleCreated)
				{
					return this.pushed;
				}
				return this.GetPushedState();
			}
			set
			{
				if (value != this.Pushed)
				{
					this.pushed = value;
					this.UpdateButton(false, false, false);
				}
			}
		}

		/// <summary>Gets the bounding rectangle for a toolbar button.</summary>
		/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> for a toolbar button.</returns>
		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x0010ADEC File Offset: 0x00108FEC
		public Rectangle Rectangle
		{
			get
			{
				if (this.parent != null)
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.SendMessage(new HandleRef(this.parent, this.parent.Handle), 1075, this.FindButtonIndex(), ref rect);
					return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
				}
				return Rectangle.Empty;
			}
		}

		/// <summary>Gets or sets the style of the toolbar button.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values. The default is <see langword="ToolBarButtonStyle.PushButton" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values.</exception>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x0010AE55 File Offset: 0x00109055
		// (set) Token: 0x06003D7C RID: 15740 RVA: 0x0010AE5D File Offset: 0x0010905D
		[DefaultValue(ToolBarButtonStyle.PushButton)]
		[SRDescription("ToolBarButtonStyleDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ToolBarButtonStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolBarButtonStyle));
				}
				if (this.style == value)
				{
					return;
				}
				this.style = value;
				this.UpdateButton(true);
			}
		}

		/// <summary>Gets or sets the object that contains data about the toolbar button.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the toolbar button. The default is <see langword="null" />.</returns>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x0010AE9D File Offset: 0x0010909D
		// (set) Token: 0x06003D7E RID: 15742 RVA: 0x0010AEA5 File Offset: 0x001090A5
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

		/// <summary>Gets or sets the text displayed on the toolbar button.</summary>
		/// <returns>The text displayed on the toolbar button. The default is an empty string ("").</returns>
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x0010AEAE File Offset: 0x001090AE
		// (set) Token: 0x06003D80 RID: 15744 RVA: 0x0010AEC4 File Offset: 0x001090C4
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("ToolBarButtonTextDescr")]
		public string Text
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				return "";
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = null;
				}
				if ((value == null && this.text != null) || (value != null && (this.text == null || !this.text.Equals(value))))
				{
					this.text = value;
					this.UpdateButton(WindowsFormsUtils.ContainsMnemonic(this.text), true, true);
				}
			}
		}

		/// <summary>Gets or sets the text that appears as a ToolTip for the button.</summary>
		/// <returns>The text that is displayed when the mouse pointer moves over the toolbar button. The default is an empty string ("").</returns>
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x0010AF1A File Offset: 0x0010911A
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x0010AF30 File Offset: 0x00109130
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("ToolBarButtonToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				if (this.tooltipText != null)
				{
					return this.tooltipText;
				}
				return "";
			}
			set
			{
				this.tooltipText = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar button is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbar button is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x0010AF39 File Offset: 0x00109139
		// (set) Token: 0x06003D84 RID: 15748 RVA: 0x0010AF41 File Offset: 0x00109141
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					this.UpdateButton(false);
				}
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x0010AF5C File Offset: 0x0010915C
		internal short Width
		{
			get
			{
				int num = 0;
				ToolBarButtonStyle toolBarButtonStyle = this.Style;
				Size border3DSize = SystemInformation.Border3DSize;
				if (toolBarButtonStyle != ToolBarButtonStyle.Separator)
				{
					using (Graphics graphics = this.parent.CreateGraphicsInternal())
					{
						Size buttonSize = this.parent.buttonSize;
						if (!buttonSize.IsEmpty)
						{
							num = buttonSize.Width;
							goto IL_14D;
						}
						if (this.parent.ImageList != null || !string.IsNullOrEmpty(this.Text))
						{
							Size imageSize = this.parent.ImageSize;
							Size size = Size.Ceiling(graphics.MeasureString(this.Text, this.parent.Font));
							if (this.parent.TextAlign == ToolBarTextAlign.Right)
							{
								if (size.Width == 0)
								{
									num = imageSize.Width + border3DSize.Width * 4;
								}
								else
								{
									num = imageSize.Width + size.Width + border3DSize.Width * 6;
								}
							}
							else if (imageSize.Width > size.Width)
							{
								num = imageSize.Width + border3DSize.Width * 4;
							}
							else
							{
								num = size.Width + border3DSize.Width * 4;
							}
							if (toolBarButtonStyle == ToolBarButtonStyle.DropDownButton && this.parent.DropDownArrows)
							{
								num += 15;
							}
						}
						else
						{
							num = this.parent.ButtonSize.Width;
						}
						goto IL_14D;
					}
				}
				num = border3DSize.Width * 2;
				IL_14D:
				return (short)num;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBarButton" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003D86 RID: 15750 RVA: 0x0010B0D4 File Offset: 0x001092D4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.parent != null)
			{
				int num = this.FindButtonIndex();
				if (num != -1)
				{
					this.parent.Buttons.RemoveAt(num);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x0010B110 File Offset: 0x00109310
		private int FindButtonIndex()
		{
			for (int i = 0; i < this.parent.Buttons.Count; i++)
			{
				if (this.parent.Buttons[i] == this)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x0010B150 File Offset: 0x00109350
		internal int GetButtonWidth()
		{
			int num = this.Parent.ButtonSize.Width;
			NativeMethods.TBBUTTONINFO tbbuttoninfo = default(NativeMethods.TBBUTTONINFO);
			tbbuttoninfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO));
			tbbuttoninfo.dwMask = 64;
			int num2 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.Parent, this.Parent.Handle), NativeMethods.TB_GETBUTTONINFO, this.commandId, ref tbbuttoninfo);
			if (num2 != -1)
			{
				num = (int)tbbuttoninfo.cx;
			}
			return num;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x0010B1D2 File Offset: 0x001093D2
		private bool GetPushedState()
		{
			if ((int)this.parent.SendMessage(1034, this.FindButtonIndex(), 0) != 0)
			{
				this.pushed = true;
			}
			else
			{
				this.pushed = false;
			}
			return this.pushed;
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x0010B208 File Offset: 0x00109408
		internal NativeMethods.TBBUTTON GetTBBUTTON(int commandId)
		{
			NativeMethods.TBBUTTON tbbutton = default(NativeMethods.TBBUTTON);
			tbbutton.iBitmap = this.ImageIndexer.ActualIndex;
			tbbutton.fsState = 0;
			if (this.enabled)
			{
				tbbutton.fsState |= 4;
			}
			if (this.partialPush && this.style == ToolBarButtonStyle.ToggleButton)
			{
				tbbutton.fsState |= 16;
			}
			if (this.pushed)
			{
				tbbutton.fsState |= 1;
			}
			if (!this.visible)
			{
				tbbutton.fsState |= 8;
			}
			switch (this.style)
			{
			case ToolBarButtonStyle.PushButton:
				tbbutton.fsStyle = 0;
				break;
			case ToolBarButtonStyle.ToggleButton:
				tbbutton.fsStyle = 2;
				break;
			case ToolBarButtonStyle.Separator:
				tbbutton.fsStyle = 1;
				break;
			case ToolBarButtonStyle.DropDownButton:
				tbbutton.fsStyle = 8;
				break;
			}
			tbbutton.dwData = (IntPtr)0;
			tbbutton.iString = this.stringIndex;
			this.commandId = commandId;
			tbbutton.idCommand = commandId;
			return tbbutton;
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x0010B308 File Offset: 0x00109508
		internal NativeMethods.TBBUTTONINFO GetTBBUTTONINFO(bool updateText, int newCommandId)
		{
			NativeMethods.TBBUTTONINFO tbbuttoninfo = default(NativeMethods.TBBUTTONINFO);
			tbbuttoninfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO));
			tbbuttoninfo.dwMask = 13;
			if (updateText)
			{
				tbbuttoninfo.dwMask |= 2;
			}
			tbbuttoninfo.iImage = this.ImageIndexer.ActualIndex;
			if (newCommandId != this.commandId)
			{
				this.commandId = newCommandId;
				tbbuttoninfo.idCommand = newCommandId;
				tbbuttoninfo.dwMask |= 32;
			}
			tbbuttoninfo.fsState = 0;
			if (this.enabled)
			{
				tbbuttoninfo.fsState |= 4;
			}
			if (this.partialPush && this.style == ToolBarButtonStyle.ToggleButton)
			{
				tbbuttoninfo.fsState |= 16;
			}
			if (this.pushed)
			{
				tbbuttoninfo.fsState |= 1;
			}
			if (!this.visible)
			{
				tbbuttoninfo.fsState |= 8;
			}
			switch (this.style)
			{
			case ToolBarButtonStyle.PushButton:
				tbbuttoninfo.fsStyle = 0;
				break;
			case ToolBarButtonStyle.ToggleButton:
				tbbuttoninfo.fsStyle = 2;
				break;
			case ToolBarButtonStyle.Separator:
				tbbuttoninfo.fsStyle = 1;
				break;
			}
			if (this.text == null)
			{
				tbbuttoninfo.pszText = Marshal.StringToHGlobalAuto("\0\0");
			}
			else
			{
				string text = this.text;
				this.PrefixAmpersands(ref text);
				tbbuttoninfo.pszText = Marshal.StringToHGlobalAuto(text);
			}
			return tbbuttoninfo;
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x0010B45C File Offset: 0x0010965C
		private void PrefixAmpersands(ref string value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			if (value.IndexOf('&') < 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '&')
				{
					if (i < value.Length - 1 && value[i + 1] == '&')
					{
						i++;
					}
					stringBuilder.Append("&&");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			value = stringBuilder.ToString();
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBarButton" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ToolBarButton" />.</returns>
		// Token: 0x06003D8D RID: 15757 RVA: 0x0010B4EB File Offset: 0x001096EB
		public override string ToString()
		{
			return "ToolBarButton: " + this.Text + ", Style: " + this.Style.ToString("G");
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x0010B517 File Offset: 0x00109717
		internal void UpdateButton(bool recreate)
		{
			this.UpdateButton(recreate, false, true);
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x0010B524 File Offset: 0x00109724
		private void UpdateButton(bool recreate, bool updateText, bool updatePushedState)
		{
			if (this.style == ToolBarButtonStyle.DropDownButton && this.parent != null && this.parent.DropDownArrows)
			{
				recreate = true;
			}
			if (updatePushedState && this.parent != null && this.parent.IsHandleCreated)
			{
				this.GetPushedState();
			}
			if (this.parent != null)
			{
				int num = this.FindButtonIndex();
				if (num != -1)
				{
					this.parent.InternalSetButton(num, this, recreate, updateText);
				}
			}
		}

		// Token: 0x04002411 RID: 9233
		private string text;

		// Token: 0x04002412 RID: 9234
		private string name;

		// Token: 0x04002413 RID: 9235
		private string tooltipText;

		// Token: 0x04002414 RID: 9236
		private bool enabled = true;

		// Token: 0x04002415 RID: 9237
		private bool visible = true;

		// Token: 0x04002416 RID: 9238
		private bool pushed;

		// Token: 0x04002417 RID: 9239
		private bool partialPush;

		// Token: 0x04002418 RID: 9240
		private int commandId = -1;

		// Token: 0x04002419 RID: 9241
		private ToolBarButton.ToolBarButtonImageIndexer imageIndexer;

		// Token: 0x0400241A RID: 9242
		private ToolBarButtonStyle style = ToolBarButtonStyle.PushButton;

		// Token: 0x0400241B RID: 9243
		private object userData;

		// Token: 0x0400241C RID: 9244
		internal IntPtr stringIndex = (IntPtr)(-1);

		// Token: 0x0400241D RID: 9245
		internal ToolBar parent;

		// Token: 0x0400241E RID: 9246
		internal Menu dropDownMenu;

		// Token: 0x020007F4 RID: 2036
		internal class ToolBarButtonImageIndexer : ImageList.Indexer
		{
			// Token: 0x06006E76 RID: 28278 RVA: 0x001945CA File Offset: 0x001927CA
			public ToolBarButtonImageIndexer(ToolBarButton button)
			{
				this.owner = button;
			}

			// Token: 0x17001824 RID: 6180
			// (get) Token: 0x06006E77 RID: 28279 RVA: 0x001945D9 File Offset: 0x001927D9
			// (set) Token: 0x06006E78 RID: 28280 RVA: 0x000070A6 File Offset: 0x000052A6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner != null && this.owner.parent != null)
					{
						return this.owner.parent.ImageList;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x040042DF RID: 17119
			private ToolBarButton owner;
		}
	}
}
