using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Deployment.Internal.Isolation;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Provides <see langword="static" /> methods and properties to manage an application, such as methods to start and stop an application, to process Windows messages, and properties to get information about an application. This class cannot be inherited.</summary>
	// Token: 0x02000120 RID: 288
	public sealed class Application
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x00002843 File Offset: 0x00000A43
		private Application()
		{
		}

		/// <summary>Gets a value indicating whether the caller can quit this application.</summary>
		/// <returns>
		///   <see langword="true" /> if the caller can quit this application; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001820D File Offset: 0x0001640D
		public static bool AllowQuit
		{
			get
			{
				return Application.ThreadContext.FromCurrent().GetAllowQuit();
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00018219 File Offset: 0x00016419
		internal static bool CanContinueIdle
		{
			get
			{
				return Application.ThreadContext.FromCurrent().ComponentManager.FContinueIdle();
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0001822A File Offset: 0x0001642A
		internal static bool ComCtlSupportsVisualStyles
		{
			get
			{
				if (!Application.comCtlSupportsVisualStylesInitialized)
				{
					Application.comCtlSupportsVisualStyles = Application.InitializeComCtlSupportsVisualStyles();
					Application.comCtlSupportsVisualStylesInitialized = true;
				}
				return Application.comCtlSupportsVisualStyles;
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00018248 File Offset: 0x00016448
		private static bool InitializeComCtlSupportsVisualStyles()
		{
			if (Application.useVisualStyles && OSFeature.Feature.IsPresent(OSFeature.Themes))
			{
				return true;
			}
			IntPtr intPtr = UnsafeNativeMethods.GetModuleHandle("comctl32.dll");
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, intPtr), "ImageList_WriteEx");
					return procAddress != IntPtr.Zero;
				}
				catch
				{
					return false;
				}
			}
			intPtr = UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("comctl32.dll");
			if (intPtr != IntPtr.Zero)
			{
				IntPtr procAddress2 = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, intPtr), "ImageList_WriteEx");
				return procAddress2 != IntPtr.Zero;
			}
			return false;
		}

		/// <summary>Gets the registry key for the application data that is shared among all users.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.RegistryKey" /> representing the registry key of the application data that is shared among all users.</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x000182F4 File Offset: 0x000164F4
		public static RegistryKey CommonAppDataRegistry
		{
			get
			{
				return Registry.LocalMachine.CreateSubKey(Application.CommonAppDataRegistryKeyName);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00018308 File Offset: 0x00016508
		internal static string CommonAppDataRegistryKeyName
		{
			get
			{
				string text = "Software\\{0}\\{1}\\{2}";
				return string.Format(CultureInfo.CurrentCulture, text, new object[]
				{
					Application.CompanyName,
					Application.ProductName,
					Application.ProductVersion
				});
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00018344 File Offset: 0x00016544
		internal static bool UseEverettThreadAffinity
		{
			get
			{
				if (!Application.checkedThreadAffinity)
				{
					Application.checkedThreadAffinity = true;
					try
					{
						new RegistryPermission(PermissionState.Unrestricted).Assert();
						RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Application.CommonAppDataRegistryKeyName);
						if (registryKey != null)
						{
							object value = registryKey.GetValue("EnableSystemEventsThreadAffinityCompatibility");
							registryKey.Close();
							if (value != null && (int)value != 0)
							{
								Application.useEverettThreadAffinity = true;
							}
						}
					}
					catch (SecurityException)
					{
					}
					catch (InvalidCastException)
					{
					}
				}
				return Application.useEverettThreadAffinity;
			}
		}

		/// <summary>Gets the path for the application data that is shared among all users.</summary>
		/// <returns>The path for the application data that is shared among all users.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000183C8 File Offset: 0x000165C8
		public static string CommonAppDataPath
		{
			get
			{
				try
				{
					if (ApplicationDeployment.IsNetworkDeployed)
					{
						string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
						if (text != null)
						{
							return text;
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return Application.GetDataPath(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
			}
		}

		/// <summary>Gets the company name associated with the application.</summary>
		/// <returns>The company name.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00018428 File Offset: 0x00016628
		public static string CompanyName
		{
			get
			{
				object obj = Application.internalSyncObject;
				lock (obj)
				{
					if (Application.companyName == null)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly != null)
						{
							object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
							if (customAttributes != null && customAttributes.Length != 0)
							{
								Application.companyName = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
							}
						}
						if (Application.companyName == null || Application.companyName.Length == 0)
						{
							Application.companyName = Application.GetAppFileVersionInfo().CompanyName;
							if (Application.companyName != null)
							{
								Application.companyName = Application.companyName.Trim();
							}
						}
						if (Application.companyName == null || Application.companyName.Length == 0)
						{
							Type appMainType = Application.GetAppMainType();
							if (appMainType != null)
							{
								string @namespace = appMainType.Namespace;
								if (!string.IsNullOrEmpty(@namespace))
								{
									int num = @namespace.IndexOf(".");
									if (num != -1)
									{
										Application.companyName = @namespace.Substring(0, num);
									}
									else
									{
										Application.companyName = @namespace;
									}
								}
								else
								{
									Application.companyName = Application.ProductName;
								}
							}
						}
					}
				}
				return Application.companyName;
			}
		}

		/// <summary>Gets or sets the culture information for the current thread.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture information for the current thread.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00018550 File Offset: 0x00016750
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001855C File Offset: 0x0001675C
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		/// <summary>Gets or sets the current input language for the current thread.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> representing the current input language for the current thread.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00018569 File Offset: 0x00016769
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00018570 File Offset: 0x00016770
		public static InputLanguage CurrentInputLanguage
		{
			get
			{
				return InputLanguage.CurrentInputLanguage;
			}
			set
			{
				IntSecurity.AffectThreadBehavior.Demand();
				InputLanguage.CurrentInputLanguage = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00018582 File Offset: 0x00016782
		internal static bool CustomThreadExceptionHandlerAttached
		{
			get
			{
				return Application.ThreadContext.FromCurrent().CustomThreadExceptionHandlerAttached;
			}
		}

		/// <summary>Gets the path for the executable file that started the application, including the executable name.</summary>
		/// <returns>The path and executable name for the executable file that started the application.  
		///  This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00018590 File Offset: 0x00016790
		public static string ExecutablePath
		{
			get
			{
				if (Application.executablePath == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					if (entryAssembly == null)
					{
						StringBuilder moduleFileNameLongPath = UnsafeNativeMethods.GetModuleFileNameLongPath(NativeMethods.NullHandleRef);
						Application.executablePath = IntSecurity.UnsafeGetFullPath(moduleFileNameLongPath.ToString());
					}
					else
					{
						string codeBase = entryAssembly.CodeBase;
						Uri uri = new Uri(codeBase);
						if (uri.IsFile)
						{
							Application.executablePath = uri.LocalPath + Uri.UnescapeDataString(uri.Fragment);
						}
						else
						{
							Application.executablePath = uri.ToString();
						}
					}
				}
				Uri uri2 = new Uri(Application.executablePath);
				if (uri2.Scheme == "file")
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Application.executablePath).Demand();
				}
				return Application.executablePath;
			}
		}

		/// <summary>Gets the path for the application data of a local, non-roaming user.</summary>
		/// <returns>The path for the application data of a local, non-roaming user.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00018648 File Offset: 0x00016848
		public static string LocalUserAppDataPath
		{
			get
			{
				try
				{
					if (ApplicationDeployment.IsNetworkDeployed)
					{
						string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
						if (text != null)
						{
							return text;
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return Application.GetDataPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			}
		}

		/// <summary>Gets a value indicating whether a message loop exists on this thread.</summary>
		/// <returns>
		///   <see langword="true" /> if a message loop exists; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000186A8 File Offset: 0x000168A8
		public static bool MessageLoop
		{
			get
			{
				return Application.ThreadContext.FromCurrent().GetMessageLoop();
			}
		}

		/// <summary>Gets a collection of open forms owned by the application.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormCollection" /> containing all the currently open forms owned by this application.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x000186B4 File Offset: 0x000168B4
		public static FormCollection OpenForms
		{
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return Application.OpenFormsInternal;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x000186BB File Offset: 0x000168BB
		internal static FormCollection OpenFormsInternal
		{
			get
			{
				if (Application.forms == null)
				{
					Application.forms = new FormCollection();
				}
				return Application.forms;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000186D3 File Offset: 0x000168D3
		internal static void OpenFormsInternalAdd(Form form)
		{
			Application.OpenFormsInternal.Add(form);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000186E0 File Offset: 0x000168E0
		internal static void OpenFormsInternalRemove(Form form)
		{
			Application.OpenFormsInternal.Remove(form);
		}

		/// <summary>Gets the product name associated with this application.</summary>
		/// <returns>The product name.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x000186F0 File Offset: 0x000168F0
		public static string ProductName
		{
			get
			{
				object obj = Application.internalSyncObject;
				lock (obj)
				{
					if (Application.productName == null)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly != null)
						{
							object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
							if (customAttributes != null && customAttributes.Length != 0)
							{
								Application.productName = ((AssemblyProductAttribute)customAttributes[0]).Product;
							}
						}
						if (Application.productName == null || Application.productName.Length == 0)
						{
							Application.productName = Application.GetAppFileVersionInfo().ProductName;
							if (Application.productName != null)
							{
								Application.productName = Application.productName.Trim();
							}
						}
						if (Application.productName == null || Application.productName.Length == 0)
						{
							Type appMainType = Application.GetAppMainType();
							if (appMainType != null)
							{
								string @namespace = appMainType.Namespace;
								if (!string.IsNullOrEmpty(@namespace))
								{
									int num = @namespace.LastIndexOf(".");
									if (num != -1 && num < @namespace.Length - 1)
									{
										Application.productName = @namespace.Substring(num + 1);
									}
									else
									{
										Application.productName = @namespace;
									}
								}
								else
								{
									Application.productName = appMainType.Name;
								}
							}
						}
					}
				}
				return Application.productName;
			}
		}

		/// <summary>Gets the product version associated with this application.</summary>
		/// <returns>The product version.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00018834 File Offset: 0x00016A34
		public static string ProductVersion
		{
			get
			{
				object obj = Application.internalSyncObject;
				lock (obj)
				{
					if (Application.productVersion == null)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly != null)
						{
							object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
							if (customAttributes != null && customAttributes.Length != 0)
							{
								Application.productVersion = ((AssemblyInformationalVersionAttribute)customAttributes[0]).InformationalVersion;
							}
						}
						if (Application.productVersion == null || Application.productVersion.Length == 0)
						{
							Application.productVersion = Application.GetAppFileVersionInfo().ProductVersion;
							if (Application.productVersion != null)
							{
								Application.productVersion = Application.productVersion.Trim();
							}
						}
						if (Application.productVersion == null || Application.productVersion.Length == 0)
						{
							Application.productVersion = "1.0.0.0";
						}
					}
				}
				return Application.productVersion;
			}
		}

		/// <summary>Registers a callback for checking whether the message loop is running in hosted environments.</summary>
		/// <param name="callback">The method to call when Windows Forms needs to check if the hosting environment is still sending messages.</param>
		// Token: 0x06000909 RID: 2313 RVA: 0x0001890C File Offset: 0x00016B0C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void RegisterMessageLoop(Application.MessageLoopCallback callback)
		{
			Application.ThreadContext.FromCurrent().RegisterMessageLoop(callback);
		}

		/// <summary>Gets a value specifying whether the current application is drawing controls with visual styles.</summary>
		/// <returns>
		///   <see langword="true" /> if visual styles are enabled for controls in the client area of application windows; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00018919 File Offset: 0x00016B19
		public static bool RenderWithVisualStyles
		{
			get
			{
				return Application.ComCtlSupportsVisualStyles && VisualStyleRenderer.IsSupported;
			}
		}

		/// <summary>Gets or sets the format string to apply to top-level window captions when they are displayed with a warning banner.</summary>
		/// <returns>The format string to apply to top-level window captions.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00018929 File Offset: 0x00016B29
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00018946 File Offset: 0x00016B46
		public static string SafeTopLevelCaptionFormat
		{
			get
			{
				if (Application.safeTopLevelCaptionSuffix == null)
				{
					Application.safeTopLevelCaptionSuffix = SR.GetString("SafeTopLevelCaptionFormat");
				}
				return Application.safeTopLevelCaptionSuffix;
			}
			set
			{
				IntSecurity.WindowAdornmentModification.Demand();
				if (value == null)
				{
					value = string.Empty;
				}
				Application.safeTopLevelCaptionSuffix = value;
			}
		}

		/// <summary>Gets the path for the executable file that started the application, not including the executable name.</summary>
		/// <returns>The path for the executable file that started the application.  
		///  This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00018964 File Offset: 0x00016B64
		public static string StartupPath
		{
			get
			{
				if (Application.startupPath == null)
				{
					StringBuilder moduleFileNameLongPath = UnsafeNativeMethods.GetModuleFileNameLongPath(NativeMethods.NullHandleRef);
					Application.startupPath = Path.GetDirectoryName(moduleFileNameLongPath.ToString());
				}
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Application.startupPath).Demand();
				return Application.startupPath;
			}
		}

		/// <summary>Unregisters the message loop callback made with <see cref="M:System.Windows.Forms.Application.RegisterMessageLoop(System.Windows.Forms.Application.MessageLoopCallback)" />.</summary>
		// Token: 0x0600090E RID: 2318 RVA: 0x000189A8 File Offset: 0x00016BA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void UnregisterMessageLoop()
		{
			Application.ThreadContext.FromCurrent().RegisterMessageLoop(null);
		}

		/// <summary>Gets or sets whether the wait cursor is used for all open forms of the application.</summary>
		/// <returns>
		///   <see langword="true" /> is the wait cursor is used for all open forms; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x000189B5 File Offset: 0x00016BB5
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x000189BC File Offset: 0x00016BBC
		public static bool UseWaitCursor
		{
			get
			{
				return Application.useWaitCursor;
			}
			set
			{
				object collectionSyncRoot = FormCollection.CollectionSyncRoot;
				lock (collectionSyncRoot)
				{
					Application.useWaitCursor = value;
					foreach (object obj in Application.OpenFormsInternal)
					{
						Form form = (Form)obj;
						form.UseWaitCursor = Application.useWaitCursor;
					}
				}
			}
		}

		/// <summary>Gets the path for the application data of a user.</summary>
		/// <returns>The path for the application data of a user.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00018A48 File Offset: 0x00016C48
		public static string UserAppDataPath
		{
			get
			{
				try
				{
					if (ApplicationDeployment.IsNetworkDeployed)
					{
						string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
						if (text != null)
						{
							return text;
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return Application.GetDataPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			}
		}

		/// <summary>Gets the registry key for the application data of a user.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.RegistryKey" /> representing the registry key for the application data specific to the user.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00018AA8 File Offset: 0x00016CA8
		public static RegistryKey UserAppDataRegistry
		{
			get
			{
				string text = "Software\\{0}\\{1}\\{2}";
				return Registry.CurrentUser.CreateSubKey(string.Format(CultureInfo.CurrentCulture, text, new object[]
				{
					Application.CompanyName,
					Application.ProductName,
					Application.ProductVersion
				}));
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00018AEE File Offset: 0x00016CEE
		internal static bool UseVisualStyles
		{
			get
			{
				return Application.useVisualStyles;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00018AF5 File Offset: 0x00016CF5
		internal static string WindowsFormsVersion
		{
			get
			{
				return "WindowsForms10";
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00018AFC File Offset: 0x00016CFC
		internal static string WindowMessagesVersion
		{
			get
			{
				return "WindowsForms12";
			}
		}

		/// <summary>Gets a value that specifies how visual styles are applied to application windows.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleState" /> values.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00018B04 File Offset: 0x00016D04
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x00018B24 File Offset: 0x00016D24
		public static VisualStyleState VisualStyleState
		{
			get
			{
				if (!VisualStyleInformation.IsSupportedByOS)
				{
					return VisualStyleState.NoneEnabled;
				}
				return (VisualStyleState)SafeNativeMethods.GetThemeAppProperties();
			}
			set
			{
				if (VisualStyleInformation.IsSupportedByOS)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3) && LocalAppContextSwitches.EnableVisualStyleValidation)
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(VisualStyleState));
					}
					SafeNativeMethods.SetThemeAppProperties((int)value);
					SafeNativeMethods.EnumThreadWindowsCallback enumThreadWindowsCallback = new SafeNativeMethods.EnumThreadWindowsCallback(Application.SendThemeChanged);
					SafeNativeMethods.EnumWindows(enumThreadWindowsCallback, IntPtr.Zero);
					GC.KeepAlive(enumThreadWindowsCallback);
				}
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00018B8C File Offset: 0x00016D8C
		private static bool SendThemeChanged(IntPtr handle, IntPtr extraParameter)
		{
			int currentProcessId = SafeNativeMethods.GetCurrentProcessId();
			int num;
			SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, handle), out num);
			if (num == currentProcessId && SafeNativeMethods.IsWindowVisible(new HandleRef(null, handle)))
			{
				Application.SendThemeChangedRecursive(handle, IntPtr.Zero);
				SafeNativeMethods.RedrawWindow(new HandleRef(null, handle), null, NativeMethods.NullHandleRef, 1157);
			}
			return true;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00018BE5 File Offset: 0x00016DE5
		private static bool SendThemeChangedRecursive(IntPtr handle, IntPtr lparam)
		{
			UnsafeNativeMethods.EnumChildWindows(new HandleRef(null, handle), new NativeMethods.EnumChildrenCallback(Application.SendThemeChangedRecursive), NativeMethods.NullHandleRef);
			UnsafeNativeMethods.SendMessage(new HandleRef(null, handle), 794, 0, 0);
			return true;
		}

		/// <summary>Occurs when the application is about to shut down.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600091A RID: 2330 RVA: 0x00018C1A File Offset: 0x00016E1A
		// (remove) Token: 0x0600091B RID: 2331 RVA: 0x00018C27 File Offset: 0x00016E27
		public static event EventHandler ApplicationExit
		{
			add
			{
				Application.AddEventHandler(Application.EVENT_APPLICATIONEXIT, value);
			}
			remove
			{
				Application.RemoveEventHandler(Application.EVENT_APPLICATIONEXIT, value);
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00018C34 File Offset: 0x00016E34
		private static void AddEventHandler(object key, Delegate value)
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.eventHandlers == null)
				{
					Application.eventHandlers = new EventHandlerList();
				}
				Application.eventHandlers.AddHandler(key, value);
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00018C8C File Offset: 0x00016E8C
		private static void RemoveEventHandler(object key, Delegate value)
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.eventHandlers != null)
				{
					Application.eventHandlers.RemoveHandler(key, value);
				}
			}
		}

		/// <summary>Adds a message filter to monitor Windows messages as they are routed to their destinations.</summary>
		/// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter" /> interface you want to install.</param>
		// Token: 0x0600091E RID: 2334 RVA: 0x00018CDC File Offset: 0x00016EDC
		public static void AddMessageFilter(IMessageFilter value)
		{
			IntSecurity.UnmanagedCode.Demand();
			Application.ThreadContext.FromCurrent().AddMessageFilter(value);
		}

		/// <summary>Runs any filters against a window message, and returns a copy of the modified message.</summary>
		/// <param name="message">The Windows event message to filter.</param>
		/// <returns>
		///   <see langword="true" /> if the filters were processed; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600091F RID: 2335 RVA: 0x00018CF4 File Offset: 0x00016EF4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool FilterMessage(ref Message message)
		{
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			msg.hwnd = message.HWnd;
			msg.message = message.Msg;
			msg.wParam = message.WParam;
			msg.lParam = message.LParam;
			bool flag2;
			bool flag = Application.ThreadContext.FromCurrent().ProcessFilters(ref msg, out flag2);
			if (flag2)
			{
				message.HWnd = msg.hwnd;
				message.Msg = msg.message;
				message.WParam = msg.wParam;
				message.LParam = msg.lParam;
			}
			return flag;
		}

		/// <summary>Occurs when the application finishes processing and is about to enter the idle state.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000920 RID: 2336 RVA: 0x00018D80 File Offset: 0x00016F80
		// (remove) Token: 0x06000921 RID: 2337 RVA: 0x00018DDC File Offset: 0x00016FDC
		public static event EventHandler Idle
		{
			add
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					Application.ThreadContext threadContext3 = threadContext;
					threadContext3.idleHandler = (EventHandler)Delegate.Combine(threadContext3.idleHandler, value);
					object componentManager = threadContext.ComponentManager;
				}
			}
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					Application.ThreadContext threadContext3 = threadContext;
					threadContext3.idleHandler = (EventHandler)Delegate.Remove(threadContext3.idleHandler, value);
				}
			}
		}

		/// <summary>Occurs when the application is about to enter a modal state.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000922 RID: 2338 RVA: 0x00018E30 File Offset: 0x00017030
		// (remove) Token: 0x06000923 RID: 2339 RVA: 0x00018E84 File Offset: 0x00017084
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static event EventHandler EnterThreadModal
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			add
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					Application.ThreadContext threadContext3 = threadContext;
					threadContext3.enterModalHandler = (EventHandler)Delegate.Combine(threadContext3.enterModalHandler, value);
				}
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					Application.ThreadContext threadContext3 = threadContext;
					threadContext3.enterModalHandler = (EventHandler)Delegate.Remove(threadContext3.enterModalHandler, value);
				}
			}
		}

		/// <summary>Occurs when the application is about to leave a modal state.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000924 RID: 2340 RVA: 0x00018ED8 File Offset: 0x000170D8
		// (remove) Token: 0x06000925 RID: 2341 RVA: 0x00018F2C File Offset: 0x0001712C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static event EventHandler LeaveThreadModal
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			add
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					Application.ThreadContext threadContext3 = threadContext;
					threadContext3.leaveModalHandler = (EventHandler)Delegate.Combine(threadContext3.leaveModalHandler, value);
				}
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					Application.ThreadContext threadContext3 = threadContext;
					threadContext3.leaveModalHandler = (EventHandler)Delegate.Remove(threadContext3.leaveModalHandler, value);
				}
			}
		}

		/// <summary>Occurs when an untrapped thread exception is thrown.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000926 RID: 2342 RVA: 0x00018F80 File Offset: 0x00017180
		// (remove) Token: 0x06000927 RID: 2343 RVA: 0x00018FCC File Offset: 0x000171CC
		public static event ThreadExceptionEventHandler ThreadException
		{
			add
			{
				IntSecurity.AffectThreadBehavior.Demand();
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					threadContext.threadExceptionHandler = value;
				}
			}
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext threadContext2 = threadContext;
				lock (threadContext2)
				{
					Application.ThreadContext threadContext3 = threadContext;
					threadContext3.threadExceptionHandler = (ThreadExceptionEventHandler)Delegate.Remove(threadContext3.threadExceptionHandler, value);
				}
			}
		}

		/// <summary>Occurs when a thread is about to shut down. When the main thread for an application is about to be shut down, this event is raised first, followed by an <see cref="E:System.Windows.Forms.Application.ApplicationExit" /> event.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000928 RID: 2344 RVA: 0x00019020 File Offset: 0x00017220
		// (remove) Token: 0x06000929 RID: 2345 RVA: 0x0001902D File Offset: 0x0001722D
		public static event EventHandler ThreadExit
		{
			add
			{
				Application.AddEventHandler(Application.EVENT_THREADEXIT, value);
			}
			remove
			{
				Application.RemoveEventHandler(Application.EVENT_THREADEXIT, value);
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001903A File Offset: 0x0001723A
		internal static void BeginModalMessageLoop()
		{
			Application.ThreadContext.FromCurrent().BeginModalMessageLoop(null);
		}

		/// <summary>Processes all Windows messages currently in the message queue.</summary>
		// Token: 0x0600092B RID: 2347 RVA: 0x00019047 File Offset: 0x00017247
		public static void DoEvents()
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(2, null);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00019055 File Offset: 0x00017255
		internal static void DoEventsModal()
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-2, null);
		}

		/// <summary>Enables visual styles for the application.</summary>
		// Token: 0x0600092D RID: 2349 RVA: 0x00019064 File Offset: 0x00017264
		public static void EnableVisualStyles()
		{
			string text = null;
			new FileIOPermission(PermissionState.None)
			{
				AllFiles = FileIOPermissionAccess.PathDiscovery
			}.Assert();
			try
			{
				text = typeof(Application).Assembly.Location;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (text != null)
			{
				Application.EnableVisualStylesInternal(text, 101);
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000190C0 File Offset: 0x000172C0
		private static void EnableVisualStylesInternal(string assemblyFileName, int nativeResourceID)
		{
			Application.useVisualStyles = UnsafeNativeMethods.ThemingScope.CreateActivationContext(assemblyFileName, nativeResourceID);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000190CE File Offset: 0x000172CE
		internal static void EndModalMessageLoop()
		{
			Application.ThreadContext.FromCurrent().EndModalMessageLoop(null);
		}

		/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
		// Token: 0x06000930 RID: 2352 RVA: 0x000190DB File Offset: 0x000172DB
		public static void Exit()
		{
			Application.Exit(null);
		}

		/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
		/// <param name="e">Returns whether any <see cref="T:System.Windows.Forms.Form" /> within the application cancelled the exit.</param>
		// Token: 0x06000931 RID: 2353 RVA: 0x000190E4 File Offset: 0x000172E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void Exit(CancelEventArgs e)
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (entryAssembly == null || callingAssembly == null || !entryAssembly.Equals(callingAssembly))
			{
				IntSecurity.AffectThreadBehavior.Demand();
			}
			bool flag = Application.ExitInternal();
			if (e != null)
			{
				e.Cancel = flag;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00019134 File Offset: 0x00017334
		private static bool ExitInternal()
		{
			bool flag = false;
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.exiting)
				{
					return false;
				}
				Application.exiting = true;
				try
				{
					if (Application.forms != null)
					{
						foreach (object obj2 in Application.OpenFormsInternal)
						{
							Form form = (Form)obj2;
							if (form.RaiseFormClosingOnAppExit())
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						if (Application.forms != null)
						{
							while (Application.OpenFormsInternal.Count > 0)
							{
								Application.OpenFormsInternal[0].RaiseFormClosedOnAppExit();
							}
						}
						Application.ThreadContext.ExitApplication();
					}
				}
				finally
				{
					Application.exiting = false;
				}
			}
			return flag;
		}

		/// <summary>Exits the message loop on the current thread and closes all windows on the thread.</summary>
		// Token: 0x06000933 RID: 2355 RVA: 0x00019224 File Offset: 0x00017424
		public static void ExitThread()
		{
			IntSecurity.AffectThreadBehavior.Demand();
			Application.ExitThreadInternal();
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00019238 File Offset: 0x00017438
		private static void ExitThreadInternal()
		{
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext.ApplicationContext != null)
			{
				threadContext.ApplicationContext.ExitThread();
				return;
			}
			threadContext.Dispose(true);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00019266 File Offset: 0x00017466
		internal static void FormActivated(bool modal, bool activated)
		{
			if (modal)
			{
				return;
			}
			Application.ThreadContext.FromCurrent().FormActivated(activated);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00019278 File Offset: 0x00017478
		private static FileVersionInfo GetAppFileVersionInfo()
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.appFileVersion == null)
				{
					Type appMainType = Application.GetAppMainType();
					if (appMainType != null)
					{
						new FileIOPermission(PermissionState.None)
						{
							AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)
						}.Assert();
						try
						{
							Application.appFileVersion = FileVersionInfo.GetVersionInfo(appMainType.Module.FullyQualifiedName);
							goto IL_73;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					Application.appFileVersion = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
				}
			}
			IL_73:
			return (FileVersionInfo)Application.appFileVersion;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00019320 File Offset: 0x00017520
		private static Type GetAppMainType()
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.mainType == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					if (entryAssembly != null)
					{
						Application.mainType = entryAssembly.EntryPoint.ReflectedType;
					}
				}
			}
			return Application.mainType;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001938C File Offset: 0x0001758C
		private static Application.ThreadContext GetContextForHandle(HandleRef handle)
		{
			int num;
			int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(handle, out num);
			return Application.ThreadContext.FromId(windowThreadProcessId);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000193AC File Offset: 0x000175AC
		private static string GetDataPath(string basePath)
		{
			string text = "{0}\\{1}\\{2}\\{3}";
			string text2 = Application.CompanyName;
			string text3 = Application.ProductName;
			string text4 = Application.ProductVersion;
			string text5 = string.Format(CultureInfo.CurrentCulture, text, new object[] { basePath, text2, text3, text4 });
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (!Directory.Exists(text5))
				{
					Directory.CreateDirectory(text5);
				}
			}
			return text5;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00019438 File Offset: 0x00017638
		private static void RaiseExit()
		{
			if (Application.eventHandlers != null)
			{
				Delegate @delegate = Application.eventHandlers[Application.EVENT_APPLICATIONEXIT];
				if (@delegate != null)
				{
					((EventHandler)@delegate)(null, EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00019470 File Offset: 0x00017670
		private static void RaiseThreadExit()
		{
			if (Application.eventHandlers != null)
			{
				Delegate @delegate = Application.eventHandlers[Application.EVENT_THREADEXIT];
				if (@delegate != null)
				{
					((EventHandler)@delegate)(null, EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000194A8 File Offset: 0x000176A8
		internal static void ParkHandle(HandleRef handle, DpiAwarenessContext dpiAwarenessContext = DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED)
		{
			Application.ThreadContext contextForHandle = Application.GetContextForHandle(handle);
			if (contextForHandle != null)
			{
				contextForHandle.GetParkingWindow(dpiAwarenessContext).ParkHandle(handle);
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000194CC File Offset: 0x000176CC
		internal static void ParkHandle(CreateParams cp, DpiAwarenessContext dpiAwarenessContext = DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED)
		{
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext != null)
			{
				cp.Parent = threadContext.GetParkingWindow(dpiAwarenessContext).Handle;
			}
		}

		/// <summary>Initializes OLE on the current thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values.</returns>
		// Token: 0x0600093E RID: 2366 RVA: 0x000194F4 File Offset: 0x000176F4
		public static ApartmentState OleRequired()
		{
			return Application.ThreadContext.FromCurrent().OleRequired();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
		/// <param name="t">An <see cref="T:System.Exception" /> that represents the exception that was thrown.</param>
		// Token: 0x0600093F RID: 2367 RVA: 0x00019500 File Offset: 0x00017700
		public static void OnThreadException(Exception t)
		{
			Application.ThreadContext.FromCurrent().OnThreadException(t);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00019510 File Offset: 0x00017710
		internal static void UnparkHandle(HandleRef handle, DpiAwarenessContext context)
		{
			Application.ThreadContext contextForHandle = Application.GetContextForHandle(handle);
			if (contextForHandle != null)
			{
				contextForHandle.GetParkingWindow(context).UnparkHandle(handle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Application.Idle" /> event in hosted scenarios.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> objects to pass to the <see cref="E:System.Windows.Forms.Application.Idle" /> event.</param>
		// Token: 0x06000941 RID: 2369 RVA: 0x00019534 File Offset: 0x00017734
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void RaiseIdle(EventArgs e)
		{
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext.idleHandler != null)
			{
				threadContext.idleHandler(Thread.CurrentThread, e);
			}
		}

		/// <summary>Removes a message filter from the message pump of the application.</summary>
		/// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter" /> to remove from the application.</param>
		// Token: 0x06000942 RID: 2370 RVA: 0x00019560 File Offset: 0x00017760
		public static void RemoveMessageFilter(IMessageFilter value)
		{
			Application.ThreadContext.FromCurrent().RemoveMessageFilter(value);
		}

		/// <summary>Shuts down the application and starts a new instance immediately.</summary>
		/// <exception cref="T:System.NotSupportedException">Your code is not a Windows Forms application. You cannot call this method in this context.</exception>
		// Token: 0x06000943 RID: 2371 RVA: 0x00019570 File Offset: 0x00017770
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Restart()
		{
			if (Assembly.GetEntryAssembly() == null)
			{
				throw new NotSupportedException(SR.GetString("RestartNotSupported"));
			}
			bool flag = false;
			Process currentProcess = Process.GetCurrentProcess();
			if (string.Equals(currentProcess.MainModule.ModuleName, "ieexec.exe", StringComparison.OrdinalIgnoreCase))
			{
				string text = string.Empty;
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				try
				{
					text = Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (string.Equals(text + "\\ieexec.exe", currentProcess.MainModule.FileName, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					Application.ExitInternal();
					string text2 = AppDomain.CurrentDomain.GetData("APP_LAUNCH_URL") as string;
					if (text2 != null)
					{
						Process.Start(currentProcess.MainModule.FileName, text2);
					}
				}
			}
			if (!flag)
			{
				if (ApplicationDeployment.IsNetworkDeployed)
				{
					string updatedApplicationFullName = ApplicationDeployment.CurrentDeployment.UpdatedApplicationFullName;
					uint hostTypeFromMetaData = (uint)Application.ClickOnceUtility.GetHostTypeFromMetaData(updatedApplicationFullName);
					Application.ExitInternal();
					UnsafeNativeMethods.CorLaunchApplication(hostTypeFromMetaData, updatedApplicationFullName, 0, null, 0, null, new UnsafeNativeMethods.PROCESS_INFORMATION());
					return;
				}
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				StringBuilder stringBuilder = new StringBuilder((commandLineArgs.Length - 1) * 16);
				for (int i = 1; i < commandLineArgs.Length - 1; i++)
				{
					stringBuilder.Append('"');
					stringBuilder.Append(commandLineArgs[i]);
					stringBuilder.Append("\" ");
				}
				if (commandLineArgs.Length > 1)
				{
					stringBuilder.Append('"');
					stringBuilder.Append(commandLineArgs[commandLineArgs.Length - 1]);
					stringBuilder.Append('"');
				}
				ProcessStartInfo startInfo = Process.GetCurrentProcess().StartInfo;
				startInfo.FileName = Application.ExecutablePath;
				if (stringBuilder.Length > 0)
				{
					startInfo.Arguments = stringBuilder.ToString();
				}
				Application.ExitInternal();
				Process.Start(startInfo);
			}
		}

		/// <summary>Begins running a standard application message loop on the current thread, without a form.</summary>
		/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread.</exception>
		// Token: 0x06000944 RID: 2372 RVA: 0x00019748 File Offset: 0x00017948
		public static void Run()
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-1, new ApplicationContext());
		}

		/// <summary>Begins running a standard application message loop on the current thread, and makes the specified form visible.</summary>
		/// <param name="mainForm">A <see cref="T:System.Windows.Forms.Form" /> that represents the form to make visible.</param>
		/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on the current thread.</exception>
		// Token: 0x06000945 RID: 2373 RVA: 0x0001975A File Offset: 0x0001795A
		public static void Run(Form mainForm)
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-1, new ApplicationContext(mainForm));
		}

		/// <summary>Begins running a standard application message loop on the current thread, with an <see cref="T:System.Windows.Forms.ApplicationContext" />.</summary>
		/// <param name="context">An <see cref="T:System.Windows.Forms.ApplicationContext" /> in which the application is run.</param>
		/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread.</exception>
		// Token: 0x06000946 RID: 2374 RVA: 0x0001976D File Offset: 0x0001796D
		public static void Run(ApplicationContext context)
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-1, context);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001977B File Offset: 0x0001797B
		internal static void RunDialog(Form form)
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(4, new Application.ModalApplicationContext(form));
		}

		/// <summary>Sets the application-wide default for the <c>UseCompatibleTextRendering</c> property defined on certain controls.</summary>
		/// <param name="defaultValue">The default value to use for new controls. If <see langword="true" />, new controls that support <c>UseCompatibleTextRendering</c> use the GDI+ based <see cref="T:System.Drawing.Graphics" /> class for text rendering; if <see langword="false" />, new controls use the GDI based <see cref="T:System.Windows.Forms.TextRenderer" /> class.</param>
		/// <exception cref="T:System.InvalidOperationException">You can only call this method before the first window is created by your Windows Forms application.</exception>
		// Token: 0x06000948 RID: 2376 RVA: 0x0001978E File Offset: 0x0001798E
		public static void SetCompatibleTextRenderingDefault(bool defaultValue)
		{
			if (NativeWindow.AnyHandleCreated)
			{
				throw new InvalidOperationException(SR.GetString("Win32WindowAlreadyCreated"));
			}
			Control.UseCompatibleTextRenderingDefault = defaultValue;
		}

		/// <summary>Suspends or hibernates the system, or requests that the system be suspended or hibernated.</summary>
		/// <param name="state">A <see cref="T:System.Windows.Forms.PowerState" /> indicating the power activity mode to which to transition.</param>
		/// <param name="force">
		///   <see langword="true" /> to force the suspended mode immediately; <see langword="false" /> to cause Windows to send a suspend request to every application.</param>
		/// <param name="disableWakeEvent">
		///   <see langword="true" /> to disable restoring the system's power status to active on a wake event, <see langword="false" /> to enable restoring the system's power status to active on a wake event.</param>
		/// <returns>
		///   <see langword="true" /> if the system is being suspended, otherwise, <see langword="false" />.</returns>
		// Token: 0x06000949 RID: 2377 RVA: 0x000197AD File Offset: 0x000179AD
		public static bool SetSuspendState(PowerState state, bool force, bool disableWakeEvent)
		{
			IntSecurity.AffectMachineState.Demand();
			return UnsafeNativeMethods.SetSuspendState(state == PowerState.Hibernate, force, disableWakeEvent);
		}

		/// <summary>Instructs the application how to respond to unhandled exceptions.</summary>
		/// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode" /> value describing how the application should behave if an exception is thrown without being caught.</param>
		/// <exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
		// Token: 0x0600094A RID: 2378 RVA: 0x000197C4 File Offset: 0x000179C4
		public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode)
		{
			Application.SetUnhandledExceptionMode(mode, true);
		}

		/// <summary>Instructs the application how to respond to unhandled exceptions, optionally applying thread-specific behavior.</summary>
		/// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode" /> value describing how the application should behave if an exception is thrown without being caught.</param>
		/// <param name="threadScope">
		///   <see langword="true" /> to set the thread exception mode; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
		// Token: 0x0600094B RID: 2379 RVA: 0x000197CD File Offset: 0x000179CD
		public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode, bool threadScope)
		{
			IntSecurity.AffectThreadBehavior.Demand();
			NativeWindow.SetUnhandledExceptionModeInternal(mode, threadScope);
		}

		// Token: 0x040005CA RID: 1482
		private static EventHandlerList eventHandlers;

		// Token: 0x040005CB RID: 1483
		private static string startupPath;

		// Token: 0x040005CC RID: 1484
		private static string executablePath;

		// Token: 0x040005CD RID: 1485
		private static object appFileVersion;

		// Token: 0x040005CE RID: 1486
		private static Type mainType;

		// Token: 0x040005CF RID: 1487
		private static string companyName;

		// Token: 0x040005D0 RID: 1488
		private static string productName;

		// Token: 0x040005D1 RID: 1489
		private static string productVersion;

		// Token: 0x040005D2 RID: 1490
		private static string safeTopLevelCaptionSuffix;

		// Token: 0x040005D3 RID: 1491
		private static bool useVisualStyles = false;

		// Token: 0x040005D4 RID: 1492
		private static bool comCtlSupportsVisualStylesInitialized = false;

		// Token: 0x040005D5 RID: 1493
		private static bool comCtlSupportsVisualStyles = false;

		// Token: 0x040005D6 RID: 1494
		private static FormCollection forms = null;

		// Token: 0x040005D7 RID: 1495
		private static object internalSyncObject = new object();

		// Token: 0x040005D8 RID: 1496
		private static bool useWaitCursor = false;

		// Token: 0x040005D9 RID: 1497
		private static bool useEverettThreadAffinity = false;

		// Token: 0x040005DA RID: 1498
		private static bool checkedThreadAffinity = false;

		// Token: 0x040005DB RID: 1499
		private const string everettThreadAffinityValue = "EnableSystemEventsThreadAffinityCompatibility";

		// Token: 0x040005DC RID: 1500
		private static bool exiting;

		// Token: 0x040005DD RID: 1501
		private static readonly object EVENT_APPLICATIONEXIT = new object();

		// Token: 0x040005DE RID: 1502
		private static readonly object EVENT_THREADEXIT = new object();

		// Token: 0x040005DF RID: 1503
		private const string IEEXEC = "ieexec.exe";

		// Token: 0x040005E0 RID: 1504
		private const string CLICKONCE_APPS_DATADIRECTORY = "DataDirectory";

		// Token: 0x040005E1 RID: 1505
		private static bool parkingWindowSupportsPMAv2 = true;

		/// <summary>Represents a method that will check whether the hosting environment is still sending messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the hosting environment is still sending messages; otherwise, <see langword="false" />.</returns>
		// Token: 0x020005FE RID: 1534
		// (Invoke) Token: 0x060061BD RID: 25021
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public delegate bool MessageLoopCallback();

		// Token: 0x020005FF RID: 1535
		private class ClickOnceUtility
		{
			// Token: 0x060061C0 RID: 25024 RVA: 0x00002843 File Offset: 0x00000A43
			private ClickOnceUtility()
			{
			}

			// Token: 0x060061C1 RID: 25025 RVA: 0x00169158 File Offset: 0x00167358
			public static Application.ClickOnceUtility.HostType GetHostTypeFromMetaData(string appFullName)
			{
				Application.ClickOnceUtility.HostType hostType = Application.ClickOnceUtility.HostType.Default;
				try
				{
					IDefinitionAppId definitionAppId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, appFullName);
					hostType = (Application.ClickOnceUtility.GetPropertyBoolean(definitionAppId, "IsFullTrust") ? Application.ClickOnceUtility.HostType.CorFlag : Application.ClickOnceUtility.HostType.AppLaunch);
				}
				catch
				{
				}
				return hostType;
			}

			// Token: 0x060061C2 RID: 25026 RVA: 0x001691A0 File Offset: 0x001673A0
			private static bool GetPropertyBoolean(IDefinitionAppId appId, string propName)
			{
				string propertyString = Application.ClickOnceUtility.GetPropertyString(appId, propName);
				if (string.IsNullOrEmpty(propertyString))
				{
					return false;
				}
				bool flag;
				try
				{
					flag = Convert.ToBoolean(propertyString, CultureInfo.InvariantCulture);
				}
				catch
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x060061C3 RID: 25027 RVA: 0x001691E4 File Offset: 0x001673E4
			private static string GetPropertyString(IDefinitionAppId appId, string propName)
			{
				byte[] deploymentProperty = IsolationInterop.UserStore.GetDeploymentProperty(Store.GetPackagePropertyFlags.Nothing, appId, Application.ClickOnceUtility.InstallReference, new Guid("2ad613da-6fdb-4671-af9e-18ab2e4df4d8"), propName);
				int num = deploymentProperty.Length;
				if (num == 0 || deploymentProperty.Length % 2 != 0 || deploymentProperty[num - 2] != 0 || deploymentProperty[num - 1] != 0)
				{
					return null;
				}
				return Encoding.Unicode.GetString(deploymentProperty, 0, num - 2);
			}

			// Token: 0x170014FF RID: 5375
			// (get) Token: 0x060061C4 RID: 25028 RVA: 0x0016923B File Offset: 0x0016743B
			private static StoreApplicationReference InstallReference
			{
				get
				{
					return new StoreApplicationReference(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{3f471841-eef2-47d6-89c0-d028f03a4ad5}", null);
				}
			}

			// Token: 0x020008B0 RID: 2224
			public enum HostType
			{
				// Token: 0x04004520 RID: 17696
				Default,
				// Token: 0x04004521 RID: 17697
				AppLaunch,
				// Token: 0x04004522 RID: 17698
				CorFlag
			}
		}

		// Token: 0x02000600 RID: 1536
		private class ComponentManager : UnsafeNativeMethods.IMsoComponentManager
		{
			// Token: 0x17001500 RID: 5376
			// (get) Token: 0x060061C5 RID: 25029 RVA: 0x0016924D File Offset: 0x0016744D
			private Hashtable OleComponents
			{
				get
				{
					if (this.oleComponents == null)
					{
						this.oleComponents = new Hashtable();
						this.cookieCounter = 0;
					}
					return this.oleComponents;
				}
			}

			// Token: 0x060061C6 RID: 25030 RVA: 0x0016926F File Offset: 0x0016746F
			int UnsafeNativeMethods.IMsoComponentManager.QueryService(ref Guid guidService, ref Guid iid, out object ppvObj)
			{
				ppvObj = null;
				return -2147467262;
			}

			// Token: 0x060061C7 RID: 25031 RVA: 0x00012E4E File Offset: 0x0001104E
			bool UnsafeNativeMethods.IMsoComponentManager.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
			{
				return true;
			}

			// Token: 0x060061C8 RID: 25032 RVA: 0x0016927C File Offset: 0x0016747C
			bool UnsafeNativeMethods.IMsoComponentManager.FRegisterComponent(UnsafeNativeMethods.IMsoComponent component, NativeMethods.MSOCRINFOSTRUCT pcrinfo, out IntPtr dwComponentID)
			{
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = new Application.ComponentManager.ComponentHashtableEntry();
				componentHashtableEntry.component = component;
				componentHashtableEntry.componentInfo = pcrinfo;
				Hashtable hashtable = this.OleComponents;
				int num = this.cookieCounter + 1;
				this.cookieCounter = num;
				hashtable.Add(num, componentHashtableEntry);
				dwComponentID = (IntPtr)this.cookieCounter;
				return true;
			}

			// Token: 0x060061C9 RID: 25033 RVA: 0x001692D0 File Offset: 0x001674D0
			bool UnsafeNativeMethods.IMsoComponentManager.FRevokeComponent(IntPtr dwComponentID)
			{
				int num = (int)(long)dwComponentID;
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				if (componentHashtableEntry.component == this.activeComponent)
				{
					this.activeComponent = null;
				}
				if (componentHashtableEntry.component == this.trackingComponent)
				{
					this.trackingComponent = null;
				}
				this.OleComponents.Remove(num);
				return true;
			}

			// Token: 0x060061CA RID: 25034 RVA: 0x00169340 File Offset: 0x00167540
			bool UnsafeNativeMethods.IMsoComponentManager.FUpdateComponentRegistration(IntPtr dwComponentID, NativeMethods.MSOCRINFOSTRUCT info)
			{
				int num = (int)(long)dwComponentID;
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				componentHashtableEntry.componentInfo = info;
				return true;
			}

			// Token: 0x060061CB RID: 25035 RVA: 0x0016937C File Offset: 0x0016757C
			bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentActivate(IntPtr dwComponentID)
			{
				int num = (int)(long)dwComponentID;
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				this.activeComponent = componentHashtableEntry.component;
				return true;
			}

			// Token: 0x060061CC RID: 25036 RVA: 0x001693BC File Offset: 0x001675BC
			bool UnsafeNativeMethods.IMsoComponentManager.FSetTrackingComponent(IntPtr dwComponentID, bool fTrack)
			{
				int num = (int)(long)dwComponentID;
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				if ((componentHashtableEntry.component == this.trackingComponent) ^ fTrack)
				{
					return false;
				}
				if (fTrack)
				{
					this.trackingComponent = componentHashtableEntry.component;
				}
				else
				{
					this.trackingComponent = null;
				}
				return true;
			}

			// Token: 0x060061CD RID: 25037 RVA: 0x0016941C File Offset: 0x0016761C
			void UnsafeNativeMethods.IMsoComponentManager.OnComponentEnterState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude, int dwReserved)
			{
				int num = (int)(long)dwComponentID;
				this.currentState |= uStateID;
				if (uContext == 0 || uContext == 1)
				{
					foreach (object obj in this.OleComponents.Values)
					{
						Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)obj;
						componentHashtableEntry.component.OnEnterState(uStateID, true);
					}
				}
			}

			// Token: 0x060061CE RID: 25038 RVA: 0x001694A0 File Offset: 0x001676A0
			bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentExitState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude)
			{
				int num = (int)(long)dwComponentID;
				this.currentState &= ~uStateID;
				if (uContext == 0 || uContext == 1)
				{
					foreach (object obj in this.OleComponents.Values)
					{
						Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)obj;
						componentHashtableEntry.component.OnEnterState(uStateID, false);
					}
				}
				return false;
			}

			// Token: 0x060061CF RID: 25039 RVA: 0x00169524 File Offset: 0x00167724
			bool UnsafeNativeMethods.IMsoComponentManager.FInState(int uStateID, IntPtr pvoid)
			{
				return (this.currentState & uStateID) != 0;
			}

			// Token: 0x060061D0 RID: 25040 RVA: 0x00169534 File Offset: 0x00167734
			bool UnsafeNativeMethods.IMsoComponentManager.FContinueIdle()
			{
				NativeMethods.MSG msg = default(NativeMethods.MSG);
				return !UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0);
			}

			// Token: 0x060061D1 RID: 25041 RVA: 0x0016955C File Offset: 0x0016775C
			bool UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(IntPtr dwComponentID, int reason, int pvLoopData)
			{
				int num = (int)(long)dwComponentID;
				int num2 = this.currentState;
				bool flag = true;
				if (!this.OleComponents.ContainsKey(num))
				{
					return false;
				}
				UnsafeNativeMethods.IMsoComponent msoComponent = this.activeComponent;
				try
				{
					NativeMethods.MSG msg = default(NativeMethods.MSG);
					NativeMethods.MSG[] array = new NativeMethods.MSG[] { msg };
					Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
					if (componentHashtableEntry == null)
					{
						return false;
					}
					UnsafeNativeMethods.IMsoComponent component = componentHashtableEntry.component;
					this.activeComponent = component;
					while (flag)
					{
						UnsafeNativeMethods.IMsoComponent msoComponent2;
						if (this.trackingComponent != null)
						{
							msoComponent2 = this.trackingComponent;
						}
						else if (this.activeComponent != null)
						{
							msoComponent2 = this.activeComponent;
						}
						else
						{
							msoComponent2 = component;
						}
						bool flag2 = UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0);
						if (flag2)
						{
							array[0] = msg;
							flag = msoComponent2.FContinueMessageLoop(reason, pvLoopData, array);
							if (flag)
							{
								bool flag3;
								if (msg.hwnd != IntPtr.Zero && SafeNativeMethods.IsWindowUnicode(new HandleRef(null, msg.hwnd)))
								{
									flag3 = true;
									UnsafeNativeMethods.GetMessageW(ref msg, NativeMethods.NullHandleRef, 0, 0);
								}
								else
								{
									flag3 = false;
									UnsafeNativeMethods.GetMessageA(ref msg, NativeMethods.NullHandleRef, 0, 0);
								}
								if (msg.message == 18)
								{
									Application.ThreadContext.FromCurrent().DisposeThreadWindows();
									if (reason != -1)
									{
										UnsafeNativeMethods.PostQuitMessage((int)msg.wParam);
									}
									flag = false;
									break;
								}
								if (!msoComponent2.FPreTranslateMessage(ref msg))
								{
									UnsafeNativeMethods.TranslateMessage(ref msg);
									if (flag3)
									{
										UnsafeNativeMethods.DispatchMessageW(ref msg);
									}
									else
									{
										UnsafeNativeMethods.DispatchMessageA(ref msg);
									}
								}
							}
						}
						else
						{
							if (reason == 2)
							{
								break;
							}
							if (reason == -2)
							{
								break;
							}
							bool flag4 = false;
							if (this.OleComponents != null)
							{
								foreach (object obj in this.OleComponents.Values)
								{
									Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry2 = (Application.ComponentManager.ComponentHashtableEntry)obj;
									flag4 |= componentHashtableEntry2.component.FDoIdle(-1);
								}
							}
							flag = msoComponent2.FContinueMessageLoop(reason, pvLoopData, null);
							if (flag)
							{
								if (flag4)
								{
									UnsafeNativeMethods.MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, 100, 255, 4);
								}
								else if (!UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0))
								{
									UnsafeNativeMethods.WaitMessage();
								}
							}
						}
					}
				}
				finally
				{
					this.currentState = num2;
					this.activeComponent = msoComponent;
				}
				return !flag;
			}

			// Token: 0x060061D2 RID: 25042 RVA: 0x001697CC File Offset: 0x001679CC
			bool UnsafeNativeMethods.IMsoComponentManager.FCreateSubComponentManager(object punkOuter, object punkServProv, ref Guid riid, out IntPtr ppvObj)
			{
				ppvObj = IntPtr.Zero;
				return false;
			}

			// Token: 0x060061D3 RID: 25043 RVA: 0x001697D7 File Offset: 0x001679D7
			bool UnsafeNativeMethods.IMsoComponentManager.FGetParentComponentManager(out UnsafeNativeMethods.IMsoComponentManager ppicm)
			{
				ppicm = null;
				return false;
			}

			// Token: 0x060061D4 RID: 25044 RVA: 0x001697E0 File Offset: 0x001679E0
			bool UnsafeNativeMethods.IMsoComponentManager.FGetActiveComponent(int dwgac, UnsafeNativeMethods.IMsoComponent[] ppic, NativeMethods.MSOCRINFOSTRUCT info, int dwReserved)
			{
				UnsafeNativeMethods.IMsoComponent msoComponent = null;
				if (dwgac == 0)
				{
					msoComponent = this.activeComponent;
				}
				else if (dwgac == 1)
				{
					msoComponent = this.trackingComponent;
				}
				else if (dwgac == 2)
				{
					if (this.trackingComponent != null)
					{
						msoComponent = this.trackingComponent;
					}
					else
					{
						msoComponent = this.activeComponent;
					}
				}
				if (ppic != null)
				{
					ppic[0] = msoComponent;
				}
				if (info != null && msoComponent != null)
				{
					foreach (object obj in this.OleComponents.Values)
					{
						Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)obj;
						if (componentHashtableEntry.component == msoComponent)
						{
							info = componentHashtableEntry.componentInfo;
							break;
						}
					}
				}
				return msoComponent != null;
			}

			// Token: 0x0400389B RID: 14491
			private Hashtable oleComponents;

			// Token: 0x0400389C RID: 14492
			private int cookieCounter;

			// Token: 0x0400389D RID: 14493
			private UnsafeNativeMethods.IMsoComponent activeComponent;

			// Token: 0x0400389E RID: 14494
			private UnsafeNativeMethods.IMsoComponent trackingComponent;

			// Token: 0x0400389F RID: 14495
			private int currentState;

			// Token: 0x020008B1 RID: 2225
			private class ComponentHashtableEntry
			{
				// Token: 0x04004523 RID: 17699
				public UnsafeNativeMethods.IMsoComponent component;

				// Token: 0x04004524 RID: 17700
				public NativeMethods.MSOCRINFOSTRUCT componentInfo;
			}
		}

		// Token: 0x02000601 RID: 1537
		internal sealed class ThreadContext : MarshalByRefObject, UnsafeNativeMethods.IMsoComponent
		{
			// Token: 0x060061D6 RID: 25046 RVA: 0x00169894 File Offset: 0x00167A94
			public ThreadContext()
			{
				IntPtr zero = IntPtr.Zero;
				UnsafeNativeMethods.DuplicateHandle(new HandleRef(null, SafeNativeMethods.GetCurrentProcess()), new HandleRef(null, SafeNativeMethods.GetCurrentThread()), new HandleRef(null, SafeNativeMethods.GetCurrentProcess()), ref zero, 0, false, 2);
				this.handle = zero;
				this.id = SafeNativeMethods.GetCurrentThreadId();
				this.messageLoopCount = 0;
				Application.ThreadContext.currentThreadContext = this;
				Application.ThreadContext.contextHash[this.id] = this;
			}

			// Token: 0x17001501 RID: 5377
			// (get) Token: 0x060061D7 RID: 25047 RVA: 0x00169920 File Offset: 0x00167B20
			public ApplicationContext ApplicationContext
			{
				get
				{
					return this.applicationContext;
				}
			}

			// Token: 0x17001502 RID: 5378
			// (get) Token: 0x060061D8 RID: 25048 RVA: 0x00169928 File Offset: 0x00167B28
			internal UnsafeNativeMethods.IMsoComponentManager ComponentManager
			{
				get
				{
					if (this.componentManager == null)
					{
						if (this.fetchingComponentManager)
						{
							return null;
						}
						this.fetchingComponentManager = true;
						try
						{
							UnsafeNativeMethods.IMsoComponentManager msoComponentManager = null;
							Application.OleRequired();
							IntPtr intPtr = (IntPtr)0;
							if (NativeMethods.Succeeded(UnsafeNativeMethods.CoRegisterMessageFilter(NativeMethods.NullHandleRef, ref intPtr)) && intPtr != (IntPtr)0)
							{
								IntPtr intPtr2 = (IntPtr)0;
								UnsafeNativeMethods.CoRegisterMessageFilter(new HandleRef(null, intPtr), ref intPtr2);
								object obj = Marshal.GetObjectForIUnknown(intPtr);
								Marshal.Release(intPtr);
								UnsafeNativeMethods.IOleServiceProvider oleServiceProvider = obj as UnsafeNativeMethods.IOleServiceProvider;
								if (oleServiceProvider != null)
								{
									try
									{
										IntPtr zero = IntPtr.Zero;
										Guid guid = new Guid("000C060B-0000-0000-C000-000000000046");
										Guid guid2 = new Guid("{000C0601-0000-0000-C000-000000000046}");
										int num = oleServiceProvider.QueryService(ref guid, ref guid2, out zero);
										if (NativeMethods.Succeeded(num) && zero != IntPtr.Zero)
										{
											IntPtr intPtr3;
											try
											{
												Guid guid3 = typeof(UnsafeNativeMethods.IMsoComponentManager).GUID;
												num = Marshal.QueryInterface(zero, ref guid3, out intPtr3);
											}
											finally
											{
												Marshal.Release(zero);
											}
											if (NativeMethods.Succeeded(num) && intPtr3 != IntPtr.Zero)
											{
												try
												{
													msoComponentManager = ComponentManagerBroker.GetComponentManager(intPtr3);
												}
												finally
												{
													Marshal.Release(intPtr3);
												}
											}
											if (msoComponentManager != null)
											{
												if (intPtr == zero)
												{
													obj = null;
												}
												this.externalComponentManager = true;
												AppDomain.CurrentDomain.DomainUnload += this.OnDomainUnload;
												AppDomain.CurrentDomain.ProcessExit += this.OnDomainUnload;
											}
										}
									}
									catch
									{
									}
								}
								if (obj != null && Marshal.IsComObject(obj))
								{
									Marshal.ReleaseComObject(obj);
								}
							}
							if (msoComponentManager == null)
							{
								msoComponentManager = new Application.ComponentManager();
								this.externalComponentManager = false;
							}
							if (msoComponentManager != null && this.componentID == -1)
							{
								IntPtr intPtr4;
								bool flag = msoComponentManager.FRegisterComponent(this, new NativeMethods.MSOCRINFOSTRUCT
								{
									cbSize = Marshal.SizeOf(typeof(NativeMethods.MSOCRINFOSTRUCT)),
									uIdleTimeInterval = 0,
									grfcrf = 9,
									grfcadvf = 1
								}, out intPtr4);
								this.componentID = (int)(long)intPtr4;
								if (flag && !(msoComponentManager is Application.ComponentManager))
								{
									this.messageLoopCount++;
								}
								this.componentManager = msoComponentManager;
							}
						}
						finally
						{
							this.fetchingComponentManager = false;
						}
					}
					return this.componentManager;
				}
			}

			// Token: 0x17001503 RID: 5379
			// (get) Token: 0x060061D9 RID: 25049 RVA: 0x00169BB4 File Offset: 0x00167DB4
			internal bool CustomThreadExceptionHandlerAttached
			{
				get
				{
					return this.threadExceptionHandler != null;
				}
			}

			// Token: 0x060061DA RID: 25050 RVA: 0x00169BC0 File Offset: 0x00167DC0
			internal Application.ParkingWindow GetParkingWindow(DpiAwarenessContext context)
			{
				Application.ParkingWindow parkingWindow2;
				lock (this)
				{
					Application.ParkingWindow parkingWindow = this.GetParkingWindowForContext(context);
					if (parkingWindow == null)
					{
						IntSecurity.ManipulateWndProcAndHandles.Assert();
						try
						{
							using (DpiHelper.EnterDpiAwarenessScope(context))
							{
								parkingWindow = new Application.ParkingWindow();
							}
							this.parkingWindows.Add(parkingWindow);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					parkingWindow2 = parkingWindow;
				}
				return parkingWindow2;
			}

			// Token: 0x060061DB RID: 25051 RVA: 0x00169C54 File Offset: 0x00167E54
			internal Application.ParkingWindow GetParkingWindowForContext(DpiAwarenessContext context)
			{
				if (this.parkingWindows.Count == 0)
				{
					return null;
				}
				if (!DpiHelper.EnableDpiChangedHighDpiImprovements || CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(context, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED))
				{
					return this.parkingWindows[0];
				}
				foreach (Application.ParkingWindow parkingWindow in this.parkingWindows)
				{
					if (CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(parkingWindow.DpiAwarenessContext, context))
					{
						return parkingWindow;
					}
				}
				return null;
			}

			// Token: 0x17001504 RID: 5380
			// (get) Token: 0x060061DC RID: 25052 RVA: 0x00169CE4 File Offset: 0x00167EE4
			// (set) Token: 0x060061DD RID: 25053 RVA: 0x00169D0D File Offset: 0x00167F0D
			internal Control ActivatingControl
			{
				get
				{
					if (this.activatingControlRef != null && this.activatingControlRef.IsAlive)
					{
						return this.activatingControlRef.Target as Control;
					}
					return null;
				}
				set
				{
					if (value != null)
					{
						this.activatingControlRef = new WeakReference(value);
						return;
					}
					this.activatingControlRef = null;
				}
			}

			// Token: 0x17001505 RID: 5381
			// (get) Token: 0x060061DE RID: 25054 RVA: 0x00169D28 File Offset: 0x00167F28
			internal Control MarshalingControl
			{
				get
				{
					Control control;
					lock (this)
					{
						if (this.marshalingControl == null)
						{
							this.marshalingControl = new Application.MarshalingControl();
						}
						control = this.marshalingControl;
					}
					return control;
				}
			}

			// Token: 0x060061DF RID: 25055 RVA: 0x00169D78 File Offset: 0x00167F78
			internal void AddMessageFilter(IMessageFilter f)
			{
				if (this.messageFilters == null)
				{
					this.messageFilters = new ArrayList();
				}
				if (f != null)
				{
					this.SetState(16, false);
					if (this.messageFilters.Count > 0 && f is IMessageModifyAndFilter)
					{
						this.messageFilters.Insert(0, f);
						return;
					}
					this.messageFilters.Add(f);
				}
			}

			// Token: 0x060061E0 RID: 25056 RVA: 0x00169DD8 File Offset: 0x00167FD8
			internal void BeginModalMessageLoop(ApplicationContext context)
			{
				bool flag = this.ourModalLoop;
				this.ourModalLoop = true;
				try
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null)
					{
						msoComponentManager.OnComponentEnterState((IntPtr)this.componentID, 1, 0, 0, 0, 0);
					}
				}
				finally
				{
					this.ourModalLoop = flag;
				}
				this.DisableWindowsForModalLoop(false, context);
				this.modalCount++;
				if (this.enterModalHandler != null && this.modalCount == 1)
				{
					this.enterModalHandler(Thread.CurrentThread, EventArgs.Empty);
				}
			}

			// Token: 0x060061E1 RID: 25057 RVA: 0x00169E6C File Offset: 0x0016806C
			internal void DisableWindowsForModalLoop(bool onlyWinForms, ApplicationContext context)
			{
				Application.ThreadWindows threadWindows = this.threadWindows;
				this.threadWindows = new Application.ThreadWindows(onlyWinForms);
				this.threadWindows.Enable(false);
				this.threadWindows.previousThreadWindows = threadWindows;
				Application.ModalApplicationContext modalApplicationContext = context as Application.ModalApplicationContext;
				if (modalApplicationContext != null)
				{
					modalApplicationContext.DisableThreadWindows(true, onlyWinForms);
				}
			}

			// Token: 0x060061E2 RID: 25058 RVA: 0x00169EB8 File Offset: 0x001680B8
			internal void Dispose(bool postQuit)
			{
				lock (this)
				{
					try
					{
						int num = this.disposeCount;
						this.disposeCount = num + 1;
						if (num == 0)
						{
							if (this.messageLoopCount > 0 && postQuit)
							{
								this.PostQuit();
							}
							else
							{
								bool flag2 = SafeNativeMethods.GetCurrentThreadId() == this.id;
								try
								{
									if (flag2)
									{
										if (this.componentManager != null)
										{
											this.RevokeComponent();
										}
										this.DisposeThreadWindows();
										try
										{
											Application.RaiseThreadExit();
										}
										finally
										{
											if (this.GetState(1) && !this.GetState(2))
											{
												this.SetState(1, false);
												UnsafeNativeMethods.OleUninitialize();
											}
										}
									}
								}
								finally
								{
									if (this.handle != IntPtr.Zero)
									{
										UnsafeNativeMethods.CloseHandle(new HandleRef(this, this.handle));
										this.handle = IntPtr.Zero;
									}
									try
									{
										if (Application.ThreadContext.totalMessageLoopCount == 0)
										{
											Application.RaiseExit();
										}
									}
									finally
									{
										object obj = Application.ThreadContext.tcInternalSyncObject;
										lock (obj)
										{
											Application.ThreadContext.contextHash.Remove(this.id);
										}
										if (Application.ThreadContext.currentThreadContext == this)
										{
											Application.ThreadContext.currentThreadContext = null;
										}
									}
								}
							}
							GC.SuppressFinalize(this);
						}
					}
					finally
					{
						this.disposeCount--;
					}
				}
			}

			// Token: 0x060061E3 RID: 25059 RVA: 0x0016A090 File Offset: 0x00168290
			private void DisposeParkingWindow()
			{
				if (this.parkingWindows.Count != 0)
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this.parkingWindows[0], this.parkingWindows[0].Handle), out num);
					int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
					for (int i = 0; i < this.parkingWindows.Count; i++)
					{
						if (windowThreadProcessId == currentThreadId)
						{
							this.parkingWindows[i].Destroy();
						}
						else
						{
							this.parkingWindows[i] = null;
						}
					}
					this.parkingWindows.Clear();
				}
			}

			// Token: 0x060061E4 RID: 25060 RVA: 0x0016A120 File Offset: 0x00168320
			internal void DisposeThreadWindows()
			{
				try
				{
					if (this.applicationContext != null)
					{
						this.applicationContext.Dispose();
						this.applicationContext = null;
					}
					Application.ThreadWindows threadWindows = new Application.ThreadWindows(true);
					threadWindows.Dispose();
					this.DisposeParkingWindow();
				}
				catch
				{
				}
			}

			// Token: 0x060061E5 RID: 25061 RVA: 0x0016A170 File Offset: 0x00168370
			internal void EnableWindowsForModalLoop(bool onlyWinForms, ApplicationContext context)
			{
				if (this.threadWindows != null)
				{
					this.threadWindows.Enable(true);
					this.threadWindows = this.threadWindows.previousThreadWindows;
				}
				Application.ModalApplicationContext modalApplicationContext = context as Application.ModalApplicationContext;
				if (modalApplicationContext != null)
				{
					modalApplicationContext.DisableThreadWindows(false, onlyWinForms);
				}
			}

			// Token: 0x060061E6 RID: 25062 RVA: 0x0016A1B4 File Offset: 0x001683B4
			internal void EndModalMessageLoop(ApplicationContext context)
			{
				this.EnableWindowsForModalLoop(false, context);
				bool flag = this.ourModalLoop;
				this.ourModalLoop = true;
				try
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null)
					{
						msoComponentManager.FOnComponentExitState((IntPtr)this.componentID, 1, 0, 0, 0);
					}
				}
				finally
				{
					this.ourModalLoop = flag;
				}
				this.modalCount--;
				if (this.leaveModalHandler != null && this.modalCount == 0)
				{
					this.leaveModalHandler(Thread.CurrentThread, EventArgs.Empty);
				}
			}

			// Token: 0x060061E7 RID: 25063 RVA: 0x0016A244 File Offset: 0x00168444
			internal static void ExitApplication()
			{
				Application.ThreadContext.ExitCommon(true);
			}

			// Token: 0x060061E8 RID: 25064 RVA: 0x0016A24C File Offset: 0x0016844C
			private static void ExitCommon(bool disposing)
			{
				object obj = Application.ThreadContext.tcInternalSyncObject;
				lock (obj)
				{
					if (Application.ThreadContext.contextHash != null)
					{
						Application.ThreadContext[] array = new Application.ThreadContext[Application.ThreadContext.contextHash.Values.Count];
						Application.ThreadContext.contextHash.Values.CopyTo(array, 0);
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].ApplicationContext != null)
							{
								array[i].ApplicationContext.ExitThread();
							}
							else
							{
								array[i].Dispose(disposing);
							}
						}
					}
				}
			}

			// Token: 0x060061E9 RID: 25065 RVA: 0x0016A2E4 File Offset: 0x001684E4
			internal static void ExitDomain()
			{
				Application.ThreadContext.ExitCommon(false);
			}

			// Token: 0x060061EA RID: 25066 RVA: 0x0016A2EC File Offset: 0x001684EC
			~ThreadContext()
			{
				if (this.handle != IntPtr.Zero)
				{
					UnsafeNativeMethods.CloseHandle(new HandleRef(this, this.handle));
					this.handle = IntPtr.Zero;
				}
			}

			// Token: 0x060061EB RID: 25067 RVA: 0x0016A344 File Offset: 0x00168544
			internal void FormActivated(bool activate)
			{
				if (activate)
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null && !(msoComponentManager is Application.ComponentManager))
					{
						msoComponentManager.FOnComponentActivate((IntPtr)this.componentID);
					}
				}
			}

			// Token: 0x060061EC RID: 25068 RVA: 0x0016A378 File Offset: 0x00168578
			internal void TrackInput(bool track)
			{
				if (track != this.GetState(32))
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null && !(msoComponentManager is Application.ComponentManager))
					{
						msoComponentManager.FSetTrackingComponent((IntPtr)this.componentID, track);
						this.SetState(32, track);
					}
				}
			}

			// Token: 0x060061ED RID: 25069 RVA: 0x0016A3C0 File Offset: 0x001685C0
			internal static Application.ThreadContext FromCurrent()
			{
				Application.ThreadContext threadContext = Application.ThreadContext.currentThreadContext;
				if (threadContext == null)
				{
					threadContext = new Application.ThreadContext();
				}
				return threadContext;
			}

			// Token: 0x060061EE RID: 25070 RVA: 0x0016A3E0 File Offset: 0x001685E0
			internal static Application.ThreadContext FromId(int id)
			{
				Application.ThreadContext threadContext = (Application.ThreadContext)Application.ThreadContext.contextHash[id];
				if (threadContext == null && id == SafeNativeMethods.GetCurrentThreadId())
				{
					threadContext = new Application.ThreadContext();
				}
				return threadContext;
			}

			// Token: 0x060061EF RID: 25071 RVA: 0x0016A415 File Offset: 0x00168615
			internal bool GetAllowQuit()
			{
				return Application.ThreadContext.totalMessageLoopCount > 0 && Application.ThreadContext.baseLoopReason == -1;
			}

			// Token: 0x060061F0 RID: 25072 RVA: 0x0016A429 File Offset: 0x00168629
			internal IntPtr GetHandle()
			{
				return this.handle;
			}

			// Token: 0x060061F1 RID: 25073 RVA: 0x0016A431 File Offset: 0x00168631
			internal int GetId()
			{
				return this.id;
			}

			// Token: 0x060061F2 RID: 25074 RVA: 0x0016A439 File Offset: 0x00168639
			internal CultureInfo GetCulture()
			{
				if (this.culture == null || this.culture.LCID != SafeNativeMethods.GetThreadLocale())
				{
					this.culture = new CultureInfo(SafeNativeMethods.GetThreadLocale());
				}
				return this.culture;
			}

			// Token: 0x060061F3 RID: 25075 RVA: 0x0016A46B File Offset: 0x0016866B
			internal bool GetMessageLoop()
			{
				return this.GetMessageLoop(false);
			}

			// Token: 0x060061F4 RID: 25076 RVA: 0x0016A474 File Offset: 0x00168674
			internal bool GetMessageLoop(bool mustBeActive)
			{
				if (this.messageLoopCount > ((mustBeActive && this.externalComponentManager) ? 1 : 0))
				{
					return true;
				}
				if (this.ComponentManager != null && this.externalComponentManager)
				{
					if (!mustBeActive)
					{
						return true;
					}
					UnsafeNativeMethods.IMsoComponent[] array = new UnsafeNativeMethods.IMsoComponent[1];
					if (this.ComponentManager.FGetActiveComponent(0, array, null, 0) && array[0] == this)
					{
						return true;
					}
				}
				Application.MessageLoopCallback messageLoopCallback = this.messageLoopCallback;
				return messageLoopCallback != null && messageLoopCallback();
			}

			// Token: 0x060061F5 RID: 25077 RVA: 0x0016A4E1 File Offset: 0x001686E1
			private bool GetState(int bit)
			{
				return (this.threadState & bit) != 0;
			}

			// Token: 0x060061F6 RID: 25078 RVA: 0x00015C90 File Offset: 0x00013E90
			public override object InitializeLifetimeService()
			{
				return null;
			}

			// Token: 0x060061F7 RID: 25079 RVA: 0x0016A4EE File Offset: 0x001686EE
			internal bool IsValidComponentId()
			{
				return this.componentID != -1;
			}

			// Token: 0x060061F8 RID: 25080 RVA: 0x0016A4FC File Offset: 0x001686FC
			internal ApartmentState OleRequired()
			{
				Thread currentThread = Thread.CurrentThread;
				if (!this.GetState(1))
				{
					int num = UnsafeNativeMethods.OleInitialize();
					this.SetState(1, true);
					if (num == -2147417850)
					{
						this.SetState(2, true);
					}
				}
				if (this.GetState(2))
				{
					return ApartmentState.MTA;
				}
				return ApartmentState.STA;
			}

			// Token: 0x060061F9 RID: 25081 RVA: 0x0016A542 File Offset: 0x00168742
			private void OnAppThreadExit(object sender, EventArgs e)
			{
				this.Dispose(true);
			}

			// Token: 0x060061FA RID: 25082 RVA: 0x0016A54B File Offset: 0x0016874B
			[PrePrepareMethod]
			private void OnDomainUnload(object sender, EventArgs e)
			{
				this.RevokeComponent();
				Application.ThreadContext.ExitDomain();
			}

			// Token: 0x060061FB RID: 25083 RVA: 0x0016A558 File Offset: 0x00168758
			internal void OnThreadException(Exception t)
			{
				if (this.GetState(4))
				{
					return;
				}
				this.SetState(4, true);
				try
				{
					if (this.threadExceptionHandler != null)
					{
						this.threadExceptionHandler(Thread.CurrentThread, new ThreadExceptionEventArgs(t));
					}
					else if (SystemInformation.UserInteractive)
					{
						ThreadExceptionDialog threadExceptionDialog = new ThreadExceptionDialog(t);
						DialogResult dialogResult = DialogResult.OK;
						IntSecurity.ModifyFocus.Assert();
						try
						{
							dialogResult = threadExceptionDialog.ShowDialog();
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							threadExceptionDialog.Dispose();
						}
						if (dialogResult != DialogResult.Abort)
						{
							if (dialogResult == DialogResult.Yes)
							{
								WarningException ex = t as WarningException;
								if (ex != null)
								{
									Help.ShowHelp(null, ex.HelpUrl, ex.HelpTopic);
								}
							}
						}
						else
						{
							Application.ExitInternal();
							new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
							Environment.Exit(0);
						}
					}
				}
				finally
				{
					this.SetState(4, false);
				}
			}

			// Token: 0x060061FC RID: 25084 RVA: 0x0016A62C File Offset: 0x0016882C
			internal void PostQuit()
			{
				UnsafeNativeMethods.PostThreadMessage(this.id, 18, IntPtr.Zero, IntPtr.Zero);
				this.SetState(8, true);
			}

			// Token: 0x060061FD RID: 25085 RVA: 0x0016A64E File Offset: 0x0016884E
			internal void RegisterMessageLoop(Application.MessageLoopCallback callback)
			{
				this.messageLoopCallback = callback;
			}

			// Token: 0x060061FE RID: 25086 RVA: 0x0016A657 File Offset: 0x00168857
			internal void RemoveMessageFilter(IMessageFilter f)
			{
				if (this.messageFilters != null)
				{
					this.SetState(16, false);
					this.messageFilters.Remove(f);
				}
			}

			// Token: 0x060061FF RID: 25087 RVA: 0x0016A678 File Offset: 0x00168878
			internal void RunMessageLoop(int reason, ApplicationContext context)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (Application.useVisualStyles)
				{
					intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				}
				try
				{
					this.RunMessageLoopInner(reason, context);
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}

			// Token: 0x06006200 RID: 25088 RVA: 0x0016A6BC File Offset: 0x001688BC
			private void RunMessageLoopInner(int reason, ApplicationContext context)
			{
				if (reason == 4 && !SystemInformation.UserInteractive)
				{
					throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
				}
				if (reason == -1)
				{
					this.SetState(8, false);
				}
				if (Application.ThreadContext.totalMessageLoopCount++ == 0)
				{
					Application.ThreadContext.baseLoopReason = reason;
				}
				this.messageLoopCount++;
				if (reason == -1)
				{
					if (this.messageLoopCount != 1)
					{
						throw new InvalidOperationException(SR.GetString("CantNestMessageLoops"));
					}
					this.applicationContext = context;
					this.applicationContext.ThreadExit += this.OnAppThreadExit;
					if (this.applicationContext.MainForm != null)
					{
						this.applicationContext.MainForm.Visible = true;
					}
					DpiHelper.InitializeDpiHelperForWinforms();
					AccessibilityImprovements.ValidateLevels();
				}
				Form form = this.currentForm;
				if (context != null)
				{
					this.currentForm = context.MainForm;
				}
				bool flag = false;
				bool flag2 = false;
				HandleRef handleRef = new HandleRef(null, IntPtr.Zero);
				if (reason == -2)
				{
					flag2 = true;
				}
				if (reason == 4 || reason == 5)
				{
					flag = true;
					bool flag3 = this.currentForm != null && this.currentForm.Enabled;
					this.BeginModalMessageLoop(context);
					handleRef = new HandleRef(null, UnsafeNativeMethods.GetWindowLong(new HandleRef(this.currentForm, this.currentForm.Handle), -8));
					if (handleRef.Handle != IntPtr.Zero)
					{
						if (SafeNativeMethods.IsWindowEnabled(handleRef))
						{
							SafeNativeMethods.EnableWindow(handleRef, false);
						}
						else
						{
							handleRef = new HandleRef(null, IntPtr.Zero);
						}
					}
					if (this.currentForm != null && this.currentForm.IsHandleCreated && SafeNativeMethods.IsWindowEnabled(new HandleRef(this.currentForm, this.currentForm.Handle)) != flag3)
					{
						SafeNativeMethods.EnableWindow(new HandleRef(this.currentForm, this.currentForm.Handle), flag3);
					}
				}
				try
				{
					if (this.messageLoopCount == 1)
					{
						WindowsFormsSynchronizationContext.InstallIfNeeded();
					}
					if (flag && this.currentForm != null)
					{
						this.currentForm.Visible = true;
					}
					if ((!flag && !flag2) || this.ComponentManager is Application.ComponentManager)
					{
						bool flag4 = this.ComponentManager.FPushMessageLoop((IntPtr)this.componentID, reason, 0);
					}
					else if (reason == 2 || reason == -2)
					{
						bool flag4 = this.LocalModalMessageLoop(null);
					}
					else
					{
						bool flag4 = this.LocalModalMessageLoop(this.currentForm);
					}
				}
				finally
				{
					if (flag)
					{
						this.EndModalMessageLoop(context);
						if (handleRef.Handle != IntPtr.Zero)
						{
							SafeNativeMethods.EnableWindow(handleRef, true);
						}
					}
					this.currentForm = form;
					Application.ThreadContext.totalMessageLoopCount--;
					this.messageLoopCount--;
					if (this.messageLoopCount == 0)
					{
						WindowsFormsSynchronizationContext.Uninstall(false);
					}
					if (reason == -1)
					{
						this.Dispose(true);
					}
					else if (this.messageLoopCount == 0 && this.componentManager != null)
					{
						this.RevokeComponent();
					}
				}
			}

			// Token: 0x06006201 RID: 25089 RVA: 0x0016A980 File Offset: 0x00168B80
			private bool LocalModalMessageLoop(Form form)
			{
				bool flag4;
				try
				{
					NativeMethods.MSG msg = default(NativeMethods.MSG);
					bool flag = true;
					while (flag)
					{
						bool flag2 = UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0);
						if (flag2)
						{
							bool flag3;
							if (msg.hwnd != IntPtr.Zero && SafeNativeMethods.IsWindowUnicode(new HandleRef(null, msg.hwnd)))
							{
								flag3 = true;
								if (!UnsafeNativeMethods.GetMessageW(ref msg, NativeMethods.NullHandleRef, 0, 0))
								{
									continue;
								}
							}
							else
							{
								flag3 = false;
								if (!UnsafeNativeMethods.GetMessageA(ref msg, NativeMethods.NullHandleRef, 0, 0))
								{
									continue;
								}
							}
							if (!this.PreTranslateMessage(ref msg))
							{
								UnsafeNativeMethods.TranslateMessage(ref msg);
								if (flag3)
								{
									UnsafeNativeMethods.DispatchMessageW(ref msg);
								}
								else
								{
									UnsafeNativeMethods.DispatchMessageA(ref msg);
								}
							}
							if (form != null)
							{
								flag = !form.CheckCloseDialog(false);
							}
						}
						else
						{
							if (form == null)
							{
								break;
							}
							if (!UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0))
							{
								UnsafeNativeMethods.WaitMessage();
							}
						}
					}
					flag4 = flag;
				}
				catch
				{
					flag4 = false;
				}
				return flag4;
			}

			// Token: 0x06006202 RID: 25090 RVA: 0x0016AA74 File Offset: 0x00168C74
			internal bool ProcessFilters(ref NativeMethods.MSG msg, out bool modified)
			{
				bool flag = false;
				modified = false;
				if (this.messageFilters != null && !this.GetState(16) && (LocalAppContextSwitches.DontSupportReentrantFilterMessage || this.inProcessFilters == 0))
				{
					if (this.messageFilters.Count > 0)
					{
						this.messageFilterSnapshot = new IMessageFilter[this.messageFilters.Count];
						this.messageFilters.CopyTo(this.messageFilterSnapshot);
					}
					else
					{
						this.messageFilterSnapshot = null;
					}
					this.SetState(16, true);
				}
				this.inProcessFilters++;
				try
				{
					if (this.messageFilterSnapshot != null)
					{
						int num = this.messageFilterSnapshot.Length;
						Message message = Message.Create(msg.hwnd, msg.message, msg.wParam, msg.lParam);
						for (int i = 0; i < num; i++)
						{
							IMessageFilter messageFilter = this.messageFilterSnapshot[i];
							bool flag2 = messageFilter.PreFilterMessage(ref message);
							if (messageFilter is IMessageModifyAndFilter)
							{
								msg.hwnd = message.HWnd;
								msg.message = message.Msg;
								msg.wParam = message.WParam;
								msg.lParam = message.LParam;
								modified = true;
							}
							if (flag2)
							{
								flag = true;
								break;
							}
						}
					}
				}
				finally
				{
					this.inProcessFilters--;
				}
				return flag;
			}

			// Token: 0x06006203 RID: 25091 RVA: 0x0016ABBC File Offset: 0x00168DBC
			internal bool PreTranslateMessage(ref NativeMethods.MSG msg)
			{
				bool flag = false;
				if (this.ProcessFilters(ref msg, out flag))
				{
					return true;
				}
				if (msg.message >= 256 && msg.message <= 264)
				{
					if (msg.message == 258)
					{
						int num = 21364736;
						if ((int)(long)msg.wParam == 3 && ((int)(long)msg.lParam & num) == num && Debugger.IsAttached)
						{
							Debugger.Break();
						}
					}
					Control control = Control.FromChildHandleInternal(msg.hwnd);
					bool flag2 = false;
					Message message = Message.Create(msg.hwnd, msg.message, msg.wParam, msg.lParam);
					if (control != null)
					{
						if (NativeWindow.WndProcShouldBeDebuggable)
						{
							if (Control.PreProcessControlMessageInternal(control, ref message) == PreProcessControlState.MessageProcessed)
							{
								flag2 = true;
								goto IL_104;
							}
							goto IL_104;
						}
						else
						{
							try
							{
								if (Control.PreProcessControlMessageInternal(control, ref message) == PreProcessControlState.MessageProcessed)
								{
									flag2 = true;
								}
								goto IL_104;
							}
							catch (Exception ex)
							{
								this.OnThreadException(ex);
								goto IL_104;
							}
						}
					}
					IntPtr ancestor = UnsafeNativeMethods.GetAncestor(new HandleRef(null, msg.hwnd), 2);
					if (ancestor != IntPtr.Zero && UnsafeNativeMethods.IsDialogMessage(new HandleRef(null, ancestor), ref msg))
					{
						return true;
					}
					IL_104:
					msg.wParam = message.WParam;
					msg.lParam = message.LParam;
					if (flag2)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006204 RID: 25092 RVA: 0x0016AD00 File Offset: 0x00168F00
			private void RevokeComponent()
			{
				if (this.componentManager != null && this.componentID != -1)
				{
					int num = this.componentID;
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.componentManager;
					try
					{
						msoComponentManager.FRevokeComponent((IntPtr)num);
						if (Marshal.IsComObject(msoComponentManager))
						{
							Marshal.ReleaseComObject(msoComponentManager);
						}
					}
					finally
					{
						this.componentManager = null;
						this.componentID = -1;
					}
				}
			}

			// Token: 0x06006205 RID: 25093 RVA: 0x0016AD6C File Offset: 0x00168F6C
			internal void SetCulture(CultureInfo culture)
			{
				if (culture != null && culture.LCID != SafeNativeMethods.GetThreadLocale())
				{
					SafeNativeMethods.SetThreadLocale(culture.LCID);
				}
			}

			// Token: 0x06006206 RID: 25094 RVA: 0x0016AD8A File Offset: 0x00168F8A
			private void SetState(int bit, bool value)
			{
				if (value)
				{
					this.threadState |= bit;
					return;
				}
				this.threadState &= ~bit;
			}

			// Token: 0x06006207 RID: 25095 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool UnsafeNativeMethods.IMsoComponent.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
			{
				return false;
			}

			// Token: 0x06006208 RID: 25096 RVA: 0x0016ADAD File Offset: 0x00168FAD
			bool UnsafeNativeMethods.IMsoComponent.FPreTranslateMessage(ref NativeMethods.MSG msg)
			{
				return this.PreTranslateMessage(ref msg);
			}

			// Token: 0x06006209 RID: 25097 RVA: 0x0016ADB6 File Offset: 0x00168FB6
			void UnsafeNativeMethods.IMsoComponent.OnEnterState(int uStateID, bool fEnter)
			{
				if (this.ourModalLoop)
				{
					return;
				}
				if (uStateID == 1)
				{
					if (fEnter)
					{
						this.DisableWindowsForModalLoop(true, null);
						return;
					}
					this.EnableWindowsForModalLoop(true, null);
				}
			}

			// Token: 0x0600620A RID: 25098 RVA: 0x000070A6 File Offset: 0x000052A6
			void UnsafeNativeMethods.IMsoComponent.OnAppActivate(bool fActive, int dwOtherThreadID)
			{
			}

			// Token: 0x0600620B RID: 25099 RVA: 0x000070A6 File Offset: 0x000052A6
			void UnsafeNativeMethods.IMsoComponent.OnLoseActivation()
			{
			}

			// Token: 0x0600620C RID: 25100 RVA: 0x000070A6 File Offset: 0x000052A6
			void UnsafeNativeMethods.IMsoComponent.OnActivationChange(UnsafeNativeMethods.IMsoComponent component, bool fSameComponent, int pcrinfo, bool fHostIsActivating, int pchostinfo, int dwReserved)
			{
			}

			// Token: 0x0600620D RID: 25101 RVA: 0x0016ADD9 File Offset: 0x00168FD9
			bool UnsafeNativeMethods.IMsoComponent.FDoIdle(int grfidlef)
			{
				if (this.idleHandler != null)
				{
					this.idleHandler(Thread.CurrentThread, EventArgs.Empty);
				}
				return false;
			}

			// Token: 0x0600620E RID: 25102 RVA: 0x0016ADFC File Offset: 0x00168FFC
			bool UnsafeNativeMethods.IMsoComponent.FContinueMessageLoop(int reason, int pvLoopData, NativeMethods.MSG[] msgPeeked)
			{
				bool flag = true;
				if (msgPeeked == null && this.GetState(8))
				{
					flag = false;
				}
				else
				{
					switch (reason)
					{
					case -2:
					case 2:
						if (!UnsafeNativeMethods.PeekMessage(ref this.tempMsg, NativeMethods.NullHandleRef, 0, 0, 0))
						{
							flag = false;
						}
						break;
					case 1:
					{
						int num;
						SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, UnsafeNativeMethods.GetActiveWindow()), out num);
						if (num == SafeNativeMethods.GetCurrentProcessId())
						{
							flag = false;
						}
						break;
					}
					case 4:
					case 5:
						if (this.currentForm == null || this.currentForm.CheckCloseDialog(false))
						{
							flag = false;
						}
						break;
					}
				}
				return flag;
			}

			// Token: 0x0600620F RID: 25103 RVA: 0x00012E4E File Offset: 0x0001104E
			bool UnsafeNativeMethods.IMsoComponent.FQueryTerminate(bool fPromptUser)
			{
				return true;
			}

			// Token: 0x06006210 RID: 25104 RVA: 0x0016AE97 File Offset: 0x00169097
			void UnsafeNativeMethods.IMsoComponent.Terminate()
			{
				if (this.messageLoopCount > 0 && !(this.ComponentManager is Application.ComponentManager))
				{
					this.messageLoopCount--;
				}
				this.Dispose(false);
			}

			// Token: 0x06006211 RID: 25105 RVA: 0x000F9C41 File Offset: 0x000F7E41
			IntPtr UnsafeNativeMethods.IMsoComponent.HwndGetWindow(int dwWhich, int dwReserved)
			{
				return IntPtr.Zero;
			}

			// Token: 0x040038A0 RID: 14496
			private const int STATE_OLEINITIALIZED = 1;

			// Token: 0x040038A1 RID: 14497
			private const int STATE_EXTERNALOLEINIT = 2;

			// Token: 0x040038A2 RID: 14498
			private const int STATE_INTHREADEXCEPTION = 4;

			// Token: 0x040038A3 RID: 14499
			private const int STATE_POSTEDQUIT = 8;

			// Token: 0x040038A4 RID: 14500
			private const int STATE_FILTERSNAPSHOTVALID = 16;

			// Token: 0x040038A5 RID: 14501
			private const int STATE_TRACKINGCOMPONENT = 32;

			// Token: 0x040038A6 RID: 14502
			private const int INVALID_ID = -1;

			// Token: 0x040038A7 RID: 14503
			private static Hashtable contextHash = new Hashtable();

			// Token: 0x040038A8 RID: 14504
			private static object tcInternalSyncObject = new object();

			// Token: 0x040038A9 RID: 14505
			private static int totalMessageLoopCount;

			// Token: 0x040038AA RID: 14506
			private static int baseLoopReason;

			// Token: 0x040038AB RID: 14507
			[ThreadStatic]
			private static Application.ThreadContext currentThreadContext;

			// Token: 0x040038AC RID: 14508
			internal ThreadExceptionEventHandler threadExceptionHandler;

			// Token: 0x040038AD RID: 14509
			internal EventHandler idleHandler;

			// Token: 0x040038AE RID: 14510
			internal EventHandler enterModalHandler;

			// Token: 0x040038AF RID: 14511
			internal EventHandler leaveModalHandler;

			// Token: 0x040038B0 RID: 14512
			private ApplicationContext applicationContext;

			// Token: 0x040038B1 RID: 14513
			private List<Application.ParkingWindow> parkingWindows = new List<Application.ParkingWindow>();

			// Token: 0x040038B2 RID: 14514
			private Control marshalingControl;

			// Token: 0x040038B3 RID: 14515
			private CultureInfo culture;

			// Token: 0x040038B4 RID: 14516
			private ArrayList messageFilters;

			// Token: 0x040038B5 RID: 14517
			private IMessageFilter[] messageFilterSnapshot;

			// Token: 0x040038B6 RID: 14518
			private int inProcessFilters;

			// Token: 0x040038B7 RID: 14519
			private IntPtr handle;

			// Token: 0x040038B8 RID: 14520
			private int id;

			// Token: 0x040038B9 RID: 14521
			private int messageLoopCount;

			// Token: 0x040038BA RID: 14522
			private int threadState;

			// Token: 0x040038BB RID: 14523
			private int modalCount;

			// Token: 0x040038BC RID: 14524
			private WeakReference activatingControlRef;

			// Token: 0x040038BD RID: 14525
			private UnsafeNativeMethods.IMsoComponentManager componentManager;

			// Token: 0x040038BE RID: 14526
			private bool externalComponentManager;

			// Token: 0x040038BF RID: 14527
			private bool fetchingComponentManager;

			// Token: 0x040038C0 RID: 14528
			private int componentID = -1;

			// Token: 0x040038C1 RID: 14529
			private Form currentForm;

			// Token: 0x040038C2 RID: 14530
			private Application.ThreadWindows threadWindows;

			// Token: 0x040038C3 RID: 14531
			private NativeMethods.MSG tempMsg;

			// Token: 0x040038C4 RID: 14532
			private int disposeCount;

			// Token: 0x040038C5 RID: 14533
			private bool ourModalLoop;

			// Token: 0x040038C6 RID: 14534
			private Application.MessageLoopCallback messageLoopCallback;
		}

		// Token: 0x02000602 RID: 1538
		internal sealed class MarshalingControl : Control
		{
			// Token: 0x06006213 RID: 25107 RVA: 0x0016AEDA File Offset: 0x001690DA
			internal MarshalingControl()
				: base(false)
			{
				base.Visible = false;
				base.SetState2(8, false);
				base.SetTopLevel(true);
				base.CreateControl();
				this.CreateHandle();
			}

			// Token: 0x17001506 RID: 5382
			// (get) Token: 0x06006214 RID: 25108 RVA: 0x0016AF08 File Offset: 0x00169108
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						createParams.Parent = (IntPtr)NativeMethods.HWND_MESSAGE;
					}
					return createParams;
				}
			}

			// Token: 0x06006215 RID: 25109 RVA: 0x000070A6 File Offset: 0x000052A6
			protected override void OnLayout(LayoutEventArgs levent)
			{
			}

			// Token: 0x06006216 RID: 25110 RVA: 0x000070A6 File Offset: 0x000052A6
			protected override void OnSizeChanged(EventArgs e)
			{
			}
		}

		// Token: 0x02000603 RID: 1539
		internal sealed class ParkingWindow : ContainerControl, IArrangedElement, IComponent, IDisposable
		{
			// Token: 0x06006217 RID: 25111 RVA: 0x0016AF3A File Offset: 0x0016913A
			public ParkingWindow()
			{
				base.SetState2(8, false);
				base.SetState(524288, true);
				this.Text = "WindowsFormsParkingWindow";
				base.Visible = false;
			}

			// Token: 0x17001507 RID: 5383
			// (get) Token: 0x06006218 RID: 25112 RVA: 0x0016AF68 File Offset: 0x00169168
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						createParams.Parent = (IntPtr)NativeMethods.HWND_MESSAGE;
					}
					return createParams;
				}
			}

			// Token: 0x06006219 RID: 25113 RVA: 0x0016AF9A File Offset: 0x0016919A
			internal override void AddReflectChild()
			{
				if (this.childCount < 0)
				{
					this.childCount = 0;
				}
				this.childCount++;
			}

			// Token: 0x0600621A RID: 25114 RVA: 0x0016AFBC File Offset: 0x001691BC
			internal override void RemoveReflectChild()
			{
				this.childCount--;
				if (this.childCount < 0)
				{
					this.childCount = 0;
				}
				if (this.childCount == 0 && base.IsHandleCreated)
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, base.HandleInternal), out num);
					Application.ThreadContext threadContext = Application.ThreadContext.FromId(windowThreadProcessId);
					if (threadContext == null || threadContext != Application.ThreadContext.FromCurrent())
					{
						UnsafeNativeMethods.PostMessage(new HandleRef(this, base.HandleInternal), 1025, IntPtr.Zero, IntPtr.Zero);
						return;
					}
					this.CheckDestroy();
				}
			}

			// Token: 0x0600621B RID: 25115 RVA: 0x0016B048 File Offset: 0x00169248
			private void CheckDestroy()
			{
				if (this.childCount == 0)
				{
					IntPtr window = UnsafeNativeMethods.GetWindow(new HandleRef(this, base.Handle), 5);
					if (window == IntPtr.Zero)
					{
						this.DestroyHandle();
					}
				}
			}

			// Token: 0x0600621C RID: 25116 RVA: 0x0016B083 File Offset: 0x00169283
			public void Destroy()
			{
				this.DestroyHandle();
			}

			// Token: 0x0600621D RID: 25117 RVA: 0x0016B08B File Offset: 0x0016928B
			internal void ParkHandle(HandleRef handle)
			{
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				UnsafeNativeMethods.SetParent(handle, new HandleRef(this, base.Handle));
			}

			// Token: 0x0600621E RID: 25118 RVA: 0x0016B0AE File Offset: 0x001692AE
			internal void UnparkHandle(HandleRef handle)
			{
				if (base.IsHandleCreated)
				{
					this.CheckDestroy();
				}
			}

			// Token: 0x0600621F RID: 25119 RVA: 0x000070A6 File Offset: 0x000052A6
			protected override void OnLayout(LayoutEventArgs levent)
			{
			}

			// Token: 0x06006220 RID: 25120 RVA: 0x000070A6 File Offset: 0x000052A6
			void IArrangedElement.PerformLayout(IArrangedElement affectedElement, string affectedProperty)
			{
			}

			// Token: 0x06006221 RID: 25121 RVA: 0x0016B0C0 File Offset: 0x001692C0
			protected override void WndProc(ref Message m)
			{
				if (m.Msg != 24)
				{
					base.WndProc(ref m);
					if (m.Msg == 528)
					{
						if (NativeMethods.Util.LOWORD((int)(long)m.WParam) == 2)
						{
							UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 1025, IntPtr.Zero, IntPtr.Zero);
							return;
						}
					}
					else if (m.Msg == 1025)
					{
						this.CheckDestroy();
					}
				}
			}

			// Token: 0x040038C7 RID: 14535
			private const int WM_CHECKDESTROY = 1025;

			// Token: 0x040038C8 RID: 14536
			private int childCount;
		}

		// Token: 0x02000604 RID: 1540
		private sealed class ThreadWindows
		{
			// Token: 0x06006222 RID: 25122 RVA: 0x0016B134 File Offset: 0x00169334
			internal ThreadWindows(bool onlyWinForms)
			{
				this.windows = new IntPtr[16];
				this.onlyWinForms = onlyWinForms;
				UnsafeNativeMethods.EnumThreadWindows(SafeNativeMethods.GetCurrentThreadId(), new NativeMethods.EnumThreadWindowsCallback(this.Callback), NativeMethods.NullHandleRef);
			}

			// Token: 0x06006223 RID: 25123 RVA: 0x0016B174 File Offset: 0x00169374
			private bool Callback(IntPtr hWnd, IntPtr lparam)
			{
				if (SafeNativeMethods.IsWindowVisible(new HandleRef(null, hWnd)) && SafeNativeMethods.IsWindowEnabled(new HandleRef(null, hWnd)))
				{
					bool flag = true;
					if (this.onlyWinForms && Control.FromHandleInternal(hWnd) == null)
					{
						flag = false;
					}
					if (flag)
					{
						if (this.windowCount == this.windows.Length)
						{
							IntPtr[] array = new IntPtr[this.windowCount * 2];
							Array.Copy(this.windows, 0, array, 0, this.windowCount);
							this.windows = array;
						}
						IntPtr[] array2 = this.windows;
						int num = this.windowCount;
						this.windowCount = num + 1;
						array2[num] = hWnd;
					}
				}
				return true;
			}

			// Token: 0x06006224 RID: 25124 RVA: 0x0016B20C File Offset: 0x0016940C
			internal void Dispose()
			{
				for (int i = 0; i < this.windowCount; i++)
				{
					IntPtr intPtr = this.windows[i];
					if (UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)))
					{
						Control control = Control.FromHandleInternal(intPtr);
						if (control != null)
						{
							control.Dispose();
						}
					}
				}
			}

			// Token: 0x06006225 RID: 25125 RVA: 0x0016B254 File Offset: 0x00169454
			internal void Enable(bool state)
			{
				if (!this.onlyWinForms && !state)
				{
					this.activeHwnd = UnsafeNativeMethods.GetActiveWindow();
					Control activatingControl = Application.ThreadContext.FromCurrent().ActivatingControl;
					if (activatingControl != null)
					{
						this.focusedHwnd = activatingControl.Handle;
					}
					else
					{
						this.focusedHwnd = UnsafeNativeMethods.GetFocus();
					}
				}
				for (int i = 0; i < this.windowCount; i++)
				{
					IntPtr intPtr = this.windows[i];
					if (UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)))
					{
						SafeNativeMethods.EnableWindow(new HandleRef(null, intPtr), state);
					}
				}
				if (!this.onlyWinForms && state)
				{
					if (this.activeHwnd != IntPtr.Zero && UnsafeNativeMethods.IsWindow(new HandleRef(null, this.activeHwnd)))
					{
						UnsafeNativeMethods.SetActiveWindow(new HandleRef(null, this.activeHwnd));
					}
					if (this.focusedHwnd != IntPtr.Zero && UnsafeNativeMethods.IsWindow(new HandleRef(null, this.focusedHwnd)))
					{
						UnsafeNativeMethods.SetFocus(new HandleRef(null, this.focusedHwnd));
					}
				}
			}

			// Token: 0x040038C9 RID: 14537
			private IntPtr[] windows;

			// Token: 0x040038CA RID: 14538
			private int windowCount;

			// Token: 0x040038CB RID: 14539
			private IntPtr activeHwnd;

			// Token: 0x040038CC RID: 14540
			private IntPtr focusedHwnd;

			// Token: 0x040038CD RID: 14541
			internal Application.ThreadWindows previousThreadWindows;

			// Token: 0x040038CE RID: 14542
			private bool onlyWinForms = true;
		}

		// Token: 0x02000605 RID: 1541
		private class ModalApplicationContext : ApplicationContext
		{
			// Token: 0x06006226 RID: 25126 RVA: 0x0016B34F File Offset: 0x0016954F
			public ModalApplicationContext(Form modalForm)
				: base(modalForm)
			{
			}

			// Token: 0x06006227 RID: 25127 RVA: 0x0016B358 File Offset: 0x00169558
			public void DisableThreadWindows(bool disable, bool onlyWinForms)
			{
				Control control = null;
				if (base.MainForm != null && base.MainForm.IsHandleCreated)
				{
					IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.MainForm.Handle), -8);
					control = Control.FromHandleInternal(windowLong);
					if (control != null && control.InvokeRequired)
					{
						this.parentWindowContext = Application.GetContextForHandle(new HandleRef(this, windowLong));
					}
					else
					{
						this.parentWindowContext = null;
					}
				}
				if (this.parentWindowContext != null)
				{
					if (control == null)
					{
						control = this.parentWindowContext.ApplicationContext.MainForm;
					}
					if (disable)
					{
						control.Invoke(new Application.ModalApplicationContext.ThreadWindowCallback(this.DisableThreadWindowsCallback), new object[] { this.parentWindowContext, onlyWinForms });
						return;
					}
					control.Invoke(new Application.ModalApplicationContext.ThreadWindowCallback(this.EnableThreadWindowsCallback), new object[] { this.parentWindowContext, onlyWinForms });
				}
			}

			// Token: 0x06006228 RID: 25128 RVA: 0x0016B438 File Offset: 0x00169638
			private void DisableThreadWindowsCallback(Application.ThreadContext context, bool onlyWinForms)
			{
				context.DisableWindowsForModalLoop(onlyWinForms, this);
			}

			// Token: 0x06006229 RID: 25129 RVA: 0x0016B442 File Offset: 0x00169642
			private void EnableThreadWindowsCallback(Application.ThreadContext context, bool onlyWinForms)
			{
				context.EnableWindowsForModalLoop(onlyWinForms, this);
			}

			// Token: 0x0600622A RID: 25130 RVA: 0x000070A6 File Offset: 0x000052A6
			protected override void ExitThreadCore()
			{
			}

			// Token: 0x040038CF RID: 14543
			private Application.ThreadContext parentWindowContext;

			// Token: 0x020008B2 RID: 2226
			// (Invoke) Token: 0x0600725B RID: 29275
			private delegate void ThreadWindowCallback(Application.ThreadContext context, bool onlyWinForms);
		}
	}
}
