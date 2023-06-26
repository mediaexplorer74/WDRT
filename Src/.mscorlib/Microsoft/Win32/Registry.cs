using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
	/// <summary>Provides <see cref="T:Microsoft.Win32.RegistryKey" /> objects that represent the root keys in the Windows registry, and <see langword="static" /> methods to access key/value pairs.</summary>
	// Token: 0x02000010 RID: 16
	[ComVisible(true)]
	public static class Registry
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00002614 File Offset: 0x00000814
		[SecurityCritical]
		private static RegistryKey GetBaseKeyFromKeyName(string keyName, out string subKeyName)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			int num = keyName.IndexOf('\\');
			string text;
			if (num != -1)
			{
				text = keyName.Substring(0, num).ToUpper(CultureInfo.InvariantCulture);
			}
			else
			{
				text = keyName.ToUpper(CultureInfo.InvariantCulture);
			}
			uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
			RegistryKey registryKey;
			if (num2 <= 1097425318U)
			{
				if (num2 != 126972219U)
				{
					if (num2 != 457190004U)
					{
						if (num2 == 1097425318U)
						{
							if (text == "HKEY_CLASSES_ROOT")
							{
								registryKey = Registry.ClassesRoot;
								goto IL_169;
							}
						}
					}
					else if (text == "HKEY_LOCAL_MACHINE")
					{
						registryKey = Registry.LocalMachine;
						goto IL_169;
					}
				}
				else if (text == "HKEY_CURRENT_CONFIG")
				{
					registryKey = Registry.CurrentConfig;
					goto IL_169;
				}
			}
			else if (num2 <= 1568329430U)
			{
				if (num2 != 1198714601U)
				{
					if (num2 == 1568329430U)
					{
						if (text == "HKEY_CURRENT_USER")
						{
							registryKey = Registry.CurrentUser;
							goto IL_169;
						}
					}
				}
				else if (text == "HKEY_USERS")
				{
					registryKey = Registry.Users;
					goto IL_169;
				}
			}
			else if (num2 != 2823865611U)
			{
				if (num2 == 3554990456U)
				{
					if (text == "HKEY_PERFORMANCE_DATA")
					{
						registryKey = Registry.PerformanceData;
						goto IL_169;
					}
				}
			}
			else if (text == "HKEY_DYN_DATA")
			{
				registryKey = RegistryKey.GetBaseKey(RegistryKey.HKEY_DYN_DATA);
				goto IL_169;
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_RegInvalidKeyName", new object[] { "keyName" }));
			IL_169:
			if (num == -1 || num == keyName.Length)
			{
				subKeyName = string.Empty;
			}
			else
			{
				subKeyName = keyName.Substring(num + 1, keyName.Length - num - 1);
			}
			return registryKey;
		}

		/// <summary>Retrieves the value associated with the specified name, in the specified registry key. If the name is not found in the specified key, returns a default value that you provide, or <see langword="null" /> if the specified key does not exist.</summary>
		/// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
		/// <param name="valueName">The name of the name/value pair.</param>
		/// <param name="defaultValue">The value to return if <paramref name="valueName" /> does not exist.</param>
		/// <returns>
		///   <see langword="null" /> if the subkey specified by <paramref name="keyName" /> does not exist; otherwise, the value associated with <paramref name="valueName" />, or <paramref name="defaultValue" /> if <paramref name="valueName" /> is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyName" /> does not begin with a valid registry root.</exception>
		// Token: 0x060000F2 RID: 242 RVA: 0x000027B8 File Offset: 0x000009B8
		[SecuritySafeCritical]
		public static object GetValue(string keyName, string valueName, object defaultValue)
		{
			string text;
			RegistryKey baseKeyFromKeyName = Registry.GetBaseKeyFromKeyName(keyName, out text);
			RegistryKey registryKey = baseKeyFromKeyName.OpenSubKey(text);
			if (registryKey == null)
			{
				return null;
			}
			object value;
			try
			{
				value = registryKey.GetValue(valueName, defaultValue);
			}
			finally
			{
				registryKey.Close();
			}
			return value;
		}

		/// <summary>Sets the specified name/value pair on the specified registry key. If the specified key does not exist, it is created.</summary>
		/// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
		/// <param name="valueName">The name of the name/value pair.</param>
		/// <param name="value">The value to be stored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyName" /> does not begin with a valid registry root.  
		/// -or-  
		/// <paramref name="keyName" /> is longer than the maximum length allowed (255 characters).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and thus cannot be written to; for example, it is a root-level node.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys.</exception>
		// Token: 0x060000F3 RID: 243 RVA: 0x00002800 File Offset: 0x00000A00
		public static void SetValue(string keyName, string valueName, object value)
		{
			Registry.SetValue(keyName, valueName, value, RegistryValueKind.Unknown);
		}

		/// <summary>Sets the name/value pair on the specified registry key, using the specified registry data type. If the specified key does not exist, it is created.</summary>
		/// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
		/// <param name="valueName">The name of the name/value pair.</param>
		/// <param name="value">The value to be stored.</param>
		/// <param name="valueKind">The registry data type to use when storing the data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyName" /> does not begin with a valid registry root.  
		/// -or-  
		/// <paramref name="keyName" /> is longer than the maximum length allowed (255 characters).  
		/// -or-  
		/// The type of <paramref name="value" /> did not match the registry data type specified by <paramref name="valueKind" />, therefore the data could not be converted properly.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and thus cannot be written to; for example, it is a root-level node, or the key has not been opened with write access.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys.</exception>
		// Token: 0x060000F4 RID: 244 RVA: 0x0000280C File Offset: 0x00000A0C
		[SecuritySafeCritical]
		public static void SetValue(string keyName, string valueName, object value, RegistryValueKind valueKind)
		{
			string text;
			RegistryKey baseKeyFromKeyName = Registry.GetBaseKeyFromKeyName(keyName, out text);
			RegistryKey registryKey = baseKeyFromKeyName.CreateSubKey(text);
			try
			{
				registryKey.SetValue(valueName, value, valueKind);
			}
			finally
			{
				registryKey.Close();
			}
		}

		/// <summary>Contains information about the current user preferences. This field reads the Windows registry base key HKEY_CURRENT_USER.</summary>
		// Token: 0x04000171 RID: 369
		public static readonly RegistryKey CurrentUser = RegistryKey.GetBaseKey(RegistryKey.HKEY_CURRENT_USER);

		/// <summary>Contains the configuration data for the local machine. This field reads the Windows registry base key HKEY_LOCAL_MACHINE.</summary>
		// Token: 0x04000172 RID: 370
		public static readonly RegistryKey LocalMachine = RegistryKey.GetBaseKey(RegistryKey.HKEY_LOCAL_MACHINE);

		/// <summary>Defines the types (or classes) of documents and the properties associated with those types. This field reads the Windows registry base key HKEY_CLASSES_ROOT.</summary>
		// Token: 0x04000173 RID: 371
		public static readonly RegistryKey ClassesRoot = RegistryKey.GetBaseKey(RegistryKey.HKEY_CLASSES_ROOT);

		/// <summary>Contains information about the default user configuration. This field reads the Windows registry base key HKEY_USERS.</summary>
		// Token: 0x04000174 RID: 372
		public static readonly RegistryKey Users = RegistryKey.GetBaseKey(RegistryKey.HKEY_USERS);

		/// <summary>Contains performance information for software components. This field reads the Windows registry base key HKEY_PERFORMANCE_DATA.</summary>
		// Token: 0x04000175 RID: 373
		public static readonly RegistryKey PerformanceData = RegistryKey.GetBaseKey(RegistryKey.HKEY_PERFORMANCE_DATA);

		/// <summary>Contains configuration information pertaining to the hardware that is not specific to the user. This field reads the Windows registry base key HKEY_CURRENT_CONFIG.</summary>
		// Token: 0x04000176 RID: 374
		public static readonly RegistryKey CurrentConfig = RegistryKey.GetBaseKey(RegistryKey.HKEY_CURRENT_CONFIG);

		/// <summary>Contains dynamic registry data. This field reads the Windows registry base key HKEY_DYN_DATA.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The operating system does not support dynamic data; that is, it is not Windows 98, Windows 98 Second Edition, or Windows Millennium Edition (Windows Me).</exception>
		// Token: 0x04000177 RID: 375
		[Obsolete("The DynData registry key only works on Win9x, which is no longer supported by the CLR.  On NT-based operating systems, use the PerformanceData registry key instead.")]
		public static readonly RegistryKey DynData = RegistryKey.GetBaseKey(RegistryKey.HKEY_DYN_DATA);
	}
}
