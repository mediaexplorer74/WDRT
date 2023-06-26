using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200050F RID: 1295
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal class ArraySubsetEnumerator : IEnumerator
	{
		// Token: 0x0600310F RID: 12559 RVA: 0x000DE5C0 File Offset: 0x000DC7C0
		public ArraySubsetEnumerator(Array array, int count)
		{
			this.array = array;
			this.total = count;
			this.current = -1;
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000DE5DD File Offset: 0x000DC7DD
		public bool MoveNext()
		{
			if (this.current < this.total - 1)
			{
				this.current++;
				return true;
			}
			return false;
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000DE600 File Offset: 0x000DC800
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000DE609 File Offset: 0x000DC809
		public object Current
		{
			get
			{
				if (this.current == -1)
				{
					throw new InvalidOperationException();
				}
				return this.array.GetValue(this.current);
			}
		}

		// Token: 0x040028EF RID: 10479
		private Array array;

		// Token: 0x040028F0 RID: 10480
		private int total;

		// Token: 0x040028F1 RID: 10481
		private int current;
	}
}
