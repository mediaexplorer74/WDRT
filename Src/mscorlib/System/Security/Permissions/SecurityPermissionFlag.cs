using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies access flags for the security permission object.</summary>
	// Token: 0x02000304 RID: 772
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SecurityPermissionFlag
	{
		/// <summary>No security access.</summary>
		// Token: 0x04000F2C RID: 3884
		NoFlags = 0,
		/// <summary>Ability to assert that all this code's callers have the requisite permission for the operation.</summary>
		// Token: 0x04000F2D RID: 3885
		Assertion = 1,
		/// <summary>Ability to call unmanaged code.</summary>
		// Token: 0x04000F2E RID: 3886
		UnmanagedCode = 2,
		/// <summary>Ability to skip verification of code in this assembly. Code that is unverifiable can be run if this permission is granted.</summary>
		// Token: 0x04000F2F RID: 3887
		SkipVerification = 4,
		/// <summary>Permission for the code to run. Without this permission, managed code will not be executed.</summary>
		// Token: 0x04000F30 RID: 3888
		Execution = 8,
		/// <summary>Ability to use certain advanced operations on threads.</summary>
		// Token: 0x04000F31 RID: 3889
		ControlThread = 16,
		/// <summary>Ability to provide evidence, including the ability to alter the evidence provided by the common language runtime.</summary>
		// Token: 0x04000F32 RID: 3890
		ControlEvidence = 32,
		/// <summary>Ability to view and modify policy.</summary>
		// Token: 0x04000F33 RID: 3891
		ControlPolicy = 64,
		/// <summary>Ability to provide serialization services. Used by serialization formatters.</summary>
		// Token: 0x04000F34 RID: 3892
		SerializationFormatter = 128,
		/// <summary>Ability to specify domain policy.</summary>
		// Token: 0x04000F35 RID: 3893
		ControlDomainPolicy = 256,
		/// <summary>Ability to manipulate the principal object.</summary>
		// Token: 0x04000F36 RID: 3894
		ControlPrincipal = 512,
		/// <summary>Ability to create and manipulate an <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x04000F37 RID: 3895
		ControlAppDomain = 1024,
		/// <summary>Permission to configure Remoting types and channels.</summary>
		// Token: 0x04000F38 RID: 3896
		RemotingConfiguration = 2048,
		/// <summary>Permission to plug code into the common language runtime infrastructure, such as adding Remoting Context Sinks, Envoy Sinks and Dynamic Sinks.</summary>
		// Token: 0x04000F39 RID: 3897
		Infrastructure = 4096,
		/// <summary>Permission to perform explicit binding redirection in the application configuration file. This includes redirection of .NET Framework assemblies that have been unified as well as other assemblies found outside the .NET Framework.</summary>
		// Token: 0x04000F3A RID: 3898
		BindingRedirects = 8192,
		/// <summary>The unrestricted state of the permission.</summary>
		// Token: 0x04000F3B RID: 3899
		AllFlags = 16383
	}
}
