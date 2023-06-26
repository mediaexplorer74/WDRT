using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when one of the arguments provided to a method is not valid.</summary>
	// Token: 0x020000A6 RID: 166
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArgumentException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class.</summary>
		// Token: 0x06000999 RID: 2457 RVA: 0x0001F374 File Offset: 0x0001D574
		[__DynamicallyInvokable]
		public ArgumentException()
			: base(Environment.GetResourceString("Arg_ArgumentException"))
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600099A RID: 2458 RVA: 0x0001F391 File Offset: 0x0001D591
		[__DynamicallyInvokable]
		public ArgumentException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600099B RID: 2459 RVA: 0x0001F3A5 File Offset: 0x0001D5A5
		[__DynamicallyInvokable]
		public ArgumentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600099C RID: 2460 RVA: 0x0001F3BA File Offset: 0x0001D5BA
		[__DynamicallyInvokable]
		public ArgumentException(string message, string paramName, Exception innerException)
			: base(message, innerException)
		{
			this.m_paramName = paramName;
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		// Token: 0x0600099D RID: 2461 RVA: 0x0001F3D6 File Offset: 0x0001D5D6
		[__DynamicallyInvokable]
		public ArgumentException(string message, string paramName)
			: base(message)
		{
			this.m_paramName = paramName;
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600099E RID: 2462 RVA: 0x0001F3F1 File Offset: 0x0001D5F1
		protected ArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_paramName = info.GetString("ParamName");
		}

		/// <summary>Gets the error message and the parameter name, or only the error message if no parameter name is set.</summary>
		/// <returns>A text string describing the details of the exception. The value of this property takes one of two forms:  
		///   Condition  
		///
		///   Value  
		///
		///   The <paramref name="paramName" /> is a null reference (<see langword="Nothing" /> in Visual Basic) or of zero length.  
		///
		///   The <paramref name="message" /> string passed to the constructor.  
		///
		///   The <paramref name="paramName" /> is not null reference (<see langword="Nothing" /> in Visual Basic) and it has a length greater than zero.  
		///
		///   The <paramref name="message" /> string appended with the name of the invalid parameter.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001F40C File Offset: 0x0001D60C
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				string message = base.Message;
				if (!string.IsNullOrEmpty(this.m_paramName))
				{
					string resourceString = Environment.GetResourceString("Arg_ParamName_Name", new object[] { this.m_paramName });
					return message + Environment.NewLine + resourceString;
				}
				return message;
			}
		}

		/// <summary>Gets the name of the parameter that causes this exception.</summary>
		/// <returns>The parameter name.</returns>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0001F455 File Offset: 0x0001D655
		[__DynamicallyInvokable]
		public virtual string ParamName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_paramName;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x060009A1 RID: 2465 RVA: 0x0001F45D File Offset: 0x0001D65D
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this.m_paramName, typeof(string));
		}

		// Token: 0x040003CD RID: 973
		private string m_paramName;
	}
}
