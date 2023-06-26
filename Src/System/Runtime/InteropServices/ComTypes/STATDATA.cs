using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="STATDATA" /> structure.</summary>
	// Token: 0x020003E5 RID: 997
	[global::__DynamicallyInvokable]
	public struct STATDATA
	{
		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure for the data of interest to the advise sink. The advise sink receives notification of changes to the data specified by this <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure.</summary>
		// Token: 0x04002083 RID: 8323
		[global::__DynamicallyInvokable]
		public FORMATETC formatetc;

		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> enumeration value that determines when the advisory sink is notified of changes in the data.</summary>
		// Token: 0x04002084 RID: 8324
		[global::__DynamicallyInvokable]
		public ADVF advf;

		/// <summary>Represents the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface that will receive change notifications.</summary>
		// Token: 0x04002085 RID: 8325
		[global::__DynamicallyInvokable]
		public IAdviseSink advSink;

		/// <summary>Represents the token that uniquely identifies the advisory connection. This token is returned by the method that sets up the advisory connection.</summary>
		// Token: 0x04002086 RID: 8326
		[global::__DynamicallyInvokable]
		public int connection;
	}
}
