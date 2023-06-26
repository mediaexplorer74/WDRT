using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a property that is offered by an extender provider. This class cannot be inherited.</summary>
	// Token: 0x02000553 RID: 1363
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x0600333B RID: 13115 RVA: 0x000E35B4 File Offset: 0x000E17B4
		internal static ExtenderProvidedPropertyAttribute Create(PropertyDescriptor extenderProperty, Type receiverType, IExtenderProvider provider)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				extenderProperty = extenderProperty,
				receiverType = receiverType,
				provider = provider
			};
		}

		/// <summary>Gets the property that is being provided.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> encapsulating the property that is being provided.</returns>
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000E35E5 File Offset: 0x000E17E5
		public PropertyDescriptor ExtenderProperty
		{
			get
			{
				return this.extenderProperty;
			}
		}

		/// <summary>Gets the extender provider that is providing the property.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IExtenderProvider" /> that is providing the property.</returns>
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000E35ED File Offset: 0x000E17ED
		public IExtenderProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		/// <summary>Gets the type of object that can receive the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> describing the type of object that can receive the property.</returns>
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000E35F5 File Offset: 0x000E17F5
		public Type ReceiverType
		{
			get
			{
				return this.receiverType;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003340 RID: 13120 RVA: 0x000E3600 File Offset: 0x000E1800
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = obj as ExtenderProvidedPropertyAttribute;
			return extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.extenderProperty.Equals(this.extenderProperty) && extenderProvidedPropertyAttribute.provider.Equals(this.provider) && extenderProvidedPropertyAttribute.receiverType.Equals(this.receiverType);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003341 RID: 13121 RVA: 0x000E3656 File Offset: 0x000E1856
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Provides an indication whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003342 RID: 13122 RVA: 0x000E365E File Offset: 0x000E185E
		public override bool IsDefaultAttribute()
		{
			return this.receiverType == null;
		}

		// Token: 0x040029A2 RID: 10658
		private PropertyDescriptor extenderProperty;

		// Token: 0x040029A3 RID: 10659
		private IExtenderProvider provider;

		// Token: 0x040029A4 RID: 10660
		private Type receiverType;
	}
}
