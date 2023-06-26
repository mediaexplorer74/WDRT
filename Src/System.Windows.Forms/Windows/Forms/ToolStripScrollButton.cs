using System;
using System.Drawing;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020003FC RID: 1020
	internal class ToolStripScrollButton : ToolStripControlHost
	{
		// Token: 0x06004673 RID: 18035 RVA: 0x001286BE File Offset: 0x001268BE
		public ToolStripScrollButton(bool up)
			: base(ToolStripScrollButton.CreateControlInstance(up))
		{
			this.up = up;
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x001286DC File Offset: 0x001268DC
		private static Control CreateControlInstance(bool up)
		{
			return new ToolStripScrollButton.StickyLabel
			{
				ImageAlign = ContentAlignment.MiddleCenter,
				Image = (up ? ToolStripScrollButton.UpImage : ToolStripScrollButton.DownImage)
			};
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x00019A61 File Offset: 0x00017C61
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x00019A61 File Offset: 0x00017C61
		protected override Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x0012870D File Offset: 0x0012690D
		private static Image DownImage
		{
			get
			{
				if (ToolStripScrollButton.downScrollImage == null)
				{
					ToolStripScrollButton.downScrollImage = new Bitmap(typeof(ToolStripScrollButton), "ScrollButtonDown.bmp");
					ToolStripScrollButton.downScrollImage.MakeTransparent(Color.White);
				}
				return ToolStripScrollButton.downScrollImage;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x00128743 File Offset: 0x00126943
		internal ToolStripScrollButton.StickyLabel Label
		{
			get
			{
				return base.Control as ToolStripScrollButton.StickyLabel;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x00128750 File Offset: 0x00126950
		private static Image UpImage
		{
			get
			{
				if (ToolStripScrollButton.upScrollImage == null)
				{
					ToolStripScrollButton.upScrollImage = new Bitmap(typeof(ToolStripScrollButton), "ScrollButtonUp.bmp");
					ToolStripScrollButton.upScrollImage.MakeTransparent(Color.White);
				}
				return ToolStripScrollButton.upScrollImage;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x00128786 File Offset: 0x00126986
		private Timer MouseDownTimer
		{
			get
			{
				if (this.mouseDownTimer == null)
				{
					this.mouseDownTimer = new Timer();
				}
				return this.mouseDownTimer;
			}
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x001287A1 File Offset: 0x001269A1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.mouseDownTimer != null)
			{
				this.mouseDownTimer.Enabled = false;
				this.mouseDownTimer.Dispose();
				this.mouseDownTimer = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x001287D4 File Offset: 0x001269D4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.UnsubscribeAll();
			base.OnMouseDown(e);
			this.Scroll();
			this.MouseDownTimer.Interval = ToolStripScrollButton.AUTOSCROLL_PAUSE;
			this.MouseDownTimer.Tick += this.OnInitialAutoScrollMouseDown;
			this.MouseDownTimer.Enabled = true;
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00128827 File Offset: 0x00126A27
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.UnsubscribeAll();
			base.OnMouseUp(e);
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x00128836 File Offset: 0x00126A36
		protected override void OnMouseLeave(EventArgs e)
		{
			this.UnsubscribeAll();
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x0012883E File Offset: 0x00126A3E
		private void UnsubscribeAll()
		{
			this.MouseDownTimer.Enabled = false;
			this.MouseDownTimer.Tick -= this.OnInitialAutoScrollMouseDown;
			this.MouseDownTimer.Tick -= this.OnAutoScrollAccellerate;
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x0012887A File Offset: 0x00126A7A
		private void OnAutoScrollAccellerate(object sender, EventArgs e)
		{
			this.Scroll();
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00128884 File Offset: 0x00126A84
		private void OnInitialAutoScrollMouseDown(object sender, EventArgs e)
		{
			this.MouseDownTimer.Tick -= this.OnInitialAutoScrollMouseDown;
			this.Scroll();
			this.MouseDownTimer.Interval = 50;
			this.MouseDownTimer.Tick += this.OnAutoScrollAccellerate;
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x001288D4 File Offset: 0x00126AD4
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size empty = Size.Empty;
			empty.Height = ((this.Label.Image != null) ? (this.Label.Image.Height + 4) : 0);
			empty.Width = ((base.ParentInternal != null) ? (base.ParentInternal.Width - 2) : empty.Width);
			return empty;
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00128938 File Offset: 0x00126B38
		private void Scroll()
		{
			ToolStripDropDownMenu toolStripDropDownMenu = base.ParentInternal as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null && this.Label.Enabled)
			{
				toolStripDropDownMenu.ScrollInternal(this.up);
			}
		}

		// Token: 0x040026A6 RID: 9894
		private bool up = true;

		// Token: 0x040026A7 RID: 9895
		[ThreadStatic]
		private static Bitmap upScrollImage;

		// Token: 0x040026A8 RID: 9896
		[ThreadStatic]
		private static Bitmap downScrollImage;

		// Token: 0x040026A9 RID: 9897
		private const int AUTOSCROLL_UPDATE = 50;

		// Token: 0x040026AA RID: 9898
		private static readonly int AUTOSCROLL_PAUSE = SystemInformation.DoubleClickTime;

		// Token: 0x040026AB RID: 9899
		private Timer mouseDownTimer;

		// Token: 0x02000817 RID: 2071
		internal class StickyLabel : Label
		{
			// Token: 0x17001868 RID: 6248
			// (get) Token: 0x06006F95 RID: 28565 RVA: 0x00199696 File Offset: 0x00197896
			public bool FreezeLocationChange
			{
				get
				{
					return this.freezeLocationChange;
				}
			}

			// Token: 0x06006F96 RID: 28566 RVA: 0x0019969E File Offset: 0x0019789E
			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				if ((specified & BoundsSpecified.Location) != BoundsSpecified.None && this.FreezeLocationChange)
				{
					return;
				}
				base.SetBoundsCore(x, y, width, height, specified);
			}

			// Token: 0x06006F97 RID: 28567 RVA: 0x001996BC File Offset: 0x001978BC
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				if (m.Msg >= 256 && m.Msg <= 264)
				{
					this.DefWndProc(ref m);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x04004321 RID: 17185
			private bool freezeLocationChange;
		}
	}
}
