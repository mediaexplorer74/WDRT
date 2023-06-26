using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the designer for a class belongs to a certain category.</summary>
	// Token: 0x02000541 RID: 1345
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with an empty string ("").</summary>
		// Token: 0x060032A6 RID: 12966 RVA: 0x000E2090 File Offset: 0x000E0290
		public DesignerCategoryAttribute()
		{
			this.category = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with the given category name.</summary>
		/// <param name="category">The name of the category.</param>
		// Token: 0x060032A7 RID: 12967 RVA: 0x000E20A3 File Offset: 0x000E02A3
		public DesignerCategoryAttribute(string category)
		{
			this.category = category;
		}

		/// <summary>Gets the name of the category.</summary>
		/// <returns>The name of the category.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000E20B2 File Offset: 0x000E02B2
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000E20BA File Offset: 0x000E02BA
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					this.typeId = base.GetType().FullName + this.Category;
				}
				return this.typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032AA RID: 12970 RVA: 0x000E20E8 File Offset: 0x000E02E8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerCategoryAttribute designerCategoryAttribute = obj as DesignerCategoryAttribute;
			return designerCategoryAttribute != null && designerCategoryAttribute.category == this.category;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060032AB RID: 12971 RVA: 0x000E2118 File Offset: 0x000E0318
		public override int GetHashCode()
		{
			return this.category.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032AC RID: 12972 RVA: 0x000E2125 File Offset: 0x000E0325
		public override bool IsDefaultAttribute()
		{
			return this.category.Equals(DesignerCategoryAttribute.Default.Category);
		}

		// Token: 0x04002971 RID: 10609
		private string category;

		// Token: 0x04002972 RID: 10610
		private string typeId;

		/// <summary>Specifies that a component marked with this category use a component designer. This field is read-only.</summary>
		// Token: 0x04002973 RID: 10611
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		/// <summary>Specifies that a component marked with this category cannot use a visual designer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002974 RID: 10612
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute();

		/// <summary>Specifies that a component marked with this category use a form designer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002975 RID: 10613
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		/// <summary>Specifies that a component marked with this category use a generic designer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002976 RID: 10614
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");
	}
}
