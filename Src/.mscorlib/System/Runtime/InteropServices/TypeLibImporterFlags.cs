using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how an assembly should be produced.</summary>
	// Token: 0x02000969 RID: 2409
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibImporterFlags
	{
		/// <summary>No special settings. This is the default.</summary>
		// Token: 0x04002BA5 RID: 11173
		None = 0,
		/// <summary>Generates a primary interop assembly. For more information, see the <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> attribute. A keyfile must be specified.</summary>
		// Token: 0x04002BA6 RID: 11174
		PrimaryInteropAssembly = 1,
		/// <summary>Imports all interfaces as interfaces that suppress the common language runtime's stack crawl for <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> permission. Be sure you understand the responsibilities associated with suppressing this security check.</summary>
		// Token: 0x04002BA7 RID: 11175
		UnsafeInterfaces = 2,
		/// <summary>Imports all <see langword="SAFEARRAY" /> instances as <see cref="T:System.Array" /> instead of typed, single-dimensional, zero-based managed arrays. This option is useful when dealing with multi-dimensional, non-zero-based <see langword="SAFEARRAY" /> instances, which otherwise cannot be accessed unless you edit the resulting assembly by using the MSIL Disassembler (Ildasm.exe) and MSIL Assembler (Ilasm.exe) tools.</summary>
		// Token: 0x04002BA8 RID: 11176
		SafeArrayAsSystemArray = 4,
		/// <summary>Transforms <see langword="[out, retval]" /> parameters of methods on dispatch-only interfaces (dispinterface) into return values.</summary>
		// Token: 0x04002BA9 RID: 11177
		TransformDispRetVals = 8,
		/// <summary>Not used.</summary>
		// Token: 0x04002BAA RID: 11178
		PreventClassMembers = 16,
		/// <summary>Uses serializable classes.</summary>
		// Token: 0x04002BAB RID: 11179
		SerializableValueClasses = 32,
		/// <summary>Imports a type library for the x86 platform.</summary>
		// Token: 0x04002BAC RID: 11180
		ImportAsX86 = 256,
		/// <summary>Imports a type library for the x86 64-bit platform.</summary>
		// Token: 0x04002BAD RID: 11181
		ImportAsX64 = 512,
		/// <summary>Imports a type library for the Itanium platform.</summary>
		// Token: 0x04002BAE RID: 11182
		ImportAsItanium = 1024,
		/// <summary>Imports a type library for any platform.</summary>
		// Token: 0x04002BAF RID: 11183
		ImportAsAgnostic = 2048,
		/// <summary>Uses reflection-only loading.</summary>
		// Token: 0x04002BB0 RID: 11184
		ReflectionOnlyLoading = 4096,
		/// <summary>Prevents inclusion of a version resource in the interop assembly. For more information, see the <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource" /> method.</summary>
		// Token: 0x04002BB1 RID: 11185
		NoDefineVersionResource = 8192,
		/// <summary>Imports a library for the ARM platform.</summary>
		// Token: 0x04002BB2 RID: 11186
		ImportAsArm = 16384
	}
}
