using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Defines methods implemented by permission types.</summary>
	// Token: 0x020001D2 RID: 466
	[ComVisible(true)]
	public interface IPermission : ISecurityEncodable
	{
		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001C6E RID: 7278
		IPermission Copy();

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not an instance of the same class as the current permission.</exception>
		// Token: 0x06001C6F RID: 7279
		IPermission Intersect(IPermission target);

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06001C70 RID: 7280
		IPermission Union(IPermission target);

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06001C71 RID: 7281
		bool IsSubsetOf(IPermission target);

		/// <summary>Throws a <see cref="T:System.Security.SecurityException" /> at run time if the security requirement is not met.</summary>
		// Token: 0x06001C72 RID: 7282
		void Demand();
	}
}
