using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Displays a single column header in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	// Token: 0x02000154 RID: 340
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[TypeConverter(typeof(ColumnHeaderConverter))]
	public class ColumnHeader : Component, ICloneable
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x000271E6 File Offset: 0x000253E6
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x000271F0 File Offset: 0x000253F0
		internal ListView OwnerListview
		{
			get
			{
				return this.listview;
			}
			set
			{
				int num = this.Width;
				this.listview = value;
				this.Width = num;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class.</summary>
		// Token: 0x06000D9E RID: 3486 RVA: 0x00027212 File Offset: 0x00025412
		public ColumnHeader()
		{
			this.imageIndexer = new ColumnHeader.ColumnHeaderImageListIndexer(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
		/// <param name="imageIndex">The index of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		// Token: 0x06000D9F RID: 3487 RVA: 0x0002723C File Offset: 0x0002543C
		public ColumnHeader(int imageIndex)
			: this()
		{
			this.ImageIndex = imageIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
		/// <param name="imageKey">The key of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002724B File Offset: 0x0002544B
		public ColumnHeader(string imageKey)
			: this()
		{
			this.ImageKey = imageKey;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0002725C File Offset: 0x0002545C
		internal int ActualImageIndex_Internal
		{
			get
			{
				int actualIndex = this.imageIndexer.ActualIndex;
				if (this.ImageList == null || this.ImageList.Images == null || actualIndex >= this.ImageList.Images.Count)
				{
					return -1;
				}
				return actualIndex;
			}
		}

		/// <summary>Gets or sets the display order of the column relative to the currently displayed columns.</summary>
		/// <returns>The display order of the column, relative to the currently displayed columns.</returns>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x000272A0 File Offset: 0x000254A0
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x000272A8 File Offset: 0x000254A8
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("ColumnHeaderDisplayIndexDescr")]
		public int DisplayIndex
		{
			get
			{
				return this.DisplayIndexInternal;
			}
			set
			{
				if (this.listview == null)
				{
					this.DisplayIndexInternal = value;
					return;
				}
				if (value < 0 || value > this.listview.Columns.Count - 1)
				{
					throw new ArgumentOutOfRangeException("DisplayIndex", SR.GetString("ColumnHeaderBadDisplayIndex"));
				}
				int num = Math.Min(this.DisplayIndexInternal, value);
				int num2 = Math.Max(this.DisplayIndexInternal, value);
				int[] array = new int[this.listview.Columns.Count];
				bool flag = value > this.DisplayIndexInternal;
				ColumnHeader columnHeader = null;
				for (int i = 0; i < this.listview.Columns.Count; i++)
				{
					ColumnHeader columnHeader2 = this.listview.Columns[i];
					if (columnHeader2.DisplayIndex == this.DisplayIndexInternal)
					{
						columnHeader = columnHeader2;
					}
					else if (columnHeader2.DisplayIndex >= num && columnHeader2.DisplayIndex <= num2)
					{
						columnHeader2.DisplayIndexInternal -= (flag ? 1 : (-1));
					}
					if (i != this.Index)
					{
						array[columnHeader2.DisplayIndexInternal] = i;
					}
				}
				columnHeader.DisplayIndexInternal = value;
				array[columnHeader.DisplayIndexInternal] = columnHeader.Index;
				this.SetDisplayIndices(array);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x000273D8 File Offset: 0x000255D8
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x000273E0 File Offset: 0x000255E0
		internal int DisplayIndexInternal
		{
			get
			{
				return this.displayIndexInternal;
			}
			set
			{
				this.displayIndexInternal = value;
			}
		}

		/// <summary>Gets the location with the <see cref="T:System.Windows.Forms.ListView" /> control's <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of this column.</summary>
		/// <returns>The zero-based index of the column header within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control it is contained in.</returns>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000273E9 File Offset: 0x000255E9
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.listview != null)
				{
					return this.listview.GetColumnIndex(this);
				}
				return -1;
			}
		}

		/// <summary>Gets or sets the index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>The index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.ColumnHeader.ImageIndex" /> is less than -1.</exception>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00027404 File Offset: 0x00025604
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00027464 File Offset: 0x00025664
		[DefaultValue(-1)]
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImageIndex
		{
			get
			{
				if (this.imageIndexer.Index != -1 && this.ImageList != null && this.imageIndexer.Index >= this.ImageList.Images.Count)
				{
					return this.ImageList.Images.Count - 1;
				}
				return this.imageIndexer.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						(-1).ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.imageIndexer.Index != value)
				{
					this.imageIndexer.Index = value;
					if (this.ListView != null && this.ListView.IsHandleCreated)
					{
						this.ListView.SetColumnInfo(16, this);
					}
				}
			}
		}

		/// <summary>Gets the image list associated with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000274F5 File Offset: 0x000256F5
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				return this.imageIndexer.ImageList;
			}
		}

		/// <summary>Gets or sets the key of the image displayed in the column.</summary>
		/// <returns>The key of the image displayed in the column.</returns>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00027502 File Offset: 0x00025702
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x00027510 File Offset: 0x00025710
		[DefaultValue("")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ImageKey
		{
			get
			{
				return this.imageIndexer.Key;
			}
			set
			{
				if (value != this.imageIndexer.Key)
				{
					this.imageIndexer.Key = value;
					if (this.ListView != null && this.ListView.IsHandleCreated)
					{
						this.ListView.SetColumnInfo(16, this);
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control the <see cref="T:System.Windows.Forms.ColumnHeader" /> is located in.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView" /> control that represents the control that contains the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000271E6 File Offset: 0x000253E6
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.listview;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0002755F File Offset: 0x0002575F
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x0002756D File Offset: 0x0002576D
		[Browsable(false)]
		[SRDescription("ColumnHeaderNameDescr")]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, this.name);
			}
			set
			{
				if (value == null)
				{
					this.name = "";
				}
				else
				{
					this.name = value;
				}
				if (this.Site != null)
				{
					this.Site.Name = value;
				}
			}
		}

		/// <summary>Gets or sets the text displayed in the column header.</summary>
		/// <returns>The text displayed in the column header.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0002759A File Offset: 0x0002579A
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x000275B0 File Offset: 0x000257B0
		[Localizable(true)]
		[SRDescription("ColumnCaption")]
		public string Text
		{
			get
			{
				if (this.text == null)
				{
					return "ColumnHeader";
				}
				return this.text;
			}
			set
			{
				if (value == null)
				{
					this.text = "";
				}
				else
				{
					this.text = value;
				}
				if (this.listview != null)
				{
					this.listview.SetColumnInfo(4, this);
				}
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the text displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000275E0 File Offset: 0x000257E0
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00027634 File Offset: 0x00025834
		[SRDescription("ColumnAlignment")]
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment TextAlign
		{
			get
			{
				if (!this.textAlignInitialized && this.listview != null)
				{
					this.textAlignInitialized = true;
					if (this.Index != 0 && this.listview.RightToLeft == RightToLeft.Yes && !this.listview.IsMirrored)
					{
						this.textAlign = HorizontalAlignment.Right;
					}
				}
				return this.textAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				this.textAlign = value;
				if (this.Index == 0 && this.textAlign != HorizontalAlignment.Left)
				{
					this.textAlign = HorizontalAlignment.Left;
				}
				if (this.listview != null)
				{
					this.listview.SetColumnInfo(1, this);
					this.listview.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets an object that contains data to associate with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data to associate with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x000276A5 File Offset: 0x000258A5
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x000276AD File Offset: 0x000258AD
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

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000276B6 File Offset: 0x000258B6
		internal int WidthInternal
		{
			get
			{
				return this.width;
			}
		}

		/// <summary>Gets or sets the width of the column.</summary>
		/// <returns>The width of the column, in pixels.</returns>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x000276C0 File Offset: 0x000258C0
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00027798 File Offset: 0x00025998
		[SRDescription("ColumnWidth")]
		[Localizable(true)]
		[DefaultValue(60)]
		public int Width
		{
			get
			{
				if (this.listview != null && this.listview.IsHandleCreated && !this.listview.Disposing && this.listview.View == View.Details)
				{
					IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, this.listview.Handle), 4127, 0, 0);
					if (intPtr != IntPtr.Zero)
					{
						int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, intPtr), 4608, 0, 0);
						if (this.Index < num)
						{
							this.width = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, this.listview.Handle), 4125, this.Index, 0);
						}
					}
				}
				return this.width;
			}
			set
			{
				this.width = value;
				if (this.listview != null)
				{
					this.listview.SetColumnWidth(this.Index, ColumnHeaderAutoResizeStyle.None);
				}
			}
		}

		/// <summary>Resizes the width of the column as indicated by the resize style.</summary>
		/// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">A value other than <see cref="F:System.Windows.Forms.ColumnHeaderAutoResizeStyle.None" /> is passed to the <see cref="M:System.Windows.Forms.ColumnHeader.AutoResize(System.Windows.Forms.ColumnHeaderAutoResizeStyle)" /> method when the <see cref="P:System.Windows.Forms.ListView.View" /> property is a value other than <see cref="F:System.Windows.Forms.View.Details" />.</exception>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x000277BB File Offset: 0x000259BB
		public void AutoResize(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (headerAutoResize < ColumnHeaderAutoResizeStyle.None || headerAutoResize > ColumnHeaderAutoResizeStyle.ColumnContent)
			{
				throw new InvalidEnumArgumentException("headerAutoResize", (int)headerAutoResize, typeof(ColumnHeaderAutoResizeStyle));
			}
			if (this.listview != null)
			{
				this.listview.AutoResizeColumn(this.Index, headerAutoResize);
			}
		}

		/// <summary>Creates an identical copy of the current <see cref="T:System.Windows.Forms.ColumnHeader" /> that is not attached to any list view control.</summary>
		/// <returns>An object representing a copy of this <see cref="T:System.Windows.Forms.ColumnHeader" /> object.</returns>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x000277F8 File Offset: 0x000259F8
		public object Clone()
		{
			Type type = base.GetType();
			ColumnHeader columnHeader;
			if (type == typeof(ColumnHeader))
			{
				columnHeader = new ColumnHeader();
			}
			else
			{
				columnHeader = (ColumnHeader)Activator.CreateInstance(type);
			}
			columnHeader.text = this.text;
			columnHeader.Width = this.width;
			columnHeader.textAlign = this.TextAlign;
			return columnHeader;
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000DBA RID: 3514 RVA: 0x0002785C File Offset: 0x00025A5C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listview != null)
			{
				int num = this.Index;
				if (num != -1)
				{
					this.listview.Columns.RemoveAt(num);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00027897 File Offset: 0x00025A97
		private void ResetText()
		{
			this.Text = null;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000278A0 File Offset: 0x00025AA0
		private void SetDisplayIndices(int[] cols)
		{
			if (this.listview.IsHandleCreated && !this.listview.Disposing)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, this.listview.Handle), 4154, cols.Length, cols);
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000278EC File Offset: 0x00025AEC
		private bool ShouldSerializeName()
		{
			return !string.IsNullOrEmpty(this.name);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000278FC File Offset: 0x00025AFC
		private bool ShouldSerializeDisplayIndex()
		{
			return this.DisplayIndex != this.Index;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0002790F File Offset: 0x00025B0F
		internal bool ShouldSerializeText()
		{
			return this.text != null;
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002791A File Offset: 0x00025B1A
		public override string ToString()
		{
			return "ColumnHeader: Text: " + this.Text;
		}

		// Token: 0x0400078B RID: 1931
		internal int index = -1;

		// Token: 0x0400078C RID: 1932
		internal string text;

		// Token: 0x0400078D RID: 1933
		internal string name;

		// Token: 0x0400078E RID: 1934
		internal int width = 60;

		// Token: 0x0400078F RID: 1935
		private HorizontalAlignment textAlign;

		// Token: 0x04000790 RID: 1936
		private bool textAlignInitialized;

		// Token: 0x04000791 RID: 1937
		private int displayIndexInternal = -1;

		// Token: 0x04000792 RID: 1938
		private ColumnHeader.ColumnHeaderImageListIndexer imageIndexer;

		// Token: 0x04000793 RID: 1939
		private object userData;

		// Token: 0x04000794 RID: 1940
		private ListView listview;

		// Token: 0x02000621 RID: 1569
		internal class ColumnHeaderImageListIndexer : ImageList.Indexer
		{
			// Token: 0x0600633D RID: 25405 RVA: 0x0016E934 File Offset: 0x0016CB34
			public ColumnHeaderImageListIndexer(ColumnHeader ch)
			{
				this.owner = ch;
			}

			// Token: 0x17001532 RID: 5426
			// (get) Token: 0x0600633E RID: 25406 RVA: 0x0016E943 File Offset: 0x0016CB43
			// (set) Token: 0x0600633F RID: 25407 RVA: 0x000070A6 File Offset: 0x000052A6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner != null && this.owner.ListView != null)
					{
						return this.owner.ListView.SmallImageList;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x0400391E RID: 14622
			private ColumnHeader owner;
		}
	}
}
