using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a field that does not exist. If a field in a class library has been removed or renamed, recompile any assemblies that reference that library.</summary>
	// Token: 0x0200010F RID: 271
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MissingFieldException : MissingMemberException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class.</summary>
		// Token: 0x06001057 RID: 4183 RVA: 0x000310D5 File Offset: 0x0002F2D5
		[__DynamicallyInvokable]
		public MissingFieldException()
			: base(Environment.GetResourceString("Arg_MissingFieldException"))
		{
			base.SetErrorCode(-2146233071);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06001058 RID: 4184 RVA: 0x000310F2 File Offset: 0x0002F2F2
		[__DynamicallyInvokable]
		public MissingFieldException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233071);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001059 RID: 4185 RVA: 0x00031106 File Offset: 0x0002F306
		[__DynamicallyInvokable]
		public MissingFieldException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233071);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600105A RID: 4186 RVA: 0x0003111B File Offset: 0x0002F31B
		protected MissingFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the text string showing the signature of the missing field, the class name, and the field name. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00031128 File Offset: 0x0002F328
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
				return Environment.GetResourceString("MissingField_Name", new object[] { ((this.Signature != null) ? (MissingMemberException.FormatSignature(this.Signature) + " ") : "") + this.ClassName + "." + this.MemberName });
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00031191 File Offset: 0x0002F391
		private MissingFieldException(string className, string fieldName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
			this.Signature = signature;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with the specified class name and field name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent field was attempted.</param>
		/// <param name="fieldName">The name of the field that cannot be accessed.</param>
		// Token: 0x0600105D RID: 4189 RVA: 0x000311AE File Offset: 0x0002F3AE
		public MissingFieldException(string className, string fieldName)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
		}
	}
}
