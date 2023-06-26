using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies permission to request information from Domain Name Servers.</summary>
	// Token: 0x020000E1 RID: 225
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DnsPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x060007B9 RID: 1977 RVA: 0x0002B1A7 File Offset: 0x000293A7
		public DnsPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.DnsPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> that corresponds to the security declaration.</returns>
		// Token: 0x060007BA RID: 1978 RVA: 0x0002B1B0 File Offset: 0x000293B0
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new DnsPermission(PermissionState.Unrestricted);
			}
			return new DnsPermission(PermissionState.None);
		}
	}
}
