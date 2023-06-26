using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Selects the remoting surrogate that can be used to serialize an object that derives from a <see cref="T:System.MarshalByRefObject" />.</summary>
	// Token: 0x02000879 RID: 2169
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class RemotingSurrogateSelector : ISurrogateSelector
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> class.</summary>
		// Token: 0x06005C65 RID: 23653 RVA: 0x00144EFF File Offset: 0x001430FF
		public RemotingSurrogateSelector()
		{
			this._messageSurrogate = new MessageSurrogate(this);
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> delegate for the current instance of the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> delegate for the current instance of the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" />.</returns>
		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005C67 RID: 23655 RVA: 0x00144F32 File Offset: 0x00143132
		// (set) Token: 0x06005C66 RID: 23654 RVA: 0x00144F29 File Offset: 0x00143129
		public MessageSurrogateFilter Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		/// <summary>Sets the object at the root of the object graph.</summary>
		/// <param name="obj">The object at the root of the object graph.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005C68 RID: 23656 RVA: 0x00144F3C File Offset: 0x0014313C
		public void SetRootObject(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			this._rootObj = obj;
			SoapMessageSurrogate soapMessageSurrogate = this._messageSurrogate as SoapMessageSurrogate;
			if (soapMessageSurrogate != null)
			{
				soapMessageSurrogate.SetRootObject(this._rootObj);
			}
		}

		/// <summary>Returns the object at the root of the object graph.</summary>
		/// <returns>The object at the root of the object graph.</returns>
		// Token: 0x06005C69 RID: 23657 RVA: 0x00144F79 File Offset: 0x00143179
		public object GetRootObject()
		{
			return this._rootObj;
		}

		/// <summary>Adds the specified <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to the surrogate selector chain.</summary>
		/// <param name="selector">The next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to examine.</param>
		// Token: 0x06005C6A RID: 23658 RVA: 0x00144F81 File Offset: 0x00143181
		[SecurityCritical]
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			this._next = selector;
		}

		/// <summary>Returns the appropriate surrogate for the given type in the given context.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for which the surrogate is requested.</param>
		/// <param name="context">The source or destination of serialization.</param>
		/// <param name="ssout">When this method returns, contains an <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that is appropriate for the specified object type. This parameter is passed uninitialized.</param>
		/// <returns>The appropriate surrogate for the given type in the given context.</returns>
		// Token: 0x06005C6B RID: 23659 RVA: 0x00144F8C File Offset: 0x0014318C
		[SecurityCritical]
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsMarshalByRef)
			{
				ssout = this;
				return this._remotingSurrogate;
			}
			if (RemotingSurrogateSelector.s_IMethodCallMessageType.IsAssignableFrom(type) || RemotingSurrogateSelector.s_IMethodReturnMessageType.IsAssignableFrom(type))
			{
				ssout = this;
				return this._messageSurrogate;
			}
			if (RemotingSurrogateSelector.s_ObjRefType.IsAssignableFrom(type))
			{
				ssout = this;
				return this._objRefSurrogate;
			}
			if (this._next != null)
			{
				return this._next.GetSurrogate(type, context, out ssout);
			}
			ssout = null;
			return null;
		}

		/// <summary>Returns the next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> in the chain of surrogate selectors.</summary>
		/// <returns>The next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> in the chain of surrogate selectors.</returns>
		// Token: 0x06005C6C RID: 23660 RVA: 0x00145015 File Offset: 0x00143215
		[SecurityCritical]
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this._next;
		}

		/// <summary>Sets up the current surrogate selector to use the SOAP format.</summary>
		// Token: 0x06005C6D RID: 23661 RVA: 0x0014501D File Offset: 0x0014321D
		public virtual void UseSoapFormat()
		{
			this._messageSurrogate = new SoapMessageSurrogate(this);
			((SoapMessageSurrogate)this._messageSurrogate).SetRootObject(this._rootObj);
		}

		// Token: 0x040029B6 RID: 10678
		private static Type s_IMethodCallMessageType = typeof(IMethodCallMessage);

		// Token: 0x040029B7 RID: 10679
		private static Type s_IMethodReturnMessageType = typeof(IMethodReturnMessage);

		// Token: 0x040029B8 RID: 10680
		private static Type s_ObjRefType = typeof(ObjRef);

		// Token: 0x040029B9 RID: 10681
		private object _rootObj;

		// Token: 0x040029BA RID: 10682
		private ISurrogateSelector _next;

		// Token: 0x040029BB RID: 10683
		private RemotingSurrogate _remotingSurrogate = new RemotingSurrogate();

		// Token: 0x040029BC RID: 10684
		private ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();

		// Token: 0x040029BD RID: 10685
		private ISerializationSurrogate _messageSurrogate;

		// Token: 0x040029BE RID: 10686
		private MessageSurrogateFilter _filter;
	}
}
