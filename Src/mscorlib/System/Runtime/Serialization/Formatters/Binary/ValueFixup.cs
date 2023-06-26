using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000798 RID: 1944
	internal sealed class ValueFixup
	{
		// Token: 0x0600546E RID: 21614 RVA: 0x0012AA61 File Offset: 0x00128C61
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x0012AA7E File Offset: 0x00128C7E
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x0012AAA4 File Offset: 0x00128CA4
		[SecurityCritical]
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_HeaderReflection", new object[] { member.Length }));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04002660 RID: 9824
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x04002661 RID: 9825
		internal Array arrayObj;

		// Token: 0x04002662 RID: 9826
		internal int[] indexMap;

		// Token: 0x04002663 RID: 9827
		internal object header;

		// Token: 0x04002664 RID: 9828
		internal object memberObject;

		// Token: 0x04002665 RID: 9829
		internal static volatile MemberInfo valueInfo;

		// Token: 0x04002666 RID: 9830
		internal ReadObjectInfo objectInfo;

		// Token: 0x04002667 RID: 9831
		internal string memberName;
	}
}
