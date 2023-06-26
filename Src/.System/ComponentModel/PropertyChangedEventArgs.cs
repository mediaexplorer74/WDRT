using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
	// Token: 0x02000597 RID: 1431
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class PropertyChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that changed.</param>
		// Token: 0x06003517 RID: 13591 RVA: 0x000E741E File Offset: 0x000E561E
		[global::__DynamicallyInvokable]
		public PropertyChangedEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that changed.</summary>
		/// <returns>The name of the property that changed.</returns>
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000E742D File Offset: 0x000E562D
		[global::__DynamicallyInvokable]
		public virtual string PropertyName
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002A27 RID: 10791
		private readonly string propertyName;
	}
}
