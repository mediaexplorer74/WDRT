using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default property for a component.</summary>
	// Token: 0x0200053C RID: 1340
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the default property for the component this attribute is bound to.</param>
		// Token: 0x06003274 RID: 12916 RVA: 0x000E1C0F File Offset: 0x000DFE0F
		public DefaultPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the default property for the component this attribute is bound to.</summary>
		/// <returns>The name of the default property for the component this attribute is bound to. The default value is <see langword="null" />.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000E1C1E File Offset: 0x000DFE1E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003276 RID: 12918 RVA: 0x000E1C28 File Offset: 0x000DFE28
		public override bool Equals(object obj)
		{
			DefaultPropertyAttribute defaultPropertyAttribute = obj as DefaultPropertyAttribute;
			return defaultPropertyAttribute != null && defaultPropertyAttribute.Name == this.name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003277 RID: 12919 RVA: 0x000E1C52 File Offset: 0x000DFE52
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002968 RID: 10600
		private readonly string name;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />, which is <see langword="null" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002969 RID: 10601
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
