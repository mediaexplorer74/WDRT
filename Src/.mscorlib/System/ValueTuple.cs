using System;
using System.Collections;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	/// <summary>Provides static methods for creating value tuples.</summary>
	// Token: 0x02000069 RID: 105
	[Serializable]
	public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
	{
		/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple" /> instance is equal to a specified object.</summary>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.ValueTuple" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003DE RID: 990 RVA: 0x0000A515 File Offset: 0x00008715
		public override bool Equals(object obj)
		{
			return obj is ValueTuple;
		}

		/// <summary>Determines whether two <see cref="T:System.ValueTuple" /> instances are equal. This method always returns <see langword="true" />.</summary>
		/// <param name="other">The value tuple to compare with the current instance.</param>
		/// <returns>This method always returns <see langword="true" />.</returns>
		// Token: 0x060003DF RID: 991 RVA: 0x0000A520 File Offset: 0x00008720
		public bool Equals(ValueTuple other)
		{
			return true;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple" /> instance is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003E0 RID: 992 RVA: 0x0000A523 File Offset: 0x00008723
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is ValueTuple;
		}

		/// <summary>Compares this <see cref="T:System.ValueTuple" /> instance with a specified object and returns an indication of their relative values.</summary>
		/// <param name="other">The object to compare with the current instance</param>
		/// <returns>0 if <paramref name="other" /> is a <see cref="T:System.ValueTuple" /> instance; otherwise, 1 if <paramref name="other" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
		// Token: 0x060003E1 RID: 993 RVA: 0x0000A530 File Offset: 0x00008730
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", new object[] { base.GetType().ToString() }), "other");
			}
			return 0;
		}

		/// <summary>Compares the current <see cref="T:System.ValueTuple" /> instance to a specified <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <returns>This method always returns 0.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000A57E File Offset: 0x0000877E
		public int CompareTo(ValueTuple other)
		{
			return 0;
		}

		/// <summary>Compares the current <see cref="T:System.ValueTuple" /> instance to a specified object.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison. This parameter is ignored.</param>
		/// <returns>Returns 0 if <paramref name="other" /> is a <see cref="T:System.ValueTuple" /> instance and 1 if <paramref name="other" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000A584 File Offset: 0x00008784
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", new object[] { base.GetType().ToString() }), "other");
			}
			return 0;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <returns>This method always return 0.</returns>
		// Token: 0x060003E4 RID: 996 RVA: 0x0000A5D2 File Offset: 0x000087D2
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method computes the hash code. This parameter is ignored.</param>
		/// <returns>The hash code for this <see cref="T:System.ValueTuple" /> instance.</returns>
		// Token: 0x060003E5 RID: 997 RVA: 0x0000A5D5 File Offset: 0x000087D5
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000A5D8 File Offset: 0x000087D8
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		/// <summary>Returns the string representation of this <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <returns>This method always returns "()".</returns>
		// Token: 0x060003E7 RID: 999 RVA: 0x0000A5DB File Offset: 0x000087DB
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A5E2 File Offset: 0x000087E2
		string IValueTupleInternal.ToStringEnd()
		{
			return ")";
		}

		/// <summary>Gets the length of this <see langword="ValueTuple" /> instance, which is always 0. There are no elements in a <see langword="ValueTuple" />.</summary>
		/// <returns>0, the number of elements in this <see langword="ValueTuple" /> instance.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000A5E9 File Offset: 0x000087E9
		int ITuple.Length
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Returns an <see cref="T:System.IndexOutOfRangeException" />. There are no elements in a <see langword="ValueTuple" />.</summary>
		/// <param name="index">There is no acceptable value for <paramref name="index" />.</param>
		/// <returns>An <see cref="T:System.IndexOutOfRangeException" />.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no acceptable value for <paramref name="index" />.</exception>
		// Token: 0x17000073 RID: 115
		object ITuple.this[int index]
		{
			get
			{
				throw new IndexOutOfRangeException();
			}
		}

		/// <summary>Creates a new value tuple with zero components.</summary>
		/// <returns>A new value tuple with no components.</returns>
		// Token: 0x060003EB RID: 1003 RVA: 0x0000A5F4 File Offset: 0x000087F4
		public static ValueTuple Create()
		{
			return default(ValueTuple);
		}

		/// <summary>Creates a new value tuple with 1 component (a singleton).</summary>
		/// <param name="item1">The value of the value tuple's only component.</param>
		/// <typeparam name="T1">The type of the value tuple's only component.</typeparam>
		/// <returns>A value tuple with 1 component.</returns>
		// Token: 0x060003EC RID: 1004 RVA: 0x0000A60A File Offset: 0x0000880A
		public static ValueTuple<T1> Create<T1>(T1 item1)
		{
			return new ValueTuple<T1>(item1);
		}

		/// <summary>Creates a new value tuple with 2 components (a pair).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <returns>A value tuple with 2 components.</returns>
		// Token: 0x060003ED RID: 1005 RVA: 0x0000A612 File Offset: 0x00008812
		public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new ValueTuple<T1, T2>(item1, item2);
		}

		/// <summary>Creates a new value tuple with 3 components (a triple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <returns>A value tuple with 3 components.</returns>
		// Token: 0x060003EE RID: 1006 RVA: 0x0000A61B File Offset: 0x0000881B
		public static ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new ValueTuple<T1, T2, T3>(item1, item2, item3);
		}

		/// <summary>Creates a new value tuple with 4 components (a quadruple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <returns>A value tuple with 4 components.</returns>
		// Token: 0x060003EF RID: 1007 RVA: 0x0000A625 File Offset: 0x00008825
		public static ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		/// <summary>Creates a new value tuple with 5 components (a quintuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <returns>A value tuple with 5 components.</returns>
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A630 File Offset: 0x00008830
		public static ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		/// <summary>Creates a new value tuple with 6 components (a sexuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <param name="item6">The value of the value tuple's sixth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
		/// <returns>A value tuple with 6 components.</returns>
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A63D File Offset: 0x0000883D
		public static ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		/// <summary>Creates a new value tuple with 7 components (a septuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <param name="item6">The value of the value tuple's sixth component.</param>
		/// <param name="item7">The value of the value tuple's seventh component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
		/// <typeparam name="T7">The type of the value tuple's seventh component.</typeparam>
		/// <returns>A value tuple with 7 components.</returns>
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000A64C File Offset: 0x0000884C
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		/// <summary>Creates a new value tuple with 8 components (an octuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <param name="item6">The value of the value tuple's sixth component.</param>
		/// <param name="item7">The value of the value tuple's seventh component.</param>
		/// <param name="item8">The value of the value tuple's eighth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
		/// <typeparam name="T7">The type of the value tuple's seventh component.</typeparam>
		/// <typeparam name="T8">The type of the value tuple's eighth component.</typeparam>
		/// <returns>A value tuple with 8 components.</returns>
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A65D File Offset: 0x0000885D
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, ValueTuple.Create<T8>(item8));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A675 File Offset: 0x00008875
		internal static int CombineHashCodes(int h1, int h2)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(h1, h2);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A67E File Offset: 0x0000887E
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A68D File Offset: 0x0000888D
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A69D File Offset: 0x0000889D
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000A6AF File Offset: 0x000088AF
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A6C3 File Offset: 0x000088C3
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000A6D9 File Offset: 0x000088D9
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
