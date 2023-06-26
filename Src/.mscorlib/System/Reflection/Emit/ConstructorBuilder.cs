using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a constructor of a dynamic class.</summary>
	// Token: 0x0200062E RID: 1582
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x060049C4 RID: 18884 RVA: 0x0010C377 File Offset: 0x0010A577
		private ConstructorBuilder()
		{
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0010C380 File Offset: 0x0010A580
		[SecurityCritical]
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers, ModuleBuilder mod, TypeBuilder type)
		{
			this.m_methodBuilder = new MethodBuilder(name, attributes, callingConvention, null, null, null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, mod, type, false);
			type.m_listMethods.Add(this.m_methodBuilder);
			int num;
			byte[] array = this.m_methodBuilder.GetMethodSignature().InternalGetSignature(out num);
			MethodToken token = this.m_methodBuilder.GetToken();
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0010C3E0 File Offset: 0x0010A5E0
		[SecurityCritical]
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type)
			: this(name, attributes, callingConvention, parameterTypes, null, null, mod, type)
		{
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x0010C3FE File Offset: 0x0010A5FE
		internal override Type[] GetParameterTypes()
		{
			return this.m_methodBuilder.GetParameterTypes();
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x0010C40B File Offset: 0x0010A60B
		private TypeBuilder GetTypeBuilder()
		{
			return this.m_methodBuilder.GetTypeBuilder();
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0010C418 File Offset: 0x0010A618
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.GetTypeBuilder().GetModuleBuilder();
		}

		/// <summary>Returns this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance as a <see cref="T:System.String" />.</summary>
		/// <returns>A string containing the name, attributes, and exceptions of this constructor, followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x060049CA RID: 18890 RVA: 0x0010C425 File Offset: 0x0010A625
		public override string ToString()
		{
			return this.m_methodBuilder.ToString();
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x0010C432 File Offset: 0x0010A632
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_methodBuilder.MetadataTokenInternal;
			}
		}

		/// <summary>Gets the dynamic module in which this constructor is defined.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module in which this constructor is defined.</returns>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x0010C43F File Offset: 0x0010A63F
		public override Module Module
		{
			get
			{
				return this.m_methodBuilder.Module;
			}
		}

		/// <summary>Holds a reference to the <see cref="T:System.Type" /> object from which this object was obtained.</summary>
		/// <returns>The <see langword="Type" /> object from which this object was obtained.</returns>
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x0010C44C File Offset: 0x0010A64C
		public override Type ReflectedType
		{
			get
			{
				return this.m_methodBuilder.ReflectedType;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Type" /> object for the type that declares this member.</summary>
		/// <returns>The type that declares this member.</returns>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x0010C459 File Offset: 0x0010A659
		public override Type DeclaringType
		{
			get
			{
				return this.m_methodBuilder.DeclaringType;
			}
		}

		/// <summary>Retrieves the name of this constructor.</summary>
		/// <returns>The name of this constructor.</returns>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x0010C466 File Offset: 0x0010A666
		public override string Name
		{
			get
			{
				return this.m_methodBuilder.Name;
			}
		}

		/// <summary>Dynamically invokes the constructor reflected by this instance with the specified arguments, under the constraints of the specified <see langword="Binder" />.</summary>
		/// <param name="obj">The object that needs to be reinitialized.</param>
		/// <param name="invokeAttr">One of the <see langword="BindingFlags" /> values that specifies the type of binding that is desired.</param>
		/// <param name="binder">A <see langword="Binder" /> that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is <see langword="null" />, then Binder.DefaultBinding is used.</param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters, this should be a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />.</exception>
		// Token: 0x060049D0 RID: 18896 RVA: 0x0010C473 File Offset: 0x0010A673
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Returns the parameters of this constructor.</summary>
		/// <returns>An array that represents the parameters of this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework versions 1.0 and 1.1.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework version 2.0.</exception>
		// Token: 0x060049D1 RID: 18897 RVA: 0x0010C484 File Offset: 0x0010A684
		public override ParameterInfo[] GetParameters()
		{
			ConstructorInfo constructor = this.GetTypeBuilder().GetConstructor(this.m_methodBuilder.m_parameterTypes);
			return constructor.GetParameters();
		}

		/// <summary>Gets the attributes for this constructor.</summary>
		/// <returns>The attributes for this constructor.</returns>
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060049D2 RID: 18898 RVA: 0x0010C4AE File Offset: 0x0010A6AE
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodBuilder.Attributes;
			}
		}

		/// <summary>Returns the method implementation flags for this constructor.</summary>
		/// <returns>The method implementation flags for this constructor.</returns>
		// Token: 0x060049D3 RID: 18899 RVA: 0x0010C4BB File Offset: 0x0010A6BB
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_methodBuilder.GetMethodImplementationFlags();
		}

		/// <summary>Gets the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>The internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this class.</exception>
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x0010C4C8 File Offset: 0x0010A6C8
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_methodBuilder.MethodHandle;
			}
		}

		/// <summary>Dynamically invokes the constructor represented by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If binder is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters this should be <see langword="null" />.</param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (For example, this is necessary to convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.)</param>
		/// <returns>The value returned by the invoked constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />.</exception>
		// Token: 0x060049D5 RID: 18901 RVA: 0x0010C4D5 File Offset: 0x0010A6D5
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored.</param>
		/// <returns>An array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		// Token: 0x060049D6 RID: 18902 RVA: 0x0010C4E6 File Offset: 0x0010A6E6
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(inherit);
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <param name="attributeType">The custom attribute type.</param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored.</param>
		/// <returns>An object array that represents the attributes of this constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		// Token: 0x060049D7 RID: 18903 RVA: 0x0010C4F4 File Offset: 0x0010A6F4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <param name="attributeType">A custom attribute type.</param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the specified custom attribute type is defined; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />.</exception>
		// Token: 0x060049D8 RID: 18904 RVA: 0x0010C503 File Offset: 0x0010A703
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.IsDefined(attributeType, inherit);
		}

		/// <summary>Returns the <see cref="T:System.Reflection.Emit.MethodToken" /> that represents the token for this constructor.</summary>
		/// <returns>The <see cref="T:System.Reflection.Emit.MethodToken" /> of this constructor.</returns>
		// Token: 0x060049D9 RID: 18905 RVA: 0x0010C512 File Offset: 0x0010A712
		public MethodToken GetToken()
		{
			return this.m_methodBuilder.GetToken();
		}

		/// <summary>Defines a parameter of this constructor.</summary>
		/// <param name="iSequence">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter.</param>
		/// <param name="attributes">The attributes of the parameter.</param>
		/// <param name="strParamName">The name of the parameter. The name can be the null string.</param>
		/// <returns>An object that represents the new parameter of this constructor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="iSequence" /> is less than 0 (zero), or it is greater than the number of parameters of the constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060049DA RID: 18906 RVA: 0x0010C51F File Offset: 0x0010A71F
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
		{
			attributes &= ~ParameterAttributes.ReservedMask;
			return this.m_methodBuilder.DefineParameter(iSequence, attributes, strParamName);
		}

		/// <summary>Sets this constructor's custom attribute associated with symbolic information.</summary>
		/// <param name="name">The name of the custom attribute.</param>
		/// <param name="data">The value of the custom attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The module does not have a symbol writer defined. For example, the module is not a debug module.</exception>
		// Token: 0x060049DB RID: 18907 RVA: 0x0010C538 File Offset: 0x0010A738
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.m_methodBuilder.SetSymCustomAttribute(name, data);
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.  
		///  -or-  
		///  The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body.</exception>
		// Token: 0x060049DC RID: 18908 RVA: 0x0010C547 File Offset: 0x0010A747
		public ILGenerator GetILGenerator()
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> object, with the specified MSIL stream size, that can be used to build a method body for this constructor.</summary>
		/// <param name="streamSize">The size of the MSIL stream, in bytes.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.  
		///  -or-  
		///  The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body.</exception>
		// Token: 0x060049DD RID: 18909 RVA: 0x0010C56C File Offset: 0x0010A76C
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator(streamSize);
		}

		/// <summary>Creates the body of the constructor by using a specified byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array that contains valid MSIL instructions.</param>
		/// <param name="maxStack">The maximum stack evaluation depth.</param>
		/// <param name="localSignature">An array of bytes that contain the serialized local variable structure. Specify <see langword="null" /> if the constructor has no local variables.</param>
		/// <param name="exceptionHandlers">A collection that contains the exception handlers for the constructor. Specify <see langword="null" /> if the constructor has no exception handlers.</param>
		/// <param name="tokenFixups">A collection of values that represent offsets in <paramref name="il" />, each of which specifies the beginning of a token that may be modified. Specify <see langword="null" /> if the constructor has no tokens that have to be modified.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="il" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStack" /> is negative.  
		/// -or-  
		/// One of <paramref name="exceptionHandlers" /> specifies an offset outside of <paramref name="il" />.  
		/// -or-  
		/// One of <paramref name="tokenFixups" /> specifies an offset that is outside the <paramref name="il" /> array.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.  
		///  -or-  
		///  This method was called previously on this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> object.</exception>
		// Token: 0x060049DE RID: 18910 RVA: 0x0010C592 File Offset: 0x0010A792
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorDefineBody"));
			}
			this.m_methodBuilder.SetMethodBody(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		/// <summary>Adds declarative security to this constructor.</summary>
		/// <param name="action">The security action to be taken, such as Demand, Assert, and so on.</param>
		/// <param name="pset">The set of permissions the action applies to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="action" /> is invalid (RequestMinimum, RequestOptional, and RequestRefuse are invalid).</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The permission set <paramref name="pset" /> contains an action that was added earlier by <see langword="AddDeclarativeSecurity" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is <see langword="null" />.</exception>
		// Token: 0x060049DF RID: 18911 RVA: 0x0010C5C0 File Offset: 0x0010A7C0
		[SecuritySafeCritical]
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			if (this.m_methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			byte[] array = pset.EncodeXml();
			TypeBuilder.AddDeclarativeSecurity(this.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, action, array, array.Length);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.CallingConventions" /> value that depends on whether the declaring type is generic.</summary>
		/// <returns>
		///   <see cref="F:System.Reflection.CallingConventions.HasThis" /> if the declaring type is generic; otherwise, <see cref="F:System.Reflection.CallingConventions.Standard" />.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x0010C652 File Offset: 0x0010A852
		public override CallingConventions CallingConvention
		{
			get
			{
				if (this.DeclaringType.IsGenericType)
				{
					return CallingConventions.HasThis;
				}
				return CallingConventions.Standard;
			}
		}

		/// <summary>Returns a reference to the module that contains this constructor.</summary>
		/// <returns>The module that contains this constructor.</returns>
		// Token: 0x060049E1 RID: 18913 RVA: 0x0010C665 File Offset: 0x0010A865
		public Module GetModule()
		{
			return this.m_methodBuilder.GetModule();
		}

		/// <summary>Gets <see langword="null" />.</summary>
		/// <returns>Returns <see langword="null" />.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x0010C672 File Offset: 0x0010A872
		[Obsolete("This property has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public Type ReturnType
		{
			get
			{
				return this.GetReturnType();
			}
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x0010C67A File Offset: 0x0010A87A
		internal override Type GetReturnType()
		{
			return this.m_methodBuilder.ReturnType;
		}

		/// <summary>Retrieves the signature of the field in the form of a string.</summary>
		/// <returns>The signature of the field.</returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x0010C687 File Offset: 0x0010A887
		public string Signature
		{
			get
			{
				return this.m_methodBuilder.Signature;
			}
		}

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		// Token: 0x060049E5 RID: 18917 RVA: 0x0010C694 File Offset: 0x0010A894
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_methodBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		// Token: 0x060049E6 RID: 18918 RVA: 0x0010C6A3 File Offset: 0x0010A8A3
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_methodBuilder.SetCustomAttribute(customBuilder);
		}

		/// <summary>Sets the method implementation flags for this constructor.</summary>
		/// <param name="attributes">The method implementation flags.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060049E7 RID: 18919 RVA: 0x0010C6B1 File Offset: 0x0010A8B1
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.m_methodBuilder.SetImplementationFlags(attributes);
		}

		/// <summary>Gets or sets whether the local variables in this constructor should be zero-initialized.</summary>
		/// <returns>Read/write. Gets or sets whether the local variables in this constructor should be zero-initialized.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060049E8 RID: 18920 RVA: 0x0010C6BF File Offset: 0x0010A8BF
		// (set) Token: 0x060049E9 RID: 18921 RVA: 0x0010C6CC File Offset: 0x0010A8CC
		public bool InitLocals
		{
			get
			{
				return this.m_methodBuilder.InitLocals;
			}
			set
			{
				this.m_methodBuilder.InitLocals = value;
			}
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060049EA RID: 18922 RVA: 0x0010C6DA File Offset: 0x0010A8DA
		void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060049EB RID: 18923 RVA: 0x0010C6E1 File Offset: 0x0010A8E1
		void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060049EC RID: 18924 RVA: 0x0010C6E8 File Offset: 0x0010A8E8
		void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060049ED RID: 18925 RVA: 0x0010C6EF File Offset: 0x0010A8EF
		void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E8B RID: 7819
		private readonly MethodBuilder m_methodBuilder;

		// Token: 0x04001E8C RID: 7820
		internal bool m_isDefaultConstructor;
	}
}
