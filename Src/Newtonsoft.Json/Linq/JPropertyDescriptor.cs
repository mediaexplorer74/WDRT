using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BF RID: 191
	[NullableContext(1)]
	[Nullable(0)]
	public class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x0002A6A7 File Offset: 0x000288A7
		public JPropertyDescriptor(string name)
			: base(name, null)
		{
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002A6B1 File Offset: 0x000288B1
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002A6B9 File Offset: 0x000288B9
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002A6BC File Offset: 0x000288BC
		[return: Nullable(2)]
		public override object GetValue(object component)
		{
			JObject jobject = component as JObject;
			if (jobject == null)
			{
				return null;
			}
			return jobject[this.Name];
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002A6D5 File Offset: 0x000288D5
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002A6D8 File Offset: 0x000288D8
		public override void SetValue(object component, object value)
		{
			JObject jobject = component as JObject;
			if (jobject != null)
			{
				JToken jtoken = (value as JToken) ?? new JValue(value);
				jobject[this.Name] = jtoken;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002A70D File Offset: 0x0002890D
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002A710 File Offset: 0x00028910
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002A71C File Offset: 0x0002891C
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002A71F File Offset: 0x0002891F
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002A72B File Offset: 0x0002892B
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
