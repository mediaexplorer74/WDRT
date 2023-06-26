using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace msclr.interop
{
	// Token: 0x02000005 RID: 5
	internal class context_node<wchar_t\u0020const\u0020*,System::String\u0020^> : context_node_base, IDisposable
	{
		// Token: 0x06000297 RID: 663 RVA: 0x000017C0 File Offset: 0x00000BC0
		public unsafe context_node<wchar_t\u0020const\u0020*,System::String\u0020^>(char** _to_object, string _from_object)
		{
			IntPtr intPtr = Marshal.StringToHGlobalUni(_from_object);
			this._ip = intPtr;
			*_to_object = this._ip.ToPointer();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000020F8 File Offset: 0x000014F8
		private void ~context_node<wchar_t\u0020const\u0020*,System::String\u0020^>()
		{
			this.!context_node<wchar_t\u0020const\u0020*,System::String\u0020^>();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000017F0 File Offset: 0x00000BF0
		private void !context_node<wchar_t\u0020const\u0020*,System::String\u0020^>()
		{
			if (this._ip != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this._ip);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000210C File Offset: 0x0000150C
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!context_node<wchar_t\u0020const\u0020*,System::String\u0020^>();
			}
			else
			{
				try
				{
					this.!context_node<wchar_t\u0020const\u0020*,System::String\u0020^>();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00002394 File Offset: 0x00001794
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00002150 File Offset: 0x00001550
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400012B RID: 299
		private IntPtr _ip;
	}
}
