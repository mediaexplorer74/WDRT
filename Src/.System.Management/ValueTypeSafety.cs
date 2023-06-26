using System;
using System.Runtime.CompilerServices;

namespace System.Management
{
	// Token: 0x02000047 RID: 71
	internal class ValueTypeSafety
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000DC15 File Offset: 0x0000BE15
		public static object GetSafeObject(object theValue)
		{
			if (theValue == null)
			{
				return null;
			}
			if (theValue.GetType().IsPrimitive)
			{
				return ((IConvertible)theValue).ToType(typeof(object), null);
			}
			return RuntimeHelpers.GetObjectValue(theValue);
		}
	}
}
