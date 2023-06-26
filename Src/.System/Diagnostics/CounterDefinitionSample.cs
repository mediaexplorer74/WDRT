using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004E6 RID: 1254
	internal class CounterDefinitionSample
	{
		// Token: 0x06002F64 RID: 12132 RVA: 0x000D6430 File Offset: 0x000D4630
		internal CounterDefinitionSample(Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION perfCounter, CategorySample categorySample, int instanceNumber)
		{
			this.NameIndex = perfCounter.CounterNameTitleIndex;
			this.CounterType = perfCounter.CounterType;
			this.offset = perfCounter.CounterOffset;
			this.size = perfCounter.CounterSize;
			if (instanceNumber == -1)
			{
				this.instanceValues = new long[1];
			}
			else
			{
				this.instanceValues = new long[instanceNumber];
			}
			this.categorySample = categorySample;
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000D6498 File Offset: 0x000D4698
		private long ReadValue(IntPtr pointer)
		{
			if (this.size == 4)
			{
				return (long)((ulong)Marshal.ReadInt32((IntPtr)((long)pointer + (long)this.offset)));
			}
			if (this.size == 8)
			{
				return Marshal.ReadInt64((IntPtr)((long)pointer + (long)this.offset));
			}
			return -1L;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000D64EC File Offset: 0x000D46EC
		internal CounterSample GetInstanceValue(string instanceName)
		{
			if (!this.categorySample.InstanceNameTable.ContainsKey(instanceName))
			{
				if (instanceName.Length > 127)
				{
					instanceName = instanceName.Substring(0, 127);
				}
				if (!this.categorySample.InstanceNameTable.ContainsKey(instanceName))
				{
					throw new InvalidOperationException(SR.GetString("CantReadInstance", new object[] { instanceName }));
				}
			}
			int num = (int)this.categorySample.InstanceNameTable[instanceName];
			long num2 = this.instanceValues[num];
			long num3 = 0L;
			if (this.BaseCounterDefinitionSample != null)
			{
				CategorySample categorySample = this.BaseCounterDefinitionSample.categorySample;
				int num4 = (int)categorySample.InstanceNameTable[instanceName];
				num3 = this.BaseCounterDefinitionSample.instanceValues[num4];
			}
			return new CounterSample(num2, num3, this.categorySample.CounterFrequency, this.categorySample.SystemFrequency, this.categorySample.TimeStamp, this.categorySample.TimeStamp100nSec, (PerformanceCounterType)this.CounterType, this.categorySample.CounterTimeStamp);
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000D65EC File Offset: 0x000D47EC
		internal InstanceDataCollection ReadInstanceData(string counterName)
		{
			InstanceDataCollection instanceDataCollection = new InstanceDataCollection(counterName);
			string[] array = new string[this.categorySample.InstanceNameTable.Count];
			this.categorySample.InstanceNameTable.Keys.CopyTo(array, 0);
			int[] array2 = new int[this.categorySample.InstanceNameTable.Count];
			this.categorySample.InstanceNameTable.Values.CopyTo(array2, 0);
			for (int i = 0; i < array.Length; i++)
			{
				long num = 0L;
				if (this.BaseCounterDefinitionSample != null)
				{
					CategorySample categorySample = this.BaseCounterDefinitionSample.categorySample;
					int num2 = (int)categorySample.InstanceNameTable[array[i]];
					num = this.BaseCounterDefinitionSample.instanceValues[num2];
				}
				CounterSample counterSample = new CounterSample(this.instanceValues[array2[i]], num, this.categorySample.CounterFrequency, this.categorySample.SystemFrequency, this.categorySample.TimeStamp, this.categorySample.TimeStamp100nSec, (PerformanceCounterType)this.CounterType, this.categorySample.CounterTimeStamp);
				instanceDataCollection.Add(array[i], new InstanceData(array[i], counterSample));
			}
			return instanceDataCollection;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000D6714 File Offset: 0x000D4914
		internal CounterSample GetSingleValue()
		{
			long num = this.instanceValues[0];
			long num2 = 0L;
			if (this.BaseCounterDefinitionSample != null)
			{
				num2 = this.BaseCounterDefinitionSample.instanceValues[0];
			}
			return new CounterSample(num, num2, this.categorySample.CounterFrequency, this.categorySample.SystemFrequency, this.categorySample.TimeStamp, this.categorySample.TimeStamp100nSec, (PerformanceCounterType)this.CounterType, this.categorySample.CounterTimeStamp);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000D6788 File Offset: 0x000D4988
		internal void SetInstanceValue(int index, IntPtr dataRef)
		{
			long num = this.ReadValue(dataRef);
			this.instanceValues[index] = num;
		}

		// Token: 0x040027E5 RID: 10213
		internal readonly int NameIndex;

		// Token: 0x040027E6 RID: 10214
		internal readonly int CounterType;

		// Token: 0x040027E7 RID: 10215
		internal CounterDefinitionSample BaseCounterDefinitionSample;

		// Token: 0x040027E8 RID: 10216
		private readonly int size;

		// Token: 0x040027E9 RID: 10217
		private readonly int offset;

		// Token: 0x040027EA RID: 10218
		private long[] instanceValues;

		// Token: 0x040027EB RID: 10219
		private CategorySample categorySample;
	}
}
