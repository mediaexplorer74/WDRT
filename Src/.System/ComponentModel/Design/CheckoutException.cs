using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>The exception that is thrown when an attempt to check out a file that is checked into a source code management program is canceled or fails.</summary>
	// Token: 0x020005C9 RID: 1481
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CheckoutException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with no associated message or error code.</summary>
		// Token: 0x06003745 RID: 14149 RVA: 0x000EFACA File Offset: 0x000EDCCA
		public CheckoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message.</summary>
		/// <param name="message">A message describing the exception.</param>
		// Token: 0x06003746 RID: 14150 RVA: 0x000EFAD2 File Offset: 0x000EDCD2
		public CheckoutException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message and error code.</summary>
		/// <param name="message">A message describing the exception.</param>
		/// <param name="errorCode">The error code to pass.</param>
		// Token: 0x06003747 RID: 14151 RVA: 0x000EFADB File Offset: 0x000EDCDB
		public CheckoutException(string message, int errorCode)
			: base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06003748 RID: 14152 RVA: 0x000EFAE5 File Offset: 0x000EDCE5
		protected CheckoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06003749 RID: 14153 RVA: 0x000EFAEF File Offset: 0x000EDCEF
		public CheckoutException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class that specifies that the check out was canceled. This field is read-only.</summary>
		// Token: 0x04002AD1 RID: 10961
		public static readonly CheckoutException Canceled = new CheckoutException(SR.GetString("CHECKOUTCanceled"), -2147467260);
	}
}
