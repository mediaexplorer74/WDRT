using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a read-only container for a collection of <see cref="T:System.ComponentModel.IComponent" /> objects.</summary>
	// Token: 0x0200052B RID: 1323
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class ComponentCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComponentCollection" /> class using the specified array of components.</summary>
		/// <param name="components">An array of <see cref="T:System.ComponentModel.IComponent" /> objects to initialize the collection with.</param>
		// Token: 0x06003208 RID: 12808 RVA: 0x000E0317 File Offset: 0x000DE517
		public ComponentCollection(IComponent[] components)
		{
			base.InnerList.AddRange(components);
		}

		/// <summary>Gets any component in the collection matching the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.IComponent" /> to get.</param>
		/// <returns>A component with a name matching the name specified by the <paramref name="name" /> parameter, or <see langword="null" /> if the named component cannot be found in the collection.</returns>
		// Token: 0x17000C48 RID: 3144
		public virtual IComponent this[string name]
		{
			get
			{
				if (name != null)
				{
					IList innerList = base.InnerList;
					foreach (object obj in innerList)
					{
						IComponent component = (IComponent)obj;
						if (component != null && component.Site != null && component.Site.Name != null && string.Equals(component.Site.Name, name, StringComparison.OrdinalIgnoreCase))
						{
							return component;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Component" /> in the collection at the specified collection index.</summary>
		/// <param name="index">The collection index of the <see cref="T:System.ComponentModel.Component" /> to get.</param>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">If the specified index is not within the index range of the collection.</exception>
		// Token: 0x17000C49 RID: 3145
		public virtual IComponent this[int index]
		{
			get
			{
				return (IComponent)base.InnerList[index];
			}
		}

		/// <summary>Copies the entire collection to an array, starting writing at the specified array index.</summary>
		/// <param name="array">An <see cref="T:System.ComponentModel.IComponent" /> array to copy the objects in the collection to.</param>
		/// <param name="index">The index of the <paramref name="array" /> at which copying to should begin.</param>
		// Token: 0x0600320B RID: 12811 RVA: 0x000E03CF File Offset: 0x000DE5CF
		public void CopyTo(IComponent[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
