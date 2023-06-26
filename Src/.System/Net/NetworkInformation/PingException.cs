using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> method calls a method that throws an exception.</summary>
	// Token: 0x020002EB RID: 747
	[Serializable]
	public class PingException : InvalidOperationException
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x0007F96D File Offset: 0x0007DB6D
		internal PingException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the contextual information about the source or destination for this serialization.</param>
		// Token: 0x06001A41 RID: 6721 RVA: 0x0007F975 File Offset: 0x0007DB75
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06001A42 RID: 6722 RVA: 0x0007F97F File Offset: 0x0007DB7F
		public PingException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		/// <param name="innerException">The exception that causes the current exception.</param>
		// Token: 0x06001A43 RID: 6723 RVA: 0x0007F988 File Offset: 0x0007DB88
		public PingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
