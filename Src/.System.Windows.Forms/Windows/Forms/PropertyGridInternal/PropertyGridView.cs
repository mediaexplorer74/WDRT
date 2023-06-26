using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Internal;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Internal;
using System.Windows.Forms.VisualStyles;
using Accessibility;
using Microsoft.Win32;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004F8 RID: 1272
	internal class PropertyGridView : Control, IWin32Window, IWindowsFormsEditorService, IServiceProvider
	{
		// Token: 0x06005296 RID: 21142 RVA: 0x00156978 File Offset: 0x00154B78
		public PropertyGridView(IServiceProvider serviceProvider, PropertyGrid propertyGrid)
		{
			if (DpiHelper.IsScalingRequired)
			{
				this.paintWidth = DpiHelper.LogicalToDeviceUnitsX(20);
				this.paintIndent = DpiHelper.LogicalToDeviceUnitsX(26);
				this.outlineSizeExplorerTreeStyle = DpiHelper.LogicalToDeviceUnitsX(16);
				this.outlineSize = DpiHelper.LogicalToDeviceUnitsX(9);
				this.maxListBoxHeight = DpiHelper.LogicalToDeviceUnitsY(200);
			}
			this.ehValueClick = new EventHandler(this.OnGridEntryValueClick);
			this.ehLabelClick = new EventHandler(this.OnGridEntryLabelClick);
			this.ehOutlineClick = new EventHandler(this.OnGridEntryOutlineClick);
			this.ehValueDblClick = new EventHandler(this.OnGridEntryValueDoubleClick);
			this.ehLabelDblClick = new EventHandler(this.OnGridEntryLabelDoubleClick);
			this.ehRecreateChildren = new GridEntryRecreateChildrenEventHandler(this.OnRecreateChildren);
			this.ownerGrid = propertyGrid;
			this.serviceProvider = serviceProvider;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, false);
			base.SetStyle(ControlStyles.UserMouse, true);
			this.BackColor = SystemColors.Window;
			this.ForeColor = SystemColors.WindowText;
			this.grayTextColor = SystemColors.GrayText;
			this.backgroundBrush = SystemBrushes.Window;
			base.TabStop = true;
			this.Text = "PropertyGridView";
			this.CreateUI();
			this.LayoutWindow(true);
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06005297 RID: 21143 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x06005298 RID: 21144 RVA: 0x00156B6C File Offset: 0x00154D6C
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				this.backgroundBrush = new SolidBrush(value);
				base.BackColor = value;
			}
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x00156B81 File Offset: 0x00154D81
		internal Brush GetBackgroundBrush(Graphics g)
		{
			return this.backgroundBrush;
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x00156B8C File Offset: 0x00154D8C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanCopy
		{
			get
			{
				if (this.selectedGridEntry == null)
				{
					return false;
				}
				if (!this.Edit.Focused)
				{
					string propertyTextValue = this.selectedGridEntry.GetPropertyTextValue();
					return propertyTextValue != null && propertyTextValue.Length > 0;
				}
				return true;
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x00156BCC File Offset: 0x00154DCC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanCut
		{
			get
			{
				return this.CanCopy && this.selectedGridEntry != null && this.selectedGridEntry.IsTextEditable;
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x0600529C RID: 21148 RVA: 0x00156BEB File Offset: 0x00154DEB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanPaste
		{
			get
			{
				return this.selectedGridEntry != null && this.selectedGridEntry.IsTextEditable;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x0600529D RID: 21149 RVA: 0x00156C02 File Offset: 0x00154E02
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanUndo
		{
			get
			{
				return this.Edit.Visible && this.Edit.Focused && (int)this.Edit.SendMessage(198, 0, 0) != 0;
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x00156C3C File Offset: 0x00154E3C
		internal DropDownButton DropDownButton
		{
			get
			{
				if (this.btnDropDown == null)
				{
					this.btnDropDown = new DropDownButton();
					this.btnDropDown.UseComboBoxTheme = true;
					Bitmap bitmap = this.CreateResizedBitmap("Arrow.ico", 16, 16);
					this.btnDropDown.Image = bitmap;
					this.btnDropDown.BackColor = SystemColors.Control;
					this.btnDropDown.ForeColor = SystemColors.ControlText;
					this.btnDropDown.Click += this.OnBtnClick;
					this.btnDropDown.GotFocus += this.OnDropDownButtonGotFocus;
					this.btnDropDown.LostFocus += this.OnChildLostFocus;
					this.btnDropDown.TabIndex = 2;
					this.CommonEditorSetup(this.btnDropDown);
					this.btnDropDown.Size = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight) : new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight));
				}
				return this.btnDropDown;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x00156D44 File Offset: 0x00154F44
		internal Button DialogButton
		{
			get
			{
				if (this.btnDialog == null)
				{
					this.btnDialog = new DropDownButton();
					this.btnDialog.BackColor = SystemColors.Control;
					this.btnDialog.ForeColor = SystemColors.ControlText;
					this.btnDialog.TabIndex = 3;
					this.btnDialog.Image = this.CreateResizedBitmap("dotdotdot.ico", 7, 8);
					this.btnDialog.Click += this.OnBtnClick;
					this.btnDialog.KeyDown += this.OnBtnKeyDown;
					this.btnDialog.GotFocus += this.OnDropDownButtonGotFocus;
					this.btnDialog.LostFocus += this.OnChildLostFocus;
					this.btnDialog.Size = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight) : new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight));
					this.CommonEditorSetup(this.btnDialog);
				}
				return this.btnDialog;
			}
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00156E54 File Offset: 0x00155054
		private static Bitmap GetBitmapFromIcon(string iconName, int iconsWidth, int iconsHeight)
		{
			Size size = new Size(iconsWidth, iconsHeight);
			Icon icon = new Icon(BitmapSelector.GetResourceStream(typeof(PropertyGrid), iconName), size);
			Bitmap bitmap = icon.ToBitmap();
			icon.Dispose();
			if ((DpiHelper.IsScalingRequired || DpiHelper.EnableDpiChangedHighDpiImprovements) && (bitmap.Size.Width != iconsWidth || bitmap.Size.Height != iconsHeight))
			{
				Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, size);
				if (bitmap2 != null)
				{
					bitmap.Dispose();
					bitmap = bitmap2;
				}
			}
			return bitmap;
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x00156ED8 File Offset: 0x001550D8
		private PropertyGridView.GridViewEdit Edit
		{
			get
			{
				if (this.edit == null)
				{
					this.edit = new PropertyGridView.GridViewEdit(this);
					this.edit.BorderStyle = BorderStyle.None;
					this.edit.AutoSize = false;
					this.edit.TabStop = false;
					this.edit.AcceptsReturn = true;
					this.edit.BackColor = this.BackColor;
					this.edit.ForeColor = this.ForeColor;
					this.edit.KeyDown += this.OnEditKeyDown;
					this.edit.KeyPress += this.OnEditKeyPress;
					this.edit.GotFocus += this.OnEditGotFocus;
					this.edit.LostFocus += this.OnEditLostFocus;
					this.edit.MouseDown += this.OnEditMouseDown;
					this.edit.TextChanged += this.OnEditChange;
					this.edit.TabIndex = 1;
					this.CommonEditorSetup(this.edit);
				}
				return this.edit;
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x00156FF6 File Offset: 0x001551F6
		internal AccessibleObject EditAccessibleObject
		{
			get
			{
				return this.Edit.AccessibilityObject;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x00157004 File Offset: 0x00155204
		internal PropertyGridView.GridViewListBox DropDownListBox
		{
			get
			{
				if (this.listBox == null)
				{
					this.listBox = new PropertyGridView.GridViewListBox(this);
					this.listBox.DrawMode = DrawMode.OwnerDrawFixed;
					this.listBox.MouseUp += this.OnListMouseUp;
					this.listBox.DrawItem += this.OnListDrawItem;
					this.listBox.SelectedIndexChanged += this.OnListChange;
					this.listBox.KeyDown += this.OnListKeyDown;
					this.listBox.LostFocus += this.OnChildLostFocus;
					this.listBox.Visible = true;
					this.listBox.ItemHeight = this.RowHeight;
				}
				return this.listBox;
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x001570CA File Offset: 0x001552CA
		internal AccessibleObject DropDownListBoxAccessibleObject
		{
			get
			{
				if (this.DropDownListBox.Visible)
				{
					return this.DropDownListBox.AccessibilityObject;
				}
				return null;
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x001570E8 File Offset: 0x001552E8
		internal bool DrawValuesRightToLeft
		{
			get
			{
				if (this.edit != null && this.edit.IsHandleCreated)
				{
					int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this.edit, this.edit.Handle), -20);
					return (num & 8192) != 0;
				}
				return false;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x0015713A File Offset: 0x0015533A
		internal PropertyGridView.DropDownHolder DropDownControlHolder
		{
			get
			{
				return this.dropDownHolder;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x00157142 File Offset: 0x00155342
		internal bool DropDownVisible
		{
			get
			{
				return this.dropDownHolder != null && this.dropDownHolder.Visible;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x00157159 File Offset: 0x00155359
		public bool FocusInside
		{
			get
			{
				return base.ContainsFocus || (this.dropDownHolder != null && this.dropDownHolder.ContainsFocus);
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x0015717C File Offset: 0x0015537C
		// (set) Token: 0x060052AA RID: 21162 RVA: 0x00157200 File Offset: 0x00155400
		internal Color GrayTextColor
		{
			get
			{
				if (this.grayTextColorModified)
				{
					return this.grayTextColor;
				}
				if (this.ForeColor.ToArgb() == SystemColors.WindowText.ToArgb())
				{
					return SystemColors.GrayText;
				}
				int num = this.ForeColor.ToArgb();
				int num2 = (num >> 24) & 255;
				if (num2 != 0)
				{
					num2 /= 2;
					num &= 16777215;
					num |= (int)((long)((long)num2 << 24) & (long)((ulong)(-16777216)));
				}
				else
				{
					num /= 2;
				}
				return Color.FromArgb(num);
			}
			set
			{
				this.grayTextColor = value;
				this.grayTextColorModified = true;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x00157210 File Offset: 0x00155410
		private GridErrorDlg ErrorDialog
		{
			get
			{
				if (this.errorDlg == null)
				{
					using (DpiHelper.EnterDpiAwarenessScope(DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE))
					{
						this.errorDlg = new GridErrorDlg(this.ownerGrid);
					}
				}
				return this.errorDlg;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x00157260 File Offset: 0x00155460
		private bool HasEntries
		{
			get
			{
				return this.topLevelGridEntries != null && this.topLevelGridEntries.Count > 0;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x0015727A File Offset: 0x0015547A
		protected int InternalLabelWidth
		{
			get
			{
				if (this.GetFlag(128))
				{
					this.UpdateUIBasedOnFont(true);
				}
				if (this.labelWidth == -1)
				{
					this.SetConstants();
				}
				return this.labelWidth;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (set) Token: 0x060052AE RID: 21166 RVA: 0x001572A5 File Offset: 0x001554A5
		internal int LabelPaintMargin
		{
			set
			{
				this.requiredLabelPaintMargin = (short)Math.Max(Math.Max(value, (int)this.requiredLabelPaintMargin), 2);
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x001572C0 File Offset: 0x001554C0
		protected bool NeedsCommit
		{
			get
			{
				if (this.edit == null || !this.Edit.Visible)
				{
					return false;
				}
				string text = this.Edit.Text;
				return ((text != null && text.Length != 0) || (this.originalTextValue != null && this.originalTextValue.Length != 0)) && (text == null || this.originalTextValue == null || !text.Equals(this.originalTextValue));
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x0015732C File Offset: 0x0015552C
		public PropertyGrid OwnerGrid
		{
			get
			{
				return this.ownerGrid;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x00157334 File Offset: 0x00155534
		protected int RowHeight
		{
			get
			{
				if (this.cachedRowHeight == -1)
				{
					this.cachedRowHeight = this.Font.Height + 2;
				}
				return this.cachedRowHeight;
			}
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x00157358 File Offset: 0x00155558
		public Point ContextMenuDefaultLocation
		{
			get
			{
				Rectangle rectangle = this.GetRectangle(this.selectedRow, 1);
				Point point = base.PointToScreen(new Point(rectangle.X, rectangle.Y));
				return new Point(point.X + rectangle.Width / 2, point.Y + rectangle.Height / 2);
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060052B3 RID: 21171 RVA: 0x001573B4 File Offset: 0x001555B4
		private ScrollBar ScrollBar
		{
			get
			{
				if (this.scrollBar == null)
				{
					this.scrollBar = new VScrollBar();
					this.scrollBar.Scroll += this.OnScroll;
					base.Controls.Add(this.scrollBar);
				}
				return this.scrollBar;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x00157402 File Offset: 0x00155602
		// (set) Token: 0x060052B5 RID: 21173 RVA: 0x0015740C File Offset: 0x0015560C
		internal GridEntry SelectedGridEntry
		{
			get
			{
				return this.selectedGridEntry;
			}
			set
			{
				if (this.allGridEntries != null)
				{
					foreach (object obj in this.allGridEntries)
					{
						GridEntry gridEntry = (GridEntry)obj;
						if (gridEntry == value)
						{
							this.SelectGridEntry(value, true);
							return;
						}
					}
				}
				GridEntry gridEntry2 = this.FindEquivalentGridEntry(new GridEntryCollection(null, new GridEntry[] { value }));
				if (gridEntry2 != null)
				{
					this.SelectGridEntry(gridEntry2, true);
					return;
				}
				throw new ArgumentException(SR.GetString("PropertyGridInvalidGridEntry"));
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x001574A8 File Offset: 0x001556A8
		// (set) Token: 0x060052B7 RID: 21175 RVA: 0x001574B0 File Offset: 0x001556B0
		public IServiceProvider ServiceProvider
		{
			get
			{
				return this.serviceProvider;
			}
			set
			{
				if (value != this.serviceProvider)
				{
					this.serviceProvider = value;
					this.topHelpService = null;
					if (this.helpService != null && this.helpService is IDisposable)
					{
						((IDisposable)this.helpService).Dispose();
					}
					this.helpService = null;
				}
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x000A83A1 File Offset: 0x000A65A1
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x060052B9 RID: 21177 RVA: 0x00157500 File Offset: 0x00155700
		// (set) Token: 0x060052BA RID: 21178 RVA: 0x00157511 File Offset: 0x00155711
		private int TipColumn
		{
			get
			{
				return (this.tipInfo & -65536) >> 16;
			}
			set
			{
				this.tipInfo &= 65535;
				this.tipInfo |= (value & 65535) << 16;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x0015753C File Offset: 0x0015573C
		// (set) Token: 0x060052BC RID: 21180 RVA: 0x0015754A File Offset: 0x0015574A
		private int TipRow
		{
			get
			{
				return this.tipInfo & 65535;
			}
			set
			{
				this.tipInfo &= -65536;
				this.tipInfo |= value & 65535;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x060052BD RID: 21181 RVA: 0x00157574 File Offset: 0x00155774
		private GridToolTip ToolTip
		{
			get
			{
				if (this.toolTip == null)
				{
					this.toolTip = new GridToolTip(new Control[] { this, this.Edit });
					this.toolTip.ToolTip = "";
					this.toolTip.Font = this.Font;
				}
				return this.toolTip;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x060052BE RID: 21182 RVA: 0x001575CE File Offset: 0x001557CE
		internal GridEntryCollection TopLevelGridEntries
		{
			get
			{
				return this.topLevelGridEntries;
			}
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x001575D6 File Offset: 0x001557D6
		internal GridEntryCollection AccessibilityGetGridEntries()
		{
			return this.GetAllGridEntries();
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x001575E0 File Offset: 0x001557E0
		internal Rectangle AccessibilityGetGridEntryBounds(GridEntry gridEntry)
		{
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			if (AccessibilityImprovements.Level4)
			{
				if (rowFromGridEntry < 0)
				{
					return Rectangle.Empty;
				}
			}
			else if (rowFromGridEntry == -1)
			{
				return new Rectangle(0, 0, 0, 0);
			}
			Rectangle rectangle = this.GetRectangle(rowFromGridEntry, 3);
			NativeMethods.POINT point = new NativeMethods.POINT(rectangle.X, rectangle.Y);
			UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
			if (AccessibilityImprovements.Level4)
			{
				bool flag;
				if (gridEntry == null)
				{
					flag = null != null;
				}
				else
				{
					PropertyGrid propertyGrid = gridEntry.OwnerGrid;
					flag = ((propertyGrid != null) ? propertyGrid.GridViewAccessibleObject : null) != null;
				}
				if (flag)
				{
					int num = gridEntry.OwnerGrid.GridViewAccessibleObject.Bounds.Bottom - 1;
					if (point.y > num)
					{
						return Rectangle.Empty;
					}
					if (point.y + rectangle.Height > num)
					{
						rectangle.Height = num - point.y;
					}
				}
			}
			return new Rectangle(point.x, point.y, rectangle.Width, rectangle.Height);
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x001576D4 File Offset: 0x001558D4
		internal int AccessibilityGetGridEntryChildID(GridEntry gridEntry)
		{
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection == null)
			{
				return -1;
			}
			for (int i = 0; i < gridEntryCollection.Count; i++)
			{
				if (gridEntryCollection[i].Equals(gridEntry))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x00157710 File Offset: 0x00155910
		internal void AccessibilitySelect(GridEntry entry)
		{
			this.SelectGridEntry(entry, true);
			this.FocusInternal();
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x00157724 File Offset: 0x00155924
		private void AddGridEntryEvents(GridEntryCollection ipeArray, int startIndex, int count)
		{
			if (ipeArray == null)
			{
				return;
			}
			if (count == -1)
			{
				count = ipeArray.Count - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (ipeArray[i] != null)
				{
					GridEntry entry = ipeArray.GetEntry(i);
					entry.AddOnValueClick(this.ehValueClick);
					entry.AddOnLabelClick(this.ehLabelClick);
					entry.AddOnOutlineClick(this.ehOutlineClick);
					entry.AddOnOutlineDoubleClick(this.ehOutlineClick);
					entry.AddOnValueDoubleClick(this.ehValueDblClick);
					entry.AddOnLabelDoubleClick(this.ehLabelDblClick);
					entry.AddOnRecreateChildren(this.ehRecreateChildren);
				}
			}
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x001577B6 File Offset: 0x001559B6
		protected virtual void AdjustOrigin(Graphics g, Point newOrigin, ref Rectangle r)
		{
			g.ResetTransform();
			g.TranslateTransform((float)newOrigin.X, (float)newOrigin.Y);
			r.Offset(-newOrigin.X, -newOrigin.Y);
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x001577EA File Offset: 0x001559EA
		private void CancelSplitterMove()
		{
			if (this.GetFlag(4))
			{
				this.SetFlag(4, false);
				base.CaptureInternal = false;
				if (this.selectedRow != -1)
				{
					this.SelectRow(this.selectedRow);
				}
			}
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00157819 File Offset: 0x00155A19
		internal PropertyGridView.GridPositionData CaptureGridPositionData()
		{
			return new PropertyGridView.GridPositionData(this);
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00157824 File Offset: 0x00155A24
		private void ClearGridEntryEvents(GridEntryCollection ipeArray, int startIndex, int count)
		{
			if (ipeArray == null)
			{
				return;
			}
			if (count == -1)
			{
				count = ipeArray.Count - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (ipeArray[i] != null)
				{
					GridEntry entry = ipeArray.GetEntry(i);
					entry.RemoveOnValueClick(this.ehValueClick);
					entry.RemoveOnLabelClick(this.ehLabelClick);
					entry.RemoveOnOutlineClick(this.ehOutlineClick);
					entry.RemoveOnOutlineDoubleClick(this.ehOutlineClick);
					entry.RemoveOnValueDoubleClick(this.ehValueDblClick);
					entry.RemoveOnLabelDoubleClick(this.ehLabelDblClick);
					entry.RemoveOnRecreateChildren(this.ehRecreateChildren);
				}
			}
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x001578B6 File Offset: 0x00155AB6
		public void ClearProps()
		{
			if (!this.HasEntries)
			{
				return;
			}
			this.CommonEditorHide();
			this.topLevelGridEntries = null;
			this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
			this.allGridEntries = null;
			this.selectedRow = -1;
			this.tipInfo = -1;
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x001578F1 File Offset: 0x00155AF1
		public void CloseDropDown()
		{
			this.CloseDropDownInternal(true);
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x001578FC File Offset: 0x00155AFC
		private void CloseDropDownInternal(bool resetFocus)
		{
			if (this.GetFlag(32))
			{
				return;
			}
			try
			{
				this.SetFlag(32, true);
				if (this.dropDownHolder != null && this.dropDownHolder.Visible)
				{
					if (this.dropDownHolder.Component == this.DropDownListBox && this.GetFlag(64))
					{
						this.OnListClick(null, null);
					}
					this.Edit.Filter = false;
					this.dropDownHolder.SetComponent(null, false);
					this.dropDownHolder.Visible = false;
					if (resetFocus)
					{
						if (this.DialogButton.Visible)
						{
							this.DialogButton.FocusInternal();
						}
						else if (this.DropDownButton.Visible)
						{
							this.DropDownButton.FocusInternal();
						}
						else if (this.Edit.Visible)
						{
							this.Edit.FocusInternal();
						}
						else
						{
							this.FocusInternal();
						}
						if (this.selectedRow != -1)
						{
							this.SelectRow(this.selectedRow);
						}
					}
					if (AccessibilityImprovements.Level3 && this.selectedRow != -1)
					{
						GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
						if (gridEntryFromRow != null)
						{
							gridEntryFromRow.AccessibilityObject.RaiseAutomationEvent(20005);
							gridEntryFromRow.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Expanded, UnsafeNativeMethods.ExpandCollapseState.Collapsed);
						}
					}
				}
			}
			finally
			{
				this.SetFlag(32, false);
			}
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00157A6C File Offset: 0x00155C6C
		private void CommonEditorHide()
		{
			this.CommonEditorHide(false);
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x00157A78 File Offset: 0x00155C78
		private void CommonEditorHide(bool always)
		{
			if (!always && !this.HasEntries)
			{
				return;
			}
			this.CloseDropDown();
			bool flag = false;
			if ((this.Edit.Focused || this.DialogButton.Focused || this.DropDownButton.Focused) && base.IsHandleCreated && base.Visible && base.Enabled)
			{
				flag = IntPtr.Zero != UnsafeNativeMethods.SetFocus(new HandleRef(this, base.Handle));
			}
			try
			{
				this.Edit.DontFocus = true;
				if (this.Edit.Focused && !flag)
				{
					flag = this.FocusInternal();
				}
				this.Edit.Visible = false;
				this.Edit.SelectionStart = 0;
				this.Edit.SelectionLength = 0;
				if (this.DialogButton.Focused && !flag)
				{
					flag = this.FocusInternal();
				}
				this.DialogButton.Visible = false;
				if (this.DropDownButton.Focused && !flag)
				{
					flag = this.FocusInternal();
				}
				this.DropDownButton.Visible = false;
				this.currentEditor = null;
			}
			finally
			{
				this.Edit.DontFocus = false;
			}
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00157BA8 File Offset: 0x00155DA8
		protected virtual void CommonEditorSetup(Control ctl)
		{
			ctl.Visible = false;
			base.Controls.Add(ctl);
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00157BC0 File Offset: 0x00155DC0
		protected virtual void CommonEditorUse(Control ctl, Rectangle rectTarget)
		{
			Rectangle bounds = ctl.Bounds;
			Rectangle clientRectangle = base.ClientRectangle;
			clientRectangle.Inflate(-1, -1);
			try
			{
				rectTarget = Rectangle.Intersect(clientRectangle, rectTarget);
				if (!rectTarget.IsEmpty)
				{
					if (!rectTarget.Equals(bounds))
					{
						ctl.SetBounds(rectTarget.X, rectTarget.Y, rectTarget.Width, rectTarget.Height);
					}
					ctl.Visible = true;
				}
			}
			catch
			{
				rectTarget = Rectangle.Empty;
			}
			if (rectTarget.IsEmpty)
			{
				ctl.Visible = false;
			}
			this.currentEditor = ctl;
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x00157C68 File Offset: 0x00155E68
		private int CountPropsFromOutline(GridEntryCollection rgipes)
		{
			if (rgipes == null)
			{
				return 0;
			}
			int num = rgipes.Count;
			for (int i = 0; i < rgipes.Count; i++)
			{
				if (((GridEntry)rgipes[i]).InternalExpanded)
				{
					num += this.CountPropsFromOutline(((GridEntry)rgipes[i]).Children);
				}
			}
			return num;
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00157CC0 File Offset: 0x00155EC0
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new PropertyGridView.PropertyGridViewAccessibleObject(this, this.ownerGrid);
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00157CD0 File Offset: 0x00155ED0
		private Bitmap CreateResizedBitmap(string icon, int width, int height)
		{
			Bitmap bitmap = null;
			int num = width;
			int num2 = height;
			try
			{
				if (DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					num = base.LogicalToDeviceUnits(width);
					num2 = base.LogicalToDeviceUnits(height);
				}
				else if (DpiHelper.IsScalingRequired)
				{
					num = DpiHelper.LogicalToDeviceUnitsX(width);
					num2 = DpiHelper.LogicalToDeviceUnitsY(height);
				}
				bitmap = PropertyGridView.GetBitmapFromIcon(icon, num, num2);
			}
			catch (Exception ex)
			{
				bitmap = new Bitmap(num, num2);
			}
			return bitmap;
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x00157D38 File Offset: 0x00155F38
		protected virtual void CreateUI()
		{
			this.UpdateUIBasedOnFont(false);
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x00157D44 File Offset: 0x00155F44
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.scrollBar != null)
				{
					this.scrollBar.Dispose();
				}
				if (this.listBox != null)
				{
					this.listBox.Dispose();
				}
				if (this.dropDownHolder != null)
				{
					this.dropDownHolder.Dispose();
				}
				this.scrollBar = null;
				this.listBox = null;
				this.dropDownHolder = null;
				this.ownerGrid = null;
				this.topLevelGridEntries = null;
				this.allGridEntries = null;
				this.serviceProvider = null;
				this.topHelpService = null;
				if (this.helpService != null && this.helpService is IDisposable)
				{
					((IDisposable)this.helpService).Dispose();
				}
				this.helpService = null;
				if (this.edit != null)
				{
					this.edit.Dispose();
					this.edit = null;
				}
				if (this.fontBold != null)
				{
					this.fontBold.Dispose();
					this.fontBold = null;
				}
				if (this.btnDropDown != null)
				{
					this.btnDropDown.Dispose();
					this.btnDropDown = null;
				}
				if (this.btnDialog != null)
				{
					this.btnDialog.Dispose();
					this.btnDialog = null;
				}
				if (this.toolTip != null)
				{
					this.toolTip.Dispose();
					this.toolTip = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00157E7D File Offset: 0x0015607D
		public void DoCopyCommand()
		{
			if (this.CanCopy)
			{
				if (this.Edit.Focused)
				{
					this.Edit.Copy();
					return;
				}
				if (this.selectedGridEntry != null)
				{
					Clipboard.SetDataObject(this.selectedGridEntry.GetPropertyTextValue());
				}
			}
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x00157EB8 File Offset: 0x001560B8
		public void DoCutCommand()
		{
			if (this.CanCut)
			{
				this.DoCopyCommand();
				if (this.Edit.Visible)
				{
					this.Edit.Cut();
				}
			}
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00157EE0 File Offset: 0x001560E0
		public void DoPasteCommand()
		{
			if (this.CanPaste && this.Edit.Visible)
			{
				if (this.Edit.Focused)
				{
					this.Edit.Paste();
					return;
				}
				IDataObject dataObject = Clipboard.GetDataObject();
				if (dataObject != null)
				{
					string text = (string)dataObject.GetData(typeof(string));
					if (text != null)
					{
						this.Edit.FocusInternal();
						this.Edit.Text = text;
						this.SetCommitError(0, true);
					}
				}
			}
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x00157F5D File Offset: 0x0015615D
		public void DoUndoCommand()
		{
			if (this.CanUndo && this.Edit.Visible)
			{
				this.Edit.SendMessage(772, 0, 0);
			}
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x00157F88 File Offset: 0x00156188
		internal void DumpPropsToConsole(GridEntry entry, string prefix)
		{
			Type type = entry.PropertyType;
			if (entry.PropertyValue != null)
			{
				type = entry.PropertyValue.GetType();
			}
			Console.WriteLine(string.Concat(new string[]
			{
				prefix,
				entry.PropertyLabel,
				", value type=",
				(type == null) ? "(null)" : type.FullName,
				", value=",
				(entry.PropertyValue == null) ? "(null)" : entry.PropertyValue.ToString(),
				", flags=",
				entry.Flags.ToString(CultureInfo.InvariantCulture),
				", TypeConverter=",
				(entry.TypeConverter == null) ? "(null)" : entry.TypeConverter.GetType().FullName,
				", UITypeEditor=",
				(entry.UITypeEditor == null) ? "(null)" : entry.UITypeEditor.GetType().FullName
			}));
			GridEntryCollection children = entry.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					GridEntry gridEntry = (GridEntry)obj;
					this.DumpPropsToConsole(gridEntry, prefix + "\t");
				}
			}
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x001580EC File Offset: 0x001562EC
		private int GetIPELabelIndent(GridEntry gridEntry)
		{
			return gridEntry.PropertyLabelIndent + 1;
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x001580F8 File Offset: 0x001562F8
		private int GetIPELabelLength(Graphics g, GridEntry gridEntry)
		{
			SizeF sizeF = PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, g, gridEntry.PropertyLabel, this.Font);
			Size size = Size.Ceiling(sizeF);
			return this.ptOurLocation.X + this.GetIPELabelIndent(gridEntry) + size.Width;
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00158140 File Offset: 0x00156340
		private bool IsIPELabelLong(Graphics g, GridEntry gridEntry)
		{
			if (gridEntry == null)
			{
				return false;
			}
			int ipelabelLength = this.GetIPELabelLength(g, gridEntry);
			return ipelabelLength > this.ptOurLocation.X + this.InternalLabelWidth;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x00158170 File Offset: 0x00156370
		protected virtual void DrawLabel(Graphics g, int row, Rectangle rect, bool selected, bool fLongLabelRequest, ref Rectangle clipRect)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow == null || rect.IsEmpty)
			{
				return;
			}
			Point point = new Point(rect.X, rect.Y);
			Rectangle rectangle = Rectangle.Intersect(rect, clipRect);
			if (rectangle.IsEmpty)
			{
				return;
			}
			this.AdjustOrigin(g, point, ref rect);
			rectangle.Offset(-point.X, -point.Y);
			try
			{
				bool flag = false;
				int ipelabelIndent = this.GetIPELabelIndent(gridEntryFromRow);
				if (fLongLabelRequest)
				{
					int ipelabelLength = this.GetIPELabelLength(g, gridEntryFromRow);
					flag = this.IsIPELabelLong(g, gridEntryFromRow);
				}
				gridEntryFromRow.PaintLabel(g, rect, rectangle, selected, flag);
			}
			catch (Exception ex)
			{
			}
			finally
			{
				this.ResetOrigin(g);
			}
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0015823C File Offset: 0x0015643C
		protected virtual void DrawValueEntry(Graphics g, int row, ref Rectangle clipRect)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow == null)
			{
				return;
			}
			Rectangle rectangle = this.GetRectangle(row, 2);
			Point point = new Point(rectangle.X, rectangle.Y);
			Rectangle rectangle2 = Rectangle.Intersect(clipRect, rectangle);
			if (rectangle2.IsEmpty)
			{
				return;
			}
			this.AdjustOrigin(g, point, ref rectangle);
			rectangle2.Offset(-point.X, -point.Y);
			try
			{
				this.DrawValueEntry(g, rectangle, rectangle2, gridEntryFromRow, null, true);
			}
			catch
			{
			}
			finally
			{
				this.ResetOrigin(g);
			}
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x001582E0 File Offset: 0x001564E0
		private void DrawValueEntry(Graphics g, Rectangle rect, Rectangle clipRect, GridEntry gridEntry, object value, bool fetchValue)
		{
			this.DrawValue(g, rect, clipRect, gridEntry, value, false, true, fetchValue, true);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x00158300 File Offset: 0x00156500
		private void DrawValue(Graphics g, Rectangle rect, Rectangle clipRect, GridEntry gridEntry, object value, bool drawSelected, bool checkShouldSerialize, bool fetchValue, bool paintInPlace)
		{
			GridEntry.PaintValueFlags paintValueFlags = GridEntry.PaintValueFlags.None;
			if (drawSelected)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.DrawSelected;
			}
			if (checkShouldSerialize)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.CheckShouldSerialize;
			}
			if (fetchValue)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.FetchValue;
			}
			if (paintInPlace)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.PaintInPlace;
			}
			gridEntry.PaintValue(value, g, rect, clipRect, paintValueFlags);
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0015833C File Offset: 0x0015653C
		private void F4Selection(bool popupModalDialog)
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return;
			}
			if (this.errorState != 0 && this.Edit.Visible)
			{
				this.Edit.FocusInternal();
				return;
			}
			if (this.DropDownButton.Visible)
			{
				this.PopupDialog(this.selectedRow);
				return;
			}
			if (!this.DialogButton.Visible)
			{
				if (this.Edit.Visible)
				{
					this.Edit.FocusInternal();
					this.SelectEdit(false);
				}
				return;
			}
			if (popupModalDialog)
			{
				this.PopupDialog(this.selectedRow);
				return;
			}
			this.DialogButton.FocusInternal();
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x001583E0 File Offset: 0x001565E0
		public void DoubleClickRow(int row, bool toggleExpand, int type)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow == null)
			{
				return;
			}
			if (!toggleExpand || type == 2)
			{
				try
				{
					bool flag = gridEntryFromRow.DoubleClickPropertyValue();
					if (flag)
					{
						this.SelectRow(row);
						return;
					}
				}
				catch (Exception ex)
				{
					this.SetCommitError(1);
					this.ShowInvalidMessage(gridEntryFromRow.PropertyLabel, null, ex);
					return;
				}
			}
			this.SelectGridEntry(gridEntryFromRow, true);
			if (type == 1 && toggleExpand && gridEntryFromRow.Expandable)
			{
				this.SetExpand(gridEntryFromRow, !gridEntryFromRow.InternalExpanded);
				return;
			}
			if (gridEntryFromRow.IsValueEditable && gridEntryFromRow.Enumerable)
			{
				int num = this.GetCurrentValueIndex(gridEntryFromRow);
				if (num != -1)
				{
					object[] propertyValueList = gridEntryFromRow.GetPropertyValueList();
					if (propertyValueList == null || num >= propertyValueList.Length - 1)
					{
						num = 0;
					}
					else
					{
						num++;
					}
					this.CommitValue(propertyValueList[num]);
					this.SelectRow(this.selectedRow);
					this.Refresh();
					return;
				}
			}
			if (this.Edit.Visible)
			{
				this.Edit.FocusInternal();
				this.SelectEdit(false);
				return;
			}
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x001584E8 File Offset: 0x001566E8
		public Font GetBaseFont()
		{
			return this.Font;
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x001584F0 File Offset: 0x001566F0
		public Font GetBoldFont()
		{
			if (this.fontBold == null)
			{
				this.fontBold = new Font(this.Font, FontStyle.Bold);
			}
			return this.fontBold;
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x00158512 File Offset: 0x00156712
		internal IntPtr GetBaseHfont()
		{
			if (this.baseHfont == IntPtr.Zero)
			{
				this.baseHfont = this.GetBaseFont().ToHfont();
			}
			return this.baseHfont;
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x00158540 File Offset: 0x00156740
		internal GridEntry GetElementFromPoint(int x, int y)
		{
			Point point = new Point(x, y);
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			GridEntry[] array = new GridEntry[gridEntryCollection.Count];
			try
			{
				this.GetGridEntriesFromOutline(gridEntryCollection, 0, gridEntryCollection.Count - 1, array);
			}
			catch (Exception ex)
			{
			}
			foreach (GridEntry gridEntry in array)
			{
				if (gridEntry.AccessibilityObject.Bounds.Contains(point))
				{
					return gridEntry;
				}
			}
			return null;
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x001585CC File Offset: 0x001567CC
		internal IntPtr GetBoldHfont()
		{
			if (this.boldHfont == IntPtr.Zero)
			{
				this.boldHfont = this.GetBoldFont().ToHfont();
			}
			return this.boldHfont;
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x001585F7 File Offset: 0x001567F7
		private bool GetFlag(short flag)
		{
			return (this.flags & flag) != 0;
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x00158604 File Offset: 0x00156804
		public virtual Color GetLineColor()
		{
			return this.ownerGrid.LineColor;
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x00158614 File Offset: 0x00156814
		public virtual Brush GetLineBrush(Graphics g)
		{
			if (this.ownerGrid.lineBrush == null)
			{
				Color nearestColor = g.GetNearestColor(this.ownerGrid.LineColor);
				this.ownerGrid.lineBrush = new SolidBrush(nearestColor);
			}
			return this.ownerGrid.lineBrush;
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0015865C File Offset: 0x0015685C
		public virtual Color GetSelectedItemWithFocusForeColor()
		{
			return this.ownerGrid.SelectedItemWithFocusForeColor;
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x00158669 File Offset: 0x00156869
		public virtual Color GetSelectedItemWithFocusBackColor()
		{
			return this.ownerGrid.SelectedItemWithFocusBackColor;
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x00158678 File Offset: 0x00156878
		public virtual Brush GetSelectedItemWithFocusBackBrush(Graphics g)
		{
			if (this.ownerGrid.selectedItemWithFocusBackBrush == null)
			{
				Color nearestColor = g.GetNearestColor(this.ownerGrid.SelectedItemWithFocusBackColor);
				this.ownerGrid.selectedItemWithFocusBackBrush = new SolidBrush(nearestColor);
			}
			return this.ownerGrid.selectedItemWithFocusBackBrush;
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x001586C0 File Offset: 0x001568C0
		public virtual IntPtr GetHostHandle()
		{
			return base.Handle;
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x001586C8 File Offset: 0x001568C8
		public virtual int GetLabelWidth()
		{
			return this.InternalLabelWidth;
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x060052EF RID: 21231 RVA: 0x001586D0 File Offset: 0x001568D0
		internal bool IsExplorerTreeSupported
		{
			get
			{
				return this.ownerGrid.CanShowVisualStyleGlyphs && UnsafeNativeMethods.IsVista && VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x001586F0 File Offset: 0x001568F0
		public virtual int GetOutlineIconSize()
		{
			if (this.IsExplorerTreeSupported)
			{
				return this.outlineSizeExplorerTreeStyle;
			}
			return this.outlineSize;
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x00158707 File Offset: 0x00156907
		public virtual int GetGridEntryHeight()
		{
			return this.RowHeight;
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00158710 File Offset: 0x00156910
		internal int GetPropertyLocation(string propName, bool getXY, bool rowValue)
		{
			if (this.allGridEntries != null && this.allGridEntries.Count > 0)
			{
				int i = 0;
				while (i < this.allGridEntries.Count)
				{
					if (string.Compare(propName, this.allGridEntries.GetEntry(i).PropertyLabel, true, CultureInfo.InvariantCulture) == 0)
					{
						if (!getXY)
						{
							return i;
						}
						int rowFromGridEntry = this.GetRowFromGridEntry(this.allGridEntries.GetEntry(i));
						if (rowFromGridEntry < 0 || rowFromGridEntry >= this.visibleRows)
						{
							return -1;
						}
						Rectangle rectangle = this.GetRectangle(rowFromGridEntry, rowValue ? 2 : 1);
						return (rectangle.Y << 16) | (rectangle.X & 65535);
					}
					else
					{
						i++;
					}
				}
			}
			return -1;
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x001587BE File Offset: 0x001569BE
		public new object GetService(Type classService)
		{
			if (classService == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			if (this.ServiceProvider != null)
			{
				return this.serviceProvider.GetService(classService);
			}
			return null;
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00012E4E File Offset: 0x0001104E
		public virtual int GetSplitterWidth()
		{
			return 1;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x001587EA File Offset: 0x001569EA
		public virtual int GetTotalWidth()
		{
			return this.GetLabelWidth() + this.GetSplitterWidth() + this.GetValueWidth();
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00158800 File Offset: 0x00156A00
		public virtual int GetValuePaintIndent()
		{
			return this.paintIndent;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x00158808 File Offset: 0x00156A08
		public virtual int GetValuePaintWidth()
		{
			return this.paintWidth;
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual int GetValueStringIndent()
		{
			return 0;
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00158810 File Offset: 0x00156A10
		public virtual int GetValueWidth()
		{
			return (int)((double)this.InternalLabelWidth * (this.labelRatio - 1.0));
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0015882C File Offset: 0x00156A2C
		private void SetDropDownWindowPosition(Rectangle rect, bool setBounds = false)
		{
			Size size = this.dropDownHolder.Size;
			size.Width = Math.Max(rect.Width + 1, size.Width);
			Point point = base.PointToScreen(new Point(0, 0));
			Rectangle workingArea = Screen.FromControl(this.Edit).WorkingArea;
			point.X = Math.Min(workingArea.X + workingArea.Width - size.Width, Math.Max(workingArea.X, point.X + rect.X + rect.Width - size.Width));
			point.Y += rect.Y;
			if (workingArea.Y + workingArea.Height < size.Height + point.Y + this.Edit.Height)
			{
				point.Y -= size.Height;
				this.dropDownHolder.ResizeUp = true;
			}
			else
			{
				point.Y += rect.Height + 1;
				this.dropDownHolder.ResizeUp = false;
			}
			int num = 20;
			if (point.X == 0 && point.Y == 0)
			{
				num |= 2;
			}
			if (base.Width == size.Width && base.Height == size.Height)
			{
				num |= 1;
			}
			SafeNativeMethods.SetWindowPos(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), NativeMethods.NullHandleRef, point.X, point.Y, size.Width, size.Height, num);
			if (setBounds)
			{
				this.dropDownHolder.SetBounds(point.X, point.Y, size.Width, size.Height);
			}
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x001589F8 File Offset: 0x00156BF8
		public void DropDownControl(Control ctl)
		{
			if (this.dropDownHolder == null)
			{
				this.dropDownHolder = new PropertyGridView.DropDownHolder(this);
			}
			this.dropDownHolder.Visible = false;
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				Rectangle rectangle = this.GetRectangle(this.selectedRow, 2);
				this.dropDownHolder.SuspendAllLayout(this.dropDownHolder);
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), -8, new HandleRef(this, base.Handle));
				this.SetDropDownWindowPosition(rectangle, false);
				this.dropDownHolder.SetComponent(ctl, this.GetFlag(1024));
				this.SetDropDownWindowPosition(rectangle, false);
				this.dropDownHolder.ResumeAllLayout(this.dropDownHolder, true);
				SafeNativeMethods.ShowWindow(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), 8);
				this.SetDropDownWindowPosition(rectangle, true);
			}
			else
			{
				this.dropDownHolder.SetComponent(ctl, this.GetFlag(1024));
				Rectangle rectangle2 = this.GetRectangle(this.selectedRow, 2);
				Size size = this.dropDownHolder.Size;
				Point point = base.PointToScreen(new Point(0, 0));
				Rectangle workingArea = Screen.FromControl(this.Edit).WorkingArea;
				size.Width = Math.Max(rectangle2.Width + 1, size.Width);
				point.X = Math.Min(workingArea.X + workingArea.Width - size.Width, Math.Max(workingArea.X, point.X + rectangle2.X + rectangle2.Width - size.Width));
				point.Y += rectangle2.Y;
				if (workingArea.Y + workingArea.Height < size.Height + point.Y + this.Edit.Height)
				{
					point.Y -= size.Height;
					this.dropDownHolder.ResizeUp = true;
				}
				else
				{
					point.Y += rectangle2.Height + 1;
					this.dropDownHolder.ResizeUp = false;
				}
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), -8, new HandleRef(this, base.Handle));
				this.dropDownHolder.SetBounds(point.X, point.Y, size.Width, size.Height);
				SafeNativeMethods.ShowWindow(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), 8);
			}
			this.Edit.Filter = true;
			this.dropDownHolder.Visible = true;
			this.dropDownHolder.FocusComponent();
			this.SelectEdit(false);
			if (AccessibilityImprovements.Level3)
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				if (gridEntryFromRow != null)
				{
					gridEntryFromRow.AccessibilityObject.RaiseAutomationEvent(20005);
					gridEntryFromRow.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Collapsed, UnsafeNativeMethods.ExpandCollapseState.Expanded);
				}
			}
			try
			{
				this.DropDownButton.IgnoreMouse = true;
				this.dropDownHolder.DoModalLoop();
			}
			finally
			{
				this.DropDownButton.IgnoreMouse = false;
			}
			if (this.selectedRow != -1)
			{
				this.FocusInternal();
				this.SelectRow(this.selectedRow);
			}
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00158D54 File Offset: 0x00156F54
		public virtual void DropDownDone()
		{
			this.CloseDropDown();
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00158D5C File Offset: 0x00156F5C
		public virtual void DropDownUpdate()
		{
			if (this.dropDownHolder != null && this.dropDownHolder.GetUsed())
			{
				int num = this.selectedRow;
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(num);
				this.Edit.Text = gridEntryFromRow.GetPropertyTextValue();
			}
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x00158D9E File Offset: 0x00156F9E
		public bool EnsurePendingChangesCommitted()
		{
			this.CloseDropDown();
			return this.Commit();
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00158DAC File Offset: 0x00156FAC
		private bool FilterEditWndProc(ref Message m)
		{
			if (this.dropDownHolder != null && this.dropDownHolder.Visible && m.Msg == 256 && (int)m.WParam != 9)
			{
				Control component = this.dropDownHolder.Component;
				if (component != null)
				{
					m.Result = component.SendMessage(m.Msg, m.WParam, m.LParam);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x00158E1C File Offset: 0x0015701C
		private bool FilterReadOnlyEditKeyPress(char keyChar)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow.Enumerable && gridEntryFromRow.IsValueEditable)
			{
				int currentValueIndex = this.GetCurrentValueIndex(gridEntryFromRow);
				object[] propertyValueList = gridEntryFromRow.GetPropertyValueList();
				string text = new string(new char[] { keyChar });
				for (int i = 0; i < propertyValueList.Length; i++)
				{
					object obj = propertyValueList[(i + currentValueIndex + 1) % propertyValueList.Length];
					string propertyTextValue = gridEntryFromRow.GetPropertyTextValue(obj);
					if (propertyTextValue != null && propertyTextValue.Length > 0 && string.Compare(propertyTextValue.Substring(0, 1), text, true, CultureInfo.InvariantCulture) == 0)
					{
						this.CommitValue(obj);
						if (this.Edit.Focused)
						{
							this.SelectEdit(false);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x00158EDC File Offset: 0x001570DC
		public virtual bool WillFilterKeyPress(char charPressed)
		{
			if (!this.Edit.Visible)
			{
				return false;
			}
			Keys modifierKeys = Control.ModifierKeys;
			if ((modifierKeys & ~Keys.Shift) != Keys.None)
			{
				return false;
			}
			if (this.selectedGridEntry != null)
			{
				if (charPressed == '\t')
				{
					return false;
				}
				switch (charPressed)
				{
				case '*':
				case '+':
				case '-':
					return !this.selectedGridEntry.Expandable;
				}
			}
			return true;
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x00158F44 File Offset: 0x00157144
		public void FilterKeyPress(char keyChar)
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return;
			}
			this.Edit.FilterKeyPress(keyChar);
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x00158F70 File Offset: 0x00157170
		private GridEntry FindEquivalentGridEntry(GridEntryCollection ipeHier)
		{
			if (ipeHier == null || ipeHier.Count == 0)
			{
				return null;
			}
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection == null || gridEntryCollection.Count == 0)
			{
				return null;
			}
			GridEntry gridEntry = null;
			int num = 0;
			int num2 = gridEntryCollection.Count;
			for (int i = 0; i < ipeHier.Count; i++)
			{
				if (ipeHier[i] != null)
				{
					if (gridEntry != null)
					{
						int count = gridEntryCollection.Count;
						if (!gridEntry.InternalExpanded)
						{
							this.SetExpand(gridEntry, true);
							gridEntryCollection = this.GetAllGridEntries();
						}
						num2 = gridEntry.VisibleChildCount;
					}
					int num3 = num;
					gridEntry = null;
					while (num < gridEntryCollection.Count && num - num3 <= num2)
					{
						if (ipeHier.GetEntry(i).NonParentEquals(gridEntryCollection[num]))
						{
							gridEntry = gridEntryCollection.GetEntry(num);
							num++;
							break;
						}
						num++;
					}
					if (gridEntry == null)
					{
						break;
					}
				}
			}
			return gridEntry;
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x00159038 File Offset: 0x00157238
		protected virtual Point FindPosition(int x, int y)
		{
			if (this.RowHeight == -1)
			{
				return PropertyGridView.InvalidPosition;
			}
			Size ourSize = this.GetOurSize();
			if (x < 0 || x > ourSize.Width + this.ptOurLocation.X)
			{
				return PropertyGridView.InvalidPosition;
			}
			Point point = new Point(1, 0);
			if (x > this.InternalLabelWidth + this.ptOurLocation.X)
			{
				point.X = 2;
			}
			point.Y = (y - this.ptOurLocation.Y) / (1 + this.RowHeight);
			return point;
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x001590BF File Offset: 0x001572BF
		public virtual void Flush()
		{
			if (this.Commit() && this.Edit.Focused)
			{
				this.FocusInternal();
			}
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x001590DD File Offset: 0x001572DD
		private GridEntryCollection GetAllGridEntries()
		{
			return this.GetAllGridEntries(false);
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x001590E8 File Offset: 0x001572E8
		private GridEntryCollection GetAllGridEntries(bool fUpdateCache)
		{
			if (this.visibleRows == -1 || this.totalProps == -1 || !this.HasEntries)
			{
				return null;
			}
			if (this.allGridEntries != null && !fUpdateCache)
			{
				return this.allGridEntries;
			}
			GridEntry[] array = new GridEntry[this.totalProps];
			try
			{
				this.GetGridEntriesFromOutline(this.topLevelGridEntries, 0, 0, array);
			}
			catch (Exception ex)
			{
			}
			this.allGridEntries = new GridEntryCollection(null, array);
			this.AddGridEntryEvents(this.allGridEntries, 0, -1);
			return this.allGridEntries;
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x00159178 File Offset: 0x00157378
		private int GetCurrentValueIndex(GridEntry gridEntry)
		{
			if (!gridEntry.Enumerable)
			{
				return -1;
			}
			try
			{
				object[] propertyValueList = gridEntry.GetPropertyValueList();
				object propertyValue = gridEntry.PropertyValue;
				string text = gridEntry.TypeConverter.ConvertToString(gridEntry, propertyValue);
				if (propertyValueList != null && propertyValueList.Length != 0)
				{
					int num = -1;
					int num2 = -1;
					for (int i = 0; i < propertyValueList.Length; i++)
					{
						object obj = propertyValueList[i];
						string text2 = gridEntry.TypeConverter.ConvertToString(obj);
						if (propertyValue == obj || string.Compare(text, text2, true, CultureInfo.InvariantCulture) == 0)
						{
							num = i;
						}
						if (propertyValue != null && obj != null && obj.Equals(propertyValue))
						{
							num2 = i;
						}
						if (num == num2 && num != -1)
						{
							return num;
						}
					}
					if (num != -1)
					{
						return num;
					}
					if (num2 != -1)
					{
						return num2;
					}
				}
			}
			catch (Exception ex)
			{
			}
			return -1;
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x00159254 File Offset: 0x00157454
		public virtual int GetDefaultOutlineIndent()
		{
			return 10;
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x00159258 File Offset: 0x00157458
		private IHelpService GetHelpService()
		{
			if (this.helpService == null && this.ServiceProvider != null)
			{
				this.topHelpService = (IHelpService)this.ServiceProvider.GetService(typeof(IHelpService));
				if (this.topHelpService != null)
				{
					IHelpService helpService = this.topHelpService.CreateLocalContext(HelpContextType.ToolWindowSelection);
					if (helpService != null)
					{
						this.helpService = helpService;
					}
				}
			}
			return this.helpService;
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x001592BC File Offset: 0x001574BC
		public virtual int GetScrollOffset()
		{
			if (this.scrollBar == null)
			{
				return 0;
			}
			return this.ScrollBar.Value;
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x001592E0 File Offset: 0x001574E0
		private GridEntryCollection GetGridEntryHierarchy(GridEntry gridEntry)
		{
			if (gridEntry == null)
			{
				return null;
			}
			int num = gridEntry.PropertyDepth;
			if (num > 0)
			{
				GridEntry[] array = new GridEntry[num + 1];
				while (gridEntry != null && num >= 0)
				{
					array[num] = gridEntry;
					gridEntry = gridEntry.ParentGridEntry;
					num = gridEntry.PropertyDepth;
				}
				return new GridEntryCollection(null, array);
			}
			return new GridEntryCollection(null, new GridEntry[] { gridEntry });
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0015933A File Offset: 0x0015753A
		private GridEntry GetGridEntryFromRow(int row)
		{
			return this.GetGridEntryFromOffset(row + this.GetScrollOffset());
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0015934C File Offset: 0x0015754C
		private GridEntry GetGridEntryFromOffset(int offset)
		{
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection != null && offset >= 0 && offset < gridEntryCollection.Count)
			{
				return gridEntryCollection.GetEntry(offset);
			}
			return null;
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x0015937C File Offset: 0x0015757C
		private int GetGridEntriesFromOutline(GridEntryCollection rgipe, int cCur, int cTarget, GridEntry[] rgipeTarget)
		{
			if (rgipe == null || rgipe.Count == 0)
			{
				return cCur;
			}
			cCur--;
			for (int i = 0; i < rgipe.Count; i++)
			{
				cCur++;
				if (cCur >= cTarget + rgipeTarget.Length)
				{
					break;
				}
				GridEntry entry = rgipe.GetEntry(i);
				if (cCur >= cTarget)
				{
					rgipeTarget[cCur - cTarget] = entry;
				}
				if (entry.InternalExpanded)
				{
					GridEntryCollection children = entry.Children;
					if (children != null && children.Count > 0)
					{
						cCur = this.GetGridEntriesFromOutline(children, cCur + 1, cTarget, rgipeTarget);
					}
				}
			}
			return cCur;
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x001593F8 File Offset: 0x001575F8
		private Size GetOurSize()
		{
			Size clientSize = base.ClientSize;
			if (clientSize.Width == 0)
			{
				Size size = base.Size;
				if (size.Width > 10)
				{
					clientSize.Width = size.Width;
					clientSize.Height = size.Height;
				}
			}
			if (!this.GetScrollbarHidden())
			{
				Size size2 = this.ScrollBar.Size;
				clientSize.Width -= size2.Width;
			}
			clientSize.Width -= 2;
			clientSize.Height -= 2;
			return clientSize;
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x0015948C File Offset: 0x0015768C
		public Rectangle GetRectangle(int row, int flRow)
		{
			Rectangle rectangle = new Rectangle(0, 0, 0, 0);
			Size ourSize = this.GetOurSize();
			rectangle.X = this.ptOurLocation.X;
			bool flag = (flRow & 1) != 0;
			bool flag2 = (flRow & 2) != 0;
			if (flag && flag2)
			{
				rectangle.X = 1;
				rectangle.Width = ourSize.Width - 1;
			}
			else if (flag)
			{
				rectangle.X = 1;
				rectangle.Width = this.InternalLabelWidth - 1;
			}
			else if (flag2)
			{
				rectangle.X = this.ptOurLocation.X + this.InternalLabelWidth;
				rectangle.Width = ourSize.Width - this.InternalLabelWidth;
			}
			rectangle.Y = row * (this.RowHeight + 1) + 1 + this.ptOurLocation.Y;
			rectangle.Height = this.RowHeight;
			return rectangle;
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x00159564 File Offset: 0x00157764
		private int GetRowFromGridEntry(GridEntry gridEntry)
		{
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntry == null || gridEntryCollection == null)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < gridEntryCollection.Count; i++)
			{
				if (gridEntry == gridEntryCollection[i])
				{
					return i - this.GetScrollOffset();
				}
				if (num == -1 && gridEntry.Equals(gridEntryCollection[i]))
				{
					num = i - this.GetScrollOffset();
				}
			}
			if (num != -1)
			{
				return num;
			}
			return -1 - this.GetScrollOffset();
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x001595D0 File Offset: 0x001577D0
		internal int GetRowFromGridEntryInternal(GridEntry gridEntry)
		{
			return this.GetRowFromGridEntry(gridEntry);
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x001595D9 File Offset: 0x001577D9
		public virtual bool GetInPropertySet()
		{
			return this.GetFlag(16);
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x001595E3 File Offset: 0x001577E3
		protected virtual bool GetScrollbarHidden()
		{
			return this.scrollBar == null || !this.ScrollBar.Visible;
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x00159600 File Offset: 0x00157800
		public virtual string GetTestingInfo(int entry)
		{
			GridEntry gridEntry = ((entry < 0) ? this.GetGridEntryFromRow(this.selectedRow) : this.GetGridEntryFromOffset(entry));
			if (gridEntry == null)
			{
				return "";
			}
			return gridEntry.GetTestingInfo();
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x00159636 File Offset: 0x00157836
		public Color GetTextColor()
		{
			return this.ForeColor;
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x00159640 File Offset: 0x00157840
		private void LayoutWindow(bool invalidate)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			Size size = new Size(clientRectangle.Width, clientRectangle.Height);
			if (this.scrollBar != null)
			{
				Rectangle bounds = this.ScrollBar.Bounds;
				bounds.X = size.Width - bounds.Width - 1;
				bounds.Y = 1;
				bounds.Height = size.Height - 2;
				this.ScrollBar.Bounds = bounds;
			}
			if (invalidate)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x001596C4 File Offset: 0x001578C4
		internal void InvalidateGridEntryValue(GridEntry ge)
		{
			int rowFromGridEntry = this.GetRowFromGridEntry(ge);
			if (rowFromGridEntry != -1)
			{
				this.InvalidateRows(rowFromGridEntry, rowFromGridEntry, 2);
			}
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x001596E6 File Offset: 0x001578E6
		private void InvalidateRow(int row)
		{
			this.InvalidateRows(row, row, 3);
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x001596F1 File Offset: 0x001578F1
		private void InvalidateRows(int startRow, int endRow)
		{
			this.InvalidateRows(startRow, endRow, 3);
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x001596FC File Offset: 0x001578FC
		private void InvalidateRows(int startRow, int endRow, int type)
		{
			if (endRow == -1)
			{
				Rectangle rectangle = this.GetRectangle(startRow, type);
				rectangle.Height = base.Size.Height - rectangle.Y - 1;
				base.Invalidate(rectangle);
				return;
			}
			for (int i = startRow; i <= endRow; i++)
			{
				Rectangle rectangle = this.GetRectangle(i, type);
				base.Invalidate(rectangle);
			}
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x0015975C File Offset: 0x0015795C
		protected override bool IsInputKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys <= Keys.Return)
			{
				if (keys != Keys.Tab)
				{
					if (keys != Keys.Return)
					{
						goto IL_34;
					}
					if (this.Edit.Focused)
					{
						return false;
					}
					goto IL_34;
				}
			}
			else if (keys != Keys.Escape && keys != Keys.F4)
			{
				goto IL_34;
			}
			return false;
			IL_34:
			return base.IsInputKey(keyData);
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x001597A4 File Offset: 0x001579A4
		private bool IsMyChild(Control c)
		{
			if (c == this || c == null)
			{
				return false;
			}
			for (Control control = c.ParentInternal; control != null; control = control.ParentInternal)
			{
				if (control == this)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x001597D4 File Offset: 0x001579D4
		private bool IsScrollValueValid(int newValue)
		{
			return newValue != this.ScrollBar.Value && newValue >= 0 && newValue <= this.ScrollBar.Maximum && newValue + (this.ScrollBar.LargeChange - 1) < this.totalProps;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x00159810 File Offset: 0x00157A10
		internal bool IsSiblingControl(Control c1, Control c2)
		{
			Control parentInternal = c1.ParentInternal;
			for (Control control = c2.ParentInternal; control != null; control = control.ParentInternal)
			{
				if (parentInternal == control)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x00159840 File Offset: 0x00157A40
		private void MoveSplitterTo(int xpos)
		{
			int width = this.GetOurSize().Width;
			int x = this.ptOurLocation.X;
			int num = Math.Max(Math.Min(xpos, width - 10), this.GetOutlineIconSize() * 2);
			int internalLabelWidth = this.InternalLabelWidth;
			this.labelRatio = (double)width / (double)(num - x);
			this.SetConstants();
			if (this.selectedRow != -1)
			{
				this.SelectRow(this.selectedRow);
			}
			Rectangle clientRectangle = base.ClientRectangle;
			if (internalLabelWidth > this.InternalLabelWidth)
			{
				int num2 = this.InternalLabelWidth - (int)this.requiredLabelPaintMargin;
				base.Invalidate(new Rectangle(num2, 0, base.Size.Width - num2, base.Size.Height));
				return;
			}
			clientRectangle.X = internalLabelWidth - (int)this.requiredLabelPaintMargin;
			clientRectangle.Width -= clientRectangle.X;
			base.Invalidate(clientRectangle);
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x0015992C File Offset: 0x00157B2C
		private void OnBtnClick(object sender, EventArgs e)
		{
			if (this.GetFlag(256))
			{
				return;
			}
			if (sender == this.DialogButton && !this.Commit())
			{
				return;
			}
			this.SetCommitError(0);
			try
			{
				this.Commit();
				this.SetFlag(256, true);
				this.PopupDialog(this.selectedRow);
			}
			finally
			{
				this.SetFlag(256, false);
			}
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x001599A0 File Offset: 0x00157BA0
		private void OnBtnKeyDown(object sender, KeyEventArgs ke)
		{
			this.OnKeyDown(sender, ke);
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x001599AA File Offset: 0x00157BAA
		private void OnChildLostFocus(object sender, EventArgs e)
		{
			base.InvokeLostFocus(this, e);
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x001599B4 File Offset: 0x00157BB4
		private void OnDropDownButtonGotFocus(object sender, EventArgs e)
		{
			if (AccessibilityImprovements.Level3)
			{
				DropDownButton dropDownButton = sender as DropDownButton;
				if (dropDownButton != null)
				{
					dropDownButton.AccessibilityObject.SetFocus();
				}
			}
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x001599E0 File Offset: 0x00157BE0
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (e != null && !this.GetInPropertySet() && !this.Commit())
			{
				this.Edit.FocusInternal();
				return;
			}
			if (this.selectedGridEntry != null && this.GetRowFromGridEntry(this.selectedGridEntry) != -1)
			{
				this.selectedGridEntry.Focus = true;
				this.SelectGridEntry(this.selectedGridEntry, false);
			}
			else
			{
				this.SelectRow(0);
			}
			if (this.selectedGridEntry != null && this.selectedGridEntry.GetValueOwner() != null)
			{
				this.UpdateHelpAttributes(null, this.selectedGridEntry);
			}
			if (this.totalProps <= 0 && AccessibilityImprovements.Level1)
			{
				int num = 2 * this.offset_2Units;
				if (base.Size.Width > num && base.Size.Height > num)
				{
					using (Graphics graphics = base.CreateGraphicsInternal())
					{
						ControlPaint.DrawFocusRectangle(graphics, new Rectangle(this.offset_2Units, this.offset_2Units, base.Size.Width - num, base.Size.Height - num));
					}
				}
			}
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x00159B04 File Offset: 0x00157D04
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			SystemEvents.UserPreferenceChanged += this.OnSysColorChange;
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x00159B1E File Offset: 0x00157D1E
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.OnSysColorChange;
			if (this.toolTip != null && !base.RecreatingHandle)
			{
				this.toolTip.Dispose();
				this.toolTip = null;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x00159B5C File Offset: 0x00157D5C
		private void OnListChange(object sender, EventArgs e)
		{
			if (!this.DropDownListBox.InSetSelectedIndex())
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				this.Edit.Text = gridEntryFromRow.GetPropertyTextValue(this.DropDownListBox.SelectedItem);
				this.Edit.FocusInternal();
				this.SelectEdit(false);
			}
			this.SetFlag(64, true);
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x00159BBB File Offset: 0x00157DBB
		private void OnListMouseUp(object sender, MouseEventArgs me)
		{
			this.OnListClick(sender, me);
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x00159BC8 File Offset: 0x00157DC8
		private void OnListClick(object sender, EventArgs e)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (this.DropDownListBox.Items.Count == 0)
			{
				this.CommonEditorHide();
				this.SetCommitError(0);
				this.SelectRow(this.selectedRow);
				return;
			}
			object selectedItem = this.DropDownListBox.SelectedItem;
			this.SetFlag(64, false);
			if (selectedItem != null && !this.CommitText((string)selectedItem))
			{
				this.SetCommitError(0);
				this.SelectRow(this.selectedRow);
			}
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x00159C48 File Offset: 0x00157E48
		private void OnListDrawItem(object sender, DrawItemEventArgs die)
		{
			int index = die.Index;
			if (index < 0 || this.selectedGridEntry == null)
			{
				return;
			}
			string text = (string)this.DropDownListBox.Items[die.Index];
			die.DrawBackground();
			die.DrawFocusRectangle();
			Rectangle bounds = die.Bounds;
			bounds.Y++;
			bounds.X--;
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			try
			{
				this.DrawValue(die.Graphics, bounds, bounds, gridEntryFromRow, gridEntryFromRow.ConvertTextToValue(text), (die.State & DrawItemState.Selected) > DrawItemState.None, false, false, false);
			}
			catch (FormatException ex)
			{
				this.ShowFormatExceptionMessage(gridEntryFromRow.PropertyLabel, text, ex);
				if (this.DropDownListBox.IsHandleCreated)
				{
					this.DropDownListBox.Visible = false;
				}
			}
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x00159D28 File Offset: 0x00157F28
		private void OnListKeyDown(object sender, KeyEventArgs ke)
		{
			if (ke.KeyCode == Keys.Return)
			{
				this.OnListClick(null, null);
				if (this.selectedGridEntry != null)
				{
					this.selectedGridEntry.OnValueReturnKey();
				}
			}
			this.OnKeyDown(sender, ke);
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00159D58 File Offset: 0x00157F58
		protected override void OnLostFocus(EventArgs e)
		{
			if (e != null)
			{
				base.OnLostFocus(e);
			}
			if (this.FocusInside)
			{
				base.OnLostFocus(e);
				return;
			}
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow != null)
			{
				gridEntryFromRow.Focus = false;
				this.CommonEditorHide();
				this.InvalidateRow(this.selectedRow);
			}
			base.OnLostFocus(e);
			if (this.totalProps <= 0 && AccessibilityImprovements.Level1)
			{
				using (Graphics graphics = base.CreateGraphicsInternal())
				{
					Rectangle rectangle = new Rectangle(1, 1, base.Size.Width - 2, base.Size.Height - 2);
					graphics.FillRectangle(this.backgroundBrush, rectangle);
				}
			}
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00159E18 File Offset: 0x00158018
		private void OnEditChange(object sender, EventArgs e)
		{
			this.SetCommitError(0, this.Edit.Focused);
			this.ToolTip.ToolTip = "";
			this.ToolTip.Visible = false;
			if (!this.Edit.InSetText())
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				if (gridEntryFromRow != null && (gridEntryFromRow.Flags & 8) != 0)
				{
					this.Commit();
				}
			}
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x00159E84 File Offset: 0x00158084
		private void OnEditGotFocus(object sender, EventArgs e)
		{
			if (!this.Edit.Visible)
			{
				this.FocusInternal();
				return;
			}
			short num = this.errorState;
			if (num != 1)
			{
				if (num == 2)
				{
					return;
				}
				if (this.NeedsCommit)
				{
					this.SetCommitError(0, true);
				}
			}
			else if (this.Edit.Visible)
			{
				this.Edit.HookMouseDown = true;
			}
			if (this.selectedGridEntry != null && this.GetRowFromGridEntry(this.selectedGridEntry) != -1)
			{
				this.selectedGridEntry.Focus = true;
				this.InvalidateRow(this.selectedRow);
				(this.Edit.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.Focus);
				if (AccessibilityImprovements.Level3)
				{
					this.Edit.AccessibilityObject.SetFocus();
					return;
				}
			}
			else
			{
				this.SelectRow(0);
			}
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x00159F4C File Offset: 0x0015814C
		private bool ProcessEnumUpAndDown(GridEntry gridEntry, Keys keyCode, bool closeDropDown = true)
		{
			object propertyValue = gridEntry.PropertyValue;
			object[] propertyValueList = gridEntry.GetPropertyValueList();
			if (propertyValueList != null)
			{
				for (int i = 0; i < propertyValueList.Length; i++)
				{
					object obj = propertyValueList[i];
					if (propertyValue != null && obj != null && propertyValue.GetType() != obj.GetType() && gridEntry.TypeConverter.CanConvertTo(gridEntry, propertyValue.GetType()))
					{
						obj = gridEntry.TypeConverter.ConvertTo(gridEntry, CultureInfo.CurrentCulture, obj, propertyValue.GetType());
					}
					bool flag = propertyValue == obj || (propertyValue != null && propertyValue.Equals(obj));
					if (!flag && propertyValue is string && obj != null)
					{
						flag = string.Compare((string)propertyValue, obj.ToString(), true, CultureInfo.CurrentCulture) == 0;
					}
					if (flag)
					{
						object obj2;
						if (keyCode == Keys.Up)
						{
							if (i == 0)
							{
								return true;
							}
							obj2 = propertyValueList[i - 1];
						}
						else
						{
							if (i == propertyValueList.Length - 1)
							{
								return true;
							}
							obj2 = propertyValueList[i + 1];
						}
						this.CommitValue(gridEntry, obj2, closeDropDown);
						this.SelectEdit(false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x0015A04C File Offset: 0x0015824C
		private void OnEditKeyDown(object sender, KeyEventArgs ke)
		{
			if (!ke.Alt && (ke.KeyCode == Keys.Up || ke.KeyCode == Keys.Down))
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				if (!gridEntryFromRow.Enumerable || !gridEntryFromRow.IsValueEditable)
				{
					return;
				}
				ke.Handled = true;
				bool flag = this.ProcessEnumUpAndDown(gridEntryFromRow, ke.KeyCode, true);
				if (flag)
				{
					return;
				}
			}
			else if ((ke.KeyCode == Keys.Left || ke.KeyCode == Keys.Right) && (ke.Modifiers & ~Keys.Shift) != Keys.None)
			{
				return;
			}
			this.OnKeyDown(sender, ke);
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0015A0DC File Offset: 0x001582DC
		private void OnEditKeyPress(object sender, KeyPressEventArgs ke)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow == null)
			{
				return;
			}
			if (!gridEntryFromRow.IsTextEditable)
			{
				ke.Handled = this.FilterReadOnlyEditKeyPress(ke.KeyChar);
			}
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0015A114 File Offset: 0x00158314
		private void OnEditLostFocus(object sender, EventArgs e)
		{
			if (this.Edit.Focused || this.errorState == 2 || this.errorState == 1 || this.GetInPropertySet())
			{
				return;
			}
			if (this.dropDownHolder != null && this.dropDownHolder.Visible)
			{
				bool flag = false;
				IntPtr intPtr = UnsafeNativeMethods.GetForegroundWindow();
				while (intPtr != IntPtr.Zero)
				{
					if (intPtr == this.dropDownHolder.Handle)
					{
						flag = true;
					}
					intPtr = UnsafeNativeMethods.GetParent(new HandleRef(null, intPtr));
				}
				if (flag)
				{
					return;
				}
			}
			if (this.FocusInside)
			{
				return;
			}
			if (!this.Commit())
			{
				this.Edit.FocusInternal();
				return;
			}
			base.InvokeLostFocus(this, EventArgs.Empty);
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x0015A1C8 File Offset: 0x001583C8
		private void OnEditMouseDown(object sender, MouseEventArgs me)
		{
			if (!this.FocusInside)
			{
				this.SelectGridEntry(this.selectedGridEntry, false);
			}
			if (me.Clicks % 2 == 0)
			{
				this.DoubleClickRow(this.selectedRow, false, 2);
				this.Edit.SelectAll();
			}
			if (this.rowSelectTime == 0L)
			{
				return;
			}
			long ticks = DateTime.Now.Ticks;
			int num = (int)((ticks - this.rowSelectTime) / 10000L);
			if (num < SystemInformation.DoubleClickTime)
			{
				Point point = this.Edit.PointToScreen(new Point(me.X, me.Y));
				if (Math.Abs(point.X - this.rowSelectPos.X) < SystemInformation.DoubleClickSize.Width && Math.Abs(point.Y - this.rowSelectPos.Y) < SystemInformation.DoubleClickSize.Height)
				{
					this.DoubleClickRow(this.selectedRow, false, 2);
					this.Edit.SendMessage(514, 0, (me.Y << 16) | (me.X & 65535));
					this.Edit.SelectAll();
				}
				this.rowSelectPos = Point.Empty;
				this.rowSelectTime = 0L;
			}
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0015A301 File Offset: 0x00158501
		private bool OnF4(Control sender)
		{
			if (Control.ModifierKeys != Keys.None)
			{
				return false;
			}
			if (sender == this || sender == this.ownerGrid)
			{
				this.F4Selection(true);
			}
			else
			{
				this.UnfocusSelection();
			}
			return true;
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x0015A32C File Offset: 0x0015852C
		private bool OnEscape(Control sender)
		{
			if ((Control.ModifierKeys & (Keys.Control | Keys.Alt)) != Keys.None)
			{
				return false;
			}
			this.SetFlag(64, false);
			if (sender != this.Edit || !this.Edit.Focused)
			{
				if (sender != this)
				{
					this.CloseDropDown();
					this.FocusInternal();
				}
				return false;
			}
			if (this.errorState == 0)
			{
				this.Edit.Text = this.originalTextValue;
				this.FocusInternal();
				return true;
			}
			if (this.NeedsCommit)
			{
				bool flag = false;
				this.Edit.Text = this.originalTextValue;
				bool flag2 = true;
				if (this.selectedGridEntry != null)
				{
					string propertyTextValue = this.selectedGridEntry.GetPropertyTextValue();
					flag2 = this.originalTextValue != propertyTextValue && (!string.IsNullOrEmpty(this.originalTextValue) || !string.IsNullOrEmpty(propertyTextValue));
				}
				if (flag2)
				{
					try
					{
						flag = this.CommitText(this.originalTextValue);
						goto IL_CC;
					}
					catch
					{
						goto IL_CC;
					}
				}
				flag = true;
				IL_CC:
				if (!flag)
				{
					this.Edit.FocusInternal();
					this.SelectEdit(false);
					return true;
				}
			}
			this.SetCommitError(0);
			this.FocusInternal();
			return true;
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0015A450 File Offset: 0x00158650
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			this.OnKeyDown(this, ke);
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x0015A45C File Offset: 0x0015865C
		private void OnKeyDown(object sender, KeyEventArgs ke)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow == null)
			{
				return;
			}
			ke.Handled = true;
			bool control = ke.Control;
			bool shift = ke.Shift;
			bool flag = control && shift;
			bool alt = ke.Alt;
			Keys keyCode = ke.KeyCode;
			bool flag2 = false;
			if (keyCode == Keys.Tab && this.ProcessDialogKey(ke.KeyData))
			{
				ke.Handled = true;
				return;
			}
			if (keyCode == Keys.Down && alt && this.DropDownButton.Visible)
			{
				this.F4Selection(false);
				return;
			}
			if (keyCode == Keys.Up && alt && this.DropDownButton.Visible && this.dropDownHolder != null && this.dropDownHolder.Visible)
			{
				this.UnfocusSelection();
				return;
			}
			if (this.ToolTip.Visible)
			{
				this.ToolTip.ToolTip = "";
			}
			if (flag || sender == this || sender == this.ownerGrid)
			{
				if (keyCode <= Keys.C)
				{
					if (keyCode <= Keys.Delete)
					{
						if (keyCode != Keys.Return)
						{
							switch (keyCode)
							{
							case Keys.Prior:
							case Keys.Next:
							{
								bool flag3 = keyCode == Keys.Next;
								int num = (flag3 ? (this.visibleRows - 1) : (1 - this.visibleRows));
								int num2 = this.selectedRow;
								if (control && !shift)
								{
									return;
								}
								if (this.selectedRow != -1)
								{
									int scrollOffset = this.GetScrollOffset();
									this.SetScrollOffset(scrollOffset + num);
									this.SetConstants();
									if (this.GetScrollOffset() != scrollOffset + num)
									{
										if (flag3)
										{
											num2 = this.visibleRows - 1;
										}
										else
										{
											num2 = 0;
										}
									}
								}
								this.SelectRow(num2);
								this.Refresh();
								return;
							}
							case Keys.End:
							case Keys.Home:
							{
								GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
								int num3 = ((keyCode == Keys.Home) ? 0 : (gridEntryCollection.Count - 1));
								this.SelectGridEntry(gridEntryCollection.GetEntry(num3), true);
								return;
							}
							case Keys.Left:
								if (control)
								{
									this.MoveSplitterTo(this.InternalLabelWidth - 3);
									return;
								}
								if (gridEntryFromRow.InternalExpanded)
								{
									this.SetExpand(gridEntryFromRow, false);
									return;
								}
								this.SelectGridEntry(this.GetGridEntryFromRow(this.selectedRow - 1), true);
								return;
							case Keys.Up:
							case Keys.Down:
							{
								int num4 = ((keyCode == Keys.Up) ? (this.selectedRow - 1) : (this.selectedRow + 1));
								this.SelectGridEntry(this.GetGridEntryFromRow(num4), true);
								this.SetFlag(512, false);
								return;
							}
							case Keys.Right:
								if (control)
								{
									this.MoveSplitterTo(this.InternalLabelWidth + 3);
									return;
								}
								if (!gridEntryFromRow.Expandable)
								{
									this.SelectGridEntry(this.GetGridEntryFromRow(this.selectedRow + 1), true);
									return;
								}
								if (gridEntryFromRow.InternalExpanded)
								{
									GridEntryCollection children = gridEntryFromRow.Children;
									this.SelectGridEntry(children.GetEntry(0), true);
									return;
								}
								this.SetExpand(gridEntryFromRow, true);
								return;
							case Keys.Select:
							case Keys.Print:
							case Keys.Execute:
							case Keys.Snapshot:
								goto IL_440;
							case Keys.Insert:
								if (shift && !control && !alt)
								{
									flag2 = true;
									goto IL_3FC;
								}
								break;
							case Keys.Delete:
								if (shift && !control && !alt)
								{
									flag2 = true;
									goto IL_3D6;
								}
								goto IL_440;
							default:
								goto IL_440;
							}
						}
						else
						{
							if (gridEntryFromRow.Expandable)
							{
								this.SetExpand(gridEntryFromRow, !gridEntryFromRow.InternalExpanded);
								return;
							}
							gridEntryFromRow.OnValueReturnKey();
							return;
						}
					}
					else if (keyCode != Keys.D8)
					{
						if (keyCode != Keys.A)
						{
							if (keyCode != Keys.C)
							{
								goto IL_440;
							}
						}
						else
						{
							if (control && !alt && !shift && this.Edit.Visible)
							{
								this.Edit.FocusInternal();
								this.Edit.SelectAll();
								goto IL_440;
							}
							goto IL_440;
						}
					}
					else
					{
						if (shift)
						{
							goto IL_308;
						}
						goto IL_440;
					}
					if (control && !alt && !shift)
					{
						this.DoCopyCommand();
						return;
					}
					goto IL_440;
				}
				else if (keyCode <= Keys.X)
				{
					if (keyCode == Keys.V)
					{
						goto IL_3FC;
					}
					if (keyCode != Keys.X)
					{
						goto IL_440;
					}
					goto IL_3D6;
				}
				else
				{
					switch (keyCode)
					{
					case Keys.Multiply:
						goto IL_308;
					case Keys.Add:
					case Keys.Subtract:
						break;
					case Keys.Separator:
						goto IL_440;
					default:
						if (keyCode != Keys.Oemplus && keyCode != Keys.OemMinus)
						{
							goto IL_440;
						}
						break;
					}
					if (gridEntryFromRow.Expandable)
					{
						this.SetFlag(8, true);
						bool flag4 = keyCode == Keys.Add || keyCode == Keys.Oemplus;
						this.SetExpand(gridEntryFromRow, flag4);
						base.Invalidate();
						ke.Handled = true;
						return;
					}
					goto IL_440;
				}
				IL_308:
				this.SetFlag(8, true);
				this.RecursivelyExpand(gridEntryFromRow, true, true, 10);
				ke.Handled = false;
				return;
				IL_3D6:
				if (flag2 || (control && !alt && !shift))
				{
					Clipboard.SetDataObject(gridEntryFromRow.GetPropertyTextValue());
					this.CommitText("");
					return;
				}
				goto IL_440;
				IL_3FC:
				if (flag2 || (control && !alt && !shift))
				{
					this.DoPasteCommand();
				}
			}
			IL_440:
			if (gridEntryFromRow != null && ke.KeyData == (Keys)458819)
			{
				Clipboard.SetDataObject(gridEntryFromRow.GetTestingInfo());
				return;
			}
			if (AccessibilityImprovements.Level3 && this.selectedGridEntry != null && this.selectedGridEntry.Enumerable && this.dropDownHolder != null && this.dropDownHolder.Visible && (keyCode == Keys.Up || keyCode == Keys.Down))
			{
				this.ProcessEnumUpAndDown(this.selectedGridEntry, keyCode, false);
			}
			ke.Handled = false;
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x0015A91C File Offset: 0x00158B1C
		protected override void OnKeyPress(KeyPressEventArgs ke)
		{
			bool flag = false;
			bool flag2 = false;
			if ((!flag || !flag2) && this.WillFilterKeyPress(ke.KeyChar))
			{
				this.FilterKeyPress(ke.KeyChar);
			}
			this.SetFlag(8, false);
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0015A958 File Offset: 0x00158B58
		protected override void OnMouseDown(MouseEventArgs me)
		{
			if (me.Button == MouseButtons.Left && this.SplitterInside(me.X, me.Y) && this.totalProps != 0)
			{
				if (!this.Commit())
				{
					return;
				}
				if (me.Clicks == 2)
				{
					this.MoveSplitterTo(base.Width / 2);
					return;
				}
				this.UnfocusSelection();
				this.SetFlag(4, true);
				this.tipInfo = -1;
				base.CaptureInternal = true;
				return;
			}
			else
			{
				Point point = this.FindPosition(me.X, me.Y);
				if (point == PropertyGridView.InvalidPosition)
				{
					return;
				}
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(point.Y);
				if (gridEntryFromRow != null)
				{
					Rectangle rectangle = this.GetRectangle(point.Y, 1);
					this.lastMouseDown = new Point(me.X, me.Y);
					if (me.Button == MouseButtons.Left)
					{
						gridEntryFromRow.OnMouseClick(me.X - rectangle.X, me.Y - rectangle.Y, me.Clicks, me.Button);
					}
					else
					{
						this.SelectGridEntry(gridEntryFromRow, false);
					}
					this.lastMouseDown = PropertyGridView.InvalidPosition;
					gridEntryFromRow.Focus = true;
					this.SetFlag(512, false);
				}
				return;
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0015AA8C File Offset: 0x00158C8C
		protected override void OnMouseLeave(EventArgs e)
		{
			if (!this.GetFlag(4))
			{
				this.Cursor = Cursors.Default;
			}
			base.OnMouseLeave(e);
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0015AAAC File Offset: 0x00158CAC
		protected override void OnMouseMove(MouseEventArgs me)
		{
			Point point = Point.Empty;
			bool flag = false;
			int num;
			if (me == null)
			{
				num = -1;
				point = PropertyGridView.InvalidPosition;
			}
			else
			{
				point = this.FindPosition(me.X, me.Y);
				if (point == PropertyGridView.InvalidPosition || (point.X != 1 && point.X != 2))
				{
					num = -1;
					this.ToolTip.ToolTip = "";
				}
				else
				{
					num = point.Y;
					flag = point.X == 1;
				}
			}
			if (point == PropertyGridView.InvalidPosition || me == null)
			{
				return;
			}
			if (this.GetFlag(4))
			{
				this.MoveSplitterTo(me.X);
			}
			if ((num != this.TipRow || point.X != this.TipColumn) && !this.GetFlag(4))
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(num);
				string text = "";
				this.tipInfo = -1;
				if (gridEntryFromRow != null)
				{
					Rectangle rectangle = this.GetRectangle(point.Y, point.X);
					if (flag && gridEntryFromRow.GetLabelToolTipLocation(me.X - rectangle.X, me.Y - rectangle.Y) != PropertyGridView.InvalidPoint)
					{
						text = gridEntryFromRow.LabelToolTipText;
						this.TipRow = num;
						this.TipColumn = point.X;
					}
					else if (!flag && gridEntryFromRow.ValueToolTipLocation != PropertyGridView.InvalidPoint && !this.Edit.Focused)
					{
						if (!this.NeedsCommit)
						{
							text = gridEntryFromRow.GetPropertyTextValue();
						}
						this.TipRow = num;
						this.TipColumn = point.X;
					}
				}
				IntPtr foregroundWindow = UnsafeNativeMethods.GetForegroundWindow();
				if (UnsafeNativeMethods.IsChild(new HandleRef(null, foregroundWindow), new HandleRef(null, base.Handle)))
				{
					if (this.dropDownHolder == null || this.dropDownHolder.Component == null || num == this.selectedRow)
					{
						this.ToolTip.ToolTip = text;
					}
				}
				else
				{
					this.ToolTip.ToolTip = "";
				}
			}
			if (this.totalProps != 0 && (this.SplitterInside(me.X, me.Y) || this.GetFlag(4)))
			{
				this.Cursor = Cursors.VSplit;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}
			base.OnMouseMove(me);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0015ACE0 File Offset: 0x00158EE0
		protected override void OnMouseUp(MouseEventArgs me)
		{
			this.CancelSplitterMove();
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x0015ACE8 File Offset: 0x00158EE8
		protected override void OnMouseWheel(MouseEventArgs me)
		{
			this.ownerGrid.OnGridViewMouseWheel(me);
			HandledMouseEventArgs handledMouseEventArgs = me as HandledMouseEventArgs;
			if (handledMouseEventArgs != null)
			{
				if (handledMouseEventArgs.Handled)
				{
					return;
				}
				handledMouseEventArgs.Handled = true;
			}
			if ((Control.ModifierKeys & (Keys.Shift | Keys.Alt)) != Keys.None || Control.MouseButtons != MouseButtons.None)
			{
				return;
			}
			int mouseWheelScrollLines = SystemInformation.MouseWheelScrollLines;
			if (mouseWheelScrollLines == 0)
			{
				return;
			}
			if (this.selectedGridEntry != null && this.selectedGridEntry.Enumerable && this.Edit.Focused && this.selectedGridEntry.IsValueEditable)
			{
				int num = this.GetCurrentValueIndex(this.selectedGridEntry);
				if (num != -1)
				{
					int num2 = ((me.Delta > 0) ? (-1) : 1);
					object[] propertyValueList = this.selectedGridEntry.GetPropertyValueList();
					if (num2 > 0 && num >= propertyValueList.Length - 1)
					{
						num = 0;
					}
					else if (num2 < 0 && num == 0)
					{
						num = propertyValueList.Length - 1;
					}
					else
					{
						num += num2;
					}
					this.CommitValue(propertyValueList[num]);
					this.SelectGridEntry(this.selectedGridEntry, true);
					this.Edit.FocusInternal();
					return;
				}
			}
			int num3 = this.GetScrollOffset();
			this.cumulativeVerticalWheelDelta += me.Delta;
			float num4 = (float)this.cumulativeVerticalWheelDelta / 120f;
			int num5 = (int)num4;
			if (mouseWheelScrollLines == -1)
			{
				if (num5 != 0)
				{
					int num6 = num3;
					int num7 = num5 * this.scrollBar.LargeChange;
					int num8 = Math.Max(0, num3 - num7);
					num8 = Math.Min(num8, this.totalProps - this.visibleRows + 1);
					num3 -= num5 * this.scrollBar.LargeChange;
					if (Math.Abs(num3 - num6) >= Math.Abs(num5 * this.scrollBar.LargeChange))
					{
						this.cumulativeVerticalWheelDelta -= num5 * 120;
					}
					else
					{
						this.cumulativeVerticalWheelDelta = 0;
					}
					if (!this.ScrollRows(num8))
					{
						this.cumulativeVerticalWheelDelta = 0;
						return;
					}
				}
			}
			else
			{
				int num9 = (int)((float)mouseWheelScrollLines * num4);
				if (num9 != 0)
				{
					if (this.ToolTip.Visible)
					{
						this.ToolTip.ToolTip = "";
					}
					int num10 = Math.Max(0, num3 - num9);
					num10 = Math.Min(num10, this.totalProps - this.visibleRows + 1);
					if (num9 > 0)
					{
						if (this.scrollBar.Value <= this.scrollBar.Minimum)
						{
							this.cumulativeVerticalWheelDelta = 0;
						}
						else
						{
							this.cumulativeVerticalWheelDelta -= (int)((float)num9 * (120f / (float)mouseWheelScrollLines));
						}
					}
					else if (this.scrollBar.Value > this.scrollBar.Maximum - this.visibleRows + 1)
					{
						this.cumulativeVerticalWheelDelta = 0;
					}
					else
					{
						this.cumulativeVerticalWheelDelta -= (int)((float)num9 * (120f / (float)mouseWheelScrollLines));
					}
					if (!this.ScrollRows(num10))
					{
						this.cumulativeVerticalWheelDelta = 0;
						return;
					}
				}
				else
				{
					this.cumulativeVerticalWheelDelta = 0;
				}
			}
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x00158D54 File Offset: 0x00156F54
		protected override void OnMove(EventArgs e)
		{
			this.CloseDropDown();
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnPaintBackground(PaintEventArgs pe)
		{
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0015AFB8 File Offset: 0x001591B8
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics graphics = pe.Graphics;
			int num = 0;
			int num2 = 0;
			int num3 = this.visibleRows - 1;
			Rectangle clipRectangle = pe.ClipRectangle;
			clipRectangle.Inflate(0, 2);
			try
			{
				Size size = base.Size;
				Point point = this.FindPosition(clipRectangle.X, clipRectangle.Y);
				Point point2 = this.FindPosition(clipRectangle.X, clipRectangle.Y + clipRectangle.Height);
				if (point != PropertyGridView.InvalidPosition)
				{
					num2 = Math.Max(0, point.Y);
				}
				if (point2 != PropertyGridView.InvalidPosition)
				{
					num3 = point2.Y;
				}
				int num4 = Math.Min(this.totalProps - this.GetScrollOffset(), 1 + this.visibleRows);
				this.SetFlag(1, false);
				Size ourSize = this.GetOurSize();
				Point point3 = this.ptOurLocation;
				if (this.GetGridEntryFromRow(num4 - 1) == null)
				{
					num4--;
				}
				if (this.totalProps > 0)
				{
					num4 = Math.Min(num4, num3 + 1);
					Pen pen = new Pen(this.ownerGrid.LineColor, (float)this.GetSplitterWidth());
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, this.labelWidth, point3.Y, this.labelWidth, num4 * (this.RowHeight + 1) + point3.Y);
					pen.Dispose();
					Pen pen2 = new Pen(graphics.GetNearestColor(this.ownerGrid.LineColor));
					int num5 = point3.X + ourSize.Width;
					int x = point3.X;
					int num6 = this.GetTotalWidth() + 1;
					int num7;
					for (int i = num2; i < num4; i++)
					{
						try
						{
							num7 = i * (this.RowHeight + 1) + point3.Y;
							graphics.DrawLine(pen2, x, num7, num5, num7);
							this.DrawValueEntry(graphics, i, ref clipRectangle);
							Rectangle rectangle = this.GetRectangle(i, 1);
							num = rectangle.Y + rectangle.Height;
							this.DrawLabel(graphics, i, rectangle, i == this.selectedRow, false, ref clipRectangle);
							if (i == this.selectedRow)
							{
								this.Edit.Invalidate();
							}
						}
						catch
						{
						}
					}
					num7 = num4 * (this.RowHeight + 1) + point3.Y;
					graphics.DrawLine(pen2, x, num7, num5, num7);
					pen2.Dispose();
				}
				if (num < base.Size.Height)
				{
					num++;
					Rectangle rectangle2 = new Rectangle(1, num, base.Size.Width - 2, base.Size.Height - num - 1);
					graphics.FillRectangle(this.backgroundBrush, rectangle2);
				}
				using (Pen pen3 = new Pen(this.ownerGrid.ViewBorderColor, 1f))
				{
					graphics.DrawRectangle(pen3, 0, 0, size.Width - 1, size.Height - 1);
				}
				this.fontBold = null;
			}
			catch
			{
			}
			finally
			{
				this.ClearCachedFontInfo();
			}
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0015B318 File Offset: 0x00159518
		private void OnGridEntryLabelDoubleClick(object s, EventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			if (gridEntry != this.lastClickedEntry)
			{
				return;
			}
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			this.DoubleClickRow(rowFromGridEntry, gridEntry.Expandable, 1);
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0015B34C File Offset: 0x0015954C
		private void OnGridEntryValueDoubleClick(object s, EventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			if (gridEntry != this.lastClickedEntry)
			{
				return;
			}
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			this.DoubleClickRow(rowFromGridEntry, gridEntry.Expandable, 2);
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x0015B380 File Offset: 0x00159580
		private void OnGridEntryLabelClick(object s, EventArgs e)
		{
			this.lastClickedEntry = (GridEntry)s;
			this.SelectGridEntry(this.lastClickedEntry, true);
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0015B39C File Offset: 0x0015959C
		private void OnGridEntryOutlineClick(object s, EventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			Cursor cursor = this.Cursor;
			if (!this.ShouldSerializeCursor())
			{
				cursor = null;
			}
			this.Cursor = Cursors.WaitCursor;
			try
			{
				this.SetExpand(gridEntry, !gridEntry.InternalExpanded);
				this.SelectGridEntry(gridEntry, false);
			}
			finally
			{
				this.Cursor = cursor;
			}
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0015B400 File Offset: 0x00159600
		private void OnGridEntryValueClick(object s, EventArgs e)
		{
			this.lastClickedEntry = (GridEntry)s;
			bool flag = s != this.selectedGridEntry;
			this.SelectGridEntry(this.lastClickedEntry, true);
			this.Edit.FocusInternal();
			if (this.lastMouseDown != PropertyGridView.InvalidPosition)
			{
				this.rowSelectTime = 0L;
				Point point = base.PointToScreen(this.lastMouseDown);
				point = this.Edit.PointToClientInternal(point);
				this.Edit.SendMessage(513, 0, (point.Y << 16) | (point.X & 65535));
				this.Edit.SendMessage(514, 0, (point.Y << 16) | (point.X & 65535));
			}
			if (flag)
			{
				this.rowSelectTime = DateTime.Now.Ticks;
				this.rowSelectPos = base.PointToScreen(this.lastMouseDown);
				return;
			}
			this.rowSelectTime = 0L;
			this.rowSelectPos = Point.Empty;
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x0015B504 File Offset: 0x00159704
		private void ClearCachedFontInfo()
		{
			if (this.baseHfont != IntPtr.Zero)
			{
				SafeNativeMethods.ExternalDeleteObject(new HandleRef(this, this.baseHfont));
				this.baseHfont = IntPtr.Zero;
			}
			if (this.boldHfont != IntPtr.Zero)
			{
				SafeNativeMethods.ExternalDeleteObject(new HandleRef(this, this.boldHfont));
				this.boldHfont = IntPtr.Zero;
			}
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x0015B570 File Offset: 0x00159770
		protected override void OnFontChanged(EventArgs e)
		{
			this.ClearCachedFontInfo();
			this.cachedRowHeight = -1;
			if (base.Disposing || this.ParentInternal == null || this.ParentInternal.Disposing)
			{
				return;
			}
			this.fontBold = null;
			this.ToolTip.Font = this.Font;
			this.SetFlag(128, true);
			this.UpdateUIBasedOnFont(true);
			base.OnFontChanged(e);
			if (this.selectedGridEntry != null)
			{
				this.SelectGridEntry(this.selectedGridEntry, true);
			}
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0015B5F0 File Offset: 0x001597F0
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Disposing || this.ParentInternal == null || this.ParentInternal.Disposing)
			{
				return;
			}
			if (base.Visible && this.ParentInternal != null)
			{
				this.SetConstants();
				if (this.selectedGridEntry != null)
				{
					this.SelectGridEntry(this.selectedGridEntry, true);
				}
				if (this.toolTip != null)
				{
					this.ToolTip.Font = this.Font;
				}
			}
			base.OnVisibleChanged(e);
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0015B668 File Offset: 0x00159868
		protected virtual void OnRecreateChildren(object s, GridEntryRecreateChildrenEventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			if (gridEntry.Expanded)
			{
				GridEntry[] array = new GridEntry[this.allGridEntries.Count];
				this.allGridEntries.CopyTo(array, 0);
				int num = -1;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == gridEntry)
					{
						num = i;
						break;
					}
				}
				this.ClearGridEntryEvents(this.allGridEntries, num + 1, e.OldChildCount);
				if (e.OldChildCount != e.NewChildCount)
				{
					int num2 = array.Length + (e.NewChildCount - e.OldChildCount);
					GridEntry[] array2 = new GridEntry[num2];
					Array.Copy(array, 0, array2, 0, num + 1);
					Array.Copy(array, num + e.OldChildCount + 1, array2, num + e.NewChildCount + 1, array.Length - (num + e.OldChildCount + 1));
					array = array2;
				}
				GridEntryCollection children = gridEntry.Children;
				int count = children.Count;
				for (int j = 0; j < count; j++)
				{
					array[num + j + 1] = children.GetEntry(j);
				}
				this.allGridEntries.Clear();
				this.allGridEntries.AddRange(array);
				this.AddGridEntryEvents(this.allGridEntries, num + 1, count);
			}
			if (e.OldChildCount != e.NewChildCount)
			{
				this.totalProps = this.CountPropsFromOutline(this.topLevelGridEntries);
				this.SetConstants();
			}
			base.Invalidate();
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x0015B7C4 File Offset: 0x001599C4
		protected override void OnResize(EventArgs e)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = ((this.lastClientRect == Rectangle.Empty) ? 0 : (clientRectangle.Height - this.lastClientRect.Height));
			bool flag = this.selectedRow + 1 == this.visibleRows;
			bool visible = this.ScrollBar.Visible;
			if (!this.lastClientRect.IsEmpty && clientRectangle.Width > this.lastClientRect.Width)
			{
				Rectangle rectangle = new Rectangle(this.lastClientRect.Width - 1, 0, clientRectangle.Width - this.lastClientRect.Width + 1, this.lastClientRect.Height);
				base.Invalidate(rectangle);
			}
			if (!this.lastClientRect.IsEmpty && num > 0)
			{
				Rectangle rectangle2 = new Rectangle(0, this.lastClientRect.Height - 1, this.lastClientRect.Width, clientRectangle.Height - this.lastClientRect.Height + 1);
				base.Invalidate(rectangle2);
			}
			int scrollOffset = this.GetScrollOffset();
			this.SetScrollOffset(0);
			this.SetConstants();
			this.SetScrollOffset(scrollOffset);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.SetFlag(128, true);
				this.UpdateUIBasedOnFont(true);
				base.OnFontChanged(e);
			}
			this.CommonEditorHide();
			this.LayoutWindow(false);
			bool flag2 = this.selectedGridEntry != null && this.selectedRow >= 0 && this.selectedRow <= this.visibleRows;
			this.SelectGridEntry(this.selectedGridEntry, flag2);
			this.lastClientRect = clientRectangle;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0015B954 File Offset: 0x00159B54
		private void OnScroll(object sender, ScrollEventArgs se)
		{
			if (!this.Commit() || !this.IsScrollValueValid(se.NewValue))
			{
				se.NewValue = this.ScrollBar.Value;
				return;
			}
			int num = -1;
			GridEntry gridEntry = this.selectedGridEntry;
			if (this.selectedGridEntry != null)
			{
				num = this.GetRowFromGridEntry(gridEntry);
			}
			this.ScrollBar.Value = se.NewValue;
			if (gridEntry != null)
			{
				this.selectedRow = -1;
				this.SelectGridEntry(gridEntry, this.ScrollBar.Value == this.totalProps);
				int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
				if (num != rowFromGridEntry)
				{
					base.Invalidate();
					return;
				}
			}
			else
			{
				base.Invalidate();
			}
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x0015B9F4 File Offset: 0x00159BF4
		private void OnSysColorChange(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Color || e.Category == UserPreferenceCategory.Accessibility)
			{
				this.SetFlag(128, true);
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0015BA14 File Offset: 0x00159C14
		public virtual void PopupDialog(int row)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow != null)
			{
				if (this.dropDownHolder != null && this.dropDownHolder.GetUsed())
				{
					this.CloseDropDown();
					return;
				}
				bool needsDropDownButton = gridEntryFromRow.NeedsDropDownButton;
				bool enumerable = gridEntryFromRow.Enumerable;
				bool needsCustomEditorButton = gridEntryFromRow.NeedsCustomEditorButton;
				if (enumerable && !needsDropDownButton)
				{
					this.DropDownListBox.Items.Clear();
					object propertyValue = gridEntryFromRow.PropertyValue;
					object[] propertyValueList = gridEntryFromRow.GetPropertyValueList();
					int num = 0;
					IntPtr dc = UnsafeNativeMethods.GetDC(new HandleRef(this.DropDownListBox, this.DropDownListBox.Handle));
					IntPtr intPtr = this.Font.ToHfont();
					System.Internal.HandleCollector.Add(intPtr, NativeMethods.CommonHandles.GDI);
					NativeMethods.TEXTMETRIC textmetric = default(NativeMethods.TEXTMETRIC);
					int num2 = -1;
					try
					{
						intPtr = SafeNativeMethods.SelectObject(new HandleRef(this.DropDownListBox, dc), new HandleRef(this.Font, intPtr));
						num2 = this.GetCurrentValueIndex(gridEntryFromRow);
						if (propertyValueList != null && propertyValueList.Length != 0)
						{
							IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
							for (int i = 0; i < propertyValueList.Length; i++)
							{
								string propertyTextValue = gridEntryFromRow.GetPropertyTextValue(propertyValueList[i]);
								this.DropDownListBox.Items.Add(propertyTextValue);
								IntUnsafeNativeMethods.GetTextExtentPoint32(new HandleRef(this.DropDownListBox, dc), propertyTextValue, size);
								num = Math.Max(size.cx, num);
							}
						}
						SafeNativeMethods.GetTextMetrics(new HandleRef(this.DropDownListBox, dc), ref textmetric);
						num += 2 + textmetric.tmMaxCharWidth + SystemInformation.VerticalScrollBarWidth;
						intPtr = SafeNativeMethods.SelectObject(new HandleRef(this.DropDownListBox, dc), new HandleRef(this.Font, intPtr));
					}
					finally
					{
						SafeNativeMethods.DeleteObject(new HandleRef(this.Font, intPtr));
						UnsafeNativeMethods.ReleaseDC(new HandleRef(this.DropDownListBox, this.DropDownListBox.Handle), new HandleRef(this.DropDownListBox, dc));
					}
					if (num2 != -1)
					{
						this.DropDownListBox.SelectedIndex = num2;
					}
					this.SetFlag(64, false);
					this.DropDownListBox.Height = Math.Max(textmetric.tmHeight + 2, Math.Min(this.maxListBoxHeight, this.DropDownListBox.PreferredHeight));
					this.DropDownListBox.Width = Math.Max(num, this.GetRectangle(row, 2).Width);
					try
					{
						bool flag = this.DropDownListBox.Items.Count > this.DropDownListBox.Height / this.DropDownListBox.ItemHeight;
						this.SetFlag(1024, flag);
						this.DropDownControl(this.DropDownListBox);
					}
					finally
					{
						this.SetFlag(1024, false);
					}
					this.Refresh();
					return;
				}
				if (needsCustomEditorButton || needsDropDownButton)
				{
					try
					{
						this.SetFlag(16, true);
						this.Edit.DisableMouseHook = true;
						try
						{
							this.SetFlag(1024, gridEntryFromRow.UITypeEditor.IsDropDownResizable);
							gridEntryFromRow.EditPropertyValue(this);
						}
						finally
						{
							this.SetFlag(1024, false);
						}
					}
					finally
					{
						this.SetFlag(16, false);
						this.Edit.DisableMouseHook = false;
					}
					this.Refresh();
					if (this.FocusInside)
					{
						this.SelectGridEntry(gridEntryFromRow, false);
					}
				}
			}
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0015BD5C File Offset: 0x00159F5C
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.HasEntries)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Return)
				{
					if (keys != Keys.Tab)
					{
						if (keys == Keys.Return)
						{
							if (this.DialogButton.Focused || this.DropDownButton.Focused)
							{
								this.OnBtnClick(this.DialogButton.Focused ? this.DialogButton : this.DropDownButton, new EventArgs());
								return true;
							}
							if (this.selectedGridEntry != null && this.selectedGridEntry.Expandable)
							{
								this.SetExpand(this.selectedGridEntry, !this.selectedGridEntry.InternalExpanded);
								return true;
							}
						}
					}
					else if ((keyData & Keys.Control) == Keys.None && (keyData & Keys.Alt) == Keys.None)
					{
						bool flag = (keyData & Keys.Shift) == Keys.None;
						Control control = Control.FromHandleInternal(UnsafeNativeMethods.GetFocus());
						if (control == null || !this.IsMyChild(control))
						{
							if (flag)
							{
								this.TabSelection();
								control = Control.FromHandleInternal(UnsafeNativeMethods.GetFocus());
								return this.IsMyChild(control) || base.ProcessDialogKey(keyData);
							}
						}
						else if (this.Edit.Focused)
						{
							if (!flag)
							{
								this.SelectGridEntry(this.GetGridEntryFromRow(this.selectedRow), false);
								return true;
							}
							if (this.DropDownButton.Visible)
							{
								this.DropDownButton.FocusInternal();
								return true;
							}
							if (this.DialogButton.Visible)
							{
								this.DialogButton.FocusInternal();
								return true;
							}
						}
						else if ((this.DialogButton.Focused || this.DropDownButton.Focused) && !flag && this.Edit.Visible)
						{
							this.Edit.FocusInternal();
							return true;
						}
					}
				}
				else
				{
					if (keys - Keys.Left <= 3)
					{
						return false;
					}
					if (keys == Keys.F4 && this.FocusInside)
					{
						return this.OnF4(this);
					}
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0015BF38 File Offset: 0x0015A138
		protected virtual void RecalculateProps()
		{
			int num = this.CountPropsFromOutline(this.topLevelGridEntries);
			if (this.totalProps != num)
			{
				this.totalProps = num;
				this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
				this.allGridEntries = null;
			}
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0015BF78 File Offset: 0x0015A178
		internal void RecursivelyExpand(GridEntry gridEntry, bool fInit, bool expand, int maxExpands)
		{
			if (gridEntry == null || (expand && --maxExpands < 0))
			{
				return;
			}
			this.SetExpand(gridEntry, expand);
			GridEntryCollection children = gridEntry.Children;
			if (children != null)
			{
				for (int i = 0; i < children.Count; i++)
				{
					this.RecursivelyExpand(children.GetEntry(i), false, expand, maxExpands);
				}
			}
			if (fInit)
			{
				GridEntry gridEntry2 = this.selectedGridEntry;
				this.Refresh();
				this.SelectGridEntry(gridEntry2, false);
				base.Invalidate();
			}
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0015BFE8 File Offset: 0x0015A1E8
		public override void Refresh()
		{
			this.Refresh(false, -1, -1);
			if (this.topLevelGridEntries != null && DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				int outlineIconSize = this.GetOutlineIconSize();
				foreach (object obj in this.topLevelGridEntries)
				{
					GridEntry gridEntry = (GridEntry)obj;
					if (gridEntry.OutlineRect.Height != outlineIconSize || gridEntry.OutlineRect.Width != outlineIconSize)
					{
						this.ResetOutline(gridEntry);
					}
				}
			}
			base.Invalidate();
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0015C08C File Offset: 0x0015A28C
		public void Refresh(bool fullRefresh)
		{
			this.Refresh(fullRefresh, -1, -1);
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0015C098 File Offset: 0x0015A298
		private void Refresh(bool fullRefresh, int rowStart, int rowEnd)
		{
			this.SetFlag(1, true);
			GridEntry gridEntry = null;
			if (base.IsDisposed)
			{
				return;
			}
			bool flag = true;
			if (rowStart == -1)
			{
				rowStart = 0;
			}
			if (fullRefresh || this.ownerGrid.HavePropEntriesChanged())
			{
				if (this.HasEntries && !this.GetInPropertySet() && !this.Commit())
				{
					this.OnEscape(this);
				}
				int num = this.totalProps;
				object obj = ((this.topLevelGridEntries == null || this.topLevelGridEntries.Count == 0) ? null : ((GridEntry)this.topLevelGridEntries[0]).GetValueOwner());
				if (fullRefresh)
				{
					this.ownerGrid.RefreshProperties(true);
				}
				if (num > 0 && !this.GetFlag(512))
				{
					this.positionData = this.CaptureGridPositionData();
					this.CommonEditorHide(true);
				}
				this.UpdateHelpAttributes(this.selectedGridEntry, null);
				this.selectedGridEntry = null;
				this.SetFlag(2, true);
				this.topLevelGridEntries = this.ownerGrid.GetPropEntries();
				this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
				this.allGridEntries = null;
				this.RecalculateProps();
				int num2 = this.totalProps;
				if (num2 > 0)
				{
					if (num2 < num)
					{
						this.SetScrollbarLength();
						this.SetScrollOffset(0);
					}
					this.SetConstants();
					if (this.positionData != null)
					{
						gridEntry = this.positionData.Restore(this);
						object obj2 = ((this.topLevelGridEntries == null || this.topLevelGridEntries.Count == 0) ? null : ((GridEntry)this.topLevelGridEntries[0]).GetValueOwner());
						flag = gridEntry == null || num != num2 || obj2 != obj;
					}
					if (gridEntry == null)
					{
						gridEntry = this.ownerGrid.GetDefaultGridEntry();
						this.SetFlag(512, gridEntry == null && this.totalProps > 0);
					}
					this.InvalidateRows(rowStart, rowEnd);
					if (gridEntry == null)
					{
						this.selectedRow = 0;
						this.selectedGridEntry = this.GetGridEntryFromRow(this.selectedRow);
					}
				}
				else
				{
					if (num == 0)
					{
						return;
					}
					this.SetConstants();
				}
				this.positionData = null;
				this.lastClickedEntry = null;
			}
			if (!this.HasEntries)
			{
				this.CommonEditorHide(this.selectedRow != -1);
				this.ownerGrid.SetStatusBox(null, null);
				this.SetScrollOffset(0);
				this.selectedRow = -1;
				base.Invalidate();
				return;
			}
			this.ownerGrid.ClearValueCaches();
			this.InvalidateRows(rowStart, rowEnd);
			if (gridEntry != null)
			{
				this.SelectGridEntry(gridEntry, flag);
			}
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0015C2E8 File Offset: 0x0015A4E8
		public virtual void Reset()
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow == null)
			{
				return;
			}
			gridEntryFromRow.ResetPropertyValue();
			this.SelectRow(this.selectedRow);
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0015C318 File Offset: 0x0015A518
		protected virtual void ResetOrigin(Graphics g)
		{
			g.ResetTransform();
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0015C320 File Offset: 0x0015A520
		internal void RestoreHierarchyState(ArrayList expandedItems)
		{
			if (expandedItems == null)
			{
				return;
			}
			foreach (object obj in expandedItems)
			{
				GridEntryCollection gridEntryCollection = (GridEntryCollection)obj;
				this.FindEquivalentGridEntry(gridEntryCollection);
			}
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0015C37C File Offset: 0x0015A57C
		public virtual DialogResult RunDialog(Form dialog)
		{
			return this.ShowDialog(dialog);
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0015C385 File Offset: 0x0015A585
		internal ArrayList SaveHierarchyState(GridEntryCollection entries)
		{
			return this.SaveHierarchyState(entries, null);
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0015C390 File Offset: 0x0015A590
		private ArrayList SaveHierarchyState(GridEntryCollection entries, ArrayList expandedItems)
		{
			if (entries == null)
			{
				return new ArrayList();
			}
			if (expandedItems == null)
			{
				expandedItems = new ArrayList();
			}
			for (int i = 0; i < entries.Count; i++)
			{
				if (((GridEntry)entries[i]).InternalExpanded)
				{
					GridEntry entry = entries.GetEntry(i);
					expandedItems.Add(this.GetGridEntryHierarchy(entry.Children.GetEntry(0)));
					this.SaveHierarchyState(entry.Children, expandedItems);
				}
			}
			return expandedItems;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0015C404 File Offset: 0x0015A604
		private bool ScrollRows(int newOffset)
		{
			GridEntry gridEntry = this.selectedGridEntry;
			if (!this.IsScrollValueValid(newOffset) || !this.Commit())
			{
				return false;
			}
			bool visible = this.Edit.Visible;
			bool visible2 = this.DropDownButton.Visible;
			bool visible3 = this.DialogButton.Visible;
			this.Edit.Visible = false;
			this.DialogButton.Visible = false;
			this.DropDownButton.Visible = false;
			this.SetScrollOffset(newOffset);
			if (gridEntry != null)
			{
				int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
				if (rowFromGridEntry >= 0 && rowFromGridEntry < this.visibleRows - 1)
				{
					this.Edit.Visible = visible;
					this.DialogButton.Visible = visible3;
					this.DropDownButton.Visible = visible2;
					this.SelectGridEntry(gridEntry, true);
				}
				else
				{
					this.CommonEditorHide();
				}
			}
			else
			{
				this.CommonEditorHide();
			}
			base.Invalidate();
			return true;
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0015C4DA File Offset: 0x0015A6DA
		private void SelectEdit(bool caretAtEnd)
		{
			if (this.edit != null)
			{
				this.Edit.SelectAll();
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0015C4F0 File Offset: 0x0015A6F0
		internal void SelectGridEntry(GridEntry gridEntry, bool fPageIn)
		{
			if (gridEntry == null)
			{
				return;
			}
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			if (rowFromGridEntry + this.GetScrollOffset() < 0)
			{
				return;
			}
			int num = (int)Math.Ceiling((double)this.GetOurSize().Height / (double)(1 + this.RowHeight));
			if (!fPageIn || (rowFromGridEntry >= 0 && rowFromGridEntry < num - 1))
			{
				this.SelectRow(rowFromGridEntry);
				return;
			}
			this.selectedRow = -1;
			int scrollOffset = this.GetScrollOffset();
			if (rowFromGridEntry < 0)
			{
				this.SetScrollOffset(rowFromGridEntry + scrollOffset);
				base.Invalidate();
				this.SelectRow(0);
				return;
			}
			int num2 = rowFromGridEntry + scrollOffset - (num - 2);
			if (num2 >= this.ScrollBar.Minimum && num2 < this.ScrollBar.Maximum)
			{
				this.SetScrollOffset(num2);
			}
			base.Invalidate();
			this.SelectGridEntry(gridEntry, false);
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0015C5B0 File Offset: 0x0015A7B0
		private void SelectRow(int row)
		{
			if (!this.GetFlag(2))
			{
				if (this.FocusInside)
				{
					if (this.errorState != 0 || (row != this.selectedRow && !this.Commit()))
					{
						return;
					}
				}
				else
				{
					this.FocusInternal();
				}
			}
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (row != this.selectedRow)
			{
				this.UpdateResetCommand(gridEntryFromRow);
			}
			if (this.GetFlag(2) && this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				this.CommonEditorHide();
			}
			this.UpdateHelpAttributes(this.selectedGridEntry, gridEntryFromRow);
			if (this.selectedGridEntry != null)
			{
				this.selectedGridEntry.Focus = false;
			}
			if (row < 0 || row >= this.visibleRows)
			{
				this.CommonEditorHide();
				this.selectedRow = row;
				this.selectedGridEntry = gridEntryFromRow;
				this.Refresh();
				return;
			}
			if (gridEntryFromRow == null)
			{
				return;
			}
			bool flag = false;
			int num = this.selectedRow;
			if (this.selectedRow != row || !gridEntryFromRow.Equals(this.selectedGridEntry))
			{
				this.CommonEditorHide();
				flag = true;
			}
			if (!flag)
			{
				this.CloseDropDown();
			}
			Rectangle rectangle = this.GetRectangle(row, 2);
			string propertyTextValue = gridEntryFromRow.GetPropertyTextValue();
			bool flag2 = gridEntryFromRow.NeedsDropDownButton | gridEntryFromRow.Enumerable;
			bool needsCustomEditorButton = gridEntryFromRow.NeedsCustomEditorButton;
			bool isTextEditable = gridEntryFromRow.IsTextEditable;
			bool isCustomPaint = gridEntryFromRow.IsCustomPaint;
			rectangle.X++;
			rectangle.Width--;
			if ((needsCustomEditorButton || flag2) && !gridEntryFromRow.ShouldRenderReadOnly && this.FocusInside)
			{
				Control control = (flag2 ? this.DropDownButton : this.DialogButton);
				Size size = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight) : new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight));
				Rectangle rectangle2 = new Rectangle(rectangle.X + rectangle.Width - size.Width, rectangle.Y, size.Width, rectangle.Height);
				this.CommonEditorUse(control, rectangle2);
				size = control.Size;
				rectangle.Width -= size.Width;
				control.Invalidate();
			}
			if (isCustomPaint)
			{
				rectangle.X += this.paintIndent + 1;
				rectangle.Width -= this.paintIndent + 1;
			}
			else
			{
				rectangle.X++;
				rectangle.Width--;
			}
			if ((this.GetFlag(2) || !this.Edit.Focused) && propertyTextValue != null && !propertyTextValue.Equals(this.Edit.Text))
			{
				this.Edit.Text = propertyTextValue;
				this.originalTextValue = propertyTextValue;
				this.Edit.SelectionStart = 0;
				this.Edit.SelectionLength = 0;
			}
			this.Edit.AccessibleName = gridEntryFromRow.Label;
			switch (PropertyGridView.inheritRenderMode)
			{
			case 2:
				if (gridEntryFromRow.ShouldSerializePropertyValue())
				{
					rectangle.X += 8;
					rectangle.Width -= 8;
				}
				break;
			case 3:
				if (gridEntryFromRow.ShouldSerializePropertyValue())
				{
					this.Edit.Font = this.GetBoldFont();
				}
				else
				{
					this.Edit.Font = this.Font;
				}
				break;
			}
			if (this.GetFlag(4) || !gridEntryFromRow.HasValue || !this.FocusInside)
			{
				this.Edit.Visible = false;
			}
			else
			{
				rectangle.Offset(1, 1);
				rectangle.Height--;
				rectangle.Width--;
				this.CommonEditorUse(this.Edit, rectangle);
				bool shouldRenderReadOnly = gridEntryFromRow.ShouldRenderReadOnly;
				this.Edit.ForeColor = (shouldRenderReadOnly ? this.GrayTextColor : this.ForeColor);
				this.Edit.BackColor = this.BackColor;
				this.Edit.ReadOnly = shouldRenderReadOnly || !gridEntryFromRow.IsTextEditable;
				this.Edit.UseSystemPasswordChar = gridEntryFromRow.ShouldRenderPassword;
			}
			GridEntry gridEntry = this.selectedGridEntry;
			this.selectedRow = row;
			this.selectedGridEntry = gridEntryFromRow;
			this.ownerGrid.SetStatusBox(gridEntryFromRow.PropertyLabel, gridEntryFromRow.PropertyDescription);
			if (this.selectedGridEntry != null)
			{
				this.selectedGridEntry.Focus = this.FocusInside;
			}
			if (!this.GetFlag(2))
			{
				this.FocusInternal();
			}
			this.InvalidateRow(num);
			this.InvalidateRow(row);
			if (this.FocusInside)
			{
				this.SetFlag(2, false);
			}
			try
			{
				if (this.selectedGridEntry != gridEntry)
				{
					this.ownerGrid.OnSelectedGridItemChanged(gridEntry, this.selectedGridEntry);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0015CA54 File Offset: 0x0015AC54
		public virtual void SetConstants()
		{
			this.visibleRows = (int)Math.Ceiling((double)this.GetOurSize().Height / (double)(1 + this.RowHeight));
			Size ourSize = this.GetOurSize();
			if (ourSize.Width >= 0)
			{
				this.labelRatio = Math.Max(Math.Min(this.labelRatio, 9.0), 1.1);
				this.labelWidth = this.ptOurLocation.X + (int)((double)ourSize.Width / this.labelRatio);
			}
			int num = this.labelWidth;
			bool flag = this.SetScrollbarLength();
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection != null)
			{
				int scrollOffset = this.GetScrollOffset();
				if (scrollOffset + this.visibleRows >= gridEntryCollection.Count)
				{
					this.visibleRows = gridEntryCollection.Count - scrollOffset;
				}
			}
			if (flag && ourSize.Width >= 0)
			{
				this.labelRatio = (double)this.GetOurSize().Width / (double)(num - this.ptOurLocation.X);
			}
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0015CB53 File Offset: 0x0015AD53
		private void SetCommitError(short error)
		{
			this.SetCommitError(error, error == 1);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0015CB60 File Offset: 0x0015AD60
		private void SetCommitError(short error, bool capture)
		{
			this.errorState = error;
			if (error != 0)
			{
				this.CancelSplitterMove();
			}
			this.Edit.HookMouseDown = capture;
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0015CB80 File Offset: 0x0015AD80
		internal void SetExpand(GridEntry gridEntry, bool value)
		{
			if (gridEntry != null && gridEntry.Expandable)
			{
				int num = this.GetRowFromGridEntry(gridEntry);
				int num2 = this.visibleRows - num;
				int num3 = this.selectedRow;
				if (this.selectedRow != -1 && num < this.selectedRow && this.Edit.Visible)
				{
					this.FocusInternal();
				}
				int scrollOffset = this.GetScrollOffset();
				int num4 = this.totalProps;
				gridEntry.InternalExpanded = value;
				if (AccessibilityImprovements.Level4)
				{
					UnsafeNativeMethods.ExpandCollapseState expandCollapseState = (value ? UnsafeNativeMethods.ExpandCollapseState.Collapsed : UnsafeNativeMethods.ExpandCollapseState.Expanded);
					UnsafeNativeMethods.ExpandCollapseState expandCollapseState2 = (value ? UnsafeNativeMethods.ExpandCollapseState.Expanded : UnsafeNativeMethods.ExpandCollapseState.Collapsed);
					GridEntry gridEntry2 = this.selectedGridEntry;
					if (gridEntry2 != null)
					{
						AccessibleObject accessibilityObject = gridEntry2.AccessibilityObject;
						if (accessibilityObject != null)
						{
							accessibilityObject.RaiseAutomationPropertyChangedEvent(30070, expandCollapseState, expandCollapseState2);
						}
					}
				}
				this.RecalculateProps();
				GridEntry gridEntry3 = this.selectedGridEntry;
				if (!value)
				{
					for (GridEntry gridEntry4 = gridEntry3; gridEntry4 != null; gridEntry4 = gridEntry4.ParentGridEntry)
					{
						if (gridEntry4.Equals(gridEntry))
						{
							gridEntry3 = gridEntry;
						}
					}
				}
				num = this.GetRowFromGridEntry(gridEntry);
				this.SetConstants();
				int num5 = this.totalProps - num4;
				if (value && num5 > 0 && num5 < this.visibleRows && num + num5 >= this.visibleRows && num5 < num3)
				{
					this.SetScrollOffset(this.totalProps - num4 + scrollOffset);
				}
				base.Invalidate();
				this.SelectGridEntry(gridEntry3, false);
				int scrollOffset2 = this.GetScrollOffset();
				this.SetScrollOffset(0);
				this.SetConstants();
				this.SetScrollOffset(scrollOffset2);
			}
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0015CCE5 File Offset: 0x0015AEE5
		private void SetFlag(short flag, bool value)
		{
			if (value)
			{
				this.flags = (short)((ushort)this.flags | (ushort)flag);
				return;
			}
			this.flags &= ~flag;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0015CD10 File Offset: 0x0015AF10
		public virtual void SetScrollOffset(int cOffset)
		{
			int num = Math.Max(0, Math.Min(this.totalProps - this.visibleRows + 1, cOffset));
			int value = this.ScrollBar.Value;
			if (num != value && this.IsScrollValueValid(num) && this.visibleRows > 0)
			{
				this.ScrollBar.Value = num;
				base.Invalidate();
				this.selectedRow = this.GetRowFromGridEntry(this.selectedGridEntry);
			}
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0015CD7F File Offset: 0x0015AF7F
		internal virtual bool _Commit()
		{
			return this.Commit();
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0015CD88 File Offset: 0x0015AF88
		private bool Commit()
		{
			if (this.errorState == 2)
			{
				return false;
			}
			if (!this.NeedsCommit)
			{
				this.SetCommitError(0);
				return true;
			}
			if (this.GetInPropertySet())
			{
				return false;
			}
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return true;
			}
			bool flag = false;
			try
			{
				flag = this.CommitText(this.Edit.Text);
			}
			finally
			{
				if (!flag)
				{
					this.Edit.FocusInternal();
					this.SelectEdit(false);
				}
				else
				{
					this.SetCommitError(0);
				}
			}
			return flag;
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0015CE14 File Offset: 0x0015B014
		private bool CommitValue(object value)
		{
			GridEntry gridEntryFromRow = this.selectedGridEntry;
			if (this.selectedGridEntry == null && this.selectedRow != -1)
			{
				gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			}
			return gridEntryFromRow == null || this.CommitValue(gridEntryFromRow, value, true);
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0015CE54 File Offset: 0x0015B054
		internal bool CommitValue(GridEntry ipeCur, object value, bool closeDropDown = true)
		{
			int childCount = ipeCur.ChildCount;
			bool hookMouseDown = this.Edit.HookMouseDown;
			object obj = null;
			try
			{
				obj = ipeCur.PropertyValue;
			}
			catch
			{
			}
			try
			{
				this.SetFlag(16, true);
				if (ipeCur != null && ipeCur.Enumerable && closeDropDown)
				{
					this.CloseDropDown();
				}
				try
				{
					this.Edit.DisableMouseHook = true;
					ipeCur.PropertyValue = value;
				}
				finally
				{
					this.Edit.DisableMouseHook = false;
					this.Edit.HookMouseDown = hookMouseDown;
				}
			}
			catch (Exception ex)
			{
				this.SetCommitError(1);
				this.ShowInvalidMessage(ipeCur.PropertyLabel, value, ex);
				return false;
			}
			finally
			{
				this.SetFlag(16, false);
			}
			this.SetCommitError(0);
			string propertyTextValue = ipeCur.GetPropertyTextValue();
			if (!string.Equals(propertyTextValue, this.Edit.Text))
			{
				this.Edit.Text = propertyTextValue;
				this.Edit.SelectionStart = 0;
				this.Edit.SelectionLength = 0;
			}
			this.originalTextValue = propertyTextValue;
			this.UpdateResetCommand(ipeCur);
			if (ipeCur.ChildCount != childCount)
			{
				this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
				this.allGridEntries = null;
				this.SelectGridEntry(ipeCur, true);
			}
			if (ipeCur.Disposed)
			{
				bool flag = this.edit != null && this.edit.Focused;
				this.SelectGridEntry(ipeCur, true);
				ipeCur = this.selectedGridEntry;
				if (flag && this.edit != null)
				{
					this.edit.Focus();
				}
			}
			this.ownerGrid.OnPropertyValueSet(ipeCur, obj);
			return true;
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x0015D004 File Offset: 0x0015B204
		private bool CommitText(string text)
		{
			object obj = null;
			GridEntry gridEntryFromRow = this.selectedGridEntry;
			if (this.selectedGridEntry == null && this.selectedRow != -1)
			{
				gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			}
			if (gridEntryFromRow == null)
			{
				return true;
			}
			try
			{
				obj = gridEntryFromRow.ConvertTextToValue(text);
			}
			catch (Exception ex)
			{
				this.SetCommitError(1);
				this.ShowInvalidMessage(gridEntryFromRow.PropertyLabel, text, ex);
				return false;
			}
			this.SetCommitError(0);
			return this.CommitValue(obj);
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0015D084 File Offset: 0x0015B284
		internal void ReverseFocus()
		{
			if (this.selectedGridEntry == null)
			{
				this.FocusInternal();
				return;
			}
			this.SelectGridEntry(this.selectedGridEntry, true);
			if (this.DialogButton.Visible)
			{
				this.DialogButton.FocusInternal();
				return;
			}
			if (this.DropDownButton.Visible)
			{
				this.DropDownButton.FocusInternal();
				return;
			}
			if (this.Edit.Visible)
			{
				this.Edit.SelectAll();
				this.Edit.FocusInternal();
			}
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0015D108 File Offset: 0x0015B308
		private bool SetScrollbarLength()
		{
			bool flag = false;
			if (this.totalProps != -1)
			{
				if (this.totalProps < this.visibleRows)
				{
					this.SetScrollOffset(0);
				}
				else if (this.GetScrollOffset() > this.totalProps)
				{
					this.SetScrollOffset(this.totalProps + 1 - this.visibleRows);
				}
				bool flag2 = !this.ScrollBar.Visible;
				if (this.visibleRows > 0)
				{
					this.ScrollBar.LargeChange = this.visibleRows - 1;
				}
				this.ScrollBar.Maximum = Math.Max(0, this.totalProps - 1);
				if (flag2 != this.totalProps < this.visibleRows)
				{
					flag = true;
					this.ScrollBar.Visible = flag2;
					Size ourSize = this.GetOurSize();
					if (this.labelWidth != -1 && ourSize.Width > 0)
					{
						if (this.labelWidth > this.ptOurLocation.X + ourSize.Width)
						{
							this.labelWidth = this.ptOurLocation.X + (int)((double)ourSize.Width / this.labelRatio);
						}
						else
						{
							this.labelRatio = (double)this.GetOurSize().Width / (double)(this.labelWidth - this.ptOurLocation.X);
						}
					}
					base.Invalidate();
				}
			}
			return flag;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0015D24C File Offset: 0x0015B44C
		public DialogResult ShowDialog(Form dialog)
		{
			if (dialog.StartPosition == FormStartPosition.CenterScreen)
			{
				Control control = this;
				if (control != null)
				{
					while (control.ParentInternal != null)
					{
						control = control.ParentInternal;
					}
					if (control.Size.Equals(dialog.Size))
					{
						dialog.StartPosition = FormStartPosition.Manual;
						Point location = control.Location;
						location.Offset(25, 25);
						dialog.Location = location;
					}
				}
			}
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			DialogResult dialogResult;
			if (iuiservice != null)
			{
				dialogResult = iuiservice.ShowDialog(dialog);
			}
			else
			{
				dialogResult = dialog.ShowDialog(this);
			}
			if (focus != IntPtr.Zero)
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(null, focus));
			}
			return dialogResult;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0015D308 File Offset: 0x0015B508
		private void ShowFormatExceptionMessage(string propName, object value, Exception ex)
		{
			if (value == null)
			{
				value = "(null)";
			}
			if (propName == null)
			{
				propName = "(unknown)";
			}
			bool hookMouseDown = this.Edit.HookMouseDown;
			this.Edit.DisableMouseHook = true;
			this.SetCommitError(2, false);
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			while (UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 512, 522, 1))
			{
			}
			if (ex is TargetInvocationException)
			{
				ex = ex.InnerException;
			}
			string text = ex.Message;
			while (text == null || text.Length == 0)
			{
				ex = ex.InnerException;
				if (ex == null)
				{
					break;
				}
				text = ex.Message;
			}
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			this.ErrorDialog.Message = SR.GetString("PBRSFormatExceptionMessage");
			this.ErrorDialog.Text = SR.GetString("PBRSErrorTitle");
			this.ErrorDialog.Details = text;
			bool flag;
			if (iuiservice != null)
			{
				flag = DialogResult.Cancel == iuiservice.ShowDialog(this.ErrorDialog);
			}
			else
			{
				flag = DialogResult.Cancel == this.ShowDialog(this.ErrorDialog);
			}
			this.Edit.DisableMouseHook = false;
			if (hookMouseDown)
			{
				this.SelectGridEntry(this.selectedGridEntry, true);
			}
			this.SetCommitError(1, hookMouseDown);
			if (flag)
			{
				this.OnEscape(this.Edit);
			}
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0015D44C File Offset: 0x0015B64C
		internal void ShowInvalidMessage(string propName, object value, Exception ex)
		{
			if (value == null)
			{
				value = "(null)";
			}
			if (propName == null)
			{
				propName = "(unknown)";
			}
			bool hookMouseDown = this.Edit.HookMouseDown;
			this.Edit.DisableMouseHook = true;
			this.SetCommitError(2, false);
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			while (UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 512, 522, 1))
			{
			}
			if (ex is TargetInvocationException)
			{
				ex = ex.InnerException;
			}
			string text = ex.Message;
			while (text == null || text.Length == 0)
			{
				ex = ex.InnerException;
				if (ex == null)
				{
					break;
				}
				text = ex.Message;
			}
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			this.ErrorDialog.Message = SR.GetString("PBRSErrorInvalidPropertyValue");
			this.ErrorDialog.Text = SR.GetString("PBRSErrorTitle");
			this.ErrorDialog.Details = text;
			bool flag;
			if (iuiservice != null)
			{
				flag = DialogResult.Cancel == iuiservice.ShowDialog(this.ErrorDialog);
			}
			else
			{
				flag = DialogResult.Cancel == this.ShowDialog(this.ErrorDialog);
			}
			this.Edit.DisableMouseHook = false;
			if (hookMouseDown)
			{
				this.SelectGridEntry(this.selectedGridEntry, true);
			}
			this.SetCommitError(1, hookMouseDown);
			if (flag)
			{
				this.OnEscape(this.Edit);
			}
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0015D58E File Offset: 0x0015B78E
		private bool SplitterInside(int x, int y)
		{
			return Math.Abs(x - this.InternalLabelWidth) < 4;
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0015D5A0 File Offset: 0x0015B7A0
		private void TabSelection()
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return;
			}
			if (this.Edit.Visible)
			{
				this.Edit.FocusInternal();
				this.SelectEdit(false);
				return;
			}
			if (this.dropDownHolder != null && this.dropDownHolder.Visible)
			{
				this.dropDownHolder.FocusComponent();
				return;
			}
			if (this.currentEditor != null)
			{
				this.currentEditor.FocusInternal();
			}
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0015D614 File Offset: 0x0015B814
		internal void RemoveSelectedEntryHelpAttributes()
		{
			this.UpdateHelpAttributes(this.selectedGridEntry, null);
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0015D624 File Offset: 0x0015B824
		private void UpdateHelpAttributes(GridEntry oldEntry, GridEntry newEntry)
		{
			IHelpService helpService = this.GetHelpService();
			if (helpService == null || oldEntry == newEntry)
			{
				return;
			}
			GridEntry gridEntry = oldEntry;
			if (oldEntry != null && !oldEntry.Disposed)
			{
				while (gridEntry != null)
				{
					helpService.RemoveContextAttribute("Keyword", gridEntry.HelpKeyword);
					gridEntry = gridEntry.ParentGridEntry;
				}
			}
			if (newEntry != null)
			{
				this.UpdateHelpAttributes(helpService, newEntry, true);
			}
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0015D678 File Offset: 0x0015B878
		private void UpdateHelpAttributes(IHelpService helpSvc, GridEntry entry, bool addAsF1)
		{
			if (entry == null)
			{
				return;
			}
			this.UpdateHelpAttributes(helpSvc, entry.ParentGridEntry, false);
			string helpKeyword = entry.HelpKeyword;
			if (helpKeyword != null)
			{
				helpSvc.AddContextAttribute("Keyword", helpKeyword, addAsF1 ? HelpKeywordType.F1Keyword : HelpKeywordType.GeneralKeyword);
			}
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0015D6B4 File Offset: 0x0015B8B4
		private void UpdateUIBasedOnFont(bool layoutRequired)
		{
			if (base.IsHandleCreated && this.GetFlag(128))
			{
				try
				{
					if (this.listBox != null)
					{
						this.DropDownListBox.ItemHeight = this.RowHeight + 2;
					}
					if (this.btnDropDown != null)
					{
						if (DpiHelper.EnableDpiChangedHighDpiImprovements)
						{
							this.btnDropDown.Size = new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight);
						}
						else
						{
							this.btnDropDown.Size = new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight);
						}
						if (this.btnDialog != null)
						{
							this.DialogButton.Size = this.DropDownButton.Size;
							if (DpiHelper.EnableDpiChangedHighDpiImprovements)
							{
								this.btnDialog.Image = this.CreateResizedBitmap("dotdotdot.ico", 7, 8);
							}
						}
						if (DpiHelper.EnableDpiChangedHighDpiImprovements)
						{
							this.btnDropDown.Image = this.CreateResizedBitmap("Arrow.ico", 16, 16);
						}
					}
					if (layoutRequired)
					{
						this.LayoutWindow(true);
					}
				}
				finally
				{
					this.SetFlag(128, false);
				}
			}
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0015D7D0 File Offset: 0x0015B9D0
		private bool UnfocusSelection()
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return true;
			}
			bool flag = this.Commit();
			if (flag && this.FocusInside)
			{
				this.FocusInternal();
			}
			return flag;
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x0015D80C File Offset: 0x0015BA0C
		private void UpdateResetCommand(GridEntry gridEntry)
		{
			if (this.totalProps > 0)
			{
				IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
				if (menuCommandService != null)
				{
					MenuCommand menuCommand = menuCommandService.FindCommand(PropertyGridCommands.Reset);
					if (menuCommand != null)
					{
						menuCommand.Enabled = gridEntry != null && gridEntry.CanResetPropertyValue();
					}
				}
			}
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0015D85C File Offset: 0x0015BA5C
		internal bool WantsTab(bool forward)
		{
			if (forward)
			{
				if (this.Focused)
				{
					if (this.DropDownButton.Visible || this.DialogButton.Visible || this.Edit.Visible)
					{
						return true;
					}
				}
				else if (this.Edit.Focused && (this.DropDownButton.Visible || this.DialogButton.Visible))
				{
					return true;
				}
				return this.ownerGrid.WantsTab(forward);
			}
			return this.Edit.Focused || this.DropDownButton.Focused || this.DialogButton.Focused || this.ownerGrid.WantsTab(forward);
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0015D908 File Offset: 0x0015BB08
		private unsafe bool WmNotify(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)(void*)m.LParam;
				if (ptr->hwndFrom == this.ToolTip.Handle)
				{
					int code = ptr->code;
					if (code != -522 && code == -521)
					{
						Point point = Cursor.Position;
						point = base.PointToClientInternal(point);
						point = this.FindPosition(point.X, point.Y);
						if (!(point == PropertyGridView.InvalidPosition))
						{
							GridEntry gridEntryFromRow = this.GetGridEntryFromRow(point.Y);
							if (gridEntryFromRow != null)
							{
								Rectangle rectangle = this.GetRectangle(point.Y, point.X);
								Point point2 = Point.Empty;
								if (point.X == 1)
								{
									point2 = gridEntryFromRow.GetLabelToolTipLocation(point.X - rectangle.X, point.Y - rectangle.Y);
								}
								else
								{
									if (point.X != 2)
									{
										return false;
									}
									point2 = gridEntryFromRow.ValueToolTipLocation;
								}
								if (point2 != PropertyGridView.InvalidPoint)
								{
									rectangle.Offset(point2);
									this.ToolTip.PositionToolTip(this, rectangle);
									m.Result = (IntPtr)1;
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x0015DA4C File Offset: 0x0015BC4C
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 135)
			{
				if (msg <= 21)
				{
					if (msg != 7)
					{
						if (msg == 21)
						{
							base.Invalidate();
						}
					}
					else if (!this.GetInPropertySet() && this.Edit.Visible && (this.errorState != 0 || !this.Commit()))
					{
						base.WndProc(ref m);
						this.Edit.FocusInternal();
						return;
					}
				}
				else if (msg != 78)
				{
					if (msg == 135)
					{
						int num = 129;
						if (this.selectedGridEntry != null && (Control.ModifierKeys & Keys.Shift) == Keys.None && this.edit.Visible)
						{
							num |= 2;
						}
						m.Result = (IntPtr)num;
						return;
					}
				}
				else if (this.WmNotify(ref m))
				{
					return;
				}
			}
			else if (msg <= 271)
			{
				if (msg == 269)
				{
					this.Edit.FocusInternal();
					this.Edit.Clear();
					UnsafeNativeMethods.PostMessage(new HandleRef(this.Edit, this.Edit.Handle), 269, 0, 0);
					return;
				}
				if (msg == 271)
				{
					this.Edit.FocusInternal();
					UnsafeNativeMethods.PostMessage(new HandleRef(this.Edit, this.Edit.Handle), 271, m.WParam, m.LParam);
					return;
				}
			}
			else if (msg != 512)
			{
				if (msg == 1110)
				{
					m.Result = (IntPtr)Math.Min(this.visibleRows, this.totalProps);
					return;
				}
				if (msg == 1111)
				{
					m.Result = (IntPtr)this.GetRowFromGridEntry(this.selectedGridEntry);
					return;
				}
			}
			else
			{
				if ((int)(long)m.LParam == this.lastMouseMove)
				{
					return;
				}
				this.lastMouseMove = (int)(long)m.LParam;
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0015DC46 File Offset: 0x0015BE46
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.RescaleConstants();
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0015DC58 File Offset: 0x0015BE58
		private void RescaleConstants()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.ClearCachedFontInfo();
				this.cachedRowHeight = -1;
				this.paintWidth = base.LogicalToDeviceUnits(20);
				this.paintIndent = base.LogicalToDeviceUnits(26);
				this.outlineSizeExplorerTreeStyle = base.LogicalToDeviceUnits(16);
				this.outlineSize = base.LogicalToDeviceUnits(9);
				this.maxListBoxHeight = base.LogicalToDeviceUnits(200);
				this.offset_2Units = base.LogicalToDeviceUnits(PropertyGridView.OFFSET_2PIXELS);
				if (this.topLevelGridEntries != null)
				{
					foreach (object obj in this.topLevelGridEntries)
					{
						GridEntry gridEntry = (GridEntry)obj;
						this.ResetOutline(gridEntry);
					}
				}
			}
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0015DD2C File Offset: 0x0015BF2C
		private void ResetOutline(GridEntry entry)
		{
			entry.OutlineRect = Rectangle.Empty;
			if (entry.ChildCount > 0)
			{
				foreach (object obj in entry.Children)
				{
					GridEntry gridEntry = (GridEntry)obj;
					this.ResetOutline(gridEntry);
				}
			}
		}

		// Token: 0x04003654 RID: 13908
		protected static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		// Token: 0x04003655 RID: 13909
		public const int RENDERMODE_LEFTDOT = 2;

		// Token: 0x04003656 RID: 13910
		public const int RENDERMODE_BOLD = 3;

		// Token: 0x04003657 RID: 13911
		public const int RENDERMODE_TRIANGLE = 4;

		// Token: 0x04003658 RID: 13912
		public static int inheritRenderMode = 3;

		// Token: 0x04003659 RID: 13913
		public static TraceSwitch GridViewDebugPaint = new TraceSwitch("GridViewDebugPaint", "PropertyGridView: Debug property painting");

		// Token: 0x0400365A RID: 13914
		private PropertyGrid ownerGrid;

		// Token: 0x0400365B RID: 13915
		private const int LEFTDOT_SIZE = 4;

		// Token: 0x0400365C RID: 13916
		private const int EDIT_INDENT = 0;

		// Token: 0x0400365D RID: 13917
		private const int OUTLINE_INDENT = 10;

		// Token: 0x0400365E RID: 13918
		private const int OUTLINE_SIZE = 9;

		// Token: 0x0400365F RID: 13919
		private const int OUTLINE_SIZE_EXPLORER_TREE_STYLE = 16;

		// Token: 0x04003660 RID: 13920
		private int outlineSize = 9;

		// Token: 0x04003661 RID: 13921
		private int outlineSizeExplorerTreeStyle = 16;

		// Token: 0x04003662 RID: 13922
		private const int PAINT_WIDTH = 20;

		// Token: 0x04003663 RID: 13923
		private int paintWidth = 20;

		// Token: 0x04003664 RID: 13924
		private const int PAINT_INDENT = 26;

		// Token: 0x04003665 RID: 13925
		private int paintIndent = 26;

		// Token: 0x04003666 RID: 13926
		private const int ROWLABEL = 1;

		// Token: 0x04003667 RID: 13927
		private const int ROWVALUE = 2;

		// Token: 0x04003668 RID: 13928
		private const int MAX_LISTBOX_HEIGHT = 200;

		// Token: 0x04003669 RID: 13929
		private int maxListBoxHeight = 200;

		// Token: 0x0400366A RID: 13930
		private const short ERROR_NONE = 0;

		// Token: 0x0400366B RID: 13931
		private const short ERROR_THROWN = 1;

		// Token: 0x0400366C RID: 13932
		private const short ERROR_MSGBOX_UP = 2;

		// Token: 0x0400366D RID: 13933
		internal const short GDIPLUS_SPACE = 2;

		// Token: 0x0400366E RID: 13934
		internal const int MaxRecurseExpand = 10;

		// Token: 0x0400366F RID: 13935
		private const int DOTDOTDOT_ICONWIDTH = 7;

		// Token: 0x04003670 RID: 13936
		private const int DOTDOTDOT_ICONHEIGHT = 8;

		// Token: 0x04003671 RID: 13937
		private const int DOWNARROW_ICONWIDTH = 16;

		// Token: 0x04003672 RID: 13938
		private const int DOWNARROW_ICONHEIGHT = 16;

		// Token: 0x04003673 RID: 13939
		private static int OFFSET_2PIXELS = 2;

		// Token: 0x04003674 RID: 13940
		private int offset_2Units = PropertyGridView.OFFSET_2PIXELS;

		// Token: 0x04003675 RID: 13941
		protected static readonly Point InvalidPosition = new Point(int.MinValue, int.MinValue);

		// Token: 0x04003676 RID: 13942
		private Brush backgroundBrush;

		// Token: 0x04003677 RID: 13943
		private Font fontBold;

		// Token: 0x04003678 RID: 13944
		private Color grayTextColor;

		// Token: 0x04003679 RID: 13945
		private bool grayTextColorModified;

		// Token: 0x0400367A RID: 13946
		private GridEntryCollection topLevelGridEntries;

		// Token: 0x0400367B RID: 13947
		private GridEntryCollection allGridEntries;

		// Token: 0x0400367C RID: 13948
		internal int totalProps = -1;

		// Token: 0x0400367D RID: 13949
		private int visibleRows = -1;

		// Token: 0x0400367E RID: 13950
		private int labelWidth = -1;

		// Token: 0x0400367F RID: 13951
		public double labelRatio = 2.0;

		// Token: 0x04003680 RID: 13952
		private short requiredLabelPaintMargin = 2;

		// Token: 0x04003681 RID: 13953
		private int selectedRow = -1;

		// Token: 0x04003682 RID: 13954
		private GridEntry selectedGridEntry;

		// Token: 0x04003683 RID: 13955
		private int tipInfo = -1;

		// Token: 0x04003684 RID: 13956
		private PropertyGridView.GridViewEdit edit;

		// Token: 0x04003685 RID: 13957
		private DropDownButton btnDropDown;

		// Token: 0x04003686 RID: 13958
		private DropDownButton btnDialog;

		// Token: 0x04003687 RID: 13959
		private PropertyGridView.GridViewListBox listBox;

		// Token: 0x04003688 RID: 13960
		private PropertyGridView.DropDownHolder dropDownHolder;

		// Token: 0x04003689 RID: 13961
		private Rectangle lastClientRect = Rectangle.Empty;

		// Token: 0x0400368A RID: 13962
		private Control currentEditor;

		// Token: 0x0400368B RID: 13963
		private ScrollBar scrollBar;

		// Token: 0x0400368C RID: 13964
		internal GridToolTip toolTip;

		// Token: 0x0400368D RID: 13965
		private GridErrorDlg errorDlg;

		// Token: 0x0400368E RID: 13966
		private const short FlagNeedsRefresh = 1;

		// Token: 0x0400368F RID: 13967
		private const short FlagIsNewSelection = 2;

		// Token: 0x04003690 RID: 13968
		private const short FlagIsSplitterMove = 4;

		// Token: 0x04003691 RID: 13969
		private const short FlagIsSpecialKey = 8;

		// Token: 0x04003692 RID: 13970
		private const short FlagInPropertySet = 16;

		// Token: 0x04003693 RID: 13971
		private const short FlagDropDownClosing = 32;

		// Token: 0x04003694 RID: 13972
		private const short FlagDropDownCommit = 64;

		// Token: 0x04003695 RID: 13973
		private const short FlagNeedUpdateUIBasedOnFont = 128;

		// Token: 0x04003696 RID: 13974
		private const short FlagBtnLaunchedEditor = 256;

		// Token: 0x04003697 RID: 13975
		private const short FlagNoDefault = 512;

		// Token: 0x04003698 RID: 13976
		private const short FlagResizableDropDown = 1024;

		// Token: 0x04003699 RID: 13977
		private short flags = 131;

		// Token: 0x0400369A RID: 13978
		private short errorState;

		// Token: 0x0400369B RID: 13979
		private Point ptOurLocation = new Point(1, 1);

		// Token: 0x0400369C RID: 13980
		private string originalTextValue;

		// Token: 0x0400369D RID: 13981
		private int cumulativeVerticalWheelDelta;

		// Token: 0x0400369E RID: 13982
		private long rowSelectTime;

		// Token: 0x0400369F RID: 13983
		private Point rowSelectPos = Point.Empty;

		// Token: 0x040036A0 RID: 13984
		private Point lastMouseDown = PropertyGridView.InvalidPosition;

		// Token: 0x040036A1 RID: 13985
		private int lastMouseMove;

		// Token: 0x040036A2 RID: 13986
		private GridEntry lastClickedEntry;

		// Token: 0x040036A3 RID: 13987
		private IServiceProvider serviceProvider;

		// Token: 0x040036A4 RID: 13988
		private IHelpService topHelpService;

		// Token: 0x040036A5 RID: 13989
		private IHelpService helpService;

		// Token: 0x040036A6 RID: 13990
		private EventHandler ehValueClick;

		// Token: 0x040036A7 RID: 13991
		private EventHandler ehLabelClick;

		// Token: 0x040036A8 RID: 13992
		private EventHandler ehOutlineClick;

		// Token: 0x040036A9 RID: 13993
		private EventHandler ehValueDblClick;

		// Token: 0x040036AA RID: 13994
		private EventHandler ehLabelDblClick;

		// Token: 0x040036AB RID: 13995
		private GridEntryRecreateChildrenEventHandler ehRecreateChildren;

		// Token: 0x040036AC RID: 13996
		private int cachedRowHeight = -1;

		// Token: 0x040036AD RID: 13997
		private IntPtr baseHfont;

		// Token: 0x040036AE RID: 13998
		private IntPtr boldHfont;

		// Token: 0x040036AF RID: 13999
		private PropertyGridView.GridPositionData positionData;

		// Token: 0x0200087E RID: 2174
		private class GridViewEdit : TextBox, PropertyGridView.IMouseHookClient
		{
			// Token: 0x170018D9 RID: 6361
			// (set) Token: 0x0600714D RID: 29005 RVA: 0x0019E335 File Offset: 0x0019C535
			public bool DontFocus
			{
				set
				{
					this.dontFocusMe = value;
				}
			}

			// Token: 0x170018DA RID: 6362
			// (get) Token: 0x0600714E RID: 29006 RVA: 0x0019E33E File Offset: 0x0019C53E
			// (set) Token: 0x0600714F RID: 29007 RVA: 0x0019E346 File Offset: 0x0019C546
			public virtual bool Filter
			{
				get
				{
					return this.filter;
				}
				set
				{
					this.filter = value;
				}
			}

			// Token: 0x170018DB RID: 6363
			// (get) Token: 0x06007150 RID: 29008 RVA: 0x000A83A1 File Offset: 0x000A65A1
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x170018DC RID: 6364
			// (get) Token: 0x06007151 RID: 29009 RVA: 0x0019E34F File Offset: 0x0019C54F
			public override bool Focused
			{
				get
				{
					return !this.dontFocusMe && base.Focused;
				}
			}

			// Token: 0x170018DD RID: 6365
			// (get) Token: 0x06007152 RID: 29010 RVA: 0x00166F0E File Offset: 0x0016510E
			// (set) Token: 0x06007153 RID: 29011 RVA: 0x0019E361 File Offset: 0x0019C561
			public override string Text
			{
				get
				{
					return base.Text;
				}
				set
				{
					this.fInSetText = true;
					base.Text = value;
					this.fInSetText = false;
				}
			}

			// Token: 0x170018DE RID: 6366
			// (set) Token: 0x06007154 RID: 29012 RVA: 0x0019E378 File Offset: 0x0019C578
			public bool DisableMouseHook
			{
				set
				{
					this.mouseHook.DisableMouseHook = value;
				}
			}

			// Token: 0x170018DF RID: 6367
			// (get) Token: 0x06007155 RID: 29013 RVA: 0x0019E386 File Offset: 0x0019C586
			// (set) Token: 0x06007156 RID: 29014 RVA: 0x0019E393 File Offset: 0x0019C593
			public virtual bool HookMouseDown
			{
				get
				{
					return this.mouseHook.HookMouseDown;
				}
				set
				{
					this.mouseHook.HookMouseDown = value;
					if (value)
					{
						this.FocusInternal();
					}
				}
			}

			// Token: 0x06007157 RID: 29015 RVA: 0x0019E3AB File Offset: 0x0019C5AB
			public GridViewEdit(PropertyGridView psheet)
			{
				this.psheet = psheet;
				this.mouseHook = new PropertyGridView.MouseHook(this, this, psheet);
			}

			// Token: 0x06007158 RID: 29016 RVA: 0x0019E3C8 File Offset: 0x0019C5C8
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level5)
				{
					return new PropertyGridView.GridViewEdit.GridViewEditAccessibleObjectLevel5(this);
				}
				if (AccessibilityImprovements.Level2)
				{
					return new PropertyGridView.GridViewEdit.GridViewEditAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x06007159 RID: 29017 RVA: 0x0019E3EC File Offset: 0x0019C5EC
			protected override void DestroyHandle()
			{
				this.mouseHook.HookMouseDown = false;
				base.DestroyHandle();
			}

			// Token: 0x0600715A RID: 29018 RVA: 0x0019E400 File Offset: 0x0019C600
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.mouseHook.Dispose();
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600715B RID: 29019 RVA: 0x0019E417 File Offset: 0x0019C617
			public void FilterKeyPress(char keyChar)
			{
				if (this.IsInputChar(keyChar))
				{
					this.FocusInternal();
					base.SelectAll();
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 258, (IntPtr)((int)keyChar), IntPtr.Zero);
				}
			}

			// Token: 0x0600715C RID: 29020 RVA: 0x0019E454 File Offset: 0x0019C654
			protected override bool IsInputKey(Keys keyData)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Return)
				{
					if (keys != Keys.Tab && keys != Keys.Return)
					{
						goto IL_2A;
					}
				}
				else if (keys != Keys.Escape && keys != Keys.F1 && keys != Keys.F4)
				{
					goto IL_2A;
				}
				return false;
				IL_2A:
				return !this.psheet.NeedsCommit && base.IsInputKey(keyData);
			}

			// Token: 0x0600715D RID: 29021 RVA: 0x0019E4A4 File Offset: 0x0019C6A4
			protected override bool IsInputChar(char keyChar)
			{
				return keyChar != '\t' && keyChar != '\r' && base.IsInputChar(keyChar);
			}

			// Token: 0x0600715E RID: 29022 RVA: 0x0019E4C6 File Offset: 0x0019C6C6
			protected override void OnKeyDown(KeyEventArgs ke)
			{
				if (this.ProcessDialogKey(ke.KeyData))
				{
					ke.Handled = true;
					return;
				}
				base.OnKeyDown(ke);
			}

			// Token: 0x0600715F RID: 29023 RVA: 0x0019E4E5 File Offset: 0x0019C6E5
			protected override void OnKeyPress(KeyPressEventArgs ke)
			{
				if (!this.IsInputChar(ke.KeyChar))
				{
					ke.Handled = true;
					return;
				}
				base.OnKeyPress(ke);
			}

			// Token: 0x06007160 RID: 29024 RVA: 0x0019E504 File Offset: 0x0019C704
			public bool OnClickHooked()
			{
				return !this.psheet._Commit();
			}

			// Token: 0x06007161 RID: 29025 RVA: 0x0019E514 File Offset: 0x0019C714
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				if (!this.Focused)
				{
					Graphics graphics = base.CreateGraphics();
					if (this.psheet.SelectedGridEntry != null && base.ClientRectangle.Width <= this.psheet.SelectedGridEntry.GetValueTextWidth(this.Text, graphics, this.Font))
					{
						this.psheet.ToolTip.ToolTip = (this.PasswordProtect ? "" : this.Text);
					}
					graphics.Dispose();
				}
			}

			// Token: 0x06007162 RID: 29026 RVA: 0x0019E59C File Offset: 0x0019C79C
			protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Delete)
				{
					if (keys != Keys.Insert)
					{
						if (keys == Keys.Delete)
						{
							if ((keyData & Keys.Control) == Keys.None && (keyData & Keys.Shift) != Keys.None && (keyData & Keys.Alt) == Keys.None)
							{
								return false;
							}
							if ((keyData & Keys.Control) == Keys.None && (keyData & Keys.Shift) == Keys.None && (keyData & Keys.Alt) == Keys.None && this.psheet.SelectedGridEntry != null && !this.psheet.SelectedGridEntry.Enumerable && !this.psheet.SelectedGridEntry.IsTextEditable && this.psheet.SelectedGridEntry.CanResetPropertyValue())
							{
								object propertyValue = this.psheet.SelectedGridEntry.PropertyValue;
								this.psheet.SelectedGridEntry.ResetPropertyValue();
								this.psheet.UnfocusSelection();
								this.psheet.ownerGrid.OnPropertyValueSet(this.psheet.SelectedGridEntry, propertyValue);
							}
						}
					}
					else if ((keyData & Keys.Alt) == Keys.None && (((keyData & Keys.Control) > Keys.None) ^ ((keyData & Keys.Shift) == Keys.None)))
					{
						return false;
					}
				}
				else if (keys != Keys.A)
				{
					if (keys != Keys.C)
					{
						switch (keys)
						{
						case Keys.V:
						case Keys.X:
						case Keys.Z:
							break;
						case Keys.W:
						case Keys.Y:
							goto IL_195;
						default:
							goto IL_195;
						}
					}
					if ((keyData & Keys.Control) != Keys.None && (keyData & Keys.Shift) == Keys.None && (keyData & Keys.Alt) == Keys.None)
					{
						return false;
					}
				}
				else if ((keyData & Keys.Control) != Keys.None && (keyData & Keys.Shift) == Keys.None && (keyData & Keys.Alt) == Keys.None)
				{
					base.SelectAll();
					return true;
				}
				IL_195:
				return base.ProcessCmdKey(ref msg, keyData);
			}

			// Token: 0x06007163 RID: 29027 RVA: 0x0019E748 File Offset: 0x0019C948
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if ((keyData & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.None)
				{
					Keys keys = keyData & Keys.KeyCode;
					if (keys == Keys.Return)
					{
						bool flag = !this.psheet.NeedsCommit;
						if (this.psheet.UnfocusSelection() && flag && this.psheet.SelectedGridEntry != null)
						{
							this.psheet.SelectedGridEntry.OnValueReturnKey();
						}
						return true;
					}
					if (keys == Keys.Escape)
					{
						this.psheet.OnEscape(this);
						return true;
					}
					if (keys == Keys.F4)
					{
						this.psheet.F4Selection(true);
						return true;
					}
				}
				if ((keyData & Keys.KeyCode) == Keys.Tab && (keyData & (Keys.Control | Keys.Alt)) == Keys.None)
				{
					return !this.psheet._Commit();
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x06007164 RID: 29028 RVA: 0x0019E7FC File Offset: 0x0019C9FC
			protected override void SetVisibleCore(bool value)
			{
				if (!value && this.HookMouseDown)
				{
					this.mouseHook.HookMouseDown = false;
				}
				base.SetVisibleCore(value);
			}

			// Token: 0x06007165 RID: 29029 RVA: 0x0019E81C File Offset: 0x0019CA1C
			internal bool WantsTab(bool forward)
			{
				return this.psheet.WantsTab(forward);
			}

			// Token: 0x06007166 RID: 29030 RVA: 0x0019E82C File Offset: 0x0019CA2C
			private unsafe bool WmNotify(ref Message m)
			{
				if (m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)(void*)m.LParam;
					if (ptr->hwndFrom == this.psheet.ToolTip.Handle)
					{
						int code = ptr->code;
						if (code == -521)
						{
							this.psheet.ToolTip.PositionToolTip(this, base.ClientRectangle);
							m.Result = (IntPtr)1;
							return true;
						}
						this.psheet.WndProc(ref m);
					}
				}
				return false;
			}

			// Token: 0x06007167 RID: 29031 RVA: 0x0019E8B8 File Offset: 0x0019CAB8
			protected override void WndProc(ref Message m)
			{
				if (this.filter && this.psheet.FilterEditWndProc(ref m))
				{
					return;
				}
				int msg = m.Msg;
				if (msg <= 78)
				{
					if (msg != 2)
					{
						if (msg != 24)
						{
							if (msg == 78)
							{
								if (this.WmNotify(ref m))
								{
									return;
								}
							}
						}
						else if (IntPtr.Zero == m.WParam)
						{
							this.mouseHook.HookMouseDown = false;
						}
					}
					else
					{
						this.mouseHook.HookMouseDown = false;
					}
				}
				else if (msg <= 135)
				{
					if (msg != 125)
					{
						if (msg == 135)
						{
							m.Result = (IntPtr)((long)m.Result | 1L | 128L);
							if (this.psheet.NeedsCommit || this.WantsTab((Control.ModifierKeys & Keys.Shift) == Keys.None))
							{
								m.Result = (IntPtr)((long)m.Result | 4L | 2L);
							}
							return;
						}
					}
					else if (((int)(long)m.WParam & -20) != 0)
					{
						this.psheet.Invalidate();
					}
				}
				else if (msg != 512)
				{
					if (msg == 770)
					{
						if (base.ReadOnly)
						{
							return;
						}
					}
				}
				else
				{
					if ((int)(long)m.LParam == this.lastMove)
					{
						return;
					}
					this.lastMove = (int)(long)m.LParam;
				}
				base.WndProc(ref m);
			}

			// Token: 0x06007168 RID: 29032 RVA: 0x0019EA37 File Offset: 0x0019CC37
			public virtual bool InSetText()
			{
				return this.fInSetText;
			}

			// Token: 0x04004476 RID: 17526
			internal bool fInSetText;

			// Token: 0x04004477 RID: 17527
			internal bool filter;

			// Token: 0x04004478 RID: 17528
			internal PropertyGridView psheet;

			// Token: 0x04004479 RID: 17529
			private bool dontFocusMe;

			// Token: 0x0400447A RID: 17530
			private int lastMove;

			// Token: 0x0400447B RID: 17531
			private PropertyGridView.MouseHook mouseHook;

			// Token: 0x0200097C RID: 2428
			[ComVisible(true)]
			private class GridViewEditAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
			{
				// Token: 0x06007546 RID: 30022 RVA: 0x001A7AF4 File Offset: 0x001A5CF4
				public GridViewEditAccessibleObjectLevel5(PropertyGridView.GridViewEdit owner)
					: base(owner)
				{
					this._owningPropertyGridView = owner.psheet;
				}

				// Token: 0x17001AF6 RID: 6902
				// (get) Token: 0x06007547 RID: 30023 RVA: 0x001A7B0C File Offset: 0x001A5D0C
				public override AccessibleStates State
				{
					get
					{
						AccessibleStates accessibleStates = base.State;
						if (this.IsReadOnly)
						{
							accessibleStates |= AccessibleStates.ReadOnly;
						}
						else
						{
							accessibleStates &= ~AccessibleStates.ReadOnly;
						}
						return accessibleStates;
					}
				}

				// Token: 0x06007548 RID: 30024 RVA: 0x001A7B38 File Offset: 0x001A5D38
				internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
				{
					GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
					PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject = ((selectedGridEntry != null) ? selectedGridEntry.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
					if (propertyDescriptorGridEntryAccessibleObject == null)
					{
						return null;
					}
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.Parent:
						return propertyDescriptorGridEntryAccessibleObject;
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetNextChildFragment(this);
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetPreviousChildFragment(this);
					default:
						return base.FragmentNavigate(direction);
					}
				}

				// Token: 0x17001AF7 RID: 6903
				// (get) Token: 0x06007549 RID: 30025 RVA: 0x001A7B94 File Offset: 0x001A5D94
				internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
				{
					get
					{
						PropertyGrid ownerGrid = this._owningPropertyGridView.OwnerGrid;
						if (ownerGrid == null)
						{
							return null;
						}
						return ownerGrid.AccessibilityObject;
					}
				}

				// Token: 0x0600754A RID: 30026 RVA: 0x001A7BAC File Offset: 0x001A5DAC
				internal override object GetPropertyValue(int propertyID)
				{
					if (propertyID == 30010)
					{
						return !this.IsReadOnly;
					}
					return base.GetPropertyValue(propertyID);
				}

				// Token: 0x0600754B RID: 30027 RVA: 0x00010ECC File Offset: 0x0000F0CC
				internal override bool IsPatternSupported(int patternId)
				{
					return patternId == 10002 || base.IsPatternSupported(patternId);
				}

				// Token: 0x17001AF8 RID: 6904
				// (get) Token: 0x0600754C RID: 30028 RVA: 0x001A7BCC File Offset: 0x001A5DCC
				// (set) Token: 0x0600754D RID: 30029 RVA: 0x00010E62 File Offset: 0x0000F062
				public override string Name
				{
					get
					{
						if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && base.Owner == null)
						{
							return base.Name;
						}
						string accessibleName = base.Owner.AccessibleName;
						if (accessibleName != null)
						{
							return accessibleName;
						}
						GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
						if (selectedGridEntry != null)
						{
							return selectedGridEntry.AccessibilityObject.Name;
						}
						return base.Name;
					}
					set
					{
						base.Name = value;
					}
				}

				// Token: 0x17001AF9 RID: 6905
				// (get) Token: 0x0600754E RID: 30030 RVA: 0x001A7C24 File Offset: 0x001A5E24
				internal override bool IsReadOnly
				{
					get
					{
						PropertyDescriptorGridEntry propertyDescriptorGridEntry = this._owningPropertyGridView.SelectedGridEntry as PropertyDescriptorGridEntry;
						return propertyDescriptorGridEntry == null || propertyDescriptorGridEntry.IsPropertyReadOnly;
					}
				}

				// Token: 0x0600754F RID: 30031 RVA: 0x0015EE49 File Offset: 0x0015D049
				internal override void SetFocus()
				{
					base.RaiseAutomationEvent(20005);
					base.SetFocus();
				}

				// Token: 0x040047CD RID: 18381
				private readonly PropertyGridView _owningPropertyGridView;
			}

			// Token: 0x0200097D RID: 2429
			[ComVisible(true)]
			protected class GridViewEditAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x06007550 RID: 30032 RVA: 0x001A7C4D File Offset: 0x001A5E4D
				public GridViewEditAccessibleObject(PropertyGridView.GridViewEdit owner)
					: base(owner)
				{
					this.propertyGridView = owner.psheet;
				}

				// Token: 0x17001AFA RID: 6906
				// (get) Token: 0x06007551 RID: 30033 RVA: 0x001A7C64 File Offset: 0x001A5E64
				public override AccessibleStates State
				{
					get
					{
						AccessibleStates accessibleStates = base.State;
						if (this.IsReadOnly)
						{
							accessibleStates |= AccessibleStates.ReadOnly;
						}
						else
						{
							accessibleStates &= ~AccessibleStates.ReadOnly;
						}
						return accessibleStates;
					}
				}

				// Token: 0x06007552 RID: 30034 RVA: 0x00012E4E File Offset: 0x0001104E
				internal override bool IsIAccessibleExSupported()
				{
					return true;
				}

				// Token: 0x06007553 RID: 30035 RVA: 0x001A7C90 File Offset: 0x001A5E90
				internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
				{
					if (AccessibilityImprovements.Level3)
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.Parent && this.propertyGridView.SelectedGridEntry != null)
						{
							return this.propertyGridView.SelectedGridEntry.AccessibilityObject;
						}
						if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
						{
							if (this.propertyGridView.DropDownButton.Visible)
							{
								return this.propertyGridView.DropDownButton.AccessibilityObject;
							}
							if (this.propertyGridView.DialogButton.Visible)
							{
								return this.propertyGridView.DialogButton.AccessibilityObject;
							}
						}
					}
					return base.FragmentNavigate(direction);
				}

				// Token: 0x17001AFB RID: 6907
				// (get) Token: 0x06007554 RID: 30036 RVA: 0x001A7D16 File Offset: 0x001A5F16
				internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
				{
					get
					{
						if (AccessibilityImprovements.Level3)
						{
							return this.propertyGridView.AccessibilityObject;
						}
						return base.FragmentRoot;
					}
				}

				// Token: 0x06007555 RID: 30037 RVA: 0x001A7D34 File Offset: 0x001A5F34
				internal override object GetPropertyValue(int propertyID)
				{
					if (propertyID == 30010)
					{
						return !this.IsReadOnly;
					}
					if (propertyID == 30043)
					{
						return this.IsPatternSupported(10002);
					}
					if (AccessibilityImprovements.Level3)
					{
						if (propertyID == 30003)
						{
							return 50004;
						}
						if (propertyID == 30005)
						{
							return this.Name;
						}
					}
					return base.GetPropertyValue(propertyID);
				}

				// Token: 0x06007556 RID: 30038 RVA: 0x000A880D File Offset: 0x000A6A0D
				internal override bool IsPatternSupported(int patternId)
				{
					return patternId == 10002 || base.IsPatternSupported(patternId);
				}

				// Token: 0x17001AFC RID: 6908
				// (get) Token: 0x06007557 RID: 30039 RVA: 0x001A7DA4 File Offset: 0x001A5FA4
				// (set) Token: 0x06007558 RID: 30040 RVA: 0x00010E62 File Offset: 0x0000F062
				public override string Name
				{
					get
					{
						if (AccessibilityImprovements.Level3)
						{
							string accessibleName = base.Owner.AccessibleName;
							if (accessibleName != null)
							{
								return accessibleName;
							}
							GridEntry selectedGridEntry = this.propertyGridView.SelectedGridEntry;
							if (selectedGridEntry != null)
							{
								return selectedGridEntry.AccessibilityObject.Name;
							}
						}
						return base.Name;
					}
					set
					{
						base.Name = value;
					}
				}

				// Token: 0x17001AFD RID: 6909
				// (get) Token: 0x06007559 RID: 30041 RVA: 0x001A7DEC File Offset: 0x001A5FEC
				internal override bool IsReadOnly
				{
					get
					{
						PropertyDescriptorGridEntry propertyDescriptorGridEntry = this.propertyGridView.SelectedGridEntry as PropertyDescriptorGridEntry;
						return propertyDescriptorGridEntry == null || propertyDescriptorGridEntry.IsPropertyReadOnly;
					}
				}

				// Token: 0x0600755A RID: 30042 RVA: 0x001A7E15 File Offset: 0x001A6015
				internal override void SetFocus()
				{
					if (AccessibilityImprovements.Level3)
					{
						base.RaiseAutomationEvent(20005);
					}
					base.SetFocus();
				}

				// Token: 0x040047CE RID: 18382
				private PropertyGridView propertyGridView;
			}
		}

		// Token: 0x0200087F RID: 2175
		internal class DropDownHolder : Form, PropertyGridView.IMouseHookClient
		{
			// Token: 0x06007169 RID: 29033 RVA: 0x0019EA40 File Offset: 0x0019CC40
			internal DropDownHolder(PropertyGridView psheet)
			{
				this.MinDropDownSize = new Size(SystemInformation.VerticalScrollBarWidth * 4, SystemInformation.HorizontalScrollBarHeight * 4);
				this.ResizeGripSize = SystemInformation.HorizontalScrollBarHeight;
				this.ResizeBarSize = this.ResizeGripSize + 1;
				this.ResizeBorderSize = this.ResizeBarSize / 2;
				base.ShowInTaskbar = false;
				base.ControlBox = false;
				base.MinimizeBox = false;
				base.MaximizeBox = false;
				this.Text = "";
				base.FormBorderStyle = FormBorderStyle.None;
				base.AutoScaleMode = AutoScaleMode.None;
				this.mouseHook = new PropertyGridView.MouseHook(this, this, psheet);
				base.Visible = false;
				this.gridView = psheet;
				this.BackColor = this.gridView.BackColor;
			}

			// Token: 0x170018E0 RID: 6368
			// (get) Token: 0x0600716A RID: 29034 RVA: 0x0019EB14 File Offset: 0x0019CD14
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.ExStyle |= 128;
					createParams.Style |= -2139095040;
					if (OSFeature.IsPresent(SystemParameter.DropShadow))
					{
						createParams.ClassStyle |= 131072;
					}
					if (this.gridView != null)
					{
						createParams.Parent = this.gridView.ParentInternal.Handle;
					}
					return createParams;
				}
			}

			// Token: 0x170018E1 RID: 6369
			// (get) Token: 0x0600716B RID: 29035 RVA: 0x0019EB85 File Offset: 0x0019CD85
			private LinkLabel CreateNewLink
			{
				get
				{
					if (this.createNewLink == null)
					{
						this.createNewLink = new LinkLabel();
						this.createNewLink.LinkClicked += this.OnNewLinkClicked;
					}
					return this.createNewLink;
				}
			}

			// Token: 0x170018E2 RID: 6370
			// (get) Token: 0x0600716C RID: 29036 RVA: 0x0019EBB7 File Offset: 0x0019CDB7
			// (set) Token: 0x0600716D RID: 29037 RVA: 0x0019EBC4 File Offset: 0x0019CDC4
			public virtual bool HookMouseDown
			{
				get
				{
					return this.mouseHook.HookMouseDown;
				}
				set
				{
					this.mouseHook.HookMouseDown = value;
				}
			}

			// Token: 0x170018E3 RID: 6371
			// (set) Token: 0x0600716E RID: 29038 RVA: 0x0019EBD4 File Offset: 0x0019CDD4
			public bool ResizeUp
			{
				set
				{
					if (this.resizeUp != value)
					{
						this.sizeGripGlyph = null;
						this.resizeUp = value;
						if (this.resizable)
						{
							base.DockPadding.Bottom = 0;
							base.DockPadding.Top = 0;
							if (value)
							{
								base.DockPadding.Top = this.ResizeBarSize;
								return;
							}
							base.DockPadding.Bottom = this.ResizeBarSize;
						}
					}
				}
			}

			// Token: 0x0600716F RID: 29039 RVA: 0x0019EC3E File Offset: 0x0019CE3E
			protected override void DestroyHandle()
			{
				this.mouseHook.HookMouseDown = false;
				base.DestroyHandle();
			}

			// Token: 0x06007170 RID: 29040 RVA: 0x0019EC52 File Offset: 0x0019CE52
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.createNewLink != null)
				{
					this.createNewLink.Dispose();
					this.createNewLink = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06007171 RID: 29041 RVA: 0x0019EC78 File Offset: 0x0019CE78
			public void DoModalLoop()
			{
				while (base.Visible)
				{
					Application.DoEventsModal();
					UnsafeNativeMethods.MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, 250, 255, 4);
				}
			}

			// Token: 0x170018E4 RID: 6372
			// (get) Token: 0x06007172 RID: 29042 RVA: 0x0019ECA0 File Offset: 0x0019CEA0
			public virtual Control Component
			{
				get
				{
					return this.currentControl;
				}
			}

			// Token: 0x06007173 RID: 29043 RVA: 0x0019ECA8 File Offset: 0x0019CEA8
			private InstanceCreationEditor GetInstanceCreationEditor(PropertyDescriptorGridEntry entry)
			{
				if (entry == null)
				{
					return null;
				}
				InstanceCreationEditor instanceCreationEditor = null;
				PropertyDescriptor propertyDescriptor = entry.PropertyDescriptor;
				if (propertyDescriptor != null)
				{
					instanceCreationEditor = propertyDescriptor.GetEditor(typeof(InstanceCreationEditor)) as InstanceCreationEditor;
				}
				if (instanceCreationEditor == null)
				{
					UITypeEditor uitypeEditor = entry.UITypeEditor;
					if (uitypeEditor != null && uitypeEditor.GetEditStyle() == UITypeEditorEditStyle.DropDown)
					{
						instanceCreationEditor = (InstanceCreationEditor)TypeDescriptor.GetEditor(uitypeEditor, typeof(InstanceCreationEditor));
					}
				}
				return instanceCreationEditor;
			}

			// Token: 0x06007174 RID: 29044 RVA: 0x0019ED0C File Offset: 0x0019CF0C
			private Bitmap GetSizeGripGlyph(Graphics g)
			{
				if (this.sizeGripGlyph != null)
				{
					return this.sizeGripGlyph;
				}
				this.sizeGripGlyph = new Bitmap(this.ResizeGripSize, this.ResizeGripSize, g);
				using (Graphics graphics = Graphics.FromImage(this.sizeGripGlyph))
				{
					Matrix matrix = new Matrix();
					matrix.Translate((float)(this.ResizeGripSize + 1), (float)(this.resizeUp ? (this.ResizeGripSize + 1) : 0));
					matrix.Scale(-1f, (float)(this.resizeUp ? (-1) : 1));
					graphics.Transform = matrix;
					ControlPaint.DrawSizeGrip(graphics, this.BackColor, 0, 0, this.ResizeGripSize, this.ResizeGripSize);
					graphics.ResetTransform();
				}
				this.sizeGripGlyph.MakeTransparent(this.BackColor);
				return this.sizeGripGlyph;
			}

			// Token: 0x06007175 RID: 29045 RVA: 0x0019EDE8 File Offset: 0x0019CFE8
			public virtual bool GetUsed()
			{
				return this.currentControl != null;
			}

			// Token: 0x06007176 RID: 29046 RVA: 0x0019EDF3 File Offset: 0x0019CFF3
			public virtual void FocusComponent()
			{
				if (this.currentControl != null && base.Visible)
				{
					this.currentControl.FocusInternal();
				}
			}

			// Token: 0x06007177 RID: 29047 RVA: 0x0019EE14 File Offset: 0x0019D014
			private bool OwnsWindow(IntPtr hWnd)
			{
				while (hWnd != IntPtr.Zero)
				{
					hWnd = UnsafeNativeMethods.GetWindowLong(new HandleRef(null, hWnd), -8);
					if (hWnd == IntPtr.Zero)
					{
						return false;
					}
					if (hWnd == base.Handle)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06007178 RID: 29048 RVA: 0x0019EE60 File Offset: 0x0019D060
			public bool OnClickHooked()
			{
				this.gridView.CloseDropDownInternal(false);
				return false;
			}

			// Token: 0x06007179 RID: 29049 RVA: 0x0019EE70 File Offset: 0x0019D070
			private void OnCurrentControlResize(object o, EventArgs e)
			{
				if (this.currentControl != null && !this.resizing)
				{
					int width = base.Width;
					Size size = new Size(2 + this.currentControl.Width, 2 + this.currentControl.Height);
					if (this.resizable)
					{
						size.Height += this.ResizeBarSize;
					}
					try
					{
						this.resizing = true;
						base.SuspendLayout();
						base.Size = size;
					}
					finally
					{
						this.resizing = false;
						base.ResumeLayout(false);
					}
					base.Left -= base.Width - width;
				}
			}

			// Token: 0x0600717A RID: 29050 RVA: 0x0019EF20 File Offset: 0x0019D120
			protected override void OnLayout(LayoutEventArgs levent)
			{
				try
				{
					this.resizing = true;
					base.OnLayout(levent);
				}
				finally
				{
					this.resizing = false;
				}
			}

			// Token: 0x0600717B RID: 29051 RVA: 0x0019EF58 File Offset: 0x0019D158
			private void OnNewLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				InstanceCreationEditor instanceCreationEditor = e.Link.LinkData as InstanceCreationEditor;
				if (instanceCreationEditor != null)
				{
					PropertyGridView propertyGridView = this.gridView;
					if (((propertyGridView != null) ? propertyGridView.SelectedGridEntry : null) != null)
					{
						Type propertyType = this.gridView.SelectedGridEntry.PropertyType;
						if (propertyType != null)
						{
							this.gridView.CloseDropDown();
							object obj = instanceCreationEditor.CreateInstance(this.gridView.SelectedGridEntry, propertyType);
							if (obj != null)
							{
								if (!propertyType.IsInstanceOfType(obj))
								{
									throw new InvalidCastException(SR.GetString("PropertyGridViewEditorCreatedInvalidObject", new object[] { propertyType }));
								}
								this.gridView.CommitValue(obj);
							}
						}
					}
				}
			}

			// Token: 0x0600717C RID: 29052 RVA: 0x0019EFF8 File Offset: 0x0019D1F8
			private int MoveTypeFromPoint(int x, int y)
			{
				Rectangle rectangle = new Rectangle(0, base.Height - this.ResizeGripSize, this.ResizeGripSize, this.ResizeGripSize);
				Rectangle rectangle2 = new Rectangle(0, 0, this.ResizeGripSize, this.ResizeGripSize);
				if (!this.resizeUp && rectangle.Contains(x, y))
				{
					return 3;
				}
				if (this.resizeUp && rectangle2.Contains(x, y))
				{
					return 6;
				}
				if (!this.resizeUp && Math.Abs(base.Height - y) < this.ResizeBorderSize)
				{
					return 1;
				}
				if (this.resizeUp && Math.Abs(y) < this.ResizeBorderSize)
				{
					return 4;
				}
				return 0;
			}

			// Token: 0x0600717D RID: 29053 RVA: 0x0019F0A0 File Offset: 0x0019D2A0
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					this.currentMoveType = this.MoveTypeFromPoint(e.X, e.Y);
					if (this.currentMoveType != 0)
					{
						this.dragStart = base.PointToScreen(new Point(e.X, e.Y));
						this.dragBaseRect = base.Bounds;
						base.Capture = true;
					}
					else
					{
						this.gridView.CloseDropDown();
					}
				}
				base.OnMouseDown(e);
			}

			// Token: 0x0600717E RID: 29054 RVA: 0x0019F120 File Offset: 0x0019D320
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (this.currentMoveType == 0)
				{
					switch (this.MoveTypeFromPoint(e.X, e.Y))
					{
					case 1:
					case 4:
						this.Cursor = Cursors.SizeNS;
						goto IL_1CB;
					case 3:
						this.Cursor = Cursors.SizeNESW;
						goto IL_1CB;
					case 6:
						this.Cursor = Cursors.SizeNWSE;
						goto IL_1CB;
					}
					this.Cursor = null;
				}
				else
				{
					Point point = base.PointToScreen(new Point(e.X, e.Y));
					Rectangle bounds = base.Bounds;
					if ((this.currentMoveType & 1) == 1)
					{
						bounds.Height = Math.Max(this.MinDropDownSize.Height, this.dragBaseRect.Height + (point.Y - this.dragStart.Y));
					}
					if ((this.currentMoveType & 4) == 4)
					{
						int num = point.Y - this.dragStart.Y;
						if (this.dragBaseRect.Height - num > this.MinDropDownSize.Height)
						{
							bounds.Y = this.dragBaseRect.Top + num;
							bounds.Height = this.dragBaseRect.Height - num;
						}
					}
					if ((this.currentMoveType & 2) == 2)
					{
						int num2 = point.X - this.dragStart.X;
						if (this.dragBaseRect.Width - num2 > this.MinDropDownSize.Width)
						{
							bounds.X = this.dragBaseRect.Left + num2;
							bounds.Width = this.dragBaseRect.Width - num2;
						}
					}
					if (bounds != base.Bounds)
					{
						try
						{
							this.resizing = true;
							base.Bounds = bounds;
						}
						finally
						{
							this.resizing = false;
						}
					}
					base.Invalidate();
				}
				IL_1CB:
				base.OnMouseMove(e);
			}

			// Token: 0x0600717F RID: 29055 RVA: 0x0019F310 File Offset: 0x0019D510
			protected override void OnMouseLeave(EventArgs e)
			{
				this.Cursor = null;
				base.OnMouseLeave(e);
			}

			// Token: 0x06007180 RID: 29056 RVA: 0x0019F320 File Offset: 0x0019D520
			protected override void OnMouseUp(MouseEventArgs e)
			{
				base.OnMouseUp(e);
				if (e.Button == MouseButtons.Left)
				{
					this.currentMoveType = 0;
					this.dragStart = Point.Empty;
					this.dragBaseRect = Rectangle.Empty;
					base.Capture = false;
				}
			}

			// Token: 0x06007181 RID: 29057 RVA: 0x0019F35C File Offset: 0x0019D55C
			protected override void OnPaint(PaintEventArgs pe)
			{
				base.OnPaint(pe);
				if (this.resizable)
				{
					Rectangle rectangle = new Rectangle(0, this.resizeUp ? 0 : (base.Height - this.ResizeGripSize), this.ResizeGripSize, this.ResizeGripSize);
					pe.Graphics.DrawImage(this.GetSizeGripGlyph(pe.Graphics), rectangle);
					int num = (this.resizeUp ? (this.ResizeBarSize - 1) : (base.Height - this.ResizeBarSize));
					Pen pen = new Pen(SystemColors.ControlDark, 1f);
					pen.DashStyle = DashStyle.Solid;
					pe.Graphics.DrawLine(pen, 0, num, base.Width, num);
					pen.Dispose();
				}
			}

			// Token: 0x06007182 RID: 29058 RVA: 0x0019F414 File Offset: 0x0019D614
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if ((keyData & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.None)
				{
					Keys keys = keyData & Keys.KeyCode;
					if (keys == Keys.Return)
					{
						if (this.gridView.UnfocusSelection() && this.gridView.SelectedGridEntry != null)
						{
							this.gridView.SelectedGridEntry.OnValueReturnKey();
						}
						return true;
					}
					if (keys == Keys.Escape)
					{
						this.gridView.OnEscape(this);
						return true;
					}
					if (keys == Keys.F4)
					{
						this.gridView.F4Selection(true);
						return true;
					}
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x06007183 RID: 29059 RVA: 0x0019F494 File Offset: 0x0019D694
			public void SetComponent(Control ctl, bool resizable)
			{
				this.resizable = resizable;
				this.Font = this.gridView.Font;
				InstanceCreationEditor instanceCreationEditor = ((ctl == null) ? null : this.GetInstanceCreationEditor(this.gridView.SelectedGridEntry as PropertyDescriptorGridEntry));
				if (this.currentControl != null)
				{
					this.currentControl.Resize -= this.OnCurrentControlResize;
					base.Controls.Remove(this.currentControl);
					this.currentControl = null;
				}
				if (this.createNewLink != null && this.createNewLink.Parent == this)
				{
					base.Controls.Remove(this.createNewLink);
				}
				if (ctl != null)
				{
					this.currentControl = ctl;
					base.DockPadding.All = 0;
					if (this.currentControl is PropertyGridView.GridViewListBox)
					{
						ListBox listBox = (ListBox)this.currentControl;
						if (listBox.Items.Count == 0)
						{
							listBox.Height = Math.Max(listBox.Height, listBox.ItemHeight);
						}
					}
					try
					{
						base.SuspendLayout();
						base.Controls.Add(ctl);
						Size size = new Size(2 + ctl.Width, 2 + ctl.Height);
						if (instanceCreationEditor != null)
						{
							this.CreateNewLink.Text = instanceCreationEditor.Text;
							this.CreateNewLink.Links.Clear();
							this.CreateNewLink.Links.Add(0, instanceCreationEditor.Text.Length, instanceCreationEditor);
							int num = this.CreateNewLink.Height;
							using (Graphics graphics = this.gridView.CreateGraphics())
							{
								num = (int)PropertyGrid.MeasureTextHelper.MeasureText(this.gridView.ownerGrid, graphics, instanceCreationEditor.Text, this.gridView.GetBaseFont()).Height;
							}
							this.CreateNewLink.Height = num + 1;
							size.Height += num + 2;
						}
						if (resizable)
						{
							size.Height += this.ResizeBarSize;
							if (this.resizeUp)
							{
								base.DockPadding.Top = this.ResizeBarSize;
							}
							else
							{
								base.DockPadding.Bottom = this.ResizeBarSize;
							}
						}
						base.Size = size;
						if (DpiHelper.EnableDpiChangedHighDpiImprovements)
						{
							ctl.Visible = true;
							if (base.Size.Height < base.PreferredSize.Height)
							{
								base.Size = new Size(base.Size.Width, base.PreferredSize.Height);
							}
							ctl.Dock = DockStyle.Fill;
						}
						else
						{
							ctl.Dock = DockStyle.Fill;
							ctl.Visible = true;
						}
						if (instanceCreationEditor != null)
						{
							this.CreateNewLink.Dock = DockStyle.Bottom;
							base.Controls.Add(this.CreateNewLink);
						}
					}
					finally
					{
						base.ResumeLayout(true);
					}
					this.currentControl.Resize += this.OnCurrentControlResize;
				}
				base.Enabled = this.currentControl != null;
			}

			// Token: 0x06007184 RID: 29060 RVA: 0x0019F7AC File Offset: 0x0019D9AC
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 6)
				{
					base.SetState(32, true);
					IntPtr lparam = m.LParam;
					if (base.Visible && NativeMethods.Util.LOWORD(m.WParam) == 0 && !this.OwnsWindow(lparam))
					{
						this.gridView.CloseDropDownInternal(false);
						return;
					}
				}
				else
				{
					if (m.Msg == 16)
					{
						if (base.Visible)
						{
							this.gridView.CloseDropDown();
						}
						return;
					}
					if (m.Msg == 736 && DpiHelper.EnableDpiChangedHighDpiImprovements)
					{
						int deviceDpi = this.deviceDpi;
						this.deviceDpi = (int)UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, base.HandleInternal));
						if (deviceDpi != this.deviceDpi)
						{
							this.RescaleConstantsForDpi(deviceDpi, this.deviceDpi);
							base.PerformLayout();
						}
						m.Result = IntPtr.Zero;
						return;
					}
				}
				base.WndProc(ref m);
			}

			// Token: 0x06007185 RID: 29061 RVA: 0x0019F888 File Offset: 0x0019DA88
			protected override void RescaleConstantsForDpi(int oldDpi, int newDpi)
			{
				base.RescaleConstantsForDpi(oldDpi, newDpi);
				if (!DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					return;
				}
				int horizontalScrollBarHeightForDpi = SystemInformation.GetHorizontalScrollBarHeightForDpi(newDpi);
				this.MinDropDownSize = new Size(SystemInformation.GetVerticalScrollBarWidthForDpi(newDpi) * 4, horizontalScrollBarHeightForDpi * 4);
				this.ResizeGripSize = horizontalScrollBarHeightForDpi;
				this.ResizeBarSize = this.ResizeGripSize + 1;
				this.ResizeBorderSize = this.ResizeBarSize / 2;
				double num = (double)newDpi / (double)oldDpi;
				base.Height = (int)Math.Round(num * (double)base.Height);
			}

			// Token: 0x0400447C RID: 17532
			private Control currentControl;

			// Token: 0x0400447D RID: 17533
			private PropertyGridView gridView;

			// Token: 0x0400447E RID: 17534
			private PropertyGridView.MouseHook mouseHook;

			// Token: 0x0400447F RID: 17535
			private LinkLabel createNewLink;

			// Token: 0x04004480 RID: 17536
			private bool resizable = true;

			// Token: 0x04004481 RID: 17537
			private bool resizing;

			// Token: 0x04004482 RID: 17538
			private bool resizeUp;

			// Token: 0x04004483 RID: 17539
			private Point dragStart = Point.Empty;

			// Token: 0x04004484 RID: 17540
			private Rectangle dragBaseRect = Rectangle.Empty;

			// Token: 0x04004485 RID: 17541
			private int currentMoveType;

			// Token: 0x04004486 RID: 17542
			private int ResizeBarSize;

			// Token: 0x04004487 RID: 17543
			private int ResizeBorderSize;

			// Token: 0x04004488 RID: 17544
			private int ResizeGripSize;

			// Token: 0x04004489 RID: 17545
			private Size MinDropDownSize;

			// Token: 0x0400448A RID: 17546
			private Bitmap sizeGripGlyph;

			// Token: 0x0400448B RID: 17547
			private const int DropDownHolderBorder = 1;

			// Token: 0x0400448C RID: 17548
			private const int MoveTypeNone = 0;

			// Token: 0x0400448D RID: 17549
			private const int MoveTypeBottom = 1;

			// Token: 0x0400448E RID: 17550
			private const int MoveTypeLeft = 2;

			// Token: 0x0400448F RID: 17551
			private const int MoveTypeTop = 4;
		}

		// Token: 0x02000880 RID: 2176
		internal class GridViewListBox : ListBox
		{
			// Token: 0x06007186 RID: 29062 RVA: 0x0019F900 File Offset: 0x0019DB00
			public GridViewListBox(PropertyGridView gridView)
			{
				base.IntegralHeight = false;
				this._owningPropertyGridView = gridView;
				base.BackColor = gridView.BackColor;
			}

			// Token: 0x170018E5 RID: 6373
			// (get) Token: 0x06007187 RID: 29063 RVA: 0x0019F924 File Offset: 0x0019DB24
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style &= -8388609;
					createParams.ExStyle &= -513;
					return createParams;
				}
			}

			// Token: 0x170018E6 RID: 6374
			// (get) Token: 0x06007188 RID: 29064 RVA: 0x0019F95D File Offset: 0x0019DB5D
			internal PropertyGridView OwningPropertyGridView
			{
				get
				{
					return this._owningPropertyGridView;
				}
			}

			// Token: 0x170018E7 RID: 6375
			// (get) Token: 0x06007189 RID: 29065 RVA: 0x000A83A1 File Offset: 0x000A65A1
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x0600718A RID: 29066 RVA: 0x0019F965 File Offset: 0x0019DB65
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level3)
				{
					return new PropertyGridView.GridViewListBoxAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x0600718B RID: 29067 RVA: 0x0019F97B File Offset: 0x0019DB7B
			public virtual bool InSetSelectedIndex()
			{
				return this.fInSetSelectedIndex;
			}

			// Token: 0x0600718C RID: 29068 RVA: 0x0019F984 File Offset: 0x0019DB84
			protected override void OnSelectedIndexChanged(EventArgs e)
			{
				this.fInSetSelectedIndex = true;
				base.OnSelectedIndexChanged(e);
				this.fInSetSelectedIndex = false;
				PropertyGridView.GridViewListBoxAccessibleObject gridViewListBoxAccessibleObject = base.AccessibilityObject as PropertyGridView.GridViewListBoxAccessibleObject;
				if (gridViewListBoxAccessibleObject != null)
				{
					gridViewListBoxAccessibleObject.SetListBoxItemFocus();
				}
			}

			// Token: 0x04004490 RID: 17552
			internal bool fInSetSelectedIndex;

			// Token: 0x04004491 RID: 17553
			private PropertyGridView _owningPropertyGridView;
		}

		// Token: 0x02000881 RID: 2177
		[ComVisible(true)]
		private class GridViewListBoxItemAccessibleObject : AccessibleObject
		{
			// Token: 0x0600718D RID: 29069 RVA: 0x0019F9BB File Offset: 0x0019DBBB
			public GridViewListBoxItemAccessibleObject(PropertyGridView.GridViewListBox owningGridViewListBox, object owningItem)
			{
				this._owningGridViewListBox = owningGridViewListBox;
				this._owningItem = owningItem;
				base.UseStdAccessibleObjects(this._owningGridViewListBox.Handle);
			}

			// Token: 0x170018E8 RID: 6376
			// (get) Token: 0x0600718E RID: 29070 RVA: 0x0019F9E4 File Offset: 0x0019DBE4
			public override Rectangle Bounds
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					int num;
					int num2;
					int num3;
					int num4;
					systemIAccessibleInternal.accLocation(out num, out num2, out num3, out num4, this.GetChildId());
					return new Rectangle(num, num2, num3, num4);
				}
			}

			// Token: 0x170018E9 RID: 6377
			// (get) Token: 0x0600718F RID: 29071 RVA: 0x0019FA1C File Offset: 0x0019DC1C
			public override string DefaultAction
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accDefaultAction(this.GetChildId());
				}
			}

			// Token: 0x06007190 RID: 29072 RVA: 0x0019FA44 File Offset: 0x0019DC44
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this._owningGridViewListBox.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					int num = this.GetCurrentIndex();
					PropertyGridView.GridViewListBoxAccessibleObject gridViewListBoxAccessibleObject = this._owningGridViewListBox.AccessibilityObject as PropertyGridView.GridViewListBoxAccessibleObject;
					if (gridViewListBoxAccessibleObject != null)
					{
						int childFragmentCount = gridViewListBoxAccessibleObject.GetChildFragmentCount();
						int num2 = num + 1;
						if (childFragmentCount > num2)
						{
							return gridViewListBoxAccessibleObject.GetChildFragment(num2);
						}
					}
					break;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					int num = this.GetCurrentIndex();
					PropertyGridView.GridViewListBoxAccessibleObject gridViewListBoxAccessibleObject = this._owningGridViewListBox.AccessibilityObject as PropertyGridView.GridViewListBoxAccessibleObject;
					if (gridViewListBoxAccessibleObject != null)
					{
						int childFragmentCount2 = gridViewListBoxAccessibleObject.GetChildFragmentCount();
						int num3 = num - 1;
						if (num3 >= 0)
						{
							return gridViewListBoxAccessibleObject.GetChildFragment(num3);
						}
					}
					break;
				}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170018EA RID: 6378
			// (get) Token: 0x06007191 RID: 29073 RVA: 0x0019FAE0 File Offset: 0x0019DCE0
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this._owningGridViewListBox.AccessibilityObject;
				}
			}

			// Token: 0x06007192 RID: 29074 RVA: 0x0019FAED File Offset: 0x0019DCED
			private int GetCurrentIndex()
			{
				return this._owningGridViewListBox.Items.IndexOf(this._owningItem);
			}

			// Token: 0x06007193 RID: 29075 RVA: 0x0019FB05 File Offset: 0x0019DD05
			internal override int GetChildId()
			{
				return this.GetCurrentIndex() + 1;
			}

			// Token: 0x06007194 RID: 29076 RVA: 0x0019FB10 File Offset: 0x0019DD10
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
					return 50007;
				case 30005:
					return this.Name;
				case 30007:
					return this.KeyboardShortcut;
				case 30008:
					return this._owningGridViewListBox.Focused;
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
					return this._owningGridViewListBox.Enabled;
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

			// Token: 0x170018EB RID: 6379
			// (get) Token: 0x06007195 RID: 29077 RVA: 0x0019FC1C File Offset: 0x0019DE1C
			public override string Help
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accHelp(this.GetChildId());
				}
			}

			// Token: 0x170018EC RID: 6380
			// (get) Token: 0x06007196 RID: 29078 RVA: 0x0019FC44 File Offset: 0x0019DE44
			public override string KeyboardShortcut
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accKeyboardShortcut(this.GetChildId());
				}
			}

			// Token: 0x06007197 RID: 29079 RVA: 0x00170A4C File Offset: 0x0016EC4C
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || patternId == 10000 || base.IsPatternSupported(patternId);
			}

			// Token: 0x170018ED RID: 6381
			// (get) Token: 0x06007198 RID: 29080 RVA: 0x0019FC69 File Offset: 0x0019DE69
			// (set) Token: 0x06007199 RID: 29081 RVA: 0x0016FA70 File Offset: 0x0016DC70
			public override string Name
			{
				get
				{
					if (this._owningGridViewListBox != null)
					{
						return this._owningItem.ToString();
					}
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x170018EE RID: 6382
			// (get) Token: 0x0600719A RID: 29082 RVA: 0x0019FC88 File Offset: 0x0019DE88
			public override AccessibleRole Role
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleRole)systemIAccessibleInternal.get_accRole(this.GetChildId());
				}
			}

			// Token: 0x170018EF RID: 6383
			// (get) Token: 0x0600719B RID: 29083 RVA: 0x0019FCB4 File Offset: 0x0019DEB4
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						(int)(long)this._owningGridViewListBox.Handle,
						this._owningItem.GetHashCode()
					};
				}
			}

			// Token: 0x170018F0 RID: 6384
			// (get) Token: 0x0600719C RID: 29084 RVA: 0x0019FCF0 File Offset: 0x0019DEF0
			public override AccessibleStates State
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleStates)systemIAccessibleInternal.get_accState(this.GetChildId());
				}
			}

			// Token: 0x0600719D RID: 29085 RVA: 0x0015EE49 File Offset: 0x0015D049
			internal override void SetFocus()
			{
				base.RaiseAutomationEvent(20005);
				base.SetFocus();
			}

			// Token: 0x04004492 RID: 17554
			private PropertyGridView.GridViewListBox _owningGridViewListBox;

			// Token: 0x04004493 RID: 17555
			private object _owningItem;
		}

		// Token: 0x02000882 RID: 2178
		private class GridViewListBoxItemAccessibleObjectCollection : Hashtable
		{
			// Token: 0x0600719E RID: 29086 RVA: 0x0019FD1A File Offset: 0x0019DF1A
			public GridViewListBoxItemAccessibleObjectCollection(PropertyGridView.GridViewListBox owningGridViewListBox)
			{
				this._owningGridViewListBox = owningGridViewListBox;
			}

			// Token: 0x170018F1 RID: 6385
			public override object this[object key]
			{
				get
				{
					if (!this.ContainsKey(key))
					{
						PropertyGridView.GridViewListBoxItemAccessibleObject gridViewListBoxItemAccessibleObject = new PropertyGridView.GridViewListBoxItemAccessibleObject(this._owningGridViewListBox, key);
						base[key] = gridViewListBoxItemAccessibleObject;
					}
					return base[key];
				}
				set
				{
					base[key] = value;
				}
			}

			// Token: 0x04004494 RID: 17556
			private PropertyGridView.GridViewListBox _owningGridViewListBox;
		}

		// Token: 0x02000883 RID: 2179
		[ComVisible(true)]
		private class GridViewListBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060071A1 RID: 29089 RVA: 0x0019FD68 File Offset: 0x0019DF68
			public GridViewListBoxAccessibleObject(PropertyGridView.GridViewListBox owningGridViewListBox)
				: base(owningGridViewListBox)
			{
				this._owningGridViewListBox = owningGridViewListBox;
				this._owningPropertyGridView = owningGridViewListBox.OwningPropertyGridView;
				this._itemAccessibleObjects = new PropertyGridView.GridViewListBoxItemAccessibleObjectCollection(owningGridViewListBox);
			}

			// Token: 0x060071A2 RID: 29090 RVA: 0x0019FD90 File Offset: 0x0019DF90
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (AccessibilityImprovements.Level5)
				{
					if (!this._owningPropertyGridView.DropDownVisible || this._owningPropertyGridView.DropDownControlHolder.Component != this._owningGridViewListBox)
					{
						return null;
					}
					GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
					PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject = ((selectedGridEntry != null) ? selectedGridEntry.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
					if (propertyDescriptorGridEntryAccessibleObject == null)
					{
						return null;
					}
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.Parent:
						return propertyDescriptorGridEntryAccessibleObject;
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetNextChildFragment(this);
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetPreviousChildFragment(this);
					}
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent && this._owningPropertyGridView.SelectedGridEntry != null)
				{
					return this._owningPropertyGridView.SelectedGridEntry.AccessibilityObject;
				}
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
				else if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					return this._owningPropertyGridView.Edit.AccessibilityObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170018F2 RID: 6386
			// (get) Token: 0x060071A3 RID: 29091 RVA: 0x0019FE75 File Offset: 0x0019E075
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this._owningPropertyGridView.AccessibilityObject;
				}
			}

			// Token: 0x060071A4 RID: 29092 RVA: 0x0019FE84 File Offset: 0x0019E084
			public AccessibleObject GetChildFragment(int index)
			{
				if (index < 0 || index >= this._owningGridViewListBox.Items.Count)
				{
					return null;
				}
				object obj = this._owningGridViewListBox.Items[index];
				return this._itemAccessibleObjects[obj] as AccessibleObject;
			}

			// Token: 0x060071A5 RID: 29093 RVA: 0x0019FECD File Offset: 0x0019E0CD
			public int GetChildFragmentCount()
			{
				return this._owningGridViewListBox.Items.Count;
			}

			// Token: 0x060071A6 RID: 29094 RVA: 0x0019FEDF File Offset: 0x0019E0DF
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50008;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x060071A7 RID: 29095 RVA: 0x0015EE49 File Offset: 0x0015D049
			internal override void SetFocus()
			{
				base.RaiseAutomationEvent(20005);
				base.SetFocus();
			}

			// Token: 0x060071A8 RID: 29096 RVA: 0x0019FF0C File Offset: 0x0019E10C
			internal void SetListBoxItemFocus()
			{
				object selectedItem = this._owningGridViewListBox.SelectedItem;
				if (selectedItem != null)
				{
					AccessibleObject accessibleObject = this._itemAccessibleObjects[selectedItem] as AccessibleObject;
					if (accessibleObject != null)
					{
						accessibleObject.SetFocus();
					}
				}
			}

			// Token: 0x04004495 RID: 17557
			private PropertyGridView.GridViewListBox _owningGridViewListBox;

			// Token: 0x04004496 RID: 17558
			private PropertyGridView _owningPropertyGridView;

			// Token: 0x04004497 RID: 17559
			private PropertyGridView.GridViewListBoxItemAccessibleObjectCollection _itemAccessibleObjects;
		}

		// Token: 0x02000884 RID: 2180
		internal interface IMouseHookClient
		{
			// Token: 0x060071A9 RID: 29097
			bool OnClickHooked();
		}

		// Token: 0x02000885 RID: 2181
		internal class MouseHook
		{
			// Token: 0x060071AA RID: 29098 RVA: 0x0019FF43 File Offset: 0x0019E143
			public MouseHook(Control control, PropertyGridView.IMouseHookClient client, PropertyGridView gridView)
			{
				this.control = control;
				this.gridView = gridView;
				this.client = client;
			}

			// Token: 0x170018F3 RID: 6387
			// (set) Token: 0x060071AB RID: 29099 RVA: 0x0019FF6B File Offset: 0x0019E16B
			public bool DisableMouseHook
			{
				set
				{
					this.hookDisable = value;
					if (value)
					{
						this.UnhookMouse();
					}
				}
			}

			// Token: 0x170018F4 RID: 6388
			// (get) Token: 0x060071AC RID: 29100 RVA: 0x0019FF7D File Offset: 0x0019E17D
			// (set) Token: 0x060071AD RID: 29101 RVA: 0x0019FF95 File Offset: 0x0019E195
			public virtual bool HookMouseDown
			{
				get
				{
					GC.KeepAlive(this);
					return this.mouseHookHandle != IntPtr.Zero;
				}
				set
				{
					if (value && !this.hookDisable)
					{
						this.HookMouse();
						return;
					}
					this.UnhookMouse();
				}
			}

			// Token: 0x060071AE RID: 29102 RVA: 0x0019FFAF File Offset: 0x0019E1AF
			public void Dispose()
			{
				this.UnhookMouse();
			}

			// Token: 0x060071AF RID: 29103 RVA: 0x0019FFB8 File Offset: 0x0019E1B8
			private void HookMouse()
			{
				GC.KeepAlive(this);
				lock (this)
				{
					if (!(this.mouseHookHandle != IntPtr.Zero))
					{
						if (this.thisProcessID == 0)
						{
							SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this.control, this.control.Handle), out this.thisProcessID);
						}
						NativeMethods.HookProc hookProc = new NativeMethods.HookProc(new PropertyGridView.MouseHook.MouseHookObject(this).Callback);
						this.mouseHookRoot = GCHandle.Alloc(hookProc);
						this.mouseHookHandle = UnsafeNativeMethods.SetWindowsHookEx(7, hookProc, NativeMethods.NullHandleRef, SafeNativeMethods.GetCurrentThreadId());
					}
				}
			}

			// Token: 0x060071B0 RID: 29104 RVA: 0x001A0068 File Offset: 0x0019E268
			private IntPtr MouseHookProc(int nCode, IntPtr wparam, IntPtr lparam)
			{
				GC.KeepAlive(this);
				if (nCode == 0)
				{
					NativeMethods.MOUSEHOOKSTRUCT mousehookstruct = (NativeMethods.MOUSEHOOKSTRUCT)UnsafeNativeMethods.PtrToStructure(lparam, typeof(NativeMethods.MOUSEHOOKSTRUCT));
					if (mousehookstruct != null)
					{
						int num = (int)(long)wparam;
						if (num <= 164)
						{
							if (num != 33 && num != 161 && num != 164)
							{
								goto IL_97;
							}
						}
						else if (num <= 513)
						{
							if (num != 167 && num != 513)
							{
								goto IL_97;
							}
						}
						else if (num != 516 && num != 519)
						{
							goto IL_97;
						}
						if (this.ProcessMouseDown(mousehookstruct.hWnd, mousehookstruct.pt_x, mousehookstruct.pt_y))
						{
							return (IntPtr)1;
						}
					}
				}
				IL_97:
				return UnsafeNativeMethods.CallNextHookEx(new HandleRef(this, this.mouseHookHandle), nCode, wparam, lparam);
			}

			// Token: 0x060071B1 RID: 29105 RVA: 0x001A0120 File Offset: 0x0019E320
			private void UnhookMouse()
			{
				GC.KeepAlive(this);
				lock (this)
				{
					if (this.mouseHookHandle != IntPtr.Zero)
					{
						UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(this, this.mouseHookHandle));
						this.mouseHookRoot.Free();
						this.mouseHookHandle = IntPtr.Zero;
					}
				}
			}

			// Token: 0x060071B2 RID: 29106 RVA: 0x001A0198 File Offset: 0x0019E398
			private bool ProcessMouseDown(IntPtr hWnd, int x, int y)
			{
				if (this.processing)
				{
					return false;
				}
				IntPtr handle = this.control.Handle;
				Control control = Control.FromHandleInternal(hWnd);
				if (hWnd != handle && !this.control.Contains(control))
				{
					int num;
					SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, hWnd), out num);
					if (num != this.thisProcessID)
					{
						this.HookMouseDown = false;
						return false;
					}
					bool flag = control == null || !this.gridView.IsSiblingControl(this.control, control);
					try
					{
						this.processing = true;
						if (flag && this.client.OnClickHooked())
						{
							return true;
						}
					}
					finally
					{
						this.processing = false;
					}
					this.HookMouseDown = false;
					return false;
				}
				return false;
			}

			// Token: 0x04004498 RID: 17560
			private PropertyGridView gridView;

			// Token: 0x04004499 RID: 17561
			private Control control;

			// Token: 0x0400449A RID: 17562
			private PropertyGridView.IMouseHookClient client;

			// Token: 0x0400449B RID: 17563
			internal int thisProcessID;

			// Token: 0x0400449C RID: 17564
			private GCHandle mouseHookRoot;

			// Token: 0x0400449D RID: 17565
			private IntPtr mouseHookHandle = IntPtr.Zero;

			// Token: 0x0400449E RID: 17566
			private bool hookDisable;

			// Token: 0x0400449F RID: 17567
			private bool processing;

			// Token: 0x0200097E RID: 2430
			private class MouseHookObject
			{
				// Token: 0x0600755B RID: 30043 RVA: 0x001A7E30 File Offset: 0x001A6030
				public MouseHookObject(PropertyGridView.MouseHook parent)
				{
					this.reference = new WeakReference(parent, false);
				}

				// Token: 0x0600755C RID: 30044 RVA: 0x001A7E48 File Offset: 0x001A6048
				public virtual IntPtr Callback(int nCode, IntPtr wparam, IntPtr lparam)
				{
					IntPtr intPtr = IntPtr.Zero;
					try
					{
						PropertyGridView.MouseHook mouseHook = (PropertyGridView.MouseHook)this.reference.Target;
						if (mouseHook != null)
						{
							intPtr = mouseHook.MouseHookProc(nCode, wparam, lparam);
						}
					}
					catch
					{
					}
					return intPtr;
				}

				// Token: 0x040047CF RID: 18383
				internal WeakReference reference;
			}
		}

		// Token: 0x02000886 RID: 2182
		[ComVisible(true)]
		internal class PropertyGridViewAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060071B3 RID: 29107 RVA: 0x001A0260 File Offset: 0x0019E460
			public PropertyGridViewAccessibleObject(PropertyGridView owner, PropertyGrid parentPropertyGrid)
				: base(owner)
			{
				this._owningPropertyGridView = owner;
				this._parentPropertyGrid = parentPropertyGrid;
			}

			// Token: 0x060071B4 RID: 29108 RVA: 0x001A0277 File Offset: 0x0019E477
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				if (AccessibilityImprovements.Level3)
				{
					return this.HitTest((int)x, (int)y);
				}
				return base.ElementProviderFromPoint(x, y);
			}

			// Token: 0x060071B5 RID: 29109 RVA: 0x001A0294 File Offset: 0x0019E494
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (AccessibilityImprovements.Level3)
				{
					PropertyGridAccessibleObject propertyGridAccessibleObject = this._parentPropertyGrid.AccessibilityObject as PropertyGridAccessibleObject;
					if (propertyGridAccessibleObject != null)
					{
						UnsafeNativeMethods.IRawElementProviderFragment rawElementProviderFragment = propertyGridAccessibleObject.ChildFragmentNavigate(this, direction);
						if (rawElementProviderFragment != null)
						{
							return rawElementProviderFragment;
						}
					}
					if (this._owningPropertyGridView.OwnerGrid.SortedByCategories)
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
						{
							return this.GetFirstCategory();
						}
						if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
						{
							return this.GetLastCategory();
						}
					}
					else
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
						{
							return this.GetChild(0);
						}
						if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
						{
							int childCount = this.GetChildCount();
							if (childCount > 0)
							{
								return this.GetChild(childCount - 1);
							}
							return null;
						}
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170018F5 RID: 6389
			// (get) Token: 0x060071B6 RID: 29110 RVA: 0x001A0324 File Offset: 0x0019E524
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (AccessibilityImprovements.Level3)
					{
						return this._owningPropertyGridView.OwnerGrid.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x060071B7 RID: 29111 RVA: 0x001A0344 File Offset: 0x0019E544
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				if (AccessibilityImprovements.Level3)
				{
					return this.GetFocused();
				}
				return base.FragmentRoot;
			}

			// Token: 0x060071B8 RID: 29112 RVA: 0x001A035C File Offset: 0x0019E55C
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30003)
					{
						return 50036;
					}
					if (propertyID == 30005)
					{
						return this.Name;
					}
				}
				if (AccessibilityImprovements.Level4 && (propertyID == 30030 || propertyID == 30038))
				{
					return true;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x060071B9 RID: 29113 RVA: 0x001A03B9 File Offset: 0x0019E5B9
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level4 && (patternId == 10006 || patternId == 10012)) || base.IsPatternSupported(patternId);
			}

			// Token: 0x170018F6 RID: 6390
			// (get) Token: 0x060071BA RID: 29114 RVA: 0x001A03DC File Offset: 0x0019E5DC
			public override string Name
			{
				get
				{
					string accessibleName = base.Owner.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					return SR.GetString("PropertyGridDefaultAccessibleName");
				}
			}

			// Token: 0x170018F7 RID: 6391
			// (get) Token: 0x060071BB RID: 29115 RVA: 0x001A0404 File Offset: 0x0019E604
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Table;
				}
			}

			// Token: 0x060071BC RID: 29116 RVA: 0x001A0428 File Offset: 0x0019E628
			public AccessibleObject Next(GridEntry current)
			{
				int rowFromGridEntry = ((PropertyGridView)base.Owner).GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow = ((PropertyGridView)base.Owner).GetGridEntryFromRow(rowFromGridEntry + 1);
				if (gridEntryFromRow != null)
				{
					return gridEntryFromRow.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060071BD RID: 29117 RVA: 0x001A0468 File Offset: 0x0019E668
			internal AccessibleObject GetCategory(int categoryIndex)
			{
				GridEntry[] array = new GridEntry[1];
				GridEntryCollection topLevelGridEntries = this._owningPropertyGridView.TopLevelGridEntries;
				int count = topLevelGridEntries.Count;
				if (count > 0)
				{
					GridItem gridItem = topLevelGridEntries[categoryIndex];
					CategoryGridEntry categoryGridEntry = gridItem as CategoryGridEntry;
					if (categoryGridEntry != null)
					{
						return categoryGridEntry.AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x060071BE RID: 29118 RVA: 0x001A04B0 File Offset: 0x0019E6B0
			internal AccessibleObject GetFirstCategory()
			{
				return this.GetCategory(0);
			}

			// Token: 0x060071BF RID: 29119 RVA: 0x001A04BC File Offset: 0x0019E6BC
			internal AccessibleObject GetLastCategory()
			{
				GridEntryCollection topLevelGridEntries = this._owningPropertyGridView.TopLevelGridEntries;
				int count = topLevelGridEntries.Count;
				return this.GetCategory(topLevelGridEntries.Count - 1);
			}

			// Token: 0x060071C0 RID: 29120 RVA: 0x001A04EC File Offset: 0x0019E6EC
			internal AccessibleObject GetPreviousGridEntry(GridEntry currentGridEntry, GridEntryCollection gridEntryCollection, out bool currentGridEntryFound)
			{
				GridEntry gridEntry = null;
				currentGridEntryFound = false;
				foreach (object obj in gridEntryCollection)
				{
					GridEntry gridEntry2 = (GridEntry)obj;
					if (currentGridEntry == gridEntry2)
					{
						currentGridEntryFound = true;
						if (gridEntry != null)
						{
							return gridEntry.AccessibilityObject;
						}
						return null;
					}
					else
					{
						gridEntry = gridEntry2;
						if (gridEntry2.ChildCount > 0)
						{
							AccessibleObject previousGridEntry = this.GetPreviousGridEntry(currentGridEntry, gridEntry2.Children, out currentGridEntryFound);
							if (previousGridEntry != null)
							{
								return previousGridEntry;
							}
							if (currentGridEntryFound)
							{
								return null;
							}
						}
					}
				}
				return null;
			}

			// Token: 0x060071C1 RID: 29121 RVA: 0x001A0588 File Offset: 0x0019E788
			internal AccessibleObject GetNextGridEntry(GridEntry currentGridEntry, GridEntryCollection gridEntryCollection, out bool currentGridEntryFound)
			{
				currentGridEntryFound = false;
				foreach (object obj in gridEntryCollection)
				{
					GridEntry gridEntry = (GridEntry)obj;
					if (currentGridEntryFound)
					{
						return gridEntry.AccessibilityObject;
					}
					if (currentGridEntry == gridEntry)
					{
						currentGridEntryFound = true;
					}
					else if (gridEntry.ChildCount > 0)
					{
						AccessibleObject nextGridEntry = this.GetNextGridEntry(currentGridEntry, gridEntry.Children, out currentGridEntryFound);
						if (nextGridEntry != null)
						{
							return nextGridEntry;
						}
						if (currentGridEntryFound)
						{
							return null;
						}
					}
				}
				return null;
			}

			// Token: 0x060071C2 RID: 29122 RVA: 0x001A061C File Offset: 0x0019E81C
			internal AccessibleObject GetFirstChildProperty(CategoryGridEntry current)
			{
				if (current.ChildCount > 0)
				{
					GridEntryCollection children = current.Children;
					if (children != null && children.Count > 0)
					{
						GridEntry[] array = new GridEntry[1];
						try
						{
							this._owningPropertyGridView.GetGridEntriesFromOutline(children, 0, 0, array);
						}
						catch (Exception ex)
						{
						}
						return array[0].AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x060071C3 RID: 29123 RVA: 0x001A067C File Offset: 0x0019E87C
			internal AccessibleObject GetLastChildProperty(CategoryGridEntry current)
			{
				if (current.ChildCount > 0)
				{
					GridEntryCollection children = current.Children;
					if (children != null && children.Count > 0)
					{
						GridEntry[] array = new GridEntry[1];
						try
						{
							this._owningPropertyGridView.GetGridEntriesFromOutline(children, 0, children.Count - 1, array);
						}
						catch (Exception ex)
						{
						}
						return array[0].AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x060071C4 RID: 29124 RVA: 0x001A06E4 File Offset: 0x0019E8E4
			internal AccessibleObject GetNextCategory(CategoryGridEntry current)
			{
				int num = this._owningPropertyGridView.GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow;
				for (;;)
				{
					gridEntryFromRow = this._owningPropertyGridView.GetGridEntryFromRow(++num);
					if (gridEntryFromRow is CategoryGridEntry)
					{
						break;
					}
					if (gridEntryFromRow == null)
					{
						goto Block_2;
					}
				}
				return gridEntryFromRow.AccessibilityObject;
				Block_2:
				return null;
			}

			// Token: 0x060071C5 RID: 29125 RVA: 0x001A0724 File Offset: 0x0019E924
			public AccessibleObject Previous(GridEntry current)
			{
				int rowFromGridEntry = ((PropertyGridView)base.Owner).GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow = ((PropertyGridView)base.Owner).GetGridEntryFromRow(rowFromGridEntry - 1);
				if (gridEntryFromRow != null)
				{
					return gridEntryFromRow.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060071C6 RID: 29126 RVA: 0x001A0764 File Offset: 0x0019E964
			internal AccessibleObject GetPreviousCategory(CategoryGridEntry current)
			{
				int num = this._owningPropertyGridView.GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow;
				for (;;)
				{
					gridEntryFromRow = this._owningPropertyGridView.GetGridEntryFromRow(--num);
					if (gridEntryFromRow is CategoryGridEntry)
					{
						break;
					}
					if (gridEntryFromRow == null)
					{
						goto Block_2;
					}
				}
				return gridEntryFromRow.AccessibilityObject;
				Block_2:
				return null;
			}

			// Token: 0x060071C7 RID: 29127 RVA: 0x001A07A4 File Offset: 0x0019E9A4
			public override AccessibleObject GetChild(int index)
			{
				GridEntryCollection gridEntryCollection = ((PropertyGridView)base.Owner).AccessibilityGetGridEntries();
				if (gridEntryCollection != null && index >= 0 && index < gridEntryCollection.Count)
				{
					return gridEntryCollection.GetEntry(index).AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060071C8 RID: 29128 RVA: 0x001A07E0 File Offset: 0x0019E9E0
			public override int GetChildCount()
			{
				GridEntryCollection gridEntryCollection = ((PropertyGridView)base.Owner).AccessibilityGetGridEntries();
				if (gridEntryCollection != null)
				{
					return gridEntryCollection.Count;
				}
				return 0;
			}

			// Token: 0x060071C9 RID: 29129 RVA: 0x001A080C File Offset: 0x0019EA0C
			public override AccessibleObject GetFocused()
			{
				GridEntry selectedGridEntry = ((PropertyGridView)base.Owner).SelectedGridEntry;
				if (selectedGridEntry != null && selectedGridEntry.Focus)
				{
					return selectedGridEntry.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060071CA RID: 29130 RVA: 0x001A0840 File Offset: 0x0019EA40
			public override AccessibleObject GetSelected()
			{
				GridEntry selectedGridEntry = ((PropertyGridView)base.Owner).SelectedGridEntry;
				if (selectedGridEntry != null)
				{
					return selectedGridEntry.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060071CB RID: 29131 RVA: 0x001A086C File Offset: 0x0019EA6C
			public override AccessibleObject HitTest(int x, int y)
			{
				NativeMethods.POINT point = new NativeMethods.POINT(x, y);
				UnsafeNativeMethods.ScreenToClient(new HandleRef(base.Owner, base.Owner.Handle), point);
				Point point2 = ((PropertyGridView)base.Owner).FindPosition(point.x, point.y);
				if (point2 != PropertyGridView.InvalidPosition)
				{
					GridEntry gridEntryFromRow = ((PropertyGridView)base.Owner).GetGridEntryFromRow(point2.Y);
					if (gridEntryFromRow != null)
					{
						return gridEntryFromRow.AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x060071CC RID: 29132 RVA: 0x00176BA6 File Offset: 0x00174DA6
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				if (this.GetChildCount() > 0)
				{
					if (navdir == AccessibleNavigation.FirstChild)
					{
						return this.GetChild(0);
					}
					if (navdir == AccessibleNavigation.LastChild)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
				}
				return null;
			}

			// Token: 0x060071CD RID: 29133 RVA: 0x001A08EB File Offset: 0x0019EAEB
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetItem(int row, int column)
			{
				if (AccessibilityImprovements.Level4)
				{
					return this.GetChild(row);
				}
				return base.GetItem(row, column);
			}

			// Token: 0x170018F8 RID: 6392
			// (get) Token: 0x060071CE RID: 29134 RVA: 0x001A0904 File Offset: 0x0019EB04
			internal override int RowCount
			{
				get
				{
					if (!AccessibilityImprovements.Level4)
					{
						return base.RowCount;
					}
					GridEntryCollection topLevelGridEntries = this._owningPropertyGridView.TopLevelGridEntries;
					if (topLevelGridEntries == null || this._owningPropertyGridView.OwnerGrid == null)
					{
						return 0;
					}
					if (!this._owningPropertyGridView.OwnerGrid.SortedByCategories)
					{
						return topLevelGridEntries.Count;
					}
					int num = 0;
					foreach (object obj in topLevelGridEntries)
					{
						if (obj is CategoryGridEntry)
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x170018F9 RID: 6393
			// (get) Token: 0x060071CF RID: 29135 RVA: 0x001A09A4 File Offset: 0x0019EBA4
			internal override int ColumnCount
			{
				get
				{
					if (AccessibilityImprovements.Level4)
					{
						return 1;
					}
					return base.ColumnCount;
				}
			}

			// Token: 0x040044A0 RID: 17568
			private PropertyGridView _owningPropertyGridView;

			// Token: 0x040044A1 RID: 17569
			private PropertyGrid _parentPropertyGrid;
		}

		// Token: 0x02000887 RID: 2183
		internal class GridPositionData
		{
			// Token: 0x060071D0 RID: 29136 RVA: 0x001A09B8 File Offset: 0x0019EBB8
			public GridPositionData(PropertyGridView gridView)
			{
				this.selectedItemTree = gridView.GetGridEntryHierarchy(gridView.selectedGridEntry);
				this.expandedState = gridView.SaveHierarchyState(gridView.topLevelGridEntries);
				this.itemRow = gridView.selectedRow;
				this.itemCount = gridView.totalProps;
			}

			// Token: 0x060071D1 RID: 29137 RVA: 0x001A0A08 File Offset: 0x0019EC08
			public GridEntry Restore(PropertyGridView gridView)
			{
				gridView.RestoreHierarchyState(this.expandedState);
				GridEntry gridEntry = gridView.FindEquivalentGridEntry(this.selectedItemTree);
				if (gridEntry != null)
				{
					gridView.SelectGridEntry(gridEntry, true);
					int num = gridView.selectedRow - this.itemRow;
					if (num != 0 && gridView.ScrollBar.Visible && this.itemRow < gridView.visibleRows)
					{
						num += gridView.GetScrollOffset();
						if (num < 0)
						{
							num = 0;
						}
						else if (num > gridView.ScrollBar.Maximum)
						{
							num = gridView.ScrollBar.Maximum - 1;
						}
						gridView.SetScrollOffset(num);
					}
				}
				return gridEntry;
			}

			// Token: 0x040044A2 RID: 17570
			private ArrayList expandedState;

			// Token: 0x040044A3 RID: 17571
			private GridEntryCollection selectedItemTree;

			// Token: 0x040044A4 RID: 17572
			private int itemRow;

			// Token: 0x040044A5 RID: 17573
			private int itemCount;
		}
	}
}
