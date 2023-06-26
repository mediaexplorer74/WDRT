using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a set of services that convert a managed assembly to a COM type library and vice versa.</summary>
	// Token: 0x0200096F RID: 2415
	[Guid("F1C3BF78-C3E4-11d3-88E7-00902754C43A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibConverter
	{
		/// <summary>Converts a COM type library to an assembly.</summary>
		/// <param name="typeLib">The object that implements the <see langword="ITypeLib" /> interface.</param>
		/// <param name="asmFileName">The file name of the resulting assembly.</param>
		/// <param name="flags">A <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> value indicating any special settings.</param>
		/// <param name="notifySink">
		///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> interface implemented by the caller.</param>
		/// <param name="publicKey">A <see langword="byte" /> array containing the public key.</param>
		/// <param name="keyPair">A <see cref="T:System.Reflection.StrongNameKeyPair" /> object containing the public and private cryptographic key pair.</param>
		/// <param name="asmNamespace">The namespace for the resulting assembly.</param>
		/// <param name="asmVersion">The version of the resulting assembly. If <see langword="null" />, the version of the type library is used.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object containing the converted type library.</returns>
		// Token: 0x06006250 RID: 25168
		AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion);

		/// <summary>Converts an assembly to a COM type library.</summary>
		/// <param name="assembly">The assembly to convert.</param>
		/// <param name="typeLibName">The file name of the resulting type library.</param>
		/// <param name="flags">A <see cref="T:System.Runtime.InteropServices.TypeLibExporterFlags" /> value indicating any special settings.</param>
		/// <param name="notifySink">The <see cref="T:System.Runtime.InteropServices.ITypeLibExporterNotifySink" /> interface implemented by the caller.</param>
		/// <returns>An object that implements the <see langword="ITypeLib" /> interface.</returns>
		// Token: 0x06006251 RID: 25169
		[return: MarshalAs(UnmanagedType.Interface)]
		object ConvertAssemblyToTypeLib(Assembly assembly, string typeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

		/// <summary>Gets the name and code base of a primary interop assembly for a specified type library.</summary>
		/// <param name="g">The GUID of the type library.</param>
		/// <param name="major">The major version number of the type library.</param>
		/// <param name="minor">The minor version number of the type library.</param>
		/// <param name="lcid">The LCID of the type library.</param>
		/// <param name="asmName">On successful return, the name of the primary interop assembly associated with <paramref name="g" />.</param>
		/// <param name="asmCodeBase">On successful return, the code base of the primary interop assembly associated with <paramref name="g" />.</param>
		/// <returns>
		///   <see langword="true" /> if the primary interop assembly was found in the registry; otherwise <see langword="false" />.</returns>
		// Token: 0x06006252 RID: 25170
		bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase);

		/// <summary>Converts a COM type library to an assembly.</summary>
		/// <param name="typeLib">The object that implements the <see langword="ITypeLib" /> interface.</param>
		/// <param name="asmFileName">The file name of the resulting assembly.</param>
		/// <param name="flags">A <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> value indicating any special settings.</param>
		/// <param name="notifySink">
		///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> interface implemented by the caller.</param>
		/// <param name="publicKey">A <see langword="byte" /> array containing the public key.</param>
		/// <param name="keyPair">A <see cref="T:System.Reflection.StrongNameKeyPair" /> object containing the public and private cryptographic key pair.</param>
		/// <param name="unsafeInterfaces">If <see langword="true" />, the interfaces require link time checks for <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> permission. If <see langword="false" />, the interfaces require run time checks that require a stack walk and are more expensive, but help provide greater protection.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object containing the converted type library.</returns>
		// Token: 0x06006253 RID: 25171
		AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces);
	}
}
