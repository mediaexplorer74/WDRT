using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020000B4 RID: 180
	internal static class TypeUtil
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x00023D74 File Offset: 0x00021F74
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		internal static object CreateInstanceWithReflectionPermission(string typeString)
		{
			Type type = Type.GetType(typeString, true);
			return Activator.CreateInstance(type, true);
		}
	}
}
