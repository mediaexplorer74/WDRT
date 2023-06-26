﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	/// <summary>Receives the error code, number of bytes, and overlapped value type when an I/O operation completes on the thread pool.</summary>
	/// <param name="errorCode">The error code.</param>
	/// <param name="numBytes">The number of bytes that are transferred.</param>
	/// <param name="pOVERLAP">A <see cref="T:System.Threading.NativeOverlapped" /> representing an unmanaged pointer to the native overlapped value type.</param>
	// Token: 0x02000523 RID: 1315
	// (Invoke) Token: 0x06003E0A RID: 15882
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public unsafe delegate void IOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP);
}
