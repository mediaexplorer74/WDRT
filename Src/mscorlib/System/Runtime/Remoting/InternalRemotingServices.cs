using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting
{
	/// <summary>Defines utility methods for use by the .NET Framework remoting infrastructure.</summary>
	// Token: 0x020007C9 RID: 1993
	[SecurityCritical]
	[ComVisible(true)]
	public class InternalRemotingServices
	{
		/// <summary>Sends a message concerning a remoting channel to an unmanaged debugger.</summary>
		/// <param name="s">A string to place in the message.</param>
		// Token: 0x0600569C RID: 22172 RVA: 0x00134855 File Offset: 0x00132A55
		[SecurityCritical]
		[Conditional("_LOGGING")]
		public static void DebugOutChnl(string s)
		{
			Message.OutToUnmanagedDebugger("CHNL:" + s + "\n");
		}

		/// <summary>Sends any number of messages concerning remoting channels to an internal debugger.</summary>
		/// <param name="messages">An array of type <see cref="T:System.Object" /> that contains any number of messages.</param>
		// Token: 0x0600569D RID: 22173 RVA: 0x0013486C File Offset: 0x00132A6C
		[Conditional("_LOGGING")]
		public static void RemotingTrace(params object[] messages)
		{
		}

		/// <summary>Instructs an internal debugger to check for a condition and display a message if the condition is <see langword="false" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to prevent a message from being displayed; otherwise, <see langword="false" />.</param>
		/// <param name="message">The message to display if <paramref name="condition" /> is <see langword="false" />.</param>
		// Token: 0x0600569E RID: 22174 RVA: 0x0013486E File Offset: 0x00132A6E
		[Conditional("_DEBUG")]
		public static void RemotingAssert(bool condition, string message)
		{
		}

		/// <summary>Sets internal identifying information for a remoted server object for each method call from client to server.</summary>
		/// <param name="m">A <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> that represents a method call on a remote object.</param>
		/// <param name="srvID">Internal identifying information for a remoted server object.</param>
		// Token: 0x0600569F RID: 22175 RVA: 0x00134870 File Offset: 0x00132A70
		[SecurityCritical]
		[CLSCompliant(false)]
		public static void SetServerIdentity(MethodCall m, object srvID)
		{
			((IInternalMessage)m).ServerIdentityObject = (ServerIdentity)srvID;
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x0013488C File Offset: 0x00132A8C
		internal static RemotingMethodCachedData GetReflectionCachedData(MethodBase mi)
		{
			RuntimeMethodInfo runtimeMethodInfo;
			if ((runtimeMethodInfo = mi as RuntimeMethodInfo) != null)
			{
				return runtimeMethodInfo.RemotingCache;
			}
			RuntimeConstructorInfo runtimeConstructorInfo;
			if ((runtimeConstructorInfo = mi as RuntimeConstructorInfo) != null)
			{
				return runtimeConstructorInfo.RemotingCache;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x001348DA File Offset: 0x00132ADA
		internal static RemotingTypeCachedData GetReflectionCachedData(RuntimeType type)
		{
			return type.RemotingCache;
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x001348E4 File Offset: 0x00132AE4
		internal static RemotingCachedData GetReflectionCachedData(MemberInfo mi)
		{
			MethodBase methodBase;
			if ((methodBase = mi as MethodBase) != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(methodBase);
			}
			RuntimeType runtimeType;
			if ((runtimeType = mi as RuntimeType) != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(runtimeType);
			}
			RuntimeFieldInfo runtimeFieldInfo;
			if ((runtimeFieldInfo = mi as RuntimeFieldInfo) != null)
			{
				return runtimeFieldInfo.RemotingCache;
			}
			SerializationFieldInfo serializationFieldInfo;
			if ((serializationFieldInfo = mi as SerializationFieldInfo) != null)
			{
				return serializationFieldInfo.RemotingCache;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00134964 File Offset: 0x00132B64
		internal static RemotingCachedData GetReflectionCachedData(RuntimeParameterInfo reflectionObject)
		{
			return reflectionObject.RemotingCache;
		}

		/// <summary>Gets an appropriate SOAP-related attribute for the specified class member or method parameter.</summary>
		/// <param name="reflectionObject">A class member or method parameter.</param>
		/// <returns>The SOAP-related attribute for the specified class member or method parameter.</returns>
		// Token: 0x060056A4 RID: 22180 RVA: 0x0013496C File Offset: 0x00132B6C
		[SecurityCritical]
		public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
		{
			MemberInfo memberInfo = reflectionObject as MemberInfo;
			RuntimeParameterInfo runtimeParameterInfo = reflectionObject as RuntimeParameterInfo;
			if (memberInfo != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(memberInfo).GetSoapAttribute();
			}
			if (runtimeParameterInfo != null)
			{
				return InternalRemotingServices.GetReflectionCachedData(runtimeParameterInfo).GetSoapAttribute();
			}
			return null;
		}
	}
}
