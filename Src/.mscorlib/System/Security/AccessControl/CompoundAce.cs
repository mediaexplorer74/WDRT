using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a compound Access Control Entry (ACE).</summary>
	// Token: 0x02000204 RID: 516
	public sealed class CompoundAce : KnownAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CompoundAce" /> class.</summary>
		/// <param name="flags">Contains flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new Access Control Entry (ACE).</param>
		/// <param name="accessMask">The access mask for the ACE.</param>
		/// <param name="compoundAceType">A value from the <see cref="T:System.Security.AccessControl.CompoundAceType" /> enumeration.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> associated with the new ACE.</param>
		// Token: 0x06001E5F RID: 7775 RVA: 0x0006A217 File Offset: 0x00068417
		public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid)
			: base(AceType.AccessAllowedCompound, flags, accessMask, sid)
		{
			this._compoundAceType = compoundAceType;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0006A22C File Offset: 0x0006842C
		internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out int accessMask, out CompoundAceType compoundAceType, out SecurityIdentifier sid)
		{
			GenericAce.VerifyHeader(binaryForm, offset);
			if (binaryForm.Length - offset >= 12 + SecurityIdentifier.MinBinaryLength)
			{
				int num = offset + 4;
				int num2 = 0;
				accessMask = (int)binaryForm[num] + ((int)binaryForm[num + 1] << 8) + ((int)binaryForm[num + 2] << 16) + ((int)binaryForm[num + 3] << 24);
				num2 += 4;
				compoundAceType = (CompoundAceType)((int)binaryForm[num + num2] + ((int)binaryForm[num + num2 + 1] << 8));
				num2 += 4;
				sid = new SecurityIdentifier(binaryForm, num + num2);
				return true;
			}
			accessMask = 0;
			compoundAceType = (CompoundAceType)0;
			sid = null;
			return false;
		}

		/// <summary>Gets or sets the type of this <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</summary>
		/// <returns>The type of this <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0006A2A6 File Offset: 0x000684A6
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x0006A2AE File Offset: 0x000684AE
		public CompoundAceType CompoundAceType
		{
			get
			{
				return this._compoundAceType;
			}
			set
			{
				this._compoundAceType = value;
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CompoundAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.CompoundAce.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</returns>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0006A2B7 File Offset: 0x000684B7
		public override int BinaryLength
		{
			get
			{
				return 12 + base.SecurityIdentifier.BinaryLength;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CompoundAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CompoundAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.CompoundAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06001E64 RID: 7780 RVA: 0x0006A2C8 File Offset: 0x000684C8
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			base.MarshalHeader(binaryForm, offset);
			int num = offset + 4;
			int num2 = 0;
			binaryForm[num] = (byte)base.AccessMask;
			binaryForm[num + 1] = (byte)(base.AccessMask >> 8);
			binaryForm[num + 2] = (byte)(base.AccessMask >> 16);
			binaryForm[num + 3] = (byte)(base.AccessMask >> 24);
			num2 += 4;
			binaryForm[num + num2] = (byte)((ushort)this.CompoundAceType);
			binaryForm[num + num2 + 1] = (byte)((ushort)this.CompoundAceType >> 8);
			binaryForm[num + num2 + 2] = 0;
			binaryForm[num + num2 + 3] = 0;
			num2 += 4;
			base.SecurityIdentifier.GetBinaryForm(binaryForm, num + num2);
		}

		// Token: 0x04000AEF RID: 2799
		private CompoundAceType _compoundAceType;

		// Token: 0x04000AF0 RID: 2800
		private const int AceTypeLength = 4;
	}
}
