using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x0200071A RID: 1818
	[FriendAccessAllowed]
	internal static class BinaryCompatibility
	{
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600515B RID: 20827 RVA: 0x001200A7 File Offset: 0x0011E2A7
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Phone_V7_1
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Phone_V7_1;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600515C RID: 20828 RVA: 0x001200B3 File Offset: 0x0011E2B3
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Phone_V8_0
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Phone_V8_0;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600515D RID: 20829 RVA: 0x001200BF File Offset: 0x0011E2BF
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600515E RID: 20830 RVA: 0x001200CB File Offset: 0x0011E2CB
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_1
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_1;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x001200D7 File Offset: 0x0011E2D7
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_2
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_2;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x001200E3 File Offset: 0x0011E2E3
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_3
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_3;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06005161 RID: 20833 RVA: 0x001200EF File Offset: 0x0011E2EF
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_4
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_4;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x001200FB File Offset: 0x0011E2FB
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V5_0
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V5_0;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x00120107 File Offset: 0x0011E307
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Silverlight_V4
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V4;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06005164 RID: 20836 RVA: 0x00120113 File Offset: 0x0011E313
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Silverlight_V5
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V5;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06005165 RID: 20837 RVA: 0x0012011F File Offset: 0x0011E31F
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Silverlight_V6
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V6;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06005166 RID: 20838 RVA: 0x0012012B File Offset: 0x0011E32B
		[FriendAccessAllowed]
		internal static TargetFrameworkId AppWasBuiltForFramework
		{
			[FriendAccessAllowed]
			get
			{
				if (BinaryCompatibility.s_AppWasBuiltForFramework == TargetFrameworkId.NotYetChecked)
				{
					BinaryCompatibility.ReadTargetFrameworkId();
				}
				return BinaryCompatibility.s_AppWasBuiltForFramework;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06005167 RID: 20839 RVA: 0x0012013E File Offset: 0x0011E33E
		[FriendAccessAllowed]
		internal static int AppWasBuiltForVersion
		{
			[FriendAccessAllowed]
			get
			{
				if (BinaryCompatibility.s_AppWasBuiltForFramework == TargetFrameworkId.NotYetChecked)
				{
					BinaryCompatibility.ReadTargetFrameworkId();
				}
				return BinaryCompatibility.s_AppWasBuiltForVersion;
			}
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x00120154 File Offset: 0x0011E354
		private static bool ParseTargetFrameworkMonikerIntoEnum(string targetFrameworkMoniker, out TargetFrameworkId targetFramework, out int targetFrameworkVersion)
		{
			targetFramework = TargetFrameworkId.NotYetChecked;
			targetFrameworkVersion = 0;
			string text = null;
			string text2 = null;
			BinaryCompatibility.ParseFrameworkName(targetFrameworkMoniker, out text, out targetFrameworkVersion, out text2);
			if (!(text == ".NETFramework"))
			{
				if (!(text == ".NETPortable"))
				{
					if (!(text == ".NETCore"))
					{
						if (!(text == "WindowsPhone"))
						{
							if (!(text == "WindowsPhoneApp"))
							{
								if (!(text == "Silverlight"))
								{
									targetFramework = TargetFrameworkId.Unrecognized;
								}
								else
								{
									targetFramework = TargetFrameworkId.Silverlight;
									if (!string.IsNullOrEmpty(text2))
									{
										if (text2 == "WindowsPhone")
										{
											targetFramework = TargetFrameworkId.Phone;
											targetFrameworkVersion = 70000;
										}
										else if (text2 == "WindowsPhone71")
										{
											targetFramework = TargetFrameworkId.Phone;
											targetFrameworkVersion = 70100;
										}
										else if (text2 == "WindowsPhone8")
										{
											targetFramework = TargetFrameworkId.Phone;
											targetFrameworkVersion = 80000;
										}
										else if (text2.StartsWith("WindowsPhone", StringComparison.Ordinal))
										{
											targetFramework = TargetFrameworkId.Unrecognized;
											targetFrameworkVersion = 70100;
										}
										else
										{
											targetFramework = TargetFrameworkId.Unrecognized;
										}
									}
								}
							}
							else
							{
								targetFramework = TargetFrameworkId.Phone;
							}
						}
						else if (targetFrameworkVersion >= 80100)
						{
							targetFramework = TargetFrameworkId.Phone;
						}
						else
						{
							targetFramework = TargetFrameworkId.Unspecified;
						}
					}
					else
					{
						targetFramework = TargetFrameworkId.NetCore;
					}
				}
				else
				{
					targetFramework = TargetFrameworkId.Portable;
				}
			}
			else
			{
				targetFramework = TargetFrameworkId.NetFramework;
			}
			return true;
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x00120278 File Offset: 0x0011E478
		private static void ParseFrameworkName(string frameworkName, out string identifier, out int version, out string profile)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "frameworkName");
			}
			string[] array = frameworkName.Split(new char[] { ',' });
			version = 0;
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameTooShort"), "frameworkName");
			}
			identifier = array[0].Trim();
			if (identifier.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameInvalid"), "frameworkName");
			}
			bool flag = false;
			profile = null;
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { '=' });
				if (array2.Length != 2)
				{
					throw new ArgumentException(Environment.GetResourceString("SR.Argument_FrameworkNameInvalid"), "frameworkName");
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
						throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameInvalid"), "frameworkName");
					}
					if (!string.IsNullOrEmpty(text2))
					{
						profile = text2;
					}
				}
			}
			if (!flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameMissingVersion"), "frameworkName");
			}
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x0012043C File Offset: 0x0011E63C
		[SecuritySafeCritical]
		private static void ReadTargetFrameworkId()
		{
			string text = AppDomain.CurrentDomain.GetTargetFrameworkName();
			string valueInternal = CompatibilitySwitch.GetValueInternal("TargetFrameworkMoniker");
			if (!string.IsNullOrEmpty(valueInternal))
			{
				text = valueInternal;
			}
			int num = 0;
			TargetFrameworkId targetFrameworkId;
			if (text == null)
			{
				targetFrameworkId = TargetFrameworkId.Unspecified;
			}
			else if (!BinaryCompatibility.ParseTargetFrameworkMonikerIntoEnum(text, out targetFrameworkId, out num))
			{
				targetFrameworkId = TargetFrameworkId.Unrecognized;
			}
			BinaryCompatibility.s_AppWasBuiltForFramework = targetFrameworkId;
			BinaryCompatibility.s_AppWasBuiltForVersion = num;
		}

		// Token: 0x04002409 RID: 9225
		private static TargetFrameworkId s_AppWasBuiltForFramework;

		// Token: 0x0400240A RID: 9226
		private static int s_AppWasBuiltForVersion;

		// Token: 0x0400240B RID: 9227
		private static readonly BinaryCompatibility.BinaryCompatibilityMap s_map = new BinaryCompatibility.BinaryCompatibilityMap();

		// Token: 0x0400240C RID: 9228
		private const char c_componentSeparator = ',';

		// Token: 0x0400240D RID: 9229
		private const char c_keyValueSeparator = '=';

		// Token: 0x0400240E RID: 9230
		private const char c_versionValuePrefix = 'v';

		// Token: 0x0400240F RID: 9231
		private const string c_versionKey = "Version";

		// Token: 0x04002410 RID: 9232
		private const string c_profileKey = "Profile";

		// Token: 0x02000C5E RID: 3166
		private sealed class BinaryCompatibilityMap
		{
			// Token: 0x06007091 RID: 28817 RVA: 0x00184CA4 File Offset: 0x00182EA4
			internal BinaryCompatibilityMap()
			{
				this.AddQuirksForFramework(BinaryCompatibility.AppWasBuiltForFramework, BinaryCompatibility.AppWasBuiltForVersion);
			}

			// Token: 0x06007092 RID: 28818 RVA: 0x00184CBC File Offset: 0x00182EBC
			private void AddQuirksForFramework(TargetFrameworkId builtAgainstFramework, int buildAgainstVersion)
			{
				switch (builtAgainstFramework)
				{
				case TargetFrameworkId.NotYetChecked:
				case TargetFrameworkId.Unrecognized:
				case TargetFrameworkId.Unspecified:
				case TargetFrameworkId.Portable:
					break;
				case TargetFrameworkId.NetFramework:
				case TargetFrameworkId.NetCore:
					if (buildAgainstVersion >= 50000)
					{
						this.TargetsAtLeast_Desktop_V5_0 = true;
					}
					if (buildAgainstVersion >= 40504)
					{
						this.TargetsAtLeast_Desktop_V4_5_4 = true;
					}
					if (buildAgainstVersion >= 40503)
					{
						this.TargetsAtLeast_Desktop_V4_5_3 = true;
					}
					if (buildAgainstVersion >= 40502)
					{
						this.TargetsAtLeast_Desktop_V4_5_2 = true;
					}
					if (buildAgainstVersion >= 40501)
					{
						this.TargetsAtLeast_Desktop_V4_5_1 = true;
					}
					if (buildAgainstVersion >= 40500)
					{
						this.TargetsAtLeast_Desktop_V4_5 = true;
						this.AddQuirksForFramework(TargetFrameworkId.Phone, 70100);
						this.AddQuirksForFramework(TargetFrameworkId.Silverlight, 50000);
						return;
					}
					break;
				case TargetFrameworkId.Silverlight:
					if (buildAgainstVersion >= 40000)
					{
						this.TargetsAtLeast_Silverlight_V4 = true;
					}
					if (buildAgainstVersion >= 50000)
					{
						this.TargetsAtLeast_Silverlight_V5 = true;
					}
					if (buildAgainstVersion >= 60000)
					{
						this.TargetsAtLeast_Silverlight_V6 = true;
					}
					break;
				case TargetFrameworkId.Phone:
					if (buildAgainstVersion >= 80000)
					{
						this.TargetsAtLeast_Phone_V8_0 = true;
					}
					if (buildAgainstVersion >= 80100)
					{
						this.TargetsAtLeast_Desktop_V4_5 = true;
						this.TargetsAtLeast_Desktop_V4_5_1 = true;
					}
					if (buildAgainstVersion >= 710)
					{
						this.TargetsAtLeast_Phone_V7_1 = true;
						return;
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x040037BD RID: 14269
			internal bool TargetsAtLeast_Phone_V7_1;

			// Token: 0x040037BE RID: 14270
			internal bool TargetsAtLeast_Phone_V8_0;

			// Token: 0x040037BF RID: 14271
			internal bool TargetsAtLeast_Phone_V8_1;

			// Token: 0x040037C0 RID: 14272
			internal bool TargetsAtLeast_Desktop_V4_5;

			// Token: 0x040037C1 RID: 14273
			internal bool TargetsAtLeast_Desktop_V4_5_1;

			// Token: 0x040037C2 RID: 14274
			internal bool TargetsAtLeast_Desktop_V4_5_2;

			// Token: 0x040037C3 RID: 14275
			internal bool TargetsAtLeast_Desktop_V4_5_3;

			// Token: 0x040037C4 RID: 14276
			internal bool TargetsAtLeast_Desktop_V4_5_4;

			// Token: 0x040037C5 RID: 14277
			internal bool TargetsAtLeast_Desktop_V5_0;

			// Token: 0x040037C6 RID: 14278
			internal bool TargetsAtLeast_Silverlight_V4;

			// Token: 0x040037C7 RID: 14279
			internal bool TargetsAtLeast_Silverlight_V5;

			// Token: 0x040037C8 RID: 14280
			internal bool TargetsAtLeast_Silverlight_V6;
		}
	}
}
