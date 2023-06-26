using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Thrown when a thread on which an operation should execute no longer exists or has no message loop.</summary>
	// Token: 0x02000571 RID: 1393
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class InvalidAsynchronousStateException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class.</summary>
		// Token: 0x060033C4 RID: 13252 RVA: 0x000E3A34 File Offset: 0x000E1C34
		public InvalidAsynchronousStateException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description.</summary>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x060033C5 RID: 13253 RVA: 0x000E3A3D File Offset: 0x000E1C3D
		public InvalidAsynchronousStateException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x060033C6 RID: 13254 RVA: 0x000E3A46 File Offset: 0x000E1C46
		public InvalidAsynchronousStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060033C7 RID: 13255 RVA: 0x000E3A50 File Offset: 0x000E1C50
		protected InvalidAsynchronousStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
