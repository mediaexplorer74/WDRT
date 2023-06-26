using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Hosting
{
	// Token: 0x02000A53 RID: 2643
	internal sealed class ManifestRunner
	{
		// Token: 0x060066D5 RID: 26325 RVA: 0x0015B44C File Offset: 0x0015964C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		internal ManifestRunner(AppDomain domain, ActivationContext activationContext)
		{
			this.m_domain = domain;
			string text;
			string text2;
			CmsUtils.GetEntryPoint(activationContext, out text, out text2);
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoMain"));
			}
			if (string.IsNullOrEmpty(text2))
			{
				this.m_args = new string[0];
			}
			else
			{
				this.m_args = text2.Split(new char[] { ' ' });
			}
			this.m_apt = ApartmentState.Unknown;
			string applicationDirectory = activationContext.ApplicationDirectory;
			this.m_path = Path.Combine(applicationDirectory, text);
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x0015B4D0 File Offset: 0x001596D0
		internal RuntimeAssembly EntryAssembly
		{
			[SecurityCritical]
			[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				if (this.m_assembly == null)
				{
					this.m_assembly = (RuntimeAssembly)Assembly.LoadFrom(this.m_path);
				}
				return this.m_assembly;
			}
		}

		// Token: 0x060066D7 RID: 26327 RVA: 0x0015B4FC File Offset: 0x001596FC
		[SecurityCritical]
		private void NewThreadRunner()
		{
			this.m_runResult = this.Run(false);
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x0015B50C File Offset: 0x0015970C
		[SecurityCritical]
		private int RunInNewThread()
		{
			Thread thread = new Thread(new ThreadStart(this.NewThreadRunner));
			thread.SetApartmentState(this.m_apt);
			thread.Start();
			thread.Join();
			return this.m_runResult;
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x0015B54C File Offset: 0x0015974C
		[SecurityCritical]
		private int Run(bool checkAptModel)
		{
			if (checkAptModel && this.m_apt != ApartmentState.Unknown)
			{
				if (Thread.CurrentThread.GetApartmentState() != ApartmentState.Unknown && Thread.CurrentThread.GetApartmentState() != this.m_apt)
				{
					return this.RunInNewThread();
				}
				Thread.CurrentThread.SetApartmentState(this.m_apt);
			}
			return this.m_domain.nExecuteAssembly(this.EntryAssembly, this.m_args);
		}

		// Token: 0x060066DA RID: 26330 RVA: 0x0015B5B4 File Offset: 0x001597B4
		[SecurityCritical]
		internal int ExecuteAsAssembly()
		{
			object[] array = this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof(STAThreadAttribute), false);
			if (array.Length != 0)
			{
				this.m_apt = ApartmentState.STA;
			}
			array = this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof(MTAThreadAttribute), false);
			if (array.Length != 0)
			{
				if (this.m_apt == ApartmentState.Unknown)
				{
					this.m_apt = ApartmentState.MTA;
				}
				else
				{
					this.m_apt = ApartmentState.Unknown;
				}
			}
			return this.Run(true);
		}

		// Token: 0x04002E19 RID: 11801
		private AppDomain m_domain;

		// Token: 0x04002E1A RID: 11802
		private string m_path;

		// Token: 0x04002E1B RID: 11803
		private string[] m_args;

		// Token: 0x04002E1C RID: 11804
		private ApartmentState m_apt;

		// Token: 0x04002E1D RID: 11805
		private RuntimeAssembly m_assembly;

		// Token: 0x04002E1E RID: 11806
		private int m_runResult;
	}
}
