using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property should be localized. This class cannot be inherited.</summary>
	// Token: 0x0200058A RID: 1418
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LocalizableAttribute" /> class.</summary>
		/// <param name="isLocalizable">
		///   <see langword="true" /> if a property should be localized; otherwise, <see langword="false" />.</param>
		// Token: 0x06003442 RID: 13378 RVA: 0x000E4596 File Offset: 0x000E2796
		public LocalizableAttribute(bool isLocalizable)
		{
			this.isLocalizable = isLocalizable;
		}

		/// <summary>Gets a value indicating whether a property should be localized.</summary>
		/// <returns>
		///   <see langword="true" /> if a property should be localized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000E45A5 File Offset: 0x000E27A5
		public bool IsLocalizable
		{
			get
			{
				return this.isLocalizable;
			}
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003444 RID: 13380 RVA: 0x000E45AD File Offset: 0x000E27AD
		public override bool IsDefaultAttribute()
		{
			return this.IsLocalizable == LocalizableAttribute.Default.IsLocalizable;
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003445 RID: 13381 RVA: 0x000E45C4 File Offset: 0x000E27C4
		public override bool Equals(object obj)
		{
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			return localizableAttribute != null && localizableAttribute.IsLocalizable == this.isLocalizable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</returns>
		// Token: 0x06003446 RID: 13382 RVA: 0x000E45EB File Offset: 0x000E27EB
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029D4 RID: 10708
		private bool isLocalizable;

		/// <summary>Specifies that a property should be localized. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029D5 RID: 10709
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		/// <summary>Specifies that a property should not be localized. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029D6 RID: 10710
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.LocalizableAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029D7 RID: 10711
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
