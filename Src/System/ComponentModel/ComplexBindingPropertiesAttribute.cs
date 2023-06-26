using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the data source and data member properties for a component that supports complex data binding. This class cannot be inherited.</summary>
	// Token: 0x02000528 RID: 1320
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComplexBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using no parameters.</summary>
		// Token: 0x060031EE RID: 12782 RVA: 0x000E003D File Offset: 0x000DE23D
		public ComplexBindingPropertiesAttribute()
		{
			this.dataSource = null;
			this.dataMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source.</summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		// Token: 0x060031EF RID: 12783 RVA: 0x000E0053 File Offset: 0x000DE253
		public ComplexBindingPropertiesAttribute(string dataSource)
		{
			this.dataSource = dataSource;
			this.dataMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source and data member.</summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="dataMember">The name of the property to be used as the source for data.</param>
		// Token: 0x060031F0 RID: 12784 RVA: 0x000E0069 File Offset: 0x000DE269
		public ComplexBindingPropertiesAttribute(string dataSource, string dataMember)
		{
			this.dataSource = dataSource;
			this.dataMember = dataMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data source property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000E007F File Offset: 0x000DE27F
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		/// <summary>Gets the name of the data member property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data member property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000E0087 File Offset: 0x000DE287
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance</param>
		/// <returns>
		///   <see langword="true" /> if the object is equal to the current instance; otherwise, <see langword="false" />, indicating they are not equal.</returns>
		// Token: 0x060031F3 RID: 12787 RVA: 0x000E0090 File Offset: 0x000DE290
		public override bool Equals(object obj)
		{
			ComplexBindingPropertiesAttribute complexBindingPropertiesAttribute = obj as ComplexBindingPropertiesAttribute;
			return complexBindingPropertiesAttribute != null && complexBindingPropertiesAttribute.DataSource == this.dataSource && complexBindingPropertiesAttribute.DataMember == this.dataMember;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060031F4 RID: 12788 RVA: 0x000E00CD File Offset: 0x000DE2CD
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002941 RID: 10561
		private readonly string dataSource;

		// Token: 0x04002942 RID: 10562
		private readonly string dataMember;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04002943 RID: 10563
		public static readonly ComplexBindingPropertiesAttribute Default = new ComplexBindingPropertiesAttribute();
	}
}
