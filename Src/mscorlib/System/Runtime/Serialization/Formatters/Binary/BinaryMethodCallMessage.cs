using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A4 RID: 1956
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x06005508 RID: 21768 RVA: 0x0012F7F8 File Offset: 0x0012D9F8
		[SecurityCritical]
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
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

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x0012F866 File Offset: 0x0012DA66
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x0012F86E File Offset: 0x0012DA6E
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x0012F876 File Offset: 0x0012DA76
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x0012F87E File Offset: 0x0012DA7E
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x0012F886 File Offset: 0x0012DA86
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x0012F88E File Offset: 0x0012DA8E
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600550F RID: 21775 RVA: 0x0012F896 File Offset: 0x0012DA96
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0012F8A4 File Offset: 0x0012DAA4
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002719 RID: 10009
		private object[] _inargs;

		// Token: 0x0400271A RID: 10010
		private string _methodName;

		// Token: 0x0400271B RID: 10011
		private string _typeName;

		// Token: 0x0400271C RID: 10012
		private object _methodSignature;

		// Token: 0x0400271D RID: 10013
		private Type[] _instArgs;

		// Token: 0x0400271E RID: 10014
		private object[] _args;

		// Token: 0x0400271F RID: 10015
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04002720 RID: 10016
		private object[] _properties;
	}
}
