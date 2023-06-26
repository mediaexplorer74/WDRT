using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Specifies when you can use a licensed object and provides a way of obtaining additional services needed to support licenses running within its domain.</summary>
	// Token: 0x0200057C RID: 1404
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class LicenseContext : IServiceProvider
	{
		/// <summary>When overridden in a derived class, gets a value that specifies when you can use a license.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values that specifies when you can use a license. The default is <see cref="F:System.ComponentModel.LicenseUsageMode.Runtime" />.</returns>
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000E3ABD File Offset: 0x000E1CBD
		public virtual LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Runtime;
			}
		}

		/// <summary>When overridden in a derived class, returns a saved license key for the specified type, from the specified resource assembly.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component.</param>
		/// <param name="resourceAssembly">An <see cref="T:System.Reflection.Assembly" /> with the license key.</param>
		/// <returns>The <see cref="P:System.ComponentModel.License.LicenseKey" /> for the specified type. This method returns <see langword="null" /> unless you override it.</returns>
		// Token: 0x060033E8 RID: 13288 RVA: 0x000E3AC0 File Offset: 0x000E1CC0
		public virtual string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Gets the requested service, if it is available.</summary>
		/// <param name="type">The type of service to retrieve.</param>
		/// <returns>An instance of the service, or <see langword="null" /> if the service cannot be found.</returns>
		// Token: 0x060033E9 RID: 13289 RVA: 0x000E3AC3 File Offset: 0x000E1CC3
		public virtual object GetService(Type type)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, sets a license key for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component associated with the license key.</param>
		/// <param name="key">The <see cref="P:System.ComponentModel.License.LicenseKey" /> to save for the type of component.</param>
		// Token: 0x060033EA RID: 13290 RVA: 0x000E3AC6 File Offset: 0x000E1CC6
		public virtual void SetSavedLicenseKey(Type type, string key)
		{
		}
	}
}
