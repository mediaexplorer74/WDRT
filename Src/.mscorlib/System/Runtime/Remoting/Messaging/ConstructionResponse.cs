using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> interface to create a message that responds to a call to instantiate a remote object.</summary>
	// Token: 0x0200086C RID: 2156
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> class from an array of remoting headers and a request message.</summary>
		/// <param name="h">An array of remoting headers that contain key-value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> fields for those headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <param name="mcm">A request message that constitutes a constructor call on a remote object.</param>
		// Token: 0x06005BE5 RID: 23525 RVA: 0x00143945 File Offset: 0x00141B45
		public ConstructionResponse(Header[] h, IMethodCallMessage mcm)
			: base(h, mcm)
		{
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x0014394F File Offset: 0x00141B4F
		internal ConstructionResponse(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005BE7 RID: 23527 RVA: 0x0014395C File Offset: 0x00141B5C
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new CRMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}
	}
}
