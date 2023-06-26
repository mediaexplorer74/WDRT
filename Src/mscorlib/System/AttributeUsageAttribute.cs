using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the usage of another attribute class. This class cannot be inherited.</summary>
	// Token: 0x020000AF RID: 175
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AttributeUsageAttribute" /> class with the specified list of <see cref="T:System.AttributeTargets" />, the <see cref="P:System.AttributeUsageAttribute.AllowMultiple" /> value, and the <see cref="P:System.AttributeUsageAttribute.Inherited" /> value.</summary>
		/// <param name="validOn">The set of values combined using a bitwise OR operation to indicate which program elements are valid.</param>
		// Token: 0x06000A04 RID: 2564 RVA: 0x000206DD File Offset: 0x0001E8DD
		[__DynamicallyInvokable]
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this.m_attributeTarget = validOn;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000206FE File Offset: 0x0001E8FE
		internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
		{
			this.m_attributeTarget = validOn;
			this.m_allowMultiple = allowMultiple;
			this.m_inherited = inherited;
		}

		/// <summary>Gets a set of values identifying which program elements that the indicated attribute can be applied to.</summary>
		/// <returns>One or several <see cref="T:System.AttributeTargets" /> values. The default is <see langword="All" />.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002072D File Offset: 0x0001E92D
		[__DynamicallyInvokable]
		public AttributeTargets ValidOn
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_attributeTarget;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether more than one instance of the indicated attribute can be specified for a single program element.</summary>
		/// <returns>
		///   <see langword="true" /> if more than one instance is allowed to be specified; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00020735 File Offset: 0x0001E935
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0002073D File Offset: 0x0001E93D
		[__DynamicallyInvokable]
		public bool AllowMultiple
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_allowMultiple;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_allowMultiple = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether the indicated attribute is inherited by derived classes and overriding members.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute can be inherited by derived classes and overriding members; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00020746 File Offset: 0x0001E946
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002074E File Offset: 0x0001E94E
		[__DynamicallyInvokable]
		public bool Inherited
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_inherited;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_inherited = value;
			}
		}

		// Token: 0x040003E6 RID: 998
		internal AttributeTargets m_attributeTarget = AttributeTargets.All;

		// Token: 0x040003E7 RID: 999
		internal bool m_allowMultiple;

		// Token: 0x040003E8 RID: 1000
		internal bool m_inherited = true;

		// Token: 0x040003E9 RID: 1001
		internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
	}
}
