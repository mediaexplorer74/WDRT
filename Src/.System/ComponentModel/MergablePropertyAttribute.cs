using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this property can be combined with properties belonging to other objects in a Properties window.</summary>
	// Token: 0x02000590 RID: 1424
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MergablePropertyAttribute" /> class.</summary>
		/// <param name="allowMerge">
		///   <see langword="true" /> if this property can be combined with properties belonging to other objects in a Properties window; otherwise, <see langword="false" />.</param>
		// Token: 0x060034E8 RID: 13544 RVA: 0x000E6DF2 File Offset: 0x000E4FF2
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.allowMerge = allowMerge;
		}

		/// <summary>Gets a value indicating whether this property can be combined with properties belonging to other objects in a Properties window.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be combined with properties belonging to other objects in a Properties window; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000E6E01 File Offset: 0x000E5001
		public bool AllowMerge
		{
			get
			{
				return this.allowMerge;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034EA RID: 13546 RVA: 0x000E6E0C File Offset: 0x000E500C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			MergablePropertyAttribute mergablePropertyAttribute = obj as MergablePropertyAttribute;
			return mergablePropertyAttribute != null && mergablePropertyAttribute.AllowMerge == this.allowMerge;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MergablePropertyAttribute" />.</returns>
		// Token: 0x060034EB RID: 13547 RVA: 0x000E6E39 File Offset: 0x000E5039
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034EC RID: 13548 RVA: 0x000E6E41 File Offset: 0x000E5041
		public override bool IsDefaultAttribute()
		{
			return this.Equals(MergablePropertyAttribute.Default);
		}

		/// <summary>Specifies that a property can be combined with properties belonging to other objects in a Properties window. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A19 RID: 10777
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);

		/// <summary>Specifies that a property cannot be combined with properties belonging to other objects in a Properties window. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A1A RID: 10778
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.MergablePropertyAttribute.Yes" />, that is a property can be combined with properties belonging to other objects in a Properties window. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A1B RID: 10779
		public static readonly MergablePropertyAttribute Default = MergablePropertyAttribute.Yes;

		// Token: 0x04002A1C RID: 10780
		private bool allowMerge;
	}
}
