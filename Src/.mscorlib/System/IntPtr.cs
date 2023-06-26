using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>A platform-specific type that is used to represent a pointer or a handle.</summary>
	// Token: 0x020000FD RID: 253
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct IntPtr : ISerializable
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x0002F8AE File Offset: 0x0002DAAE
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsNull()
		{
			return this.m_value == null;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified 32-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 32-bit signed integer.</param>
		// Token: 0x06000F89 RID: 3977 RVA: 0x0002F8BA File Offset: 0x0002DABA
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public IntPtr(int value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified 64-bit pointer.</summary>
		/// <param name="value">A pointer or handle contained in a 64-bit signed integer.</param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large or too small to represent as an <see cref="T:System.IntPtr" />.</exception>
		// Token: 0x06000F8A RID: 3978 RVA: 0x0002F8C5 File Offset: 0x0002DAC5
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public IntPtr(long value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified pointer to an unspecified type.</summary>
		/// <param name="value">A pointer to an unspecified type.</param>
		// Token: 0x06000F8B RID: 3979 RVA: 0x0002F8CF File Offset: 0x0002DACF
		[SecurityCritical]
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public unsafe IntPtr(void* value)
		{
			this.m_value = value;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0002F8D8 File Offset: 0x0002DAD8
		[SecurityCritical]
		private IntPtr(SerializationInfo info, StreamingContext context)
		{
			long @int = info.GetInt64("value");
			if (IntPtr.Size == 4 && (@int > 2147483647L || @int < -2147483648L))
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_InvalidPtrValue"));
			}
			this.m_value = @int;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.IntPtr" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06000F8D RID: 3981 RVA: 0x0002F923 File Offset: 0x0002DB23
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
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.IntPtr" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F8E RID: 3982 RVA: 0x0002F945 File Offset: 0x0002DB45
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is IntPtr && this.m_value == ((IntPtr)obj).m_value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F8F RID: 3983 RVA: 0x0002F964 File Offset: 0x0002DB64
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer equal to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of this instance is too large or too small to represent as a 32-bit signed integer.</exception>
		// Token: 0x06000F90 RID: 3984 RVA: 0x0002F970 File Offset: 0x0002DB70
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public int ToInt32()
		{
			long num = this.m_value;
			return checked((int)num);
		}

		/// <summary>Converts the value of this instance to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer equal to the value of this instance.</returns>
		// Token: 0x06000F91 RID: 3985 RVA: 0x0002F987 File Offset: 0x0002DB87
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public long ToInt64()
		{
			return this.m_value;
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.IntPtr" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06000F92 RID: 3986 RVA: 0x0002F990 File Offset: 0x0002DB90
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.m_value.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.IntPtr" /> object to its equivalent string representation.</summary>
		/// <param name="format">A format specification that governs how the current <see cref="T:System.IntPtr" /> object is converted.</param>
		/// <returns>The string representation of the value of the current <see cref="T:System.IntPtr" /> object.</returns>
		// Token: 0x06000F93 RID: 3987 RVA: 0x0002F9B4 File Offset: 0x0002DBB4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return this.m_value.ToString(format, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the value of a 32-bit signed integer to an <see cref="T:System.IntPtr" />.</summary>
		/// <param name="value">A 32-bit signed integer.</param>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		// Token: 0x06000F94 RID: 3988 RVA: 0x0002F9D6 File Offset: 0x0002DBD6
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the value of a 64-bit signed integer to an <see cref="T:System.IntPtr" />.</summary>
		/// <param name="value">A 64-bit signed integer.</param>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.IntPtr" />.</exception>
		// Token: 0x06000F95 RID: 3989 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static explicit operator IntPtr(long value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the specified pointer to an unspecified type to an <see cref="T:System.IntPtr" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">A pointer to an unspecified type.</param>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		// Token: 0x06000F96 RID: 3990 RVA: 0x0002F9E6 File Offset: 0x0002DBE6
		[SecurityCritical]
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public unsafe static explicit operator IntPtr(void* value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a pointer to an unspecified type.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <returns>The contents of <paramref name="value" />.</returns>
		// Token: 0x06000F97 RID: 3991 RVA: 0x0002F9EE File Offset: 0x0002DBEE
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe static explicit operator void*(IntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a 32-bit signed integer.</summary>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of <paramref name="value" /> is too large to represent as a 32-bit signed integer.</exception>
		// Token: 0x06000F98 RID: 3992 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		[SecuritySafeCritical]
		[NonVersionable]
		public static explicit operator int(IntPtr value)
		{
			long num = value.m_value;
			return checked((int)num);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a 64-bit signed integer.</summary>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <returns>The contents of <paramref name="value" />.</returns>
		// Token: 0x06000F99 RID: 3993 RVA: 0x0002FA10 File Offset: 0x0002DC10
		[SecuritySafeCritical]
		[NonVersionable]
		public static explicit operator long(IntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.IntPtr" /> are equal.</summary>
		/// <param name="value1">The first pointer or handle to compare.</param>
		/// <param name="value2">The second pointer or handle to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> equals <paramref name="value2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F9A RID: 3994 RVA: 0x0002FA1A File Offset: 0x0002DC1A
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(IntPtr value1, IntPtr value2)
		{
			return value1.m_value == value2.m_value;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.IntPtr" /> are not equal.</summary>
		/// <param name="value1">The first pointer or handle to compare.</param>
		/// <param name="value2">The second pointer or handle to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> does not equal <paramref name="value2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F9B RID: 3995 RVA: 0x0002FA2C File Offset: 0x0002DC2C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(IntPtr value1, IntPtr value2)
		{
			return value1.m_value != value2.m_value;
		}

		/// <summary>Adds an offset to the value of a pointer.</summary>
		/// <param name="pointer">The pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		/// <returns>A new pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		// Token: 0x06000F9C RID: 3996 RVA: 0x0002FA41 File Offset: 0x0002DC41
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr Add(IntPtr pointer, int offset)
		{
			return pointer + offset;
		}

		/// <summary>Adds an offset to the value of a pointer.</summary>
		/// <param name="pointer">The pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		/// <returns>A new pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		// Token: 0x06000F9D RID: 3997 RVA: 0x0002FA4A File Offset: 0x0002DC4A
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr operator +(IntPtr pointer, int offset)
		{
			return new IntPtr(pointer.ToInt64() + (long)offset);
		}

		/// <summary>Subtracts an offset from the value of a pointer.</summary>
		/// <param name="pointer">The pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		/// <returns>A new pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		// Token: 0x06000F9E RID: 3998 RVA: 0x0002FA5B File Offset: 0x0002DC5B
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr Subtract(IntPtr pointer, int offset)
		{
			return pointer - offset;
		}

		/// <summary>Subtracts an offset from the value of a pointer.</summary>
		/// <param name="pointer">The pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		/// <returns>A new pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		// Token: 0x06000F9F RID: 3999 RVA: 0x0002FA64 File Offset: 0x0002DC64
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr operator -(IntPtr pointer, int offset)
		{
			return new IntPtr(pointer.ToInt64() - (long)offset);
		}

		/// <summary>Gets the size of this instance.</summary>
		/// <returns>The size of a pointer or handle in this process, measured in bytes. The value of this property is 4 in a 32-bit process, and 8 in a 64-bit process. You can define the process type by setting the <see langword="/platform" /> switch when you compile your code with the C# and Visual Basic compilers.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0002FA75 File Offset: 0x0002DC75
		[__DynamicallyInvokable]
		public static int Size
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[NonVersionable]
			[__DynamicallyInvokable]
			get
			{
				return 8;
			}
		}

		/// <summary>Converts the value of this instance to a pointer to an unspecified type.</summary>
		/// <returns>A pointer to <see cref="T:System.Void" />; that is, a pointer to memory containing data of an unspecified type.</returns>
		// Token: 0x06000FA1 RID: 4001 RVA: 0x0002FA78 File Offset: 0x0002DC78
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		public unsafe void* ToPointer()
		{
			return this.m_value;
		}

		// Token: 0x040005A8 RID: 1448
		[SecurityCritical]
		private unsafe void* m_value;

		/// <summary>A read-only field that represents a pointer or handle that has been initialized to zero.</summary>
		// Token: 0x040005A9 RID: 1449
		public static readonly IntPtr Zero;
	}
}
