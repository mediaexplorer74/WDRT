using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanging" /> event.</summary>
	// Token: 0x02000025 RID: 37
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class UserPreferenceChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.UserPreferenceChangingEventArgs" /> class using the specified user preference category identifier.</summary>
		/// <param name="category">One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicate the user preference category that is changing.</param>
		// Token: 0x0600026E RID: 622 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public UserPreferenceChangingEventArgs(UserPreferenceCategory category)
		{
			this.category = category;
		}

		/// <summary>Gets the category of user preferences that is changing.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the category of user preferences that is changing.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000F3CA File Offset: 0x0000D5CA
		public UserPreferenceCategory Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x0400038D RID: 909
		private readonly UserPreferenceCategory category;
	}
}
