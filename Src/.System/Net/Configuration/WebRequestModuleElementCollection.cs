using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for Web request module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x0200034B RID: 843
	[ConfigurationCollection(typeof(WebRequestModuleElement))]
	public sealed class WebRequestModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> at the specified location.</returns>
		// Token: 0x170007D0 RID: 2000
		public WebRequestModuleElement this[int index]
		{
			get
			{
				return (WebRequestModuleElement)base.BaseGet(index);
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
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> with the specified key or <see langword="null" /> if there is no element with the specified key.</returns>
		// Token: 0x170007D1 RID: 2001
		public WebRequestModuleElement this[string name]
		{
			get
			{
				return (WebRequestModuleElement)base.BaseGet(name);
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
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to add to the collection.</param>
		// Token: 0x06001E34 RID: 7732 RVA: 0x0008D929 File Offset: 0x0008BB29
		public void Add(WebRequestModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06001E35 RID: 7733 RVA: 0x0008D932 File Offset: 0x0008BB32
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0008D93A File Offset: 0x0008BB3A
		protected override ConfigurationElement CreateNewElement()
		{
			return new WebRequestModuleElement();
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0008D941 File Offset: 0x0008BB41
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((WebRequestModuleElement)element).Key;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <param name="element">A <see cref="T:System.Net.Configuration.WebRequestModuleElement" />.</param>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		// Token: 0x06001E38 RID: 7736 RVA: 0x0008D95C File Offset: 0x0008BB5C
		public int IndexOf(WebRequestModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to remove.</param>
		// Token: 0x06001E39 RID: 7737 RVA: 0x0008D965 File Offset: 0x0008BB65
		public void Remove(WebRequestModuleElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06001E3A RID: 7738 RVA: 0x0008D981 File Offset: 0x0008BB81
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06001E3B RID: 7739 RVA: 0x0008D98A File Offset: 0x0008BB8A
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
