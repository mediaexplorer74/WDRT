using System;

namespace System.Diagnostics
{
	/// <summary>Holds instance data associated with a performance counter sample.</summary>
	// Token: 0x020004D8 RID: 1240
	public class InstanceData
	{
		/// <summary>Initializes a new instance of the InstanceData class, using the specified sample and performance counter instance.</summary>
		/// <param name="instanceName">The name of an instance associated with the performance counter.</param>
		/// <param name="sample">A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <paramref name="instanceName" /> parameter.</param>
		// Token: 0x06002ED0 RID: 11984 RVA: 0x000D2709 File Offset: 0x000D0909
		public InstanceData(string instanceName, CounterSample sample)
		{
			this.instanceName = instanceName;
			this.sample = sample;
		}

		/// <summary>Gets the instance name associated with this instance data.</summary>
		/// <returns>The name of an instance associated with the performance counter.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000D271F File Offset: 0x000D091F
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		/// <summary>Gets the performance counter sample that generated this data.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <see cref="P:System.Diagnostics.InstanceData.InstanceName" /> property.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000D2727 File Offset: 0x000D0927
		public CounterSample Sample
		{
			get
			{
				return this.sample;
			}
		}

		/// <summary>Gets the raw data value associated with the performance counter sample.</summary>
		/// <returns>The raw value read by the performance counter sample associated with the <see cref="P:System.Diagnostics.InstanceData.Sample" /> property.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000D272F File Offset: 0x000D092F
		public long RawValue
		{
			get
			{
				return this.sample.RawValue;
			}
		}

		// Token: 0x04002789 RID: 10121
		private string instanceName;

		// Token: 0x0400278A RID: 10122
		private CounterSample sample;
	}
}
