using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown in <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> when the filter criteria is not valid for the type of filter you are using.</summary>
	// Token: 0x020005ED RID: 1517
	[ComVisible(true)]
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the default properties.</summary>
		// Token: 0x06004678 RID: 18040 RVA: 0x00103E01 File Offset: 0x00102001
		public InvalidFilterCriteriaException()
			: base(Environment.GetResourceString("Arg_InvalidFilterCriteriaException"))
		{
			base.SetErrorCode(-2146232831);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the given HRESULT and message string.</summary>
		/// <param name="message">The message text for the exception.</param>
		// Token: 0x06004679 RID: 18041 RVA: 0x00103E1E File Offset: 0x0010201E
		public InvalidFilterCriteriaException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232831);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600467A RID: 18042 RVA: 0x00103E32 File Offset: 0x00102032
		public InvalidFilterCriteriaException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232831);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">A <see langword="SerializationInfo" /> object that contains the information required to serialize this instance.</param>
		/// <param name="context">A <see langword="StreamingContext" /> object that contains the source and destination of the serialized stream associated with this instance.</param>
		// Token: 0x0600467B RID: 18043 RVA: 0x00103E47 File Offset: 0x00102047
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
