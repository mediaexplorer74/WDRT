using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting
{
	/// <summary>Provides several methods for using and publishing remoted objects and proxies. This class cannot be inherited.</summary>
	// Token: 0x020007C8 RID: 1992
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class RemotingServices
	{
		/// <summary>Returns a Boolean value that indicates whether the given object is a transparent proxy or a real object.</summary>
		/// <param name="proxy">The reference to the object to check.</param>
		/// <returns>A Boolean value that indicates whether the object specified in the <paramref name="proxy" /> parameter is a transparent proxy or a real object.</returns>
		// Token: 0x06005639 RID: 22073
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTransparentProxy(object proxy);

		/// <summary>Returns a Boolean value that indicates whether the object represented by the given proxy is contained in a different context than the object that called the current method.</summary>
		/// <param name="tp">The object to check.</param>
		/// <returns>
		///   <see langword="true" /> if the object is out of the current context; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600563A RID: 22074 RVA: 0x00132DF0 File Offset: 0x00130FF0
		[SecuritySafeCritical]
		public static bool IsObjectOutOfContext(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			return serverIdentity == null || !(realProxy is RemotingProxy) || Thread.CurrentContext != serverIdentity.ServerContext;
		}

		/// <summary>Returns a Boolean value that indicates whether the object specified by the given transparent proxy is contained in a different application domain than the object that called the current method.</summary>
		/// <param name="tp">The object to check.</param>
		/// <returns>
		///   <see langword="true" /> if the object is out of the current application domain; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600563B RID: 22075 RVA: 0x00132E39 File Offset: 0x00131039
		[__DynamicallyInvokable]
		public static bool IsObjectOutOfAppDomain(object tp)
		{
			return RemotingServices.IsClientProxy(tp);
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x00132E44 File Offset: 0x00131044
		internal static bool IsClientProxy(object obj)
		{
			MarshalByRefObject marshalByRefObject = obj as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			bool flag = false;
			bool flag2;
			Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out flag2);
			if (identity != null && !(identity is ServerIdentity))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x00132E78 File Offset: 0x00131078
		[SecurityCritical]
		internal static bool IsObjectOutOfProcess(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject is ServerIdentity)
			{
				return false;
			}
			if (identityObject != null)
			{
				ObjRef objectRef = identityObject.ObjectRef;
				return objectRef == null || !objectRef.IsFromThisProcess();
			}
			return true;
		}

		/// <summary>Returns the real proxy backing the specified transparent proxy.</summary>
		/// <param name="proxy">A transparent proxy.</param>
		/// <returns>The real proxy instance backing the transparent proxy.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600563E RID: 22078
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RealProxy GetRealProxy(object proxy);

		// Token: 0x0600563F RID: 22079
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateTransparentProxy(RealProxy rp, RuntimeType typeToProxy, IntPtr stub, object stubData);

		// Token: 0x06005640 RID: 22080 RVA: 0x00132EC4 File Offset: 0x001310C4
		[SecurityCritical]
		internal static object CreateTransparentProxy(RealProxy rp, Type typeToProxy, IntPtr stub, object stubData)
		{
			RuntimeType runtimeType = typeToProxy as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "typeToProxy"));
			}
			return RemotingServices.CreateTransparentProxy(rp, runtimeType, stub, stubData);
		}

		// Token: 0x06005641 RID: 22081
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateUninitializedObject(RuntimeType objectType);

		// Token: 0x06005642 RID: 22082
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CallDefaultCtor(object o);

		// Token: 0x06005643 RID: 22083 RVA: 0x00132F0C File Offset: 0x0013110C
		[SecurityCritical]
		internal static MarshalByRefObject AllocateUninitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "objectType"));
			}
			return RemotingServices.AllocateUninitializedObject(runtimeType);
		}

		// Token: 0x06005644 RID: 22084
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateInitializedObject(RuntimeType objectType);

		// Token: 0x06005645 RID: 22085 RVA: 0x00132F50 File Offset: 0x00131150
		[SecurityCritical]
		internal static MarshalByRefObject AllocateInitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "objectType"));
			}
			return RemotingServices.AllocateInitializedObject(runtimeType);
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x00132F94 File Offset: 0x00131194
		[SecurityCritical]
		internal static bool RegisterWellKnownChannels()
		{
			if (!RemotingServices.s_bRegisteredWellKnownChannels)
			{
				bool flag = false;
				object configLock = Thread.GetDomain().RemotingData.ConfigLock;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(configLock, ref flag);
					if (!RemotingServices.s_bRegisteredWellKnownChannels && !RemotingServices.s_bInProcessOfRegisteringWellKnownChannels)
					{
						RemotingServices.s_bInProcessOfRegisteringWellKnownChannels = true;
						CrossAppDomainChannel.RegisterChannel();
						RemotingServices.s_bRegisteredWellKnownChannels = true;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(configLock);
					}
				}
			}
			return true;
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x0013300C File Offset: 0x0013120C
		[SecurityCritical]
		internal static void InternalSetRemoteActivationConfigured()
		{
			if (!RemotingServices.s_bRemoteActivationConfigured)
			{
				RemotingServices.nSetRemoteActivationConfigured();
				RemotingServices.s_bRemoteActivationConfigured = true;
			}
		}

		// Token: 0x06005648 RID: 22088
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nSetRemoteActivationConfigured();

		/// <summary>Retrieves a session ID for a message.</summary>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> for which a session ID is requested.</param>
		/// <returns>A session ID string that uniquely identifies the current session.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005649 RID: 22089 RVA: 0x00133024 File Offset: 0x00131224
		[SecurityCritical]
		public static string GetSessionIdForMethodMessage(IMethodMessage msg)
		{
			return msg.Uri;
		}

		/// <summary>Returns a lifetime service object that controls the lifetime policy of the specified object.</summary>
		/// <param name="obj">The object to obtain lifetime service for.</param>
		/// <returns>The object that controls the lifetime of <paramref name="obj" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600564A RID: 22090 RVA: 0x0013302C File Offset: 0x0013122C
		[SecuritySafeCritical]
		public static object GetLifetimeService(MarshalByRefObject obj)
		{
			if (obj != null)
			{
				return obj.GetLifetimeService();
			}
			return null;
		}

		/// <summary>Retrieves the URI for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.MarshalByRefObject" /> for which a URI is requested.</param>
		/// <returns>The URI of the specified object if it has one, or <see langword="null" /> if the object has not yet been marshaled.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600564B RID: 22091 RVA: 0x0013303C File Offset: 0x0013123C
		[SecurityCritical]
		public static string GetObjectUri(MarshalByRefObject obj)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity != null)
			{
				return identity.URI;
			}
			return null;
		}

		/// <summary>Sets the URI for the subsequent call to the <see cref="M:System.Runtime.Remoting.RemotingServices.Marshal(System.MarshalByRefObject)" /> method.</summary>
		/// <param name="obj">The object to set a URI for.</param>
		/// <param name="uri">The URI to assign to the specified object.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="obj" /> is not a local object, has already been marshaled, or the current method has already been called on.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600564C RID: 22092 RVA: 0x00133060 File Offset: 0x00131260
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			Identity identity2 = identity as ServerIdentity;
			if (identity != null && identity2 == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__ObjectNeedsToBeLocal"));
			}
			if (identity != null && identity.URI != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
			}
			if (identity == null)
			{
				Context defaultContext = Thread.GetDomain().GetDefaultContext();
				ServerIdentity serverIdentity = new ServerIdentity(obj, defaultContext, uri);
				identity = obj.__RaceSetServerIdentity(serverIdentity);
				if (identity != serverIdentity)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
				}
			}
			else
			{
				identity.SetOrCreateURI(uri, true);
			}
		}

		/// <summary>Takes a <see cref="T:System.MarshalByRefObject" />, registers it with the remoting infrastructure, and converts it into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class.</summary>
		/// <param name="Obj">The object to convert.</param>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The <paramref name="Obj" /> parameter is an object proxy.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600564D RID: 22093 RVA: 0x001330F2 File Offset: 0x001312F2
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj)
		{
			return RemotingServices.MarshalInternal(Obj, null, null);
		}

		/// <summary>Converts the given <see cref="T:System.MarshalByRefObject" /> into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with the specified URI.</summary>
		/// <param name="Obj">The object to convert.</param>
		/// <param name="URI">The specified URI with which to initialize the new <see cref="T:System.Runtime.Remoting.ObjRef" />. Can be <see langword="null" />.</param>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="Obj" /> is an object proxy, and the <paramref name="URI" /> parameter is not <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600564E RID: 22094 RVA: 0x001330FC File Offset: 0x001312FC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
		{
			return RemotingServices.MarshalInternal(Obj, URI, null);
		}

		/// <summary>Takes a <see cref="T:System.MarshalByRefObject" /> and converts it into an instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with the specified URI, and the provided <see cref="T:System.Type" />.</summary>
		/// <param name="Obj">The object to convert into a <see cref="T:System.Runtime.Remoting.ObjRef" />.</param>
		/// <param name="ObjURI">The URI the object specified in the <paramref name="Obj" /> parameter is marshaled with. Can be <see langword="null" />.</param>
		/// <param name="RequestedType">The <see cref="T:System.Type" /><paramref name="Obj" /> is marshaled as. Can be <see langword="null" />.</param>
		/// <returns>An instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class that represents the object specified in the <paramref name="Obj" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="Obj" /> is a proxy of a remote object, and the <paramref name="ObjUri" /> parameter is not <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600564F RID: 22095 RVA: 0x00133106 File Offset: 0x00131306
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType);
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x00133110 File Offset: 0x00131310
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, true);
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x0013311B File Offset: 0x0013131B
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, updateChannelData, false);
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x00133128 File Offset: 0x00131328
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData, bool isInitializing)
		{
			if (Obj == null)
			{
				return null;
			}
			ObjRef objRef = null;
			Identity orCreateIdentity = RemotingServices.GetOrCreateIdentity(Obj, ObjURI, isInitializing);
			if (RequestedType != null)
			{
				ServerIdentity serverIdentity = orCreateIdentity as ServerIdentity;
				if (serverIdentity != null)
				{
					serverIdentity.ServerType = RequestedType;
					serverIdentity.MarshaledAsSpecificType = true;
				}
			}
			objRef = orCreateIdentity.ObjectRef;
			if (objRef == null)
			{
				if (RemotingServices.IsTransparentProxy(Obj))
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
					objRef = realProxy.CreateObjRef(RequestedType);
				}
				else
				{
					objRef = Obj.CreateObjRef(RequestedType);
				}
				if (orCreateIdentity == null || objRef == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMarshalByRefObject"), "Obj");
				}
				objRef = orCreateIdentity.RaceSetObjRef(objRef);
			}
			ServerIdentity serverIdentity2 = orCreateIdentity as ServerIdentity;
			if (serverIdentity2 != null)
			{
				MarshalByRefObject marshalByRefObject = null;
				serverIdentity2.GetServerObjectChain(out marshalByRefObject);
				Lease lease = orCreateIdentity.Lease;
				if (lease != null)
				{
					Lease lease2 = lease;
					lock (lease2)
					{
						if (lease.CurrentState == LeaseState.Expired)
						{
							lease.ActivateLease();
						}
						else
						{
							lease.RenewInternal(orCreateIdentity.Lease.InitialLeaseTime);
						}
					}
				}
				if (updateChannelData && objRef.ChannelInfo != null)
				{
					object[] currentChannelData = ChannelServices.CurrentChannelData;
					if (!(Obj is AppDomain))
					{
						objRef.ChannelInfo.ChannelData = currentChannelData;
					}
					else
					{
						int num = currentChannelData.Length;
						object[] array = new object[num];
						Array.Copy(currentChannelData, array, num);
						for (int i = 0; i < num; i++)
						{
							if (!(array[i] is CrossAppDomainData))
							{
								array[i] = null;
							}
						}
						objRef.ChannelInfo.ChannelData = array;
					}
				}
			}
			TrackingServices.MarshaledObject(Obj, objRef);
			return objRef;
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x001332B0 File Offset: 0x001314B0
		[SecurityCritical]
		private static Identity GetOrCreateIdentity(MarshalByRefObject Obj, string ObjURI, bool isInitializing)
		{
			int num = 2;
			if (isInitializing)
			{
				num |= 4;
			}
			Identity identity;
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
				identity = realProxy.IdentityObject;
				if (identity == null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, num);
					identity.RaceSetTransparentProxy(Obj);
				}
				ServerIdentity serverIdentity = identity as ServerIdentity;
				if (serverIdentity != null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(serverIdentity.TPOrObject, ObjURI, num);
					if (ObjURI != null && ObjURI != Identity.RemoveAppNameOrAppGuidIfNecessary(identity.ObjURI))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_URIExists"));
					}
				}
				else if (ObjURI != null && ObjURI != identity.ObjURI)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_URIToProxy"));
				}
			}
			else
			{
				identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, num);
			}
			return identity;
		}

		/// <summary>Serializes the specified marshal by reference object into the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="obj">The object to serialize.</param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> into which the object is serialized.</param>
		/// <param name="context">The source and destination of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> or <paramref name="info" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005654 RID: 22100 RVA: 0x00133360 File Offset: 0x00131560
		[SecurityCritical]
		public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject)obj, null, null);
			objRef.GetObjectData(info, context);
		}

		/// <summary>Takes a <see cref="T:System.Runtime.Remoting.ObjRef" /> and creates a proxy object out of it.</summary>
		/// <param name="objectRef">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object for which the proxy is being created.</param>
		/// <returns>A proxy to the object that the given <see cref="T:System.Runtime.Remoting.ObjRef" /> represents.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Runtime.Remoting.ObjRef" /> instance specified in the <paramref name="objectRef" /> parameter is not well-formed.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06005655 RID: 22101 RVA: 0x0013339F File Offset: 0x0013159F
		[SecurityCritical]
		public static object Unmarshal(ObjRef objectRef)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, false);
		}

		/// <summary>Takes a <see cref="T:System.Runtime.Remoting.ObjRef" /> and creates a proxy object out of it, refining it to the type on the server.</summary>
		/// <param name="objectRef">The <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object for which the proxy is being created.</param>
		/// <param name="fRefine">
		///   <see langword="true" /> to refine the proxy to the type on the server; otherwise, <see langword="false" />.</param>
		/// <returns>A proxy to the object that the given <see cref="T:System.Runtime.Remoting.ObjRef" /> represents.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Runtime.Remoting.ObjRef" /> instance specified in the <paramref name="objectRef" /> parameter is not well-formed.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06005656 RID: 22102 RVA: 0x001333A9 File Offset: 0x001315A9
		[SecurityCritical]
		public static object Unmarshal(ObjRef objectRef, bool fRefine)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, fRefine);
		}

		/// <summary>Creates a proxy for a well-known object, given the <see cref="T:System.Type" /> and URL.</summary>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of a well-known object on the server end to which you want to connect.</param>
		/// <param name="url">The URL of the server class.</param>
		/// <returns>A proxy to the remote object that points to an endpoint served by the specified well-known object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06005657 RID: 22103 RVA: 0x001333B3 File Offset: 0x001315B3
		[SecurityCritical]
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		/// <summary>Creates a proxy for a well-known object, given the <see cref="T:System.Type" />, URL, and channel-specific data.</summary>
		/// <param name="classToProxy">The <see cref="T:System.Type" /> of the well-known object to which you want to connect.</param>
		/// <param name="url">The URL of the well-known object.</param>
		/// <param name="data">Channel specific data. Can be <see langword="null" />.</param>
		/// <returns>A proxy that points to an endpoint that is served by the requested well-known object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06005658 RID: 22104 RVA: 0x001333BD File Offset: 0x001315BD
		[SecurityCritical]
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url, object data)
		{
			return RemotingServices.Unmarshal(classToProxy, url, data);
		}

		/// <summary>Stops an object from receiving any further messages through the registered remoting channels.</summary>
		/// <param name="obj">Object to disconnect from its channel.</param>
		/// <returns>
		///   <see langword="true" /> if the object was disconnected from the registered remoting channels successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a proxy.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06005659 RID: 22105 RVA: 0x001333C7 File Offset: 0x001315C7
		[SecurityCritical]
		public static bool Disconnect(MarshalByRefObject obj)
		{
			return RemotingServices.Disconnect(obj, true);
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x001333D0 File Offset: 0x001315D0
		[SecurityCritical]
		internal static bool Disconnect(MarshalByRefObject obj, bool bResetURI)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			bool flag2 = false;
			if (identity != null)
			{
				if (!(identity is ServerIdentity))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_CantDisconnectClientProxy"));
				}
				if (identity.IsInIDTable())
				{
					IdentityHolder.RemoveIdentity(identity.URI, bResetURI);
					flag2 = true;
				}
				TrackingServices.DisconnectedObject(obj);
			}
			return flag2;
		}

		/// <summary>Returns a chain of envoy sinks that should be used when sending messages to the remote object represented by the specified proxy.</summary>
		/// <param name="obj">The proxy of the remote object that requested envoy sinks are associated with.</param>
		/// <returns>A chain of envoy sinks associated with the specified proxy.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600565B RID: 22107 RVA: 0x00133430 File Offset: 0x00131630
		[SecurityCritical]
		public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
		{
			IMessageSink messageSink = null;
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					messageSink = identityObject.EnvoyChain;
				}
			}
			return messageSink;
		}

		/// <summary>Returns the <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object from the specified proxy.</summary>
		/// <param name="obj">A proxy connected to the object you want to create a <see cref="T:System.Runtime.Remoting.ObjRef" /> for.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.ObjRef" /> that represents the remote object the specified proxy is connected to, or <see langword="null" /> if the object or proxy have not been marshaled.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600565C RID: 22108 RVA: 0x00133460 File Offset: 0x00131660
		[SecurityCritical]
		public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
		{
			ObjRef objRef = null;
			if (!RemotingServices.IsTransparentProxy(obj))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadType"));
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(obj);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null)
			{
				objRef = identityObject.ObjectRef;
			}
			return objRef;
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x001334A0 File Offset: 0x001316A0
		[SecurityCritical]
		internal static object Unmarshal(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x001334AC File Offset: 0x001316AC
		[SecurityCritical]
		internal static object Unmarshal(Type classToProxy, string url, object data)
		{
			if (null == classToProxy)
			{
				throw new ArgumentNullException("classToProxy");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_NotRemotableByReference"));
			}
			Identity identity = IdentityHolder.ResolveIdentity(url);
			if (identity == null || identity.ChannelSink == null || identity.EnvoyChain == null)
			{
				IMessageSink messageSink = null;
				IMessageSink messageSink2 = null;
				string text = RemotingServices.CreateEnvoyAndChannelSinks(url, data, out messageSink, out messageSink2);
				if (messageSink == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Connect_CantCreateChannelSink"), url));
				}
				if (text == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
				}
				identity = IdentityHolder.FindOrCreateIdentity(text, url, null);
				RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, messageSink2);
			}
			return RemotingServices.GetOrCreateProxy(classToProxy, identity);
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x00133576 File Offset: 0x00131776
		[SecurityCritical]
		internal static object Wrap(ContextBoundObject obj)
		{
			return RemotingServices.Wrap(obj, null, true);
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x00133580 File Offset: 0x00131780
		[SecurityCritical]
		internal static object Wrap(ContextBoundObject obj, object proxy, bool fCreateSinks)
		{
			if (obj != null && !RemotingServices.IsTransparentProxy(obj))
			{
				Identity identity;
				if (proxy != null)
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
					if (realProxy.UnwrappedServerObject == null)
					{
						realProxy.AttachServerHelper(obj);
					}
					identity = MarshalByRefObject.GetIdentity(obj);
				}
				else
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(obj, null, 0);
				}
				proxy = RemotingServices.GetOrCreateProxy(identity, proxy, true);
				RemotingServices.GetRealProxy(proxy).Wrap();
				if (fCreateSinks)
				{
					IMessageSink messageSink = null;
					IMessageSink messageSink2 = null;
					RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject)proxy, null, out messageSink, out messageSink2);
					RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, messageSink2);
				}
				RealProxy realProxy2 = RemotingServices.GetRealProxy(proxy);
				if (realProxy2.UnwrappedServerObject == null)
				{
					realProxy2.AttachServerHelper(obj);
				}
				return proxy;
			}
			return obj;
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x00133618 File Offset: 0x00131818
		internal static string GetObjectUriFromFullUri(string fullUri)
		{
			if (fullUri == null)
			{
				return null;
			}
			int num = fullUri.LastIndexOf('/');
			if (num == -1)
			{
				return fullUri;
			}
			return fullUri.Substring(num + 1);
		}

		// Token: 0x06005662 RID: 22114
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Unwrap(ContextBoundObject obj);

		// Token: 0x06005663 RID: 22115
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AlwaysUnwrap(ContextBoundObject obj);

		// Token: 0x06005664 RID: 22116 RVA: 0x00133644 File Offset: 0x00131844
		[SecurityCritical]
		internal static object InternalUnmarshal(ObjRef objectRef, object proxy, bool fRefine)
		{
			Context context = Thread.CurrentContext;
			if (!ObjRef.IsWellFormed(objectRef))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadObjRef"), "Unmarshal"));
			}
			object obj;
			Identity identity;
			if (objectRef.IsWellKnown())
			{
				obj = RemotingServices.Unmarshal(typeof(MarshalByRefObject), objectRef.URI);
				identity = IdentityHolder.ResolveIdentity(objectRef.URI);
				if (identity.ObjectRef == null)
				{
					identity.RaceSetObjRef(objectRef);
				}
				return obj;
			}
			identity = IdentityHolder.FindOrCreateIdentity(objectRef.URI, null, objectRef);
			context = Thread.CurrentContext;
			ServerIdentity serverIdentity = identity as ServerIdentity;
			if (serverIdentity != null)
			{
				context = Thread.CurrentContext;
				if (!serverIdentity.IsContextBound)
				{
					if (proxy != null)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ProxySameAppDomain"), Array.Empty<object>()));
					}
					obj = serverIdentity.TPOrObject;
				}
				else
				{
					IMessageSink messageSink = null;
					IMessageSink messageSink2 = null;
					RemotingServices.CreateEnvoyAndChannelSinks(serverIdentity.TPOrObject, null, out messageSink, out messageSink2);
					RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, messageSink2);
					obj = RemotingServices.GetOrCreateProxy(identity, proxy, true);
				}
			}
			else
			{
				IMessageSink messageSink3 = null;
				IMessageSink messageSink4 = null;
				if (!objectRef.IsObjRefLite())
				{
					RemotingServices.CreateEnvoyAndChannelSinks(null, objectRef, out messageSink3, out messageSink4);
				}
				else
				{
					RemotingServices.CreateEnvoyAndChannelSinks(objectRef.URI, null, out messageSink3, out messageSink4);
				}
				RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink3, messageSink4);
				if (objectRef.HasProxyAttribute())
				{
					fRefine = true;
				}
				obj = RemotingServices.GetOrCreateProxy(identity, proxy, fRefine);
			}
			TrackingServices.UnmarshaledObject(obj, objectRef);
			return obj;
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x00133794 File Offset: 0x00131994
		[SecurityCritical]
		private static object GetOrCreateProxy(Identity idObj, object proxy, bool fRefine)
		{
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				Type type;
				if (serverIdentity != null)
				{
					type = serverIdentity.ServerType;
				}
				else
				{
					IRemotingTypeInfo typeInfo = idObj.ObjectRef.TypeInfo;
					type = null;
					if ((typeInfo is TypeInfo && !fRefine) || typeInfo == null)
					{
						type = typeof(MarshalByRefObject);
					}
					else
					{
						string typeName = typeInfo.TypeName;
						if (typeName != null)
						{
							string text = null;
							string text2 = null;
							TypeInfo.ParseTypeAndAssembly(typeName, out text, out text2);
							Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(text2);
							if (assembly != null)
							{
								type = assembly.GetType(text, false, false);
							}
						}
					}
					if (null == type)
					{
						throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), typeInfo.TypeName));
					}
				}
				proxy = RemotingServices.SetOrCreateProxy(idObj, type, null);
			}
			else
			{
				proxy = RemotingServices.SetOrCreateProxy(idObj, null, proxy);
			}
			if (proxy == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_UnexpectedNullTP"));
			}
			return proxy;
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00133874 File Offset: 0x00131A74
		[SecurityCritical]
		private static object GetOrCreateProxy(Type classToProxy, Identity idObj)
		{
			object obj = idObj.TPOrObject;
			if (obj == null)
			{
				obj = RemotingServices.SetOrCreateProxy(idObj, classToProxy, null);
			}
			ServerIdentity serverIdentity = idObj as ServerIdentity;
			if (serverIdentity != null)
			{
				Type serverType = serverIdentity.ServerType;
				if (!classToProxy.IsAssignableFrom(serverType))
				{
					throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), serverType.FullName, classToProxy.FullName));
				}
			}
			return obj;
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x001338D8 File Offset: 0x00131AD8
		[SecurityCritical]
		private static MarshalByRefObject SetOrCreateProxy(Identity idObj, Type classToProxy, object proxy)
		{
			RealProxy realProxy = null;
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				if (idObj.ObjectRef != null)
				{
					ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(classToProxy);
					realProxy = proxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, null);
				}
				if (realProxy == null)
				{
					ProxyAttribute defaultProxyAttribute = ActivationServices.DefaultProxyAttribute;
					realProxy = defaultProxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, (serverIdentity == null) ? null : serverIdentity.ServerContext);
				}
			}
			else
			{
				realProxy = RemotingServices.GetRealProxy(proxy);
			}
			realProxy.IdentityObject = idObj;
			proxy = realProxy.GetTransparentProxy();
			proxy = idObj.RaceSetTransparentProxy(proxy);
			return (MarshalByRefObject)proxy;
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x0013395C File Offset: 0x00131B5C
		private static bool AreChannelDataElementsNull(object[] channelData)
		{
			foreach (object obj in channelData)
			{
				if (obj != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x00133984 File Offset: 0x00131B84
		[SecurityCritical]
		internal static void CreateEnvoyAndChannelSinks(MarshalByRefObject tpOrObject, ObjRef objectRef, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			chnlSink = null;
			envoySink = null;
			if (objectRef == null)
			{
				chnlSink = ChannelServices.GetCrossContextChannelSink();
				envoySink = Thread.CurrentContext.CreateEnvoyChain(tpOrObject);
				return;
			}
			object[] channelData = objectRef.ChannelInfo.ChannelData;
			if (channelData != null && !RemotingServices.AreChannelDataElementsNull(channelData))
			{
				for (int i = 0; i < channelData.Length; i++)
				{
					chnlSink = ChannelServices.CreateMessageSink(channelData[i]);
					if (chnlSink != null)
					{
						break;
					}
				}
				if (chnlSink == null)
				{
					object obj = RemotingServices.s_delayLoadChannelLock;
					lock (obj)
					{
						for (int j = 0; j < channelData.Length; j++)
						{
							chnlSink = ChannelServices.CreateMessageSink(channelData[j]);
							if (chnlSink != null)
							{
								break;
							}
						}
						if (chnlSink == null)
						{
							foreach (object obj2 in channelData)
							{
								string text;
								chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(null, obj2, out text);
								if (chnlSink != null)
								{
									break;
								}
							}
						}
					}
				}
			}
			if (objectRef.EnvoyInfo != null && objectRef.EnvoyInfo.EnvoySinks != null)
			{
				envoySink = objectRef.EnvoyInfo.EnvoySinks;
				return;
			}
			envoySink = EnvoyTerminatorSink.MessageSink;
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x00133A98 File Offset: 0x00131C98
		[SecurityCritical]
		internal static string CreateEnvoyAndChannelSinks(string url, object data, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			string text = RemotingServices.CreateChannelSink(url, data, out chnlSink);
			envoySink = EnvoyTerminatorSink.MessageSink;
			return text;
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x00133AB8 File Offset: 0x00131CB8
		[SecurityCritical]
		private static string CreateChannelSink(string url, object data, out IMessageSink chnlSink)
		{
			string text = null;
			chnlSink = ChannelServices.CreateMessageSink(url, data, out text);
			if (chnlSink == null)
			{
				object obj = RemotingServices.s_delayLoadChannelLock;
				lock (obj)
				{
					chnlSink = ChannelServices.CreateMessageSink(url, data, out text);
					if (chnlSink == null)
					{
						chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(url, data, out text);
					}
				}
			}
			return text;
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x00133B20 File Offset: 0x00131D20
		internal static void SetEnvoyAndChannelSinks(Identity idObj, IMessageSink chnlSink, IMessageSink envoySink)
		{
			if (idObj.ChannelSink == null && chnlSink != null)
			{
				idObj.RaceSetChannelSink(chnlSink);
			}
			if (idObj.EnvoyChain != null)
			{
				return;
			}
			if (envoySink != null)
			{
				idObj.RaceSetEnvoyChain(envoySink);
				return;
			}
			throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_FailEnvoySink"), Array.Empty<object>()));
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x00133B74 File Offset: 0x00131D74
		[SecurityCritical]
		private static bool CheckCast(RealProxy rp, RuntimeType castType)
		{
			bool flag = false;
			if (castType == typeof(object))
			{
				return true;
			}
			if (!castType.IsInterface && !castType.IsMarshalByRef)
			{
				return false;
			}
			if (castType != typeof(IObjectReference))
			{
				IRemotingTypeInfo remotingTypeInfo = rp as IRemotingTypeInfo;
				if (remotingTypeInfo != null)
				{
					flag = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
				}
				else
				{
					Identity identityObject = rp.IdentityObject;
					if (identityObject != null)
					{
						ObjRef objectRef = identityObject.ObjectRef;
						if (objectRef != null)
						{
							remotingTypeInfo = objectRef.TypeInfo;
							if (remotingTypeInfo != null)
							{
								flag = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x00133C02 File Offset: 0x00131E02
		[SecurityCritical]
		internal static bool ProxyCheckCast(RealProxy rp, RuntimeType castType)
		{
			return RemotingServices.CheckCast(rp, castType);
		}

		// Token: 0x0600566F RID: 22127
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CheckCast(object objToExpand, RuntimeType type);

		// Token: 0x06005670 RID: 22128 RVA: 0x00133C0C File Offset: 0x00131E0C
		[SecurityCritical]
		internal static GCHandle CreateDelegateInvocation(WaitCallback waitDelegate, object state)
		{
			return GCHandle.Alloc(new object[] { waitDelegate, state });
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x00133C2E File Offset: 0x00131E2E
		[SecurityCritical]
		internal static void DisposeDelegateInvocation(GCHandle delegateCallToken)
		{
			delegateCallToken.Free();
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x00133C38 File Offset: 0x00131E38
		[SecurityCritical]
		internal static object CreateProxyForDomain(int appDomainId, IntPtr defCtxID)
		{
			ObjRef objRef = RemotingServices.CreateDataForDomain(appDomainId, defCtxID);
			return (AppDomain)RemotingServices.Unmarshal(objRef);
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x00133C5C File Offset: 0x00131E5C
		[SecurityCritical]
		internal static object CreateDataForDomainCallback(object[] args)
		{
			RemotingServices.RegisterWellKnownChannels();
			ObjRef objRef = RemotingServices.MarshalInternal(Thread.CurrentContext.AppDomain, null, null, false);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(Thread.CurrentContext.AppDomain);
			serverIdentity.SetHandle();
			objRef.SetServerIdentity(serverIdentity.GetHandle());
			objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
			return objRef;
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x00133CBC File Offset: 0x00131EBC
		[SecurityCritical]
		internal static ObjRef CreateDataForDomain(int appDomainId, IntPtr defCtxID)
		{
			RemotingServices.RegisterWellKnownChannels();
			InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(RemotingServices.CreateDataForDomainCallback);
			return (ObjRef)Thread.CurrentThread.InternalCrossContextCallback(null, defCtxID, appDomainId, internalCrossContextDelegate, null);
		}

		/// <summary>Returns the method base from the given <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" />.</summary>
		/// <param name="msg">The method message to extract the method base from.</param>
		/// <returns>The method base extracted from the <paramref name="msg" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">Either the immediate caller does not have infrastructure permission, or at least one of the callers higher in the callstack does not have permission to retrieve the type information of non-public members.</exception>
		// Token: 0x06005675 RID: 22133 RVA: 0x00133CF0 File Offset: 0x00131EF0
		[SecurityCritical]
		public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			return RemotingServices.InternalGetMethodBaseFromMethodMessage(msg);
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x00133D08 File Offset: 0x00131F08
		[SecurityCritical]
		internal static MethodBase InternalGetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			if (msg == null)
			{
				return null;
			}
			Type type = RemotingServices.InternalGetTypeFromQualifiedTypeName(msg.TypeName);
			if (type == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), msg.TypeName));
			}
			Type[] array = (Type[])msg.MethodSignature;
			return RemotingServices.GetMethodBase(msg, type, array);
		}

		/// <summary>Returns a Boolean value that indicates whether the method in the given message is overloaded.</summary>
		/// <param name="msg">The message that contains a call to the method in question.</param>
		/// <returns>
		///   <see langword="true" /> if the method called in <paramref name="msg" /> is overloaded; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06005677 RID: 22135 RVA: 0x00133D64 File Offset: 0x00131F64
		[SecurityCritical]
		public static bool IsMethodOverloaded(IMethodMessage msg)
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(msg.MethodBase);
			return reflectionCachedData.IsOverloaded();
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x00133D84 File Offset: 0x00131F84
		[SecurityCritical]
		private static MethodBase GetMethodBase(IMethodMessage msg, Type t, Type[] signature)
		{
			MethodBase methodBase = null;
			if (msg is IConstructionCallMessage || msg is IConstructionReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType = t as RuntimeType;
					ConstructorInfo[] array;
					if (runtimeType == null)
					{
						array = t.GetConstructors();
					}
					else
					{
						array = runtimeType.GetConstructors();
					}
					if (1 != array.Length)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Remoting_AmbiguousCTOR"));
					}
					methodBase = array[0];
				}
				else
				{
					RuntimeType runtimeType2 = t as RuntimeType;
					if (runtimeType2 == null)
					{
						methodBase = t.GetConstructor(signature);
					}
					else
					{
						methodBase = runtimeType2.GetConstructor(signature);
					}
				}
			}
			else if (msg is IMethodCallMessage || msg is IMethodReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType3 = t as RuntimeType;
					if (runtimeType3 == null)
					{
						methodBase = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
					else
					{
						methodBase = runtimeType3.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
				}
				else
				{
					RuntimeType runtimeType4 = t as RuntimeType;
					if (runtimeType4 == null)
					{
						methodBase = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
					}
					else
					{
						methodBase = runtimeType4.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, signature, null);
					}
				}
			}
			return methodBase;
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x00133E94 File Offset: 0x00132094
		[SecurityCritical]
		internal static bool IsMethodAllowedRemotely(MethodBase method)
		{
			if (RemotingServices.s_FieldGetterMB == null || RemotingServices.s_FieldSetterMB == null || RemotingServices.s_IsInstanceOfTypeMB == null || RemotingServices.s_InvokeMemberMB == null || RemotingServices.s_CanCastToXmlTypeMB == null)
			{
				CodeAccessPermission.Assert(true);
				if (RemotingServices.s_FieldGetterMB == null)
				{
					RemotingServices.s_FieldGetterMB = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_FieldSetterMB == null)
				{
					RemotingServices.s_FieldSetterMB = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_IsInstanceOfTypeMB == null)
				{
					RemotingServices.s_IsInstanceOfTypeMB = typeof(MarshalByRefObject).GetMethod("IsInstanceOfType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_CanCastToXmlTypeMB == null)
				{
					RemotingServices.s_CanCastToXmlTypeMB = typeof(MarshalByRefObject).GetMethod("CanCastToXmlType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_InvokeMemberMB == null)
				{
					RemotingServices.s_InvokeMemberMB = typeof(MarshalByRefObject).GetMethod("InvokeMember", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
			}
			return method == RemotingServices.s_FieldGetterMB || method == RemotingServices.s_FieldSetterMB || method == RemotingServices.s_IsInstanceOfTypeMB || method == RemotingServices.s_InvokeMemberMB || method == RemotingServices.s_CanCastToXmlTypeMB;
		}

		/// <summary>Returns a Boolean value that indicates whether the client that called the method specified in the given message is waiting for the server to finish processing the method before continuing execution.</summary>
		/// <param name="method">The method in question.</param>
		/// <returns>
		///   <see langword="true" /> if the method is one way; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600567A RID: 22138 RVA: 0x0013401C File Offset: 0x0013221C
		[SecurityCritical]
		public static bool IsOneWay(MethodBase method)
		{
			if (method == null)
			{
				return false;
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(method);
			return reflectionCachedData.IsOneWayMethod();
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x00134044 File Offset: 0x00132244
		internal static bool FindAsyncMethodVersion(MethodInfo method, out MethodInfo beginMethod, out MethodInfo endMethod)
		{
			beginMethod = null;
			endMethod = null;
			string text = "Begin" + method.Name;
			string text2 = "End" + method.Name;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			Type typeFromHandle = typeof(IAsyncResult);
			Type returnType = method.ReturnType;
			ParameterInfo[] parameters = method.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.IsOut)
				{
					arrayList2.Add(parameterInfo);
				}
				else if (parameterInfo.ParameterType.IsByRef)
				{
					arrayList.Add(parameterInfo);
					arrayList2.Add(parameterInfo);
				}
				else
				{
					arrayList.Add(parameterInfo);
				}
			}
			arrayList.Add(typeof(AsyncCallback));
			arrayList.Add(typeof(object));
			arrayList2.Add(typeof(IAsyncResult));
			Type declaringType = method.DeclaringType;
			MethodInfo[] methods = declaringType.GetMethods();
			foreach (MethodInfo methodInfo in methods)
			{
				ParameterInfo[] parameters2 = methodInfo.GetParameters();
				if (methodInfo.Name.Equals(text) && methodInfo.ReturnType == typeFromHandle && RemotingServices.CompareParameterList(arrayList, parameters2))
				{
					beginMethod = methodInfo;
				}
				else if (methodInfo.Name.Equals(text2) && methodInfo.ReturnType == returnType && RemotingServices.CompareParameterList(arrayList2, parameters2))
				{
					endMethod = methodInfo;
				}
			}
			return beginMethod != null && endMethod != null;
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x001341DC File Offset: 0x001323DC
		private static bool CompareParameterList(ArrayList params1, ParameterInfo[] params2)
		{
			if (params1.Count != params2.Length)
			{
				return false;
			}
			int num = 0;
			foreach (object obj in params1)
			{
				ParameterInfo parameterInfo = params2[num];
				ParameterInfo parameterInfo2 = obj as ParameterInfo;
				if (parameterInfo2 != null)
				{
					if (parameterInfo2.ParameterType != parameterInfo.ParameterType || parameterInfo2.IsIn != parameterInfo.IsIn || parameterInfo2.IsOut != parameterInfo.IsOut)
					{
						return false;
					}
				}
				else if ((Type)obj != parameterInfo.ParameterType && parameterInfo.IsIn)
				{
					return false;
				}
				num++;
			}
			return true;
		}

		/// <summary>Returns the <see cref="T:System.Type" /> of the object with the specified URI.</summary>
		/// <param name="URI">The URI of the object whose <see cref="T:System.Type" /> is requested.</param>
		/// <returns>The <see cref="T:System.Type" /> of the object with the specified URI.</returns>
		/// <exception cref="T:System.Security.SecurityException">Either the immediate caller does not have infrastructure permission, or at least one of the callers higher in the callstack does not have permission to retrieve the type information of non-public members.</exception>
		// Token: 0x0600567D RID: 22141 RVA: 0x001342A8 File Offset: 0x001324A8
		[SecurityCritical]
		public static Type GetServerTypeForUri(string URI)
		{
			Type type = null;
			if (URI != null)
			{
				ServerIdentity serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
				if (serverIdentity == null)
				{
					type = RemotingConfigHandler.GetServerTypeForUri(URI);
				}
				else
				{
					type = serverIdentity.ServerType;
				}
			}
			return type;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x001342DA File Offset: 0x001324DA
		[SecurityCritical]
		internal static void DomainUnloaded(int domainID)
		{
			IdentityHolder.FlushIdentityTable();
			CrossAppDomainSink.DomainUnloaded(domainID);
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x001342E8 File Offset: 0x001324E8
		[SecurityCritical]
		internal static IntPtr GetServerContextForProxy(object tp)
		{
			ObjRef objRef = null;
			bool flag;
			int num;
			return RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out num);
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x00134304 File Offset: 0x00132504
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static int GetServerDomainIdForProxy(object tp)
		{
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			return identityObject.ObjectRef.GetServerDomainId();
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x0013432C File Offset: 0x0013252C
		[SecurityCritical]
		internal static void GetServerContextAndDomainIdForProxy(object tp, out IntPtr contextId, out int domainId)
		{
			ObjRef objRef;
			bool flag;
			contextId = RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out domainId);
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x00134348 File Offset: 0x00132548
		[SecurityCritical]
		private static IntPtr GetServerContextForProxy(object tp, out ObjRef objRef, out bool bSameDomain, out int domainId)
		{
			IntPtr intPtr = IntPtr.Zero;
			objRef = null;
			bSameDomain = false;
			domainId = 0;
			if (RemotingServices.IsTransparentProxy(tp))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(tp);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					ServerIdentity serverIdentity = identityObject as ServerIdentity;
					if (serverIdentity != null)
					{
						bSameDomain = true;
						intPtr = serverIdentity.ServerContext.InternalContextID;
						domainId = Thread.GetDomain().GetId();
					}
					else
					{
						objRef = identityObject.ObjectRef;
						if (objRef != null)
						{
							intPtr = objRef.GetServerContext(out domainId);
						}
						else
						{
							intPtr = IntPtr.Zero;
						}
					}
				}
				else
				{
					intPtr = Context.DefaultContext.InternalContextID;
				}
			}
			return intPtr;
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x001343D0 File Offset: 0x001325D0
		[SecurityCritical]
		internal static Context GetServerContext(MarshalByRefObject obj)
		{
			Context context = null;
			if (!RemotingServices.IsTransparentProxy(obj) && obj is ContextBoundObject)
			{
				context = Thread.CurrentContext;
			}
			else
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				ServerIdentity serverIdentity = identityObject as ServerIdentity;
				if (serverIdentity != null)
				{
					context = serverIdentity.ServerContext;
				}
			}
			return context;
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x00134418 File Offset: 0x00132618
		[SecurityCritical]
		private static object GetType(object tp)
		{
			Type type = null;
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null && identityObject.ObjectRef != null && identityObject.ObjectRef.TypeInfo != null)
			{
				IRemotingTypeInfo typeInfo = identityObject.ObjectRef.TypeInfo;
				string typeName = typeInfo.TypeName;
				if (typeName != null)
				{
					type = RemotingServices.InternalGetTypeFromQualifiedTypeName(typeName);
				}
			}
			return type;
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x00134470 File Offset: 0x00132670
		[SecurityCritical]
		internal static byte[] MarshalToBuffer(object o, bool crossRuntime)
		{
			if (crossRuntime)
			{
				if (RemotingServices.IsTransparentProxy(o))
				{
					if (RemotingServices.GetRealProxy(o) is RemotingProxy && ChannelServices.RegisteredChannels.Length == 0)
					{
						return null;
					}
				}
				else
				{
					MarshalByRefObject marshalByRefObject = o as MarshalByRefObject;
					if (marshalByRefObject != null)
					{
						ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(marshalByRefObject.GetType());
						if (proxyAttribute == ActivationServices.DefaultProxyAttribute && ChannelServices.RegisteredChannels.Length == 0)
						{
							return null;
						}
					}
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			RemotingSurrogateSelector remotingSurrogateSelector = new RemotingSurrogateSelector();
			new BinaryFormatter
			{
				SurrogateSelector = remotingSurrogateSelector,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Serialize(memoryStream, o, null, false);
			return memoryStream.GetBuffer();
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x00134500 File Offset: 0x00132700
		[SecurityCritical]
		internal static object UnmarshalFromBuffer(byte[] b, bool crossRuntime)
		{
			MemoryStream memoryStream = new MemoryStream(b);
			object obj = new BinaryFormatter
			{
				AssemblyFormat = FormatterAssemblyStyle.Simple,
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Deserialize(memoryStream, null, false);
			if (crossRuntime && RemotingServices.IsTransparentProxy(obj))
			{
				if (!(RemotingServices.GetRealProxy(obj) is RemotingProxy))
				{
					return obj;
				}
				if (ChannelServices.RegisteredChannels.Length == 0)
				{
					return null;
				}
				obj.GetHashCode();
			}
			return obj;
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x0013456C File Offset: 0x0013276C
		internal static object UnmarshalReturnMessageFromBuffer(byte[] b, IMethodCallMessage msg)
		{
			MemoryStream memoryStream = new MemoryStream(b);
			return new BinaryFormatter
			{
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.DeserializeMethodResponse(memoryStream, null, msg);
		}

		/// <summary>Connects to the specified remote object, and executes the provided <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> on it.</summary>
		/// <param name="target">The remote object whose method you want to call.</param>
		/// <param name="reqMsg">A method call message to the specified remote object's method.</param>
		/// <returns>The response of the remote method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The method was called from a context other than the native context of the object.</exception>
		// Token: 0x06005688 RID: 22152 RVA: 0x001345A8 File Offset: 0x001327A8
		[SecurityCritical]
		public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(target);
			if (realProxy is RemotingProxy && !realProxy.DoContextsMatch())
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_WrongContext"));
			}
			StackBuilderSink stackBuilderSink = new StackBuilderSink(target);
			return (IMethodReturnMessage)stackBuilderSink.SyncProcessMessage(reqMsg);
		}

		// Token: 0x06005689 RID: 22153 RVA: 0x00134600 File Offset: 0x00132800
		[SecurityCritical]
		internal static string DetermineDefaultQualifiedTypeName(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = null;
			string text2 = null;
			if (SoapServices.GetXmlTypeForInteropType(type, out text, out text2))
			{
				return "soap:" + text + ", " + text2;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x0013464C File Offset: 0x0013284C
		[SecurityCritical]
		internal static string GetDefaultQualifiedTypeName(RuntimeType type)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(type);
			return reflectionCachedData.QualifiedTypeName;
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x00134668 File Offset: 0x00132868
		internal static string InternalGetClrTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 4 && string.CompareOrdinal(qualifiedTypeName, 0, "clr:", 0, 4) == 0)
			{
				return qualifiedTypeName.Substring(4);
			}
			return null;
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x00134699 File Offset: 0x00132899
		private static int IsSoapType(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 5 && string.CompareOrdinal(qualifiedTypeName, 0, "soap:", 0, 5) == 0)
			{
				return qualifiedTypeName.IndexOf(',', 5);
			}
			return -1;
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x001346C0 File Offset: 0x001328C0
		[SecurityCritical]
		internal static string InternalGetSoapTypeNameFromQualifiedTypeName(string xmlTypeName, string xmlTypeNamespace)
		{
			string text;
			string text2;
			if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out text, out text2))
			{
				return null;
			}
			string text3;
			if (text != null && text.Length > 0)
			{
				text3 = text + "." + xmlTypeName;
			}
			else
			{
				text3 = xmlTypeName;
			}
			try
			{
				return text3 + ", " + text2;
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x00134724 File Offset: 0x00132924
		[SecurityCritical]
		internal static string InternalGetTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return text;
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string text2 = qualifiedTypeName.Substring(5, num - 5);
				string text3 = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(text2, text3);
				if (text != null)
				{
					return text;
				}
			}
			return qualifiedTypeName;
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x00134784 File Offset: 0x00132984
		[SecurityCritical]
		internal static RuntimeType InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName, bool partialFallback)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return RemotingServices.LoadClrTypeWithPartialBindFallback(text, partialFallback);
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string text2 = qualifiedTypeName.Substring(5, num - 5);
				string text3 = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				RuntimeType runtimeType = (RuntimeType)SoapServices.GetInteropTypeFromXmlType(text2, text3);
				if (runtimeType != null)
				{
					return runtimeType;
				}
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(text2, text3);
				if (text != null)
				{
					return RemotingServices.LoadClrTypeWithPartialBindFallback(text, true);
				}
			}
			return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName, partialFallback);
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x00134810 File Offset: 0x00132A10
		[SecurityCritical]
		internal static Type InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName)
		{
			return RemotingServices.InternalGetTypeFromQualifiedTypeName(qualifiedTypeName, true);
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0013481C File Offset: 0x00132A1C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static RuntimeType LoadClrTypeWithPartialBindFallback(string typeName, bool partialFallback)
		{
			if (!partialFallback)
			{
				return (RuntimeType)Type.GetType(typeName, false);
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackCrawlMark, true);
		}

		// Token: 0x06005692 RID: 22162
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemoting();

		// Token: 0x06005693 RID: 22163
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingCookie();

		// Token: 0x06005694 RID: 22164
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingAsync();

		// Token: 0x06005695 RID: 22165
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientSendingMessage(out Guid id, bool fIsAsync);

		// Token: 0x06005696 RID: 22166
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientReceivingReply(Guid id, bool fIsAsync);

		// Token: 0x06005697 RID: 22167
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerReceivingMessage(Guid id, bool fIsAsync);

		// Token: 0x06005698 RID: 22168
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerSendingReply(out Guid id, bool fIsAsync);

		/// <summary>Logs the stage in a remoting exchange to an external debugger.</summary>
		/// <param name="stage">An internally defined constant that identifies the stage in a remoting exchange.</param>
		// Token: 0x06005699 RID: 22169 RVA: 0x00134847 File Offset: 0x00132A47
		[SecurityCritical]
		[Conditional("REMOTING_PERF")]
		[Obsolete("Use of this method is not recommended. The LogRemotingStage existed for internal diagnostic purposes only.")]
		public static void LogRemotingStage(int stage)
		{
		}

		// Token: 0x0600569A RID: 22170
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetInterfaceCache(object proxy);

		// Token: 0x04002799 RID: 10137
		private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400279A RID: 10138
		private const string FieldGetterName = "FieldGetter";

		// Token: 0x0400279B RID: 10139
		private const string FieldSetterName = "FieldSetter";

		// Token: 0x0400279C RID: 10140
		private const string IsInstanceOfTypeName = "IsInstanceOfType";

		// Token: 0x0400279D RID: 10141
		private const string CanCastToXmlTypeName = "CanCastToXmlType";

		// Token: 0x0400279E RID: 10142
		private const string InvokeMemberName = "InvokeMember";

		// Token: 0x0400279F RID: 10143
		private static volatile MethodBase s_FieldGetterMB;

		// Token: 0x040027A0 RID: 10144
		private static volatile MethodBase s_FieldSetterMB;

		// Token: 0x040027A1 RID: 10145
		private static volatile MethodBase s_IsInstanceOfTypeMB;

		// Token: 0x040027A2 RID: 10146
		private static volatile MethodBase s_CanCastToXmlTypeMB;

		// Token: 0x040027A3 RID: 10147
		private static volatile MethodBase s_InvokeMemberMB;

		// Token: 0x040027A4 RID: 10148
		private static volatile bool s_bRemoteActivationConfigured;

		// Token: 0x040027A5 RID: 10149
		private static volatile bool s_bRegisteredWellKnownChannels;

		// Token: 0x040027A6 RID: 10150
		private static bool s_bInProcessOfRegisteringWellKnownChannels;

		// Token: 0x040027A7 RID: 10151
		private static readonly object s_delayLoadChannelLock = new object();
	}
}
