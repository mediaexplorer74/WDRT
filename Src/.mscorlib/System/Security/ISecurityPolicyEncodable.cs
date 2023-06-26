using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Supports the methods that convert permission object state to and from an XML element representation.</summary>
	// Token: 0x020001D4 RID: 468
	[ComVisible(true)]
	public interface ISecurityPolicyEncodable
	{
		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <param name="level">The policy-level context to resolve named permission set references.</param>
		/// <returns>The root element of the XML representation of the policy object.</returns>
		// Token: 0x06001C75 RID: 7285
		SecurityElement ToXml(PolicyLevel level);

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy-level context to resolve named permission set references.</param>
		// Token: 0x06001C76 RID: 7286
		void FromXml(SecurityElement e, PolicyLevel level);
	}
}
