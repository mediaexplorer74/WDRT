using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 3-tuple, or triple.</summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	// Token: 0x02000062 RID: 98
	[__DynamicallyInvokable]
	[Serializable]
	public class Tuple<T1, T2, T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's first component.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00008A4A File Offset: 0x00006C4A
		[__DynamicallyInvokable]
		public T1 Item1
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's second component.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00008A52 File Offset: 0x00006C52
		[__DynamicallyInvokable]
		public T2 Item2
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's third component.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00008A5A File Offset: 0x00006C5A
		[__DynamicallyInvokable]
		public T3 Item3
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`3" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		// Token: 0x06000376 RID: 886 RVA: 0x00008A62 File Offset: 0x00006C62
		[__DynamicallyInvokable]
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`3" /> object is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000377 RID: 887 RVA: 0x00008A7F File Offset: 0x00006C7F
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`3" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000378 RID: 888 RVA: 0x00008A90 File Offset: 0x00006C90
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2)) && comparer.Equals(this.m_Item3, tuple.m_Item3);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`3" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance precedes <paramref name="obj" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="obj" /> have the same position in the sort order.  
		///
		///   A positive integer  
		///
		///   This instance follows <paramref name="obj" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`3" /> object.</exception>
		// Token: 0x06000379 RID: 889 RVA: 0x00008B08 File Offset: 0x00006D08
		[__DynamicallyInvokable]
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`3" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance precedes <paramref name="other" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="other" /> have the same position in the sort order.  
		///
		///   A positive integer  
		///
		///   This instance follows <paramref name="other" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`3" /> object.</exception>
		// Token: 0x0600037A RID: 890 RVA: 0x00008B18 File Offset: 0x00006D18
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			if (tuple == null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleIncorrectType", new object[] { base.GetType().ToString() }), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item3, tuple.m_Item3);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`3" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600037B RID: 891 RVA: 0x00008BBF File Offset: 0x00006DBF
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`3" /> object by using a specified computation method.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.Tuple`3" /> object.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600037C RID: 892 RVA: 0x00008BCC File Offset: 0x00006DCC
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00008C06 File Offset: 0x00006E06
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`3" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`3" /> object.</returns>
		// Token: 0x0600037E RID: 894 RVA: 0x00008C10 File Offset: 0x00006E10
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00008C38 File Offset: 0x00006E38
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(")");
			return sb.ToString();
		}

		/// <summary>Gets the number of elements in the <see langword="Tuple" />.</summary>
		/// <returns>3, the number of elements in a <see cref="T:System.Tuple`3" /> object.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00008CA5 File Offset: 0x00006EA5
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets the value of the specified <see langword="Tuple" /> element.</summary>
		/// <param name="index">The index of the specified <see langword="Tuple" /> element. <paramref name="index" /> can range from 0 to 2.</param>
		/// <returns>The value of the <see langword="Tuple" /> element at the specified position.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than 2.</exception>
		// Token: 0x17000049 RID: 73
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Item1;
				case 1:
					return this.Item2;
				case 2:
					return this.Item3;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x0400023E RID: 574
		private readonly T1 m_Item1;

		// Token: 0x0400023F RID: 575
		private readonly T2 m_Item2;

		// Token: 0x04000240 RID: 576
		private readonly T3 m_Item3;
	}
}
