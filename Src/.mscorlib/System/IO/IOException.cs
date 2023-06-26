using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when an I/O error occurs.</summary>
	// Token: 0x02000197 RID: 407
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class IOException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to the empty string (""), its HRESULT set to COR_E_IO, and its inner exception set to a null reference.</summary>
		// Token: 0x060018D0 RID: 6352 RVA: 0x0005112E File Offset: 0x0004F32E
		[__DynamicallyInvokable]
		public IOException()
			: base(Environment.GetResourceString("Arg_IOException"))
		{
			base.SetErrorCode(-2146232800);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to <paramref name="message" />, its HRESULT set to COR_E_IO, and its inner exception set to <see langword="null" />.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060018D1 RID: 6353 RVA: 0x0005114B File Offset: 0x0004F34B
		[__DynamicallyInvokable]
		public IOException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232800);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to <paramref name="message" /> and its HRESULT user-defined.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="hresult">An integer identifying the error that has occurred.</param>
		// Token: 0x060018D2 RID: 6354 RVA: 0x0005115F File Offset: 0x0004F35F
		[__DynamicallyInvokable]
		public IOException(string message, int hresult)
			: base(message)
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0005116F File Offset: 0x0004F36F
		internal IOException(string message, int hresult, string maybeFullPath)
			: base(message)
		{
			base.SetErrorCode(hresult);
			this._maybeFullPath = maybeFullPath;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060018D4 RID: 6356 RVA: 0x00051186 File Offset: 0x0004F386
		[__DynamicallyInvokable]
		public IOException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232800);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the object.</param>
		/// <param name="context">The source and destination for the object.</param>
		// Token: 0x060018D5 RID: 6357 RVA: 0x0005119B File Offset: 0x0004F39B
		protected IOException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040008B3 RID: 2227
		[NonSerialized]
		private string _maybeFullPath;
	}
}
