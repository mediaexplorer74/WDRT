using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Microsoft.Win32;

namespace System.Security.Policy
{
	// Token: 0x02000105 RID: 261
	internal partial class TrustManagerPromptUI : Form
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		internal TrustManagerPromptUI(string appName, string defaultBrowserExePath, string supportUrl, string deploymentUrl, string publisherName, X509Certificate2 certificate, TrustManagerPromptOptions options)
		{
			this.m_appName = appName;
			this.m_defaultBrowserExePath = defaultBrowserExePath;
			this.m_supportUrl = supportUrl;
			this.m_deploymentUrl = deploymentUrl;
			this.m_publisherName = publisherName;
			this.m_certificate = certificate;
			this.m_options = options;
			this.InitializeComponent();
			this.LoadResources();
			if (AccessibilityImprovements.Level2)
			{
				this.tableLayoutPanelOuter.AccessibleName = string.Empty;
				this.tableLayoutPanelQuestion.AccessibleName = string.Empty;
				this.lblQuestion.AccessibleName = this.lblQuestion.Text;
				this.pictureBoxQuestion.AccessibleName = SR.GetString("TrustManagerPromptUI_GlobeIcon");
				this.pictureBoxQuestion.AccessibleRole = AccessibleRole.Graphic;
				this.tableLayoutPanelInfo.AccessibleName = string.Empty;
				this.lblName.AccessibleName = SR.GetString("TrustManagerPromptUI_Name");
				this.linkLblName.AccessibleName = this.linkLblName.Text;
				this.lblFrom.AccessibleName = SR.GetString("TrustManagerPromptUI_From");
				this.linkLblFromUrl.AccessibleName = this.linkLblFromUrl.Text;
				this.lblPublisher.AccessibleName = SR.GetString("TrustManagerPromptUI_Publisher");
				this.linkLblPublisher.AccessibleName = this.linkLblPublisher.Text;
				this.tableLayoutPanelButtons.AccessibleName = string.Empty;
				this.btnInstall.AccessibleName = TrustManagerPromptUI.StripOutAccelerator(this.btnInstall.Text);
				this.btnCancel.AccessibleName = TrustManagerPromptUI.StripOutAccelerator(this.btnCancel.Text);
				this.warningTextTableLayoutPanel.AccessibleName = string.Empty;
				this.pictureBoxWarning.AccessibleName = this.pictureBoxWarning.AccessibleDescription;
				this.pictureBoxWarning.AccessibleRole = AccessibleRole.Graphic;
				this.linkLblMoreInformation.AccessibleName = this.linkLblMoreInformation.Text;
				this.lineLabel.AccessibleName = string.Empty;
				this.lineLabel.AccessibleRole = AccessibleRole.Separator;
				this.tableLayoutPanelOuter.Controls.SetChildIndex(this.tableLayoutPanelQuestion, 0);
				this.tableLayoutPanelOuter.Controls.SetChildIndex(this.tableLayoutPanelInfo, 1);
				this.tableLayoutPanelOuter.Controls.SetChildIndex(this.tableLayoutPanelButtons, 2);
				this.tableLayoutPanelOuter.Controls.SetChildIndex(this.warningTextTableLayoutPanel, 3);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		private void LoadGlobeBitmap()
		{
			Type typeFromHandle = typeof(Form);
			Bitmap bitmap;
			lock (typeFromHandle)
			{
				if (!LocalAppContextSwitches.UseLegacyImages)
				{
					Icon icon = new Icon(typeof(Form), "TrustManagerGlobe.ico");
					bitmap = icon.ToBitmap();
				}
				else
				{
					Bitmap bitmap2 = new Bitmap(typeof(Form), "TrustManagerGlobe.bmp");
					base.ScaleBitmapLogicalToDevice(ref bitmap2);
					bitmap = bitmap2;
				}
			}
			if (bitmap != null)
			{
				bitmap.MakeTransparent();
				this.pictureBoxQuestion.Image = bitmap;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000EC08 File Offset: 0x0000CE08
		private void LoadResources()
		{
			base.SuspendAllLayout(this);
			this.LoadGlobeBitmap();
			this.UpdateFonts();
			if ((this.m_options & TrustManagerPromptOptions.StopApp) != TrustManagerPromptOptions.None)
			{
				this.btnInstall.Visible = false;
				this.btnCancel.Text = SR.GetString("TrustManagerPromptUI_Close");
				this.btnCancel.DialogResult = DialogResult.OK;
			}
			else
			{
				if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
				{
					this.btnCancel.Text = SR.GetString("TrustManagerPromptUI_DoNotInstall");
				}
				else
				{
					this.btnCancel.Text = SR.GetString("TrustManagerPromptUI_DoNotRun");
				}
				this.btnInstall.DialogResult = DialogResult.OK;
				this.btnCancel.DialogResult = DialogResult.Cancel;
			}
			this.linkLblName.Links.Clear();
			this.linkLblPublisher.Links.Clear();
			this.linkLblFromUrl.Links.Clear();
			this.linkLblMoreInformation.Links.Clear();
			this.linkLblName.Text = this.m_appName;
			if (this.m_defaultBrowserExePath != null && this.m_certificate != null && this.m_supportUrl != null && this.m_supportUrl.Length > 0)
			{
				this.linkLblName.Links.Add(0, this.m_appName.Length, this.m_supportUrl);
			}
			if (this.linkLblName.Links.Count == 0)
			{
				this.lblName.Text = TrustManagerPromptUI.StripOutAccelerator(this.lblName.Text);
			}
			this.linkLblFromUrl.Text = this.m_deploymentUrl;
			if (this.m_publisherName == null)
			{
				this.linkLblPublisher.Text = SR.GetString("TrustManagerPromptUI_UnknownPublisher");
				if (this.m_certificate != null)
				{
					this.linkLblPublisher.Links.Add(0, this.linkLblPublisher.Text.Length);
				}
			}
			else
			{
				this.linkLblPublisher.Text = this.m_publisherName;
				if (this.m_publisherName.Length > 0)
				{
					this.linkLblPublisher.Links.Add(0, this.m_publisherName.Length);
				}
			}
			if (this.linkLblPublisher.Links.Count == 0)
			{
				this.lblPublisher.Text = TrustManagerPromptUI.StripOutAccelerator(this.lblPublisher.Text);
			}
			if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
			{
				this.Text = SR.GetString("TrustManagerPromptUI_InstallTitle");
			}
			else
			{
				this.Text = SR.GetString("TrustManagerPromptUI_RunTitle");
				this.btnInstall.Text = SR.GetString("TrustManagerPromptUI_Run");
			}
			if ((this.m_options & TrustManagerPromptOptions.StopApp) != TrustManagerPromptOptions.None)
			{
				this.lblQuestion.Text = SR.GetString("TrustManagerPromptUI_BlockedApp");
			}
			else if (this.m_publisherName == null)
			{
				if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
				{
					this.lblQuestion.Text = SR.GetString("TrustManagerPromptUI_NoPublisherInstallQuestion");
				}
				else
				{
					this.lblQuestion.Text = SR.GetString("TrustManagerPromptUI_NoPublisherRunQuestion");
				}
			}
			else if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
			{
				this.lblQuestion.Text = SR.GetString("TrustManagerPromptUI_InstallQuestion");
			}
			else
			{
				this.lblQuestion.Text = SR.GetString("TrustManagerPromptUI_RunQuestion");
			}
			if ((this.m_options & TrustManagerPromptOptions.StopApp) != TrustManagerPromptOptions.None)
			{
				if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
				{
					this.linkLblMoreInformation.Text = SR.GetString("TrustManagerPromptUI_InstalledAppBlockedWarning");
				}
				else
				{
					this.linkLblMoreInformation.Text = SR.GetString("TrustManagerPromptUI_RunAppBlockedWarning");
				}
				this.linkLblMoreInformation.TabStop = false;
				this.linkLblMoreInformation.AccessibleDescription = SR.GetString("TrustManagerPromptUI_WarningAccessibleDescription");
				this.linkLblMoreInformation.AccessibleName = SR.GetString("TrustManagerPromptUI_WarningAccessibleName");
			}
			else
			{
				string @string = SR.GetString("TrustManagerPromptUI_MoreInformation");
				if ((this.m_options & TrustManagerPromptOptions.LocalComputerSource) != TrustManagerPromptOptions.None)
				{
					if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
					{
						this.linkLblMoreInformation.Text = SR.GetString("TrustManagerPromptUI_InstallFromLocalMachineWarning", new object[] { @string });
					}
					else
					{
						this.linkLblMoreInformation.Text = SR.GetString("TrustManagerPromptUI_RunFromLocalMachineWarning", new object[] { @string });
					}
				}
				else if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
				{
					this.linkLblMoreInformation.Text = SR.GetString("TrustManagerPromptUI_InstallWarning", new object[] { @string });
				}
				else
				{
					this.linkLblMoreInformation.Text = SR.GetString("TrustManagerPromptUI_RunWarning", new object[] { @string });
				}
				this.linkLblMoreInformation.TabStop = true;
				this.linkLblMoreInformation.AccessibleDescription = SR.GetString("TrustManagerPromptUI_MoreInformationAccessibleDescription");
				this.linkLblMoreInformation.AccessibleName = SR.GetString("TrustManagerPromptUI_MoreInformationAccessibleName");
				this.linkLblMoreInformation.Links.Add(new LinkLabel.Link(this.linkLblMoreInformation.Text.Length - @string.Length, @string.Length));
			}
			if ((this.m_options & TrustManagerPromptOptions.StopApp) != TrustManagerPromptOptions.None || this.m_publisherName == null)
			{
				if ((this.m_options & TrustManagerPromptOptions.RequiresPermissions) == TrustManagerPromptOptions.None && (this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
				{
					this.LoadWarningBitmap(TrustManagerWarningLevel.Yellow);
				}
				else
				{
					this.LoadWarningBitmap(TrustManagerWarningLevel.Red);
				}
			}
			else if ((this.m_options & TrustManagerPromptOptions.RequiresPermissions) == TrustManagerPromptOptions.None)
			{
				this.LoadWarningBitmap(TrustManagerWarningLevel.Green);
			}
			else
			{
				this.LoadWarningBitmap(TrustManagerWarningLevel.Yellow);
			}
			if ((this.m_options & TrustManagerPromptOptions.StopApp) != TrustManagerPromptOptions.None)
			{
				if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
				{
					base.AccessibleDescription = SR.GetString("TrustManagerPromptUI_AccessibleDescription_InstallBlocked");
				}
				else
				{
					base.AccessibleDescription = SR.GetString("TrustManagerPromptUI_AccessibleDescription_RunBlocked");
				}
			}
			else if ((this.m_options & TrustManagerPromptOptions.RequiresPermissions) != TrustManagerPromptOptions.None)
			{
				if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
				{
					base.AccessibleDescription = SR.GetString("TrustManagerPromptUI_AccessibleDescription_InstallWithElevatedPermissions");
				}
				else
				{
					base.AccessibleDescription = SR.GetString("TrustManagerPromptUI_AccessibleDescription_RunWithElevatedPermissions");
				}
			}
			else if ((this.m_options & TrustManagerPromptOptions.AddsShortcut) != TrustManagerPromptOptions.None)
			{
				base.AccessibleDescription = SR.GetString("TrustManagerPromptUI_AccessibleDescription_InstallConfirmation");
			}
			else
			{
				base.AccessibleDescription = SR.GetString("TrustManagerPromptUI_AccessibleDescription_RunConfirmation");
			}
			base.ResumeAllLayout(this, true);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000F198 File Offset: 0x0000D398
		private void LoadWarningBitmap(TrustManagerWarningLevel warningLevel)
		{
			Bitmap bitmap;
			if (warningLevel != TrustManagerWarningLevel.Green)
			{
				if (warningLevel != TrustManagerWarningLevel.Yellow)
				{
					if (!LocalAppContextSwitches.UseLegacyImages)
					{
						Icon icon = new Icon(typeof(Form), "TrustManagerHighRisk.ico");
						bitmap = icon.ToBitmap();
					}
					else
					{
						bitmap = new Bitmap(typeof(Form), "TrustManagerHighRisk.bmp");
					}
					this.pictureBoxWarning.AccessibleDescription = string.Format(CultureInfo.CurrentCulture, SR.GetString("TrustManager_WarningIconAccessibleDescription_HighRisk"), new object[] { this.pictureBoxWarning.AccessibleDescription });
				}
				else
				{
					if (!LocalAppContextSwitches.UseLegacyImages)
					{
						Icon icon2 = new Icon(typeof(Form), "TrustManagerWarning.ico");
						bitmap = icon2.ToBitmap();
					}
					else
					{
						bitmap = new Bitmap(typeof(Form), "TrustManagerWarning.bmp");
					}
					this.pictureBoxWarning.AccessibleDescription = string.Format(CultureInfo.CurrentCulture, SR.GetString("TrustManager_WarningIconAccessibleDescription_MediumRisk"), new object[] { this.pictureBoxWarning.AccessibleDescription });
				}
			}
			else
			{
				if (!LocalAppContextSwitches.UseLegacyImages)
				{
					Icon icon3 = new Icon(typeof(Form), "TrustManagerOK.ico");
					bitmap = icon3.ToBitmap();
				}
				else
				{
					bitmap = new Bitmap(typeof(Form), "TrustManagerOK.bmp");
				}
				this.pictureBoxWarning.AccessibleDescription = string.Format(CultureInfo.CurrentCulture, SR.GetString("TrustManager_WarningIconAccessibleDescription_LowRisk"), new object[] { this.pictureBoxWarning.AccessibleDescription });
			}
			if (bitmap != null)
			{
				bitmap.MakeTransparent();
				this.pictureBoxWarning.Image = bitmap;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000F318 File Offset: 0x0000D518
		private static string StripOutAccelerator(string text)
		{
			int num = text.IndexOf('&');
			if (num == -1)
			{
				return text;
			}
			if (num > 0 && text[num - 1] == '(' && text.Length > num + 2 && text[num + 2] == ')')
			{
				return text.Remove(num - 1, 4);
			}
			return text.Replace("&", "");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000F378 File Offset: 0x0000D578
		private void TrustManagerPromptUI_Load(object sender, EventArgs e)
		{
			base.ActiveControl = this.btnCancel;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000F388 File Offset: 0x0000D588
		private void linkLblFromUrl_MouseEnter(object sender, EventArgs e)
		{
			if (!this.controlToolTip && this.toolTipFromUrl != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.controlToolTip = true;
					this.toolTipFromUrl.Show(this.linkLblFromUrl.Text, this.linkLblFromUrl);
					UnsafeNativeMethods.SendMessage(new HandleRef(this.toolTipFromUrl, this.toolTipFromUrl.Handle), 1048, 0, 600);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
					this.controlToolTip = false;
				}
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000F418 File Offset: 0x0000D618
		private void linkLblFromUrl_MouseLeave(object sender, EventArgs e)
		{
			if (!this.controlToolTip && this.toolTipFromUrl != null && this.toolTipFromUrl.GetHandleCreated())
			{
				this.toolTipFromUrl.RemoveAll();
				IntSecurity.AllWindows.Assert();
				try
				{
					this.toolTipFromUrl.Hide(this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000F47C File Offset: 0x0000D67C
		private void TrustManagerPromptUI_ShowMoreInformation(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				using (TrustManagerMoreInformation trustManagerMoreInformation = new TrustManagerMoreInformation(this.m_options, this.m_publisherName))
				{
					trustManagerMoreInformation.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		private void TrustManagerPromptUI_ShowPublisherCertificate(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				X509Certificate2UI.DisplayCertificate(this.m_certificate, base.Handle);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000F504 File Offset: 0x0000D704
		private void TrustManagerPromptUI_ShowSupportPage(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start(this.m_defaultBrowserExePath, e.Link.LinkData.ToString());
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000F544 File Offset: 0x0000D744
		private void TrustManagerPromptUI_VisibleChanged(object sender, EventArgs e)
		{
			if (base.Visible && Form.ActiveForm != this)
			{
				base.Activate();
				base.ActiveControl = this.btnCancel;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000F568 File Offset: 0x0000D768
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000F582 File Offset: 0x0000D782
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000F59C File Offset: 0x0000D79C
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Window)
			{
				this.UpdateFonts();
			}
			base.Invalidate();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		private void UpdateFonts()
		{
			this.Font = SystemFonts.MessageBoxFont;
			this.lblQuestion.Font = (this.lblPublisher.Font = (this.lblFrom.Font = (this.lblName.Font = new Font(this.Font, FontStyle.Bold))));
			Control control = this.linkLblPublisher;
			Control control2 = this.linkLblFromUrl;
			Control control3 = this.linkLblName;
			Size size = new Size(0, this.Font.Height + 2);
			control3.MaximumSize = size;
			control.MaximumSize = (control2.MaximumSize = size);
		}

		// Token: 0x04000469 RID: 1129
		private string m_appName;

		// Token: 0x0400046A RID: 1130
		private string m_defaultBrowserExePath;

		// Token: 0x0400046B RID: 1131
		private string m_supportUrl;

		// Token: 0x0400046C RID: 1132
		private string m_deploymentUrl;

		// Token: 0x0400046D RID: 1133
		private string m_publisherName;

		// Token: 0x0400046E RID: 1134
		private X509Certificate2 m_certificate;

		// Token: 0x0400046F RID: 1135
		private TrustManagerPromptOptions m_options;
	}
}
