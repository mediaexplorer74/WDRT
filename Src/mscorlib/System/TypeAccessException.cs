using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method attempts to use a type that it does not have access to.</summary>
	// Token: 0x02000149 RID: 329
	[__DynamicallyInvokable]
	[Serializable]
	public class TypeAccessException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060014AD RID: 5293 RVA: 0x0003D4F2 File Offset: 0x0003B6F2
		[__DynamicallyInvokable]
		public TypeAccessException()
			: base(Environment.GetResourceString("Arg_TypeAccessException"))
		{
			base.SetErrorCode(-2146233021);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060014AE RID: 5294 RVA: 0x0003D50F File Offset: 0x0003B70F
		[__DynamicallyInvokable]
		public TypeAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233021);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must ensure that this string has been localized for the current system culture.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060014AF RID: 5295 RVA: 0x0003D523 File Offset: 0x0003B723
		[__DynamicallyInvokable]
		public TypeAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233021);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060014B0 RID: 5296 RVA: 0x0003D538 File Offset: 0x0003B738
		protected TypeAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			base.SetErrorCode(-2146233021);
		}
	}
}
