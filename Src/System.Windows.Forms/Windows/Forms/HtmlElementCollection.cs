using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Defines a collection of <see cref="T:System.Windows.Forms.HtmlElement" /> objects.</summary>
	// Token: 0x0200027B RID: 635
	public sealed class HtmlElementCollection : ICollection, IEnumerable
	{
		// Token: 0x060028E0 RID: 10464 RVA: 0x000BC5C7 File Offset: 0x000BA7C7
		internal HtmlElementCollection(HtmlShimManager shimManager)
		{
			this.htmlElementCollection = null;
			this.elementsArray = null;
			this.shimManager = shimManager;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000BC5E4 File Offset: 0x000BA7E4
		internal HtmlElementCollection(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLElementCollection elements)
		{
			this.htmlElementCollection = elements;
			this.elementsArray = null;
			this.shimManager = shimManager;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000BC601 File Offset: 0x000BA801
		internal HtmlElementCollection(HtmlShimManager shimManager, HtmlElement[] array)
		{
			this.htmlElementCollection = null;
			this.elementsArray = array;
			this.shimManager = shimManager;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x000BC61E File Offset: 0x000BA81E
		private UnsafeNativeMethods.IHTMLElementCollection NativeHtmlElementCollection
		{
			get
			{
				return this.htmlElementCollection;
			}
		}

		/// <summary>Gets an item from the collection by specifying its numerical index.</summary>
		/// <param name="index">The position from which to retrieve an item from the collection.</param>
		/// <returns>An item from the collection by specifying its numerical index.</returns>
		// Token: 0x17000988 RID: 2440
		public HtmlElement this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidBoundArgument", new object[]
					{
						"index",
						index,
						0,
						this.Count - 1
					}));
				}
				if (this.NativeHtmlElementCollection != null)
				{
					UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlElementCollection.Item(index, 0) as UnsafeNativeMethods.IHTMLElement;
					if (ihtmlelement == null)
					{
						return null;
					}
					return new HtmlElement(this.shimManager, ihtmlelement);
				}
				else
				{
					if (this.elementsArray != null)
					{
						return this.elementsArray[index];
					}
					return null;
				}
			}
		}

		/// <summary>Gets an item from the collection by specifying its name.</summary>
		/// <param name="elementId">The <see cref="P:System.Windows.Forms.HtmlElement.Name" /> or <see cref="P:System.Windows.Forms.HtmlElement.Id" /> attribute of the element.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" />, if the named element is found. Otherwise, <see langword="null" />.</returns>
		// Token: 0x17000989 RID: 2441
		public HtmlElement this[string elementId]
		{
			get
			{
				if (this.NativeHtmlElementCollection != null)
				{
					UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlElementCollection.Item(elementId, 0) as UnsafeNativeMethods.IHTMLElement;
					if (ihtmlelement == null)
					{
						return null;
					}
					return new HtmlElement(this.shimManager, ihtmlelement);
				}
				else
				{
					if (this.elementsArray != null)
					{
						int num = this.elementsArray.Length;
						for (int i = 0; i < num; i++)
						{
							HtmlElement htmlElement = this.elementsArray[i];
							if (htmlElement.Id == elementId)
							{
								return htmlElement;
							}
						}
						return null;
					}
					return null;
				}
			}
		}

		/// <summary>Gets a collection of elements by their name.</summary>
		/// <param name="name">The name or ID of the element.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> containing the elements whose <see cref="P:System.Windows.Forms.HtmlElement.Name" /> property match <paramref name="name" />.</returns>
		// Token: 0x060028E6 RID: 10470 RVA: 0x000BC748 File Offset: 0x000BA948
		public HtmlElementCollection GetElementsByName(string name)
		{
			int count = this.Count;
			HtmlElement[] array = new HtmlElement[count];
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				HtmlElement htmlElement = this[i];
				if (htmlElement.GetAttribute("name") == name)
				{
					array[num] = htmlElement;
					num++;
				}
			}
			if (num == 0)
			{
				return new HtmlElementCollection(this.shimManager);
			}
			HtmlElement[] array2 = new HtmlElement[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[j];
			}
			return new HtmlElementCollection(this.shimManager, array2);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the number of elements in the collection.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000BC7D4 File Offset: 0x000BA9D4
		public int Count
		{
			get
			{
				if (this.NativeHtmlElementCollection != null)
				{
					return this.NativeHtmlElementCollection.GetLength();
				}
				if (this.elementsArray != null)
				{
					return this.elementsArray.Length;
				}
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.HtmlElementCollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins.</param>
		// Token: 0x060028EA RID: 10474 RVA: 0x000BC7FC File Offset: 0x000BA9FC
		void ICollection.CopyTo(Array dest, int index)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				dest.SetValue(this[i], index++);
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060028EB RID: 10475 RVA: 0x000BC830 File Offset: 0x000BAA30
		public IEnumerator GetEnumerator()
		{
			HtmlElement[] array = new HtmlElement[this.Count];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x040010C0 RID: 4288
		private UnsafeNativeMethods.IHTMLElementCollection htmlElementCollection;

		// Token: 0x040010C1 RID: 4289
		private HtmlElement[] elementsArray;

		// Token: 0x040010C2 RID: 4290
		private HtmlShimManager shimManager;
	}
}
