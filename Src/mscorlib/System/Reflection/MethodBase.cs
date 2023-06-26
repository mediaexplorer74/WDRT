using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	/// <summary>Provides information about methods and constructors.</summary>
	// Token: 0x02000604 RID: 1540
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodBase))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class MethodBase : MemberInfo, _MethodBase
	{
		/// <summary>Gets method information by using the method's internal metadata representation (handle).</summary>
		/// <param name="handle">The method's handle.</param>
		/// <returns>A <see langword="MethodBase" /> containing information about the method.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x06004709 RID: 18185 RVA: 0x00104E9C File Offset: 0x0010309C
		[__DynamicallyInvokable]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			MethodBase methodBase = RuntimeType.GetMethodBase(handle.GetMethodInfo());
			Type declaringType = methodBase.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGeneric"), methodBase, declaringType.GetGenericTypeDefinition()));
			}
			return methodBase;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> object for the constructor or method represented by the specified handle, for the specified generic type.</summary>
		/// <param name="handle">A handle to the internal metadata representation of a constructor or method.</param>
		/// <param name="declaringType">A handle to the generic type that defines the constructor or method.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method or constructor specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x0600470A RID: 18186 RVA: 0x00104F09 File Offset: 0x00103109
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			return RuntimeType.GetMethodBase(declaringType.GetRuntimeType(), handle.GetMethodInfo());
		}

		/// <summary>Returns a <see langword="MethodBase" /> object representing the currently executing method.</summary>
		/// <returns>
		///   <see cref="M:System.Reflection.MethodBase.GetCurrentMethod" /> is a static method that is called from within an executing method and that returns information about that method.  
		/// A <see langword="MethodBase" /> object representing the currently executing method.</returns>
		/// <exception cref="T:System.Reflection.TargetException">This member was invoked with a late-binding mechanism.</exception>
		// Token: 0x0600470B RID: 18187 RVA: 0x00104F38 File Offset: 0x00103138
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static MethodBase GetCurrentMethod()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeMethodInfo.InternalGetCurrentMethod(ref stackCrawlMark);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600470D RID: 18189 RVA: 0x00104F58 File Offset: 0x00103158
		[__DynamicallyInvokable]
		public static bool operator ==(MethodBase left, MethodBase right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			MethodInfo methodInfo;
			MethodInfo methodInfo2;
			if ((methodInfo = left as MethodInfo) != null && (methodInfo2 = right as MethodInfo) != null)
			{
				return methodInfo == methodInfo2;
			}
			ConstructorInfo constructorInfo;
			ConstructorInfo constructorInfo2;
			return (constructorInfo = left as ConstructorInfo) != null && (constructorInfo2 = right as ConstructorInfo) != null && constructorInfo == constructorInfo2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600470E RID: 18190 RVA: 0x00104FC4 File Offset: 0x001031C4
		[__DynamicallyInvokable]
		public static bool operator !=(MethodBase left, MethodBase right)
		{
			return !(left == right);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600470F RID: 18191 RVA: 0x00104FD0 File Offset: 0x001031D0
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004710 RID: 18192 RVA: 0x00104FD9 File Offset: 0x001031D9
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00104FE4 File Offset: 0x001031E4
		[SecurityCritical]
		private IntPtr GetMethodDesc()
		{
			return this.MethodHandle.Value;
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x00104FFF File Offset: 0x001031FF
		internal virtual bool IsDynamicallyInvokable
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00105002 File Offset: 0x00103202
		internal virtual ParameterInfo[] GetParametersNoCopy()
		{
			return this.GetParameters();
		}

		/// <summary>When overridden in a derived class, gets the parameters of the specified method or constructor.</summary>
		/// <returns>An array of type <see langword="ParameterInfo" /> containing information that matches the signature of the method (or constructor) reflected by this <see langword="MethodBase" /> instance.</returns>
		// Token: 0x06004714 RID: 18196
		[__DynamicallyInvokable]
		public abstract ParameterInfo[] GetParameters();

		/// <summary>Gets the <see cref="T:System.Reflection.MethodImplAttributes" /> flags that specify the attributes of a method implementation.</summary>
		/// <returns>The method implementation flags.</returns>
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x0010500A File Offset: 0x0010320A
		[__DynamicallyInvokable]
		public virtual MethodImplAttributes MethodImplementationFlags
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethodImplementationFlags();
			}
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.MethodImplAttributes" /> flags.</summary>
		/// <returns>The <see langword="MethodImplAttributes" /> flags.</returns>
		// Token: 0x06004716 RID: 18198
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		/// <summary>Gets a handle to the internal metadata representation of a method.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> object.</returns>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004717 RID: 18199
		[__DynamicallyInvokable]
		public abstract RuntimeMethodHandle MethodHandle
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the attributes associated with this method.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MethodAttributes" /> values.</returns>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06004718 RID: 18200
		[__DynamicallyInvokable]
		public abstract MethodAttributes Attributes
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>When overridden in a derived class, invokes the reflected method or constructor with the given parameters.</summary>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be <see langword="null" /> or an instance of the class that defines the constructor.</param>
		/// <param name="invokeAttr">A bitmask that is a combination of 0 or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. If <paramref name="binder" /> is <see langword="null" />, this parameter is assigned the value <see cref="F:System.Reflection.BindingFlags.Default" />; thus, whatever you pass in is ignored.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, this should be <see langword="null" />.  
		///  If the method or constructor represented by this instance takes a ByRef parameter, there is no special attribute required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see langword="CultureInfo" /> for the current thread is used. (This is necessary to convert a <see langword="String" /> that represents 1000 to a <see langword="Double" /> value, for example, since 1000 is represented differently by different cultures.)</param>
		/// <returns>An <see langword="Object" /> containing the return value of the invoked method, or <see langword="null" /> in the case of a constructor, or <see langword="null" /> if the method's return type is <see langword="void" />. Before calling the method or constructor, <see langword="Invoke" /> checks to see if the user has access permission and verifies that the parameters are valid.</returns>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is <see langword="null" /> and the method is not static.  
		///  -or-  
		///  The method is not declared or inherited by the class of <paramref name="obj" />.  
		///  -or-  
		///  A static constructor is invoked, and <paramref name="obj" /> is neither <see langword="null" /> nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="parameters" /> parameter does not match the signature of the method or constructor reflected by this instance.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to execute the method or constructor that is represented by the current instance.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" /> for the declaring type.</exception>
		// Token: 0x06004719 RID: 18201
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Gets a value indicating the calling conventions for this method.</summary>
		/// <returns>The <see cref="T:System.Reflection.CallingConventions" /> for this method.</returns>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600471A RID: 18202 RVA: 0x00105012 File Offset: 0x00103212
		[__DynamicallyInvokable]
		public virtual CallingConventions CallingConvention
		{
			[__DynamicallyInvokable]
			get
			{
				return CallingConventions.Standard;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
		/// <exception cref="T:System.NotSupportedException">The current object is a <see cref="T:System.Reflection.ConstructorInfo" />. Generic constructors are not supported in the .NET Framework version 2.0. This exception is the default behavior if this method is not overridden in a derived class.</exception>
		// Token: 0x0600471B RID: 18203 RVA: 0x00105015 File Offset: 0x00103215
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		/// <summary>Gets a value indicating whether the method is a generic method definition.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.MethodBase" /> object represents the definition of a generic method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600471C RID: 18204 RVA: 0x00105026 File Offset: 0x00103226
		[__DynamicallyInvokable]
		public virtual bool IsGenericMethodDefinition
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the generic method contains unassigned generic type parameters.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.MethodBase" /> object represents a generic method that contains unassigned generic type parameters; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600471D RID: 18205 RVA: 0x00105029 File Offset: 0x00103229
		[__DynamicallyInvokable]
		public virtual bool ContainsGenericParameters
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the method is generic.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.MethodBase" /> represents a generic method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x0010502C File Offset: 0x0010322C
		[__DynamicallyInvokable]
		public virtual bool IsGenericMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the current method or constructor is security-critical or security-safe-critical at the current trust level; <see langword="false" /> if it is transparent.</returns>
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600471F RID: 18207 RVA: 0x0010502F File Offset: 0x0010322F
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code.</summary>
		/// <returns>
		///   <see langword="true" /> if the method or constructor is security-safe-critical at the current trust level; <see langword="false" /> if it is security-critical or transparent.</returns>
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x00105036 File Offset: 0x00103236
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is transparent at the current trust level, and therefore cannot perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the method or constructor is security-transparent at the current trust level; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x0010503D File Offset: 0x0010323D
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Invokes the method or constructor represented by the current instance, using the specified parameters.</summary>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be <see langword="null" /> or an instance of the class that defines the constructor.</param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, <paramref name="parameters" /> should be <see langword="null" />.  
		///  If the method or constructor represented by this instance takes a <see langword="ref" /> parameter (<see langword="ByRef" /> in Visual Basic), no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <returns>An object containing the return value of the invoked method, or <see langword="null" /> in the case of a constructor.</returns>
		/// <exception cref="T:System.Reflection.TargetException">In the.NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The <paramref name="obj" /> parameter is <see langword="null" /> and the method is not static.  
		///  -or-  
		///  The method is not declared or inherited by the class of <paramref name="obj" />.  
		///  -or-  
		///  A static constructor is invoked, and <paramref name="obj" /> is neither <see langword="null" /> nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The elements of the <paramref name="parameters" /> array do not match the signature of the method or constructor reflected by this instance.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception.  
		///  -or-  
		///  The current instance is a <see cref="T:System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code. See the "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have permission to execute the method or constructor that is represented by the current instance.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" /> for the declaring type.</exception>
		/// <exception cref="T:System.NotSupportedException">The current instance is a <see cref="T:System.Reflection.Emit.MethodBuilder" />.</exception>
		// Token: 0x06004722 RID: 18210 RVA: 0x00105044 File Offset: 0x00103244
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		/// <summary>Gets a value indicating whether this is a public method.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is public; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x00105051 File Offset: 0x00103251
		[__DynamicallyInvokable]
		public bool IsPublic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether this member is private.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method is restricted to other members of the class itself; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06004724 RID: 18212 RVA: 0x0010505E File Offset: 0x0010325E
		[__DynamicallyInvokable]
		public bool IsPrivate
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.Family" />; that is, the method or constructor is visible only within its class and derived classes.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.Family" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x0010506B File Offset: 0x0010326B
		[__DynamicallyInvokable]
		public bool IsFamily
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.Assembly" />; that is, the method or constructor is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the visibility of this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.Assembly" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x00105078 File Offset: 0x00103278
		[__DynamicallyInvokable]
		public bool IsAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; that is, the method or constructor can be called by derived classes, but only if they are in the same assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x00105085 File Offset: 0x00103285
		[__DynamicallyInvokable]
		public bool IsFamilyAndAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; that is, the method or constructor can be called by derived classes wherever they are, and by classes in the same assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x00105092 File Offset: 0x00103292
		[__DynamicallyInvokable]
		public bool IsFamilyOrAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
			}
		}

		/// <summary>Gets a value indicating whether the method is <see langword="static" />.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is <see langword="static" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06004729 RID: 18217 RVA: 0x0010509F File Offset: 0x0010329F
		[__DynamicallyInvokable]
		public bool IsStatic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this method is <see langword="final" />.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is <see langword="final" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x001050AD File Offset: 0x001032AD
		[__DynamicallyInvokable]
		public bool IsFinal
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Final) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is <see langword="virtual" />.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is <see langword="virtual" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x0600472B RID: 18219 RVA: 0x001050BB File Offset: 0x001032BB
		[__DynamicallyInvokable]
		public bool IsVirtual
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether only a member of the same kind with exactly the same signature is hidden in the derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if the member is hidden by signature; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x001050C9 File Offset: 0x001032C9
		[__DynamicallyInvokable]
		public bool IsHideBySig
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.HideBySig) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is abstract.</summary>
		/// <returns>
		///   <see langword="true" /> if the method is abstract; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600472D RID: 18221 RVA: 0x001050DA File Offset: 0x001032DA
		[__DynamicallyInvokable]
		public bool IsAbstract
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this method has a special name.</summary>
		/// <returns>
		///   <see langword="true" /> if this method has a special name; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x001050EB File Offset: 0x001032EB
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is a constructor.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is a constructor represented by a <see cref="T:System.Reflection.ConstructorInfo" /> object (see note in Remarks about <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> objects); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600472F RID: 18223 RVA: 0x001050FC File Offset: 0x001032FC
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public bool IsConstructor
		{
			[__DynamicallyInvokable]
			get
			{
				return this is ConstructorInfo && !this.IsStatic && (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
			}
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Reflection.MethodBody" /> object that provides access to the MSIL stream, local variables, and exceptions for the current method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBody" /> object that provides access to the MSIL stream, local variables, and exceptions for the current method.</returns>
		/// <exception cref="T:System.InvalidOperationException">This method is invalid unless overridden in a derived class.</exception>
		// Token: 0x06004730 RID: 18224 RVA: 0x00105123 File Offset: 0x00103323
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public virtual MethodBody GetMethodBody()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x0010512C File Offset: 0x0010332C
		internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (Type type in parameterTypes)
			{
				stringBuilder.Append(text);
				string text2 = type.FormatTypeName(serialization);
				if (type.IsByRef && !serialization)
				{
					stringBuilder.Append(text2.TrimEnd(new char[] { '&' }));
					stringBuilder.Append(" ByRef");
				}
				else
				{
					stringBuilder.Append(text2);
				}
				text = ", ";
			}
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x001051C9 File Offset: 0x001033C9
		internal string FullName
		{
			get
			{
				return string.Format("{0}.{1}", this.DeclaringType.FullName, this.FormatNameAndSig());
			}
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x001051E6 File Offset: 0x001033E6
		internal string FormatNameAndSig()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x001051F0 File Offset: 0x001033F0
		internal virtual string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x00105240 File Offset: 0x00103440
		internal virtual Type[] GetParameterTypes()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			Type[] array = new Type[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				array[i] = parametersNoCopy[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x00105278 File Offset: 0x00103478
		[SecuritySafeCritical]
		internal object[] CheckArguments(object[] parameters, Binder binder, BindingFlags invokeAttr, CultureInfo culture, Signature sig)
		{
			object[] array = new object[parameters.Length];
			ParameterInfo[] array2 = null;
			for (int i = 0; i < parameters.Length; i++)
			{
				object obj = parameters[i];
				RuntimeType runtimeType = sig.Arguments[i];
				if (obj == Type.Missing)
				{
					if (array2 == null)
					{
						array2 = this.GetParametersNoCopy();
					}
					if (array2[i].DefaultValue == DBNull.Value)
					{
						throw new ArgumentException(Environment.GetResourceString("Arg_VarMissNull"), "parameters");
					}
					obj = array2[i].DefaultValue;
				}
				array[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
			}
			return array;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.InteropServices._MethodBase.GetType" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Runtime.InteropServices._MethodBase.GetType" />.</returns>
		// Token: 0x06004737 RID: 18231 RVA: 0x001052FC File Offset: 0x001034FC
		Type _MethodBase.GetType()
		{
			return base.GetType();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPublic" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPublic" />.</returns>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x00105304 File Offset: 0x00103504
		bool _MethodBase.IsPublic
		{
			get
			{
				return this.IsPublic;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPrivate" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPrivate" />.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x0010530C File Offset: 0x0010350C
		bool _MethodBase.IsPrivate
		{
			get
			{
				return this.IsPrivate;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamily" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamily" />.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600473A RID: 18234 RVA: 0x00105314 File Offset: 0x00103514
		bool _MethodBase.IsFamily
		{
			get
			{
				return this.IsFamily;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAssembly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAssembly" />.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600473B RID: 18235 RVA: 0x0010531C File Offset: 0x0010351C
		bool _MethodBase.IsAssembly
		{
			get
			{
				return this.IsAssembly;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyAndAssembly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyAndAssembly" />.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x00105324 File Offset: 0x00103524
		bool _MethodBase.IsFamilyAndAssembly
		{
			get
			{
				return this.IsFamilyAndAssembly;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyOrAssembly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyOrAssembly" />.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600473D RID: 18237 RVA: 0x0010532C File Offset: 0x0010352C
		bool _MethodBase.IsFamilyOrAssembly
		{
			get
			{
				return this.IsFamilyOrAssembly;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsStatic" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsStatic" />.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600473E RID: 18238 RVA: 0x00105334 File Offset: 0x00103534
		bool _MethodBase.IsStatic
		{
			get
			{
				return this.IsStatic;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFinal" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFinal" />.</returns>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x0010533C File Offset: 0x0010353C
		bool _MethodBase.IsFinal
		{
			get
			{
				return this.IsFinal;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsVirtual" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsVirtual" />.</returns>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x00105344 File Offset: 0x00103544
		bool _MethodBase.IsVirtual
		{
			get
			{
				return this.IsVirtual;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsHideBySig" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsHideBySig" />.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06004741 RID: 18241 RVA: 0x0010534C File Offset: 0x0010354C
		bool _MethodBase.IsHideBySig
		{
			get
			{
				return this.IsHideBySig;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAbstract" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAbstract" />.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x00105354 File Offset: 0x00103554
		bool _MethodBase.IsAbstract
		{
			get
			{
				return this.IsAbstract;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsSpecialName" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsSpecialName" />.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x0010535C File Offset: 0x0010355C
		bool _MethodBase.IsSpecialName
		{
			get
			{
				return this.IsSpecialName;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsConstructor" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsConstructor" />.</returns>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x00105364 File Offset: 0x00103564
		bool _MethodBase.IsConstructor
		{
			get
			{
				return this.IsConstructor;
			}
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004745 RID: 18245 RVA: 0x0010536C File Offset: 0x0010356C
		void _MethodBase.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004746 RID: 18246 RVA: 0x00105373 File Offset: 0x00103573
		void _MethodBase.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
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
		// Token: 0x06004747 RID: 18247 RVA: 0x0010537A File Offset: 0x0010357A
		void _MethodBase.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x06004748 RID: 18248 RVA: 0x00105381 File Offset: 0x00103581
		void _MethodBase.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
