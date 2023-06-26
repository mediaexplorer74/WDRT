using System;

namespace System
{
	// Token: 0x02000036 RID: 54
	internal static class AppContextDefaultValues
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0001095C File Offset: 0x0000EB5C
		public static void PopulateDefaultValues()
		{
			string text;
			string text2;
			int num;
			System.AppContextDefaultValues.ParseTargetFrameworkName(out text, out text2, out num);
			System.AppContextDefaultValues.PopulateDefaultValuesPartial(text, text2, num);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001097C File Offset: 0x0000EB7C
		private static void ParseTargetFrameworkName(out string identifier, out string profile, out int version)
		{
			string targetFrameworkName = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
			if (!System.AppContextDefaultValues.TryParseFrameworkName(targetFrameworkName, out identifier, out version, out profile))
			{
				identifier = ".NETFramework";
				version = 40000;
				profile = string.Empty;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000109BC File Offset: 0x0000EBBC
		private static bool TryParseFrameworkName(string frameworkName, out string identifier, out int version, out string profile)
		{
			string empty;
			profile = (empty = string.Empty);
			identifier = empty;
			version = 0;
			if (frameworkName == null || frameworkName.Length == 0)
			{
				return false;
			}
			string[] array = frameworkName.Split(new char[] { ',' });
			version = 0;
			if (array.Length < 2 || array.Length > 3)
			{
				return false;
			}
			identifier = array[0].Trim();
			if (identifier.Length == 0)
			{
				return false;
			}
			bool flag = false;
			profile = null;
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { '=' });
				if (array2.Length != 2)
				{
					return false;
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					Version version2 = new Version(text2);
					version = version2.Major * 10000;
					if (version2.Minor > 0)
					{
						version += version2.Minor * 100;
					}
					if (version2.Build > 0)
					{
						version += version2.Build;
					}
				}
				else
				{
					if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
					if (!string.IsNullOrEmpty(text2))
					{
						profile = text2;
					}
				}
			}
			return flag;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00010B18 File Offset: 0x0000ED18
		private static void PopulateDefaultValuesPartial(string platformIdentifier, string profile, int version)
		{
			if (!(platformIdentifier == ".NETCore") && !(platformIdentifier == ".NETFramework"))
			{
				if (!(platformIdentifier == "WindowsPhone") && !(platformIdentifier == "WindowsPhoneApp"))
				{
					return;
				}
				if (version <= 80100)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.Net.DontEnableSchUseStrongCrypto", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.Net.DontEnableSystemDefaultTlsVersions", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.Net.DontEnableTlsAlerts", true);
				}
			}
			else
			{
				if (version <= 40502)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.Net.DontEnableSchUseStrongCrypto", true);
				}
				if (version <= 40601)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.MemberDescriptorEqualsReturnsFalseIfEquivalent", true);
				}
				if (version <= 40602)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.Net.DontEnableSystemDefaultTlsVersions", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.Net.DontEnableTlsAlerts", true);
				}
				if (version <= 40700)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.IO.Ports.DoNotCatchSerialStreamThreadExceptions", true);
				}
				if (version <= 40701)
				{
					LocalAppContext.DefineSwitchDefault("Switch.System.Uri.DontEnableStrictRFC3986ReservedCharacterSets", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.Uri.DontKeepUnicodeBidiFormattingCharacters", true);
					LocalAppContext.DefineSwitchDefault("Switch.System.IO.Compression.DoNotUseNativeZipLibraryForDecompression", true);
					return;
				}
			}
		}
	}
}
