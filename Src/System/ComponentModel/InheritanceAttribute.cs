using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the component associated with this attribute has been inherited from a base class. This class cannot be inherited.</summary>
	// Token: 0x020005BC RID: 1468
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class InheritanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class.</summary>
		// Token: 0x06003703 RID: 14083 RVA: 0x000EF210 File Offset: 0x000ED410
		public InheritanceAttribute()
		{
			this.inheritanceLevel = InheritanceAttribute.Default.inheritanceLevel;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class with the specified inheritance level.</summary>
		/// <param name="inheritanceLevel">An <see cref="T:System.ComponentModel.InheritanceLevel" /> that indicates the level of inheritance to set this attribute to.</param>
		// Token: 0x06003704 RID: 14084 RVA: 0x000EF228 File Offset: 0x000ED428
		public InheritanceAttribute(InheritanceLevel inheritanceLevel)
		{
			this.inheritanceLevel = inheritanceLevel;
		}

		/// <summary>Gets or sets the current inheritance level stored in this attribute.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceLevel" /> stored in this attribute.</returns>
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000EF237 File Offset: 0x000ED437
		public InheritanceLevel InheritanceLevel
		{
			get
			{
				return this.inheritanceLevel;
			}
		}

		/// <summary>Override to test for equality.</summary>
		/// <param name="value">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the object is the same; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003706 RID: 14086 RVA: 0x000EF240 File Offset: 0x000ED440
		public override bool Equals(object value)
		{
			if (value == this)
			{
				return true;
			}
			if (!(value is InheritanceAttribute))
			{
				return false;
			}
			InheritanceLevel inheritanceLevel = ((InheritanceAttribute)value).InheritanceLevel;
			return inheritanceLevel == this.inheritanceLevel;
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06003707 RID: 14087 RVA: 0x000EF272 File Offset: 0x000ED472
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003708 RID: 14088 RVA: 0x000EF27A File Offset: 0x000ED47A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(InheritanceAttribute.Default);
		}

		/// <summary>Converts this attribute to a string.</summary>
		/// <returns>A string that represents this <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06003709 RID: 14089 RVA: 0x000EF287 File Offset: 0x000ED487
		public override string ToString()
		{
			return TypeDescriptor.GetConverter(typeof(InheritanceLevel)).ConvertToString(this.InheritanceLevel);
		}

		// Token: 0x04002AA9 RID: 10921
		private readonly InheritanceLevel inheritanceLevel;

		/// <summary>Specifies that the component is inherited. This field is read-only.</summary>
		// Token: 0x04002AAA RID: 10922
		public static readonly InheritanceAttribute Inherited = new InheritanceAttribute(InheritanceLevel.Inherited);

		/// <summary>Specifies that the component is inherited and is read-only. This field is read-only.</summary>
		// Token: 0x04002AAB RID: 10923
		public static readonly InheritanceAttribute InheritedReadOnly = new InheritanceAttribute(InheritanceLevel.InheritedReadOnly);

		/// <summary>Specifies that the component is not inherited. This field is read-only.</summary>
		// Token: 0x04002AAC RID: 10924
		public static readonly InheritanceAttribute NotInherited = new InheritanceAttribute(InheritanceLevel.NotInherited);

		/// <summary>Specifies that the default value for <see cref="T:System.ComponentModel.InheritanceAttribute" /> is <see cref="F:System.ComponentModel.InheritanceAttribute.NotInherited" />. This field is read-only.</summary>
		// Token: 0x04002AAD RID: 10925
		public static readonly InheritanceAttribute Default = InheritanceAttribute.NotInherited;
	}
}
