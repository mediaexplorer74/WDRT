using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a string comparison operation that uses specific case and culture-based or ordinal comparison rules.</summary>
	// Token: 0x02000075 RID: 117
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000138B7 File Offset: 0x00011AB7
		public static StringComparer InvariantCulture
		{
			get
			{
				return StringComparer._invariantCulture;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000138BE File Offset: 0x00011ABE
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return StringComparer._invariantCultureIgnoreCase;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000138C5 File Offset: 0x00011AC5
		[__DynamicallyInvokable]
		public static StringComparer CurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, false);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs case-insensitive string comparisons using the word comparison rules of the current culture.</summary>
		/// <returns>A new object for string comparison.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000138D2 File Offset: 0x00011AD2
		[__DynamicallyInvokable]
		public static StringComparer CurrentCultureIgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, true);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000138DF File Offset: 0x00011ADF
		[__DynamicallyInvokable]
		public static StringComparer Ordinal
		{
			[__DynamicallyInvokable]
			get
			{
				return StringComparer._ordinal;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000138E6 File Offset: 0x00011AE6
		[__DynamicallyInvokable]
		public static StringComparer OrdinalIgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return StringComparer._ordinalIgnoreCase;
			}
		}

		/// <summary>Creates a <see cref="T:System.StringComparer" /> object that compares strings according to the rules of a specified culture.</summary>
		/// <param name="culture">A culture whose linguistic rules are used to perform a string comparison.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that comparison operations be case-insensitive; <see langword="false" /> to specify that comparison operations be case-sensitive.</param>
		/// <returns>A new <see cref="T:System.StringComparer" /> object that performs string comparisons according to the comparison rules used by the <paramref name="culture" /> parameter and the case rule specified by the <paramref name="ignoreCase" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x0600057B RID: 1403 RVA: 0x000138ED File Offset: 0x00011AED
		[__DynamicallyInvokable]
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase);
		}

		/// <summary>When overridden in a derived class, compares two objects and returns an indication of their relative sort order.</summary>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="x" /> precedes  <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> is <see langword="null" /> and <paramref name="y" /> is not <see langword="null" />.  
		///
		///   Zero  
		///
		///  <paramref name="x" /> is equal to <paramref name="y" />.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> and <paramref name="y" /> are both <see langword="null" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="x" /> follows <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="y" /> is <see langword="null" /> and <paramref name="x" /> is not <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="x" /> nor <paramref name="y" /> is a <see cref="T:System.String" /> object, and neither <paramref name="x" /> nor <paramref name="y" /> implements the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x0600057C RID: 1404 RVA: 0x00013904 File Offset: 0x00011B04
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
		}

		/// <summary>When overridden in a derived class, indicates whether two objects are equal.</summary>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are both the same type of object and those objects are equal, or both <paramref name="x" /> and <paramref name="y" /> are <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600057D RID: 1405 RVA: 0x00013960 File Offset: 0x00011B60
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		/// <summary>When overridden in a derived class, gets the hash code for the specified object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x0600057E RID: 1406 RVA: 0x000139A0 File Offset: 0x00011BA0
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text != null)
			{
				return this.GetHashCode(text);
			}
			return obj.GetHashCode();
		}

		/// <summary>When overridden in a derived class, compares two strings and returns an indication of their relative sort order.</summary>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="x" /> precedes <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> is <see langword="null" /> and <paramref name="y" /> is not <see langword="null" />.  
		///
		///   Zero  
		///
		///  <paramref name="x" /> is equal to <paramref name="y" />.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> and <paramref name="y" /> are both <see langword="null" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="x" /> follows <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="y" /> is <see langword="null" /> and <paramref name="x" /> is not <see langword="null" />.</returns>
		// Token: 0x0600057F RID: 1407
		[__DynamicallyInvokable]
		public abstract int Compare(string x, string y);

		/// <summary>When overridden in a derived class, indicates whether two strings are equal.</summary>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are equal, or <paramref name="x" /> and <paramref name="y" /> are <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000580 RID: 1408
		[__DynamicallyInvokable]
		public abstract bool Equals(string x, string y);

		/// <summary>When overridden in a derived class, gets the hash code for the specified string.</summary>
		/// <param name="obj">A string.</param>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06000581 RID: 1409
		[__DynamicallyInvokable]
		public abstract int GetHashCode(string obj);

		/// <summary>Initializes a new instance of the <see cref="T:System.StringComparer" /> class.</summary>
		// Token: 0x06000582 RID: 1410 RVA: 0x000139D3 File Offset: 0x00011BD3
		[__DynamicallyInvokable]
		protected StringComparer()
		{
		}

		// Token: 0x0400028E RID: 654
		private static readonly StringComparer _invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, false);

		// Token: 0x0400028F RID: 655
		private static readonly StringComparer _invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, true);

		// Token: 0x04000290 RID: 656
		private static readonly StringComparer _ordinal = new OrdinalComparer(false);

		// Token: 0x04000291 RID: 657
		private static readonly StringComparer _ordinalIgnoreCase = new OrdinalComparer(true);
	}
}
