using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000662 RID: 1634
	internal sealed class TypeBuilderInstantiation : TypeInfo
	{
		// Token: 0x06004E06 RID: 19974 RVA: 0x0011C8E3 File Offset: 0x0011AAE3
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x0011C8FC File Offset: 0x0011AAFC
		internal static Type MakeGenericType(Type type, Type[] typeArguments)
		{
			if (!type.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException();
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			foreach (Type type2 in typeArguments)
			{
				if (type2 == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new TypeBuilderInstantiation(type, typeArguments);
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x0011C954 File Offset: 0x0011AB54
		private TypeBuilderInstantiation(Type type, Type[] inst)
		{
			this.m_type = type;
			this.m_inst = inst;
			this.m_hashtable = new Hashtable();
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x0011C980 File Offset: 0x0011AB80
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x0011C989 File Offset: 0x0011AB89
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x0011C996 File Offset: 0x0011AB96
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x0011C9A3 File Offset: 0x0011ABA3
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x0011C9B0 File Offset: 0x0011ABB0
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0011C9BD File Offset: 0x0011ABBD
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0011C9D0 File Offset: 0x0011ABD0
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x0011C9E3 File Offset: 0x0011ABE3
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x0011C9F8 File Offset: 0x0011ABF8
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			for (int i = 1; i < rank; i++)
			{
				text += ",";
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x0011CA4B File Offset: 0x0011AC4B
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x0011CA52 File Offset: 0x0011AC52
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004E14 RID: 19988 RVA: 0x0011CA59 File Offset: 0x0011AC59
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004E15 RID: 19989 RVA: 0x0011CA66 File Offset: 0x0011AC66
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x0011CA6D File Offset: 0x0011AC6D
		public override string FullName
		{
			get
			{
				if (this.m_strFullQualName == null)
				{
					this.m_strFullQualName = TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
				}
				return this.m_strFullQualName;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004E17 RID: 19991 RVA: 0x0011CA8A File Offset: 0x0011AC8A
		public override string Namespace
		{
			get
			{
				return this.m_type.Namespace;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x0011CA97 File Offset: 0x0011AC97
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0011CAA0 File Offset: 0x0011ACA0
		private Type Substitute(Type[] substitutes)
		{
			Type[] genericArguments = this.GetGenericArguments();
			Type[] array = new Type[genericArguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Type type = genericArguments[i];
				if (type is TypeBuilderInstantiation)
				{
					array[i] = (type as TypeBuilderInstantiation).Substitute(substitutes);
				}
				else if (type is GenericTypeParameterBuilder)
				{
					array[i] = substitutes[type.GenericParameterPosition];
				}
				else
				{
					array[i] = type;
				}
			}
			return this.GetGenericTypeDefinition().MakeGenericType(array);
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x0011CB10 File Offset: 0x0011AD10
		public override Type BaseType
		{
			get
			{
				Type baseType = this.m_type.BaseType;
				if (baseType == null)
				{
					return null;
				}
				TypeBuilderInstantiation typeBuilderInstantiation = baseType as TypeBuilderInstantiation;
				if (typeBuilderInstantiation == null)
				{
					return baseType;
				}
				return typeBuilderInstantiation.Substitute(this.GetGenericArguments());
			}
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x0011CB52 File Offset: 0x0011AD52
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x0011CB59 File Offset: 0x0011AD59
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x0011CB60 File Offset: 0x0011AD60
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0011CB67 File Offset: 0x0011AD67
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x0011CB6E File Offset: 0x0011AD6E
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x0011CB75 File Offset: 0x0011AD75
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x0011CB7C File Offset: 0x0011AD7C
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0011CB83 File Offset: 0x0011AD83
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x0011CB8A File Offset: 0x0011AD8A
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x0011CB91 File Offset: 0x0011AD91
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x0011CB98 File Offset: 0x0011AD98
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x0011CB9F File Offset: 0x0011AD9F
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x0011CBA6 File Offset: 0x0011ADA6
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x0011CBAD File Offset: 0x0011ADAD
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x0011CBB4 File Offset: 0x0011ADB4
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x0011CBBB File Offset: 0x0011ADBB
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x0011CBC2 File Offset: 0x0011ADC2
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x0011CBC9 File Offset: 0x0011ADC9
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x0011CBD0 File Offset: 0x0011ADD0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_type.Attributes;
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x0011CBDD File Offset: 0x0011ADDD
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x0011CBE0 File Offset: 0x0011ADE0
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x0011CBE3 File Offset: 0x0011ADE3
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x0011CBE6 File Offset: 0x0011ADE6
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x0011CBE9 File Offset: 0x0011ADE9
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x0011CBEC File Offset: 0x0011ADEC
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x0011CBF3 File Offset: 0x0011ADF3
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004E35 RID: 20021 RVA: 0x0011CBF6 File Offset: 0x0011ADF6
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0011CBF9 File Offset: 0x0011ADF9
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004E37 RID: 20023 RVA: 0x0011CC01 File Offset: 0x0011AE01
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x0011CC04 File Offset: 0x0011AE04
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x0011CC07 File Offset: 0x0011AE07
		public override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x0011CC0A File Offset: 0x0011AE0A
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x0011CC0D File Offset: 0x0011AE0D
		public override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x0011CC14 File Offset: 0x0011AE14
		protected override bool IsValueTypeImpl()
		{
			return this.m_type.IsValueType;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x0011CC24 File Offset: 0x0011AE24
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x0011CC56 File Offset: 0x0011AE56
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x0011CC59 File Offset: 0x0011AE59
		public override Type GetGenericTypeDefinition()
		{
			return this.m_type;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0011CC61 File Offset: 0x0011AE61
		public override Type MakeGenericType(params Type[] inst)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x0011CC72 File Offset: 0x0011AE72
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0011CC79 File Offset: 0x0011AE79
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x0011CC80 File Offset: 0x0011AE80
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x0011CC87 File Offset: 0x0011AE87
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x0011CC8E File Offset: 0x0011AE8E
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040021D4 RID: 8660
		private Type m_type;

		// Token: 0x040021D5 RID: 8661
		private Type[] m_inst;

		// Token: 0x040021D6 RID: 8662
		private string m_strFullQualName;

		// Token: 0x040021D7 RID: 8663
		internal Hashtable m_hashtable = new Hashtable();
	}
}
