using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes the exceptions that occur during <see langword="IDispatch::Invoke" />.</summary>
	// Token: 0x02000A46 RID: 2630
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		/// <summary>Represents an error code identifying the error.</summary>
		// Token: 0x04002DCE RID: 11726
		[__DynamicallyInvokable]
		public short wCode;

		/// <summary>This field is reserved; it must be set to 0.</summary>
		// Token: 0x04002DCF RID: 11727
		[__DynamicallyInvokable]
		public short wReserved;

		/// <summary>Indicates the name of the source of the exception. Typically, this is an application name.</summary>
		// Token: 0x04002DD0 RID: 11728
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		/// <summary>Describes the error intended for the customer.</summary>
		// Token: 0x04002DD1 RID: 11729
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		/// <summary>Contains the fully-qualified drive, path, and file name of a Help file that contains more information about the error.</summary>
		// Token: 0x04002DD2 RID: 11730
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		/// <summary>Indicates the Help context ID of the topic within the Help file.</summary>
		// Token: 0x04002DD3 RID: 11731
		[__DynamicallyInvokable]
		public int dwHelpContext;

		/// <summary>This field is reserved; it must be set to <see langword="null" />.</summary>
		// Token: 0x04002DD4 RID: 11732
		public IntPtr pvReserved;

		/// <summary>Represents a pointer to a function that takes an <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> structure as an argument and returns an HRESULT value. If deferred fill-in is not desired, this field is set to <see langword="null" />.</summary>
		// Token: 0x04002DD5 RID: 11733
		public IntPtr pfnDeferredFillIn;

		/// <summary>A return value describing the error.</summary>
		// Token: 0x04002DD6 RID: 11734
		[__DynamicallyInvokable]
		public int scode;
	}
}
