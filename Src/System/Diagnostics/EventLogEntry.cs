using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Encapsulates a single record in the event log. This class cannot be inherited.</summary>
	// Token: 0x020004CC RID: 1228
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Serializable]
	public sealed class EventLogEntry : Component, ISerializable
	{
		// Token: 0x06002E40 RID: 11840 RVA: 0x000D0CCD File Offset: 0x000CEECD
		internal EventLogEntry(byte[] buf, int offset, EventLogInternal log)
		{
			this.dataBuf = buf;
			this.bufOffset = offset;
			this.owner = log;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000D0CF0 File Offset: 0x000CEEF0
		private EventLogEntry(SerializationInfo info, StreamingContext context)
		{
			this.dataBuf = (byte[])info.GetValue("DataBuffer", typeof(byte[]));
			string @string = info.GetString("LogName");
			string string2 = info.GetString("MachineName");
			this.owner = new EventLogInternal(@string, string2, "");
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the name of the computer on which this entry was generated.</summary>
		/// <returns>The name of the computer that contains the event log.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x000D0D54 File Offset: 0x000CEF54
		[MonitoringDescription("LogEntryMachineName")]
		public string MachineName
		{
			get
			{
				int num = this.bufOffset + 56;
				while (this.CharFrom(this.dataBuf, num) != '\0')
				{
					num += 2;
				}
				num += 2;
				char c = this.CharFrom(this.dataBuf, num);
				StringBuilder stringBuilder = new StringBuilder();
				while (c != '\0')
				{
					stringBuilder.Append(c);
					num += 2;
					c = this.CharFrom(this.dataBuf, num);
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets the binary data associated with the entry.</summary>
		/// <returns>An array of bytes that holds the binary data associated with the entry.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x000D0DC0 File Offset: 0x000CEFC0
		[MonitoringDescription("LogEntryData")]
		public byte[] Data
		{
			get
			{
				int num = this.IntFrom(this.dataBuf, this.bufOffset + 48);
				byte[] array = new byte[num];
				Array.Copy(this.dataBuf, this.bufOffset + this.IntFrom(this.dataBuf, this.bufOffset + 52), array, 0, num);
				return array;
			}
		}

		/// <summary>Gets the index of this entry in the event log.</summary>
		/// <returns>The index of this entry in the event log.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x000D0E15 File Offset: 0x000CF015
		[MonitoringDescription("LogEntryIndex")]
		public int Index
		{
			get
			{
				return this.IntFrom(this.dataBuf, this.bufOffset + 8);
			}
		}

		/// <summary>Gets the text associated with the <see cref="P:System.Diagnostics.EventLogEntry.CategoryNumber" /> property for this entry.</summary>
		/// <returns>The application-specific category text.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the category.</exception>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x000D0E2C File Offset: 0x000CF02C
		[MonitoringDescription("LogEntryCategory")]
		public string Category
		{
			get
			{
				if (this.category == null)
				{
					string messageLibraryNames = this.GetMessageLibraryNames("CategoryMessageFile");
					string text = this.owner.FormatMessageWrapper(messageLibraryNames, (uint)this.CategoryNumber, null);
					if (text == null)
					{
						this.category = "(" + this.CategoryNumber.ToString(CultureInfo.CurrentCulture) + ")";
					}
					else
					{
						this.category = text;
					}
				}
				return this.category;
			}
		}

		/// <summary>Gets the category number of the event log entry.</summary>
		/// <returns>The application-specific category number for this entry.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000D0E9B File Offset: 0x000CF09B
		[MonitoringDescription("LogEntryCategoryNumber")]
		public short CategoryNumber
		{
			get
			{
				return this.ShortFrom(this.dataBuf, this.bufOffset + 28);
			}
		}

		/// <summary>Gets the application-specific event identifier for the current event entry.</summary>
		/// <returns>The application-specific identifier for the event message.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x000D0EB2 File Offset: 0x000CF0B2
		[MonitoringDescription("LogEntryEventID")]
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.EventLogEntry.InstanceId instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public int EventID
		{
			get
			{
				return this.IntFrom(this.dataBuf, this.bufOffset + 20) & 1073741823;
			}
		}

		/// <summary>Gets the event type of this entry.</summary>
		/// <returns>The event type that is associated with the entry in the event log.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x000D0ECF File Offset: 0x000CF0CF
		[MonitoringDescription("LogEntryEntryType")]
		public EventLogEntryType EntryType
		{
			get
			{
				return (EventLogEntryType)this.ShortFrom(this.dataBuf, this.bufOffset + 24);
			}
		}

		/// <summary>Gets the localized message associated with this event entry.</summary>
		/// <returns>The formatted, localized text for the message. This includes associated replacement strings.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the message.</exception>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000D0EE8 File Offset: 0x000CF0E8
		[MonitoringDescription("LogEntryMessage")]
		[Editor("System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Message
		{
			get
			{
				if (this.message == null)
				{
					string messageLibraryNames = this.GetMessageLibraryNames("EventMessageFile");
					int num = this.IntFrom(this.dataBuf, this.bufOffset + 20);
					string text = this.owner.FormatMessageWrapper(messageLibraryNames, (uint)num, this.ReplacementStrings);
					if (text == null)
					{
						StringBuilder stringBuilder = new StringBuilder(SR.GetString("MessageNotFormatted", new object[] { num, this.Source }));
						string[] replacementStrings = this.ReplacementStrings;
						for (int i = 0; i < replacementStrings.Length; i++)
						{
							if (i != 0)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append("'");
							stringBuilder.Append(replacementStrings[i]);
							stringBuilder.Append("'");
						}
						text = stringBuilder.ToString();
					}
					else
					{
						text = this.ReplaceMessageParameters(text, this.ReplacementStrings);
					}
					this.message = text;
				}
				return this.message;
			}
		}

		/// <summary>Gets the name of the application that generated this event.</summary>
		/// <returns>The name registered with the event log as the source of this event.</returns>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000D0FD8 File Offset: 0x000CF1D8
		[MonitoringDescription("LogEntrySource")]
		public string Source
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = this.bufOffset + 56;
				for (char c = this.CharFrom(this.dataBuf, num); c != '\0'; c = this.CharFrom(this.dataBuf, num))
				{
					stringBuilder.Append(c);
					num += 2;
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets the replacement strings associated with the event log entry.</summary>
		/// <returns>An array that holds the replacement strings stored in the event entry.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x000D1028 File Offset: 0x000CF228
		[MonitoringDescription("LogEntryReplacementStrings")]
		public string[] ReplacementStrings
		{
			get
			{
				string[] array = new string[(int)this.ShortFrom(this.dataBuf, this.bufOffset + 26)];
				int i = 0;
				int num = this.bufOffset + this.IntFrom(this.dataBuf, this.bufOffset + 36);
				StringBuilder stringBuilder = new StringBuilder();
				while (i < array.Length)
				{
					char c = this.CharFrom(this.dataBuf, num);
					if (c != '\0')
					{
						stringBuilder.Append(c);
					}
					else
					{
						array[i] = stringBuilder.ToString();
						i++;
						stringBuilder = new StringBuilder();
					}
					num += 2;
				}
				return array;
			}
		}

		/// <summary>Gets the resource identifier that designates the message text of the event entry.</summary>
		/// <returns>A resource identifier that corresponds to a string definition in the message resource file of the event source.</returns>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000D10B3 File Offset: 0x000CF2B3
		[MonitoringDescription("LogEntryResourceId")]
		[ComVisible(false)]
		public long InstanceId
		{
			get
			{
				return (long)((ulong)this.IntFrom(this.dataBuf, this.bufOffset + 20));
			}
		}

		/// <summary>Gets the local time at which this event was generated.</summary>
		/// <returns>The local time at which this event was generated.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x000D10CC File Offset: 0x000CF2CC
		[MonitoringDescription("LogEntryTimeGenerated")]
		public DateTime TimeGenerated
		{
			get
			{
				return EventLogEntry.beginningOfTime.AddSeconds((double)this.IntFrom(this.dataBuf, this.bufOffset + 12)).ToLocalTime();
			}
		}

		/// <summary>Gets the local time at which this event was written to the log.</summary>
		/// <returns>The local time at which this event was written to the log.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000D1104 File Offset: 0x000CF304
		[MonitoringDescription("LogEntryTimeWritten")]
		public DateTime TimeWritten
		{
			get
			{
				return EventLogEntry.beginningOfTime.AddSeconds((double)this.IntFrom(this.dataBuf, this.bufOffset + 16)).ToLocalTime();
			}
		}

		/// <summary>Gets the name of the user who is responsible for this event.</summary>
		/// <returns>The security identifier (SID) that uniquely identifies a user or group.</returns>
		/// <exception cref="T:System.SystemException">Account information could not be obtained for the user's SID.</exception>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000D113C File Offset: 0x000CF33C
		[MonitoringDescription("LogEntryUserName")]
		public string UserName
		{
			get
			{
				int num = this.IntFrom(this.dataBuf, this.bufOffset + 40);
				if (num == 0)
				{
					return null;
				}
				byte[] array = new byte[num];
				Array.Copy(this.dataBuf, this.bufOffset + this.IntFrom(this.dataBuf, this.bufOffset + 44), array, 0, array.Length);
				int num2 = 256;
				int num3 = 256;
				int num4 = 0;
				StringBuilder stringBuilder = new StringBuilder(num2);
				StringBuilder stringBuilder2 = new StringBuilder(num3);
				StringBuilder stringBuilder3 = new StringBuilder();
				if (Microsoft.Win32.UnsafeNativeMethods.LookupAccountSid(this.MachineName, array, stringBuilder, ref num2, stringBuilder2, ref num3, ref num4) != 0)
				{
					stringBuilder3.Append(stringBuilder2.ToString());
					stringBuilder3.Append("\\");
					stringBuilder3.Append(stringBuilder.ToString());
				}
				return stringBuilder3.ToString();
			}
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000D1207 File Offset: 0x000CF407
		private char CharFrom(byte[] buf, int offset)
		{
			return (char)this.ShortFrom(buf, offset);
		}

		/// <summary>Performs a comparison between two event log entries.</summary>
		/// <param name="otherEntry">The <see cref="T:System.Diagnostics.EventLogEntry" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Diagnostics.EventLogEntry" /> objects are identical; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E51 RID: 11857 RVA: 0x000D1214 File Offset: 0x000CF414
		public bool Equals(EventLogEntry otherEntry)
		{
			if (otherEntry == null)
			{
				return false;
			}
			int num = this.IntFrom(this.dataBuf, this.bufOffset);
			int num2 = this.IntFrom(otherEntry.dataBuf, otherEntry.bufOffset);
			if (num != num2)
			{
				return false;
			}
			int num3 = this.bufOffset;
			int num4 = this.bufOffset + num;
			int num5 = otherEntry.bufOffset;
			int i = num3;
			while (i < num4)
			{
				if (this.dataBuf[i] != otherEntry.dataBuf[num5])
				{
					return false;
				}
				i++;
				num5++;
			}
			return true;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000D1297 File Offset: 0x000CF497
		private int IntFrom(byte[] buf, int offset)
		{
			return (-16777216 & ((int)buf[offset + 3] << 24)) | (16711680 & ((int)buf[offset + 2] << 16)) | (65280 & ((int)buf[offset + 1] << 8)) | (int)(byte.MaxValue & buf[offset]);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000D12D0 File Offset: 0x000CF4D0
		internal string ReplaceMessageParameters(string msg, string[] insertionStrings)
		{
			int i = msg.IndexOf('%');
			if (i < 0)
			{
				return msg;
			}
			int num = 0;
			int length = msg.Length;
			StringBuilder stringBuilder = new StringBuilder();
			string messageLibraryNames = this.GetMessageLibraryNames("ParameterMessageFile");
			while (i >= 0)
			{
				string text = null;
				int num2 = i + 1;
				while (num2 < length && char.IsDigit(msg, num2))
				{
					num2++;
				}
				uint num3 = 0U;
				if (num2 != i + 1)
				{
					uint.TryParse(msg.Substring(i + 1, num2 - i - 1), out num3);
				}
				if (num3 != 0U)
				{
					text = this.owner.FormatMessageWrapper(messageLibraryNames, num3, insertionStrings);
				}
				if (text != null)
				{
					if (i > num)
					{
						stringBuilder.Append(msg, num, i - num);
					}
					stringBuilder.Append(text);
					num = num2;
				}
				i = msg.IndexOf('%', i + 1);
			}
			if (length - num > 0)
			{
				stringBuilder.Append(msg, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000D13B0 File Offset: 0x000CF5B0
		private static RegistryKey GetSourceRegKey(string logName, string source, string machineName)
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			RegistryKey registryKey3;
			try
			{
				registryKey = EventLog.GetEventLogRegKey(machineName, false);
				if (registryKey == null)
				{
					registryKey3 = null;
				}
				else
				{
					if (logName == null)
					{
						registryKey2 = registryKey.OpenSubKey("Application", false);
					}
					else
					{
						registryKey2 = registryKey.OpenSubKey(logName, false);
					}
					if (registryKey2 == null)
					{
						registryKey3 = null;
					}
					else
					{
						registryKey3 = registryKey2.OpenSubKey(source, false);
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
			}
			return registryKey3;
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000D1424 File Offset: 0x000CF624
		private string GetMessageLibraryNames(string libRegKey)
		{
			string text = null;
			RegistryKey registryKey = null;
			try
			{
				registryKey = EventLogEntry.GetSourceRegKey(this.owner.Log, this.Source, this.owner.MachineName);
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue(libRegKey);
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			if (text == null)
			{
				return null;
			}
			if (!(this.owner.MachineName != "."))
			{
				return text;
			}
			string[] array = text.Split(new char[] { ';' });
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length >= 2 && array[i][1] == ':')
				{
					stringBuilder.Append("\\\\");
					stringBuilder.Append(this.owner.MachineName);
					stringBuilder.Append("\\");
					stringBuilder.Append(array[i][0]);
					stringBuilder.Append("$");
					stringBuilder.Append(array[i], 2, array[i].Length - 2);
					stringBuilder.Append(';');
				}
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			return stringBuilder.ToString(0, stringBuilder.Length - 1);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000D1570 File Offset: 0x000CF770
		private short ShortFrom(byte[] buf, int offset)
		{
			return (short)((65280 & ((int)buf[offset + 1] << 8)) | (int)(byte.MaxValue & buf[offset]));
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06002E57 RID: 11863 RVA: 0x000D158C File Offset: 0x000CF78C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			int num = this.IntFrom(this.dataBuf, this.bufOffset);
			byte[] array = new byte[num];
			Array.Copy(this.dataBuf, this.bufOffset, array, 0, num);
			info.AddValue("DataBuffer", array, typeof(byte[]));
			info.AddValue("LogName", this.owner.Log);
			info.AddValue("MachineName", this.owner.MachineName);
		}

		// Token: 0x0400274E RID: 10062
		internal byte[] dataBuf;

		// Token: 0x0400274F RID: 10063
		internal int bufOffset;

		// Token: 0x04002750 RID: 10064
		private EventLogInternal owner;

		// Token: 0x04002751 RID: 10065
		private string category;

		// Token: 0x04002752 RID: 10066
		private string message;

		// Token: 0x04002753 RID: 10067
		private static readonly DateTime beginningOfTime = new DateTime(1970, 1, 1, 0, 0, 0);

		// Token: 0x04002754 RID: 10068
		private const int OFFSETFIXUP = 56;

		// Token: 0x0200087B RID: 2171
		private static class FieldOffsets
		{
			// Token: 0x04003710 RID: 14096
			internal const int LENGTH = 0;

			// Token: 0x04003711 RID: 14097
			internal const int RESERVED = 4;

			// Token: 0x04003712 RID: 14098
			internal const int RECORDNUMBER = 8;

			// Token: 0x04003713 RID: 14099
			internal const int TIMEGENERATED = 12;

			// Token: 0x04003714 RID: 14100
			internal const int TIMEWRITTEN = 16;

			// Token: 0x04003715 RID: 14101
			internal const int EVENTID = 20;

			// Token: 0x04003716 RID: 14102
			internal const int EVENTTYPE = 24;

			// Token: 0x04003717 RID: 14103
			internal const int NUMSTRINGS = 26;

			// Token: 0x04003718 RID: 14104
			internal const int EVENTCATEGORY = 28;

			// Token: 0x04003719 RID: 14105
			internal const int RESERVEDFLAGS = 30;

			// Token: 0x0400371A RID: 14106
			internal const int CLOSINGRECORDNUMBER = 32;

			// Token: 0x0400371B RID: 14107
			internal const int STRINGOFFSET = 36;

			// Token: 0x0400371C RID: 14108
			internal const int USERSIDLENGTH = 40;

			// Token: 0x0400371D RID: 14109
			internal const int USERSIDOFFSET = 44;

			// Token: 0x0400371E RID: 14110
			internal const int DATALENGTH = 48;

			// Token: 0x0400371F RID: 14111
			internal const int DATAOFFSET = 52;

			// Token: 0x04003720 RID: 14112
			internal const int RAWDATA = 56;
		}
	}
}
