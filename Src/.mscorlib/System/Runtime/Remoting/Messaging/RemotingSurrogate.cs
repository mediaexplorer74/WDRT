using System;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200087A RID: 2170
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x06005C6F RID: 23663 RVA: 0x00145070 File Offset: 0x00143270
		[SecurityCritical]
		public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				realProxy.GetObjectData(info, context);
				return;
			}
			RemotingServices.GetObjectData(obj, info, context);
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x001450B9 File Offset: 0x001432B9
		[SecurityCritical]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}
	}
}
