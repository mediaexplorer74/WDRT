﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000002 RID: 2
	public sealed class ApplicationInfo
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ApplicationInfo()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly == null;
			if (flag)
			{
				throw new InvalidOperationException("CouldNotRetrieveInformationOnCurrentlyRunningApplication");
			}
			this.applicationAssemblyName = ApplicationInfo.GetApplicationAssemblyName(entryAssembly);
			this.applicationAssemblyFilePath = ApplicationInfo.GetApplicationAssemblyFilePath(entryAssembly);
			this.applicationAssemblyDirectoryPath = Path.GetDirectoryName(this.applicationAssemblyFilePath);
			this.applicationAssemblyFileName = Path.GetFileName(this.applicationAssemblyFilePath);
			this.applicationName = ApplicationInfo.GetApplicationName(entryAssembly);
			this.applicationDisplayName = ApplicationInfo.GetApplicationDisplayName(entryAssembly);
			this.applicationVersion = ApplicationInfo.GetApplicationVersion(entryAssembly);
			this.companyName = ApplicationInfo.GetCompanyName(entryAssembly);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		public ApplicationInfo(string applicationAssemblyName, string applicationAssemblyFilePath, string applicationName, string applicationDisplayName, string applicationVersion, string companyName)
		{
			bool flag = applicationAssemblyName == null;
			if (flag)
			{
				throw new ArgumentNullException("applicationAssemblyName");
			}
			bool flag2 = applicationAssemblyFilePath == null;
			if (flag2)
			{
				throw new ArgumentNullException("applicationAssemblyFilePath");
			}
			bool flag3 = applicationName == null;
			if (flag3)
			{
				throw new ArgumentNullException("applicationName");
			}
			bool flag4 = applicationDisplayName == null;
			if (flag4)
			{
				throw new ArgumentNullException("applicationDisplayName");
			}
			bool flag5 = applicationVersion == null;
			if (flag5)
			{
				throw new ArgumentNullException("applicationVersion");
			}
			bool flag6 = companyName == null;
			if (flag6)
			{
				throw new ArgumentNullException("companyName");
			}
			this.applicationAssemblyName = applicationAssemblyName;
			this.applicationAssemblyDirectoryPath = Path.GetDirectoryName(applicationAssemblyFilePath);
			this.applicationAssemblyFileName = Path.GetFileName(applicationAssemblyFilePath);
			this.applicationAssemblyFilePath = applicationAssemblyFilePath;
			this.applicationName = applicationName;
			this.applicationDisplayName = applicationDisplayName;
			this.applicationVersion = applicationVersion;
			this.companyName = companyName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000021C8 File Offset: 0x000003C8
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002204 File Offset: 0x00000404
		public static CultureInfo CurrentLanguageInRegistry
		{
			get
			{
				string registryValue = ApplicationInfo.GetRegistryValue("CurrentLanguage");
				bool flag;
				CultureInfo cultureInfo = ApplicationInfo.TryGetCultureInfo(registryValue, out flag);
				bool flag2 = flag;
				if (flag2)
				{
					ApplicationInfo.SetRegistryValue("CurrentLanguage", cultureInfo);
				}
				return cultureInfo;
			}
			set
			{
				ApplicationInfo.SetRegistryValue("CurrentLanguage", value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002214 File Offset: 0x00000414
		public static CultureInfo DefaultLanguageInRegistry
		{
			get
			{
				string registryValue = ApplicationInfo.GetRegistryValue("DefaultLanguage");
				bool flag;
				CultureInfo cultureInfo = ApplicationInfo.TryGetCultureInfo(registryValue, out flag);
				bool flag2 = flag;
				if (flag2)
				{
					ApplicationInfo.SetRegistryValue("DefaultLanguage", cultureInfo);
				}
				return cultureInfo;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002250 File Offset: 0x00000450
		public string ApplicationAssemblyDirectoryPath
		{
			get
			{
				return this.applicationAssemblyDirectoryPath;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002268 File Offset: 0x00000468
		public string ApplicationAssemblyFileName
		{
			get
			{
				return this.applicationAssemblyFileName;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002280 File Offset: 0x00000480
		public string ApplicationAssemblyFilePath
		{
			get
			{
				return this.applicationAssemblyFilePath;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002298 File Offset: 0x00000498
		public string ApplicationAssemblyName
		{
			get
			{
				return this.applicationAssemblyName;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022B0 File Offset: 0x000004B0
		public string ApplicationName
		{
			get
			{
				return this.applicationName;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022C8 File Offset: 0x000004C8
		public string ApplicationDisplayName
		{
			get
			{
				return this.applicationDisplayName;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022E0 File Offset: 0x000004E0
		public string CompanyName
		{
			get
			{
				return this.companyName;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022F8 File Offset: 0x000004F8
		public string ApplicationVersion
		{
			get
			{
				return this.applicationVersion;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002310 File Offset: 0x00000510
		public static string GetRegistryValue(string name)
		{
			try
			{
				Tracer<ApplicationInfo>.WriteInformation("Looking for 64bit '{0}' registry value", new object[] { name });
				RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
				registryKey = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Care Suite\\Windows Device Recovery Tool");
				bool flag = registryKey != null;
				if (flag)
				{
					Tracer<ApplicationInfo>.WriteInformation("Found 64bit registry value");
					return (string)registryKey.GetValue(name);
				}
				Tracer<ApplicationInfo>.WriteInformation("Looking for 32bit '{0}' registry value", new object[] { name });
				registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
				registryKey = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Care Suite\\Windows Device Recovery Tool");
				bool flag2 = registryKey != null;
				if (flag2)
				{
					Tracer<ApplicationInfo>.WriteInformation("Found 32bit registry value");
					return (string)registryKey.GetValue(name);
				}
				Tracer<ApplicationInfo>.WriteInformation("Registry value not found!");
			}
			catch (Exception ex)
			{
				Tracer<ApplicationInfo>.WriteError("Error while looking for registry value", new object[] { ex });
			}
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000240C File Offset: 0x0000060C
		public static void SetRegistryValue(string name, object value)
		{
			try
			{
				Tracer<ApplicationInfo>.WriteInformation("Looking for 64bit registry value");
				RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
				registryKey = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Care Suite\\Windows Device Recovery Tool", true);
				bool flag = registryKey != null;
				if (flag)
				{
					Tracer<ApplicationInfo>.WriteInformation("Found 64bit registry value");
					registryKey.SetValue(name, value);
				}
				Tracer<ApplicationInfo>.WriteInformation("Looking for 32bit registry value");
				registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
				registryKey = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Care Suite\\Windows Device Recovery Tool", true);
				bool flag2 = registryKey != null;
				if (flag2)
				{
					Tracer<ApplicationInfo>.WriteInformation("Found 32bit registry value");
					registryKey.SetValue(name, value);
				}
				Tracer<ApplicationInfo>.WriteInformation("Registry value not found!");
			}
			catch (Exception ex)
			{
				Tracer<ApplicationInfo>.WriteError("Error while looking for registry value", new object[] { ex });
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024E0 File Offset: 0x000006E0
		public static bool IsInternal()
		{
			string registryValue = ApplicationInfo.GetRegistryValue("Internal");
			return registryValue == "0";
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002508 File Offset: 0x00000708
		public static bool UseTestServer()
		{
			string registryValue = ApplicationInfo.GetRegistryValue("TestServer");
			return ApplicationInfo.IsInternal() && registryValue == "0";
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000253C File Offset: 0x0000073C
		private static CultureInfo TryGetCultureInfo(string language, out bool shouldUpdateLanguage)
		{
			bool flag = !string.IsNullOrEmpty(language);
			CultureInfo cultureInfo;
			if (flag)
			{
				try
				{
					cultureInfo = CultureInfo.GetCultureInfo(language);
					shouldUpdateLanguage = false;
				}
				catch (Exception)
				{
					cultureInfo = CultureInfo.GetCultureInfo("en-US");
					shouldUpdateLanguage = true;
				}
			}
			else
			{
				cultureInfo = CultureInfo.GetCultureInfo("en-US");
				shouldUpdateLanguage = true;
			}
			return cultureInfo;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025A4 File Offset: 0x000007A4
		private static string GetApplicationAssemblyFilePath(Assembly assembly)
		{
			Uri uri = new Uri(assembly.CodeBase, UriKind.Absolute);
			return uri.LocalPath;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025CC File Offset: 0x000007CC
		private static string GetApplicationName(Assembly assembly)
		{
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			return assemblyName.Name;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025F0 File Offset: 0x000007F0
		private static string GetApplicationAssemblyName(Assembly assembly)
		{
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			return assemblyName.FullName;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002614 File Offset: 0x00000814
		private static string GetApplicationVersion(Assembly assembly)
		{
			AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute));
			bool flag = assemblyFileVersionAttribute == null;
			if (flag)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "InvalidOperationException_ErrorMessage_AssemblyNotMarkedWithAttribute", new object[0]));
			}
			return assemblyFileVersionAttribute.Version;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002668 File Offset: 0x00000868
		private static string GetApplicationDisplayName(Assembly assembly)
		{
			AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)assembly.GetCustomAttribute(typeof(AssemblyTitleAttribute));
			bool flag = assemblyTitleAttribute == null;
			if (flag)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Resources.InvalidOperationException_ErrorMessage_AssemblyNotMarkedWithAttribute", new object[0]));
			}
			return assemblyTitleAttribute.Title;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026BC File Offset: 0x000008BC
		private static string GetCompanyName(Assembly assembly)
		{
			AssemblyCompanyAttribute assemblyCompanyAttribute = (AssemblyCompanyAttribute)assembly.GetCustomAttribute(typeof(AssemblyCompanyAttribute));
			bool flag = assemblyCompanyAttribute == null;
			if (flag)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Resources.InvalidOperationException_ErrorMessage_AssemblyNotMarkedWithAttribute", new object[0]));
			}
			return assemblyCompanyAttribute.Company;
		}

		// Token: 0x04000001 RID: 1
		private const string Internal = "Internal";

		// Token: 0x04000002 RID: 2
		private const string TestServer = "TestServer";

		// Token: 0x04000003 RID: 3
		private const string CurrentLanguage = "CurrentLanguage";

		// Token: 0x04000004 RID: 4
		private const string DefaultLanguage = "DefaultLanguage";

		// Token: 0x04000005 RID: 5
		private const string KeyRegistry = "SOFTWARE\\Microsoft\\Care Suite\\Windows Device Recovery Tool";

		// Token: 0x04000006 RID: 6
		private readonly string applicationAssemblyName;

		// Token: 0x04000007 RID: 7
		private readonly string applicationAssemblyFilePath;

		// Token: 0x04000008 RID: 8
		private readonly string applicationName;

		// Token: 0x04000009 RID: 9
		private readonly string applicationDisplayName;

		// Token: 0x0400000A RID: 10
		private readonly string applicationVersion;

		// Token: 0x0400000B RID: 11
		private readonly string companyName;

		// Token: 0x0400000C RID: 12
		private readonly string applicationAssemblyDirectoryPath;

		// Token: 0x0400000D RID: 13
		private readonly string applicationAssemblyFileName;
	}
}
