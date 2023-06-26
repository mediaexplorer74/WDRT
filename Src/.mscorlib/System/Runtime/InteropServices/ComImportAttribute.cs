using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed type was previously defined in COM.</summary>
	// Token: 0x0200092A RID: 2346
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComImportAttribute : Attribute
	{
		// Token: 0x0600603E RID: 24638 RVA: 0x0014D1CE File Offset: 0x0014B3CE
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
			{
				return null;
			}
			return new ComImportAttribute();
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x0014D1E5 File Offset: 0x0014B3E5
		internal static bool IsDefined(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />.</summary>
		// Token: 0x06006040 RID: 24640 RVA: 0x0014D1F6 File Offset: 0x0014B3F6
		[__DynamicallyInvokable]
		public ComImportAttribute()
		{
		}
	}
}
