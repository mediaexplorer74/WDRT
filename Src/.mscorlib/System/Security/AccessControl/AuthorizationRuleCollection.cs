using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Represents a collection of <see cref="T:System.Security.AccessControl.AuthorizationRule" /> objects.</summary>
	// Token: 0x02000236 RID: 566
	public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
	{
		/// <summary>Adds an <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object to the collection.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object to add to the collection.</param>
		// Token: 0x06002049 RID: 8265 RVA: 0x000715BC File Offset: 0x0006F7BC
		public void AddRule(AuthorizationRule rule)
		{
			base.InnerList.Add(rule);
		}

		/// <summary>Copies the contents of the collection to an array.</summary>
		/// <param name="rules">An array to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index from which to begin copying.</param>
		// Token: 0x0600204A RID: 8266 RVA: 0x000715CB File Offset: 0x0006F7CB
		public void CopyTo(AuthorizationRule[] rules, int index)
		{
			((ICollection)this).CopyTo(rules, index);
		}

		/// <summary>Gets the <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object to get.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object at the specified index.</returns>
		// Token: 0x170003CA RID: 970
		public AuthorizationRule this[int index]
		{
			get
			{
				return base.InnerList[index] as AuthorizationRule;
			}
		}
	}
}
