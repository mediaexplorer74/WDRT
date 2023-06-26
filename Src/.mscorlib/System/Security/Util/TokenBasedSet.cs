using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Util
{
	// Token: 0x0200037C RID: 892
	[Serializable]
	internal class TokenBasedSet
	{
		// Token: 0x06002C7B RID: 11387 RVA: 0x000A709C File Offset: 0x000A529C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserializedInternal();
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000A70A4 File Offset: 0x000A52A4
		private void OnDeserializedInternal()
		{
			if (this.m_objSet != null)
			{
				if (this.m_cElt == 1)
				{
					this.m_Obj = this.m_objSet[this.m_maxIndex];
				}
				else
				{
					this.m_Set = this.m_objSet;
				}
				this.m_objSet = null;
			}
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000A70F0 File Offset: 0x000A52F0
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				if (this.m_cElt == 1)
				{
					this.m_objSet = new object[this.m_maxIndex + 1];
					this.m_objSet[this.m_maxIndex] = this.m_Obj;
					return;
				}
				if (this.m_cElt > 0)
				{
					this.m_objSet = this.m_Set;
				}
			}
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000A7159 File Offset: 0x000A5359
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_objSet = null;
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000A7174 File Offset: 0x000A5374
		internal bool MoveNext(ref TokenBasedSetEnumerator e)
		{
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				return false;
			}
			if (cElt != 1)
			{
				do
				{
					int num = e.Index + 1;
					e.Index = num;
					if (num > this.m_maxIndex)
					{
						goto Block_5;
					}
					e.Current = Volatile.Read<object>(ref this.m_Set[e.Index]);
				}
				while (e.Current == null);
				return true;
				Block_5:
				e.Current = null;
				return false;
			}
			if (e.Index == -1)
			{
				e.Index = this.m_maxIndex;
				e.Current = this.m_Obj;
				return true;
			}
			e.Index = (int)((short)(this.m_maxIndex + 1));
			e.Current = null;
			return false;
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000A721C File Offset: 0x000A541C
		internal TokenBasedSet()
		{
			this.Reset();
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000A723C File Offset: 0x000A543C
		internal TokenBasedSet(TokenBasedSet tbSet)
		{
			if (tbSet == null)
			{
				this.Reset();
				return;
			}
			if (tbSet.m_cElt > 1)
			{
				object[] set = tbSet.m_Set;
				int num = set.Length;
				object[] array = new object[num];
				Array.Copy(set, 0, array, 0, num);
				this.m_Set = array;
			}
			else
			{
				this.m_Obj = tbSet.m_Obj;
			}
			this.m_cElt = tbSet.m_cElt;
			this.m_maxIndex = tbSet.m_maxIndex;
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000A72C6 File Offset: 0x000A54C6
		internal void Reset()
		{
			this.m_Obj = null;
			this.m_Set = null;
			this.m_cElt = 0;
			this.m_maxIndex = -1;
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000A72EC File Offset: 0x000A54EC
		internal void SetItem(int index, object item)
		{
			if (item == null)
			{
				this.RemoveItem(index);
				return;
			}
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				this.m_cElt = 1;
				this.m_maxIndex = (int)((short)index);
				this.m_Obj = item;
				return;
			}
			if (cElt != 1)
			{
				object[] array = this.m_Set;
				if (index >= array.Length)
				{
					object[] array2 = new object[index + 1];
					Array.Copy(array, 0, array2, 0, this.m_maxIndex + 1);
					this.m_maxIndex = (int)((short)index);
					array2[index] = item;
					this.m_Set = array2;
					this.m_cElt++;
					return;
				}
				if (array[index] == null)
				{
					this.m_cElt++;
				}
				array[index] = item;
				if (index > this.m_maxIndex)
				{
					this.m_maxIndex = (int)((short)index);
				}
				return;
			}
			else
			{
				if (index == this.m_maxIndex)
				{
					this.m_Obj = item;
					return;
				}
				object obj = this.m_Obj;
				int num = Math.Max(this.m_maxIndex, index);
				object[] array = new object[num + 1];
				array[this.m_maxIndex] = obj;
				array[index] = item;
				this.m_maxIndex = (int)((short)num);
				this.m_cElt = 2;
				this.m_Set = array;
				this.m_Obj = null;
				return;
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000A7420 File Offset: 0x000A5620
		internal object GetItem(int index)
		{
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				return null;
			}
			if (cElt != 1)
			{
				if (index < this.m_Set.Length)
				{
					return Volatile.Read<object>(ref this.m_Set[index]);
				}
				return null;
			}
			else
			{
				if (index == this.m_maxIndex)
				{
					return this.m_Obj;
				}
				return null;
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000A7478 File Offset: 0x000A5678
		internal object RemoveItem(int index)
		{
			object obj = null;
			int cElt = this.m_cElt;
			if (cElt != 0)
			{
				if (cElt != 1)
				{
					if (index < this.m_Set.Length && (obj = Volatile.Read<object>(ref this.m_Set[index])) != null)
					{
						Volatile.Write<object>(ref this.m_Set[index], null);
						this.m_cElt--;
						if (index == this.m_maxIndex)
						{
							this.ResetMaxIndex(this.m_Set);
						}
						if (this.m_cElt == 1)
						{
							this.m_Obj = Volatile.Read<object>(ref this.m_Set[this.m_maxIndex]);
							this.m_Set = null;
						}
					}
				}
				else if (index != this.m_maxIndex)
				{
					obj = null;
				}
				else
				{
					obj = this.m_Obj;
					this.Reset();
				}
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000A755C File Offset: 0x000A575C
		private void ResetMaxIndex(object[] aObj)
		{
			for (int i = aObj.Length - 1; i >= 0; i--)
			{
				if (aObj[i] != null)
				{
					this.m_maxIndex = (int)((short)i);
					return;
				}
			}
			this.m_maxIndex = -1;
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000A7592 File Offset: 0x000A5792
		internal int GetStartingIndex()
		{
			if (this.m_cElt <= 1)
			{
				return this.m_maxIndex;
			}
			return 0;
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000A75A7 File Offset: 0x000A57A7
		internal int GetCount()
		{
			return this.m_cElt;
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000A75AF File Offset: 0x000A57AF
		internal int GetMaxUsedIndex()
		{
			return this.m_maxIndex;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000A75B9 File Offset: 0x000A57B9
		internal bool FastIsEmpty()
		{
			return this.m_cElt == 0;
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000A75C4 File Offset: 0x000A57C4
		internal TokenBasedSet SpecialUnion(TokenBasedSet other)
		{
			this.OnDeserializedInternal();
			TokenBasedSet tokenBasedSet = new TokenBasedSet();
			int num;
			if (other != null)
			{
				other.OnDeserializedInternal();
				num = ((this.GetMaxUsedIndex() > other.GetMaxUsedIndex()) ? this.GetMaxUsedIndex() : other.GetMaxUsedIndex());
			}
			else
			{
				num = this.GetMaxUsedIndex();
			}
			for (int i = 0; i <= num; i++)
			{
				object item = this.GetItem(i);
				IPermission permission = item as IPermission;
				ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
				object obj = ((other != null) ? other.GetItem(i) : null);
				IPermission permission2 = obj as IPermission;
				ISecurityElementFactory securityElementFactory2 = obj as ISecurityElementFactory;
				if (item != null || obj != null)
				{
					if (item == null)
					{
						if (securityElementFactory2 != null)
						{
							permission2 = PermissionSet.CreatePerm(securityElementFactory2, false);
						}
						PermissionToken token = PermissionToken.GetToken(permission2);
						if (token == null)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
						}
						tokenBasedSet.SetItem(token.m_index, permission2);
					}
					else if (obj == null)
					{
						if (securityElementFactory != null)
						{
							permission = PermissionSet.CreatePerm(securityElementFactory, false);
						}
						PermissionToken token2 = PermissionToken.GetToken(permission);
						if (token2 == null)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
						}
						tokenBasedSet.SetItem(token2.m_index, permission);
					}
				}
			}
			return tokenBasedSet;
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000A76DC File Offset: 0x000A58DC
		internal void SpecialSplit(ref TokenBasedSet unrestrictedPermSet, ref TokenBasedSet normalPermSet, bool ignoreTypeLoadFailures)
		{
			int maxUsedIndex = this.GetMaxUsedIndex();
			for (int i = this.GetStartingIndex(); i <= maxUsedIndex; i++)
			{
				object item = this.GetItem(i);
				if (item != null)
				{
					IPermission permission = item as IPermission;
					if (permission == null)
					{
						permission = PermissionSet.CreatePerm(item, ignoreTypeLoadFailures);
					}
					PermissionToken token = PermissionToken.GetToken(permission);
					if (permission != null && token != null)
					{
						if (permission is IUnrestrictedPermission)
						{
							if (unrestrictedPermSet == null)
							{
								unrestrictedPermSet = new TokenBasedSet();
							}
							unrestrictedPermSet.SetItem(token.m_index, permission);
						}
						else
						{
							if (normalPermSet == null)
							{
								normalPermSet = new TokenBasedSet();
							}
							normalPermSet.SetItem(token.m_index, permission);
						}
					}
				}
			}
		}

		// Token: 0x040011D9 RID: 4569
		private int m_initSize = 24;

		// Token: 0x040011DA RID: 4570
		private int m_increment = 8;

		// Token: 0x040011DB RID: 4571
		private object[] m_objSet;

		// Token: 0x040011DC RID: 4572
		[OptionalField(VersionAdded = 2)]
		private volatile object m_Obj;

		// Token: 0x040011DD RID: 4573
		[OptionalField(VersionAdded = 2)]
		private volatile object[] m_Set;

		// Token: 0x040011DE RID: 4574
		private int m_cElt;

		// Token: 0x040011DF RID: 4575
		private volatile int m_maxIndex;
	}
}
