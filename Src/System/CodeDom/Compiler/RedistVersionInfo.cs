using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000683 RID: 1667
	internal static class RedistVersionInfo
	{
		// Token: 0x06003D60 RID: 15712 RVA: 0x000FB884 File Offset: 0x000F9A84
		public static string GetCompilerPath(IDictionary<string, string> provOptions, string compilerExecutable)
		{
			string text = Executor.GetRuntimeInstallDirectory();
			if (provOptions != null)
			{
				string text2;
				bool flag = provOptions.TryGetValue("CompilerDirectoryPath", out text2);
				string text3;
				bool flag2 = provOptions.TryGetValue("CompilerVersion", out text3);
				if (flag && flag2)
				{
					throw new InvalidOperationException(SR.GetString("Cannot_Specify_Both_Compiler_Path_And_Version", new object[] { "CompilerDirectoryPath", "CompilerVersion" }));
				}
				if (flag)
				{
					return text2;
				}
				if (flag2 && !(text3 == "v4.0"))
				{
					if (!(text3 == "v3.5"))
					{
						if (!(text3 == "v2.0"))
						{
							text = null;
						}
						else
						{
							text = RedistVersionInfo.GetCompilerPathFromRegistry(text3);
						}
					}
					else
					{
						text = RedistVersionInfo.GetCompilerPathFromRegistry(text3);
					}
				}
			}
			if (text == null)
			{
				throw new InvalidOperationException(SR.GetString("CompilerNotFound", new object[] { compilerExecutable }));
			}
			return text;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000FB94C File Offset: 0x000F9B4C
		private static string GetCompilerPathFromRegistry(string versionVal)
		{
			string environmentVariable = Environment.GetEnvironmentVariable("COMPLUS_InstallRoot");
			string environmentVariable2 = Environment.GetEnvironmentVariable("COMPLUS_Version");
			string text;
			if (!string.IsNullOrEmpty(environmentVariable) && !string.IsNullOrEmpty(environmentVariable2))
			{
				text = Path.Combine(environmentVariable, environmentVariable2);
				if (Directory.Exists(text))
				{
					return text;
				}
			}
			string text2 = versionVal.Substring(1);
			string text3 = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\MSBuild\\ToolsVersions\\" + text2;
			text = Registry.GetValue(text3, "MSBuildToolsPath", null) as string;
			if (text != null && Directory.Exists(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x04002CAA RID: 11434
		internal const string DirectoryPath = "CompilerDirectoryPath";

		// Token: 0x04002CAB RID: 11435
		internal const string NameTag = "CompilerVersion";

		// Token: 0x04002CAC RID: 11436
		internal const string DefaultVersion = "v4.0";

		// Token: 0x04002CAD RID: 11437
		internal const string InPlaceVersion = "v4.0";

		// Token: 0x04002CAE RID: 11438
		internal const string RedistVersion = "v3.5";

		// Token: 0x04002CAF RID: 11439
		internal const string RedistVersion20 = "v2.0";

		// Token: 0x04002CB0 RID: 11440
		private const string MSBuildToolsPath = "MSBuildToolsPath";

		// Token: 0x04002CB1 RID: 11441
		private const string dotNetFrameworkRegistryPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\MSBuild\\ToolsVersions\\";
	}
}
