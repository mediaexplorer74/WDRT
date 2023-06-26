using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a hierarchical collection of labeled items, each represented by a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	// Token: 0x02000416 RID: 1046
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Nodes")]
	[DefaultEvent("AfterSelect")]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.TreeViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTreeView")]
	public class TreeView : Control
	{
		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x001330A9 File Offset: 0x001312A9
		private static Size? ScaledStateImageSize
		{
			get
			{
				if (!TreeView.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						TreeView.scaledStateImageSize = new Size?(DpiHelper.LogicalToDeviceUnits(new Size(16, 16), 0));
					}
					TreeView.isScalingInitialized = true;
				}
				return TreeView.scaledStateImageSize;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x001330DD File Offset: 0x001312DD
		internal ImageList.Indexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ImageList.Indexer();
				}
				this.imageIndexer.ImageList = this.ImageList;
				return this.imageIndexer;
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x00133109 File Offset: 0x00131309
		internal ImageList.Indexer SelectedImageIndexer
		{
			get
			{
				if (this.selectedImageIndexer == null)
				{
					this.selectedImageIndexer = new ImageList.Indexer();
				}
				this.selectedImageIndexer.ImageList = this.ImageList;
				return this.selectedImageIndexer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeView" /> class.</summary>
		// Token: 0x060048F5 RID: 18677 RVA: 0x00133138 File Offset: 0x00131338
		public TreeView()
		{
			this.treeViewState = new BitVector32(117);
			this.root = new TreeNode(this);
			this.SelectedImageIndexer.Index = 0;
			this.ImageIndexer.Index = 0;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x00027DA7 File Offset: 0x00025FA7
		// (set) Token: 0x060048F7 RID: 18679 RVA: 0x001331D2 File Offset: 0x001313D2
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
				if (base.IsHandleCreated)
				{
					base.SendMessage(4381, 0, ColorTranslator.ToWin32(this.BackColor));
					base.SendMessage(4359, this.Indent, 0);
				}
			}
		}

		/// <summary>Gets or set the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060048F9 RID: 18681 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TreeView.BackgroundImage" /> property changes.</summary>
		// Token: 0x140003A0 RID: 928
		// (add) Token: 0x060048FA RID: 18682 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060048FB RID: 18683 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets the layout of the background image for the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values. The default is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />.</returns>
		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060048FD RID: 18685 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TreeView.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140003A1 RID: 929
		// (add) Token: 0x060048FE RID: 18686 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060048FF RID: 18687 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets the border style of the tree view control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06004900 RID: 18688 RVA: 0x0013320E File Offset: 0x0013140E
		// (set) Token: 0x06004901 RID: 18689 RVA: 0x00133216 File Offset: 0x00131416
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("borderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether check boxes are displayed next to the tree nodes in the tree view control.</summary>
		/// <returns>
		///   <see langword="true" /> if a check box is displayed next to each tree node in the tree view control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06004902 RID: 18690 RVA: 0x00133254 File Offset: 0x00131454
		// (set) Token: 0x06004903 RID: 18691 RVA: 0x00133264 File Offset: 0x00131464
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("TreeViewCheckBoxesDescr")]
		public bool CheckBoxes
		{
			get
			{
				return this.treeViewState[8];
			}
			set
			{
				if (this.CheckBoxes != value)
				{
					this.treeViewState[8] = value;
					if (base.IsHandleCreated)
					{
						if (this.CheckBoxes)
						{
							base.UpdateStyles();
							return;
						}
						this.UpdateCheckedState(this.root, false);
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>The creation parameters.</returns>
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x001332B4 File Offset: 0x001314B4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysTreeView32";
				if (base.IsHandleCreated)
				{
					int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16);
					createParams.Style |= num & 3145728;
				}
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				if (!this.Scrollable)
				{
					createParams.Style |= 8192;
				}
				if (!this.HideSelection)
				{
					createParams.Style |= 32;
				}
				if (this.LabelEdit)
				{
					createParams.Style |= 8;
				}
				if (this.ShowLines)
				{
					createParams.Style |= 2;
				}
				if (this.ShowPlusMinus)
				{
					createParams.Style |= 1;
				}
				if (this.ShowRootLines)
				{
					createParams.Style |= 4;
				}
				if (this.HotTracking)
				{
					createParams.Style |= 512;
				}
				if (this.FullRowSelect)
				{
					createParams.Style |= 4096;
				}
				if (this.setOddHeight)
				{
					createParams.Style |= 16384;
				}
				if (this.ShowNodeToolTips && base.IsHandleCreated && !base.DesignMode)
				{
					createParams.Style |= 2048;
				}
				if (this.CheckBoxes && base.IsHandleCreated)
				{
					createParams.Style |= 256;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					if (this.RightToLeftLayout)
					{
						createParams.ExStyle |= 4194304;
						createParams.ExStyle &= -28673;
					}
					else
					{
						createParams.Style |= 64;
					}
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x000C98A0 File Offset: 0x000C7AA0
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer. The <see cref="P:System.Windows.Forms.TreeView.DoubleBuffered" /> property does not affect the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the control uses a secondary buffer; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x06004907 RID: 18695 RVA: 0x00012FCB File Offset: 0x000111CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x0001300E File Offset: 0x0001120E
		// (set) Token: 0x06004909 RID: 18697 RVA: 0x001334A8 File Offset: 0x001316A8
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
				if (base.IsHandleCreated)
				{
					base.SendMessage(4382, 0, ColorTranslator.ToWin32(this.ForeColor));
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the selection highlight spans the width of the tree view control.</summary>
		/// <returns>
		///   <see langword="true" /> if the selection highlight spans the width of the tree view control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x001334D1 File Offset: 0x001316D1
		// (set) Token: 0x0600490B RID: 18699 RVA: 0x001334E3 File Offset: 0x001316E3
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewFullRowSelectDescr")]
		public bool FullRowSelect
		{
			get
			{
				return this.treeViewState[512];
			}
			set
			{
				if (this.FullRowSelect != value)
				{
					this.treeViewState[512] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected tree node remains highlighted even when the tree view has lost the focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the selected tree node is not highlighted when the tree view has lost the focus; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x0013350D File Offset: 0x0013170D
		// (set) Token: 0x0600490D RID: 18701 RVA: 0x0013351B File Offset: 0x0013171B
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.treeViewState[1];
			}
			set
			{
				if (this.HideSelection != value)
				{
					this.treeViewState[1] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a tree node label takes on the appearance of a hyperlink as the mouse pointer passes over it.</summary>
		/// <returns>
		///   <see langword="true" /> if a tree node label takes on the appearance of a hyperlink as the mouse pointer passes over it; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x00133541 File Offset: 0x00131741
		// (set) Token: 0x0600490F RID: 18703 RVA: 0x00133553 File Offset: 0x00131753
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewHotTrackingDescr")]
		public bool HotTracking
		{
			get
			{
				return this.treeViewState[256];
			}
			set
			{
				if (this.HotTracking != value)
				{
					this.treeViewState[256] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets the image-list index value of the default image that is displayed by the tree nodes.</summary>
		/// <returns>A zero-based index that represents the position of an <see cref="T:System.Drawing.Image" /> in an <see cref="T:System.Windows.Forms.ImageList" />. The default is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than 0.</exception>
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x00133580 File Offset: 0x00131780
		// (set) Token: 0x06004911 RID: 18705 RVA: 0x001335D8 File Offset: 0x001317D8
		[DefaultValue(-1)]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("TreeViewImageIndexDescr")]
		[RelatedImageList("ImageList")]
		public int ImageIndex
		{
			get
			{
				if (this.imageList == null)
				{
					return -1;
				}
				if (this.ImageIndexer.Index >= this.imageList.Images.Count)
				{
					return Math.Max(0, this.imageList.Images.Count - 1);
				}
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value == -1)
				{
					value = 0;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.ImageIndexer.Index != value)
				{
					this.ImageIndexer.Index = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets the key of the default image for each node in the <see cref="T:System.Windows.Forms.TreeView" /> control when it is in an unselected state.</summary>
		/// <returns>The key of the default image shown for each node <see cref="T:System.Windows.Forms.TreeView" /> control when the node is in an unselected state.</returns>
		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x0013365B File Offset: 0x0013185B
		// (set) Token: 0x06004913 RID: 18707 RVA: 0x00133668 File Offset: 0x00131868
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TreeViewImageKeyDescr")]
		[RelatedImageList("ImageList")]
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
					if (string.IsNullOrEmpty(value) || value.Equals(SR.GetString("toStringNone")))
					{
						this.ImageIndex = ((this.ImageList != null) ? 0 : (-1));
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> objects that are used by the tree nodes.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> objects that are used by the tree nodes. The default value is <see langword="null" />.</returns>
		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x001336CE File Offset: 0x001318CE
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x001336D8 File Offset: 0x001318D8
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("TreeViewImageListDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				if (value != this.imageList)
				{
					this.DetachImageListHandlers();
					this.imageList = value;
					this.AttachImageListHandlers();
					if (base.IsHandleCreated)
					{
						base.SendMessage(4361, 0, (value == null) ? IntPtr.Zero : value.Handle);
						if (this.StateImageList != null && this.StateImageList.Images.Count > 0)
						{
							this.SetStateImageList(this.internalStateImageList.Handle);
						}
					}
					this.UpdateCheckedState(this.root, true);
				}
			}
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x00133760 File Offset: 0x00131960
		private void AttachImageListHandlers()
		{
			if (this.imageList != null)
			{
				this.imageList.RecreateHandle += this.ImageListRecreateHandle;
				this.imageList.Disposed += this.DetachImageList;
				this.imageList.ChangeHandle += this.ImageListChangedHandle;
			}
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x001337BC File Offset: 0x001319BC
		private void DetachImageListHandlers()
		{
			if (this.imageList != null)
			{
				this.imageList.RecreateHandle -= this.ImageListRecreateHandle;
				this.imageList.Disposed -= this.DetachImageList;
				this.imageList.ChangeHandle -= this.ImageListChangedHandle;
			}
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00133818 File Offset: 0x00131A18
		private void AttachStateImageListHandlers()
		{
			if (this.stateImageList != null)
			{
				this.stateImageList.RecreateHandle += this.StateImageListRecreateHandle;
				this.stateImageList.Disposed += this.DetachStateImageList;
				this.stateImageList.ChangeHandle += this.StateImageListChangedHandle;
			}
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00133874 File Offset: 0x00131A74
		private void DetachStateImageListHandlers()
		{
			if (this.stateImageList != null)
			{
				this.stateImageList.RecreateHandle -= this.StateImageListRecreateHandle;
				this.stateImageList.Disposed -= this.DetachStateImageList;
				this.stateImageList.ChangeHandle -= this.StateImageListChangedHandle;
			}
		}

		/// <summary>Gets or sets the image list that is used to indicate the state of the <see cref="T:System.Windows.Forms.TreeView" /> and its nodes.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> used for indicating the state of the <see cref="T:System.Windows.Forms.TreeView" /> and its nodes.</returns>
		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x001338CE File Offset: 0x00131ACE
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x001338D8 File Offset: 0x00131AD8
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("TreeViewStateImageListDescr")]
		public ImageList StateImageList
		{
			get
			{
				return this.stateImageList;
			}
			set
			{
				if (value != this.stateImageList)
				{
					this.DetachStateImageListHandlers();
					this.stateImageList = value;
					this.AttachStateImageListHandlers();
					if (base.IsHandleCreated)
					{
						this.UpdateNativeStateImageList();
						this.UpdateCheckedState(this.root, true);
						if ((value == null || this.stateImageList.Images.Count == 0) && this.CheckBoxes)
						{
							base.RecreateHandle();
							return;
						}
						this.RefreshNodes();
					}
				}
			}
		}

		/// <summary>Gets or sets the distance to indent each child tree node level.</summary>
		/// <returns>The distance, in pixels, to indent each child tree node level. The default value is 19.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0.  
		///  -or-  
		///  The assigned value is greater than 32,000.</exception>
		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x0600491C RID: 18716 RVA: 0x00133946 File Offset: 0x00131B46
		// (set) Token: 0x0600491D RID: 18717 RVA: 0x00133978 File Offset: 0x00131B78
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewIndentDescr")]
		public int Indent
		{
			get
			{
				if (this.indent != -1)
				{
					return this.indent;
				}
				if (base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(4358, 0, 0);
				}
				return 19;
			}
			set
			{
				if (this.indent != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Indent", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Indent",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value > 32000)
					{
						throw new ArgumentOutOfRangeException("Indent", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"Indent",
							value.ToString(CultureInfo.CurrentCulture),
							32000.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.indent = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(4359, value, 0);
						this.indent = (int)(long)base.SendMessage(4358, 0, 0);
					}
				}
			}
		}

		/// <summary>Gets or sets the height of each tree node in the tree view control.</summary>
		/// <returns>The height, in pixels, of each tree node in the tree view.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than one.  
		///  -or-  
		///  The assigned value is greater than the <see cref="F:System.Int16.MaxValue" /> value.</exception>
		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x00133A60 File Offset: 0x00131C60
		// (set) Token: 0x0600491F RID: 18719 RVA: 0x00133AC4 File Offset: 0x00131CC4
		[SRCategory("CatAppearance")]
		[SRDescription("TreeViewItemHeightDescr")]
		public int ItemHeight
		{
			get
			{
				if (this.itemHeight != -1)
				{
					return this.itemHeight;
				}
				if (base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(4380, 0, 0);
				}
				if (this.CheckBoxes && this.DrawMode == TreeViewDrawMode.OwnerDrawAll)
				{
					return Math.Max(16, base.FontHeight + 3);
				}
				return base.FontHeight + 3;
			}
			set
			{
				if (this.itemHeight != value)
				{
					if (value < 1)
					{
						throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"ItemHeight",
							value.ToString(CultureInfo.CurrentCulture),
							1.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value >= 32767)
					{
						throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidHighBoundArgument", new object[]
						{
							"ItemHeight",
							value.ToString(CultureInfo.CurrentCulture),
							short.MaxValue.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.itemHeight = value;
					if (base.IsHandleCreated)
					{
						if (this.itemHeight % 2 != 0)
						{
							this.setOddHeight = true;
							try
							{
								base.RecreateHandle();
							}
							finally
							{
								this.setOddHeight = false;
							}
						}
						base.SendMessage(4379, value, 0);
						this.itemHeight = (int)(long)base.SendMessage(4380, 0, 0);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the label text of the tree nodes can be edited.</summary>
		/// <returns>
		///   <see langword="true" /> if the label text of the tree nodes can be edited; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x00133BDC File Offset: 0x00131DDC
		// (set) Token: 0x06004921 RID: 18721 RVA: 0x00133BEA File Offset: 0x00131DEA
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewLabelEditDescr")]
		public bool LabelEdit
		{
			get
			{
				return this.treeViewState[2];
			}
			set
			{
				if (this.LabelEdit != value)
				{
					this.treeViewState[2] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets the color of the lines connecting the nodes of the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the lines connecting the tree nodes.</returns>
		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x00133C10 File Offset: 0x00131E10
		// (set) Token: 0x06004923 RID: 18723 RVA: 0x00133C46 File Offset: 0x00131E46
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewLineColorDescr")]
		[DefaultValue(typeof(Color), "Black")]
		public Color LineColor
		{
			get
			{
				if (base.IsHandleCreated)
				{
					int num = (int)(long)base.SendMessage(4393, 0, 0);
					return ColorTranslator.FromWin32(num);
				}
				return this.lineColor;
			}
			set
			{
				if (this.lineColor != value)
				{
					this.lineColor = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(4392, 0, ColorTranslator.ToWin32(this.lineColor));
					}
				}
			}
		}

		/// <summary>Gets the collection of tree nodes that are assigned to the tree view control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNodeCollection" /> that represents the tree nodes assigned to the tree view control.</returns>
		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06004924 RID: 18724 RVA: 0x00133C7D File Offset: 0x00131E7D
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("TreeViewNodesDescr")]
		[MergableProperty(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this.root);
				}
				return this.nodes;
			}
		}

		/// <summary>Gets or sets the mode in which the control is drawn.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewDrawMode" /> values. The default is <see cref="F:System.Windows.Forms.TreeViewDrawMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TreeViewDrawMode" /> value.</exception>
		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x00133C9E File Offset: 0x00131E9E
		// (set) Token: 0x06004926 RID: 18726 RVA: 0x00133CA8 File Offset: 0x00131EA8
		[SRCategory("CatBehavior")]
		[DefaultValue(TreeViewDrawMode.Normal)]
		[SRDescription("TreeViewDrawModeDescr")]
		public TreeViewDrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TreeViewDrawMode));
				}
				if (this.drawMode != value)
				{
					this.drawMode = value;
					base.Invalidate();
					if (this.DrawMode == TreeViewDrawMode.OwnerDrawAll)
					{
						base.SetStyle(ControlStyles.ResizeRedraw, true);
					}
				}
			}
		}

		/// <summary>Gets or sets the delimiter string that the tree node path uses.</summary>
		/// <returns>The delimiter string that the tree node <see cref="P:System.Windows.Forms.TreeNode.FullPath" /> property uses. The default is the backslash character (\).</returns>
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x00133D03 File Offset: 0x00131F03
		// (set) Token: 0x06004928 RID: 18728 RVA: 0x00133D0B File Offset: 0x00131F0B
		[SRCategory("CatBehavior")]
		[DefaultValue("\\")]
		[SRDescription("TreeViewPathSeparatorDescr")]
		public string PathSeparator
		{
			get
			{
				return this.pathSeparator;
			}
			set
			{
				this.pathSeparator = value;
			}
		}

		/// <summary>Gets or sets the spacing between the <see cref="T:System.Windows.Forms.TreeView" /> control's contents and its edges.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> indicating the space between the control edges and its contents.</returns>
		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x0600492A RID: 18730 RVA: 0x0001344A File Offset: 0x0001164A
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TreeView.Padding" /> property changes.</summary>
		// Token: 0x140003A2 RID: 930
		// (add) Token: 0x0600492B RID: 18731 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x0600492C RID: 18732 RVA: 0x0001345C File Offset: 0x0001165C
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Windows.Forms.TreeView" /> should be laid out from right-to-left.</summary>
		/// <returns>
		///   <see langword="true" /> if the control should be laid out from right-to-left; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x00133D14 File Offset: 0x00131F14
		// (set) Token: 0x0600492E RID: 18734 RVA: 0x00133D1C File Offset: 0x00131F1C
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
					{
						this.OnRightToLeftLayoutChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree view control displays scroll bars when they are needed.</summary>
		/// <returns>
		///   <see langword="true" /> if the tree view control displays scroll bars when they are needed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x00133D70 File Offset: 0x00131F70
		// (set) Token: 0x06004930 RID: 18736 RVA: 0x00133D7E File Offset: 0x00131F7E
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewScrollableDescr")]
		public bool Scrollable
		{
			get
			{
				return this.treeViewState[4];
			}
			set
			{
				if (this.Scrollable != value)
				{
					this.treeViewState[4] = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets the image list index value of the image that is displayed when a tree node is selected.</summary>
		/// <returns>A zero-based index value that represents the position of an <see cref="T:System.Drawing.Image" /> in an <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <exception cref="T:System.ArgumentException">The index assigned value is less than zero.</exception>
		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x00133D9C File Offset: 0x00131F9C
		// (set) Token: 0x06004932 RID: 18738 RVA: 0x00133DF4 File Offset: 0x00131FF4
		[DefaultValue(-1)]
		[SRCategory("CatBehavior")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("TreeViewSelectedImageIndexDescr")]
		[RelatedImageList("ImageList")]
		public int SelectedImageIndex
		{
			get
			{
				if (this.imageList == null)
				{
					return -1;
				}
				if (this.SelectedImageIndexer.Index >= this.imageList.Images.Count)
				{
					return Math.Max(0, this.imageList.Images.Count - 1);
				}
				return this.SelectedImageIndexer.Index;
			}
			set
			{
				if (value == -1)
				{
					value = 0;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectedImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"SelectedImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.SelectedImageIndexer.Index != value)
				{
					this.SelectedImageIndexer.Index = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets the key of the default image shown when a <see cref="T:System.Windows.Forms.TreeNode" /> is in a selected state.</summary>
		/// <returns>The key of the default image shown when a <see cref="T:System.Windows.Forms.TreeNode" /> is in a selected state.</returns>
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x00133E77 File Offset: 0x00132077
		// (set) Token: 0x06004934 RID: 18740 RVA: 0x00133E84 File Offset: 0x00132084
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TreeViewSelectedImageKeyDescr")]
		[RelatedImageList("ImageList")]
		public string SelectedImageKey
		{
			get
			{
				return this.SelectedImageIndexer.Key;
			}
			set
			{
				if (this.SelectedImageIndexer.Key != value)
				{
					this.SelectedImageIndexer.Key = value;
					if (string.IsNullOrEmpty(value) || value.Equals(SR.GetString("toStringNone")))
					{
						this.SelectedImageIndex = ((this.ImageList != null) ? 0 : (-1));
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets the tree node that is currently selected in the tree view control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that is currently selected in the tree view control.</returns>
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x00133EEC File Offset: 0x001320EC
		// (set) Token: 0x06004936 RID: 18742 RVA: 0x00133F48 File Offset: 0x00132148
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewSelectedNodeDescr")]
		public TreeNode SelectedNode
		{
			get
			{
				if (base.IsHandleCreated)
				{
					IntPtr intPtr = base.SendMessage(4362, 9, 0);
					if (intPtr == IntPtr.Zero)
					{
						return null;
					}
					return this.NodeFromHandle(intPtr);
				}
				else
				{
					if (this.selectedNode != null && this.selectedNode.TreeView == this)
					{
						return this.selectedNode;
					}
					return null;
				}
			}
			set
			{
				if (base.IsHandleCreated && (value == null || value.TreeView == this))
				{
					IntPtr intPtr = ((value == null) ? IntPtr.Zero : value.Handle);
					base.SendMessage(4363, 9, intPtr);
					this.selectedNode = null;
					return;
				}
				this.selectedNode = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether lines are drawn between tree nodes in the tree view control.</summary>
		/// <returns>
		///   <see langword="true" /> if lines are drawn between tree nodes in the tree view control; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x00133F98 File Offset: 0x00132198
		// (set) Token: 0x06004938 RID: 18744 RVA: 0x00133FA7 File Offset: 0x001321A7
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewShowLinesDescr")]
		public bool ShowLines
		{
			get
			{
				return this.treeViewState[16];
			}
			set
			{
				if (this.ShowLines != value)
				{
					this.treeViewState[16] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating ToolTips are shown when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>
		///   <see langword="true" /> if ToolTips are shown when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x00133FCE File Offset: 0x001321CE
		// (set) Token: 0x0600493A RID: 18746 RVA: 0x00133FE0 File Offset: 0x001321E0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewShowShowNodeToolTipsDescr")]
		public bool ShowNodeToolTips
		{
			get
			{
				return this.treeViewState[1024];
			}
			set
			{
				if (this.ShowNodeToolTips != value)
				{
					this.treeViewState[1024] = value;
					if (this.ShowNodeToolTips)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are displayed next to tree nodes that contain child tree nodes.</summary>
		/// <returns>
		///   <see langword="true" /> if plus sign and minus sign buttons are displayed next to tree nodes that contain child tree nodes; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x0013400A File Offset: 0x0013220A
		// (set) Token: 0x0600493C RID: 18748 RVA: 0x00134019 File Offset: 0x00132219
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewShowPlusMinusDescr")]
		public bool ShowPlusMinus
		{
			get
			{
				return this.treeViewState[32];
			}
			set
			{
				if (this.ShowPlusMinus != value)
				{
					this.treeViewState[32] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether lines are drawn between the tree nodes that are at the root of the tree view.</summary>
		/// <returns>
		///   <see langword="true" /> if lines are drawn between the tree nodes that are at the root of the tree view; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x00134040 File Offset: 0x00132240
		// (set) Token: 0x0600493E RID: 18750 RVA: 0x0013404F File Offset: 0x0013224F
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewShowRootLinesDescr")]
		public bool ShowRootLines
		{
			get
			{
				return this.treeViewState[64];
			}
			set
			{
				if (this.ShowRootLines != value)
				{
					this.treeViewState[64] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree nodes in the tree view are sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the tree nodes in the tree view are sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x00134076 File Offset: 0x00132276
		// (set) Token: 0x06004940 RID: 18752 RVA: 0x00134088 File Offset: 0x00132288
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewSortedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Sorted
		{
			get
			{
				return this.treeViewState[128];
			}
			set
			{
				if (this.Sorted != value)
				{
					this.treeViewState[128] = value;
					if (this.Sorted && this.TreeViewNodeSorter == null && this.Nodes.Count >= 1)
					{
						this.RefreshNodes();
					}
				}
			}
		}

		/// <summary>Gets or sets the implementation of <see cref="T:System.Collections.IComparer" /> to perform a custom sort of the <see cref="T:System.Windows.Forms.TreeView" /> nodes.</summary>
		/// <returns>The <see cref="T:System.Collections.IComparer" /> to perform the custom sort.</returns>
		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x001340C8 File Offset: 0x001322C8
		// (set) Token: 0x06004942 RID: 18754 RVA: 0x001340D0 File Offset: 0x001322D0
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewNodeSorterDescr")]
		public IComparer TreeViewNodeSorter
		{
			get
			{
				return this.treeViewNodeSorter;
			}
			set
			{
				if (this.treeViewNodeSorter != value)
				{
					this.treeViewNodeSorter = value;
					if (value != null)
					{
						this.Sort();
					}
				}
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.TreeView" />.</summary>
		/// <returns>
		///   <see langword="Null" /> in all cases.</returns>
		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TreeView.Text" /> property changes.</summary>
		// Token: 0x140003A3 RID: 931
		// (add) Token: 0x06004945 RID: 18757 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06004946 RID: 18758 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Gets or sets the first fully-visible tree node in the tree view control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the first fully-visible tree node in the tree view control.</returns>
		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x001340EC File Offset: 0x001322EC
		// (set) Token: 0x06004948 RID: 18760 RVA: 0x0013412C File Offset: 0x0013232C
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewTopNodeDescr")]
		public TreeNode TopNode
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return this.topNode;
				}
				IntPtr intPtr = base.SendMessage(4362, 5, 0);
				if (!(intPtr == IntPtr.Zero))
				{
					return this.NodeFromHandle(intPtr);
				}
				return null;
			}
			set
			{
				if (base.IsHandleCreated && (value == null || value.TreeView == this))
				{
					IntPtr intPtr = ((value == null) ? IntPtr.Zero : value.Handle);
					base.SendMessage(4363, 5, intPtr);
					this.topNode = null;
					return;
				}
				this.topNode = value;
			}
		}

		/// <summary>Gets the number of tree nodes that can be fully visible in the tree view control.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.TreeNode" /> items that can be fully visible in the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x0013417B File Offset: 0x0013237B
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewVisibleCountDescr")]
		public int VisibleCount
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(4368, 0, 0);
				}
				return 0;
			}
		}

		/// <summary>Occurs before the tree node label text is edited.</summary>
		// Token: 0x140003A4 RID: 932
		// (add) Token: 0x0600494A RID: 18762 RVA: 0x0013419A File Offset: 0x0013239A
		// (remove) Token: 0x0600494B RID: 18763 RVA: 0x001341B3 File Offset: 0x001323B3
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeEditDescr")]
		public event NodeLabelEditEventHandler BeforeLabelEdit
		{
			add
			{
				this.onBeforeLabelEdit = (NodeLabelEditEventHandler)Delegate.Combine(this.onBeforeLabelEdit, value);
			}
			remove
			{
				this.onBeforeLabelEdit = (NodeLabelEditEventHandler)Delegate.Remove(this.onBeforeLabelEdit, value);
			}
		}

		/// <summary>Occurs after the tree node label text is edited.</summary>
		// Token: 0x140003A5 RID: 933
		// (add) Token: 0x0600494C RID: 18764 RVA: 0x001341CC File Offset: 0x001323CC
		// (remove) Token: 0x0600494D RID: 18765 RVA: 0x001341E5 File Offset: 0x001323E5
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterEditDescr")]
		public event NodeLabelEditEventHandler AfterLabelEdit
		{
			add
			{
				this.onAfterLabelEdit = (NodeLabelEditEventHandler)Delegate.Combine(this.onAfterLabelEdit, value);
			}
			remove
			{
				this.onAfterLabelEdit = (NodeLabelEditEventHandler)Delegate.Remove(this.onAfterLabelEdit, value);
			}
		}

		/// <summary>Occurs before the tree node check box is checked.</summary>
		// Token: 0x140003A6 RID: 934
		// (add) Token: 0x0600494E RID: 18766 RVA: 0x001341FE File Offset: 0x001323FE
		// (remove) Token: 0x0600494F RID: 18767 RVA: 0x00134217 File Offset: 0x00132417
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeCheckDescr")]
		public event TreeViewCancelEventHandler BeforeCheck
		{
			add
			{
				this.onBeforeCheck = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeCheck, value);
			}
			remove
			{
				this.onBeforeCheck = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeCheck, value);
			}
		}

		/// <summary>Occurs after the tree node check box is checked.</summary>
		// Token: 0x140003A7 RID: 935
		// (add) Token: 0x06004950 RID: 18768 RVA: 0x00134230 File Offset: 0x00132430
		// (remove) Token: 0x06004951 RID: 18769 RVA: 0x00134249 File Offset: 0x00132449
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterCheckDescr")]
		public event TreeViewEventHandler AfterCheck
		{
			add
			{
				this.onAfterCheck = (TreeViewEventHandler)Delegate.Combine(this.onAfterCheck, value);
			}
			remove
			{
				this.onAfterCheck = (TreeViewEventHandler)Delegate.Remove(this.onAfterCheck, value);
			}
		}

		/// <summary>Occurs before the tree node is collapsed.</summary>
		// Token: 0x140003A8 RID: 936
		// (add) Token: 0x06004952 RID: 18770 RVA: 0x00134262 File Offset: 0x00132462
		// (remove) Token: 0x06004953 RID: 18771 RVA: 0x0013427B File Offset: 0x0013247B
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeCollapseDescr")]
		public event TreeViewCancelEventHandler BeforeCollapse
		{
			add
			{
				this.onBeforeCollapse = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeCollapse, value);
			}
			remove
			{
				this.onBeforeCollapse = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeCollapse, value);
			}
		}

		/// <summary>Occurs after the tree node is collapsed.</summary>
		// Token: 0x140003A9 RID: 937
		// (add) Token: 0x06004954 RID: 18772 RVA: 0x00134294 File Offset: 0x00132494
		// (remove) Token: 0x06004955 RID: 18773 RVA: 0x001342AD File Offset: 0x001324AD
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterCollapseDescr")]
		public event TreeViewEventHandler AfterCollapse
		{
			add
			{
				this.onAfterCollapse = (TreeViewEventHandler)Delegate.Combine(this.onAfterCollapse, value);
			}
			remove
			{
				this.onAfterCollapse = (TreeViewEventHandler)Delegate.Remove(this.onAfterCollapse, value);
			}
		}

		/// <summary>Occurs before the tree node is expanded.</summary>
		// Token: 0x140003AA RID: 938
		// (add) Token: 0x06004956 RID: 18774 RVA: 0x001342C6 File Offset: 0x001324C6
		// (remove) Token: 0x06004957 RID: 18775 RVA: 0x001342DF File Offset: 0x001324DF
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeExpandDescr")]
		public event TreeViewCancelEventHandler BeforeExpand
		{
			add
			{
				this.onBeforeExpand = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeExpand, value);
			}
			remove
			{
				this.onBeforeExpand = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeExpand, value);
			}
		}

		/// <summary>Occurs after the tree node is expanded.</summary>
		// Token: 0x140003AB RID: 939
		// (add) Token: 0x06004958 RID: 18776 RVA: 0x001342F8 File Offset: 0x001324F8
		// (remove) Token: 0x06004959 RID: 18777 RVA: 0x00134311 File Offset: 0x00132511
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterExpandDescr")]
		public event TreeViewEventHandler AfterExpand
		{
			add
			{
				this.onAfterExpand = (TreeViewEventHandler)Delegate.Combine(this.onAfterExpand, value);
			}
			remove
			{
				this.onAfterExpand = (TreeViewEventHandler)Delegate.Remove(this.onAfterExpand, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.TreeView" /> is drawn and the <see cref="P:System.Windows.Forms.TreeView.DrawMode" /> property is set to a <see cref="T:System.Windows.Forms.TreeViewDrawMode" /> value other than <see cref="F:System.Windows.Forms.TreeViewDrawMode.Normal" />.</summary>
		// Token: 0x140003AC RID: 940
		// (add) Token: 0x0600495A RID: 18778 RVA: 0x0013432A File Offset: 0x0013252A
		// (remove) Token: 0x0600495B RID: 18779 RVA: 0x00134343 File Offset: 0x00132543
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewDrawNodeEventDescr")]
		public event DrawTreeNodeEventHandler DrawNode
		{
			add
			{
				this.onDrawNode = (DrawTreeNodeEventHandler)Delegate.Combine(this.onDrawNode, value);
			}
			remove
			{
				this.onDrawNode = (DrawTreeNodeEventHandler)Delegate.Remove(this.onDrawNode, value);
			}
		}

		/// <summary>Occurs when the user begins dragging a node.</summary>
		// Token: 0x140003AD RID: 941
		// (add) Token: 0x0600495C RID: 18780 RVA: 0x0013435C File Offset: 0x0013255C
		// (remove) Token: 0x0600495D RID: 18781 RVA: 0x00134375 File Offset: 0x00132575
		[SRCategory("CatAction")]
		[SRDescription("ListViewItemDragDescr")]
		public event ItemDragEventHandler ItemDrag
		{
			add
			{
				this.onItemDrag = (ItemDragEventHandler)Delegate.Combine(this.onItemDrag, value);
			}
			remove
			{
				this.onItemDrag = (ItemDragEventHandler)Delegate.Remove(this.onItemDrag, value);
			}
		}

		/// <summary>Occurs when the mouse hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x140003AE RID: 942
		// (add) Token: 0x0600495E RID: 18782 RVA: 0x0013438E File Offset: 0x0013258E
		// (remove) Token: 0x0600495F RID: 18783 RVA: 0x001343A7 File Offset: 0x001325A7
		[SRCategory("CatAction")]
		[SRDescription("TreeViewNodeMouseHoverDescr")]
		public event TreeNodeMouseHoverEventHandler NodeMouseHover
		{
			add
			{
				this.onNodeMouseHover = (TreeNodeMouseHoverEventHandler)Delegate.Combine(this.onNodeMouseHover, value);
			}
			remove
			{
				this.onNodeMouseHover = (TreeNodeMouseHoverEventHandler)Delegate.Remove(this.onNodeMouseHover, value);
			}
		}

		/// <summary>Occurs before the tree node is selected.</summary>
		// Token: 0x140003AF RID: 943
		// (add) Token: 0x06004960 RID: 18784 RVA: 0x001343C0 File Offset: 0x001325C0
		// (remove) Token: 0x06004961 RID: 18785 RVA: 0x001343D9 File Offset: 0x001325D9
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeSelectDescr")]
		public event TreeViewCancelEventHandler BeforeSelect
		{
			add
			{
				this.onBeforeSelect = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeSelect, value);
			}
			remove
			{
				this.onBeforeSelect = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeSelect, value);
			}
		}

		/// <summary>Occurs after the tree node is selected.</summary>
		// Token: 0x140003B0 RID: 944
		// (add) Token: 0x06004962 RID: 18786 RVA: 0x001343F2 File Offset: 0x001325F2
		// (remove) Token: 0x06004963 RID: 18787 RVA: 0x0013440B File Offset: 0x0013260B
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterSelectDescr")]
		public event TreeViewEventHandler AfterSelect
		{
			add
			{
				this.onAfterSelect = (TreeViewEventHandler)Delegate.Combine(this.onAfterSelect, value);
			}
			remove
			{
				this.onAfterSelect = (TreeViewEventHandler)Delegate.Remove(this.onAfterSelect, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.TreeView" /> is drawn.</summary>
		// Token: 0x140003B1 RID: 945
		// (add) Token: 0x06004964 RID: 18788 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06004965 RID: 18789 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		/// <summary>Occurs when the user clicks a <see cref="T:System.Windows.Forms.TreeNode" /> with the mouse.</summary>
		// Token: 0x140003B2 RID: 946
		// (add) Token: 0x06004966 RID: 18790 RVA: 0x00134424 File Offset: 0x00132624
		// (remove) Token: 0x06004967 RID: 18791 RVA: 0x0013443D File Offset: 0x0013263D
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewNodeMouseClickDescr")]
		public event TreeNodeMouseClickEventHandler NodeMouseClick
		{
			add
			{
				this.onNodeMouseClick = (TreeNodeMouseClickEventHandler)Delegate.Combine(this.onNodeMouseClick, value);
			}
			remove
			{
				this.onNodeMouseClick = (TreeNodeMouseClickEventHandler)Delegate.Remove(this.onNodeMouseClick, value);
			}
		}

		/// <summary>Occurs when the user double-clicks a <see cref="T:System.Windows.Forms.TreeNode" /> with the mouse.</summary>
		// Token: 0x140003B3 RID: 947
		// (add) Token: 0x06004968 RID: 18792 RVA: 0x00134456 File Offset: 0x00132656
		// (remove) Token: 0x06004969 RID: 18793 RVA: 0x0013446F File Offset: 0x0013266F
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewNodeMouseDoubleClickDescr")]
		public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick
		{
			add
			{
				this.onNodeMouseDoubleClick = (TreeNodeMouseClickEventHandler)Delegate.Combine(this.onNodeMouseDoubleClick, value);
			}
			remove
			{
				this.onNodeMouseDoubleClick = (TreeNodeMouseClickEventHandler)Delegate.Remove(this.onNodeMouseDoubleClick, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TreeView.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x140003B4 RID: 948
		// (add) Token: 0x0600496A RID: 18794 RVA: 0x00134488 File Offset: 0x00132688
		// (remove) Token: 0x0600496B RID: 18795 RVA: 0x001344A1 File Offset: 0x001326A1
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Combine(this.onRightToLeftLayoutChanged, value);
			}
			remove
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Remove(this.onRightToLeftLayoutChanged, value);
			}
		}

		/// <summary>Disables any redrawing of the tree view.</summary>
		// Token: 0x0600496C RID: 18796 RVA: 0x00103F69 File Offset: 0x00102169
		public void BeginUpdate()
		{
			base.BeginUpdateInternal();
		}

		/// <summary>Collapses all the tree nodes.</summary>
		// Token: 0x0600496D RID: 18797 RVA: 0x001344BA File Offset: 0x001326BA
		public void CollapseAll()
		{
			this.root.Collapse();
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x0600496E RID: 18798 RVA: 0x001344C8 File Offset: 0x001326C8
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 2
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x00134518 File Offset: 0x00132718
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x00134521 File Offset: 0x00132721
		private void DetachStateImageList(object sender, EventArgs e)
		{
			this.internalStateImageList = null;
			this.StateImageList = null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.TreeView" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004971 RID: 18801 RVA: 0x00134534 File Offset: 0x00132734
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (object obj in this.Nodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					treeNode.ContextMenu = null;
				}
				lock (this)
				{
					this.DetachImageListHandlers();
					this.imageList = null;
					this.DetachStateImageListHandlers();
					this.stateImageList = null;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Enables the redrawing of the tree view.</summary>
		// Token: 0x06004972 RID: 18802 RVA: 0x00109D04 File Offset: 0x00107F04
		public void EndUpdate()
		{
			base.EndUpdateInternal();
		}

		/// <summary>Expands all the tree nodes.</summary>
		// Token: 0x06004973 RID: 18803 RVA: 0x001345D8 File Offset: 0x001327D8
		public void ExpandAll()
		{
			this.root.ExpandAll();
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x001345E8 File Offset: 0x001327E8
		internal void ForceScrollbarUpdate(bool delayed)
		{
			if (!base.IsUpdating() && base.IsHandleCreated)
			{
				base.SendMessage(11, 0, 0);
				if (delayed)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 11, (IntPtr)1, IntPtr.Zero);
					return;
				}
				base.SendMessage(11, 1, 0);
			}
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x00134640 File Offset: 0x00132840
		internal void SetToolTip(ToolTip toolTip, string toolTipText)
		{
			if (toolTip != null)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(toolTip, toolTip.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4376, new HandleRef(toolTip, toolTip.Handle), 0);
				this.controlToolTipText = toolTipText;
			}
		}

		/// <summary>Provides node information, given a point.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> at which to retrieve node information.</param>
		/// <returns>The node information.</returns>
		// Token: 0x06004976 RID: 18806 RVA: 0x001346A0 File Offset: 0x001328A0
		public TreeViewHitTestInfo HitTest(Point pt)
		{
			return this.HitTest(pt.X, pt.Y);
		}

		/// <summary>Provides node information, given x- and y-coordinates.</summary>
		/// <param name="x">The x-coordinate at which to retrieve node information</param>
		/// <param name="y">The y-coordinate at which to retrieve node information.</param>
		/// <returns>The node information.</returns>
		// Token: 0x06004977 RID: 18807 RVA: 0x001346B8 File Offset: 0x001328B8
		public TreeViewHitTestInfo HitTest(int x, int y)
		{
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			tv_HITTESTINFO.pt_x = x;
			tv_HITTESTINFO.pt_y = y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			TreeNode treeNode = ((intPtr == IntPtr.Zero) ? null : this.NodeFromHandle(intPtr));
			TreeViewHitTestLocations flags = (TreeViewHitTestLocations)tv_HITTESTINFO.flags;
			return new TreeViewHitTestInfo(treeNode, flags);
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x00134718 File Offset: 0x00132918
		internal bool TreeViewBeforeCheck(TreeNode node, TreeViewAction actionTaken)
		{
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(node, false, actionTaken);
			this.OnBeforeCheck(treeViewCancelEventArgs);
			return treeViewCancelEventArgs.Cancel;
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x0013473B File Offset: 0x0013293B
		internal void TreeViewAfterCheck(TreeNode node, TreeViewAction actionTaken)
		{
			this.OnAfterCheck(new TreeViewEventArgs(node, actionTaken));
		}

		/// <summary>Retrieves the number of tree nodes, optionally including those in all subtrees, assigned to the tree view control.</summary>
		/// <param name="includeSubTrees">
		///   <see langword="true" /> to count the <see cref="T:System.Windows.Forms.TreeNode" /> items that the subtrees contain; otherwise, <see langword="false" />.</param>
		/// <returns>The number of tree nodes, optionally including those in all subtrees, assigned to the tree view control.</returns>
		// Token: 0x0600497A RID: 18810 RVA: 0x0013474A File Offset: 0x0013294A
		public int GetNodeCount(bool includeSubTrees)
		{
			return this.root.GetNodeCount(includeSubTrees);
		}

		/// <summary>Retrieves the tree node that is at the specified point.</summary>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to evaluate and retrieve the node from.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified point, in tree view (client) coordinates, or <see langword="null" /> if there is no node at that location.</returns>
		// Token: 0x0600497B RID: 18811 RVA: 0x00134758 File Offset: 0x00132958
		public TreeNode GetNodeAt(Point pt)
		{
			return this.GetNodeAt(pt.X, pt.Y);
		}

		/// <summary>Retrieves the tree node at the point with the specified coordinates.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> position to evaluate and retrieve the node from.</param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> position to evaluate and retrieve the node from.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified location, in tree view (client) coordinates, or <see langword="null" /> if there is no node at that location.</returns>
		// Token: 0x0600497C RID: 18812 RVA: 0x00134770 File Offset: 0x00132970
		public TreeNode GetNodeAt(int x, int y)
		{
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			tv_HITTESTINFO.pt_x = x;
			tv_HITTESTINFO.pt_y = y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (!(intPtr == IntPtr.Zero))
			{
				return this.NodeFromHandle(intPtr);
			}
			return null;
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x001347C0 File Offset: 0x001329C0
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr intPtr = ((this.ImageList == null) ? IntPtr.Zero : this.ImageList.Handle);
				base.SendMessage(4361, 0, intPtr);
			}
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00134800 File Offset: 0x00132A00
		private void UpdateImagesRecursive(TreeNode node)
		{
			node.UpdateImage();
			foreach (object obj in node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.UpdateImagesRecursive(treeNode);
			}
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x00134860 File Offset: 0x00132A60
		private void ImageListChangedHandle(object sender, EventArgs e)
		{
			if (sender != null && sender == this.imageList && base.IsHandleCreated)
			{
				this.BeginUpdate();
				foreach (object obj in this.Nodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					this.UpdateImagesRecursive(treeNode);
				}
				this.EndUpdate();
			}
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x001348DC File Offset: 0x00132ADC
		private void StateImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.internalStateImageList != null)
				{
					intPtr = this.internalStateImageList.Handle;
				}
				this.SetStateImageList(intPtr);
			}
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x00134914 File Offset: 0x00132B14
		private void StateImageListChangedHandle(object sender, EventArgs e)
		{
			if (sender != null && sender == this.stateImageList && base.IsHandleCreated)
			{
				if (this.stateImageList != null && this.stateImageList.Images.Count > 0)
				{
					Image[] array = new Image[this.stateImageList.Images.Count + 1];
					array[0] = this.stateImageList.Images[0];
					for (int i = 1; i <= this.stateImageList.Images.Count; i++)
					{
						array[i] = this.stateImageList.Images[i - 1];
					}
					if (this.internalStateImageList != null)
					{
						this.internalStateImageList.Images.Clear();
						this.internalStateImageList.Images.AddRange(array);
					}
					else
					{
						this.internalStateImageList = new ImageList();
						this.internalStateImageList.Images.AddRange(array);
					}
					if (this.internalStateImageList != null)
					{
						if (TreeView.ScaledStateImageSize != null)
						{
							this.internalStateImageList.ImageSize = TreeView.ScaledStateImageSize.Value;
						}
						this.SetStateImageList(this.internalStateImageList.Handle);
						return;
					}
				}
				else
				{
					this.UpdateCheckedState(this.root, true);
				}
			}
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the Keys values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004982 RID: 18818 RVA: 0x00134A54 File Offset: 0x00132C54
		protected override bool IsInputKey(Keys keyData)
		{
			if (this.editNode != null && (keyData & Keys.Alt) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Return || keys == Keys.Escape || keys - Keys.Prior <= 3)
				{
					return true;
				}
			}
			return base.IsInputKey(keyData);
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x00134A94 File Offset: 0x00132C94
		internal TreeNode NodeFromHandle(IntPtr handle)
		{
			return (TreeNode)this.nodeTable[handle];
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.DrawNode" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs" /> that contains the event data.</param>
		// Token: 0x06004984 RID: 18820 RVA: 0x00134AB9 File Offset: 0x00132CB9
		protected virtual void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			if (this.onDrawNode != null)
			{
				this.onDrawNode(this, e);
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004985 RID: 18821 RVA: 0x00134AD0 File Offset: 0x00132CD0
		protected override void OnHandleCreated(EventArgs e)
		{
			TreeNode treeNode = this.selectedNode;
			this.selectedNode = null;
			base.OnHandleCreated(e);
			int num = (int)(long)base.SendMessage(8200, 0, 0);
			if (num < 5)
			{
				base.SendMessage(8199, 5, 0);
			}
			if (this.CheckBoxes)
			{
				int num2 = (int)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16);
				num2 |= 256;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -16, new HandleRef(null, (IntPtr)num2));
			}
			if (this.ShowNodeToolTips && !base.DesignMode)
			{
				int num3 = (int)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16);
				num3 |= 2048;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -16, new HandleRef(null, (IntPtr)num3));
			}
			Color color = this.BackColor;
			if (color != SystemColors.Window)
			{
				base.SendMessage(4381, 0, ColorTranslator.ToWin32(color));
			}
			color = this.ForeColor;
			if (color != SystemColors.WindowText)
			{
				base.SendMessage(4382, 0, ColorTranslator.ToWin32(color));
			}
			if (this.lineColor != Color.Empty)
			{
				base.SendMessage(4392, 0, ColorTranslator.ToWin32(this.lineColor));
			}
			if (this.imageList != null)
			{
				base.SendMessage(4361, 0, this.imageList.Handle);
			}
			if (this.stateImageList != null)
			{
				this.UpdateNativeStateImageList();
			}
			if (this.indent != -1)
			{
				base.SendMessage(4359, this.indent, 0);
			}
			if (this.itemHeight != -1)
			{
				base.SendMessage(4379, this.ItemHeight, 0);
			}
			try
			{
				this.treeViewState[32768] = true;
				int width = base.Width;
				int num4 = 22;
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef, base.Left, base.Top, int.MaxValue, base.Height, num4);
				this.root.Realize(false);
				if (width != 0)
				{
					SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef, base.Left, base.Top, width, base.Height, num4);
				}
			}
			finally
			{
				this.treeViewState[32768] = false;
			}
			this.SelectedNode = treeNode;
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x00134D4C File Offset: 0x00132F4C
		private void UpdateNativeStateImageList()
		{
			if (this.stateImageList != null && this.stateImageList.Images.Count > 0)
			{
				ImageList imageList = new ImageList();
				if (TreeView.ScaledStateImageSize != null)
				{
					imageList.ImageSize = TreeView.ScaledStateImageSize.Value;
				}
				Image[] array = new Image[this.stateImageList.Images.Count + 1];
				array[0] = this.stateImageList.Images[0];
				for (int i = 1; i <= this.stateImageList.Images.Count; i++)
				{
					array[i] = this.stateImageList.Images[i - 1];
				}
				imageList.Images.AddRange(array);
				base.SendMessage(4361, 2, imageList.Handle);
				if (this.internalStateImageList != null)
				{
					this.internalStateImageList.Dispose();
				}
				this.internalStateImageList = imageList;
			}
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x00134E38 File Offset: 0x00133038
		private void SetStateImageList(IntPtr handle)
		{
			IntPtr intPtr = base.SendMessage(4361, 2, handle);
			if (intPtr != IntPtr.Zero && intPtr != handle)
			{
				SafeNativeMethods.ImageList_Destroy_Native(new HandleRef(this, intPtr));
			}
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x00134E78 File Offset: 0x00133078
		private void DestroyNativeStateImageList(bool reset)
		{
			IntPtr intPtr = base.SendMessage(4360, 2, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				SafeNativeMethods.ImageList_Destroy_Native(new HandleRef(this, intPtr));
				if (reset)
				{
					base.SendMessage(4361, 2, IntPtr.Zero);
				}
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004989 RID: 18825 RVA: 0x00134EC6 File Offset: 0x001330C6
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.selectedNode = this.SelectedNode;
			this.DestroyNativeStateImageList(true);
			if (this.internalStateImageList != null)
			{
				this.internalStateImageList.Dispose();
				this.internalStateImageList = null;
			}
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600498A RID: 18826 RVA: 0x00134EFC File Offset: 0x001330FC
		protected override void OnMouseLeave(EventArgs e)
		{
			this.hoveredAlready = false;
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600498B RID: 18827 RVA: 0x00134F0C File Offset: 0x0013310C
		protected override void OnMouseHover(EventArgs e)
		{
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point point = Cursor.Position;
			point = base.PointToClientInternal(point);
			tv_HITTESTINFO.pt_x = point.X;
			tv_HITTESTINFO.pt_y = point.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (intPtr != IntPtr.Zero && (tv_HITTESTINFO.flags & 70) != 0)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (treeNode != this.prevHoveredNode && treeNode != null)
				{
					this.OnNodeMouseHover(new TreeNodeMouseHoverEventArgs(treeNode));
					this.prevHoveredNode = treeNode;
				}
			}
			if (!this.hoveredAlready)
			{
				base.OnMouseHover(e);
				this.hoveredAlready = true;
			}
			base.ResetMouseEventArgs();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data.</param>
		// Token: 0x0600498C RID: 18828 RVA: 0x00134FBB File Offset: 0x001331BB
		protected virtual void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
		{
			if (this.onBeforeLabelEdit != null)
			{
				this.onBeforeLabelEdit(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> that contains the event data.</param>
		// Token: 0x0600498D RID: 18829 RVA: 0x00134FD2 File Offset: 0x001331D2
		protected virtual void OnAfterLabelEdit(NodeLabelEditEventArgs e)
		{
			if (this.onAfterLabelEdit != null)
			{
				this.onAfterLabelEdit(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data.</param>
		// Token: 0x0600498E RID: 18830 RVA: 0x00134FE9 File Offset: 0x001331E9
		protected virtual void OnBeforeCheck(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeCheck != null)
			{
				this.onBeforeCheck(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCheck" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
		// Token: 0x0600498F RID: 18831 RVA: 0x00135000 File Offset: 0x00133200
		protected virtual void OnAfterCheck(TreeViewEventArgs e)
		{
			if (this.onAfterCheck != null)
			{
				this.onAfterCheck(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06004990 RID: 18832 RVA: 0x00135017 File Offset: 0x00133217
		protected internal virtual void OnBeforeCollapse(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeCollapse != null)
			{
				this.onBeforeCollapse(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCollapse" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
		// Token: 0x06004991 RID: 18833 RVA: 0x0013502E File Offset: 0x0013322E
		protected internal virtual void OnAfterCollapse(TreeViewEventArgs e)
		{
			if (this.onAfterCollapse != null)
			{
				this.onAfterCollapse(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06004992 RID: 18834 RVA: 0x00135045 File Offset: 0x00133245
		protected virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeExpand != null)
			{
				this.onBeforeExpand(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterExpand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
		// Token: 0x06004993 RID: 18835 RVA: 0x0013505C File Offset: 0x0013325C
		protected virtual void OnAfterExpand(TreeViewEventArgs e)
		{
			if (this.onAfterExpand != null)
			{
				this.onAfterExpand(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.ItemDrag" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> that contains the event data.</param>
		// Token: 0x06004994 RID: 18836 RVA: 0x00135073 File Offset: 0x00133273
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			if (this.onItemDrag != null)
			{
				this.onItemDrag(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseHover" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.TreeNodeMouseHoverEventArgs" /> that contains the event data.</param>
		// Token: 0x06004995 RID: 18837 RVA: 0x0013508A File Offset: 0x0013328A
		protected virtual void OnNodeMouseHover(TreeNodeMouseHoverEventArgs e)
		{
			if (this.onNodeMouseHover != null)
			{
				this.onNodeMouseHover(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06004996 RID: 18838 RVA: 0x001350A1 File Offset: 0x001332A1
		protected virtual void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeSelect != null)
			{
				this.onBeforeSelect(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
		// Token: 0x06004997 RID: 18839 RVA: 0x001350B8 File Offset: 0x001332B8
		protected virtual void OnAfterSelect(TreeViewEventArgs e)
		{
			if (this.onAfterSelect != null)
			{
				this.onAfterSelect(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data.</param>
		// Token: 0x06004998 RID: 18840 RVA: 0x001350CF File Offset: 0x001332CF
		protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			if (this.onNodeMouseClick != null)
			{
				this.onNodeMouseClick(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> that contains the event data.</param>
		// Token: 0x06004999 RID: 18841 RVA: 0x001350E6 File Offset: 0x001332E6
		protected virtual void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
		{
			if (this.onNodeMouseDoubleClick != null)
			{
				this.onNodeMouseDoubleClick(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x0600499A RID: 18842 RVA: 0x00135100 File Offset: 0x00133300
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Handled)
			{
				return;
			}
			if (this.CheckBoxes && (e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				TreeNode treeNode = this.SelectedNode;
				if (treeNode != null)
				{
					if (!this.TreeViewBeforeCheck(treeNode, TreeViewAction.ByKeyboard))
					{
						treeNode.CheckedInternal = !treeNode.CheckedInternal;
						this.TreeViewAfterCheck(treeNode, TreeViewAction.ByKeyboard);
					}
					e.Handled = true;
					return;
				}
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnKeyUp(System.Windows.Forms.KeyEventArgs)" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x0600499B RID: 18843 RVA: 0x0013516A File Offset: 0x0013336A
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.Handled)
			{
				return;
			}
			if ((e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				e.Handled = true;
				return;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x0600499C RID: 18844 RVA: 0x00135194 File Offset: 0x00133394
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (e.Handled)
			{
				return;
			}
			if (e.KeyChar == ' ')
			{
				e.Handled = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TreeView.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600499D RID: 18845 RVA: 0x001351B7 File Offset: 0x001333B7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				base.RecreateHandle();
			}
			if (this.onRightToLeftLayoutChanged != null)
			{
				this.onRightToLeftLayoutChanged(this, e);
			}
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x001351E8 File Offset: 0x001333E8
		private void RefreshNodes()
		{
			TreeNode[] array = new TreeNode[this.Nodes.Count];
			this.Nodes.CopyTo(array, 0);
			this.Nodes.Clear();
			this.Nodes.AddRange(array);
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x0013522A File Offset: 0x0013342A
		private void ResetIndent()
		{
			this.indent = -1;
			base.RecreateHandle();
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x00135239 File Offset: 0x00133439
		private void ResetItemHeight()
		{
			this.itemHeight = -1;
			base.RecreateHandle();
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x00135248 File Offset: 0x00133448
		private bool ShouldSerializeIndent()
		{
			return this.indent != -1;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x00135256 File Offset: 0x00133456
		private bool ShouldSerializeItemHeight()
		{
			return this.itemHeight != -1;
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x00135264 File Offset: 0x00133464
		private bool ShouldSerializeSelectedImageIndex()
		{
			if (this.imageList != null)
			{
				return this.SelectedImageIndex != 0;
			}
			return this.SelectedImageIndex != -1;
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x00135284 File Offset: 0x00133484
		private bool ShouldSerializeImageIndex()
		{
			if (this.imageList != null)
			{
				return this.ImageIndex != 0;
			}
			return this.ImageIndex != -1;
		}

		/// <summary>Sorts the items in <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x060049A5 RID: 18853 RVA: 0x001352A4 File Offset: 0x001334A4
		public void Sort()
		{
			this.Sorted = true;
			this.RefreshNodes();
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x060049A6 RID: 18854 RVA: 0x001352B4 File Offset: 0x001334B4
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Nodes != null)
			{
				text = text + ", Nodes.Count: " + this.Nodes.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Nodes.Count > 0)
				{
					text = text + ", Nodes[0]: " + this.Nodes[0].ToString();
				}
			}
			return text;
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x00135320 File Offset: 0x00133520
		private unsafe void TvnBeginDrag(MouseButtons buttons, NativeMethods.NMTREEVIEW* nmtv)
		{
			NativeMethods.TV_ITEM itemNew = nmtv->itemNew;
			if (itemNew.hItem == IntPtr.Zero)
			{
				return;
			}
			TreeNode treeNode = this.NodeFromHandle(itemNew.hItem);
			this.OnItemDrag(new ItemDragEventArgs(buttons, treeNode));
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x00135364 File Offset: 0x00133564
		private unsafe IntPtr TvnExpanding(NativeMethods.NMTREEVIEW* nmtv)
		{
			NativeMethods.TV_ITEM itemNew = nmtv->itemNew;
			if (itemNew.hItem == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			TreeViewCancelEventArgs treeViewCancelEventArgs;
			if ((itemNew.state & 32) == 0)
			{
				treeViewCancelEventArgs = new TreeViewCancelEventArgs(this.NodeFromHandle(itemNew.hItem), false, TreeViewAction.Expand);
				this.OnBeforeExpand(treeViewCancelEventArgs);
			}
			else
			{
				treeViewCancelEventArgs = new TreeViewCancelEventArgs(this.NodeFromHandle(itemNew.hItem), false, TreeViewAction.Collapse);
				this.OnBeforeCollapse(treeViewCancelEventArgs);
			}
			return (IntPtr)(treeViewCancelEventArgs.Cancel ? 1 : 0);
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x001353E8 File Offset: 0x001335E8
		private unsafe void TvnExpanded(NativeMethods.NMTREEVIEW* nmtv)
		{
			NativeMethods.TV_ITEM itemNew = nmtv->itemNew;
			if (itemNew.hItem == IntPtr.Zero)
			{
				return;
			}
			TreeNode treeNode = this.NodeFromHandle(itemNew.hItem);
			TreeViewEventArgs treeViewEventArgs;
			if ((itemNew.state & 32) == 0)
			{
				treeViewEventArgs = new TreeViewEventArgs(treeNode, TreeViewAction.Collapse);
				this.OnAfterCollapse(treeViewEventArgs);
				return;
			}
			treeViewEventArgs = new TreeViewEventArgs(treeNode, TreeViewAction.Expand);
			this.OnAfterExpand(treeViewEventArgs);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x00135448 File Offset: 0x00133648
		private unsafe IntPtr TvnSelecting(NativeMethods.NMTREEVIEW* nmtv)
		{
			if (this.treeViewState[65536])
			{
				return (IntPtr)1;
			}
			if (nmtv->itemNew.hItem == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			TreeNode treeNode = this.NodeFromHandle(nmtv->itemNew.hItem);
			TreeViewAction treeViewAction = TreeViewAction.Unknown;
			int action = nmtv->action;
			if (action != 1)
			{
				if (action == 2)
				{
					treeViewAction = TreeViewAction.ByKeyboard;
				}
			}
			else
			{
				treeViewAction = TreeViewAction.ByMouse;
			}
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(treeNode, false, treeViewAction);
			this.OnBeforeSelect(treeViewCancelEventArgs);
			return (IntPtr)(treeViewCancelEventArgs.Cancel ? 1 : 0);
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x001354D8 File Offset: 0x001336D8
		private unsafe void TvnSelected(NativeMethods.NMTREEVIEW* nmtv)
		{
			if (this.nodesCollectionClear)
			{
				return;
			}
			if (nmtv->itemNew.hItem != IntPtr.Zero)
			{
				TreeViewAction treeViewAction = TreeViewAction.Unknown;
				int action = nmtv->action;
				if (action != 1)
				{
					if (action == 2)
					{
						treeViewAction = TreeViewAction.ByKeyboard;
					}
				}
				else
				{
					treeViewAction = TreeViewAction.ByMouse;
				}
				this.OnAfterSelect(new TreeViewEventArgs(this.NodeFromHandle(nmtv->itemNew.hItem), treeViewAction));
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			*(IntPtr*)(&rect.left) = nmtv->itemOld.hItem;
			if (nmtv->itemOld.hItem != IntPtr.Zero && (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4356, 1, ref rect) != 0)
			{
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), ref rect, true);
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x001355A4 File Offset: 0x001337A4
		private IntPtr TvnBeginLabelEdit(NativeMethods.NMTVDISPINFO nmtvdi)
		{
			if (nmtvdi.item.hItem == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			TreeNode treeNode = this.NodeFromHandle(nmtvdi.item.hItem);
			NodeLabelEditEventArgs nodeLabelEditEventArgs = new NodeLabelEditEventArgs(treeNode);
			this.OnBeforeLabelEdit(nodeLabelEditEventArgs);
			if (!nodeLabelEditEventArgs.CancelEdit)
			{
				this.editNode = treeNode;
			}
			return (IntPtr)(nodeLabelEditEventArgs.CancelEdit ? 1 : 0);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x00135610 File Offset: 0x00133810
		private IntPtr TvnEndLabelEdit(NativeMethods.NMTVDISPINFO nmtvdi)
		{
			this.editNode = null;
			if (nmtvdi.item.hItem == IntPtr.Zero)
			{
				return (IntPtr)1;
			}
			TreeNode treeNode = this.NodeFromHandle(nmtvdi.item.hItem);
			string text = ((nmtvdi.item.pszText == IntPtr.Zero) ? null : Marshal.PtrToStringAuto(nmtvdi.item.pszText));
			NodeLabelEditEventArgs nodeLabelEditEventArgs = new NodeLabelEditEventArgs(treeNode, text);
			this.OnAfterLabelEdit(nodeLabelEditEventArgs);
			if (text != null && !nodeLabelEditEventArgs.CancelEdit && treeNode != null)
			{
				treeNode.text = text;
				if (this.Scrollable)
				{
					this.ForceScrollbarUpdate(true);
				}
			}
			return (IntPtr)(nodeLabelEditEventArgs.CancelEdit ? 0 : 1);
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x001356C3 File Offset: 0x001338C3
		internal override void UpdateStylesCore()
		{
			base.UpdateStylesCore();
			if (base.IsHandleCreated && this.CheckBoxes && this.StateImageList != null && this.internalStateImageList != null)
			{
				this.SetStateImageList(this.internalStateImageList.Handle);
			}
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x001356FC File Offset: 0x001338FC
		private void UpdateCheckedState(TreeNode node, bool update)
		{
			if (update)
			{
				node.CheckedInternal = node.CheckedInternal;
				for (int i = node.Nodes.Count - 1; i >= 0; i--)
				{
					this.UpdateCheckedState(node.Nodes[i], update);
				}
				return;
			}
			node.CheckedInternal = false;
			for (int j = node.Nodes.Count - 1; j >= 0; j--)
			{
				this.UpdateCheckedState(node.Nodes[j], update);
			}
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x00135778 File Offset: 0x00133978
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			base.SendMessage(4363, 8, null);
			this.OnMouseDown(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
			if (!base.ValidationCancelled)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x001357C8 File Offset: 0x001339C8
		private void CustomDraw(ref Message m)
		{
			NativeMethods.NMTVCUSTOMDRAW nmtvcustomdraw = (NativeMethods.NMTVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMTVCUSTOMDRAW));
			int dwDrawStage = nmtvcustomdraw.nmcd.dwDrawStage;
			if (dwDrawStage != 1)
			{
				if (dwDrawStage != 65537)
				{
					if (dwDrawStage == 65538)
					{
						if (this.drawMode == TreeViewDrawMode.OwnerDrawText)
						{
							TreeNode treeNode = this.NodeFromHandle(nmtvcustomdraw.nmcd.dwItemSpec);
							if (treeNode == null)
							{
								return;
							}
							Graphics graphics = Graphics.FromHdcInternal(nmtvcustomdraw.nmcd.hdc);
							try
							{
								Rectangle bounds = treeNode.Bounds;
								Size size = TextRenderer.MeasureText(treeNode.Text, treeNode.TreeView.Font);
								Point point = new Point(bounds.X - 1, bounds.Y);
								bounds = new Rectangle(point, new Size(size.Width, bounds.Height));
								DrawTreeNodeEventArgs drawTreeNodeEventArgs = new DrawTreeNodeEventArgs(graphics, treeNode, bounds, (TreeNodeStates)nmtvcustomdraw.nmcd.uItemState);
								this.OnDrawNode(drawTreeNodeEventArgs);
								if (drawTreeNodeEventArgs.DrawDefault)
								{
									TreeNodeStates state = drawTreeNodeEventArgs.State;
									Font font = ((treeNode.NodeFont != null) ? treeNode.NodeFont : treeNode.TreeView.Font);
									Color color = (((state & TreeNodeStates.Selected) == TreeNodeStates.Selected && treeNode.TreeView.Focused) ? SystemColors.HighlightText : ((treeNode.ForeColor != Color.Empty) ? treeNode.ForeColor : treeNode.TreeView.ForeColor));
									if ((state & TreeNodeStates.Selected) == TreeNodeStates.Selected)
									{
										graphics.FillRectangle(SystemBrushes.Highlight, bounds);
										ControlPaint.DrawFocusRectangle(graphics, bounds, color, SystemColors.Highlight);
										TextRenderer.DrawText(graphics, drawTreeNodeEventArgs.Node.Text, font, bounds, color, TextFormatFlags.Default);
									}
									else
									{
										using (Brush brush = new SolidBrush(this.BackColor))
										{
											graphics.FillRectangle(brush, bounds);
										}
										TextRenderer.DrawText(graphics, drawTreeNodeEventArgs.Node.Text, font, bounds, color, TextFormatFlags.Default);
									}
								}
							}
							finally
							{
								graphics.Dispose();
							}
							m.Result = (IntPtr)32;
							return;
						}
					}
				}
				else
				{
					TreeNode treeNode = this.NodeFromHandle(nmtvcustomdraw.nmcd.dwItemSpec);
					if (treeNode == null)
					{
						m.Result = (IntPtr)4;
						return;
					}
					int uItemState = nmtvcustomdraw.nmcd.uItemState;
					if (this.drawMode == TreeViewDrawMode.OwnerDrawText)
					{
						nmtvcustomdraw.clrText = nmtvcustomdraw.clrTextBk;
						Marshal.StructureToPtr(nmtvcustomdraw, m.LParam, false);
						m.Result = (IntPtr)18;
						return;
					}
					if (this.drawMode == TreeViewDrawMode.OwnerDrawAll)
					{
						Graphics graphics2 = Graphics.FromHdcInternal(nmtvcustomdraw.nmcd.hdc);
						DrawTreeNodeEventArgs drawTreeNodeEventArgs2;
						try
						{
							Rectangle rowBounds = treeNode.RowBounds;
							NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
							scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
							scrollinfo.fMask = 4;
							if (UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 0, scrollinfo))
							{
								int nPos = scrollinfo.nPos;
								if (nPos > 0)
								{
									rowBounds.X -= nPos;
									rowBounds.Width += nPos;
								}
							}
							drawTreeNodeEventArgs2 = new DrawTreeNodeEventArgs(graphics2, treeNode, rowBounds, (TreeNodeStates)uItemState);
							this.OnDrawNode(drawTreeNodeEventArgs2);
						}
						finally
						{
							graphics2.Dispose();
						}
						if (!drawTreeNodeEventArgs2.DrawDefault)
						{
							m.Result = (IntPtr)4;
							return;
						}
					}
					OwnerDrawPropertyBag itemRenderStyles = this.GetItemRenderStyles(treeNode, uItemState);
					bool flag = false;
					Color foreColor = itemRenderStyles.ForeColor;
					Color backColor = itemRenderStyles.BackColor;
					if (itemRenderStyles != null && !foreColor.IsEmpty)
					{
						nmtvcustomdraw.clrText = ColorTranslator.ToWin32(foreColor);
						flag = true;
					}
					if (itemRenderStyles != null && !backColor.IsEmpty)
					{
						nmtvcustomdraw.clrTextBk = ColorTranslator.ToWin32(backColor);
						flag = true;
					}
					if (flag)
					{
						Marshal.StructureToPtr(nmtvcustomdraw, m.LParam, false);
					}
					if (itemRenderStyles != null && itemRenderStyles.Font != null)
					{
						SafeNativeMethods.SelectObject(new HandleRef(nmtvcustomdraw.nmcd, nmtvcustomdraw.nmcd.hdc), new HandleRef(itemRenderStyles, itemRenderStyles.FontHandle));
						m.Result = (IntPtr)2;
						return;
					}
				}
				m.Result = (IntPtr)0;
				return;
			}
			m.Result = (IntPtr)32;
		}

		/// <summary>Returns an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> for which to return an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</param>
		/// <param name="state">The visible state of the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		// Token: 0x060049B2 RID: 18866 RVA: 0x00135C04 File Offset: 0x00133E04
		protected OwnerDrawPropertyBag GetItemRenderStyles(TreeNode node, int state)
		{
			OwnerDrawPropertyBag ownerDrawPropertyBag = new OwnerDrawPropertyBag();
			if (node == null || node.propBag == null)
			{
				return ownerDrawPropertyBag;
			}
			if ((state & 71) == 0)
			{
				ownerDrawPropertyBag.ForeColor = node.propBag.ForeColor;
				ownerDrawPropertyBag.BackColor = node.propBag.BackColor;
			}
			ownerDrawPropertyBag.Font = node.propBag.Font;
			return ownerDrawPropertyBag;
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x00135C60 File Offset: 0x00133E60
		private unsafe bool WmShowToolTip(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)(void*)m.LParam;
			IntPtr hwndFrom = ptr->hwndFrom;
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point point = Cursor.Position;
			point = base.PointToClientInternal(point);
			tv_HITTESTINFO.pt_x = point.X;
			tv_HITTESTINFO.pt_y = point.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (intPtr != IntPtr.Zero && (tv_HITTESTINFO.flags & 70) != 0)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (treeNode != null && !this.ShowNodeToolTips)
				{
					Rectangle bounds = treeNode.Bounds;
					bounds.Location = base.PointToScreen(bounds.Location);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, hwndFrom), 1055, 1, ref bounds);
					SafeNativeMethods.SetWindowPos(new HandleRef(this, hwndFrom), NativeMethods.HWND_TOPMOST, bounds.Left, bounds.Top, 0, 0, 21);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x00135D50 File Offset: 0x00133F50
		private void WmNeedText(ref Message m)
		{
			NativeMethods.TOOLTIPTEXT tooltiptext = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
			string text = this.controlToolTipText;
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point point = Cursor.Position;
			point = base.PointToClientInternal(point);
			tv_HITTESTINFO.pt_x = point.X;
			tv_HITTESTINFO.pt_y = point.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (intPtr != IntPtr.Zero && (tv_HITTESTINFO.flags & 70) != 0)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (this.ShowNodeToolTips && treeNode != null && !string.IsNullOrEmpty(treeNode.ToolTipText))
				{
					text = treeNode.ToolTipText;
				}
				else if (treeNode != null && treeNode.Bounds.Right > base.Bounds.Right)
				{
					text = treeNode.Text;
				}
				else
				{
					text = null;
				}
			}
			tooltiptext.lpszText = text;
			tooltiptext.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptext.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptext, m.LParam, false);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x00135E70 File Offset: 0x00134070
		private unsafe void WmNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)(void*)m.LParam;
			if (ptr->code == -12)
			{
				this.CustomDraw(ref m);
				return;
			}
			NativeMethods.NMTREEVIEW* ptr2 = (NativeMethods.NMTREEVIEW*)(void*)m.LParam;
			int code = ptr2->nmhdr.code;
			if (code <= -401)
			{
				switch (code)
				{
				case -460:
					goto IL_12E;
				case -459:
					goto IL_10C;
				case -458:
				case -453:
				case -452:
					return;
				case -457:
					goto IL_FF;
				case -456:
					goto IL_F2;
				case -455:
					goto IL_D4;
				case -454:
					break;
				case -451:
					goto IL_EA;
				case -450:
					goto IL_DC;
				default:
					switch (code)
					{
					case -411:
						goto IL_12E;
					case -410:
						goto IL_10C;
					case -409:
					case -404:
					case -403:
						return;
					case -408:
						goto IL_FF;
					case -407:
						goto IL_F2;
					case -406:
						goto IL_D4;
					case -405:
						break;
					case -402:
						goto IL_EA;
					case -401:
						goto IL_DC;
					default:
						return;
					}
					break;
				}
				m.Result = this.TvnExpanding(ptr2);
				return;
				IL_D4:
				this.TvnExpanded(ptr2);
				return;
				IL_DC:
				m.Result = this.TvnSelecting(ptr2);
				return;
				IL_EA:
				this.TvnSelected(ptr2);
				return;
				IL_F2:
				this.TvnBeginDrag(MouseButtons.Left, ptr2);
				return;
				IL_FF:
				this.TvnBeginDrag(MouseButtons.Right, ptr2);
				return;
				IL_10C:
				m.Result = this.TvnBeginLabelEdit((NativeMethods.NMTVDISPINFO)m.GetLParam(typeof(NativeMethods.NMTVDISPINFO)));
				return;
				IL_12E:
				m.Result = this.TvnEndLabelEdit((NativeMethods.NMTVDISPINFO)m.GetLParam(typeof(NativeMethods.NMTVDISPINFO)));
				return;
			}
			if (code != -5 && code != -2)
			{
				return;
			}
			MouseButtons mouseButtons = MouseButtons.Left;
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point point = Cursor.Position;
			point = base.PointToClientInternal(point);
			tv_HITTESTINFO.pt_x = point.X;
			tv_HITTESTINFO.pt_y = point.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (ptr2->nmhdr.code != -2 || (tv_HITTESTINFO.flags & 70) != 0)
			{
				mouseButtons = ((ptr2->nmhdr.code == -2) ? MouseButtons.Left : MouseButtons.Right);
			}
			if ((ptr2->nmhdr.code != -2 || (tv_HITTESTINFO.flags & 70) != 0 || this.FullRowSelect) && intPtr != IntPtr.Zero && !base.ValidationCancelled)
			{
				this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(this.NodeFromHandle(intPtr), mouseButtons, 1, point.X, point.Y));
				this.OnClick(new MouseEventArgs(mouseButtons, 1, point.X, point.Y, 0));
				this.OnMouseClick(new MouseEventArgs(mouseButtons, 1, point.X, point.Y, 0));
			}
			if (ptr2->nmhdr.code == -5)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (treeNode != null && (treeNode.ContextMenu != null || treeNode.ContextMenuStrip != null))
				{
					this.ShowContextMenu(treeNode);
				}
				else
				{
					this.treeViewState[8192] = true;
					base.SendMessage(123, base.Handle, SafeNativeMethods.GetMessagePos());
				}
				m.Result = (IntPtr)1;
			}
			if (!this.treeViewState[4096] && (ptr2->nmhdr.code != -2 || (tv_HITTESTINFO.flags & 70) != 0))
			{
				this.OnMouseUp(new MouseEventArgs(mouseButtons, 1, point.X, point.Y, 0));
				this.treeViewState[4096] = true;
			}
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x001361AC File Offset: 0x001343AC
		private void ShowContextMenu(TreeNode treeNode)
		{
			if (treeNode.ContextMenu != null || treeNode.ContextMenuStrip != null)
			{
				ContextMenu contextMenu = treeNode.ContextMenu;
				ContextMenuStrip contextMenuStrip = treeNode.ContextMenuStrip;
				if (contextMenu != null)
				{
					NativeMethods.POINT point = new NativeMethods.POINT();
					UnsafeNativeMethods.GetCursorPos(point);
					UnsafeNativeMethods.SetForegroundWindow(new HandleRef(this, base.Handle));
					contextMenu.OnPopup(EventArgs.Empty);
					SafeNativeMethods.TrackPopupMenuEx(new HandleRef(contextMenu, contextMenu.Handle), 64, point.x, point.y, new HandleRef(this, base.Handle), null);
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 0, IntPtr.Zero, IntPtr.Zero);
					return;
				}
				if (contextMenuStrip != null)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 4363, 8, treeNode.Handle);
					contextMenuStrip.ShowInternal(this, base.PointToClient(Control.MousePosition), false);
					contextMenuStrip.Closing += this.ContextMenuStripClosing;
				}
			}
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x00136298 File Offset: 0x00134498
		private void ContextMenuStripClosing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			ContextMenuStrip contextMenuStrip = sender as ContextMenuStrip;
			contextMenuStrip.Closing -= this.ContextMenuStripClosing;
			base.SendMessage(4363, 8, null);
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x001362CC File Offset: 0x001344CC
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)m.LParam) != 0 && Application.RenderWithVisualStyles && this.BorderStyle == BorderStyle.Fixed3D)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					using (Graphics graphics = Graphics.FromHdc(m.WParam))
					{
						Rectangle rectangle = new Rectangle(0, 0, base.Size.Width - 1, base.Size.Height - 1);
						graphics.DrawRectangle(new Pen(VisualStyleInformation.TextControlBorder), rectangle);
						rectangle.Inflate(-1, -1);
						graphics.DrawRectangle(SystemPens.Window, rectangle);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060049B9 RID: 18873 RVA: 0x00136398 File Offset: 0x00134598
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 131)
			{
				if (msg <= 21)
				{
					if (msg != 5)
					{
						if (msg != 7)
						{
							if (msg != 21)
							{
								goto IL_878;
							}
							base.SendMessage(4359, this.Indent, 0);
							base.WndProc(ref m);
							return;
						}
						else
						{
							if (this.treeViewState[16384])
							{
								this.treeViewState[16384] = false;
								base.WmImeSetFocus();
								this.DefWndProc(ref m);
								base.InvokeGotFocus(this, EventArgs.Empty);
								return;
							}
							base.WndProc(ref m);
							return;
						}
					}
				}
				else if (msg <= 78)
				{
					if (msg - 70 > 1)
					{
						if (msg != 78)
						{
							goto IL_878;
						}
						NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
						int code = nmhdr.code;
						if (code != -530)
						{
							if (code != -521)
							{
								if (code != -520)
								{
									base.WndProc(ref m);
									return;
								}
							}
							else
							{
								if (this.WmShowToolTip(ref m))
								{
									m.Result = (IntPtr)1;
									return;
								}
								base.WndProc(ref m);
								return;
							}
						}
						UnsafeNativeMethods.SendMessage(new HandleRef(nmhdr, nmhdr.hwndFrom), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
						this.WmNeedText(ref m);
						m.Result = (IntPtr)1;
						return;
					}
				}
				else if (msg != 123)
				{
					if (msg != 131)
					{
						goto IL_878;
					}
				}
				else
				{
					if (this.treeViewState[8192])
					{
						this.treeViewState[8192] = false;
						base.WndProc(ref m);
						return;
					}
					TreeNode treeNode = this.SelectedNode;
					if (treeNode == null || (treeNode.ContextMenu == null && treeNode.ContextMenuStrip == null))
					{
						base.WndProc(ref m);
						return;
					}
					Point point = new Point(treeNode.Bounds.X, treeNode.Bounds.Y + treeNode.Bounds.Height / 2);
					if (!base.ClientRectangle.Contains(point))
					{
						return;
					}
					if (treeNode.ContextMenu != null)
					{
						treeNode.ContextMenu.Show(this, point);
						return;
					}
					if (treeNode.ContextMenuStrip != null)
					{
						bool flag = (int)(long)m.LParam == -1;
						treeNode.ContextMenuStrip.ShowInternal(this, point, flag);
						return;
					}
					return;
				}
				if (this.treeViewState[32768])
				{
					this.DefWndProc(ref m);
					return;
				}
				base.WndProc(ref m);
				return;
			}
			else if (msg <= 675)
			{
				if (msg != 276)
				{
					switch (msg)
					{
					case 513:
					{
						try
						{
							this.treeViewState[65536] = true;
							this.FocusInternal();
						}
						finally
						{
							this.treeViewState[65536] = false;
						}
						this.treeViewState[4096] = false;
						NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
						tv_HITTESTINFO.pt_x = NativeMethods.Util.SignedLOWORD(m.LParam);
						tv_HITTESTINFO.pt_y = NativeMethods.Util.SignedHIWORD(m.LParam);
						this.hNodeMouseDown = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
						if ((tv_HITTESTINFO.flags & 64) != 0)
						{
							this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							if (!base.ValidationCancelled && this.CheckBoxes)
							{
								TreeNode treeNode2 = this.NodeFromHandle(this.hNodeMouseDown);
								if (!this.TreeViewBeforeCheck(treeNode2, TreeViewAction.ByMouse) && treeNode2 != null)
								{
									treeNode2.CheckedInternal = !treeNode2.CheckedInternal;
									this.TreeViewAfterCheck(treeNode2, TreeViewAction.ByMouse);
								}
							}
							m.Result = IntPtr.Zero;
						}
						else
						{
							this.WmMouseDown(ref m, MouseButtons.Left, 1);
						}
						this.downButton = MouseButtons.Left;
						return;
					}
					case 514:
					case 517:
					{
						NativeMethods.TV_HITTESTINFO tv_HITTESTINFO2 = new NativeMethods.TV_HITTESTINFO();
						tv_HITTESTINFO2.pt_x = NativeMethods.Util.SignedLOWORD(m.LParam);
						tv_HITTESTINFO2.pt_y = NativeMethods.Util.SignedHIWORD(m.LParam);
						IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO2);
						if (intPtr != IntPtr.Zero)
						{
							if (!base.ValidationCancelled && (!this.treeViewState[2048] & !this.treeViewState[4096]))
							{
								if (intPtr == this.hNodeMouseDown)
								{
									this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(this.NodeFromHandle(intPtr), this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam)));
								}
								this.OnClick(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								this.OnMouseClick(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							}
							if (this.treeViewState[2048])
							{
								this.treeViewState[2048] = false;
								if (!base.ValidationCancelled)
								{
									this.OnNodeMouseDoubleClick(new TreeNodeMouseClickEventArgs(this.NodeFromHandle(intPtr), this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam)));
									this.OnDoubleClick(new MouseEventArgs(this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
									this.OnMouseDoubleClick(new MouseEventArgs(this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								}
							}
						}
						if (!this.treeViewState[4096])
						{
							this.OnMouseUp(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						}
						this.treeViewState[2048] = false;
						this.treeViewState[4096] = false;
						base.CaptureInternal = false;
						this.hNodeMouseDown = IntPtr.Zero;
						return;
					}
					case 515:
						this.WmMouseDown(ref m, MouseButtons.Left, 2);
						this.treeViewState[2048] = true;
						this.treeViewState[4096] = false;
						base.CaptureInternal = true;
						return;
					case 516:
					{
						this.treeViewState[4096] = false;
						NativeMethods.TV_HITTESTINFO tv_HITTESTINFO3 = new NativeMethods.TV_HITTESTINFO();
						tv_HITTESTINFO3.pt_x = NativeMethods.Util.SignedLOWORD(m.LParam);
						tv_HITTESTINFO3.pt_y = NativeMethods.Util.SignedHIWORD(m.LParam);
						this.hNodeMouseDown = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO3);
						this.WmMouseDown(ref m, MouseButtons.Right, 1);
						this.downButton = MouseButtons.Right;
						return;
					}
					case 518:
						this.WmMouseDown(ref m, MouseButtons.Right, 2);
						this.treeViewState[2048] = true;
						this.treeViewState[4096] = false;
						base.CaptureInternal = true;
						return;
					case 519:
						this.treeViewState[4096] = false;
						this.WmMouseDown(ref m, MouseButtons.Middle, 1);
						this.downButton = MouseButtons.Middle;
						return;
					case 520:
						break;
					case 521:
						this.treeViewState[4096] = false;
						this.WmMouseDown(ref m, MouseButtons.Middle, 2);
						return;
					default:
						if (msg == 675)
						{
							this.prevHoveredNode = null;
							base.WndProc(ref m);
							return;
						}
						break;
					}
				}
				else
				{
					base.WndProc(ref m);
					if (this.DrawMode == TreeViewDrawMode.OwnerDrawAll)
					{
						base.Invalidate();
						return;
					}
					return;
				}
			}
			else
			{
				if (msg <= 4365)
				{
					if (msg == 791)
					{
						this.WmPrint(ref m);
						return;
					}
					if (msg != 4365)
					{
						goto IL_878;
					}
				}
				else if (msg != 4415)
				{
					if (msg != 8270)
					{
						goto IL_878;
					}
					this.WmNotify(ref m);
					return;
				}
				base.WndProc(ref m);
				if (!this.CheckBoxes)
				{
					return;
				}
				NativeMethods.TV_ITEM tv_ITEM = (NativeMethods.TV_ITEM)m.GetLParam(typeof(NativeMethods.TV_ITEM));
				if (tv_ITEM.hItem != IntPtr.Zero)
				{
					NativeMethods.TV_ITEM tv_ITEM2 = default(NativeMethods.TV_ITEM);
					tv_ITEM2.mask = 24;
					tv_ITEM2.hItem = tv_ITEM.hItem;
					tv_ITEM2.stateMask = 61440;
					UnsafeNativeMethods.SendMessage(new HandleRef(null, base.Handle), NativeMethods.TVM_GETITEM, 0, ref tv_ITEM2);
					TreeNode treeNode3 = this.NodeFromHandle(tv_ITEM.hItem);
					treeNode3.CheckedStateInternal = tv_ITEM2.state >> 12 > 1;
					return;
				}
				return;
			}
			IL_878:
			base.WndProc(ref m);
		}

		// Token: 0x04002747 RID: 10055
		private const int MaxIndent = 32000;

		// Token: 0x04002748 RID: 10056
		private const string backSlash = "\\";

		// Token: 0x04002749 RID: 10057
		private const int DefaultTreeViewIndent = 19;

		// Token: 0x0400274A RID: 10058
		private DrawTreeNodeEventHandler onDrawNode;

		// Token: 0x0400274B RID: 10059
		private NodeLabelEditEventHandler onBeforeLabelEdit;

		// Token: 0x0400274C RID: 10060
		private NodeLabelEditEventHandler onAfterLabelEdit;

		// Token: 0x0400274D RID: 10061
		private TreeViewCancelEventHandler onBeforeCheck;

		// Token: 0x0400274E RID: 10062
		private TreeViewEventHandler onAfterCheck;

		// Token: 0x0400274F RID: 10063
		private TreeViewCancelEventHandler onBeforeCollapse;

		// Token: 0x04002750 RID: 10064
		private TreeViewEventHandler onAfterCollapse;

		// Token: 0x04002751 RID: 10065
		private TreeViewCancelEventHandler onBeforeExpand;

		// Token: 0x04002752 RID: 10066
		private TreeViewEventHandler onAfterExpand;

		// Token: 0x04002753 RID: 10067
		private TreeViewCancelEventHandler onBeforeSelect;

		// Token: 0x04002754 RID: 10068
		private TreeViewEventHandler onAfterSelect;

		// Token: 0x04002755 RID: 10069
		private ItemDragEventHandler onItemDrag;

		// Token: 0x04002756 RID: 10070
		private TreeNodeMouseHoverEventHandler onNodeMouseHover;

		// Token: 0x04002757 RID: 10071
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04002758 RID: 10072
		internal TreeNode selectedNode;

		// Token: 0x04002759 RID: 10073
		private ImageList.Indexer imageIndexer;

		// Token: 0x0400275A RID: 10074
		private ImageList.Indexer selectedImageIndexer;

		// Token: 0x0400275B RID: 10075
		private bool setOddHeight;

		// Token: 0x0400275C RID: 10076
		private TreeNode prevHoveredNode;

		// Token: 0x0400275D RID: 10077
		private bool hoveredAlready;

		// Token: 0x0400275E RID: 10078
		private bool rightToLeftLayout;

		// Token: 0x0400275F RID: 10079
		private IntPtr hNodeMouseDown = IntPtr.Zero;

		// Token: 0x04002760 RID: 10080
		private const int TREEVIEWSTATE_hideSelection = 1;

		// Token: 0x04002761 RID: 10081
		private const int TREEVIEWSTATE_labelEdit = 2;

		// Token: 0x04002762 RID: 10082
		private const int TREEVIEWSTATE_scrollable = 4;

		// Token: 0x04002763 RID: 10083
		private const int TREEVIEWSTATE_checkBoxes = 8;

		// Token: 0x04002764 RID: 10084
		private const int TREEVIEWSTATE_showLines = 16;

		// Token: 0x04002765 RID: 10085
		private const int TREEVIEWSTATE_showPlusMinus = 32;

		// Token: 0x04002766 RID: 10086
		private const int TREEVIEWSTATE_showRootLines = 64;

		// Token: 0x04002767 RID: 10087
		private const int TREEVIEWSTATE_sorted = 128;

		// Token: 0x04002768 RID: 10088
		private const int TREEVIEWSTATE_hotTracking = 256;

		// Token: 0x04002769 RID: 10089
		private const int TREEVIEWSTATE_fullRowSelect = 512;

		// Token: 0x0400276A RID: 10090
		private const int TREEVIEWSTATE_showNodeToolTips = 1024;

		// Token: 0x0400276B RID: 10091
		private const int TREEVIEWSTATE_doubleclickFired = 2048;

		// Token: 0x0400276C RID: 10092
		private const int TREEVIEWSTATE_mouseUpFired = 4096;

		// Token: 0x0400276D RID: 10093
		private const int TREEVIEWSTATE_showTreeViewContextMenu = 8192;

		// Token: 0x0400276E RID: 10094
		private const int TREEVIEWSTATE_lastControlValidated = 16384;

		// Token: 0x0400276F RID: 10095
		private const int TREEVIEWSTATE_stopResizeWindowMsgs = 32768;

		// Token: 0x04002770 RID: 10096
		private const int TREEVIEWSTATE_ignoreSelects = 65536;

		// Token: 0x04002771 RID: 10097
		private BitVector32 treeViewState;

		// Token: 0x04002772 RID: 10098
		private static bool isScalingInitialized;

		// Token: 0x04002773 RID: 10099
		private static Size? scaledStateImageSize;

		// Token: 0x04002774 RID: 10100
		private ImageList imageList;

		// Token: 0x04002775 RID: 10101
		private int indent = -1;

		// Token: 0x04002776 RID: 10102
		private int itemHeight = -1;

		// Token: 0x04002777 RID: 10103
		private string pathSeparator = "\\";

		// Token: 0x04002778 RID: 10104
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x04002779 RID: 10105
		internal TreeNodeCollection nodes;

		// Token: 0x0400277A RID: 10106
		internal TreeNode editNode;

		// Token: 0x0400277B RID: 10107
		internal TreeNode root;

		// Token: 0x0400277C RID: 10108
		internal Hashtable nodeTable = new Hashtable();

		// Token: 0x0400277D RID: 10109
		internal bool nodesCollectionClear;

		// Token: 0x0400277E RID: 10110
		private MouseButtons downButton;

		// Token: 0x0400277F RID: 10111
		private TreeViewDrawMode drawMode;

		// Token: 0x04002780 RID: 10112
		private ImageList internalStateImageList;

		// Token: 0x04002781 RID: 10113
		private TreeNode topNode;

		// Token: 0x04002782 RID: 10114
		private ImageList stateImageList;

		// Token: 0x04002783 RID: 10115
		private Color lineColor;

		// Token: 0x04002784 RID: 10116
		private string controlToolTipText;

		// Token: 0x04002785 RID: 10117
		private IComparer treeViewNodeSorter;

		// Token: 0x04002786 RID: 10118
		private TreeNodeMouseClickEventHandler onNodeMouseClick;

		// Token: 0x04002787 RID: 10119
		private TreeNodeMouseClickEventHandler onNodeMouseDoubleClick;
	}
}
