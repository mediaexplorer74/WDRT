using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a class member that does not exist or that is not declared as public. If a member in a class library has been removed or renamed, recompile any assemblies that reference that library.</summary>
	// Token: 0x02000110 RID: 272
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MissingMemberException : MemberAccessException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class.</summary>
		// Token: 0x0600105E RID: 4190 RVA: 0x000311C4 File Offset: 0x0002F3C4
		[__DynamicallyInvokable]
		public MissingMemberException()
			: base(Environment.GetResourceString("Arg_MissingMemberException"))
		{
			base.SetErrorCode(-2146233070);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x0600105F RID: 4191 RVA: 0x000311E1 File Offset: 0x0002F3E1
		[__DynamicallyInvokable]
		public MissingMemberException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233070);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with a specified error message and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">An instance of <see cref="T:System.Exception" /> that is the cause of the current <see langword="Exception" />. If <paramref name="inner" /> is not a null reference (<see langword="Nothing" /> in Visual Basic), then the current <see langword="Exception" /> is raised in a catch block handling <paramref name="inner" />.</param>
		// Token: 0x06001060 RID: 4192 RVA: 0x000311F5 File Offset: 0x0002F3F5
		[__DynamicallyInvokable]
		public MissingMemberException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233070);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001061 RID: 4193 RVA: 0x0003120C File Offset: 0x0002F40C
		protected MissingMemberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ClassName = info.GetString("MMClassName");
			this.MemberName = info.GetString("MMMemberName");
			this.Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
		}

		/// <summary>Gets the text string showing the class name, the member name, and the signature of the missing member.</summary>
		/// <returns>The error message string.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00031264 File Offset: 0x0002F464
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
				return Environment.GetResourceString("MissingMember_Name", new object[] { this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : "") });
			}
		}

		// Token: 0x06001063 RID: 4195
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string FormatSignature(byte[] signature);

		// Token: 0x06001064 RID: 4196 RVA: 0x000312CD File Offset: 0x0002F4CD
		private MissingMemberException(string className, string memberName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = memberName;
			this.Signature = signature;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with the specified class name and member name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent member was attempted.</param>
		/// <param name="memberName">The name of the member that cannot be accessed.</param>
		// Token: 0x06001065 RID: 4197 RVA: 0x000312EA File Offset: 0x0002F4EA
		public MissingMemberException(string className, string memberName)
		{
			this.ClassName = className;
			this.MemberName = memberName;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the class name, the member name, the signature of the missing member, and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is <see langword="null" />.</exception>
		// Token: 0x06001066 RID: 4198 RVA: 0x00031300 File Offset: 0x0002F500
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("MMClassName", this.ClassName, typeof(string));
			info.AddValue("MMMemberName", this.MemberName, typeof(string));
			info.AddValue("MMSignature", this.Signature, typeof(byte[]));
		}

		/// <summary>Holds the class name of the missing member.</summary>
		// Token: 0x040005C3 RID: 1475
		protected string ClassName;

		/// <summary>Holds the name of the missing member.</summary>
		// Token: 0x040005C4 RID: 1476
		protected string MemberName;

		/// <summary>Holds the signature of the missing member.</summary>
		// Token: 0x040005C5 RID: 1477
		protected byte[] Signature;
	}
}
