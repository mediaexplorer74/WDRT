using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides the <see langword="abstract" /> base class for implementing a license provider.</summary>
	// Token: 0x0200057F RID: 1407
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class LicenseProvider
	{
		/// <summary>When overridden in a derived class, gets a license for an instance or type of component, when given a context and whether the denial of a license throws an exception.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies where you can use the licensed object.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component requesting the license.</param>
		/// <param name="instance">An object that is requesting the license.</param>
		/// <param name="allowExceptions">
		///   <see langword="true" /> if a <see cref="T:System.ComponentModel.LicenseException" /> should be thrown when the component cannot be granted a license; otherwise, <see langword="false" />.</param>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />.</returns>
		// Token: 0x06003408 RID: 13320
		public abstract License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions);
	}
}
