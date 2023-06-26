using System;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides methods to verify the machine name and path conform to a specific syntax. This class cannot be inherited.</summary>
	// Token: 0x020005AD RID: 1453
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public static class SyntaxCheck
	{
		/// <summary>Checks the syntax of the machine name to confirm that it does not contain "\".</summary>
		/// <param name="value">A string containing the machine name to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> matches the proper machine name format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003619 RID: 13849 RVA: 0x000EBE4E File Offset: 0x000EA04E
		public static bool CheckMachineName(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.IndexOf('\\') == -1;
		}

		/// <summary>Checks the syntax of the path to see whether it starts with "\\".</summary>
		/// <param name="value">A string containing the path to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> matches the proper path format; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600361A RID: 13850 RVA: 0x000EBE77 File Offset: 0x000EA077
		public static bool CheckPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.StartsWith("\\\\");
		}

		/// <summary>Checks the syntax of the path to see if it starts with "\" or drive letter "C:".</summary>
		/// <param name="value">A string containing the path to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> matches the proper path format; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600361B RID: 13851 RVA: 0x000EBEA0 File Offset: 0x000EA0A0
		public static bool CheckRootedPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && Path.IsPathRooted(value);
		}
	}
}
