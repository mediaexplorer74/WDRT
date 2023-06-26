using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property can only be set at design time.</summary>
	// Token: 0x02000544 RID: 1348
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignOnlyAttribute" /> class.</summary>
		/// <param name="isDesignOnly">
		///   <see langword="true" /> if a property can be set only at design time; <see langword="false" /> if the property can be set at design time and at run time.</param>
		// Token: 0x060032B4 RID: 12980 RVA: 0x000E21FB File Offset: 0x000E03FB
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.isDesignOnly = isDesignOnly;
		}

		/// <summary>Gets a value indicating whether a property can be set only at design time.</summary>
		/// <returns>
		///   <see langword="true" /> if a property can be set only at design time; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000E220A File Offset: 0x000E040A
		public bool IsDesignOnly
		{
			get
			{
				return this.isDesignOnly;
			}
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032B6 RID: 12982 RVA: 0x000E2212 File Offset: 0x000E0412
		public override bool IsDefaultAttribute()
		{
			return this.IsDesignOnly == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032B7 RID: 12983 RVA: 0x000E2228 File Offset: 0x000E0428
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			return designOnlyAttribute != null && designOnlyAttribute.isDesignOnly == this.isDesignOnly;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060032B8 RID: 12984 RVA: 0x000E2255 File Offset: 0x000E0455
		public override int GetHashCode()
		{
			return this.isDesignOnly.GetHashCode();
		}

		// Token: 0x04002980 RID: 10624
		private bool isDesignOnly;

		/// <summary>Specifies that a property can be set only at design time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002981 RID: 10625
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		/// <summary>Specifies that a property can be set at design time or at run time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002982 RID: 10626
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />, which is <see cref="F:System.ComponentModel.DesignOnlyAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002983 RID: 10627
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
