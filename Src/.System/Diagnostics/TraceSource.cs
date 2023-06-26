using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that enable applications to trace the execution of code and associate trace messages with their source.</summary>
	// Token: 0x020004B6 RID: 1206
	public class TraceSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source.</summary>
		/// <param name="name">The name of the source (typically, the name of the application).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x06002CF9 RID: 11513 RVA: 0x000C9F05 File Offset: 0x000C8105
		public TraceSource(string name)
			: this(name, SourceLevels.Off)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source and the default source level at which tracing is to occur.</summary>
		/// <param name="name">The name of the source, typically the name of the application.</param>
		/// <param name="defaultLevel">A bitwise combination of the enumeration values that specifies the default source level at which to trace.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x06002CFA RID: 11514 RVA: 0x000C9F10 File Offset: 0x000C8110
		public TraceSource(string name, SourceLevels defaultLevel)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this.sourceName = name;
			this.switchLevel = defaultLevel;
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				TraceSource._pruneCachedTraceSources();
				TraceSource.tracesources.Add(new WeakReference(this));
			}
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000C9F98 File Offset: 0x000C8198
		private static void _pruneCachedTraceSources()
		{
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				if (TraceSource.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list2 = new List<WeakReference>(TraceSource.tracesources.Count);
					for (int i = 0; i < TraceSource.tracesources.Count; i++)
					{
						TraceSource traceSource = (TraceSource)TraceSource.tracesources[i].Target;
						if (traceSource != null)
						{
							list2.Add(TraceSource.tracesources[i]);
						}
					}
					if (list2.Count < TraceSource.tracesources.Count)
					{
						TraceSource.tracesources.Clear();
						TraceSource.tracesources.AddRange(list2);
						TraceSource.tracesources.TrimExcess();
					}
					TraceSource.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000CA070 File Offset: 0x000C8270
		private void Initialize()
		{
			if (!this._initCalled)
			{
				lock (this)
				{
					if (!this._initCalled)
					{
						SourceElementsCollection sources = DiagnosticsConfiguration.Sources;
						if (sources != null)
						{
							SourceElement sourceElement = sources[this.sourceName];
							if (sourceElement != null)
							{
								if (!string.IsNullOrEmpty(sourceElement.SwitchName))
								{
									this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
								}
								else
								{
									this.CreateSwitch(sourceElement.SwitchType, this.sourceName);
									if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
									{
										this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
									}
								}
								this.listeners = sourceElement.Listeners.GetRuntimeObject();
								this.attributes = new StringDictionary();
								TraceUtils.VerifyAttributes(sourceElement.Attributes, this.GetSupportedAttributes(), this);
								this.attributes.ReplaceHashtable(sourceElement.Attributes);
							}
							else
							{
								this.NoConfigInit();
							}
						}
						else
						{
							this.NoConfigInit();
						}
						this._initCalled = true;
					}
				}
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000CA1A8 File Offset: 0x000C83A8
		private void NoConfigInit()
		{
			this.internalSwitch = new SourceSwitch(this.sourceName, this.switchLevel.ToString());
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener());
			this.attributes = null;
		}

		/// <summary>Closes all the trace listeners in the trace listener collection.</summary>
		// Token: 0x06002CFE RID: 11518 RVA: 0x000CA204 File Offset: 0x000C8404
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Close()
		{
			if (this.listeners != null)
			{
				object critSec = TraceInternal.critSec;
				lock (critSec)
				{
					foreach (object obj in this.listeners)
					{
						TraceListener traceListener = (TraceListener)obj;
						traceListener.Close();
					}
				}
			}
		}

		/// <summary>Flushes all the trace listeners in the trace listener collection.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002CFF RID: 11519 RVA: 0x000CA294 File Offset: 0x000C8494
		public void Flush()
		{
			if (this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						using (IEnumerator enumerator = this.listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj;
								traceListener.Flush();
							}
							return;
						}
					}
				}
				foreach (object obj2 in this.listeners)
				{
					TraceListener traceListener2 = (TraceListener)obj2;
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.Flush();
							continue;
						}
					}
					traceListener2.Flush();
				}
			}
		}

		/// <summary>Gets the custom attributes supported by the trace source.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace source, or <see langword="null" /> if there are no custom attributes.</returns>
		// Token: 0x06002D00 RID: 11520 RVA: 0x000CA3BC File Offset: 0x000C85BC
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000CA3C0 File Offset: 0x000C85C0
		internal static void RefreshAll()
		{
			List<WeakReference> list = TraceSource.tracesources;
			lock (list)
			{
				TraceSource._pruneCachedTraceSources();
				for (int i = 0; i < TraceSource.tracesources.Count; i++)
				{
					TraceSource traceSource = (TraceSource)TraceSource.tracesources[i].Target;
					if (traceSource != null)
					{
						traceSource.Refresh();
					}
				}
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000CA434 File Offset: 0x000C8634
		internal void Refresh()
		{
			if (!this._initCalled)
			{
				this.Initialize();
				return;
			}
			SourceElementsCollection sources = DiagnosticsConfiguration.Sources;
			if (sources != null)
			{
				SourceElement sourceElement = sources[this.Name];
				if (sourceElement != null)
				{
					if ((string.IsNullOrEmpty(sourceElement.SwitchType) && this.internalSwitch.GetType() != typeof(SourceSwitch)) || sourceElement.SwitchType != this.internalSwitch.GetType().AssemblyQualifiedName)
					{
						if (!string.IsNullOrEmpty(sourceElement.SwitchName))
						{
							this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
						}
						else
						{
							this.CreateSwitch(sourceElement.SwitchType, this.Name);
							if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
							{
								this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
							}
						}
					}
					else if (!string.IsNullOrEmpty(sourceElement.SwitchName))
					{
						if (sourceElement.SwitchName != this.internalSwitch.DisplayName)
						{
							this.CreateSwitch(sourceElement.SwitchType, sourceElement.SwitchName);
						}
						else
						{
							this.internalSwitch.Refresh();
						}
					}
					else if (!string.IsNullOrEmpty(sourceElement.SwitchValue))
					{
						this.internalSwitch.Level = (SourceLevels)Enum.Parse(typeof(SourceLevels), sourceElement.SwitchValue);
					}
					else
					{
						this.internalSwitch.Level = SourceLevels.Off;
					}
					TraceListenerCollection traceListenerCollection = new TraceListenerCollection();
					foreach (object obj in sourceElement.Listeners)
					{
						ListenerElement listenerElement = (ListenerElement)obj;
						TraceListener traceListener = this.listeners[listenerElement.Name];
						if (traceListener != null)
						{
							traceListenerCollection.Add(listenerElement.RefreshRuntimeObject(traceListener));
						}
						else
						{
							traceListenerCollection.Add(listenerElement.GetRuntimeObject());
						}
					}
					TraceUtils.VerifyAttributes(sourceElement.Attributes, this.GetSupportedAttributes(), this);
					this.attributes = new StringDictionary();
					this.attributes.ReplaceHashtable(sourceElement.Attributes);
					this.listeners = traceListenerCollection;
					return;
				}
				this.internalSwitch.Level = this.switchLevel;
				this.listeners.Clear();
				this.attributes = null;
			}
		}

		/// <summary>Writes a trace event message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type and event identifier.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002D03 RID: 11523 RVA: 0x000CA6A4 File Offset: 0x000C88A4
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(traceEventCache, this.Name, eventType, id);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_111;
						}
						goto IL_F3;
					}
					goto IL_F3;
					IL_111:
					j++;
					continue;
					IL_F3:
					traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_111;
					}
					goto IL_111;
				}
			}
		}

		/// <summary>Writes a trace event message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and message.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002D04 RID: 11524 RVA: 0x000CA7F8 File Offset: 0x000C89F8
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string message)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(traceEventCache, this.Name, eventType, id, message);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, message);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_114;
						}
						goto IL_F5;
					}
					goto IL_F5;
					IL_114:
					j++;
					continue;
					IL_F5:
					traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, message);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_114;
					}
					goto IL_114;
				}
			}
		}

		/// <summary>Writes a trace event to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and argument array and format.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002D05 RID: 11525 RVA: 0x000CA950 File Offset: 0x000C8B50
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceEvent(traceEventCache, this.Name, eventType, id, format, args);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, format, args);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_11D;
						}
						goto IL_FC;
					}
					goto IL_FC;
					IL_11D:
					j++;
					continue;
					IL_FC:
					traceListener2.TraceEvent(traceEventCache, this.Name, eventType, id, format, args);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_11D;
					}
					goto IL_11D;
				}
			}
		}

		/// <summary>Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and trace data.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002D06 RID: 11526 RVA: 0x000CAAB0 File Offset: 0x000C8CB0
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, object data)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_114;
						}
						goto IL_F5;
					}
					goto IL_F5;
					IL_114:
					j++;
					continue;
					IL_F5:
					traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_114;
					}
					goto IL_114;
				}
			}
		}

		/// <summary>Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and trace data array.</summary>
		/// <param name="eventType">One of the enumeration values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An object array containing the trace data.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002D07 RID: 11527 RVA: 0x000CAC08 File Offset: 0x000C8E08
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, params object[] data)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(eventType) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_114;
						}
						goto IL_F5;
					}
					goto IL_F5;
					IL_114:
					j++;
					continue;
					IL_F5:
					traceListener2.TraceData(traceEventCache, this.Name, eventType, id, data);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_114;
					}
					goto IL_114;
				}
			}
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002D08 RID: 11528 RVA: 0x000CAD60 File Offset: 0x000C8F60
		[Conditional("TRACE")]
		public void TraceInformation(string message)
		{
			this.TraceEvent(TraceEventType.Information, 0, message, null);
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified object array and formatting information.</summary>
		/// <param name="format">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		// Token: 0x06002D09 RID: 11529 RVA: 0x000CAD6C File Offset: 0x000C8F6C
		[Conditional("TRACE")]
		public void TraceInformation(string format, params object[] args)
		{
			this.TraceEvent(TraceEventType.Information, 0, format, args);
		}

		/// <summary>Writes a trace transfer message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified numeric identifier, message, and related activity identifier.</summary>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write.</param>
		/// <param name="relatedActivityId">A structure that identifies the related activity.</param>
		// Token: 0x06002D0A RID: 11530 RVA: 0x000CAD78 File Offset: 0x000C8F78
		[Conditional("TRACE")]
		public void TraceTransfer(int id, string message, Guid relatedActivityId)
		{
			this.Initialize();
			TraceEventCache traceEventCache = new TraceEventCache();
			if (this.internalSwitch.ShouldTrace(TraceEventType.Transfer) && this.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object critSec = TraceInternal.critSec;
					lock (critSec)
					{
						for (int i = 0; i < this.listeners.Count; i++)
						{
							TraceListener traceListener = this.listeners[i];
							traceListener.TraceTransfer(traceEventCache, this.Name, id, message, relatedActivityId);
							if (Trace.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
				int j = 0;
				while (j < this.listeners.Count)
				{
					TraceListener traceListener2 = this.listeners[j];
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.TraceTransfer(traceEventCache, this.Name, id, message, relatedActivityId);
							if (Trace.AutoFlush)
							{
								traceListener2.Flush();
							}
							goto IL_118;
						}
						goto IL_F9;
					}
					goto IL_F9;
					IL_118:
					j++;
					continue;
					IL_F9:
					traceListener2.TraceTransfer(traceEventCache, this.Name, id, message, relatedActivityId);
					if (Trace.AutoFlush)
					{
						traceListener2.Flush();
						goto IL_118;
					}
					goto IL_118;
				}
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000CAED4 File Offset: 0x000C90D4
		private void CreateSwitch(string typename, string name)
		{
			if (!string.IsNullOrEmpty(typename))
			{
				this.internalSwitch = (SourceSwitch)TraceUtils.GetRuntimeObject(typename, typeof(SourceSwitch), name);
				return;
			}
			this.internalSwitch = new SourceSwitch(name, this.switchLevel.ToString());
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace switch.</returns>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000CAF27 File Offset: 0x000C9127
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets the name of the trace source.</summary>
		/// <returns>The name of the trace source.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000CAF48 File Offset: 0x000C9148
		public string Name
		{
			get
			{
				return this.sourceName;
			}
		}

		/// <summary>Gets the collection of trace listeners for the trace source.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that contains the active trace listeners associated with the source.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000CAF52 File Offset: 0x000C9152
		public TraceListenerCollection Listeners
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.Initialize();
				return this.listeners;
			}
		}

		/// <summary>Gets or sets the source switch value.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.SourceSwitch" /> object representing the source switch value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.TraceSource.Switch" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000CAF62 File Offset: 0x000C9162
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x000CAF72 File Offset: 0x000C9172
		public SourceSwitch Switch
		{
			get
			{
				this.Initialize();
				return this.internalSwitch;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Switch");
				}
				this.Initialize();
				this.internalSwitch = value;
			}
		}

		// Token: 0x040026EC RID: 9964
		private static List<WeakReference> tracesources = new List<WeakReference>();

		// Token: 0x040026ED RID: 9965
		private static int s_LastCollectionCount;

		// Token: 0x040026EE RID: 9966
		private volatile SourceSwitch internalSwitch;

		// Token: 0x040026EF RID: 9967
		private volatile TraceListenerCollection listeners;

		// Token: 0x040026F0 RID: 9968
		private StringDictionary attributes;

		// Token: 0x040026F1 RID: 9969
		private SourceLevels switchLevel;

		// Token: 0x040026F2 RID: 9970
		private volatile string sourceName;

		// Token: 0x040026F3 RID: 9971
		internal volatile bool _initCalled;
	}
}
