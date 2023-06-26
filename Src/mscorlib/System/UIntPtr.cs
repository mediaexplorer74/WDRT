using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>A platform-specific type that is used to represent a pointer or a handle.</summary>
	// Token: 0x02000153 RID: 339
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct UIntPtr : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UIntPtr" /> structure using the specified 32-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 32-bit unsigned integer.</param>
		// Token: 0x06001540 RID: 5440 RVA: 0x0003E456 File Offset: 0x0003C656
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public UIntPtr(uint value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified 64-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 64-bit unsigned integer.</param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />.</exception>
		// Token: 0x06001541 RID: 5441 RVA: 0x0003E460 File Offset: 0x0003C660
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public UIntPtr(ulong value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified pointer to an unspecified type.</summary>
		/// <param name="value">A pointer to an unspecified type.</param>
		// Token: 0x06001542 RID: 5442 RVA: 0x0003E46A File Offset: 0x0003C66A
		[SecurityCritical]
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe UIntPtr(void* value)
		{
			this.m_value = value;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0003E474 File Offset: 0x0003C674
		[SecurityCritical]
		private UIntPtr(SerializationInfo info, StreamingContext context)
		{
			ulong @uint = info.GetUInt64("value");
			if (UIntPtr.Size == 4 && @uint > (ulong)(-1))
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_InvalidPtrValue"));
			}
			this.m_value = @uint;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.UIntPtr" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06001544 RID: 5444 RVA: 0x0003E4B2 File Offset: 0x0003C6B2
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", this.m_value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.UIntPtr" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001545 RID: 5445 RVA: 0x0003E4D4 File Offset: 0x0003C6D4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is UIntPtr && this.m_value == ((UIntPtr)obj).m_value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001546 RID: 5446 RVA: 0x0003E4F3 File Offset: 0x0003C6F3
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_value & int.MaxValue;
		}

		/// <summary>Converts the value of this instance to a 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer equal to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of this instance is too large to represent as a 32-bit unsigned integer.</exception>
		// Token: 0x06001547 RID: 5447 RVA: 0x0003E503 File Offset: 0x0003C703
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public uint ToUInt32()
		{
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer equal to the value of this instance.</returns>
		// Token: 0x06001548 RID: 5448 RVA: 0x0003E50C File Offset: 0x0003C70C
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public ulong ToUInt64()
		{
			return this.m_value;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06001549 RID: 5449 RVA: 0x0003E518 File Offset: 0x0003C718
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.m_value.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the value of a 32-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <param name="value">A 32-bit unsigned integer.</param>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		// Token: 0x0600154A RID: 5450 RVA: 0x0003E539 File Offset: 0x0003C739
		[NonVersionable]
		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the value of a 64-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <param name="value">A 64-bit unsigned integer.</param>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />.</exception>
		// Token: 0x0600154B RID: 5451 RVA: 0x0003E541 File Offset: 0x0003C741
		[NonVersionable]
		public static explicit operator UIntPtr(ulong value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 32-bit unsigned integer.</summary>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of <paramref name="value" /> is too large to represent as a 32-bit unsigned integer.</exception>
		// Token: 0x0600154C RID: 5452 RVA: 0x0003E549 File Offset: 0x0003C749
		[SecuritySafeCritical]
		[NonVersionable]
		public static explicit operator uint(UIntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 64-bit unsigned integer.</summary>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <returns>The contents of <paramref name="value" />.</returns>
		// Token: 0x0600154D RID: 5453 RVA: 0x0003E553 File Offset: 0x0003C753
		[SecuritySafeCritical]
		[NonVersionable]
		public static explicit operator ulong(UIntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Converts the specified pointer to an unspecified type to an <see cref="T:System.UIntPtr" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">A pointer to an unspecified type.</param>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		// Token: 0x0600154E RID: 5454 RVA: 0x0003E55D File Offset: 0x0003C75D
		[SecurityCritical]
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe static explicit operator UIntPtr(void* value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a pointer to an unspecified type.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <returns>The contents of <paramref name="value" />.</returns>
		// Token: 0x0600154F RID: 5455 RVA: 0x0003E565 File Offset: 0x0003C765
		[SecurityCritical]
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe static explicit operator void*(UIntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are equal.</summary>
		/// <param name="value1">The first pointer or handle to compare.</param>
		/// <param name="value2">The second pointer or handle to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> equals <paramref name="value2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001550 RID: 5456 RVA: 0x0003E56E File Offset: 0x0003C76E
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(UIntPtr value1, UIntPtr value2)
		{
			return value1.m_value == value2.m_value;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are not equal.</summary>
		/// <param name="value1">The first pointer or handle to compare.</param>
		/// <param name="value2">The second pointer or handle to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> does not equal <paramref name="value2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001551 RID: 5457 RVA: 0x0003E580 File Offset: 0x0003C780
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(UIntPtr value1, UIntPtr value2)
		{
			return value1.m_value != value2.m_value;
		}

		/// <summary>Adds an offset to the value of an unsigned pointer.</summary>
		/// <param name="pointer">The unsigned pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		/// <returns>A new unsigned pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		// Token: 0x06001552 RID: 5458 RVA: 0x0003E595 File Offset: 0x0003C795
		[NonVersionable]
		public static UIntPtr Add(UIntPtr pointer, int offset)
		{
			return pointer + offset;
		}

		/// <summary>Adds an offset to the value of an unsigned pointer.</summary>
		/// <param name="pointer">The unsigned pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		/// <returns>A new unsigned pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		// Token: 0x06001553 RID: 5459 RVA: 0x0003E59E File Offset: 0x0003C79E
		[NonVersionable]
		public static UIntPtr operator +(UIntPtr pointer, int offset)
		{
			return new UIntPtr(pointer.ToUInt64() + (ulong)((long)offset));
		}

		/// <summary>Subtracts an offset from the value of an unsigned pointer.</summary>
		/// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		/// <returns>A new unsigned pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		// Token: 0x06001554 RID: 5460 RVA: 0x0003E5AF File Offset: 0x0003C7AF
		[NonVersionable]
		public static UIntPtr Subtract(UIntPtr pointer, int offset)
		{
			return pointer - offset;
		}

		/// <summary>Subtracts an offset from the value of an unsigned pointer.</summary>
		/// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		/// <returns>A new unsigned pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		// Token: 0x06001555 RID: 5461 RVA: 0x0003E5B8 File Offset: 0x0003C7B8
		[NonVersionable]
		public static UIntPtr operator -(UIntPtr pointer, int offset)
		{
			return new UIntPtr(pointer.ToUInt64() - (ulong)((long)offset));
		}

		/// <summary>Gets the size of this instance.</summary>
		/// <returns>The size of a pointer or handle on this platform, measured in bytes. The value of this property is 4 on a 32-bit platform, and 8 on a 64-bit platform.</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0003E5C9 File Offset: 0x0003C7C9
		[__DynamicallyInvokable]
		public static int Size
		{
			[NonVersionable]
			[__DynamicallyInvokable]
			get
			{
				return 8;
			}
		}

		/// <summary>Converts the value of this instance to a pointer to an unspecified type.</summary>
		/// <returns>A pointer to <see cref="T:System.Void" />; that is, a pointer to memory containing data of an unspecified type.</returns>
		// Token: 0x06001557 RID: 5463 RVA: 0x0003E5CC File Offset: 0x0003C7CC
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe void* ToPointer()
		{
			return this.m_value;
		}

		// Token: 0x040006FA RID: 1786
		[SecurityCritical]
		private unsafe void* m_value;

		/// <summary>A read-only field that represents a pointer or handle that has been initialized to zero.</summary>
		// Token: 0x040006FB RID: 1787
		public static readonly UIntPtr Zero;
	}
}
