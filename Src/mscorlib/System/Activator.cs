using System;
using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System
{
	/// <summary>Contains methods to create types of objects locally or remotely, or obtain references to existing remote objects. This class cannot be inherited.</summary>
	// Token: 0x0200008E RID: 142
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Activator))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class Activator : _Activator
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0001991C File Offset: 0x00017B1C
		private Activator()
		{
		}

		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="type" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="type" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="type" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <returns>A reference to the newly created object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		/// -or-  
		/// The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.  
		/// -or-  
		/// The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type.</exception>
		// Token: 0x06000747 RID: 1863 RVA: 0x00019924 File Offset: 0x00017B24
		public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
		{
			return Activator.CreateInstance(type, bindingAttr, binder, args, culture, null);
		}

		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="type" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="type" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="type" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A reference to the newly created object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		/// -or-  
		/// <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.  
		/// -or-  
		/// The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.  
		/// -or-  
		/// The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type.</exception>
		// Token: 0x06000748 RID: 1864 RVA: 0x00019934 File Offset: 0x00017B34
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type is TypeBuilder)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_CreateInstanceWithTypeBuilder"));
			}
			if ((bindingAttr & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingAttr |= BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			}
			if (activationAttributes != null && activationAttributes.Length != 0)
			{
				if (!type.IsMarshalByRef)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_ActivAttrOnNonMBR"));
				}
				if (!type.IsContextful && (activationAttributes.Length > 1 || !(activationAttributes[0] is UrlAttribute)))
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonUrlAttrOnMBR"));
				}
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return runtimeType.CreateInstanceImpl(bindingAttr, binder, args, culture, activationAttributes, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <returns>A reference to the newly created object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		/// -or-  
		/// The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.  
		/// -or-  
		/// The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.MissingMethodException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.  
		///
		///
		///
		///
		///  No matching public constructor was found.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type.</exception>
		// Token: 0x06000749 RID: 1865 RVA: 0x000199FF File Offset: 0x00017BFF
		[__DynamicallyInvokable]
		public static object CreateInstance(Type type, params object[] args)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, null);
		}

		/// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A reference to the newly created object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		/// -or-  
		/// <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.  
		/// -or-  
		/// The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.  
		/// -or-  
		/// The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type.</exception>
		// Token: 0x0600074A RID: 1866 RVA: 0x00019A10 File Offset: 0x00017C10
		public static object CreateInstance(Type type, object[] args, object[] activationAttributes)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, activationAttributes);
		}

		/// <summary>Creates an instance of the specified type using that type's default constructor.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <returns>A reference to the newly created object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		/// -or-  
		/// The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.MissingMethodException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.  
		///
		///
		///
		///
		///  No matching public constructor was found.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type.</exception>
		// Token: 0x0600074B RID: 1867 RVA: 0x00019A21 File Offset: 0x00017C21
		[__DynamicallyInvokable]
		public static object CreateInstance(Type type)
		{
			return Activator.CreateInstance(type, false);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and default constructor.</summary>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is <see langword="null" />, the executing assembly is searched.</param>
		/// <param name="typeName">The fully qualified name of the preferred type.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">You cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.  
		///  -or-  
		///  The assembly name or code base is invalid.</exception>
		// Token: 0x0600074C RID: 1868 RVA: 0x00019A2C File Offset: 0x00017C2C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null, null, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and default constructor.</summary>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is <see langword="null" />, the executing assembly is searched.</param>
		/// <param name="typeName">The fully qualified name of the preferred type.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		///  -or-  
		///  <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.  
		///  -or-  
		///  <paramref name="activationAttributes" /> is not a <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />  
		///  array.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.  
		///  -or-  
		///  The assembly name or code base is invalid.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred when attempting remote activation in a target specified in <paramref name="activationAttributes" />.</exception>
		// Token: 0x0600074D RID: 1869 RVA: 0x00019A50 File Offset: 0x00017C50
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes, null, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the specified type using that type's default constructor.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <param name="nonPublic">
		///   <see langword="true" /> if a public or nonpublic default constructor can match; <see langword="false" /> if only a public default constructor can match.</param>
		/// <returns>A reference to the newly created object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a <see langword="RuntimeType" />.  
		/// -or-  
		/// <paramref name="type" /> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="type" /> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		/// -or-  
		/// The assembly that contains <paramref name="type" /> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">
		///   <paramref name="type" /> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="type" /> is not a valid type.</exception>
		// Token: 0x0600074E RID: 1870 RVA: 0x00019A74 File Offset: 0x00017C74
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object CreateInstance(Type type, bool nonPublic)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return runtimeType.CreateInstanceDefaultCtor(!nonPublic, false, true, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the type designated by the specified generic type parameter, using the parameterless constructor.</summary>
		/// <typeparam name="T">The type to create.</typeparam>
		/// <returns>A reference to the newly created object.</returns>
		/// <exception cref="T:System.MissingMethodException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.  
		///
		///
		///
		///
		///  The type that is specified for <paramref name="T" /> does not have a parameterless constructor.</exception>
		// Token: 0x0600074F RID: 1871 RVA: 0x00019ACC File Offset: 0x00017CCC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static T CreateInstance<T>()
		{
			RuntimeType runtimeType = typeof(T) as RuntimeType;
			if (runtimeType.HasElementType)
			{
				throw new MissingMethodException(Environment.GetResourceString("Arg_NoDefCTor"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return (T)((object)runtimeType.CreateInstanceDefaultCtor(true, true, true, ref stackCrawlMark));
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and default constructor.</summary>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does have the required <see cref="T:System.Security.Permissions.FileIOPermission" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		// Token: 0x06000750 RID: 1872 RVA: 0x00019B13 File Offset: 0x00017D13
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, null);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and default constructor.</summary>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does have the required <see cref="T:System.Security.Permissions.FileIOPermission" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		// Token: 0x06000751 RID: 1873 RVA: 0x00019B1D File Offset: 0x00017D1D
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is <see langword="null" />, the executing assembly is searched.</param>
		/// <param name="typeName">The fully qualified name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <param name="securityInfo">Information used to make security policy decisions and grant code permissions.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		///  -or-  
		///  <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.  
		///  -or-  
		///  The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.  
		///  -or-  
		///  The assembly name or code base is invalid.</exception>
		// Token: 0x06000752 RID: 1874 RVA: 0x00019B30 File Offset: 0x00017D30
		[SecuritySafeCritical]
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo, ref stackCrawlMark);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is <see langword="null" />, the executing assembly is searched.</param>
		/// <param name="typeName">The fully qualified name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		///  -or-  
		///  <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.  
		///  -or-  
		///  The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.  
		///  -or-  
		///  The assembly name or code base is invalid.</exception>
		// Token: 0x06000753 RID: 1875 RVA: 0x00019B54 File Offset: 0x00017D54
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null, ref stackCrawlMark);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00019B78 File Offset: 0x00017D78
		[SecurityCritical]
		internal static ObjectHandle CreateInstance(string assemblyString, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo, ref StackCrawlMark stackMark)
		{
			if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			Type type = null;
			Assembly assembly = null;
			if (assemblyString == null)
			{
				assembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
			}
			else
			{
				RuntimeAssembly runtimeAssembly;
				AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out runtimeAssembly);
				if (runtimeAssembly != null)
				{
					assembly = runtimeAssembly;
				}
				else if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
				{
					type = Type.GetType(typeName + ", " + assemblyString, true, ignoreCase);
				}
				else
				{
					assembly = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, securityInfo, null, ref stackMark, true, false, false);
				}
			}
			if (type == null)
			{
				if (assembly == null)
				{
					return null;
				}
				type = assembly.GetType(typeName, true, ignoreCase);
			}
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and the constructor that best matches the specified parameters.</summary>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <param name="securityInfo">Information used to make security policy decisions and grant code permissions.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Security.Permissions.FileIOPermission" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		// Token: 0x06000755 RID: 1877 RVA: 0x00019C3C File Offset: 0x00017E3C
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo);
		}

		/// <summary>Creates an instance of the type whose name is specified, using the named assembly file and the constructor that best matches the specified parameters.</summary>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Security.Permissions.FileIOPermission" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		// Token: 0x06000756 RID: 1878 RVA: 0x00019C7C File Offset: 0x00017E7C
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00019C9C File Offset: 0x00017E9C
		private static ObjectHandle CreateInstanceFromInternal(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, securityInfo);
			Type type = assembly.GetType(typeName, true, ignoreCase);
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly and default constructor.</summary>
		/// <param name="domain">The remote domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is <see langword="null" />, the executing assembly is searched.</param>
		/// <param name="typeName">The fully qualified name of the preferred type.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> or <paramref name="domain" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract type.  
		///  -or-  
		///  This member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.  
		///  -or-  
		///  The assembly name or code base is invalid.</exception>
		// Token: 0x06000758 RID: 1880 RVA: 0x00019CD9 File Offset: 0x00017ED9
		[SecurityCritical]
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <param name="domain">The domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is <see langword="null" />, the executing assembly is searched.</param>
		/// <param name="typeName">The fully qualified name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <param name="securityAttributes">Information used to make security policy decisions and grant code permissions.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> or <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		///  -or-  
		///  <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.  
		///  -or-  
		///  The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.  
		///  -or-  
		///  The assembly name or code base is invalid.</exception>
		// Token: 0x06000759 RID: 1881 RVA: 0x00019CF4 File Offset: 0x00017EF4
		[SecurityCritical]
		[Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly and the constructor that best matches the specified parameters.</summary>
		/// <param name="domain">The domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyName">The name of the assembly where the type named <paramref name="typeName" /> is sought. If <paramref name="assemblyName" /> is <see langword="null" />, the executing assembly is searched.</param>
		/// <param name="typeName">The fully qualified name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> or <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Overload:System.Type.GetTypeFromProgID" /> or <see cref="Overload:System.Type.GetTypeFromCLSID" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" />, and <see cref="T:System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.  
		///  -or-  
		///  <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.  
		///  -or-  
		///  The constructor that best matches <paramref name="args" /> has <see langword="varargs" /> arguments.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.  
		///  -or-  
		///  The assembly name or code base is invalid.</exception>
		// Token: 0x0600075A RID: 1882 RVA: 0x00019D44 File Offset: 0x00017F44
		[SecurityCritical]
		public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly file and default constructor.</summary>
		/// <param name="domain">The remote domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> or <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does have the required <see cref="T:System.Security.Permissions.FileIOPermission" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		// Token: 0x0600075B RID: 1883 RVA: 0x00019D73 File Offset: 0x00017F73
		[SecurityCritical]
		public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly file and the constructor that best matches the specified parameters.</summary>
		/// <param name="domain">The remote domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <param name="securityAttributes">Information used to make security policy decisions and grant code permissions.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> or <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does have the required <see cref="T:System.Security.Permissions.FileIOPermission" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// The common language runtime (CLR) version 2.0 or later is currently loaded, and <paramref name="assemblyName" /> was compiled for a version of the CLR that is later than the currently loaded version. Note that the .NET Framework versions 2.0, 3.0, and 3.5 all use CLR version 2.0.</exception>
		// Token: 0x0600075C RID: 1884 RVA: 0x00019D8C File Offset: 0x00017F8C
		[SecurityCritical]
		[Obsolete("Methods which use Evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates an instance of the type whose name is specified in the specified remote domain, using the named assembly file and the constructor that best matches the specified parameters.</summary>
		/// <param name="domain">The remote domain where the type named <paramref name="typeName" /> is created.</param>
		/// <param name="assemblyFile">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that the search for <paramref name="typeName" /> is not case-sensitive; <see langword="false" /> to specify that the search is case-sensitive.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that uses <paramref name="bindingAttr" /> and <paramref name="args" /> to seek and identify the <paramref name="typeName" /> constructor. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. This is typically an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  
		///  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> or <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyFile" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The constructor, which was invoked through reflection, threw an exception.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does have the required <see cref="T:System.Security.Permissions.FileIOPermission" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="activationAttributes" /> is not an empty array, and the type being created does not derive from <see cref="T:System.MarshalByRefObject" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// <paramref name="assemblyName" /> was compiled for a version of the common language runtime that is later than the version that is currently loaded.</exception>
		// Token: 0x0600075D RID: 1885 RVA: 0x00019DDC File Offset: 0x00017FDC
		[SecurityCritical]
		public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		/// <summary>Creates an instance of the type designated by the specified <see cref="T:System.ActivationContext" /> object.</summary>
		/// <param name="activationContext">An activation context object that specifies the object to create.</param>
		/// <returns>A handle that must be unwrapped to access the newly created object.</returns>
		// Token: 0x0600075E RID: 1886 RVA: 0x00019E0C File Offset: 0x0001800C
		[SecuritySafeCritical]
		public static ObjectHandle CreateInstance(ActivationContext activationContext)
		{
			AppDomainManager appDomainManager = AppDomain.CurrentDomain.DomainManager;
			if (appDomainManager == null)
			{
				appDomainManager = new AppDomainManager();
			}
			return appDomainManager.ApplicationActivator.CreateInstance(activationContext);
		}

		/// <summary>Creates an instance of the type that is designated by the specified <see cref="T:System.ActivationContext" /> object and activated with the specified custom activation data.</summary>
		/// <param name="activationContext">An activation context object that specifies the object to create.</param>
		/// <param name="activationCustomData">An array of Unicode strings that contain custom activation data.</param>
		/// <returns>A handle that must be unwrapped to access the newly created object.</returns>
		// Token: 0x0600075F RID: 1887 RVA: 0x00019E3C File Offset: 0x0001803C
		[SecuritySafeCritical]
		public static ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
		{
			AppDomainManager appDomainManager = AppDomain.CurrentDomain.DomainManager;
			if (appDomainManager == null)
			{
				appDomainManager = new AppDomainManager();
			}
			return appDomainManager.ApplicationActivator.CreateInstance(activationContext, activationCustomData);
		}

		/// <summary>Creates an instance of the COM object whose name is specified, using the named assembly file and the default constructor.</summary>
		/// <param name="assemblyName">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> or <paramref name="assemblyName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">An instance cannot be created through COM.  
		///  -or-  
		///  <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> is not found, or the module you are trying to load does not specify a file name extension.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class.  
		///  -or-  
		///  This member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is the empty string ("").</exception>
		// Token: 0x06000760 RID: 1888 RVA: 0x00019E6A File Offset: 0x0001806A
		public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
		{
			return Activator.CreateComInstanceFrom(assemblyName, typeName, null, AssemblyHashAlgorithm.None);
		}

		/// <summary>Creates an instance of the COM object whose name is specified, using the named assembly file and the default constructor.</summary>
		/// <param name="assemblyName">The name of a file that contains an assembly where the type named <paramref name="typeName" /> is sought.</param>
		/// <param name="typeName">The name of the preferred type.</param>
		/// <param name="hashValue">The value of the computed hash code.</param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and generating the strong name.</param>
		/// <returns>A handle that must be unwrapped to access the newly created instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> or <paramref name="assemblyName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is the empty string ("").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">An assembly or module was loaded twice with two different evidences.
		///  -or- 
		///  <paramref name="assemblyName" /> is longer than the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> is not found, or the module you are trying to load does not specify a file name extension.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyName" /> is found but cannot be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.Security.SecurityException">A code base that does not start with "file://" was specified without the required <see langword="WebPermission" />.</exception>
		/// <exception cref="T:System.TypeLoadException">An instance cannot be created through COM.  
		///  -or-  
		///  <paramref name="typename" /> was not found in <paramref name="assemblyName" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.MemberAccessException">An instance of an abstract class cannot be created.  
		///  -or-  
		///  This member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />.</exception>
		// Token: 0x06000761 RID: 1889 RVA: 0x00019E78 File Offset: 0x00018078
		public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyName, hashValue, hashAlgorithm);
			Type type = assembly.GetType(typeName, true, false);
			object[] customAttributes = type.GetCustomAttributes(typeof(ComVisibleAttribute), false);
			if (customAttributes.Length != 0 && !((ComVisibleAttribute)customAttributes[0]).Value)
			{
				throw new TypeLoadException(Environment.GetResourceString("Argument_TypeMustBeVisibleFromCom"));
			}
			if (assembly == null)
			{
				return null;
			}
			object obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		/// <summary>Creates a proxy for the well-known object indicated by the specified type and URL.</summary>
		/// <param name="type">The type of the well-known object to which you want to connect.</param>
		/// <param name="url">The URL of the well-known object.</param>
		/// <returns>A proxy that points to an endpoint served by the requested well-known object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="url" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="type" /> is not marshaled by reference and is not an interface.</exception>
		/// <exception cref="T:System.MemberAccessException">This member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06000762 RID: 1890 RVA: 0x00019EF7 File Offset: 0x000180F7
		[SecurityCritical]
		public static object GetObject(Type type, string url)
		{
			return Activator.GetObject(type, url, null);
		}

		/// <summary>Creates a proxy for the well-known object indicated by the specified type, URL, and channel data.</summary>
		/// <param name="type">The type of the well-known object to which you want to connect.</param>
		/// <param name="url">The URL of the well-known object.</param>
		/// <param name="state">Channel-specific data or <see langword="null" />.</param>
		/// <returns>A proxy that points to an endpoint served by the requested well-known object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="url" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="type" /> is not marshaled by reference and is not an interface.</exception>
		/// <exception cref="T:System.MemberAccessException">This member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06000763 RID: 1891 RVA: 0x00019F01 File Offset: 0x00018101
		[SecurityCritical]
		public static object GetObject(Type type, string url, object state)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return RemotingServices.Connect(type, url, state);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00019F1F File Offset: 0x0001811F
		[Conditional("_DEBUG")]
		private static void Log(bool test, string title, string success, string failure)
		{
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">When this method returns, contains a pointer to a location that receives the number of type information interfaces provided by the object. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000765 RID: 1893 RVA: 0x00019F23 File Offset: 0x00018123
		void _Activator.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">An object that receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000766 RID: 1894 RVA: 0x00019F2A File Offset: 0x0001812A
		void _Activator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">The passed-in array of names to map.</param>
		/// <param name="cNames">The count of the names to map.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">The caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000767 RID: 1895 RVA: 0x00019F31 File Offset: 0x00018131
		void _Activator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">A dispatch identifier that identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">A pointer to a structure that contains an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">A pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">A pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000768 RID: 1896 RVA: 0x00019F38 File Offset: 0x00018138
		void _Activator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000374 RID: 884
		internal const int LookupMask = 255;

		// Token: 0x04000375 RID: 885
		internal const BindingFlags ConLookup = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x04000376 RID: 886
		internal const BindingFlags ConstructorDefault = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
	}
}
