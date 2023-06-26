using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a unit of data is read from or written to an address that is not a multiple of the data size. This class cannot be inherited.</summary>
	// Token: 0x02000083 RID: 131
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DataMisalignedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class.</summary>
		// Token: 0x060006CF RID: 1743 RVA: 0x00017996 File Offset: 0x00015B96
		[__DynamicallyInvokable]
		public DataMisalignedException()
			: base(Environment.GetResourceString("Arg_DataMisalignedException"))
		{
			base.SetErrorCode(-2146233023);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class using the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060006D0 RID: 1744 RVA: 0x000179B3 File Offset: 0x00015BB3
		[__DynamicallyInvokable]
		public DataMisalignedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233023);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class using the specified error message and underlying exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current <see cref="T:System.DataMisalignedException" />. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060006D1 RID: 1745 RVA: 0x000179C7 File Offset: 0x00015BC7
		[__DynamicallyInvokable]
		public DataMisalignedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000179DC File Offset: 0x00015BDC
		internal DataMisalignedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
