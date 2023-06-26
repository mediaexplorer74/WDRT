using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;

namespace System.Security.Policy
{
	// Token: 0x02000100 RID: 256
	internal class TrustManagerPromptUIThread
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public TrustManagerPromptUIThread(string appName, string defaultBrowserExePath, string supportUrl, string deploymentUrl, string publisherName, X509Certificate2 certificate, TrustManagerPromptOptions options)
		{
			this.m_appName = appName;
			this.m_defaultBrowserExePath = defaultBrowserExePath;
			this.m_supportUrl = supportUrl;
			this.m_deploymentUrl = deploymentUrl;
			this.m_publisherName = publisherName;
			this.m_certificate = certificate;
			this.m_options = options;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public DialogResult ShowDialog()
		{
			Thread thread = new Thread(new ThreadStart(this.ShowDialogWork));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			return this.m_ret;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		private void ShowDialogWork()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				using (TrustManagerPromptUI trustManagerPromptUI = new TrustManagerPromptUI(this.m_appName, this.m_defaultBrowserExePath, this.m_supportUrl, this.m_deploymentUrl, this.m_publisherName, this.m_certificate, this.m_options))
				{
					this.m_ret = trustManagerPromptUI.ShowDialog();
				}
			}
			catch
			{
			}
			finally
			{
				Application.ExitThread();
			}
		}

		// Token: 0x04000435 RID: 1077
		private string m_appName;

		// Token: 0x04000436 RID: 1078
		private string m_defaultBrowserExePath;

		// Token: 0x04000437 RID: 1079
		private string m_supportUrl;

		// Token: 0x04000438 RID: 1080
		private string m_deploymentUrl;

		// Token: 0x04000439 RID: 1081
		private string m_publisherName;

		// Token: 0x0400043A RID: 1082
		private X509Certificate2 m_certificate;

		// Token: 0x0400043B RID: 1083
		private TrustManagerPromptOptions m_options;

		// Token: 0x0400043C RID: 1084
		private DialogResult m_ret = DialogResult.No;
	}
}
