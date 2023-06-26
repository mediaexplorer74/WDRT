using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Represents a membership condition that matches all code. This class cannot be inherited.</summary>
	// Token: 0x0200033D RID: 829
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The evidence set against which to make the test.</param>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x06002976 RID: 10614 RVA: 0x0009A640 File Offset: 0x00098840
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x0009A658 File Offset: 0x00098858
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			return true;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06002978 RID: 10616 RVA: 0x0009A65E File Offset: 0x0009885E
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A representation of the membership condition.</returns>
		// Token: 0x06002979 RID: 10617 RVA: 0x0009A665 File Offset: 0x00098865
		public override string ToString()
		{
			return Environment.GetResourceString("All_ToString");
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x0600297A RID: 10618 RVA: 0x0009A671 File Offset: 0x00098871
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x0600297B RID: 10619 RVA: 0x0009A67A File Offset: 0x0009887A
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The policy level context for resolving named permission set references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x0600297C RID: 10620 RVA: 0x0009A684 File Offset: 0x00098884
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.AllMembershipCondition");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level context used to resolve named permission set references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x0600297D RID: 10621 RVA: 0x0009A6BE File Offset: 0x000988BE
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
		}

		/// <summary>Determines whether the specified membership condition is an <see cref="T:System.Security.Policy.AllMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to <see cref="T:System.Security.Policy.AllMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified membership condition is an <see cref="T:System.Security.Policy.AllMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600297E RID: 10622 RVA: 0x0009A6F0 File Offset: 0x000988F0
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		// Token: 0x0600297F RID: 10623 RVA: 0x0009A6FB File Offset: 0x000988FB
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}
	}
}
