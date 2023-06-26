using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	/// <summary>Defines the properties for a type.</summary>
	// Token: 0x0200065B RID: 1627
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x06004CE8 RID: 19688 RVA: 0x0011874D File Offset: 0x0011694D
		private PropertyBuilder()
		{
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x00118758 File Offset: 0x00116958
		internal PropertyBuilder(ModuleBuilder mod, string name, SignatureHelper sig, PropertyAttributes attr, Type returnType, PropertyToken prToken, TypeBuilder containingType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
			}
			this.m_name = name;
			this.m_moduleBuilder = mod;
			this.m_signature = sig;
			this.m_attributes = attr;
			this.m_returnType = returnType;
			this.m_prToken = prToken;
			this.m_tkProperty = prToken.Token;
			this.m_containingType = containingType;
		}

		/// <summary>Sets the default value of this property.</summary>
		/// <param name="defaultValue">The default value of this property.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		/// <exception cref="T:System.ArgumentException">The property is not one of the supported types.  
		///  -or-  
		///  The type of <paramref name="defaultValue" /> does not match the type of the property.  
		///  -or-  
		///  The property is of type <see cref="T:System.Object" /> or other reference type, <paramref name="defaultValue" /> is not <see langword="null" />, and the value cannot be assigned to the reference type.</exception>
		// Token: 0x06004CEA RID: 19690 RVA: 0x001187F6 File Offset: 0x001169F6
		[SecuritySafeCritical]
		public void SetConstant(object defaultValue)
		{
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_moduleBuilder, this.m_prToken.Token, this.m_returnType, defaultValue);
		}

		/// <summary>Retrieves the token for this property.</summary>
		/// <returns>Read-only. Retrieves the token for this property.</returns>
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x00118820 File Offset: 0x00116A20
		public PropertyToken PropertyToken
		{
			get
			{
				return this.m_prToken;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x00118828 File Offset: 0x00116A28
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_tkProperty;
			}
		}

		/// <summary>Gets the module in which the type that declares the current property is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the type that declares the current property is defined.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x00118830 File Offset: 0x00116A30
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x00118840 File Offset: 0x00116A40
		[SecurityCritical]
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.DefineMethodSemantics(this.m_moduleBuilder.GetNativeHandle(), this.m_prToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		/// <summary>Sets the method that gets the property value.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method that gets the property value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004CEF RID: 19695 RVA: 0x00118896 File Offset: 0x00116A96
		[SecuritySafeCritical]
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Getter);
			this.m_getMethod = mdBuilder;
		}

		/// <summary>Sets the method that sets the property value.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method that sets the property value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004CF0 RID: 19696 RVA: 0x001188A7 File Offset: 0x00116AA7
		[SecuritySafeCritical]
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Setter);
			this.m_setMethod = mdBuilder;
		}

		/// <summary>Adds one of the other methods associated with this property.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the other method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004CF1 RID: 19697 RVA: 0x001188B8 File Offset: 0x00116AB8
		[SecuritySafeCritical]
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004CF2 RID: 19698 RVA: 0x001188C4 File Offset: 0x00116AC4
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token, this.m_moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">if <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004CF3 RID: 19699 RVA: 0x0011892B File Offset: 0x00116B2B
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token);
		}

		/// <summary>Gets the value of the indexed property by calling the property's getter method.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <returns>The value of the specified indexed property.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004CF4 RID: 19700 RVA: 0x0011895D File Offset: 0x00116B5D
		public override object GetValue(object obj, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Gets the value of a property having the specified binding, index, and <see langword="CultureInfo" />.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. A suitable invocation attribute must be specified. If a static member is to be invoked, the <see langword="Static" /> flag of <see langword="BindingFlags" /> must be set.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The <see langword="CultureInfo" /> object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the <see langword="CultureInfo.Parent" /> method will be called successively in search of a match. If this value is <see langword="null" />, the <see langword="CultureInfo" /> is obtained from the <see langword="CultureInfo.CurrentUICulture" /> property.</param>
		/// <returns>The property value for <paramref name="obj" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004CF5 RID: 19701 RVA: 0x0011896E File Offset: 0x00116B6E
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Sets the value of the property with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new value for this property.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004CF6 RID: 19702 RVA: 0x0011897F File Offset: 0x00116B7F
		public override void SetValue(object obj, object value, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Sets the property value for the given object to the given value.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="value">The new value for this property.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. A suitable invocation attribute must be specified. If a static member is to be invoked, the <see langword="Static" /> flag of <see langword="BindingFlags" /> must be set.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The <see langword="CultureInfo" /> object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the <see langword="CultureInfo.Parent" /> method will be called successively in search of a match. If this value is <see langword="null" />, the <see langword="CultureInfo" /> is obtained from the <see langword="CultureInfo.CurrentUICulture" /> property.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004CF7 RID: 19703 RVA: 0x00118990 File Offset: 0x00116B90
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Returns an array of the public and non-public <see langword="get" /> and <see langword="set" /> accessors on this property.</summary>
		/// <param name="nonPublic">Indicates whether non-public methods should be returned in the <see langword="MethodInfo" /> array. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>An array of type <see langword="MethodInfo" /> containing the matching public or non-public accessors, or an empty array if matching accessors do not exist on this property.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004CF8 RID: 19704 RVA: 0x001189A1 File Offset: 0x00116BA1
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Returns the public and non-public get accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether non-public get accessors should be returned. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="MethodInfo" /> object representing the get accessor for this property, if <paramref name="nonPublic" /> is <see langword="true" />. Returns <see langword="null" /> if <paramref name="nonPublic" /> is <see langword="false" /> and the get accessor is non-public, or if <paramref name="nonPublic" /> is <see langword="true" /> but no get accessors exist.</returns>
		// Token: 0x06004CF9 RID: 19705 RVA: 0x001189B2 File Offset: 0x00116BB2
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_getMethod == null)
			{
				return this.m_getMethod;
			}
			if ((this.m_getMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_getMethod;
			}
			return null;
		}

		/// <summary>Returns the set accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>The property's <see langword="Set" /> method, or <see langword="null" />, as shown in the following table.  
		///   Value  
		///
		///   Condition  
		///
		///   A <see cref="T:System.Reflection.MethodInfo" /> object representing the Set method for this property.  
		///
		///   The set accessor is public.  
		///
		///  <paramref name="nonPublic" /> is true and non-public methods can be returned.  
		///
		///   null  
		///
		///  <paramref name="nonPublic" /> is true, but the property is read-only.  
		///
		///  <paramref name="nonPublic" /> is false and the set accessor is non-public.</returns>
		// Token: 0x06004CFA RID: 19706 RVA: 0x001189E4 File Offset: 0x00116BE4
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_setMethod == null)
			{
				return this.m_setMethod;
			}
			if ((this.m_setMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_setMethod;
			}
			return null;
		}

		/// <summary>Returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type <see langword="ParameterInfo" /> containing the parameters for the indexes.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004CFB RID: 19707 RVA: 0x00118A16 File Offset: 0x00116C16
		public override ParameterInfo[] GetIndexParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Gets the type of the field of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004CFC RID: 19708 RVA: 0x00118A27 File Offset: 0x00116C27
		public override Type PropertyType
		{
			get
			{
				return this.m_returnType;
			}
		}

		/// <summary>Gets the attributes for this property.</summary>
		/// <returns>Attributes of this property.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x00118A2F File Offset: 0x00116C2F
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be read; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x00118A37 File Offset: 0x00116C37
		public override bool CanRead
		{
			get
			{
				return this.m_getMethod != null;
			}
		}

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be written to; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x00118A4A File Offset: 0x00116C4A
		public override bool CanWrite
		{
			get
			{
				return this.m_setMethod != null;
			}
		}

		/// <summary>Returns an array of all the custom attributes for this property.</summary>
		/// <param name="inherit">If <see langword="true" />, walks up this property's inheritance chain to find the custom attributes</param>
		/// <returns>An array of all the custom attributes.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004D00 RID: 19712 RVA: 0x00118A5D File Offset: 0x00116C5D
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Returns an array of custom attributes identified by <see cref="T:System.Type" />.</summary>
		/// <param name="attributeType">An array of custom attributes identified by type.</param>
		/// <param name="inherit">If <see langword="true" />, walks up this property's inheritance chain to find the custom attributes.</param>
		/// <returns>An array of custom attributes defined on this reflected member, or <see langword="null" /> if no attributes are defined on this member.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004D01 RID: 19713 RVA: 0x00118A6E File Offset: 0x00116C6E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Indicates whether one or more instance of <paramref name="attributeType" /> is defined on this property.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to which the custom attributes are applied.</param>
		/// <param name="inherit">Specifies whether to walk up this property's inheritance chain to find the custom attributes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instance of <paramref name="attributeType" /> is defined on this property; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06004D02 RID: 19714 RVA: 0x00118A7F File Offset: 0x00116C7F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004D03 RID: 19715 RVA: 0x00118A90 File Offset: 0x00116C90
		void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004D04 RID: 19716 RVA: 0x00118A97 File Offset: 0x00116C97
		void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
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
		// Token: 0x06004D05 RID: 19717 RVA: 0x00118A9E File Offset: 0x00116C9E
		void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x06004D06 RID: 19718 RVA: 0x00118AA5 File Offset: 0x00116CA5
		void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the name of this member.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x00118AAC File Offset: 0x00116CAC
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The <see langword="Type" /> object for the class that declares this member.</returns>
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004D08 RID: 19720 RVA: 0x00118AB4 File Offset: 0x00116CB4
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		/// <summary>Gets the class object that was used to obtain this instance of <see langword="MemberInfo" />.</summary>
		/// <returns>The <see langword="Type" /> object through which this <see langword="MemberInfo" /> object was obtained.</returns>
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x00118ABC File Offset: 0x00116CBC
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x04002199 RID: 8601
		private string m_name;

		// Token: 0x0400219A RID: 8602
		private PropertyToken m_prToken;

		// Token: 0x0400219B RID: 8603
		private int m_tkProperty;

		// Token: 0x0400219C RID: 8604
		private ModuleBuilder m_moduleBuilder;

		// Token: 0x0400219D RID: 8605
		private SignatureHelper m_signature;

		// Token: 0x0400219E RID: 8606
		private PropertyAttributes m_attributes;

		// Token: 0x0400219F RID: 8607
		private Type m_returnType;

		// Token: 0x040021A0 RID: 8608
		private MethodInfo m_getMethod;

		// Token: 0x040021A1 RID: 8609
		private MethodInfo m_setMethod;

		// Token: 0x040021A2 RID: 8610
		private TypeBuilder m_containingType;
	}
}
