using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when there is a syntax error in XML parsing. This class cannot be inherited.</summary>
	// Token: 0x020001C0 RID: 448
	[ComVisible(true)]
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with default properties.</summary>
		// Token: 0x06001C12 RID: 7186 RVA: 0x00060D4E File Offset: 0x0005EF4E
		public XmlSyntaxException()
			: base(Environment.GetResourceString("XMLSyntax_InvalidSyntax"))
		{
			base.SetErrorCode(-2146233320);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001C13 RID: 7187 RVA: 0x00060D6B File Offset: 0x0005EF6B
		public XmlSyntaxException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233320);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001C14 RID: 7188 RVA: 0x00060D7F File Offset: 0x0005EF7F
		public XmlSyntaxException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233320);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected.</param>
		// Token: 0x06001C15 RID: 7189 RVA: 0x00060D94 File Offset: 0x0005EF94
		public XmlSyntaxException(int lineNumber)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxError"), lineNumber))
		{
			base.SetErrorCode(-2146233320);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001C16 RID: 7190 RVA: 0x00060DC1 File Offset: 0x0005EFC1
		public XmlSyntaxException(int lineNumber, string message)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxErrorEx"), lineNumber, message))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00060DEF File Offset: 0x0005EFEF
		internal XmlSyntaxException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
