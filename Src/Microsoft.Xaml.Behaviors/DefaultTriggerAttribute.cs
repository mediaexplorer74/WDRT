using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200000B RID: 11
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	public sealed class DefaultTriggerAttribute : Attribute
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A6E File Offset: 0x00000C6E
		public Type TargetType
		{
			get
			{
				return this.targetType;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002A76 File Offset: 0x00000C76
		public Type TriggerType
		{
			get
			{
				return this.triggerType;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A7E File Offset: 0x00000C7E
		public IEnumerable Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A86 File Offset: 0x00000C86
		public DefaultTriggerAttribute(Type targetType, Type triggerType, object parameter)
			: this(targetType, triggerType, new object[] { parameter })
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A9C File Offset: 0x00000C9C
		public DefaultTriggerAttribute(Type targetType, Type triggerType, params object[] parameters)
		{
			if (!typeof(TriggerBase).IsAssignableFrom(triggerType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.DefaultTriggerAttributeInvalidTriggerTypeSpecifiedExceptionMessage, new object[] { triggerType.Name }));
			}
			this.targetType = targetType;
			this.triggerType = triggerType;
			this.parameters = parameters;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002AFC File Offset: 0x00000CFC
		public TriggerBase Instantiate()
		{
			object obj = null;
			try
			{
				obj = Activator.CreateInstance(this.TriggerType, this.parameters);
			}
			catch
			{
			}
			return (TriggerBase)obj;
		}

		// Token: 0x0400001B RID: 27
		private Type targetType;

		// Token: 0x0400001C RID: 28
		private Type triggerType;

		// Token: 0x0400001D RID: 29
		private object[] parameters;
	}
}
