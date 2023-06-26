using System;

namespace System.Diagnostics
{
	/// <summary>Represents the configuration settings used to create an event log source on the local computer or a remote computer.</summary>
	// Token: 0x020004D5 RID: 1237
	public class EventSourceCreationData
	{
		// Token: 0x06002E95 RID: 11925 RVA: 0x000D1D6A File Offset: 0x000CFF6A
		private EventSourceCreationData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSourceCreationData" /> class with a specified event source and event log name.</summary>
		/// <param name="source">The name to register with the event log as a source of entries.</param>
		/// <param name="logName">The name of the log to which entries from the source are written.</param>
		// Token: 0x06002E96 RID: 11926 RVA: 0x000D1D88 File Offset: 0x000CFF88
		public EventSourceCreationData(string source, string logName)
		{
			this._source = source;
			this._logName = logName;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000D1DB4 File Offset: 0x000CFFB4
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = machineName;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000D1DE8 File Offset: 0x000CFFE8
		private EventSourceCreationData(string source, string logName, string machineName, string messageResourceFile, string parameterResourceFile, string categoryResourceFile, short categoryCount)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = machineName;
			this._messageResourceFile = messageResourceFile;
			this._parameterResourceFile = parameterResourceFile;
			this._categoryResourceFile = categoryResourceFile;
			this.CategoryCount = (int)categoryCount;
		}

		/// <summary>Gets or sets the name of the event log to which the source writes entries.</summary>
		/// <returns>The name of the event log. This can be Application, System, or a custom log name. The default value is "Application."</returns>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000D1E46 File Offset: 0x000D0046
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x000D1E4E File Offset: 0x000D004E
		public string LogName
		{
			get
			{
				return this._logName;
			}
			set
			{
				this._logName = value;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to register the event source.</summary>
		/// <returns>The name of the system on which to register the event source. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid.</exception>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000D1E57 File Offset: 0x000D0057
		// (set) Token: 0x06002E9C RID: 11932 RVA: 0x000D1E5F File Offset: 0x000D005F
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
			set
			{
				this._machineName = value;
			}
		}

		/// <summary>Gets or sets the name to register with the event log as an event source.</summary>
		/// <returns>The name to register with the event log as a source of entries. The default is an empty string ("").</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000D1E68 File Offset: 0x000D0068
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x000D1E70 File Offset: 0x000D0070
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		/// <summary>Gets or sets the path of the message resource file that contains message formatting strings for the source.</summary>
		/// <returns>The path of the message resource file. The default is an empty string ("").</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000D1E79 File Offset: 0x000D0079
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000D1E81 File Offset: 0x000D0081
		public string MessageResourceFile
		{
			get
			{
				return this._messageResourceFile;
			}
			set
			{
				this._messageResourceFile = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains message parameter strings for the source.</summary>
		/// <returns>The path of the parameter resource file. The default is an empty string ("").</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000D1E8A File Offset: 0x000D008A
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x000D1E92 File Offset: 0x000D0092
		public string ParameterResourceFile
		{
			get
			{
				return this._parameterResourceFile;
			}
			set
			{
				this._parameterResourceFile = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains category strings for the source.</summary>
		/// <returns>The path of the category resource file. The default is an empty string ("").</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000D1E9B File Offset: 0x000D009B
		// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000D1EA3 File Offset: 0x000D00A3
		public string CategoryResourceFile
		{
			get
			{
				return this._categoryResourceFile;
			}
			set
			{
				this._categoryResourceFile = value;
			}
		}

		/// <summary>Gets or sets the number of categories in the category resource file.</summary>
		/// <returns>The number of categories in the category resource file. The default value is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000D1EAC File Offset: 0x000D00AC
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x000D1EB4 File Offset: 0x000D00B4
		public int CategoryCount
		{
			get
			{
				return this._categoryCount;
			}
			set
			{
				if (value > 65535 || value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryCount = value;
			}
		}

		// Token: 0x0400276B RID: 10091
		private string _logName = "Application";

		// Token: 0x0400276C RID: 10092
		private string _machineName = ".";

		// Token: 0x0400276D RID: 10093
		private string _source;

		// Token: 0x0400276E RID: 10094
		private string _messageResourceFile;

		// Token: 0x0400276F RID: 10095
		private string _parameterResourceFile;

		// Token: 0x04002770 RID: 10096
		private string _categoryResourceFile;

		// Token: 0x04002771 RID: 10097
		private int _categoryCount;
	}
}
