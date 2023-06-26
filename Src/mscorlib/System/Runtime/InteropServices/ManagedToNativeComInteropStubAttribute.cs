using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides support for user customization of interop stubs in managed-to-COM interop scenarios.</summary>
	// Token: 0x0200093F RID: 2367
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[ComVisible(false)]
	public sealed class ManagedToNativeComInteropStubAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute" /> class with the specified class type and method name.</summary>
		/// <param name="classType">The class that contains the required stub method.</param>
		/// <param name="methodName">The name of the stub method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="methodName" /> cannot be found.  
		/// -or-  
		/// The method is not static or non-generic.  
		/// -or-  
		/// The method's parameter list does not match the expected parameter list for the stub.</exception>
		/// <exception cref="T:System.MethodAccessException">The interface that contains the managed interop method has no access to the stub method, because the stub method has private or protected accessibility, or because of a security issue.</exception>
		// Token: 0x06006079 RID: 24697 RVA: 0x0014D72F File Offset: 0x0014B92F
		public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
		{
			this._classType = classType;
			this._methodName = methodName;
		}

		/// <summary>Gets the class that contains the required stub method.</summary>
		/// <returns>The class that contains the customized interop stub.</returns>
		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x0600607A RID: 24698 RVA: 0x0014D745 File Offset: 0x0014B945
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}

		/// <summary>Gets the name of the stub method.</summary>
		/// <returns>The name of a customized interop stub.</returns>
		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x0014D74D File Offset: 0x0014B94D
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x04002B39 RID: 11065
		internal Type _classType;

		// Token: 0x04002B3A RID: 11066
		internal string _methodName;
	}
}
