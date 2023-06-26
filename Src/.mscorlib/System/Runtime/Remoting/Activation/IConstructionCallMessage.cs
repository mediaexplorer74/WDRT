using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Represents the construction call request of an object.</summary>
	// Token: 0x02000899 RID: 2201
	[ComVisible(true)]
	public interface IConstructionCallMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		/// <summary>Gets or sets the activator that activates the remote object.</summary>
		/// <returns>The activator that activates the remote object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06005D44 RID: 23876
		// (set) Token: 0x06005D45 RID: 23877
		IActivator Activator
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		/// <summary>Gets the call site activation attributes.</summary>
		/// <returns>The call site activation attributes.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005D46 RID: 23878
		object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the full type name of the remote type to activate.</summary>
		/// <returns>The full type name of the remote type to activate.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06005D47 RID: 23879
		string ActivationTypeName
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the type of the remote object to activate.</summary>
		/// <returns>The type of the remote object to activate.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06005D48 RID: 23880
		Type ActivationType
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets a list of context properties that define the context in which the object is to be created.</summary>
		/// <returns>A list of properties of the context in which to construct the object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06005D49 RID: 23881
		IList ContextProperties
		{
			[SecurityCritical]
			get;
		}
	}
}
