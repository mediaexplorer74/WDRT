using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for the addresses of resources that bypass the proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000329 RID: 809
	[ConfigurationCollection(typeof(BypassElement))]
	public sealed class BypassElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> at the specified location.</returns>
		// Token: 0x17000722 RID: 1826
		public BypassElement this[int index]
		{
			get
			{
				return (BypassElement)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="name">The key for an element in the collection.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> with the specified key, or <see langword="null" /> if there is no element with the specified key.</returns>
		// Token: 0x17000723 RID: 1827
		public BypassElement this[string name]
		{
			get
			{
				return (BypassElement)base.BaseGet(name);
			}
			set
			{
				if (base.BaseGet(name) != null)
				{
					base.BaseRemove(name);
				}
				this.BaseAdd(value);
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to add to the collection.</param>
		// Token: 0x06001CF5 RID: 7413 RVA: 0x0008A878 File Offset: 0x00088A78
		public void Add(BypassElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06001CF6 RID: 7414 RVA: 0x0008A881 File Offset: 0x00088A81
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0008A889 File Offset: 0x00088A89
		protected override ConfigurationElement CreateNewElement()
		{
			return new BypassElement();
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0008A890 File Offset: 0x00088A90
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((BypassElement)element).Key;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <param name="element">A <see cref="T:System.Net.Configuration.BypassElement" />.</param>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		// Token: 0x06001CF9 RID: 7417 RVA: 0x0008A8AB File Offset: 0x00088AAB
		public int IndexOf(BypassElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to remove.</param>
		// Token: 0x06001CFA RID: 7418 RVA: 0x0008A8B4 File Offset: 0x00088AB4
		public void Remove(BypassElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06001CFB RID: 7419 RVA: 0x0008A8D0 File Offset: 0x00088AD0
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06001CFC RID: 7420 RVA: 0x0008A8D9 File Offset: 0x00088AD9
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0008A8E2 File Offset: 0x00088AE2
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}
	}
}
