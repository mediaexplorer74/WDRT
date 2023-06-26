using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanged" /> event.</summary>
	// Token: 0x02000023 RID: 35
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class UserPreferenceChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.UserPreferenceChangedEventArgs" /> class using the specified user preference category identifier.</summary>
		/// <param name="category">One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the user preference category that has changed.</param>
		// Token: 0x06000268 RID: 616 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public UserPreferenceChangedEventArgs(UserPreferenceCategory category)
		{
			this.category = category;
		}

		/// <summary>Gets the category of user preferences that has changed.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the category of user preferences that has changed.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		public UserPreferenceCategory Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x0400038C RID: 908
		private readonly UserPreferenceCategory category;
	}
}
