using System;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x020001B9 RID: 441
	internal class Cache<TContainer, TProperty>
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		public TProperty GetValue(TContainer container, Func<TContainer, TProperty> compute, Func<TContainer, TProperty> onCycle)
		{
			object obj = onCycle ?? this;
			object obj2 = this.value;
			if (obj2 == CacheHelper.Unknown)
			{
				lock (obj)
				{
					if (this.value == CacheHelper.Unknown)
					{
						this.value = CacheHelper.CycleSentinel;
						TProperty tproperty;
						try
						{
							tproperty = compute(container);
						}
						catch
						{
							this.value = CacheHelper.Unknown;
							throw;
						}
						if (this.value == CacheHelper.CycleSentinel)
						{
							this.value = ((typeof(TProperty) == typeof(bool)) ? CacheHelper.BoxedBool((bool)((object)tproperty)) : tproperty);
						}
					}
					obj2 = this.value;
					goto IL_1B5;
				}
			}
			if (obj2 == CacheHelper.CycleSentinel)
			{
				lock (obj)
				{
					if (this.value == CacheHelper.CycleSentinel)
					{
						this.value = CacheHelper.SecondPassCycleSentinel;
						try
						{
							compute(container);
						}
						catch
						{
							this.value = CacheHelper.CycleSentinel;
							throw;
						}
						if (this.value == CacheHelper.SecondPassCycleSentinel)
						{
							this.value = onCycle(container);
						}
					}
					else if (this.value == CacheHelper.Unknown)
					{
						return this.GetValue(container, compute, onCycle);
					}
					obj2 = this.value;
					goto IL_1B5;
				}
			}
			if (obj2 == CacheHelper.SecondPassCycleSentinel)
			{
				lock (obj)
				{
					if (this.value == CacheHelper.SecondPassCycleSentinel)
					{
						this.value = onCycle(container);
					}
					else if (this.value == CacheHelper.Unknown)
					{
						return this.GetValue(container, compute, onCycle);
					}
					obj2 = this.value;
				}
			}
			IL_1B5:
			return (TProperty)((object)obj2);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		public void Clear(Func<TContainer, TProperty> onCycle)
		{
			lock (onCycle ?? this)
			{
				if (this.value != CacheHelper.CycleSentinel && this.value != CacheHelper.SecondPassCycleSentinel)
				{
					this.value = CacheHelper.Unknown;
				}
			}
		}

		// Token: 0x040004E5 RID: 1253
		private object value = CacheHelper.Unknown;
	}
}
