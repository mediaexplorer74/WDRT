using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a design-time license context that can support a license provider at design time.</summary>
	// Token: 0x020005D9 RID: 1497
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesigntimeLicenseContext : LicenseContext
	{
		/// <summary>Gets the license usage mode.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseUsageMode" /> indicating the licensing mode for the context.</returns>
		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000F0106 File Offset: 0x000EE306
		public override LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Designtime;
			}
		}

		/// <summary>Gets a saved license key.</summary>
		/// <param name="type">The type of the license key.</param>
		/// <param name="resourceAssembly">The assembly to get the key from.</param>
		/// <returns>The saved license key that matches the specified type.</returns>
		// Token: 0x060037A3 RID: 14243 RVA: 0x000F0109 File Offset: 0x000EE309
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Sets a saved license key.</summary>
		/// <param name="type">The type of the license key.</param>
		/// <param name="key">The license key.</param>
		// Token: 0x060037A4 RID: 14244 RVA: 0x000F010C File Offset: 0x000EE30C
		public override void SetSavedLicenseKey(Type type, string key)
		{
			this.savedLicenseKeys[type.AssemblyQualifiedName] = key;
		}

		// Token: 0x04002AE5 RID: 10981
		internal Hashtable savedLicenseKeys = new Hashtable();
	}
}
