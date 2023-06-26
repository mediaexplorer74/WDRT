using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides a simple listener that directs tracing or debugging output to an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	// Token: 0x020004D4 RID: 1236
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public sealed class EventLogTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class without a trace listener.</summary>
		// Token: 0x06002E85 RID: 11909 RVA: 0x000D1A1F File Offset: 0x000CFC1F
		public EventLogTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified event log.</summary>
		/// <param name="eventLog">The event log to write to.</param>
		// Token: 0x06002E86 RID: 11910 RVA: 0x000D1A27 File Offset: 0x000CFC27
		public EventLogTraceListener(EventLog eventLog)
			: base((eventLog != null) ? eventLog.Source : string.Empty)
		{
			this.eventLog = eventLog;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified source.</summary>
		/// <param name="source">The name of an existing event log source.</param>
		// Token: 0x06002E87 RID: 11911 RVA: 0x000D1A46 File Offset: 0x000CFC46
		public EventLogTraceListener(string source)
		{
			this.eventLog = new EventLog();
			this.eventLog.Source = source;
		}

		/// <summary>Gets or sets the event log to write to.</summary>
		/// <returns>The event log to write to.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000D1A65 File Offset: 0x000CFC65
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000D1A6D File Offset: 0x000CFC6D
		public EventLog EventLog
		{
			get
			{
				return this.eventLog;
			}
			set
			{
				this.eventLog = value;
			}
		}

		/// <summary>Gets or sets the name of this <see cref="T:System.Diagnostics.EventLogTraceListener" />.</summary>
		/// <returns>The name of this trace listener.</returns>
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000D1A76 File Offset: 0x000CFC76
		// (set) Token: 0x06002E8B RID: 11915 RVA: 0x000D1AA6 File Offset: 0x000CFCA6
		public override string Name
		{
			get
			{
				if (!this.nameSet && this.eventLog != null)
				{
					this.nameSet = true;
					base.Name = this.eventLog.Source;
				}
				return base.Name;
			}
			set
			{
				this.nameSet = true;
				base.Name = value;
			}
		}

		/// <summary>Closes the event log so that it no longer receives tracing or debugging output.</summary>
		// Token: 0x06002E8C RID: 11916 RVA: 0x000D1AB6 File Offset: 0x000CFCB6
		public override void Close()
		{
			if (this.eventLog != null)
			{
				this.eventLog.Close();
			}
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000D1ACC File Offset: 0x000CFCCC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Close();
				}
				else
				{
					if (this.eventLog != null)
					{
						this.eventLog.Close();
					}
					this.eventLog = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		// Token: 0x06002E8E RID: 11918 RVA: 0x000D1B18 File Offset: 0x000CFD18
		public override void Write(string message)
		{
			if (this.eventLog != null)
			{
				this.eventLog.WriteEntry(message);
			}
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		// Token: 0x06002E8F RID: 11919 RVA: 0x000D1B2E File Offset: 0x000CFD2E
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		/// <summary>Writes trace information, a formatted array of objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06002E90 RID: 11920 RVA: 0x000D1B38 File Offset: 0x000CFD38
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, format, args))
			{
				return;
			}
			EventInstance eventInstance = this.CreateEventInstance(severity, id);
			if (args == null)
			{
				this.eventLog.WriteEvent(eventInstance, new object[] { format });
				return;
			}
			if (string.IsNullOrEmpty(format))
			{
				string[] array = new string[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = args[i].ToString();
				}
				EventLog eventLog = this.eventLog;
				EventInstance eventInstance2 = eventInstance;
				object[] array2 = array;
				eventLog.WriteEvent(eventInstance2, array2);
				return;
			}
			this.eventLog.WriteEvent(eventInstance, new object[] { string.Format(CultureInfo.InvariantCulture, format, args) });
		}

		/// <summary>Writes trace information, a message, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="message">The trace message.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06002E91 RID: 11921 RVA: 0x000D1BEC File Offset: 0x000CFDEC
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, message))
			{
				return;
			}
			EventInstance eventInstance = this.CreateEventInstance(severity, id);
			this.eventLog.WriteEvent(eventInstance, new object[] { message });
		}

		/// <summary>Writes trace information, a data object, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">A data object to write to the output file or stream.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06002E92 RID: 11922 RVA: 0x000D1C38 File Offset: 0x000CFE38
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, null, null, data))
			{
				return;
			}
			EventInstance eventInstance = this.CreateEventInstance(severity, id);
			this.eventLog.WriteEvent(eventInstance, new object[] { data });
		}

		/// <summary>Writes trace information, an array of data objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">An array of data objects.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06002E93 RID: 11923 RVA: 0x000D1C88 File Offset: 0x000CFE88
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, null, null, null, data))
			{
				return;
			}
			EventInstance eventInstance = this.CreateEventInstance(severity, id);
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
			this.eventLog.WriteEvent(eventInstance, new object[] { stringBuilder.ToString() });
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000D1D18 File Offset: 0x000CFF18
		private EventInstance CreateEventInstance(TraceEventType severity, int id)
		{
			if (id > 65535)
			{
				id = 65535;
			}
			if (id < 0)
			{
				id = 0;
			}
			EventInstance eventInstance = new EventInstance((long)id, 0);
			if (severity == TraceEventType.Error || severity == TraceEventType.Critical)
			{
				eventInstance.EntryType = EventLogEntryType.Error;
			}
			else if (severity == TraceEventType.Warning)
			{
				eventInstance.EntryType = EventLogEntryType.Warning;
			}
			else
			{
				eventInstance.EntryType = EventLogEntryType.Information;
			}
			return eventInstance;
		}

		// Token: 0x04002769 RID: 10089
		private EventLog eventLog;

		// Token: 0x0400276A RID: 10090
		private bool nameSet;
	}
}
