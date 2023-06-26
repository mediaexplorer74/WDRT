using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	/// <summary>Stores all relevant information required to generate a proxy in order to communicate with a remote object.</summary>
	// Token: 0x020007BA RID: 1978
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ObjRef : IObjectReference, ISerializable
	{
		// Token: 0x060055BA RID: 21946 RVA: 0x0013188A File Offset: 0x0012FA8A
		internal void SetServerIdentity(GCHandle hndSrvIdentity)
		{
			this.srvIdentity = hndSrvIdentity;
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x00131893 File Offset: 0x0012FA93
		internal GCHandle GetServerIdentity()
		{
			return this.srvIdentity;
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x0013189B File Offset: 0x0012FA9B
		internal void SetDomainID(int id)
		{
			this.domainID = id;
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x001318A4 File Offset: 0x0012FAA4
		internal int GetDomainID()
		{
			return this.domainID;
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x001318AC File Offset: 0x0012FAAC
		[SecurityCritical]
		private ObjRef(ObjRef o)
		{
			this.uri = o.uri;
			this.typeInfo = o.typeInfo;
			this.envoyInfo = o.envoyInfo;
			this.channelInfo = o.channelInfo;
			this.objrefFlags = o.objrefFlags;
			this.SetServerIdentity(o.GetServerIdentity());
			this.SetDomainID(o.GetDomainID());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class to reference a specified <see cref="T:System.MarshalByRefObject" /> of a specified <see cref="T:System.Type" />.</summary>
		/// <param name="o">The object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> instance will reference.</param>
		/// <param name="requestedType">The <see cref="T:System.Type" /> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> instance will reference.</param>
		// Token: 0x060055BF RID: 21951 RVA: 0x00131914 File Offset: 0x0012FB14
		[SecurityCritical]
		public ObjRef(MarshalByRefObject o, Type requestedType)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			RuntimeType runtimeType = requestedType as RuntimeType;
			if (requestedType != null && runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(o, out flag);
			this.Init(o, identity, runtimeType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		// Token: 0x060055C0 RID: 21952 RVA: 0x00131970 File Offset: 0x0012FB70
		[SecurityCritical]
		protected ObjRef(SerializationInfo info, StreamingContext context)
		{
			string text = null;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("uri"))
				{
					this.uri = (string)enumerator.Value;
				}
				else if (enumerator.Name.Equals("typeInfo"))
				{
					this.typeInfo = (IRemotingTypeInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("envoyInfo"))
				{
					this.envoyInfo = (IEnvoyInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("channelInfo"))
				{
					this.channelInfo = (IChannelInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("objrefFlags"))
				{
					object value = enumerator.Value;
					if (value.GetType() == typeof(string))
					{
						this.objrefFlags = ((IConvertible)value).ToInt32(null);
					}
					else
					{
						this.objrefFlags = (int)value;
					}
				}
				else if (enumerator.Name.Equals("fIsMarshalled"))
				{
					object value2 = enumerator.Value;
					int num;
					if (value2.GetType() == typeof(string))
					{
						num = ((IConvertible)value2).ToInt32(null);
					}
					else
					{
						num = (int)value2;
					}
					if (num == 0)
					{
						flag = true;
					}
				}
				else if (enumerator.Name.Equals("url"))
				{
					text = (string)enumerator.Value;
				}
				else if (enumerator.Name.Equals("SrvIdentity"))
				{
					this.SetServerIdentity((GCHandle)enumerator.Value);
				}
				else if (enumerator.Name.Equals("DomainId"))
				{
					this.SetDomainID((int)enumerator.Value);
				}
			}
			if (!flag)
			{
				this.objrefFlags |= 1;
			}
			else
			{
				this.objrefFlags &= -2;
			}
			if (text != null)
			{
				this.uri = text;
				this.objrefFlags |= 4;
			}
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00131B8C File Offset: 0x0012FD8C
		[SecurityCritical]
		internal bool CanSmuggle()
		{
			if (base.GetType() != typeof(ObjRef) || this.IsObjRefLite())
			{
				return false;
			}
			Type type = null;
			if (this.typeInfo != null)
			{
				type = this.typeInfo.GetType();
			}
			Type type2 = null;
			if (this.channelInfo != null)
			{
				type2 = this.channelInfo.GetType();
			}
			if ((type == null || type == typeof(TypeInfo) || type == typeof(DynamicTypeInfo)) && this.envoyInfo == null && (type2 == null || type2 == typeof(ChannelInfo)))
			{
				if (this.channelInfo != null)
				{
					foreach (object obj in this.channelInfo.ChannelData)
					{
						if (!(obj is CrossAppDomainData))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x00131C6B File Offset: 0x0012FE6B
		[SecurityCritical]
		internal ObjRef CreateSmuggleableCopy()
		{
			return new ObjRef(this);
		}

		/// <summary>Populates a specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The contextual information about the source or destination of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have serialization formatter permission.</exception>
		// Token: 0x060055C3 RID: 21955 RVA: 0x00131C74 File Offset: 0x0012FE74
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(ObjRef.orType);
			if (!this.IsObjRefLite())
			{
				info.AddValue("uri", this.uri, typeof(string));
				info.AddValue("objrefFlags", this.objrefFlags);
				info.AddValue("typeInfo", this.typeInfo, typeof(IRemotingTypeInfo));
				info.AddValue("envoyInfo", this.envoyInfo, typeof(IEnvoyInfo));
				info.AddValue("channelInfo", this.GetChannelInfoHelper(), typeof(IChannelInfo));
				return;
			}
			info.AddValue("url", this.uri, typeof(string));
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00131D3C File Offset: 0x0012FF3C
		[SecurityCritical]
		private IChannelInfo GetChannelInfoHelper()
		{
			ChannelInfo channelInfo = this.channelInfo as ChannelInfo;
			if (channelInfo == null)
			{
				return this.channelInfo;
			}
			object[] channelData = channelInfo.ChannelData;
			if (channelData == null)
			{
				return channelInfo;
			}
			string[] array = (string[])CallContext.GetData("__bashChannelUrl");
			if (array == null)
			{
				return channelInfo;
			}
			string text = array[0];
			string text2 = array[1];
			ChannelInfo channelInfo2 = new ChannelInfo();
			channelInfo2.ChannelData = new object[channelData.Length];
			for (int i = 0; i < channelData.Length; i++)
			{
				channelInfo2.ChannelData[i] = channelData[i];
				ChannelDataStore channelDataStore = channelInfo2.ChannelData[i] as ChannelDataStore;
				if (channelDataStore != null)
				{
					string[] channelUris = channelDataStore.ChannelUris;
					if (channelUris != null && channelUris.Length == 1 && channelUris[0].Equals(text))
					{
						ChannelDataStore channelDataStore2 = channelDataStore.InternalShallowCopy();
						channelDataStore2.ChannelUris = new string[1];
						channelDataStore2.ChannelUris[0] = text2;
						channelInfo2.ChannelData[i] = channelDataStore2;
					}
				}
			}
			return channelInfo2;
		}

		/// <summary>Gets or sets the URI of the specific object instance.</summary>
		/// <returns>The URI of the specific object instance.</returns>
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060055C5 RID: 21957 RVA: 0x00131E23 File Offset: 0x00130023
		// (set) Token: 0x060055C6 RID: 21958 RVA: 0x00131E2B File Offset: 0x0013002B
		public virtual string URI
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> for the object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> for the object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</returns>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060055C7 RID: 21959 RVA: 0x00131E34 File Offset: 0x00130034
		// (set) Token: 0x060055C8 RID: 21960 RVA: 0x00131E3C File Offset: 0x0013003C
		public virtual IRemotingTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				this.typeInfo = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> interface for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060055C9 RID: 21961 RVA: 0x00131E45 File Offset: 0x00130045
		// (set) Token: 0x060055CA RID: 21962 RVA: 0x00131E4D File Offset: 0x0013004D
		public virtual IEnvoyInfo EnvoyInfo
		{
			get
			{
				return this.envoyInfo;
			}
			set
			{
				this.envoyInfo = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IChannelInfo" /> for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IChannelInfo" /> interface for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060055CB RID: 21963 RVA: 0x00131E56 File Offset: 0x00130056
		// (set) Token: 0x060055CC RID: 21964 RVA: 0x00131E5E File Offset: 0x0013005E
		public virtual IChannelInfo ChannelInfo
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.channelInfo;
			}
			set
			{
				this.channelInfo = value;
			}
		}

		/// <summary>Returns a reference to the remote object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</summary>
		/// <param name="context">The context where the current object resides.</param>
		/// <returns>A reference to the remote object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have serialization formatter permission.</exception>
		// Token: 0x060055CD RID: 21965 RVA: 0x00131E67 File Offset: 0x00130067
		[SecurityCritical]
		public virtual object GetRealObject(StreamingContext context)
		{
			return this.GetRealObjectHelper();
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x00131E70 File Offset: 0x00130070
		[SecurityCritical]
		internal object GetRealObjectHelper()
		{
			if (!this.IsMarshaledObject())
			{
				return this;
			}
			if (this.IsObjRefLite())
			{
				int num = this.uri.IndexOf(RemotingConfiguration.ApplicationId);
				if (num > 0)
				{
					this.uri = this.uri.Substring(num - 1);
				}
			}
			bool flag = !(base.GetType() == typeof(ObjRef));
			object obj = RemotingServices.Unmarshal(this, flag);
			return this.GetCustomMarshaledCOMObject(obj);
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x00131EE4 File Offset: 0x001300E4
		[SecurityCritical]
		private object GetCustomMarshaledCOMObject(object ret)
		{
			DynamicTypeInfo dynamicTypeInfo = this.TypeInfo as DynamicTypeInfo;
			if (dynamicTypeInfo != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.IsFromThisProcess() && !this.IsFromThisAppDomain())
				{
					try
					{
						bool flag;
						intPtr = ((__ComObject)ret).GetIUnknown(out flag);
						if (intPtr != IntPtr.Zero && !flag)
						{
							string typeName = this.TypeInfo.TypeName;
							string text = null;
							string text2 = null;
							System.Runtime.Remoting.TypeInfo.ParseTypeAndAssembly(typeName, out text, out text2);
							Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(text2);
							if (assembly == null)
							{
								throw new RemotingException(Environment.GetResourceString("Serialization_AssemblyNotFound", new object[] { text2 }));
							}
							Type type = assembly.GetType(text, false, false);
							if (type != null && !type.IsVisible)
							{
								type = null;
							}
							object typedObjectForIUnknown = Marshal.GetTypedObjectForIUnknown(intPtr, type);
							if (typedObjectForIUnknown != null)
							{
								ret = typedObjectForIUnknown;
							}
						}
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							Marshal.Release(intPtr);
						}
					}
				}
			}
			return ret;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with default values.</summary>
		// Token: 0x060055D0 RID: 21968 RVA: 0x00131FEC File Offset: 0x001301EC
		public ObjRef()
		{
			this.objrefFlags = 0;
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x00131FFB File Offset: 0x001301FB
		internal bool IsMarshaledObject()
		{
			return (this.objrefFlags & 1) == 1;
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x00132008 File Offset: 0x00130208
		internal void SetMarshaledObject()
		{
			this.objrefFlags |= 1;
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x00132018 File Offset: 0x00130218
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsWellKnown()
		{
			return (this.objrefFlags & 2) == 2;
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x00132025 File Offset: 0x00130225
		internal void SetWellKnown()
		{
			this.objrefFlags |= 2;
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x00132035 File Offset: 0x00130235
		internal bool HasProxyAttribute()
		{
			return (this.objrefFlags & 8) == 8;
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x00132042 File Offset: 0x00130242
		internal void SetHasProxyAttribute()
		{
			this.objrefFlags |= 8;
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x00132052 File Offset: 0x00130252
		internal bool IsObjRefLite()
		{
			return (this.objrefFlags & 4) == 4;
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x0013205F File Offset: 0x0013025F
		internal void SetObjRefLite()
		{
			this.objrefFlags |= 4;
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x00132070 File Offset: 0x00130270
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private CrossAppDomainData GetAppDomainChannelData()
		{
			for (int i = 0; i < this.ChannelInfo.ChannelData.Length; i++)
			{
				CrossAppDomainData crossAppDomainData = this.ChannelInfo.ChannelData[i] as CrossAppDomainData;
				if (crossAppDomainData != null)
				{
					return crossAppDomainData;
				}
			}
			return null;
		}

		/// <summary>Returns a Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current process.</summary>
		/// <returns>A Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current process.</returns>
		// Token: 0x060055DA RID: 21978 RVA: 0x001320B0 File Offset: 0x001302B0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool IsFromThisProcess()
		{
			if (this.IsWellKnown())
			{
				return false;
			}
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData != null && appDomainChannelData.IsFromThisProcess();
		}

		/// <summary>Returns a Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current <see cref="T:System.AppDomain" />.</summary>
		/// <returns>A Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current <see cref="T:System.AppDomain" />.</returns>
		// Token: 0x060055DB RID: 21979 RVA: 0x001320DC File Offset: 0x001302DC
		[SecurityCritical]
		public bool IsFromThisAppDomain()
		{
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData != null && appDomainChannelData.IsFromThisAppDomain();
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x001320FC File Offset: 0x001302FC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int GetServerDomainId()
		{
			if (!this.IsFromThisProcess())
			{
				return 0;
			}
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData.DomainID;
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x00132120 File Offset: 0x00130320
		[SecurityCritical]
		internal IntPtr GetServerContext(out int domainId)
		{
			IntPtr intPtr = IntPtr.Zero;
			domainId = 0;
			if (this.IsFromThisProcess())
			{
				CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
				domainId = appDomainChannelData.DomainID;
				if (AppDomain.IsDomainIdValid(appDomainChannelData.DomainID))
				{
					intPtr = appDomainChannelData.ContextID;
				}
			}
			return intPtr;
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x00132164 File Offset: 0x00130364
		[SecurityCritical]
		internal void Init(object o, Identity idObj, RuntimeType requestedType)
		{
			this.uri = idObj.URI;
			MarshalByRefObject tporObject = idObj.TPOrObject;
			RuntimeType runtimeType;
			if (!RemotingServices.IsTransparentProxy(tporObject))
			{
				runtimeType = (RuntimeType)tporObject.GetType();
			}
			else
			{
				runtimeType = (RuntimeType)RemotingServices.GetRealProxy(tporObject).GetProxiedType();
			}
			RuntimeType runtimeType2 = ((null == requestedType) ? runtimeType : requestedType);
			if (null != requestedType && !requestedType.IsAssignableFrom(runtimeType) && !typeof(IMessageSink).IsAssignableFrom(runtimeType))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidRequestedType"), requestedType.ToString()));
			}
			if (runtimeType.IsCOMObject)
			{
				DynamicTypeInfo dynamicTypeInfo = new DynamicTypeInfo(runtimeType2);
				this.TypeInfo = dynamicTypeInfo;
			}
			else
			{
				RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(runtimeType2);
				this.TypeInfo = reflectionCachedData.TypeInfo;
			}
			if (!idObj.IsWellKnown())
			{
				this.EnvoyInfo = System.Runtime.Remoting.EnvoyInfo.CreateEnvoyInfo(idObj as ServerIdentity);
				IChannelInfo channelInfo = new ChannelInfo();
				if (o is AppDomain)
				{
					object[] channelData = channelInfo.ChannelData;
					int num = channelData.Length;
					object[] array = new object[num];
					Array.Copy(channelData, array, num);
					for (int i = 0; i < num; i++)
					{
						if (!(array[i] is CrossAppDomainData))
						{
							array[i] = null;
						}
					}
					channelInfo.ChannelData = array;
				}
				this.ChannelInfo = channelInfo;
				if (runtimeType.HasProxyAttribute)
				{
					this.SetHasProxyAttribute();
				}
			}
			else
			{
				this.SetWellKnown();
			}
			if (ObjRef.ShouldUseUrlObjRef())
			{
				if (this.IsWellKnown())
				{
					this.SetObjRefLite();
					return;
				}
				string text = ChannelServices.FindFirstHttpUrlForObject(this.URI);
				if (text != null)
				{
					this.URI = text;
					this.SetObjRefLite();
				}
			}
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x001322F9 File Offset: 0x001304F9
		internal static bool ShouldUseUrlObjRef()
		{
			return RemotingConfigHandler.UrlObjRefMode;
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x00132300 File Offset: 0x00130500
		[SecurityCritical]
		internal static bool IsWellFormed(ObjRef objectRef)
		{
			bool flag = true;
			if (objectRef == null || objectRef.URI == null || (!objectRef.IsWellKnown() && !objectRef.IsObjRefLite() && !(objectRef.GetType() != ObjRef.orType) && objectRef.ChannelInfo == null))
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04002772 RID: 10098
		internal const int FLG_MARSHALED_OBJECT = 1;

		// Token: 0x04002773 RID: 10099
		internal const int FLG_WELLKNOWN_OBJREF = 2;

		// Token: 0x04002774 RID: 10100
		internal const int FLG_LITE_OBJREF = 4;

		// Token: 0x04002775 RID: 10101
		internal const int FLG_PROXY_ATTRIBUTE = 8;

		// Token: 0x04002776 RID: 10102
		internal string uri;

		// Token: 0x04002777 RID: 10103
		internal IRemotingTypeInfo typeInfo;

		// Token: 0x04002778 RID: 10104
		internal IEnvoyInfo envoyInfo;

		// Token: 0x04002779 RID: 10105
		internal IChannelInfo channelInfo;

		// Token: 0x0400277A RID: 10106
		internal int objrefFlags;

		// Token: 0x0400277B RID: 10107
		internal GCHandle srvIdentity;

		// Token: 0x0400277C RID: 10108
		internal int domainID;

		// Token: 0x0400277D RID: 10109
		private static Type orType = typeof(ObjRef);
	}
}
