using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Specifies a set of values that are used when you start a process.</summary>
	// Token: 0x020004FE RID: 1278
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, SelfAffectingProcessMgmt = true)]
	public sealed class ProcessStartInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class without specifying a file name with which to start the process.</summary>
		// Token: 0x06003059 RID: 12377 RVA: 0x000DB217 File Offset: 0x000D9417
		public ProcessStartInfo()
		{
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000DB226 File Offset: 0x000D9426
		internal ProcessStartInfo(Process parent)
		{
			this.weakParentProcess = new WeakReference(parent);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class and specifies a file name such as an application or document with which to start the process.</summary>
		/// <param name="fileName">An application or document with which to start a process.</param>
		// Token: 0x0600305B RID: 12379 RVA: 0x000DB241 File Offset: 0x000D9441
		public ProcessStartInfo(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class, specifies an application file name with which to start the process, and specifies a set of command-line arguments to pass to the application.</summary>
		/// <param name="fileName">An application with which to start a process.</param>
		/// <param name="arguments">Command-line arguments to pass to the application when the process starts.</param>
		// Token: 0x0600305C RID: 12380 RVA: 0x000DB257 File Offset: 0x000D9457
		public ProcessStartInfo(string fileName, string arguments)
		{
			this.fileName = fileName;
			this.arguments = arguments;
		}

		/// <summary>Gets or sets the verb to use when opening the application or document specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The action to take with the file that the process opens. The default is an empty string (""), which signifies no action.</returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000DB274 File Offset: 0x000D9474
		// (set) Token: 0x0600305E RID: 12382 RVA: 0x000DB28A File Offset: 0x000D948A
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.VerbConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("ProcessVerb")]
		[NotifyParentProperty(true)]
		public string Verb
		{
			get
			{
				if (this.verb == null)
				{
					return string.Empty;
				}
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		/// <summary>Gets or sets the set of command-line arguments to use when starting the application.</summary>
		/// <returns>A single string containing the arguments to pass to the target application specified in the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property. The default is an empty string (""). On Windows Vista and earlier versions of the Windows operating system, the length of the arguments added to the length of the full path to the process must be less than 2080. On Windows 7 and later versions, the length must be less than 32699.  
		///  Arguments are parsed and interpreted by the target application, so must align with the expectations of that application. For.NET applications as demonstrated in the Examples below, spaces are interpreted as a separator between multiple arguments. A single argument that includes spaces must be surrounded by quotation marks, but those quotation marks are not carried through to the target application. In include quotation marks in the final parsed argument, triple-escape each mark.</returns>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000DB293 File Offset: 0x000D9493
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x000DB2A9 File Offset: 0x000D94A9
		[DefaultValue("")]
		[MonitoringDescription("ProcessArguments")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string Arguments
		{
			get
			{
				if (this.arguments == null)
				{
					return string.Empty;
				}
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to start the process in a new window.</summary>
		/// <returns>
		///   <see langword="true" /> if the process should be started without creating a new window to contain it; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x000DB2B2 File Offset: 0x000D94B2
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x000DB2BA File Offset: 0x000D94BA
		[DefaultValue(false)]
		[MonitoringDescription("ProcessCreateNoWindow")]
		[NotifyParentProperty(true)]
		public bool CreateNoWindow
		{
			get
			{
				return this.createNoWindow;
			}
			set
			{
				this.createNoWindow = value;
			}
		}

		/// <summary>Gets search paths for files, directories for temporary files, application-specific options, and other similar information.</summary>
		/// <returns>A string dictionary that provides environment variables that apply to this process and child processes. The default is <see langword="null" />.</returns>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000DB2C4 File Offset: 0x000D94C4
		[Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[MonitoringDescription("ProcessEnvironmentVariables")]
		[NotifyParentProperty(true)]
		public StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.environmentVariables == null)
				{
					this.environmentVariables = new StringDictionaryWithComparer();
					if (this.weakParentProcess == null || !this.weakParentProcess.IsAlive || ((Component)this.weakParentProcess.Target).Site == null || !((Component)this.weakParentProcess.Target).Site.DesignMode)
					{
						foreach (object obj in System.Environment.GetEnvironmentVariables())
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.environmentVariables.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
					}
				}
				return this.environmentVariables;
			}
		}

		/// <summary>Gets the environment variables that apply to this process and its child processes.</summary>
		/// <returns>A generic dictionary containing the environment variables that apply to this process and its child processes. The default is <see langword="null" />.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000DB39C File Offset: 0x000D959C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public IDictionary<string, string> Environment
		{
			get
			{
				if (this.environment == null)
				{
					this.environment = this.EnvironmentVariables.AsGenericDictionary();
				}
				return this.environment;
			}
		}

		/// <summary>Gets or sets a value indicating whether the input for an application is read from the <see cref="P:System.Diagnostics.Process.StandardInput" /> stream.</summary>
		/// <returns>
		///   <see langword="true" /> if input should be read from <see cref="P:System.Diagnostics.Process.StandardInput" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000DB3BD File Offset: 0x000D95BD
		// (set) Token: 0x06003066 RID: 12390 RVA: 0x000DB3C5 File Offset: 0x000D95C5
		[DefaultValue(false)]
		[MonitoringDescription("ProcessRedirectStandardInput")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirectStandardInput;
			}
			set
			{
				this.redirectStandardInput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the textual output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <returns>
		///   <see langword="true" /> if output should be written to <see cref="P:System.Diagnostics.Process.StandardOutput" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x000DB3CE File Offset: 0x000D95CE
		// (set) Token: 0x06003068 RID: 12392 RVA: 0x000DB3D6 File Offset: 0x000D95D6
		[DefaultValue(false)]
		[MonitoringDescription("ProcessRedirectStandardOutput")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirectStandardOutput;
			}
			set
			{
				this.redirectStandardOutput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <returns>
		///   <see langword="true" /> if error output should be written to <see cref="P:System.Diagnostics.Process.StandardError" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000DB3DF File Offset: 0x000D95DF
		// (set) Token: 0x0600306A RID: 12394 RVA: 0x000DB3E7 File Offset: 0x000D95E7
		[DefaultValue(false)]
		[MonitoringDescription("ProcessRedirectStandardError")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirectStandardError;
			}
			set
			{
				this.redirectStandardError = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for error output.</summary>
		/// <returns>An object that represents the preferred encoding for error output. The default is <see langword="null" />.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000DB3F0 File Offset: 0x000D95F0
		// (set) Token: 0x0600306C RID: 12396 RVA: 0x000DB3F8 File Offset: 0x000D95F8
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.standardErrorEncoding;
			}
			set
			{
				this.standardErrorEncoding = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for standard output.</summary>
		/// <returns>An object that represents the preferred encoding for standard output. The default is <see langword="null" />.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000DB401 File Offset: 0x000D9601
		// (set) Token: 0x0600306E RID: 12398 RVA: 0x000DB409 File Offset: 0x000D9609
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.standardOutputEncoding;
			}
			set
			{
				this.standardOutputEncoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the operating system shell to start the process.</summary>
		/// <returns>
		///   <see langword="true" /> if the shell should be used when starting the process; <see langword="false" /> if the process should be created directly from the executable file. The default is <see langword="true" /> on .NET Framework apps and <see langword="false" /> on .NET Core apps.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to set the value to <see langword="true" /> on Universal Windows Platform (UWP) apps occurs.</exception>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x000DB412 File Offset: 0x000D9612
		// (set) Token: 0x06003070 RID: 12400 RVA: 0x000DB41A File Offset: 0x000D961A
		[DefaultValue(true)]
		[MonitoringDescription("ProcessUseShellExecute")]
		[NotifyParentProperty(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.useShellExecute;
			}
			set
			{
				this.useShellExecute = value;
			}
		}

		/// <summary>Gets the set of verbs associated with the type of file specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The actions that the system can apply to the file indicated by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000DB424 File Offset: 0x000D9624
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] Verbs
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				RegistryKey registryKey = null;
				string extension = Path.GetExtension(this.FileName);
				try
				{
					if (extension != null && extension.Length > 0)
					{
						registryKey = Registry.ClassesRoot.OpenSubKey(extension);
						if (registryKey != null)
						{
							string text = (string)registryKey.GetValue(string.Empty);
							registryKey.Close();
							registryKey = Registry.ClassesRoot.OpenSubKey(text + "\\shell");
							if (registryKey != null)
							{
								string[] subKeyNames = registryKey.GetSubKeyNames();
								for (int i = 0; i < subKeyNames.Length; i++)
								{
									if (string.Compare(subKeyNames[i], "new", StringComparison.OrdinalIgnoreCase) != 0)
									{
										arrayList.Add(subKeyNames[i]);
									}
								}
								registryKey.Close();
								registryKey = null;
							}
						}
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
				string[] array = new string[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}
		}

		/// <summary>Gets or sets the user name to use when starting the process. If you use the UPN format, <paramref name="user" />@<paramref name="DNS_domain_name" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.Domain" /> property must be <see langword="null" />.</summary>
		/// <returns>The user name to use when starting the process. If you use the UPN format, <paramref name="user" />@<paramref name="DNS_domain_name" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.Domain" /> property must be <see langword="null" />.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000DB50C File Offset: 0x000D970C
		// (set) Token: 0x06003073 RID: 12403 RVA: 0x000DB522 File Offset: 0x000D9722
		[NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				if (this.userName == null)
				{
					return string.Empty;
				}
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		/// <summary>Gets or sets a secure string that contains the user password to use when starting the process.</summary>
		/// <returns>The user password to use when starting the process.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000DB52B File Offset: 0x000D972B
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x000DB533 File Offset: 0x000D9733
		public SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		/// <summary>Gets or sets the user password in clear text to use when starting the process.</summary>
		/// <returns>The user password in clear text.</returns>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000DB53C File Offset: 0x000D973C
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x000DB544 File Offset: 0x000D9744
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string PasswordInClearText
		{
			get
			{
				return this.passwordInClearText;
			}
			set
			{
				this.passwordInClearText = value;
			}
		}

		/// <summary>Gets or sets a value that identifies the domain to use when starting the process. If this value is <see langword="null" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property must be specified in UPN format.</summary>
		/// <returns>The Active Directory domain to use when starting the process. If this value is <see langword="null" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property must be specified in UPN format.</returns>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000DB54D File Offset: 0x000D974D
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x000DB563 File Offset: 0x000D9763
		[NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				if (this.domain == null)
				{
					return string.Empty;
				}
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the Windows user profile is to be loaded from the registry.</summary>
		/// <returns>
		///   <see langword="true" /> if the Windows user profile should be loaded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000DB56C File Offset: 0x000D976C
		// (set) Token: 0x0600307B RID: 12411 RVA: 0x000DB574 File Offset: 0x000D9774
		[NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.loadUserProfile;
			}
			set
			{
				this.loadUserProfile = value;
			}
		}

		/// <summary>Gets or sets the application or document to start.</summary>
		/// <returns>The name of the application to start, or the name of a document of a file type that is associated with an application and that has a default open action available to it. The default is an empty string ("").</returns>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000DB57D File Offset: 0x000D977D
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x000DB593 File Offset: 0x000D9793
		[DefaultValue("")]
		[Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("ProcessFileName")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string FileName
		{
			get
			{
				if (this.fileName == null)
				{
					return string.Empty;
				}
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is <see langword="false" />, gets or sets the working directory for the process to be started. When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is <see langword="true" />, gets or sets the directory that contains the process to be started.</summary>
		/// <returns>When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is <see langword="true" />, the fully qualified name of the directory that contains the process to be started. When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is <see langword="false" />, the working directory for the process to be started. The default is an empty string ("").</returns>
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000DB59C File Offset: 0x000D979C
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x000DB5B2 File Offset: 0x000D97B2
		[DefaultValue("")]
		[MonitoringDescription("ProcessWorkingDirectory")]
		[Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string WorkingDirectory
		{
			get
			{
				if (this.directory == null)
				{
					return string.Empty;
				}
				return this.directory;
			}
			set
			{
				this.directory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an error dialog box is displayed to the user if the process cannot be started.</summary>
		/// <returns>
		///   <see langword="true" /> if an error dialog box should be displayed on the screen if the process cannot be started; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000DB5BB File Offset: 0x000D97BB
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000DB5C3 File Offset: 0x000D97C3
		[DefaultValue(false)]
		[MonitoringDescription("ProcessErrorDialog")]
		[NotifyParentProperty(true)]
		public bool ErrorDialog
		{
			get
			{
				return this.errorDialog;
			}
			set
			{
				this.errorDialog = value;
			}
		}

		/// <summary>Gets or sets the window handle to use when an error dialog box is shown for a process that cannot be started.</summary>
		/// <returns>A pointer to the handle of the error dialog box that results from a process start failure.</returns>
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000DB5CC File Offset: 0x000D97CC
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x000DB5D4 File Offset: 0x000D97D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr ErrorDialogParentHandle
		{
			get
			{
				return this.errorDialogParentHandle;
			}
			set
			{
				this.errorDialogParentHandle = value;
			}
		}

		/// <summary>Gets or sets the window state to use when the process is started.</summary>
		/// <returns>One of the enumeration values that indicates whether the process is started in a window that is maximized, minimized, normal (neither maximized nor minimized), or not visible. The default is <see langword="Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The window style is not one of the <see cref="T:System.Diagnostics.ProcessWindowStyle" /> enumeration members.</exception>
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000DB5DD File Offset: 0x000D97DD
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x000DB5E5 File Offset: 0x000D97E5
		[DefaultValue(ProcessWindowStyle.Normal)]
		[MonitoringDescription("ProcessWindowStyle")]
		[NotifyParentProperty(true)]
		public ProcessWindowStyle WindowStyle
		{
			get
			{
				return this.windowStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessWindowStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessWindowStyle));
				}
				this.windowStyle = value;
			}
		}

		// Token: 0x04002884 RID: 10372
		private string fileName;

		// Token: 0x04002885 RID: 10373
		private string arguments;

		// Token: 0x04002886 RID: 10374
		private string directory;

		// Token: 0x04002887 RID: 10375
		private string verb;

		// Token: 0x04002888 RID: 10376
		private ProcessWindowStyle windowStyle;

		// Token: 0x04002889 RID: 10377
		private bool errorDialog;

		// Token: 0x0400288A RID: 10378
		private IntPtr errorDialogParentHandle;

		// Token: 0x0400288B RID: 10379
		private bool useShellExecute = true;

		// Token: 0x0400288C RID: 10380
		private string userName;

		// Token: 0x0400288D RID: 10381
		private string domain;

		// Token: 0x0400288E RID: 10382
		private SecureString password;

		// Token: 0x0400288F RID: 10383
		private string passwordInClearText;

		// Token: 0x04002890 RID: 10384
		private bool loadUserProfile;

		// Token: 0x04002891 RID: 10385
		private bool redirectStandardInput;

		// Token: 0x04002892 RID: 10386
		private bool redirectStandardOutput;

		// Token: 0x04002893 RID: 10387
		private bool redirectStandardError;

		// Token: 0x04002894 RID: 10388
		private Encoding standardOutputEncoding;

		// Token: 0x04002895 RID: 10389
		private Encoding standardErrorEncoding;

		// Token: 0x04002896 RID: 10390
		private bool createNoWindow;

		// Token: 0x04002897 RID: 10391
		private WeakReference weakParentProcess;

		// Token: 0x04002898 RID: 10392
		internal StringDictionary environmentVariables;

		// Token: 0x04002899 RID: 10393
		private IDictionary<string, string> environment;
	}
}
