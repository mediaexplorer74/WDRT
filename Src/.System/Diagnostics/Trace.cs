using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help you trace the execution of your code. This class cannot be inherited.</summary>
	// Token: 0x020004AC RID: 1196
	public sealed class Trace
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		private Trace()
		{
		}

		/// <summary>Gets the collection of listeners that is monitoring the trace output.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that represents a collection of type <see cref="T:System.Diagnostics.TraceListener" /> monitoring the trace output.</returns>
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000C78C0 File Offset: 0x000C5AC0
		public static TraceListenerCollection Listeners
		{
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				return TraceInternal.Listeners;
			}
		}

		/// <summary>Gets or sets whether <see cref="M:System.Diagnostics.Trace.Flush" /> should be called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.Diagnostics.Trace.Flush" /> is called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000C78D2 File Offset: 0x000C5AD2
		// (set) Token: 0x06002C49 RID: 11337 RVA: 0x000C78D9 File Offset: 0x000C5AD9
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

		/// <summary>Gets or sets a value indicating whether the global lock should be used.</summary>
		/// <returns>
		///   <see langword="true" /> if the global lock is to be used; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000C78E1 File Offset: 0x000C5AE1
		// (set) Token: 0x06002C4B RID: 11339 RVA: 0x000C78E8 File Offset: 0x000C5AE8
		public static bool UseGlobalLock
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.UseGlobalLock;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.UseGlobalLock = value;
			}
		}

		/// <summary>Gets the correlation manager for the thread for this trace.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.CorrelationManager" /> object associated with the thread for this trace.</returns>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000C78F0 File Offset: 0x000C5AF0
		public static CorrelationManager CorrelationManager
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (Trace.correlationManager == null)
				{
					Trace.correlationManager = new CorrelationManager();
				}
				return Trace.correlationManager;
			}
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x000C790E File Offset: 0x000C5B0E
		// (set) Token: 0x06002C4E RID: 11342 RVA: 0x000C7915 File Offset: 0x000C5B15
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
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x000C791D File Offset: 0x000C5B1D
		// (set) Token: 0x06002C50 RID: 11344 RVA: 0x000C7924 File Offset: 0x000C5B24
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

		/// <summary>Flushes the output buffer, and causes buffered data to be written to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</summary>
		// Token: 0x06002C51 RID: 11345 RVA: 0x000C792C File Offset: 0x000C5B2C
		[Conditional("TRACE")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		/// <summary>Flushes the output buffer, and then closes the <see cref="P:System.Diagnostics.Trace.Listeners" />.</summary>
		// Token: 0x06002C52 RID: 11346 RVA: 0x000C7933 File Offset: 0x000C5B33
		[Conditional("TRACE")]
		public static void Close()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			TraceInternal.Close();
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, a failure message is not sent and the message box is not displayed.</param>
		// Token: 0x06002C53 RID: 11347 RVA: 0x000C7945 File Offset: 0x000C5B45
		[Conditional("TRACE")]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs a specified message and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified message is not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x06002C54 RID: 11348 RVA: 0x000C794D File Offset: 0x000C5B4D
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs two specified messages and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified messages are not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		/// <param name="detailMessage">The detailed message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x06002C55 RID: 11349 RVA: 0x000C7956 File Offset: 0x000C5B56
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		/// <summary>Emits the specified error message.</summary>
		/// <param name="message">A message to emit.</param>
		// Token: 0x06002C56 RID: 11350 RVA: 0x000C7960 File Offset: 0x000C5B60
		[Conditional("TRACE")]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		/// <summary>Emits an error message, and a detailed error message.</summary>
		/// <param name="message">A message to emit.</param>
		/// <param name="detailMessage">A detailed message to emit.</param>
		// Token: 0x06002C57 RID: 11351 RVA: 0x000C7968 File Offset: 0x000C5B68
		[Conditional("TRACE")]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		/// <summary>Refreshes the trace configuration data.</summary>
		// Token: 0x06002C58 RID: 11352 RVA: 0x000C7971 File Offset: 0x000C5B71
		public static void Refresh()
		{
			DiagnosticsConfiguration.Refresh();
			Switch.RefreshAll();
			TraceSource.RefreshAll();
			TraceInternal.Refresh();
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		// Token: 0x06002C59 RID: 11353 RVA: 0x000C7987 File Offset: 0x000C5B87
		[Conditional("TRACE")]
		public static void TraceInformation(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, message, null);
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x06002C5A RID: 11354 RVA: 0x000C7992 File Offset: 0x000C5B92
		[Conditional("TRACE")]
		public static void TraceInformation(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, format, args);
		}

		/// <summary>Writes a warning message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		// Token: 0x06002C5B RID: 11355 RVA: 0x000C799D File Offset: 0x000C5B9D
		[Conditional("TRACE")]
		public static void TraceWarning(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, message, null);
		}

		/// <summary>Writes a warning message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x06002C5C RID: 11356 RVA: 0x000C79A8 File Offset: 0x000C5BA8
		[Conditional("TRACE")]
		public static void TraceWarning(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, format, args);
		}

		/// <summary>Writes an error message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		// Token: 0x06002C5D RID: 11357 RVA: 0x000C79B3 File Offset: 0x000C5BB3
		[Conditional("TRACE")]
		public static void TraceError(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, message, null);
		}

		/// <summary>Writes an error message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x06002C5E RID: 11358 RVA: 0x000C79BE File Offset: 0x000C5BBE
		[Conditional("TRACE")]
		public static void TraceError(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, format, args);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002C5F RID: 11359 RVA: 0x000C79C9 File Offset: 0x000C5BC9
		[Conditional("TRACE")]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x06002C60 RID: 11360 RVA: 0x000C79D1 File Offset: 0x000C5BD1
		[Conditional("TRACE")]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		/// <summary>Writes a category name and a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C61 RID: 11361 RVA: 0x000C79D9 File Offset: 0x000C5BD9
		[Conditional("TRACE")]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C62 RID: 11362 RVA: 0x000C79E2 File Offset: 0x000C5BE2
		[Conditional("TRACE")]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002C63 RID: 11363 RVA: 0x000C79EB File Offset: 0x000C5BEB
		[Conditional("TRACE")]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x06002C64 RID: 11364 RVA: 0x000C79F3 File Offset: 0x000C5BF3
		[Conditional("TRACE")]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C65 RID: 11365 RVA: 0x000C79FB File Offset: 0x000C5BFB
		[Conditional("TRACE")]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C66 RID: 11366 RVA: 0x000C7A04 File Offset: 0x000C5C04
		[Conditional("TRACE")]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002C67 RID: 11367 RVA: 0x000C7A0D File Offset: 0x000C5C0D
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x06002C68 RID: 11368 RVA: 0x000C7A16 File Offset: 0x000C5C16
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C69 RID: 11369 RVA: 0x000C7A1F File Offset: 0x000C5C1F
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C6A RID: 11370 RVA: 0x000C7A29 File Offset: 0x000C5C29
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002C6B RID: 11371 RVA: 0x000C7A33 File Offset: 0x000C5C33
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x06002C6C RID: 11372 RVA: 0x000C7A3C File Offset: 0x000C5C3C
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C6D RID: 11373 RVA: 0x000C7A45 File Offset: 0x000C5C45
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002C6E RID: 11374 RVA: 0x000C7A4F File Offset: 0x000C5C4F
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		/// <summary>Increases the current <see cref="P:System.Diagnostics.Trace.IndentLevel" /> by one.</summary>
		// Token: 0x06002C6F RID: 11375 RVA: 0x000C7A59 File Offset: 0x000C5C59
		[Conditional("TRACE")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		/// <summary>Decreases the current <see cref="P:System.Diagnostics.Trace.IndentLevel" /> by one.</summary>
		// Token: 0x06002C70 RID: 11376 RVA: 0x000C7A60 File Offset: 0x000C5C60
		[Conditional("TRACE")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}

		// Token: 0x040026B5 RID: 9909
		private static volatile CorrelationManager correlationManager;
	}
}
