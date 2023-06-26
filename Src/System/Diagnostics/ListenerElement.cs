using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x0200049D RID: 1181
	internal class ListenerElement : TypedElement
	{
		// Token: 0x06002BC3 RID: 11203 RVA: 0x000C5F54 File Offset: 0x000C4154
		public ListenerElement(bool allowReferences)
			: base(typeof(TraceListener))
		{
			this._allowReferences = allowReferences;
			ConfigurationPropertyOptions configurationPropertyOptions = ConfigurationPropertyOptions.None;
			if (!this._allowReferences)
			{
				configurationPropertyOptions |= ConfigurationPropertyOptions.IsRequired;
			}
			this._propListenerTypeName = new ConfigurationProperty("type", typeof(string), null, configurationPropertyOptions);
			this._properties.Remove("type");
			this._properties.Add(this._propListenerTypeName);
			this._properties.Add(ListenerElement._propFilter);
			this._properties.Add(ListenerElement._propName);
			this._properties.Add(ListenerElement._propOutputOpts);
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000C5FF4 File Offset: 0x000C41F4
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000C6014 File Offset: 0x000C4214
		[ConfigurationProperty("filter")]
		public FilterElement Filter
		{
			get
			{
				return (FilterElement)base[ListenerElement._propFilter];
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000C6026 File Offset: 0x000C4226
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x000C6038 File Offset: 0x000C4238
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ListenerElement._propName];
			}
			set
			{
				base[ListenerElement._propName] = value;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x000C6046 File Offset: 0x000C4246
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x000C6058 File Offset: 0x000C4258
		[ConfigurationProperty("traceOutputOptions", DefaultValue = TraceOptions.None)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return (TraceOptions)base[ListenerElement._propOutputOpts];
			}
			set
			{
				base[ListenerElement._propOutputOpts] = value;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x000C606B File Offset: 0x000C426B
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x000C607E File Offset: 0x000C427E
		[ConfigurationProperty("type")]
		public override string TypeName
		{
			get
			{
				return (string)base[this._propListenerTypeName];
			}
			set
			{
				base[this._propListenerTypeName] = value;
			}
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000C6090 File Offset: 0x000C4290
		public override bool Equals(object compareTo)
		{
			if (this.Name.Equals("Default") && this.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				ListenerElement listenerElement = compareTo as ListenerElement;
				return listenerElement != null && listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName);
			}
			return base.Equals(compareTo);
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000C610B File Offset: 0x000C430B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000C6114 File Offset: 0x000C4314
		public TraceListener GetRuntimeObject()
		{
			if (this._runtimeObject != null)
			{
				return (TraceListener)this._runtimeObject;
			}
			TraceListener traceListener;
			try
			{
				string typeName = this.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_listener_cant_have_properties", new object[] { this.Name }));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[] { this.Name }));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[] { this.Name }));
					}
					this._runtimeObject = listenerElement.GetRuntimeObject();
					traceListener = (TraceListener)this._runtimeObject;
				}
				else
				{
					TraceListener traceListener2 = (TraceListener)base.BaseGetRuntimeObject();
					traceListener2.initializeData = base.InitData;
					traceListener2.Name = this.Name;
					traceListener2.SetAttributes(this.Attributes);
					traceListener2.TraceOutputOptions = this.TraceOutputOptions;
					if (this.Filter != null && this.Filter.TypeName != null && this.Filter.TypeName.Length != 0)
					{
						traceListener2.Filter = this.Filter.GetRuntimeObject();
						XmlWriterTraceListener xmlWriterTraceListener = traceListener2 as XmlWriterTraceListener;
						if (xmlWriterTraceListener != null)
						{
							xmlWriterTraceListener.shouldRespectFilterOnTraceTransfer = true;
						}
					}
					this._runtimeObject = traceListener2;
					traceListener = traceListener2;
				}
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Could_not_create_listener", new object[] { this.Name }), ex);
			}
			return traceListener;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000C62F0 File Offset: 0x000C44F0
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000C6300 File Offset: 0x000C4500
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string text2 = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(text2, text);
					}
				}
			}
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000C6354 File Offset: 0x000C4554
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000C638C File Offset: 0x000C458C
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			ListenerElement listenerElement = sourceElement as ListenerElement;
			if (listenerElement != null && listenerElement._attributes != null)
			{
				this._attributes = listenerElement._attributes;
			}
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000C63C0 File Offset: 0x000C45C0
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				this._properties.Clear();
				this._properties.Add(this._propListenerTypeName);
				this._properties.Add(ListenerElement._propFilter);
				this._properties.Add(ListenerElement._propName);
				this._properties.Add(ListenerElement._propOutputOpts);
			}
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000C642C File Offset: 0x000C462C
		internal TraceListener RefreshRuntimeObject(TraceListener listener)
		{
			this._runtimeObject = null;
			TraceListener traceListener;
			try
			{
				string typeName = this.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_listener_cant_have_properties", new object[] { this.Name }));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[] { this.Name }));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[] { this.Name }));
					}
					this._runtimeObject = listenerElement.RefreshRuntimeObject(listener);
					traceListener = (TraceListener)this._runtimeObject;
				}
				else if (Type.GetType(typeName) != listener.GetType() || base.InitData != listener.initializeData)
				{
					traceListener = this.GetRuntimeObject();
				}
				else
				{
					listener.SetAttributes(this.Attributes);
					listener.TraceOutputOptions = this.TraceOutputOptions;
					if (listener.Filter != null)
					{
						if (base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.Inherited)
						{
							listener.Filter = this.Filter.RefreshRuntimeObject(listener.Filter);
						}
						else
						{
							listener.Filter = null;
						}
					}
					this._runtimeObject = listener;
					traceListener = listener;
				}
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Could_not_create_listener", new object[] { this.Name }), ex);
			}
			return traceListener;
		}

		// Token: 0x0400267E RID: 9854
		private static readonly ConfigurationProperty _propFilter = new ConfigurationProperty("filter", typeof(FilterElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400267F RID: 9855
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002680 RID: 9856
		private static readonly ConfigurationProperty _propOutputOpts = new ConfigurationProperty("traceOutputOptions", typeof(TraceOptions), TraceOptions.None, ConfigurationPropertyOptions.None);

		// Token: 0x04002681 RID: 9857
		private ConfigurationProperty _propListenerTypeName;

		// Token: 0x04002682 RID: 9858
		private bool _allowReferences;

		// Token: 0x04002683 RID: 9859
		private Hashtable _attributes;

		// Token: 0x04002684 RID: 9860
		internal bool _isAddedByDefault;
	}
}
