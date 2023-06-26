using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the editor to use to change a property. This class cannot be inherited.</summary>
	// Token: 0x0200054A RID: 1354
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the default editor, which is no editor.</summary>
		// Token: 0x060032D9 RID: 13017 RVA: 0x000E2445 File Offset: 0x000E0645
		public EditorAttribute()
		{
			this.typeName = string.Empty;
			this.baseTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and base type name of the editor.</summary>
		/// <param name="typeName">The fully qualified type name of the editor.</param>
		/// <param name="baseTypeName">The fully qualified type name of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />.</param>
		// Token: 0x060032DA RID: 13018 RVA: 0x000E2464 File Offset: 0x000E0664
		public EditorAttribute(string typeName, string baseTypeName)
		{
			string text = typeName.ToUpper(CultureInfo.InvariantCulture);
			this.typeName = typeName;
			this.baseTypeName = baseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and the base type.</summary>
		/// <param name="typeName">The fully qualified type name of the editor.</param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />.</param>
		// Token: 0x060032DB RID: 13019 RVA: 0x000E2494 File Offset: 0x000E0694
		public EditorAttribute(string typeName, Type baseType)
		{
			string text = typeName.ToUpper(CultureInfo.InvariantCulture);
			this.typeName = typeName;
			this.baseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type and the base type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the editor.</param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />.</param>
		// Token: 0x060032DC RID: 13020 RVA: 0x000E24C6 File Offset: 0x000E06C6
		public EditorAttribute(Type type, Type baseType)
		{
			this.typeName = type.AssemblyQualifiedName;
			this.baseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base class or interface serving as a lookup key for this editor.</summary>
		/// <returns>The name of the base class or interface serving as a lookup key for this editor.</returns>
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000E24E6 File Offset: 0x000E06E6
		public string EditorBaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
		}

		/// <summary>Gets the name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</summary>
		/// <returns>The name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</returns>
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000E24EE File Offset: 0x000E06EE
		public string EditorTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000E24F8 File Offset: 0x000E06F8
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.baseTypeName;
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

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032E0 RID: 13024 RVA: 0x000E2548 File Offset: 0x000E0748
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorAttribute editorAttribute = obj as EditorAttribute;
			return editorAttribute != null && editorAttribute.typeName == this.typeName && editorAttribute.baseTypeName == this.baseTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060032E1 RID: 13025 RVA: 0x000E258B File Offset: 0x000E078B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400298C RID: 10636
		private string baseTypeName;

		// Token: 0x0400298D RID: 10637
		private string typeName;

		// Token: 0x0400298E RID: 10638
		private string typeId;
	}
}
