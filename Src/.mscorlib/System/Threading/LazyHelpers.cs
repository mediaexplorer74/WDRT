using System;

namespace System.Threading
{
	// Token: 0x0200053D RID: 1341
	internal static class LazyHelpers<T>
	{
		// Token: 0x06003EFE RID: 16126 RVA: 0x000EB77C File Offset: 0x000E997C
		private static T ActivatorFactorySelector()
		{
			T t;
			try
			{
				t = (T)((object)Activator.CreateInstance(typeof(T)));
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
			}
			return t;
		}

		// Token: 0x04001A7E RID: 6782
		internal static Func<T> s_activatorFactorySelector = new Func<T>(LazyHelpers<T>.ActivatorFactorySelector);
	}
}
