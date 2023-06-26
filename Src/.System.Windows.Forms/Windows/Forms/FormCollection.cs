using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Form" /> objects.</summary>
	// Token: 0x02000262 RID: 610
	public class FormCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets or sets an element in the collection by the name of the associated <see cref="T:System.Windows.Forms.Form" /> object.</summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.Form" />.</param>
		/// <returns>The form with the specified name.</returns>
		// Token: 0x17000922 RID: 2338
		public virtual Form this[string name]
		{
			get
			{
				if (name != null)
				{
					object collectionSyncRoot = FormCollection.CollectionSyncRoot;
					lock (collectionSyncRoot)
					{
						foreach (object obj in base.InnerList)
						{
							Form form = (Form)obj;
							if (string.Equals(form.Name, name, StringComparison.OrdinalIgnoreCase))
							{
								return form;
							}
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets an element in the collection by its numeric index.</summary>
		/// <param name="index">The location of the <see cref="T:System.Windows.Forms.Form" /> within the collection.</param>
		/// <returns>The form at the specified index.</returns>
		// Token: 0x17000923 RID: 2339
		public virtual Form this[int index]
		{
			get
			{
				Form form = null;
				object collectionSyncRoot = FormCollection.CollectionSyncRoot;
				lock (collectionSyncRoot)
				{
					form = (Form)base.InnerList[index];
				}
				return form;
			}
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000B8BF8 File Offset: 0x000B6DF8
		internal void Add(Form form)
		{
			object collectionSyncRoot = FormCollection.CollectionSyncRoot;
			lock (collectionSyncRoot)
			{
				base.InnerList.Add(form);
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000B8C40 File Offset: 0x000B6E40
		internal bool Contains(Form form)
		{
			bool flag = false;
			object collectionSyncRoot = FormCollection.CollectionSyncRoot;
			lock (collectionSyncRoot)
			{
				flag = base.InnerList.Contains(form);
			}
			return flag;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000B8C8C File Offset: 0x000B6E8C
		internal void Remove(Form form)
		{
			object collectionSyncRoot = FormCollection.CollectionSyncRoot;
			lock (collectionSyncRoot)
			{
				base.InnerList.Remove(form);
			}
		}

		// Token: 0x0400103A RID: 4154
		internal static object CollectionSyncRoot = new object();
	}
}
