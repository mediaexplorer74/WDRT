using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BC RID: 188
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct JEnumerable<[Nullable(0)] T> : IJEnumerable<T>, IEnumerable<T>, IEnumerable, IEquatable<JEnumerable<T>> where T : JToken
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x00029664 File Offset: 0x00027864
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00029678 File Offset: 0x00027878
		public IEnumerator<T> GetEnumerator()
		{
			return (this._enumerable ?? JEnumerable<T>.Empty).GetEnumerator();
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00029693 File Offset: 0x00027893
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001D9 RID: 473
		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				if (this._enumerable == null)
				{
					return JEnumerable<JToken>.Empty;
				}
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000296C6 File Offset: 0x000278C6
		public bool Equals([Nullable(new byte[] { 0, 1 })] JEnumerable<T> other)
		{
			return object.Equals(this._enumerable, other._enumerable);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000296DC File Offset: 0x000278DC
		public override bool Equals(object obj)
		{
			if (obj is JEnumerable<T>)
			{
				JEnumerable<T> jenumerable = (JEnumerable<T>)obj;
				return this.Equals(jenumerable);
			}
			return false;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00029701 File Offset: 0x00027901
		public override int GetHashCode()
		{
			if (this._enumerable == null)
			{
				return 0;
			}
			return this._enumerable.GetHashCode();
		}

		// Token: 0x04000366 RID: 870
		[Nullable(new byte[] { 0, 1 })]
		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		// Token: 0x04000367 RID: 871
		private readonly IEnumerable<T> _enumerable;
	}
}
