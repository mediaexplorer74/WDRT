using System;

namespace System.ComponentModel
{
	/// <summary>Enables attribute redirection. This class cannot be inherited.</summary>
	// Token: 0x02000515 RID: 1301
	[AttributeUsage(AttributeTargets.Property)]
	public class AttributeProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		// Token: 0x0600313E RID: 12606 RVA: 0x000DECFE File Offset: 0x000DCEFE
		public AttributeProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this._typeName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name and property name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <param name="propertyName">The name of the property for which attributes will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyName" /> is <see langword="null" />.</exception>
		// Token: 0x0600313F RID: 12607 RVA: 0x000DED1B File Offset: 0x000DCF1B
		public AttributeProviderAttribute(string typeName, string propertyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			this._typeName = typeName;
			this._propertyName = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type.</summary>
		/// <param name="type">The type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06003140 RID: 12608 RVA: 0x000DED4D File Offset: 0x000DCF4D
		public AttributeProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Gets the assembly qualified type name passed into the constructor.</summary>
		/// <returns>The assembly qualified name of the type specified in the constructor.</returns>
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000DED75 File Offset: 0x000DCF75
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		/// <summary>Gets the name of the property for which attributes will be retrieved.</summary>
		/// <returns>The name of the property for which attributes will be retrieved.</returns>
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000DED7D File Offset: 0x000DCF7D
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x040028FF RID: 10495
		private string _typeName;

		// Token: 0x04002900 RID: 10496
		private string _propertyName;
	}
}
