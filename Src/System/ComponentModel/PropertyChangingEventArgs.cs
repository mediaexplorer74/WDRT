using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event.</summary>
	// Token: 0x02000599 RID: 1433
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class PropertyChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property whose value is changing.</param>
		// Token: 0x0600351D RID: 13597 RVA: 0x000E7435 File Offset: 0x000E5635
		[global::__DynamicallyInvokable]
		public PropertyChangingEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property whose value is changing.</summary>
		/// <returns>The name of the property whose value is changing.</returns>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000E7444 File Offset: 0x000E5644
		[global::__DynamicallyInvokable]
		public virtual string PropertyName
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002A28 RID: 10792
		private readonly string propertyName;
	}
}
