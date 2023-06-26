using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	// Token: 0x0200017C RID: 380
	internal class DataGridCaption
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x0004F75C File Offset: 0x0004D95C
		internal DataGridCaption(DataGrid dataGrid)
		{
			this.dataGrid = dataGrid;
			this.downButtonVisible = dataGrid.ParentRowsVisible;
			DataGridCaption.colorMap[0].OldColor = Color.White;
			DataGridCaption.colorMap[0].NewColor = this.ForeColor;
			this.OnGridFontChanged();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0004F7D8 File Offset: 0x0004D9D8
		internal void OnGridFontChanged()
		{
			if (this.dataGridFont == null || !this.dataGridFont.Equals(this.dataGrid.Font))
			{
				try
				{
					this.dataGridFont = new Font(this.dataGrid.Font, FontStyle.Bold);
				}
				catch
				{
				}
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0004F834 File Offset: 0x0004DA34
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x0004F83C File Offset: 0x0004DA3C
		internal bool BackButtonActive
		{
			get
			{
				return this.backActive;
			}
			set
			{
				if (this.backActive != value)
				{
					this.backActive = value;
					this.InvalidateCaptionRect(this.backButtonRect);
				}
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0004F85A File Offset: 0x0004DA5A
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x0004F862 File Offset: 0x0004DA62
		internal bool DownButtonActive
		{
			get
			{
				return this.downActive;
			}
			set
			{
				if (this.downActive != value)
				{
					this.downActive = value;
					this.InvalidateCaptionRect(this.downButtonRect);
				}
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x000445D2 File Offset: 0x000427D2
		internal static SolidBrush DefaultBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaption;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0004F880 File Offset: 0x0004DA80
		internal static Pen DefaultTextBorderPen
		{
			get
			{
				return new Pen(SystemColors.ActiveCaptionText);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x000445DE File Offset: 0x000427DE
		internal static SolidBrush DefaultForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaptionText;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0004F88C File Offset: 0x0004DA8C
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x0004F89C File Offset: 0x0004DA9C
		internal Color BackColor
		{
			get
			{
				return this.backBrush.Color;
			}
			set
			{
				if (!this.backBrush.Color.Equals(value))
				{
					if (value.IsEmpty)
					{
						throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "Caption BackColor" }));
					}
					this.backBrush = new SolidBrush(value);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0004F903 File Offset: 0x0004DB03
		internal EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0004F91E File Offset: 0x0004DB1E
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x0004F938 File Offset: 0x0004DB38
		internal Font Font
		{
			get
			{
				if (this.textFont == null)
				{
					return this.dataGridFont;
				}
				return this.textFont;
			}
			set
			{
				if (this.textFont == null || !this.textFont.Equals(value))
				{
					this.textFont = value;
					if (this.dataGrid.Caption != null)
					{
						this.dataGrid.RecalculateFonts();
						this.dataGrid.PerformLayout();
						this.dataGrid.Invalidate();
					}
				}
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0004F990 File Offset: 0x0004DB90
		internal bool ShouldSerializeFont()
		{
			return this.textFont != null && !this.textFont.Equals(this.dataGridFont);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0004F9B0 File Offset: 0x0004DBB0
		internal bool ShouldSerializeBackColor()
		{
			return !this.backBrush.Equals(DataGridCaption.DefaultBackBrush);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0004F9C5 File Offset: 0x0004DBC5
		internal void ResetBackColor()
		{
			if (this.ShouldSerializeBackColor())
			{
				this.backBrush = DataGridCaption.DefaultBackBrush;
				this.Invalidate();
			}
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
		internal void ResetForeColor()
		{
			if (this.ShouldSerializeForeColor())
			{
				this.foreBrush = DataGridCaption.DefaultForeBrush;
				this.Invalidate();
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0004F9FB File Offset: 0x0004DBFB
		internal bool ShouldSerializeForeColor()
		{
			return !this.foreBrush.Equals(DataGridCaption.DefaultForeBrush);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0004FA10 File Offset: 0x0004DC10
		internal void ResetFont()
		{
			this.textFont = null;
			this.Invalidate();
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0004FA1F File Offset: 0x0004DC1F
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x0004FA27 File Offset: 0x0004DC27
		internal string Text
		{
			get
			{
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
				this.Invalidate();
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0004FA46 File Offset: 0x0004DC46
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x0004FA4E File Offset: 0x0004DC4E
		internal bool TextBorderVisible
		{
			get
			{
				return this.textBorderVisible;
			}
			set
			{
				this.textBorderVisible = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0004FA5D File Offset: 0x0004DC5D
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x0004FA6C File Offset: 0x0004DC6C
		internal Color ForeColor
		{
			get
			{
				return this.foreBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "Caption ForeColor" }));
				}
				this.foreBrush = new SolidBrush(value);
				DataGridCaption.colorMap[0].NewColor = this.ForeColor;
				this.Invalidate();
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0004FAC4 File Offset: 0x0004DCC4
		internal Point MinimumBounds
		{
			get
			{
				return DataGridCaption.minimumBounds;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0004FACB File Offset: 0x0004DCCB
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x0004FAD3 File Offset: 0x0004DCD3
		internal bool BackButtonVisible
		{
			get
			{
				return this.backButtonVisible;
			}
			set
			{
				if (this.backButtonVisible != value)
				{
					this.backButtonVisible = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0004FAEB File Offset: 0x0004DCEB
		// (set) Token: 0x06001624 RID: 5668 RVA: 0x0004FAF3 File Offset: 0x0004DCF3
		internal bool DownButtonVisible
		{
			get
			{
				return this.downButtonVisible;
			}
			set
			{
				if (this.downButtonVisible != value)
				{
					this.downButtonVisible = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0004FB0C File Offset: 0x0004DD0C
		protected virtual void AddEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					for (DataGridCaption.EventEntry next = this.eventList; next != null; next = next.next)
					{
						if (next.key == key)
						{
							next.handler = Delegate.Combine(next.handler, handler);
							return;
						}
					}
					this.eventList = new DataGridCaption.EventEntry(this.eventList, key, handler);
				}
			}
		}

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06001626 RID: 5670 RVA: 0x0004FB8C File Offset: 0x0004DD8C
		// (remove) Token: 0x06001627 RID: 5671 RVA: 0x0004FB9F File Offset: 0x0004DD9F
		internal event EventHandler BackwardClicked
		{
			add
			{
				this.Events.AddHandler(DataGridCaption.EVENT_BACKWARDCLICKED, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataGridCaption.EVENT_BACKWARDCLICKED, value);
			}
		}

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06001628 RID: 5672 RVA: 0x0004FBB2 File Offset: 0x0004DDB2
		// (remove) Token: 0x06001629 RID: 5673 RVA: 0x0004FBC5 File Offset: 0x0004DDC5
		internal event EventHandler CaptionClicked
		{
			add
			{
				this.Events.AddHandler(DataGridCaption.EVENT_CAPTIONCLICKED, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataGridCaption.EVENT_CAPTIONCLICKED, value);
			}
		}

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x0600162A RID: 5674 RVA: 0x0004FBD8 File Offset: 0x0004DDD8
		// (remove) Token: 0x0600162B RID: 5675 RVA: 0x0004FBEB File Offset: 0x0004DDEB
		internal event EventHandler DownClicked
		{
			add
			{
				this.Events.AddHandler(DataGridCaption.EVENT_DOWNCLICKED, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataGridCaption.EVENT_DOWNCLICKED, value);
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0004FBFE File Offset: 0x0004DDFE
		private void Invalidate()
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateCaption();
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0004FC13 File Offset: 0x0004DE13
		private void InvalidateCaptionRect(Rectangle r)
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateCaptionRect(r);
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		private void InvalidateLocation(DataGridCaption.CaptionLocation loc)
		{
			Rectangle rectangle;
			if (loc == DataGridCaption.CaptionLocation.BackButton)
			{
				rectangle = this.backButtonRect;
				rectangle.Inflate(1, 1);
				this.InvalidateCaptionRect(rectangle);
				return;
			}
			if (loc != DataGridCaption.CaptionLocation.DownButton)
			{
				return;
			}
			rectangle = this.downButtonRect;
			rectangle.Inflate(1, 1);
			this.InvalidateCaptionRect(rectangle);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0004FC74 File Offset: 0x0004DE74
		protected void OnBackwardClicked(EventArgs e)
		{
			if (this.backActive)
			{
				EventHandler eventHandler = (EventHandler)this.Events[DataGridCaption.EVENT_BACKWARDCLICKED];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0004FCAC File Offset: 0x0004DEAC
		protected void OnCaptionClicked(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.Events[DataGridCaption.EVENT_CAPTIONCLICKED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004FCDC File Offset: 0x0004DEDC
		protected void OnDownClicked(EventArgs e)
		{
			if (this.downActive && this.downButtonVisible)
			{
				EventHandler eventHandler = (EventHandler)this.Events[DataGridCaption.EVENT_DOWNCLICKED];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0004FD1C File Offset: 0x0004DF1C
		private Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(typeof(DataGridCaption), bitmapName);
				bitmap.MakeTransparent();
			}
			catch (Exception ex)
			{
			}
			return bitmap;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004FD58 File Offset: 0x0004DF58
		private Bitmap GetBackButtonBmp(bool alignRight)
		{
			if (alignRight)
			{
				if (DataGridCaption.leftButtonBitmap_bidi == null)
				{
					DataGridCaption.leftButtonBitmap_bidi = this.GetBitmap("DataGridCaption.backarrow_bidi.bmp");
				}
				return DataGridCaption.leftButtonBitmap_bidi;
			}
			if (DataGridCaption.leftButtonBitmap == null)
			{
				DataGridCaption.leftButtonBitmap = this.GetBitmap("DataGridCaption.backarrow.bmp");
			}
			return DataGridCaption.leftButtonBitmap;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0004FD96 File Offset: 0x0004DF96
		private Bitmap GetDetailsBmp()
		{
			if (DataGridCaption.magnifyingGlassBitmap == null)
			{
				DataGridCaption.magnifyingGlassBitmap = this.GetBitmap("DataGridCaption.Details.bmp");
			}
			return DataGridCaption.magnifyingGlassBitmap;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004FDB4 File Offset: 0x0004DFB4
		protected virtual Delegate GetEventHandler(object key)
		{
			Delegate @delegate;
			lock (this)
			{
				for (DataGridCaption.EventEntry next = this.eventList; next != null; next = next.next)
				{
					if (next.key == key)
					{
						return next.handler;
					}
				}
				@delegate = null;
			}
			return @delegate;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0004FE14 File Offset: 0x0004E014
		internal Rectangle GetBackButtonRect(Rectangle bounds, bool alignRight, int downButtonWidth)
		{
			Bitmap backButtonBmp = this.GetBackButtonBmp(false);
			Bitmap bitmap = backButtonBmp;
			Size size;
			lock (bitmap)
			{
				size = backButtonBmp.Size;
			}
			return new Rectangle(bounds.Right - 12 - downButtonWidth - size.Width, bounds.Y + 1 + 2, size.Width, size.Height);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0004FE8C File Offset: 0x0004E08C
		internal int GetDetailsButtonWidth()
		{
			int num = 0;
			Bitmap detailsBmp = this.GetDetailsBmp();
			Bitmap bitmap = detailsBmp;
			lock (bitmap)
			{
				num = detailsBmp.Size.Width;
			}
			return num;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0004FEDC File Offset: 0x0004E0DC
		internal Rectangle GetDetailsButtonRect(Rectangle bounds, bool alignRight)
		{
			Bitmap detailsBmp = this.GetDetailsBmp();
			Bitmap bitmap = detailsBmp;
			Size size;
			lock (bitmap)
			{
				size = detailsBmp.Size;
			}
			int width = size.Width;
			return new Rectangle(bounds.Right - 6 - width, bounds.Y + 1 + 2, width, size.Height);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0004FF4C File Offset: 0x0004E14C
		internal void Paint(Graphics g, Rectangle bounds, bool alignRight)
		{
			Size size = new Size((int)g.MeasureString(this.text, this.Font).Width + 2, this.Font.Height + 2);
			this.downButtonRect = this.GetDetailsButtonRect(bounds, alignRight);
			int detailsButtonWidth = this.GetDetailsButtonWidth();
			this.backButtonRect = this.GetBackButtonRect(bounds, alignRight, detailsButtonWidth);
			int num = (this.backButtonVisible ? (this.backButtonRect.Width + 3 + 4) : 0);
			int num2 = ((this.downButtonVisible && !this.dataGrid.ParentRowsIsEmpty()) ? (detailsButtonWidth + 3 + 4) : 0);
			int num3 = bounds.Width - 3 - num - num2;
			this.textRect = new Rectangle(bounds.X, bounds.Y + 1, Math.Min(num3, 4 + size.Width), 4 + size.Height);
			if (alignRight)
			{
				this.textRect.X = bounds.Right - this.textRect.Width;
				this.backButtonRect.X = bounds.X + 12 + detailsButtonWidth;
				this.downButtonRect.X = bounds.X + 6;
			}
			g.FillRectangle(this.backBrush, bounds);
			if (this.backButtonVisible)
			{
				this.PaintBackButton(g, this.backButtonRect, alignRight);
				if (this.backActive && this.lastMouseLocation == DataGridCaption.CaptionLocation.BackButton)
				{
					this.backButtonRect.Inflate(1, 1);
					ControlPaint.DrawBorder3D(g, this.backButtonRect, this.backPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
				}
			}
			this.PaintText(g, this.textRect, alignRight);
			if (this.downButtonVisible && !this.dataGrid.ParentRowsIsEmpty())
			{
				this.PaintDownButton(g, this.downButtonRect);
				if (this.lastMouseLocation == DataGridCaption.CaptionLocation.DownButton)
				{
					this.downButtonRect.Inflate(1, 1);
					ControlPaint.DrawBorder3D(g, this.downButtonRect, this.downPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
				}
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00050130 File Offset: 0x0004E330
		private void PaintIcon(Graphics g, Rectangle bounds, Bitmap b)
		{
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetRemapTable(DataGridCaption.colorMap, ColorAdjustType.Bitmap);
			g.DrawImage(b, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, imageAttributes);
			imageAttributes.Dispose();
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00050170 File Offset: 0x0004E370
		private void PaintBackButton(Graphics g, Rectangle bounds, bool alignRight)
		{
			Bitmap backButtonBmp = this.GetBackButtonBmp(alignRight);
			Bitmap bitmap = backButtonBmp;
			lock (bitmap)
			{
				this.PaintIcon(g, bounds, backButtonBmp);
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000501B8 File Offset: 0x0004E3B8
		private void PaintDownButton(Graphics g, Rectangle bounds)
		{
			Bitmap detailsBmp = this.GetDetailsBmp();
			Bitmap bitmap = detailsBmp;
			lock (bitmap)
			{
				this.PaintIcon(g, bounds, detailsBmp);
			}
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00050200 File Offset: 0x0004E400
		private void PaintText(Graphics g, Rectangle bounds, bool alignToRight)
		{
			Rectangle rectangle = bounds;
			if (rectangle.Width <= 0 || rectangle.Height <= 0)
			{
				return;
			}
			if (this.textBorderVisible)
			{
				g.DrawRectangle(this.textBorderPen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				rectangle.Inflate(-1, -1);
			}
			Rectangle rectangle2 = rectangle;
			rectangle2.Height = 2;
			g.FillRectangle(this.backBrush, rectangle2);
			rectangle2.Y = rectangle.Bottom - 2;
			g.FillRectangle(this.backBrush, rectangle2);
			rectangle2 = new Rectangle(rectangle.X, rectangle.Y + 2, 2, rectangle.Height - 4);
			g.FillRectangle(this.backBrush, rectangle2);
			rectangle2.X = rectangle.Right - 2;
			g.FillRectangle(this.backBrush, rectangle2);
			rectangle.Inflate(-2, -2);
			g.FillRectangle(this.backBrush, rectangle);
			StringFormat stringFormat = new StringFormat();
			if (alignToRight)
			{
				stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				stringFormat.Alignment = StringAlignment.Far;
			}
			g.DrawString(this.text, this.Font, this.foreBrush, rectangle, stringFormat);
			stringFormat.Dispose();
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0005033C File Offset: 0x0004E53C
		private DataGridCaption.CaptionLocation FindLocation(int x, int y)
		{
			if (!this.backButtonRect.IsEmpty && this.backButtonRect.Contains(x, y))
			{
				return DataGridCaption.CaptionLocation.BackButton;
			}
			if (!this.downButtonRect.IsEmpty && this.downButtonRect.Contains(x, y))
			{
				return DataGridCaption.CaptionLocation.DownButton;
			}
			if (!this.textRect.IsEmpty && this.textRect.Contains(x, y))
			{
				return DataGridCaption.CaptionLocation.Text;
			}
			return DataGridCaption.CaptionLocation.Nowhere;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000503A4 File Offset: 0x0004E5A4
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x000503AC File Offset: 0x0004E5AC
		private bool DownButtonDown
		{
			get
			{
				return this.downButtonDown;
			}
			set
			{
				if (this.downButtonDown != value)
				{
					this.downButtonDown = value;
					this.InvalidateLocation(DataGridCaption.CaptionLocation.DownButton);
				}
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000503C5 File Offset: 0x0004E5C5
		internal bool GetDownButtonDirection()
		{
			return this.DownButtonDown;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000503D0 File Offset: 0x0004E5D0
		internal void MouseDown(int x, int y)
		{
			DataGridCaption.CaptionLocation captionLocation = this.FindLocation(x, y);
			switch (captionLocation)
			{
			case DataGridCaption.CaptionLocation.BackButton:
				this.backPressed = true;
				this.InvalidateLocation(captionLocation);
				return;
			case DataGridCaption.CaptionLocation.DownButton:
				this.downPressed = true;
				this.InvalidateLocation(captionLocation);
				return;
			case DataGridCaption.CaptionLocation.Text:
				this.OnCaptionClicked(EventArgs.Empty);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00050424 File Offset: 0x0004E624
		internal void MouseUp(int x, int y)
		{
			DataGridCaption.CaptionLocation captionLocation = this.FindLocation(x, y);
			if (captionLocation != DataGridCaption.CaptionLocation.BackButton)
			{
				if (captionLocation == DataGridCaption.CaptionLocation.DownButton && this.downPressed)
				{
					this.downPressed = false;
					this.OnDownClicked(EventArgs.Empty);
					return;
				}
			}
			else if (this.backPressed)
			{
				this.backPressed = false;
				this.OnBackwardClicked(EventArgs.Empty);
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00050478 File Offset: 0x0004E678
		internal void MouseLeft()
		{
			DataGridCaption.CaptionLocation captionLocation = this.lastMouseLocation;
			this.lastMouseLocation = DataGridCaption.CaptionLocation.Nowhere;
			this.InvalidateLocation(captionLocation);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0005049C File Offset: 0x0004E69C
		internal void MouseOver(int x, int y)
		{
			DataGridCaption.CaptionLocation captionLocation = this.FindLocation(x, y);
			this.InvalidateLocation(this.lastMouseLocation);
			this.InvalidateLocation(captionLocation);
			this.lastMouseLocation = captionLocation;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000504CC File Offset: 0x0004E6CC
		protected virtual void RaiseEvent(object key, EventArgs e)
		{
			Delegate eventHandler = this.GetEventHandler(key);
			if (eventHandler != null)
			{
				((EventHandler)eventHandler)(this, e);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000504F4 File Offset: 0x0004E6F4
		protected virtual void RemoveEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					DataGridCaption.EventEntry next = this.eventList;
					DataGridCaption.EventEntry eventEntry = null;
					while (next != null)
					{
						if (next.key == key)
						{
							next.handler = Delegate.Remove(next.handler, handler);
							if (next.handler == null)
							{
								if (eventEntry == null)
								{
									this.eventList = next.next;
								}
								else
								{
									eventEntry.next = next.next;
								}
							}
							break;
						}
						eventEntry = next;
						next = next.next;
					}
				}
			}
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00050588 File Offset: 0x0004E788
		protected virtual void RemoveEventHandlers()
		{
			this.eventList = null;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00050591 File Offset: 0x0004E791
		internal void SetDownButtonDirection(bool pointDown)
		{
			this.DownButtonDown = pointDown;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0005059A File Offset: 0x0004E79A
		internal bool ToggleDownButtonDirection()
		{
			this.DownButtonDown = !this.DownButtonDown;
			return this.DownButtonDown;
		}

		// Token: 0x04000A10 RID: 2576
		internal EventHandlerList events;

		// Token: 0x04000A11 RID: 2577
		private const int xOffset = 3;

		// Token: 0x04000A12 RID: 2578
		private const int yOffset = 1;

		// Token: 0x04000A13 RID: 2579
		private const int textPadding = 2;

		// Token: 0x04000A14 RID: 2580
		private const int buttonToText = 4;

		// Token: 0x04000A15 RID: 2581
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000A16 RID: 2582
		private static readonly Point minimumBounds = new Point(50, 30);

		// Token: 0x04000A17 RID: 2583
		private DataGrid dataGrid;

		// Token: 0x04000A18 RID: 2584
		private bool backButtonVisible;

		// Token: 0x04000A19 RID: 2585
		private bool downButtonVisible;

		// Token: 0x04000A1A RID: 2586
		private SolidBrush backBrush = DataGridCaption.DefaultBackBrush;

		// Token: 0x04000A1B RID: 2587
		private SolidBrush foreBrush = DataGridCaption.DefaultForeBrush;

		// Token: 0x04000A1C RID: 2588
		private Pen textBorderPen = DataGridCaption.DefaultTextBorderPen;

		// Token: 0x04000A1D RID: 2589
		private string text = "";

		// Token: 0x04000A1E RID: 2590
		private bool textBorderVisible;

		// Token: 0x04000A1F RID: 2591
		private Font textFont;

		// Token: 0x04000A20 RID: 2592
		private Font dataGridFont;

		// Token: 0x04000A21 RID: 2593
		private bool backActive;

		// Token: 0x04000A22 RID: 2594
		private bool downActive;

		// Token: 0x04000A23 RID: 2595
		private bool backPressed;

		// Token: 0x04000A24 RID: 2596
		private bool downPressed;

		// Token: 0x04000A25 RID: 2597
		private bool downButtonDown;

		// Token: 0x04000A26 RID: 2598
		private static Bitmap leftButtonBitmap;

		// Token: 0x04000A27 RID: 2599
		private static Bitmap leftButtonBitmap_bidi;

		// Token: 0x04000A28 RID: 2600
		private static Bitmap magnifyingGlassBitmap;

		// Token: 0x04000A29 RID: 2601
		private Rectangle backButtonRect;

		// Token: 0x04000A2A RID: 2602
		private Rectangle downButtonRect;

		// Token: 0x04000A2B RID: 2603
		private Rectangle textRect;

		// Token: 0x04000A2C RID: 2604
		private DataGridCaption.CaptionLocation lastMouseLocation;

		// Token: 0x04000A2D RID: 2605
		private DataGridCaption.EventEntry eventList;

		// Token: 0x04000A2E RID: 2606
		private static readonly object EVENT_BACKWARDCLICKED = new object();

		// Token: 0x04000A2F RID: 2607
		private static readonly object EVENT_DOWNCLICKED = new object();

		// Token: 0x04000A30 RID: 2608
		private static readonly object EVENT_CAPTIONCLICKED = new object();

		// Token: 0x02000649 RID: 1609
		internal enum CaptionLocation
		{
			// Token: 0x040039C6 RID: 14790
			Nowhere,
			// Token: 0x040039C7 RID: 14791
			BackButton,
			// Token: 0x040039C8 RID: 14792
			DownButton,
			// Token: 0x040039C9 RID: 14793
			Text
		}

		// Token: 0x0200064A RID: 1610
		private sealed class EventEntry
		{
			// Token: 0x060064C7 RID: 25799 RVA: 0x00177260 File Offset: 0x00175460
			internal EventEntry(DataGridCaption.EventEntry next, object key, Delegate handler)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

			// Token: 0x040039CA RID: 14794
			internal DataGridCaption.EventEntry next;

			// Token: 0x040039CB RID: 14795
			internal object key;

			// Token: 0x040039CC RID: 14796
			internal Delegate handler;
		}
	}
}
