using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help debug your code.</summary>
	// Token: 0x02000495 RID: 1173
	[global::__DynamicallyInvokable]
	public static class Debug
	{
		/// <summary>Gets the collection of listeners that is monitoring the debug output.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> representing a collection of type <see cref="T:System.Diagnostics.TraceListener" /> that monitors the debug output.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000C4DAA File Offset: 0x000C2FAA
		public static TraceListenerCollection Listeners
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return TraceInternal.Listeners;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="M:System.Diagnostics.Debug.Flush" /> should be called on the <see cref="P:System.Diagnostics.Debug.Listeners" /> after every write.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.Diagnostics.Debug.Flush" /> is called on the <see cref="P:System.Diagnostics.Debug.Listeners" /> after every write; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000C4DB1 File Offset: 0x000C2FB1
		// (set) Token: 0x06002B5E RID: 11102 RVA: 0x000C4DB8 File Offset: 0x000C2FB8
		public static bool AutoFlush
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.AutoFlush;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.AutoFlush = value;
			}
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is 0.</returns>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x000C4DC0 File Offset: 0x000C2FC0
		// (set) Token: 0x06002B60 RID: 11104 RVA: 0x000C4DC7 File Offset: 0x000C2FC7
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.IndentLevel;
			}
			set
			{
				TraceInternal.IndentLevel = value;
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x000C4DCF File Offset: 0x000C2FCF
		// (set) Token: 0x06002B62 RID: 11106 RVA: 0x000C4DD6 File Offset: 0x000C2FD6
		public static int IndentSize
		{
			get
			{
				return TraceInternal.IndentSize;
			}
			set
			{
				TraceInternal.IndentSize = value;
			}
		}

		/// <summary>Flushes the output buffer and causes buffered data to write to the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		// Token: 0x06002B63 RID: 11107 RVA: 0x000C4DDE File Offset: 0x000C2FDE
		[Conditional("DEBUG")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		/// <summary>Flushes the output buffer and then calls the <see langword="Close" /> method on each of the <see cref="P:System.Diagnostics.Debug.Listeners" />.</summary>
		// Token: 0x06002B64 RID: 11108 RVA: 0x000C4DE5 File Offset: 0x000C2FE5
		[Conditional("DEBUG")]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Close()
		{
			TraceInternal.Close();
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, a failure message is not sent and the message box is not displayed.</param>
		// Token: 0x06002B65 RID: 11109 RVA: 0x000C4DEC File Offset: 0x000C2FEC
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs a specified message and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified message is not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x06002B66 RID: 11110 RVA: 0x000C4DF4 File Offset: 0x000C2FF4
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs two specified messages and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified messages are not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		/// <param name="detailMessage">The detailed message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x06002B67 RID: 11111 RVA: 0x000C4DFD File Offset: 0x000C2FFD
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs two messages (simple and formatted) and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified messages are not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		/// <param name="detailMessageFormat">The composite format string to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. This message contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x06002B68 RID: 11112 RVA: 0x000C4E07 File Offset: 0x000C3007
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Assert(bool condition, string message, string detailMessageFormat, params object[] args)
		{
			TraceInternal.Assert(condition, message, string.Format(CultureInfo.InvariantCulture, detailMessageFormat, args));
		}

		/// <summary>Emits the specified error message.</summary>
		/// <param name="message">A message to emit.</param>
		// Token: 0x06002B69 RID: 11113 RVA: 0x000C4E1C File Offset: 0x000C301C
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		/// <summary>Emits an error message and a detailed error message.</summary>
		/// <param name="message">A message to emit.</param>
		/// <param name="detailMessage">A detailed message to emit.</param>
		// Token: 0x06002B6A RID: 11114 RVA: 0x000C4E24 File Offset: 0x000C3024
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		/// <summary>Writes a message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x06002B6B RID: 11115 RVA: 0x000C4E2D File Offset: 0x000C302D
		[Conditional("DEBUG")]
		public static void Print(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes a formatted string followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="format">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		// Token: 0x06002B6C RID: 11116 RVA: 0x000C4E35 File Offset: 0x000C3035
		[Conditional("DEBUG")]
		public static void Print(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002B6D RID: 11117 RVA: 0x000C4E48 File Offset: 0x000C3048
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06002B6E RID: 11118 RVA: 0x000C4E50 File Offset: 0x000C3050
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B6F RID: 11119 RVA: 0x000C4E58 File Offset: 0x000C3058
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B70 RID: 11120 RVA: 0x000C4E61 File Offset: 0x000C3061
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		/// <summary>Writes a message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002B71 RID: 11121 RVA: 0x000C4E6A File Offset: 0x000C306A
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06002B72 RID: 11122 RVA: 0x000C4E72 File Offset: 0x000C3072
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B73 RID: 11123 RVA: 0x000C4E7A File Offset: 0x000C307A
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B74 RID: 11124 RVA: 0x000C4E83 File Offset: 0x000C3083
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		/// <summary>Writes a formatted message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="format">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x06002B75 RID: 11125 RVA: 0x000C4E8C File Offset: 0x000C308C
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLine(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the message is written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002B76 RID: 11126 RVA: 0x000C4E9F File Offset: 0x000C309F
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the value is written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06002B77 RID: 11127 RVA: 0x000C4EA8 File Offset: 0x000C30A8
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the category name and message are written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B78 RID: 11128 RVA: 0x000C4EB1 File Offset: 0x000C30B1
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the category name and value are written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B79 RID: 11129 RVA: 0x000C4EBB File Offset: 0x000C30BB
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the message is written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002B7A RID: 11130 RVA: 0x000C4EC5 File Offset: 0x000C30C5
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the value is written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06002B7B RID: 11131 RVA: 0x000C4ECE File Offset: 0x000C30CE
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B7C RID: 11132 RVA: 0x000C4ED7 File Offset: 0x000C30D7
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the category name and value are written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002B7D RID: 11133 RVA: 0x000C4EE1 File Offset: 0x000C30E1
		[Conditional("DEBUG")]
		[global::__DynamicallyInvokable]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		/// <summary>Increases the current <see cref="P:System.Diagnostics.Debug.IndentLevel" /> by one.</summary>
		// Token: 0x06002B7E RID: 11134 RVA: 0x000C4EEB File Offset: 0x000C30EB
		[Conditional("DEBUG")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		/// <summary>Decreases the current <see cref="P:System.Diagnostics.Debug.IndentLevel" /> by one.</summary>
		// Token: 0x06002B7F RID: 11135 RVA: 0x000C4EF2 File Offset: 0x000C30F2
		[Conditional("DEBUG")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}
	}
}
