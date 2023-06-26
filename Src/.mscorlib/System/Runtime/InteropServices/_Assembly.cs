using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.Assembly" /> class to unmanaged code.</summary>
	// Token: 0x02000901 RID: 2305
	[Guid("17156360-2f1a-384a-bc52-fde93c215c5b")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[TypeLibImportClass(typeof(Assembly))]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _Assembly
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.ToString" /> method.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
		// Token: 0x06005EEA RID: 24298
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <param name="other">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005EEB RID: 24299
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06005EEC RID: 24300
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x06005EED RID: 24301
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.CodeBase" /> property.</summary>
		/// <returns>The location of the assembly as specified originally.</returns>
		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06005EEE RID: 24302
		string CodeBase { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.EscapedCodeBase" /> property.</summary>
		/// <returns>A Uniform Resource Identifier (URI) with escape characters.</returns>
		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06005EEF RID: 24303
		string EscapedCodeBase { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetName" /> method.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</returns>
		// Token: 0x06005EF0 RID: 24304
		AssemblyName GetName();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetName(System.Boolean)" /> method.</summary>
		/// <param name="copiedName">
		///   <see langword="true" /> to set the <see cref="P:System.Reflection.Assembly.CodeBase" /> to the location of the assembly after it was shadow copied; <see langword="false" /> to set <see cref="P:System.Reflection.Assembly.CodeBase" /> to the original location.</param>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</returns>
		// Token: 0x06005EF1 RID: 24305
		AssemblyName GetName(bool copiedName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.FullName" /> property.</summary>
		/// <returns>The display name of the assembly.</returns>
		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06005EF2 RID: 24306
		string FullName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.EntryPoint" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the entry point of this assembly. If no entry point is found (for example, the assembly is a DLL), <see langword="null" /> is returned.</returns>
		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06005EF3 RID: 24307
		MethodInfo EntryPoint { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetType(System.String)" /> method.</summary>
		/// <param name="name">The full name of the type.</param>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class, or <see langword="null" /> if the class is not found.</returns>
		// Token: 0x06005EF4 RID: 24308
		Type GetType(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean)" /> method.</summary>
		/// <param name="name">The full name of the type.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type is not found; <see langword="false" /> to return <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class.</returns>
		// Token: 0x06005EF5 RID: 24309
		Type GetType(string name, bool throwOnError);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetExportedTypes" /> property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the types defined in this assembly that are visible outside the assembly.</returns>
		// Token: 0x06005EF6 RID: 24310
		Type[] GetExportedTypes();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetTypes" /> method.</summary>
		/// <returns>An array of type <see cref="T:System.Type" /> containing objects for all the types defined in this assembly.</returns>
		// Token: 0x06005EF7 RID: 24311
		Type[] GetTypes();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.Type,System.String)" /> method.</summary>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name.</param>
		/// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		// Token: 0x06005EF8 RID: 24312
		Stream GetManifestResourceStream(Type type, string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.String)" /> method.</summary>
		/// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		// Token: 0x06005EF9 RID: 24313
		Stream GetManifestResourceStream(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetFile(System.String)" /> method.</summary>
		/// <param name="name">The name of the specified file. Do not include the path to the file.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> for the specified file, or <see langword="null" /> if the file is not found.</returns>
		// Token: 0x06005EFA RID: 24314
		FileStream GetFile(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetFiles" /> method.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		// Token: 0x06005EFB RID: 24315
		FileStream[] GetFiles();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetFiles(System.Boolean)" /> method.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		// Token: 0x06005EFC RID: 24316
		FileStream[] GetFiles(bool getResourceModules);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" /> method.</summary>
		/// <returns>An array of type <see langword="String" /> containing the names of all the resources.</returns>
		// Token: 0x06005EFD RID: 24317
		string[] GetManifestResourceNames();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetManifestResourceInfo(System.String)" /> method.</summary>
		/// <param name="resourceName">The case-sensitive name of the resource.</param>
		/// <returns>A <see cref="T:System.Reflection.ManifestResourceInfo" /> object populated with information about the resource's topology, or <see langword="null" /> if the resource is not found.</returns>
		// Token: 0x06005EFE RID: 24318
		ManifestResourceInfo GetManifestResourceInfo(string resourceName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.Location" /> property.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file was shadow-copied, the location is that of the file after being shadow-copied. If the assembly is loaded from a byte array, such as when using the <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> method overload, the value returned is an empty string ("").</returns>
		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06005EFF RID: 24319
		string Location { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.Evidence" /> property.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object for this assembly.</returns>
		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06005F00 RID: 24320
		Evidence Evidence { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <param name="attributeType">The <see cref="T:System.Type" /> for which the custom attributes are to be returned.</param>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />.</param>
		/// <returns>An array of type <see cref="T:System.Object" /> containing the custom attributes for this assembly as specified by <paramref name="attributeType" />.</returns>
		// Token: 0x06005F01 RID: 24321
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />.</param>
		/// <returns>An array of type <see langword="Object" /> containing the custom attributes for this assembly.</returns>
		// Token: 0x06005F02 RID: 24322
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.IsDefined(System.Type,System.Boolean)" /> method.</summary>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the custom attribute to be checked for this assembly.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>
		///   <see langword="true" /> if a custom attribute identified by the specified <see cref="T:System.Type" /> is defined; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005F03 RID: 24323
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06005F04 RID: 24324
		[SecurityCritical]
		void GetObjectData(SerializationInfo info, StreamingContext context);

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.Reflection.Assembly.ModuleResolve" /> event.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06005F05 RID: 24325
		// (remove) Token: 0x06005F06 RID: 24326
		event ModuleResolveEventHandler ModuleResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean,System.Boolean)" /> method.</summary>
		/// <param name="name">The full name of the type.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type is not found; <see langword="false" /> to return <see langword="null" />.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of the type name; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class.</returns>
		// Token: 0x06005F07 RID: 24327
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo)" /> method.</summary>
		/// <param name="culture">The specified culture.</param>
		/// <returns>The specified satellite assembly.</returns>
		// Token: 0x06005F08 RID: 24328
		Assembly GetSatelliteAssembly(CultureInfo culture);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo,System.Version)" /> method.</summary>
		/// <param name="culture">The specified culture.</param>
		/// <param name="version">The version of the satellite assembly.</param>
		/// <returns>The specified satellite assembly.</returns>
		// Token: 0x06005F09 RID: 24329
		Assembly GetSatelliteAssembly(CultureInfo culture, Version version);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[])" /> method.</summary>
		/// <param name="moduleName">Name of the module. Must correspond to a file name in this assembly's manifest.</param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource.</param>
		/// <returns>The loaded Module.</returns>
		// Token: 0x06005F0A RID: 24330
		Module LoadModule(string moduleName, byte[] rawModule);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[],System.Byte[])" /> method.</summary>
		/// <param name="moduleName">Name of the module. Must correspond to a file name in this assembly's manifest.</param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource.</param>
		/// <param name="rawSymbolStore">A byte array containing the raw bytes representing the symbols for the module. Must be <see langword="null" /> if this is a resource file.</param>
		/// <returns>The loaded module.</returns>
		// Token: 0x06005F0B RID: 24331
		Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.CreateInstance(System.String)" /> method.</summary>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate.</param>
		/// <returns>An instance of <see cref="T:System.Object" /> representing the type, with culture, arguments, binder, and activation attributes set to <see langword="null" />, and <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance, or <see langword="null" /> if <paramref name="typeName" /> is not found.</returns>
		// Token: 0x06005F0C RID: 24332
		object CreateInstance(string typeName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean)" /> method.</summary>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of the type name; otherwise, <see langword="false" />.</param>
		/// <returns>An instance of <see cref="T:System.Object" /> representing the type, with culture, arguments, binder, and activation attributes set to <see langword="null" />, and <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance, or <see langword="null" /> if <paramref name="typeName" /> is not found.</returns>
		// Token: 0x06005F0D RID: 24333
		object CreateInstance(string typeName, bool ignoreCase);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[])" /> method.</summary>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of the type name; otherwise, <see langword="false" />.</param>
		/// <param name="bindingAttr">A bitmask that affects how the search is conducted. The value is a combination of bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array of type <see langword="Object" /> containing the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the default constructor is desired, <paramref name="args" /> must be an empty array or <see langword="null" />.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see langword="CultureInfo" /> for the current thread is used. (This is necessary to convert a <see langword="String" /> that represents 1000 to a <see langword="Double" /> value, for example, since 1000 is represented differently by different cultures.)</param>
		/// <param name="activationAttributes">An array of type <see langword="Object" /> containing one or more activation attributes that can participate in the activation. An example of an activation attribute is:  
		///  URLAttribute(http://hostname/appname/objectURI)</param>
		/// <returns>An instance of <see langword="Object" /> representing the type and matching the specified criteria, or <see langword="null" /> if <paramref name="typeName" /> is not found.</returns>
		// Token: 0x06005F0E RID: 24334
		object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetLoadedModules" /> method.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x06005F0F RID: 24335
		Module[] GetLoadedModules();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetLoadedModules(System.Boolean)" /> method.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>An array of modules.</returns>
		// Token: 0x06005F10 RID: 24336
		Module[] GetLoadedModules(bool getResourceModules);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetModules" /> method.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x06005F11 RID: 24337
		Module[] GetModules();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetModules(System.Boolean)" /> method.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>An array of modules.</returns>
		// Token: 0x06005F12 RID: 24338
		Module[] GetModules(bool getResourceModules);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetModule(System.String)" /> method.</summary>
		/// <param name="name">The name of the module being requested.</param>
		/// <returns>The module being requested, or <see langword="null" /> if the module is not found.</returns>
		// Token: 0x06005F13 RID: 24339
		Module GetModule(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetReferencedAssemblies" /> method.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.AssemblyName" /> containing all the assemblies referenced by this assembly.</returns>
		// Token: 0x06005F14 RID: 24340
		AssemblyName[] GetReferencedAssemblies();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.Assembly.GlobalAssemblyCache" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the assembly was loaded from the global assembly cache; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06005F15 RID: 24341
		bool GlobalAssemblyCache { get; }
	}
}
