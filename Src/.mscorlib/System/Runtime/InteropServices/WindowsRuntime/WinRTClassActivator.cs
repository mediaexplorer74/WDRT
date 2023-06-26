using System;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A11 RID: 2577
	internal sealed class WinRTClassActivator : MarshalByRefObject, IWinRTClassActivator
	{
		// Token: 0x060065D1 RID: 26065 RVA: 0x0015B2C0 File Offset: 0x001594C0
		[SecurityCritical]
		public object ActivateInstance(string activatableClassId)
		{
			ManagedActivationFactory managedActivationFactory = WindowsRuntimeMarshal.GetManagedActivationFactory(this.LoadWinRTType(activatableClassId));
			return managedActivationFactory.ActivateInstance();
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x0015B2E0 File Offset: 0x001594E0
		[SecurityCritical]
		public IntPtr GetActivationFactory(string activatableClassId, ref Guid iid)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2;
			try
			{
				intPtr = WindowsRuntimeMarshal.GetActivationFactoryForType(this.LoadWinRTType(activatableClassId));
				IntPtr zero = IntPtr.Zero;
				int num = Marshal.QueryInterface(intPtr, ref iid, out zero);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				intPtr2 = zero;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
			}
			return intPtr2;
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x0015B344 File Offset: 0x00159544
		private Type LoadWinRTType(string acid)
		{
			Type type = Type.GetType(acid + ", Windows, ContentType=WindowsRuntime");
			if (type == null)
			{
				throw new COMException(-2147221164);
			}
			return type;
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x0015B377 File Offset: 0x00159577
		[SecurityCritical]
		internal IntPtr GetIWinRTClassActivator()
		{
			return Marshal.GetComInterfaceForObject(this, typeof(IWinRTClassActivator));
		}
	}
}
