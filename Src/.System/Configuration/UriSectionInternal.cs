using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000076 RID: 118
	internal sealed class UriSectionInternal
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0001FADE File Offset: 0x0001DCDE
		private UriSectionInternal()
		{
			this.schemeSettings = new Dictionary<string, SchemeSettingInternal>();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		private UriSectionInternal(UriSection section)
			: this()
		{
			this.idnScope = section.Idn.Enabled;
			this.iriParsing = section.IriParsing.Enabled;
			if (section.SchemeSettings != null)
			{
				foreach (object obj in section.SchemeSettings)
				{
					SchemeSettingElement schemeSettingElement = (SchemeSettingElement)obj;
					SchemeSettingInternal schemeSettingInternal = new SchemeSettingInternal(schemeSettingElement.Name, schemeSettingElement.GenericUriParserOptions);
					this.schemeSettings.Add(schemeSettingInternal.Name, schemeSettingInternal);
				}
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		private UriSectionInternal(UriIdnScope idnScope, bool iriParsing, IEnumerable<SchemeSettingInternal> schemeSettings)
			: this()
		{
			this.idnScope = idnScope;
			this.iriParsing = iriParsing;
			if (schemeSettings != null)
			{
				foreach (SchemeSettingInternal schemeSettingInternal in schemeSettings)
				{
					this.schemeSettings.Add(schemeSettingInternal.Name, schemeSettingInternal);
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0001FC08 File Offset: 0x0001DE08
		internal UriIdnScope IdnScope
		{
			get
			{
				return this.idnScope;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001FC10 File Offset: 0x0001DE10
		internal bool IriParsing
		{
			get
			{
				return this.iriParsing;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001FC18 File Offset: 0x0001DE18
		internal SchemeSettingInternal GetSchemeSetting(string scheme)
		{
			SchemeSettingInternal schemeSettingInternal;
			if (this.schemeSettings.TryGetValue(scheme.ToLowerInvariant(), out schemeSettingInternal))
			{
				return schemeSettingInternal;
			}
			return null;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001FC40 File Offset: 0x0001DE40
		internal static UriSectionInternal GetSection()
		{
			object obj = UriSectionInternal.classSyncObject;
			UriSectionInternal uriSectionInternal;
			lock (obj)
			{
				string text = null;
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				try
				{
					text = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (UriSectionInternal.IsWebConfig(text))
				{
					uriSectionInternal = UriSectionInternal.LoadUsingSystemConfiguration();
				}
				else
				{
					uriSectionInternal = UriSectionInternal.LoadUsingCustomParser(text);
				}
			}
			return uriSectionInternal;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001FCC4 File Offset: 0x0001DEC4
		private static UriSectionInternal LoadUsingSystemConfiguration()
		{
			UriSectionInternal uriSectionInternal;
			try
			{
				UriSection uriSection = PrivilegedConfigurationManager.GetSection("uri") as UriSection;
				if (uriSection == null)
				{
					uriSectionInternal = null;
				}
				else
				{
					uriSectionInternal = new UriSectionInternal(uriSection);
				}
			}
			catch (ConfigurationException)
			{
				uriSectionInternal = null;
			}
			return uriSectionInternal;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001FD08 File Offset: 0x0001DF08
		private static UriSectionInternal LoadUsingCustomParser(string appConfigFilePath)
		{
			string text = null;
			new FileIOPermission(PermissionState.Unrestricted).Assert();
			try
			{
				text = RuntimeEnvironment.GetRuntimeDirectory();
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			string text2 = Path.Combine(Path.Combine(text, "Config"), "machine.config");
			UriSectionData uriSectionData = UriSectionReader.Read(text2);
			UriSectionData uriSectionData2 = UriSectionReader.Read(appConfigFilePath, uriSectionData);
			UriSectionData uriSectionData3 = null;
			if (uriSectionData2 != null)
			{
				uriSectionData3 = uriSectionData2;
			}
			else if (uriSectionData != null)
			{
				uriSectionData3 = uriSectionData;
			}
			if (uriSectionData3 != null)
			{
				UriIdnScope valueOrDefault = uriSectionData3.IdnScope.GetValueOrDefault();
				bool valueOrDefault2 = uriSectionData3.IriParsing.GetValueOrDefault();
				IEnumerable<SchemeSettingInternal> values = uriSectionData3.SchemeSettings.Values;
				return new UriSectionInternal(valueOrDefault, valueOrDefault2, values);
			}
			return null;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		private static bool IsWebConfig(string appConfigFile)
		{
			string text = AppDomain.CurrentDomain.GetData(".appVPath") as string;
			return text != null || (appConfigFile != null && (appConfigFile.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || appConfigFile.StartsWith("https://", StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x04000BF1 RID: 3057
		private static readonly object classSyncObject = new object();

		// Token: 0x04000BF2 RID: 3058
		private UriIdnScope idnScope;

		// Token: 0x04000BF3 RID: 3059
		private bool iriParsing;

		// Token: 0x04000BF4 RID: 3060
		private Dictionary<string, SchemeSettingInternal> schemeSettings;
	}
}
