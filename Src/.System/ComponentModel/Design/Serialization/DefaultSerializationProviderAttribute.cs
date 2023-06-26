using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>The <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> attribute is placed on a serializer to indicate the class to use as a default provider of that type of serializer.</summary>
	// Token: 0x02000604 RID: 1540
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class DefaultSerializationProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the given provider type.</summary>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the serialization provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerType" /> is <see langword="null" />.</exception>
		// Token: 0x0600388F RID: 14479 RVA: 0x000F12DD File Offset: 0x000EF4DD
		public DefaultSerializationProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this._providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the named provider type.</summary>
		/// <param name="providerTypeName">The name of the serialization provider type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerTypeName" /> is <see langword="null" />.</exception>
		// Token: 0x06003890 RID: 14480 RVA: 0x000F1305 File Offset: 0x000EF505
		public DefaultSerializationProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this._providerTypeName = providerTypeName;
		}

		/// <summary>Gets the type name of the serialization provider.</summary>
		/// <returns>A string containing the name of the provider.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000F1322 File Offset: 0x000EF522
		public string ProviderTypeName
		{
			get
			{
				return this._providerTypeName;
			}
		}

		// Token: 0x04002B5A RID: 11098
		private string _providerTypeName;
	}
}
