using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates the base serializer to use for a root designer object. This class cannot be inherited.</summary>
	// Token: 0x02000613 RID: 1555
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	[Obsolete("This attribute has been deprecated. Use DesignerSerializerAttribute instead.  For example, to specify a root designer for CodeDom, use DesignerSerializerAttribute(...,typeof(TypeCodeDomSerializer)).  http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class RootDesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerType">The data type of the serializer.</param>
		/// <param name="baseSerializerType">The base type of the serializer. A class can include multiple serializers as they all have different base types.</param>
		/// <param name="reloadable">
		///   <see langword="true" /> if this serializer supports dynamic reloading of the document; otherwise, <see langword="false" />.</param>
		// Token: 0x060038DA RID: 14554 RVA: 0x000F1961 File Offset: 0x000EFB61
		public RootDesignerSerializerAttribute(Type serializerType, Type baseSerializerType, bool reloadable)
		{
			this.serializerTypeName = serializerType.AssemblyQualifiedName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerType">The name of the base type of the serializer. A class can include multiple serializers, as they all have different base types.</param>
		/// <param name="reloadable">
		///   <see langword="true" /> if this serializer supports dynamic reloading of the document; otherwise, <see langword="false" />.</param>
		// Token: 0x060038DB RID: 14555 RVA: 0x000F1988 File Offset: 0x000EFB88
		public RootDesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType, bool reloadable)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerTypeName">The name of the base type of the serializer. A class can include multiple serializers as they all have different base types.</param>
		/// <param name="reloadable">
		///   <see langword="true" /> if this serializer supports dynamic reloading of the document; otherwise, <see langword="false" />.</param>
		// Token: 0x060038DC RID: 14556 RVA: 0x000F19AA File Offset: 0x000EFBAA
		public RootDesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName, bool reloadable)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerTypeName;
			this.reloadable = reloadable;
		}

		/// <summary>Gets a value indicating whether the root serializer supports reloading of the design document without first disposing the designer host.</summary>
		/// <returns>
		///   <see langword="true" /> if the root serializer supports reloading; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000F19C7 File Offset: 0x000EFBC7
		public bool Reloadable
		{
			get
			{
				return this.reloadable;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The name of the type of the serializer.</returns>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000F19CF File Offset: 0x000EFBCF
		public string SerializerTypeName
		{
			get
			{
				return this.serializerTypeName;
			}
		}

		/// <summary>Gets the fully qualified type name of the base type of the serializer.</summary>
		/// <returns>The name of the base type of the serializer.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000F19D7 File Offset: 0x000EFBD7
		public string SerializerBaseTypeName
		{
			get
			{
				return this.serializerBaseTypeName;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>An object containing a unique ID for this attribute type.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000F19E0 File Offset: 0x000EFBE0
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

		// Token: 0x04002B67 RID: 11111
		private bool reloadable;

		// Token: 0x04002B68 RID: 11112
		private string serializerTypeName;

		// Token: 0x04002B69 RID: 11113
		private string serializerBaseTypeName;

		// Token: 0x04002B6A RID: 11114
		private string typeId;
	}
}
