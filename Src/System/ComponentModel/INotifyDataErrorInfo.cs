using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Defines members that data entity classes can implement to provide custom synchronous and asynchronous validation support.</summary>
	// Token: 0x02000568 RID: 1384
	[global::__DynamicallyInvokable]
	public interface INotifyDataErrorInfo
	{
		/// <summary>Gets a value that indicates whether the entity has validation errors.</summary>
		/// <returns>
		///   <see langword="true" /> if the entity currently has validation errors; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060033A0 RID: 13216
		[global::__DynamicallyInvokable]
		bool HasErrors
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the validation errors for a specified property or for the entire entity.</summary>
		/// <param name="propertyName">The name of the property to retrieve validation errors for; or <see langword="null" /> or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
		/// <returns>The validation errors for the property or entity.</returns>
		// Token: 0x060033A1 RID: 13217
		[global::__DynamicallyInvokable]
		IEnumerable GetErrors(string propertyName);

		/// <summary>Occurs when the validation errors have changed for a property or for the entire entity.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060033A2 RID: 13218
		// (remove) Token: 0x060033A3 RID: 13219
		[global::__DynamicallyInvokable]
		event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
	}
}
