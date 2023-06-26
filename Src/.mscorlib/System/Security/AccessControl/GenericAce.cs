using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE), and is the base class for all other ACE classes.</summary>
	// Token: 0x02000200 RID: 512
	public abstract class GenericAce
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x00069B3C File Offset: 0x00067D3C
		internal void MarshalHeader(byte[] binaryForm, int offset)
		{
			int binaryLength = this.BinaryLength;
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
			if (binaryLength > 65535)
			{
				throw new SystemException();
			}
			binaryForm[offset] = (byte)this.AceType;
			binaryForm[offset + 1] = (byte)this.AceFlags;
			binaryForm[offset + 2] = (byte)binaryLength;
			binaryForm[offset + 3] = (byte)(binaryLength >> 8);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00069BCB File Offset: 0x00067DCB
		internal GenericAce(AceType type, AceFlags flags)
		{
			this._type = type;
			this._flags = flags;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00069BE4 File Offset: 0x00067DE4
		internal static AceFlags AceFlagsFromAuditFlags(AuditFlags auditFlags)
		{
			AceFlags aceFlags = AceFlags.None;
			if ((auditFlags & AuditFlags.Success) != AuditFlags.None)
			{
				aceFlags |= AceFlags.SuccessfulAccess;
			}
			if ((auditFlags & AuditFlags.Failure) != AuditFlags.None)
			{
				aceFlags |= AceFlags.FailedAccess;
			}
			if (aceFlags == AceFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "auditFlags");
			}
			return aceFlags;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00069C24 File Offset: 0x00067E24
		internal static AceFlags AceFlagsFromInheritanceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			AceFlags aceFlags = AceFlags.None;
			if ((inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ContainerInherit;
			}
			if ((inheritanceFlags & InheritanceFlags.ObjectInherit) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ObjectInherit;
			}
			if (aceFlags != AceFlags.None)
			{
				if ((propagationFlags & PropagationFlags.NoPropagateInherit) != PropagationFlags.None)
				{
					aceFlags |= AceFlags.NoPropagateInherit;
				}
				if ((propagationFlags & PropagationFlags.InheritOnly) != PropagationFlags.None)
				{
					aceFlags |= AceFlags.InheritOnly;
				}
			}
			return aceFlags;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00069C5C File Offset: 0x00067E5C
		internal static void VerifyHeader(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < 4)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			if (((int)binaryForm[offset + 3] << 8) + (int)binaryForm[offset + 2] > binaryForm.Length - offset)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
		}

		/// <summary>Creates a <see cref="T:System.Security.AccessControl.GenericAce" /> object from the specified binary data.</summary>
		/// <param name="binaryForm">The binary data from which to create the new <see cref="T:System.Security.AccessControl.GenericAce" /> object.</param>
		/// <param name="offset">The offset at which to begin unmarshaling.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> object this method creates.</returns>
		// Token: 0x06001E44 RID: 7748 RVA: 0x00069CD8 File Offset: 0x00067ED8
		public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
		{
			GenericAce.VerifyHeader(binaryForm, offset);
			AceType aceType = (AceType)binaryForm[offset];
			GenericAce genericAce;
			if (aceType == AceType.AccessAllowed || aceType == AceType.AccessDenied || aceType == AceType.SystemAudit || aceType == AceType.SystemAlarm || aceType == AceType.AccessAllowedCallback || aceType == AceType.AccessDeniedCallback || aceType == AceType.SystemAuditCallback || aceType == AceType.SystemAlarmCallback)
			{
				AceQualifier aceQualifier;
				int num;
				SecurityIdentifier securityIdentifier;
				bool flag;
				byte[] array;
				if (!CommonAce.ParseBinaryForm(binaryForm, offset, out aceQualifier, out num, out securityIdentifier, out flag, out array))
				{
					goto IL_1A8;
				}
				AceFlags aceFlags = (AceFlags)binaryForm[offset + 1];
				genericAce = new CommonAce(aceFlags, aceQualifier, num, securityIdentifier, flag, array);
			}
			else if (aceType == AceType.AccessAllowedObject || aceType == AceType.AccessDeniedObject || aceType == AceType.SystemAuditObject || aceType == AceType.SystemAlarmObject || aceType == AceType.AccessAllowedCallbackObject || aceType == AceType.AccessDeniedCallbackObject || aceType == AceType.SystemAuditCallbackObject || aceType == AceType.SystemAlarmCallbackObject)
			{
				AceQualifier aceQualifier2;
				int num2;
				SecurityIdentifier securityIdentifier2;
				ObjectAceFlags objectAceFlags;
				Guid guid;
				Guid guid2;
				bool flag2;
				byte[] array2;
				if (!ObjectAce.ParseBinaryForm(binaryForm, offset, out aceQualifier2, out num2, out securityIdentifier2, out objectAceFlags, out guid, out guid2, out flag2, out array2))
				{
					goto IL_1A8;
				}
				AceFlags aceFlags2 = (AceFlags)binaryForm[offset + 1];
				genericAce = new ObjectAce(aceFlags2, aceQualifier2, num2, securityIdentifier2, objectAceFlags, guid, guid2, flag2, array2);
			}
			else if (aceType == AceType.AccessAllowedCompound)
			{
				int num3;
				CompoundAceType compoundAceType;
				SecurityIdentifier securityIdentifier3;
				if (!CompoundAce.ParseBinaryForm(binaryForm, offset, out num3, out compoundAceType, out securityIdentifier3))
				{
					goto IL_1A8;
				}
				AceFlags aceFlags3 = (AceFlags)binaryForm[offset + 1];
				genericAce = new CompoundAce(aceFlags3, num3, compoundAceType, securityIdentifier3);
			}
			else
			{
				AceFlags aceFlags4 = (AceFlags)binaryForm[offset + 1];
				byte[] array3 = null;
				int num4 = (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8);
				if (num4 % 4 != 0)
				{
					goto IL_1A8;
				}
				int num5 = num4 - 4;
				if (num5 > 0)
				{
					array3 = new byte[num5];
					for (int i = 0; i < num5; i++)
					{
						array3[i] = binaryForm[offset + num4 - num5 + i];
					}
				}
				genericAce = new CustomAce(aceType, aceFlags4, array3);
			}
			if ((genericAce is ObjectAce || (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8) == genericAce.BinaryLength) && (!(genericAce is ObjectAce) || (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8) == genericAce.BinaryLength || (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8) - 32 == genericAce.BinaryLength))
			{
				return genericAce;
			}
			IL_1A8:
			throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAceBinaryForm"), "binaryForm");
		}

		/// <summary>Gets the type of this Access Control Entry (ACE).</summary>
		/// <returns>The type of this ACE.</returns>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x00069EA1 File Offset: 0x000680A1
		public AceType AceType
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.AceFlags" /> associated with this <see cref="T:System.Security.AccessControl.GenericAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AceFlags" /> associated with this <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x00069EA9 File Offset: 0x000680A9
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x00069EB1 File Offset: 0x000680B1
		public AceFlags AceFlags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this Access Control Entry (ACE) is inherited or is set explicitly.</summary>
		/// <returns>
		///   <see langword="true" /> if this ACE is inherited; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x00069EBA File Offset: 0x000680BA
		public bool IsInherited
		{
			get
			{
				return (this.AceFlags & AceFlags.Inherited) > AceFlags.None;
			}
		}

		/// <summary>Gets flags that specify the inheritance properties of this Access Control Entry (ACE).</summary>
		/// <returns>Flags that specify the inheritance properties of this ACE.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x00069EC8 File Offset: 0x000680C8
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				InheritanceFlags inheritanceFlags = InheritanceFlags.None;
				if ((this.AceFlags & AceFlags.ContainerInherit) != AceFlags.None)
				{
					inheritanceFlags |= InheritanceFlags.ContainerInherit;
				}
				if ((this.AceFlags & AceFlags.ObjectInherit) != AceFlags.None)
				{
					inheritanceFlags |= InheritanceFlags.ObjectInherit;
				}
				return inheritanceFlags;
			}
		}

		/// <summary>Gets flags that specify the inheritance propagation properties of this Access Control Entry (ACE).</summary>
		/// <returns>Flags that specify the inheritance propagation properties of this ACE.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x00069EF4 File Offset: 0x000680F4
		public PropagationFlags PropagationFlags
		{
			get
			{
				PropagationFlags propagationFlags = PropagationFlags.None;
				if ((this.AceFlags & AceFlags.InheritOnly) != AceFlags.None)
				{
					propagationFlags |= PropagationFlags.InheritOnly;
				}
				if ((this.AceFlags & AceFlags.NoPropagateInherit) != AceFlags.None)
				{
					propagationFlags |= PropagationFlags.NoPropagateInherit;
				}
				return propagationFlags;
			}
		}

		/// <summary>Gets the audit information associated with this Access Control Entry (ACE).</summary>
		/// <returns>The audit information associated with this Access Control Entry (ACE).</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00069F20 File Offset: 0x00068120
		public AuditFlags AuditFlags
		{
			get
			{
				AuditFlags auditFlags = AuditFlags.None;
				if ((this.AceFlags & AceFlags.SuccessfulAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Success;
				}
				if ((this.AceFlags & AceFlags.FailedAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Failure;
				}
				return auditFlags;
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericAce.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001E4C RID: 7756
		public abstract int BinaryLength { get; }

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.GenericAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06001E4D RID: 7757
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		/// <summary>Creates a deep copy of this Access Control Entry (ACE).</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> object that this method creates.</returns>
		// Token: 0x06001E4E RID: 7758 RVA: 0x00069F54 File Offset: 0x00068154
		public GenericAce Copy()
		{
			byte[] array = new byte[this.BinaryLength];
			this.GetBinaryForm(array, 0);
			return GenericAce.CreateFromBinaryForm(array, 0);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> object is equal to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</summary>
		/// <param name="o">The <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.AccessControl.GenericAce" /> object is equal to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E4F RID: 7759 RVA: 0x00069F7C File Offset: 0x0006817C
		public sealed override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			GenericAce genericAce = o as GenericAce;
			if (genericAce == null)
			{
				return false;
			}
			if (this.AceType != genericAce.AceType || this.AceFlags != genericAce.AceFlags)
			{
				return false;
			}
			int binaryLength = this.BinaryLength;
			int binaryLength2 = genericAce.BinaryLength;
			if (binaryLength != binaryLength2)
			{
				return false;
			}
			byte[] array = new byte[binaryLength];
			byte[] array2 = new byte[binaryLength2];
			this.GetBinaryForm(array, 0);
			genericAce.GetBinaryForm(array2, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Security.AccessControl.GenericAce" /> class. The  <see cref="M:System.Security.AccessControl.GenericAce.GetHashCode" /> method is suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x06001E50 RID: 7760 RVA: 0x0006A014 File Offset: 0x00068214
		public sealed override int GetHashCode()
		{
			int binaryLength = this.BinaryLength;
			byte[] array = new byte[binaryLength];
			this.GetBinaryForm(array, 0);
			int num = 0;
			for (int i = 0; i < binaryLength; i += 4)
			{
				int num2 = (int)array[i] + ((int)array[i + 1] << 8) + ((int)array[i + 2] << 16) + ((int)array[i + 3] << 24);
				num ^= num2;
			}
			return num;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> objects are considered equal.</summary>
		/// <param name="left">The first <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare.</param>
		/// <param name="right">The second <see cref="T:System.Security.AccessControl.GenericAce" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Security.AccessControl.GenericAce" /> objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E51 RID: 7761 RVA: 0x0006A06C File Offset: 0x0006826C
		public static bool operator ==(GenericAce left, GenericAce right)
		{
			return (left == null && right == null) || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> objects are considered unequal.</summary>
		/// <param name="left">The first <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare.</param>
		/// <param name="right">The second <see cref="T:System.Security.AccessControl.GenericAce" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Security.AccessControl.GenericAce" /> objects are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E52 RID: 7762 RVA: 0x0006A094 File Offset: 0x00068294
		public static bool operator !=(GenericAce left, GenericAce right)
		{
			return !(left == right);
		}

		// Token: 0x04000AE4 RID: 2788
		private readonly AceType _type;

		// Token: 0x04000AE5 RID: 2789
		private AceFlags _flags;

		// Token: 0x04000AE6 RID: 2790
		internal ushort _indexInAcl;

		// Token: 0x04000AE7 RID: 2791
		internal const int HeaderLength = 4;
	}
}
