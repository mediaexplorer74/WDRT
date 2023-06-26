using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyDataErrorInfo.ErrorsChanged" /> event.</summary>
	// Token: 0x02000532 RID: 1330
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DataErrorsChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataErrorsChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that has an error.  <see langword="null" /> or <see cref="F:System.String.Empty" /> if the error is object-level.</param>
		// Token: 0x0600323C RID: 12860 RVA: 0x000E110C File Offset: 0x000DF30C
		[global::__DynamicallyInvokable]
		public DataErrorsChangedEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that has an error.</summary>
		/// <returns>The name of the property that has an error. <see langword="null" /> or <see cref="F:System.String.Empty" /> if the error is object-level.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000E111B File Offset: 0x000DF31B
		[global::__DynamicallyInvokable]
		public virtual string PropertyName
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002953 RID: 10579
		private readonly string propertyName;
	}
}
