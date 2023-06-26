using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to iterate through the access control entries (ACEs) in an access control list (ACL).</summary>
	// Token: 0x0200020A RID: 522
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x06001E84 RID: 7812 RVA: 0x0006AC67 File Offset: 0x00068E67
		internal AceEnumerator(GenericAcl collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._acl = collection;
			this.Reset();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0006AC8A File Offset: 0x00068E8A
		object IEnumerator.Current
		{
			get
			{
				if (this._current == -1 || this._current >= this._acl.Count)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_InvalidOperationException"));
				}
				return this._acl[this._current];
			}
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection. This property gets the type-friendly version of the object.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0006ACC9 File Offset: 0x00068EC9
		public GenericAce Current
		{
			get
			{
				return ((IEnumerator)this).Current as GenericAce;
			}
		}

		/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06001E87 RID: 7815 RVA: 0x0006ACD6 File Offset: 0x00068ED6
		public bool MoveNext()
		{
			this._current++;
			return this._current < this._acl.Count;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06001E88 RID: 7816 RVA: 0x0006ACF9 File Offset: 0x00068EF9
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x04000B03 RID: 2819
		private int _current;

		// Token: 0x04000B04 RID: 2820
		private readonly GenericAcl _acl;
	}
}
