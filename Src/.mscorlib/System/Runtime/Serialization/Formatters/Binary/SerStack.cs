using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000794 RID: 1940
	internal sealed class SerStack
	{
		// Token: 0x06005453 RID: 21587 RVA: 0x0012A4E6 File Offset: 0x001286E6
		internal SerStack()
		{
			this.stackId = "System";
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0012A50C File Offset: 0x0012870C
		internal SerStack(string stackId)
		{
			this.stackId = stackId;
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0012A530 File Offset: 0x00128730
		internal void Push(object obj)
		{
			if (this.top == this.objects.Length - 1)
			{
				this.IncreaseCapacity();
			}
			object[] array = this.objects;
			int num = this.top + 1;
			this.top = num;
			array[num] = obj;
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0012A570 File Offset: 0x00128770
		internal object Pop()
		{
			if (this.top < 0)
			{
				return null;
			}
			object obj = this.objects[this.top];
			object[] array = this.objects;
			int num = this.top;
			this.top = num - 1;
			array[num] = null;
			return obj;
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0012A5B0 File Offset: 0x001287B0
		internal void IncreaseCapacity()
		{
			int num = this.objects.Length * 2;
			object[] array = new object[num];
			Array.Copy(this.objects, 0, array, 0, this.objects.Length);
			this.objects = array;
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0012A5EC File Offset: 0x001287EC
		internal object Peek()
		{
			if (this.top < 0)
			{
				return null;
			}
			return this.objects[this.top];
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x0012A606 File Offset: 0x00128806
		internal object PeekPeek()
		{
			if (this.top < 1)
			{
				return null;
			}
			return this.objects[this.top - 1];
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0012A622 File Offset: 0x00128822
		internal int Count()
		{
			return this.top + 1;
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0012A62C File Offset: 0x0012882C
		internal bool IsEmpty()
		{
			return this.top <= 0;
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0012A63C File Offset: 0x0012883C
		[Conditional("SER_LOGGING")]
		internal void Dump()
		{
			for (int i = 0; i < this.Count(); i++)
			{
				object obj = this.objects[i];
			}
		}

		// Token: 0x04002656 RID: 9814
		internal object[] objects = new object[5];

		// Token: 0x04002657 RID: 9815
		internal string stackId;

		// Token: 0x04002658 RID: 9816
		internal int top = -1;

		// Token: 0x04002659 RID: 9817
		internal int next;
	}
}
