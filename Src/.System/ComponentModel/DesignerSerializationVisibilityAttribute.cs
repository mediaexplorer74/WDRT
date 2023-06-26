using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the type of persistence to use when serializing a property on a component at design time.</summary>
	// Token: 0x02000543 RID: 1347
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" /> class using the specified <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> value.</summary>
		/// <param name="visibility">One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values.</param>
		// Token: 0x060032AE RID: 12974 RVA: 0x000E2175 File Offset: 0x000E0375
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility visibility)
		{
			this.visibility = visibility;
		}

		/// <summary>Gets a value indicating the basic serialization mode a serializer should use when determining whether and how to persist the value of a property.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. The default is <see cref="F:System.ComponentModel.DesignerSerializationVisibility.Visible" />.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000E2184 File Offset: 0x000E0384
		public DesignerSerializationVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032B0 RID: 12976 RVA: 0x000E218C File Offset: 0x000E038C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = obj as DesignerSerializationVisibilityAttribute;
			return designerSerializationVisibilityAttribute != null && designerSerializationVisibilityAttribute.Visibility == this.visibility;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060032B1 RID: 12977 RVA: 0x000E21B9 File Offset: 0x000E03B9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is set to the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032B2 RID: 12978 RVA: 0x000E21C1 File Offset: 0x000E03C1
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DesignerSerializationVisibilityAttribute.Default);
		}

		/// <summary>Specifies that a serializer should serialize the contents of the property, rather than the property itself. This field is read-only.</summary>
		// Token: 0x0400297B RID: 10619
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		/// <summary>Specifies that a serializer should not serialize the value of the property. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400297C RID: 10620
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		/// <summary>Specifies that a serializer should be allowed to serialize the value of the property. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400297D RID: 10621
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.DesignerSerializationVisibilityAttribute.Visible" />, that is, a visual designer uses default rules to generate the value of a property. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400297E RID: 10622
		public static readonly DesignerSerializationVisibilityAttribute Default = DesignerSerializationVisibilityAttribute.Visible;

		// Token: 0x0400297F RID: 10623
		private DesignerSerializationVisibility visibility;
	}
}
