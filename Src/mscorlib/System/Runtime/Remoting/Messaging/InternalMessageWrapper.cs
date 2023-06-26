using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Wraps remoting data for passing between message sinks, either for requests from client to server or for the subsequent responses.</summary>
	// Token: 0x02000870 RID: 2160
	[SecurityCritical]
	[ComVisible(true)]
	public class InternalMessageWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.InternalMessageWrapper" /> class.</summary>
		/// <param name="msg">A message that acts either as an outgoing method call on a remote object, or as the subsequent response.</param>
		// Token: 0x06005C13 RID: 23571 RVA: 0x00144308 File Offset: 0x00142508
		public InternalMessageWrapper(IMessage msg)
		{
			this.WrappedMessage = msg;
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x00144318 File Offset: 0x00142518
		[SecurityCritical]
		internal object GetIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.IdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetIdentityObject();
			}
			return null;
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x00144354 File Offset: 0x00142554
		[SecurityCritical]
		internal object GetServerIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.ServerIdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetServerIdentityObject();
			}
			return null;
		}

		/// <summary>Represents the request or response <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> interface that is wrapped by the message wrapper.</summary>
		// Token: 0x0400299B RID: 10651
		protected IMessage WrappedMessage;
	}
}
