using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200080E RID: 2062
	internal class DynamicPropertyHolder
	{
		// Token: 0x060058E8 RID: 22760 RVA: 0x0013A68C File Offset: 0x0013888C
		[SecurityCritical]
		internal virtual bool AddDynamicProperty(IDynamicProperty prop)
		{
			bool flag3;
			lock (this)
			{
				DynamicPropertyHolder.CheckPropertyNameClash(prop.Name, this._props, this._numProps);
				bool flag2 = false;
				if (this._props == null || this._numProps == this._props.Length)
				{
					this._props = DynamicPropertyHolder.GrowPropertiesArray(this._props);
					flag2 = true;
				}
				IDynamicProperty[] props = this._props;
				int numProps = this._numProps;
				this._numProps = numProps + 1;
				props[numProps] = prop;
				if (flag2)
				{
					this._sinks = DynamicPropertyHolder.GrowDynamicSinksArray(this._sinks);
				}
				if (this._sinks == null)
				{
					this._sinks = new IDynamicMessageSink[this._props.Length];
					for (int i = 0; i < this._numProps; i++)
					{
						this._sinks[i] = ((IContributeDynamicSink)this._props[i]).GetDynamicSink();
					}
				}
				else
				{
					this._sinks[this._numProps - 1] = ((IContributeDynamicSink)prop).GetDynamicSink();
				}
				flag3 = true;
			}
			return flag3;
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0013A7A0 File Offset: 0x001389A0
		[SecurityCritical]
		internal virtual bool RemoveDynamicProperty(string name)
		{
			lock (this)
			{
				for (int i = 0; i < this._numProps; i++)
				{
					if (this._props[i].Name.Equals(name))
					{
						this._props[i] = this._props[this._numProps - 1];
						this._numProps--;
						this._sinks = null;
						return true;
					}
				}
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), name));
			}
			bool flag2;
			return flag2;
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x0013A848 File Offset: 0x00138A48
		internal virtual IDynamicProperty[] DynamicProperties
		{
			get
			{
				if (this._props == null)
				{
					return null;
				}
				IDynamicProperty[] array2;
				lock (this)
				{
					IDynamicProperty[] array = new IDynamicProperty[this._numProps];
					Array.Copy(this._props, array, this._numProps);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060058EB RID: 22763 RVA: 0x0013A8A8 File Offset: 0x00138AA8
		internal virtual ArrayWithSize DynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._numProps == 0)
				{
					return null;
				}
				lock (this)
				{
					if (this._sinks == null)
					{
						this._sinks = new IDynamicMessageSink[this._numProps + 8];
						for (int i = 0; i < this._numProps; i++)
						{
							this._sinks[i] = ((IContributeDynamicSink)this._props[i]).GetDynamicSink();
						}
					}
				}
				return new ArrayWithSize(this._sinks, this._numProps);
			}
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x0013A940 File Offset: 0x00138B40
		private static IDynamicMessageSink[] GrowDynamicSinksArray(IDynamicMessageSink[] sinks)
		{
			int num = ((sinks != null) ? sinks.Length : 0) + 8;
			IDynamicMessageSink[] array = new IDynamicMessageSink[num];
			if (sinks != null)
			{
				Array.Copy(sinks, array, sinks.Length);
			}
			return array;
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0013A970 File Offset: 0x00138B70
		[SecurityCritical]
		internal static void NotifyDynamicSinks(IMessage msg, ArrayWithSize dynSinks, bool bCliSide, bool bStart, bool bAsync)
		{
			for (int i = 0; i < dynSinks.Count; i++)
			{
				if (bStart)
				{
					dynSinks.Sinks[i].ProcessMessageStart(msg, bCliSide, bAsync);
				}
				else
				{
					dynSinks.Sinks[i].ProcessMessageFinish(msg, bCliSide, bAsync);
				}
			}
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x0013A9B8 File Offset: 0x00138BB8
		[SecurityCritical]
		internal static void CheckPropertyNameClash(string name, IDynamicProperty[] props, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (props[i].Name.Equals(name))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
				}
			}
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0013A9F4 File Offset: 0x00138BF4
		internal static IDynamicProperty[] GrowPropertiesArray(IDynamicProperty[] props)
		{
			int num = ((props != null) ? props.Length : 0) + 8;
			IDynamicProperty[] array = new IDynamicProperty[num];
			if (props != null)
			{
				Array.Copy(props, array, props.Length);
			}
			return array;
		}

		// Token: 0x0400287D RID: 10365
		private const int GROW_BY = 8;

		// Token: 0x0400287E RID: 10366
		private IDynamicProperty[] _props;

		// Token: 0x0400287F RID: 10367
		private int _numProps;

		// Token: 0x04002880 RID: 10368
		private IDynamicMessageSink[] _sinks;
	}
}
