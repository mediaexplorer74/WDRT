using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	public interface IValueProvider
	{
		// Token: 0x06000683 RID: 1667
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x06000684 RID: 1668
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
