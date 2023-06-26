using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the context keyword for a class or member. This class cannot be inherited.</summary>
	// Token: 0x020005E0 RID: 1504
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class HelpKeywordAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class.</summary>
		// Token: 0x060037BF RID: 14271 RVA: 0x000F0691 File Offset: 0x000EE891
		public HelpKeywordAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class.</summary>
		/// <param name="keyword">The Help keyword value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is <see langword="null" />.</exception>
		// Token: 0x060037C0 RID: 14272 RVA: 0x000F0699 File Offset: 0x000EE899
		public HelpKeywordAttribute(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			this.contextKeyword = keyword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class from the given type.</summary>
		/// <param name="t">The type from which the Help keyword will be taken.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="t" /> is <see langword="null" />.</exception>
		// Token: 0x060037C1 RID: 14273 RVA: 0x000F06B6 File Offset: 0x000EE8B6
		public HelpKeywordAttribute(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			this.contextKeyword = t.FullName;
		}

		/// <summary>Gets the Help keyword supplied by this attribute.</summary>
		/// <returns>The Help keyword supplied by this attribute.</returns>
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000F06DE File Offset: 0x000EE8DE
		public string HelpKeyword
		{
			get
			{
				return this.contextKeyword;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> to compare with the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> is equal to the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037C3 RID: 14275 RVA: 0x000F06E6 File Offset: 0x000EE8E6
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is HelpKeywordAttribute && ((HelpKeywordAttribute)obj).HelpKeyword == this.HelpKeyword);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</returns>
		// Token: 0x060037C4 RID: 14276 RVA: 0x000F0711 File Offset: 0x000EE911
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether the Help keyword is <see langword="null" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the Help keyword is <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037C5 RID: 14277 RVA: 0x000F0719 File Offset: 0x000EE919
		public override bool IsDefaultAttribute()
		{
			return this.Equals(HelpKeywordAttribute.Default);
		}

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />. This field is read-only.</summary>
		// Token: 0x04002AF0 RID: 10992
		public static readonly HelpKeywordAttribute Default = new HelpKeywordAttribute();

		// Token: 0x04002AF1 RID: 10993
		private string contextKeyword;
	}
}
