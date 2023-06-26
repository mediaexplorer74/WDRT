using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Encapsulates an array and an offset within the specified array.</summary>
	// Token: 0x0200090C RID: 2316
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct ArrayWithOffset
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> structure.</summary>
		/// <param name="array">A managed array.</param>
		/// <param name="offset">The offset in bytes, of the element to be passed through platform invoke.</param>
		/// <exception cref="T:System.ArgumentException">The array is larger than 2 gigabytes (GB).</exception>
		// Token: 0x06005FFC RID: 24572 RVA: 0x0014CCC8 File Offset: 0x0014AEC8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public ArrayWithOffset(object array, int offset)
		{
			this.m_array = array;
			this.m_offset = offset;
			this.m_count = 0;
			this.m_count = this.CalculateCount();
		}

		/// <summary>Returns the managed array referenced by this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.</summary>
		/// <returns>The managed array this instance references.</returns>
		// Token: 0x06005FFD RID: 24573 RVA: 0x0014CCEB File Offset: 0x0014AEEB
		[__DynamicallyInvokable]
		public object GetArray()
		{
			return this.m_array;
		}

		/// <summary>Returns the offset provided when this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> was constructed.</summary>
		/// <returns>The offset for this instance.</returns>
		// Token: 0x06005FFE RID: 24574 RVA: 0x0014CCF3 File Offset: 0x0014AEF3
		[__DynamicallyInvokable]
		public int GetOffset()
		{
			return this.m_offset;
		}

		/// <summary>Returns a hash code for this value type.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06005FFF RID: 24575 RVA: 0x0014CCFB File Offset: 0x0014AEFB
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_count + this.m_offset;
		}

		/// <summary>Indicates whether the specified object matches the current <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object.</summary>
		/// <param name="obj">Object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the object matches this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006000 RID: 24576 RVA: 0x0014CD0A File Offset: 0x0014AF0A
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ArrayWithOffset && this.Equals((ArrayWithOffset)obj);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance.</summary>
		/// <param name="obj">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006001 RID: 24577 RVA: 0x0014CD22 File Offset: 0x0014AF22
		[__DynamicallyInvokable]
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.m_array == this.m_array && obj.m_offset == this.m_offset && obj.m_count == this.m_count;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects have the same value.</summary>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter.</param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006002 RID: 24578 RVA: 0x0014CD50 File Offset: 0x0014AF50
		[__DynamicallyInvokable]
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects no not have the same value.</summary>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter.</param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="a" /> is not the same as the value of <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006003 RID: 24579 RVA: 0x0014CD5A File Offset: 0x0014AF5A
		[__DynamicallyInvokable]
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !(a == b);
		}

		// Token: 0x06006004 RID: 24580
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CalculateCount();

		// Token: 0x04002A62 RID: 10850
		private object m_array;

		// Token: 0x04002A63 RID: 10851
		private int m_offset;

		// Token: 0x04002A64 RID: 10852
		private int m_count;
	}
}
