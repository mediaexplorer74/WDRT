using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Encapsulates all Access Control Entry (ACE) types currently defined by Microsoft Corporation. All <see cref="T:System.Security.AccessControl.KnownAce" /> objects contain a 32-bit access mask and a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
	// Token: 0x02000201 RID: 513
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x0006A0A0 File Offset: 0x000682A0
		internal KnownAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier securityIdentifier)
			: base(type, flags)
		{
			if (securityIdentifier == null)
			{
				throw new ArgumentNullException("securityIdentifier");
			}
			this.AccessMask = accessMask;
			this.SecurityIdentifier = securityIdentifier;
		}

		/// <summary>Gets or sets the access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0006A0CE File Offset: 0x000682CE
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x0006A0D6 File Offset: 0x000682D6
		public int AccessMask
		{
			get
			{
				return this._accessMask;
			}
			set
			{
				this._accessMask = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x0006A0DF File Offset: 0x000682DF
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x0006A0E7 File Offset: 0x000682E7
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this._sid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._sid = value;
			}
		}

		// Token: 0x04000AE8 RID: 2792
		private int _accessMask;

		// Token: 0x04000AE9 RID: 2793
		private SecurityIdentifier _sid;

		// Token: 0x04000AEA RID: 2794
		internal const int AccessMaskLength = 4;
	}
}
