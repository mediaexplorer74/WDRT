using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000792 RID: 1938
	internal sealed class ObjectProgress
	{
		// Token: 0x0600544A RID: 21578 RVA: 0x0012A1B8 File Offset: 0x001283B8
		internal ObjectProgress()
		{
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0012A1D4 File Offset: 0x001283D4
		[Conditional("SER_LOGGING")]
		private void Counter()
		{
			lock (this)
			{
				this.opRecordId = ObjectProgress.opRecordIdCount++;
				if (ObjectProgress.opRecordIdCount > 1000)
				{
					ObjectProgress.opRecordIdCount = 1;
				}
			}
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0012A230 File Offset: 0x00128430
		internal void Init()
		{
			this.isInitial = false;
			this.count = 0;
			this.expectedType = BinaryTypeEnum.ObjectUrt;
			this.expectedTypeInformation = null;
			this.name = null;
			this.objectTypeEnum = InternalObjectTypeE.Empty;
			this.memberTypeEnum = InternalMemberTypeE.Empty;
			this.memberValueEnum = InternalMemberValueE.Empty;
			this.dtType = null;
			this.numItems = 0;
			this.nullCount = 0;
			this.typeInformation = null;
			this.memberLength = 0;
			this.binaryTypeEnumA = null;
			this.typeInformationA = null;
			this.memberNames = null;
			this.memberTypes = null;
			this.pr.Init();
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0012A2BF File Offset: 0x001284BF
		internal void ArrayCountIncrement(int value)
		{
			this.count += value;
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0012A2D0 File Offset: 0x001284D0
		internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
		{
			outBinaryTypeEnum = BinaryTypeEnum.Primitive;
			outTypeInformation = null;
			if (this.objectTypeEnum == InternalObjectTypeE.Array)
			{
				if (this.count == this.numItems)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnum;
				outTypeInformation = this.typeInformation;
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.count++;
				return true;
			}
			else
			{
				if (this.count == this.memberLength && !this.isInitial)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
				outTypeInformation = this.typeInformationA[this.count];
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.name = this.memberNames[this.count];
				Type[] array = this.memberTypes;
				this.dtType = this.memberTypes[this.count];
				this.count++;
				return true;
			}
		}

		// Token: 0x04002616 RID: 9750
		internal static int opRecordIdCount = 1;

		// Token: 0x04002617 RID: 9751
		internal int opRecordId;

		// Token: 0x04002618 RID: 9752
		internal bool isInitial;

		// Token: 0x04002619 RID: 9753
		internal int count;

		// Token: 0x0400261A RID: 9754
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x0400261B RID: 9755
		internal object expectedTypeInformation;

		// Token: 0x0400261C RID: 9756
		internal string name;

		// Token: 0x0400261D RID: 9757
		internal InternalObjectTypeE objectTypeEnum;

		// Token: 0x0400261E RID: 9758
		internal InternalMemberTypeE memberTypeEnum;

		// Token: 0x0400261F RID: 9759
		internal InternalMemberValueE memberValueEnum;

		// Token: 0x04002620 RID: 9760
		internal Type dtType;

		// Token: 0x04002621 RID: 9761
		internal int numItems;

		// Token: 0x04002622 RID: 9762
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04002623 RID: 9763
		internal object typeInformation;

		// Token: 0x04002624 RID: 9764
		internal int nullCount;

		// Token: 0x04002625 RID: 9765
		internal int memberLength;

		// Token: 0x04002626 RID: 9766
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x04002627 RID: 9767
		internal object[] typeInformationA;

		// Token: 0x04002628 RID: 9768
		internal string[] memberNames;

		// Token: 0x04002629 RID: 9769
		internal Type[] memberTypes;

		// Token: 0x0400262A RID: 9770
		internal ParseRecord pr = new ParseRecord();
	}
}
