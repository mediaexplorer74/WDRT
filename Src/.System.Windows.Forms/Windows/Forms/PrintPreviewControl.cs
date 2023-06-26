using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents the raw preview part of print previewing from a Windows Forms application, without any dialog boxes or buttons. Most <see cref="T:System.Windows.Forms.PrintPreviewControl" /> objects are found on <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> objects, but they do not have to be.</summary>
	// Token: 0x0200044A RID: 1098
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Document")]
	[SRDescription("DescriptionPrintPreviewControl")]
	public class PrintPreviewControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> class.</summary>
		// Token: 0x06004C44 RID: 19524 RVA: 0x0013CB58 File Offset: 0x0013AD58
		public PrintPreviewControl()
		{
			this.ResetBackColor();
			this.ResetForeColor();
			base.Size = new Size(100, 100);
			base.SetStyle(ControlStyles.ResizeRedraw, false);
			base.SetStyle(ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer, true);
		}

		/// <summary>Gets or sets a value indicating whether printing uses the anti-aliasing features of the operating system.</summary>
		/// <returns>
		///   <see langword="true" /> if anti-aliasing is used; otherwise, <see langword="false" />.</returns>
		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06004C45 RID: 19525 RVA: 0x0013CBEF File Offset: 0x0013ADEF
		// (set) Token: 0x06004C46 RID: 19526 RVA: 0x0013CBF7 File Offset: 0x0013ADF7
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PrintPreviewAntiAliasDescr")]
		public bool UseAntiAlias
		{
			get
			{
				return this.antiAlias;
			}
			set
			{
				this.antiAlias = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether resizing the control or changing the number of pages shown automatically adjusts the <see cref="P:System.Windows.Forms.PrintPreviewControl.Zoom" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the changing the control size or number of pages adjusts the <see cref="P:System.Windows.Forms.PrintPreviewControl.Zoom" /> property; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06004C47 RID: 19527 RVA: 0x0013CC00 File Offset: 0x0013AE00
		// (set) Token: 0x06004C48 RID: 19528 RVA: 0x0013CC08 File Offset: 0x0013AE08
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PrintPreviewAutoZoomDescr")]
		public bool AutoZoom
		{
			get
			{
				return this.autoZoom;
			}
			set
			{
				if (this.autoZoom != value)
				{
					this.autoZoom = value;
					this.InvalidateLayout();
				}
			}
		}

		/// <summary>Gets or sets a value indicating the document to preview.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> representing the document to preview.</returns>
		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06004C49 RID: 19529 RVA: 0x0013CC20 File Offset: 0x0013AE20
		// (set) Token: 0x06004C4A RID: 19530 RVA: 0x0013CC28 File Offset: 0x0013AE28
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("PrintPreviewDocumentDescr")]
		public PrintDocument Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
				this.InvalidatePreview();
			}
		}

		/// <summary>Gets or sets the number of pages displayed horizontally across the screen.</summary>
		/// <returns>The number of pages displayed horizontally across the screen. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 1.</exception>
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06004C4B RID: 19531 RVA: 0x0013CC37 File Offset: 0x0013AE37
		// (set) Token: 0x06004C4C RID: 19532 RVA: 0x0013CC40 File Offset: 0x0013AE40
		[DefaultValue(1)]
		[SRCategory("CatLayout")]
		[SRDescription("PrintPreviewColumnsDescr")]
		public int Columns
		{
			get
			{
				return this.columns;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("Columns", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"Columns",
						value.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.columns = value;
				this.InvalidateLayout();
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06004C4D RID: 19533 RVA: 0x0013CCA4 File Offset: 0x0013AEA4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 1048576;
				createParams.Style |= 2097152;
				return createParams;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06004C4E RID: 19534 RVA: 0x0013CCDD File Offset: 0x0013AEDD
		// (set) Token: 0x06004C4F RID: 19535 RVA: 0x0013CCE5 File Offset: 0x0013AEE5
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWithScrollbarsPositionDescr")]
		private Point Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.SetPositionNoInvalidate(value);
			}
		}

		/// <summary>Gets or sets the number of pages displayed vertically down the screen.</summary>
		/// <returns>The number of pages displayed vertically down the screen. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 1.</exception>
		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06004C50 RID: 19536 RVA: 0x0013CCEE File Offset: 0x0013AEEE
		// (set) Token: 0x06004C51 RID: 19537 RVA: 0x0013CCF8 File Offset: 0x0013AEF8
		[DefaultValue(1)]
		[SRDescription("PrintPreviewRowsDescr")]
		[SRCategory("CatBehavior")]
		public int Rows
		{
			get
			{
				return this.rows;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("Rows", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"Rows",
						value.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.rows = value;
				this.InvalidateLayout();
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06004C52 RID: 19538 RVA: 0x000E322B File Offset: 0x000E142B
		// (set) Token: 0x06004C53 RID: 19539 RVA: 0x0013CD59 File Offset: 0x0013AF59
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		[SRDescription("ControlRightToLeftDescr")]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
				this.InvalidatePreview();
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06004C54 RID: 19540 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06004C55 RID: 19541 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewControl.Text" /> property changes.</summary>
		// Token: 0x140003EE RID: 1006
		// (add) Token: 0x06004C56 RID: 19542 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06004C57 RID: 19543 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Gets or sets the page number of the upper left page.</summary>
		/// <returns>The page number of the upper left page. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06004C58 RID: 19544 RVA: 0x0013CD68 File Offset: 0x0013AF68
		// (set) Token: 0x06004C59 RID: 19545 RVA: 0x0013CDAC File Offset: 0x0013AFAC
		[DefaultValue(0)]
		[SRDescription("PrintPreviewStartPageDescr")]
		[SRCategory("CatBehavior")]
		public int StartPage
		{
			get
			{
				int num = this.startPage;
				if (this.pageInfo != null)
				{
					num = Math.Min(num, this.pageInfo.Length - this.rows * this.columns);
				}
				return Math.Max(num, 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("StartPage", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"StartPage",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				int num = this.StartPage;
				this.startPage = value;
				if (num != this.startPage)
				{
					this.InvalidateLayout();
					this.OnStartPageChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the start page changes.</summary>
		// Token: 0x140003EF RID: 1007
		// (add) Token: 0x06004C5A RID: 19546 RVA: 0x0013CE28 File Offset: 0x0013B028
		// (remove) Token: 0x06004C5B RID: 19547 RVA: 0x0013CE3B File Offset: 0x0013B03B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("RadioButtonOnStartPageChangedDescr")]
		public event EventHandler StartPageChanged
		{
			add
			{
				base.Events.AddHandler(PrintPreviewControl.EVENT_STARTPAGECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(PrintPreviewControl.EVENT_STARTPAGECHANGED, value);
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06004C5C RID: 19548 RVA: 0x0013CE4E File Offset: 0x0013B04E
		// (set) Token: 0x06004C5D RID: 19549 RVA: 0x0013CE56 File Offset: 0x0013B056
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWithScrollbarsVirtualSizeDescr")]
		private Size VirtualSize
		{
			get
			{
				return this.virtualSize;
			}
			set
			{
				this.SetVirtualSizeNoInvalidate(value);
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating how large the pages will appear.</summary>
		/// <returns>A value indicating how large the pages will appear. A value of 1.0 indicates full size.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than or equal to 0.</exception>
		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06004C5E RID: 19550 RVA: 0x0013CE65 File Offset: 0x0013B065
		// (set) Token: 0x06004C5F RID: 19551 RVA: 0x0013CE6D File Offset: 0x0013B06D
		[SRCategory("CatBehavior")]
		[SRDescription("PrintPreviewZoomDescr")]
		[DefaultValue(0.3)]
		public double Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException(SR.GetString("PrintPreviewControlZoomNegative"));
				}
				this.autoZoom = false;
				this.zoom = value;
				this.InvalidateLayout();
			}
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0013CEA0 File Offset: 0x0013B0A0
		private int AdjustScroll(Message m, int pos, int maxPos, bool horizontal)
		{
			switch (NativeMethods.Util.LOWORD(m.WParam))
			{
			case 0:
				if (pos > 5)
				{
					pos -= 5;
				}
				else
				{
					pos = 0;
				}
				break;
			case 1:
				if (pos < maxPos - 5)
				{
					pos += 5;
				}
				else
				{
					pos = maxPos;
				}
				break;
			case 2:
				if (pos > 100)
				{
					pos -= 100;
				}
				else
				{
					pos = 0;
				}
				break;
			case 3:
				if (pos < maxPos - 100)
				{
					pos += 100;
				}
				else
				{
					pos = maxPos;
				}
				break;
			case 4:
			case 5:
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
				scrollinfo.fMask = 16;
				int num = (horizontal ? 0 : 1);
				if (SafeNativeMethods.GetScrollInfo(new HandleRef(this, m.HWnd), num, scrollinfo))
				{
					pos = scrollinfo.nTrackPos;
				}
				else
				{
					pos = NativeMethods.Util.HIWORD(m.WParam);
				}
				break;
			}
			}
			return pos;
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0013CF80 File Offset: 0x0013B180
		private void ComputeLayout()
		{
			this.layoutOk = true;
			if (this.pageInfo.Length == 0)
			{
				base.ClientSize = base.Size;
				return;
			}
			Graphics graphics = base.CreateGraphicsInternal();
			IntPtr hdc = graphics.GetHdc();
			this.screendpi = new Point(UnsafeNativeMethods.GetDeviceCaps(new HandleRef(graphics, hdc), 88), UnsafeNativeMethods.GetDeviceCaps(new HandleRef(graphics, hdc), 90));
			graphics.ReleaseHdcInternal(hdc);
			graphics.Dispose();
			Size physicalSize = this.pageInfo[this.StartPage].PhysicalSize;
			Size size = new Size(PrintPreviewControl.PixelsToPhysical(new Point(base.Size), this.screendpi));
			if (this.autoZoom)
			{
				double num = ((double)size.Width - (double)(10 * (this.columns + 1))) / (double)(this.columns * physicalSize.Width);
				double num2 = ((double)size.Height - (double)(10 * (this.rows + 1))) / (double)(this.rows * physicalSize.Height);
				this.zoom = Math.Min(num, num2);
			}
			this.imageSize = new Size((int)(this.zoom * (double)physicalSize.Width), (int)(this.zoom * (double)physicalSize.Height));
			int num3 = this.imageSize.Width * this.columns + 10 * (this.columns + 1);
			int num4 = this.imageSize.Height * this.rows + 10 * (this.rows + 1);
			this.SetVirtualSizeNoInvalidate(new Size(PrintPreviewControl.PhysicalToPixels(new Point(num3, num4), this.screendpi)));
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0013D10C File Offset: 0x0013B30C
		private void ComputePreview()
		{
			int num = this.StartPage;
			if (this.document == null)
			{
				this.pageInfo = new PreviewPageInfo[0];
			}
			else
			{
				IntSecurity.SafePrinting.Demand();
				PrintController printController = this.document.PrintController;
				PreviewPrintController previewPrintController = new PreviewPrintController();
				previewPrintController.UseAntiAlias = this.UseAntiAlias;
				this.document.PrintController = new PrintControllerWithStatusDialog(previewPrintController, SR.GetString("PrintControllerWithStatusDialog_DialogTitlePreview"));
				this.document.Print();
				this.pageInfo = previewPrintController.GetPreviewPageInfo();
				this.document.PrintController = printController;
			}
			if (num != this.StartPage)
			{
				this.OnStartPageChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0013D1B0 File Offset: 0x0013B3B0
		private void InvalidateLayout()
		{
			this.layoutOk = false;
			base.Invalidate();
		}

		/// <summary>Refreshes the preview of the document.</summary>
		// Token: 0x06004C64 RID: 19556 RVA: 0x0013D1BF File Offset: 0x0013B3BF
		public void InvalidatePreview()
		{
			this.pageInfo = null;
			this.InvalidateLayout();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004C65 RID: 19557 RVA: 0x0013D1CE File Offset: 0x0013B3CE
		protected override void OnResize(EventArgs eventargs)
		{
			this.InvalidateLayout();
			base.OnResize(eventargs);
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x0013D1E0 File Offset: 0x0013B3E0
		private void CalculatePageInfo()
		{
			if (this.pageInfoCalcPending)
			{
				return;
			}
			this.pageInfoCalcPending = true;
			try
			{
				if (this.pageInfo == null)
				{
					try
					{
						this.ComputePreview();
					}
					catch
					{
						this.exceptionPrinting = true;
						throw;
					}
					finally
					{
						base.Invalidate();
					}
				}
			}
			finally
			{
				this.pageInfoCalcPending = false;
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" /> method.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06004C67 RID: 19559 RVA: 0x0013D250 File Offset: 0x0013B450
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Brush brush = new SolidBrush(this.BackColor);
			try
			{
				if (this.pageInfo == null || this.pageInfo.Length == 0)
				{
					pevent.Graphics.FillRectangle(brush, base.ClientRectangle);
					if (this.pageInfo != null || this.exceptionPrinting)
					{
						StringFormat stringFormat = new StringFormat();
						stringFormat.Alignment = ControlPaint.TranslateAlignment(ContentAlignment.MiddleCenter);
						stringFormat.LineAlignment = ControlPaint.TranslateLineAlignment(ContentAlignment.MiddleCenter);
						SolidBrush solidBrush = new SolidBrush(this.ForeColor);
						try
						{
							if (this.exceptionPrinting)
							{
								pevent.Graphics.DrawString(SR.GetString("PrintPreviewExceptionPrinting"), this.Font, solidBrush, base.ClientRectangle, stringFormat);
								goto IL_4E4;
							}
							pevent.Graphics.DrawString(SR.GetString("PrintPreviewNoPages"), this.Font, solidBrush, base.ClientRectangle, stringFormat);
							goto IL_4E4;
						}
						finally
						{
							solidBrush.Dispose();
							stringFormat.Dispose();
						}
					}
					base.BeginInvoke(new MethodInvoker(this.CalculatePageInfo));
				}
				else
				{
					if (!this.layoutOk)
					{
						this.ComputeLayout();
					}
					Size size = new Size(PrintPreviewControl.PixelsToPhysical(new Point(base.Size), this.screendpi));
					Point point = new Point(this.VirtualSize);
					Point point2 = new Point(Math.Max(0, (base.Size.Width - point.X) / 2), Math.Max(0, (base.Size.Height - point.Y) / 2));
					point2.X -= this.Position.X;
					point2.Y -= this.Position.Y;
					this.lastOffset = point2;
					int num = PrintPreviewControl.PhysicalToPixels(10, this.screendpi.X);
					int num2 = PrintPreviewControl.PhysicalToPixels(10, this.screendpi.Y);
					Region clip = pevent.Graphics.Clip;
					Rectangle[] array = new Rectangle[this.rows * this.columns];
					Point empty = Point.Empty;
					int num3 = 0;
					try
					{
						for (int i = 0; i < this.rows; i++)
						{
							empty.X = 0;
							empty.Y = num3 * i;
							for (int j = 0; j < this.columns; j++)
							{
								int num4 = this.StartPage + j + i * this.columns;
								if (num4 < this.pageInfo.Length)
								{
									Size physicalSize = this.pageInfo[num4].PhysicalSize;
									if (this.autoZoom)
									{
										double num5 = ((double)size.Width - (double)(10 * (this.columns + 1))) / (double)(this.columns * physicalSize.Width);
										double num6 = ((double)size.Height - (double)(10 * (this.rows + 1))) / (double)(this.rows * physicalSize.Height);
										this.zoom = Math.Min(num5, num6);
									}
									this.imageSize = new Size((int)(this.zoom * (double)physicalSize.Width), (int)(this.zoom * (double)physicalSize.Height));
									Point point3 = PrintPreviewControl.PhysicalToPixels(new Point(this.imageSize), this.screendpi);
									int num7 = point2.X + num * (j + 1) + empty.X;
									int num8 = point2.Y + num2 * (i + 1) + empty.Y;
									empty.X += point3.X;
									num3 = Math.Max(num3, point3.Y);
									array[num4 - this.StartPage] = new Rectangle(num7, num8, point3.X, point3.Y);
									pevent.Graphics.ExcludeClip(array[num4 - this.StartPage]);
								}
							}
						}
						pevent.Graphics.FillRectangle(brush, base.ClientRectangle);
					}
					finally
					{
						pevent.Graphics.Clip = clip;
					}
					for (int k = 0; k < array.Length; k++)
					{
						if (k + this.StartPage < this.pageInfo.Length)
						{
							Rectangle rectangle = array[k];
							pevent.Graphics.DrawRectangle(Pens.Black, rectangle);
							using (SolidBrush solidBrush2 = new SolidBrush(this.ForeColor))
							{
								pevent.Graphics.FillRectangle(solidBrush2, rectangle);
							}
							rectangle.Inflate(-1, -1);
							if (this.pageInfo[k + this.StartPage].Image != null)
							{
								pevent.Graphics.DrawImage(this.pageInfo[k + this.StartPage].Image, rectangle);
							}
							int num9 = rectangle.Width;
							rectangle.Width = num9 - 1;
							num9 = rectangle.Height;
							rectangle.Height = num9 - 1;
							pevent.Graphics.DrawRectangle(Pens.Black, rectangle);
						}
					}
				}
			}
			finally
			{
				brush.Dispose();
			}
			IL_4E4:
			base.OnPaint(pevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PrintPreviewControl.StartPageChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004C68 RID: 19560 RVA: 0x0013D7AC File Offset: 0x0013B9AC
		protected virtual void OnStartPageChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[PrintPreviewControl.EVENT_STARTPAGECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0013D7DA File Offset: 0x0013B9DA
		private static int PhysicalToPixels(int physicalSize, int dpi)
		{
			return (int)((double)(physicalSize * dpi) / 100.0);
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0013D7EB File Offset: 0x0013B9EB
		private static Point PhysicalToPixels(Point physical, Point dpi)
		{
			return new Point(PrintPreviewControl.PhysicalToPixels(physical.X, dpi.X), PrintPreviewControl.PhysicalToPixels(physical.Y, dpi.Y));
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0013D818 File Offset: 0x0013BA18
		private static Size PhysicalToPixels(Size physicalSize, Point dpi)
		{
			return new Size(PrintPreviewControl.PhysicalToPixels(physicalSize.Width, dpi.X), PrintPreviewControl.PhysicalToPixels(physicalSize.Height, dpi.Y));
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0013D845 File Offset: 0x0013BA45
		private static int PixelsToPhysical(int pixels, int dpi)
		{
			return (int)((double)pixels * 100.0 / (double)dpi);
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0013D857 File Offset: 0x0013BA57
		private static Point PixelsToPhysical(Point pixels, Point dpi)
		{
			return new Point(PrintPreviewControl.PixelsToPhysical(pixels.X, dpi.X), PrintPreviewControl.PixelsToPhysical(pixels.Y, dpi.Y));
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0013D884 File Offset: 0x0013BA84
		private static Size PixelsToPhysical(Size pixels, Point dpi)
		{
			return new Size(PrintPreviewControl.PixelsToPhysical(pixels.Width, dpi.X), PrintPreviewControl.PixelsToPhysical(pixels.Height, dpi.Y));
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.BackColor" /> property to <see cref="P:System.Drawing.SystemColors.AppWorkspace" />, which is the default color.</summary>
		// Token: 0x06004C6F RID: 19567 RVA: 0x0013D8B1 File Offset: 0x0013BAB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetBackColor()
		{
			this.BackColor = SystemColors.AppWorkspace;
		}

		/// <summary>Resets the foreground color of the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> to <see cref="P:System.Drawing.Color.White" />, which is the default color.</summary>
		// Token: 0x06004C70 RID: 19568 RVA: 0x0013D8BE File Offset: 0x0013BABE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetForeColor()
		{
			this.ForeColor = Color.White;
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0013D8CC File Offset: 0x0013BACC
		private void WmHScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
				return;
			}
			Point point = this.position;
			int x = point.X;
			int num = Math.Max(base.Width, this.virtualSize.Width);
			point.X = this.AdjustScroll(m, x, num, true);
			this.Position = point;
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0013D938 File Offset: 0x0013BB38
		private void SetPositionNoInvalidate(Point value)
		{
			Point point = this.position;
			this.position = value;
			this.position.X = Math.Min(this.position.X, this.virtualSize.Width - base.Width);
			this.position.Y = Math.Min(this.position.Y, this.virtualSize.Height - base.Height);
			if (this.position.X < 0)
			{
				this.position.X = 0;
			}
			if (this.position.Y < 0)
			{
				this.position.Y = 0;
			}
			Rectangle clientRectangle = base.ClientRectangle;
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, clientRectangle.Height);
			SafeNativeMethods.ScrollWindow(new HandleRef(this, base.Handle), point.X - this.position.X, point.Y - this.position.Y, ref rect, ref rect);
			UnsafeNativeMethods.SetScrollPos(new HandleRef(this, base.Handle), 0, this.position.X, true);
			UnsafeNativeMethods.SetScrollPos(new HandleRef(this, base.Handle), 1, this.position.Y, true);
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0013DA84 File Offset: 0x0013BC84
		internal void SetVirtualSizeNoInvalidate(Size value)
		{
			this.virtualSize = value;
			this.SetPositionNoInvalidate(this.position);
			NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
			scrollinfo.fMask = 3;
			scrollinfo.nMin = 0;
			scrollinfo.nMax = Math.Max(base.Height, this.virtualSize.Height) - 1;
			scrollinfo.nPage = base.Height;
			UnsafeNativeMethods.SetScrollInfo(new HandleRef(this, base.Handle), 1, scrollinfo, true);
			scrollinfo.fMask = 3;
			scrollinfo.nMin = 0;
			scrollinfo.nMax = Math.Max(base.Width, this.virtualSize.Width) - 1;
			scrollinfo.nPage = base.Width;
			UnsafeNativeMethods.SetScrollInfo(new HandleRef(this, base.Handle), 0, scrollinfo, true);
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0013DB44 File Offset: 0x0013BD44
		private void WmVScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
				return;
			}
			Point point = this.Position;
			int y = point.Y;
			int num = Math.Max(base.Height, this.virtualSize.Height);
			point.Y = this.AdjustScroll(m, y, num, false);
			this.Position = point;
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0013DBB0 File Offset: 0x0013BDB0
		private void WmKeyDown(ref Message msg)
		{
			Keys keys = (Keys)((int)msg.WParam | (int)Control.ModifierKeys);
			Point point = this.Position;
			switch (keys & Keys.KeyCode)
			{
			case Keys.Prior:
				if ((keys & Keys.Modifiers) == Keys.Control)
				{
					int num = point.X;
					if (num > 100)
					{
						num -= 100;
					}
					else
					{
						num = 0;
					}
					point.X = num;
					this.Position = point;
					return;
				}
				if (this.StartPage > 0)
				{
					int num2 = this.StartPage;
					this.StartPage = num2 - 1;
					return;
				}
				break;
			case Keys.Next:
				if ((keys & Keys.Modifiers) == Keys.Control)
				{
					int num = point.X;
					int num3 = Math.Max(base.Width, this.virtualSize.Width);
					if (num < num3 - 100)
					{
						num += 100;
					}
					else
					{
						num = num3;
					}
					point.X = num;
					this.Position = point;
					return;
				}
				if (this.StartPage < this.pageInfo.Length)
				{
					int num2 = this.StartPage;
					this.StartPage = num2 + 1;
					return;
				}
				break;
			case Keys.End:
				if ((keys & Keys.Modifiers) == Keys.Control)
				{
					this.StartPage = this.pageInfo.Length;
					return;
				}
				break;
			case Keys.Home:
				if ((keys & Keys.Modifiers) == Keys.Control)
				{
					this.StartPage = 0;
					return;
				}
				break;
			case Keys.Left:
			{
				int num = point.X;
				if (num > 5)
				{
					num -= 5;
				}
				else
				{
					num = 0;
				}
				point.X = num;
				this.Position = point;
				return;
			}
			case Keys.Up:
			{
				int num = point.Y;
				if (num > 5)
				{
					num -= 5;
				}
				else
				{
					num = 0;
				}
				point.Y = num;
				this.Position = point;
				return;
			}
			case Keys.Right:
			{
				int num = point.X;
				int num3 = Math.Max(base.Width, this.virtualSize.Width);
				if (num < num3 - 5)
				{
					num += 5;
				}
				else
				{
					num = num3;
				}
				point.X = num;
				this.Position = point;
				break;
			}
			case Keys.Down:
			{
				int num = point.Y;
				int num3 = Math.Max(base.Height, this.virtualSize.Height);
				if (num < num3 - 5)
				{
					num += 5;
				}
				else
				{
					num = num3;
				}
				point.Y = num;
				this.Position = point;
				return;
			}
			default:
				return;
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06004C76 RID: 19574 RVA: 0x0013DDD0 File Offset: 0x0013BFD0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 256)
			{
				this.WmKeyDown(ref m);
				return;
			}
			if (msg == 276)
			{
				this.WmHScroll(ref m);
				return;
			}
			if (msg == 277)
			{
				this.WmVScroll(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0013DE1C File Offset: 0x0013C01C
		internal override bool ShouldSerializeBackColor()
		{
			return !this.BackColor.Equals(SystemColors.AppWorkspace);
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0013DE4C File Offset: 0x0013C04C
		internal override bool ShouldSerializeForeColor()
		{
			return !this.ForeColor.Equals(Color.White);
		}

		// Token: 0x0400287E RID: 10366
		private Size virtualSize = new Size(1, 1);

		// Token: 0x0400287F RID: 10367
		private Point position = new Point(0, 0);

		// Token: 0x04002880 RID: 10368
		private Point lastOffset;

		// Token: 0x04002881 RID: 10369
		private bool antiAlias;

		// Token: 0x04002882 RID: 10370
		private const int SCROLL_PAGE = 100;

		// Token: 0x04002883 RID: 10371
		private const int SCROLL_LINE = 5;

		// Token: 0x04002884 RID: 10372
		private const double DefaultZoom = 0.3;

		// Token: 0x04002885 RID: 10373
		private const int border = 10;

		// Token: 0x04002886 RID: 10374
		private PrintDocument document;

		// Token: 0x04002887 RID: 10375
		private PreviewPageInfo[] pageInfo;

		// Token: 0x04002888 RID: 10376
		private int startPage;

		// Token: 0x04002889 RID: 10377
		private int rows = 1;

		// Token: 0x0400288A RID: 10378
		private int columns = 1;

		// Token: 0x0400288B RID: 10379
		private bool autoZoom = true;

		// Token: 0x0400288C RID: 10380
		private bool layoutOk;

		// Token: 0x0400288D RID: 10381
		private Size imageSize = Size.Empty;

		// Token: 0x0400288E RID: 10382
		private Point screendpi = Point.Empty;

		// Token: 0x0400288F RID: 10383
		private double zoom = 0.3;

		// Token: 0x04002890 RID: 10384
		private bool pageInfoCalcPending;

		// Token: 0x04002891 RID: 10385
		private bool exceptionPrinting;

		// Token: 0x04002892 RID: 10386
		private static readonly object EVENT_STARTPAGECHANGED = new object();
	}
}
