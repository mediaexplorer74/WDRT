using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using Accessibility;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows combo box control.</summary>
	// Token: 0x0200015E RID: 350
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[DefaultBindingProperty("Text")]
	[Designer("System.Windows.Forms.Design.ComboBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionComboBox")]
	public class ComboBox : ListControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ComboBox" /> class.</summary>
		// Token: 0x06000DDB RID: 3547 RVA: 0x00027B20 File Offset: 0x00025D20
		public ComboBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
			this.requestedHeight = 150;
			base.SetState2(2048, true);
		}

		/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. The values are <see cref="F:System.Windows.Forms.AutoCompleteMode.Append" />, <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />, <see cref="F:System.Windows.Forms.AutoCompleteMode.Suggest" />, and <see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend" />. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />.</exception>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00027BAF File Offset: 0x00025DAF
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x00027BB8 File Offset: 0x00025DB8
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("ComboBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.autoCompleteMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteMode));
				}
				if (this.DropDownStyle == ComboBoxStyle.DropDownList && this.AutoCompleteSource != AutoCompleteSource.ListItems && value != AutoCompleteMode.None)
				{
					throw new NotSupportedException(SR.GetString("ComboBoxAutoCompleteModeOnlyNoneAllowed"));
				}
				if (Application.OleRequired() != ApartmentState.STA)
				{
					throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
				}
				bool flag = false;
				if (this.autoCompleteMode != AutoCompleteMode.None && value == AutoCompleteMode.None)
				{
					flag = true;
				}
				this.autoCompleteMode = value;
				this.SetAutoComplete(flag, true);
			}
		}

		/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. The options are <see langword="AllSystemSources" />, <see langword="AllUrl" />, <see langword="FileSystem" />, <see langword="HistoryList" />, <see langword="RecentlyUsedList" />, <see langword="CustomSource" />, and <see langword="None" />. The default is <see langword="None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />.</exception>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00027C49 File Offset: 0x00025E49
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00027C54 File Offset: 0x00025E54
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("ComboBoxAutoCompleteSourceDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.autoCompleteSource;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 128, 7, 6, 64, 1, 32, 2, 256, 4 }))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteSource));
				}
				if (this.DropDownStyle == ComboBoxStyle.DropDownList && this.AutoCompleteMode != AutoCompleteMode.None && value != AutoCompleteSource.ListItems)
				{
					throw new NotSupportedException(SR.GetString("ComboBoxAutoCompleteSourceOnlyListItemsAllowed"));
				}
				if (Application.OleRequired() != ApartmentState.STA)
				{
					throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
				}
				if (value != AutoCompleteSource.None && value != AutoCompleteSource.CustomSource && value != AutoCompleteSource.ListItems)
				{
					new FileIOPermission(PermissionState.Unrestricted)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Demand();
				}
				this.autoCompleteSource = value;
				this.SetAutoComplete(false, true);
			}
		}

		/// <summary>Gets or sets a custom <see cref="T:System.Collections.Specialized.StringCollection" /> to use when the <see cref="P:System.Windows.Forms.ComboBox.AutoCompleteSource" /> property is set to <see langword="CustomSource" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> to use with <see cref="P:System.Windows.Forms.ComboBox.AutoCompleteSource" />.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00027D0F File Offset: 0x00025F0F
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x00027D44 File Offset: 0x00025F44
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxAutoCompleteCustomSourceDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				if (this.autoCompleteCustomSource == null)
				{
					this.autoCompleteCustomSource = new AutoCompleteStringCollection();
					this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
				}
				return this.autoCompleteCustomSource;
			}
			set
			{
				if (this.autoCompleteCustomSource != value)
				{
					if (this.autoCompleteCustomSource != null)
					{
						this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
					}
					this.autoCompleteCustomSource = value;
					if (this.autoCompleteCustomSource != null)
					{
						this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
					}
					this.SetAutoComplete(false, true);
				}
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A color object that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00027DA7 File Offset: 0x00025FA7
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x00012D84 File Offset: 0x00010F84
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (<see langword="Center" />, <see langword="None" />, <see langword="Stretch" />, <see langword="Tile" />, or <see langword="Zoom" />).</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.ImageLayout" />.</exception>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ComboBox.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06000DE8 RID: 3560 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06000DE9 RID: 3561 RVA: 0x00011896 File Offset: 0x0000FA96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ComboBox.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06000DEA RID: 3562 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06000DEB RID: 3563 RVA: 0x000118B9 File Offset: 0x0000FAB9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00027DBD File Offset: 0x00025FBD
		internal ComboBox.ChildAccessibleObject ChildEditAccessibleObject
		{
			get
			{
				if (this.childEditAccessibleObject == null)
				{
					this.childEditAccessibleObject = new ComboBox.ComboBoxChildEditUiaProvider(this, this.childEdit.Handle);
				}
				return this.childEditAccessibleObject;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00027DE4 File Offset: 0x00025FE4
		internal ComboBox.ChildAccessibleObject ChildListAccessibleObject
		{
			get
			{
				if (this.childListAccessibleObject == null)
				{
					this.childListAccessibleObject = new ComboBox.ComboBoxChildListUiaProvider(this, (this.DropDownStyle == ComboBoxStyle.Simple) ? this.childListBox.Handle : this.dropDownHandle);
				}
				return this.childListAccessibleObject;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00027E1B File Offset: 0x0002601B
		internal AccessibleObject ChildTextAccessibleObject
		{
			get
			{
				if (this.childTextAccessibleObject == null)
				{
					this.childTextAccessibleObject = new ComboBox.ComboBoxChildTextUiaProvider(this);
				}
				return this.childTextAccessibleObject;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00027E38 File Offset: 0x00026038
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "COMBOBOX";
				createParams.Style |= 2097728;
				createParams.ExStyle |= 512;
				if (!this.integralHeight)
				{
					createParams.Style |= 1024;
				}
				switch (this.DropDownStyle)
				{
				case ComboBoxStyle.Simple:
					createParams.Style |= 1;
					break;
				case ComboBoxStyle.DropDown:
					createParams.Style |= 2;
					createParams.Height = this.PreferredHeight;
					break;
				case ComboBoxStyle.DropDownList:
					createParams.Style |= 3;
					createParams.Height = this.PreferredHeight;
					break;
				}
				DrawMode drawMode = this.DrawMode;
				if (drawMode != DrawMode.OwnerDrawFixed)
				{
					if (drawMode == DrawMode.OwnerDrawVariable)
					{
						createParams.Style |= 32;
					}
				}
				else
				{
					createParams.Style |= 16;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default size of the control.</returns>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00027F28 File Offset: 0x00026128
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, this.PreferredHeight);
			}
		}

		/// <summary>Gets or sets the data source for this <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IList" /> interface or an <see cref="T:System.Array" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x000259A3 File Offset: 0x00023BA3
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x000259AB File Offset: 0x00023BAB
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("ListControlDataSourceDescr")]
		public new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DrawMode" /> enumeration values. The default is <see cref="F:System.Windows.Forms.DrawMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a valid <see cref="T:System.Windows.Forms.DrawMode" /> enumeration value.</exception>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00027F38 File Offset: 0x00026138
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00027F60 File Offset: 0x00026160
		[SRCategory("CatBehavior")]
		[DefaultValue(DrawMode.Normal)]
		[SRDescription("ComboBoxDrawModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public DrawMode DrawMode
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropDrawMode, out flag);
				if (flag)
				{
					return (DrawMode)integer;
				}
				return DrawMode.Normal;
			}
			set
			{
				if (this.DrawMode != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(DrawMode));
					}
					this.ResetHeightCache();
					base.Properties.SetInteger(ComboBox.PropDrawMode, (int)value);
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets the width of the of the drop-down portion of a combo box.</summary>
		/// <returns>The width, in pixels, of the drop-down box.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is less than one.</exception>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00027FBC File Offset: 0x000261BC
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00027FE8 File Offset: 0x000261E8
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownWidthDescr")]
		public int DropDownWidth
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropDropDownWidth, out flag);
				if (flag)
				{
					return integer;
				}
				return base.Width;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownWidth", SR.GetString("InvalidArgument", new object[]
					{
						"DropDownWidth",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.Properties.GetInteger(ComboBox.PropDropDownWidth) != value)
				{
					base.Properties.SetInteger(ComboBox.PropDropDownWidth, value);
					if (base.IsHandleCreated)
					{
						base.SendMessage(352, value, 0);
					}
				}
			}
		}

		/// <summary>Gets or sets the height in pixels of the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The height, in pixels, of the drop-down box.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is less than one.</exception>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00028068 File Offset: 0x00026268
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00028090 File Offset: 0x00026290
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownHeightDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(106)]
		public int DropDownHeight
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropDropDownHeight, out flag);
				if (flag)
				{
					return integer;
				}
				return 106;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownHeight", SR.GetString("InvalidArgument", new object[]
					{
						"DropDownHeight",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.Properties.GetInteger(ComboBox.PropDropDownHeight) != value)
				{
					base.Properties.SetInteger(ComboBox.PropDropDownHeight, value);
					this.IntegralHeight = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the combo box is displaying its drop-down portion.</summary>
		/// <returns>
		///   <see langword="true" /> if the drop-down portion is displayed; otherwise, <see langword="false" />. The default is false.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x000280FE File Offset: 0x000262FE
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00028120 File Offset: 0x00026320
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxDroppedDownDescr")]
		public bool DroppedDown
		{
			get
			{
				return base.IsHandleCreated && (int)(long)base.SendMessage(343, 0, 0) != 0;
			}
			set
			{
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				base.SendMessage(335, value ? (-1) : 0, 0);
			}
		}

		/// <summary>Gets or sets the appearance of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>One of the enumeration values that specifies the appearance of the control. The options are <see langword="Flat" />, <see langword="Popup" />, <see langword="Standard" />, and <see langword="System" />. The default is <see langword="Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.FlatStyle" />.</exception>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00028144 File Offset: 0x00026344
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x0002814C File Offset: 0x0002634C
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[Localizable(true)]
		[SRDescription("ComboBoxFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				this.flatStyle = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ComboBox" /> has focus.</summary>
		/// <returns>
		///   <see langword="true" /> if this control has focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00028184 File Offset: 0x00026384
		public override bool Focused
		{
			get
			{
				if (base.Focused)
				{
					return true;
				}
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				return focus != IntPtr.Zero && ((this.childEdit != null && focus == this.childEdit.Handle) || (this.childListBox != null && focus == this.childListBox.Handle));
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0001300E File Offset: 0x0001120E
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x00013024 File Offset: 0x00011224
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should resize to avoid showing partial items.</summary>
		/// <returns>
		///   <see langword="true" /> if the list portion can contain only complete items; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000281E8 File Offset: 0x000263E8
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x000281F0 File Offset: 0x000263F0
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ComboBoxIntegralHeightDescr")]
		public bool IntegralHeight
		{
			get
			{
				return this.integralHeight;
			}
			set
			{
				if (this.integralHeight != value)
				{
					this.integralHeight = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets the height of an item in the combo box.</summary>
		/// <returns>The height, in pixels, of an item in the combo box.</returns>
		/// <exception cref="T:System.ArgumentException">The item height value is less than zero.</exception>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00028208 File Offset: 0x00026408
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x0002826C File Offset: 0x0002646C
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ComboBoxItemHeightDescr")]
		public int ItemHeight
		{
			get
			{
				DrawMode drawMode = this.DrawMode;
				if (drawMode == DrawMode.OwnerDrawFixed || drawMode == DrawMode.OwnerDrawVariable || !base.IsHandleCreated)
				{
					bool flag;
					int integer = base.Properties.GetInteger(ComboBox.PropItemHeight, out flag);
					if (flag)
					{
						return integer;
					}
					return base.FontHeight + 2;
				}
				else
				{
					int num = (int)(long)base.SendMessage(340, 0, 0);
					if (num == -1)
					{
						throw new Win32Exception();
					}
					return num;
				}
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidArgument", new object[]
					{
						"ItemHeight",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.ResetHeightCache();
				if (base.Properties.GetInteger(ComboBox.PropItemHeight) != value)
				{
					base.Properties.SetInteger(ComboBox.PropItemHeight, value);
					if (this.DrawMode != DrawMode.Normal)
					{
						this.UpdateItemHeight();
					}
				}
			}
		}

		/// <summary>Gets an object representing the collection of the items contained in this <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> representing the items in the <see cref="T:System.Windows.Forms.ComboBox" />.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x000282E7 File Offset: 0x000264E7
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				if (this.itemsCollection == null)
				{
					this.itemsCollection = new ComboBox.ObjectCollection(this);
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00028304 File Offset: 0x00026504
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x00028331 File Offset: 0x00026531
		private string MatchingText
		{
			get
			{
				string text = (string)base.Properties.GetObject(ComboBox.PropMatchingText);
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(ComboBox.PropMatchingText))
				{
					base.Properties.SetObject(ComboBox.PropMatchingText, value);
				}
			}
		}

		/// <summary>Gets or sets the maximum number of items to be shown in the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The maximum number of items of in the drop-down portion. The minimum for this property is 1 and the maximum is 100.</returns>
		/// <exception cref="T:System.ArgumentException">The maximum number is set less than one or greater than 100.</exception>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00028359 File Offset: 0x00026559
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x00028364 File Offset: 0x00026564
		[SRCategory("CatBehavior")]
		[DefaultValue(8)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxDropDownItemsDescr")]
		public int MaxDropDownItems
		{
			get
			{
				return (int)this.maxDropDownItems;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("MaxDropDownItems", SR.GetString("InvalidBoundArgument", new object[]
					{
						"MaxDropDownItems",
						value.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture),
						100.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.maxDropDownItems = (short)value;
			}
		}

		/// <summary>Gets or sets the size that is the upper limit that the <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> method can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00011A0E File Offset: 0x0000FC0E
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00011A16 File Offset: 0x0000FC16
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Gets or sets the size that is the lower limit that the <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> method can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00011A2B File Offset: 0x0000FC2B
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x00011A33 File Offset: 0x0000FC33
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Gets or sets the number of characters a user can type into the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The maximum number of characters a user can enter. Values of less than zero are reset to zero, which is the default value.</returns>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000283D7 File Offset: 0x000265D7
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x000283E9 File Offset: 0x000265E9
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxLengthDescr")]
		public int MaxLength
		{
			get
			{
				return base.Properties.GetInteger(ComboBox.PropMaxLength);
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (this.MaxLength != value)
				{
					base.Properties.SetInteger(ComboBox.PropMaxLength, value);
					if (base.IsHandleCreated)
					{
						base.SendMessage(321, value, 0);
					}
				}
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00028422 File Offset: 0x00026622
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x0002842A File Offset: 0x0002662A
		internal bool MouseIsOver
		{
			get
			{
				return this.mouseOver;
			}
			set
			{
				if (this.mouseOver != value)
				{
					this.mouseOver = value;
					if ((!base.ContainsFocus || !Application.RenderWithVisualStyles) && this.FlatStyle == FlatStyle.Popup)
					{
						base.Invalidate();
						base.Update();
					}
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06000E13 RID: 3603 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06000E14 RID: 3604 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Gets the preferred height of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The preferred height, in pixels, of the item area of the combo box.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00028460 File Offset: 0x00026660
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxPreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				if (!base.FormattingEnabled)
				{
					this.prefHeightCache = (short)(TextRenderer.MeasureText(LayoutUtils.TestString, this.Font, new Size(32767, (int)((double)base.FontHeight * 1.25)), TextFormatFlags.SingleLine).Height + SystemInformation.BorderSize.Height * 8 + this.Padding.Size.Height);
					return (int)this.prefHeightCache;
				}
				if (this.prefHeightCache < 0)
				{
					Size size = TextRenderer.MeasureText(LayoutUtils.TestString, this.Font, new Size(32767, (int)((double)base.FontHeight * 1.25)), TextFormatFlags.SingleLine);
					if (this.DropDownStyle == ComboBoxStyle.Simple)
					{
						int num = this.Items.Count + 1;
						this.prefHeightCache = (short)(size.Height * num + SystemInformation.BorderSize.Height * 16 + this.Padding.Size.Height);
					}
					else
					{
						this.prefHeightCache = (short)this.GetComboHeight();
					}
				}
				return (int)this.prefHeightCache;
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00028580 File Offset: 0x00026780
		private int GetComboHeight()
		{
			Size size = Size.Empty;
			using (WindowsFont windowsFont = WindowsFont.FromFont(this.Font))
			{
				size = WindowsGraphicsCacheManager.MeasurementGraphics.GetTextExtent("0", windowsFont);
			}
			int num = size.Height + SystemInformation.Border3DSize.Height;
			if (this.DrawMode != DrawMode.Normal)
			{
				num = this.ItemHeight;
			}
			return 2 * SystemInformation.FixedFrameBorderSize.Height + num;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0002860C File Offset: 0x0002680C
		private string[] GetStringsForAutoComplete(IList collection)
		{
			if (collection is AutoCompleteStringCollection)
			{
				string[] array = new string[this.AutoCompleteCustomSource.Count];
				for (int i = 0; i < this.AutoCompleteCustomSource.Count; i++)
				{
					array[i] = this.AutoCompleteCustomSource[i];
				}
				return array;
			}
			if (collection is ComboBox.ObjectCollection)
			{
				string[] array2 = new string[this.itemsCollection.Count];
				for (int j = 0; j < this.itemsCollection.Count; j++)
				{
					array2[j] = base.GetItemText(this.itemsCollection[j]);
				}
				return array2;
			}
			return new string[0];
		}

		/// <summary>Gets or sets the index specifying the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than or equal to -2.  
		///  -or-  
		///  The specified index is greater than or equal to the number of items in the combo box.</exception>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000286A5 File Offset: 0x000268A5
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x000286CC File Offset: 0x000268CC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedIndexDescr")]
		public override int SelectedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(327, 0, 0);
				}
				return this.selectedIndex;
			}
			set
			{
				if (this.SelectedIndex != value)
				{
					int num = 0;
					if (this.itemsCollection != null)
					{
						num = this.itemsCollection.Count;
					}
					if (value < -1 || value >= num)
					{
						throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidArgument", new object[]
						{
							"SelectedIndex",
							value.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(334, value, 0);
					}
					else
					{
						this.selectedIndex = value;
					}
					this.UpdateText();
					if (base.IsHandleCreated)
					{
						this.OnTextChanged(EventArgs.Empty);
					}
					this.OnSelectedItemChanged(EventArgs.Empty);
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets currently selected item in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The object that is the currently selected item or <see langword="null" /> if there is no currently selected item.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00028784 File Offset: 0x00026984
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x000287AC File Offset: 0x000269AC
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				int num = this.SelectedIndex;
				if (num != -1)
				{
					return this.Items[num];
				}
				return null;
			}
			set
			{
				int num = -1;
				if (this.itemsCollection != null)
				{
					if (value != null)
					{
						num = this.itemsCollection.IndexOf(value);
					}
					else
					{
						this.SelectedIndex = -1;
					}
				}
				if (num != -1)
				{
					this.SelectedIndex = num;
				}
			}
		}

		/// <summary>Gets or sets the text that is selected in the editable portion of a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>A string that represents the currently selected text in the combo box. If <see cref="P:System.Windows.Forms.ComboBox.DropDownStyle" /> is set to <see cref="F:System.Windows.Forms.ComboBoxStyle.DropDownList" />, the return value is an empty string ("").</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000287E7 File Offset: 0x000269E7
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x00028810 File Offset: 0x00026A10
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedTextDescr")]
		public string SelectedText
		{
			get
			{
				if (this.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					return "";
				}
				return this.Text.Substring(this.SelectionStart, this.SelectionLength);
			}
			set
			{
				if (this.DropDownStyle != ComboBoxStyle.DropDownList)
				{
					string text = ((value == null) ? "" : value);
					base.CreateControl();
					if (base.IsHandleCreated && this.childEdit != null)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 194, NativeMethods.InvalidIntPtr, text);
					}
				}
			}
		}

		/// <summary>Gets or sets the number of characters selected in the editable portion of the combo box.</summary>
		/// <returns>The number of characters selected in the combo box.</returns>
		/// <exception cref="T:System.ArgumentException">The value was less than zero.</exception>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0002886C File Offset: 0x00026A6C
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x000288A7 File Offset: 0x00026AA7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionLengthDescr")]
		public int SelectionLength
		{
			get
			{
				int[] array = new int[1];
				int[] array2 = new int[1];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 320, array2, array);
				return array[0] - array2[0];
			}
			set
			{
				this.Select(this.SelectionStart, value);
			}
		}

		/// <summary>Gets or sets the starting index of text selected in the combo box.</summary>
		/// <returns>The zero-based index of the first character in the string of the current text selection.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than zero.</exception>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x000288B8 File Offset: 0x00026AB8
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x000288E8 File Offset: 0x00026AE8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				int[] array = new int[1];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 320, array, null);
				return array[0];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectionStart", SR.GetString("InvalidArgument", new object[]
					{
						"SelectionStart",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.Select(value, this.SelectionLength);
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the combo box is sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to sort a <see cref="T:System.Windows.Forms.ComboBox" /> that is attached to a data source.</exception>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00028938 File Offset: 0x00026B38
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x00028940 File Offset: 0x00026B40
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ComboBoxSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted != value)
				{
					if (this.DataSource != null && value)
					{
						throw new ArgumentException(SR.GetString("ComboBoxSortWithDataSource"));
					}
					this.sorted = value;
					this.RefreshItems();
					this.SelectedIndex = -1;
				}
			}
		}

		/// <summary>Gets or sets a value specifying the style of the combo box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is <see langword="DropDown" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values.</exception>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0002897C File Offset: 0x00026B7C
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x000289A4 File Offset: 0x00026BA4
		[SRCategory("CatAppearance")]
		[DefaultValue(ComboBoxStyle.DropDown)]
		[SRDescription("ComboBoxStyleDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropStyle, out flag);
				if (flag)
				{
					return (ComboBoxStyle)integer;
				}
				return ComboBoxStyle.DropDown;
			}
			set
			{
				if (this.DropDownStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ComboBoxStyle));
					}
					if (value == ComboBoxStyle.DropDownList && this.AutoCompleteSource != AutoCompleteSource.ListItems && this.AutoCompleteMode != AutoCompleteMode.None)
					{
						this.AutoCompleteMode = AutoCompleteMode.None;
					}
					this.ResetHeightCache();
					base.Properties.SetInteger(ComboBox.PropStyle, (int)value);
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					this.OnDropDownStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00028A30 File Offset: 0x00026C30
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00028A98 File Offset: 0x00026C98
		[Localizable(true)]
		[Bindable(true)]
		public override string Text
		{
			get
			{
				if (this.SelectedItem != null && !base.BindingFieldEmpty)
				{
					if (!base.FormattingEnabled)
					{
						return base.FilterItemOnProperty(this.SelectedItem).ToString();
					}
					string itemText = base.GetItemText(this.SelectedItem);
					if (!string.IsNullOrEmpty(itemText) && string.Compare(itemText, base.Text, true, CultureInfo.CurrentCulture) == 0)
					{
						return itemText;
					}
				}
				return base.Text;
			}
			set
			{
				if (this.DropDownStyle == ComboBoxStyle.DropDownList && !base.IsHandleCreated && !string.IsNullOrEmpty(value) && this.FindStringExact(value) == -1)
				{
					return;
				}
				base.Text = value;
				object selectedItem = this.SelectedItem;
				if (!base.DesignMode)
				{
					if (value == null)
					{
						this.SelectedIndex = -1;
						return;
					}
					if (value != null && (selectedItem == null || string.Compare(value, base.GetItemText(selectedItem), false, CultureInfo.CurrentCulture) != 0))
					{
						int num = this.FindStringIgnoreCase(value);
						if (num != -1)
						{
							this.SelectedIndex = num;
						}
					}
				}
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00028B1C File Offset: 0x00026D1C
		private int FindStringIgnoreCase(string value)
		{
			int num = this.FindStringExact(value, -1, false);
			if (num == -1)
			{
				num = this.FindStringExact(value, -1, true);
			}
			return num;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00028B42 File Offset: 0x00026D42
		private void NotifyAutoComplete()
		{
			this.NotifyAutoComplete(true);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00028B4C File Offset: 0x00026D4C
		private void NotifyAutoComplete(bool setSelectedIndex)
		{
			string text = this.Text;
			bool flag = text != this.lastTextChangedValue;
			bool flag2 = false;
			if (setSelectedIndex)
			{
				int num = this.FindStringIgnoreCase(text);
				if (num != -1 && num != this.SelectedIndex)
				{
					this.SelectedIndex = num;
					this.SelectionStart = 0;
					this.SelectionLength = text.Length;
					flag2 = true;
				}
			}
			if (flag && !flag2)
			{
				this.OnTextChanged(EventArgs.Empty);
			}
			this.lastTextChangedValue = text;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00028BBB File Offset: 0x00026DBB
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00028BCF File Offset: 0x00026DCF
		private bool SystemAutoCompleteEnabled
		{
			get
			{
				return this.autoCompleteMode != AutoCompleteMode.None && this.DropDownStyle != ComboBoxStyle.DropDownList;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06000E2D RID: 3629 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x06000E2E RID: 3630 RVA: 0x00023760 File Offset: 0x00021960
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Occurs when a visual aspect of an owner-drawn <see cref="T:System.Windows.Forms.ComboBox" /> changes.</summary>
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06000E2F RID: 3631 RVA: 0x00028BE7 File Offset: 0x00026DE7
		// (remove) Token: 0x06000E30 RID: 3632 RVA: 0x00028BFA File Offset: 0x00026DFA
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DRAWITEM, value);
			}
		}

		/// <summary>Occurs when the drop-down portion of a <see cref="T:System.Windows.Forms.ComboBox" /> is shown.</summary>
		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06000E31 RID: 3633 RVA: 0x00028C0D File Offset: 0x00026E0D
		// (remove) Token: 0x06000E32 RID: 3634 RVA: 0x00028C20 File Offset: 0x00026E20
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownDescr")]
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DROPDOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DROPDOWN, value);
			}
		}

		/// <summary>Occurs each time an owner-drawn <see cref="T:System.Windows.Forms.ComboBox" /> item needs to be drawn and when the sizes of the list items are determined.</summary>
		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06000E33 RID: 3635 RVA: 0x00028C33 File Offset: 0x00026E33
		// (remove) Token: 0x06000E34 RID: 3636 RVA: 0x00028C4C File Offset: 0x00026E4C
		[SRCategory("CatBehavior")]
		[SRDescription("measureItemEventDescr")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_MEASUREITEM, value);
				this.UpdateItemHeight();
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_MEASUREITEM, value);
				this.UpdateItemHeight();
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ComboBox.SelectedIndex" /> property has changed.</summary>
		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06000E35 RID: 3637 RVA: 0x00028C65 File Offset: 0x00026E65
		// (remove) Token: 0x06000E36 RID: 3638 RVA: 0x00028C78 File Offset: 0x00026E78
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
		}

		/// <summary>Occurs when the user changes the selected item and that change is displayed in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06000E37 RID: 3639 RVA: 0x00028C8B File Offset: 0x00026E8B
		// (remove) Token: 0x06000E38 RID: 3640 RVA: 0x00028C9E File Offset: 0x00026E9E
		[SRCategory("CatBehavior")]
		[SRDescription("selectionChangeCommittedEventDescr")]
		public event EventHandler SelectionChangeCommitted
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_SELECTIONCHANGECOMMITTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_SELECTIONCHANGECOMMITTED, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ComboBox.DropDownStyle" /> property has changed.</summary>
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06000E39 RID: 3641 RVA: 0x00028CB1 File Offset: 0x00026EB1
		// (remove) Token: 0x06000E3A RID: 3642 RVA: 0x00028CC4 File Offset: 0x00026EC4
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownStyleChangedDescr")]
		public event EventHandler DropDownStyleChanged
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DROPDOWNSTYLE, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DROPDOWNSTYLE, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ComboBox" /> control is redrawn.</summary>
		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06000E3B RID: 3643 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06000E3C RID: 3644 RVA: 0x00013D7C File Offset: 0x00011F7C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Occurs when the control has formatted the text, but before the text is displayed.</summary>
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06000E3D RID: 3645 RVA: 0x00028CD7 File Offset: 0x00026ED7
		// (remove) Token: 0x06000E3E RID: 3646 RVA: 0x00028CEA File Offset: 0x00026EEA
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnTextUpdateDescr")]
		public event EventHandler TextUpdate
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_TEXTUPDATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_TEXTUPDATE, value);
			}
		}

		/// <summary>Occurs when the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" /> is no longer visible.</summary>
		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06000E3F RID: 3647 RVA: 0x00028CFD File Offset: 0x00026EFD
		// (remove) Token: 0x06000E40 RID: 3648 RVA: 0x00028D10 File Offset: 0x00026F10
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownClosedDescr")]
		public event EventHandler DropDownClosed
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DROPDOWNCLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DROPDOWNCLOSED, value);
			}
		}

		/// <summary>Adds the specified items to the combo box.</summary>
		/// <param name="value">The items to add.</param>
		// Token: 0x06000E41 RID: 3649 RVA: 0x00028D24 File Offset: 0x00026F24
		[Obsolete("This method has been deprecated.  There is no replacement.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void AddItemsCore(object[] value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			this.BeginUpdate();
			try
			{
				this.Items.AddRangeInternal(value);
			}
			finally
			{
				this.EndUpdate();
			}
		}

		/// <summary>Maintains performance when items are added to the <see cref="T:System.Windows.Forms.ComboBox" /> one at a time.</summary>
		// Token: 0x06000E42 RID: 3650 RVA: 0x00028D6C File Offset: 0x00026F6C
		public void BeginUpdate()
		{
			this.updateCount++;
			base.BeginUpdateInternal();
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00028D82 File Offset: 0x00026F82
		private void CheckNoDataSource()
		{
			if (this.DataSource != null)
			{
				throw new ArgumentException(SR.GetString("DataSourceLocksItems"));
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new accessibility object for the control.</returns>
		// Token: 0x06000E44 RID: 3652 RVA: 0x00028D9C File Offset: 0x00026F9C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ComboBox.ComboBoxUiaProvider(this);
			}
			if (AccessibilityImprovements.Level1)
			{
				return new ComboBox.ComboBoxExAccessibleObject(this);
			}
			return new ComboBox.ComboBoxAccessibleObject(this);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00028DC0 File Offset: 0x00026FC0
		internal bool UpdateNeeded()
		{
			return this.updateCount == 0;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00028DCC File Offset: 0x00026FCC
		internal Point EditToComboboxMapping(Message m)
		{
			if (this.childEdit == null)
			{
				return new Point(0, 0);
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
			NativeMethods.RECT rect2 = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.childEdit.Handle), ref rect2);
			int num = NativeMethods.Util.SignedLOWORD(m.LParam) + (rect2.left - rect.left);
			int num2 = NativeMethods.Util.SignedHIWORD(m.LParam) + (rect2.top - rect.top);
			return new Point(num, num2);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00028E64 File Offset: 0x00027064
		private void ChildWndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 48)
			{
				if (msg <= 8)
				{
					if (msg != 7)
					{
						if (msg == 8)
						{
							if (!base.DesignMode)
							{
								base.OnImeContextStatusChanged(m.HWnd);
							}
							this.DefChildWndProc(ref m);
							if (this.fireLostFocus)
							{
								base.InvokeLostFocus(this, EventArgs.Empty);
							}
							if (this.FlatStyle == FlatStyle.Popup)
							{
								base.Invalidate();
								return;
							}
							return;
						}
					}
					else
					{
						if (!base.DesignMode)
						{
							ImeContext.SetImeStatus(base.CachedImeMode, m.HWnd);
						}
						if (!base.HostedInWin32DialogManager)
						{
							IContainerControl containerControlInternal = base.GetContainerControlInternal();
							if (containerControlInternal != null)
							{
								ContainerControl containerControl = containerControlInternal as ContainerControl;
								if (containerControl != null && !containerControl.ActivateControlInternal(this, false))
								{
									return;
								}
							}
						}
						this.DefChildWndProc(ref m);
						if (this.fireSetFocus)
						{
							if (!base.DesignMode && this.childEdit != null && m.HWnd == this.childEdit.Handle && !LocalAppContextSwitches.EnableLegacyIMEFocusInComboBox)
							{
								base.WmImeSetFocus();
							}
							base.InvokeGotFocus(this, EventArgs.Empty);
						}
						if (this.FlatStyle == FlatStyle.Popup)
						{
							base.Invalidate();
							return;
						}
						return;
					}
				}
				else if (msg != 32)
				{
					if (msg == 48)
					{
						this.DefChildWndProc(ref m);
						if (this.childEdit != null && m.HWnd == this.childEdit.Handle)
						{
							UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 211, 3, 0);
							return;
						}
						return;
					}
				}
				else
				{
					if (this.Cursor != this.DefaultCursor && this.childEdit != null && m.HWnd == this.childEdit.Handle && NativeMethods.Util.LOWORD(m.LParam) == 1)
					{
						Cursor.CurrentInternal = this.Cursor;
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				}
			}
			else if (msg <= 123)
			{
				if (msg == 81)
				{
					this.DefChildWndProc(ref m);
					return;
				}
				if (msg == 123)
				{
					if (this.ContextMenu != null || this.ContextMenuStrip != null)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 123, m.WParam, m.LParam);
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				}
			}
			else
			{
				switch (msg)
				{
				case 256:
				case 260:
					if (this.SystemAutoCompleteEnabled && !ComboBox.ACNativeWindow.AutoCompleteActive)
					{
						this.finder.FindDropDowns(false);
					}
					if (this.AutoCompleteMode != AutoCompleteMode.None)
					{
						char c = (char)(long)m.WParam;
						if (c == '\u001b')
						{
							this.DroppedDown = false;
						}
						else if (c == '\r' && this.DroppedDown)
						{
							this.UpdateText();
							this.OnSelectionChangeCommittedInternal(EventArgs.Empty);
							this.DroppedDown = false;
						}
					}
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
						return;
					}
					if (base.PreProcessControlMessage(ref m) == PreProcessControlState.MessageProcessed)
					{
						return;
					}
					if (this.ProcessKeyMessage(ref m))
					{
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				case 257:
				case 261:
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
					}
					else if (base.PreProcessControlMessage(ref m) != PreProcessControlState.MessageProcessed)
					{
						if (this.ProcessKeyMessage(ref m))
						{
							return;
						}
						this.DefChildWndProc(ref m);
					}
					if (this.SystemAutoCompleteEnabled && !ComboBox.ACNativeWindow.AutoCompleteActive)
					{
						this.finder.FindDropDowns();
						return;
					}
					return;
				case 258:
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
						return;
					}
					if (base.PreProcessControlMessage(ref m) == PreProcessControlState.MessageProcessed)
					{
						return;
					}
					if (this.ProcessKeyMessage(ref m))
					{
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				case 259:
					break;
				case 262:
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
						return;
					}
					if (base.PreProcessControlMessage(ref m) == PreProcessControlState.MessageProcessed)
					{
						return;
					}
					if (this.ProcessKeyEventArgs(ref m))
					{
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				default:
					switch (msg)
					{
					case 512:
					{
						Point point = this.EditToComboboxMapping(m);
						this.DefChildWndProc(ref m);
						this.OnMouseEnterInternal(EventArgs.Empty);
						this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, point.X, point.Y, 0));
						return;
					}
					case 513:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point2 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, point2.X, point2.Y, 0));
						return;
					}
					case 514:
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
						Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
						int num = NativeMethods.Util.SignedLOWORD(m.LParam);
						int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
						Point point3 = new Point(num, num2);
						point3 = base.PointToScreen(point3);
						if (this.mouseEvents && !base.ValidationCancelled)
						{
							this.mouseEvents = false;
							if (this.mousePressed)
							{
								if (rectangle.Contains(point3))
								{
									this.mousePressed = false;
									this.OnClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
									this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								}
								else
								{
									this.mousePressed = false;
									this.mouseInEdit = false;
									this.OnMouseLeave(EventArgs.Empty);
								}
							}
						}
						this.DefChildWndProc(ref m);
						base.CaptureInternal = false;
						point3 = this.EditToComboboxMapping(m);
						this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, point3.X, point3.Y, 0));
						return;
					}
					case 515:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point4 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, point4.X, point4.Y, 0));
						return;
					}
					case 516:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						if (this.ContextMenu != null || this.ContextMenuStrip != null)
						{
							base.CaptureInternal = true;
						}
						this.DefChildWndProc(ref m);
						Point point5 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, point5.X, point5.Y, 0));
						return;
					}
					case 517:
					{
						this.mousePressed = false;
						this.mouseEvents = false;
						if (this.ContextMenu != null)
						{
							base.CaptureInternal = false;
						}
						this.DefChildWndProc(ref m);
						Point point6 = this.EditToComboboxMapping(m);
						this.OnMouseUp(new MouseEventArgs(MouseButtons.Right, 1, point6.X, point6.Y, 0));
						return;
					}
					case 518:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point7 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, point7.X, point7.Y, 0));
						return;
					}
					case 519:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point8 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Middle, 1, point8.X, point8.Y, 0));
						return;
					}
					case 520:
						this.mousePressed = false;
						this.mouseEvents = false;
						base.CaptureInternal = false;
						this.DefChildWndProc(ref m);
						this.OnMouseUp(new MouseEventArgs(MouseButtons.Middle, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						return;
					case 521:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point9 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Middle, 1, point9.X, point9.Y, 0));
						return;
					}
					default:
						if (msg == 675)
						{
							this.DefChildWndProc(ref m);
							this.OnMouseLeaveInternal(EventArgs.Empty);
							return;
						}
						break;
					}
					break;
				}
			}
			this.DefChildWndProc(ref m);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000296C3 File Offset: 0x000278C3
		private void OnMouseEnterInternal(EventArgs args)
		{
			if (!this.mouseInEdit)
			{
				this.OnMouseEnter(args);
				this.mouseInEdit = true;
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000296DC File Offset: 0x000278DC
		private void OnMouseLeaveInternal(EventArgs args)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
			Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
			Point mousePosition = Control.MousePosition;
			if (!rectangle.Contains(mousePosition))
			{
				this.OnMouseLeave(args);
				this.mouseInEdit = false;
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00029750 File Offset: 0x00027950
		private void DefChildWndProc(ref Message m)
		{
			if (this.childEdit != null)
			{
				NativeWindow nativeWindow;
				if (m.HWnd == this.childEdit.Handle)
				{
					nativeWindow = this.childEdit;
				}
				else if (AccessibilityImprovements.Level3 && m.HWnd == this.dropDownHandle)
				{
					nativeWindow = this.childDropDown;
				}
				else
				{
					nativeWindow = this.childListBox;
				}
				if (nativeWindow != null)
				{
					nativeWindow.DefWndProc(ref m);
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ComboBox" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000E4B RID: 3659 RVA: 0x000297BC File Offset: 0x000279BC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.autoCompleteCustomSource != null)
				{
					this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
				}
				if (this.stringSource != null)
				{
					this.stringSource.ReleaseAutoComplete();
					this.stringSource = null;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ComboBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ComboBox.BeginUpdate" /> method.</summary>
		// Token: 0x06000E4C RID: 3660 RVA: 0x0002980C File Offset: 0x00027A0C
		public void EndUpdate()
		{
			this.updateCount--;
			if (this.updateCount == 0 && this.AutoCompleteSource == AutoCompleteSource.ListItems)
			{
				this.SetAutoComplete(false, false);
			}
			if (base.EndUpdateInternal())
			{
				if (this.childEdit != null && this.childEdit.Handle != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(this, this.childEdit.Handle), null, false);
				}
				if (this.childListBox != null && this.childListBox.Handle != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(this, this.childListBox.Handle), null, false);
				}
			}
		}

		/// <summary>Returns the index of the first item in the <see cref="T:System.Windows.Forms.ComboBox" /> that starts with the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		// Token: 0x06000E4D RID: 3661 RVA: 0x000298BC File Offset: 0x00027ABC
		public int FindString(string s)
		{
			return this.FindString(s, -1);
		}

		/// <summary>Returns the index of the first item in the <see cref="T:System.Windows.Forms.ComboBox" /> beyond the specified index that contains the specified string. The search is not case sensitive.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the <paramref name="s" /> parameter specifies <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> is less than -1.  
		///  -or-  
		///  The <paramref name="startIndex" /> is greater than the last index in the collection.</exception>
		// Token: 0x06000E4E RID: 3662 RVA: 0x000298C8 File Offset: 0x00027AC8
		public int FindString(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			if (this.itemsCollection == null || this.itemsCollection.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.itemsCollection.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, false);
		}

		/// <summary>Finds the first item in the combo box that matches the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the <paramref name="s" /> parameter specifies <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x06000E4F RID: 3663 RVA: 0x0002991D File Offset: 0x00027B1D
		public int FindStringExact(string s)
		{
			return this.FindStringExact(s, -1, true);
		}

		/// <summary>Finds the first item after the specified index that matches the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the <paramref name="s" /> parameter specifies <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> is less than -1.  
		///  -or-  
		///  The <paramref name="startIndex" /> is equal to the last index in the collection.</exception>
		// Token: 0x06000E50 RID: 3664 RVA: 0x00029928 File Offset: 0x00027B28
		public int FindStringExact(string s, int startIndex)
		{
			return this.FindStringExact(s, startIndex, true);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00029934 File Offset: 0x00027B34
		internal int FindStringExact(string s, int startIndex, bool ignorecase)
		{
			if (s == null)
			{
				return -1;
			}
			if (this.itemsCollection == null || this.itemsCollection.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.itemsCollection.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, true, ignorecase);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002998A File Offset: 0x00027B8A
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			if (this.DropDownStyle == ComboBoxStyle.DropDown || this.DropDownStyle == ComboBoxStyle.DropDownList)
			{
				proposedHeight = this.PreferredHeight;
			}
			return base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, proposedHeight);
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A  value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06000E53 RID: 3667 RVA: 0x000299B1 File Offset: 0x00027BB1
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			if (factor.Width != 1f && factor.Height != 1f)
			{
				this.ResetHeightCache();
			}
			base.ScaleControl(factor, specified);
		}

		/// <summary>Returns the height of an item in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <param name="index">The index of the item to return the height of.</param>
		/// <returns>The height, in pixels, of the item at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is less than zero.  
		///  -or-  
		///  The <paramref name="index" /> is greater than count of items in the list.</exception>
		// Token: 0x06000E54 RID: 3668 RVA: 0x000299E0 File Offset: 0x00027BE0
		public int GetItemHeight(int index)
		{
			if (this.DrawMode != DrawMode.OwnerDrawVariable)
			{
				return this.ItemHeight;
			}
			if (index < 0 || this.itemsCollection == null || index >= this.itemsCollection.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!base.IsHandleCreated)
			{
				return this.ItemHeight;
			}
			int num = (int)(long)base.SendMessage(340, index, 0);
			if (num == -1)
			{
				throw new Win32Exception();
			}
			return num;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00029A77 File Offset: 0x00027C77
		internal IntPtr GetListHandle()
		{
			if (this.DropDownStyle != ComboBoxStyle.Simple)
			{
				return this.dropDownHandle;
			}
			return this.childListBox.Handle;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00029A93 File Offset: 0x00027C93
		internal NativeWindow GetListNativeWindow()
		{
			if (this.DropDownStyle != ComboBoxStyle.Simple)
			{
				return this.childDropDown;
			}
			return this.childListBox;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00029AAC File Offset: 0x00027CAC
		internal int GetListNativeWindowRuntimeIdPart()
		{
			NativeWindow listNativeWindow = this.GetListNativeWindow();
			if (listNativeWindow == null)
			{
				return 0;
			}
			return listNativeWindow.GetHashCode();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00029ACC File Offset: 0x00027CCC
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (msg == 312 && !this.ShouldSerializeBackColor())
			{
				return IntPtr.Zero;
			}
			if (msg == 308 && base.GetStyle(ControlStyles.UserPaint))
			{
				SafeNativeMethods.SetTextColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.ForeColor));
				SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.BackColor));
				return base.BackColorBrush;
			}
			return base.InitializeDCForWmCtlColor(dc, msg);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00029B40 File Offset: 0x00027D40
		private bool InterceptAutoCompleteKeystroke(Message m)
		{
			if (m.Msg == 256)
			{
				if ((int)(long)m.WParam == 46)
				{
					this.MatchingText = "";
					this.autoCompleteTimeStamp = DateTime.Now.Ticks;
					if (this.Items.Count > 0)
					{
						this.SelectedIndex = 0;
					}
					return false;
				}
			}
			else if (m.Msg == 258)
			{
				char c = (char)(long)m.WParam;
				if (c == '\b')
				{
					if (DateTime.Now.Ticks - this.autoCompleteTimeStamp > 10000000L || this.MatchingText.Length <= 1)
					{
						this.MatchingText = "";
						if (this.Items.Count > 0)
						{
							this.SelectedIndex = 0;
						}
					}
					else
					{
						this.MatchingText = this.MatchingText.Remove(this.MatchingText.Length - 1);
						this.SelectedIndex = this.FindString(this.MatchingText);
					}
					this.autoCompleteTimeStamp = DateTime.Now.Ticks;
					return false;
				}
				if (c == '\u001b')
				{
					this.MatchingText = "";
				}
				if (c != '\u001b' && c != '\r' && !this.DroppedDown && this.AutoCompleteMode != AutoCompleteMode.Append)
				{
					this.DroppedDown = true;
				}
				string text;
				if (DateTime.Now.Ticks - this.autoCompleteTimeStamp > 10000000L)
				{
					text = new string(c, 1);
					if (this.FindString(text) != -1)
					{
						this.MatchingText = text;
					}
					this.autoCompleteTimeStamp = DateTime.Now.Ticks;
					return false;
				}
				text = this.MatchingText + c.ToString();
				int num = this.FindString(text);
				if (num != -1)
				{
					this.MatchingText = text;
					if (num != this.SelectedIndex)
					{
						this.SelectedIndex = num;
					}
				}
				this.autoCompleteTimeStamp = DateTime.Now.Ticks;
				return true;
			}
			return false;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00029D23 File Offset: 0x00027F23
		private void InvalidateEverything()
		{
			SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1157);
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000E5B RID: 3675 RVA: 0x00029D44 File Offset: 0x00027F44
		protected override bool IsInputKey(Keys keyData)
		{
			Keys keys = keyData & (Keys.KeyCode | Keys.Alt);
			if (keys == Keys.Return || keys == Keys.Escape)
			{
				if (this.DroppedDown || this.autoCompleteDroppedDown)
				{
					return true;
				}
				if (this.SystemAutoCompleteEnabled && ComboBox.ACNativeWindow.AutoCompleteActive)
				{
					this.autoCompleteDroppedDown = true;
					return true;
				}
			}
			return base.IsInputKey(keyData);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00029D94 File Offset: 0x00027F94
		private int NativeAdd(object item)
		{
			int num = (int)(long)base.SendMessage(323, 0, base.GetItemText(item));
			if (num < 0)
			{
				throw new OutOfMemoryException(SR.GetString("ComboBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00029DD0 File Offset: 0x00027FD0
		private void NativeClear()
		{
			string text = null;
			if (this.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				text = this.WindowText;
			}
			base.SendMessage(331, 0, 0);
			if (text != null)
			{
				this.WindowText = text;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00029E08 File Offset: 0x00028008
		private string NativeGetItemText(int index)
		{
			int num = (int)(long)base.SendMessage(329, index, 0);
			StringBuilder stringBuilder = new StringBuilder(num + 1);
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 328, index, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00029E54 File Offset: 0x00028054
		private int NativeInsert(int index, object item)
		{
			int num = (int)(long)base.SendMessage(330, index, base.GetItemText(item));
			if (num < 0)
			{
				throw new OutOfMemoryException(SR.GetString("ComboBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00029E90 File Offset: 0x00028090
		private void NativeRemoveAt(int index)
		{
			if (this.DropDownStyle == ComboBoxStyle.DropDownList && this.SelectedIndex == index)
			{
				base.Invalidate();
			}
			base.SendMessage(324, index, 0);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00029EB8 File Offset: 0x000280B8
		internal override void RecreateHandleCore()
		{
			string windowText = this.WindowText;
			base.RecreateHandleCore();
			if (!string.IsNullOrEmpty(windowText) && string.IsNullOrEmpty(this.WindowText))
			{
				this.WindowText = windowText;
			}
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x06000E62 RID: 3682 RVA: 0x00029EF0 File Offset: 0x000280F0
		protected override void CreateHandle()
		{
			using (new LayoutTransaction(this.ParentInternal, this, PropertyNames.Bounds))
			{
				base.CreateHandle();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E63 RID: 3683 RVA: 0x00029F34 File Offset: 0x00028134
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.MaxLength > 0)
			{
				base.SendMessage(321, this.MaxLength, 0);
			}
			bool flag = this.childEdit == null && this.childListBox == null;
			if (flag && this.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				IntPtr intPtr = UnsafeNativeMethods.GetWindow(new HandleRef(this, base.Handle), 5);
				if (intPtr != IntPtr.Zero)
				{
					if (this.DropDownStyle == ComboBoxStyle.Simple)
					{
						this.childListBox = new ComboBox.ComboBoxChildNativeWindow(this, ComboBox.ChildWindowType.ListBox);
						this.childListBox.AssignHandle(intPtr);
						intPtr = UnsafeNativeMethods.GetWindow(new HandleRef(this, intPtr), 2);
					}
					this.childEdit = new ComboBox.ComboBoxChildNativeWindow(this, ComboBox.ChildWindowType.Edit);
					this.childEdit.AssignHandle(intPtr);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 211, 3, 0);
				}
			}
			bool flag2;
			int integer = base.Properties.GetInteger(ComboBox.PropDropDownWidth, out flag2);
			if (flag2)
			{
				base.SendMessage(352, integer, 0);
			}
			flag2 = false;
			int integer2 = base.Properties.GetInteger(ComboBox.PropItemHeight, out flag2);
			if (flag2)
			{
				this.UpdateItemHeight();
			}
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				base.Height = this.requestedHeight;
			}
			try
			{
				this.fromHandleCreate = true;
				this.SetAutoComplete(false, false);
			}
			finally
			{
				this.fromHandleCreate = false;
			}
			if (this.itemsCollection != null)
			{
				foreach (object obj in this.itemsCollection)
				{
					this.NativeAdd(obj);
				}
				if (this.selectedIndex >= 0)
				{
					base.SendMessage(334, this.selectedIndex, 0);
					this.UpdateText();
					this.selectedIndex = -1;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E64 RID: 3684 RVA: 0x0002A118 File Offset: 0x00028318
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.dropDownHandle = IntPtr.Zero;
			if (base.Disposing)
			{
				this.itemsCollection = null;
				this.selectedIndex = -1;
			}
			else
			{
				this.selectedIndex = this.SelectedIndex;
			}
			if (this.stringSource != null)
			{
				this.stringSource.ReleaseAutoComplete();
				this.stringSource = null;
			}
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06000E65 RID: 3685 RVA: 0x0002A178 File Offset: 0x00028378
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[ComboBox.EVENT_DRAWITEM];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDown" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E66 RID: 3686 RVA: 0x0002A1A8 File Offset: 0x000283A8
		protected virtual void OnDropDown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_DROPDOWN];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (AccessibilityImprovements.Level3 && base.IsHandleCreated)
			{
				base.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Collapsed, UnsafeNativeMethods.ExpandCollapseState.Expanded);
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null)
				{
					comboBoxUiaProvider.SetComboBoxItemFocus();
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06000E67 RID: 3687 RVA: 0x0002A218 File Offset: 0x00028418
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (this.SystemAutoCompleteEnabled)
			{
				if (e.KeyCode == Keys.Return)
				{
					this.NotifyAutoComplete(true);
				}
				else if (e.KeyCode == Keys.Escape && this.autoCompleteDroppedDown)
				{
					this.NotifyAutoComplete(false);
				}
				this.autoCompleteDroppedDown = false;
			}
			base.OnKeyDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06000E68 RID: 3688 RVA: 0x0002A268 File Offset: 0x00028468
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (!e.Handled && (e.KeyChar == '\r' || e.KeyChar == '\u001b') && this.DroppedDown)
			{
				this.dropDown = false;
				if (base.FormattingEnabled)
				{
					this.Text = this.WindowText;
					this.SelectAll();
					e.Handled = false;
					return;
				}
				this.DroppedDown = false;
				e.Handled = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.MeasureItem" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that was raised.</param>
		// Token: 0x06000E69 RID: 3689 RVA: 0x0002A2D8 File Offset: 0x000284D8
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[ComboBox.EVENT_MEASUREITEM];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E6A RID: 3690 RVA: 0x0002A306 File Offset: 0x00028506
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.MouseIsOver = true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E6B RID: 3691 RVA: 0x0002A316 File Offset: 0x00028516
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.MouseIsOver = false;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0002A328 File Offset: 0x00028528
		private void OnSelectionChangeCommittedInternal(EventArgs e)
		{
			if (this.allowCommit)
			{
				try
				{
					this.allowCommit = false;
					this.OnSelectionChangeCommitted(e);
				}
				finally
				{
					this.allowCommit = true;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectionChangeCommitted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E6D RID: 3693 RVA: 0x0002A368 File Offset: 0x00028568
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_SELECTIONCHANGECOMMITTED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.dropDown)
			{
				this.dropDownWillBeClosed = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E6E RID: 3694 RVA: 0x0002A3A8 File Offset: 0x000285A8
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_SELECTEDINDEXCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.dropDownWillBeClosed)
			{
				this.dropDownWillBeClosed = false;
			}
			else if (AccessibilityImprovements.Level3 && base.IsHandleCreated)
			{
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null && (this.DropDownStyle == ComboBoxStyle.DropDownList || this.DropDownStyle == ComboBoxStyle.DropDown))
				{
					if (this.dropDown)
					{
						comboBoxUiaProvider.SetComboBoxItemFocus();
					}
					comboBoxUiaProvider.SetComboBoxItemSelection();
				}
			}
			if (base.DataManager != null && base.DataManager.Position != this.SelectedIndex && (!base.FormattingEnabled || this.SelectedIndex != -1))
			{
				base.DataManager.Position = this.SelectedIndex;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E6F RID: 3695 RVA: 0x0002A46F File Offset: 0x0002866F
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
			this.selectedValueChangedFired = true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E70 RID: 3696 RVA: 0x0002A480 File Offset: 0x00028680
		protected virtual void OnSelectedItemChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_SELECTEDITEMCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E71 RID: 3697 RVA: 0x0002A4B0 File Offset: 0x000286B0
		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_DROPDOWNSTYLE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E72 RID: 3698 RVA: 0x0002A4DE File Offset: 0x000286DE
		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged(e);
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06000E73 RID: 3699 RVA: 0x0002A4F5 File Offset: 0x000286F5
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.ResetHeightCache();
			if (this.AutoCompleteMode == AutoCompleteMode.None)
			{
				this.UpdateControl(true);
			}
			else
			{
				base.RecreateHandle();
			}
			CommonProperties.xClearPreferredSizeCache(this);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0002A521 File Offset: 0x00028721
		private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
			{
				if (this.AutoCompleteCustomSource.Count == 0)
				{
					this.SetAutoComplete(true, true);
					return;
				}
				this.SetAutoComplete(true, false);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06000E75 RID: 3701 RVA: 0x0002A54B File Offset: 0x0002874B
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.UpdateControl(false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06000E76 RID: 3702 RVA: 0x0002A55B File Offset: 0x0002875B
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.UpdateControl(false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E77 RID: 3703 RVA: 0x0002A56B File Offset: 0x0002876B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnGotFocus(EventArgs e)
		{
			if (!this.canFireLostFocus)
			{
				base.OnGotFocus(e);
				this.canFireLostFocus = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E78 RID: 3704 RVA: 0x0002A584 File Offset: 0x00028784
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.canFireLostFocus)
			{
				if (this.AutoCompleteMode != AutoCompleteMode.None && this.AutoCompleteSource == AutoCompleteSource.ListItems && this.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					this.MatchingText = "";
				}
				base.OnLostFocus(e);
				this.canFireLostFocus = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E79 RID: 3705 RVA: 0x0002A5D0 File Offset: 0x000287D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e)
		{
			if (this.SystemAutoCompleteEnabled)
			{
				string text = this.Text;
				if (text != this.lastTextChangedValue)
				{
					base.OnTextChanged(e);
					this.lastTextChangedValue = text;
					return;
				}
			}
			else
			{
				base.OnTextChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06000E7A RID: 3706 RVA: 0x0002A610 File Offset: 0x00028810
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnValidating(CancelEventArgs e)
		{
			if (this.SystemAutoCompleteEnabled)
			{
				this.NotifyAutoComplete();
			}
			base.OnValidating(e);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0002A627 File Offset: 0x00028827
		private void UpdateControl(bool recreate)
		{
			this.ResetHeightCache();
			if (base.IsHandleCreated)
			{
				if (this.DropDownStyle == ComboBoxStyle.Simple && recreate)
				{
					base.RecreateHandle();
					return;
				}
				this.UpdateItemHeight();
				this.InvalidateEverything();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E7C RID: 3708 RVA: 0x0002A657 File Offset: 0x00028857
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				this.InvalidateEverything();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DataSourceChanged" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06000E7D RID: 3709 RVA: 0x0002A670 File Offset: 0x00028870
		protected override void OnDataSourceChanged(EventArgs e)
		{
			if (this.Sorted && this.DataSource != null && base.Created)
			{
				this.DataSource = null;
				throw new InvalidOperationException(SR.GetString("ComboBoxDataSourceWithSort"));
			}
			if (this.DataSource == null)
			{
				this.BeginUpdate();
				this.SelectedIndex = -1;
				this.Items.ClearInternal();
				this.EndUpdate();
			}
			if (!this.Sorted && base.Created)
			{
				base.OnDataSourceChanged(e);
			}
			this.RefreshItems();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DisplayMemberChanged" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06000E7E RID: 3710 RVA: 0x0002A6EF File Offset: 0x000288EF
		protected override void OnDisplayMemberChanged(EventArgs e)
		{
			base.OnDisplayMemberChanged(e);
			this.RefreshItems();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownClosed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E7F RID: 3711 RVA: 0x0002A700 File Offset: 0x00028900
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_DROPDOWNCLOSED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (AccessibilityImprovements.Level3 && base.IsHandleCreated)
			{
				if (this.DropDownStyle == ComboBoxStyle.DropDown)
				{
					base.AccessibilityObject.RaiseAutomationEvent(20005);
				}
				base.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Expanded, UnsafeNativeMethods.ExpandCollapseState.Collapsed);
				this.dropDownWillBeClosed = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.TextUpdate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000E80 RID: 3712 RVA: 0x0002A77C File Offset: 0x0002897C
		protected virtual void OnTextUpdate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_TEXTUPDATE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Processes a key message and generates the appropriate control events.</summary>
		/// <param name="m">A message object, passed by reference, that represents the window message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000E81 RID: 3713 RVA: 0x0002A7AA File Offset: 0x000289AA
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			return (this.AutoCompleteMode != AutoCompleteMode.None && this.AutoCompleteSource == AutoCompleteSource.ListItems && this.DropDownStyle == ComboBoxStyle.DropDownList && this.InterceptAutoCompleteKeystroke(m)) || base.ProcessKeyEventArgs(ref m);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002A7E1 File Offset: 0x000289E1
		private void ResetHeightCache()
		{
			this.prefHeightCache = -1;
		}

		/// <summary>Refreshes all <see cref="T:System.Windows.Forms.ComboBox" /> items.</summary>
		// Token: 0x06000E83 RID: 3715 RVA: 0x0002A7EC File Offset: 0x000289EC
		protected override void RefreshItems()
		{
			int num = this.SelectedIndex;
			ComboBox.ObjectCollection objectCollection = this.itemsCollection;
			this.itemsCollection = null;
			if (base.IsHandleCreated && base.IsAccessibilityObjectCreated)
			{
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null)
				{
					comboBoxUiaProvider.ResetListItemAccessibleObjects();
				}
			}
			object[] array = null;
			if (base.DataManager != null && base.DataManager.Count != -1)
			{
				array = new object[base.DataManager.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = base.DataManager[i];
				}
			}
			else if (objectCollection != null)
			{
				array = new object[objectCollection.Count];
				objectCollection.CopyTo(array, 0);
			}
			this.BeginUpdate();
			try
			{
				if (base.IsHandleCreated)
				{
					this.NativeClear();
				}
				if (array != null)
				{
					this.Items.AddRangeInternal(array);
				}
				if (base.DataManager != null)
				{
					this.SelectedIndex = base.DataManager.Position;
				}
				else
				{
					this.SelectedIndex = num;
				}
			}
			finally
			{
				this.EndUpdate();
			}
		}

		/// <summary>Refreshes the item contained at the specified location.</summary>
		/// <param name="index">The location of the item to refresh.</param>
		// Token: 0x06000E84 RID: 3716 RVA: 0x0002A8F4 File Offset: 0x00028AF4
		protected override void RefreshItem(int index)
		{
			this.Items.SetItemInternal(index, this.Items[index]);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002A910 File Offset: 0x00028B10
		private void ReleaseChildWindow()
		{
			if (this.childEdit != null)
			{
				this.childEdit.ReleaseHandle();
				this.childEdit = null;
				if (LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy)
				{
					ComboBox.ComboBoxChildEditUiaProvider comboBoxChildEditUiaProvider = this.childEditAccessibleObject;
					if (comboBoxChildEditUiaProvider != null)
					{
						comboBoxChildEditUiaProvider.ClearOwner();
					}
				}
			}
			if (this.childListBox != null)
			{
				if (AccessibilityImprovements.Level3 && !LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy)
				{
					base.ReleaseUiaProvider(this.childListBox.Handle);
				}
				this.childListBox.ReleaseHandle();
				this.childListBox = null;
			}
			if (this.childDropDown != null)
			{
				if (AccessibilityImprovements.Level3 && !LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy)
				{
					base.ReleaseUiaProvider(this.childDropDown.Handle);
				}
				this.childDropDown.ReleaseHandle();
				this.childDropDown = null;
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0002A9C4 File Offset: 0x00028BC4
		internal override void ReleaseUiaProvider(IntPtr handle)
		{
			base.ReleaseUiaProvider(handle);
			ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
			if (comboBoxUiaProvider != null)
			{
				comboBoxUiaProvider.ResetListItemAccessibleObjects();
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0002A9ED File Offset: 0x00028BED
		private void ResetAutoCompleteCustomSource()
		{
			this.AutoCompleteCustomSource = null;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0002A9F6 File Offset: 0x00028BF6
		private void ResetDropDownWidth()
		{
			base.Properties.RemoveInteger(ComboBox.PropDropDownWidth);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0002AA08 File Offset: 0x00028C08
		private void ResetItemHeight()
		{
			base.Properties.RemoveInteger(ComboBox.PropItemHeight);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Text" /> property to its default value (<see cref="F:System.String.Empty" />).</summary>
		// Token: 0x06000E8A RID: 3722 RVA: 0x0002AA1A File Offset: 0x00028C1A
		public override void ResetText()
		{
			base.ResetText();
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0002AA24 File Offset: 0x00028C24
		private void SetAutoComplete(bool reset, bool recreate)
		{
			if (!base.IsHandleCreated || this.childEdit == null)
			{
				return;
			}
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				if (!this.fromHandleCreate && recreate && base.IsHandleCreated)
				{
					AutoCompleteMode autoCompleteMode = this.AutoCompleteMode;
					this.autoCompleteMode = AutoCompleteMode.None;
					base.RecreateHandle();
					this.autoCompleteMode = autoCompleteMode;
				}
				if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
				{
					if (this.AutoCompleteCustomSource == null)
					{
						return;
					}
					if (this.AutoCompleteCustomSource.Count == 0)
					{
						int num = -1610612736;
						SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), num);
						return;
					}
					if (this.stringSource != null)
					{
						this.stringSource.RefreshList(this.GetStringsForAutoComplete(this.AutoCompleteCustomSource));
						return;
					}
					this.stringSource = new StringSource(this.GetStringsForAutoComplete(this.AutoCompleteCustomSource));
					if (!this.stringSource.Bind(new HandleRef(this, this.childEdit.Handle), (int)this.AutoCompleteMode))
					{
						throw new ArgumentException(SR.GetString("AutoCompleteFailure"));
					}
					return;
				}
				else if (this.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					if (this.DropDownStyle == ComboBoxStyle.DropDownList)
					{
						int num2 = -1610612736;
						SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), num2);
						return;
					}
					if (this.itemsCollection == null)
					{
						return;
					}
					if (this.itemsCollection.Count == 0)
					{
						int num3 = -1610612736;
						SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), num3);
						return;
					}
					if (this.stringSource != null)
					{
						this.stringSource.RefreshList(this.GetStringsForAutoComplete(this.Items));
						return;
					}
					this.stringSource = new StringSource(this.GetStringsForAutoComplete(this.Items));
					if (!this.stringSource.Bind(new HandleRef(this, this.childEdit.Handle), (int)this.AutoCompleteMode))
					{
						throw new ArgumentException(SR.GetString("AutoCompleteFailureListItems"));
					}
					return;
				}
				else
				{
					try
					{
						int num4 = 0;
						if (this.AutoCompleteMode == AutoCompleteMode.Suggest)
						{
							num4 |= -1879048192;
						}
						if (this.AutoCompleteMode == AutoCompleteMode.Append)
						{
							num4 |= 1610612736;
						}
						if (this.AutoCompleteMode == AutoCompleteMode.SuggestAppend)
						{
							num4 |= 268435456;
							num4 |= 1073741824;
						}
						int num5 = SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), (int)(this.AutoCompleteSource | (AutoCompleteSource)num4));
						return;
					}
					catch (SecurityException)
					{
						return;
					}
				}
			}
			if (reset)
			{
				int num6 = -1610612736;
				SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), num6);
			}
		}

		/// <summary>Selects a range of text in the editable portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> is less than zero.  
		///  -or-  
		///  <paramref name="start" /> plus <paramref name="length" /> is less than zero.</exception>
		// Token: 0x06000E8C RID: 3724 RVA: 0x0002ACB0 File Offset: 0x00028EB0
		public void Select(int start, int length)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("InvalidArgument", new object[]
				{
					"start",
					start.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = start + length;
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.GetString("InvalidArgument", new object[]
				{
					"length",
					length.ToString(CultureInfo.CurrentCulture)
				}));
			}
			base.SendMessage(322, 0, NativeMethods.Util.MAKELPARAM(start, num));
		}

		/// <summary>Selects all the text in the editable portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		// Token: 0x06000E8D RID: 3725 RVA: 0x0002AD41 File Offset: 0x00028F41
		public void SelectAll()
		{
			this.Select(0, int.MaxValue);
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <param name="x">The horizontal location in pixels of the control.</param>
		/// <param name="y">The vertical location in pixels of the control.</param>
		/// <param name="width">The width in pixels of the control.</param>
		/// <param name="height">The height in pixels of the control.</param>
		/// <param name="specified">One of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06000E8E RID: 3726 RVA: 0x0002AD4F File Offset: 0x00028F4F
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				this.requestedHeight = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>When overridden in a derived class, sets the specified array of objects in a collection in the derived class.</summary>
		/// <param name="value">An array of items.</param>
		// Token: 0x06000E8F RID: 3727 RVA: 0x0002AD6C File Offset: 0x00028F6C
		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			this.Items.ClearInternal();
			this.Items.AddRangeInternal(value);
			if (base.DataManager != null)
			{
				if (this.DataSource is ICurrencyManagerProvider)
				{
					this.selectedValueChangedFired = false;
				}
				if (base.IsHandleCreated)
				{
					base.SendMessage(334, base.DataManager.Position, 0);
				}
				else
				{
					this.selectedIndex = base.DataManager.Position;
				}
				if (!this.selectedValueChangedFired)
				{
					this.OnSelectedValueChanged(EventArgs.Empty);
					this.selectedValueChangedFired = false;
				}
			}
			this.EndUpdate();
		}

		/// <summary>When overridden in a derived class, sets the object with the specified index in the derived class.</summary>
		/// <param name="index">The array index of the object.</param>
		/// <param name="value">The object.</param>
		// Token: 0x06000E90 RID: 3728 RVA: 0x0002AE05 File Offset: 0x00029005
		protected override void SetItemCore(int index, object value)
		{
			this.Items.SetItemInternal(index, value);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002AE14 File Offset: 0x00029014
		private bool ShouldSerializeAutoCompleteCustomSource()
		{
			return this.autoCompleteCustomSource != null && this.autoCompleteCustomSource.Count > 0;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0002AE2E File Offset: 0x0002902E
		internal bool ShouldSerializeDropDownWidth()
		{
			return base.Properties.ContainsInteger(ComboBox.PropDropDownWidth);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002AE40 File Offset: 0x00029040
		internal bool ShouldSerializeItemHeight()
		{
			return base.Properties.ContainsInteger(ComboBox.PropItemHeight);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0002AE52 File Offset: 0x00029052
		internal override bool ShouldSerializeText()
		{
			return this.SelectedIndex == -1 && base.ShouldSerializeText();
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.ComboBox" />. The string includes the type and the number of items in the <see cref="T:System.Windows.Forms.ComboBox" /> control.</returns>
		// Token: 0x06000E95 RID: 3733 RVA: 0x0002AE68 File Offset: 0x00029068
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Items.Count: " + ((this.itemsCollection == null) ? 0.ToString(CultureInfo.CurrentCulture) : this.itemsCollection.Count.ToString(CultureInfo.CurrentCulture));
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0002AEB8 File Offset: 0x000290B8
		private void UpdateDropDownHeight()
		{
			if (this.dropDownHandle != IntPtr.Zero)
			{
				int num = this.DropDownHeight;
				if (num == 106)
				{
					int num2 = ((this.itemsCollection == null) ? 0 : this.itemsCollection.Count);
					int num3 = Math.Min(Math.Max(num2, 1), (int)this.maxDropDownItems);
					num = this.ItemHeight * num3 + 2;
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, this.dropDownHandle), NativeMethods.NullHandleRef, 0, 0, this.DropDownWidth, num, 6);
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002AF3C File Offset: 0x0002913C
		private void UpdateItemHeight()
		{
			if (!base.IsHandleCreated)
			{
				base.CreateControl();
			}
			if (this.DrawMode == DrawMode.OwnerDrawFixed)
			{
				base.SendMessage(339, -1, this.ItemHeight);
				base.SendMessage(339, 0, this.ItemHeight);
				return;
			}
			if (this.DrawMode == DrawMode.OwnerDrawVariable)
			{
				base.SendMessage(339, -1, this.ItemHeight);
				Graphics graphics = base.CreateGraphicsInternal();
				for (int i = 0; i < this.Items.Count; i++)
				{
					int num = (int)(long)base.SendMessage(340, i, 0);
					MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, i, num);
					this.OnMeasureItem(measureItemEventArgs);
					if (measureItemEventArgs.ItemHeight != num)
					{
						base.SendMessage(339, i, measureItemEventArgs.ItemHeight);
					}
				}
				graphics.Dispose();
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0002B008 File Offset: 0x00029208
		private void UpdateText()
		{
			string text = null;
			if (this.SelectedIndex != -1)
			{
				object obj = this.Items[this.SelectedIndex];
				if (obj != null)
				{
					text = base.GetItemText(obj);
				}
			}
			this.Text = text;
			if (this.DropDownStyle == ComboBoxStyle.DropDown && this.childEdit != null && this.childEdit.Handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 12, IntPtr.Zero, text);
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002B08C File Offset: 0x0002928C
		private void WmEraseBkgnd(ref Message m)
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple && this.ParentInternal != null)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				SafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
				Control parentInternal = this.ParentInternal;
				Graphics graphics = Graphics.FromHdcInternal(m.WParam);
				if (parentInternal != null)
				{
					Brush brush = new SolidBrush(parentInternal.BackColor);
					graphics.FillRectangle(brush, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
					brush.Dispose();
				}
				else
				{
					graphics.FillRectangle(SystemBrushes.Control, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
				}
				graphics.Dispose();
				m.Result = (IntPtr)1;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002B170 File Offset: 0x00029370
		private void WmParentNotify(ref Message m)
		{
			base.WndProc(ref m);
			if ((int)(long)m.WParam == 65536001)
			{
				this.dropDownHandle = m.LParam;
				if (AccessibilityImprovements.Level3)
				{
					if (this.childDropDown != null)
					{
						this.ReleaseUiaProvider(this.childDropDown.Handle);
						this.childDropDown.ReleaseHandle();
					}
					this.childDropDown = new ComboBox.ComboBoxChildNativeWindow(this, ComboBox.ChildWindowType.DropDownList);
					this.childDropDown.AssignHandle(this.dropDownHandle);
					this.childListAccessibleObject = null;
				}
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002B1F4 File Offset: 0x000293F4
		private void WmReflectCommand(ref Message m)
		{
			switch (NativeMethods.Util.HIWORD(m.WParam))
			{
			case 1:
				this.UpdateText();
				this.OnSelectedIndexChanged(EventArgs.Empty);
				return;
			case 2:
			case 3:
			case 4:
				break;
			case 5:
				this.OnTextChanged(EventArgs.Empty);
				return;
			case 6:
				this.OnTextUpdate(EventArgs.Empty);
				return;
			case 7:
				this.currentText = this.Text;
				this.dropDown = true;
				this.OnDropDown(EventArgs.Empty);
				this.UpdateDropDownHeight();
				return;
			case 8:
				this.OnDropDownClosed(EventArgs.Empty);
				if (base.FormattingEnabled && this.Text != this.currentText && this.dropDown)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
				this.dropDown = false;
				return;
			case 9:
				this.OnSelectionChangeCommittedInternal(EventArgs.Empty);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002B2D8 File Offset: 0x000294D8
		private void WmReflectDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr intPtr = Control.SetUpPalette(drawitemstruct.hDC, false, false);
			try
			{
				Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC);
				try
				{
					this.OnDrawItem(new DrawItemEventArgs(graphics, this.Font, Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom), drawitemstruct.itemID, (DrawItemState)drawitemstruct.itemState, this.ForeColor, this.BackColor));
				}
				finally
				{
					graphics.Dispose();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectPalette(new HandleRef(this, drawitemstruct.hDC), new HandleRef(null, intPtr), 0);
				}
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002B3CC File Offset: 0x000295CC
		private void WmReflectMeasureItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			if (this.DrawMode == DrawMode.OwnerDrawVariable && measureitemstruct.itemID >= 0)
			{
				Graphics graphics = base.CreateGraphicsInternal();
				MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, measureitemstruct.itemID, this.ItemHeight);
				this.OnMeasureItem(measureItemEventArgs);
				measureitemstruct.itemHeight = measureItemEventArgs.ItemHeight;
				graphics.Dispose();
			}
			else
			{
				measureitemstruct.itemHeight = this.ItemHeight;
			}
			Marshal.StructureToPtr(measureitemstruct, m.LParam, false);
			m.Result = (IntPtr)1;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0002B45C File Offset: 0x0002965C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 130)
			{
				if (msg <= 20)
				{
					if (msg <= 8)
					{
						if (msg != 7)
						{
							if (msg != 8)
							{
								goto IL_547;
							}
						}
						else
						{
							try
							{
								this.fireSetFocus = false;
								base.WndProc(ref m);
								return;
							}
							finally
							{
								this.fireSetFocus = true;
							}
						}
						try
						{
							this.fireLostFocus = false;
							base.WndProc(ref m);
							if (!Application.RenderWithVisualStyles && !base.GetStyle(ControlStyles.UserPaint) && this.DropDownStyle == ComboBoxStyle.DropDownList && (this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup))
							{
								UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 675, 0, 0);
							}
							return;
						}
						finally
						{
							this.fireLostFocus = true;
						}
					}
					else
					{
						if (msg == 15)
						{
							if (!base.GetStyle(ControlStyles.UserPaint) && (this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup))
							{
								using (WindowsRegion windowsRegion = new WindowsRegion(this.FlatComboBoxAdapter.dropDownRect))
								{
									using (WindowsRegion windowsRegion2 = new WindowsRegion(base.Bounds))
									{
										NativeMethods.RegionFlags updateRgn = (NativeMethods.RegionFlags)SafeNativeMethods.GetUpdateRgn(new HandleRef(this, base.Handle), new HandleRef(this, windowsRegion2.HRegion), true);
										windowsRegion.CombineRegion(windowsRegion2, windowsRegion, RegionCombineMode.DIFF);
										Rectangle rectangle = windowsRegion2.ToRectangle();
										this.FlatComboBoxAdapter.ValidateOwnerDrawRegions(this, rectangle);
										NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
										bool flag = false;
										IntPtr intPtr;
										if (m.WParam == IntPtr.Zero)
										{
											intPtr = UnsafeNativeMethods.BeginPaint(new HandleRef(this, base.Handle), ref paintstruct);
											flag = true;
										}
										else
										{
											intPtr = m.WParam;
										}
										using (DeviceContext deviceContext = DeviceContext.FromHdc(intPtr))
										{
											using (WindowsGraphics windowsGraphics = new WindowsGraphics(deviceContext))
											{
												if (updateRgn != NativeMethods.RegionFlags.ERROR)
												{
													windowsGraphics.DeviceContext.SetClip(windowsRegion);
												}
												m.WParam = intPtr;
												this.DefWndProc(ref m);
												if (updateRgn != NativeMethods.RegionFlags.ERROR)
												{
													windowsGraphics.DeviceContext.SetClip(windowsRegion2);
												}
												using (Graphics graphics = Graphics.FromHdcInternal(intPtr))
												{
													this.FlatComboBoxAdapter.DrawFlatCombo(this, graphics);
												}
											}
										}
										if (flag)
										{
											UnsafeNativeMethods.EndPaint(new HandleRef(this, base.Handle), ref paintstruct);
										}
										return;
									}
								}
							}
							base.WndProc(ref m);
							return;
						}
						if (msg != 20)
						{
							goto IL_547;
						}
						this.WmEraseBkgnd(ref m);
						return;
					}
				}
				else if (msg <= 48)
				{
					if (msg == 32)
					{
						base.WndProc(ref m);
						return;
					}
					if (msg != 48)
					{
						goto IL_547;
					}
					if (base.Width == 0)
					{
						this.suppressNextWindosPos = true;
					}
					base.WndProc(ref m);
					return;
				}
				else
				{
					if (msg == 71)
					{
						if (!this.suppressNextWindosPos)
						{
							base.WndProc(ref m);
						}
						this.suppressNextWindosPos = false;
						return;
					}
					if (msg != 130)
					{
						goto IL_547;
					}
					base.WndProc(ref m);
					this.ReleaseChildWindow();
					return;
				}
			}
			else if (msg <= 528)
			{
				if (msg <= 513)
				{
					if (msg - 307 > 1)
					{
						if (msg != 513)
						{
							goto IL_547;
						}
						this.mouseEvents = true;
						base.WndProc(ref m);
						return;
					}
				}
				else if (msg != 514)
				{
					if (msg != 528)
					{
						goto IL_547;
					}
					this.WmParentNotify(ref m);
					return;
				}
				else
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
					Rectangle rectangle2 = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
					int num = NativeMethods.Util.SignedLOWORD(m.LParam);
					int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
					Point point = new Point(num, num2);
					point = base.PointToScreen(point);
					if (this.mouseEvents && !base.ValidationCancelled)
					{
						this.mouseEvents = false;
						bool capture = base.Capture;
						if (capture && rectangle2.Contains(point))
						{
							this.OnClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						}
						base.WndProc(ref m);
						return;
					}
					base.CaptureInternal = false;
					this.DefWndProc(ref m);
					return;
				}
			}
			else if (msg <= 792)
			{
				if (msg == 675)
				{
					this.DefWndProc(ref m);
					this.OnMouseLeaveInternal(EventArgs.Empty);
					return;
				}
				if (msg != 792)
				{
					goto IL_547;
				}
				if ((!base.GetStyle(ControlStyles.UserPaint) && this.FlatStyle == FlatStyle.Flat) || this.FlatStyle == FlatStyle.Popup)
				{
					this.DefWndProc(ref m);
					if (((int)(long)m.LParam & 4) == 4)
					{
						if ((!base.GetStyle(ControlStyles.UserPaint) && this.FlatStyle == FlatStyle.Flat) || this.FlatStyle == FlatStyle.Popup)
						{
							using (Graphics graphics2 = Graphics.FromHdcInternal(m.WParam))
							{
								this.FlatComboBoxAdapter.DrawFlatCombo(this, graphics2);
							}
						}
						return;
					}
				}
				base.WndProc(ref m);
				return;
			}
			else
			{
				if (msg == 8235)
				{
					this.WmReflectDrawItem(ref m);
					return;
				}
				if (msg == 8236)
				{
					this.WmReflectMeasureItem(ref m);
					return;
				}
				if (msg != 8465)
				{
					goto IL_547;
				}
				this.WmReflectCommand(ref m);
				return;
			}
			m.Result = this.InitializeDCForWmCtlColor(m.WParam, m.Msg);
			return;
			IL_547:
			if (m.Msg == NativeMethods.WM_MOUSEENTER)
			{
				this.DefWndProc(ref m);
				this.OnMouseEnterInternal(EventArgs.Empty);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0002BA9C File Offset: 0x00029C9C
		private ComboBox.FlatComboAdapter FlatComboBoxAdapter
		{
			get
			{
				ComboBox.FlatComboAdapter flatComboAdapter = base.Properties.GetObject(ComboBox.PropFlatComboAdapter) as ComboBox.FlatComboAdapter;
				if (flatComboAdapter == null || !flatComboAdapter.IsValid(this))
				{
					flatComboAdapter = this.CreateFlatComboAdapterInstance();
					base.Properties.SetObject(ComboBox.PropFlatComboAdapter, flatComboAdapter);
				}
				return flatComboAdapter;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002BAE4 File Offset: 0x00029CE4
		internal virtual ComboBox.FlatComboAdapter CreateFlatComboAdapterInstance()
		{
			return new ComboBox.FlatComboAdapter(this, false);
		}

		// Token: 0x040007A3 RID: 1955
		private static readonly object EVENT_DROPDOWN = new object();

		// Token: 0x040007A4 RID: 1956
		private static readonly object EVENT_DRAWITEM = new object();

		// Token: 0x040007A5 RID: 1957
		private static readonly object EVENT_MEASUREITEM = new object();

		// Token: 0x040007A6 RID: 1958
		private static readonly object EVENT_SELECTEDINDEXCHANGED = new object();

		// Token: 0x040007A7 RID: 1959
		private static readonly object EVENT_SELECTIONCHANGECOMMITTED = new object();

		// Token: 0x040007A8 RID: 1960
		private static readonly object EVENT_SELECTEDITEMCHANGED = new object();

		// Token: 0x040007A9 RID: 1961
		private static readonly object EVENT_DROPDOWNSTYLE = new object();

		// Token: 0x040007AA RID: 1962
		private static readonly object EVENT_TEXTUPDATE = new object();

		// Token: 0x040007AB RID: 1963
		private static readonly object EVENT_DROPDOWNCLOSED = new object();

		// Token: 0x040007AC RID: 1964
		private static readonly int PropMaxLength = PropertyStore.CreateKey();

		// Token: 0x040007AD RID: 1965
		private static readonly int PropItemHeight = PropertyStore.CreateKey();

		// Token: 0x040007AE RID: 1966
		private static readonly int PropDropDownWidth = PropertyStore.CreateKey();

		// Token: 0x040007AF RID: 1967
		private static readonly int PropDropDownHeight = PropertyStore.CreateKey();

		// Token: 0x040007B0 RID: 1968
		private static readonly int PropStyle = PropertyStore.CreateKey();

		// Token: 0x040007B1 RID: 1969
		private static readonly int PropDrawMode = PropertyStore.CreateKey();

		// Token: 0x040007B2 RID: 1970
		private static readonly int PropMatchingText = PropertyStore.CreateKey();

		// Token: 0x040007B3 RID: 1971
		private static readonly int PropFlatComboAdapter = PropertyStore.CreateKey();

		// Token: 0x040007B4 RID: 1972
		private const int DefaultSimpleStyleHeight = 150;

		// Token: 0x040007B5 RID: 1973
		private const int DefaultDropDownHeight = 106;

		// Token: 0x040007B6 RID: 1974
		private const int AutoCompleteTimeout = 10000000;

		// Token: 0x040007B7 RID: 1975
		private bool autoCompleteDroppedDown;

		// Token: 0x040007B8 RID: 1976
		private FlatStyle flatStyle = FlatStyle.Standard;

		// Token: 0x040007B9 RID: 1977
		private int updateCount;

		// Token: 0x040007BA RID: 1978
		private long autoCompleteTimeStamp;

		// Token: 0x040007BB RID: 1979
		private int selectedIndex = -1;

		// Token: 0x040007BC RID: 1980
		private bool allowCommit = true;

		// Token: 0x040007BD RID: 1981
		private int requestedHeight;

		// Token: 0x040007BE RID: 1982
		private ComboBox.ComboBoxChildNativeWindow childDropDown;

		// Token: 0x040007BF RID: 1983
		private ComboBox.ComboBoxChildNativeWindow childEdit;

		// Token: 0x040007C0 RID: 1984
		private ComboBox.ComboBoxChildNativeWindow childListBox;

		// Token: 0x040007C1 RID: 1985
		private IntPtr dropDownHandle;

		// Token: 0x040007C2 RID: 1986
		private ComboBox.ObjectCollection itemsCollection;

		// Token: 0x040007C3 RID: 1987
		private short prefHeightCache = -1;

		// Token: 0x040007C4 RID: 1988
		private short maxDropDownItems = 8;

		// Token: 0x040007C5 RID: 1989
		private bool integralHeight = true;

		// Token: 0x040007C6 RID: 1990
		private bool mousePressed;

		// Token: 0x040007C7 RID: 1991
		private bool mouseEvents;

		// Token: 0x040007C8 RID: 1992
		private bool mouseInEdit;

		// Token: 0x040007C9 RID: 1993
		private bool sorted;

		// Token: 0x040007CA RID: 1994
		private bool fireSetFocus = true;

		// Token: 0x040007CB RID: 1995
		private bool fireLostFocus = true;

		// Token: 0x040007CC RID: 1996
		private bool mouseOver;

		// Token: 0x040007CD RID: 1997
		private bool suppressNextWindosPos;

		// Token: 0x040007CE RID: 1998
		private bool canFireLostFocus;

		// Token: 0x040007CF RID: 1999
		private string currentText = "";

		// Token: 0x040007D0 RID: 2000
		private string lastTextChangedValue;

		// Token: 0x040007D1 RID: 2001
		private bool dropDown;

		// Token: 0x040007D2 RID: 2002
		private ComboBox.AutoCompleteDropDownFinder finder = new ComboBox.AutoCompleteDropDownFinder();

		// Token: 0x040007D3 RID: 2003
		private bool selectedValueChangedFired;

		// Token: 0x040007D4 RID: 2004
		private AutoCompleteMode autoCompleteMode;

		// Token: 0x040007D5 RID: 2005
		private AutoCompleteSource autoCompleteSource = AutoCompleteSource.None;

		// Token: 0x040007D6 RID: 2006
		private AutoCompleteStringCollection autoCompleteCustomSource;

		// Token: 0x040007D7 RID: 2007
		private StringSource stringSource;

		// Token: 0x040007D8 RID: 2008
		private bool fromHandleCreate;

		// Token: 0x040007D9 RID: 2009
		private ComboBox.ComboBoxChildListUiaProvider childListAccessibleObject;

		// Token: 0x040007DA RID: 2010
		private ComboBox.ComboBoxChildEditUiaProvider childEditAccessibleObject;

		// Token: 0x040007DB RID: 2011
		private ComboBox.ComboBoxChildTextUiaProvider childTextAccessibleObject;

		// Token: 0x040007DC RID: 2012
		private bool dropDownWillBeClosed;

		// Token: 0x02000622 RID: 1570
		[ComVisible(true)]
		internal class ComboBoxChildNativeWindow : NativeWindow
		{
			// Token: 0x06006340 RID: 25408 RVA: 0x0016E96C File Offset: 0x0016CB6C
			public ComboBoxChildNativeWindow(ComboBox comboBox, ComboBox.ChildWindowType childWindowType)
			{
				this._owner = comboBox;
				this._childWindowType = childWindowType;
			}

			// Token: 0x06006341 RID: 25409 RVA: 0x0016E984 File Offset: 0x0016CB84
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 2)
				{
					if (msg != 61)
					{
						if (msg != 512)
						{
							if (this._childWindowType == ComboBox.ChildWindowType.DropDownList)
							{
								base.DefWndProc(ref m);
								return;
							}
							this._owner.ChildWndProc(ref m);
						}
						else
						{
							if (this._childWindowType != ComboBox.ChildWindowType.DropDownList)
							{
								this._owner.ChildWndProc(ref m);
								return;
							}
							object selectedItem = this._owner.SelectedItem;
							base.DefWndProc(ref m);
							object selectedItem2 = this._owner.SelectedItem;
							if (selectedItem != selectedItem2)
							{
								(this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider).SetComboBoxItemFocus();
								return;
							}
						}
						return;
					}
					this.WmGetObject(ref m);
					return;
				}
				else
				{
					if (AccessibilityImprovements.Level3 && LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy && (this._childWindowType == ComboBox.ChildWindowType.ListBox || this._childWindowType == ComboBox.ChildWindowType.DropDownList))
					{
						if (base.Handle != IntPtr.Zero)
						{
							UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, base.Handle), IntPtr.Zero, IntPtr.Zero, null);
						}
						if (this._accessibilityObject != null && ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
						{
							int num = UnsafeNativeMethods.UiaDisconnectProvider(this._accessibilityObject);
						}
					}
					if (this._childWindowType == ComboBox.ChildWindowType.DropDownList)
					{
						base.DefWndProc(ref m);
						return;
					}
					this._owner.ChildWndProc(ref m);
					return;
				}
			}

			// Token: 0x06006342 RID: 25410 RVA: 0x0016EAB6 File Offset: 0x0016CCB6
			private ComboBox.ChildAccessibleObject GetChildAccessibleObject(ComboBox.ChildWindowType childWindowType)
			{
				if (childWindowType == ComboBox.ChildWindowType.Edit)
				{
					return this._owner.ChildEditAccessibleObject;
				}
				if (childWindowType == ComboBox.ChildWindowType.ListBox || childWindowType == ComboBox.ChildWindowType.DropDownList)
				{
					return this._owner.ChildListAccessibleObject;
				}
				return new ComboBox.ChildAccessibleObject(this._owner, base.Handle);
			}

			// Token: 0x06006343 RID: 25411 RVA: 0x0016EAEC File Offset: 0x0016CCEC
			private void WmGetObject(ref Message m)
			{
				if (AccessibilityImprovements.Level3 && m.LParam == (IntPtr)(-25) && (this._childWindowType == ComboBox.ChildWindowType.ListBox || this._childWindowType == ComboBox.ChildWindowType.DropDownList))
				{
					if (this._accessibilityObject == null)
					{
						this._accessibilityObject = Control.CreateInternalAccessibleObject(this.GetChildAccessibleObject(this._childWindowType));
					}
					m.Result = UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, base.Handle), m.WParam, m.LParam, this._accessibilityObject);
					return;
				}
				if (-4 == (int)(long)m.LParam)
				{
					Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
					try
					{
						if (this._accessibilityObject == null)
						{
							AccessibleObject accessibleObject = (AccessibilityImprovements.Level3 ? this.GetChildAccessibleObject(this._childWindowType) : new ComboBox.ChildAccessibleObject(this._owner, base.Handle));
							this._accessibilityObject = Control.CreateInternalAccessibleObject(accessibleObject);
						}
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this._accessibilityObject);
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							m.Result = UnsafeNativeMethods.LresultFromObject(ref guid, m.WParam, new HandleRef(this, iunknownForObject));
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							Marshal.Release(iunknownForObject);
						}
						return;
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(SR.GetString("RichControlLresult"), ex);
					}
				}
				base.DefWndProc(ref m);
			}

			// Token: 0x0400391F RID: 14623
			private ComboBox _owner;

			// Token: 0x04003920 RID: 14624
			private InternalAccessibleObject _accessibilityObject;

			// Token: 0x04003921 RID: 14625
			private ComboBox.ChildWindowType _childWindowType;
		}

		// Token: 0x02000623 RID: 1571
		private sealed class ItemComparer : IComparer
		{
			// Token: 0x06006344 RID: 25412 RVA: 0x0016EC40 File Offset: 0x0016CE40
			public ItemComparer(ComboBox comboBox)
			{
				this.comboBox = comboBox;
			}

			// Token: 0x06006345 RID: 25413 RVA: 0x0016EC50 File Offset: 0x0016CE50
			public int Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					string itemText = this.comboBox.GetItemText(item1);
					string itemText2 = this.comboBox.GetItemText(item2);
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					return compareInfo.Compare(itemText, itemText2, CompareOptions.StringSort);
				}
			}

			// Token: 0x04003922 RID: 14626
			private ComboBox comboBox;
		}

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		// Token: 0x02000624 RID: 1572
		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ComboBox" /> that owns this object collection.</param>
			// Token: 0x06006346 RID: 25414 RVA: 0x0016EC9E File Offset: 0x0016CE9E
			public ObjectCollection(ComboBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001533 RID: 5427
			// (get) Token: 0x06006347 RID: 25415 RVA: 0x0016ECAD File Offset: 0x0016CEAD
			private IComparer Comparer
			{
				get
				{
					if (this.comparer == null)
					{
						this.comparer = new ComboBox.ItemComparer(this.owner);
					}
					return this.comparer;
				}
			}

			// Token: 0x17001534 RID: 5428
			// (get) Token: 0x06006348 RID: 25416 RVA: 0x0016ECCE File Offset: 0x0016CECE
			private ArrayList InnerList
			{
				get
				{
					if (this.innerList == null)
					{
						this.innerList = new ArrayList();
					}
					return this.innerList;
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x17001535 RID: 5429
			// (get) Token: 0x06006349 RID: 25417 RVA: 0x0016ECE9 File Offset: 0x0016CEE9
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" />.</returns>
			// Token: 0x17001536 RID: 5430
			// (get) Token: 0x0600634A RID: 25418 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001537 RID: 5431
			// (get) Token: 0x0600634B RID: 25419 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001538 RID: 5432
			// (get) Token: 0x0600634C RID: 25420 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether this collection can be modified.</summary>
			/// <returns>Always <see langword="false" />.</returns>
			// Token: 0x17001539 RID: 5433
			// (get) Token: 0x0600634D RID: 25421 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <param name="item">An object representing the item to add to the collection.</param>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter was <see langword="null" />.</exception>
			// Token: 0x0600634E RID: 25422 RVA: 0x0016ECF8 File Offset: 0x0016CEF8
			public int Add(object item)
			{
				this.owner.CheckNoDataSource();
				int num = this.AddInternal(item);
				if (this.owner.UpdateNeeded() && this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, false);
				}
				return num;
			}

			// Token: 0x0600634F RID: 25423 RVA: 0x0016ED48 File Offset: 0x0016CF48
			private int AddInternal(object item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int num = -1;
				if (!this.owner.sorted)
				{
					this.InnerList.Add(item);
				}
				else
				{
					num = this.InnerList.BinarySearch(item, this.Comparer);
					if (num < 0)
					{
						num = ~num;
					}
					this.InnerList.Insert(num, item);
				}
				bool flag = false;
				try
				{
					if (this.owner.sorted)
					{
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeInsert(num, item);
						}
					}
					else
					{
						num = this.InnerList.Count - 1;
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeAdd(item);
						}
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.InnerList.Remove(item);
					}
				}
				if (flag && this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						comboBoxUiaProvider.InsertToItemsCollection(num, item, this.InnerList.Count);
					}
				}
				return num;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="item">An object that represents the item to add to the collection.</param>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is <see langword="null" />.</exception>
			/// <exception cref="T:System.SystemException">There is insufficient space available to store the new item.</exception>
			// Token: 0x06006350 RID: 25424 RVA: 0x0016EE68 File Offset: 0x0016D068
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			/// <summary>Adds an array of items to the list of items for a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <param name="items">An array of objects to add to the list.</param>
			/// <exception cref="T:System.ArgumentNullException">An item in the <paramref name="items" /> parameter was <see langword="null" />.</exception>
			// Token: 0x06006351 RID: 25425 RVA: 0x0016EE74 File Offset: 0x0016D074
			public void AddRange(object[] items)
			{
				this.owner.CheckNoDataSource();
				this.owner.BeginUpdate();
				try
				{
					this.AddRangeInternal(items);
				}
				finally
				{
					this.owner.EndUpdate();
				}
			}

			// Token: 0x06006352 RID: 25426 RVA: 0x0016EEBC File Offset: 0x0016D0BC
			internal void AddRangeInternal(IList items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (object obj in items)
				{
					this.AddInternal(obj);
				}
				if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, false);
				}
			}

			/// <summary>Retrieves the item at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to retrieve.</param>
			/// <returns>An object representing the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index was less than zero.  
			///  -or-  
			///  The <paramref name="index" /> was greater of equal to the count of items in the collection.</exception>
			// Token: 0x1700153A RID: 5434
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerList.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerList[index];
				}
				set
				{
					this.owner.CheckNoDataSource();
					this.SetItemInternal(index, value);
				}
			}

			/// <summary>Removes all items from the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			// Token: 0x06006355 RID: 25429 RVA: 0x0016EFAE File Offset: 0x0016D1AE
			public void Clear()
			{
				this.owner.CheckNoDataSource();
				this.ClearInternal();
			}

			// Token: 0x06006356 RID: 25430 RVA: 0x0016EFC4 File Offset: 0x0016D1C4
			internal void ClearInternal()
			{
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeClear();
				}
				this.InnerList.Clear();
				this.owner.selectedIndex = -1;
				if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, true);
				}
				if (this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider == null)
					{
						return;
					}
					comboBoxUiaProvider.ResetListItemAccessibleObjects();
				}
			}

			/// <summary>Determines if the specified item is located within the collection.</summary>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the item is located within the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006357 RID: 25431 RVA: 0x0016F053 File Offset: 0x0016D253
			public bool Contains(object value)
			{
				return this.IndexOf(value) != -1;
			}

			/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
			/// <param name="destination">The object array to copy the collection to.</param>
			/// <param name="arrayIndex">The location in the destination array to copy the collection to.</param>
			// Token: 0x06006358 RID: 25432 RVA: 0x0016F062 File Offset: 0x0016D262
			public void CopyTo(object[] destination, int arrayIndex)
			{
				this.InnerList.CopyTo(destination, arrayIndex);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="destination">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			// Token: 0x06006359 RID: 25433 RVA: 0x0016F062 File Offset: 0x0016D262
			void ICollection.CopyTo(Array destination, int index)
			{
				this.InnerList.CopyTo(destination, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x0600635A RID: 25434 RVA: 0x0016F071 File Offset: 0x0016D271
			public IEnumerator GetEnumerator()
			{
				return this.InnerList.GetEnumerator();
			}

			/// <summary>Retrieves the index within the collection of the specified item.</summary>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <returns>The zero-based index where the item is located within the collection; otherwise, -1.</returns>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter was <see langword="null" />.</exception>
			// Token: 0x0600635B RID: 25435 RVA: 0x0016F07E File Offset: 0x0016D27E
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerList.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">An object representing the item to insert.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> was <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> was less than zero.  
			///  -or-  
			///  The <paramref name="index" /> was greater than the count of items in the collection.</exception>
			// Token: 0x0600635C RID: 25436 RVA: 0x0016F09C File Offset: 0x0016D29C
			public void Insert(int index, object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (index < 0 || index > this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.sorted)
				{
					this.Add(item);
					return;
				}
				this.InnerList.Insert(index, item);
				if (this.owner.IsHandleCreated)
				{
					bool flag = false;
					try
					{
						this.owner.NativeInsert(index, item);
						flag = true;
					}
					finally
					{
						if (flag)
						{
							if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
							{
								this.owner.SetAutoComplete(false, false);
							}
							if (this.owner.IsAccessibilityObjectCreated)
							{
								ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
								if (comboBoxUiaProvider != null)
								{
									comboBoxUiaProvider.InsertToItemsCollection(index, item, this.InnerList.Count);
								}
							}
						}
						else
						{
							this.InnerList.RemoveAt(index);
						}
					}
				}
			}

			/// <summary>Removes an item from the <see cref="T:System.Windows.Forms.ComboBox" /> at the specified index.</summary>
			/// <param name="index">The index of the item to remove.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="value" /> parameter was less than zero.  
			///  -or-  
			///  The <paramref name="value" /> parameter was greater than or equal to the count of items in the collection.</exception>
			// Token: 0x0600635D RID: 25437 RVA: 0x0016F1C0 File Offset: 0x0016D3C0
			public void RemoveAt(int index)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index >= this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeRemoveAt(index);
				}
				this.InnerList.RemoveAt(index);
				if (this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						comboBoxUiaProvider.RemoveFromItemsCollection(index, this.InnerList.Count);
					}
				}
				if (!this.owner.IsHandleCreated && index < this.owner.selectedIndex)
				{
					this.owner.selectedIndex--;
				}
				if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, false);
				}
			}

			/// <summary>Removes the specified item from the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the list.</param>
			// Token: 0x0600635E RID: 25438 RVA: 0x0016F2D0 File Offset: 0x0016D4D0
			public void Remove(object value)
			{
				int num = this.InnerList.IndexOf(value);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x0600635F RID: 25439 RVA: 0x0016F2F8 File Offset: 0x0016D4F8
			internal void SetItemInternal(int index, object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (index < 0 || index >= this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerList[index] = value;
				if (this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						comboBoxUiaProvider.SetItemInternal(index, value, this.InnerList.Count);
					}
				}
				if (this.owner.IsHandleCreated)
				{
					bool flag = index == this.owner.SelectedIndex;
					if (string.Compare(this.owner.GetItemText(value), this.owner.NativeGetItemText(index), true, CultureInfo.CurrentCulture) != 0)
					{
						this.owner.NativeRemoveAt(index);
						this.owner.NativeInsert(index, value);
						if (flag)
						{
							this.owner.SelectedIndex = index;
							this.owner.UpdateText();
						}
						if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
						{
							this.owner.SetAutoComplete(false, false);
							return;
						}
					}
					else if (flag)
					{
						this.owner.OnSelectedItemChanged(EventArgs.Empty);
						this.owner.OnSelectedIndexChanged(EventArgs.Empty);
					}
				}
			}

			// Token: 0x04003923 RID: 14627
			private ComboBox owner;

			// Token: 0x04003924 RID: 14628
			private ArrayList innerList;

			// Token: 0x04003925 RID: 14629
			private IComparer comparer;
		}

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.ComboBox" /> control to accessibility client applications.</summary>
		// Token: 0x02000625 RID: 1573
		[ComVisible(true)]
		public class ChildAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ComboBox.ChildAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ComboBox" /> control that owns the <see cref="T:System.Windows.Forms.ComboBox.ChildAccessibleObject" />.</param>
			/// <param name="handle">A handle to part of the <see cref="T:System.Windows.Forms.ComboBox" />.</param>
			// Token: 0x06006360 RID: 25440 RVA: 0x0016F461 File Offset: 0x0016D661
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public ChildAccessibleObject(ComboBox owner, IntPtr handle)
			{
				this.Owner = owner;
				base.UseStdAccessibleObjects(handle);
			}

			// Token: 0x1700153B RID: 5435
			// (get) Token: 0x06006361 RID: 25441 RVA: 0x0016F477 File Offset: 0x0016D677
			// (set) Token: 0x06006362 RID: 25442 RVA: 0x0016F47F File Offset: 0x0016D67F
			internal ComboBox Owner { get; private set; }

			// Token: 0x06006363 RID: 25443 RVA: 0x0016F488 File Offset: 0x0016D688
			internal void ClearOwner()
			{
				this.Owner = null;
			}

			/// <summary>Gets the name of the object.</summary>
			/// <returns>The value of the <see cref="P:System.Windows.Forms.ComboBox.ChildAccessibleObject.Name" /> property is the same as the <see cref="P:System.Windows.Forms.AccessibleObject.Name" /> property for the <see cref="T:System.Windows.Forms.AccessibleObject" /> of the <see cref="T:System.Windows.Forms.ComboBox" />.</returns>
			// Token: 0x1700153C RID: 5436
			// (get) Token: 0x06006364 RID: 25444 RVA: 0x0016F491 File Offset: 0x0016D691
			public override string Name
			{
				get
				{
					ComboBox owner = this.Owner;
					if (owner == null)
					{
						return null;
					}
					return owner.AccessibilityObject.Name;
				}
			}
		}

		// Token: 0x02000626 RID: 1574
		[ComVisible(true)]
		internal class ComboBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006365 RID: 25445 RVA: 0x0009B733 File Offset: 0x00099933
			public ComboBoxAccessibleObject(Control ownerControl)
				: base(ownerControl)
			{
			}

			// Token: 0x06006366 RID: 25446 RVA: 0x0016F4A9 File Offset: 0x0016D6A9
			internal override string get_accNameInternal(object childID)
			{
				base.ValidateChildID(ref childID);
				if (childID != null && (int)childID == 1)
				{
					return this.Name;
				}
				return base.get_accNameInternal(childID);
			}

			// Token: 0x06006367 RID: 25447 RVA: 0x0016F4CD File Offset: 0x0016D6CD
			internal override string get_accKeyboardShortcutInternal(object childID)
			{
				base.ValidateChildID(ref childID);
				if (childID != null && (int)childID == 1)
				{
					return this.KeyboardShortcut;
				}
				return base.get_accKeyboardShortcutInternal(childID);
			}

			// Token: 0x04003927 RID: 14631
			private const int COMBOBOX_ACC_ITEM_INDEX = 1;
		}

		// Token: 0x02000627 RID: 1575
		[ComVisible(true)]
		internal class ComboBoxExAccessibleObject : ComboBox.ComboBoxAccessibleObject
		{
			// Token: 0x06006368 RID: 25448 RVA: 0x0016F4F1 File Offset: 0x0016D6F1
			private void ComboBoxDefaultAction(bool expand)
			{
				if (this.ownerItem.DroppedDown != expand)
				{
					this.ownerItem.DroppedDown = expand;
				}
			}

			// Token: 0x06006369 RID: 25449 RVA: 0x0016F50D File Offset: 0x0016D70D
			public ComboBoxExAccessibleObject(ComboBox ownerControl)
				: base(ownerControl)
			{
				this.ownerItem = ownerControl;
			}

			// Token: 0x0600636A RID: 25450 RVA: 0x0016F51D File Offset: 0x0016D71D
			internal override bool IsIAccessibleExSupported()
			{
				return this.ownerItem != null || base.IsIAccessibleExSupported();
			}

			// Token: 0x0600636B RID: 25451 RVA: 0x0016F52F File Offset: 0x0016D72F
			internal override bool IsPatternSupported(int patternId)
			{
				if (patternId == 10005)
				{
					return this.ownerItem.DropDownStyle != ComboBoxStyle.Simple;
				}
				if (patternId == 10002)
				{
					return this.ownerItem.DropDownStyle != ComboBoxStyle.DropDownList || AccessibilityImprovements.Level3;
				}
				return base.IsPatternSupported(patternId);
			}

			// Token: 0x1700153D RID: 5437
			// (get) Token: 0x0600636C RID: 25452 RVA: 0x0016F570 File Offset: 0x0016D770
			internal override int[] RuntimeId
			{
				get
				{
					if (this.ownerItem != null)
					{
						return new int[]
						{
							42,
							(int)(long)this.ownerItem.Handle,
							this.ownerItem.GetHashCode()
						};
					}
					return base.RuntimeId;
				}
			}

			// Token: 0x0600636D RID: 25453 RVA: 0x0016F5BC File Offset: 0x0016D7BC
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID == 30028)
				{
					return this.IsPatternSupported(10005);
				}
				if (propertyID != 30043)
				{
					return base.GetPropertyValue(propertyID);
				}
				return this.IsPatternSupported(10002);
			}

			// Token: 0x0600636E RID: 25454 RVA: 0x0016F613 File Offset: 0x0016D813
			internal override void Expand()
			{
				this.ComboBoxDefaultAction(true);
			}

			// Token: 0x0600636F RID: 25455 RVA: 0x0016F61C File Offset: 0x0016D81C
			internal override void Collapse()
			{
				this.ComboBoxDefaultAction(false);
			}

			// Token: 0x1700153E RID: 5438
			// (get) Token: 0x06006370 RID: 25456 RVA: 0x0016F625 File Offset: 0x0016D825
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (!this.ownerItem.DroppedDown)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Expanded;
				}
			}

			// Token: 0x04003928 RID: 14632
			private ComboBox ownerItem;
		}

		// Token: 0x02000628 RID: 1576
		[ComVisible(true)]
		internal class ComboBoxItemAccessibleObject : AccessibleObject
		{
			// Token: 0x06006371 RID: 25457 RVA: 0x0016F637 File Offset: 0x0016D837
			public ComboBoxItemAccessibleObject(ComboBox owningComboBox, object owningItem)
			{
				this._owningComboBox = owningComboBox;
				this._owningItem = owningItem;
				this._systemIAccessible = this._owningComboBox.ChildListAccessibleObject.GetSystemIAccessibleInternal();
			}

			// Token: 0x1700153F RID: 5439
			// (get) Token: 0x06006372 RID: 25458 RVA: 0x0016F664 File Offset: 0x0016D864
			public override Rectangle Bounds
			{
				get
				{
					int currentIndex = this.GetCurrentIndex();
					IntPtr listHandle = this._owningComboBox.GetListHandle();
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					if ((int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, listHandle), 408, currentIndex, ref rect) == -1)
					{
						return Rectangle.Empty;
					}
					UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, listHandle), NativeMethods.NullHandleRef, ref rect, 2);
					return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
				}
			}

			// Token: 0x17001540 RID: 5440
			// (get) Token: 0x06006373 RID: 25459 RVA: 0x0016F6E1 File Offset: 0x0016D8E1
			public override string DefaultAction
			{
				get
				{
					return this._systemIAccessible.get_accDefaultAction(this.GetChildId());
				}
			}

			// Token: 0x06006374 RID: 25460 RVA: 0x0016F6FC File Offset: 0x0016D8FC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this._owningComboBox.ChildListAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					if (!this._owningComboBox.IsHandleCreated)
					{
						return null;
					}
					int num = this.GetCurrentIndex();
					ComboBox.ComboBoxChildListUiaProvider comboBoxChildListUiaProvider = this._owningComboBox.ChildListAccessibleObject as ComboBox.ComboBoxChildListUiaProvider;
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owningComboBox.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (num >= 0 && comboBoxChildListUiaProvider != null && comboBoxUiaProvider != null)
					{
						int count = comboBoxUiaProvider.ItemsAccessibleObjects.Count;
						int num2 = num + 1;
						if (num2 < count)
						{
							return comboBoxUiaProvider.ItemsAccessibleObjects[num2];
						}
					}
					break;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					if (!this._owningComboBox.IsHandleCreated)
					{
						return null;
					}
					int num = this.GetCurrentIndex();
					ComboBox.ComboBoxChildListUiaProvider comboBoxChildListUiaProvider = this._owningComboBox.ChildListAccessibleObject as ComboBox.ComboBoxChildListUiaProvider;
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owningComboBox.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (num > 0 && comboBoxChildListUiaProvider != null && comboBoxUiaProvider != null)
					{
						int count2 = comboBoxUiaProvider.ItemsAccessibleObjects.Count;
						int num3 = num - 1;
						if (num3 < count2)
						{
							return comboBoxUiaProvider.ItemsAccessibleObjects[num3];
						}
					}
					break;
				}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x17001541 RID: 5441
			// (get) Token: 0x06006375 RID: 25461 RVA: 0x0016F80A File Offset: 0x0016DA0A
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this._owningComboBox.AccessibilityObject;
				}
			}

			// Token: 0x06006376 RID: 25462 RVA: 0x0016F818 File Offset: 0x0016DA18
			private int GetCurrentIndex()
			{
				if (!this._owningComboBox.IsHandleCreated)
				{
					return -1;
				}
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owningComboBox.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null)
				{
					return comboBoxUiaProvider.ItemsAccessibleObjects.IndexOf(this);
				}
				return -1;
			}

			// Token: 0x06006377 RID: 25463 RVA: 0x0016F856 File Offset: 0x0016DA56
			internal override int GetChildId()
			{
				return this.GetCurrentIndex() + 1;
			}

			// Token: 0x06006378 RID: 25464 RVA: 0x0016F860 File Offset: 0x0016DA60
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30035)
				{
					switch (propertyID)
					{
					case 30000:
						return this.RuntimeId;
					case 30001:
						return this.BoundingRectangle;
					case 30002:
					case 30004:
					case 30006:
					case 30011:
					case 30012:
					case 30014:
					case 30015:
					case 30018:
					case 30020:
					case 30021:
						break;
					case 30003:
						return 50007;
					case 30005:
						return this.Name;
					case 30007:
						return this.KeyboardShortcut ?? string.Empty;
					case 30008:
						return this._owningComboBox.Focused && this._owningComboBox.SelectedIndex == this.GetCurrentIndex();
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
						return this._owningComboBox.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					case 30016:
						return true;
					case 30017:
						return true;
					case 30019:
						return false;
					case 30022:
						return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
					default:
						if (propertyID == 30035)
						{
							return true;
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30036)
					{
						return true;
					}
					if (propertyID == 30079)
					{
						return (this.State & AccessibleStates.Selected) > AccessibleStates.None;
					}
					if (propertyID == 30080)
					{
						return this._owningComboBox.ChildListAccessibleObject;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001542 RID: 5442
			// (get) Token: 0x06006379 RID: 25465 RVA: 0x0016FA0B File Offset: 0x0016DC0B
			public override string Help
			{
				get
				{
					return this._systemIAccessible.get_accHelp(this.GetChildId());
				}
			}

			// Token: 0x0600637A RID: 25466 RVA: 0x0016FA23 File Offset: 0x0016DC23
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || patternId == 10000 || patternId == 10017 || patternId == 10010 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001543 RID: 5443
			// (get) Token: 0x0600637B RID: 25467 RVA: 0x0016FA4E File Offset: 0x0016DC4E
			// (set) Token: 0x0600637C RID: 25468 RVA: 0x0016FA70 File Offset: 0x0016DC70
			public override string Name
			{
				get
				{
					if (this._owningComboBox != null)
					{
						return this._owningComboBox.GetItemText(this._owningItem);
					}
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x17001544 RID: 5444
			// (get) Token: 0x0600637D RID: 25469 RVA: 0x0016FA79 File Offset: 0x0016DC79
			public override AccessibleRole Role
			{
				get
				{
					return (AccessibleRole)this._systemIAccessible.get_accRole(this.GetChildId());
				}
			}

			// Token: 0x17001545 RID: 5445
			// (get) Token: 0x0600637E RID: 25470 RVA: 0x0016FA98 File Offset: 0x0016DC98
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						(int)(long)this._owningComboBox.Handle,
						this._owningComboBox.GetListNativeWindowRuntimeIdPart(),
						this._owningItem.GetHashCode(),
						this.GetCurrentIndex()
					};
				}
			}

			// Token: 0x0600637F RID: 25471 RVA: 0x0016FAEB File Offset: 0x0016DCEB
			internal void SetItemInternal(object newValue)
			{
				this._owningItem = newValue;
			}

			// Token: 0x06006380 RID: 25472 RVA: 0x0016FAF4 File Offset: 0x0016DCF4
			internal override void ScrollIntoView()
			{
				if (!this._owningComboBox.IsHandleCreated || !this._owningComboBox.Enabled)
				{
					return;
				}
				if (this._owningComboBox.ChildListAccessibleObject.BoundingRectangle.IntersectsWith(this.Bounds))
				{
					return;
				}
				this._owningComboBox.SendMessage(348, this.GetCurrentIndex(), 0);
			}

			// Token: 0x17001546 RID: 5446
			// (get) Token: 0x06006381 RID: 25473 RVA: 0x0016FB55 File Offset: 0x0016DD55
			public override AccessibleStates State
			{
				get
				{
					return (AccessibleStates)this._systemIAccessible.get_accState(this.GetChildId());
				}
			}

			// Token: 0x06006382 RID: 25474 RVA: 0x0015EE49 File Offset: 0x0015D049
			internal override void SetFocus()
			{
				base.RaiseAutomationEvent(20005);
				base.SetFocus();
			}

			// Token: 0x06006383 RID: 25475 RVA: 0x0016FB72 File Offset: 0x0016DD72
			internal override void SelectItem()
			{
				this._owningComboBox.SelectedIndex = this.GetCurrentIndex();
				SafeNativeMethods.InvalidateRect(new HandleRef(this, this._owningComboBox.GetListHandle()), null, false);
			}

			// Token: 0x06006384 RID: 25476 RVA: 0x000172A5 File Offset: 0x000154A5
			internal override void AddToSelection()
			{
				this.SelectItem();
			}

			// Token: 0x06006385 RID: 25477 RVA: 0x000070A6 File Offset: 0x000052A6
			internal override void RemoveFromSelection()
			{
			}

			// Token: 0x17001547 RID: 5447
			// (get) Token: 0x06006386 RID: 25478 RVA: 0x0016FB9E File Offset: 0x0016DD9E
			internal override bool IsItemSelected
			{
				get
				{
					return (this.State & AccessibleStates.Selected) > AccessibleStates.None;
				}
			}

			// Token: 0x17001548 RID: 5448
			// (get) Token: 0x06006387 RID: 25479 RVA: 0x0016FBAB File Offset: 0x0016DDAB
			internal override UnsafeNativeMethods.IRawElementProviderSimple ItemSelectionContainer
			{
				get
				{
					return this._owningComboBox.ChildListAccessibleObject;
				}
			}

			// Token: 0x04003929 RID: 14633
			private ComboBox _owningComboBox;

			// Token: 0x0400392A RID: 14634
			private object _owningItem;

			// Token: 0x0400392B RID: 14635
			private IAccessible _systemIAccessible;
		}

		// Token: 0x02000629 RID: 1577
		[ComVisible(true)]
		internal class ComboBoxUiaProvider : ComboBox.ComboBoxExAccessibleObject
		{
			// Token: 0x06006388 RID: 25480 RVA: 0x0016FBB8 File Offset: 0x0016DDB8
			public ComboBoxUiaProvider(ComboBox owningComboBox)
				: base(owningComboBox)
			{
				this._owningComboBox = owningComboBox;
			}

			// Token: 0x17001549 RID: 5449
			// (get) Token: 0x06006389 RID: 25481 RVA: 0x0016FBC8 File Offset: 0x0016DDC8
			internal List<ComboBox.ComboBoxItemAccessibleObject> ItemsAccessibleObjects
			{
				get
				{
					if (this.IsItemsCollectionCreated)
					{
						return this._itemAccessibleObjects;
					}
					this._itemAccessibleObjects = new List<ComboBox.ComboBoxItemAccessibleObject>();
					foreach (object obj in this._owningComboBox.Items)
					{
						this._itemAccessibleObjects.Add(new ComboBox.ComboBoxItemAccessibleObject(this._owningComboBox, obj));
					}
					return this._itemAccessibleObjects;
				}
			}

			// Token: 0x1700154A RID: 5450
			// (get) Token: 0x0600638A RID: 25482 RVA: 0x0016FC54 File Offset: 0x0016DE54
			private bool IsItemsCollectionCreated
			{
				get
				{
					return this._itemAccessibleObjects != null;
				}
			}

			// Token: 0x0600638B RID: 25483 RVA: 0x0016FC5F File Offset: 0x0016DE5F
			internal void InsertToItemsCollection(int index, object item, int total)
			{
				if (this.IsItemsCollectionCreated && this._itemAccessibleObjects.Count == total - 1)
				{
					this._itemAccessibleObjects.Insert(index, new ComboBox.ComboBoxItemAccessibleObject(this._owningComboBox, item));
				}
			}

			// Token: 0x0600638C RID: 25484 RVA: 0x0016FC91 File Offset: 0x0016DE91
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || base.IsPatternSupported(patternId);
			}

			// Token: 0x1700154B RID: 5451
			// (get) Token: 0x0600638D RID: 25485 RVA: 0x0016FCA4 File Offset: 0x0016DEA4
			public ComboBox.ComboBoxChildDropDownButtonUiaProvider DropDownButtonUiaProvider
			{
				get
				{
					if (this._dropDownButtonUiaProvider == null && this._owningComboBox.IsHandleCreated)
					{
						this._dropDownButtonUiaProvider = new ComboBox.ComboBoxChildDropDownButtonUiaProvider(this._owningComboBox, this._owningComboBox.Handle);
					}
					return this._dropDownButtonUiaProvider;
				}
			}

			// Token: 0x0600638E RID: 25486 RVA: 0x0016FCE0 File Offset: 0x0016DEE0
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					return this.GetChildFragment(0);
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					int childFragmentCount = this.GetChildFragmentCount();
					if (childFragmentCount > 0)
					{
						return this.GetChildFragment(childFragmentCount - 1);
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x1700154C RID: 5452
			// (get) Token: 0x0600638F RID: 25487 RVA: 0x0016FD1C File Offset: 0x0016DF1C
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStripControlHost toolStripControlHost = base.Owner.ToolStripControlHost;
					ToolStrip toolStrip = ((toolStripControlHost != null) ? toolStripControlHost.Owner : null);
					if (toolStrip != null && toolStrip.IsHandleCreated)
					{
						return toolStrip.AccessibilityObject;
					}
					return this;
				}
			}

			// Token: 0x06006390 RID: 25488 RVA: 0x0016FD54 File Offset: 0x0016DF54
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetOverrideProviderForHwnd(IntPtr hwnd)
			{
				if (hwnd == this._owningComboBox.childEdit.Handle)
				{
					return this._owningComboBox.ChildEditAccessibleObject;
				}
				if (hwnd == this._owningComboBox.childListBox.Handle || hwnd == this._owningComboBox.dropDownHandle)
				{
					return this._owningComboBox.ChildListAccessibleObject;
				}
				return null;
			}

			// Token: 0x06006391 RID: 25489 RVA: 0x0016FDBD File Offset: 0x0016DFBD
			internal AccessibleObject GetChildFragment(int index)
			{
				if (this._owningComboBox.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					if (index == 0)
					{
						return this._owningComboBox.ChildTextAccessibleObject;
					}
					index--;
				}
				if (index == 0 && this._owningComboBox.DropDownStyle != ComboBoxStyle.Simple)
				{
					return this.DropDownButtonUiaProvider;
				}
				return null;
			}

			// Token: 0x06006392 RID: 25490 RVA: 0x0016FDFC File Offset: 0x0016DFFC
			internal int GetChildFragmentCount()
			{
				int num = 0;
				if (this._owningComboBox.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					num++;
				}
				if (this._owningComboBox.DropDownStyle != ComboBoxStyle.Simple)
				{
					num++;
				}
				return num;
			}

			// Token: 0x06006393 RID: 25491 RVA: 0x0016FE30 File Offset: 0x0016E030
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50003;
				}
				if (propertyID == 30008)
				{
					return this._owningComboBox.Focused;
				}
				if (propertyID != 30020)
				{
					return base.GetPropertyValue(propertyID);
				}
				return this._owningComboBox.Handle;
			}

			// Token: 0x06006394 RID: 25492 RVA: 0x0016FE8B File Offset: 0x0016E08B
			internal void RemoveFromItemsCollection(int index, int total)
			{
				if (this.IsItemsCollectionCreated && this._itemAccessibleObjects.Count == total + 1)
				{
					this._itemAccessibleObjects.RemoveAt(index);
				}
			}

			// Token: 0x06006395 RID: 25493 RVA: 0x0016FEB1 File Offset: 0x0016E0B1
			internal void ResetListItemAccessibleObjects()
			{
				List<ComboBox.ComboBoxItemAccessibleObject> itemAccessibleObjects = this._itemAccessibleObjects;
				if (itemAccessibleObjects != null)
				{
					itemAccessibleObjects.Clear();
				}
				this._itemAccessibleObjects = null;
			}

			// Token: 0x06006396 RID: 25494 RVA: 0x0016FECC File Offset: 0x0016E0CC
			internal void SetComboBoxItemFocus()
			{
				int selectedIndex = this._owningComboBox.SelectedIndex;
				if (selectedIndex < 0 || selectedIndex >= this.ItemsAccessibleObjects.Count)
				{
					return;
				}
				this.ItemsAccessibleObjects[selectedIndex].SetFocus();
			}

			// Token: 0x06006397 RID: 25495 RVA: 0x0016FF0C File Offset: 0x0016E10C
			internal void SetComboBoxItemSelection()
			{
				int selectedIndex = this._owningComboBox.SelectedIndex;
				if (selectedIndex < 0 || selectedIndex >= this.ItemsAccessibleObjects.Count)
				{
					return;
				}
				this.ItemsAccessibleObjects[selectedIndex].RaiseAutomationEvent(20012);
			}

			// Token: 0x06006398 RID: 25496 RVA: 0x0016FF4F File Offset: 0x0016E14F
			internal void SetItemInternal(int index, object value, int total)
			{
				if (this.IsItemsCollectionCreated && this._itemAccessibleObjects.Count == total)
				{
					this._itemAccessibleObjects[index].SetItemInternal(value);
				}
			}

			// Token: 0x06006399 RID: 25497 RVA: 0x0016FF79 File Offset: 0x0016E179
			internal override void SetFocus()
			{
				base.SetFocus();
				base.RaiseAutomationEvent(20005);
			}

			// Token: 0x0400392C RID: 14636
			private ComboBox.ComboBoxChildDropDownButtonUiaProvider _dropDownButtonUiaProvider;

			// Token: 0x0400392D RID: 14637
			private List<ComboBox.ComboBoxItemAccessibleObject> _itemAccessibleObjects;

			// Token: 0x0400392E RID: 14638
			private ComboBox _owningComboBox;
		}

		// Token: 0x0200062A RID: 1578
		[ComVisible(true)]
		internal class ComboBoxChildEditUiaProvider : ComboBox.ChildAccessibleObject
		{
			// Token: 0x0600639A RID: 25498 RVA: 0x0016FF8D File Offset: 0x0016E18D
			public ComboBoxChildEditUiaProvider(ComboBox owner, IntPtr childEditControlhandle)
				: base(owner, childEditControlhandle)
			{
				this._handle = childEditControlhandle;
			}

			// Token: 0x0600639B RID: 25499 RVA: 0x0016FFA0 File Offset: 0x0016E1A0
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.Owner == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return base.Owner.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					if (base.Owner.DropDownStyle == ComboBoxStyle.Simple)
					{
						return null;
					}
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.Owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						int childFragmentCount = comboBoxUiaProvider.GetChildFragmentCount();
						if (childFragmentCount > 1)
						{
							return comboBoxUiaProvider.GetChildFragment(childFragmentCount - 1);
						}
					}
					return null;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.Owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						AccessibleObject childFragment = comboBoxUiaProvider.GetChildFragment(0);
						if (this.RuntimeId != childFragment.RuntimeId)
						{
							return childFragment;
						}
					}
					return null;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x1700154D RID: 5453
			// (get) Token: 0x0600639C RID: 25500 RVA: 0x00170046 File Offset: 0x0016E246
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ComboBox owner = base.Owner;
					if (owner == null)
					{
						return null;
					}
					return owner.AccessibilityObject;
				}
			}

			// Token: 0x0600639D RID: 25501 RVA: 0x0017005C File Offset: 0x0016E25C
			internal override object GetPropertyValue(int propertyID)
			{
				switch (propertyID)
				{
				case 30000:
					return this.RuntimeId;
				case 30001:
					return this.Bounds;
				case 30003:
					return 50004;
				case 30005:
					return this.Name;
				case 30007:
					return string.Empty;
				case 30008:
				{
					ComboBox owner = base.Owner;
					return (owner != null) ? new bool?(owner.Focused) : null;
				}
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
				{
					ComboBox owner2 = base.Owner;
					return (owner2 != null) ? new bool?(owner2.Enabled) : null;
				}
				case 30011:
					return "1001";
				case 30013:
					return this.Help ?? string.Empty;
				case 30019:
					return false;
				case 30020:
					return this._handle;
				case 30022:
					return false;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x1700154E RID: 5454
			// (get) Token: 0x0600639E RID: 25502 RVA: 0x0017019C File Offset: 0x0016E39C
			internal override UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
			{
				get
				{
					if (AccessibilityImprovements.Level3)
					{
						UnsafeNativeMethods.IRawElementProviderSimple rawElementProviderSimple;
						UnsafeNativeMethods.UiaHostProviderFromHwnd(new HandleRef(this, this._handle), out rawElementProviderSimple);
						return rawElementProviderSimple;
					}
					return base.HostRawElementProvider;
				}
			}

			// Token: 0x0600639F RID: 25503 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsIAccessibleExSupported()
			{
				return true;
			}

			// Token: 0x1700154F RID: 5455
			// (get) Token: 0x060063A0 RID: 25504 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override int ProviderOptions
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x17001550 RID: 5456
			// (get) Token: 0x060063A1 RID: 25505 RVA: 0x001701CC File Offset: 0x0016E3CC
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						this.GetHashCode()
					};
				}
			}

			// Token: 0x0400392F RID: 14639
			private const string COMBO_BOX_EDIT_AUTOMATION_ID = "1001";

			// Token: 0x04003930 RID: 14640
			private IntPtr _handle;
		}

		// Token: 0x0200062B RID: 1579
		[ComVisible(true)]
		internal class ComboBoxChildListUiaProvider : ComboBox.ChildAccessibleObject
		{
			// Token: 0x060063A2 RID: 25506 RVA: 0x001701EF File Offset: 0x0016E3EF
			public ComboBoxChildListUiaProvider(ComboBox owner, IntPtr childListControlhandle)
				: base(owner, childListControlhandle)
			{
				this._childListControlhandle = childListControlhandle;
			}

			// Token: 0x060063A3 RID: 25507 RVA: 0x00170200 File Offset: 0x0016E400
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				if (AccessibilityImprovements.Level3)
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					if (systemIAccessibleInternal != null)
					{
						object obj = systemIAccessibleInternal.accHitTest((int)x, (int)y);
						if (obj is int)
						{
							int num = (int)obj;
							return this.GetChildFragment(num - 1);
						}
						return null;
					}
				}
				return base.ElementProviderFromPoint(x, y);
			}

			// Token: 0x060063A4 RID: 25508 RVA: 0x0017024C File Offset: 0x0016E44C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					return this.GetChildFragment(0);
				}
				if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return base.FragmentNavigate(direction);
				}
				int childFragmentCount = this.GetChildFragmentCount();
				if (childFragmentCount > 0)
				{
					return this.GetChildFragment(childFragmentCount - 1);
				}
				return null;
			}

			// Token: 0x17001551 RID: 5457
			// (get) Token: 0x060063A5 RID: 25509 RVA: 0x00170046 File Offset: 0x0016E246
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ComboBox owner = base.Owner;
					if (owner == null)
					{
						return null;
					}
					return owner.AccessibilityObject;
				}
			}

			// Token: 0x060063A6 RID: 25510 RVA: 0x0017028C File Offset: 0x0016E48C
			public AccessibleObject GetChildFragment(int index)
			{
				if (base.Owner == null || !base.Owner.IsHandleCreated)
				{
					return null;
				}
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.Owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (index < 0 || index >= base.Owner.Items.Count || comboBoxUiaProvider == null || index >= comboBoxUiaProvider.ItemsAccessibleObjects.Count)
				{
					return null;
				}
				return comboBoxUiaProvider.ItemsAccessibleObjects[index];
			}

			// Token: 0x060063A7 RID: 25511 RVA: 0x001702F7 File Offset: 0x0016E4F7
			public int GetChildFragmentCount()
			{
				if (base.Owner == null)
				{
					return 0;
				}
				return base.Owner.Items.Count;
			}

			// Token: 0x060063A8 RID: 25512 RVA: 0x00170314 File Offset: 0x0016E514
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30037)
				{
					switch (propertyID)
					{
					case 30000:
						return this.RuntimeId;
					case 30001:
						return this.Bounds;
					case 30002:
					case 30004:
					case 30006:
					case 30012:
					case 30014:
					case 30015:
					case 30016:
					case 30017:
					case 30018:
					case 30021:
						break;
					case 30003:
						return 50008;
					case 30005:
						return this.Name;
					case 30007:
						return string.Empty;
					case 30008:
						return false;
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
					{
						ComboBox owner = base.Owner;
						return (owner != null) ? new bool?(owner.Enabled) : null;
					}
					case 30011:
						return "1000";
					case 30013:
						return this.Help ?? string.Empty;
					case 30019:
						return false;
					case 30020:
						return this._childListControlhandle;
					case 30022:
						return false;
					default:
						if (propertyID == 30037)
						{
							return true;
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30060)
					{
						return this.CanSelectMultiple;
					}
					if (propertyID == 30061)
					{
						return this.IsSelectionRequired;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x060063A9 RID: 25513 RVA: 0x000F1520 File Offset: 0x000EF720
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				return this.GetFocused();
			}

			// Token: 0x060063AA RID: 25514 RVA: 0x00170484 File Offset: 0x0016E684
			public override AccessibleObject GetFocused()
			{
				if (base.Owner == null)
				{
					return null;
				}
				int selectedIndex = base.Owner.SelectedIndex;
				return this.GetChildFragment(selectedIndex);
			}

			// Token: 0x060063AB RID: 25515 RVA: 0x001704B0 File Offset: 0x0016E6B0
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetSelection()
			{
				if (base.Owner == null)
				{
					return null;
				}
				int selectedIndex = base.Owner.SelectedIndex;
				AccessibleObject childFragment = this.GetChildFragment(selectedIndex);
				if (childFragment != null)
				{
					return new UnsafeNativeMethods.IRawElementProviderSimple[] { childFragment };
				}
				return new UnsafeNativeMethods.IRawElementProviderSimple[0];
			}

			// Token: 0x17001552 RID: 5458
			// (get) Token: 0x060063AC RID: 25516 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal override bool CanSelectMultiple
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001553 RID: 5459
			// (get) Token: 0x060063AD RID: 25517 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsSelectionRequired
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060063AE RID: 25518 RVA: 0x001704EF File Offset: 0x0016E6EF
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || patternId == 10001 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001554 RID: 5460
			// (get) Token: 0x060063AF RID: 25519 RVA: 0x0017050C File Offset: 0x0016E70C
			internal override UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
			{
				get
				{
					if (AccessibilityImprovements.Level3)
					{
						UnsafeNativeMethods.IRawElementProviderSimple rawElementProviderSimple;
						UnsafeNativeMethods.UiaHostProviderFromHwnd(new HandleRef(this, this._childListControlhandle), out rawElementProviderSimple);
						return rawElementProviderSimple;
					}
					return base.HostRawElementProvider;
				}
			}

			// Token: 0x17001555 RID: 5461
			// (get) Token: 0x060063B0 RID: 25520 RVA: 0x0017053C File Offset: 0x0016E73C
			internal override int[] RuntimeId
			{
				get
				{
					if (base.Owner == null)
					{
						return new int[0];
					}
					return new int[]
					{
						42,
						(int)(long)base.Owner.Handle,
						base.Owner.GetListNativeWindowRuntimeIdPart()
					};
				}
			}

			// Token: 0x17001556 RID: 5462
			// (get) Token: 0x060063B1 RID: 25521 RVA: 0x00170588 File Offset: 0x0016E788
			public override AccessibleStates State
			{
				get
				{
					if (base.Owner == null)
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable;
					if (base.Owner.Focused)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x04003931 RID: 14641
			private const string COMBO_BOX_LIST_AUTOMATION_ID = "1000";

			// Token: 0x04003932 RID: 14642
			private IntPtr _childListControlhandle;
		}

		// Token: 0x0200062C RID: 1580
		[ComVisible(true)]
		internal class ComboBoxChildTextUiaProvider : AccessibleObject
		{
			// Token: 0x060063B2 RID: 25522 RVA: 0x001705B7 File Offset: 0x0016E7B7
			public ComboBoxChildTextUiaProvider(ComboBox owner)
			{
				this._owner = owner;
			}

			// Token: 0x17001557 RID: 5463
			// (get) Token: 0x060063B3 RID: 25523 RVA: 0x001705C6 File Offset: 0x0016E7C6
			public override Rectangle Bounds
			{
				get
				{
					return this._owner.AccessibilityObject.Bounds;
				}
			}

			// Token: 0x060063B4 RID: 25524 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override int GetChildId()
			{
				return 1;
			}

			// Token: 0x17001558 RID: 5464
			// (get) Token: 0x060063B5 RID: 25525 RVA: 0x001705D8 File Offset: 0x0016E7D8
			// (set) Token: 0x060063B6 RID: 25526 RVA: 0x000070A6 File Offset: 0x000052A6
			public override string Name
			{
				get
				{
					return this._owner.AccessibilityObject.Name ?? string.Empty;
				}
				set
				{
				}
			}

			// Token: 0x060063B7 RID: 25527 RVA: 0x001705F4 File Offset: 0x0016E7F4
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this._owner.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						int childFragmentCount = comboBoxUiaProvider.GetChildFragmentCount();
						if (childFragmentCount > 1)
						{
							return comboBoxUiaProvider.GetChildFragment(childFragmentCount - 1);
						}
					}
					return null;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						AccessibleObject childFragment = comboBoxUiaProvider.GetChildFragment(0);
						if (this.RuntimeId != childFragment.RuntimeId)
						{
							return childFragment;
						}
					}
					return null;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x17001559 RID: 5465
			// (get) Token: 0x060063B8 RID: 25528 RVA: 0x00170681 File Offset: 0x0016E881
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this._owner.AccessibilityObject;
				}
			}

			// Token: 0x060063B9 RID: 25529 RVA: 0x00170690 File Offset: 0x0016E890
			internal override object GetPropertyValue(int propertyID)
			{
				switch (propertyID)
				{
				case 30000:
					return this.RuntimeId;
				case 30001:
					return this.Bounds;
				case 30002:
				case 30004:
				case 30006:
				case 30011:
				case 30012:
					break;
				case 30003:
					return 50020;
				case 30005:
					return this.Name;
				case 30007:
					return string.Empty;
				case 30008:
					return this._owner.Focused;
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
					return this._owner.Enabled;
				case 30013:
					return this.Help ?? string.Empty;
				default:
					if (propertyID == 30019 || propertyID == 30022)
					{
						return false;
					}
					break;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x1700155A RID: 5466
			// (get) Token: 0x060063BA RID: 25530 RVA: 0x0017077C File Offset: 0x0016E97C
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						(int)(long)this._owner.Handle,
						this._owner.GetHashCode(),
						this.GetHashCode(),
						this.GetChildId()
					};
				}
			}

			// Token: 0x1700155B RID: 5467
			// (get) Token: 0x060063BB RID: 25531 RVA: 0x001707CC File Offset: 0x0016E9CC
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable;
					if (this._owner.Focused)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x04003933 RID: 14643
			private const int COMBOBOX_TEXT_ACC_ITEM_INDEX = 1;

			// Token: 0x04003934 RID: 14644
			private ComboBox _owner;
		}

		// Token: 0x0200062D RID: 1581
		[ComVisible(true)]
		internal class ComboBoxChildDropDownButtonUiaProvider : AccessibleObject
		{
			// Token: 0x060063BC RID: 25532 RVA: 0x001707F1 File Offset: 0x0016E9F1
			public ComboBoxChildDropDownButtonUiaProvider(ComboBox owner, IntPtr comboBoxControlhandle)
			{
				this._owner = owner;
				base.UseStdAccessibleObjects(comboBoxControlhandle);
			}

			// Token: 0x1700155C RID: 5468
			// (get) Token: 0x060063BD RID: 25533 RVA: 0x00170807 File Offset: 0x0016EA07
			// (set) Token: 0x060063BE RID: 25534 RVA: 0x00170828 File Offset: 0x0016EA28
			public override string Name
			{
				get
				{
					return SR.GetString(this._owner.DroppedDown ? "ComboboxDropDownButtonCloseName" : "ComboboxDropDownButtonOpenName");
				}
				set
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					systemIAccessibleInternal.set_accName(2, value);
				}
			}

			// Token: 0x1700155D RID: 5469
			// (get) Token: 0x060063BF RID: 25535 RVA: 0x0017084C File Offset: 0x0016EA4C
			public override Rectangle Bounds
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					int num;
					int num2;
					int num3;
					int num4;
					systemIAccessibleInternal.accLocation(out num, out num2, out num3, out num4, 2);
					return new Rectangle(num, num2, num3, num4);
				}
			}

			// Token: 0x1700155E RID: 5470
			// (get) Token: 0x060063C0 RID: 25536 RVA: 0x00170880 File Offset: 0x0016EA80
			public override string DefaultAction
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accDefaultAction(2);
				}
			}

			// Token: 0x060063C1 RID: 25537 RVA: 0x001708A0 File Offset: 0x0016EAA0
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent)
				{
					return this._owner.AccessibilityObject;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						int childFragmentCount = comboBoxUiaProvider.GetChildFragmentCount();
						if (childFragmentCount > 1)
						{
							return comboBoxUiaProvider.GetChildFragment(childFragmentCount - 1);
						}
					}
					return null;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x1700155F RID: 5471
			// (get) Token: 0x060063C2 RID: 25538 RVA: 0x001708F2 File Offset: 0x0016EAF2
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this._owner.AccessibilityObject;
				}
			}

			// Token: 0x060063C3 RID: 25539 RVA: 0x00016041 File Offset: 0x00014241
			internal override int GetChildId()
			{
				return 2;
			}

			// Token: 0x060063C4 RID: 25540 RVA: 0x00170900 File Offset: 0x0016EB00
			internal override object GetPropertyValue(int propertyID)
			{
				switch (propertyID)
				{
				case 30000:
					return this.RuntimeId;
				case 30001:
					return this.BoundingRectangle;
				case 30002:
				case 30004:
				case 30006:
				case 30011:
				case 30012:
					break;
				case 30003:
					return 50000;
				case 30005:
					return this.Name;
				case 30007:
					return this.KeyboardShortcut;
				case 30008:
					return this._owner.Focused;
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
					return this._owner.Enabled;
				case 30013:
					return this.Help ?? string.Empty;
				default:
					if (propertyID == 30019)
					{
						return false;
					}
					if (propertyID == 30022)
					{
						return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
					}
					break;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001560 RID: 5472
			// (get) Token: 0x060063C5 RID: 25541 RVA: 0x00170A0C File Offset: 0x0016EC0C
			public override string Help
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accHelp(2);
				}
			}

			// Token: 0x17001561 RID: 5473
			// (get) Token: 0x060063C6 RID: 25542 RVA: 0x00170A2C File Offset: 0x0016EC2C
			public override string KeyboardShortcut
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accKeyboardShortcut(2);
				}
			}

			// Token: 0x060063C7 RID: 25543 RVA: 0x00170A4C File Offset: 0x0016EC4C
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || patternId == 10000 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001562 RID: 5474
			// (get) Token: 0x060063C8 RID: 25544 RVA: 0x00170A68 File Offset: 0x0016EC68
			public override AccessibleRole Role
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleRole)systemIAccessibleInternal.get_accRole(2);
				}
			}

			// Token: 0x17001563 RID: 5475
			// (get) Token: 0x060063C9 RID: 25545 RVA: 0x00170A90 File Offset: 0x0016EC90
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						(int)(long)this._owner.Handle,
						this._owner.GetHashCode(),
						61453,
						2
					};
				}
			}

			// Token: 0x17001564 RID: 5476
			// (get) Token: 0x060063CA RID: 25546 RVA: 0x00170AD8 File Offset: 0x0016ECD8
			public override AccessibleStates State
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleStates)systemIAccessibleInternal.get_accState(2);
				}
			}

			// Token: 0x04003935 RID: 14645
			private const int COMBOBOX_DROPDOWN_BUTTON_ACC_ITEM_INDEX = 2;

			// Token: 0x04003936 RID: 14646
			private ComboBox _owner;
		}

		// Token: 0x0200062E RID: 1582
		private sealed class ACNativeWindow : NativeWindow
		{
			// Token: 0x060063CB RID: 25547 RVA: 0x00170AFD File Offset: 0x0016ECFD
			internal ACNativeWindow(IntPtr acHandle)
			{
				base.AssignHandle(acHandle);
				ComboBox.ACNativeWindow.ACWindows.Add(acHandle, this);
				UnsafeNativeMethods.EnumChildWindows(new HandleRef(this, acHandle), new NativeMethods.EnumChildrenCallback(ComboBox.ACNativeWindow.RegisterACWindowRecursive), NativeMethods.NullHandleRef);
			}

			// Token: 0x060063CC RID: 25548 RVA: 0x00170B3C File Offset: 0x0016ED3C
			private static bool RegisterACWindowRecursive(IntPtr handle, IntPtr lparam)
			{
				if (!ComboBox.ACNativeWindow.ACWindows.ContainsKey(handle))
				{
					ComboBox.ACNativeWindow acnativeWindow = new ComboBox.ACNativeWindow(handle);
				}
				return true;
			}

			// Token: 0x17001565 RID: 5477
			// (get) Token: 0x060063CD RID: 25549 RVA: 0x00170B63 File Offset: 0x0016ED63
			internal bool Visible
			{
				get
				{
					return SafeNativeMethods.IsWindowVisible(new HandleRef(this, base.Handle));
				}
			}

			// Token: 0x17001566 RID: 5478
			// (get) Token: 0x060063CE RID: 25550 RVA: 0x00170B78 File Offset: 0x0016ED78
			internal static bool AutoCompleteActive
			{
				get
				{
					if (ComboBox.ACNativeWindow.inWndProcCnt > 0)
					{
						return true;
					}
					foreach (object obj in ComboBox.ACNativeWindow.ACWindows.Values)
					{
						ComboBox.ACNativeWindow acnativeWindow = obj as ComboBox.ACNativeWindow;
						if (acnativeWindow != null && acnativeWindow.Visible)
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x060063CF RID: 25551 RVA: 0x00170BF0 File Offset: 0x0016EDF0
			protected override void WndProc(ref Message m)
			{
				ComboBox.ACNativeWindow.inWndProcCnt++;
				try
				{
					base.WndProc(ref m);
				}
				finally
				{
					ComboBox.ACNativeWindow.inWndProcCnt--;
				}
				if (m.Msg == 130)
				{
					ComboBox.ACNativeWindow.ACWindows.Remove(base.Handle);
				}
			}

			// Token: 0x060063D0 RID: 25552 RVA: 0x00170C54 File Offset: 0x0016EE54
			internal static void RegisterACWindow(IntPtr acHandle, bool subclass)
			{
				if (subclass && ComboBox.ACNativeWindow.ACWindows.ContainsKey(acHandle) && ComboBox.ACNativeWindow.ACWindows[acHandle] == null)
				{
					ComboBox.ACNativeWindow.ACWindows.Remove(acHandle);
				}
				if (!ComboBox.ACNativeWindow.ACWindows.ContainsKey(acHandle))
				{
					if (subclass)
					{
						ComboBox.ACNativeWindow acnativeWindow = new ComboBox.ACNativeWindow(acHandle);
						return;
					}
					ComboBox.ACNativeWindow.ACWindows.Add(acHandle, null);
				}
			}

			// Token: 0x060063D1 RID: 25553 RVA: 0x00170CC8 File Offset: 0x0016EEC8
			internal static void ClearNullACWindows()
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in ComboBox.ACNativeWindow.ACWindows)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value == null)
					{
						arrayList.Add(dictionaryEntry.Key);
					}
				}
				foreach (object obj2 in arrayList)
				{
					IntPtr intPtr = (IntPtr)obj2;
					ComboBox.ACNativeWindow.ACWindows.Remove(intPtr);
				}
			}

			// Token: 0x04003937 RID: 14647
			internal static int inWndProcCnt;

			// Token: 0x04003938 RID: 14648
			private static Hashtable ACWindows = new Hashtable();
		}

		// Token: 0x0200062F RID: 1583
		private class AutoCompleteDropDownFinder
		{
			// Token: 0x060063D3 RID: 25555 RVA: 0x00170D94 File Offset: 0x0016EF94
			internal void FindDropDowns()
			{
				this.FindDropDowns(true);
			}

			// Token: 0x060063D4 RID: 25556 RVA: 0x00170D9D File Offset: 0x0016EF9D
			internal void FindDropDowns(bool subclass)
			{
				if (!subclass)
				{
					ComboBox.ACNativeWindow.ClearNullACWindows();
				}
				this.shouldSubClass = subclass;
				UnsafeNativeMethods.EnumThreadWindows(SafeNativeMethods.GetCurrentThreadId(), new NativeMethods.EnumThreadWindowsCallback(this.Callback), new HandleRef(null, IntPtr.Zero));
			}

			// Token: 0x060063D5 RID: 25557 RVA: 0x00170DD0 File Offset: 0x0016EFD0
			private bool Callback(IntPtr hWnd, IntPtr lParam)
			{
				HandleRef handleRef = new HandleRef(null, hWnd);
				if (ComboBox.AutoCompleteDropDownFinder.GetClassName(handleRef) == "Auto-Suggest Dropdown")
				{
					ComboBox.ACNativeWindow.RegisterACWindow(handleRef.Handle, this.shouldSubClass);
				}
				return true;
			}

			// Token: 0x060063D6 RID: 25558 RVA: 0x00170E0C File Offset: 0x0016F00C
			private static string GetClassName(HandleRef hRef)
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				UnsafeNativeMethods.GetClassName(hRef, stringBuilder, 256);
				return stringBuilder.ToString();
			}

			// Token: 0x04003939 RID: 14649
			private const int MaxClassName = 256;

			// Token: 0x0400393A RID: 14650
			private const string AutoCompleteClassName = "Auto-Suggest Dropdown";

			// Token: 0x0400393B RID: 14651
			private bool shouldSubClass;
		}

		// Token: 0x02000630 RID: 1584
		internal class FlatComboAdapter
		{
			// Token: 0x060063D8 RID: 25560 RVA: 0x00170E38 File Offset: 0x0016F038
			public FlatComboAdapter(ComboBox comboBox, bool smallButton)
			{
				if ((!ComboBox.FlatComboAdapter.isScalingInitialized && DpiHelper.IsScalingRequired) || DpiHelper.EnableDpiChangedMessageHandling)
				{
					ComboBox.FlatComboAdapter.Offset2Pixels = comboBox.LogicalToDeviceUnits(ComboBox.FlatComboAdapter.OFFSET_2PIXELS);
					ComboBox.FlatComboAdapter.isScalingInitialized = true;
				}
				this.clientRect = comboBox.ClientRectangle;
				int horizontalScrollBarArrowWidthForDpi = SystemInformation.GetHorizontalScrollBarArrowWidthForDpi(comboBox.deviceDpi);
				this.outerBorder = new Rectangle(this.clientRect.Location, new Size(this.clientRect.Width - 1, this.clientRect.Height - 1));
				this.innerBorder = new Rectangle(this.outerBorder.X + 1, this.outerBorder.Y + 1, this.outerBorder.Width - horizontalScrollBarArrowWidthForDpi - 2, this.outerBorder.Height - 2);
				this.innerInnerBorder = new Rectangle(this.innerBorder.X + 1, this.innerBorder.Y + 1, this.innerBorder.Width - 2, this.innerBorder.Height - 2);
				this.dropDownRect = new Rectangle(this.innerBorder.Right + 1, this.innerBorder.Y, horizontalScrollBarArrowWidthForDpi, this.innerBorder.Height + 1);
				if (smallButton)
				{
					this.whiteFillRect = this.dropDownRect;
					this.whiteFillRect.Width = 5;
					this.dropDownRect.X = this.dropDownRect.X + 5;
					this.dropDownRect.Width = this.dropDownRect.Width - 5;
				}
				this.origRightToLeft = comboBox.RightToLeft;
				if (this.origRightToLeft == RightToLeft.Yes)
				{
					this.innerBorder.X = this.clientRect.Width - this.innerBorder.Right;
					this.innerInnerBorder.X = this.clientRect.Width - this.innerInnerBorder.Right;
					this.dropDownRect.X = this.clientRect.Width - this.dropDownRect.Right;
					this.whiteFillRect.X = this.clientRect.Width - this.whiteFillRect.Right + 1;
				}
			}

			// Token: 0x060063D9 RID: 25561 RVA: 0x00171057 File Offset: 0x0016F257
			public bool IsValid(ComboBox combo)
			{
				return combo.ClientRectangle == this.clientRect && combo.RightToLeft == this.origRightToLeft;
			}

			// Token: 0x060063DA RID: 25562 RVA: 0x0017107C File Offset: 0x0016F27C
			public virtual void DrawFlatCombo(ComboBox comboBox, Graphics g)
			{
				if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
				{
					return;
				}
				Color outerBorderColor = this.GetOuterBorderColor(comboBox);
				Color innerBorderColor = this.GetInnerBorderColor(comboBox);
				bool flag = comboBox.RightToLeft == RightToLeft.Yes;
				this.DrawFlatComboDropDown(comboBox, g, this.dropDownRect);
				if (!LayoutUtils.IsZeroWidthOrHeight(this.whiteFillRect))
				{
					using (Brush brush = new SolidBrush(innerBorderColor))
					{
						g.FillRectangle(brush, this.whiteFillRect);
					}
				}
				if (outerBorderColor.IsSystemColor)
				{
					Pen pen = SystemPens.FromSystemColor(outerBorderColor);
					g.DrawRectangle(pen, this.outerBorder);
					if (flag)
					{
						g.DrawRectangle(pen, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
					}
					else
					{
						g.DrawRectangle(pen, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
					}
				}
				else
				{
					using (Pen pen2 = new Pen(outerBorderColor))
					{
						g.DrawRectangle(pen2, this.outerBorder);
						if (flag)
						{
							g.DrawRectangle(pen2, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
						}
						else
						{
							g.DrawRectangle(pen2, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
						}
					}
				}
				if (innerBorderColor.IsSystemColor)
				{
					Pen pen3 = SystemPens.FromSystemColor(innerBorderColor);
					g.DrawRectangle(pen3, this.innerBorder);
					g.DrawRectangle(pen3, this.innerInnerBorder);
				}
				else
				{
					using (Pen pen4 = new Pen(innerBorderColor))
					{
						g.DrawRectangle(pen4, this.innerBorder);
						g.DrawRectangle(pen4, this.innerInnerBorder);
					}
				}
				if (!comboBox.Enabled || comboBox.FlatStyle == FlatStyle.Popup)
				{
					bool flag2 = comboBox.ContainsFocus || comboBox.MouseIsOver;
					Color popupOuterBorderColor = this.GetPopupOuterBorderColor(comboBox, flag2);
					using (Pen pen5 = new Pen(popupOuterBorderColor))
					{
						Pen pen6 = (comboBox.Enabled ? pen5 : SystemPens.Control);
						if (flag)
						{
							g.DrawRectangle(pen6, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
						}
						else
						{
							g.DrawRectangle(pen6, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
						}
						g.DrawRectangle(pen5, this.outerBorder);
					}
				}
			}

			// Token: 0x060063DB RID: 25563 RVA: 0x001713C4 File Offset: 0x0016F5C4
			protected virtual void DrawFlatComboDropDown(ComboBox comboBox, Graphics g, Rectangle dropDownRect)
			{
				g.FillRectangle(SystemBrushes.Control, dropDownRect);
				Brush brush = (comboBox.Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark);
				Point point = new Point(dropDownRect.Left + dropDownRect.Width / 2, dropDownRect.Top + dropDownRect.Height / 2);
				if (this.origRightToLeft == RightToLeft.Yes)
				{
					point.X -= dropDownRect.Width % 2;
				}
				else
				{
					point.X += dropDownRect.Width % 2;
				}
				g.FillPolygon(brush, new Point[]
				{
					new Point(point.X - ComboBox.FlatComboAdapter.Offset2Pixels, point.Y - 1),
					new Point(point.X + ComboBox.FlatComboAdapter.Offset2Pixels + 1, point.Y - 1),
					new Point(point.X, point.Y + ComboBox.FlatComboAdapter.Offset2Pixels)
				});
			}

			// Token: 0x060063DC RID: 25564 RVA: 0x001714C7 File Offset: 0x0016F6C7
			protected virtual Color GetOuterBorderColor(ComboBox comboBox)
			{
				if (!comboBox.Enabled)
				{
					return SystemColors.ControlDark;
				}
				return SystemColors.Window;
			}

			// Token: 0x060063DD RID: 25565 RVA: 0x001714DC File Offset: 0x0016F6DC
			protected virtual Color GetPopupOuterBorderColor(ComboBox comboBox, bool focused)
			{
				if (!comboBox.Enabled)
				{
					return SystemColors.ControlDark;
				}
				if (!focused)
				{
					return SystemColors.Window;
				}
				return SystemColors.ControlDark;
			}

			// Token: 0x060063DE RID: 25566 RVA: 0x001714FA File Offset: 0x0016F6FA
			protected virtual Color GetInnerBorderColor(ComboBox comboBox)
			{
				if (!comboBox.Enabled)
				{
					return SystemColors.Control;
				}
				return comboBox.BackColor;
			}

			// Token: 0x060063DF RID: 25567 RVA: 0x00171510 File Offset: 0x0016F710
			public void ValidateOwnerDrawRegions(ComboBox comboBox, Rectangle updateRegionBox)
			{
				if (comboBox != null)
				{
					return;
				}
				Rectangle rectangle = new Rectangle(0, 0, comboBox.Width, this.innerBorder.Top);
				Rectangle rectangle2 = new Rectangle(0, this.innerBorder.Bottom, comboBox.Width, comboBox.Height - this.innerBorder.Bottom);
				Rectangle rectangle3 = new Rectangle(0, 0, this.innerBorder.Left, comboBox.Height);
				Rectangle rectangle4 = new Rectangle(this.innerBorder.Right, 0, comboBox.Width - this.innerBorder.Right, comboBox.Height);
				if (rectangle.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(rectangle);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
				if (rectangle2.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(rectangle2);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
				if (rectangle3.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(rectangle3);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
				if (rectangle4.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(rectangle4);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
			}

			// Token: 0x0400393C RID: 14652
			private Rectangle outerBorder;

			// Token: 0x0400393D RID: 14653
			private Rectangle innerBorder;

			// Token: 0x0400393E RID: 14654
			private Rectangle innerInnerBorder;

			// Token: 0x0400393F RID: 14655
			internal Rectangle dropDownRect;

			// Token: 0x04003940 RID: 14656
			private Rectangle whiteFillRect;

			// Token: 0x04003941 RID: 14657
			private Rectangle clientRect;

			// Token: 0x04003942 RID: 14658
			private RightToLeft origRightToLeft;

			// Token: 0x04003943 RID: 14659
			private const int WhiteFillRectWidth = 5;

			// Token: 0x04003944 RID: 14660
			private static bool isScalingInitialized = false;

			// Token: 0x04003945 RID: 14661
			private static int OFFSET_2PIXELS = 2;

			// Token: 0x04003946 RID: 14662
			protected static int Offset2Pixels = ComboBox.FlatComboAdapter.OFFSET_2PIXELS;
		}

		// Token: 0x02000631 RID: 1585
		internal enum ChildWindowType
		{
			// Token: 0x04003948 RID: 14664
			ListBox,
			// Token: 0x04003949 RID: 14665
			Edit,
			// Token: 0x0400394A RID: 14666
			DropDownList
		}
	}
}
