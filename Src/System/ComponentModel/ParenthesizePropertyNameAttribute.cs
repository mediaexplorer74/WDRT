using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the name of the associated property is displayed with parentheses in the Properties window. This class cannot be inherited.</summary>
	// Token: 0x020005BF RID: 1471
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class that indicates that the associated property should not be shown with parentheses.</summary>
		// Token: 0x06003711 RID: 14097 RVA: 0x000EF34B File Offset: 0x000ED54B
		public ParenthesizePropertyNameAttribute()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class, using the specified value to indicate whether the attribute is displayed with parentheses.</summary>
		/// <param name="needParenthesis">
		///   <see langword="true" /> if the name should be enclosed in parentheses; otherwise, <see langword="false" />.</param>
		// Token: 0x06003712 RID: 14098 RVA: 0x000EF354 File Offset: 0x000ED554
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.needParenthesis = needParenthesis;
		}

		/// <summary>Gets a value indicating whether the Properties window displays the name of the property in parentheses in the Properties window.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is displayed with parentheses; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000EF363 File Offset: 0x000ED563
		public bool NeedParenthesis
		{
			get
			{
				return this.needParenthesis;
			}
		}

		/// <summary>Compares the specified object to this object and tests for equality.</summary>
		/// <param name="o">The object to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003714 RID: 14100 RVA: 0x000EF36B File Offset: 0x000ED56B
		public override bool Equals(object o)
		{
			return o is ParenthesizePropertyNameAttribute && ((ParenthesizePropertyNameAttribute)o).NeedParenthesis == this.needParenthesis;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06003715 RID: 14101 RVA: 0x000EF38A File Offset: 0x000ED58A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default value of the attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003716 RID: 14102 RVA: 0x000EF392 File Offset: 0x000ED592
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParenthesizePropertyNameAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class with a default value that indicates that the associated property should not be shown with parentheses. This field is read-only.</summary>
		// Token: 0x04002AB6 RID: 10934
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();

		// Token: 0x04002AB7 RID: 10935
		private bool needParenthesis;
	}
}
