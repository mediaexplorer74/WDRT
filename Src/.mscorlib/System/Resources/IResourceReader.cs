using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Provides the base functionality for reading data from resource files.</summary>
	// Token: 0x0200038C RID: 908
	[ComVisible(true)]
	public interface IResourceReader : IEnumerable, IDisposable
	{
		/// <summary>Closes the resource reader after releasing any resources associated with it.</summary>
		// Token: 0x06002D06 RID: 11526
		void Close();

		/// <summary>Returns a dictionary enumerator of the resources for this reader.</summary>
		/// <returns>A dictionary enumerator for the resources for this reader.</returns>
		// Token: 0x06002D07 RID: 11527
		IDictionaryEnumerator GetEnumerator();
	}
}
