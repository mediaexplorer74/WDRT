using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
	/// <summary>The exception that is thrown when a time zone cannot be found.</summary>
	// Token: 0x02000147 RID: 327
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x060013E6 RID: 5094 RVA: 0x0003BEED File Offset: 0x0003A0ED
		public TimeZoneNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x060013E7 RID: 5095 RVA: 0x0003BEF6 File Offset: 0x0003A0F6
		public TimeZoneNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class from serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="context" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060013E8 RID: 5096 RVA: 0x0003BF00 File Offset: 0x0003A100
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class with a system-supplied message.</summary>
		// Token: 0x060013E9 RID: 5097 RVA: 0x0003BF0A File Offset: 0x0003A10A
		public TimeZoneNotFoundException()
		{
		}
	}
}
