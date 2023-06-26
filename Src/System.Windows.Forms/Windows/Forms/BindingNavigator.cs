using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the navigation and manipulation user interface (UI) for controls on a form that are bound to data.</summary>
	// Token: 0x02000138 RID: 312
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("BindingSource")]
	[DefaultEvent("RefreshItems")]
	[Designer("System.Windows.Forms.Design.BindingNavigatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionBindingNavigator")]
	public class BindingNavigator : ToolStrip, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class.</summary>
		// Token: 0x06000B5B RID: 2907 RVA: 0x000202F0 File Offset: 0x0001E4F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public BindingNavigator()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class with the specified <see cref="T:System.Windows.Forms.BindingSource" /> as the data source.</summary>
		/// <param name="bindingSource">The <see cref="T:System.Windows.Forms.BindingSource" /> used as a data source.</param>
		// Token: 0x06000B5C RID: 2908 RVA: 0x000202F9 File Offset: 0x0001E4F9
		public BindingNavigator(BindingSource bindingSource)
			: this(true)
		{
			this.BindingSource = bindingSource;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class and adds this new instance to the specified container.</summary>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to add the new <see cref="T:System.Windows.Forms.BindingNavigator" /> control to.</param>
		// Token: 0x06000B5D RID: 2909 RVA: 0x00020309 File Offset: 0x0001E509
		[EditorBrowsable(EditorBrowsableState.Never)]
		public BindingNavigator(IContainer container)
			: this(false)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class, indicating whether to display the standard navigation user interface (UI).</summary>
		/// <param name="addStandardItems">
		///   <see langword="true" /> to show the standard navigational UI; otherwise, <see langword="false" />.</param>
		// Token: 0x06000B5E RID: 2910 RVA: 0x00020327 File Offset: 0x0001E527
		public BindingNavigator(bool addStandardItems)
		{
			if (addStandardItems)
			{
				this.AddStandardItems();
			}
		}

		/// <summary>Disables updates to the <see cref="T:System.Windows.Forms.ToolStripItem" /> controls of the <see cref="T:System.Windows.Forms.BindingNavigator" /> during the component's initialization.</summary>
		// Token: 0x06000B5F RID: 2911 RVA: 0x00020356 File Offset: 0x0001E556
		public void BeginInit()
		{
			this.initializing = true;
		}

		/// <summary>Enables updates to the <see cref="T:System.Windows.Forms.ToolStripItem" /> controls of the <see cref="T:System.Windows.Forms.BindingNavigator" /> after the component's initialization has concluded.</summary>
		// Token: 0x06000B60 RID: 2912 RVA: 0x0002035F File Offset: 0x0001E55F
		public void EndInit()
		{
			this.initializing = false;
			this.RefreshItemsInternal();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.BindingNavigator" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000B61 RID: 2913 RVA: 0x0002036E File Offset: 0x0001E56E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.BindingSource = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Adds the standard set of navigation items to the <see cref="T:System.Windows.Forms.BindingNavigator" /> control.</summary>
		// Token: 0x06000B62 RID: 2914 RVA: 0x00020384 File Offset: 0x0001E584
		public virtual void AddStandardItems()
		{
			this.MoveFirstItem = new ToolStripButton();
			this.MovePreviousItem = new ToolStripButton();
			this.MoveNextItem = new ToolStripButton();
			this.MoveLastItem = new ToolStripButton();
			this.PositionItem = new ToolStripTextBox();
			this.CountItem = new ToolStripLabel();
			this.AddNewItem = new ToolStripButton();
			this.DeleteItem = new ToolStripButton();
			ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
			ToolStripSeparator toolStripSeparator2 = new ToolStripSeparator();
			ToolStripSeparator toolStripSeparator3 = new ToolStripSeparator();
			char c = ((string.IsNullOrEmpty(base.Name) || char.IsLower(base.Name[0])) ? 'b' : 'B');
			this.MoveFirstItem.Name = c.ToString() + "indingNavigatorMoveFirstItem";
			this.MovePreviousItem.Name = c.ToString() + "indingNavigatorMovePreviousItem";
			this.MoveNextItem.Name = c.ToString() + "indingNavigatorMoveNextItem";
			this.MoveLastItem.Name = c.ToString() + "indingNavigatorMoveLastItem";
			this.PositionItem.Name = c.ToString() + "indingNavigatorPositionItem";
			this.CountItem.Name = c.ToString() + "indingNavigatorCountItem";
			this.AddNewItem.Name = c.ToString() + "indingNavigatorAddNewItem";
			this.DeleteItem.Name = c.ToString() + "indingNavigatorDeleteItem";
			toolStripSeparator.Name = c.ToString() + "indingNavigatorSeparator";
			toolStripSeparator2.Name = c.ToString() + "indingNavigatorSeparator";
			toolStripSeparator3.Name = c.ToString() + "indingNavigatorSeparator";
			this.MoveFirstItem.Text = SR.GetString("BindingNavigatorMoveFirstItemText");
			this.MovePreviousItem.Text = SR.GetString("BindingNavigatorMovePreviousItemText");
			this.MoveNextItem.Text = SR.GetString("BindingNavigatorMoveNextItemText");
			this.MoveLastItem.Text = SR.GetString("BindingNavigatorMoveLastItemText");
			this.AddNewItem.Text = SR.GetString("BindingNavigatorAddNewItemText");
			this.DeleteItem.Text = SR.GetString("BindingNavigatorDeleteItemText");
			this.CountItem.ToolTipText = SR.GetString("BindingNavigatorCountItemTip");
			this.PositionItem.ToolTipText = SR.GetString("BindingNavigatorPositionItemTip");
			this.CountItem.AutoToolTip = false;
			this.PositionItem.AutoToolTip = false;
			this.PositionItem.AccessibleName = SR.GetString("BindingNavigatorPositionAccessibleName");
			Bitmap bitmap = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveFirst.bmp");
			Bitmap bitmap2 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MovePrevious.bmp");
			Bitmap bitmap3 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveNext.bmp");
			Bitmap bitmap4 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveLast.bmp");
			Bitmap bitmap5 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.AddNew.bmp");
			Bitmap bitmap6 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.Delete.bmp");
			bitmap.MakeTransparent(Color.Magenta);
			bitmap2.MakeTransparent(Color.Magenta);
			bitmap3.MakeTransparent(Color.Magenta);
			bitmap4.MakeTransparent(Color.Magenta);
			bitmap5.MakeTransparent(Color.Magenta);
			bitmap6.MakeTransparent(Color.Magenta);
			this.MoveFirstItem.Image = bitmap;
			this.MovePreviousItem.Image = bitmap2;
			this.MoveNextItem.Image = bitmap3;
			this.MoveLastItem.Image = bitmap4;
			this.AddNewItem.Image = bitmap5;
			this.DeleteItem.Image = bitmap6;
			this.MoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.MovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.MoveNextItem.RightToLeftAutoMirrorImage = true;
			this.MoveLastItem.RightToLeftAutoMirrorImage = true;
			this.AddNewItem.RightToLeftAutoMirrorImage = true;
			this.DeleteItem.RightToLeftAutoMirrorImage = true;
			this.MoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.MovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.MoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.MoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.AddNewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.DeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.PositionItem.AutoSize = false;
			this.PositionItem.Width = 50;
			this.Items.AddRange(new ToolStripItem[]
			{
				this.MoveFirstItem, this.MovePreviousItem, toolStripSeparator, this.PositionItem, this.CountItem, toolStripSeparator2, this.MoveNextItem, this.MoveLastItem, toolStripSeparator3, this.AddNewItem,
				this.DeleteItem
			});
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingSource" /> component that is the source of data.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.BindingSource" /> component associated with this <see cref="T:System.Windows.Forms.BindingNavigator" />. The default is <see langword="null" />.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00020847 File Offset: 0x0001EA47
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x0002084F File Offset: 0x0001EA4F
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("BindingNavigatorBindingSourcePropDescr")]
		[TypeConverter(typeof(ReferenceConverter))]
		public BindingSource BindingSource
		{
			get
			{
				return this.bindingSource;
			}
			set
			{
				this.WireUpBindingSource(ref this.bindingSource, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move First functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move First button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002085E File Offset: 0x0001EA5E
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00020882 File Offset: 0x0001EA82
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMoveFirstItemPropDescr")]
		public ToolStripItem MoveFirstItem
		{
			get
			{
				if (this.moveFirstItem != null && this.moveFirstItem.IsDisposed)
				{
					this.moveFirstItem = null;
				}
				return this.moveFirstItem;
			}
			set
			{
				this.WireUpButton(ref this.moveFirstItem, value, new EventHandler(this.OnMoveFirst));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Previous functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Previous button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002089D File Offset: 0x0001EA9D
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x000208C1 File Offset: 0x0001EAC1
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMovePreviousItemPropDescr")]
		public ToolStripItem MovePreviousItem
		{
			get
			{
				if (this.movePreviousItem != null && this.movePreviousItem.IsDisposed)
				{
					this.movePreviousItem = null;
				}
				return this.movePreviousItem;
			}
			set
			{
				this.WireUpButton(ref this.movePreviousItem, value, new EventHandler(this.OnMovePrevious));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Next functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Next button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x000208DC File Offset: 0x0001EADC
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x00020900 File Offset: 0x0001EB00
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMoveNextItemPropDescr")]
		public ToolStripItem MoveNextItem
		{
			get
			{
				if (this.moveNextItem != null && this.moveNextItem.IsDisposed)
				{
					this.moveNextItem = null;
				}
				return this.moveNextItem;
			}
			set
			{
				this.WireUpButton(ref this.moveNextItem, value, new EventHandler(this.OnMoveNext));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Last functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Last button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0002091B File Offset: 0x0001EB1B
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0002093F File Offset: 0x0001EB3F
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMoveLastItemPropDescr")]
		public ToolStripItem MoveLastItem
		{
			get
			{
				if (this.moveLastItem != null && this.moveLastItem.IsDisposed)
				{
					this.moveLastItem = null;
				}
				return this.moveLastItem;
			}
			set
			{
				this.WireUpButton(ref this.moveLastItem, value, new EventHandler(this.OnMoveLast));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Add New button.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Add New button for the <see cref="T:System.Windows.Forms.BindingSource" />. The default is <see langword="null" />.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002095A File Offset: 0x0001EB5A
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x00020980 File Offset: 0x0001EB80
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorAddNewItemPropDescr")]
		public ToolStripItem AddNewItem
		{
			get
			{
				if (this.addNewItem != null && this.addNewItem.IsDisposed)
				{
					this.addNewItem = null;
				}
				return this.addNewItem;
			}
			set
			{
				if (this.addNewItem != value && value != null)
				{
					value.InternalEnabledChanged += this.OnAddNewItemEnabledChanged;
					this.addNewItemUserEnabled = value.Enabled;
				}
				this.WireUpButton(ref this.addNewItem, value, new EventHandler(this.OnAddNew));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Delete functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Delete button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x000209D0 File Offset: 0x0001EBD0
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x000209F4 File Offset: 0x0001EBF4
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorDeleteItemPropDescr")]
		public ToolStripItem DeleteItem
		{
			get
			{
				if (this.deleteItem != null && this.deleteItem.IsDisposed)
				{
					this.deleteItem = null;
				}
				return this.deleteItem;
			}
			set
			{
				if (this.deleteItem != value && value != null)
				{
					value.InternalEnabledChanged += this.OnDeleteItemEnabledChanged;
					this.deleteItemUserEnabled = value.Enabled;
				}
				this.WireUpButton(ref this.deleteItem, value, new EventHandler(this.OnDelete));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the current position within the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the current position.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00020A44 File Offset: 0x0001EC44
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00020A68 File Offset: 0x0001EC68
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorPositionItemPropDescr")]
		public ToolStripItem PositionItem
		{
			get
			{
				if (this.positionItem != null && this.positionItem.IsDisposed)
				{
					this.positionItem = null;
				}
				return this.positionItem;
			}
			set
			{
				this.WireUpTextBox(ref this.positionItem, value, new KeyEventHandler(this.OnPositionKey), new EventHandler(this.OnPositionLostFocus));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the total number of items in the associated <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the total number of items in the associated <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00020A8F File Offset: 0x0001EC8F
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x00020AB3 File Offset: 0x0001ECB3
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorCountItemPropDescr")]
		public ToolStripItem CountItem
		{
			get
			{
				if (this.countItem != null && this.countItem.IsDisposed)
				{
					this.countItem = null;
				}
				return this.countItem;
			}
			set
			{
				this.WireUpLabel(ref this.countItem, value);
			}
		}

		/// <summary>Gets or sets a string used to format the information displayed in the <see cref="P:System.Windows.Forms.BindingNavigator.CountItem" /> control.</summary>
		/// <returns>The format <see cref="T:System.String" /> used to format the item count. The default is the string "of {0}".</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00020AC2 File Offset: 0x0001ECC2
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00020ACA File Offset: 0x0001ECCA
		[SRCategory("CatAppearance")]
		[SRDescription("BindingNavigatorCountItemFormatPropDescr")]
		public string CountItemFormat
		{
			get
			{
				return this.countItemFormat;
			}
			set
			{
				if (this.countItemFormat != value)
				{
					this.countItemFormat = value;
					this.RefreshItemsInternal();
				}
			}
		}

		/// <summary>Occurs when the state of the navigational user interface (UI) needs to be refreshed to reflect the current state of the underlying data.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000B77 RID: 2935 RVA: 0x00020AE7 File Offset: 0x0001ECE7
		// (remove) Token: 0x06000B78 RID: 2936 RVA: 0x00020B00 File Offset: 0x0001ED00
		[SRCategory("CatBehavior")]
		[SRDescription("BindingNavigatorRefreshItemsEventDescr")]
		public event EventHandler RefreshItems
		{
			add
			{
				this.onRefreshItems = (EventHandler)Delegate.Combine(this.onRefreshItems, value);
			}
			remove
			{
				this.onRefreshItems = (EventHandler)Delegate.Remove(this.onRefreshItems, value);
			}
		}

		/// <summary>Refreshes the state of the standard items to reflect the current state of the data.</summary>
		// Token: 0x06000B79 RID: 2937 RVA: 0x00020B1C File Offset: 0x0001ED1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RefreshItemsCore()
		{
			int num;
			int num2;
			bool flag;
			bool flag2;
			if (this.bindingSource == null)
			{
				num = 0;
				num2 = 0;
				flag = false;
				flag2 = false;
			}
			else
			{
				num = this.bindingSource.Count;
				num2 = this.bindingSource.Position + 1;
				flag = ((IBindingList)this.bindingSource).AllowNew;
				flag2 = ((IBindingList)this.bindingSource).AllowRemove;
			}
			if (!base.DesignMode)
			{
				if (this.MoveFirstItem != null)
				{
					this.moveFirstItem.Enabled = num2 > 1;
				}
				if (this.MovePreviousItem != null)
				{
					this.movePreviousItem.Enabled = num2 > 1;
				}
				if (this.MoveNextItem != null)
				{
					this.moveNextItem.Enabled = num2 < num;
				}
				if (this.MoveLastItem != null)
				{
					this.moveLastItem.Enabled = num2 < num;
				}
				if (this.AddNewItem != null)
				{
					EventHandler eventHandler = new EventHandler(this.OnAddNewItemEnabledChanged);
					this.addNewItem.InternalEnabledChanged -= eventHandler;
					this.addNewItem.Enabled = this.addNewItemUserEnabled && flag;
					this.addNewItem.InternalEnabledChanged += eventHandler;
				}
				if (this.DeleteItem != null)
				{
					EventHandler eventHandler2 = new EventHandler(this.OnDeleteItemEnabledChanged);
					this.deleteItem.InternalEnabledChanged -= eventHandler2;
					this.deleteItem.Enabled = this.deleteItemUserEnabled && flag2 && num > 0;
					this.deleteItem.InternalEnabledChanged += eventHandler2;
				}
				if (this.PositionItem != null)
				{
					this.positionItem.Enabled = num2 > 0 && num > 0;
				}
				if (this.CountItem != null)
				{
					this.countItem.Enabled = num > 0;
				}
			}
			if (this.positionItem != null)
			{
				this.positionItem.Text = num2.ToString(CultureInfo.CurrentCulture);
			}
			if (this.countItem != null)
			{
				this.countItem.Text = (base.DesignMode ? this.CountItemFormat : string.Format(CultureInfo.CurrentCulture, this.CountItemFormat, new object[] { num }));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingNavigator.RefreshItems" /> event.</summary>
		// Token: 0x06000B7A RID: 2938 RVA: 0x00020CF9 File Offset: 0x0001EEF9
		protected virtual void OnRefreshItems()
		{
			this.RefreshItemsCore();
			if (this.onRefreshItems != null)
			{
				this.onRefreshItems(this, EventArgs.Empty);
			}
		}

		/// <summary>Causes form validation to occur and returns whether validation was successful.</summary>
		/// <returns>
		///   <see langword="true" /> if validation was successful and focus can shift to the <see cref="T:System.Windows.Forms.BindingNavigator" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B7B RID: 2939 RVA: 0x00020D1C File Offset: 0x0001EF1C
		public bool Validate()
		{
			bool flag;
			return base.ValidateActiveControl(out flag);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00020D34 File Offset: 0x0001EF34
		private void AcceptNewPosition()
		{
			if (this.positionItem == null || this.bindingSource == null)
			{
				return;
			}
			int num = this.bindingSource.Position;
			try
			{
				num = Convert.ToInt32(this.positionItem.Text, CultureInfo.CurrentCulture) - 1;
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			if (num != this.bindingSource.Position)
			{
				this.bindingSource.Position = num;
			}
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00020DBC File Offset: 0x0001EFBC
		private void CancelNewPosition()
		{
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00020DC4 File Offset: 0x0001EFC4
		private void OnMoveFirst(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MoveFirst();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00020DE7 File Offset: 0x0001EFE7
		private void OnMovePrevious(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MovePrevious();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00020E0A File Offset: 0x0001F00A
		private void OnMoveNext(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MoveNext();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00020E2D File Offset: 0x0001F02D
		private void OnMoveLast(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MoveLast();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00020E50 File Offset: 0x0001F050
		private void OnAddNew(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.AddNew();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00020E74 File Offset: 0x0001F074
		private void OnDelete(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.RemoveCurrent();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00020E98 File Offset: 0x0001F098
		private void OnPositionKey(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return)
			{
				this.AcceptNewPosition();
				return;
			}
			if (keyCode != Keys.Escape)
			{
				return;
			}
			this.CancelNewPosition();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		private void OnPositionLostFocus(object sender, EventArgs e)
		{
			this.AcceptNewPosition();
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00020DBC File Offset: 0x0001EFBC
		private void OnBindingSourceStateChanged(object sender, EventArgs e)
		{
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00020DBC File Offset: 0x0001EFBC
		private void OnBindingSourceListChanged(object sender, ListChangedEventArgs e)
		{
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00020ECC File Offset: 0x0001F0CC
		private void RefreshItemsInternal()
		{
			if (this.initializing)
			{
				return;
			}
			this.OnRefreshItems();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00020EDD File Offset: 0x0001F0DD
		private void ResetCountItemFormat()
		{
			this.countItemFormat = SR.GetString("BindingNavigatorCountItemFormat");
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00020EEF File Offset: 0x0001F0EF
		private bool ShouldSerializeCountItemFormat()
		{
			return this.countItemFormat != SR.GetString("BindingNavigatorCountItemFormat");
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00020F06 File Offset: 0x0001F106
		private void OnAddNewItemEnabledChanged(object sender, EventArgs e)
		{
			if (this.AddNewItem != null)
			{
				this.addNewItemUserEnabled = this.addNewItem.Enabled;
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00020F21 File Offset: 0x0001F121
		private void OnDeleteItemEnabledChanged(object sender, EventArgs e)
		{
			if (this.DeleteItem != null)
			{
				this.deleteItemUserEnabled = this.deleteItem.Enabled;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00020F3C File Offset: 0x0001F13C
		private void WireUpButton(ref ToolStripItem oldButton, ToolStripItem newButton, EventHandler clickHandler)
		{
			if (oldButton == newButton)
			{
				return;
			}
			if (oldButton != null)
			{
				oldButton.Click -= clickHandler;
			}
			if (newButton != null)
			{
				newButton.Click += clickHandler;
			}
			oldButton = newButton;
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00020F64 File Offset: 0x0001F164
		private void WireUpTextBox(ref ToolStripItem oldTextBox, ToolStripItem newTextBox, KeyEventHandler keyUpHandler, EventHandler lostFocusHandler)
		{
			if (oldTextBox != newTextBox)
			{
				ToolStripControlHost toolStripControlHost = oldTextBox as ToolStripControlHost;
				ToolStripControlHost toolStripControlHost2 = newTextBox as ToolStripControlHost;
				if (toolStripControlHost != null)
				{
					toolStripControlHost.KeyUp -= keyUpHandler;
					toolStripControlHost.LostFocus -= lostFocusHandler;
				}
				if (toolStripControlHost2 != null)
				{
					toolStripControlHost2.KeyUp += keyUpHandler;
					toolStripControlHost2.LostFocus += lostFocusHandler;
				}
				oldTextBox = newTextBox;
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00020FB2 File Offset: 0x0001F1B2
		private void WireUpLabel(ref ToolStripItem oldLabel, ToolStripItem newLabel)
		{
			if (oldLabel != newLabel)
			{
				oldLabel = newLabel;
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00020FC4 File Offset: 0x0001F1C4
		private void WireUpBindingSource(ref BindingSource oldBindingSource, BindingSource newBindingSource)
		{
			if (oldBindingSource != newBindingSource)
			{
				if (oldBindingSource != null)
				{
					oldBindingSource.PositionChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.CurrentChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.CurrentItemChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.DataSourceChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.DataMemberChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.ListChanged -= this.OnBindingSourceListChanged;
				}
				if (newBindingSource != null)
				{
					newBindingSource.PositionChanged += this.OnBindingSourceStateChanged;
					newBindingSource.CurrentChanged += this.OnBindingSourceStateChanged;
					newBindingSource.CurrentItemChanged += this.OnBindingSourceStateChanged;
					newBindingSource.DataSourceChanged += this.OnBindingSourceStateChanged;
					newBindingSource.DataMemberChanged += this.OnBindingSourceStateChanged;
					newBindingSource.ListChanged += this.OnBindingSourceListChanged;
				}
				oldBindingSource = newBindingSource;
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x040006BE RID: 1726
		private BindingSource bindingSource;

		// Token: 0x040006BF RID: 1727
		private ToolStripItem moveFirstItem;

		// Token: 0x040006C0 RID: 1728
		private ToolStripItem movePreviousItem;

		// Token: 0x040006C1 RID: 1729
		private ToolStripItem moveNextItem;

		// Token: 0x040006C2 RID: 1730
		private ToolStripItem moveLastItem;

		// Token: 0x040006C3 RID: 1731
		private ToolStripItem addNewItem;

		// Token: 0x040006C4 RID: 1732
		private ToolStripItem deleteItem;

		// Token: 0x040006C5 RID: 1733
		private ToolStripItem positionItem;

		// Token: 0x040006C6 RID: 1734
		private ToolStripItem countItem;

		// Token: 0x040006C7 RID: 1735
		private string countItemFormat = SR.GetString("BindingNavigatorCountItemFormat");

		// Token: 0x040006C8 RID: 1736
		private EventHandler onRefreshItems;

		// Token: 0x040006C9 RID: 1737
		private bool initializing;

		// Token: 0x040006CA RID: 1738
		private bool addNewItemUserEnabled = true;

		// Token: 0x040006CB RID: 1739
		private bool deleteItemUserEnabled = true;
	}
}
