using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x0200049B RID: 1179
	[ConfigurationCollection(typeof(ListenerElement))]
	internal class ListenerElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A8C RID: 2700
		public ListenerElement this[string name]
		{
			get
			{
				return (ListenerElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000C5D90 File Offset: 0x000C3F90
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000C5D93 File Offset: 0x000C3F93
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(true);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000C5D9B File Offset: 0x000C3F9B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ListenerElement)element).Name;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000C5DA8 File Offset: 0x000C3FA8
		public TraceListenerCollection GetRuntimeObject()
		{
			TraceListenerCollection traceListenerCollection = new TraceListenerCollection();
			bool flag = false;
			foreach (object obj in this)
			{
				ListenerElement listenerElement = (ListenerElement)obj;
				if (!flag && !listenerElement._isAddedByDefault)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
					flag = true;
				}
				traceListenerCollection.Add(listenerElement.GetRuntimeObject());
			}
			return traceListenerCollection;
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000C5E28 File Offset: 0x000C4028
		protected override void InitializeDefault()
		{
			this.InitializeDefaultInternal();
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000C5E30 File Offset: 0x000C4030
		internal void InitializeDefaultInternal()
		{
			this.BaseAdd(new ListenerElement(false)
			{
				Name = "Default",
				TypeName = typeof(DefaultTraceListener).FullName,
				_isAddedByDefault = true
			});
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000C5E74 File Offset: 0x000C4074
		protected override void BaseAdd(ConfigurationElement element)
		{
			ListenerElement listenerElement = element as ListenerElement;
			if (listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				base.BaseAdd(listenerElement, false);
				return;
			}
			base.BaseAdd(listenerElement, this.ThrowOnDuplicate);
		}
	}
}
