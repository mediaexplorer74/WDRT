using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Controls;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Properties;
using Microsoft.WindowsDeviceRecoveryTool.States.Error;
using Microsoft.WindowsDeviceRecoveryTool.States.Shell;
using Microsoft.WindowsDeviceRecoveryTool.Styles.Assets;

namespace Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic
{
	// Token: 0x020000E2 RID: 226
	public class AppBootstrapper
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x00020490 File Offset: 0x0001E690
		public AppBootstrapper()
		{
			this.InitializeSettings();
			bool flag = this.CantRunApplication();
			if (!flag)
			{
				this.ShowSplashScreen();
				this.InitializeApplication();
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x000204C7 File Offset: 0x0001E6C7
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x000204CF File Offset: 0x0001E6CF
		private protected AppContext AppContext { protected get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x000204D8 File Offset: 0x0001E6D8
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x000204E0 File Offset: 0x0001E6E0
		protected ShellState ShellState { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x000204E9 File Offset: 0x0001E6E9
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x000204F1 File Offset: 0x0001E6F1
		private protected CompositionContainer Container { protected get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000204FA File Offset: 0x0001E6FA
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00020502 File Offset: 0x0001E702
		private protected ICommandRepository CommandRepository { protected get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0002050B File Offset: 0x0001E70B
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00020513 File Offset: 0x0001E713
		private protected Application Application { protected get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0002051C File Offset: 0x0001E71C
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00020524 File Offset: 0x0001E724
		private protected MainWindow ShellWindow { protected get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0002052D File Offset: 0x0001E72D
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x00020535 File Offset: 0x0001E735
		private protected EventAggregator EventAggregator { protected get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00020540 File Offset: 0x0001E740
		public static bool IsInDesignMode
		{
			get
			{
				bool flag = AppBootstrapper.isInDesignMode == null;
				if (flag)
				{
					AppBootstrapper.isInDesignMode = new bool?(Application.Current != null && (Application.Current.ToString() == "System.Windows.Application" || Application.Current.ToString() == "Microsoft.Expression.Blend.BlendApplication"));
				}
				return AppBootstrapper.isInDesignMode.GetValueOrDefault(false);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000205B4 File Offset: 0x0001E7B4
		protected bool CantRunApplication()
		{
			bool flag = AppBootstrapper.IsInDesignMode;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = AppInfo.IsAnotherInstanceRunning();
				if (flag3)
				{
					string text = string.Format(LocalizationManager.GetTranslation("AnotherInstanceAlreadyRunning"), AppInfo.AppTitle());
					this.ShowSthWentWrongMessage(text);
					flag2 = true;
				}
				else
				{
					bool flag4 = DateTime.UtcNow >= ApplicationBuildSettings.ExpirationDate;
					if (flag4)
					{
						Tracer<AppBootstrapper>.WriteInformation("Build is out of date!");
						this.ShowSthWentWrongMessage("Windows Device Recovery Tool build has expired, please install a new version.");
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00020634 File Offset: 0x0001E834
		protected void InitializeSettings()
		{
			this.RestorePreviousSettings();
			StyleLogic.RestoreStyle(Settings.Default.Style);
			StyleLogic.LoadTheme(Settings.GetSelectedThemeFileName());
			bool flag = Settings.Default.CustomPackagesPathEnabled && !string.IsNullOrWhiteSpace(Settings.Default.PackagesPath);
			if (flag)
			{
				Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CustomPackagesPath = Settings.Default.PackagesPath;
			}
			else
			{
				Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CustomPackagesPath = null;
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000206A8 File Offset: 0x0001E8A8
		private void RestorePreviousSettings()
		{
			bool callUpgrade = Settings.Default.CallUpgrade;
			if (callUpgrade)
			{
				Tracer<AppBootstrapper>.WriteInformation("Settings upgrade needed");
				Settings.Default.Upgrade();
				Settings.Default.CallUpgrade = false;
				bool flag = !StyleLogic.IfStyleExists(Settings.Default.Style);
				if (flag)
				{
					Tracer<AppBootstrapper>.WriteInformation("Saved Style doesn't exist. Need to reset it to default");
					Settings.Default.Style = Settings.Default.Properties["Style"].DefaultValue as string;
					Settings.Default.Theme = Settings.Default.Properties["Theme"].DefaultValue as string;
				}
				Settings.Default.Save();
				Tracer<AppBootstrapper>.WriteInformation("Settings are upgraded");
			}
			LocalizationManager.Instance().CurrentLanguage = ApplicationInfo.CurrentLanguageInRegistry;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00020788 File Offset: 0x0001E988
		protected void InitializeApplication()
		{
			try
			{
				Tracer<AppBootstrapper>.WriteInformation("Operating system version: {0}", new object[] { Environment.OSVersion.ToString() });
				this.Application = Application.Current;
				this.ConfigureDispacher();
				this.ConfigureLogging();
				this.ConfigureMef();
				this.ClearUpdatesFolder();
			}
			catch (Exception ex)
			{
				this.ShowSthWentWrongMessage(ex);
			}
			this.InitializeBusinessLogicLayer();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00020804 File Offset: 0x0001EA04
		protected void InitializeUiLogic()
		{
			try
			{
				this.ConfigureCommands();
				this.ConfigureApplication();
				this.ConfigureShellWindow();
				this.Application.MainWindow = this.ShellWindow;
				this.CloseSplashScreen();
				this.ShellWindow.Show();
				this.ShellWindow.DataContext = this.Container.GetExportedValue<MainWindowViewModel>();
				this.ShellWindow.ContentRendered += this.ShellWindowOnContentRendered;
			}
			catch (Exception ex)
			{
				this.ShowSthWentWrongMessage(ex);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0002089C File Offset: 0x0001EA9C
		protected void ShellWindowOnContentRendered(object sender, EventArgs eventArgs)
		{
			AppDispatcher.Execute(delegate
			{
				this.ShellState.StartStateMachine();
			}, false);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000208B2 File Offset: 0x0001EAB2
		protected void InitializeBusinessLogicLayer()
		{
			this.businessLogicHostThread = new Thread(new ThreadStart(this.StartBusinessLogic));
			this.businessLogicHostThread.Start();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000208D8 File Offset: 0x0001EAD8
		protected void StartBusinessLogic()
		{
			try
			{
				Tracer<AppBootstrapper>.WriteInformation("Start initializing a business logic.");
				this.logicContext = this.Container.GetExportedValue<LogicContext>();
				Tracer<AppBootstrapper>.WriteInformation("The business logic is initialized.");
				AppDispatcher.Execute(new Action(this.InitializeUiLogic), false);
			}
			catch (Exception ex)
			{
				this.ShowSthWentWrongMessage(ex);
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00020944 File Offset: 0x0001EB44
		protected void ClearUpdatesFolder()
		{
			try
			{
				string[] files = Directory.GetFiles(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.AppUpdate));
				bool flag = files.Any<string>();
				if (flag)
				{
					foreach (string text in files)
					{
						File.Delete(text);
					}
				}
			}
			catch (Exception ex)
			{
				Tracer<AppBootstrapper>.WriteError("Cannot delete content of Updates folder!", new object[] { ex });
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000209BC File Offset: 0x0001EBBC
		protected void ConfigureLogging()
		{
			try
			{
				bool traceEnabled = Settings.Default.TraceEnabled;
				if (traceEnabled)
				{
					TraceManager.Instance.EnableDiagnosticLogs(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Traces), Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppNamePrefix);
					Tracer<AppBootstrapper>.WriteInformation("App version: {0} (running on: {1})", new object[]
					{
						AppInfo.Version,
						Environment.OSVersion
					});
					this.isLoggerConfigured = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowSthWentWrongMessage(ex);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00020A3C File Offset: 0x0001EC3C
		protected void ConfigureCommands()
		{
			Tracer<AppBootstrapper>.LogEntry("ConfigureCommands");
			List<Type> list = (from type in Assembly.GetExecutingAssembly().GetTypes()
				where typeof(IController).IsAssignableFrom(type) && type.FullName.Contains("Microsoft.WindowsDeviceRecoveryTool.Controllers")
				select type).ToList<Type>();
			foreach (Type type2 in list)
			{
				this.Container.GetExportedValue<IController>(type2.FullName);
			}
			this.CommandRepository = this.Container.GetExportedValue<ICommandRepository>();
			Tracer<AppBootstrapper>.LogExit("ConfigureCommands");
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		protected void ConfigureDispacher()
		{
			AppDispatcher.Initialize(Dispatcher.CurrentDispatcher);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00020B08 File Offset: 0x0001ED08
		protected void ConfigureMef()
		{
			AggregateCatalog aggregateCatalog = new AggregateCatalog(new ComposablePartCatalog[]
			{
				new AssemblyCatalog(Assembly.GetExecutingAssembly()),
				new DirectoryCatalog(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppPath, "Microsoft.WindowsDeviceRecoveryTool*.dll")
			});
			this.Container = new CompositionContainer(aggregateCatalog, new ExportProvider[0]);
			this.Container.ComposeExportedValue(this.Container);
			try
			{
				this.Container.ComposeParts(new object[0]);
			}
			catch (CompositionException ex)
			{
				Tracer<AppBootstrapper>.WriteError(ex.Message, new object[0]);
			}
			this.AppContext = this.Container.GetExportedValue<AppContext>();
			this.EventAggregator = this.Container.GetExportedValue<EventAggregator>();
			Tracer<AppBootstrapper>.WriteInformation("The container configured.");
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		protected void ConfigureApplication()
		{
			Tracer<AppBootstrapper>.LogEntry("ConfigureApplication");
			this.Application.DispatcherUnhandledException += this.OnUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += this.OnCurrentDomainUnhandledException;
			this.ShellState = this.Container.GetExportedValue<ShellState>();
			this.ShellState.Container = this.Container;
			this.ShellState.InitializeStateMachine();
			Tracer<AppBootstrapper>.LogExit("ConfigureApplication");
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00020C58 File Offset: 0x0001EE58
		protected void ConfigureShellWindow()
		{
			Tracer<AppBootstrapper>.LogEntry("ConfigureShellWindow");
			this.ShellWindow = this.Container.GetExportedValue<MainWindow>("ShellWindow");
			ShellView exportedValue = this.Container.GetExportedValue<ShellView>();
			exportedValue.DataContext = this.Container.GetExportedValue<ShellViewModel>();
			this.ShellWindow.Root.Children.Add(exportedValue);
			this.ShellWindow.Closing += this.OnWindowClosing;
			Tracer<AppBootstrapper>.LogExit("ConfigureShellWindow");
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		protected void OnWindowClosing(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.CommandRepository.Run((AppController c) => c.CloseAppOperations(this.ShellWindow, CancellationToken.None));
			Tracer<AppBootstrapper>.WriteInformation("Starting closing App.");
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00020D8B File Offset: 0x0001EF8B
		protected void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			this.OnExceptionOccured(e.ExceptionObject as Exception);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		protected void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			this.OnExceptionOccured(e.Exception);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		private void OnExceptionOccured(Exception exception)
		{
			Tracer<AppBootstrapper>.WriteError(exception);
			ErrorView exportedValue = this.Container.GetExportedValue<ErrorView>();
			ErrorTemplateSelector errorTemplateSelector = exportedValue.Resources["ErrorSelector"] as ErrorTemplateSelector;
			bool flag = errorTemplateSelector != null && errorTemplateSelector.IsImplemented(exception);
			if (flag)
			{
				this.EventAggregator.Publish<ErrorMessage>(new ErrorMessage(exception));
				this.CommandRepository.Run((AppController c) => c.SwitchToState("ErrorState"));
			}
			else
			{
				this.ShowSthWentWrongMessage(exception);
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00020E84 File Offset: 0x0001F084
		private void ShowSthWentWrongMessage(Exception ex)
		{
			bool flag = this.isLoggerConfigured;
			if (flag)
			{
				Tracer<AppBootstrapper>.WriteError(ex);
			}
			this.ShowSthWentWrongMessage(ex.Message);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00020EB4 File Offset: 0x0001F0B4
		private void ShowSthWentWrongMessage(string message)
		{
			ExtendedMessageBox extendedMessageBox = new ExtendedMessageBox();
			bool flag = this.ShellWindow != null && this.ShellWindow.IsLoaded;
			if (flag)
			{
				extendedMessageBox.Owner = this.ShellWindow;
			}
			extendedMessageBox.MessageBoxText = LocalizationManager.GetTranslation("SthWentWrongMessage");
			extendedMessageBox.MessageBoxAdvance = message;
			extendedMessageBox.Title = LocalizationManager.GetTranslation("Error");
			extendedMessageBox.ShowDialog();
			bool flag2 = (this.ShellWindow == null || !this.ShellWindow.IsLoaded) && this.Application != null;
			if (flag2)
			{
				this.Application.Shutdown();
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00020F55 File Offset: 0x0001F155
		private void ShowSplashScreen()
		{
			this.splashScreen = new SplashScreen("Resources/splashScreen.png");
			this.splashScreen.Show(false);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00020F75 File Offset: 0x0001F175
		private void CloseSplashScreen()
		{
			this.splashScreen.Close(new TimeSpan(100L));
		}

		// Token: 0x0400032B RID: 811
		private static bool? isInDesignMode;

		// Token: 0x0400032C RID: 812
		private Thread businessLogicHostThread;

		// Token: 0x0400032D RID: 813
		private LogicContext logicContext;

		// Token: 0x0400032E RID: 814
		private bool isLoggerConfigured;

		// Token: 0x0400032F RID: 815
		private SplashScreen splashScreen;
	}
}
