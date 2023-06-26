using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;
using System.Security.Principal;

namespace System
{
	/// <summary>Exposes the public members of the <see cref="T:System.AppDomain" /> class to unmanaged code.</summary>
	// Token: 0x0200009D RID: 157
	[Guid("05F696DC-2B29-3663-AD8B-C4389CF2A713")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _AppDomain
	{
		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060008A6 RID: 2214
		void GetTypeInfoCount(out uint pcTInfo);

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060008A7 RID: 2215
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060008A8 RID: 2216
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

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
		// Token: 0x060008A9 RID: 2217
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ToString" /> method.</summary>
		/// <returns>A string formed by concatenating the literal string "Name:", the friendly name of the application domain, and either string representations of the context policies or the string "There are no context policies."</returns>
		// Token: 0x060008AA RID: 2218
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the inherited <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <param name="other">The <see cref="T:System.Object" /> to compare to the current <see cref="T:System.Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060008AB RID: 2219
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the inherited <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060008AC RID: 2220
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the current instance.</returns>
		// Token: 0x060008AD RID: 2221
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.InitializeLifetimeService" /> method.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x060008AE RID: 2222
		[SecurityCritical]
		object InitializeLifetimeService();

		/// <summary>Provides COM objects with version-independent access to the inherited <see cref="M:System.MarshalByRefObject.GetLifetimeService" /> method.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance.</returns>
		// Token: 0x060008AF RID: 2223
		[SecurityCritical]
		object GetLifetimeService();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.Evidence" /> property.</summary>
		/// <returns>Gets the <see cref="T:System.Security.Policy.Evidence" /> associated with this application domain that is used as input to the security policy.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060008B0 RID: 2224
		Evidence Evidence { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.DomainUnload" /> event.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060008B1 RID: 2225
		// (remove) Token: 0x060008B2 RID: 2226
		event EventHandler DomainUnload;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.AssemblyLoad" /> event.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060008B3 RID: 2227
		// (remove) Token: 0x060008B4 RID: 2228
		event AssemblyLoadEventHandler AssemblyLoad;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.ProcessExit" /> event.</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060008B5 RID: 2229
		// (remove) Token: 0x060008B6 RID: 2230
		event EventHandler ProcessExit;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.TypeResolve" /> event.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060008B7 RID: 2231
		// (remove) Token: 0x060008B8 RID: 2232
		event ResolveEventHandler TypeResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.ResourceResolve" /> event.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060008B9 RID: 2233
		// (remove) Token: 0x060008BA RID: 2234
		event ResolveEventHandler ResourceResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.AssemblyResolve" /> event.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060008BB RID: 2235
		// (remove) Token: 0x060008BC RID: 2236
		event ResolveEventHandler AssemblyResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.UnhandledException" /> event.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060008BD RID: 2237
		// (remove) Token: 0x060008BE RID: 2238
		event UnhandledExceptionEventHandler UnhandledException;

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The access mode for the dynamic assembly.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008BF RID: 2239
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C0 RID: 2240
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C1 RID: 2241
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C2 RID: 2242
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C3 RID: 2243
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C4 RID: 2244
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C5 RID: 2245
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C6 RID: 2246
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet,System.Boolean)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <param name="isSynchronized">
		///   <see langword="true" /> to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, <see langword="false" />.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x060008C7 RID: 2247
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstance(System.String,System.String)" /> method.</summary>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x060008C8 RID: 2248
		ObjectHandle CreateInstance(string assemblyName, string typeName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String)" /> method overload.</summary>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <returns>An object that is a wrapper for the new instance, or <see langword="null" /> if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x060008C9 RID: 2249
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Object[])" /> method overload.</summary>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x060008CA RID: 2250
		ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Object[])" /> method overload.</summary>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <returns>An object that is a wrapper for the new instance, or <see langword="null" /> if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x060008CB RID: 2251
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />.</param>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x060008CC RID: 2252
		ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />.</param>
		/// <returns>An object that is a wrapper for the new instance, or <see langword="null" /> if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x060008CD RID: 2253
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName)" /> method overload.</summary>
		/// <param name="assemblyRef">An object that describes the assembly to load.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x060008CE RID: 2254
		Assembly Load(AssemblyName assemblyRef);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.String)" /> method overload.</summary>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x060008CF RID: 2255
		Assembly Load(string assemblyString);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Byte[])" /> method overload.</summary>
		/// <param name="rawAssembly">An array of type <see langword="byte" /> that is a COFF-based image containing an emitted assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x060008D0 RID: 2256
		Assembly Load(byte[] rawAssembly);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[])" /> method overload.</summary>
		/// <param name="rawAssembly">An array of type <see langword="byte" /> that is a COFF-based image containing an emitted assembly.</param>
		/// <param name="rawSymbolStore">An array of type <see langword="byte" /> containing the raw bytes representing the symbols for the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x060008D1 RID: 2257
		Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[],System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="rawAssembly">An array of type <see langword="byte" /> that is a COFF-based image containing an emitted assembly.</param>
		/// <param name="rawSymbolStore">An array of type <see langword="byte" /> containing the raw bytes representing the symbols for the assembly.</param>
		/// <param name="securityEvidence">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x060008D2 RID: 2258
		Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyRef">An object that describes the assembly to load.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x060008D3 RID: 2259
		Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.String,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x060008D4 RID: 2260
		Assembly Load(string assemblyString, Evidence assemblySecurity);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		// Token: 0x060008D5 RID: 2261
		int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" /> method overload.</summary>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		// Token: 0x060008D6 RID: 2262
		int ExecuteAssembly(string assemblyFile);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence,System.String[])" /> method overload.</summary>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="assemblySecurity">The supplied evidence for the assembly.</param>
		/// <param name="args">The arguments to the entry point of the assembly.</param>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		// Token: 0x060008D7 RID: 2263
		int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.FriendlyName" /> property.</summary>
		/// <returns>The friendly name of this application domain.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060008D8 RID: 2264
		string FriendlyName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.BaseDirectory" /> property.</summary>
		/// <returns>The base directory that the assembly resolver uses to probe for assemblies.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060008D9 RID: 2265
		string BaseDirectory { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.RelativeSearchPath" /> property.</summary>
		/// <returns>The path under the base directory where the assembly resolver should probe for private assemblies.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060008DA RID: 2266
		string RelativeSearchPath { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.ShadowCopyFiles" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the application domain is configured to shadow copy files; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060008DB RID: 2267
		bool ShadowCopyFiles { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.GetAssemblies" /> method.</summary>
		/// <returns>An array of assemblies in this application domain.</returns>
		// Token: 0x060008DC RID: 2268
		Assembly[] GetAssemblies();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.AppendPrivatePath(System.String)" /> method.</summary>
		/// <param name="path">The name of the directory to be appended to the private path.</param>
		// Token: 0x060008DD RID: 2269
		[SecurityCritical]
		void AppendPrivatePath(string path);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ClearPrivatePath" /> method.</summary>
		// Token: 0x060008DE RID: 2270
		[SecurityCritical]
		void ClearPrivatePath();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetShadowCopyPath(System.String)" /> method.</summary>
		/// <param name="s">A list of directory names, where each name is separated by a semicolon.</param>
		// Token: 0x060008DF RID: 2271
		[SecurityCritical]
		void SetShadowCopyPath(string s);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ClearShadowCopyPath" /> method.</summary>
		// Token: 0x060008E0 RID: 2272
		[SecurityCritical]
		void ClearShadowCopyPath();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetCachePath(System.String)" /> method.</summary>
		/// <param name="s">The fully qualified path to the shadow copy location.</param>
		// Token: 0x060008E1 RID: 2273
		[SecurityCritical]
		void SetCachePath(string s);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetData(System.String,System.Object)" /> method.</summary>
		/// <param name="name">The name of a user-defined application domain property to create or change.</param>
		/// <param name="data">The value of the property.</param>
		// Token: 0x060008E2 RID: 2274
		[SecurityCritical]
		void SetData(string name, object data);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.GetData(System.String)" /> method.</summary>
		/// <param name="name">The name of a predefined application domain property, or the name of an application domain property you have defined.</param>
		/// <returns>The value of the <paramref name="name" /> property.</returns>
		// Token: 0x060008E3 RID: 2275
		object GetData(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetAppDomainPolicy(System.Security.Policy.PolicyLevel)" /> method.</summary>
		/// <param name="domainPolicy">The security policy level.</param>
		// Token: 0x060008E4 RID: 2276
		[SecurityCritical]
		void SetAppDomainPolicy(PolicyLevel domainPolicy);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetThreadPrincipal(System.Security.Principal.IPrincipal)" /> method.</summary>
		/// <param name="principal">The principal object to attach to threads.</param>
		// Token: 0x060008E5 RID: 2277
		void SetThreadPrincipal(IPrincipal principal);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy)" /> method.</summary>
		/// <param name="policy">One of the <see cref="T:System.Security.Principal.PrincipalPolicy" /> values that specifies the type of the principal object to attach to threads.</param>
		// Token: 0x060008E6 RID: 2278
		void SetPrincipalPolicy(PrincipalPolicy policy);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> method.</summary>
		/// <param name="theDelegate">A delegate that specifies a method to call.</param>
		// Token: 0x060008E7 RID: 2279
		void DoCallBack(CrossAppDomainDelegate theDelegate);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.DynamicDirectory" /> property.</summary>
		/// <returns>Get the directory that the assembly resolver uses to probe for dynamically-created assemblies.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060008E8 RID: 2280
		string DynamicDirectory { get; }
	}
}
