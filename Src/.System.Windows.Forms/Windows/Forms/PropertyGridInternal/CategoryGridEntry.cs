using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FA RID: 1274
	internal class CategoryGridEntry : GridEntry
	{
		// Token: 0x06005387 RID: 21383 RVA: 0x0015DEC0 File Offset: 0x0015C0C0
		public CategoryGridEntry(PropertyGrid ownerGrid, GridEntry peParent, string name, GridEntry[] childGridEntries)
			: base(ownerGrid, peParent)
		{
			this.name = name;
			if (CategoryGridEntry.categoryStates == null)
			{
				CategoryGridEntry.categoryStates = new Hashtable();
			}
			Hashtable hashtable = CategoryGridEntry.categoryStates;
			lock (hashtable)
			{
				if (!CategoryGridEntry.categoryStates.ContainsKey(name))
				{
					CategoryGridEntry.categoryStates.Add(name, true);
				}
			}
			this.IsExpandable = true;
			for (int i = 0; i < childGridEntries.Length; i++)
			{
				childGridEntries[i].ParentGridEntry = this;
			}
			base.ChildCollection = new GridEntryCollection(this, childGridEntries);
			Hashtable hashtable2 = CategoryGridEntry.categoryStates;
			lock (hashtable2)
			{
				this.InternalExpanded = (bool)CategoryGridEntry.categoryStates[name];
			}
			this.SetFlag(64, true);
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool HasValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x0015DFB0 File Offset: 0x0015C1B0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.backBrush != null)
				{
					this.backBrush.Dispose();
					this.backBrush = null;
				}
				if (base.ChildCollection != null)
				{
					base.ChildCollection = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x000070A6 File Offset: 0x000052A6
		public override void DisposeChildren()
		{
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x0015DFE5 File Offset: 0x0015C1E5
		public override int PropertyDepth
		{
			get
			{
				return base.PropertyDepth - 1;
			}
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0015DFEF File Offset: 0x0015C1EF
		protected override GridEntry.GridEntryAccessibleObject GetAccessibilityObject()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new CategoryGridEntry.CategoryGridEntryAccessibleObject(this);
			}
			return base.GetAccessibilityObject();
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0015E005 File Offset: 0x0015C205
		protected override Brush GetBackgroundBrush(Graphics g)
		{
			return this.GridEntryHost.GetLineBrush(g);
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x0600538E RID: 21390 RVA: 0x0015E013 File Offset: 0x0015C213
		protected override Color LabelTextColor
		{
			get
			{
				return this.ownerGrid.CategoryForeColor;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x0600538F RID: 21391 RVA: 0x0015E020 File Offset: 0x0015C220
		public override bool Expandable
		{
			get
			{
				return !this.GetFlagSet(524288);
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (set) Token: 0x06005390 RID: 21392 RVA: 0x0015E030 File Offset: 0x0015C230
		internal override bool InternalExpanded
		{
			set
			{
				base.InternalExpanded = value;
				Hashtable hashtable = CategoryGridEntry.categoryStates;
				lock (hashtable)
				{
					CategoryGridEntry.categoryStates[this.name] = value;
				}
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06005391 RID: 21393 RVA: 0x00012E4E File Offset: 0x0001104E
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Category;
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06005392 RID: 21394 RVA: 0x00015C90 File Offset: 0x00013E90
		public override string HelpKeyword
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06005393 RID: 21395 RVA: 0x0015E088 File Offset: 0x0015C288
		public override string PropertyLabel
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06005394 RID: 21396 RVA: 0x0015E090 File Offset: 0x0015C290
		internal override int PropertyLabelIndent
		{
			get
			{
				PropertyGridView gridEntryHost = this.GridEntryHost;
				return 1 + gridEntryHost.GetOutlineIconSize() + 5 + base.PropertyDepth * gridEntryHost.GetDefaultOutlineIndent();
			}
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x000F17EC File Offset: 0x000EF9EC
		public override string GetPropertyTextValue(object o)
		{
			return "";
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06005396 RID: 21398 RVA: 0x0015E0BC File Offset: 0x0015C2BC
		public override Type PropertyType
		{
			get
			{
				return typeof(void);
			}
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0015E0C8 File Offset: 0x0015C2C8
		public override object GetChildValueOwner(GridEntry childEntry)
		{
			return this.ParentGridEntry.GetChildValueOwner(childEntry);
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x00012E4E File Offset: 0x0001104E
		protected override bool CreateChildren(bool diffOldChildren)
		{
			return true;
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x0015E0D8 File Offset: 0x0015C2D8
		public override string GetTestingInfo()
		{
			string text = "object = (";
			text += base.FullLabel;
			return text + "), Category = (" + this.PropertyLabel + ")";
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x0015E110 File Offset: 0x0015C310
		public override void PaintLabel(Graphics g, Rectangle rect, Rectangle clipRect, bool selected, bool paintFullLabel)
		{
			base.PaintLabel(g, rect, clipRect, false, true);
			if (selected && this.hasFocus)
			{
				bool flag = (this.Flags & 64) != 0;
				Font font = base.GetFont(flag);
				int labelTextWidth = base.GetLabelTextWidth(this.PropertyLabel, g, font);
				int num = this.PropertyLabelIndent - 2;
				Rectangle rectangle = new Rectangle(num, rect.Y, labelTextWidth + 3, rect.Height - 1);
				if (SystemInformation.HighContrast && !base.OwnerGrid.developerOverride && AccessibilityImprovements.Level1)
				{
					ControlPaint.DrawFocusRectangle(g, rectangle, SystemColors.ControlText, base.OwnerGrid.LineColor);
				}
				else
				{
					ControlPaint.DrawFocusRectangle(g, rectangle);
				}
			}
			if (this.parentPE.GetChildIndex(this) > 0)
			{
				using (Pen pen = new Pen(this.ownerGrid.CategorySplitterColor, 1f))
				{
					g.DrawLine(pen, rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Y - 1);
				}
			}
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0015E230 File Offset: 0x0015C430
		public override void PaintValue(object val, Graphics g, Rectangle rect, Rectangle clipRect, GridEntry.PaintValueFlags paintFlags)
		{
			base.PaintValue(val, g, rect, clipRect, paintFlags & ~GridEntry.PaintValueFlags.DrawSelected);
			if (this.parentPE.GetChildIndex(this) > 0)
			{
				using (Pen pen = new Pen(this.ownerGrid.CategorySplitterColor, 1f))
				{
					g.DrawLine(pen, rect.X - 2, rect.Y - 1, rect.Width + 1, rect.Y - 1);
				}
			}
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0015E2BC File Offset: 0x0015C4BC
		internal override bool NotifyChildValue(GridEntry pe, int type)
		{
			return this.parentPE.NotifyChildValue(pe, type);
		}

		// Token: 0x040036B1 RID: 14001
		internal string name;

		// Token: 0x040036B2 RID: 14002
		private Brush backBrush;

		// Token: 0x040036B3 RID: 14003
		private static Hashtable categoryStates;

		// Token: 0x02000888 RID: 2184
		[ComVisible(true)]
		internal class CategoryGridEntryAccessibleObject : GridEntry.GridEntryAccessibleObject
		{
			// Token: 0x060071D2 RID: 29138 RVA: 0x001A0A9A File Offset: 0x0019EC9A
			public CategoryGridEntryAccessibleObject(CategoryGridEntry owningCategoryGridEntry)
				: base(owningCategoryGridEntry)
			{
				this._owningCategoryGridEntry = owningCategoryGridEntry;
			}

			// Token: 0x060071D3 RID: 29139 RVA: 0x001A0AAC File Offset: 0x0019ECAC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.Parent;
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this.Parent;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return propertyGridViewAccessibleObject.GetNextCategory(this._owningCategoryGridEntry);
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return propertyGridViewAccessibleObject.GetPreviousCategory(this._owningCategoryGridEntry);
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
					return propertyGridViewAccessibleObject.GetFirstChildProperty(this._owningCategoryGridEntry);
				case UnsafeNativeMethods.NavigateDirection.LastChild:
					return propertyGridViewAccessibleObject.GetLastChildProperty(this._owningCategoryGridEntry);
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x060071D4 RID: 29140 RVA: 0x001A0B23 File Offset: 0x0019ED23
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level4 && (patternId == 10007 || patternId == 10013)) || base.IsPatternSupported(patternId);
			}

			// Token: 0x060071D5 RID: 29141 RVA: 0x001A0B45 File Offset: 0x0019ED45
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level4)
				{
					if (propertyID == 30003)
					{
						return 50024;
					}
					if (propertyID == 30004)
					{
						if (AccessibilityImprovements.Level5)
						{
							return SR.GetString("CategoryPropertyGridLocalizedControlType");
						}
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x170018FA RID: 6394
			// (get) Token: 0x060071D6 RID: 29142 RVA: 0x001A0B84 File Offset: 0x0019ED84
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ButtonDropDownGrid;
				}
			}

			// Token: 0x170018FB RID: 6395
			// (get) Token: 0x060071D7 RID: 29143 RVA: 0x001A0B88 File Offset: 0x0019ED88
			internal override int Row
			{
				get
				{
					if (!AccessibilityImprovements.Level4)
					{
						return base.Row;
					}
					PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = this.Parent as PropertyGridView.PropertyGridViewAccessibleObject;
					if (propertyGridViewAccessibleObject == null)
					{
						return -1;
					}
					PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
					if (propertyGridView == null || propertyGridView.OwnerGrid == null || !propertyGridView.OwnerGrid.SortedByCategories)
					{
						return -1;
					}
					GridEntryCollection topLevelGridEntries = propertyGridView.TopLevelGridEntries;
					if (topLevelGridEntries == null)
					{
						return -1;
					}
					int num = 0;
					foreach (object obj in topLevelGridEntries)
					{
						if (this._owningCategoryGridEntry == obj)
						{
							return num;
						}
						if (obj is CategoryGridEntry)
						{
							num++;
						}
					}
					return -1;
				}
			}

			// Token: 0x040044A6 RID: 17574
			private CategoryGridEntry _owningCategoryGridEntry;
		}
	}
}
