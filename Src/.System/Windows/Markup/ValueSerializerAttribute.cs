using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Markup
{
	/// <summary>Identifies the <see cref="T:System.Windows.Markup.ValueSerializer" /> class that a type or property should use when it is serialized.</summary>
	// Token: 0x020003A3 RID: 931
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	[TypeForwardedFrom("WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public sealed class ValueSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Markup.ValueSerializerAttribute" /> class, using the specified type.</summary>
		/// <param name="valueSerializerType">A type that represents the type of the <see cref="T:System.Windows.Markup.ValueSerializer" /> class.</param>
		// Token: 0x0600229C RID: 8860 RVA: 0x000A4C9C File Offset: 0x000A2E9C
		public ValueSerializerAttribute(Type valueSerializerType)
		{
			this._valueSerializerType = valueSerializerType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Markup.ValueSerializerAttribute" /> class, using an assembly qualified type name string.</summary>
		/// <param name="valueSerializerTypeName">The assembly qualified type name string for the <see cref="T:System.Windows.Markup.ValueSerializer" /> class to use.</param>
		// Token: 0x0600229D RID: 8861 RVA: 0x000A4CAB File Offset: 0x000A2EAB
		public ValueSerializerAttribute(string valueSerializerTypeName)
		{
			this._valueSerializerTypeName = valueSerializerTypeName;
		}

		/// <summary>Gets the type of the <see cref="T:System.Windows.Markup.ValueSerializer" /> class reported by this attribute.</summary>
		/// <returns>The type of the <see cref="T:System.Windows.Markup.ValueSerializer" />.</returns>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x000A4CBA File Offset: 0x000A2EBA
		public Type ValueSerializerType
		{
			get
			{
				if (this._valueSerializerType == null && this._valueSerializerTypeName != null)
				{
					this._valueSerializerType = Type.GetType(this._valueSerializerTypeName);
				}
				return this._valueSerializerType;
			}
		}

		/// <summary>Gets the assembly qualified name of the <see cref="T:System.Windows.Markup.ValueSerializer" /> type for this type or property.</summary>
		/// <returns>The assembly qualified name of the type.</returns>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x000A4CE9 File Offset: 0x000A2EE9
		public string ValueSerializerTypeName
		{
			get
			{
				if (this._valueSerializerType != null)
				{
					return this._valueSerializerType.AssemblyQualifiedName;
				}
				return this._valueSerializerTypeName;
			}
		}

		// Token: 0x04001F85 RID: 8069
		private Type _valueSerializerType;

		// Token: 0x04001F86 RID: 8070
		private string _valueSerializerTypeName;
	}
}
