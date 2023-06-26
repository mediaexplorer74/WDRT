using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003F4 RID: 1012
	[Serializable]
	internal class LogSwitch
	{
		// Token: 0x0600336A RID: 13162 RVA: 0x000C69E3 File Offset: 0x000C4BE3
		private LogSwitch()
		{
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000C69EC File Offset: 0x000C4BEC
		[SecuritySafeCritical]
		public LogSwitch(string name, string description, LogSwitch parent)
		{
			if (name != null && name.Length == 0)
			{
				throw new ArgumentOutOfRangeException("Name", Environment.GetResourceString("Argument_StringZeroLength"));
			}
			if (name != null && parent != null)
			{
				this.strName = name;
				this.strDescription = description;
				this.iLevel = LoggingLevels.ErrorLevel;
				this.iOldLevel = this.iLevel;
				this.ParentSwitch = parent;
				Log.m_Hashtable.Add(this.strName, this);
				Log.AddLogSwitch(this);
				return;
			}
			throw new ArgumentNullException((name == null) ? "name" : "parent");
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000C6A80 File Offset: 0x000C4C80
		[SecuritySafeCritical]
		internal LogSwitch(string name, string description)
		{
			this.strName = name;
			this.strDescription = description;
			this.iLevel = LoggingLevels.ErrorLevel;
			this.iOldLevel = this.iLevel;
			this.ParentSwitch = null;
			Log.m_Hashtable.Add(this.strName, this);
			Log.AddLogSwitch(this);
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000C6AD9 File Offset: 0x000C4CD9
		public virtual string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000C6AE1 File Offset: 0x000C4CE1
		public virtual string Description
		{
			get
			{
				return this.strDescription;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000C6AE9 File Offset: 0x000C4CE9
		public virtual LogSwitch Parent
		{
			get
			{
				return this.ParentSwitch;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000C6AF1 File Offset: 0x000C4CF1
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x000C6AFC File Offset: 0x000C4CFC
		public virtual LoggingLevels MinimumLevel
		{
			get
			{
				return this.iLevel;
			}
			[SecuritySafeCritical]
			set
			{
				this.iLevel = value;
				this.iOldLevel = value;
				string text = ((this.ParentSwitch != null) ? this.ParentSwitch.Name : "");
				if (Debugger.IsAttached)
				{
					Log.ModifyLogSwitch((int)this.iLevel, this.strName, text);
				}
				Log.InvokeLogSwitchLevelHandlers(this, this.iLevel);
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000C6B5F File Offset: 0x000C4D5F
		public virtual bool CheckLevel(LoggingLevels level)
		{
			return this.iLevel <= level || (this.ParentSwitch != null && this.ParentSwitch.CheckLevel(level));
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000C6B84 File Offset: 0x000C4D84
		public static LogSwitch GetSwitch(string name)
		{
			return (LogSwitch)Log.m_Hashtable[name];
		}

		// Token: 0x040016D1 RID: 5841
		internal string strName;

		// Token: 0x040016D2 RID: 5842
		internal string strDescription;

		// Token: 0x040016D3 RID: 5843
		private LogSwitch ParentSwitch;

		// Token: 0x040016D4 RID: 5844
		internal volatile LoggingLevels iLevel;

		// Token: 0x040016D5 RID: 5845
		internal volatile LoggingLevels iOldLevel;
	}
}
