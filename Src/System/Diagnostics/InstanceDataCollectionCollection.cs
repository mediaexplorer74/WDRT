using System;
using System.Collections;
using System.Globalization;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects.</summary>
	// Token: 0x020004DA RID: 1242
	public class InstanceDataCollectionCollection : DictionaryBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> class.</summary>
		// Token: 0x06002EDC RID: 11996 RVA: 0x000D2833 File Offset: 0x000D0A33
		[Obsolete("This constructor has been deprecated.  Please use System.Diagnostics.PerformanceCounterCategory.ReadCategory() to get an instance of this collection instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public InstanceDataCollectionCollection()
		{
		}

		/// <summary>Gets the instance data for the specified counter.</summary>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollection" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> object is indexed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x17000B67 RID: 2919
		public InstanceDataCollection this[string counterName]
		{
			get
			{
				if (counterName == null)
				{
					throw new ArgumentNullException("counterName");
				}
				object obj = counterName.ToLower(CultureInfo.InvariantCulture);
				return (InstanceDataCollection)base.Dictionary[obj];
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000D2874 File Offset: 0x000D0A74
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the instance data values that comprise the collection of instances for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's instances and their associated data values.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000D2881 File Offset: 0x000D0A81
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000D2890 File Offset: 0x000D0A90
		internal void Add(string counterName, InstanceDataCollection value)
		{
			object obj = counterName.ToLower(CultureInfo.InvariantCulture);
			base.Dictionary.Add(obj, value);
		}

		/// <summary>Determines whether an instance data collection for the specified counter (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects) exists in the collection.</summary>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <returns>
		///   <see langword="true" /> if an instance data collection containing the specified counter exists in the collection; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002EE1 RID: 12001 RVA: 0x000D28B8 File Offset: 0x000D0AB8
		public bool Contains(string counterName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			object obj = counterName.ToLower(CultureInfo.InvariantCulture);
			return base.Dictionary.Contains(obj);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances to the collection, at the specified index.</summary>
		/// <param name="counters">An array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances (identified by the counters they contain) to add to the collection.</param>
		/// <param name="index">The location at which to add the new instances.</param>
		// Token: 0x06002EE2 RID: 12002 RVA: 0x000D28EB File Offset: 0x000D0AEB
		public void CopyTo(InstanceDataCollection[] counters, int index)
		{
			base.Dictionary.Values.CopyTo(counters, index);
		}
	}
}
