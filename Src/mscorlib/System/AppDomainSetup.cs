using System;
using System.Collections.Generic;
using System.Deployment.Internal.Isolation.Manifest;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	// Token: 0x0200009E RID: 158
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	public sealed class AppDomainSetup : IAppDomainSetup
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		[SecuritySafeCritical]
		internal AppDomainSetup(AppDomainSetup copy, bool copyDomainBoundData)
		{
			string[] value = this.Value;
			if (copy != null)
			{
				string[] value2 = copy.Value;
				int num = this._Entries.Length;
				int num2 = value2.Length;
				int num3 = ((num2 < num) ? num2 : num);
				for (int i = 0; i < num3; i++)
				{
					value[i] = value2[i];
				}
				if (num3 < num)
				{
					for (int j = num3; j < num; j++)
					{
						value[j] = null;
					}
				}
				this._LoaderOptimization = copy._LoaderOptimization;
				this._AppDomainInitializerArguments = copy.AppDomainInitializerArguments;
				this._ActivationArguments = copy.ActivationArguments;
				this._ApplicationTrust = copy._ApplicationTrust;
				if (copyDomainBoundData)
				{
					this._AppDomainInitializer = copy.AppDomainInitializer;
				}
				else
				{
					this._AppDomainInitializer = null;
				}
				this._ConfigurationBytes = copy.GetConfigurationBytes();
				this._DisableInterfaceCache = copy._DisableInterfaceCache;
				this._AppDomainManagerAssembly = copy.AppDomainManagerAssembly;
				this._AppDomainManagerType = copy.AppDomainManagerType;
				this._AptcaVisibleAssemblies = copy.PartialTrustVisibleAssemblies;
				if (copy._CompatFlags != null)
				{
					this.SetCompatibilitySwitches(copy._CompatFlags.Keys);
				}
				if (copy._AppDomainSortingSetupInfo != null)
				{
					this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo(copy._AppDomainSortingSetupInfo);
				}
				this._TargetFrameworkName = copy._TargetFrameworkName;
				this._UseRandomizedStringHashing = copy._UseRandomizedStringHashing;
				return;
			}
			this._LoaderOptimization = LoaderOptimization.NotSpecified;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class.</summary>
		// Token: 0x060008EA RID: 2282 RVA: 0x0001D828 File Offset: 0x0001BA28
		public AppDomainSetup()
		{
			this._LoaderOptimization = LoaderOptimization.NotSpecified;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation context to use for manifest-based activation of an application domain.</summary>
		/// <param name="activationContext">The activation context to be used for an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is <see langword="null" />.</exception>
		// Token: 0x060008EB RID: 2283 RVA: 0x0001D837 File Offset: 0x0001BA37
		public AppDomainSetup(ActivationContext activationContext)
			: this(new ActivationArguments(activationContext))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation arguments required for manifest-based activation of an application domain.</summary>
		/// <param name="activationArguments">An object that specifies information required for the manifest-based activation of a new application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationArguments" /> is <see langword="null" />.</exception>
		// Token: 0x060008EC RID: 2284 RVA: 0x0001D848 File Offset: 0x0001BA48
		[SecuritySafeCritical]
		public AppDomainSetup(ActivationArguments activationArguments)
		{
			if (activationArguments == null)
			{
				throw new ArgumentNullException("activationArguments");
			}
			this._LoaderOptimization = LoaderOptimization.NotSpecified;
			this.ActivationArguments = activationArguments;
			string entryPointFullPath = CmsUtils.GetEntryPointFullPath(activationArguments);
			if (!string.IsNullOrEmpty(entryPointFullPath))
			{
				this.SetupDefaults(entryPointFullPath, false);
				return;
			}
			this.ApplicationBase = activationArguments.ActivationContext.ApplicationDirectory;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		internal void SetupDefaults(string imageLocation, bool imageLocationAlreadyNormalized = false)
		{
			char[] array = new char[] { '\\', '/' };
			int num = imageLocation.LastIndexOfAny(array);
			if (num == -1)
			{
				this.ApplicationName = imageLocation;
			}
			else
			{
				this.ApplicationName = imageLocation.Substring(num + 1);
				string text = imageLocation.Substring(0, num + 1);
				if (imageLocationAlreadyNormalized)
				{
					this.Value[0] = text;
				}
				else
				{
					this.ApplicationBase = text;
				}
			}
			this.ConfigurationFile = this.ApplicationName + AppDomainSetup.ConfigurationExtension;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0001D918 File Offset: 0x0001BB18
		internal string[] Value
		{
			get
			{
				if (this._Entries == null)
				{
					this._Entries = new string[18];
				}
				return this._Entries;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001D935 File Offset: 0x0001BB35
		internal string GetUnsecureApplicationBase()
		{
			return this.Value[0];
		}

		/// <summary>Gets or sets the display name of the assembly that provides the type of the application domain manager for application domains created using this <see cref="T:System.AppDomainSetup" /> object.</summary>
		/// <returns>The display name of the assembly that provides the <see cref="T:System.Type" /> of the application domain manager.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0001D93F File Offset: 0x0001BB3F
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0001D947 File Offset: 0x0001BB47
		public string AppDomainManagerAssembly
		{
			get
			{
				return this._AppDomainManagerAssembly;
			}
			set
			{
				this._AppDomainManagerAssembly = value;
			}
		}

		/// <summary>Gets or sets the full name of the type that provides the application domain manager for application domains created using this <see cref="T:System.AppDomainSetup" /> object.</summary>
		/// <returns>The full name of the type, including the namespace.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001D950 File Offset: 0x0001BB50
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x0001D958 File Offset: 0x0001BB58
		public string AppDomainManagerType
		{
			get
			{
				return this._AppDomainManagerType;
			}
			set
			{
				this._AppDomainManagerType = value;
			}
		}

		/// <summary>Gets or sets a list of assemblies marked with the <see cref="F:System.Security.PartialTrustVisibilityLevel.NotVisibleByDefault" /> flag that are made visible to partial-trust code running in a sandboxed application domain.</summary>
		/// <returns>An array of partial assembly names, where each partial name consists of the simple assembly name and the public key.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0001D961 File Offset: 0x0001BB61
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0001D969 File Offset: 0x0001BB69
		public string[] PartialTrustVisibleAssemblies
		{
			get
			{
				return this._AptcaVisibleAssemblies;
			}
			set
			{
				if (value != null)
				{
					this._AptcaVisibleAssemblies = (string[])value.Clone();
					Array.Sort<string>(this._AptcaVisibleAssemblies, StringComparer.OrdinalIgnoreCase);
					return;
				}
				this._AptcaVisibleAssemblies = null;
			}
		}

		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>The name of the application base directory.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0001D997 File Offset: 0x0001BB97
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0001D9A6 File Offset: 0x0001BBA6
		public string ApplicationBase
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.GetUnsecureApplicationBase(), false);
			}
			set
			{
				this.Value[0] = this.NormalizePath(value, false);
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		[SecuritySafeCritical]
		private string NormalizePath(string path, bool useAppBase)
		{
			if (path == null)
			{
				return null;
			}
			if (!useAppBase)
			{
				path = URLString.PreProcessForExtendedPathRemoval(false, path, false);
			}
			int num = path.Length;
			if (num == 0)
			{
				return null;
			}
			bool flag = false;
			if (num > 7 && string.Compare(path, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
			{
				int num2;
				if (path[6] == '\\')
				{
					if (path[7] == '\\' || path[7] == '/')
					{
						if (num > 8 && (path[8] == '\\' || path[8] == '/'))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
						}
						num2 = 8;
					}
					else
					{
						num2 = 5;
						flag = true;
					}
				}
				else if (path[7] == '/')
				{
					num2 = 8;
				}
				else
				{
					if (num > 8 && path[7] == '\\' && path[8] == '\\')
					{
						num2 = 7;
					}
					else
					{
						num2 = 5;
						StringBuilder stringBuilder = new StringBuilder(num);
						for (int i = 0; i < num; i++)
						{
							char c = path[i];
							if (c == '/')
							{
								stringBuilder.Append('\\');
							}
							else
							{
								stringBuilder.Append(c);
							}
						}
						path = stringBuilder.ToString();
					}
					flag = true;
				}
				path = path.Substring(num2);
				num -= num2;
			}
			bool flag2;
			if (flag || (num > 1 && (path[0] == '/' || path[0] == '\\') && (path[1] == '/' || path[1] == '\\')))
			{
				flag2 = false;
			}
			else
			{
				int num3 = path.IndexOf(':') + 1;
				flag2 = num3 == 0 || num <= num3 + 1 || (path[num3] != '/' && path[num3] != '\\') || (path[num3 + 1] != '/' && path[num3 + 1] != '\\');
			}
			if (flag2)
			{
				if (useAppBase && (num == 1 || path[1] != ':'))
				{
					string text = this.Value[0];
					if (text == null || text.Length == 0)
					{
						throw new MemberAccessException(Environment.GetResourceString("AppDomain_AppBaseNotSet"));
					}
					StringBuilder stringBuilder2 = StringBuilderCache.Acquire(16);
					bool flag3 = false;
					if (path[0] == '/' || path[0] == '\\')
					{
						string text2 = AppDomain.NormalizePath(text, false);
						text2 = text2.Substring(0, PathInternal.GetRootLength(text2));
						if (text2.Length == 0)
						{
							int j = text.IndexOf(":/", StringComparison.Ordinal);
							if (j == -1)
							{
								j = text.IndexOf(":\\", StringComparison.Ordinal);
							}
							int length = text.Length;
							for (j++; j < length; j++)
							{
								if (text[j] != '/' && text[j] != '\\')
								{
									break;
								}
							}
							while (j < length && text[j] != '/' && text[j] != '\\')
							{
								j++;
							}
							text2 = text.Substring(0, j);
						}
						stringBuilder2.Append(text2);
						flag3 = true;
					}
					else
					{
						stringBuilder2.Append(text);
					}
					int num4 = stringBuilder2.Length - 1;
					if (stringBuilder2[num4] != '/' && stringBuilder2[num4] != '\\')
					{
						if (!flag3)
						{
							if (text.IndexOf(":/", StringComparison.Ordinal) == -1)
							{
								stringBuilder2.Append('\\');
							}
							else
							{
								stringBuilder2.Append('/');
							}
						}
					}
					else if (flag3)
					{
						stringBuilder2.Remove(num4, 1);
					}
					stringBuilder2.Append(path);
					path = StringBuilderCache.GetStringAndRelease(stringBuilder2);
				}
				else
				{
					path = AppDomain.NormalizePath(path, true);
				}
			}
			return path;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001DD18 File Offset: 0x0001BF18
		private bool IsFilePath(string path)
		{
			return path[1] == ':' || (path[0] == '\\' && path[1] == '\\');
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0001DD3F File Offset: 0x0001BF3F
		internal static string ApplicationBaseKey
		{
			get
			{
				return "APPBASE";
			}
		}

		/// <summary>Gets or sets the name of the configuration file for an application domain.</summary>
		/// <returns>The name of the configuration file.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001DD46 File Offset: 0x0001BF46
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001DD57 File Offset: 0x0001BF57
		public string ConfigurationFile
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[1], true);
			}
			set
			{
				this.Value[1] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001DD62 File Offset: 0x0001BF62
		internal string ConfigurationFileInternal
		{
			get
			{
				return this.NormalizePath(this.Value[1], true);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001DD73 File Offset: 0x0001BF73
		internal static string ConfigurationFileKey
		{
			get
			{
				return "APP_CONFIG_FILE";
			}
		}

		/// <summary>Returns the XML configuration information set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, which overrides the application's XML configuration information.</summary>
		/// <returns>An array that contains the XML configuration information that was set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, or <see langword="null" /> if the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method has not been called.</returns>
		// Token: 0x060008FF RID: 2303 RVA: 0x0001DD7A File Offset: 0x0001BF7A
		public byte[] GetConfigurationBytes()
		{
			if (this._ConfigurationBytes == null)
			{
				return null;
			}
			return (byte[])this._ConfigurationBytes.Clone();
		}

		/// <summary>Provides XML configuration information for the application domain, replacing the application's XML configuration information.</summary>
		/// <param name="value">An array that contains the XML configuration information to be used for the application domain.</param>
		// Token: 0x06000900 RID: 2304 RVA: 0x0001DD96 File Offset: 0x0001BF96
		public void SetConfigurationBytes(byte[] value)
		{
			this._ConfigurationBytes = value;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0001DD9F File Offset: 0x0001BF9F
		private static string ConfigurationBytesKey
		{
			get
			{
				return "APP_CONFIG_BLOB";
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001DDA6 File Offset: 0x0001BFA6
		internal Dictionary<string, object> GetCompatibilityFlags()
		{
			return this._CompatFlags;
		}

		/// <summary>Sets the specified switches, making the application domain compatible with previous versions of the .NET Framework for the specified issues.</summary>
		/// <param name="switches">An enumerable set of string values that specify compatibility switches, or <see langword="null" /> to erase the existing compatibility switches.</param>
		// Token: 0x06000903 RID: 2307 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public void SetCompatibilitySwitches(IEnumerable<string> switches)
		{
			if (this._AppDomainSortingSetupInfo != null)
			{
				this._AppDomainSortingSetupInfo._useV2LegacySorting = false;
				this._AppDomainSortingSetupInfo._useV4LegacySorting = false;
			}
			this._UseRandomizedStringHashing = false;
			if (switches != null)
			{
				this._CompatFlags = new Dictionary<string, object>();
				using (IEnumerator<string> enumerator = switches.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (StringComparer.OrdinalIgnoreCase.Equals("NetFx40_Legacy20SortingBehavior", text))
						{
							if (this._AppDomainSortingSetupInfo == null)
							{
								this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
							}
							this._AppDomainSortingSetupInfo._useV2LegacySorting = true;
						}
						if (StringComparer.OrdinalIgnoreCase.Equals("NetFx45_Legacy40SortingBehavior", text))
						{
							if (this._AppDomainSortingSetupInfo == null)
							{
								this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
							}
							this._AppDomainSortingSetupInfo._useV4LegacySorting = true;
						}
						if (StringComparer.OrdinalIgnoreCase.Equals("UseRandomizedStringHashAlgorithm", text))
						{
							this._UseRandomizedStringHashing = true;
						}
						this._CompatFlags.Add(text, null);
					}
					return;
				}
			}
			this._CompatFlags = null;
		}

		/// <summary>Gets or sets a string that specifies the target version and profile of the .NET Framework for the application domain, in a format that can be parsed by the <see cref="M:System.Runtime.Versioning.FrameworkName.#ctor(System.String)" /> constructor.</summary>
		/// <returns>The target version and profile of the .NET Framework.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001DEC0 File Offset: 0x0001C0C0
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		public string TargetFrameworkName
		{
			get
			{
				return this._TargetFrameworkName;
			}
			set
			{
				this._TargetFrameworkName = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001DED1 File Offset: 0x0001C0D1
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x0001DED9 File Offset: 0x0001C0D9
		internal bool CheckedForTargetFrameworkName
		{
			get
			{
				return this._CheckedForTargetFrameworkName;
			}
			set
			{
				this._CheckedForTargetFrameworkName = value;
			}
		}

		/// <summary>Provides the common language runtime with an alternate implementation of a string comparison function.</summary>
		/// <param name="functionName">The name of the string comparison function to override.</param>
		/// <param name="functionVersion">The function version. For .NET Framework 4.5, its value must be 1 or greater.</param>
		/// <param name="functionPointer">A pointer to the function that overrides <paramref name="functionName" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="functionName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="functionVersion" /> is not 1 or greater.  
		/// -or-  
		/// <paramref name="functionPointer" /> is <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x06000908 RID: 2312 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		[SecurityCritical]
		public void SetNativeFunction(string functionName, int functionVersion, IntPtr functionPointer)
		{
			if (functionName == null)
			{
				throw new ArgumentNullException("functionName");
			}
			if (functionPointer == IntPtr.Zero)
			{
				throw new ArgumentNullException("functionPointer");
			}
			if (string.IsNullOrWhiteSpace(functionName))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"), "functionName");
			}
			if (functionVersion < 1)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_MinSortingVersion", new object[] { 1, functionName }));
			}
			if (this._AppDomainSortingSetupInfo == null)
			{
				this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
			}
			if (string.Equals(functionName, "IsNLSDefinedString", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnIsNLSDefinedString = functionPointer;
			}
			if (string.Equals(functionName, "CompareStringEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnCompareStringEx = functionPointer;
			}
			if (string.Equals(functionName, "LCMapStringEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnLCMapStringEx = functionPointer;
			}
			if (string.Equals(functionName, "FindNLSStringEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnFindNLSStringEx = functionPointer;
			}
			if (string.Equals(functionName, "CompareStringOrdinal", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnCompareStringOrdinal = functionPointer;
			}
			if (string.Equals(functionName, "GetNLSVersionEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnGetNLSVersionEx = functionPointer;
			}
			if (string.Equals(functionName, "FindStringOrdinal", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnFindStringOrdinal = functionPointer;
			}
		}

		/// <summary>Gets or sets the base directory where the directory for dynamically generated files is located.</summary>
		/// <returns>The directory where the <see cref="P:System.AppDomain.DynamicDirectory" /> is located.  
		///
		///  The return value of this property is different from the value assigned.</returns>
		/// <exception cref="T:System.MemberAccessException">This property cannot be set because the application name on the application domain is <see langword="null" />.</exception>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001E024 File Offset: 0x0001C224
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0001E038 File Offset: 0x0001C238
		public string DynamicBase
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[2], true);
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					this.Value[2] = null;
					return;
				}
				if (this.ApplicationName == null)
				{
					throw new MemberAccessException(Environment.GetResourceString("AppDomain_RequireApplicationName"));
				}
				StringBuilder stringBuilder = new StringBuilder(this.NormalizePath(value, false));
				stringBuilder.Append('\\');
				string text = ParseNumbers.IntToString(this.ApplicationName.GetLegacyNonRandomizedHashCode(), 16, 8, '0', 256);
				stringBuilder.Append(text);
				this.Value[2] = stringBuilder.ToString();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001E0B2 File Offset: 0x0001C2B2
		internal static string DynamicBaseKey
		{
			get
			{
				return "DYNAMIC_BASE";
			}
		}

		/// <summary>Gets or sets a value that indicates whether the &lt;publisherPolicy&gt; section of the configuration file is applied to an application domain.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="&lt;publisherPolicy&gt;" /> section of the configuration file for an application domain is ignored; <see langword="false" /> if the declared publisher policy is honored.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001E0B9 File Offset: 0x0001C2B9
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0001E0C7 File Offset: 0x0001C2C7
		public bool DisallowPublisherPolicy
		{
			get
			{
				return this.Value[11] != null;
			}
			set
			{
				if (value)
				{
					this.Value[11] = "true";
					return;
				}
				this.Value[11] = null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether an application domain allows assembly binding redirection.</summary>
		/// <returns>
		///   <see langword="true" /> if redirection of assemblies is not allowed; <see langword="false" /> if it is allowed.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001E0E5 File Offset: 0x0001C2E5
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0001E0F3 File Offset: 0x0001C2F3
		public bool DisallowBindingRedirects
		{
			get
			{
				return this.Value[13] != null;
			}
			set
			{
				if (value)
				{
					this.Value[13] = "true";
					return;
				}
				this.Value[13] = null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether HTTP download of assemblies is allowed for an application domain.</summary>
		/// <returns>
		///   <see langword="true" /> if HTTP download of assemblies is not allowed; <see langword="false" /> if it is allowed.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001E111 File Offset: 0x0001C311
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0001E11F File Offset: 0x0001C31F
		public bool DisallowCodeDownload
		{
			get
			{
				return this.Value[12] != null;
			}
			set
			{
				if (value)
				{
					this.Value[12] = "true";
					return;
				}
				this.Value[12] = null;
			}
		}

		/// <summary>Specifies whether the application base path and private binary path are probed when searching for assemblies to load.</summary>
		/// <returns>
		///   <see langword="true" /> if probing is not allowed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001E13D File Offset: 0x0001C33D
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0001E14B File Offset: 0x0001C34B
		public bool DisallowApplicationBaseProbing
		{
			get
			{
				return this.Value[14] != null;
			}
			set
			{
				if (value)
				{
					this.Value[14] = "true";
					return;
				}
				this.Value[14] = null;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001E169 File Offset: 0x0001C369
		[SecurityCritical]
		private string VerifyDir(string dir, bool normalize)
		{
			if (dir != null)
			{
				if (dir.Length == 0)
				{
					dir = null;
				}
				else
				{
					if (normalize)
					{
						dir = this.NormalizePath(dir, true);
					}
					if (this.IsFilePath(dir))
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[] { dir }, false, false).Demand();
					}
				}
			}
			return dir;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		[SecurityCritical]
		private void VerifyDirList(string dirs)
		{
			if (dirs != null)
			{
				string[] array = dirs.Split(new char[] { ';' });
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					this.VerifyDir(array[i], true);
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0001E20C File Offset: 0x0001C40C
		internal string DeveloperPath
		{
			[SecurityCritical]
			get
			{
				string text = this.Value[3];
				this.VerifyDirList(text);
				return text;
			}
			set
			{
				if (value == null)
				{
					this.Value[3] = null;
					return;
				}
				string[] array = value.Split(new char[] { ';' });
				int num = array.Length;
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				bool flag = false;
				for (int i = 0; i < num; i++)
				{
					if (array[i].Length != 0)
					{
						if (flag)
						{
							stringBuilder.Append(";");
						}
						else
						{
							flag = true;
						}
						stringBuilder.Append(Path.GetFullPathInternal(array[i]));
					}
				}
				string stringAndRelease = StringBuilderCache.GetStringAndRelease(stringBuilder);
				if (stringAndRelease.Length == 0)
				{
					this.Value[3] = null;
					return;
				}
				this.Value[3] = stringAndRelease;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001E2A9 File Offset: 0x0001C4A9
		internal static string DisallowPublisherPolicyKey
		{
			get
			{
				return "DISALLOW_APP";
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		internal static string DisallowCodeDownloadKey
		{
			get
			{
				return "CODE_DOWNLOAD_DISABLED";
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001E2B7 File Offset: 0x0001C4B7
		internal static string DisallowBindingRedirectsKey
		{
			get
			{
				return "DISALLOW_APP_REDIRECTS";
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001E2BE File Offset: 0x0001C4BE
		internal static string DeveloperPathKey
		{
			get
			{
				return "DEV_PATH";
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001E2C5 File Offset: 0x0001C4C5
		internal static string DisallowAppBaseProbingKey
		{
			get
			{
				return "DISALLOW_APP_BASE_PROBING";
			}
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001E2CC File Offset: 0x0001C4CC
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0001E2D6 File Offset: 0x0001C4D6
		public string ApplicationName
		{
			get
			{
				return this.Value[4];
			}
			set
			{
				this.Value[4] = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001E2E1 File Offset: 0x0001C4E1
		internal static string ApplicationNameKey
		{
			get
			{
				return "APP_NAME";
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.AppDomainInitializer" /> delegate, which represents a callback method that is invoked when the application domain is initialized.</summary>
		/// <returns>A delegate that represents a callback method that is invoked when the application domain is initialized.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
		[XmlIgnoreMember]
		public AppDomainInitializer AppDomainInitializer
		{
			get
			{
				return this._AppDomainInitializer;
			}
			set
			{
				this._AppDomainInitializer = value;
			}
		}

		/// <summary>Gets or sets the arguments passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate. The callback method is invoked when the application domain is initialized.</summary>
		/// <returns>An array of strings that is passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate, when the callback method is invoked during <see cref="T:System.AppDomain" /> initialization.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001E2F9 File Offset: 0x0001C4F9
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0001E301 File Offset: 0x0001C501
		public string[] AppDomainInitializerArguments
		{
			get
			{
				return this._AppDomainInitializerArguments;
			}
			set
			{
				this._AppDomainInitializerArguments = value;
			}
		}

		/// <summary>Gets or sets data about the activation of an application domain.</summary>
		/// <returns>An object that contains data about the activation of an application domain.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object whose application identity does not match the application identity of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object returned by the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property is <see langword="null" />.</exception>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001E30A File Offset: 0x0001C50A
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0001E312 File Offset: 0x0001C512
		[XmlIgnoreMember]
		public ActivationArguments ActivationArguments
		{
			get
			{
				return this._ActivationArguments;
			}
			set
			{
				this._ActivationArguments = value;
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001E31C File Offset: 0x0001C51C
		internal ApplicationTrust InternalGetApplicationTrust()
		{
			if (this._ApplicationTrust == null)
			{
				return null;
			}
			SecurityElement securityElement = SecurityElement.FromString(this._ApplicationTrust);
			ApplicationTrust applicationTrust = new ApplicationTrust();
			applicationTrust.FromXml(securityElement);
			return applicationTrust;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001E34D File Offset: 0x0001C54D
		internal void InternalSetApplicationTrust(ApplicationTrust value)
		{
			if (value != null)
			{
				this._ApplicationTrust = value.ToXml().ToString();
				return;
			}
			this._ApplicationTrust = null;
		}

		/// <summary>Gets or sets an object containing security and trust information.</summary>
		/// <returns>An object that contains security and trust information.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Security.Policy.ApplicationTrust" /> object whose application identity does not match the application identity of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object returned by the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property is set to <see langword="null" />.</exception>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001E36B File Offset: 0x0001C56B
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0001E373 File Offset: 0x0001C573
		[XmlIgnoreMember]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				return this.InternalGetApplicationTrust();
			}
			set
			{
				this.InternalSetApplicationTrust(value);
			}
		}

		/// <summary>Gets or sets the list of directories under the application base directory that are probed for private assemblies.</summary>
		/// <returns>A list of directory names separated by semicolons.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001E37C File Offset: 0x0001C57C
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0001E39A File Offset: 0x0001C59A
		public string PrivateBinPath
		{
			[SecuritySafeCritical]
			get
			{
				string text = this.Value[5];
				this.VerifyDirList(text);
				return text;
			}
			set
			{
				this.Value[5] = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001E3A5 File Offset: 0x0001C5A5
		internal static string PrivateBinPathKey
		{
			get
			{
				return "PRIVATE_BINPATH";
			}
		}

		/// <summary>Gets or sets a string value that includes or excludes <see cref="P:System.AppDomainSetup.ApplicationBase" /> from the search path for the application, and searches only <see cref="P:System.AppDomainSetup.PrivateBinPath" />.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) to include the application base path when searching for assemblies; any non-null string value to exclude the path. The default value is <see langword="null" />.</returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0001E3B6 File Offset: 0x0001C5B6
		public string PrivateBinPathProbe
		{
			get
			{
				return this.Value[6];
			}
			set
			{
				this.Value[6] = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001E3C1 File Offset: 0x0001C5C1
		internal static string PrivateBinPathProbeKey
		{
			get
			{
				return "BINPATH_PROBE_ONLY";
			}
		}

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A list of directory names separated by semicolons.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0001E3E6 File Offset: 0x0001C5E6
		public string ShadowCopyDirectories
		{
			[SecuritySafeCritical]
			get
			{
				string text = this.Value[7];
				this.VerifyDirList(text);
				return text;
			}
			set
			{
				this.Value[7] = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001E3F1 File Offset: 0x0001C5F1
		internal static string ShadowCopyDirectoriesKey
		{
			get
			{
				return "SHADOW_COPY_DIRS";
			}
		}

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>The string value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0001E402 File Offset: 0x0001C602
		public string ShadowCopyFiles
		{
			get
			{
				return this.Value[8];
			}
			set
			{
				if (value != null && string.Compare(value, "true", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Value[8] = value;
					return;
				}
				this.Value[8] = null;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001E428 File Offset: 0x0001C628
		internal static string ShadowCopyFilesKey
		{
			get
			{
				return "FORCE_CACHE_INSTALL";
			}
		}

		/// <summary>Gets or sets the name of an area specific to the application where files are shadow copied.</summary>
		/// <returns>The fully qualified name of the directory path and file name where files are shadow copied.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001E42F File Offset: 0x0001C62F
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0001E441 File Offset: 0x0001C641
		public string CachePath
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[9], false);
			}
			set
			{
				this.Value[9] = this.NormalizePath(value, false);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001E454 File Offset: 0x0001C654
		internal static string CachePathKey
		{
			get
			{
				return "CACHE_BASE";
			}
		}

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>The location and name of the license file.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001E45B File Offset: 0x0001C65B
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0001E46D File Offset: 0x0001C66D
		public string LicenseFile
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[10], true);
			}
			set
			{
				this.Value[10] = value;
			}
		}

		/// <summary>Specifies the optimization policy used to load an executable.</summary>
		/// <returns>An enumerated constant that is used with the <see cref="T:System.LoaderOptimizationAttribute" />.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001E479 File Offset: 0x0001C679
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0001E481 File Offset: 0x0001C681
		public LoaderOptimization LoaderOptimization
		{
			get
			{
				return this._LoaderOptimization;
			}
			set
			{
				this._LoaderOptimization = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001E48A File Offset: 0x0001C68A
		internal static string LoaderOptimizationKey
		{
			get
			{
				return "LOADER_OPTIMIZATION";
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001E491 File Offset: 0x0001C691
		internal static string ConfigurationExtension
		{
			get
			{
				return ".config";
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001E498 File Offset: 0x0001C698
		internal static string PrivateBinPathEnvironmentVariable
		{
			get
			{
				return "RELPATH";
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001E49F File Offset: 0x0001C69F
		internal static string RuntimeConfigurationFile
		{
			get
			{
				return "config\\machine.config";
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001E4A6 File Offset: 0x0001C6A6
		internal static string MachineConfigKey
		{
			get
			{
				return "MACHINE_CONFIG";
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001E4AD File Offset: 0x0001C6AD
		internal static string HostBindingKey
		{
			get
			{
				return "HOST_CONFIG";
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		[SecurityCritical]
		internal bool UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, string UpdatedField, IntPtr fusionContext, AppDomainSetup oldADS)
		{
			string text = this.Value[(int)FieldValue];
			string text2 = ((oldADS == null) ? null : oldADS.Value[(int)FieldValue]);
			if (text != text2)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (UpdatedField == null) ? text : UpdatedField);
				return true;
			}
			return false;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001E4F6 File Offset: 0x0001C6F6
		[SecurityCritical]
		internal void UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, IntPtr fusionContext, AppDomainSetup oldADS)
		{
			if (this.Value[(int)FieldValue] != null)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, "true");
				return;
			}
			if (oldADS != null && oldADS.Value[(int)FieldValue] != null)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, "false");
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001E52C File Offset: 0x0001C72C
		[SecurityCritical]
		internal static bool ByteArraysAreDifferent(byte[] A, byte[] B)
		{
			int num = A.Length;
			if (num != B.Length)
			{
				return true;
			}
			for (int i = 0; i < num; i++)
			{
				if (A[i] != B[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001E55C File Offset: 0x0001C75C
		[SecurityCritical]
		internal static void UpdateByteArrayContextPropertyIfNeeded(byte[] NewArray, byte[] OldArray, string FieldKey, IntPtr fusionContext)
		{
			if ((NewArray != null && OldArray == null) || (NewArray == null && OldArray != null) || (NewArray != null && OldArray != null && AppDomainSetup.ByteArraysAreDifferent(NewArray, OldArray)))
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, NewArray);
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001E584 File Offset: 0x0001C784
		[SecurityCritical]
		internal void SetupFusionContext(IntPtr fusionContext, AppDomainSetup oldADS)
		{
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationBaseValue, AppDomainSetup.ApplicationBaseKey, null, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathValue, AppDomainSetup.PrivateBinPathKey, null, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DevPathValue, AppDomainSetup.DeveloperPathKey, null, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowPublisherPolicyValue, AppDomainSetup.DisallowPublisherPolicyKey, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowCodeDownloadValue, AppDomainSetup.DisallowCodeDownloadKey, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowBindingRedirectsValue, AppDomainSetup.DisallowBindingRedirectsKey, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowAppBaseProbingValue, AppDomainSetup.DisallowAppBaseProbingKey, fusionContext, oldADS);
			if (this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyFilesValue, AppDomainSetup.ShadowCopyFilesKey, this.ShadowCopyFiles, fusionContext, oldADS))
			{
				if (this.Value[7] == null)
				{
					this.ShadowCopyDirectories = this.BuildShadowCopyDirectories();
				}
				this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyDirectoriesValue, AppDomainSetup.ShadowCopyDirectoriesKey, null, fusionContext, oldADS);
			}
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.CachePathValue, AppDomainSetup.CachePathKey, null, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathProbeValue, AppDomainSetup.PrivateBinPathProbeKey, this.PrivateBinPathProbe, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ConfigurationFileValue, AppDomainSetup.ConfigurationFileKey, null, fusionContext, oldADS);
			AppDomainSetup.UpdateByteArrayContextPropertyIfNeeded(this._ConfigurationBytes, (oldADS == null) ? null : oldADS.GetConfigurationBytes(), AppDomainSetup.ConfigurationBytesKey, fusionContext);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationNameValue, AppDomainSetup.ApplicationNameKey, this.ApplicationName, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DynamicBaseValue, AppDomainSetup.DynamicBaseKey, null, fusionContext, oldADS);
			AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.MachineConfigKey, RuntimeEnvironment.GetRuntimeDirectoryImpl() + AppDomainSetup.RuntimeConfigurationFile);
			string hostBindingFile = RuntimeEnvironment.GetHostBindingFile();
			if (hostBindingFile != null || oldADS != null)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.HostBindingKey, hostBindingFile);
			}
		}

		// Token: 0x06000948 RID: 2376
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateContextProperty(IntPtr fusionContext, string key, object value);

		// Token: 0x06000949 RID: 2377 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		internal static int Locate(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return -1;
			}
			char c = s[0];
			if (c <= 'L')
			{
				switch (c)
				{
				case 'A':
					if (s == "APP_CONFIG_FILE")
					{
						return 1;
					}
					if (s == "APP_NAME")
					{
						return 4;
					}
					if (s == "APPBASE")
					{
						return 0;
					}
					if (s == "APP_CONFIG_BLOB")
					{
						return 15;
					}
					break;
				case 'B':
					if (s == "BINPATH_PROBE_ONLY")
					{
						return 6;
					}
					break;
				case 'C':
					if (s == "CACHE_BASE")
					{
						return 9;
					}
					if (s == "CODE_DOWNLOAD_DISABLED")
					{
						return 12;
					}
					break;
				case 'D':
					if (s == "DEV_PATH")
					{
						return 3;
					}
					if (s == "DYNAMIC_BASE")
					{
						return 2;
					}
					if (s == "DISALLOW_APP")
					{
						return 11;
					}
					if (s == "DISALLOW_APP_REDIRECTS")
					{
						return 13;
					}
					if (s == "DISALLOW_APP_BASE_PROBING")
					{
						return 14;
					}
					break;
				case 'E':
					break;
				case 'F':
					if (s == "FORCE_CACHE_INSTALL")
					{
						return 8;
					}
					break;
				default:
					if (c == 'L')
					{
						if (s == "LICENSE_FILE")
						{
							return 10;
						}
					}
					break;
				}
			}
			else if (c != 'P')
			{
				if (c == 'S')
				{
					if (s == "SHADOW_COPY_DIRS")
					{
						return 7;
					}
				}
			}
			else if (s == "PRIVATE_BINPATH")
			{
				return 5;
			}
			return -1;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001E84C File Offset: 0x0001CA4C
		private string BuildShadowCopyDirectories()
		{
			string text = this.Value[5];
			if (text == null)
			{
				return null;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			string text2 = this.Value[0];
			if (text2 != null)
			{
				char[] array = new char[] { ';' };
				string[] array2 = text.Split(array);
				int num = array2.Length;
				bool flag = text2[text2.Length - 1] != '/' && text2[text2.Length - 1] != '\\';
				if (num == 0)
				{
					stringBuilder.Append(text2);
					if (flag)
					{
						stringBuilder.Append('\\');
					}
					stringBuilder.Append(text);
				}
				else
				{
					for (int i = 0; i < num; i++)
					{
						stringBuilder.Append(text2);
						if (flag)
						{
							stringBuilder.Append('\\');
						}
						stringBuilder.Append(array2[i]);
						if (i < num - 1)
						{
							stringBuilder.Append(';');
						}
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Gets or sets a value that indicates whether interface caching is disabled for interop calls in the application domain, so that a QueryInterface is performed on each call.</summary>
		/// <returns>
		///   <see langword="true" /> if interface caching is disabled for interop calls in application domains created with the current <see cref="T:System.AppDomainSetup" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001E931 File Offset: 0x0001CB31
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0001E939 File Offset: 0x0001CB39
		public bool SandboxInterop
		{
			get
			{
				return this._DisableInterfaceCache;
			}
			set
			{
				this._DisableInterfaceCache = value;
			}
		}

		// Token: 0x040003A6 RID: 934
		private string[] _Entries;

		// Token: 0x040003A7 RID: 935
		private LoaderOptimization _LoaderOptimization;

		// Token: 0x040003A8 RID: 936
		private string _AppBase;

		// Token: 0x040003A9 RID: 937
		[OptionalField(VersionAdded = 2)]
		private AppDomainInitializer _AppDomainInitializer;

		// Token: 0x040003AA RID: 938
		[OptionalField(VersionAdded = 2)]
		private string[] _AppDomainInitializerArguments;

		// Token: 0x040003AB RID: 939
		[OptionalField(VersionAdded = 2)]
		private ActivationArguments _ActivationArguments;

		// Token: 0x040003AC RID: 940
		[OptionalField(VersionAdded = 2)]
		private string _ApplicationTrust;

		// Token: 0x040003AD RID: 941
		[OptionalField(VersionAdded = 2)]
		private byte[] _ConfigurationBytes;

		// Token: 0x040003AE RID: 942
		[OptionalField(VersionAdded = 3)]
		private bool _DisableInterfaceCache;

		// Token: 0x040003AF RID: 943
		[OptionalField(VersionAdded = 4)]
		private string _AppDomainManagerAssembly;

		// Token: 0x040003B0 RID: 944
		[OptionalField(VersionAdded = 4)]
		private string _AppDomainManagerType;

		// Token: 0x040003B1 RID: 945
		[OptionalField(VersionAdded = 4)]
		private string[] _AptcaVisibleAssemblies;

		// Token: 0x040003B2 RID: 946
		[OptionalField(VersionAdded = 4)]
		private Dictionary<string, object> _CompatFlags;

		// Token: 0x040003B3 RID: 947
		[OptionalField(VersionAdded = 5)]
		private string _TargetFrameworkName;

		// Token: 0x040003B4 RID: 948
		[NonSerialized]
		internal AppDomainSortingSetupInfo _AppDomainSortingSetupInfo;

		// Token: 0x040003B5 RID: 949
		[OptionalField(VersionAdded = 5)]
		private bool _CheckedForTargetFrameworkName;

		// Token: 0x040003B6 RID: 950
		[OptionalField(VersionAdded = 5)]
		private bool _UseRandomizedStringHashing;

		// Token: 0x02000ACE RID: 2766
		[Serializable]
		internal enum LoaderInformation
		{
			// Token: 0x04003103 RID: 12547
			ApplicationBaseValue,
			// Token: 0x04003104 RID: 12548
			ConfigurationFileValue,
			// Token: 0x04003105 RID: 12549
			DynamicBaseValue,
			// Token: 0x04003106 RID: 12550
			DevPathValue,
			// Token: 0x04003107 RID: 12551
			ApplicationNameValue,
			// Token: 0x04003108 RID: 12552
			PrivateBinPathValue,
			// Token: 0x04003109 RID: 12553
			PrivateBinPathProbeValue,
			// Token: 0x0400310A RID: 12554
			ShadowCopyDirectoriesValue,
			// Token: 0x0400310B RID: 12555
			ShadowCopyFilesValue,
			// Token: 0x0400310C RID: 12556
			CachePathValue,
			// Token: 0x0400310D RID: 12557
			LicenseFileValue,
			// Token: 0x0400310E RID: 12558
			DisallowPublisherPolicyValue,
			// Token: 0x0400310F RID: 12559
			DisallowCodeDownloadValue,
			// Token: 0x04003110 RID: 12560
			DisallowBindingRedirectsValue,
			// Token: 0x04003111 RID: 12561
			DisallowAppBaseProbingValue,
			// Token: 0x04003112 RID: 12562
			ConfigurationBytesValue,
			// Token: 0x04003113 RID: 12563
			LoaderMaximum = 18
		}
	}
}
