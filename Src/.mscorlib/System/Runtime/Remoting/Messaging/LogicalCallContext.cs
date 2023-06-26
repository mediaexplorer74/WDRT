using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a set of properties that are carried with the execution code path during remote method calls.</summary>
	// Token: 0x0200088C RID: 2188
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class LogicalCallContext : ISerializable, ICloneable
	{
		// Token: 0x06005CDA RID: 23770 RVA: 0x00146A82 File Offset: 0x00144C82
		internal LogicalCallContext()
		{
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x00146A8C File Offset: 0x00144C8C
		[SecurityCritical]
		internal LogicalCallContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__RemotingData"))
				{
					this.m_RemotingData = (CallContextRemotingData)enumerator.Value;
				}
				else if (enumerator.Name.Equals("__SecurityData"))
				{
					if (context.State == StreamingContextStates.CrossAppDomain)
					{
						this.m_SecurityData = (CallContextSecurityData)enumerator.Value;
					}
				}
				else if (enumerator.Name.Equals("__HostContext"))
				{
					this.m_HostContext = enumerator.Value;
				}
				else if (enumerator.Name.Equals("__CorrelationMgrSlotPresent"))
				{
					this.m_IsCorrelationMgr = (bool)enumerator.Value;
				}
				else
				{
					this.Datastore[enumerator.Name] = enumerator.Value;
				}
			}
		}

		/// <summary>Populates a specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The contextual information about the source or destination of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have SerializationFormatter permission.</exception>
		// Token: 0x06005CDC RID: 23772 RVA: 0x00146B70 File Offset: 0x00144D70
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(LogicalCallContext.s_callContextType);
			if (this.m_RemotingData != null)
			{
				info.AddValue("__RemotingData", this.m_RemotingData);
			}
			if (this.m_SecurityData != null && context.State == StreamingContextStates.CrossAppDomain)
			{
				info.AddValue("__SecurityData", this.m_SecurityData);
			}
			if (this.m_HostContext != null)
			{
				info.AddValue("__HostContext", this.m_HostContext);
			}
			if (this.m_IsCorrelationMgr)
			{
				info.AddValue("__CorrelationMgrSlotPresent", this.m_IsCorrelationMgr);
			}
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					info.AddValue((string)enumerator.Key, enumerator.Value);
				}
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06005CDD RID: 23773 RVA: 0x00146C40 File Offset: 0x00144E40
		[SecuritySafeCritical]
		public object Clone()
		{
			LogicalCallContext logicalCallContext = new LogicalCallContext();
			if (this.m_RemotingData != null)
			{
				logicalCallContext.m_RemotingData = (CallContextRemotingData)this.m_RemotingData.Clone();
			}
			if (this.m_SecurityData != null)
			{
				logicalCallContext.m_SecurityData = (CallContextSecurityData)this.m_SecurityData.Clone();
			}
			if (this.m_HostContext != null)
			{
				logicalCallContext.m_HostContext = this.m_HostContext;
			}
			logicalCallContext.m_IsCorrelationMgr = this.m_IsCorrelationMgr;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				if (!this.m_IsCorrelationMgr)
				{
					while (enumerator.MoveNext())
					{
						logicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
					}
				}
				else
				{
					while (enumerator.MoveNext())
					{
						string text = (string)enumerator.Key;
						if (text.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
						{
							logicalCallContext.Datastore[text] = ((ICloneable)enumerator.Value).Clone();
						}
						else
						{
							logicalCallContext.Datastore[text] = enumerator.Value;
						}
					}
				}
			}
			return logicalCallContext;
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x00146D48 File Offset: 0x00144F48
		[SecurityCritical]
		internal void Merge(LogicalCallContext lc)
		{
			if (lc != null && this != lc && lc.HasUserData)
			{
				IDictionaryEnumerator enumerator = lc.Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> contains information.</summary>
		/// <returns>A Boolean value indicating whether the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> contains information.</returns>
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06005CDF RID: 23775 RVA: 0x00146D98 File Offset: 0x00144F98
		public bool HasInfo
		{
			[SecurityCritical]
			get
			{
				bool flag = false;
				if ((this.m_RemotingData != null && this.m_RemotingData.HasInfo) || (this.m_SecurityData != null && this.m_SecurityData.HasInfo) || this.m_HostContext != null || this.HasUserData)
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06005CE0 RID: 23776 RVA: 0x00146DE4 File Offset: 0x00144FE4
		private bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06005CE1 RID: 23777 RVA: 0x00146DFE File Offset: 0x00144FFE
		internal CallContextRemotingData RemotingData
		{
			get
			{
				if (this.m_RemotingData == null)
				{
					this.m_RemotingData = new CallContextRemotingData();
				}
				return this.m_RemotingData;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x00146E19 File Offset: 0x00145019
		internal CallContextSecurityData SecurityData
		{
			get
			{
				if (this.m_SecurityData == null)
				{
					this.m_SecurityData = new CallContextSecurityData();
				}
				return this.m_SecurityData;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06005CE3 RID: 23779 RVA: 0x00146E34 File Offset: 0x00145034
		// (set) Token: 0x06005CE4 RID: 23780 RVA: 0x00146E3C File Offset: 0x0014503C
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06005CE5 RID: 23781 RVA: 0x00146E45 File Offset: 0x00145045
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06005CE6 RID: 23782 RVA: 0x00146E60 File Offset: 0x00145060
		// (set) Token: 0x06005CE7 RID: 23783 RVA: 0x00146E77 File Offset: 0x00145077
		internal IPrincipal Principal
		{
			get
			{
				if (this.m_SecurityData != null)
				{
					return this.m_SecurityData.Principal;
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				this.SecurityData.Principal = value;
			}
		}

		/// <summary>Empties a data slot with the specified name.</summary>
		/// <param name="name">The name of the data slot to empty.</param>
		// Token: 0x06005CE8 RID: 23784 RVA: 0x00146E85 File Offset: 0x00145085
		[SecurityCritical]
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		/// <summary>Retrieves an object associated with the specified name from the current instance.</summary>
		/// <param name="name">The name of the item in the call context.</param>
		/// <returns>The object in the logical call context associated with the specified name.</returns>
		// Token: 0x06005CE9 RID: 23785 RVA: 0x00146E93 File Offset: 0x00145093
		[SecurityCritical]
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		/// <summary>Stores the specified object in the current instance, and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the call context.</param>
		/// <param name="data">The object to store in the call context.</param>
		// Token: 0x06005CEA RID: 23786 RVA: 0x00146EA1 File Offset: 0x001450A1
		[SecurityCritical]
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
			if (name.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
			{
				this.m_IsCorrelationMgr = true;
			}
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x00146EC4 File Offset: 0x001450C4
		private Header[] InternalGetOutgoingHeaders()
		{
			Header[] sendHeaders = this._sendHeaders;
			this._sendHeaders = null;
			this._recvHeaders = null;
			return sendHeaders;
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x00146EE7 File Offset: 0x001450E7
		internal void InternalSetHeaders(Header[] headers)
		{
			this._sendHeaders = headers;
			this._recvHeaders = null;
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x00146EF7 File Offset: 0x001450F7
		internal Header[] InternalGetHeaders()
		{
			if (this._sendHeaders != null)
			{
				return this._sendHeaders;
			}
			return this._recvHeaders;
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x00146F10 File Offset: 0x00145110
		[SecurityCritical]
		internal IPrincipal RemovePrincipalIfNotSerializable()
		{
			IPrincipal principal = this.Principal;
			if (principal != null && !principal.GetType().IsSerializable)
			{
				this.Principal = null;
			}
			return principal;
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x00146F3C File Offset: 0x0014513C
		[SecurityCritical]
		internal void PropagateOutgoingHeadersToMessage(IMessage msg)
		{
			Header[] array = this.InternalGetOutgoingHeaders();
			if (array != null)
			{
				IDictionary properties = msg.Properties;
				foreach (Header header in array)
				{
					if (header != null)
					{
						string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
						properties[propertyKeyForHeader] = header;
					}
				}
			}
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x00146F86 File Offset: 0x00145186
		internal static string GetPropertyKeyForHeader(Header header)
		{
			if (header == null)
			{
				return null;
			}
			if (header.HeaderNamespace != null)
			{
				return header.Name + ", " + header.HeaderNamespace;
			}
			return header.Name;
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x00146FB4 File Offset: 0x001451B4
		[SecurityCritical]
		internal void PropagateIncomingHeadersToCallContext(IMessage msg)
		{
			IInternalMessage internalMessage = msg as IInternalMessage;
			if (internalMessage != null && !internalMessage.HasProperties())
			{
				return;
			}
			IDictionary properties = msg.Properties;
			IDictionaryEnumerator enumerator = properties.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				if (!text.StartsWith("__", StringComparison.Ordinal) && enumerator.Value is Header)
				{
					num++;
				}
			}
			Header[] array = null;
			if (num > 0)
			{
				array = new Header[num];
				num = 0;
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					string text2 = (string)enumerator.Key;
					if (!text2.StartsWith("__", StringComparison.Ordinal))
					{
						Header header = enumerator.Value as Header;
						if (header != null)
						{
							array[num++] = header;
						}
					}
				}
			}
			this._recvHeaders = array;
			this._sendHeaders = null;
		}

		// Token: 0x040029E1 RID: 10721
		private static Type s_callContextType = typeof(LogicalCallContext);

		// Token: 0x040029E2 RID: 10722
		private const string s_CorrelationMgrSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x040029E3 RID: 10723
		private Hashtable m_Datastore;

		// Token: 0x040029E4 RID: 10724
		private CallContextRemotingData m_RemotingData;

		// Token: 0x040029E5 RID: 10725
		private CallContextSecurityData m_SecurityData;

		// Token: 0x040029E6 RID: 10726
		private object m_HostContext;

		// Token: 0x040029E7 RID: 10727
		private bool m_IsCorrelationMgr;

		// Token: 0x040029E8 RID: 10728
		private Header[] _sendHeaders;

		// Token: 0x040029E9 RID: 10729
		private Header[] _recvHeaders;

		// Token: 0x02000C77 RID: 3191
		internal struct Reader
		{
			// Token: 0x060070DB RID: 28891 RVA: 0x001863E3 File Offset: 0x001845E3
			public Reader(LogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x17001358 RID: 4952
			// (get) Token: 0x060070DC RID: 28892 RVA: 0x001863EC File Offset: 0x001845EC
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x17001359 RID: 4953
			// (get) Token: 0x060070DD RID: 28893 RVA: 0x001863F7 File Offset: 0x001845F7
			public bool HasInfo
			{
				get
				{
					return !this.IsNull && this.m_ctx.HasInfo;
				}
			}

			// Token: 0x060070DE RID: 28894 RVA: 0x0018640E File Offset: 0x0018460E
			public LogicalCallContext Clone()
			{
				return (LogicalCallContext)this.m_ctx.Clone();
			}

			// Token: 0x1700135A RID: 4954
			// (get) Token: 0x060070DF RID: 28895 RVA: 0x00186420 File Offset: 0x00184620
			public IPrincipal Principal
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.Principal;
					}
					return null;
				}
			}

			// Token: 0x060070E0 RID: 28896 RVA: 0x00186437 File Offset: 0x00184637
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x1700135B RID: 4955
			// (get) Token: 0x060070E1 RID: 28897 RVA: 0x0018644F File Offset: 0x0018464F
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x0400380A RID: 14346
			private LogicalCallContext m_ctx;
		}
	}
}
