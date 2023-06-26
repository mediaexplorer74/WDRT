using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Implements a base class that holds the configuration information used to activate an instance of a remote type.</summary>
	// Token: 0x020007BE RID: 1982
	[ComVisible(true)]
	public class TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.TypeEntry" /> class.</summary>
		// Token: 0x06005605 RID: 22021 RVA: 0x00132718 File Offset: 0x00130918
		protected TypeEntry()
		{
		}

		/// <summary>Gets the full type name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The full type name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06005606 RID: 22022 RVA: 0x00132720 File Offset: 0x00130920
		// (set) Token: 0x06005607 RID: 22023 RVA: 0x00132728 File Offset: 0x00130928
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		/// <summary>Gets the assembly name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The assembly name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06005608 RID: 22024 RVA: 0x00132731 File Offset: 0x00130931
		// (set) Token: 0x06005609 RID: 22025 RVA: 0x00132739 File Offset: 0x00130939
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				this._assemblyName = value;
			}
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x00132742 File Offset: 0x00130942
		internal void CacheRemoteAppEntry(RemoteAppEntry entry)
		{
			this._cachedRemoteAppEntry = entry;
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x0013274B File Offset: 0x0013094B
		internal RemoteAppEntry GetRemoteAppEntry()
		{
			return this._cachedRemoteAppEntry;
		}

		// Token: 0x04002785 RID: 10117
		private string _typeName;

		// Token: 0x04002786 RID: 10118
		private string _assemblyName;

		// Token: 0x04002787 RID: 10119
		private RemoteAppEntry _cachedRemoteAppEntry;
	}
}
