using System;
using System.Globalization;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that is not defined by one of the members of the <see cref="T:System.Security.AccessControl.AceType" /> enumeration.</summary>
	// Token: 0x02000202 RID: 514
	public sealed class CustomAce : GenericAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CustomAce" /> class.</summary>
		/// <param name="type">Type of the new Access Control Entry (ACE). This value must be greater than <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" />.</param>
		/// <param name="flags">Flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new ACE.</param>
		/// <param name="opaque">An array of byte values that contains the data for the new ACE. This value can be <see langword="null" />. The length of this array must not be greater than the value of the <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> field, and must be a multiple of four.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="type" /> parameter is not greater than <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" /> or the length of the <paramref name="opaque" /> array is either greater than the value of the <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> field or not a multiple of four.</exception>
		// Token: 0x06001E58 RID: 7768 RVA: 0x0006A104 File Offset: 0x00068304
		public CustomAce(AceType type, AceFlags flags, byte[] opaque)
			: base(type, flags)
		{
			if (type <= AceType.SystemAlarmCallbackObject)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_InvalidUserDefinedAceType"));
			}
			this.SetOpaque(opaque);
		}

		/// <summary>Gets the length of the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0006A12F File Offset: 0x0006832F
		public int OpaqueLength
		{
			get
			{
				if (this._opaque == null)
				{
					return 0;
				}
				return this._opaque.Length;
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CustomAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.CustomAce.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CustomAce" /> object.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0006A143 File Offset: 0x00068343
		public override int BinaryLength
		{
			get
			{
				return 4 + this.OpaqueLength;
			}
		}

		/// <summary>Returns the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <returns>An array of byte values that represents the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</returns>
		// Token: 0x06001E5B RID: 7771 RVA: 0x0006A14D File Offset: 0x0006834D
		public byte[] GetOpaque()
		{
			return this._opaque;
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</param>
		// Token: 0x06001E5C RID: 7772 RVA: 0x0006A158 File Offset: 0x00068358
		public void SetOpaque(byte[] opaque)
		{
			if (opaque != null)
			{
				if (opaque.Length > CustomAce.MaxOpaqueLength)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), 0, CustomAce.MaxOpaqueLength));
				}
				if (opaque.Length % 4 != 0)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), 4));
				}
			}
			this._opaque = opaque;
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CustomAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CustomAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.CustomAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06001E5D RID: 7773 RVA: 0x0006A1D4 File Offset: 0x000683D4
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			base.MarshalHeader(binaryForm, offset);
			offset += 4;
			if (this.OpaqueLength != 0)
			{
				if (this.OpaqueLength > CustomAce.MaxOpaqueLength)
				{
					throw new SystemException();
				}
				this.GetOpaque().CopyTo(binaryForm, offset);
			}
		}

		// Token: 0x04000AEB RID: 2795
		private byte[] _opaque;

		/// <summary>Returns the maximum allowed length of an opaque data blob for this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		// Token: 0x04000AEC RID: 2796
		public static readonly int MaxOpaqueLength = 65531;
	}
}
