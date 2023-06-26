using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its application directory. This class cannot be inherited.</summary>
	// Token: 0x02000340 RID: 832
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectoryMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		/// <summary>Determines whether the membership condition is satisfied by the specified evidence.</summary>
		/// <param name="evidence">The evidence set against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600298E RID: 10638 RVA: 0x0009A8A0 File Offset: 0x00098AA0
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0009A8B8 File Offset: 0x00098AB8
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			ApplicationDirectory hostEvidence = evidence.GetHostEvidence<ApplicationDirectory>();
			Url hostEvidence2 = evidence.GetHostEvidence<Url>();
			if (hostEvidence != null && hostEvidence2 != null)
			{
				string text = hostEvidence.Directory;
				if (text != null && text.Length > 1)
				{
					if (text[text.Length - 1] == '/')
					{
						text += "*";
					}
					else
					{
						text += "/*";
					}
					URLString urlstring = new URLString(text);
					if (hostEvidence2.GetURLString().IsSubsetOf(urlstring))
					{
						usedEvidence = hostEvidence;
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06002990 RID: 10640 RVA: 0x0009A93B File Offset: 0x00098B3B
		public IMembershipCondition Copy()
		{
			return new ApplicationDirectoryMembershipCondition();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002991 RID: 10641 RVA: 0x0009A942 File Offset: 0x00098B42
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid application directory membership condition element.</exception>
		// Token: 0x06002992 RID: 10642 RVA: 0x0009A94B File Offset: 0x00098B4B
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The policy level context for resolving named permission set references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002993 RID: 10643 RVA: 0x0009A958 File Offset: 0x00098B58
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.ApplicationDirectoryMembershipCondition");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level context, used to resolve named permission set references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid application directory membership condition element.</exception>
		// Token: 0x06002994 RID: 10644 RVA: 0x0009A992 File Offset: 0x00098B92
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

		/// <summary>Determines whether the specified membership condition is an <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified membership condition is an <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002995 RID: 10645 RVA: 0x0009A9C4 File Offset: 0x00098BC4
		public override bool Equals(object o)
		{
			return o is ApplicationDirectoryMembershipCondition;
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		// Token: 0x06002996 RID: 10646 RVA: 0x0009A9CF File Offset: 0x00098BCF
		public override int GetHashCode()
		{
			return typeof(ApplicationDirectoryMembershipCondition).GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		// Token: 0x06002997 RID: 10647 RVA: 0x0009A9E0 File Offset: 0x00098BE0
		public override string ToString()
		{
			return Environment.GetResourceString("ApplicationDirectory_ToString");
		}
	}
}
