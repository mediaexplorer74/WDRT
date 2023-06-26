using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Microsoft.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042F RID: 1071
	internal class ManifestBuilder
	{
		// Token: 0x06003587 RID: 13703 RVA: 0x000D0930 File Offset: 0x000CEB30
		public ManifestBuilder(string providerName, Guid providerGuid, string dllName, ResourceManager resources, EventManifestOptions flags)
		{
			this.providerName = providerName;
			this.flags = flags;
			this.resources = resources;
			this.sb = new StringBuilder();
			this.events = new StringBuilder();
			this.templates = new StringBuilder();
			this.opcodeTab = new Dictionary<int, string>();
			this.stringTab = new Dictionary<string, string>();
			this.errors = new List<string>();
			this.perEventByteArrayArgIndices = new Dictionary<string, List<int>>();
			this.sb.AppendLine("<instrumentationManifest xmlns=\"http://schemas.microsoft.com/win/2004/08/events\">");
			this.sb.AppendLine(" <instrumentation xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:win=\"http://manifests.microsoft.com/win/2004/08/windows/events\">");
			this.sb.AppendLine("  <events xmlns=\"http://schemas.microsoft.com/win/2004/08/events\">");
			this.sb.Append("<provider name=\"").Append(providerName).Append("\" guid=\"{")
				.Append(providerGuid.ToString())
				.Append("}");
			if (dllName != null)
			{
				this.sb.Append("\" resourceFileName=\"").Append(dllName).Append("\" messageFileName=\"")
					.Append(dllName);
			}
			string text = providerName.Replace("-", "").Replace(".", "_");
			this.sb.Append("\" symbol=\"").Append(text);
			this.sb.Append("\">").AppendLine();
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000D0AA0 File Offset: 0x000CECA0
		public void AddOpcode(string name, int value)
		{
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				if (value <= 10 || value >= 239)
				{
					this.ManifestError(Environment.GetResourceString("EventSource_IllegalOpcodeValue", new object[] { name, value }), false);
				}
				string text;
				if (this.opcodeTab.TryGetValue(value, out text) && !name.Equals(text, StringComparison.Ordinal))
				{
					this.ManifestError(Environment.GetResourceString("EventSource_OpcodeCollision", new object[] { name, text, value }), false);
				}
			}
			this.opcodeTab[value] = name;
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000D0B38 File Offset: 0x000CED38
		public void AddTask(string name, int value)
		{
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				if (value <= 0 || value >= 65535)
				{
					this.ManifestError(Environment.GetResourceString("EventSource_IllegalTaskValue", new object[] { name, value }), false);
				}
				string text;
				if (this.taskTab != null && this.taskTab.TryGetValue(value, out text) && !name.Equals(text, StringComparison.Ordinal))
				{
					this.ManifestError(Environment.GetResourceString("EventSource_TaskCollision", new object[] { name, text, value }), false);
				}
			}
			if (this.taskTab == null)
			{
				this.taskTab = new Dictionary<int, string>();
			}
			this.taskTab[value] = name;
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000D0BEC File Offset: 0x000CEDEC
		public void AddKeyword(string name, ulong value)
		{
			if ((value & (value - 1UL)) != 0UL)
			{
				this.ManifestError(Environment.GetResourceString("EventSource_KeywordNeedPowerOfTwo", new object[]
				{
					"0x" + value.ToString("x", CultureInfo.CurrentCulture),
					name
				}), true);
			}
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				if (value >= 17592186044416UL && !name.StartsWith("Session", StringComparison.Ordinal))
				{
					this.ManifestError(Environment.GetResourceString("EventSource_IllegalKeywordsValue", new object[]
					{
						name,
						"0x" + value.ToString("x", CultureInfo.CurrentCulture)
					}), false);
				}
				string text;
				if (this.keywordTab != null && this.keywordTab.TryGetValue(value, out text) && !name.Equals(text, StringComparison.Ordinal))
				{
					this.ManifestError(Environment.GetResourceString("EventSource_KeywordCollision", new object[]
					{
						name,
						text,
						"0x" + value.ToString("x", CultureInfo.CurrentCulture)
					}), false);
				}
			}
			if (this.keywordTab == null)
			{
				this.keywordTab = new Dictionary<ulong, string>();
			}
			this.keywordTab[value] = name;
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000D0D1C File Offset: 0x000CEF1C
		public void AddChannel(string name, int value, EventChannelAttribute channelAttribute)
		{
			EventChannel eventChannel = (EventChannel)value;
			if (value < 16 || value > 255)
			{
				this.ManifestError(Environment.GetResourceString("EventSource_EventChannelOutOfRange", new object[] { name, value }), false);
			}
			else if (eventChannel >= EventChannel.Admin && eventChannel <= EventChannel.Debug && channelAttribute != null && this.EventChannelToChannelType(eventChannel) != channelAttribute.EventChannelType)
			{
				this.ManifestError(Environment.GetResourceString("EventSource_ChannelTypeDoesNotMatchEventChannelValue", new object[]
				{
					name,
					((EventChannel)value).ToString()
				}), false);
			}
			ulong channelKeyword = this.GetChannelKeyword(eventChannel);
			if (this.channelTab == null)
			{
				this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
			}
			this.channelTab[value] = new ManifestBuilder.ChannelInfo
			{
				Name = name,
				Keywords = channelKeyword,
				Attribs = channelAttribute
			};
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000D0DEB File Offset: 0x000CEFEB
		private EventChannelType EventChannelToChannelType(EventChannel channel)
		{
			return (EventChannelType)(channel - 16 + 1);
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000D0DF4 File Offset: 0x000CEFF4
		private EventChannelAttribute GetDefaultChannelAttribute(EventChannel channel)
		{
			EventChannelAttribute eventChannelAttribute = new EventChannelAttribute();
			eventChannelAttribute.EventChannelType = this.EventChannelToChannelType(channel);
			if (eventChannelAttribute.EventChannelType <= EventChannelType.Operational)
			{
				eventChannelAttribute.Enabled = true;
			}
			return eventChannelAttribute;
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000D0E28 File Offset: 0x000CF028
		public ulong[] GetChannelData()
		{
			if (this.channelTab == null)
			{
				return new ulong[0];
			}
			int num = -1;
			foreach (int num2 in this.channelTab.Keys)
			{
				if (num2 > num)
				{
					num = num2;
				}
			}
			ulong[] array = new ulong[num + 1];
			foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair in this.channelTab)
			{
				array[keyValuePair.Key] = keyValuePair.Value.Keywords;
			}
			return array;
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000D0EEC File Offset: 0x000CF0EC
		public void StartEvent(string eventName, EventAttribute eventAttribute)
		{
			this.eventName = eventName;
			this.numParams = 0;
			this.byteArrArgIndices = null;
			this.events.Append("  <event").Append(" value=\"").Append(eventAttribute.EventId)
				.Append("\"")
				.Append(" version=\"")
				.Append(eventAttribute.Version)
				.Append("\"")
				.Append(" level=\"")
				.Append(ManifestBuilder.GetLevelName(eventAttribute.Level))
				.Append("\"")
				.Append(" symbol=\"")
				.Append(eventName)
				.Append("\"");
			this.WriteMessageAttrib(this.events, "event", eventName, eventAttribute.Message);
			if (eventAttribute.Keywords != EventKeywords.None)
			{
				this.events.Append(" keywords=\"").Append(this.GetKeywords((ulong)eventAttribute.Keywords, eventName)).Append("\"");
			}
			if (eventAttribute.Opcode != EventOpcode.Info)
			{
				this.events.Append(" opcode=\"").Append(this.GetOpcodeName(eventAttribute.Opcode, eventName)).Append("\"");
			}
			if (eventAttribute.Task != EventTask.None)
			{
				this.events.Append(" task=\"").Append(this.GetTaskName(eventAttribute.Task, eventName)).Append("\"");
			}
			if (eventAttribute.Channel != EventChannel.None)
			{
				this.events.Append(" channel=\"").Append(this.GetChannelName(eventAttribute.Channel, eventName, eventAttribute.Message)).Append("\"");
			}
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000D1090 File Offset: 0x000CF290
		public void AddEventParameter(Type type, string name)
		{
			if (this.numParams == 0)
			{
				this.templates.Append("  <template tid=\"").Append(this.eventName).Append("Args\">")
					.AppendLine();
			}
			if (type == typeof(byte[]))
			{
				if (this.byteArrArgIndices == null)
				{
					this.byteArrArgIndices = new List<int>(4);
				}
				this.byteArrArgIndices.Add(this.numParams);
				this.numParams++;
				this.templates.Append("   <data name=\"").Append(name).Append("Size\" inType=\"win:UInt32\"/>")
					.AppendLine();
			}
			this.numParams++;
			this.templates.Append("   <data name=\"").Append(name).Append("\" inType=\"")
				.Append(this.GetTypeName(type))
				.Append("\"");
			if ((type.IsArray || type.IsPointer) && type.GetElementType() == typeof(byte))
			{
				this.templates.Append(" length=\"").Append(name).Append("Size\"");
			}
			if (type.IsEnum() && Enum.GetUnderlyingType(type) != typeof(ulong) && Enum.GetUnderlyingType(type) != typeof(long))
			{
				this.templates.Append(" map=\"").Append(type.Name).Append("\"");
				if (this.mapsTab == null)
				{
					this.mapsTab = new Dictionary<string, Type>();
				}
				if (!this.mapsTab.ContainsKey(type.Name))
				{
					this.mapsTab.Add(type.Name, type);
				}
			}
			this.templates.Append("/>").AppendLine();
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000D1278 File Offset: 0x000CF478
		public void EndEvent()
		{
			if (this.numParams > 0)
			{
				this.templates.Append("  </template>").AppendLine();
				this.events.Append(" template=\"").Append(this.eventName).Append("Args\"");
			}
			this.events.Append("/>").AppendLine();
			if (this.byteArrArgIndices != null)
			{
				this.perEventByteArrayArgIndices[this.eventName] = this.byteArrArgIndices;
			}
			string text;
			if (this.stringTab.TryGetValue("event_" + this.eventName, out text))
			{
				text = this.TranslateToManifestConvention(text, this.eventName);
				this.stringTab["event_" + this.eventName] = text;
			}
			this.eventName = null;
			this.numParams = 0;
			this.byteArrArgIndices = null;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000D1360 File Offset: 0x000CF560
		public ulong GetChannelKeyword(EventChannel channel)
		{
			if (this.channelTab == null)
			{
				this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
			}
			if (this.channelTab.Count == 8)
			{
				this.ManifestError(Environment.GetResourceString("EventSource_MaxChannelExceeded"), false);
			}
			ManifestBuilder.ChannelInfo channelInfo;
			ulong keywords;
			if (!this.channelTab.TryGetValue((int)channel, out channelInfo))
			{
				keywords = this.nextChannelKeywordBit;
				this.nextChannelKeywordBit >>= 1;
			}
			else
			{
				keywords = channelInfo.Keywords;
			}
			return keywords;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000D13D0 File Offset: 0x000CF5D0
		public byte[] CreateManifest()
		{
			string text = this.CreateManifestString();
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000D13EF File Offset: 0x000CF5EF
		public IList<string> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000D13F7 File Offset: 0x000CF5F7
		public void ManifestError(string msg, bool runtimeCritical = false)
		{
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				this.errors.Add(msg);
				return;
			}
			if (runtimeCritical)
			{
				throw new ArgumentException(msg);
			}
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000D141C File Offset: 0x000CF61C
		private string CreateManifestString()
		{
			if (this.channelTab != null)
			{
				this.sb.Append(" <channels>").AppendLine();
				List<KeyValuePair<int, ManifestBuilder.ChannelInfo>> list = new List<KeyValuePair<int, ManifestBuilder.ChannelInfo>>();
				foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair in this.channelTab)
				{
					list.Add(keyValuePair);
				}
				list.Sort((KeyValuePair<int, ManifestBuilder.ChannelInfo> p1, KeyValuePair<int, ManifestBuilder.ChannelInfo> p2) => -Comparer<ulong>.Default.Compare(p1.Value.Keywords, p2.Value.Keywords));
				foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair2 in list)
				{
					int key = keyValuePair2.Key;
					ManifestBuilder.ChannelInfo value = keyValuePair2.Value;
					string text = null;
					string text2 = "channel";
					bool flag = false;
					string text3 = null;
					if (value.Attribs != null)
					{
						EventChannelAttribute attribs = value.Attribs;
						if (Enum.IsDefined(typeof(EventChannelType), attribs.EventChannelType))
						{
							text = attribs.EventChannelType.ToString();
						}
						flag = attribs.Enabled;
					}
					if (text3 == null)
					{
						text3 = this.providerName + "/" + value.Name;
					}
					this.sb.Append("  <").Append(text2);
					this.sb.Append(" chid=\"").Append(value.Name).Append("\"");
					this.sb.Append(" name=\"").Append(text3).Append("\"");
					if (text2 == "channel")
					{
						this.WriteMessageAttrib(this.sb, "channel", value.Name, null);
						this.sb.Append(" value=\"").Append(key).Append("\"");
						if (text != null)
						{
							this.sb.Append(" type=\"").Append(text).Append("\"");
						}
						this.sb.Append(" enabled=\"").Append(flag.ToString().ToLower()).Append("\"");
					}
					this.sb.Append("/>").AppendLine();
				}
				this.sb.Append(" </channels>").AppendLine();
			}
			if (this.taskTab != null)
			{
				this.sb.Append(" <tasks>").AppendLine();
				List<int> list2 = new List<int>(this.taskTab.Keys);
				list2.Sort();
				foreach (int num in list2)
				{
					this.sb.Append("  <task");
					this.WriteNameAndMessageAttribs(this.sb, "task", this.taskTab[num]);
					this.sb.Append(" value=\"").Append(num).Append("\"/>")
						.AppendLine();
				}
				this.sb.Append(" </tasks>").AppendLine();
			}
			if (this.mapsTab != null)
			{
				this.sb.Append(" <maps>").AppendLine();
				foreach (Type type in this.mapsTab.Values)
				{
					bool flag2 = EventSource.GetCustomAttributeHelper(type, typeof(FlagsAttribute), this.flags) != null;
					string text4 = (flag2 ? "bitMap" : "valueMap");
					this.sb.Append("  <").Append(text4).Append(" name=\"")
						.Append(type.Name)
						.Append("\">")
						.AppendLine();
					FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
					foreach (FieldInfo fieldInfo in fields)
					{
						object rawConstantValue = fieldInfo.GetRawConstantValue();
						if (rawConstantValue != null)
						{
							long num2;
							if (rawConstantValue is int)
							{
								num2 = (long)((int)rawConstantValue);
							}
							else
							{
								if (!(rawConstantValue is long))
								{
									goto IL_4D1;
								}
								num2 = (long)rawConstantValue;
							}
							if (!flag2 || ((num2 & (num2 - 1L)) == 0L && num2 != 0L))
							{
								this.sb.Append("   <map value=\"0x").Append(num2.ToString("x", CultureInfo.InvariantCulture)).Append("\"");
								this.WriteMessageAttrib(this.sb, "map", type.Name + "." + fieldInfo.Name, fieldInfo.Name);
								this.sb.Append("/>").AppendLine();
							}
						}
						IL_4D1:;
					}
					this.sb.Append("  </").Append(text4).Append(">")
						.AppendLine();
				}
				this.sb.Append(" </maps>").AppendLine();
			}
			this.sb.Append(" <opcodes>").AppendLine();
			List<int> list3 = new List<int>(this.opcodeTab.Keys);
			list3.Sort();
			foreach (int num3 in list3)
			{
				this.sb.Append("  <opcode");
				this.WriteNameAndMessageAttribs(this.sb, "opcode", this.opcodeTab[num3]);
				this.sb.Append(" value=\"").Append(num3).Append("\"/>")
					.AppendLine();
			}
			this.sb.Append(" </opcodes>").AppendLine();
			if (this.keywordTab != null)
			{
				this.sb.Append(" <keywords>").AppendLine();
				List<ulong> list4 = new List<ulong>(this.keywordTab.Keys);
				list4.Sort();
				foreach (ulong num4 in list4)
				{
					this.sb.Append("  <keyword");
					this.WriteNameAndMessageAttribs(this.sb, "keyword", this.keywordTab[num4]);
					this.sb.Append(" mask=\"0x").Append(num4.ToString("x", CultureInfo.InvariantCulture)).Append("\"/>")
						.AppendLine();
				}
				this.sb.Append(" </keywords>").AppendLine();
			}
			this.sb.Append(" <events>").AppendLine();
			this.sb.Append(this.events);
			this.sb.Append(" </events>").AppendLine();
			this.sb.Append(" <templates>").AppendLine();
			if (this.templates.Length > 0)
			{
				this.sb.Append(this.templates);
			}
			else
			{
				this.sb.Append("    <template tid=\"_empty\"></template>").AppendLine();
			}
			this.sb.Append(" </templates>").AppendLine();
			this.sb.Append("</provider>").AppendLine();
			this.sb.Append("</events>").AppendLine();
			this.sb.Append("</instrumentation>").AppendLine();
			this.sb.Append("<localization>").AppendLine();
			List<CultureInfo> list5;
			if (this.resources != null && (this.flags & EventManifestOptions.AllCultures) != EventManifestOptions.None)
			{
				list5 = ManifestBuilder.GetSupportedCultures(this.resources);
			}
			else
			{
				list5 = new List<CultureInfo>();
				list5.Add(CultureInfo.CurrentUICulture);
			}
			string[] array2 = new string[this.stringTab.Keys.Count];
			this.stringTab.Keys.CopyTo(array2, 0);
			ArraySortHelper<string>.IntrospectiveSort(array2, 0, array2.Length, Comparer<string>.Default);
			foreach (CultureInfo cultureInfo in list5)
			{
				this.sb.Append(" <resources culture=\"").Append(cultureInfo.Name).Append("\">")
					.AppendLine();
				this.sb.Append("  <stringTable>").AppendLine();
				foreach (string text5 in array2)
				{
					string localizedMessage = this.GetLocalizedMessage(text5, cultureInfo, true);
					this.sb.Append("   <string id=\"").Append(text5).Append("\" value=\"")
						.Append(localizedMessage)
						.Append("\"/>")
						.AppendLine();
				}
				this.sb.Append("  </stringTable>").AppendLine();
				this.sb.Append(" </resources>").AppendLine();
			}
			this.sb.Append("</localization>").AppendLine();
			this.sb.AppendLine("</instrumentationManifest>");
			return this.sb.ToString();
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000D1E50 File Offset: 0x000D0050
		private void WriteNameAndMessageAttribs(StringBuilder stringBuilder, string elementName, string name)
		{
			stringBuilder.Append(" name=\"").Append(name).Append("\"");
			this.WriteMessageAttrib(this.sb, elementName, name, name);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000D1E80 File Offset: 0x000D0080
		private void WriteMessageAttrib(StringBuilder stringBuilder, string elementName, string name, string value)
		{
			string text = elementName + "_" + name;
			if (this.resources != null)
			{
				string @string = this.resources.GetString(text, CultureInfo.InvariantCulture);
				if (@string != null)
				{
					value = @string;
				}
			}
			if (value == null)
			{
				return;
			}
			stringBuilder.Append(" message=\"$(string.").Append(text).Append(")\"");
			string text2;
			if (this.stringTab.TryGetValue(text, out text2) && !text2.Equals(value))
			{
				this.ManifestError(Environment.GetResourceString("EventSource_DuplicateStringKey", new object[] { text }), true);
				return;
			}
			this.stringTab[text] = value;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000D1F20 File Offset: 0x000D0120
		internal string GetLocalizedMessage(string key, CultureInfo ci, bool etwFormat)
		{
			string text = null;
			if (this.resources != null)
			{
				string @string = this.resources.GetString(key, ci);
				if (@string != null)
				{
					text = @string;
					if (etwFormat && key.StartsWith("event_"))
					{
						string text2 = key.Substring("event_".Length);
						text = this.TranslateToManifestConvention(text, text2);
					}
				}
			}
			if (etwFormat && text == null)
			{
				this.stringTab.TryGetValue(key, out text);
			}
			return text;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000D1F8C File Offset: 0x000D018C
		private static List<CultureInfo> GetSupportedCultures(ResourceManager resources)
		{
			List<CultureInfo> list = new List<CultureInfo>();
			foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
			{
				if (resources.GetResourceSet(cultureInfo, true, false) != null)
				{
					list.Add(cultureInfo);
				}
			}
			if (!list.Contains(CultureInfo.CurrentUICulture))
			{
				list.Insert(0, CultureInfo.CurrentUICulture);
			}
			return list;
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000D1FE4 File Offset: 0x000D01E4
		private static string GetLevelName(EventLevel level)
		{
			return ((level >= (EventLevel)16) ? "" : "win:") + level.ToString();
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000D200C File Offset: 0x000D020C
		private string GetChannelName(EventChannel channel, string eventName, string eventMessage)
		{
			ManifestBuilder.ChannelInfo channelInfo = null;
			if (this.channelTab == null || !this.channelTab.TryGetValue((int)channel, out channelInfo))
			{
				if (channel < EventChannel.Admin)
				{
					this.ManifestError(Environment.GetResourceString("EventSource_UndefinedChannel", new object[] { channel, eventName }), false);
				}
				if (this.channelTab == null)
				{
					this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
				}
				string text = channel.ToString();
				if (EventChannel.Debug < channel)
				{
					text = "Channel" + text;
				}
				this.AddChannel(text, (int)channel, this.GetDefaultChannelAttribute(channel));
				if (!this.channelTab.TryGetValue((int)channel, out channelInfo))
				{
					this.ManifestError(Environment.GetResourceString("EventSource_UndefinedChannel", new object[] { channel, eventName }), false);
				}
			}
			if (this.resources != null && eventMessage == null)
			{
				eventMessage = this.resources.GetString("event_" + eventName, CultureInfo.InvariantCulture);
			}
			if (channelInfo.Attribs.EventChannelType == EventChannelType.Admin && eventMessage == null)
			{
				this.ManifestError(Environment.GetResourceString("EventSource_EventWithAdminChannelMustHaveMessage", new object[] { eventName, channelInfo.Name }), false);
			}
			return channelInfo.Name;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000D2138 File Offset: 0x000D0338
		private string GetTaskName(EventTask task, string eventName)
		{
			if (task == EventTask.None)
			{
				return "";
			}
			if (this.taskTab == null)
			{
				this.taskTab = new Dictionary<int, string>();
			}
			string text;
			if (!this.taskTab.TryGetValue((int)task, out text))
			{
				this.taskTab[(int)task] = eventName;
				text = eventName;
			}
			return text;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000D2184 File Offset: 0x000D0384
		private string GetOpcodeName(EventOpcode opcode, string eventName)
		{
			switch (opcode)
			{
			case EventOpcode.Info:
				return "win:Info";
			case EventOpcode.Start:
				return "win:Start";
			case EventOpcode.Stop:
				return "win:Stop";
			case EventOpcode.DataCollectionStart:
				return "win:DC_Start";
			case EventOpcode.DataCollectionStop:
				return "win:DC_Stop";
			case EventOpcode.Extension:
				return "win:Extension";
			case EventOpcode.Reply:
				return "win:Reply";
			case EventOpcode.Resume:
				return "win:Resume";
			case EventOpcode.Suspend:
				return "win:Suspend";
			case EventOpcode.Send:
				return "win:Send";
			default:
				if (opcode != EventOpcode.Receive)
				{
					string text;
					if (this.opcodeTab == null || !this.opcodeTab.TryGetValue((int)opcode, out text))
					{
						this.ManifestError(Environment.GetResourceString("EventSource_UndefinedOpcode", new object[] { opcode, eventName }), true);
						text = null;
					}
					return text;
				}
				return "win:Receive";
			}
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000D224C File Offset: 0x000D044C
		private string GetKeywords(ulong keywords, string eventName)
		{
			string text = "";
			for (ulong num = 1UL; num != 0UL; num <<= 1)
			{
				if ((keywords & num) != 0UL)
				{
					string text2 = null;
					if ((this.keywordTab == null || !this.keywordTab.TryGetValue(num, out text2)) && num >= 281474976710656UL)
					{
						text2 = string.Empty;
					}
					if (text2 == null)
					{
						this.ManifestError(Environment.GetResourceString("EventSource_UndefinedKeyword", new object[]
						{
							"0x" + num.ToString("x", CultureInfo.CurrentCulture),
							eventName
						}), true);
						text2 = string.Empty;
					}
					if (text.Length != 0 && text2.Length != 0)
					{
						text += " ";
					}
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000D230C File Offset: 0x000D050C
		private string GetTypeName(Type type)
		{
			if (type.IsEnum())
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				string typeName = this.GetTypeName(fields[0].FieldType);
				return typeName.Replace("win:Int", "win:UInt");
			}
			switch (type.GetTypeCode())
			{
			case TypeCode.Boolean:
				return "win:Boolean";
			case TypeCode.Char:
			case TypeCode.UInt16:
				return "win:UInt16";
			case TypeCode.SByte:
				return "win:Int8";
			case TypeCode.Byte:
				return "win:UInt8";
			case TypeCode.Int16:
				return "win:Int16";
			case TypeCode.Int32:
				return "win:Int32";
			case TypeCode.UInt32:
				return "win:UInt32";
			case TypeCode.Int64:
				return "win:Int64";
			case TypeCode.UInt64:
				return "win:UInt64";
			case TypeCode.Single:
				return "win:Float";
			case TypeCode.Double:
				return "win:Double";
			case TypeCode.DateTime:
				return "win:FILETIME";
			case TypeCode.String:
				return "win:UnicodeString";
			}
			if (type == typeof(Guid))
			{
				return "win:GUID";
			}
			if (type == typeof(IntPtr))
			{
				return "win:Pointer";
			}
			if ((type.IsArray || type.IsPointer) && type.GetElementType() == typeof(byte))
			{
				return "win:Binary";
			}
			this.ManifestError(Environment.GetResourceString("EventSource_UnsupportedEventTypeInManifest", new object[] { type.Name }), true);
			return string.Empty;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000D246B File Offset: 0x000D066B
		private static void UpdateStringBuilder(ref StringBuilder stringBuilder, string eventMessage, int startIndex, int count)
		{
			if (stringBuilder == null)
			{
				stringBuilder = new StringBuilder();
			}
			stringBuilder.Append(eventMessage, startIndex, count);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000D2484 File Offset: 0x000D0684
		private string TranslateToManifestConvention(string eventMessage, string evtName)
		{
			StringBuilder stringBuilder = null;
			int writtenSoFar = 0;
			int i = 0;
			Action<char, string> <>9__0;
			while (i < eventMessage.Length)
			{
				int num4;
				if (eventMessage[i] == '%')
				{
					ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
					stringBuilder.Append("%%");
					int num = i;
					i = num + 1;
					writtenSoFar = i;
				}
				else if (i < eventMessage.Length - 1 && ((eventMessage[i] == '{' && eventMessage[i + 1] == '{') || (eventMessage[i] == '}' && eventMessage[i + 1] == '}')))
				{
					ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
					stringBuilder.Append(eventMessage[i]);
					int num = i;
					i = num + 1;
					num = i;
					i = num + 1;
					writtenSoFar = i;
				}
				else if (eventMessage[i] == '{')
				{
					int j = i;
					int num = i;
					i = num + 1;
					int num2 = 0;
					while (i < eventMessage.Length && char.IsDigit(eventMessage[i]))
					{
						num2 = num2 * 10 + (int)eventMessage[i] - 48;
						num = i;
						i = num + 1;
					}
					if (i < eventMessage.Length && eventMessage[i] == '}')
					{
						num = i;
						i = num + 1;
						ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, j - writtenSoFar);
						int num3 = this.TranslateIndexToManifestConvention(num2, evtName);
						stringBuilder.Append('%').Append(num3);
						if (i < eventMessage.Length && eventMessage[i] == '!')
						{
							num = i;
							i = num + 1;
							stringBuilder.Append("%!");
						}
						writtenSoFar = i;
					}
					else
					{
						this.ManifestError(Environment.GetResourceString("EventSource_UnsupportedMessageProperty", new object[] { evtName, eventMessage }), false);
					}
				}
				else if ((num4 = "&<>'\"\r\n\t".IndexOf(eventMessage[i])) >= 0)
				{
					string[] array = new string[] { "&amp;", "&lt;", "&gt;", "&apos;", "&quot;", "%r", "%n", "%t" };
					Action<char, string> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(char ch, string escape)
						{
							ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
							int i2 = i;
							i = i2 + 1;
							stringBuilder.Append(escape);
							writtenSoFar = i;
						});
					}
					Action<char, string> action2 = action;
					action2(eventMessage[i], array[num4]);
				}
				else
				{
					int num = i;
					i = num + 1;
				}
			}
			if (stringBuilder == null)
			{
				return eventMessage;
			}
			ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
			return stringBuilder.ToString();
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000D28A8 File Offset: 0x000D0AA8
		private int TranslateIndexToManifestConvention(int idx, string evtName)
		{
			List<int> list;
			if (this.perEventByteArrayArgIndices.TryGetValue(evtName, out list))
			{
				foreach (int num in list)
				{
					if (idx < num)
					{
						break;
					}
					idx++;
				}
			}
			return idx + 1;
		}

		// Token: 0x040017D4 RID: 6100
		private Dictionary<int, string> opcodeTab;

		// Token: 0x040017D5 RID: 6101
		private Dictionary<int, string> taskTab;

		// Token: 0x040017D6 RID: 6102
		private Dictionary<int, ManifestBuilder.ChannelInfo> channelTab;

		// Token: 0x040017D7 RID: 6103
		private Dictionary<ulong, string> keywordTab;

		// Token: 0x040017D8 RID: 6104
		private Dictionary<string, Type> mapsTab;

		// Token: 0x040017D9 RID: 6105
		private Dictionary<string, string> stringTab;

		// Token: 0x040017DA RID: 6106
		private ulong nextChannelKeywordBit = 9223372036854775808UL;

		// Token: 0x040017DB RID: 6107
		private const int MaxCountChannels = 8;

		// Token: 0x040017DC RID: 6108
		private StringBuilder sb;

		// Token: 0x040017DD RID: 6109
		private StringBuilder events;

		// Token: 0x040017DE RID: 6110
		private StringBuilder templates;

		// Token: 0x040017DF RID: 6111
		private string providerName;

		// Token: 0x040017E0 RID: 6112
		private ResourceManager resources;

		// Token: 0x040017E1 RID: 6113
		private EventManifestOptions flags;

		// Token: 0x040017E2 RID: 6114
		private IList<string> errors;

		// Token: 0x040017E3 RID: 6115
		private Dictionary<string, List<int>> perEventByteArrayArgIndices;

		// Token: 0x040017E4 RID: 6116
		private string eventName;

		// Token: 0x040017E5 RID: 6117
		private int numParams;

		// Token: 0x040017E6 RID: 6118
		private List<int> byteArrArgIndices;

		// Token: 0x02000B8D RID: 2957
		private class ChannelInfo
		{
			// Token: 0x04003514 RID: 13588
			public string Name;

			// Token: 0x04003515 RID: 13589
			public ulong Keywords;

			// Token: 0x04003516 RID: 13590
			public EventChannelAttribute Attribs;
		}
	}
}
