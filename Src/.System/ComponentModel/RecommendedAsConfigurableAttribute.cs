using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the property can be used as an application setting.</summary>
	// Token: 0x0200059F RID: 1439
	[AttributeUsage(AttributeTargets.Property)]
	[Obsolete("Use System.ComponentModel.SettingsBindableAttribute instead to work with the new settings model.")]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" /> class.</summary>
		/// <param name="recommendedAsConfigurable">
		///   <see langword="true" /> if the property this attribute is bound to can be used as an application setting; otherwise, <see langword="false" />.</param>
		// Token: 0x06003581 RID: 13697 RVA: 0x000E844B File Offset: 0x000E664B
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.recommendedAsConfigurable = recommendedAsConfigurable;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to can be used as an application setting.</summary>
		/// <returns>
		///   <see langword="true" /> if the property this attribute is bound to can be used as an application setting; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x000E845A File Offset: 0x000E665A
		public bool RecommendedAsConfigurable
		{
			get
			{
				return this.recommendedAsConfigurable;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003583 RID: 13699 RVA: 0x000E8464 File Offset: 0x000E6664
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecommendedAsConfigurableAttribute recommendedAsConfigurableAttribute = obj as RecommendedAsConfigurableAttribute;
			return recommendedAsConfigurableAttribute != null && recommendedAsConfigurableAttribute.RecommendedAsConfigurable == this.recommendedAsConfigurable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />.</returns>
		// Token: 0x06003584 RID: 13700 RVA: 0x000E8491 File Offset: 0x000E6691
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value for the class.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003585 RID: 13701 RVA: 0x000E8499 File Offset: 0x000E6699
		public override bool IsDefaultAttribute()
		{
			return !this.recommendedAsConfigurable;
		}

		// Token: 0x04002A3E RID: 10814
		private bool recommendedAsConfigurable;

		/// <summary>Specifies that a property cannot be used as an application setting. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A3F RID: 10815
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		/// <summary>Specifies that a property can be used as an application setting. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A40 RID: 10816
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />, which is <see cref="F:System.ComponentModel.RecommendedAsConfigurableAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A41 RID: 10817
		public static readonly RecommendedAsConfigurableAttribute Default = RecommendedAsConfigurableAttribute.No;
	}
}
