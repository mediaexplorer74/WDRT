using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000956 RID: 2390
	[SecurityCritical]
	internal sealed class SafeHeapHandle : SafeBuffer
	{
		// Token: 0x060061FC RID: 25084 RVA: 0x0015045E File Offset: 0x0014E65E
		public SafeHeapHandle(ulong byteLength)
			: base(true)
		{
			this.Resize(byteLength);
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060061FD RID: 25085 RVA: 0x0015046E File Offset: 0x0014E66E
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x00150480 File Offset: 0x0014E680
		public void Resize(ulong byteLength)
		{
			if (base.IsClosed)
			{
				throw new ObjectDisposedException("SafeHeapHandle");
			}
			ulong num = 0UL;
			if (this.handle == IntPtr.Zero)
			{
				this.handle = Marshal.AllocHGlobal((IntPtr)((long)byteLength));
			}
			else
			{
				num = base.ByteLength;
				this.handle = Marshal.ReAllocHGlobal(this.handle, (IntPtr)((long)byteLength));
			}
			if (this.handle == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			if (byteLength > num)
			{
				ulong num2 = byteLength - num;
				if (num2 > 9223372036854775807UL)
				{
					GC.AddMemoryPressure(long.MaxValue);
					GC.AddMemoryPressure((long)(num2 - 9223372036854775807UL));
				}
				else
				{
					GC.AddMemoryPressure((long)num2);
				}
			}
			else
			{
				this.RemoveMemoryPressure(num - byteLength);
			}
			base.Initialize(byteLength);
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x0015054A File Offset: 0x0014E74A
		private void RemoveMemoryPressure(ulong removedBytes)
		{
			if (removedBytes == 0UL)
			{
				return;
			}
			if (removedBytes > 9223372036854775807UL)
			{
				GC.RemoveMemoryPressure(long.MaxValue);
				GC.RemoveMemoryPressure((long)(removedBytes - 9223372036854775807UL));
				return;
			}
			GC.RemoveMemoryPressure((long)removedBytes);
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x00150584 File Offset: 0x0014E784
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (handle != IntPtr.Zero)
			{
				this.RemoveMemoryPressure(base.ByteLength);
				Marshal.FreeHGlobal(handle);
			}
			return true;
		}
	}
}
