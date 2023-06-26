using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default event for a component.</summary>
	// Token: 0x0200053B RID: 1339
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultEventAttribute" /> class.</summary>
		/// <param name="name">The name of the default event for the component this attribute is bound to.</param>
		// Token: 0x0600326F RID: 12911 RVA: 0x000E1BB6 File Offset: 0x000DFDB6
		public DefaultEventAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the default event for the component this attribute is bound to.</summary>
		/// <returns>The name of the default event for the component this attribute is bound to. The default value is <see langword="null" />.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000E1BC5 File Offset: 0x000DFDC5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultEventAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003271 RID: 12913 RVA: 0x000E1BD0 File Offset: 0x000DFDD0
		public override bool Equals(object obj)
		{
			DefaultEventAttribute defaultEventAttribute = obj as DefaultEventAttribute;
			return defaultEventAttribute != null && defaultEventAttribute.Name == this.name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003272 RID: 12914 RVA: 0x000E1BFA File Offset: 0x000DFDFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002966 RID: 10598
		private readonly string name;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultEventAttribute" />, which is <see langword="null" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002967 RID: 10599
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
