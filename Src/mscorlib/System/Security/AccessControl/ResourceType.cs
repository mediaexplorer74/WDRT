using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the defined native object types.</summary>
	// Token: 0x020001FB RID: 507
	public enum ResourceType
	{
		/// <summary>An unknown object type.</summary>
		// Token: 0x04000AAE RID: 2734
		Unknown,
		/// <summary>A file or directory.</summary>
		// Token: 0x04000AAF RID: 2735
		FileObject,
		/// <summary>A Windows service.</summary>
		// Token: 0x04000AB0 RID: 2736
		Service,
		/// <summary>A printer.</summary>
		// Token: 0x04000AB1 RID: 2737
		Printer,
		/// <summary>A registry key.</summary>
		// Token: 0x04000AB2 RID: 2738
		RegistryKey,
		/// <summary>A network share.</summary>
		// Token: 0x04000AB3 RID: 2739
		LMShare,
		/// <summary>A local kernel object.</summary>
		// Token: 0x04000AB4 RID: 2740
		KernelObject,
		/// <summary>A window station or desktop object on the local computer.</summary>
		// Token: 0x04000AB5 RID: 2741
		WindowObject,
		/// <summary>A directory service (DS) object or a property set or property of a directory service object.</summary>
		// Token: 0x04000AB6 RID: 2742
		DSObject,
		/// <summary>A directory service object and all of its property sets and properties.</summary>
		// Token: 0x04000AB7 RID: 2743
		DSObjectAll,
		/// <summary>An object defined by a provider.</summary>
		// Token: 0x04000AB8 RID: 2744
		ProviderDefined,
		/// <summary>A Windows Management Instrumentation (WMI) object.</summary>
		// Token: 0x04000AB9 RID: 2745
		WmiGuidObject,
		/// <summary>An object for a registry entry under WOW64.</summary>
		// Token: 0x04000ABA RID: 2746
		RegistryWow6432Key
	}
}
