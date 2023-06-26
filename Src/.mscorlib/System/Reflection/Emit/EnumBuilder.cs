using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	/// <summary>Describes and represents an enumeration type.</summary>
	// Token: 0x02000664 RID: 1636
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EnumBuilder : TypeInfo, _EnumBuilder
	{
		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Reflection.TypeInfo" /> object can be assigned to this object.</summary>
		/// <param name="typeInfo">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="typeInfo" /> can be assigned to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E8D RID: 20109 RVA: 0x0011CFA8 File Offset: 0x0011B1A8
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		/// <summary>Defines the named static field in an enumeration type with the specified constant value.</summary>
		/// <param name="literalName">The name of the static field.</param>
		/// <param name="literalValue">The constant value of the literal.</param>
		/// <returns>The defined field.</returns>
		// Token: 0x06004E8E RID: 20110 RVA: 0x0011CFC4 File Offset: 0x0011B1C4
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this.m_typeBuilder.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.TypeInfo" /> object that represents this enumeration.</summary>
		/// <returns>An object that represents this enumeration.</returns>
		// Token: 0x06004E8F RID: 20111 RVA: 0x0011CFE9 File Offset: 0x0011B1E9
		public TypeInfo CreateTypeInfo()
		{
			return this.m_typeBuilder.CreateTypeInfo();
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object for this enum.</summary>
		/// <returns>A <see cref="T:System.Type" /> object for this enum.</returns>
		/// <exception cref="T:System.InvalidOperationException">This type has been previously created.  
		///  -or-  
		///  The enclosing type has not been created.</exception>
		// Token: 0x06004E90 RID: 20112 RVA: 0x0011CFF6 File Offset: 0x0011B1F6
		public Type CreateType()
		{
			return this.m_typeBuilder.CreateType();
		}

		/// <summary>Returns the internal metadata type token of this enum.</summary>
		/// <returns>Read-only. The type token of this enum.</returns>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x0011D003 File Offset: 0x0011B203
		public TypeToken TypeToken
		{
			get
			{
				return this.m_typeBuilder.TypeToken;
			}
		}

		/// <summary>Returns the underlying field for this enum.</summary>
		/// <returns>Read-only. The underlying field for this enum.</returns>
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x0011D010 File Offset: 0x0011B210
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this.m_underlyingField;
			}
		}

		/// <summary>Returns the name of this enum.</summary>
		/// <returns>Read-only. The name of this enum.</returns>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x0011D018 File Offset: 0x0011B218
		public override string Name
		{
			get
			{
				return this.m_typeBuilder.Name;
			}
		}

		/// <summary>Returns the GUID of this enum.</summary>
		/// <returns>Read-only. The GUID of this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x0011D025 File Offset: 0x0011B225
		public override Guid GUID
		{
			get
			{
				return this.m_typeBuilder.GUID;
			}
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the contraints of the specified binder and invocation attributes.</summary>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If binder is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored.</param>
		/// <param name="args">An argument list. This is an array of objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null.</param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for details such as this.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is null, the <see langword="CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a string that represents 1000 to a double value, since 1000 is represented differently by different cultures.)</param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order.</param>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004E95 RID: 20117 RVA: 0x0011D034 File Offset: 0x0011B234
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.m_typeBuilder.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		/// <summary>Retrieves the dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</summary>
		/// <returns>Read-only. The dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</returns>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004E96 RID: 20118 RVA: 0x0011D059 File Offset: 0x0011B259
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		/// <summary>Retrieves the dynamic assembly that contains this enum definition.</summary>
		/// <returns>Read-only. The dynamic assembly that contains this enum definition.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x0011D066 File Offset: 0x0011B266
		public override Assembly Assembly
		{
			get
			{
				return this.m_typeBuilder.Assembly;
			}
		}

		/// <summary>Retrieves the internal handle for this enum.</summary>
		/// <returns>Read-only. The internal handle for this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not currently supported.</exception>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x0011D073 File Offset: 0x0011B273
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.m_typeBuilder.TypeHandle;
			}
		}

		/// <summary>Returns the full path of this enum.</summary>
		/// <returns>Read-only. The full path of this enum.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x0011D080 File Offset: 0x0011B280
		public override string FullName
		{
			get
			{
				return this.m_typeBuilder.FullName;
			}
		}

		/// <summary>Returns the full path of this enum qualified by the display name of the parent assembly.</summary>
		/// <returns>Read-only. The full path of this enum qualified by the display name of the parent assembly.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004E9A RID: 20122 RVA: 0x0011D08D File Offset: 0x0011B28D
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.m_typeBuilder.AssemblyQualifiedName;
			}
		}

		/// <summary>Returns the namespace of this enum.</summary>
		/// <returns>Read-only. The namespace of this enum.</returns>
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004E9B RID: 20123 RVA: 0x0011D09A File Offset: 0x0011B29A
		public override string Namespace
		{
			get
			{
				return this.m_typeBuilder.Namespace;
			}
		}

		/// <summary>Returns the parent <see cref="T:System.Type" /> of this type which is always <see cref="T:System.Enum" />.</summary>
		/// <returns>Read-only. The parent <see cref="T:System.Type" /> of this type.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004E9C RID: 20124 RVA: 0x0011D0A7 File Offset: 0x0011B2A7
		public override Type BaseType
		{
			get
			{
				return this.m_typeBuilder.BaseType;
			}
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0011D0B4 File Offset: 0x0011B2B4
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.m_typeBuilder.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004E9E RID: 20126 RVA: 0x0011D0C8 File Offset: 0x0011B2C8
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetConstructors(bindingAttr);
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0011D0D6 File Offset: 0x0011B2D6
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.m_typeBuilder.GetMethod(name, bindingAttr);
			}
			return this.m_typeBuilder.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA0 RID: 20128 RVA: 0x0011D0FE File Offset: 0x0011B2FE
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMethods(bindingAttr);
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <param name="name">The name of the field to get.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches, then null is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA1 RID: 20129 RVA: 0x0011D10C File Offset: 0x0011B30C
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetField(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA2 RID: 20130 RVA: 0x0011D11B File Offset: 0x0011B31B
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetFields(bindingAttr);
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this type, with the specified fully-qualified name.</summary>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">If <see langword="true" />, the search is case-insensitive. If <see langword="false" />, the search is case-sensitive.</param>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA3 RID: 20131 RVA: 0x0011D129 File Offset: 0x0011B329
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.m_typeBuilder.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an array of all the interfaces implemented on this a class and its base classes.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x06004EA4 RID: 20132 RVA: 0x0011D138 File Offset: 0x0011B338
		public override Type[] GetInterfaces()
		{
			return this.m_typeBuilder.GetInterfaces();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <param name="name">The name of the event to get.</param>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name. If there are no matches, <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA5 RID: 20133 RVA: 0x0011D145 File Offset: 0x0011B345
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the events for the public events declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public events declared or inherited by this type. An empty array is returned if there are no public events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA6 RID: 20134 RVA: 0x0011D154 File Offset: 0x0011B354
		public override EventInfo[] GetEvents()
		{
			return this.m_typeBuilder.GetEvents();
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0011D161 File Offset: 0x0011B361
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA8 RID: 20136 RVA: 0x0011D172 File Offset: 0x0011B372
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetProperties(bindingAttr);
		}

		/// <summary>Returns the public and non-public nested types that are declared or inherited by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />, or if none of the nested types match the binding constraints.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EA9 RID: 20137 RVA: 0x0011D180 File Offset: 0x0011B380
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedTypes(bindingAttr);
		}

		/// <summary>Returns the specified nested type that is declared by this type.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to conduct a case-sensitive search for public methods.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EAA RID: 20138 RVA: 0x0011D18E File Offset: 0x0011B38E
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns all members with the specified name, type, and binding that are declared or inherited by this type.</summary>
		/// <param name="name">The name of the member.</param>
		/// <param name="type">The type of member that is to be returned.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EAB RID: 20139 RVA: 0x0011D19D File Offset: 0x0011B39D
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns the specified members declared or inherited by this type,.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EAC RID: 20140 RVA: 0x0011D1AD File Offset: 0x0011B3AD
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMembers(bindingAttr);
		}

		/// <summary>Returns an interface mapping for the interface requested.</summary>
		/// <param name="interfaceType">The type of the interface for which the interface mapping is to be retrieved.</param>
		/// <returns>The requested interface mapping.</returns>
		/// <exception cref="T:System.ArgumentException">The type does not implement the interface.</exception>
		// Token: 0x06004EAD RID: 20141 RVA: 0x0011D1BB File Offset: 0x0011B3BB
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.m_typeBuilder.GetInterfaceMap(interfaceType);
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public and non-public events declared or inherited by this type. An empty array is returned if there are no events, as specified.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EAE RID: 20142 RVA: 0x0011D1C9 File Offset: 0x0011B3C9
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvents(bindingAttr);
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x0011D1D7 File Offset: 0x0011B3D7
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_typeBuilder.Attributes;
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x0011D1E4 File Offset: 0x0011B3E4
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x0011D1E7 File Offset: 0x0011B3E7
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0011D1EA File Offset: 0x0011B3EA
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x0011D1ED File Offset: 0x0011B3ED
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x0011D1F0 File Offset: 0x0011B3F0
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x0011D1F3 File Offset: 0x0011B3F3
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if this object represents a constructed generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x0011D1F6 File Offset: 0x0011B3F6
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		// Token: 0x06004EB7 RID: 20151 RVA: 0x0011D1F9 File Offset: 0x0011B3F9
		public override Type GetElementType()
		{
			return this.m_typeBuilder.GetElementType();
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0011D206 File Offset: 0x0011B406
		protected override bool HasElementTypeImpl()
		{
			return this.m_typeBuilder.HasElementType;
		}

		/// <summary>Returns the underlying integer type of the current enumeration, which is set when the enumeration builder is defined.</summary>
		/// <returns>The underlying type.</returns>
		// Token: 0x06004EB9 RID: 20153 RVA: 0x0011D213 File Offset: 0x0011B413
		public override Type GetEnumUnderlyingType()
		{
			return this.m_underlyingField.FieldType;
		}

		/// <summary>Returns the underlying system type for this enum.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004EBA RID: 20154 RVA: 0x0011D220 File Offset: 0x0011B420
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.GetEnumUnderlyingType();
			}
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Returns an array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EBB RID: 20155 RVA: 0x0011D228 File Offset: 0x0011B428
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(inherit);
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to which the custom attributes are applied.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Returns an array of objects representing the attributes of this constructor that are of <see cref="T:System.Type" /><paramref name="attributeType" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EBC RID: 20156 RVA: 0x0011D236 File Offset: 0x0011B436
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		// Token: 0x06004EBD RID: 20157 RVA: 0x0011D245 File Offset: 0x0011B445
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_typeBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.</exception>
		// Token: 0x06004EBE RID: 20158 RVA: 0x0011D254 File Offset: 0x0011B454
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_typeBuilder.SetCustomAttribute(customBuilder);
		}

		/// <summary>Returns the type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x0011D262 File Offset: 0x0011B462
		public override Type DeclaringType
		{
			get
			{
				return this.m_typeBuilder.DeclaringType;
			}
		}

		/// <summary>Returns the type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004EC0 RID: 20160 RVA: 0x0011D26F File Offset: 0x0011B46F
		public override Type ReflectedType
		{
			get
			{
				return this.m_typeBuilder.ReflectedType;
			}
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to which the custom attributes are applied.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instance of <paramref name="attributeType" /> is defined on this member; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004EC1 RID: 20161 RVA: 0x0011D27C File Offset: 0x0011B47C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x0011D28B File Offset: 0x0011B48B
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_typeBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x0011D298 File Offset: 0x0011B498
		private EnumBuilder()
		{
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current type.</returns>
		// Token: 0x06004EC4 RID: 20164 RVA: 0x0011D2A0 File Offset: 0x0011B4A0
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</returns>
		// Token: 0x06004EC5 RID: 20165 RVA: 0x0011D2B3 File Offset: 0x0011B4B3
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
		// Token: 0x06004EC6 RID: 20166 RVA: 0x0011D2C6 File Offset: 0x0011B4C6
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing an array of the current type, with the specified number of dimensions.</summary>
		/// <param name="rank">The number of dimensions for the array. This number must be less than or equal to 32.</param>
		/// <returns>An object representing an array of the current type, with the specified number of dimensions.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is less than 1.</exception>
		// Token: 0x06004EC7 RID: 20167 RVA: 0x0011D2DC File Offset: 0x0011B4DC
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
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x0011D33C File Offset: 0x0011B53C
		[SecurityCritical]
		internal EnumBuilder(string name, Type underlyingType, TypeAttributes visibility, ModuleBuilder module)
		{
			if ((visibility & ~TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ShouldOnlySetVisibilityFlags"), "name");
			}
			this.m_typeBuilder = new TypeBuilder(name, visibility | TypeAttributes.Sealed, typeof(Enum), null, module, PackingSize.Unspecified, 0, null);
			this.m_underlyingField = this.m_typeBuilder.DefineField("value__", underlyingType, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004EC9 RID: 20169 RVA: 0x0011D3A9 File Offset: 0x0011B5A9
		void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004ECA RID: 20170 RVA: 0x0011D3B0 File Offset: 0x0011B5B0
		void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004ECB RID: 20171 RVA: 0x0011D3B7 File Offset: 0x0011B5B7
		void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004ECC RID: 20172 RVA: 0x0011D3BE File Offset: 0x0011B5BE
		void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040021D9 RID: 8665
		internal TypeBuilder m_typeBuilder;

		// Token: 0x040021DA RID: 8666
		private FieldBuilder m_underlyingField;
	}
}
