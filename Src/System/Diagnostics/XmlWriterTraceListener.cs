using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output as XML-encoded data to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as a <see cref="T:System.IO.FileStream" />.</summary>
	// Token: 0x020004BA RID: 1210
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class XmlWriterTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class, using the specified stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the trace listener writes to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x06002D29 RID: 11561 RVA: 0x000CB540 File Offset: 0x000C9740
		public XmlWriterTraceListener(Stream stream)
			: base(stream)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the trace listener writes to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x06002D2A RID: 11562 RVA: 0x000CB554 File Offset: 0x000C9754
		public XmlWriterTraceListener(Stream stream, string name)
			: base(stream, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class using the specified writer as the recipient of the debugging and tracing output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the trace listener.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x06002D2B RID: 11563 RVA: 0x000CB569 File Offset: 0x000C9769
		public XmlWriterTraceListener(TextWriter writer)
			: base(writer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified writer as the recipient of the debugging and tracing output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the trace listener.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x06002D2C RID: 11564 RVA: 0x000CB57D File Offset: 0x000C977D
		public XmlWriterTraceListener(TextWriter writer, string name)
			: base(writer, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class, using the specified file as the recipient of the debugging and tracing output.</summary>
		/// <param name="filename">The name of the file to write to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> is <see langword="null" />.</exception>
		// Token: 0x06002D2D RID: 11565 RVA: 0x000CB592 File Offset: 0x000C9792
		public XmlWriterTraceListener(string filename)
			: base(filename)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified file as the recipient of the debugging and tracing output.</summary>
		/// <param name="filename">The name of the file to write to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x06002D2E RID: 11566 RVA: 0x000CB5A6 File Offset: 0x000C97A6
		public XmlWriterTraceListener(string filename, string name)
			: base(filename, name)
		{
		}

		/// <summary>Writes a verbatim message without any additional context information to the file or stream.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x06002D2F RID: 11567 RVA: 0x000CB5BB File Offset: 0x000C97BB
		public override void Write(string message)
		{
			this.WriteLine(message);
		}

		/// <summary>Writes a verbatim message without any additional context information followed by the current line terminator to the file or stream.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x06002D30 RID: 11568 RVA: 0x000CB5C4 File Offset: 0x000C97C4
		public override void WriteLine(string message)
		{
			this.TraceEvent(null, SR.GetString("TraceAsTraceSource"), TraceEventType.Information, 0, message);
		}

		/// <summary>Writes trace information including an error message and a detailed error message to the file or stream.</summary>
		/// <param name="message">The error message to write.</param>
		/// <param name="detailMessage">The detailed error message to append to the error message.</param>
		// Token: 0x06002D31 RID: 11569 RVA: 0x000CB5DC File Offset: 0x000C97DC
		public override void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.TraceEvent(null, SR.GetString("TraceAsTraceSource"), TraceEventType.Error, 0, stringBuilder.ToString());
		}

		/// <summary>Writes trace information, a formatted message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		// Token: 0x06002D32 RID: 11570 RVA: 0x000CB620 File Offset: 0x000C9820
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			string text;
			if (args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				text = format;
			}
			this.WriteEscaped(text);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The message to write.</param>
		// Token: 0x06002D33 RID: 11571 RVA: 0x000CB67D File Offset: 0x000C987D
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.WriteEscaped(message);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a data object, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">A data object to emit.</param>
		// Token: 0x06002D34 RID: 11572 RVA: 0x000CB6B8 File Offset: 0x000C98B8
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.InternalWrite("<TraceData>");
			if (data != null)
			{
				this.InternalWrite("<DataItem>");
				this.WriteData(data);
				this.InternalWrite("</DataItem>");
			}
			this.InternalWrite("</TraceData>");
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, data objects, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of data objects to emit.</param>
		// Token: 0x06002D35 RID: 11573 RVA: 0x000CB730 File Offset: 0x000C9930
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.InternalWrite("<TraceData>");
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					this.InternalWrite("<DataItem>");
					if (data[i] != null)
					{
						this.WriteData(data[i]);
					}
					this.InternalWrite("</DataItem>");
				}
			}
			this.InternalWrite("</TraceData>");
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000CB7C0 File Offset: 0x000C99C0
		private void WriteData(object data)
		{
			XPathNavigator xpathNavigator = data as XPathNavigator;
			if (xpathNavigator == null)
			{
				this.WriteEscaped(data.ToString());
				return;
			}
			if (this.strBldr == null)
			{
				this.strBldr = new StringBuilder();
				this.xmlBlobWriter = new XmlTextWriter(new StringWriter(this.strBldr, CultureInfo.CurrentCulture));
			}
			else
			{
				this.strBldr.Length = 0;
			}
			try
			{
				xpathNavigator.MoveToRoot();
				this.xmlBlobWriter.WriteNode(xpathNavigator, false);
				this.InternalWrite(this.strBldr.ToString());
			}
			catch (Exception)
			{
				this.InternalWrite(data.ToString());
			}
		}

		/// <summary>Closes the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> for this listener so that it no longer receives tracing or debugging output.</summary>
		// Token: 0x06002D37 RID: 11575 RVA: 0x000CB868 File Offset: 0x000C9A68
		public override void Close()
		{
			base.Close();
			if (this.xmlBlobWriter != null)
			{
				this.xmlBlobWriter.Close();
			}
			this.xmlBlobWriter = null;
			this.strBldr = null;
		}

		/// <summary>Writes trace information including the identity of a related activity, a message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A trace message to write.</param>
		/// <param name="relatedActivityId">A <see cref="T:System.Guid" /> structure that identifies a related activity.</param>
		// Token: 0x06002D38 RID: 11576 RVA: 0x000CB894 File Offset: 0x000C9A94
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			if (this.shouldRespectFilterOnTraceTransfer && base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, TraceEventType.Transfer, id, message))
			{
				return;
			}
			this.WriteHeader(source, TraceEventType.Transfer, id, eventCache, relatedActivityId);
			this.WriteEscaped(message);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000CB8E8 File Offset: 0x000C9AE8
		private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache, Guid relatedActivityId)
		{
			this.WriteStartHeader(source, eventType, id, eventCache);
			this.InternalWrite("\" RelatedActivityID=\"");
			this.InternalWrite(relatedActivityId.ToString("B"));
			this.WriteEndHeader(eventCache);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000CB91A File Offset: 0x000C9B1A
		private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
		{
			this.WriteStartHeader(source, eventType, id, eventCache);
			this.WriteEndHeader(eventCache);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000CB930 File Offset: 0x000C9B30
		private void WriteStartHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
		{
			this.InternalWrite("<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">");
			this.InternalWrite("<EventID>");
			uint num = (uint)id;
			this.InternalWrite(num.ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("</EventID>");
			this.InternalWrite("<Type>3</Type>");
			this.InternalWrite("<SubType Name=\"");
			this.InternalWrite(eventType.ToString());
			this.InternalWrite("\">0</SubType>");
			this.InternalWrite("<Level>");
			int num2 = (int)eventType;
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			this.InternalWrite(num2.ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("</Level>");
			this.InternalWrite("<TimeCreated SystemTime=\"");
			if (eventCache != null)
			{
				this.InternalWrite(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			else
			{
				this.InternalWrite(DateTime.Now.ToString("o", CultureInfo.InvariantCulture));
			}
			this.InternalWrite("\" />");
			this.InternalWrite("<Source Name=\"");
			this.WriteEscaped(source);
			this.InternalWrite("\" />");
			this.InternalWrite("<Correlation ActivityID=\"");
			if (eventCache != null)
			{
				this.InternalWrite(eventCache.ActivityId.ToString("B"));
				return;
			}
			this.InternalWrite(Guid.Empty.ToString("B"));
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000CBA9C File Offset: 0x000C9C9C
		private void WriteEndHeader(TraceEventCache eventCache)
		{
			this.InternalWrite("\" />");
			this.InternalWrite("<Execution ProcessName=\"");
			this.InternalWrite(TraceEventCache.GetProcessName());
			this.InternalWrite("\" ProcessID=\"");
			this.InternalWrite(((uint)TraceEventCache.GetProcessId()).ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("\" ThreadID=\"");
			if (eventCache != null)
			{
				this.WriteEscaped(eventCache.ThreadId.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				this.WriteEscaped(TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture));
			}
			this.InternalWrite("\" />");
			this.InternalWrite("<Channel/>");
			this.InternalWrite("<Computer>");
			this.InternalWrite(this.machineName);
			this.InternalWrite("</Computer>");
			this.InternalWrite("</System>");
			this.InternalWrite("<ApplicationData>");
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000CBB7C File Offset: 0x000C9D7C
		private void WriteFooter(TraceEventCache eventCache)
		{
			bool flag = base.IsEnabled(TraceOptions.LogicalOperationStack);
			bool flag2 = base.IsEnabled(TraceOptions.Callstack);
			if (eventCache != null && (flag || flag2))
			{
				this.InternalWrite("<System.Diagnostics xmlns=\"http://schemas.microsoft.com/2004/08/System.Diagnostics\">");
				if (flag)
				{
					this.InternalWrite("<LogicalOperationStack>");
					Stack logicalOperationStack = eventCache.LogicalOperationStack;
					if (logicalOperationStack != null)
					{
						foreach (object obj in logicalOperationStack)
						{
							this.InternalWrite("<LogicalOperation>");
							this.WriteEscaped(obj.ToString());
							this.InternalWrite("</LogicalOperation>");
						}
					}
					this.InternalWrite("</LogicalOperationStack>");
				}
				this.InternalWrite("<Timestamp>");
				this.InternalWrite(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
				this.InternalWrite("</Timestamp>");
				if (flag2)
				{
					this.InternalWrite("<Callstack>");
					this.WriteEscaped(eventCache.Callstack);
					this.InternalWrite("</Callstack>");
				}
				this.InternalWrite("</System.Diagnostics>");
			}
			this.InternalWrite("</ApplicationData></E2ETraceEvent>");
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000CBCA4 File Offset: 0x000C9EA4
		private void WriteEscaped(string str)
		{
			if (str == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (c <= '"')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c == '"')
							{
								this.InternalWrite(str.Substring(num, i - num));
								this.InternalWrite("&quot;");
								num = i + 1;
							}
						}
						else
						{
							this.InternalWrite(str.Substring(num, i - num));
							this.InternalWrite("&#xD;");
							num = i + 1;
						}
					}
					else
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&#xA;");
						num = i + 1;
					}
				}
				else if (c <= '\'')
				{
					if (c != '&')
					{
						if (c == '\'')
						{
							this.InternalWrite(str.Substring(num, i - num));
							this.InternalWrite("&apos;");
							num = i + 1;
						}
					}
					else
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&amp;");
						num = i + 1;
					}
				}
				else if (c != '<')
				{
					if (c == '>')
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&gt;");
						num = i + 1;
					}
				}
				else
				{
					this.InternalWrite(str.Substring(num, i - num));
					this.InternalWrite("&lt;");
					num = i + 1;
				}
			}
			this.InternalWrite(str.Substring(num, str.Length - num));
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000CBE21 File Offset: 0x000CA021
		private void InternalWrite(string message)
		{
			if (!base.EnsureWriter())
			{
				return;
			}
			this.writer.Write(message);
		}

		// Token: 0x040026F9 RID: 9977
		private const string fixedHeader = "<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">";

		// Token: 0x040026FA RID: 9978
		private readonly string machineName = Environment.MachineName;

		// Token: 0x040026FB RID: 9979
		private StringBuilder strBldr;

		// Token: 0x040026FC RID: 9980
		private XmlTextWriter xmlBlobWriter;

		// Token: 0x040026FD RID: 9981
		internal bool shouldRespectFilterOnTraceTransfer;
	}
}
