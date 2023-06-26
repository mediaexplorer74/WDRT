using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a member is typically used for binding. This class cannot be inherited.</summary>
	// Token: 0x02000519 RID: 1305
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with a Boolean value.</summary>
		/// <param name="bindable">
		///   <see langword="true" /> to use property for binding; otherwise, <see langword="false" />.</param>
		// Token: 0x0600316B RID: 12651 RVA: 0x000DF280 File Offset: 0x000DD480
		public BindableAttribute(bool bindable)
			: this(bindable, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="bindable">
		///   <see langword="true" /> to use property for binding; otherwise, <see langword="false" />.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x0600316C RID: 12652 RVA: 0x000DF28A File Offset: 0x000DD48A
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.bindable = bindable;
			this.direction = direction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with one of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</param>
		// Token: 0x0600316D RID: 12653 RVA: 0x000DF2A0 File Offset: 0x000DD4A0
		public BindableAttribute(BindableSupport flags)
			: this(flags, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x0600316E RID: 12654 RVA: 0x000DF2AA File Offset: 0x000DD4AA
		public BindableAttribute(BindableSupport flags, BindingDirection direction)
		{
			this.bindable = flags > BindableSupport.No;
			this.isDefault = flags == BindableSupport.Default;
			this.direction = direction;
		}

		/// <summary>Gets a value indicating that a property is typically used for binding.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is typically used for binding; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000DF2CD File Offset: 0x000DD4CD
		public bool Bindable
		{
			get
			{
				return this.bindable;
			}
		}

		/// <summary>Gets a value indicating the direction or directions of this property's data binding.</summary>
		/// <returns>The direction of this property's data binding.</returns>
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000DF2D5 File Offset: 0x000DD4D5
		public BindingDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.BindableAttribute" /> objects are equal.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.BindableAttribute" /> is equal to the current <see cref="T:System.ComponentModel.BindableAttribute" />; <see langword="false" /> if it is not equal.</returns>
		// Token: 0x06003171 RID: 12657 RVA: 0x000DF2DD File Offset: 0x000DD4DD
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is BindableAttribute && ((BindableAttribute)obj).Bindable == this.bindable);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.BindableAttribute" />.</returns>
		// Token: 0x06003172 RID: 12658 RVA: 0x000DF305 File Offset: 0x000DD505
		public override int GetHashCode()
		{
			return this.bindable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003173 RID: 12659 RVA: 0x000DF312 File Offset: 0x000DD512
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BindableAttribute.Default) || this.isDefault;
		}

		/// <summary>Specifies that a property is typically used for binding. This field is read-only.</summary>
		// Token: 0x0400290C RID: 10508
		public static readonly BindableAttribute Yes = new BindableAttribute(true);

		/// <summary>Specifies that a property is not typically used for binding. This field is read-only.</summary>
		// Token: 0x0400290D RID: 10509
		public static readonly BindableAttribute No = new BindableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BindableAttribute" />, which is <see cref="F:System.ComponentModel.BindableAttribute.No" />. This field is read-only.</summary>
		// Token: 0x0400290E RID: 10510
		public static readonly BindableAttribute Default = BindableAttribute.No;

		// Token: 0x0400290F RID: 10511
		private bool bindable;

		// Token: 0x04002910 RID: 10512
		private bool isDefault;

		// Token: 0x04002911 RID: 10513
		private BindingDirection direction;
	}
}
