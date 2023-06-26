using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050F RID: 1295
	internal class MultiSelectRootGridEntry : SingleSelectRootGridEntry
	{
		// Token: 0x060054E5 RID: 21733 RVA: 0x00163E93 File Offset: 0x00162093
		internal MultiSelectRootGridEntry(PropertyGridView view, object obj, IServiceProvider baseProvider, IDesignerHost host, PropertyTab tab, PropertySort sortType)
			: base(view, obj, baseProvider, host, tab, sortType)
		{
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x00163EA4 File Offset: 0x001620A4
		internal override bool ForceReadOnly
		{
			get
			{
				if (!this.forceReadOnlyChecked)
				{
					bool flag = false;
					foreach (object obj in ((Array)this.objValue))
					{
						ReadOnlyAttribute readOnlyAttribute = (ReadOnlyAttribute)TypeDescriptor.GetAttributes(obj)[typeof(ReadOnlyAttribute)];
						if ((readOnlyAttribute != null && !readOnlyAttribute.IsDefaultAttribute()) || TypeDescriptor.GetAttributes(obj).Contains(InheritanceAttribute.InheritedReadOnly))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						this.flags |= 1024;
					}
					this.forceReadOnlyChecked = true;
				}
				return base.ForceReadOnly;
			}
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x0015FB48 File Offset: 0x0015DD48
		protected override bool CreateChildren()
		{
			return this.CreateChildren(false);
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x00163F64 File Offset: 0x00162164
		protected override bool CreateChildren(bool diffOldChildren)
		{
			bool flag2;
			try
			{
				object[] array = (object[])this.objValue;
				base.ChildCollection.Clear();
				MultiPropertyDescriptorGridEntry[] mergedProperties = MultiSelectRootGridEntry.PropertyMerger.GetMergedProperties(array, this, this.PropertySort, this.CurrentTab);
				if (mergedProperties != null)
				{
					GridEntryCollection childCollection = base.ChildCollection;
					GridEntry[] array2 = mergedProperties;
					childCollection.AddRange(array2);
				}
				bool flag = this.Children.Count > 0;
				if (!flag)
				{
					this.SetFlag(524288, true);
				}
				base.CategorizePropEntries();
				flag2 = flag;
			}
			catch
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x04003722 RID: 14114
		private static MultiSelectRootGridEntry.PDComparer PropertyComparer = new MultiSelectRootGridEntry.PDComparer();

		// Token: 0x02000891 RID: 2193
		internal static class PropertyMerger
		{
			// Token: 0x06007206 RID: 29190 RVA: 0x001A15F0 File Offset: 0x0019F7F0
			public static MultiPropertyDescriptorGridEntry[] GetMergedProperties(object[] rgobjs, GridEntry parentEntry, PropertySort sort, PropertyTab tab)
			{
				MultiPropertyDescriptorGridEntry[] array = null;
				try
				{
					int num = rgobjs.Length;
					object[] array2 = new object[1];
					if ((sort & PropertySort.Alphabetical) != PropertySort.NoSort)
					{
						ArrayList commonProperties = MultiSelectRootGridEntry.PropertyMerger.GetCommonProperties(rgobjs, true, tab, parentEntry);
						MultiPropertyDescriptorGridEntry[] array3 = new MultiPropertyDescriptorGridEntry[commonProperties.Count];
						for (int i = 0; i < array3.Length; i++)
						{
							array3[i] = new MultiPropertyDescriptorGridEntry(parentEntry.OwnerGrid, parentEntry, rgobjs, (PropertyDescriptor[])commonProperties[i], false);
						}
						array = MultiSelectRootGridEntry.PropertyMerger.SortParenEntries(array3);
					}
					else
					{
						object[] array4 = new object[num - 1];
						Array.Copy(rgobjs, 1, array4, 0, num - 1);
						ArrayList arrayList = MultiSelectRootGridEntry.PropertyMerger.GetCommonProperties(array4, true, tab, parentEntry);
						ArrayList commonProperties2 = MultiSelectRootGridEntry.PropertyMerger.GetCommonProperties(new object[] { rgobjs[0] }, false, tab, parentEntry);
						PropertyDescriptor[] array5 = new PropertyDescriptor[commonProperties2.Count];
						for (int j = 0; j < commonProperties2.Count; j++)
						{
							array5[j] = ((PropertyDescriptor[])commonProperties2[j])[0];
						}
						arrayList = MultiSelectRootGridEntry.PropertyMerger.UnsortedMerge(array5, arrayList);
						MultiPropertyDescriptorGridEntry[] array6 = new MultiPropertyDescriptorGridEntry[arrayList.Count];
						for (int k = 0; k < array6.Length; k++)
						{
							array6[k] = new MultiPropertyDescriptorGridEntry(parentEntry.OwnerGrid, parentEntry, rgobjs, (PropertyDescriptor[])arrayList[k], false);
						}
						array = MultiSelectRootGridEntry.PropertyMerger.SortParenEntries(array6);
					}
				}
				catch
				{
				}
				return array;
			}

			// Token: 0x06007207 RID: 29191 RVA: 0x001A1750 File Offset: 0x0019F950
			private static ArrayList GetCommonProperties(object[] objs, bool presort, PropertyTab tab, GridEntry parentEntry)
			{
				PropertyDescriptorCollection[] array = new PropertyDescriptorCollection[objs.Length];
				Attribute[] array2 = new Attribute[parentEntry.BrowsableAttributes.Count];
				parentEntry.BrowsableAttributes.CopyTo(array2, 0);
				for (int i = 0; i < objs.Length; i++)
				{
					PropertyDescriptorCollection propertyDescriptorCollection = tab.GetProperties(parentEntry, objs[i], array2);
					if (presort)
					{
						propertyDescriptorCollection = propertyDescriptorCollection.Sort(MultiSelectRootGridEntry.PropertyComparer);
					}
					array[i] = propertyDescriptorCollection;
				}
				ArrayList arrayList = new ArrayList();
				PropertyDescriptor[] array3 = new PropertyDescriptor[objs.Length];
				int[] array4 = new int[array.Length];
				for (int j = 0; j < array[0].Count; j++)
				{
					PropertyDescriptor propertyDescriptor = array[0][j];
					bool flag = propertyDescriptor.Attributes[typeof(MergablePropertyAttribute)].IsDefaultAttribute();
					int num = 1;
					while (flag && num < array.Length)
					{
						if (array4[num] >= array[num].Count)
						{
							flag = false;
							break;
						}
						PropertyDescriptor propertyDescriptor2 = array[num][array4[num]];
						if (propertyDescriptor.Equals(propertyDescriptor2))
						{
							array4[num]++;
							if (!propertyDescriptor2.Attributes[typeof(MergablePropertyAttribute)].IsDefaultAttribute())
							{
								flag = false;
								break;
							}
							array3[num] = propertyDescriptor2;
						}
						else
						{
							int num2 = array4[num];
							propertyDescriptor2 = array[num][num2];
							flag = false;
							while (MultiSelectRootGridEntry.PropertyComparer.Compare(propertyDescriptor2, propertyDescriptor) <= 0)
							{
								if (propertyDescriptor.Equals(propertyDescriptor2))
								{
									if (!propertyDescriptor2.Attributes[typeof(MergablePropertyAttribute)].IsDefaultAttribute())
									{
										flag = false;
										num2++;
										break;
									}
									flag = true;
									array3[num] = propertyDescriptor2;
									array4[num] = num2 + 1;
									break;
								}
								else
								{
									num2++;
									if (num2 >= array[num].Count)
									{
										break;
									}
									propertyDescriptor2 = array[num][num2];
								}
							}
							if (!flag)
							{
								array4[num] = num2;
								break;
							}
						}
						num++;
					}
					if (flag)
					{
						array3[0] = propertyDescriptor;
						arrayList.Add(array3.Clone());
					}
				}
				return arrayList;
			}

			// Token: 0x06007208 RID: 29192 RVA: 0x001A195C File Offset: 0x0019FB5C
			private static MultiPropertyDescriptorGridEntry[] SortParenEntries(MultiPropertyDescriptorGridEntry[] entries)
			{
				MultiPropertyDescriptorGridEntry[] array = null;
				int num = 0;
				for (int i = 0; i < entries.Length; i++)
				{
					if (entries[i].ParensAroundName)
					{
						if (array == null)
						{
							array = new MultiPropertyDescriptorGridEntry[entries.Length];
						}
						array[num++] = entries[i];
						entries[i] = null;
					}
				}
				if (num > 0)
				{
					for (int j = 0; j < entries.Length; j++)
					{
						if (entries[j] != null)
						{
							array[num++] = entries[j];
						}
					}
					entries = array;
				}
				return entries;
			}

			// Token: 0x06007209 RID: 29193 RVA: 0x001A19C4 File Offset: 0x0019FBC4
			private static ArrayList UnsortedMerge(PropertyDescriptor[] baseEntries, ArrayList sortedMergedEntries)
			{
				ArrayList arrayList = new ArrayList();
				PropertyDescriptor[] array = new PropertyDescriptor[((PropertyDescriptor[])sortedMergedEntries[0]).Length + 1];
				foreach (PropertyDescriptor propertyDescriptor in baseEntries)
				{
					PropertyDescriptor[] array2 = null;
					string text = propertyDescriptor.Name + " " + propertyDescriptor.PropertyType.FullName;
					int j = sortedMergedEntries.Count;
					int num = j / 2;
					int num2 = 0;
					while (j > 0)
					{
						PropertyDescriptor[] array3 = (PropertyDescriptor[])sortedMergedEntries[num2 + num];
						PropertyDescriptor propertyDescriptor2 = array3[0];
						string text2 = propertyDescriptor2.Name + " " + propertyDescriptor2.PropertyType.FullName;
						int num3 = string.Compare(text, text2, false, CultureInfo.InvariantCulture);
						if (num3 == 0)
						{
							array2 = array3;
							break;
						}
						if (num3 < 0)
						{
							j = num;
						}
						else
						{
							int num4 = num + 1;
							num2 += num4;
							j -= num4;
						}
						num = j / 2;
					}
					if (array2 != null)
					{
						array[0] = propertyDescriptor;
						Array.Copy(array2, 0, array, 1, array2.Length);
						arrayList.Add(array.Clone());
					}
				}
				return arrayList;
			}
		}

		// Token: 0x02000892 RID: 2194
		private class PDComparer : IComparer
		{
			// Token: 0x0600720A RID: 29194 RVA: 0x001A1AD8 File Offset: 0x0019FCD8
			public int Compare(object obj1, object obj2)
			{
				PropertyDescriptor propertyDescriptor = obj1 as PropertyDescriptor;
				PropertyDescriptor propertyDescriptor2 = obj2 as PropertyDescriptor;
				if (propertyDescriptor == null && propertyDescriptor2 == null)
				{
					return 0;
				}
				if (propertyDescriptor == null)
				{
					return -1;
				}
				if (propertyDescriptor2 == null)
				{
					return 1;
				}
				int num = string.Compare(propertyDescriptor.Name, propertyDescriptor2.Name, false, CultureInfo.InvariantCulture);
				if (num == 0)
				{
					num = string.Compare(propertyDescriptor.PropertyType.FullName, propertyDescriptor2.PropertyType.FullName, true, CultureInfo.CurrentCulture);
				}
				return num;
			}
		}
	}
}
