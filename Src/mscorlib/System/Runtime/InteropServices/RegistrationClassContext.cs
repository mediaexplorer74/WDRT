using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the set of execution contexts in which a class object will be made available for requests to construct instances.</summary>
	// Token: 0x02000972 RID: 2418
	[Flags]
	public enum RegistrationClassContext
	{
		/// <summary>The code that creates and manages objects of this class is a DLL that runs in the same process as the caller of the function specifying the class context.</summary>
		// Token: 0x04002BC3 RID: 11203
		InProcessServer = 1,
		/// <summary>The code that manages objects of this class is an in-process handler.</summary>
		// Token: 0x04002BC4 RID: 11204
		InProcessHandler = 2,
		/// <summary>The EXE code that creates and manages objects of this class runs on same machine but is loaded in a separate process space.</summary>
		// Token: 0x04002BC5 RID: 11205
		LocalServer = 4,
		/// <summary>Not used.</summary>
		// Token: 0x04002BC6 RID: 11206
		InProcessServer16 = 8,
		/// <summary>A remote machine context.</summary>
		// Token: 0x04002BC7 RID: 11207
		RemoteServer = 16,
		/// <summary>Not used.</summary>
		// Token: 0x04002BC8 RID: 11208
		InProcessHandler16 = 32,
		/// <summary>Not used.</summary>
		// Token: 0x04002BC9 RID: 11209
		Reserved1 = 64,
		/// <summary>Not used.</summary>
		// Token: 0x04002BCA RID: 11210
		Reserved2 = 128,
		/// <summary>Not used.</summary>
		// Token: 0x04002BCB RID: 11211
		Reserved3 = 256,
		/// <summary>Not used.</summary>
		// Token: 0x04002BCC RID: 11212
		Reserved4 = 512,
		/// <summary>Disallows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04002BCD RID: 11213
		NoCodeDownload = 1024,
		/// <summary>Not used.</summary>
		// Token: 0x04002BCE RID: 11214
		Reserved5 = 2048,
		/// <summary>Specifies whether activation fails if it uses custom marshaling.</summary>
		// Token: 0x04002BCF RID: 11215
		NoCustomMarshal = 4096,
		/// <summary>Allows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04002BD0 RID: 11216
		EnableCodeDownload = 8192,
		/// <summary>Overrides the logging of failures.</summary>
		// Token: 0x04002BD1 RID: 11217
		NoFailureLog = 16384,
		/// <summary>Disables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x04002BD2 RID: 11218
		DisableActivateAsActivator = 32768,
		/// <summary>Enables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x04002BD3 RID: 11219
		EnableActivateAsActivator = 65536,
		/// <summary>Begin this activation from the default context of the current apartment.</summary>
		// Token: 0x04002BD4 RID: 11220
		FromDefaultContext = 131072
	}
}
