using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Gathers naming information from the context property and determines whether the new context is ok for the context property.</summary>
	// Token: 0x0200080B RID: 2059
	[ComVisible(true)]
	public interface IContextProperty
	{
		/// <summary>Gets the name of the property under which it will be added to the context.</summary>
		/// <returns>The name of the property.</returns>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060058D8 RID: 22744
		string Name
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Returns a Boolean value indicating whether the context property is compatible with the new context.</summary>
		/// <param name="newCtx">The new context in which the <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> has been created.</param>
		/// <returns>
		///   <see langword="true" /> if the context property can coexist with the other context properties in the given context; otherwise, <see langword="false" />.</returns>
		// Token: 0x060058D9 RID: 22745
		[SecurityCritical]
		bool IsNewContextOK(Context newCtx);

		/// <summary>Called when the context is frozen.</summary>
		/// <param name="newContext">The context to freeze.</param>
		// Token: 0x060058DA RID: 22746
		[SecurityCritical]
		void Freeze(Context newContext);
	}
}
