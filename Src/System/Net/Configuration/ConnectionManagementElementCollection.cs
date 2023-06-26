using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for connection management configuration elements. This class cannot be inherited.</summary>
	// Token: 0x0200032C RID: 812
	[ConfigurationCollection(typeof(ConnectionManagementElement))]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> at the specified location.</returns>
		// Token: 0x17000730 RID: 1840
		public ConnectionManagementElement this[int index]
		{
			get
			{
				return (ConnectionManagementElement)base.BaseGet(index);
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
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> with the specified key or <see langword="null" /> if there is no element with the specified key.</returns>
		// Token: 0x17000731 RID: 1841
		public ConnectionManagementElement this[string name]
		{
			get
			{
				return (ConnectionManagementElement)base.BaseGet(name);
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
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to add to the collection.</param>
		// Token: 0x06001D14 RID: 7444 RVA: 0x0008AACB File Offset: 0x00088CCB
		public void Add(ConnectionManagementElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06001D15 RID: 7445 RVA: 0x0008AAD4 File Offset: 0x00088CD4
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0008AADC File Offset: 0x00088CDC
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0008AAE3 File Offset: 0x00088CE3
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ConnectionManagementElement)element).Key;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <param name="element">A <see cref="T:System.Net.Configuration.ConnectionManagementElement" />.</param>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		// Token: 0x06001D18 RID: 7448 RVA: 0x0008AAFE File Offset: 0x00088CFE
		public int IndexOf(ConnectionManagementElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to remove.</param>
		// Token: 0x06001D19 RID: 7449 RVA: 0x0008AB07 File Offset: 0x00088D07
		public void Remove(ConnectionManagementElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06001D1A RID: 7450 RVA: 0x0008AB23 File Offset: 0x00088D23
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06001D1B RID: 7451 RVA: 0x0008AB2C File Offset: 0x00088D2C
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
