using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B2 RID: 1970
	internal sealed class IdentityHolder
	{
		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06005588 RID: 21896 RVA: 0x00130E2E File Offset: 0x0012F02E
		internal static Hashtable URITable
		{
			get
			{
				return IdentityHolder._URITable;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06005589 RID: 21897 RVA: 0x00130E35 File Offset: 0x0012F035
		internal static Context DefaultContext
		{
			[SecurityCritical]
			get
			{
				if (IdentityHolder._cachedDefaultContext == null)
				{
					IdentityHolder._cachedDefaultContext = Thread.GetDomain().GetDefaultContext();
				}
				return IdentityHolder._cachedDefaultContext;
			}
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x00130E58 File Offset: 0x0012F058
		private static string MakeURIKey(string uri)
		{
			return Identity.RemoveAppNameOrAppGuidIfNecessary(uri.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x00130E6A File Offset: 0x0012F06A
		private static string MakeURIKeyNoLower(string uri)
		{
			return Identity.RemoveAppNameOrAppGuidIfNecessary(uri);
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600558C RID: 21900 RVA: 0x00130E72 File Offset: 0x0012F072
		internal static ReaderWriterLock TableLock
		{
			get
			{
				return Thread.GetDomain().RemotingData.IDTableLock;
			}
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00130E84 File Offset: 0x0012F084
		private static void CleanupIdentities(object state)
		{
			IDictionaryEnumerator enumerator = IdentityHolder.URITable.GetEnumerator();
			ArrayList arrayList = new ArrayList();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				WeakReference weakReference = value as WeakReference;
				if (weakReference != null && weakReference.Target == null)
				{
					arrayList.Add(enumerator.Key);
				}
			}
			foreach (object obj in arrayList)
			{
				string text = (string)obj;
				IdentityHolder.URITable.Remove(text);
			}
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00130F28 File Offset: 0x0012F128
		[SecurityCritical]
		internal static void FlushIdentityTable()
		{
			ReaderWriterLock tableLock = IdentityHolder.TableLock;
			bool flag = !tableLock.IsWriterLockHeld;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (flag)
				{
					tableLock.AcquireWriterLock(int.MaxValue);
				}
				IdentityHolder.CleanupIdentities(null);
			}
			finally
			{
				if (flag && tableLock.IsWriterLockHeld)
				{
					tableLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x00130F84 File Offset: 0x0012F184
		private IdentityHolder()
		{
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x00130F8C File Offset: 0x0012F18C
		[SecurityCritical]
		internal static Identity ResolveIdentity(string URI)
		{
			if (URI == null)
			{
				throw new ArgumentNullException("URI");
			}
			ReaderWriterLock tableLock = IdentityHolder.TableLock;
			bool flag = !tableLock.IsReaderLockHeld;
			RuntimeHelpers.PrepareConstrainedRegions();
			Identity identity;
			try
			{
				if (flag)
				{
					tableLock.AcquireReaderLock(int.MaxValue);
				}
				identity = IdentityHolder.ResolveReference(IdentityHolder.URITable[IdentityHolder.MakeURIKey(URI)]);
			}
			finally
			{
				if (flag && tableLock.IsReaderLockHeld)
				{
					tableLock.ReleaseReaderLock();
				}
			}
			return identity;
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x00131008 File Offset: 0x0012F208
		[SecurityCritical]
		internal static Identity CasualResolveIdentity(string uri)
		{
			if (uri == null)
			{
				return null;
			}
			Identity identity = IdentityHolder.CasualResolveReference(IdentityHolder.URITable[IdentityHolder.MakeURIKeyNoLower(uri)]);
			if (identity == null)
			{
				identity = IdentityHolder.CasualResolveReference(IdentityHolder.URITable[IdentityHolder.MakeURIKey(uri)]);
				if (identity == null || identity.IsInitializing)
				{
					identity = RemotingConfigHandler.CreateWellKnownObject(uri);
				}
			}
			return identity;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x0013105C File Offset: 0x0012F25C
		private static Identity ResolveReference(object o)
		{
			WeakReference weakReference = o as WeakReference;
			if (weakReference != null)
			{
				return (Identity)weakReference.Target;
			}
			return (Identity)o;
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x00131088 File Offset: 0x0012F288
		private static Identity CasualResolveReference(object o)
		{
			WeakReference weakReference = o as WeakReference;
			if (weakReference != null)
			{
				return (Identity)weakReference.Target;
			}
			return (Identity)o;
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x001310B4 File Offset: 0x0012F2B4
		[SecurityCritical]
		internal static ServerIdentity FindOrCreateServerIdentity(MarshalByRefObject obj, string objURI, int flags)
		{
			ServerIdentity serverIdentity = null;
			bool flag;
			serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(obj, out flag);
			if (serverIdentity == null)
			{
				Context context;
				if (obj is ContextBoundObject)
				{
					context = Thread.CurrentContext;
				}
				else
				{
					context = IdentityHolder.DefaultContext;
				}
				ServerIdentity serverIdentity2 = new ServerIdentity(obj, context);
				if (flag)
				{
					serverIdentity = obj.__RaceSetServerIdentity(serverIdentity2);
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(obj);
					realProxy.IdentityObject = serverIdentity2;
					serverIdentity = (ServerIdentity)realProxy.IdentityObject;
				}
				if (IdOps.bIsInitializing(flags))
				{
					serverIdentity.IsInitializing = true;
				}
			}
			if (IdOps.bStrongIdentity(flags))
			{
				ReaderWriterLock tableLock = IdentityHolder.TableLock;
				bool flag2 = !tableLock.IsWriterLockHeld;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (flag2)
					{
						tableLock.AcquireWriterLock(int.MaxValue);
					}
					if (serverIdentity.ObjURI == null || !serverIdentity.IsInIDTable())
					{
						IdentityHolder.SetIdentity(serverIdentity, objURI, DuplicateIdentityOption.Unique);
					}
					if (serverIdentity.IsDisconnected())
					{
						serverIdentity.SetFullyConnected();
					}
				}
				finally
				{
					if (flag2 && tableLock.IsWriterLockHeld)
					{
						tableLock.ReleaseWriterLock();
					}
				}
			}
			return serverIdentity;
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x001311B0 File Offset: 0x0012F3B0
		[SecurityCritical]
		internal static Identity FindOrCreateIdentity(string objURI, string URL, ObjRef objectRef)
		{
			Identity identity = null;
			bool flag = URL != null;
			identity = IdentityHolder.ResolveIdentity(flag ? URL : objURI);
			if (flag && identity != null && identity is ServerIdentity)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CantDirectlyConnect"), URL));
			}
			if (identity == null)
			{
				identity = new Identity(objURI, URL);
				ReaderWriterLock tableLock = IdentityHolder.TableLock;
				bool flag2 = !tableLock.IsWriterLockHeld;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (flag2)
					{
						tableLock.AcquireWriterLock(int.MaxValue);
					}
					identity = IdentityHolder.SetIdentity(identity, null, DuplicateIdentityOption.UseExisting);
					identity.RaceSetObjRef(objectRef);
				}
				finally
				{
					if (flag2 && tableLock.IsWriterLockHeld)
					{
						tableLock.ReleaseWriterLock();
					}
				}
			}
			return identity;
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x00131260 File Offset: 0x0012F460
		[SecurityCritical]
		private static Identity SetIdentity(Identity idObj, string URI, DuplicateIdentityOption duplicateOption)
		{
			bool flag = idObj is ServerIdentity;
			if (idObj.URI == null)
			{
				idObj.SetOrCreateURI(URI);
				if (idObj.ObjectRef != null)
				{
					idObj.ObjectRef.URI = idObj.URI;
				}
			}
			string text = IdentityHolder.MakeURIKey(idObj.URI);
			object obj = IdentityHolder.URITable[text];
			if (obj != null)
			{
				WeakReference weakReference = obj as WeakReference;
				Identity identity;
				bool flag2;
				if (weakReference != null)
				{
					identity = (Identity)weakReference.Target;
					flag2 = identity is ServerIdentity;
				}
				else
				{
					identity = (Identity)obj;
					flag2 = identity is ServerIdentity;
				}
				if (identity != null && identity != idObj)
				{
					if (duplicateOption == DuplicateIdentityOption.Unique)
					{
						string uri = idObj.URI;
						throw new RemotingException(Environment.GetResourceString("Remoting_URIClash", new object[] { uri }));
					}
					if (duplicateOption == DuplicateIdentityOption.UseExisting)
					{
						idObj = identity;
					}
				}
				else if (weakReference != null)
				{
					if (flag2)
					{
						IdentityHolder.URITable[text] = idObj;
					}
					else
					{
						weakReference.Target = idObj;
					}
				}
			}
			else
			{
				object obj2;
				if (flag)
				{
					obj2 = idObj;
					((ServerIdentity)idObj).SetHandle();
				}
				else
				{
					obj2 = new WeakReference(idObj);
				}
				IdentityHolder.URITable.Add(text, obj2);
				idObj.SetInIDTable();
				IdentityHolder.SetIDCount++;
				if (IdentityHolder.SetIDCount % 64 == 0)
				{
					IdentityHolder.CleanupIdentities(null);
				}
			}
			return idObj;
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x001313AB File Offset: 0x0012F5AB
		[SecurityCritical]
		internal static void RemoveIdentity(string uri)
		{
			IdentityHolder.RemoveIdentity(uri, true);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x001313B4 File Offset: 0x0012F5B4
		[SecurityCritical]
		internal static void RemoveIdentity(string uri, bool bResetURI)
		{
			string text = IdentityHolder.MakeURIKey(uri);
			ReaderWriterLock tableLock = IdentityHolder.TableLock;
			bool flag = !tableLock.IsWriterLockHeld;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (flag)
				{
					tableLock.AcquireWriterLock(int.MaxValue);
				}
				object obj = IdentityHolder.URITable[text];
				WeakReference weakReference = obj as WeakReference;
				Identity identity;
				if (weakReference != null)
				{
					identity = (Identity)weakReference.Target;
					weakReference.Target = null;
				}
				else
				{
					identity = (Identity)obj;
					if (identity != null)
					{
						((ServerIdentity)identity).ResetHandle();
					}
				}
				if (identity != null)
				{
					IdentityHolder.URITable.Remove(text);
					identity.ResetInIDTable(bResetURI);
				}
			}
			finally
			{
				if (flag && tableLock.IsWriterLockHeld)
				{
					tableLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x0013146C File Offset: 0x0012F66C
		[SecurityCritical]
		internal static bool AddDynamicProperty(MarshalByRefObject obj, IDynamicProperty prop)
		{
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				return realProxy.IdentityObject.AddProxySideDynamicProperty(prop);
			}
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)RemotingServices.AlwaysUnwrap((ContextBoundObject)obj);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(marshalByRefObject);
			if (serverIdentity != null)
			{
				return serverIdentity.AddServerSideDynamicProperty(prop);
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x001314CC File Offset: 0x0012F6CC
		[SecurityCritical]
		internal static bool RemoveDynamicProperty(MarshalByRefObject obj, string name)
		{
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				return realProxy.IdentityObject.RemoveProxySideDynamicProperty(name);
			}
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)RemotingServices.AlwaysUnwrap((ContextBoundObject)obj);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(marshalByRefObject);
			if (serverIdentity != null)
			{
				return serverIdentity.RemoveServerSideDynamicProperty(name);
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
		}

		// Token: 0x04002768 RID: 10088
		private static volatile int SetIDCount = 0;

		// Token: 0x04002769 RID: 10089
		private const int CleanUpCountInterval = 64;

		// Token: 0x0400276A RID: 10090
		private const int INFINITE = 2147483647;

		// Token: 0x0400276B RID: 10091
		private static Hashtable _URITable = new Hashtable();

		// Token: 0x0400276C RID: 10092
		private static volatile Context _cachedDefaultContext = null;
	}
}
