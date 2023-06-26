using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Controls how a document is printed from a Windows Forms application.</summary>
	// Token: 0x02000448 RID: 1096
	public class PrintControllerWithStatusDialog : PrintController
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class, wrapping the supplied <see cref="T:System.Drawing.Printing.PrintController" />.</summary>
		/// <param name="underlyingController">A <see cref="T:System.Drawing.Printing.PrintController" /> to encapsulate.</param>
		// Token: 0x06004C1F RID: 19487 RVA: 0x0013BE0C File Offset: 0x0013A00C
		public PrintControllerWithStatusDialog(PrintController underlyingController)
			: this(underlyingController, SR.GetString("PrintControllerWithStatusDialog_DialogTitlePrint"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class, wrapping the supplied <see cref="T:System.Drawing.Printing.PrintController" /> and specifying a title for the dialog box.</summary>
		/// <param name="underlyingController">A <see cref="T:System.Drawing.Printing.PrintController" /> to encapsulate.</param>
		/// <param name="dialogTitle">A <see cref="T:System.String" /> containing a title for the status dialog box.</param>
		// Token: 0x06004C20 RID: 19488 RVA: 0x0013BE1F File Offset: 0x0013A01F
		public PrintControllerWithStatusDialog(PrintController underlyingController, string dialogTitle)
		{
			this.underlyingController = underlyingController;
			this.dialogTitle = dialogTitle;
		}

		/// <summary>Gets a value indicating this <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> is used for print preview.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> is used for print preview, otherwise, <see langword="false" />.</returns>
		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x0013BE35 File Offset: 0x0013A035
		public override bool IsPreview
		{
			get
			{
				return this.underlyingController != null && this.underlyingController.IsPreview;
			}
		}

		/// <summary>Begins the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
		// Token: 0x06004C22 RID: 19490 RVA: 0x0013BE4C File Offset: 0x0013A04C
		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			base.OnStartPrint(document, e);
			this.document = document;
			this.pageNumber = 1;
			if (SystemInformation.UserInteractive)
			{
				this.backgroundThread = new PrintControllerWithStatusDialog.BackgroundThread(this);
			}
			try
			{
				this.underlyingController.OnStartPrint(document, e);
			}
			catch
			{
				if (this.backgroundThread != null)
				{
					this.backgroundThread.Stop();
				}
				throw;
			}
			finally
			{
				if (this.backgroundThread != null && this.backgroundThread.canceled)
				{
					e.Cancel = true;
				}
			}
		}

		/// <summary>Begins the control sequence that determines when and how to print a page of a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> object that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
		// Token: 0x06004C23 RID: 19491 RVA: 0x0013BEE4 File Offset: 0x0013A0E4
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			base.OnStartPage(document, e);
			if (this.backgroundThread != null)
			{
				this.backgroundThread.UpdateLabel();
			}
			Graphics graphics = this.underlyingController.OnStartPage(document, e);
			if (this.backgroundThread != null && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			return graphics;
		}

		/// <summary>Completes the control sequence that determines when and how to print a page of a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		// Token: 0x06004C24 RID: 19492 RVA: 0x0013BF38 File Offset: 0x0013A138
		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
			this.underlyingController.OnEndPage(document, e);
			if (this.backgroundThread != null && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			this.pageNumber++;
			base.OnEndPage(document, e);
		}

		/// <summary>Completes the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		// Token: 0x06004C25 RID: 19493 RVA: 0x0013BF84 File Offset: 0x0013A184
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
			this.underlyingController.OnEndPrint(document, e);
			if (this.backgroundThread != null && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			if (this.backgroundThread != null)
			{
				this.backgroundThread.Stop();
			}
			base.OnEndPrint(document, e);
		}

		// Token: 0x0400286E RID: 10350
		private PrintController underlyingController;

		// Token: 0x0400286F RID: 10351
		private PrintDocument document;

		// Token: 0x04002870 RID: 10352
		private PrintControllerWithStatusDialog.BackgroundThread backgroundThread;

		// Token: 0x04002871 RID: 10353
		private int pageNumber;

		// Token: 0x04002872 RID: 10354
		private string dialogTitle;

		// Token: 0x02000830 RID: 2096
		private class BackgroundThread
		{
			// Token: 0x0600701E RID: 28702 RVA: 0x0019A5F1 File Offset: 0x001987F1
			internal BackgroundThread(PrintControllerWithStatusDialog parent)
			{
				this.parent = parent;
				this.thread = new Thread(new ThreadStart(this.Run));
				this.thread.SetApartmentState(ApartmentState.STA);
				this.thread.Start();
			}

			// Token: 0x0600701F RID: 28703 RVA: 0x0019A630 File Offset: 0x00198830
			[UIPermission(SecurityAction.Assert, Window = UIPermissionWindow.AllWindows)]
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			private void Run()
			{
				try
				{
					lock (this)
					{
						if (this.alreadyStopped)
						{
							return;
						}
						this.dialog = new PrintControllerWithStatusDialog.StatusDialog(this, this.parent.dialogTitle);
						this.ThreadUnsafeUpdateLabel();
						this.dialog.Visible = true;
					}
					if (!this.alreadyStopped)
					{
						Application.Run(this.dialog);
					}
				}
				finally
				{
					lock (this)
					{
						if (this.dialog != null)
						{
							this.dialog.Dispose();
							this.dialog = null;
						}
					}
				}
			}

			// Token: 0x06007020 RID: 28704 RVA: 0x0019A6F8 File Offset: 0x001988F8
			internal void Stop()
			{
				lock (this)
				{
					if (this.dialog != null && this.dialog.IsHandleCreated)
					{
						this.dialog.BeginInvoke(new MethodInvoker(this.dialog.Close));
					}
					else
					{
						this.alreadyStopped = true;
					}
				}
			}

			// Token: 0x06007021 RID: 28705 RVA: 0x0019A768 File Offset: 0x00198968
			private void ThreadUnsafeUpdateLabel()
			{
				this.dialog.label1.Text = SR.GetString("PrintControllerWithStatusDialog_NowPrinting", new object[]
				{
					this.parent.pageNumber,
					this.parent.document.DocumentName
				});
			}

			// Token: 0x06007022 RID: 28706 RVA: 0x0019A7BB File Offset: 0x001989BB
			internal void UpdateLabel()
			{
				if (this.dialog != null && this.dialog.IsHandleCreated)
				{
					this.dialog.BeginInvoke(new MethodInvoker(this.ThreadUnsafeUpdateLabel));
				}
			}

			// Token: 0x04004354 RID: 17236
			private PrintControllerWithStatusDialog parent;

			// Token: 0x04004355 RID: 17237
			private PrintControllerWithStatusDialog.StatusDialog dialog;

			// Token: 0x04004356 RID: 17238
			private Thread thread;

			// Token: 0x04004357 RID: 17239
			internal bool canceled;

			// Token: 0x04004358 RID: 17240
			private bool alreadyStopped;
		}

		// Token: 0x02000831 RID: 2097
		private class StatusDialog : Form
		{
			// Token: 0x06007023 RID: 28707 RVA: 0x0019A7EA File Offset: 0x001989EA
			internal StatusDialog(PrintControllerWithStatusDialog.BackgroundThread backgroundThread, string dialogTitle)
			{
				this.InitializeComponent();
				this.backgroundThread = backgroundThread;
				this.Text = dialogTitle;
				this.MinimumSize = base.Size;
			}

			// Token: 0x1700187F RID: 6271
			// (get) Token: 0x06007024 RID: 28708 RVA: 0x000F750D File Offset: 0x000F570D
			private static bool IsRTLResources
			{
				get
				{
					return SR.GetString("RTL") != "RTL_False";
				}
			}

			// Token: 0x06007025 RID: 28709 RVA: 0x0019A814 File Offset: 0x00198A14
			private void InitializeComponent()
			{
				if (PrintControllerWithStatusDialog.StatusDialog.IsRTLResources)
				{
					this.RightToLeft = RightToLeft.Yes;
				}
				this.tableLayoutPanel1 = new TableLayoutPanel();
				this.label1 = new Label();
				this.button1 = new Button();
				this.label1.AutoSize = true;
				this.label1.Location = new Point(8, 16);
				this.label1.TextAlign = ContentAlignment.MiddleCenter;
				this.label1.Size = new Size(240, 64);
				this.label1.TabIndex = 1;
				this.label1.Anchor = AnchorStyles.None;
				this.button1.AutoSize = true;
				this.button1.Size = new Size(75, 23);
				this.button1.TabIndex = 0;
				this.button1.Text = SR.GetString("PrintControllerWithStatusDialog_Cancel");
				this.button1.Location = new Point(88, 88);
				this.button1.Anchor = AnchorStyles.None;
				this.button1.Click += this.button1_Click;
				this.tableLayoutPanel1.AutoSize = true;
				this.tableLayoutPanel1.ColumnCount = 1;
				this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				this.tableLayoutPanel1.Dock = DockStyle.Fill;
				this.tableLayoutPanel1.Location = new Point(0, 0);
				this.tableLayoutPanel1.RowCount = 2;
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
				this.tableLayoutPanel1.TabIndex = 0;
				this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
				this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
				base.AutoScaleDimensions = new Size(6, 13);
				base.AutoScaleMode = AutoScaleMode.Font;
				base.MaximizeBox = false;
				base.ControlBox = false;
				base.MinimizeBox = false;
				Size size = new Size(256, 122);
				if (DpiHelper.IsScalingRequired)
				{
					base.ClientSize = DpiHelper.LogicalToDeviceUnits(size, 0);
				}
				else
				{
					base.ClientSize = size;
				}
				base.CancelButton = this.button1;
				base.SizeGripStyle = SizeGripStyle.Hide;
				base.Controls.Add(this.tableLayoutPanel1);
			}

			// Token: 0x06007026 RID: 28710 RVA: 0x0019AA73 File Offset: 0x00198C73
			private void button1_Click(object sender, EventArgs e)
			{
				this.button1.Enabled = false;
				this.label1.Text = SR.GetString("PrintControllerWithStatusDialog_Canceling");
				this.backgroundThread.canceled = true;
			}

			// Token: 0x04004359 RID: 17241
			internal Label label1;

			// Token: 0x0400435A RID: 17242
			private Button button1;

			// Token: 0x0400435B RID: 17243
			private TableLayoutPanel tableLayoutPanel1;

			// Token: 0x0400435C RID: 17244
			private PrintControllerWithStatusDialog.BackgroundThread backgroundThread;
		}
	}
}
