using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Represents an attribute of a toolbox item.</summary>
	// Token: 0x020005C4 RID: 1476
	[AttributeUsage(AttributeTargets.All)]
	public class ToolboxItemAttribute : Attribute
	{
		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600372C RID: 14124 RVA: 0x000EF7B7 File Offset: 0x000ED9B7
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxItemAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and specifies whether to use default initialization values.</summary>
		/// <param name="defaultType">
		///   <see langword="true" /> to create a toolbox item attribute for a default type; <see langword="false" /> to associate no default toolbox item support for this attribute.</param>
		// Token: 0x0600372D RID: 14125 RVA: 0x000EF7C4 File Offset: 0x000ED9C4
		public ToolboxItemAttribute(bool defaultType)
		{
			if (defaultType)
			{
				this.toolboxItemTypeName = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified name of the type.</summary>
		/// <param name="toolboxItemTypeName">The names of the type of the toolbox item and of the assembly that contains the type.</param>
		// Token: 0x0600372E RID: 14126 RVA: 0x000EF7DC File Offset: 0x000ED9DC
		public ToolboxItemAttribute(string toolboxItemTypeName)
		{
			string text = toolboxItemTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.toolboxItemTypeName = toolboxItemTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified type of the toolbox item.</summary>
		/// <param name="toolboxItemType">The type of the toolbox item.</param>
		// Token: 0x0600372F RID: 14127 RVA: 0x000EF802 File Offset: 0x000EDA02
		public ToolboxItemAttribute(Type toolboxItemType)
		{
			this.toolboxItemType = toolboxItemType;
			this.toolboxItemTypeName = toolboxItemType.AssemblyQualifiedName;
		}

		/// <summary>Gets or sets the type of the toolbox item.</summary>
		/// <returns>The type of the toolbox item.</returns>
		/// <exception cref="T:System.ArgumentException">The type cannot be found.</exception>
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000EF820 File Offset: 0x000EDA20
		public Type ToolboxItemType
		{
			get
			{
				if (this.toolboxItemType == null && this.toolboxItemTypeName != null)
				{
					try
					{
						this.toolboxItemType = Type.GetType(this.toolboxItemTypeName, true);
					}
					catch (Exception ex)
					{
						throw new ArgumentException(SR.GetString("ToolboxItemAttributeFailedGetType", new object[] { this.toolboxItemTypeName }), ex);
					}
				}
				return this.toolboxItemType;
			}
		}

		/// <summary>Gets or sets the name of the type of the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>The fully qualified type name of the current toolbox item.</returns>
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000EF890 File Offset: 0x000EDA90
		public string ToolboxItemTypeName
		{
			get
			{
				if (this.toolboxItemTypeName == null)
				{
					return string.Empty;
				}
				return this.toolboxItemTypeName;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003732 RID: 14130 RVA: 0x000EF8A8 File Offset: 0x000EDAA8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemAttribute toolboxItemAttribute = obj as ToolboxItemAttribute;
			return toolboxItemAttribute != null && toolboxItemAttribute.ToolboxItemTypeName == this.ToolboxItemTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003733 RID: 14131 RVA: 0x000EF8D8 File Offset: 0x000EDAD8
		public override int GetHashCode()
		{
			if (this.toolboxItemTypeName != null)
			{
				return this.toolboxItemTypeName.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x04002AC8 RID: 10952
		private Type toolboxItemType;

		// Token: 0x04002AC9 RID: 10953
		private string toolboxItemTypeName;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to the default, <see cref="T:System.Drawing.Design.ToolboxItem" />. This field is read-only.</summary>
		// Token: 0x04002ACA RID: 10954
		public static readonly ToolboxItemAttribute Default = new ToolboxItemAttribute("System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to <see langword="null" />. This field is read-only.</summary>
		// Token: 0x04002ACB RID: 10955
		public static readonly ToolboxItemAttribute None = new ToolboxItemAttribute(false);
	}
}
