using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	/// <summary>The exception that is thrown when the key specified for accessing an element in a collection does not match any key in the collection.</summary>
	// Token: 0x020004D8 RID: 1240
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class KeyNotFoundException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class using default property values.</summary>
		// Token: 0x06003AFB RID: 15099 RVA: 0x000E11E8 File Offset: 0x000DF3E8
		[__DynamicallyInvokable]
		public KeyNotFoundException()
			: base(Environment.GetResourceString("Arg_KeyNotFound"))
		{
			base.SetErrorCode(-2146232969);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06003AFC RID: 15100 RVA: 0x000E1205 File Offset: 0x000DF405
		[__DynamicallyInvokable]
		public KeyNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232969);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003AFD RID: 15101 RVA: 0x000E1219 File Offset: 0x000DF419
		[__DynamicallyInvokable]
		public KeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232969);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06003AFE RID: 15102 RVA: 0x000E122E File Offset: 0x000DF42E
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
