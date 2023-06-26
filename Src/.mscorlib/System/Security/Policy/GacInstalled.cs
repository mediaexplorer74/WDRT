using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Confirms that a code assembly originates in the global assembly cache (GAC) as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000371 RID: 881
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Creates a new identity permission that corresponds to the current object.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission.</param>
		/// <returns>A new identity permission that corresponds to the current object.</returns>
		// Token: 0x06002BD6 RID: 11222 RVA: 0x000A46CB File Offset: 0x000A28CB
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <param name="o">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacInstalled" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BD7 RID: 11223 RVA: 0x000A46D2 File Offset: 0x000A28D2
		public override bool Equals(object o)
		{
			return o is GacInstalled;
		}

		/// <summary>Returns a hash code for the current object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06002BD8 RID: 11224 RVA: 0x000A46DD File Offset: 0x000A28DD
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000A46E0 File Offset: 0x000A28E0
		public override EvidenceBase Clone()
		{
			return new GacInstalled();
		}

		/// <summary>Creates an equivalent copy of the current object.</summary>
		/// <returns>An equivalent copy of <see cref="T:System.Security.Policy.GacInstalled" />.</returns>
		// Token: 0x06002BDA RID: 11226 RVA: 0x000A46E7 File Offset: 0x000A28E7
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000A46F0 File Offset: 0x000A28F0
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		/// <summary>Returns a string representation of the current  object.</summary>
		/// <returns>A string representation of the current object.</returns>
		// Token: 0x06002BDC RID: 11228 RVA: 0x000A471F File Offset: 0x000A291F
		public override string ToString()
		{
			return this.ToXml().ToString();
		}
	}
}
