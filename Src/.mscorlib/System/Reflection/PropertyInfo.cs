using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a property and provides access to property metadata.</summary>
	// Token: 0x02000619 RID: 1561
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class PropertyInfo : MemberInfo, _PropertyInfo
	{
		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600486C RID: 18540 RVA: 0x00108552 File Offset: 0x00106752
		[__DynamicallyInvokable]
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimePropertyInfo) && !(right is RuntimePropertyInfo) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600486D RID: 18541 RVA: 0x00108579 File Offset: 0x00106779
		[__DynamicallyInvokable]
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return !(left == right);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600486E RID: 18542 RVA: 0x00108585 File Offset: 0x00106785
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600486F RID: 18543 RVA: 0x0010858E File Offset: 0x0010678E
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</returns>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x00108596 File Offset: 0x00106796
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		/// <summary>Returns a literal value associated with the property by a compiler.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata.</exception>
		// Token: 0x06004871 RID: 18545 RVA: 0x0010859A File Offset: 0x0010679A
		[__DynamicallyInvokable]
		public virtual object GetConstantValue()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a literal value associated with the property by a compiler.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata Logical Format: Other Structures, Element Types used in Signatures.</exception>
		// Token: 0x06004872 RID: 18546 RVA: 0x001085A1 File Offset: 0x001067A1
		public virtual object GetRawConstantValue()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004873 RID: 18547
		[__DynamicallyInvokable]
		public abstract Type PropertyType
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>When overridden in a derived class, sets the property value for a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. You must specify a suitable invocation attribute. For example, to invoke a static member, set the <see langword="Static" /> flag.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is <see langword="null" />, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="set" /> accessor is not found.  
		///  -or-  
		///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x06004874 RID: 18548
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Returns an array whose elements reflect the public and, if specified, non-public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance.</summary>
		/// <param name="nonPublic">Indicates whether non-public methods should be returned in the returned array. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>An array whose elements reflect the <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance. If <paramref name="nonPublic" /> is <see langword="true" />, this array contains public and non-public <see langword="get" /> and <see langword="set" /> accessors. If <paramref name="nonPublic" /> is <see langword="false" />, this array contains only public <see langword="get" /> and <see langword="set" /> accessors. If no accessors with the specified visibility are found, this method returns an array with zero (0) elements.</returns>
		// Token: 0x06004875 RID: 18549
		[__DynamicallyInvokable]
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		/// <summary>When overridden in a derived class, returns the public or non-public <see langword="get" /> accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether a non-public <see langword="get" /> accessor should be returned. <see langword="true" /> if a non-public accessor is to be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="MethodInfo" /> object representing the <see langword="get" /> accessor for this property, if <paramref name="nonPublic" /> is <see langword="true" />. Returns <see langword="null" /> if <paramref name="nonPublic" /> is <see langword="false" /> and the <see langword="get" /> accessor is non-public, or if <paramref name="nonPublic" /> is <see langword="true" /> but no <see langword="get" /> accessors exist.</returns>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method.</exception>
		// Token: 0x06004876 RID: 18550
		[__DynamicallyInvokable]
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, returns the <see langword="set" /> accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. <see langword="true" /> if a non-public accessor is to be returned; otherwise, <see langword="false" />.</param>
		/// <returns>This property's <see langword="Set" /> method, or <see langword="null" />, as shown in the following table.  
		///   Value  
		///
		///   Condition  
		///
		///   The <see langword="Set" /> method for this property.  
		///
		///   The <see langword="set" /> accessor is public.  
		///
		///  -or-  
		///
		///  <paramref name="nonPublic" /> is <see langword="true" /> and the <see langword="set" /> accessor is non-public.  
		///
		///  <see langword="null" /><paramref name="nonPublic" /> is <see langword="true" />, but the property is read-only.  
		///
		///  -or-  
		///
		///  <paramref name="nonPublic" /> is <see langword="false" /> and the <see langword="set" /> accessor is non-public.  
		///
		///  -or-  
		///
		///  There is no <see langword="set" /> accessor.</returns>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method.</exception>
		// Token: 0x06004877 RID: 18551
		[__DynamicallyInvokable]
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type <see langword="ParameterInfo" /> containing the parameters for the indexes. If the property is not indexed, the array has 0 (zero) elements.</returns>
		// Token: 0x06004878 RID: 18552
		[__DynamicallyInvokable]
		public abstract ParameterInfo[] GetIndexParameters();

		/// <summary>Gets the attributes for this property.</summary>
		/// <returns>The attributes of this property.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004879 RID: 18553
		[__DynamicallyInvokable]
		public abstract PropertyAttributes Attributes
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be read; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600487A RID: 18554
		[__DynamicallyInvokable]
		public abstract bool CanRead
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be written to; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600487B RID: 18555
		[__DynamicallyInvokable]
		public abstract bool CanWrite
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Returns the property value of a specified object.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <returns>The property value of the specified object.</returns>
		// Token: 0x0600487C RID: 18556 RVA: 0x001085A8 File Offset: 0x001067A8
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, null);
		}

		/// <summary>Returns the property value of a specified object with optional index values for indexed properties.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="index">Optional index values for indexed properties. The indexes of indexed properties are zero-based. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <returns>The property value of the specified object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="get" /> accessor is not found.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x0600487D RID: 18557 RVA: 0x001085B2 File Offset: 0x001067B2
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		/// <summary>When overridden in a derived class, returns the property value of a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, and <see langword="SetProperty" />. You must specify a suitable invocation attribute. For example, to invoke a static member, set the <see langword="Static" /> flag.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is <see langword="null" />, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property.</param>
		/// <returns>The property value of the specified object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="get" /> accessor is not found.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x0600487E RID: 18558
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Sets the property value of a specified object.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		/// <exception cref="T:System.ArgumentException">The property's <see langword="set" /> accessor is not found.  
		///  -or-  
		///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The type of <paramref name="obj" /> does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x0600487F RID: 18559 RVA: 0x001085BF File Offset: 0x001067BF
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, null);
		}

		/// <summary>Sets the property value of a specified object with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="set" /> accessor is not found.  
		///  -or-  
		///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x06004880 RID: 18560 RVA: 0x001085CA File Offset: 0x001067CA
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		/// <summary>Returns an array of types representing the required custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06004881 RID: 18561 RVA: 0x001085D8 File Offset: 0x001067D8
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		/// <summary>Returns an array of types representing the optional custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06004882 RID: 18562 RVA: 0x001085DF File Offset: 0x001067DF
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		/// <summary>Returns an array whose elements reflect the public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects that reflect the public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance, if found; otherwise, this method returns an array with zero (0) elements.</returns>
		// Token: 0x06004883 RID: 18563 RVA: 0x001085E6 File Offset: 0x001067E6
		[__DynamicallyInvokable]
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		/// <summary>Gets the <see langword="get" /> accessor for this property.</summary>
		/// <returns>The <see langword="get" /> accessor for this property.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004884 RID: 18564 RVA: 0x001085EF File Offset: 0x001067EF
		[__DynamicallyInvokable]
		public virtual MethodInfo GetMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetGetMethod(true);
			}
		}

		/// <summary>Gets the <see langword="set" /> accessor for this property.</summary>
		/// <returns>The <see langword="set" /> accessor for this property, or <see langword="null" /> if the property is read-only.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004885 RID: 18565 RVA: 0x001085F8 File Offset: 0x001067F8
		[__DynamicallyInvokable]
		public virtual MethodInfo SetMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetSetMethod(true);
			}
		}

		/// <summary>Returns the public <see langword="get" /> accessor for this property.</summary>
		/// <returns>A <see langword="MethodInfo" /> object representing the public <see langword="get" /> accessor for this property, or <see langword="null" /> if the <see langword="get" /> accessor is non-public or does not exist.</returns>
		// Token: 0x06004886 RID: 18566 RVA: 0x00108601 File Offset: 0x00106801
		[__DynamicallyInvokable]
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		/// <summary>Returns the public <see langword="set" /> accessor for this property.</summary>
		/// <returns>The <see langword="MethodInfo" /> object representing the <see langword="Set" /> method for this property if the <see langword="set" /> accessor is public, or <see langword="null" /> if the <see langword="set" /> accessor is not public.</returns>
		// Token: 0x06004887 RID: 18567 RVA: 0x0010860A File Offset: 0x0010680A
		[__DynamicallyInvokable]
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		/// <summary>Gets a value indicating whether the property is the special name.</summary>
		/// <returns>
		///   <see langword="true" /> if this property is the special name; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x00108613 File Offset: 0x00106813
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</returns>
		// Token: 0x06004889 RID: 18569 RVA: 0x00108624 File Offset: 0x00106824
		Type _PropertyInfo.GetType()
		{
			return base.GetType();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600488A RID: 18570 RVA: 0x0010862C File Offset: 0x0010682C
		void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600488B RID: 18571 RVA: 0x00108633 File Offset: 0x00106833
		void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
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
		// Token: 0x0600488C RID: 18572 RVA: 0x0010863A File Offset: 0x0010683A
		void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x0600488D RID: 18573 RVA: 0x00108641 File Offset: 0x00106841
		void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
