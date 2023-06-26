using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies the requested behavior when setting up an advise sink or a caching connection with an object.</summary>
	// Token: 0x020003DD RID: 989
	[Flags]
	[global::__DynamicallyInvokable]
	public enum ADVF
	{
		/// <summary>For data advisory connections (<see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.ADVF,System.Runtime.InteropServices.ComTypes.IAdviseSink,System.Int32@)" /> or <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" />), this flag requests the data object not to send data when it calls <see cref="M:System.Runtime.InteropServices.ComTypes.IAdviseSink.OnDataChange(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />.</summary>
		// Token: 0x0400206F RID: 8303
		[global::__DynamicallyInvokable]
		ADVF_NODATA = 1,
		/// <summary>Requests that the object not wait for the data or view to change before making an initial call to <see cref="M:System.Runtime.InteropServices.ComTypes.IAdviseSink.OnDataChange(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> (for data or view advisory connections) or updating the cache (for cache connections).</summary>
		// Token: 0x04002070 RID: 8304
		[global::__DynamicallyInvokable]
		ADVF_PRIMEFIRST = 2,
		/// <summary>Requests that the object make only one change notification or cache update before deleting the connection.</summary>
		// Token: 0x04002071 RID: 8305
		[global::__DynamicallyInvokable]
		ADVF_ONLYONCE = 4,
		/// <summary>For data advisory connections, assures accessibility to data.</summary>
		// Token: 0x04002072 RID: 8306
		[global::__DynamicallyInvokable]
		ADVF_DATAONSTOP = 64,
		/// <summary>Synonym for <see cref="F:System.Runtime.InteropServices.ComTypes.ADVF.ADVFCACHE_FORCEBUILTIN" />, which is used more often.</summary>
		// Token: 0x04002073 RID: 8307
		[global::__DynamicallyInvokable]
		ADVFCACHE_NOHANDLER = 8,
		/// <summary>This value is used by DLL object applications and object handlers that perform the drawing of their objects.</summary>
		// Token: 0x04002074 RID: 8308
		[global::__DynamicallyInvokable]
		ADVFCACHE_FORCEBUILTIN = 16,
		/// <summary>For cache connections, this flag updates the cached representation only when the object containing the cache is saved.</summary>
		// Token: 0x04002075 RID: 8309
		[global::__DynamicallyInvokable]
		ADVFCACHE_ONSAVE = 32
	}
}
