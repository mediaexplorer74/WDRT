using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace msclr.interop
{
	// Token: 0x02000006 RID: 6
	internal class marshal_context : IDisposable
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00001844 File Offset: 0x00000C44
		private void ~marshal_context()
		{
			LinkedList<object>.Enumerator enumerator = this._clean_up_list.GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					IDisposable disposable = enumerator.Current as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				while (enumerator.MoveNext());
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00001888 File Offset: 0x00000C88
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~marshal_context();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00002164 File Offset: 0x00001564
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400012C RID: 300
		internal readonly LinkedList<object> _clean_up_list = new LinkedList<object>();
	}
}
