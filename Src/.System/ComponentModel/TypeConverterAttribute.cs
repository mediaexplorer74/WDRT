using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies what type to use as a converter for the object this attribute is bound to.</summary>
	// Token: 0x020005B2 RID: 1458
	[AttributeUsage(AttributeTargets.All)]
	public sealed class TypeConverterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class with the default type converter, which is an empty string ("").</summary>
		// Token: 0x06003653 RID: 13907 RVA: 0x000EC517 File Offset: 0x000EA717
		public TypeConverterAttribute()
		{
			this.typeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type as the data converter for the object this attribute is bound to.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the converter class to use for data conversion for the object this attribute is bound to.</param>
		// Token: 0x06003654 RID: 13908 RVA: 0x000EC52A File Offset: 0x000EA72A
		public TypeConverterAttribute(Type type)
		{
			this.typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type name as the data converter for the object this attribute is bound to.</summary>
		/// <param name="typeName">The fully qualified name of the class to use for data conversion for the object this attribute is bound to.</param>
		// Token: 0x06003655 RID: 13909 RVA: 0x000EC540 File Offset: 0x000EA740
		public TypeConverterAttribute(string typeName)
		{
			string text = typeName.ToUpper(CultureInfo.InvariantCulture);
			this.typeName = typeName;
		}

		/// <summary>Gets the fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to.</summary>
		/// <returns>The fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to, or an empty string ("") if none exists. The default value is an empty string ("").</returns>
		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003656 RID: 13910 RVA: 0x000EC566 File Offset: 0x000EA766
		public string ConverterTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003657 RID: 13911 RVA: 0x000EC570 File Offset: 0x000EA770
		public override bool Equals(object obj)
		{
			TypeConverterAttribute typeConverterAttribute = obj as TypeConverterAttribute;
			return typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName == this.typeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</returns>
		// Token: 0x06003658 RID: 13912 RVA: 0x000EC59A File Offset: 0x000EA79A
		public override int GetHashCode()
		{
			return this.typeName.GetHashCode();
		}

		// Token: 0x04002A8E RID: 10894
		private string typeName;

		/// <summary>Specifies the type to use as a converter for the object this attribute is bound to.</summary>
		// Token: 0x04002A8F RID: 10895
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();
	}
}
