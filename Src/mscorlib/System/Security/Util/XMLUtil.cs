using System;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000381 RID: 897
	internal static class XMLUtil
	{
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000A8C75 File Offset: 0x000A6E75
		public static SecurityElement NewPermissionElement(IPermission ip)
		{
			return XMLUtil.NewPermissionElement(ip.GetType().FullName);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000A8C88 File Offset: 0x000A6E88
		public static SecurityElement NewPermissionElement(string name)
		{
			SecurityElement securityElement = new SecurityElement("Permission");
			securityElement.AddAttribute("class", name);
			return securityElement;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000A8CAD File Offset: 0x000A6EAD
		public static void AddClassAttribute(SecurityElement element, Type type, string typename)
		{
			if (typename == null)
			{
				typename = type.FullName;
			}
			element.AddAttribute("class", typename + ", " + type.Module.Assembly.FullName.Replace('"', '\''));
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000A8CEC File Offset: 0x000A6EEC
		internal static bool ParseElementForAssemblyIdentification(SecurityElement el, out string className, out string assemblyName, out string assemblyVersion)
		{
			className = null;
			assemblyName = null;
			assemblyVersion = null;
			string text = el.Attribute("class");
			if (text == null)
			{
				return false;
			}
			if (text.IndexOf('\'') >= 0)
			{
				text = text.Replace('\'', '"');
			}
			int num = text.IndexOf(',');
			if (num == -1)
			{
				return false;
			}
			int num2 = num;
			className = text.Substring(0, num2);
			string text2 = text.Substring(num + 1);
			AssemblyName assemblyName2 = new AssemblyName(text2);
			assemblyName = assemblyName2.Name;
			assemblyVersion = assemblyName2.Version.ToString();
			return true;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000A8D70 File Offset: 0x000A6F70
		[SecurityCritical]
		private static bool ParseElementForObjectCreation(SecurityElement el, string requiredNamespace, out string className, out int classNameStart, out int classNameLength)
		{
			className = null;
			classNameStart = 0;
			classNameLength = 0;
			int length = requiredNamespace.Length;
			string text = el.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoClass"));
			}
			if (text.IndexOf('\'') >= 0)
			{
				text = text.Replace('\'', '"');
			}
			if (!PermissionToken.IsMscorlibClassName(text))
			{
				return false;
			}
			int num = text.IndexOf(',');
			int num2;
			if (num == -1)
			{
				num2 = text.Length;
			}
			else
			{
				num2 = num;
			}
			if (num2 > length && text.StartsWith(requiredNamespace, StringComparison.Ordinal))
			{
				className = text;
				classNameLength = num2 - length;
				classNameStart = length;
				return true;
			}
			return false;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000A8E04 File Offset: 0x000A7004
		public static string SecurityObjectToXmlString(object ob)
		{
			if (ob == null)
			{
				return "";
			}
			PermissionSet permissionSet = ob as PermissionSet;
			if (permissionSet != null)
			{
				return permissionSet.ToXml().ToString();
			}
			return ((IPermission)ob).ToXml().ToString();
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000A8E40 File Offset: 0x000A7040
		[SecurityCritical]
		public static object XmlStringToSecurityObject(string s)
		{
			if (s == null)
			{
				return null;
			}
			if (s.Length < 1)
			{
				return null;
			}
			return SecurityElement.FromString(s).ToSecurityObject();
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000A8E60 File Offset: 0x000A7060
		[SecuritySafeCritical]
		public static IPermission CreatePermission(SecurityElement el, PermissionState permState, bool ignoreTypeLoadFailures)
		{
			if (el == null || (!el.Tag.Equals("Permission") && !el.Tag.Equals("IPermission")))
			{
				throw new ArgumentException(string.Format(null, Environment.GetResourceString("Argument_WrongElementType"), "<Permission>"));
			}
			string text;
			int num;
			int num2;
			if (XMLUtil.ParseElementForObjectCreation(el, "System.Security.Permissions.", out text, out num, out num2))
			{
				switch (num2)
				{
				case 12:
					if (string.Compare(text, num, "UIPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new UIPermission(permState);
					}
					break;
				case 16:
					if (string.Compare(text, num, "FileIOPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new FileIOPermission(permState);
					}
					break;
				case 18:
					if (text[num] == 'R')
					{
						if (string.Compare(text, num, "RegistryPermission", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new RegistryPermission(permState);
						}
					}
					else if (string.Compare(text, num, "SecurityPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new SecurityPermission(permState);
					}
					break;
				case 19:
					if (string.Compare(text, num, "PrincipalPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new PrincipalPermission(permState);
					}
					break;
				case 20:
					if (text[num] == 'R')
					{
						if (string.Compare(text, num, "ReflectionPermission", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new ReflectionPermission(permState);
						}
					}
					else if (string.Compare(text, num, "FileDialogPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new FileDialogPermission(permState);
					}
					break;
				case 21:
					if (text[num] == 'E')
					{
						if (string.Compare(text, num, "EnvironmentPermission", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new EnvironmentPermission(permState);
						}
					}
					else if (text[num] == 'U')
					{
						if (string.Compare(text, num, "UrlIdentityPermission", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new UrlIdentityPermission(permState);
						}
					}
					else if (string.Compare(text, num, "GacIdentityPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new GacIdentityPermission(permState);
					}
					break;
				case 22:
					if (text[num] == 'S')
					{
						if (string.Compare(text, num, "SiteIdentityPermission", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new SiteIdentityPermission(permState);
						}
					}
					else if (text[num] == 'Z')
					{
						if (string.Compare(text, num, "ZoneIdentityPermission", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new ZoneIdentityPermission(permState);
						}
					}
					else if (string.Compare(text, num, "KeyContainerPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new KeyContainerPermission(permState);
					}
					break;
				case 24:
					if (string.Compare(text, num, "HostProtectionPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new HostProtectionPermission(permState);
					}
					break;
				case 27:
					if (string.Compare(text, num, "PublisherIdentityPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new PublisherIdentityPermission(permState);
					}
					break;
				case 28:
					if (string.Compare(text, num, "StrongNameIdentityPermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new StrongNameIdentityPermission(permState);
					}
					break;
				case 29:
					if (string.Compare(text, num, "IsolatedStorageFilePermission", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new IsolatedStorageFilePermission(permState);
					}
					break;
				}
			}
			object[] array = new object[] { permState };
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			Type classFromElement = XMLUtil.GetClassFromElement(el, ignoreTypeLoadFailures);
			if (classFromElement == null)
			{
				return null;
			}
			if (!typeof(IPermission).IsAssignableFrom(classFromElement))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotAPermissionType"));
			}
			return (IPermission)Activator.CreateInstance(classFromElement, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, array, null);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000A9184 File Offset: 0x000A7384
		[SecuritySafeCritical]
		public static CodeGroup CreateCodeGroup(SecurityElement el)
		{
			if (el == null || !el.Tag.Equals("CodeGroup"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongElementType"), "<CodeGroup>"));
			}
			string text;
			int num;
			int num2;
			if (XMLUtil.ParseElementForObjectCreation(el, "System.Security.Policy.", out text, out num, out num2))
			{
				switch (num2)
				{
				case 12:
					if (string.Compare(text, num, "NetCodeGroup", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new NetCodeGroup();
					}
					break;
				case 13:
					if (string.Compare(text, num, "FileCodeGroup", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new FileCodeGroup();
					}
					break;
				case 14:
					if (string.Compare(text, num, "UnionCodeGroup", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new UnionCodeGroup();
					}
					break;
				default:
					if (num2 == 19)
					{
						if (string.Compare(text, num, "FirstMatchCodeGroup", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new FirstMatchCodeGroup();
						}
					}
					break;
				}
			}
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			Type classFromElement = XMLUtil.GetClassFromElement(el, true);
			if (classFromElement == null)
			{
				return null;
			}
			if (!typeof(CodeGroup).IsAssignableFrom(classFromElement))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotACodeGroupType"));
			}
			return (CodeGroup)Activator.CreateInstance(classFromElement, true);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000A92A8 File Offset: 0x000A74A8
		[SecurityCritical]
		internal static IMembershipCondition CreateMembershipCondition(SecurityElement el)
		{
			if (el == null || !el.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongElementType"), "<IMembershipCondition>"));
			}
			string text;
			int num;
			int num2;
			if (XMLUtil.ParseElementForObjectCreation(el, "System.Security.Policy.", out text, out num, out num2))
			{
				if (num2 <= 23)
				{
					if (num2 != 22)
					{
						if (num2 == 23)
						{
							if (text[num] == 'H')
							{
								if (string.Compare(text, num, "HashMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
								{
									return new HashMembershipCondition();
								}
							}
							else if (text[num] == 'S')
							{
								if (string.Compare(text, num, "SiteMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
								{
									return new SiteMembershipCondition();
								}
							}
							else if (string.Compare(text, num, "ZoneMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
							{
								return new ZoneMembershipCondition();
							}
						}
					}
					else if (text[num] == 'A')
					{
						if (string.Compare(text, num, "AllMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
						{
							return new AllMembershipCondition();
						}
					}
					else if (string.Compare(text, num, "UrlMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new UrlMembershipCondition();
					}
				}
				else if (num2 != 28)
				{
					if (num2 != 29)
					{
						if (num2 == 39)
						{
							if (string.Compare(text, num, "ApplicationDirectoryMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
							{
								return new ApplicationDirectoryMembershipCondition();
							}
						}
					}
					else if (string.Compare(text, num, "StrongNameMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
					{
						return new StrongNameMembershipCondition();
					}
				}
				else if (string.Compare(text, num, "PublisherMembershipCondition", 0, num2, StringComparison.Ordinal) == 0)
				{
					return new PublisherMembershipCondition();
				}
			}
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			Type classFromElement = XMLUtil.GetClassFromElement(el, true);
			if (classFromElement == null)
			{
				return null;
			}
			if (!typeof(IMembershipCondition).IsAssignableFrom(classFromElement))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotAMembershipCondition"));
			}
			return (IMembershipCondition)Activator.CreateInstance(classFromElement, true);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000A9468 File Offset: 0x000A7668
		internal static Type GetClassFromElement(SecurityElement el, bool ignoreTypeLoadFailures)
		{
			string text = el.Attribute("class");
			if (text != null)
			{
				if (ignoreTypeLoadFailures)
				{
					try
					{
						return Type.GetType(text, false, false);
					}
					catch (SecurityException)
					{
						return null;
					}
				}
				return Type.GetType(text, true, false);
			}
			if (ignoreTypeLoadFailures)
			{
				return null;
			}
			throw new ArgumentException(string.Format(null, Environment.GetResourceString("Argument_InvalidXMLMissingAttr"), "class"));
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000A94D4 File Offset: 0x000A76D4
		public static bool IsPermissionElement(IPermission ip, SecurityElement el)
		{
			return el.Tag.Equals("Permission") || el.Tag.Equals("IPermission");
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000A9500 File Offset: 0x000A7700
		public static bool IsUnrestricted(SecurityElement el)
		{
			string text = el.Attribute("Unrestricted");
			return text != null && (text.Equals("true") || text.Equals("TRUE") || text.Equals("True"));
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000A9548 File Offset: 0x000A7748
		public static string BitFieldEnumToString(Type type, object value)
		{
			int num = (int)value;
			if (num == 0)
			{
				return Enum.GetName(type, 0);
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			bool flag = true;
			int num2 = 1;
			int i = 1;
			while (i < 32)
			{
				if ((num2 & num) == 0)
				{
					goto IL_59;
				}
				string name = Enum.GetName(type, num2);
				if (name != null)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(name);
					flag = false;
					goto IL_59;
				}
				IL_5D:
				i++;
				continue;
				IL_59:
				num2 <<= 1;
				goto IL_5D;
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x040011F3 RID: 4595
		private const string BuiltInPermission = "System.Security.Permissions.";

		// Token: 0x040011F4 RID: 4596
		private const string BuiltInMembershipCondition = "System.Security.Policy.";

		// Token: 0x040011F5 RID: 4597
		private const string BuiltInCodeGroup = "System.Security.Policy.";

		// Token: 0x040011F6 RID: 4598
		private const string BuiltInApplicationSecurityManager = "System.Security.Policy.";

		// Token: 0x040011F7 RID: 4599
		private static readonly char[] sepChar = new char[] { ',', ' ' };
	}
}
