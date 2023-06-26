using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x02000238 RID: 568
	public abstract class GenericSecurityDescriptor
	{
		// Token: 0x0600204C RID: 8268 RVA: 0x000715E8 File Offset: 0x0006F7E8
		private static void MarshalInt(byte[] binaryForm, int offset, int number)
		{
			binaryForm[offset] = (byte)number;
			binaryForm[offset + 1] = (byte)(number >> 8);
			binaryForm[offset + 2] = (byte)(number >> 16);
			binaryForm[offset + 3] = (byte)(number >> 24);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0007160C File Offset: 0x0006F80C
		internal static int UnmarshalInt(byte[] binaryForm, int offset)
		{
			return (int)binaryForm[offset] + ((int)binaryForm[offset + 1] << 8) + ((int)binaryForm[offset + 2] << 16) + ((int)binaryForm[offset + 3] << 24);
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600204F RID: 8271
		internal abstract GenericAcl GenericSacl { get; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06002050 RID: 8272
		internal abstract GenericAcl GenericDacl { get; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x00071633 File Offset: 0x0006F833
		private bool IsCraftedAefaDacl
		{
			get
			{
				return this.GenericDacl is DiscretionaryAcl && (this.GenericDacl as DiscretionaryAcl).EveryOneFullAccessForNullDacl;
			}
		}

		/// <summary>Returns a boolean value that specifies whether the security descriptor associated with this  <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object can be converted to the Security Descriptor Definition Language (SDDL) format.</summary>
		/// <returns>
		///   <see langword="true" /> if the security descriptor associated with this  <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object can be converted to the Security Descriptor Definition Language (SDDL) format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002052 RID: 8274 RVA: 0x00071654 File Offset: 0x0006F854
		public static bool IsSddlConversionSupported()
		{
			return true;
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00071657 File Offset: 0x0006F857
		public static byte Revision
		{
			get
			{
				return 1;
			}
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06002054 RID: 8276
		public abstract ControlFlags ControlFlags { get; }

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06002055 RID: 8277
		// (set) Token: 0x06002056 RID: 8278
		public abstract SecurityIdentifier Owner { get; set; }

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06002057 RID: 8279
		// (set) Token: 0x06002058 RID: 8280
		public abstract SecurityIdentifier Group { get; set; }

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericSecurityDescriptor.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0007165C File Offset: 0x0006F85C
		public int BinaryLength
		{
			get
			{
				int num = 20;
				if (this.Owner != null)
				{
					num += this.Owner.BinaryLength;
				}
				if (this.Group != null)
				{
					num += this.Group.BinaryLength;
				}
				if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
				{
					num += this.GenericSacl.BinaryLength;
				}
				if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
				{
					num += this.GenericDacl.BinaryLength;
				}
				return num;
			}
		}

		/// <summary>Returns the Security Descriptor Definition Language (SDDL) representation of the specified sections of the security descriptor that this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object represents.</summary>
		/// <param name="includeSections">Specifies which sections (access rules, audit rules, primary group, owner) of the security descriptor to get.</param>
		/// <returns>The SDDL representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x0600205A RID: 8282 RVA: 0x000716F0 File Offset: 0x0006F8F0
		[SecuritySafeCritical]
		public string GetSddlForm(AccessControlSections includeSections)
		{
			byte[] array = new byte[this.BinaryLength];
			this.GetBinaryForm(array, 0);
			SecurityInfos securityInfos = (SecurityInfos)0;
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Owner;
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Group;
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.SystemAcl;
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
			}
			string text;
			int num = Win32.ConvertSdToSddl(array, 1, securityInfos, out text);
			if (num == 87 || num == 1305)
			{
				throw new InvalidOperationException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
			return text;
		}

		/// <summary>Returns an array of byte values that represents the information contained in this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x0600205B RID: 8283 RVA: 0x00071760 File Offset: 0x0006F960
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < this.BinaryLength)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			int num = offset;
			int binaryLength = this.BinaryLength;
			byte b = ((this is RawSecurityDescriptor && (this.ControlFlags & ControlFlags.RMControlValid) != ControlFlags.None) ? (this as RawSecurityDescriptor).ResourceManagerControl : 0);
			int num2 = (int)this.ControlFlags;
			if (this.IsCraftedAefaDacl)
			{
				num2 &= -5;
			}
			binaryForm[offset] = GenericSecurityDescriptor.Revision;
			binaryForm[offset + 1] = b;
			binaryForm[offset + 2] = (byte)num2;
			binaryForm[offset + 3] = (byte)(num2 >> 8);
			int num3 = offset + 4;
			int num4 = offset + 8;
			int num5 = offset + 12;
			int num6 = offset + 16;
			offset += 20;
			if (this.Owner != null)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, num3, offset - num);
				this.Owner.GetBinaryForm(binaryForm, offset);
				offset += this.Owner.BinaryLength;
			}
			else
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, num3, 0);
			}
			if (this.Group != null)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, num4, offset - num);
				this.Group.GetBinaryForm(binaryForm, offset);
				offset += this.Group.BinaryLength;
			}
			else
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, num4, 0);
			}
			if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, num5, offset - num);
				this.GenericSacl.GetBinaryForm(binaryForm, offset);
				offset += this.GenericSacl.BinaryLength;
			}
			else
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, num5, 0);
			}
			if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, num6, offset - num);
				this.GenericDacl.GetBinaryForm(binaryForm, offset);
				offset += this.GenericDacl.BinaryLength;
				return;
			}
			GenericSecurityDescriptor.MarshalInt(binaryForm, num6, 0);
		}

		// Token: 0x04000BC1 RID: 3009
		internal const int HeaderLength = 20;

		// Token: 0x04000BC2 RID: 3010
		internal const int OwnerFoundAt = 4;

		// Token: 0x04000BC3 RID: 3011
		internal const int GroupFoundAt = 8;

		// Token: 0x04000BC4 RID: 3012
		internal const int SaclFoundAt = 12;

		// Token: 0x04000BC5 RID: 3013
		internal const int DaclFoundAt = 16;
	}
}
