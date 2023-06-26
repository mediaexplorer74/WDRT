using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x0200053F RID: 1343
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with no parameters.</summary>
		// Token: 0x06003293 RID: 12947 RVA: 0x000E1E5F File Offset: 0x000E005F
		public DescriptionAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with a description.</summary>
		/// <param name="description">The description text.</param>
		// Token: 0x06003294 RID: 12948 RVA: 0x000E1E6C File Offset: 0x000E006C
		public DescriptionAttribute(string description)
		{
			this.description = description;
		}

		/// <summary>Gets the description stored in this attribute.</summary>
		/// <returns>The description stored in this attribute.</returns>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000E1E7B File Offset: 0x000E007B
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the string stored as the description.</summary>
		/// <returns>The string stored as the description. The default value is an empty string ("").</returns>
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000E1E83 File Offset: 0x000E0083
		// (set) Token: 0x06003297 RID: 12951 RVA: 0x000E1E8B File Offset: 0x000E008B
		protected string DescriptionValue
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003298 RID: 12952 RVA: 0x000E1E94 File Offset: 0x000E0094
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003299 RID: 12953 RVA: 0x000E1EC4 File Offset: 0x000E00C4
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance.</summary>
		/// <returns>
		///   <see langword="true" />, if this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600329A RID: 12954 RVA: 0x000E1ED1 File Offset: 0x000E00D1
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DescriptionAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DescriptionAttribute" />, which is an empty string (""). This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400296C RID: 10604
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();

		// Token: 0x0400296D RID: 10605
		private string description;
	}
}
