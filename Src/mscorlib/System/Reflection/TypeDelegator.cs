using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Wraps a <see cref="T:System.Type" /> object and delegates methods to that <see langword="Type" />.</summary>
	// Token: 0x02000623 RID: 1571
	[ComVisible(true)]
	[Serializable]
	public class TypeDelegator : TypeInfo
	{
		/// <summary>Returns a value that indicates whether the specified type can be assigned to this type.</summary>
		/// <param name="typeInfo">The type to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified type can be assigned to this type; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048D7 RID: 18647 RVA: 0x0010918B File Offset: 0x0010738B
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TypeDelegator" /> class with default properties.</summary>
		// Token: 0x060048D8 RID: 18648 RVA: 0x001091A4 File Offset: 0x001073A4
		protected TypeDelegator()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TypeDelegator" /> class specifying the encapsulating instance.</summary>
		/// <param name="delegatingType">The instance of the class <see cref="T:System.Type" /> that encapsulates the call to the method of an object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="delegatingType" /> is <see langword="null" />.</exception>
		// Token: 0x060048D9 RID: 18649 RVA: 0x001091AC File Offset: 0x001073AC
		public TypeDelegator(Type delegatingType)
		{
			if (delegatingType == null)
			{
				throw new ArgumentNullException("delegatingType");
			}
			this.typeImpl = delegatingType;
		}

		/// <summary>Gets the GUID (globally unique identifier) of the implemented type.</summary>
		/// <returns>A GUID.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060048DA RID: 18650 RVA: 0x001091CF File Offset: 0x001073CF
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		/// <summary>Gets a value that identifies this entity in metadata.</summary>
		/// <returns>A value which, in combination with the module, uniquely identifies this entity in metadata.</returns>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x001091DC File Offset: 0x001073DC
		public override int MetadataToken
		{
			get
			{
				return this.typeImpl.MetadataToken;
			}
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the constraints of the specified binder and invocation attributes.</summary>
		/// <param name="name">The name of the member to invoke. This may be a constructor, method, property, or field. If an empty string ("") is passed, the default member is invoked.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be one of the following <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. A suitable invocation attribute must be specified. If a static member is to be invoked, the <see langword="Static" /> flag must be set.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="target">The object on which to invoke the specified member.</param>
		/// <param name="args">An array of type <see langword="Object" /> that contains the number, order, and type of the parameters of the member to be invoked. If <paramref name="args" /> contains an uninitialized <see langword="Object" />, it is treated as empty, which, with the default binder, can be widened to 0, 0.0 or a string.</param>
		/// <param name="modifiers">An array of type <see langword="ParameterModifer" /> that is the same length as <paramref name="args" />, with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the member's signature. For ByRef, use <see langword="ParameterModifer.ByRef" />, and for none, use <see langword="ParameterModifer.None" />. The default binder does exact matching on these. Attributes such as <see langword="In" /> and <see langword="InOut" /> are not used in binding, and can be viewed using <see langword="ParameterInfo" />.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. This is necessary, for example, to convert a string that represents 1000 to a <see langword="Double" /> value, since 1000 is represented differently by different cultures. If <paramref name="culture" /> is <see langword="null" />, the <see langword="CultureInfo" /> for the current thread's <see langword="CultureInfo" /> is used.</param>
		/// <param name="namedParameters">An array of type <see langword="String" /> containing parameter names that match up, starting at element zero, with the <paramref name="args" /> array. There must be no holes in the array. If <paramref name="args" />. <see langword="Length" /> is greater than <paramref name="namedParameters" />. <see langword="Length" />, the remaining parameters are filled in order.</param>
		/// <returns>An <see langword="Object" /> representing the return value of the invoked member.</returns>
		// Token: 0x060048DC RID: 18652 RVA: 0x001091EC File Offset: 0x001073EC
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		/// <summary>Gets the module that contains the implemented type.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object representing the module of the implemented type.</returns>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060048DD RID: 18653 RVA: 0x00109211 File Offset: 0x00107411
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		/// <summary>Gets the assembly of the implemented type.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the assembly of the implemented type.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x0010921E File Offset: 0x0010741E
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		/// <summary>Gets a handle to the internal metadata representation of an implemented type.</summary>
		/// <returns>A <see langword="RuntimeTypeHandle" /> object.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x0010922B File Offset: 0x0010742B
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		/// <summary>Gets the name of the implemented type, with the path removed.</summary>
		/// <returns>A <see langword="String" /> containing the type's non-qualified name.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x00109238 File Offset: 0x00107438
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		/// <summary>Gets the fully qualified name of the implemented type.</summary>
		/// <returns>A <see langword="String" /> containing the type's fully qualified name.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060048E1 RID: 18657 RVA: 0x00109245 File Offset: 0x00107445
		public override string FullName
		{
			get
			{
				return this.typeImpl.FullName;
			}
		}

		/// <summary>Gets the namespace of the implemented type.</summary>
		/// <returns>A <see langword="String" /> containing the type's namespace.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x00109252 File Offset: 0x00107452
		public override string Namespace
		{
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		/// <summary>Gets the assembly's fully qualified name.</summary>
		/// <returns>A <see langword="String" /> containing the assembly's fully qualified name.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x0010925F File Offset: 0x0010745F
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		/// <summary>Gets the base type for the current type.</summary>
		/// <returns>The base type for a type.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x0010926C File Offset: 0x0010746C
		public override Type BaseType
		{
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		/// <summary>Gets the constructor that implemented the <see langword="TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="callConvention">The calling conventions.</param>
		/// <param name="types">An array of type <see langword="Type" /> containing a list of the parameter number, order, and types. Types cannot be <see langword="null" />; use an appropriate <see langword="GetMethod" /> method or an empty array to search for a method without parameters.</param>
		/// <param name="modifiers">An array of type <see langword="ParameterModifier" /> having the same length as the <paramref name="types" /> array, whose elements represent the attributes associated with the parameters of the method to get.</param>
		/// <returns>A <see langword="ConstructorInfo" /> object for the method that matches the specified criteria, or <see langword="null" /> if a match cannot be found.</returns>
		// Token: 0x060048E5 RID: 18661 RVA: 0x00109279 File Offset: 0x00107479
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing constructors defined for the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="ConstructorInfo" /> containing the specified constructors defined for this class. If no constructors are defined, an empty array is returned. Depending on the value of a specified parameter, only public constructors or both public and non-public constructors will be returned.</returns>
		// Token: 0x060048E6 RID: 18662 RVA: 0x0010928D File Offset: 0x0010748D
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <param name="name">The method name.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="callConvention">The calling conventions.</param>
		/// <param name="types">An array of type <see langword="Type" /> containing a list of the parameter number, order, and types. Types cannot be <see langword="null" />; use an appropriate <see langword="GetMethod" /> method or an empty array to search for a method without parameters.</param>
		/// <param name="modifiers">An array of type <see langword="ParameterModifier" /> having the same length as the <paramref name="types" /> array, whose elements represent the attributes associated with the parameters of the method to get.</param>
		/// <returns>A <see langword="MethodInfoInfo" /> object for the implementation method that matches the specified criteria, or <see langword="null" /> if a match cannot be found.</returns>
		// Token: 0x060048E7 RID: 18663 RVA: 0x0010929B File Offset: 0x0010749B
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.typeImpl.GetMethod(name, bindingAttr);
			}
			return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing specified methods of the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of <see langword="MethodInfo" /> objects representing the methods defined on this <see langword="TypeDelegator" />.</returns>
		// Token: 0x060048E8 RID: 18664 RVA: 0x001092C3 File Offset: 0x001074C3
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		/// <summary>Returns a <see cref="T:System.Reflection.FieldInfo" /> object representing the field with the specified name.</summary>
		/// <param name="name">The name of the field to find.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>A <see langword="FieldInfo" /> object representing the field declared or inherited by this <see langword="TypeDelegator" /> with the specified name. Returns <see langword="null" /> if no such field is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060048E9 RID: 18665 RVA: 0x001092D1 File Offset: 0x001074D1
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the data fields defined for the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="FieldInfo" /> containing the fields declared or inherited by the current <see langword="TypeDelegator" />. An empty array is returned if there are no matched fields.</returns>
		// Token: 0x060048EA RID: 18666 RVA: 0x001092E0 File Offset: 0x001074E0
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		/// <summary>Returns the specified interface implemented by the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="name">The fully qualified name of the interface implemented by the current class.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> if the case is to be ignored; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="Type" /> object representing the interface implemented (directly or indirectly) by the current class with the fully qualified name matching the specified name. If no interface that matches name is found, null is returned.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060048EB RID: 18667 RVA: 0x001092EE File Offset: 0x001074EE
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.typeImpl.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns all the interfaces implemented on the current class and its base classes.</summary>
		/// <returns>An array of type <see langword="Type" /> containing all the interfaces implemented on the current class and its base classes. If none are defined, an empty array is returned.</returns>
		// Token: 0x060048EC RID: 18668 RVA: 0x001092FD File Offset: 0x001074FD
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		/// <summary>Returns the specified event.</summary>
		/// <param name="name">The name of the event to get.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name. This method returns <see langword="null" /> if no such event is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060048ED RID: 18669 RVA: 0x0010930A File Offset: 0x0010750A
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing all the public events declared or inherited by the current <see langword="TypeDelegator" />.</summary>
		/// <returns>An array that contains all the events declared or inherited by the current type. If there are no events, an empty array is returned.</returns>
		// Token: 0x060048EE RID: 18670 RVA: 0x00109319 File Offset: 0x00107519
		public override EventInfo[] GetEvents()
		{
			return this.typeImpl.GetEvents();
		}

		/// <summary>When overridden in a derived class, searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <param name="name">The property to get.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">A list of parameter types. The list represents the number, order, and types of the parameters. Types cannot be null; use an appropriate <see langword="GetMethod" /> method or an empty array to search for a method without parameters.</param>
		/// <param name="modifiers">An array of the same length as types with elements that represent the attributes associated with the parameters of the method to get.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the property that matches the specified criteria, or null if a match cannot be found.</returns>
		// Token: 0x060048EF RID: 18671 RVA: 0x00109326 File Offset: 0x00107526
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (returnType == null && types == null)
			{
				return this.typeImpl.GetProperty(name, bindingAttr);
			}
			return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing properties of the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of <see langword="PropertyInfo" /> objects representing properties defined on this <see langword="TypeDelegator" />.</returns>
		// Token: 0x060048F0 RID: 18672 RVA: 0x00109358 File Offset: 0x00107558
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		/// <summary>Returns the events specified in <paramref name="bindingAttr" /> that are declared or inherited by the current <see langword="TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="EventInfo" /> containing the events specified in <paramref name="bindingAttr" />. If there are no events, an empty array is returned.</returns>
		// Token: 0x060048F1 RID: 18673 RVA: 0x00109366 File Offset: 0x00107566
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		/// <summary>Returns the nested types specified in <paramref name="bindingAttr" /> that are declared or inherited by the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="Type" /> containing the nested types.</returns>
		// Token: 0x060048F2 RID: 18674 RVA: 0x00109374 File Offset: 0x00107574
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedTypes(bindingAttr);
		}

		/// <summary>Returns a nested type specified by <paramref name="name" /> and in <paramref name="bindingAttr" /> that are declared or inherited by the type represented by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="name">The nested type's name.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>A <see langword="Type" /> object representing the nested type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060048F3 RID: 18675 RVA: 0x00109382 File Offset: 0x00107582
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns members (properties, methods, constructors, fields, events, and nested types) specified by the given <paramref name="name" />, <paramref name="type" />, and <paramref name="bindingAttr" />.</summary>
		/// <param name="name">The name of the member to get.</param>
		/// <param name="type">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="bindingAttr">The type of members to get.</param>
		/// <returns>An array of type <see langword="MemberInfo" /> containing all the members of the current class and its base class meeting the specified criteria.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060048F4 RID: 18676 RVA: 0x00109391 File Offset: 0x00107591
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns members specified by <paramref name="bindingAttr" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="MemberInfo" /> containing all the members of the current class and its base classes that meet the <paramref name="bindingAttr" /> filter.</returns>
		// Token: 0x060048F5 RID: 18677 RVA: 0x001093A1 File Offset: 0x001075A1
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMembers(bindingAttr);
		}

		/// <summary>Gets the attributes assigned to the <see langword="TypeDelegator" />.</summary>
		/// <returns>A <see langword="TypeAttributes" /> object representing the implementation attribute flags.</returns>
		// Token: 0x060048F6 RID: 18678 RVA: 0x001093AF File Offset: 0x001075AF
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048F7 RID: 18679 RVA: 0x001093BC File Offset: 0x001075BC
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048F8 RID: 18680 RVA: 0x001093C9 File Offset: 0x001075C9
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048F9 RID: 18681 RVA: 0x001093D6 File Offset: 0x001075D6
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a pointer; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048FA RID: 18682 RVA: 0x001093E3 File Offset: 0x001075E3
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		/// <summary>Returns a value that indicates whether the type is a value type; that is, not a class or an interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is a value type; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048FB RID: 18683 RVA: 0x001093F0 File Offset: 0x001075F0
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a COM object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048FC RID: 18684 RVA: 0x001093FD File Offset: 0x001075FD
		protected override bool IsCOMObjectImpl()
		{
			return this.typeImpl.IsCOMObject;
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if this object represents a constructed generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x0010940A File Offset: 0x0010760A
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.typeImpl.IsConstructedGenericType;
			}
		}

		/// <summary>Returns the <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or ByRef.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or <see langword="ByRef" />, or <see langword="null" /> if the current <see cref="T:System.Type" /> is not an array, a pointer or a <see langword="ByRef" />.</returns>
		// Token: 0x060048FE RID: 18686 RVA: 0x00109417 File Offset: 0x00107617
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer or a ByRef.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array, a pointer or a ByRef; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048FF RID: 18687 RVA: 0x00109424 File Offset: 0x00107624
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		/// <summary>Gets the underlying <see cref="T:System.Type" /> that represents the implemented type.</summary>
		/// <returns>The underlying type.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06004900 RID: 18688 RVA: 0x00109431 File Offset: 0x00107631
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		/// <summary>Returns all the custom attributes defined for this type, specifying whether to search the type's inheritance chain.</summary>
		/// <param name="inherit">Specifies whether to search this type's inheritance chain to find the attributes.</param>
		/// <returns>An array of objects containing all the custom attributes defined for this type.</returns>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x06004901 RID: 18689 RVA: 0x0010943E File Offset: 0x0010763E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		/// <summary>Returns an array of custom attributes identified by type.</summary>
		/// <param name="attributeType">An array of custom attributes identified by type.</param>
		/// <param name="inherit">Specifies whether to search this type's inheritance chain to find the attributes.</param>
		/// <returns>An array of objects containing the custom attributes defined in this type that match the <paramref name="attributeType" /> parameter, specifying whether to search the type's inheritance chain, or <see langword="null" /> if no custom attributes are defined on this type.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x06004902 RID: 18690 RVA: 0x0010944C File Offset: 0x0010764C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Indicates whether a custom attribute identified by <paramref name="attributeType" /> is defined.</summary>
		/// <param name="attributeType">Specifies whether to search this type's inheritance chain to find the attributes.</param>
		/// <param name="inherit">An array of custom attributes identified by type.</param>
		/// <returns>
		///   <see langword="true" /> if a custom attribute identified by <paramref name="attributeType" /> is defined; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The custom attribute type cannot be loaded.</exception>
		// Token: 0x06004903 RID: 18691 RVA: 0x0010945B File Offset: 0x0010765B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		/// <summary>Returns an interface mapping for the specified interface type.</summary>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface to retrieve a mapping of.</param>
		/// <returns>An <see cref="T:System.Reflection.InterfaceMapping" /> object representing the interface mapping for <paramref name="interfaceType" />.</returns>
		// Token: 0x06004904 RID: 18692 RVA: 0x0010946A File Offset: 0x0010766A
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.typeImpl.GetInterfaceMap(interfaceType);
		}

		/// <summary>A value indicating type information.</summary>
		// Token: 0x04001E49 RID: 7753
		protected Type typeImpl;
	}
}
