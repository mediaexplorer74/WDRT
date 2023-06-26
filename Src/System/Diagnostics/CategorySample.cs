using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004E5 RID: 1253
	internal class CategorySample
	{
		// Token: 0x06002F60 RID: 12128 RVA: 0x000D5CD0 File Offset: 0x000D3ED0
		internal unsafe CategorySample(byte[] data, CategoryEntry entry, PerformanceCounterLib library)
		{
			this.entry = entry;
			this.library = library;
			int nameIndex = entry.NameIndex;
			Microsoft.Win32.NativeMethods.PERF_DATA_BLOCK perf_DATA_BLOCK = new Microsoft.Win32.NativeMethods.PERF_DATA_BLOCK();
			fixed (byte[] array = data)
			{
				byte* ptr;
				if (data == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				IntPtr intPtr = new IntPtr((void*)ptr);
				Marshal.PtrToStructure(intPtr, perf_DATA_BLOCK);
				this.SystemFrequency = perf_DATA_BLOCK.PerfFreq;
				this.TimeStamp = perf_DATA_BLOCK.PerfTime;
				this.TimeStamp100nSec = perf_DATA_BLOCK.PerfTime100nSec;
				intPtr = (IntPtr)((long)intPtr + (long)perf_DATA_BLOCK.HeaderLength);
				int numObjectTypes = perf_DATA_BLOCK.NumObjectTypes;
				if (numObjectTypes == 0)
				{
					this.CounterTable = new Hashtable();
					this.InstanceNameTable = new Hashtable(StringComparer.OrdinalIgnoreCase);
					return;
				}
				Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE perf_OBJECT_TYPE = null;
				bool flag = false;
				for (int i = 0; i < numObjectTypes; i++)
				{
					perf_OBJECT_TYPE = new Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE();
					Marshal.PtrToStructure(intPtr, perf_OBJECT_TYPE);
					if (perf_OBJECT_TYPE.ObjectNameTitleIndex == nameIndex)
					{
						flag = true;
						break;
					}
					intPtr = (IntPtr)((long)intPtr + (long)perf_OBJECT_TYPE.TotalByteLength);
				}
				if (!flag)
				{
					throw new InvalidOperationException(SR.GetString("CantReadCategoryIndex", new object[] { nameIndex.ToString(CultureInfo.CurrentCulture) }));
				}
				this.CounterFrequency = perf_OBJECT_TYPE.PerfFreq;
				this.CounterTimeStamp = perf_OBJECT_TYPE.PerfTime;
				int numCounters = perf_OBJECT_TYPE.NumCounters;
				int numInstances = perf_OBJECT_TYPE.NumInstances;
				if (numInstances == -1)
				{
					this.IsMultiInstance = false;
				}
				else
				{
					this.IsMultiInstance = true;
				}
				intPtr = (IntPtr)((long)intPtr + (long)perf_OBJECT_TYPE.HeaderLength);
				CounterDefinitionSample[] array2 = new CounterDefinitionSample[numCounters];
				this.CounterTable = new Hashtable(numCounters);
				for (int j = 0; j < array2.Length; j++)
				{
					Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION = new Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION();
					Marshal.PtrToStructure(intPtr, perf_COUNTER_DEFINITION);
					array2[j] = new CounterDefinitionSample(perf_COUNTER_DEFINITION, this, numInstances);
					intPtr = (IntPtr)((long)intPtr + (long)perf_COUNTER_DEFINITION.ByteLength);
					int counterType = array2[j].CounterType;
					if (!PerformanceCounterLib.IsBaseCounter(counterType))
					{
						if (counterType != 1073742336)
						{
							this.CounterTable[array2[j].NameIndex] = array2[j];
						}
					}
					else if (j > 0)
					{
						array2[j - 1].BaseCounterDefinitionSample = array2[j];
					}
				}
				if (!this.IsMultiInstance)
				{
					this.InstanceNameTable = new Hashtable(1, StringComparer.OrdinalIgnoreCase);
					this.InstanceNameTable["systemdiagnosticsperfcounterlibsingleinstance"] = 0;
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k].SetInstanceValue(0, intPtr);
					}
				}
				else
				{
					string[] array3 = null;
					this.InstanceNameTable = new Hashtable(numInstances, StringComparer.OrdinalIgnoreCase);
					for (int l = 0; l < numInstances; l++)
					{
						Microsoft.Win32.NativeMethods.PERF_INSTANCE_DEFINITION perf_INSTANCE_DEFINITION = new Microsoft.Win32.NativeMethods.PERF_INSTANCE_DEFINITION();
						Marshal.PtrToStructure(intPtr, perf_INSTANCE_DEFINITION);
						if (perf_INSTANCE_DEFINITION.ParentObjectTitleIndex > 0 && array3 == null)
						{
							array3 = this.GetInstanceNamesFromIndex(perf_INSTANCE_DEFINITION.ParentObjectTitleIndex);
						}
						string text;
						if (array3 != null && perf_INSTANCE_DEFINITION.ParentObjectInstance >= 0 && perf_INSTANCE_DEFINITION.ParentObjectInstance < array3.Length - 1)
						{
							text = array3[perf_INSTANCE_DEFINITION.ParentObjectInstance] + "/" + Marshal.PtrToStringUni((IntPtr)((long)intPtr + (long)perf_INSTANCE_DEFINITION.NameOffset));
						}
						else
						{
							text = Marshal.PtrToStringUni((IntPtr)((long)intPtr + (long)perf_INSTANCE_DEFINITION.NameOffset));
						}
						string text2 = text;
						int num = 1;
						while (this.InstanceNameTable.ContainsKey(text2))
						{
							text2 = text + "#" + num.ToString(CultureInfo.InvariantCulture);
							num++;
						}
						this.InstanceNameTable[text2] = l;
						intPtr = (IntPtr)((long)intPtr + (long)perf_INSTANCE_DEFINITION.ByteLength);
						for (int m = 0; m < array2.Length; m++)
						{
							array2[m].SetInstanceValue(l, intPtr);
						}
						intPtr = (IntPtr)((long)intPtr + (long)Marshal.ReadInt32(intPtr));
					}
				}
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000D60C8 File Offset: 0x000D42C8
		internal unsafe string[] GetInstanceNamesFromIndex(int categoryIndex)
		{
			byte[] performanceData = this.library.GetPerformanceData(categoryIndex.ToString(CultureInfo.InvariantCulture));
			byte[] array;
			byte* ptr;
			if ((array = performanceData) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			IntPtr intPtr = new IntPtr((void*)ptr);
			Microsoft.Win32.NativeMethods.PERF_DATA_BLOCK perf_DATA_BLOCK = new Microsoft.Win32.NativeMethods.PERF_DATA_BLOCK();
			Marshal.PtrToStructure(intPtr, perf_DATA_BLOCK);
			intPtr = (IntPtr)((long)intPtr + (long)perf_DATA_BLOCK.HeaderLength);
			int numObjectTypes = perf_DATA_BLOCK.NumObjectTypes;
			Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE perf_OBJECT_TYPE = null;
			bool flag = false;
			for (int i = 0; i < numObjectTypes; i++)
			{
				perf_OBJECT_TYPE = new Microsoft.Win32.NativeMethods.PERF_OBJECT_TYPE();
				Marshal.PtrToStructure(intPtr, perf_OBJECT_TYPE);
				if (perf_OBJECT_TYPE.ObjectNameTitleIndex == categoryIndex)
				{
					flag = true;
					break;
				}
				intPtr = (IntPtr)((long)intPtr + (long)perf_OBJECT_TYPE.TotalByteLength);
			}
			if (!flag)
			{
				return new string[0];
			}
			int numCounters = perf_OBJECT_TYPE.NumCounters;
			int numInstances = perf_OBJECT_TYPE.NumInstances;
			intPtr = (IntPtr)((long)intPtr + (long)perf_OBJECT_TYPE.HeaderLength);
			if (numInstances == -1)
			{
				return new string[0];
			}
			CounterDefinitionSample[] array2 = new CounterDefinitionSample[numCounters];
			for (int j = 0; j < array2.Length; j++)
			{
				Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION perf_COUNTER_DEFINITION = new Microsoft.Win32.NativeMethods.PERF_COUNTER_DEFINITION();
				Marshal.PtrToStructure(intPtr, perf_COUNTER_DEFINITION);
				intPtr = (IntPtr)((long)intPtr + (long)perf_COUNTER_DEFINITION.ByteLength);
			}
			string[] array3 = new string[numInstances];
			for (int k = 0; k < numInstances; k++)
			{
				Microsoft.Win32.NativeMethods.PERF_INSTANCE_DEFINITION perf_INSTANCE_DEFINITION = new Microsoft.Win32.NativeMethods.PERF_INSTANCE_DEFINITION();
				Marshal.PtrToStructure(intPtr, perf_INSTANCE_DEFINITION);
				array3[k] = Marshal.PtrToStringUni((IntPtr)((long)intPtr + (long)perf_INSTANCE_DEFINITION.NameOffset));
				intPtr = (IntPtr)((long)intPtr + (long)perf_INSTANCE_DEFINITION.ByteLength);
				intPtr = (IntPtr)((long)intPtr + (long)Marshal.ReadInt32(intPtr));
			}
			return array3;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000D6274 File Offset: 0x000D4474
		internal CounterDefinitionSample GetCounterDefinitionSample(string counter)
		{
			int i = 0;
			while (i < this.entry.CounterIndexes.Length)
			{
				int num = this.entry.CounterIndexes[i];
				string text = (string)this.library.NameTable[num];
				if (text != null && string.Compare(text, counter, StringComparison.OrdinalIgnoreCase) == 0)
				{
					CounterDefinitionSample counterDefinitionSample = (CounterDefinitionSample)this.CounterTable[num];
					if (counterDefinitionSample == null)
					{
						foreach (object obj in this.CounterTable.Values)
						{
							CounterDefinitionSample counterDefinitionSample2 = (CounterDefinitionSample)obj;
							if (counterDefinitionSample2.BaseCounterDefinitionSample != null && counterDefinitionSample2.BaseCounterDefinitionSample.NameIndex == num)
							{
								return counterDefinitionSample2.BaseCounterDefinitionSample;
							}
						}
						throw new InvalidOperationException(SR.GetString("CounterLayout"));
					}
					return counterDefinitionSample;
				}
				else
				{
					i++;
				}
			}
			throw new InvalidOperationException(SR.GetString("CantReadCounter", new object[] { counter }));
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000D639C File Offset: 0x000D459C
		internal InstanceDataCollectionCollection ReadCategory()
		{
			InstanceDataCollectionCollection instanceDataCollectionCollection = new InstanceDataCollectionCollection();
			for (int i = 0; i < this.entry.CounterIndexes.Length; i++)
			{
				int num = this.entry.CounterIndexes[i];
				string text = (string)this.library.NameTable[num];
				if (text != null && text != string.Empty)
				{
					CounterDefinitionSample counterDefinitionSample = (CounterDefinitionSample)this.CounterTable[num];
					if (counterDefinitionSample != null)
					{
						instanceDataCollectionCollection.Add(text, counterDefinitionSample.ReadInstanceData(text));
					}
				}
			}
			return instanceDataCollectionCollection;
		}

		// Token: 0x040027DB RID: 10203
		internal readonly long SystemFrequency;

		// Token: 0x040027DC RID: 10204
		internal readonly long TimeStamp;

		// Token: 0x040027DD RID: 10205
		internal readonly long TimeStamp100nSec;

		// Token: 0x040027DE RID: 10206
		internal readonly long CounterFrequency;

		// Token: 0x040027DF RID: 10207
		internal readonly long CounterTimeStamp;

		// Token: 0x040027E0 RID: 10208
		internal Hashtable CounterTable;

		// Token: 0x040027E1 RID: 10209
		internal Hashtable InstanceNameTable;

		// Token: 0x040027E2 RID: 10210
		internal bool IsMultiInstance;

		// Token: 0x040027E3 RID: 10211
		private CategoryEntry entry;

		// Token: 0x040027E4 RID: 10212
		private PerformanceCounterLib library;
	}
}
