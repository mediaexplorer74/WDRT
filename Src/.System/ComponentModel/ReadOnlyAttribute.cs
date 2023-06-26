using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the property this attribute is bound to is read-only or read/write. This class cannot be inherited</summary>
	// Token: 0x0200059E RID: 1438
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ReadOnlyAttribute" /> class.</summary>
		/// <param name="isReadOnly">
		///   <see langword="true" /> to show that the property this attribute is bound to is read-only; <see langword="false" /> to show that the property is read/write.</param>
		// Token: 0x0600357B RID: 13691 RVA: 0x000E83C8 File Offset: 0x000E65C8
		public ReadOnlyAttribute(bool isReadOnly)
		{
			this.isReadOnly = isReadOnly;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the property this attribute is bound to is read-only; <see langword="false" /> if the property is read/write.</returns>
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000E83D7 File Offset: 0x000E65D7
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="value">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600357D RID: 13693 RVA: 0x000E83E0 File Offset: 0x000E65E0
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			ReadOnlyAttribute readOnlyAttribute = value as ReadOnlyAttribute;
			return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly == this.IsReadOnly;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ReadOnlyAttribute" />.</returns>
		// Token: 0x0600357E RID: 13694 RVA: 0x000E840D File Offset: 0x000E660D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600357F RID: 13695 RVA: 0x000E8415 File Offset: 0x000E6615
		public override bool IsDefaultAttribute()
		{
			return this.IsReadOnly == ReadOnlyAttribute.Default.IsReadOnly;
		}

		// Token: 0x04002A3A RID: 10810
		private bool isReadOnly;

		/// <summary>Specifies that the property this attribute is bound to is read-only and cannot be modified in the server explorer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A3B RID: 10811
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		/// <summary>Specifies that the property this attribute is bound to is read/write and can be modified. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A3C RID: 10812
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.ReadOnlyAttribute" />, which is <see cref="F:System.ComponentModel.ReadOnlyAttribute.No" /> (that is, the property this attribute is bound to is read/write). This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A3D RID: 10813
		public static readonly ReadOnlyAttribute Default = ReadOnlyAttribute.No;
	}
}
