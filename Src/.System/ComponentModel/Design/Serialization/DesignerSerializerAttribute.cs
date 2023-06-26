using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates a serializer for the serialization manager to use to serialize the values of the type this attribute is applied to. This class cannot be inherited.</summary>
	// Token: 0x02000606 RID: 1542
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerType">The data type of the serializer.</param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types.</param>
		// Token: 0x06003897 RID: 14487 RVA: 0x000F1337 File Offset: 0x000EF537
		public DesignerSerializerAttribute(Type serializerType, Type baseSerializerType)
		{
			this.serializerTypeName = serializerType.AssemblyQualifiedName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types.</param>
		// Token: 0x06003898 RID: 14488 RVA: 0x000F1357 File Offset: 0x000EF557
		public DesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerTypeName">The fully qualified name of the base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types.</param>
		// Token: 0x06003899 RID: 14489 RVA: 0x000F1372 File Offset: 0x000EF572
		public DesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerTypeName;
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The fully qualified type name of the serializer.</returns>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x000F1388 File Offset: 0x000EF588
		public string SerializerTypeName
		{
			get
			{
				return this.serializerTypeName;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer base type.</summary>
		/// <returns>The fully qualified type name of the serializer base type.</returns>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x000F1390 File Offset: 0x000EF590
		public string SerializerBaseTypeName
		{
			get
			{
				return this.serializerBaseTypeName;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000F1398 File Offset: 0x000EF598
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.serializerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		// Token: 0x04002B5B RID: 11099
		private string serializerTypeName;

		// Token: 0x04002B5C RID: 11100
		private string serializerBaseTypeName;

		// Token: 0x04002B5D RID: 11101
		private string typeId;
	}
}
