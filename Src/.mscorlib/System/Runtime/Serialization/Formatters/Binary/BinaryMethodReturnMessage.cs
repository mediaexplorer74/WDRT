using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A5 RID: 1957
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x06005511 RID: 21777 RVA: 0x0012F8E4 File Offset: 0x0012DAE4
		[SecurityCritical]
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06005512 RID: 21778 RVA: 0x0012F93F File Offset: 0x0012DB3F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x0012F947 File Offset: 0x0012DB47
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06005514 RID: 21780 RVA: 0x0012F94F File Offset: 0x0012DB4F
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x0012F957 File Offset: 0x0012DB57
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x0012F95F File Offset: 0x0012DB5F
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0012F96C File Offset: 0x0012DB6C
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002721 RID: 10017
		private object[] _outargs;

		// Token: 0x04002722 RID: 10018
		private Exception _exception;

		// Token: 0x04002723 RID: 10019
		private object _returnValue;

		// Token: 0x04002724 RID: 10020
		private object[] _args;

		// Token: 0x04002725 RID: 10021
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04002726 RID: 10022
		private object[] _properties;
	}
}
