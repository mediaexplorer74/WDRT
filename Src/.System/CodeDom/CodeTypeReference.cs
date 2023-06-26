using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a type.</summary>
	// Token: 0x02000664 RID: 1636
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeReference : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class.</summary>
		// Token: 0x06003B33 RID: 15155 RVA: 0x000F4914 File Offset: 0x000F2B14
		public CodeTypeReference()
		{
			this.baseType = string.Empty;
			this.arrayRank = 0;
			this.arrayElementType = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to reference.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06003B34 RID: 15156 RVA: 0x000F4938 File Offset: 0x000F2B38
		public CodeTypeReference(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsArray)
			{
				this.arrayRank = type.GetArrayRank();
				this.arrayElementType = new CodeTypeReference(type.GetElementType());
				this.baseType = null;
			}
			else
			{
				this.InitializeFromType(type);
				this.arrayRank = 0;
				this.arrayElementType = null;
			}
			this.isInterface = type.IsInterface;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type and code type reference.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to reference.</param>
		/// <param name="codeTypeReferenceOption">The code type reference option, one of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values.</param>
		// Token: 0x06003B35 RID: 15157 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public CodeTypeReference(Type type, CodeTypeReferenceOptions codeTypeReferenceOption)
			: this(type)
		{
			this.referenceOptions = codeTypeReferenceOption;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name and code type reference option.</summary>
		/// <param name="typeName">The name of the type to reference.</param>
		/// <param name="codeTypeReferenceOption">The code type reference option, one of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values.</param>
		// Token: 0x06003B36 RID: 15158 RVA: 0x000F49BE File Offset: 0x000F2BBE
		public CodeTypeReference(string typeName, CodeTypeReferenceOptions codeTypeReferenceOption)
		{
			this.Initialize(typeName, codeTypeReferenceOption);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name.</summary>
		/// <param name="typeName">The name of the type to reference.</param>
		// Token: 0x06003B37 RID: 15159 RVA: 0x000F49CE File Offset: 0x000F2BCE
		public CodeTypeReference(string typeName)
		{
			this.Initialize(typeName);
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000F49E0 File Offset: 0x000F2BE0
		private void InitializeFromType(Type type)
		{
			this.baseType = type.Name;
			if (!type.IsGenericParameter)
			{
				Type type2 = type;
				while (type2.IsNested)
				{
					type2 = type2.DeclaringType;
					this.baseType = type2.Name + "+" + this.baseType;
				}
				if (!string.IsNullOrEmpty(type.Namespace))
				{
					this.baseType = type.Namespace + "." + this.baseType;
				}
			}
			if (type.IsGenericType && !type.ContainsGenericParameters)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					this.TypeArguments.Add(new CodeTypeReference(genericArguments[i]));
				}
				return;
			}
			if (!type.IsGenericTypeDefinition)
			{
				this.needsFixup = true;
			}
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000F4AA2 File Offset: 0x000F2CA2
		private void Initialize(string typeName)
		{
			this.Initialize(typeName, this.referenceOptions);
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000F4AB4 File Offset: 0x000F2CB4
		private void Initialize(string typeName, CodeTypeReferenceOptions options)
		{
			this.Options = options;
			if (typeName == null || typeName.Length == 0)
			{
				typeName = typeof(void).FullName;
				this.baseType = typeName;
				this.arrayRank = 0;
				this.arrayElementType = null;
				return;
			}
			typeName = this.RipOffAssemblyInformationFromTypeName(typeName);
			int num = typeName.Length - 1;
			int i = num;
			this.needsFixup = true;
			Queue queue = new Queue();
			while (i >= 0)
			{
				int num2 = 1;
				if (typeName[i--] != ']')
				{
					break;
				}
				while (i >= 0 && typeName[i] == ',')
				{
					num2++;
					i--;
				}
				if (i < 0 || typeName[i] != '[')
				{
					break;
				}
				queue.Enqueue(num2);
				i--;
				num = i;
			}
			i = num;
			ArrayList arrayList = new ArrayList();
			Stack stack = new Stack();
			if (i > 0 && typeName[i--] == ']')
			{
				this.needsFixup = false;
				int num3 = 1;
				int num4 = num;
				while (i >= 0)
				{
					if (typeName[i] == '[')
					{
						if (--num3 == 0)
						{
							break;
						}
					}
					else if (typeName[i] == ']')
					{
						num3++;
					}
					else if (typeName[i] == ',' && num3 == 1)
					{
						if (i + 1 < num4)
						{
							stack.Push(typeName.Substring(i + 1, num4 - i - 1));
						}
						num4 = i;
					}
					i--;
				}
				if (i > 0 && num - i - 1 > 0)
				{
					if (i + 1 < num4)
					{
						stack.Push(typeName.Substring(i + 1, num4 - i - 1));
					}
					while (stack.Count > 0)
					{
						string text = this.RipOffAssemblyInformationFromTypeName((string)stack.Pop());
						arrayList.Add(new CodeTypeReference(text));
					}
					num = i - 1;
				}
			}
			if (num < 0)
			{
				this.baseType = typeName;
				return;
			}
			if (queue.Count > 0)
			{
				CodeTypeReference codeTypeReference = new CodeTypeReference(typeName.Substring(0, num + 1), this.Options);
				for (int j = 0; j < arrayList.Count; j++)
				{
					codeTypeReference.TypeArguments.Add((CodeTypeReference)arrayList[j]);
				}
				while (queue.Count > 1)
				{
					codeTypeReference = new CodeTypeReference(codeTypeReference, (int)queue.Dequeue());
				}
				this.baseType = null;
				this.arrayRank = (int)queue.Dequeue();
				this.arrayElementType = codeTypeReference;
			}
			else if (arrayList.Count > 0)
			{
				for (int k = 0; k < arrayList.Count; k++)
				{
					this.TypeArguments.Add((CodeTypeReference)arrayList[k]);
				}
				this.baseType = typeName.Substring(0, num + 1);
			}
			else
			{
				this.baseType = typeName;
			}
			if (this.baseType != null && this.baseType.IndexOf('`') != -1)
			{
				this.needsFixup = false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name and type arguments.</summary>
		/// <param name="typeName">The name of the type to reference.</param>
		/// <param name="typeArguments">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values.</param>
		// Token: 0x06003B3B RID: 15163 RVA: 0x000F4D71 File Offset: 0x000F2F71
		public CodeTypeReference(string typeName, params CodeTypeReference[] typeArguments)
			: this(typeName)
		{
			if (typeArguments != null && typeArguments.Length != 0)
			{
				this.TypeArguments.AddRange(typeArguments);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified code type parameter.</summary>
		/// <param name="typeParameter">A <see cref="T:System.CodeDom.CodeTypeParameter" /> that represents the type of the type parameter.</param>
		// Token: 0x06003B3C RID: 15164 RVA: 0x000F4D8D File Offset: 0x000F2F8D
		public CodeTypeReference(CodeTypeParameter typeParameter)
			: this((typeParameter == null) ? null : typeParameter.Name)
		{
			this.referenceOptions = CodeTypeReferenceOptions.GenericTypeParameter;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified array type name and rank.</summary>
		/// <param name="baseType">The name of the type of the elements of the array.</param>
		/// <param name="rank">The number of dimensions of the array.</param>
		// Token: 0x06003B3D RID: 15165 RVA: 0x000F4DA8 File Offset: 0x000F2FA8
		public CodeTypeReference(string baseType, int rank)
		{
			this.baseType = null;
			this.arrayRank = rank;
			this.arrayElementType = new CodeTypeReference(baseType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified array type and rank.</summary>
		/// <param name="arrayType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</param>
		/// <param name="rank">The number of dimensions in the array.</param>
		// Token: 0x06003B3E RID: 15166 RVA: 0x000F4DCA File Offset: 0x000F2FCA
		public CodeTypeReference(CodeTypeReference arrayType, int rank)
		{
			this.baseType = null;
			this.arrayRank = rank;
			this.arrayElementType = arrayType;
		}

		/// <summary>Gets or sets the type of the elements in the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array elements.</returns>
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000F4DE7 File Offset: 0x000F2FE7
		// (set) Token: 0x06003B40 RID: 15168 RVA: 0x000F4DEF File Offset: 0x000F2FEF
		public CodeTypeReference ArrayElementType
		{
			get
			{
				return this.arrayElementType;
			}
			set
			{
				this.arrayElementType = value;
			}
		}

		/// <summary>Gets or sets the array rank of the array.</summary>
		/// <returns>The number of dimensions of the array.</returns>
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x000F4DF8 File Offset: 0x000F2FF8
		// (set) Token: 0x06003B42 RID: 15170 RVA: 0x000F4E00 File Offset: 0x000F3000
		public int ArrayRank
		{
			get
			{
				return this.arrayRank;
			}
			set
			{
				this.arrayRank = value;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000F4E09 File Offset: 0x000F3009
		internal int NestedArrayDepth
		{
			get
			{
				if (this.arrayElementType == null)
				{
					return 0;
				}
				return 1 + this.arrayElementType.NestedArrayDepth;
			}
		}

		/// <summary>Gets or sets the name of the type being referenced.</summary>
		/// <returns>The name of the type being referenced.</returns>
		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000F4E24 File Offset: 0x000F3024
		// (set) Token: 0x06003B45 RID: 15173 RVA: 0x000F4EA3 File Offset: 0x000F30A3
		public string BaseType
		{
			get
			{
				if (this.arrayRank > 0 && this.arrayElementType != null)
				{
					return this.arrayElementType.BaseType;
				}
				if (string.IsNullOrEmpty(this.baseType))
				{
					return string.Empty;
				}
				string text = this.baseType;
				if (this.needsFixup && this.TypeArguments.Count > 0)
				{
					text = text + "`" + this.TypeArguments.Count.ToString(CultureInfo.InvariantCulture);
				}
				return text;
			}
			set
			{
				this.baseType = value;
				this.Initialize(this.baseType);
			}
		}

		/// <summary>Gets or sets the code type reference option.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values.</returns>
		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000F4EB8 File Offset: 0x000F30B8
		// (set) Token: 0x06003B47 RID: 15175 RVA: 0x000F4EC0 File Offset: 0x000F30C0
		[ComVisible(false)]
		public CodeTypeReferenceOptions Options
		{
			get
			{
				return this.referenceOptions;
			}
			set
			{
				this.referenceOptions = value;
			}
		}

		/// <summary>Gets the type arguments for the current generic type reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current <see cref="T:System.CodeDom.CodeTypeReference" /> object.</returns>
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000F4EC9 File Offset: 0x000F30C9
		[ComVisible(false)]
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.arrayRank > 0 && this.arrayElementType != null)
				{
					return this.arrayElementType.TypeArguments;
				}
				if (this.typeArguments == null)
				{
					this.typeArguments = new CodeTypeReferenceCollection();
				}
				return this.typeArguments;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000F4F01 File Offset: 0x000F3101
		internal bool IsInterface
		{
			get
			{
				return this.isInterface;
			}
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000F4F0C File Offset: 0x000F310C
		private string RipOffAssemblyInformationFromTypeName(string typeName)
		{
			int i = 0;
			int num = typeName.Length - 1;
			string text = typeName;
			while (i < typeName.Length)
			{
				if (!char.IsWhiteSpace(typeName[i]))
				{
					break;
				}
				i++;
			}
			while (num >= 0 && char.IsWhiteSpace(typeName[num]))
			{
				num--;
			}
			if (i < num)
			{
				if (typeName[i] == '[' && typeName[num] == ']')
				{
					i++;
					num--;
				}
				if (typeName[num] != ']')
				{
					int num2 = 0;
					for (int j = num; j >= i; j--)
					{
						if (typeName[j] == ',')
						{
							num2++;
							if (num2 == 4)
							{
								text = typeName.Substring(i, j - i);
								break;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x04002C2A RID: 11306
		private string baseType;

		// Token: 0x04002C2B RID: 11307
		[OptionalField]
		private bool isInterface;

		// Token: 0x04002C2C RID: 11308
		private int arrayRank;

		// Token: 0x04002C2D RID: 11309
		private CodeTypeReference arrayElementType;

		// Token: 0x04002C2E RID: 11310
		[OptionalField]
		private CodeTypeReferenceCollection typeArguments;

		// Token: 0x04002C2F RID: 11311
		[OptionalField]
		private CodeTypeReferenceOptions referenceOptions;

		// Token: 0x04002C30 RID: 11312
		[OptionalField]
		private bool needsFixup;
	}
}
