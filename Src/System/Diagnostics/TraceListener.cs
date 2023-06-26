using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides the <see langword="abstract" /> base class for the listeners who monitor trace and debug output.</summary>
	// Token: 0x020004B2 RID: 1202
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public abstract class TraceListener : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		// Token: 0x06002CAA RID: 11434 RVA: 0x000C924C File Offset: 0x000C744C
		protected TraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class using the specified name as the listener.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" />.</param>
		// Token: 0x06002CAB RID: 11435 RVA: 0x000C9262 File Offset: 0x000C7462
		protected TraceListener(string name)
		{
			this.listenerName = name;
		}

		/// <summary>Gets the custom trace listener attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace listener.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000C927F File Offset: 0x000C747F
		public StringDictionary Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets a name for this <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <returns>A name for this <see cref="T:System.Diagnostics.TraceListener" />. The default is an empty string ("").</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000C929A File Offset: 0x000C749A
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x000C92B0 File Offset: 0x000C74B0
		public virtual string Name
		{
			get
			{
				if (this.listenerName != null)
				{
					return this.listenerName;
				}
				return "";
			}
			set
			{
				this.listenerName = value;
			}
		}

		/// <summary>Gets a value indicating whether the trace listener is thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if the trace listener is thread safe; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000C92B9 File Offset: 0x000C74B9
		public virtual bool IsThreadSafe
		{
			get
			{
				return false;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		// Token: 0x06002CB0 RID: 11440 RVA: 0x000C92BC File Offset: 0x000C74BC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.TraceListener" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002CB1 RID: 11441 RVA: 0x000C92CB File Offset: 0x000C74CB
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, closes the output stream so it no longer receives tracing or debugging output.</summary>
		// Token: 0x06002CB2 RID: 11442 RVA: 0x000C92CD File Offset: 0x000C74CD
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, flushes the output buffer.</summary>
		// Token: 0x06002CB3 RID: 11443 RVA: 0x000C92CF File Offset: 0x000C74CF
		public virtual void Flush()
		{
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000C92D1 File Offset: 0x000C74D1
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x000C92D9 File Offset: 0x000C74D9
		public int IndentLevel
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = ((value < 0) ? 0 : value);
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four spaces.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is less than zero.</exception>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000C92E9 File Offset: 0x000C74E9
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x000C92F1 File Offset: 0x000C74F1
		public int IndentSize
		{
			get
			{
				return this.indentSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("IndentSize", value, SR.GetString("TraceListenerIndentSize"));
				}
				this.indentSize = value;
			}
		}

		/// <summary>Gets or sets the trace filter for the trace listener.</summary>
		/// <returns>An object derived from the <see cref="T:System.Diagnostics.TraceFilter" /> base class.</returns>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000C9319 File Offset: 0x000C7519
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000C9321 File Offset: 0x000C7521
		[ComVisible(false)]
		public TraceFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent the output.</summary>
		/// <returns>
		///   <see langword="true" /> if the output should be indented; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000C932A File Offset: 0x000C752A
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000C9332 File Offset: 0x000C7532
		protected bool NeedIndent
		{
			get
			{
				return this.needIndent;
			}
			set
			{
				this.needIndent = value;
			}
		}

		/// <summary>Gets or sets the trace output options.</summary>
		/// <returns>A bitwise combination of the enumeration values. The default is <see cref="F:System.Diagnostics.TraceOptions.None" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is invalid.</exception>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000C933B File Offset: 0x000C753B
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000C9343 File Offset: 0x000C7543
		[ComVisible(false)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return this.traceOptions;
			}
			set
			{
				if (value >> 6 != TraceOptions.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.traceOptions = value;
			}
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000C935C File Offset: 0x000C755C
		internal void SetAttributes(Hashtable attribs)
		{
			TraceUtils.VerifyAttributes(attribs, this.GetSupportedAttributes(), this);
			this.attributes = new StringDictionary();
			this.attributes.ReplaceHashtable(attribs);
		}

		/// <summary>Emits an error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit.</param>
		// Token: 0x06002CBF RID: 11455 RVA: 0x000C9382 File Offset: 0x000C7582
		public virtual void Fail(string message)
		{
			this.Fail(message, null);
		}

		/// <summary>Emits an error message and a detailed error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit.</param>
		/// <param name="detailMessage">A detailed message to emit.</param>
		// Token: 0x06002CC0 RID: 11456 RVA: 0x000C938C File Offset: 0x000C758C
		public virtual void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(SR.GetString("TraceListenerFail"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.WriteLine(stringBuilder.ToString());
		}

		/// <summary>Gets the custom attributes supported by the trace listener.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace listener, or <see langword="null" /> if there are no custom attributes.</returns>
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000C93E7 File Offset: 0x000C75E7
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, writes the specified message to the listener you create in the derived class.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002CC2 RID: 11458
		public abstract void Write(string message);

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		// Token: 0x06002CC3 RID: 11459 RVA: 0x000C93EA File Offset: 0x000C75EA
		public virtual void Write(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			if (o == null)
			{
				return;
			}
			this.Write(o.ToString());
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002CC4 RID: 11460 RVA: 0x000C9420 File Offset: 0x000C7620
		public virtual void Write(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.Write(message);
				return;
			}
			this.Write(category + ": " + ((message == null) ? string.Empty : message));
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002CC5 RID: 11461 RVA: 0x000C9474 File Offset: 0x000C7674
		public virtual void Write(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			if (category == null)
			{
				this.Write(o);
				return;
			}
			this.Write((o == null) ? "" : o.ToString(), category);
		}

		/// <summary>Writes the indent to the listener you create when you implement this class, and resets the <see cref="P:System.Diagnostics.TraceListener.NeedIndent" /> property to <see langword="false" />.</summary>
		// Token: 0x06002CC6 RID: 11462 RVA: 0x000C94C8 File Offset: 0x000C76C8
		protected virtual void WriteIndent()
		{
			this.NeedIndent = false;
			for (int i = 0; i < this.indentLevel; i++)
			{
				if (this.indentSize == 4)
				{
					this.Write("    ");
				}
				else
				{
					for (int j = 0; j < this.indentSize; j++)
					{
						this.Write(" ");
					}
				}
			}
		}

		/// <summary>When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002CC7 RID: 11463
		public abstract void WriteLine(string message);

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		// Token: 0x06002CC8 RID: 11464 RVA: 0x000C951F File Offset: 0x000C771F
		public virtual void WriteLine(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString());
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002CC9 RID: 11465 RVA: 0x000C955C File Offset: 0x000C775C
		public virtual void WriteLine(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.WriteLine(message);
				return;
			}
			this.WriteLine(category + ": " + ((message == null) ? string.Empty : message));
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06002CCA RID: 11466 RVA: 0x000C95B0 File Offset: 0x000C77B0
		public virtual void WriteLine(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString(), category);
		}

		/// <summary>Writes trace information, a data object and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data to emit.</param>
		// Token: 0x06002CCB RID: 11467 RVA: 0x000C95EC File Offset: 0x000C77EC
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			string text = string.Empty;
			if (data != null)
			{
				text = data.ToString();
			}
			this.WriteLine(text);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, an array of data objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of objects to emit as data.</param>
		// Token: 0x06002CCC RID: 11468 RVA: 0x000C9644 File Offset: 0x000C7844
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			StringBuilder stringBuilder = new StringBuilder();
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					if (data[i] != null)
					{
						stringBuilder.Append(data[i].ToString());
					}
				}
			}
			this.WriteLine(stringBuilder.ToString());
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		// Token: 0x06002CCD RID: 11469 RVA: 0x000C96CC File Offset: 0x000C78CC
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.TraceEvent(eventCache, source, eventType, id, string.Empty);
		}

		/// <summary>Writes trace information, a message, and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002CCE RID: 11470 RVA: 0x000C96DE File Offset: 0x000C78DE
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.WriteLine(message);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a formatted array of objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x06002CCF RID: 11471 RVA: 0x000C9718 File Offset: 0x000C7918
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			if (args != null)
			{
				this.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
			}
			else
			{
				this.WriteLine(format);
			}
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a message, a related activity identity and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="relatedActivityId">A <see cref="T:System.Guid" /> object identifying a related activity.</param>
		// Token: 0x06002CD0 RID: 11472 RVA: 0x000C9777 File Offset: 0x000C7977
		[ComVisible(false)]
		public virtual void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			this.TraceEvent(eventCache, source, TraceEventType.Transfer, id, message + ", relatedActivityId=" + relatedActivityId.ToString());
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000C97A0 File Offset: 0x000C79A0
		private void WriteHeader(string source, TraceEventType eventType, int id)
		{
			this.Write(string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", new object[]
			{
				source,
				eventType.ToString(),
				id.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000C97E0 File Offset: 0x000C79E0
		private void WriteFooter(TraceEventCache eventCache)
		{
			if (eventCache == null)
			{
				return;
			}
			this.indentLevel++;
			if (this.IsEnabled(TraceOptions.ProcessId))
			{
				this.WriteLine("ProcessId=" + eventCache.ProcessId.ToString());
			}
			if (this.IsEnabled(TraceOptions.LogicalOperationStack))
			{
				this.Write("LogicalOperationStack=");
				Stack logicalOperationStack = eventCache.LogicalOperationStack;
				bool flag = true;
				foreach (object obj in logicalOperationStack)
				{
					if (!flag)
					{
						this.Write(", ");
					}
					else
					{
						flag = false;
					}
					this.Write(obj.ToString());
				}
				this.WriteLine(string.Empty);
			}
			if (this.IsEnabled(TraceOptions.ThreadId))
			{
				this.WriteLine("ThreadId=" + eventCache.ThreadId);
			}
			if (this.IsEnabled(TraceOptions.DateTime))
			{
				this.WriteLine("DateTime=" + eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			if (this.IsEnabled(TraceOptions.Timestamp))
			{
				this.WriteLine("Timestamp=" + eventCache.Timestamp.ToString());
			}
			if (this.IsEnabled(TraceOptions.Callstack))
			{
				this.WriteLine("Callstack=" + eventCache.Callstack);
			}
			this.indentLevel--;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000C9954 File Offset: 0x000C7B54
		internal bool IsEnabled(TraceOptions opts)
		{
			return (opts & this.TraceOutputOptions) > TraceOptions.None;
		}

		// Token: 0x040026D6 RID: 9942
		private int indentLevel;

		// Token: 0x040026D7 RID: 9943
		private int indentSize = 4;

		// Token: 0x040026D8 RID: 9944
		private TraceOptions traceOptions;

		// Token: 0x040026D9 RID: 9945
		private bool needIndent = true;

		// Token: 0x040026DA RID: 9946
		private string listenerName;

		// Token: 0x040026DB RID: 9947
		private TraceFilter filter;

		// Token: 0x040026DC RID: 9948
		private StringDictionary attributes;

		// Token: 0x040026DD RID: 9949
		internal string initializeData;
	}
}
