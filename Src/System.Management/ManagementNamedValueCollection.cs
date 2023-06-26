using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Management
{
	/// <summary>Represents a collection of named values suitable for use as context information to WMI operations. The names are case-insensitive.</summary>
	// Token: 0x0200001B RID: 27
	public class ManagementNamedValueCollection : NameObjectCollectionBase
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600009B RID: 155 RVA: 0x00004D80 File Offset: 0x00002F80
		// (remove) Token: 0x0600009C RID: 156 RVA: 0x00004DB8 File Offset: 0x00002FB8
		internal event IdentifierChangedEventHandler IdentifierChanged;

		// Token: 0x0600009D RID: 157 RVA: 0x00004DED File Offset: 0x00002FED
		private void FireIdentifierChanged()
		{
			if (this.IdentifierChanged != null)
			{
				this.IdentifierChanged(this, null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementNamedValueCollection" /> class, which is empty. This is the default constructor.</summary>
		// Token: 0x0600009E RID: 158 RVA: 0x00004E04 File Offset: 0x00003004
		public ManagementNamedValueCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ManagementNamedValueCollection" /> class that is serializable and uses the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" /> ) for this serialization.</param>
		// Token: 0x0600009F RID: 159 RVA: 0x00004E0C File Offset: 0x0000300C
		protected ManagementNamedValueCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004E18 File Offset: 0x00003018
		internal IWbemContext GetContext()
		{
			IWbemContext wbemContext = null;
			if (0 < this.Count)
			{
				try
				{
					wbemContext = (IWbemContext)new WbemContext();
					foreach (object obj in this)
					{
						string text = (string)obj;
						object obj2 = base.BaseGet(text);
						int num = wbemContext.SetValue_(text, 0, ref obj2);
						if (((long)num & (long)((ulong)(-2147483648))) != 0L)
						{
							break;
						}
					}
				}
				catch
				{
				}
			}
			return wbemContext;
		}

		/// <summary>Adds a single-named value to the collection.</summary>
		/// <param name="name">The name of the new value.</param>
		/// <param name="value">The value to be associated with the name.</param>
		// Token: 0x060000A1 RID: 161 RVA: 0x00004EB4 File Offset: 0x000030B4
		public void Add(string name, object value)
		{
			try
			{
				base.BaseRemove(name);
			}
			catch
			{
			}
			base.BaseAdd(name, value);
			this.FireIdentifierChanged();
		}

		/// <summary>Removes a single-named value from the collection. If the collection does not contain an element with the specified name, the collection remains unchanged and no exception is thrown.</summary>
		/// <param name="name">The name of the value to be removed.</param>
		// Token: 0x060000A2 RID: 162 RVA: 0x00004EEC File Offset: 0x000030EC
		public void Remove(string name)
		{
			base.BaseRemove(name);
			this.FireIdentifierChanged();
		}

		/// <summary>Removes all entries from the collection.</summary>
		// Token: 0x060000A3 RID: 163 RVA: 0x00004EFB File Offset: 0x000030FB
		public void RemoveAll()
		{
			base.BaseClear();
			this.FireIdentifierChanged();
		}

		/// <summary>Creates a clone of the collection. Individual values are cloned. If a value does not support cloning, then a <see cref="T:System.NotSupportedException" /> is thrown.</summary>
		/// <returns>The new copy of the collection.</returns>
		// Token: 0x060000A4 RID: 164 RVA: 0x00004F0C File Offset: 0x0000310C
		public ManagementNamedValueCollection Clone()
		{
			ManagementNamedValueCollection managementNamedValueCollection = new ManagementNamedValueCollection();
			foreach (object obj in this)
			{
				string text = (string)obj;
				object obj2 = base.BaseGet(text);
				if (obj2 != null)
				{
					Type type = obj2.GetType();
					if (type.IsByRef)
					{
						try
						{
							object obj3 = ((ICloneable)obj2).Clone();
							managementNamedValueCollection.Add(text, obj3);
							continue;
						}
						catch
						{
							throw new NotSupportedException();
						}
					}
					managementNamedValueCollection.Add(text, obj2);
				}
				else
				{
					managementNamedValueCollection.Add(text, null);
				}
			}
			return managementNamedValueCollection;
		}

		/// <summary>Gets the value associated with the specified name from this collection. In C#, this property is the indexer for the <see cref="T:System.Management.ManagementNamedValueCollection" /> class.</summary>
		/// <param name="name">The name of the value to be returned.</param>
		/// <returns>An object that is associated with the specified name from this collection.</returns>
		// Token: 0x1700001E RID: 30
		public object this[string name]
		{
			get
			{
				return base.BaseGet(name);
			}
		}
	}
}
