using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
	/// <summary>The exception that is thrown when time zone information is invalid.</summary>
	// Token: 0x02000102 RID: 258
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x06000FBA RID: 4026 RVA: 0x000302C1 File Offset: 0x0002E4C1
		[__DynamicallyInvokable]
		public InvalidTimeZoneException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000FBB RID: 4027 RVA: 0x000302CA File Offset: 0x0002E4CA
		[__DynamicallyInvokable]
		public InvalidTimeZoneException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class from serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="context" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000FBC RID: 4028 RVA: 0x000302D4 File Offset: 0x0002E4D4
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with a system-supplied message.</summary>
		// Token: 0x06000FBD RID: 4029 RVA: 0x000302DE File Offset: 0x0002E4DE
		[__DynamicallyInvokable]
		public InvalidTimeZoneException()
		{
		}
	}
}
