using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default binding property for a component. This class cannot be inherited.</summary>
	// Token: 0x0200053A RID: 1338
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultBindingPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using no parameters.</summary>
		// Token: 0x06003269 RID: 12905 RVA: 0x000E1B51 File Offset: 0x000DFD51
		public DefaultBindingPropertyAttribute()
		{
			this.name = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using the specified property name.</summary>
		/// <param name="name">The name of the default binding property.</param>
		// Token: 0x0600326A RID: 12906 RVA: 0x000E1B60 File Offset: 0x000DFD60
		public DefaultBindingPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</summary>
		/// <returns>The name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</returns>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000E1B6F File Offset: 0x000DFD6F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance</param>
		/// <returns>
		///   <see langword="true" /> if the object is equal to the current instance; otherwise, <see langword="false" />, indicating they are not equal.</returns>
		// Token: 0x0600326C RID: 12908 RVA: 0x000E1B78 File Offset: 0x000DFD78
		public override bool Equals(object obj)
		{
			DefaultBindingPropertyAttribute defaultBindingPropertyAttribute = obj as DefaultBindingPropertyAttribute;
			return defaultBindingPropertyAttribute != null && defaultBindingPropertyAttribute.Name == this.name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600326D RID: 12909 RVA: 0x000E1BA2 File Offset: 0x000DFDA2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002964 RID: 10596
		private readonly string name;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class.</summary>
		// Token: 0x04002965 RID: 10597
		public static readonly DefaultBindingPropertyAttribute Default = new DefaultBindingPropertyAttribute();
	}
}
