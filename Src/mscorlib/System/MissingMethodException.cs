using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a method that does not exist.</summary>
	// Token: 0x02000111 RID: 273
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MissingMethodException : MissingMemberException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class.</summary>
		// Token: 0x06001067 RID: 4199 RVA: 0x00031374 File Offset: 0x0002F574
		[__DynamicallyInvokable]
		public MissingMethodException()
			: base(Environment.GetResourceString("Arg_MissingMethodException"))
		{
			base.SetErrorCode(-2146233069);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06001068 RID: 4200 RVA: 0x00031391 File Offset: 0x0002F591
		[__DynamicallyInvokable]
		public MissingMethodException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233069);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001069 RID: 4201 RVA: 0x000313A5 File Offset: 0x0002F5A5
		[__DynamicallyInvokable]
		public MissingMethodException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233069);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600106A RID: 4202 RVA: 0x000313BA File Offset: 0x0002F5BA
		protected MissingMethodException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the text string showing the class name, the method name, and the signature of the missing method. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000313C4 File Offset: 0x0002F5C4
		[__DynamicallyInvokable]
		public override string Message
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return Environment.GetResourceString("MissingMethod_Name", new object[] { this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : "") });
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003142D File Offset: 0x0002F62D
		private MissingMethodException(string className, string methodName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = methodName;
			this.Signature = signature;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with the specified class name and method name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent method was attempted.</param>
		/// <param name="methodName">The name of the method that cannot be accessed.</param>
		// Token: 0x0600106D RID: 4205 RVA: 0x0003144A File Offset: 0x0002F64A
		public MissingMethodException(string className, string methodName)
		{
			this.ClassName = className;
			this.MemberName = methodName;
		}
	}
}
