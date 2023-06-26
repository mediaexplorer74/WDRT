using System;
using System.Collections;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Provides information about a specific culture (called a locale for unmanaged code development). The information includes the names for the culture, the writing system, the calendar used, the sort order of strings, and formatting for dates and numbers.</summary>
	// Token: 0x020003A7 RID: 935
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		// Token: 0x06002E62 RID: 11874 RVA: 0x000B27EC File Offset: 0x000B09EC
		private static bool Init()
		{
			if (CultureInfo.s_InvariantCultureInfo == null)
			{
				CultureInfo.s_InvariantCultureInfo = new CultureInfo("", false)
				{
					m_isReadOnly = true
				};
			}
			CultureInfo.s_userDefaultCulture = (CultureInfo.s_userDefaultUICulture = CultureInfo.s_InvariantCultureInfo);
			CultureInfo.s_userDefaultCulture = CultureInfo.InitUserDefaultCulture();
			CultureInfo.s_userDefaultUICulture = CultureInfo.InitUserDefaultUICulture();
			return true;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000B284C File Offset: 0x000B0A4C
		[SecuritySafeCritical]
		private static CultureInfo InitUserDefaultCulture()
		{
			string text = CultureInfo.GetDefaultLocaleName(1024);
			if (text == null)
			{
				text = CultureInfo.GetDefaultLocaleName(2048);
				if (text == null)
				{
					return CultureInfo.InvariantCulture;
				}
			}
			CultureInfo cultureByName = CultureInfo.GetCultureByName(text, true);
			cultureByName.m_isReadOnly = true;
			return cultureByName;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000B288C File Offset: 0x000B0A8C
		private static CultureInfo InitUserDefaultUICulture()
		{
			string userDefaultUILanguage = CultureInfo.GetUserDefaultUILanguage();
			if (userDefaultUILanguage == CultureInfo.UserDefaultCulture.Name)
			{
				return CultureInfo.UserDefaultCulture;
			}
			CultureInfo cultureByName = CultureInfo.GetCultureByName(userDefaultUILanguage, true);
			if (cultureByName == null)
			{
				return CultureInfo.InvariantCulture;
			}
			cultureByName.m_isReadOnly = true;
			return cultureByName;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000B28D0 File Offset: 0x000B0AD0
		[SecuritySafeCritical]
		internal static CultureInfo GetCultureInfoForUserPreferredLanguageInAppX()
		{
			if (CultureInfo.ts_IsDoingAppXCultureInfoLookup)
			{
				return null;
			}
			if (AppDomain.IsAppXNGen)
			{
				return null;
			}
			CultureInfo cultureInfo = null;
			try
			{
				CultureInfo.ts_IsDoingAppXCultureInfoLookup = true;
				if (CultureInfo.s_WindowsRuntimeResourceManager == null)
				{
					CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
				}
				cultureInfo = CultureInfo.s_WindowsRuntimeResourceManager.GlobalResourceContextBestFitCultureInfo;
			}
			finally
			{
				CultureInfo.ts_IsDoingAppXCultureInfoLookup = false;
			}
			return cultureInfo;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000B2934 File Offset: 0x000B0B34
		[SecuritySafeCritical]
		internal static bool SetCultureInfoForUserPreferredLanguageInAppX(CultureInfo ci)
		{
			if (AppDomain.IsAppXNGen)
			{
				return false;
			}
			if (CultureInfo.s_WindowsRuntimeResourceManager == null)
			{
				CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
			}
			return CultureInfo.s_WindowsRuntimeResourceManager.SetGlobalResourceContextDefaultCulture(ci);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name. For more information, see the Notes to Callers section.</exception>
		// Token: 0x06002E67 RID: 11879 RVA: 0x000B2961 File Offset: 0x000B0B61
		[__DynamicallyInvokable]
		public CultureInfo(string name)
			: this(name, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (<see langword="true" />) or the default culture settings (<see langword="false" />).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name. See the Notes to Callers section for more information.</exception>
		// Token: 0x06002E68 RID: 11880 RVA: 0x000B296C File Offset: 0x000B0B6C
		public CultureInfo(string name, bool useUserOverride)
		{
			this.cultureID = 127;
			base..ctor();
			if (name == null)
			{
				throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_String"));
			}
			this.m_cultureData = CultureData.GetCultureData(name, useUserOverride);
			if (this.m_cultureData == null)
			{
				throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			this.m_name = this.m_cultureData.CultureName;
			this.m_isInherited = base.GetType() != typeof(CultureInfo);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000B29F6 File Offset: 0x000B0BF6
		private CultureInfo(CultureData cultureData)
		{
			this.cultureID = 127;
			base..ctor();
			this.m_cultureData = cultureData;
			this.m_name = cultureData.CultureName;
			this.m_isInherited = false;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000B2A20 File Offset: 0x000B0C20
		private static CultureInfo CreateCultureInfoNoThrow(string name, bool useUserOverride)
		{
			CultureData cultureData = CultureData.GetCultureData(name, useUserOverride);
			if (cultureData == null)
			{
				return null;
			}
			return new CultureInfo(cultureData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier. See the Notes to Callers section for more information.</exception>
		// Token: 0x06002E6B RID: 11883 RVA: 0x000B2A40 File Offset: 0x000B0C40
		public CultureInfo(int culture)
			: this(culture, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier.</param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (<see langword="true" />) or the default culture settings (<see langword="false" />).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier. See the Notes to Callers section for more information.</exception>
		// Token: 0x06002E6C RID: 11884 RVA: 0x000B2A4A File Offset: 0x000B0C4A
		public CultureInfo(int culture, bool useUserOverride)
		{
			this.cultureID = 127;
			base..ctor();
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			this.InitializeFromCultureId(culture, useUserOverride);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000B2A7C File Offset: 0x000B0C7C
		private void InitializeFromCultureId(int culture, bool useUserOverride)
		{
			if (culture <= 1024)
			{
				if (culture != 0 && culture != 1024)
				{
					goto IL_43;
				}
			}
			else if (culture != 2048 && culture != 3072 && culture != 4096)
			{
				goto IL_43;
			}
			throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
			IL_43:
			this.m_cultureData = CultureData.GetCultureData(culture, useUserOverride);
			this.m_isInherited = base.GetType() != typeof(CultureInfo);
			this.m_name = this.m_cultureData.CultureName;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000B2B08 File Offset: 0x000B0D08
		internal static void CheckDomainSafetyObject(object obj, object container)
		{
			if (obj.GetType().Assembly != typeof(CultureInfo).Assembly)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidOperation_SubclassedObject"), obj.GetType(), container.GetType()));
			}
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000B2B5C File Offset: 0x000B0D5C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_name == null || CultureInfo.IsAlternateSortLcid(this.cultureID))
			{
				this.InitializeFromCultureId(this.cultureID, this.m_useUserOverride);
			}
			else
			{
				this.m_cultureData = CultureData.GetCultureData(this.m_name, this.m_useUserOverride);
				if (this.m_cultureData == null)
				{
					throw new CultureNotFoundException("m_name", this.m_name, Environment.GetResourceString("Argument_CultureNotSupported"));
				}
			}
			this.m_isInherited = base.GetType() != typeof(CultureInfo);
			if (base.GetType().Assembly == typeof(CultureInfo).Assembly)
			{
				if (this.textInfo != null)
				{
					CultureInfo.CheckDomainSafetyObject(this.textInfo, this);
				}
				if (this.compareInfo != null)
				{
					CultureInfo.CheckDomainSafetyObject(this.compareInfo, this);
				}
			}
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000B2C30 File Offset: 0x000B0E30
		private static bool IsAlternateSortLcid(int lcid)
		{
			return lcid == 1034 || (lcid & 983040) != 0;
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000B2C46 File Offset: 0x000B0E46
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_name = this.m_cultureData.CultureName;
			this.m_useUserOverride = this.m_cultureData.UseUserOverride;
			this.cultureID = this.m_cultureData.ILANGUAGE;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000B2C7B File Offset: 0x000B0E7B
		internal bool IsSafeCrossDomain
		{
			get
			{
				return this.m_isSafeCrossDomain;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000B2C83 File Offset: 0x000B0E83
		internal int CreatedDomainID
		{
			get
			{
				return this.m_createdDomainID;
			}
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000B2C8B File Offset: 0x000B0E8B
		internal void StartCrossDomainTracking()
		{
			if (this.m_createdDomainID != 0)
			{
				return;
			}
			if (this.CanSendCrossDomain())
			{
				this.m_isSafeCrossDomain = true;
			}
			Thread.MemoryBarrier();
			this.m_createdDomainID = Thread.GetDomainID();
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000B2CB8 File Offset: 0x000B0EB8
		internal bool CanSendCrossDomain()
		{
			bool flag = false;
			if (base.GetType() == typeof(CultureInfo))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000B2CE4 File Offset: 0x000B0EE4
		internal CultureInfo(string cultureName, string textAndCompareCultureName)
		{
			this.cultureID = 127;
			base..ctor();
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName", Environment.GetResourceString("ArgumentNull_String"));
			}
			this.m_cultureData = CultureData.GetCultureData(cultureName, false);
			if (this.m_cultureData == null)
			{
				throw new CultureNotFoundException("cultureName", cultureName, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			this.m_name = this.m_cultureData.CultureName;
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(textAndCompareCultureName);
			this.compareInfo = cultureInfo.CompareInfo;
			this.textInfo = cultureInfo.TextInfo;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000B2D74 File Offset: 0x000B0F74
		private static CultureInfo GetCultureByName(string name, bool userOverride)
		{
			try
			{
				return userOverride ? new CultureInfo(name) : CultureInfo.GetCultureInfo(name);
			}
			catch (ArgumentException)
			{
			}
			return null;
		}

		/// <summary>Creates a <see cref="T:System.Globalization.CultureInfo" /> that represents the specific culture that is associated with the specified name.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name or the name of an existing <see cref="T:System.Globalization.CultureInfo" /> object. <paramref name="name" /> is not case-sensitive.</param>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object that represents:  
		///  The invariant culture, if <paramref name="name" /> is an empty string ("").  
		///  -or-  
		///  The specific culture associated with <paramref name="name" />, if <paramref name="name" /> is a neutral culture.  
		///  -or-  
		///  The culture specified by <paramref name="name" />, if <paramref name="name" /> is already a specific culture.</returns>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.  
		/// -or-  
		/// The culture specified by <paramref name="name" /> does not have a specific culture associated with it.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x06002E78 RID: 11896 RVA: 0x000B2DAC File Offset: 0x000B0FAC
		public static CultureInfo CreateSpecificCulture(string name)
		{
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = new CultureInfo(name);
			}
			catch (ArgumentException)
			{
				cultureInfo = null;
				for (int i = 0; i < name.Length; i++)
				{
					if ('-' == name[i])
					{
						try
						{
							cultureInfo = new CultureInfo(name.Substring(0, i));
							break;
						}
						catch (ArgumentException)
						{
							throw;
						}
					}
				}
				if (cultureInfo == null)
				{
					throw;
				}
			}
			if (!cultureInfo.IsNeutralCulture)
			{
				return cultureInfo;
			}
			return new CultureInfo(cultureInfo.m_cultureData.SSPECIFICCULTURE);
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000B2E34 File Offset: 0x000B1034
		internal static bool VerifyCultureName(string cultureName, bool throwException)
		{
			int i = 0;
			while (i < cultureName.Length)
			{
				char c = cultureName[i];
				if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					if (throwException)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", new object[] { cultureName }));
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return true;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000B2E8C File Offset: 0x000B108C
		internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
		{
			return !culture.m_isInherited || CultureInfo.VerifyCultureName(culture.Name, throwException);
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture used by the current thread.</summary>
		/// <returns>An object that represents the culture used by the current thread.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to <see langword="null" />.</exception>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x000B2EA4 File Offset: 0x000B10A4
		// (set) Token: 0x06002E7C RID: 11900 RVA: 0x000B2EB0 File Offset: 0x000B10B0
		[__DynamicallyInvokable]
		public static CultureInfo CurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
				{
					return;
				}
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000B2EDC File Offset: 0x000B10DC
		internal static CultureInfo UserDefaultCulture
		{
			get
			{
				CultureInfo cultureInfo = CultureInfo.s_userDefaultCulture;
				if (cultureInfo == null)
				{
					CultureInfo.s_userDefaultCulture = CultureInfo.InvariantCulture;
					cultureInfo = CultureInfo.InitUserDefaultCulture();
					CultureInfo.s_userDefaultCulture = cultureInfo;
				}
				return cultureInfo;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000B2F10 File Offset: 0x000B1110
		internal static CultureInfo UserDefaultUICulture
		{
			get
			{
				CultureInfo cultureInfo = CultureInfo.s_userDefaultUICulture;
				if (cultureInfo == null)
				{
					CultureInfo.s_userDefaultUICulture = CultureInfo.InvariantCulture;
					cultureInfo = CultureInfo.InitUserDefaultUICulture();
					CultureInfo.s_userDefaultUICulture = cultureInfo;
				}
				return cultureInfo;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the current user interface culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>The culture used by the Resource Manager to look up culture-specific resources at run time.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is set to a culture name that cannot be used to locate a resource file. Resource filenames can include only letters, numbers, hyphens, or underscores.</exception>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000B2F43 File Offset: 0x000B1143
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000B2F4F File Offset: 0x000B114F
		[__DynamicallyInvokable]
		public static CultureInfo CurrentUICulture
		{
			[__DynamicallyInvokable]
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
				{
					return;
				}
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000B2F7C File Offset: 0x000B117C
		public static CultureInfo InstalledUICulture
		{
			get
			{
				CultureInfo cultureInfo = CultureInfo.s_InstalledUICultureInfo;
				if (cultureInfo == null)
				{
					string systemDefaultUILanguage = CultureInfo.GetSystemDefaultUILanguage();
					cultureInfo = CultureInfo.GetCultureByName(systemDefaultUILanguage, true);
					if (cultureInfo == null)
					{
						cultureInfo = CultureInfo.InvariantCulture;
					}
					cultureInfo.m_isReadOnly = true;
					CultureInfo.s_InstalledUICultureInfo = cultureInfo;
				}
				return cultureInfo;
			}
		}

		/// <summary>Gets or sets the default culture for threads in the current application domain.</summary>
		/// <returns>The default culture for threads in the current application domain, or <see langword="null" /> if the current system culture is the default thread culture in the application domain.</returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000B2FBB File Offset: 0x000B11BB
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x000B2FC4 File Offset: 0x000B11C4
		[__DynamicallyInvokable]
		public static CultureInfo DefaultThreadCurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return CultureInfo.s_DefaultThreadCurrentCulture;
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
			set
			{
				CultureInfo.s_DefaultThreadCurrentCulture = value;
			}
		}

		/// <summary>Gets or sets the default UI culture for threads in the current application domain.</summary>
		/// <returns>The default UI culture for threads in the current application domain, or <see langword="null" /> if the current system UI culture is the default thread UI culture in the application domain.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the <see cref="P:System.Globalization.CultureInfo.Name" /> property value is invalid.</exception>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000B2FCE File Offset: 0x000B11CE
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x000B2FD7 File Offset: 0x000B11D7
		[__DynamicallyInvokable]
		public static CultureInfo DefaultThreadCurrentUICulture
		{
			[__DynamicallyInvokable]
			get
			{
				return CultureInfo.s_DefaultThreadCurrentUICulture;
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
			set
			{
				if (value != null)
				{
					CultureInfo.VerifyCultureName(value, true);
				}
				CultureInfo.s_DefaultThreadCurrentUICulture = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>The object that is culture-independent (invariant).</returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x000B2FEC File Offset: 0x000B11EC
		[__DynamicallyInvokable]
		public static CultureInfo InvariantCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return CultureInfo.s_InvariantCultureInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000B2FF8 File Offset: 0x000B11F8
		[__DynamicallyInvokable]
		public virtual CultureInfo Parent
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.m_parent == null)
				{
					string sparent = this.m_cultureData.SPARENT;
					CultureInfo cultureInfo;
					if (string.IsNullOrEmpty(sparent))
					{
						cultureInfo = CultureInfo.InvariantCulture;
					}
					else
					{
						cultureInfo = CultureInfo.CreateCultureInfoNoThrow(sparent, this.m_cultureData.UseUserOverride);
						if (cultureInfo == null)
						{
							cultureInfo = CultureInfo.InvariantCulture;
						}
					}
					Interlocked.CompareExchange<CultureInfo>(ref this.m_parent, cultureInfo, null);
				}
				return this.m_parent;
			}
		}

		/// <summary>Gets the culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000B305A File Offset: 0x000B125A
		public virtual int LCID
		{
			get
			{
				return this.m_cultureData.ILANGUAGE;
			}
		}

		/// <summary>Gets the active input locale identifier.</summary>
		/// <returns>A 32-bit signed number that specifies an input locale identifier.</returns>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000B3068 File Offset: 0x000B1268
		[ComVisible(false)]
		public virtual int KeyboardLayoutId
		{
			get
			{
				return this.m_cultureData.IINPUTLANGUAGEHANDLE;
			}
		}

		/// <summary>Gets the list of supported cultures filtered by the specified <see cref="T:System.Globalization.CultureTypes" /> parameter.</summary>
		/// <param name="types">A bitwise combination of the enumeration values that filter the cultures to retrieve.</param>
		/// <returns>An array that contains the cultures specified by the <paramref name="types" /> parameter. The array of cultures is unsorted.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="types" /> specifies an invalid combination of <see cref="T:System.Globalization.CultureTypes" /> values.</exception>
		// Token: 0x06002E8A RID: 11914 RVA: 0x000B3082 File Offset: 0x000B1282
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			if ((types & CultureTypes.UserCustomCulture) == CultureTypes.UserCustomCulture)
			{
				types |= CultureTypes.ReplacementCultures;
			}
			return CultureData.GetCultures(types);
		}

		/// <summary>Gets the culture name in the format languagecode2-country/regioncode2.</summary>
		/// <returns>The culture name in the format languagecode2-country/regioncode2. languagecode2 is a lowercase two-letter code derived from ISO 639-1. country/regioncode2 is derived from ISO 3166 and usually consists of two uppercase letters, or a BCP-47 language tag.</returns>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000B3096 File Offset: 0x000B1296
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_nonSortName == null)
				{
					this.m_nonSortName = this.m_cultureData.SNAME;
					if (this.m_nonSortName == null)
					{
						this.m_nonSortName = string.Empty;
					}
				}
				return this.m_nonSortName;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x000B30CA File Offset: 0x000B12CA
		internal string SortName
		{
			get
			{
				if (this.m_sortName == null)
				{
					this.m_sortName = this.m_cultureData.SCOMPAREINFO;
				}
				return this.m_sortName;
			}
		}

		/// <summary>Deprecated. Gets the RFC 4646 standard identification for a language.</summary>
		/// <returns>A string that is the RFC 4646 standard identification for a language.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000B30EC File Offset: 0x000B12EC
		[ComVisible(false)]
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				if (name == "zh-CHT")
				{
					return "zh-Hant";
				}
				if (!(name == "zh-CHS"))
				{
					return this.Name;
				}
				return "zh-Hans";
			}
		}

		/// <summary>Gets the full localized culture name.</summary>
		/// <returns>The full localized culture name in the format languagefull [country/regionfull], where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000B312E File Offset: 0x000B132E
		[__DynamicallyInvokable]
		public virtual string DisplayName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SLOCALIZEDDISPLAYNAME;
			}
		}

		/// <summary>Gets the culture name, consisting of the language, the country/region, and the optional script, that the culture is set to display.</summary>
		/// <returns>The culture name. consisting of the full name of the language, the full name of the country/region, and the optional script. The format is discussed in the description of the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000B313B File Offset: 0x000B133B
		[__DynamicallyInvokable]
		public virtual string NativeName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SNATIVEDISPLAYNAME;
			}
		}

		/// <summary>Gets the culture name in the format languagefull [country/regionfull] in English.</summary>
		/// <returns>The culture name in the format languagefull [country/regionfull] in English, where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000B3148 File Offset: 0x000B1348
		[__DynamicallyInvokable]
		public virtual string EnglishName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SENGDISPLAYNAME;
			}
		}

		/// <summary>Gets the ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000B3155 File Offset: 0x000B1355
		[__DynamicallyInvokable]
		public virtual string TwoLetterISOLanguageName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SISO639LANGNAME;
			}
		}

		/// <summary>Gets the ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x000B3162 File Offset: 0x000B1362
		public virtual string ThreeLetterISOLanguageName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SISO639LANGNAME2;
			}
		}

		/// <summary>Gets the three-letter code for the language as defined in the Windows API.</summary>
		/// <returns>The three-letter code for the language as defined in the Windows API.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000B316F File Offset: 0x000B136F
		public virtual string ThreeLetterWindowsLanguageName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SABBREVLANGNAME;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000B317C File Offset: 0x000B137C
		[__DynamicallyInvokable]
		public virtual CompareInfo CompareInfo
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.compareInfo == null)
				{
					CompareInfo compareInfo = (this.UseUserOverride ? CultureInfo.GetCultureInfo(this.m_name).CompareInfo : new CompareInfo(this));
					if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
					{
						return compareInfo;
					}
					this.compareInfo = compareInfo;
				}
				return this.compareInfo;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000B31CC File Offset: 0x000B13CC
		private RegionInfo Region
		{
			get
			{
				if (this.regionInfo == null)
				{
					RegionInfo regionInfo = new RegionInfo(this.m_cultureData);
					this.regionInfo = regionInfo;
				}
				return this.regionInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</returns>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x000B31FC File Offset: 0x000B13FC
		[__DynamicallyInvokable]
		public virtual TextInfo TextInfo
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.textInfo == null)
				{
					TextInfo textInfo = new TextInfo(this.m_cultureData);
					textInfo.SetReadOnlyState(this.m_isReadOnly);
					if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
					{
						return textInfo;
					}
					this.textInfo = textInfo;
				}
				return this.textInfo;
			}
		}

		/// <summary>Determines whether the specified object is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CultureInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E97 RID: 11927 RVA: 0x000B3244 File Offset: 0x000B1444
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && this.Name.Equals(cultureInfo.Name) && this.CompareInfo.Equals(cultureInfo.CompareInfo);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CultureInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06002E98 RID: 11928 RVA: 0x000B3289 File Offset: 0x000B1489
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() + this.CompareInfo.GetHashCode();
		}

		/// <summary>Returns a string containing the name of the current <see cref="T:System.Globalization.CultureInfo" /> in the format languagecode2-country/regioncode2.</summary>
		/// <returns>A string containing the name of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06002E99 RID: 11929 RVA: 0x000B32A2 File Offset: 0x000B14A2
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.m_name;
		}

		/// <summary>Gets an object that defines how to format the specified type.</summary>
		/// <param name="formatType">The <see cref="T:System.Type" /> for which to get a formatting object. This method only supports the <see cref="T:System.Globalization.NumberFormatInfo" /> and <see cref="T:System.Globalization.DateTimeFormatInfo" /> types.</param>
		/// <returns>The value of the <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property, which is a <see cref="T:System.Globalization.NumberFormatInfo" /> containing the default number format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.NumberFormatInfo" /> class.  
		///  -or-  
		///  The value of the <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property, which is a <see cref="T:System.Globalization.DateTimeFormatInfo" /> containing the default date and time format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class.  
		///  -or-  
		///  null, if <paramref name="formatType" /> is any other object.</returns>
		// Token: 0x06002E9A RID: 11930 RVA: 0x000B32AA File Offset: 0x000B14AA
		[__DynamicallyInvokable]
		public virtual object GetFormat(Type formatType)
		{
			if (formatType == typeof(NumberFormatInfo))
			{
				return this.NumberFormat;
			}
			if (formatType == typeof(DateTimeFormatInfo))
			{
				return this.DateTimeFormat;
			}
			return null;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000B32DF File Offset: 0x000B14DF
		[__DynamicallyInvokable]
		public virtual bool IsNeutralCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.IsNeutralCulture;
			}
		}

		/// <summary>Gets the culture types that pertain to the current <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>A bitwise combination of one or more <see cref="T:System.Globalization.CultureTypes" /> values. There is no default value.</returns>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x000B32EC File Offset: 0x000B14EC
		[ComVisible(false)]
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = (CultureTypes)0;
				if (this.m_cultureData.IsNeutralCulture)
				{
					cultureTypes |= CultureTypes.NeutralCultures;
				}
				else
				{
					cultureTypes |= CultureTypes.SpecificCultures;
				}
				cultureTypes |= (this.m_cultureData.IsWin32Installed ? CultureTypes.InstalledWin32Cultures : ((CultureTypes)0));
				cultureTypes |= (this.m_cultureData.IsFramework ? CultureTypes.FrameworkCultures : ((CultureTypes)0));
				cultureTypes |= (this.m_cultureData.IsSupplementalCustomCulture ? CultureTypes.UserCustomCulture : ((CultureTypes)0));
				return cultureTypes | (this.m_cultureData.IsReplacementCulture ? (CultureTypes.UserCustomCulture | CultureTypes.ReplacementCultures) : ((CultureTypes)0));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</summary>
		/// <returns>A <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property or any of the <see cref="T:System.Globalization.NumberFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only.</exception>
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000B3368 File Offset: 0x000B1568
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x000B33A2 File Offset: 0x000B15A2
		[__DynamicallyInvokable]
		public virtual NumberFormatInfo NumberFormat
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.numInfo == null)
				{
					this.numInfo = new NumberFormatInfo(this.m_cultureData)
					{
						isReadOnly = this.m_isReadOnly
					};
				}
				return this.numInfo;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				this.numInfo = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property or any of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only.</exception>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000B33CC File Offset: 0x000B15CC
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000B3411 File Offset: 0x000B1611
		[__DynamicallyInvokable]
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.dateTimeInfo == null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
					dateTimeFormatInfo.m_isReadOnly = this.m_isReadOnly;
					Thread.MemoryBarrier();
					this.dateTimeInfo = dateTimeFormatInfo;
				}
				return this.dateTimeInfo;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				this.dateTimeInfo = value;
			}
		}

		/// <summary>Refreshes cached culture-related information.</summary>
		// Token: 0x06002EA1 RID: 11937 RVA: 0x000B3438 File Offset: 0x000B1638
		public void ClearCachedData()
		{
			CultureInfo.s_userDefaultUICulture = null;
			CultureInfo.s_userDefaultCulture = null;
			RegionInfo.s_currentRegionInfo = null;
			TimeZone.ResetTimeZone();
			TimeZoneInfo.ClearCachedData();
			CultureInfo.s_LcidCachedCultures = null;
			CultureInfo.s_NameCachedCultures = null;
			CultureData.ClearCachedData();
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000B3471 File Offset: 0x000B1671
		internal static Calendar GetCalendarInstance(int calType)
		{
			if (calType == 1)
			{
				return new GregorianCalendar();
			}
			return CultureInfo.GetCalendarInstanceRare(calType);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000B3484 File Offset: 0x000B1684
		internal static Calendar GetCalendarInstanceRare(int calType)
		{
			switch (calType)
			{
			case 2:
			case 9:
			case 10:
			case 11:
			case 12:
				return new GregorianCalendar((GregorianCalendarTypes)calType);
			case 3:
				return new JapaneseCalendar();
			case 4:
				return new TaiwanCalendar();
			case 5:
				return new KoreanCalendar();
			case 6:
				return new HijriCalendar();
			case 7:
				return new ThaiBuddhistCalendar();
			case 8:
				return new HebrewCalendar();
			case 14:
				return new JapaneseLunisolarCalendar();
			case 15:
				return new ChineseLunisolarCalendar();
			case 20:
				return new KoreanLunisolarCalendar();
			case 21:
				return new TaiwanLunisolarCalendar();
			case 22:
				return new PersianCalendar();
			case 23:
				return new UmAlQuraCalendar();
			}
			return new GregorianCalendar();
		}

		/// <summary>Gets the default calendar used by the culture.</summary>
		/// <returns>A <see cref="T:System.Globalization.Calendar" /> that represents the default calendar used by the culture.</returns>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000B3548 File Offset: 0x000B1748
		[__DynamicallyInvokable]
		public virtual Calendar Calendar
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.calendar == null)
				{
					Calendar defaultCalendar = this.m_cultureData.DefaultCalendar;
					Thread.MemoryBarrier();
					defaultCalendar.SetReadOnlyState(this.m_isReadOnly);
					this.calendar = defaultCalendar;
				}
				return this.calendar;
			}
		}

		/// <summary>Gets the list of calendars that can be used by the culture.</summary>
		/// <returns>An array of type <see cref="T:System.Globalization.Calendar" /> that represents the calendars that can be used by the culture represented by the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000B3588 File Offset: 0x000B1788
		[__DynamicallyInvokable]
		public virtual Calendar[] OptionalCalendars
		{
			[__DynamicallyInvokable]
			get
			{
				int[] calendarIds = this.m_cultureData.CalendarIds;
				Calendar[] array = new Calendar[calendarIds.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = CultureInfo.GetCalendarInstance(calendarIds[i]);
				}
				return array;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> object uses the user-selected culture settings.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x000B35C4 File Offset: 0x000B17C4
		public bool UseUserOverride
		{
			get
			{
				return this.m_cultureData.UseUserOverride;
			}
		}

		/// <summary>Gets an alternate user interface culture suitable for console applications when the default graphic user interface culture is unsuitable.</summary>
		/// <returns>An alternate culture that is used to read and display text on the console.</returns>
		// Token: 0x06002EA7 RID: 11943 RVA: 0x000B35D4 File Offset: 0x000B17D4
		[SecuritySafeCritical]
		[ComVisible(false)]
		public CultureInfo GetConsoleFallbackUICulture()
		{
			CultureInfo cultureInfo = this.m_consoleFallbackCulture;
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.CreateSpecificCulture(this.m_cultureData.SCONSOLEFALLBACKNAME);
				cultureInfo.m_isReadOnly = true;
				this.m_consoleFallbackCulture = cultureInfo;
			}
			return cultureInfo;
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A copy of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06002EA8 RID: 11944 RVA: 0x000B360C File Offset: 0x000B180C
		[__DynamicallyInvokable]
		public virtual object Clone()
		{
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			if (!this.m_isInherited)
			{
				if (this.dateTimeInfo != null)
				{
					cultureInfo.dateTimeInfo = (DateTimeFormatInfo)this.dateTimeInfo.Clone();
				}
				if (this.numInfo != null)
				{
					cultureInfo.numInfo = (NumberFormatInfo)this.numInfo.Clone();
				}
			}
			else
			{
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
			}
			if (this.textInfo != null)
			{
				cultureInfo.textInfo = (TextInfo)this.textInfo.Clone();
			}
			if (this.calendar != null)
			{
				cultureInfo.calendar = (Calendar)this.calendar.Clone();
			}
			return cultureInfo;
		}

		/// <summary>Returns a read-only wrapper around the specified <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> wrapper around <paramref name="ci" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ci" /> is null.</exception>
		// Token: 0x06002EA9 RID: 11945 RVA: 0x000B36DC File Offset: 0x000B18DC
		[__DynamicallyInvokable]
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.IsReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.MemberwiseClone();
			if (!ci.IsNeutralCulture)
			{
				if (!ci.m_isInherited)
				{
					if (ci.dateTimeInfo != null)
					{
						cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(ci.dateTimeInfo);
					}
					if (ci.numInfo != null)
					{
						cultureInfo.numInfo = NumberFormatInfo.ReadOnly(ci.numInfo);
					}
				}
				else
				{
					cultureInfo.DateTimeFormat = DateTimeFormatInfo.ReadOnly(ci.DateTimeFormat);
					cultureInfo.NumberFormat = NumberFormatInfo.ReadOnly(ci.NumberFormat);
				}
			}
			if (ci.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(ci.textInfo);
			}
			if (ci.calendar != null)
			{
				cultureInfo.calendar = Calendar.ReadOnly(ci.calendar);
			}
			cultureInfo.m_isReadOnly = true;
			return cultureInfo;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.CultureInfo" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000B37AD File Offset: 0x000B19AD
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000B37B5 File Offset: 0x000B19B5
		private void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000B37CF File Offset: 0x000B19CF
		internal bool HasInvariantCultureName
		{
			get
			{
				return this.Name == CultureInfo.InvariantCulture.Name;
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000B37E8 File Offset: 0x000B19E8
		internal static CultureInfo GetCultureInfoHelper(int lcid, string name, string altName)
		{
			Hashtable hashtable = CultureInfo.s_NameCachedCultures;
			if (name != null)
			{
				name = CultureData.AnsiToLower(name);
			}
			if (altName != null)
			{
				altName = CultureData.AnsiToLower(altName);
			}
			CultureInfo cultureInfo;
			if (hashtable == null)
			{
				hashtable = Hashtable.Synchronized(new Hashtable());
			}
			else if (lcid == -1)
			{
				cultureInfo = (CultureInfo)hashtable[name + "\ufffd" + altName];
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			else if (lcid == 0)
			{
				cultureInfo = (CultureInfo)hashtable[name];
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			Hashtable hashtable2 = CultureInfo.s_LcidCachedCultures;
			if (hashtable2 == null)
			{
				hashtable2 = Hashtable.Synchronized(new Hashtable());
			}
			else if (lcid > 0)
			{
				cultureInfo = (CultureInfo)hashtable2[lcid];
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			try
			{
				if (lcid != -1)
				{
					if (lcid != 0)
					{
						cultureInfo = new CultureInfo(lcid, false);
					}
					else
					{
						cultureInfo = new CultureInfo(name, false);
					}
				}
				else
				{
					cultureInfo = new CultureInfo(name, altName);
				}
			}
			catch (ArgumentException)
			{
				return null;
			}
			cultureInfo.m_isReadOnly = true;
			if (lcid == -1)
			{
				hashtable[name + "\ufffd" + altName] = cultureInfo;
				cultureInfo.TextInfo.SetReadOnlyState(true);
			}
			else
			{
				string text = CultureData.AnsiToLower(cultureInfo.m_name);
				hashtable[text] = cultureInfo;
				if ((cultureInfo.LCID != 4 || !(text == "zh-hans")) && (cultureInfo.LCID != 31748 || !(text == "zh-hant")))
				{
					hashtable2[cultureInfo.LCID] = cultureInfo;
				}
			}
			if (-1 != lcid)
			{
				CultureInfo.s_LcidCachedCultures = hashtable2;
			}
			CultureInfo.s_NameCachedCultures = hashtable;
			return cultureInfo;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture by using the specified culture identifier.</summary>
		/// <param name="culture">A locale identifier (LCID).</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> specifies a culture that is not supported. See the Notes to Caller section for more information.</exception>
		// Token: 0x06002EAE RID: 11950 RVA: 0x000B396C File Offset: 0x000B1B6C
		public static CultureInfo GetCultureInfo(int culture)
		{
			if (culture <= 0)
			{
				throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(culture, null, null);
			if (cultureInfoHelper == null)
			{
				throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			return cultureInfoHelper;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture using the specified culture name.</summary>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> specifies a culture that is not supported. See the Notes to Callers section for more information.</exception>
		// Token: 0x06002EAF RID: 11951 RVA: 0x000B39B8 File Offset: 0x000B1BB8
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(0, name, null);
			if (cultureInfoHelper == null)
			{
				throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			return cultureInfoHelper;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture. Parameters specify a culture that is initialized with the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects specified by another culture.</summary>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="altName">The name of a culture that supplies the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects used to initialize <paramref name="name" />. <paramref name="altName" /> is not case-sensitive.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="altName" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> or <paramref name="altName" /> specifies a culture that is not supported. See the Notes to Callers section for more information.</exception>
		// Token: 0x06002EB0 RID: 11952 RVA: 0x000B39F8 File Offset: 0x000B1BF8
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("altName");
			}
			CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(-1, name, altName);
			if (cultureInfoHelper == null)
			{
				throw new CultureNotFoundException("name or altName", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_OneOfCulturesNotSupported"), name, altName));
			}
			return cultureInfoHelper;
		}

		/// <summary>Deprecated. Retrieves a read-only <see cref="T:System.Globalization.CultureInfo" /> object having linguistic characteristics that are identified by the specified RFC 4646 language tag.</summary>
		/// <param name="name">The name of a language as specified by the RFC 4646 standard.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> does not correspond to a supported culture.</exception>
		// Token: 0x06002EB1 RID: 11953 RVA: 0x000B3A50 File Offset: 0x000B1C50
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name == "zh-CHT" || name == "zh-CHS")
			{
				throw new CultureNotFoundException("name", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), name));
			}
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
			if (cultureInfo.LCID > 65535 || cultureInfo.LCID == 1034)
			{
				throw new CultureNotFoundException("name", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), name));
			}
			return cultureInfo;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000B3AD9 File Offset: 0x000B1CD9
		internal static bool IsTaiwanSku
		{
			get
			{
				if (!CultureInfo.s_haveIsTaiwanSku)
				{
					CultureInfo.s_isTaiwanSku = CultureInfo.GetSystemDefaultUILanguage() == "zh-TW";
					CultureInfo.s_haveIsTaiwanSku = true;
				}
				return CultureInfo.s_isTaiwanSku;
			}
		}

		// Token: 0x06002EB3 RID: 11955
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string nativeGetLocaleInfoEx(string localeName, uint field);

		// Token: 0x06002EB4 RID: 11956
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int nativeGetLocaleInfoExInt(string localeName, uint field);

		// Token: 0x06002EB5 RID: 11957
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool nativeSetThreadLocale(string localeName);

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000B3B0C File Offset: 0x000B1D0C
		[SecurityCritical]
		private static string GetDefaultLocaleName(int localeType)
		{
			string text = null;
			if (CultureInfo.InternalGetDefaultLocaleName(localeType, JitHelpers.GetStringHandleOnStack(ref text)))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06002EB7 RID: 11959
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalGetDefaultLocaleName(int localetype, StringHandleOnStack localeString);

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000B3B34 File Offset: 0x000B1D34
		[SecuritySafeCritical]
		private static string GetUserDefaultUILanguage()
		{
			string text = null;
			if (CultureInfo.InternalGetUserDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref text)))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06002EB9 RID: 11961
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalGetUserDefaultUILanguage(StringHandleOnStack userDefaultUiLanguage);

		// Token: 0x06002EBA RID: 11962 RVA: 0x000B3B58 File Offset: 0x000B1D58
		[SecuritySafeCritical]
		private static string GetSystemDefaultUILanguage()
		{
			string text = null;
			if (CultureInfo.InternalGetSystemDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref text)))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06002EBB RID: 11963
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalGetSystemDefaultUILanguage(StringHandleOnStack systemDefaultUiLanguage);

		// Token: 0x04001336 RID: 4918
		internal bool m_isReadOnly;

		// Token: 0x04001337 RID: 4919
		internal CompareInfo compareInfo;

		// Token: 0x04001338 RID: 4920
		internal TextInfo textInfo;

		// Token: 0x04001339 RID: 4921
		[NonSerialized]
		internal RegionInfo regionInfo;

		// Token: 0x0400133A RID: 4922
		internal NumberFormatInfo numInfo;

		// Token: 0x0400133B RID: 4923
		internal DateTimeFormatInfo dateTimeInfo;

		// Token: 0x0400133C RID: 4924
		internal Calendar calendar;

		// Token: 0x0400133D RID: 4925
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x0400133E RID: 4926
		[OptionalField(VersionAdded = 1)]
		internal int cultureID;

		// Token: 0x0400133F RID: 4927
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x04001340 RID: 4928
		[NonSerialized]
		internal bool m_isInherited;

		// Token: 0x04001341 RID: 4929
		[NonSerialized]
		private bool m_isSafeCrossDomain;

		// Token: 0x04001342 RID: 4930
		[NonSerialized]
		private int m_createdDomainID;

		// Token: 0x04001343 RID: 4931
		[NonSerialized]
		private CultureInfo m_consoleFallbackCulture;

		// Token: 0x04001344 RID: 4932
		internal string m_name;

		// Token: 0x04001345 RID: 4933
		[NonSerialized]
		private string m_nonSortName;

		// Token: 0x04001346 RID: 4934
		[NonSerialized]
		private string m_sortName;

		// Token: 0x04001347 RID: 4935
		private static volatile CultureInfo s_userDefaultCulture;

		// Token: 0x04001348 RID: 4936
		private static volatile CultureInfo s_InvariantCultureInfo;

		// Token: 0x04001349 RID: 4937
		private static volatile CultureInfo s_userDefaultUICulture;

		// Token: 0x0400134A RID: 4938
		private static volatile CultureInfo s_InstalledUICultureInfo;

		// Token: 0x0400134B RID: 4939
		private static volatile CultureInfo s_DefaultThreadCurrentUICulture;

		// Token: 0x0400134C RID: 4940
		private static volatile CultureInfo s_DefaultThreadCurrentCulture;

		// Token: 0x0400134D RID: 4941
		private static volatile Hashtable s_LcidCachedCultures;

		// Token: 0x0400134E RID: 4942
		private static volatile Hashtable s_NameCachedCultures;

		// Token: 0x0400134F RID: 4943
		[SecurityCritical]
		private static volatile WindowsRuntimeResourceManagerBase s_WindowsRuntimeResourceManager;

		// Token: 0x04001350 RID: 4944
		[ThreadStatic]
		private static bool ts_IsDoingAppXCultureInfoLookup;

		// Token: 0x04001351 RID: 4945
		[NonSerialized]
		private CultureInfo m_parent;

		// Token: 0x04001352 RID: 4946
		internal const int LOCALE_NEUTRAL = 0;

		// Token: 0x04001353 RID: 4947
		private const int LOCALE_USER_DEFAULT = 1024;

		// Token: 0x04001354 RID: 4948
		private const int LOCALE_SYSTEM_DEFAULT = 2048;

		// Token: 0x04001355 RID: 4949
		internal const int LOCALE_CUSTOM_DEFAULT = 3072;

		// Token: 0x04001356 RID: 4950
		internal const int LOCALE_CUSTOM_UNSPECIFIED = 4096;

		// Token: 0x04001357 RID: 4951
		internal const int LOCALE_INVARIANT = 127;

		// Token: 0x04001358 RID: 4952
		private const int LOCALE_TRADITIONAL_SPANISH = 1034;

		// Token: 0x04001359 RID: 4953
		private static readonly bool init = CultureInfo.Init();

		// Token: 0x0400135A RID: 4954
		private bool m_useUserOverride;

		// Token: 0x0400135B RID: 4955
		private const int LOCALE_SORTID_MASK = 983040;

		// Token: 0x0400135C RID: 4956
		private static volatile bool s_isTaiwanSku;

		// Token: 0x0400135D RID: 4957
		private static volatile bool s_haveIsTaiwanSku;
	}
}
