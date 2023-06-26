using System;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a value type that can be assigned <see langword="null" />.</summary>
	/// <typeparam name="T">The underlying value type of the <see cref="T:System.Nullable`1" /> generic type.</typeparam>
	// Token: 0x02000162 RID: 354
	[NonVersionable]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Nullable`1" /> structure to the specified value.</summary>
		/// <param name="value">A value type.</param>
		// Token: 0x060015CE RID: 5582 RVA: 0x000403DC File Offset: 0x0003E5DC
		[NonVersionable]
		[__DynamicallyInvokable]
		public Nullable(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Nullable`1" /> object has a valid value of its underlying type.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Nullable`1" /> object has a value; <see langword="false" /> if the current <see cref="T:System.Nullable`1" /> object has no value.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x000403EC File Offset: 0x0003E5EC
		[__DynamicallyInvokable]
		public bool HasValue
		{
			[NonVersionable]
			[__DynamicallyInvokable]
			get
			{
				return this.hasValue;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Nullable`1" /> object if it has been assigned a valid underlying value.</summary>
		/// <returns>The value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />. An exception is thrown if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</exception>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x000403F4 File Offset: 0x0003E5F4
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.hasValue)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_NoValue);
				}
				return this.value;
			}
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the default value of the underlying type.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the  <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />; otherwise, the default value of the underlying type.</returns>
		// Token: 0x060015D1 RID: 5585 RVA: 0x0004040B File Offset: 0x0003E60B
		[NonVersionable]
		[__DynamicallyInvokable]
		public T GetValueOrDefault()
		{
			return this.value;
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the specified default value.</summary>
		/// <param name="defaultValue">A value to return if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</param>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />; otherwise, the <paramref name="defaultValue" /> parameter.</returns>
		// Token: 0x060015D2 RID: 5586 RVA: 0x00040413 File Offset: 0x0003E613
		[NonVersionable]
		[__DynamicallyInvokable]
		public T GetValueOrDefault(T defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		/// <summary>Indicates whether the current <see cref="T:System.Nullable`1" /> object is equal to a specified object.</summary>
		/// <param name="other">An object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="other" /> parameter is equal to the current <see cref="T:System.Nullable`1" /> object; otherwise, <see langword="false" />.  
		/// This table describes how equality is defined for the compared values:  
		///  Return Value  
		///
		///  Description  
		///
		/// <see langword="true" /> The <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />, and the <paramref name="other" /> parameter is <see langword="null" />. That is, two null values are equal by definition.  
		///
		/// -or-  
		///
		/// The <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is equal to the <paramref name="other" /> parameter.  
		///
		/// <see langword="false" /> The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is <see langword="true" />, and the <paramref name="other" /> parameter is <see langword="null" />.  
		///
		/// -or-  
		///
		/// The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is <see langword="false" />, and the <paramref name="other" /> parameter is not <see langword="null" />.  
		///
		/// -or-  
		///
		/// The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is <see langword="true" />, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is not equal to the <paramref name="other" /> parameter.</returns>
		// Token: 0x060015D3 RID: 5587 RVA: 0x00040425 File Offset: 0x0003E625
		[__DynamicallyInvokable]
		public override bool Equals(object other)
		{
			if (!this.hasValue)
			{
				return other == null;
			}
			return other != null && this.value.Equals(other);
		}

		/// <summary>Retrieves the hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property.</summary>
		/// <returns>The hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />, or zero if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</returns>
		// Token: 0x060015D4 RID: 5588 RVA: 0x0004044B File Offset: 0x0003E64B
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		/// <summary>Returns the text representation of the value of the current <see cref="T:System.Nullable`1" /> object.</summary>
		/// <returns>The text representation of the value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />, or an empty string ("") if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</returns>
		// Token: 0x060015D5 RID: 5589 RVA: 0x00040468 File Offset: 0x0003E668
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		/// <summary>Creates a new <see cref="T:System.Nullable`1" /> object initialized to a specified value.</summary>
		/// <param name="value">A value type.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> object whose <see cref="P:System.Nullable`1.Value" /> property is initialized with the <paramref name="value" /> parameter.</returns>
		// Token: 0x060015D6 RID: 5590 RVA: 0x00040489 File Offset: 0x0003E689
		[NonVersionable]
		[__DynamicallyInvokable]
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Nullable`1" /> instance to its underlying value.</summary>
		/// <param name="value">A nullable value.</param>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property for the <paramref name="value" /> parameter.</returns>
		// Token: 0x060015D7 RID: 5591 RVA: 0x00040491 File Offset: 0x0003E691
		[NonVersionable]
		[__DynamicallyInvokable]
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x0400074A RID: 1866
		private bool hasValue;

		// Token: 0x0400074B RID: 1867
		internal T value;
	}
}
