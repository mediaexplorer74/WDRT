using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System
{
	/// <summary>Enables access to objects across application domain boundaries in applications that support remoting.</summary>
	// Token: 0x0200010A RID: 266
	[ComVisible(true)]
	[Serializable]
	public abstract class MarshalByRefObject
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0003090B File Offset: 0x0002EB0B
		// (set) Token: 0x06000FED RID: 4077 RVA: 0x00030913 File Offset: 0x0002EB13
		private object Identity
		{
			get
			{
				return this.__identity;
			}
			set
			{
				this.__identity = value;
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003091C File Offset: 0x0002EB1C
		[SecuritySafeCritical]
		internal IntPtr GetComIUnknown(bool fIsBeingMarshalled)
		{
			IntPtr intPtr;
			if (RemotingServices.IsTransparentProxy(this))
			{
				intPtr = RemotingServices.GetRealProxy(this).GetCOMIUnknown(fIsBeingMarshalled);
			}
			else
			{
				intPtr = Marshal.GetIUnknownForObject(this);
			}
			return intPtr;
		}

		// Token: 0x06000FEF RID: 4079
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetComIUnknown(MarshalByRefObject o);

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00030948 File Offset: 0x0002EB48
		internal bool IsInstanceOfType(Type T)
		{
			return T.IsInstanceOfType(this);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00030954 File Offset: 0x0002EB54
		internal object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			Type type = base.GetType();
			if (!type.IsCOMObject)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_InvokeMember"));
			}
			return type.InvokeMember(name, invokeAttr, binder, this, args, modifiers, culture, namedParameters);
		}

		/// <summary>Creates a shallow copy of the current <see cref="T:System.MarshalByRefObject" /> object.</summary>
		/// <param name="cloneIdentity">
		///   <see langword="false" /> to delete the current <see cref="T:System.MarshalByRefObject" /> object's identity, which will cause the object to be assigned a new identity when it is marshaled across a remoting boundary. A value of <see langword="false" /> is usually appropriate. <see langword="true" /> to copy the current <see cref="T:System.MarshalByRefObject" /> object's identity to its clone, which will cause remoting client calls to be routed to the remote server object.</param>
		/// <returns>A shallow copy of the current <see cref="T:System.MarshalByRefObject" /> object.</returns>
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00030994 File Offset: 0x0002EB94
		protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)base.MemberwiseClone();
			if (!cloneIdentity)
			{
				marshalByRefObject.Identity = null;
			}
			return marshalByRefObject;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000309B8 File Offset: 0x0002EBB8
		[SecuritySafeCritical]
		internal static Identity GetIdentity(MarshalByRefObject obj, out bool fServer)
		{
			fServer = true;
			Identity identity = null;
			if (obj != null)
			{
				if (!RemotingServices.IsTransparentProxy(obj))
				{
					identity = (Identity)obj.Identity;
				}
				else
				{
					fServer = false;
					identity = RemotingServices.GetRealProxy(obj).IdentityObject;
				}
			}
			return identity;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000309F4 File Offset: 0x0002EBF4
		internal static Identity GetIdentity(MarshalByRefObject obj)
		{
			bool flag;
			return MarshalByRefObject.GetIdentity(obj, out flag);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00030A09 File Offset: 0x0002EC09
		internal ServerIdentity __RaceSetServerIdentity(ServerIdentity id)
		{
			if (this.__identity == null)
			{
				if (!id.IsContextBound)
				{
					id.RaceSetTransparentProxy(this);
				}
				Interlocked.CompareExchange(ref this.__identity, id, null);
			}
			return (ServerIdentity)this.__identity;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00030A3C File Offset: 0x0002EC3C
		internal void __ResetServerIdentity()
		{
			this.__identity = null;
		}

		/// <summary>Retrieves the current lifetime service object that controls the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00030A45 File Offset: 0x0002EC45
		[SecurityCritical]
		public object GetLifetimeService()
		{
			return LifetimeServices.GetLease(this);
		}

		/// <summary>Obtains a lifetime service object to control the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x00030A4D File Offset: 0x0002EC4D
		[SecurityCritical]
		public virtual object InitializeLifetimeService()
		{
			return LifetimeServices.GetLeaseInitial(this);
		}

		/// <summary>Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.</summary>
		/// <param name="requestedType">The <see cref="T:System.Type" /> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> will reference.</param>
		/// <returns>Information required to generate a proxy.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">This instance is not a valid remoting object.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06000FF9 RID: 4089 RVA: 0x00030A55 File Offset: 0x0002EC55
		[SecurityCritical]
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this.__identity == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
			}
			return new ObjRef(this, requestedType);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00030A78 File Offset: 0x0002EC78
		[SecuritySafeCritical]
		internal bool CanCastToXmlType(string xmlTypeName, string xmlTypeNamespace)
		{
			Type type = SoapServices.GetInteropTypeFromXmlType(xmlTypeName, xmlTypeNamespace);
			if (type == null)
			{
				string text;
				string text2;
				if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out text, out text2))
				{
					return false;
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
					Assembly assembly = Assembly.Load(text2);
					type = assembly.GetType(text3, false, false);
				}
				catch
				{
					return false;
				}
			}
			return type != null && type.IsAssignableFrom(base.GetType());
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00030B08 File Offset: 0x0002ED08
		[SecuritySafeCritical]
		internal static bool CanCastToXmlTypeHelper(RuntimeType castType, MarshalByRefObject o)
		{
			if (castType == null)
			{
				throw new ArgumentNullException("castType");
			}
			if (!castType.IsInterface && !castType.IsMarshalByRef)
			{
				return false;
			}
			string text = null;
			string text2 = null;
			if (!SoapServices.GetXmlTypeForInteropType(castType, out text, out text2))
			{
				text = castType.Name;
				text2 = SoapServices.CodeXmlNamespaceForClrTypeNamespace(castType.Namespace, castType.GetRuntimeAssembly().GetSimpleName());
			}
			return o.CanCastToXmlType(text, text2);
		}

		// Token: 0x040005BA RID: 1466
		private object __identity;
	}
}
