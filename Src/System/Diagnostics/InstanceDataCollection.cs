using System;
using System.Collections;
using System.Globalization;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceData" /> objects.</summary>
	// Token: 0x020004D9 RID: 1241
	public class InstanceDataCollection : DictionaryBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollection" /> class, using the specified performance counter (which defines a performance instance).</summary>
		/// <param name="counterName">The name of the counter, which often describes the quantity that is being counted.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002ED4 RID: 11988 RVA: 0x000D273C File Offset: 0x000D093C
		[Obsolete("This constructor has been deprecated.  Please use System.Diagnostics.InstanceDataCollectionCollection.get_Item to get an instance of this collection instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public InstanceDataCollection(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			this.counterName = counterName;
		}

		/// <summary>Gets the name of the performance counter whose instance data you want to get.</summary>
		/// <returns>The performance counter name.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000D2759 File Offset: 0x000D0959
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x000D2761 File Offset: 0x000D0961
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the raw counter values that comprise the instance data for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's raw data values.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000D276E File Offset: 0x000D096E
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Gets the instance data associated with this counter. This is typically a set of raw counter values.</summary>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string ("") if the category contains a single instance.</param>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceData" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollection" /> object is indexed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x17000B66 RID: 2918
		public InstanceData this[string instanceName]
		{
			get
			{
				if (instanceName == null)
				{
					throw new ArgumentNullException("instanceName");
				}
				if (instanceName.Length == 0)
				{
					instanceName = "systemdiagnosticsperfcounterlibsingleinstance";
				}
				object obj = instanceName.ToLower(CultureInfo.InvariantCulture);
				return (InstanceData)base.Dictionary[obj];
			}
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000D27C4 File Offset: 0x000D09C4
		internal void Add(string instanceName, InstanceData value)
		{
			object obj = instanceName.ToLower(CultureInfo.InvariantCulture);
			base.Dictionary.Add(obj, value);
		}

		/// <summary>Determines whether a performance instance with a specified name (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceData" /> objects) exists in the collection.</summary>
		/// <param name="instanceName">The name of the instance to find in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if the instance exists in the collection; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002EDA RID: 11994 RVA: 0x000D27EC File Offset: 0x000D09EC
		public bool Contains(string instanceName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			object obj = instanceName.ToLower(CultureInfo.InvariantCulture);
			return base.Dictionary.Contains(obj);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional array at the specified index.</summary>
		/// <param name="instances">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The zero-based index value at which to add the new instances.</param>
		// Token: 0x06002EDB RID: 11995 RVA: 0x000D281F File Offset: 0x000D0A1F
		public void CopyTo(InstanceData[] instances, int index)
		{
			base.Dictionary.Values.CopyTo(instances, index);
		}

		// Token: 0x0400278B RID: 10123
		private string counterName;
	}
}
