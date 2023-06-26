using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Resources;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000014 RID: 20
	internal static class InteractionContext
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000386C File Offset: 0x00001A6C
		static InteractionContext()
		{
			if (InteractionContext.runtimeAssembly != null)
			{
				InteractionContext.InitializeRuntimeNavigation();
				InteractionContext.LibraryName = (string)InteractionContext.libraryNamePropertyInfo.GetValue(InteractionContext.playerContextInstance, null);
				InteractionContext.LoadNavigationData(InteractionContext.LibraryName);
				return;
			}
			InteractionContext.InitalizePlatformNavigation();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000038CF File Offset: 0x00001ACF
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000038EA File Offset: 0x00001AEA
		public static object ActiveNavigationViewModelObject
		{
			get
			{
				return InteractionContext.activeNavigationViewModelObject ?? InteractionContext.activeNavigationViewModelPropertyInfo.GetValue(InteractionContext.playerContextInstance, null);
			}
			internal set
			{
				InteractionContext.activeNavigationViewModelObject = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000038F2 File Offset: 0x00001AF2
		private static bool IsPrototypingRuntimeLoaded
		{
			get
			{
				return InteractionContext.runtimeAssembly != null;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000038FF File Offset: 0x00001AFF
		private static bool CanGoBack
		{
			get
			{
				return (bool)InteractionContext.canGoBackPropertyInfo.GetValue(InteractionContext.ActiveNavigationViewModelObject, null);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003916 File Offset: 0x00001B16
		private static bool CanGoForward
		{
			get
			{
				return (bool)InteractionContext.canGoForwardPropertyInfo.GetValue(InteractionContext.ActiveNavigationViewModelObject, null);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000392D File Offset: 0x00001B2D
		public static void GoBack()
		{
			if (InteractionContext.IsPrototypingRuntimeLoaded)
			{
				if (InteractionContext.CanGoBack)
				{
					InteractionContext.goBackMethodInfo.Invoke(InteractionContext.ActiveNavigationViewModelObject, null);
					return;
				}
			}
			else
			{
				InteractionContext.PlatformGoBack();
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003954 File Offset: 0x00001B54
		public static void GoForward()
		{
			if (InteractionContext.IsPrototypingRuntimeLoaded)
			{
				if (InteractionContext.CanGoForward)
				{
					InteractionContext.goForwardMethodInfo.Invoke(InteractionContext.ActiveNavigationViewModelObject, null);
					return;
				}
			}
			else
			{
				InteractionContext.PlatformGoForward();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000397B File Offset: 0x00001B7B
		public static bool IsScreen(string screenName)
		{
			return InteractionContext.IsPrototypingRuntimeLoaded && InteractionContext.GetScreenClassName(screenName) != null;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003990 File Offset: 0x00001B90
		public static void GoToScreen(string screenName, Assembly assembly)
		{
			if (InteractionContext.IsPrototypingRuntimeLoaded)
			{
				string screenClassName = InteractionContext.GetScreenClassName(screenName);
				if (string.IsNullOrEmpty(screenClassName))
				{
					return;
				}
				object[] array = new object[] { screenClassName, true };
				InteractionContext.navigateToScreenMethodInfo.Invoke(InteractionContext.ActiveNavigationViewModelObject, array);
				return;
			}
			else
			{
				if (assembly == null)
				{
					return;
				}
				AssemblyName assemblyName = new AssemblyName(assembly.FullName);
				if (assemblyName != null)
				{
					InteractionContext.PlatformGoToScreen(assemblyName.Name, screenName);
				}
				return;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A00 File Offset: 0x00001C00
		public static void GoToState(string screen, string state)
		{
			if (string.IsNullOrEmpty(screen) || string.IsNullOrEmpty(state))
			{
				return;
			}
			if (InteractionContext.IsPrototypingRuntimeLoaded)
			{
				object[] array = new object[] { screen, state, false };
				InteractionContext.invokeStateChangeMethodInfo.Invoke(InteractionContext.ActiveNavigationViewModelObject, array);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A50 File Offset: 0x00001C50
		public static void PlaySketchFlowAnimation(string sketchFlowAnimation, string owningScreen)
		{
			if (string.IsNullOrEmpty(sketchFlowAnimation) || string.IsNullOrEmpty(owningScreen))
			{
				return;
			}
			if (InteractionContext.IsPrototypingRuntimeLoaded)
			{
				object value = InteractionContext.activeNavigationViewModelPropertyInfo.GetValue(InteractionContext.playerContextInstance, null);
				object[] array = new object[] { sketchFlowAnimation, owningScreen };
				InteractionContext.playSketchFlowAnimationMethodInfo.Invoke(value, array);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003AA4 File Offset: 0x00001CA4
		private static void InitializeRuntimeNavigation()
		{
			Type type = InteractionContext.runtimeAssembly.GetType("Microsoft.Expression.Prototyping.Services.PlayerContext");
			PropertyInfo property = type.GetProperty("Instance");
			InteractionContext.activeNavigationViewModelPropertyInfo = type.GetProperty("ActiveNavigationViewModel");
			InteractionContext.libraryNamePropertyInfo = type.GetProperty("LibraryName");
			InteractionContext.playerContextInstance = property.GetValue(null, null);
			Type type2 = InteractionContext.runtimeAssembly.GetType("Microsoft.Expression.Prototyping.Navigation.NavigationViewModel");
			InteractionContext.canGoBackPropertyInfo = type2.GetProperty("CanGoBack");
			InteractionContext.canGoForwardPropertyInfo = type2.GetProperty("CanGoForward");
			InteractionContext.goBackMethodInfo = type2.GetMethod("GoBack");
			InteractionContext.goForwardMethodInfo = type2.GetMethod("GoForward");
			InteractionContext.navigateToScreenMethodInfo = type2.GetMethod("NavigateToScreen");
			InteractionContext.invokeStateChangeMethodInfo = type2.GetMethod("InvokeStateChange");
			InteractionContext.playSketchFlowAnimationMethodInfo = type2.GetMethod("PlaySketchFlowAnimation");
			InteractionContext.sketchFlowAnimationPlayerPropertyInfo = type2.GetProperty("SketchFlowAnimationPlayer");
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B88 File Offset: 0x00001D88
		private static Serializer.Data LoadNavigationData(string assemblyName)
		{
			Serializer.Data data = null;
			if (InteractionContext.NavigationData.TryGetValue(assemblyName, out data))
			{
				return data;
			}
			Application application = Application.Current;
			string text = string.Format(CultureInfo.InvariantCulture, "/{0};component/Sketch.Flow", new object[] { assemblyName });
			try
			{
				StreamResourceInfo resourceStream = Application.GetResourceStream(new Uri(text, UriKind.Relative));
				if (resourceStream != null)
				{
					data = Serializer.Deserialize(resourceStream.Stream);
					InteractionContext.NavigationData[assemblyName] = data;
				}
			}
			catch (IOException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			return data ?? new Serializer.Data();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C20 File Offset: 0x00001E20
		private static string GetScreenClassName(string screenName)
		{
			Serializer.Data data = null;
			InteractionContext.NavigationData.TryGetValue(InteractionContext.LibraryName, out data);
			if (data == null || data.Screens == null)
			{
				return null;
			}
			if (!data.Screens.Any((Serializer.Data.Screen screen) => screen.ClassName == screenName))
			{
				screenName = (from screen in data.Screens
					where screen.DisplayName == screenName
					select screen.ClassName).FirstOrDefault<string>();
			}
			return screenName;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003CC0 File Offset: 0x00001EC0
		private static void InitalizePlatformNavigation()
		{
			NavigationWindow navigationWindow = Application.Current.MainWindow as NavigationWindow;
			if (navigationWindow != null)
			{
				InteractionContext.navigationService = navigationWindow.NavigationService;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003CEC File Offset: 0x00001EEC
		private static Assembly FindPlatformRuntimeAssembly()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (assembly.GetName().Name.Equals("Microsoft.Expression.Prototyping.Runtime"))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D30 File Offset: 0x00001F30
		public static void PlatformGoBack()
		{
			if (InteractionContext.navigationService != null && InteractionContext.PlatformCanGoBack)
			{
				InteractionContext.navigationService.GoBack();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D4A File Offset: 0x00001F4A
		public static void PlatformGoForward()
		{
			if (InteractionContext.navigationService != null && InteractionContext.PlatformCanGoForward)
			{
				InteractionContext.navigationService.GoForward();
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D64 File Offset: 0x00001F64
		public static void PlatformGoToScreen(string assemblyName, string screen)
		{
			ObjectHandle objectHandle = Activator.CreateInstance(assemblyName, screen);
			InteractionContext.navigationService.Navigate(objectHandle.Unwrap());
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003D8A File Offset: 0x00001F8A
		private static bool PlatformCanGoBack
		{
			get
			{
				return InteractionContext.navigationService != null && InteractionContext.navigationService.CanGoBack;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003D9F File Offset: 0x00001F9F
		private static bool PlatformCanGoForward
		{
			get
			{
				return InteractionContext.navigationService != null && InteractionContext.navigationService.CanGoForward;
			}
		}

		// Token: 0x0400002E RID: 46
		private static Assembly runtimeAssembly = InteractionContext.FindPlatformRuntimeAssembly();

		// Token: 0x0400002F RID: 47
		private static object playerContextInstance;

		// Token: 0x04000030 RID: 48
		private static object activeNavigationViewModelObject;

		// Token: 0x04000031 RID: 49
		private static PropertyInfo libraryNamePropertyInfo;

		// Token: 0x04000032 RID: 50
		private static PropertyInfo activeNavigationViewModelPropertyInfo;

		// Token: 0x04000033 RID: 51
		private static PropertyInfo canGoBackPropertyInfo;

		// Token: 0x04000034 RID: 52
		private static PropertyInfo canGoForwardPropertyInfo;

		// Token: 0x04000035 RID: 53
		private static PropertyInfo sketchFlowAnimationPlayerPropertyInfo;

		// Token: 0x04000036 RID: 54
		private static MethodInfo goBackMethodInfo;

		// Token: 0x04000037 RID: 55
		private static MethodInfo goForwardMethodInfo;

		// Token: 0x04000038 RID: 56
		private static MethodInfo navigateToScreenMethodInfo;

		// Token: 0x04000039 RID: 57
		private static MethodInfo invokeStateChangeMethodInfo;

		// Token: 0x0400003A RID: 58
		private static MethodInfo playSketchFlowAnimationMethodInfo;

		// Token: 0x0400003B RID: 59
		private static NavigationService navigationService;

		// Token: 0x0400003C RID: 60
		private static readonly string LibraryName;

		// Token: 0x0400003D RID: 61
		private static readonly Dictionary<string, Serializer.Data> NavigationData = new Dictionary<string, Serializer.Data>(StringComparer.OrdinalIgnoreCase);
	}
}
