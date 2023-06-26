using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000087 RID: 135
	[Export(typeof(ICommandRepository))]
	[Serializable]
	public class CommandRepository : Dictionary<string, IDelegateCommand>, ICommandRepository, IDictionary<string, IDelegateCommand>, ICollection<KeyValuePair<string, IDelegateCommand>>, IEnumerable<KeyValuePair<string, IDelegateCommand>>, IEnumerable
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x0001720E File Offset: 0x0001540E
		public CommandRepository()
		{
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00017218 File Offset: 0x00015418
		protected CommandRepository(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
