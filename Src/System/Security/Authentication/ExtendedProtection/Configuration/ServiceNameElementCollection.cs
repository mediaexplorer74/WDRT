using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> class is a collection of service principal names that represent a configuration element for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
	// Token: 0x0200044B RID: 1099
	[ConfigurationCollection(typeof(ServiceNameElement))]
	public sealed class ServiceNameElementCollection : ConfigurationElementCollection
	{
		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance at the specified index location.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then <see langword="null" /> is returned.</returns>
		// Token: 0x170009F9 RID: 2553
		public ServiceNameElement this[int index]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(index);
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

		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance based on a string that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then <see langword="null" /> is returned.</returns>
		// Token: 0x170009FA RID: 2554
		public ServiceNameElement this[string name]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(name);
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

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Add(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method adds a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to add to this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x060028A1 RID: 10401 RVA: 0x000BA6E6 File Offset: 0x000B88E6
		public void Add(ServiceNameElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Clear" /> method removes all configuration element objects from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		// Token: 0x060028A2 RID: 10402 RVA: 0x000BA6EF File Offset: 0x000B88EF
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000BA6F7 File Offset: 0x000B88F7
		protected override ConfigurationElement CreateNewElement()
		{
			return new ServiceNameElement();
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000BA6FE File Offset: 0x000B88FE
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ServiceNameElement)element).Key;
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.IndexOf(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method retrieves the index of the specified configuration element in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to retrieve the index of in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <returns>The index of the specified <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</returns>
		// Token: 0x060028A5 RID: 10405 RVA: 0x000BA719 File Offset: 0x000B8919
		public int IndexOf(ServiceNameElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is <see langword="null" />.</exception>
		// Token: 0x060028A6 RID: 10406 RVA: 0x000BA722 File Offset: 0x000B8922
		public void Remove(ServiceNameElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Key);
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> based on the <see cref="T:System.String" /> specified.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /></param>
		// Token: 0x060028A7 RID: 10407 RVA: 0x000BA73E File Offset: 0x000B893E
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> based on the index specified.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x060028A8 RID: 10408 RVA: 0x000BA747 File Offset: 0x000B8947
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
