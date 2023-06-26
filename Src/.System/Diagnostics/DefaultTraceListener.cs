using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Provides the default output methods and behavior for tracing.</summary>
	// Token: 0x02000496 RID: 1174
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DefaultTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DefaultTraceListener" /> class with "Default" as its <see cref="P:System.Diagnostics.TraceListener.Name" /> property value.</summary>
		// Token: 0x06002B80 RID: 11136 RVA: 0x000C4EF9 File Offset: 0x000C30F9
		public DefaultTraceListener()
			: base("Default")
		{
		}

		/// <summary>Gets or sets a value indicating whether the application is running in user-interface mode.</summary>
		/// <returns>
		///   <see langword="true" /> if user-interface mode is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x000C4F06 File Offset: 0x000C3106
		// (set) Token: 0x06002B82 RID: 11138 RVA: 0x000C4F1C File Offset: 0x000C311C
		public bool AssertUiEnabled
		{
			get
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				return this.assertUIEnabled;
			}
			set
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				this.assertUIEnabled = value;
			}
		}

		/// <summary>Gets or sets the name of a log file to write trace or debug messages to.</summary>
		/// <returns>The name of a log file to write trace or debug messages to.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000C4F33 File Offset: 0x000C3133
		// (set) Token: 0x06002B84 RID: 11140 RVA: 0x000C4F49 File Offset: 0x000C3149
		public string LogFileName
		{
			get
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				return this.logFileName;
			}
			set
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				this.logFileName = value;
			}
		}

		/// <summary>Emits or displays a message and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display.</param>
		// Token: 0x06002B85 RID: 11141 RVA: 0x000C4F60 File Offset: 0x000C3160
		public override void Fail(string message)
		{
			this.Fail(message, null);
		}

		/// <summary>Emits or displays detailed messages and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display.</param>
		/// <param name="detailMessage">The detailed message to emit or display.</param>
		// Token: 0x06002B86 RID: 11142 RVA: 0x000C4F6C File Offset: 0x000C316C
		public override void Fail(string message, string detailMessage)
		{
			StackTrace stackTrace = new StackTrace(true);
			int num = 0;
			bool uiPermission = DefaultTraceListener.UiPermission;
			string text;
			try
			{
				text = stackTrace.ToString();
			}
			catch
			{
				text = "";
			}
			this.WriteAssert(text, message, detailMessage);
			if (this.AssertUiEnabled && uiPermission)
			{
				AssertWrapper.ShowAssert(text, stackTrace.GetFrame(num), message, detailMessage);
			}
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000C4FCC File Offset: 0x000C31CC
		private void InitializeSettings()
		{
			this.assertUIEnabled = DiagnosticsConfiguration.AssertUIEnabled;
			this.logFileName = DiagnosticsConfiguration.LogFileName;
			this.settingsInitialized = true;
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000C4FEC File Offset: 0x000C31EC
		private void WriteAssert(string stackTrace, string message, string detailMessage)
		{
			string text = string.Concat(new string[]
			{
				SR.GetString("DebugAssertBanner"),
				Environment.NewLine,
				SR.GetString("DebugAssertShortMessage"),
				Environment.NewLine,
				message,
				Environment.NewLine,
				SR.GetString("DebugAssertLongMessage"),
				Environment.NewLine,
				detailMessage,
				Environment.NewLine,
				stackTrace
			});
			this.WriteLine(text);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000C506C File Offset: 0x000C326C
		private void WriteToLogFile(string message, bool useWriteLine)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(this.LogFileName);
				using (Stream stream = fileInfo.Open(FileMode.OpenOrCreate))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						stream.Position = stream.Length;
						if (useWriteLine)
						{
							streamWriter.WriteLine(message);
						}
						else
						{
							streamWriter.Write(message);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.WriteLine(SR.GetString("ExceptionOccurred", new object[]
				{
					this.LogFileName,
					ex.ToString()
				}), false);
			}
		}

		/// <summary>Writes the output to the <see langword="OutputDebugString" /> function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method.</summary>
		/// <param name="message">The message to write to <see langword="OutputDebugString" /> and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />.</param>
		// Token: 0x06002B8A RID: 11146 RVA: 0x000C5124 File Offset: 0x000C3324
		public override void Write(string message)
		{
			this.Write(message, true);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000C5130 File Offset: 0x000C3330
		private void Write(string message, bool useLogFile)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			if (message == null || message.Length <= 16384)
			{
				this.internalWrite(message);
			}
			else
			{
				int i;
				for (i = 0; i < message.Length - 16384; i += 16384)
				{
					this.internalWrite(message.Substring(i, 16384));
				}
				this.internalWrite(message.Substring(i));
			}
			if (useLogFile && this.LogFileName.Length != 0)
			{
				this.WriteToLogFile(message, false);
			}
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000C51B6 File Offset: 0x000C33B6
		private void internalWrite(string message)
		{
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
				return;
			}
			if (message == null)
			{
				SafeNativeMethods.OutputDebugString(string.Empty);
				return;
			}
			SafeNativeMethods.OutputDebugString(message);
		}

		/// <summary>Writes the output to the <see langword="OutputDebugString" /> function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method, followed by a carriage return and line feed (\r\n).</summary>
		/// <param name="message">The message to write to <see langword="OutputDebugString" /> and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />.</param>
		// Token: 0x06002B8D RID: 11149 RVA: 0x000C51DC File Offset: 0x000C33DC
		public override void WriteLine(string message)
		{
			this.WriteLine(message, true);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000C51E6 File Offset: 0x000C33E6
		private void WriteLine(string message, bool useLogFile)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			this.Write(message + Environment.NewLine, useLogFile);
			base.NeedIndent = true;
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000C5210 File Offset: 0x000C3410
		private static bool UiPermission
		{
			get
			{
				bool flag = false;
				try
				{
					new UIPermission(UIPermissionWindow.SafeSubWindows).Demand();
					flag = true;
				}
				catch
				{
				}
				return flag;
			}
		}

		// Token: 0x04002671 RID: 9841
		private bool assertUIEnabled;

		// Token: 0x04002672 RID: 9842
		private string logFileName;

		// Token: 0x04002673 RID: 9843
		private bool settingsInitialized;

		// Token: 0x04002674 RID: 9844
		private const int internalWriteSize = 16384;
	}
}
