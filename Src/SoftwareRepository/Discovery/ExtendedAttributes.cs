using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SoftwareRepository.Discovery
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class ExtendedAttributes : ISerializable
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000483A File Offset: 0x00002A3A
		public ExtendedAttributes()
		{
			this.Dictionary = new Dictionary<string, string>();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004850 File Offset: 0x00002A50
		protected ExtendedAttributes(SerializationInfo info, StreamingContext context)
		{
			bool flag = info != null;
			if (flag)
			{
				this.Dictionary = new Dictionary<string, string>();
				foreach (SerializationEntry serializationEntry in info)
				{
					this.Dictionary.Add(serializationEntry.Name, (string)serializationEntry.Value);
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000048B4 File Offset: 0x00002AB4
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			bool flag = info != null;
			if (flag)
			{
				foreach (string text in this.Dictionary.Keys)
				{
					info.AddValue(text, this.Dictionary[text]);
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004928 File Offset: 0x00002B28
		public void Add(string key, string value)
		{
			this.Add(key, value, MatchingCondition.Equal);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004938 File Offset: 0x00002B38
		public void Add(string key, string value, MatchingCondition condition)
		{
			bool flag = !string.IsNullOrEmpty(key);
			if (flag)
			{
				bool flag2 = this.Dictionary == null;
				if (flag2)
				{
					this.Dictionary = new Dictionary<string, string>();
				}
				string text = value;
				bool flag3 = MatchingCondition.Contains == condition;
				if (flag3)
				{
					text = "$CONTAINS " + text;
				}
				this.Dictionary.Add(key, text);
			}
		}

		// Token: 0x040000A9 RID: 169
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public Dictionary<string, string> Dictionary;

		// Token: 0x040000AA RID: 170
		private const string CONTAINS_PREFIX = "$CONTAINS ";
	}
}
