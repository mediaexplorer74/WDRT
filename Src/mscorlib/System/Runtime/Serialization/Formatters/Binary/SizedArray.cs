using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000795 RID: 1941
	[Serializable]
	internal sealed class SizedArray : ICloneable
	{
		// Token: 0x0600545D RID: 21597 RVA: 0x0012A663 File Offset: 0x00128863
		internal SizedArray()
		{
			this.objects = new object[16];
			this.negObjects = new object[4];
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0012A684 File Offset: 0x00128884
		internal SizedArray(int length)
		{
			this.objects = new object[length];
			this.negObjects = new object[length];
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x0012A6A4 File Offset: 0x001288A4
		private SizedArray(SizedArray sizedArray)
		{
			this.objects = new object[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new object[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x0012A701 File Offset: 0x00128901
		public object Clone()
		{
			return new SizedArray(this);
		}

		// Token: 0x17000DD8 RID: 3544
		internal object this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return null;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return null;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				object obj = this.objects[index];
				this.objects[index] = value;
			}
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0012A798 File Offset: 0x00128998
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int num = Math.Max(this.negObjects.Length * 2, -index + 1);
					object[] array = new object[num];
					Array.Copy(this.negObjects, 0, array, 0, this.negObjects.Length);
					this.negObjects = array;
				}
				else
				{
					int num2 = Math.Max(this.objects.Length * 2, index + 1);
					object[] array2 = new object[num2];
					Array.Copy(this.objects, 0, array2, 0, this.objects.Length);
					this.objects = array2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
			}
		}

		// Token: 0x0400265A RID: 9818
		internal object[] objects;

		// Token: 0x0400265B RID: 9819
		internal object[] negObjects;
	}
}
