using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Implements a dialog box that is displayed when an unhandled exception occurs in a thread.</summary>
	// Token: 0x020003A4 RID: 932
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[UIPermission(SecurityAction.Assert, Window = UIPermissionWindow.AllWindows)]
	public partial class ThreadExceptionDialog : Form
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ThreadExceptionDialog" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that represents the exception that occurred.</param>
		// Token: 0x06003CE9 RID: 15593 RVA: 0x00108440 File Offset: 0x00106640
		public ThreadExceptionDialog(Exception t)
		{
			if (DpiHelper.EnableThreadExceptionDialogHighDpiImprovements)
			{
				this.scaledMaxWidth = base.LogicalToDeviceUnits(440);
				this.scaledMaxHeight = base.LogicalToDeviceUnits(325);
				this.scaledPaddingWidth = base.LogicalToDeviceUnits(84);
				this.scaledPaddingHeight = base.LogicalToDeviceUnits(26);
				this.scaledMaxTextWidth = base.LogicalToDeviceUnits(180);
				this.scaledMaxTextHeight = base.LogicalToDeviceUnits(40);
				this.scaledButtonTopPadding = base.LogicalToDeviceUnits(31);
				this.scaledButtonDetailsLeftPadding = base.LogicalToDeviceUnits(8);
				this.scaledMessageTopPadding = base.LogicalToDeviceUnits(8);
				this.scaledHeightPadding = base.LogicalToDeviceUnits(8);
				this.scaledButtonWidth = base.LogicalToDeviceUnits(100);
				this.scaledButtonHeight = base.LogicalToDeviceUnits(23);
				this.scaledButtonAlignmentWidth = base.LogicalToDeviceUnits(105);
				this.scaledButtonAlignmentPadding = base.LogicalToDeviceUnits(5);
				this.scaledDetailsWidthPadding = base.LogicalToDeviceUnits(16);
				this.scaledDetailsHeight = base.LogicalToDeviceUnits(154);
				this.scaledPictureWidth = base.LogicalToDeviceUnits(64);
				this.scaledPictureHeight = base.LogicalToDeviceUnits(64);
				this.scaledExceptionMessageVerticalPadding = base.LogicalToDeviceUnits(4);
			}
			bool flag = false;
			WarningException ex = t as WarningException;
			string text;
			string text2;
			Button[] array;
			if (ex != null)
			{
				text = "ExDlgWarningText";
				text2 = ex.Message;
				if (ex.HelpUrl == null)
				{
					array = new Button[] { this.continueButton };
				}
				else
				{
					array = new Button[] { this.continueButton, this.helpButton };
				}
			}
			else
			{
				text2 = t.Message;
				flag = true;
				if (Application.AllowQuit)
				{
					if (t is SecurityException)
					{
						text = "ExDlgSecurityErrorText";
					}
					else
					{
						text = "ExDlgErrorText";
					}
					array = new Button[] { this.detailsButton, this.continueButton, this.quitButton };
				}
				else
				{
					if (t is SecurityException)
					{
						text = "ExDlgSecurityContinueErrorText";
					}
					else
					{
						text = "ExDlgContinueErrorText";
					}
					array = new Button[] { this.detailsButton, this.continueButton };
				}
			}
			if (text2.Length == 0)
			{
				text2 = t.GetType().Name;
			}
			if (t is SecurityException)
			{
				text2 = SR.GetString(text, new object[]
				{
					t.GetType().Name,
					ThreadExceptionDialog.Trim(text2)
				});
			}
			else
			{
				text2 = SR.GetString(text, new object[] { ThreadExceptionDialog.Trim(text2) });
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text3 = "\r\n";
			string @string = SR.GetString("ExDlgMsgSeperator");
			string string2 = SR.GetString("ExDlgMsgSectionSeperator");
			if (Application.CustomThreadExceptionHandlerAttached)
			{
				stringBuilder.Append(SR.GetString("ExDlgMsgHeaderNonSwitchable"));
			}
			else
			{
				stringBuilder.Append(SR.GetString("ExDlgMsgHeaderSwitchable"));
			}
			stringBuilder.Append(string.Format(CultureInfo.CurrentCulture, string2, new object[] { SR.GetString("ExDlgMsgExceptionSection") }));
			stringBuilder.Append(t.ToString());
			stringBuilder.Append(text3);
			stringBuilder.Append(text3);
			stringBuilder.Append(string.Format(CultureInfo.CurrentCulture, string2, new object[] { SR.GetString("ExDlgMsgLoadedAssembliesSection") }));
			new FileIOPermission(PermissionState.Unrestricted).Assert();
			try
			{
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					AssemblyName name = assembly.GetName();
					string text4 = SR.GetString("NotAvailable");
					try
					{
						if (name.EscapedCodeBase != null && name.EscapedCodeBase.Length > 0)
						{
							Uri uri = new Uri(name.EscapedCodeBase);
							if (uri.Scheme == "file")
							{
								text4 = FileVersionInfo.GetVersionInfo(NativeMethods.GetLocalPath(name.EscapedCodeBase)).FileVersion;
							}
						}
					}
					catch (FileNotFoundException)
					{
					}
					stringBuilder.Append(SR.GetString("ExDlgMsgLoadedAssembliesEntry", new object[] { name.Name, name.Version, text4, name.EscapedCodeBase }));
					stringBuilder.Append(@string);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			stringBuilder.Append(string.Format(CultureInfo.CurrentCulture, string2, new object[] { SR.GetString("ExDlgMsgJITDebuggingSection") }));
			if (Application.CustomThreadExceptionHandlerAttached)
			{
				stringBuilder.Append(SR.GetString("ExDlgMsgFooterNonSwitchable"));
			}
			else
			{
				stringBuilder.Append(SR.GetString("ExDlgMsgFooterSwitchable"));
			}
			stringBuilder.Append(text3);
			stringBuilder.Append(text3);
			string text5 = stringBuilder.ToString();
			Graphics graphics = this.message.CreateGraphicsInternal();
			Size size = new Size(this.scaledMaxWidth - this.scaledPaddingWidth, int.MaxValue);
			if (DpiHelper.EnableThreadExceptionDialogHighDpiImprovements && !Control.UseCompatibleTextRenderingDefault)
			{
				size = Size.Ceiling(TextRenderer.MeasureText(text2, this.Font, size, TextFormatFlags.WordBreak));
			}
			else
			{
				size = Size.Ceiling(graphics.MeasureString(text2, this.Font, size.Width));
			}
			size.Height += this.scaledExceptionMessageVerticalPadding;
			graphics.Dispose();
			if (size.Width < this.scaledMaxTextWidth)
			{
				size.Width = this.scaledMaxTextWidth;
			}
			if (size.Height > this.scaledMaxHeight)
			{
				size.Height = this.scaledMaxHeight;
			}
			int num = size.Width + this.scaledPaddingWidth;
			int num2 = Math.Max(size.Height, this.scaledMaxTextHeight) + this.scaledPaddingHeight;
			IntSecurity.GetParent.Assert();
			try
			{
				Form activeForm = Form.ActiveForm;
				if (activeForm == null || activeForm.Text.Length == 0)
				{
					this.Text = SR.GetString("ExDlgCaption");
				}
				else
				{
					this.Text = SR.GetString("ExDlgCaption2", new object[] { activeForm.Text });
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			base.AcceptButton = this.continueButton;
			base.CancelButton = this.continueButton;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Icon = null;
			base.ClientSize = new Size(num, num2 + this.scaledButtonTopPadding);
			base.TopMost = true;
			this.pictureBox.Location = new Point(this.scaledPictureWidth / 8, this.scaledPictureHeight / 8);
			this.pictureBox.Size = new Size(this.scaledPictureWidth * 3 / 4, this.scaledPictureHeight * 3 / 4);
			this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			if (t is SecurityException)
			{
				this.pictureBox.Image = SystemIcons.Information.ToBitmap();
			}
			else
			{
				this.pictureBox.Image = SystemIcons.Error.ToBitmap();
			}
			base.Controls.Add(this.pictureBox);
			this.message.SetBounds(this.scaledPictureWidth, this.scaledMessageTopPadding + (this.scaledMaxTextHeight - Math.Min(size.Height, this.scaledMaxTextHeight)) / 2, size.Width, size.Height);
			this.message.Text = text2;
			base.Controls.Add(this.message);
			this.continueButton.Text = SR.GetString("ExDlgContinue");
			this.continueButton.FlatStyle = FlatStyle.Standard;
			this.continueButton.DialogResult = DialogResult.Cancel;
			this.quitButton.Text = SR.GetString("ExDlgQuit");
			this.quitButton.FlatStyle = FlatStyle.Standard;
			this.quitButton.DialogResult = DialogResult.Abort;
			this.helpButton.Text = SR.GetString("ExDlgHelp");
			this.helpButton.FlatStyle = FlatStyle.Standard;
			this.helpButton.DialogResult = DialogResult.Yes;
			this.detailsButton.Text = SR.GetString("ExDlgShowDetails");
			this.detailsButton.FlatStyle = FlatStyle.Standard;
			this.detailsButton.Click += this.DetailsClick;
			int num3 = 0;
			if (flag)
			{
				Button button = this.detailsButton;
				this.expandImage = new Bitmap(base.GetType(), "down.bmp");
				this.expandImage.MakeTransparent();
				this.collapseImage = new Bitmap(base.GetType(), "up.bmp");
				this.collapseImage.MakeTransparent();
				if (DpiHelper.EnableThreadExceptionDialogHighDpiImprovements)
				{
					base.ScaleBitmapLogicalToDevice(ref this.expandImage);
					base.ScaleBitmapLogicalToDevice(ref this.collapseImage);
				}
				button.SetBounds(this.scaledButtonDetailsLeftPadding, num2, this.scaledButtonWidth, this.scaledButtonHeight);
				button.Image = this.expandImage;
				button.ImageAlign = ContentAlignment.MiddleLeft;
				base.Controls.Add(button);
				num3 = 1;
			}
			int num4 = num - this.scaledButtonDetailsLeftPadding - ((array.Length - num3) * this.scaledButtonAlignmentWidth - this.scaledButtonAlignmentPadding);
			for (int j = num3; j < array.Length; j++)
			{
				Button button = array[j];
				button.SetBounds(num4, num2, this.scaledButtonWidth, this.scaledButtonHeight);
				base.Controls.Add(button);
				num4 += this.scaledButtonAlignmentWidth;
			}
			this.details.Text = text5;
			this.details.ScrollBars = ScrollBars.Both;
			this.details.Multiline = true;
			this.details.ReadOnly = true;
			this.details.WordWrap = false;
			this.details.TabStop = false;
			this.details.AcceptsReturn = false;
			this.details.SetBounds(this.scaledButtonDetailsLeftPadding, num2 + this.scaledButtonTopPadding, num - this.scaledDetailsWidthPadding, this.scaledDetailsHeight);
			this.details.Visible = this.detailsVisible;
			base.Controls.Add(this.details);
			if (DpiHelper.EnableThreadExceptionDialogHighDpiImprovements)
			{
				base.DpiChanged += this.ThreadExceptionDialog_DpiChanged;
			}
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00108EF8 File Offset: 0x001070F8
		private void ThreadExceptionDialog_DpiChanged(object sender, DpiChangedEventArgs e)
		{
			if (this.expandImage != null)
			{
				this.expandImage.Dispose();
			}
			this.expandImage = new Bitmap(base.GetType(), "down.bmp");
			this.expandImage.MakeTransparent();
			if (this.collapseImage != null)
			{
				this.collapseImage.Dispose();
			}
			this.collapseImage = new Bitmap(base.GetType(), "up.bmp");
			this.collapseImage.MakeTransparent();
			base.ScaleBitmapLogicalToDevice(ref this.expandImage);
			base.ScaleBitmapLogicalToDevice(ref this.collapseImage);
			this.detailsButton.Image = (this.detailsVisible ? this.collapseImage : this.expandImage);
		}

		/// <summary>Gets or sets a value indicating whether the dialog box automatically sizes to its content.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box automatically sizes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x00108FA6 File Offset: 0x001071A6
		// (set) Token: 0x06003CEC RID: 15596 RVA: 0x00108FAE File Offset: 0x001071AE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ThreadExceptionDialog.AutoSize" /> property changes.</summary>
		// Token: 0x140002E8 RID: 744
		// (add) Token: 0x06003CED RID: 15597 RVA: 0x00108FB7 File Offset: 0x001071B7
		// (remove) Token: 0x06003CEE RID: 15598 RVA: 0x00108FC0 File Offset: 0x001071C0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00108FCC File Offset: 0x001071CC
		private void DetailsClick(object sender, EventArgs eventargs)
		{
			int num = this.details.Height + this.scaledHeightPadding;
			if (this.detailsVisible)
			{
				num = -num;
			}
			base.Height += num;
			this.detailsVisible = !this.detailsVisible;
			this.details.Visible = this.detailsVisible;
			this.detailsButton.Image = (this.detailsVisible ? this.collapseImage : this.expandImage);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x00109048 File Offset: 0x00107248
		private static string Trim(string s)
		{
			if (s == null)
			{
				return s;
			}
			int num = s.Length;
			while (num > 0 && s[num - 1] == '.')
			{
				num--;
			}
			return s.Substring(0, num);
		}

		// Token: 0x040023BA RID: 9146
		private const string DownBitmapName = "down.bmp";

		// Token: 0x040023BB RID: 9147
		private const string UpBitmapName = "up.bmp";

		// Token: 0x040023BC RID: 9148
		private const int MAXWIDTH = 440;

		// Token: 0x040023BD RID: 9149
		private const int MAXHEIGHT = 325;

		// Token: 0x040023BE RID: 9150
		private const int PADDINGWIDTH = 84;

		// Token: 0x040023BF RID: 9151
		private const int PADDINGHEIGHT = 26;

		// Token: 0x040023C0 RID: 9152
		private const int MAXTEXTWIDTH = 180;

		// Token: 0x040023C1 RID: 9153
		private const int MAXTEXTHEIGHT = 40;

		// Token: 0x040023C2 RID: 9154
		private const int BUTTONTOPPADDING = 31;

		// Token: 0x040023C3 RID: 9155
		private const int BUTTONDETAILS_LEFTPADDING = 8;

		// Token: 0x040023C4 RID: 9156
		private const int MESSAGE_TOPPADDING = 8;

		// Token: 0x040023C5 RID: 9157
		private const int HEIGHTPADDING = 8;

		// Token: 0x040023C6 RID: 9158
		private const int BUTTONWIDTH = 100;

		// Token: 0x040023C7 RID: 9159
		private const int BUTTONHEIGHT = 23;

		// Token: 0x040023C8 RID: 9160
		private const int BUTTONALIGNMENTWIDTH = 105;

		// Token: 0x040023C9 RID: 9161
		private const int BUTTONALIGNMENTPADDING = 5;

		// Token: 0x040023CA RID: 9162
		private const int DETAILSWIDTHPADDING = 16;

		// Token: 0x040023CB RID: 9163
		private const int DETAILSHEIGHT = 154;

		// Token: 0x040023CC RID: 9164
		private const int PICTUREWIDTH = 64;

		// Token: 0x040023CD RID: 9165
		private const int PICTUREHEIGHT = 64;

		// Token: 0x040023CE RID: 9166
		private const int EXCEPTIONMESSAGEVERTICALPADDING = 4;

		// Token: 0x040023CF RID: 9167
		private int scaledMaxWidth = 440;

		// Token: 0x040023D0 RID: 9168
		private int scaledMaxHeight = 325;

		// Token: 0x040023D1 RID: 9169
		private int scaledPaddingWidth = 84;

		// Token: 0x040023D2 RID: 9170
		private int scaledPaddingHeight = 26;

		// Token: 0x040023D3 RID: 9171
		private int scaledMaxTextWidth = 180;

		// Token: 0x040023D4 RID: 9172
		private int scaledMaxTextHeight = 40;

		// Token: 0x040023D5 RID: 9173
		private int scaledButtonTopPadding = 31;

		// Token: 0x040023D6 RID: 9174
		private int scaledButtonDetailsLeftPadding = 8;

		// Token: 0x040023D7 RID: 9175
		private int scaledMessageTopPadding = 8;

		// Token: 0x040023D8 RID: 9176
		private int scaledHeightPadding = 8;

		// Token: 0x040023D9 RID: 9177
		private int scaledButtonWidth = 100;

		// Token: 0x040023DA RID: 9178
		private int scaledButtonHeight = 23;

		// Token: 0x040023DB RID: 9179
		private int scaledButtonAlignmentWidth = 105;

		// Token: 0x040023DC RID: 9180
		private int scaledButtonAlignmentPadding = 5;

		// Token: 0x040023DD RID: 9181
		private int scaledDetailsWidthPadding = 16;

		// Token: 0x040023DE RID: 9182
		private int scaledDetailsHeight = 154;

		// Token: 0x040023DF RID: 9183
		private int scaledPictureWidth = 64;

		// Token: 0x040023E0 RID: 9184
		private int scaledPictureHeight = 64;

		// Token: 0x040023E1 RID: 9185
		private int scaledExceptionMessageVerticalPadding = 4;

		// Token: 0x040023E2 RID: 9186
		private PictureBox pictureBox = new PictureBox();

		// Token: 0x040023E3 RID: 9187
		private Label message = new Label();

		// Token: 0x040023E4 RID: 9188
		private Button continueButton = new Button();

		// Token: 0x040023E5 RID: 9189
		private Button quitButton = new Button();

		// Token: 0x040023E6 RID: 9190
		private Button detailsButton = new Button();

		// Token: 0x040023E7 RID: 9191
		private Button helpButton = new Button();

		// Token: 0x040023E8 RID: 9192
		private TextBox details = new TextBox();

		// Token: 0x040023E9 RID: 9193
		private Bitmap expandImage;

		// Token: 0x040023EA RID: 9194
		private Bitmap collapseImage;

		// Token: 0x040023EB RID: 9195
		private bool detailsVisible;
	}
}
