using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Identifies the property tab or tabs to display for the specified class or classes.</summary>
	// Token: 0x020005C0 RID: 1472
	[AttributeUsage(AttributeTargets.All)]
	public class PropertyTabAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class.</summary>
		// Token: 0x06003718 RID: 14104 RVA: 0x000EF3AB File Offset: 0x000ED5AB
		public PropertyTabAttribute()
		{
			this.tabScopes = new PropertyTabScope[0];
			this.tabClassNames = new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified type of tab.</summary>
		/// <param name="tabClass">The type of tab to create.</param>
		// Token: 0x06003719 RID: 14105 RVA: 0x000EF3CB File Offset: 0x000ED5CB
		public PropertyTabAttribute(Type tabClass)
			: this(tabClass, PropertyTabScope.Component)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified tab class name.</summary>
		/// <param name="tabClassName">The assembly qualified name of the type of tab to create. For an example of this format convention, see <see cref="P:System.Type.AssemblyQualifiedName" />.</param>
		// Token: 0x0600371A RID: 14106 RVA: 0x000EF3D5 File Offset: 0x000ED5D5
		public PropertyTabAttribute(string tabClassName)
			: this(tabClassName, PropertyTabScope.Component)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified type of tab and tab scope.</summary>
		/// <param name="tabClass">The type of tab to create.</param>
		/// <param name="tabScope">A <see cref="T:System.ComponentModel.PropertyTabScope" /> that indicates the scope of this tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tabScope" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.</exception>
		// Token: 0x0600371B RID: 14107 RVA: 0x000EF3E0 File Offset: 0x000ED5E0
		public PropertyTabAttribute(Type tabClass, PropertyTabScope tabScope)
		{
			this.tabClasses = new Type[] { tabClass };
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(SR.GetString("PropertyTabAttributeBadPropertyTabScope"), "tabScope");
			}
			this.tabScopes = new PropertyTabScope[] { tabScope };
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified tab class name and tab scope.</summary>
		/// <param name="tabClassName">The assembly qualified name of the type of tab to create. For an example of this format convention, see <see cref="P:System.Type.AssemblyQualifiedName" />.</param>
		/// <param name="tabScope">A <see cref="T:System.ComponentModel.PropertyTabScope" /> that indicates the scope of this tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tabScope" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.</exception>
		// Token: 0x0600371C RID: 14108 RVA: 0x000EF42C File Offset: 0x000ED62C
		public PropertyTabAttribute(string tabClassName, PropertyTabScope tabScope)
		{
			this.tabClassNames = new string[] { tabClassName };
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(SR.GetString("PropertyTabAttributeBadPropertyTabScope"), "tabScope");
			}
			this.tabScopes = new PropertyTabScope[] { tabScope };
		}

		/// <summary>Gets the types of tabs that this attribute uses.</summary>
		/// <returns>An array of types indicating the types of tabs that this attribute uses.</returns>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property could not be found.</exception>
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000EF478 File Offset: 0x000ED678
		public Type[] TabClasses
		{
			get
			{
				if (this.tabClasses == null && this.tabClassNames != null)
				{
					this.tabClasses = new Type[this.tabClassNames.Length];
					for (int i = 0; i < this.tabClassNames.Length; i++)
					{
						int num = this.tabClassNames[i].IndexOf(',');
						string text = null;
						string text2;
						if (num != -1)
						{
							text2 = this.tabClassNames[i].Substring(0, num).Trim();
							text = this.tabClassNames[i].Substring(num + 1).Trim();
						}
						else
						{
							text2 = this.tabClassNames[i];
						}
						this.tabClasses[i] = Type.GetType(text2, false);
						if (this.tabClasses[i] == null)
						{
							if (text == null)
							{
								throw new TypeLoadException(SR.GetString("PropertyTabAttributeTypeLoadException", new object[] { text2 }));
							}
							Assembly assembly = Assembly.Load(text);
							if (assembly != null)
							{
								this.tabClasses[i] = assembly.GetType(text2, true);
							}
						}
					}
				}
				return this.tabClasses;
			}
		}

		/// <summary>Gets the names of the tab classes that this attribute uses.</summary>
		/// <returns>The names of the tab classes that this attribute uses.</returns>
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000EF57C File Offset: 0x000ED77C
		protected string[] TabClassNames
		{
			get
			{
				if (this.tabClassNames != null)
				{
					return (string[])this.tabClassNames.Clone();
				}
				return null;
			}
		}

		/// <summary>Gets an array of tab scopes of each tab of this <see cref="T:System.ComponentModel.PropertyTabAttribute" />.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyTabScope" /> objects that indicate the scopes of the tabs.</returns>
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000EF598 File Offset: 0x000ED798
		public PropertyTabScope[] TabScopes
		{
			get
			{
				return this.tabScopes;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="other">An object to compare to this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> refers to the same <see cref="T:System.ComponentModel.PropertyTabAttribute" /> instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property of the <paramref name="other" /> parameter could not be found.</exception>
		// Token: 0x06003720 RID: 14112 RVA: 0x000EF5A0 File Offset: 0x000ED7A0
		public override bool Equals(object other)
		{
			return other is PropertyTabAttribute && this.Equals((PropertyTabAttribute)other);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified attribute.</summary>
		/// <param name="other">A <see cref="T:System.ComponentModel.PropertyTabAttribute" /> to compare to this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> instances are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property of the <paramref name="other" /> parameter cannot be found.</exception>
		// Token: 0x06003721 RID: 14113 RVA: 0x000EF5B8 File Offset: 0x000ED7B8
		public bool Equals(PropertyTabAttribute other)
		{
			if (other == this)
			{
				return true;
			}
			if (other.TabClasses.Length != this.TabClasses.Length || other.TabScopes.Length != this.TabScopes.Length)
			{
				return false;
			}
			for (int i = 0; i < this.TabClasses.Length; i++)
			{
				if (this.TabClasses[i] != other.TabClasses[i] || this.TabScopes[i] != other.TabScopes[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06003722 RID: 14114 RVA: 0x000EF630 File Offset: 0x000ED830
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Initializes the attribute using the specified names of tab classes and array of tab scopes.</summary>
		/// <param name="tabClassNames">An array of fully qualified type names of the types to create for tabs on the Properties window.</param>
		/// <param name="tabScopes">The scope of each tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document.</param>
		/// <exception cref="T:System.ArgumentException">One or more of the values in <paramref name="tabScopes" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.  
		///  -or-  
		///  The length of the <paramref name="tabClassNames" /> and <paramref name="tabScopes" /> arrays do not match.  
		///  -or-  
		///  <paramref name="tabClassNames" /> or <paramref name="tabScopes" /> is <see langword="null" />.</exception>
		// Token: 0x06003723 RID: 14115 RVA: 0x000EF638 File Offset: 0x000ED838
		protected void InitializeArrays(string[] tabClassNames, PropertyTabScope[] tabScopes)
		{
			this.InitializeArrays(tabClassNames, null, tabScopes);
		}

		/// <summary>Initializes the attribute using the specified names of tab classes and array of tab scopes.</summary>
		/// <param name="tabClasses">The types of tabs to create.</param>
		/// <param name="tabScopes">The scope of each tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document.</param>
		/// <exception cref="T:System.ArgumentException">One or more of the values in <paramref name="tabScopes" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.  
		///  -or-  
		///  The length of the <paramref name="tabClassNames" /> and <paramref name="tabScopes" /> arrays do not match.  
		///  -or-  
		///  <paramref name="tabClassNames" /> or <paramref name="tabScopes" /> is <see langword="null" />.</exception>
		// Token: 0x06003724 RID: 14116 RVA: 0x000EF643 File Offset: 0x000ED843
		protected void InitializeArrays(Type[] tabClasses, PropertyTabScope[] tabScopes)
		{
			this.InitializeArrays(null, tabClasses, tabScopes);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000EF650 File Offset: 0x000ED850
		private void InitializeArrays(string[] tabClassNames, Type[] tabClasses, PropertyTabScope[] tabScopes)
		{
			if (tabClasses != null)
			{
				if (tabScopes != null && tabClasses.Length != tabScopes.Length)
				{
					throw new ArgumentException(SR.GetString("PropertyTabAttributeArrayLengthMismatch"));
				}
				this.tabClasses = (Type[])tabClasses.Clone();
			}
			else if (tabClassNames != null)
			{
				if (tabScopes != null && tabClasses.Length != tabScopes.Length)
				{
					throw new ArgumentException(SR.GetString("PropertyTabAttributeArrayLengthMismatch"));
				}
				this.tabClassNames = (string[])tabClassNames.Clone();
				this.tabClasses = null;
			}
			else if (this.tabClasses == null && this.tabClassNames == null)
			{
				throw new ArgumentException(SR.GetString("PropertyTabAttributeParamsBothNull"));
			}
			if (tabScopes != null)
			{
				for (int i = 0; i < tabScopes.Length; i++)
				{
					if (tabScopes[i] < PropertyTabScope.Document)
					{
						throw new ArgumentException(SR.GetString("PropertyTabAttributeBadPropertyTabScope"));
					}
				}
				this.tabScopes = (PropertyTabScope[])tabScopes.Clone();
				return;
			}
			this.tabScopes = new PropertyTabScope[tabClasses.Length];
			for (int j = 0; j < this.TabScopes.Length; j++)
			{
				this.tabScopes[j] = PropertyTabScope.Component;
			}
		}

		// Token: 0x04002AB8 RID: 10936
		private PropertyTabScope[] tabScopes;

		// Token: 0x04002AB9 RID: 10937
		private Type[] tabClasses;

		// Token: 0x04002ABA RID: 10938
		private string[] tabClassNames;
	}
}
