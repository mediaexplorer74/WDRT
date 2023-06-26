using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Used to indicate the expected drop location when an item is dragged to a new position in a <see cref="T:System.Windows.Forms.ListView" /> control. This functionality is available only on Windows XP and later.</summary>
	// Token: 0x020002DA RID: 730
	public sealed class ListViewInsertionMark
	{
		// Token: 0x06002E2B RID: 11819 RVA: 0x000D176F File Offset: 0x000CF96F
		internal ListViewInsertionMark(ListView listView)
		{
			this.listView = listView;
		}

		/// <summary>Gets or sets a value indicating whether the insertion mark appears to the right of the item with the index specified by the <see cref="P:System.Windows.Forms.ListViewInsertionMark.Index" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the insertion mark appears to the right of the item with the index specified by the <see cref="P:System.Windows.Forms.ListViewInsertionMark.Index" /> property; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000D1789 File Offset: 0x000CF989
		// (set) Token: 0x06002E2D RID: 11821 RVA: 0x000D1791 File Offset: 0x000CF991
		public bool AppearsAfterItem
		{
			get
			{
				return this.appearsAfterItem;
			}
			set
			{
				if (this.appearsAfterItem != value)
				{
					this.appearsAfterItem = value;
					if (this.listView.IsHandleCreated)
					{
						this.UpdateListView();
					}
				}
			}
		}

		/// <summary>Gets the bounding rectangle of the insertion mark.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the position and size of the insertion mark.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000D17B8 File Offset: 0x000CF9B8
		public Rectangle Bounds
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				this.listView.SendMessage(4265, 0, ref rect);
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		/// <summary>Gets or sets the color of the insertion mark.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the color of the insertion mark. The default value is the value of the <see cref="P:System.Windows.Forms.ListView.ForeColor" /> property.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000D17FE File Offset: 0x000CF9FE
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x000D1838 File Offset: 0x000CFA38
		public Color Color
		{
			get
			{
				if (this.color.IsEmpty)
				{
					this.color = SafeNativeMethods.ColorFromCOLORREF((int)this.listView.SendMessage(4267, 0, 0));
				}
				return this.color;
			}
			set
			{
				if (this.color != value)
				{
					this.color = value;
					if (this.listView.IsHandleCreated)
					{
						this.listView.SendMessage(4266, 0, SafeNativeMethods.ColorToCOLORREF(this.color));
					}
				}
			}
		}

		/// <summary>Gets or sets the index of the item next to which the insertion mark appears.</summary>
		/// <returns>The index of the item next to which the insertion mark appears or -1 when the insertion mark is hidden.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000D1884 File Offset: 0x000CFA84
		// (set) Token: 0x06002E32 RID: 11826 RVA: 0x000D188C File Offset: 0x000CFA8C
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				if (this.index != value)
				{
					this.index = value;
					if (this.listView.IsHandleCreated)
					{
						this.UpdateListView();
					}
				}
			}
		}

		/// <summary>Retrieves the index of the item closest to the specified point.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> representing the location from which to find the nearest item.</param>
		/// <returns>The index of the item closest to the specified point or -1 if the closest item is the item currently being dragged.</returns>
		// Token: 0x06002E33 RID: 11827 RVA: 0x000D18B4 File Offset: 0x000CFAB4
		public int NearestIndex(Point pt)
		{
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = pt.X;
			point.y = pt.Y;
			NativeMethods.LVINSERTMARK lvinsertmark = new NativeMethods.LVINSERTMARK();
			UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), 4264, point, lvinsertmark);
			return lvinsertmark.iItem;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000D1910 File Offset: 0x000CFB10
		internal void UpdateListView()
		{
			NativeMethods.LVINSERTMARK lvinsertmark = new NativeMethods.LVINSERTMARK();
			lvinsertmark.dwFlags = (this.appearsAfterItem ? 1 : 0);
			lvinsertmark.iItem = this.index;
			UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), 4262, 0, lvinsertmark);
			if (!this.color.IsEmpty)
			{
				this.listView.SendMessage(4266, 0, SafeNativeMethods.ColorToCOLORREF(this.color));
			}
		}

		// Token: 0x0400131A RID: 4890
		private ListView listView;

		// Token: 0x0400131B RID: 4891
		private int index;

		// Token: 0x0400131C RID: 4892
		private Color color = Color.Empty;

		// Token: 0x0400131D RID: 4893
		private bool appearsAfterItem;
	}
}
