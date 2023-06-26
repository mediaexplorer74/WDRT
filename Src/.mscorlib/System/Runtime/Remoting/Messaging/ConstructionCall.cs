using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> interface to create a request message that constitutes a constructor call on a remote object.</summary>
	// Token: 0x02000868 RID: 2152
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class from an array of remoting headers.</summary>
		/// <param name="headers">An array of remoting headers that contain key-value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> fields for those headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		// Token: 0x06005BAB RID: 23467 RVA: 0x00142ACB File Offset: 0x00140CCB
		public ConstructionCall(Header[] headers)
			: base(headers)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class by copying an existing message.</summary>
		/// <param name="m">A remoting message.</param>
		// Token: 0x06005BAC RID: 23468 RVA: 0x00142AD4 File Offset: 0x00140CD4
		public ConstructionCall(IMessage m)
			: base(m)
		{
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x00142ADD File Offset: 0x00140CDD
		internal ConstructionCall(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x00142AE8 File Offset: 0x00140CE8
		[SecurityCritical]
		internal override bool FillSpecialHeader(string key, object value)
		{
			if (key != null)
			{
				if (key.Equals("__ActivationType"))
				{
					this._activationType = null;
				}
				else if (key.Equals("__ContextProperties"))
				{
					this._contextProperties = (IList)value;
				}
				else if (key.Equals("__CallSiteActivationAttributes"))
				{
					this._callSiteActivationAttributes = (object[])value;
				}
				else if (key.Equals("__Activator"))
				{
					this._activator = (IActivator)value;
				}
				else
				{
					if (!key.Equals("__ActivationTypeName"))
					{
						return base.FillSpecialHeader(key, value);
					}
					this._activationTypeName = (string)value;
				}
			}
			return true;
		}

		/// <summary>Gets the call site activation attributes for the remote object.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> containing the call site activation attributes for the remote object.</returns>
		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x00142B87 File Offset: 0x00140D87
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._callSiteActivationAttributes;
			}
		}

		/// <summary>Gets the type of the remote object to activate.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the remote object to activate.</returns>
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x00142B8F File Offset: 0x00140D8F
		public Type ActivationType
		{
			[SecurityCritical]
			get
			{
				if (this._activationType == null && this._activationTypeName != null)
				{
					this._activationType = RemotingServices.InternalGetTypeFromQualifiedTypeName(this._activationTypeName, false);
				}
				return this._activationType;
			}
		}

		/// <summary>Gets the full type name of the remote object to activate.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the full type name of the remote object to activate.</returns>
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x00142BBF File Offset: 0x00140DBF
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		/// <summary>Gets a list of properties that define the context in which the remote object is to be created.</summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> that contains a list of properties that define the context in which the remote object is to be created.</returns>
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x00142BC7 File Offset: 0x00140DC7
		public IList ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06005BB3 RID: 23475 RVA: 0x00142BE4 File Offset: 0x00140DE4
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
						this.ExternalProperties = new CCMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}

		/// <summary>Gets or sets the activator that activates the remote object.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Activation.IActivator" /> that activates the remote object.</returns>
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06005BB4 RID: 23476 RVA: 0x00142C50 File Offset: 0x00140E50
		// (set) Token: 0x06005BB5 RID: 23477 RVA: 0x00142C58 File Offset: 0x00140E58
		public IActivator Activator
		{
			[SecurityCritical]
			get
			{
				return this._activator;
			}
			[SecurityCritical]
			set
			{
				this._activator = value;
			}
		}

		// Token: 0x04002973 RID: 10611
		internal Type _activationType;

		// Token: 0x04002974 RID: 10612
		internal string _activationTypeName;

		// Token: 0x04002975 RID: 10613
		internal IList _contextProperties;

		// Token: 0x04002976 RID: 10614
		internal object[] _callSiteActivationAttributes;

		// Token: 0x04002977 RID: 10615
		internal IActivator _activator;
	}
}
