using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Generates IDs for objects.</summary>
	// Token: 0x02000748 RID: 1864
	[ComVisible(true)]
	[Serializable]
	public class ObjectIDGenerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> class.</summary>
		// Token: 0x06005275 RID: 21109 RVA: 0x00122A1C File Offset: 0x00120C1C
		public ObjectIDGenerator()
		{
			this.m_currentCount = 1;
			this.m_currentSize = ObjectIDGenerator.sizes[0];
			this.m_ids = new long[this.m_currentSize * 4];
			this.m_objs = new object[this.m_currentSize * 4];
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00122A6C File Offset: 0x00120C6C
		private int FindElement(object obj, out bool found)
		{
			int num = RuntimeHelpers.GetHashCode(obj);
			int num2 = 1 + (num & int.MaxValue) % (this.m_currentSize - 2);
			int i;
			for (;;)
			{
				int num3 = (num & int.MaxValue) % this.m_currentSize * 4;
				for (i = num3; i < num3 + 4; i++)
				{
					if (this.m_objs[i] == null)
					{
						goto Block_1;
					}
					if (this.m_objs[i] == obj)
					{
						goto Block_2;
					}
				}
				num += num2;
			}
			Block_1:
			found = false;
			return i;
			Block_2:
			found = true;
			return i;
		}

		/// <summary>Returns the ID for the specified object, generating a new ID if the specified object has not already been identified by the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />.</summary>
		/// <param name="obj">The object you want an ID for.</param>
		/// <param name="firstTime">
		///   <see langword="true" /> if <paramref name="obj" /> was not previously known to the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; otherwise, <see langword="false" />.</param>
		/// <returns>The object's ID is used for serialization. <paramref name="firstTime" /> is set to <see langword="true" /> if this is the first time the object has been identified; otherwise, it is set to <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> has been asked to keep track of too many objects.</exception>
		// Token: 0x06005277 RID: 21111 RVA: 0x00122AD8 File Offset: 0x00120CD8
		public virtual long GetId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			bool flag;
			int num = this.FindElement(obj, out flag);
			long num3;
			if (!flag)
			{
				this.m_objs[num] = obj;
				long[] ids = this.m_ids;
				int num2 = num;
				int currentCount = this.m_currentCount;
				this.m_currentCount = currentCount + 1;
				ids[num2] = (long)currentCount;
				num3 = this.m_ids[num];
				if (this.m_currentCount > this.m_currentSize * 4 / 2)
				{
					this.Rehash();
				}
			}
			else
			{
				num3 = this.m_ids[num];
			}
			firstTime = !flag;
			return num3;
		}

		/// <summary>Determines whether an object has already been assigned an ID.</summary>
		/// <param name="obj">The object you are asking for.</param>
		/// <param name="firstTime">
		///   <see langword="true" /> if <paramref name="obj" /> was not previously known to the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; otherwise, <see langword="false" />.</param>
		/// <returns>The object ID of <paramref name="obj" /> if previously known to the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; otherwise, zero.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005278 RID: 21112 RVA: 0x00122B60 File Offset: 0x00120D60
		public virtual long HasId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			bool flag;
			int num = this.FindElement(obj, out flag);
			if (flag)
			{
				firstTime = false;
				return this.m_ids[num];
			}
			firstTime = true;
			return 0L;
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x00122BA4 File Offset: 0x00120DA4
		private void Rehash()
		{
			int[] array = (AppContextSwitches.UseNewMaxArraySize ? ObjectIDGenerator.sizesWithMaxArraySwitch : ObjectIDGenerator.sizes);
			int num = 0;
			int currentSize = this.m_currentSize;
			while (num < array.Length && array[num] <= currentSize)
			{
				num++;
			}
			if (num == array.Length)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
			}
			this.m_currentSize = array[num];
			long[] array2 = new long[this.m_currentSize * 4];
			object[] array3 = new object[this.m_currentSize * 4];
			long[] ids = this.m_ids;
			object[] objs = this.m_objs;
			this.m_ids = array2;
			this.m_objs = array3;
			for (int i = 0; i < objs.Length; i++)
			{
				if (objs[i] != null)
				{
					bool flag;
					int num2 = this.FindElement(objs[i], out flag);
					this.m_objs[num2] = objs[i];
					this.m_ids[num2] = ids[i];
				}
			}
		}

		// Token: 0x0400247B RID: 9339
		private const int numbins = 4;

		// Token: 0x0400247C RID: 9340
		internal int m_currentCount;

		// Token: 0x0400247D RID: 9341
		internal int m_currentSize;

		// Token: 0x0400247E RID: 9342
		internal long[] m_ids;

		// Token: 0x0400247F RID: 9343
		internal object[] m_objs;

		// Token: 0x04002480 RID: 9344
		private static readonly int[] sizes = new int[]
		{
			5, 11, 29, 47, 97, 197, 397, 797, 1597, 3203,
			6421, 12853, 25717, 51437, 102877, 205759, 411527, 823117, 1646237, 3292489,
			6584983
		};

		// Token: 0x04002481 RID: 9345
		private static readonly int[] sizesWithMaxArraySwitch = new int[]
		{
			5, 11, 29, 47, 97, 197, 397, 797, 1597, 3203,
			6421, 12853, 25717, 51437, 102877, 205759, 411527, 823117, 1646237, 3292489,
			6584983, 13169977, 26339969, 52679969, 105359939, 210719881, 421439783
		};
	}
}
