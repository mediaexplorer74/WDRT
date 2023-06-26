using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000649 RID: 1609
	internal sealed class SymbolType : TypeInfo
	{
		// Token: 0x06004BB5 RID: 19381 RVA: 0x001132FD File Offset: 0x001114FD
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x00113318 File Offset: 0x00111518
		internal static Type FormCompoundType(char[] bFormat, Type baseType, int curIndex)
		{
			if (bFormat == null || curIndex == bFormat.Length)
			{
				return baseType;
			}
			if (bFormat[curIndex] == '&')
			{
				SymbolType symbolType = new SymbolType(TypeKind.IsByRef);
				symbolType.SetFormat(bFormat, curIndex, 1);
				curIndex++;
				if (curIndex != bFormat.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
				}
				symbolType.SetElementType(baseType);
				return symbolType;
			}
			else
			{
				if (bFormat[curIndex] == '[')
				{
					SymbolType symbolType = new SymbolType(TypeKind.IsArray);
					int num = curIndex;
					curIndex++;
					int num2 = 0;
					int num3 = -1;
					while (bFormat[curIndex] != ']')
					{
						if (bFormat[curIndex] == '*')
						{
							symbolType.m_isSzArray = false;
							curIndex++;
						}
						if ((bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9') || bFormat[curIndex] == '-')
						{
							bool flag = false;
							if (bFormat[curIndex] == '-')
							{
								flag = true;
								curIndex++;
							}
							while (bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9')
							{
								num2 *= 10;
								num2 += (int)(bFormat[curIndex] - '0');
								curIndex++;
							}
							if (flag)
							{
								num2 = 0 - num2;
							}
							num3 = num2 - 1;
						}
						if (bFormat[curIndex] == '.')
						{
							curIndex++;
							if (bFormat[curIndex] != '.')
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
							}
							curIndex++;
							if ((bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9') || bFormat[curIndex] == '-')
							{
								bool flag2 = false;
								num3 = 0;
								if (bFormat[curIndex] == '-')
								{
									flag2 = true;
									curIndex++;
								}
								while (bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9')
								{
									num3 *= 10;
									num3 += (int)(bFormat[curIndex] - '0');
									curIndex++;
								}
								if (flag2)
								{
									num3 = 0 - num3;
								}
								if (num3 < num2)
								{
									throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
								}
							}
						}
						if (bFormat[curIndex] == ',')
						{
							curIndex++;
							symbolType.SetBounds(num2, num3);
							num2 = 0;
							num3 = -1;
						}
						else if (bFormat[curIndex] != ']')
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
						}
					}
					symbolType.SetBounds(num2, num3);
					curIndex++;
					symbolType.SetFormat(bFormat, num, curIndex - num);
					symbolType.SetElementType(baseType);
					return SymbolType.FormCompoundType(bFormat, symbolType, curIndex);
				}
				if (bFormat[curIndex] == '*')
				{
					SymbolType symbolType = new SymbolType(TypeKind.IsPointer);
					symbolType.SetFormat(bFormat, curIndex, 1);
					curIndex++;
					symbolType.SetElementType(baseType);
					return SymbolType.FormCompoundType(bFormat, symbolType, curIndex);
				}
				return null;
			}
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x00113518 File Offset: 0x00111718
		internal SymbolType(TypeKind typeKind)
		{
			this.m_typeKind = typeKind;
			this.m_iaLowerBound = new int[4];
			this.m_iaUpperBound = new int[4];
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x00113546 File Offset: 0x00111746
		internal void SetElementType(Type baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			this.m_baseType = baseType;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x00113564 File Offset: 0x00111764
		private void SetBounds(int lower, int upper)
		{
			if (lower != 0 || upper != -1)
			{
				this.m_isSzArray = false;
			}
			if (this.m_iaLowerBound.Length <= this.m_cRank)
			{
				int[] array = new int[this.m_cRank * 2];
				Array.Copy(this.m_iaLowerBound, array, this.m_cRank);
				this.m_iaLowerBound = array;
				Array.Copy(this.m_iaUpperBound, array, this.m_cRank);
				this.m_iaUpperBound = array;
			}
			this.m_iaLowerBound[this.m_cRank] = lower;
			this.m_iaUpperBound[this.m_cRank] = upper;
			this.m_cRank++;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x001135FC File Offset: 0x001117FC
		internal void SetFormat(char[] bFormat, int curIndex, int length)
		{
			char[] array = new char[length];
			Array.Copy(bFormat, curIndex, array, 0, length);
			this.m_bFormat = array;
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004BBB RID: 19387 RVA: 0x00113621 File Offset: 0x00111821
		internal override bool IsSzArray
		{
			get
			{
				return this.m_cRank <= 1 && this.m_isSzArray;
			}
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x00113634 File Offset: 0x00111834
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + "*").ToCharArray(), this.m_baseType, 0);
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x0011365C File Offset: 0x0011185C
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + "&").ToCharArray(), this.m_baseType, 0);
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x00113684 File Offset: 0x00111884
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + "[]").ToCharArray(), this.m_baseType, 0);
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x001136AC File Offset: 0x001118AC
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			if (rank == 1)
			{
				text = "*";
			}
			else
			{
				for (int i = 1; i < rank; i++)
				{
					text += ",";
				}
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + text2).ToCharArray(), this.m_baseType, 0) as SymbolType;
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x00113727 File Offset: 0x00111927
		public override int GetArrayRank()
		{
			if (!base.IsArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
			}
			return this.m_cRank;
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x00113747 File Offset: 0x00111947
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
			}
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x00113758 File Offset: 0x00111958
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x0011376C File Offset: 0x0011196C
		public override Module Module
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Module;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x0011379C File Offset: 0x0011199C
		public override Assembly Assembly
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Assembly;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x001137CC File Offset: 0x001119CC
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x001137E0 File Offset: 0x001119E0
		public override string Name
		{
			get
			{
				string text = new string(this.m_bFormat);
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					text = new string(((SymbolType)type).m_bFormat) + text;
					type = ((SymbolType)type).m_baseType;
				}
				return type.Name + text;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x00113839 File Offset: 0x00111A39
		public override string FullName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x00113842 File Offset: 0x00111A42
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0011384B File Offset: 0x00111A4B
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004BCA RID: 19402 RVA: 0x00113854 File Offset: 0x00111A54
		public override string Namespace
		{
			get
			{
				return this.m_baseType.Namespace;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004BCB RID: 19403 RVA: 0x00113861 File Offset: 0x00111A61
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x0011386D File Offset: 0x00111A6D
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0011387E File Offset: 0x00111A7E
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0011388F File Offset: 0x00111A8F
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x001138A0 File Offset: 0x00111AA0
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x001138B1 File Offset: 0x00111AB1
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x001138C2 File Offset: 0x00111AC2
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x001138D3 File Offset: 0x00111AD3
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x001138E4 File Offset: 0x00111AE4
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x001138F5 File Offset: 0x00111AF5
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00113906 File Offset: 0x00111B06
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00113917 File Offset: 0x00111B17
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00113928 File Offset: 0x00111B28
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x00113939 File Offset: 0x00111B39
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x0011394A File Offset: 0x00111B4A
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x0011395B File Offset: 0x00111B5B
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x0011396C File Offset: 0x00111B6C
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0011397D File Offset: 0x00111B7D
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0011398E File Offset: 0x00111B8E
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x001139A0 File Offset: 0x00111BA0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			Type type = this.m_baseType;
			while (type is SymbolType)
			{
				type = ((SymbolType)type).m_baseType;
			}
			return type.Attributes;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x001139D0 File Offset: 0x00111BD0
		protected override bool IsArrayImpl()
		{
			return this.m_typeKind == TypeKind.IsArray;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x001139DB File Offset: 0x00111BDB
		protected override bool IsPointerImpl()
		{
			return this.m_typeKind == TypeKind.IsPointer;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x001139E6 File Offset: 0x00111BE6
		protected override bool IsByRefImpl()
		{
			return this.m_typeKind == TypeKind.IsByRef;
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x001139F1 File Offset: 0x00111BF1
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x001139F4 File Offset: 0x00111BF4
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x001139F7 File Offset: 0x00111BF7
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x001139FA File Offset: 0x00111BFA
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x001139FD File Offset: 0x00111BFD
		public override Type GetElementType()
		{
			return this.m_baseType;
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00113A05 File Offset: 0x00111C05
		protected override bool HasElementTypeImpl()
		{
			return this.m_baseType != null;
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x00113A13 File Offset: 0x00111C13
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00113A16 File Offset: 0x00111C16
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00113A27 File Offset: 0x00111C27
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x00113A38 File Offset: 0x00111C38
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x04001F47 RID: 8007
		internal TypeKind m_typeKind;

		// Token: 0x04001F48 RID: 8008
		internal Type m_baseType;

		// Token: 0x04001F49 RID: 8009
		internal int m_cRank;

		// Token: 0x04001F4A RID: 8010
		internal int[] m_iaLowerBound;

		// Token: 0x04001F4B RID: 8011
		internal int[] m_iaUpperBound;

		// Token: 0x04001F4C RID: 8012
		private char[] m_bFormat;

		// Token: 0x04001F4D RID: 8013
		private bool m_isSzArray = true;
	}
}
