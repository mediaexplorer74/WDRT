using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides the <see langword="abstract" /> base class for all licenses. A license is granted to a specific instance of a component.</summary>
	// Token: 0x0200057B RID: 1403
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class License : IDisposable
	{
		/// <summary>When overridden in a derived class, gets the license key granted to this component.</summary>
		/// <returns>A license key granted to this component.</returns>
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060033E4 RID: 13284
		public abstract string LicenseKey { get; }

		/// <summary>When overridden in a derived class, disposes of the resources used by the license.</summary>
		// Token: 0x060033E5 RID: 13285
		public abstract void Dispose();
	}
}
