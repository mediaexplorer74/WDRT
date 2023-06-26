using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the HRESULT or <see langword="retval" /> signature transformation that takes place during COM interop calls should be suppressed.</summary>
	// Token: 0x0200092C RID: 2348
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class PreserveSigAttribute : Attribute
	{
		// Token: 0x06006043 RID: 24643 RVA: 0x0014D215 File Offset: 0x0014B415
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
			{
				return null;
			}
			return new PreserveSigAttribute();
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x0014D22C File Offset: 0x0014B42C
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.PreserveSigAttribute" /> class.</summary>
		// Token: 0x06006045 RID: 24645 RVA: 0x0014D23D File Offset: 0x0014B43D
		[__DynamicallyInvokable]
		public PreserveSigAttribute()
		{
		}
	}
}
