using System;

namespace System.Security.Permissions
{
	// Token: 0x020002E7 RID: 743
	[Serializable]
	internal enum BuiltInPermissionFlag
	{
		// Token: 0x04000EBC RID: 3772
		EnvironmentPermission = 1,
		// Token: 0x04000EBD RID: 3773
		FileDialogPermission,
		// Token: 0x04000EBE RID: 3774
		FileIOPermission = 4,
		// Token: 0x04000EBF RID: 3775
		IsolatedStorageFilePermission = 8,
		// Token: 0x04000EC0 RID: 3776
		ReflectionPermission = 16,
		// Token: 0x04000EC1 RID: 3777
		RegistryPermission = 32,
		// Token: 0x04000EC2 RID: 3778
		SecurityPermission = 64,
		// Token: 0x04000EC3 RID: 3779
		UIPermission = 128,
		// Token: 0x04000EC4 RID: 3780
		PrincipalPermission = 256,
		// Token: 0x04000EC5 RID: 3781
		PublisherIdentityPermission = 512,
		// Token: 0x04000EC6 RID: 3782
		SiteIdentityPermission = 1024,
		// Token: 0x04000EC7 RID: 3783
		StrongNameIdentityPermission = 2048,
		// Token: 0x04000EC8 RID: 3784
		UrlIdentityPermission = 4096,
		// Token: 0x04000EC9 RID: 3785
		ZoneIdentityPermission = 8192,
		// Token: 0x04000ECA RID: 3786
		KeyContainerPermission = 16384
	}
}
