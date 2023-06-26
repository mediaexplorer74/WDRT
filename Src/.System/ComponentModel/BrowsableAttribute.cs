using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property or event should be displayed in a Properties window.</summary>
	// Token: 0x0200051E RID: 1310
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BrowsableAttribute" /> class.</summary>
		/// <param name="browsable">
		///   <see langword="true" /> if a property or event can be modified at design time; otherwise, <see langword="false" />. The default is <see langword="true" />.</param>
		// Token: 0x060031B6 RID: 12726 RVA: 0x000DFB20 File Offset: 0x000DDD20
		public BrowsableAttribute(bool browsable)
		{
			this.browsable = browsable;
		}

		/// <summary>Gets a value indicating whether an object is browsable.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is browsable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000DFB36 File Offset: 0x000DDD36
		public bool Browsable
		{
			get
			{
				return this.browsable;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031B8 RID: 12728 RVA: 0x000DFB40 File Offset: 0x000DDD40
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			return browsableAttribute != null && browsableAttribute.Browsable == this.browsable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060031B9 RID: 12729 RVA: 0x000DFB6D File Offset: 0x000DDD6D
		public override int GetHashCode()
		{
			return this.browsable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031BA RID: 12730 RVA: 0x000DFB7A File Offset: 0x000DDD7A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BrowsableAttribute.Default);
		}

		/// <summary>Specifies that a property or event can be modified at design time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002926 RID: 10534
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		/// <summary>Specifies that a property or event cannot be modified at design time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002927 RID: 10535
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BrowsableAttribute" />, which is <see cref="F:System.ComponentModel.BrowsableAttribute.Yes" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002928 RID: 10536
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;

		// Token: 0x04002929 RID: 10537
		private bool browsable = true;
	}
}
