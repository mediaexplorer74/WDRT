using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200063F RID: 1599
	internal sealed class ScopeTree
	{
		// Token: 0x06004B0D RID: 19213 RVA: 0x00110F52 File Offset: 0x0010F152
		internal ScopeTree()
		{
			this.m_iOpenScopeCount = 0;
			this.m_iCount = 0;
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00110F68 File Offset: 0x0010F168
		internal int GetCurrentActiveScopeIndex()
		{
			int num = 0;
			int num2 = this.m_iCount - 1;
			if (this.m_iCount == 0)
			{
				return -1;
			}
			while (num > 0 || this.m_ScopeActions[num2] == ScopeAction.Close)
			{
				if (this.m_ScopeActions[num2] == ScopeAction.Open)
				{
					num--;
				}
				else
				{
					num++;
				}
				num2--;
			}
			return num2;
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x00110FB4 File Offset: 0x0010F1B4
		internal void AddLocalSymInfoToCurrentScope(string strName, byte[] signature, int slot, int startOffset, int endOffset)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddLocalSymInfo(strName, signature, slot, startOffset, endOffset);
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x00110FF4 File Offset: 0x0010F1F4
		internal void AddUsingNamespaceToCurrentScope(string strNamespace)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddUsingNamespace(strNamespace);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00111030 File Offset: 0x0010F230
		internal void AddScopeInfo(ScopeAction sa, int iOffset)
		{
			if (sa == ScopeAction.Close && this.m_iOpenScopeCount <= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchingSymScope"));
			}
			this.EnsureCapacity();
			this.m_ScopeActions[this.m_iCount] = sa;
			this.m_iOffsets[this.m_iCount] = iOffset;
			this.m_localSymInfos[this.m_iCount] = null;
			checked
			{
				this.m_iCount++;
			}
			if (sa == ScopeAction.Open)
			{
				this.m_iOpenScopeCount++;
				return;
			}
			this.m_iOpenScopeCount--;
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x001110B8 File Offset: 0x0010F2B8
		internal void EnsureCapacity()
		{
			if (this.m_iCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_ScopeActions = new ScopeAction[16];
				this.m_localSymInfos = new LocalSymInfo[16];
				return;
			}
			if (this.m_iCount == this.m_iOffsets.Length)
			{
				int num = checked(this.m_iCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iOffsets, array, this.m_iCount);
				this.m_iOffsets = array;
				ScopeAction[] array2 = new ScopeAction[num];
				Array.Copy(this.m_ScopeActions, array2, this.m_iCount);
				this.m_ScopeActions = array2;
				LocalSymInfo[] array3 = new LocalSymInfo[num];
				Array.Copy(this.m_localSymInfos, array3, this.m_iCount);
				this.m_localSymInfos = array3;
			}
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x00111170 File Offset: 0x0010F370
		internal void EmitScopeTree(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_iCount; i++)
			{
				if (this.m_ScopeActions[i] == ScopeAction.Open)
				{
					symWriter.OpenScope(this.m_iOffsets[i]);
				}
				else
				{
					symWriter.CloseScope(this.m_iOffsets[i]);
				}
				if (this.m_localSymInfos[i] != null)
				{
					this.m_localSymInfos[i].EmitLocalSymInfo(symWriter);
				}
			}
		}

		// Token: 0x04001EFB RID: 7931
		internal int[] m_iOffsets;

		// Token: 0x04001EFC RID: 7932
		internal ScopeAction[] m_ScopeActions;

		// Token: 0x04001EFD RID: 7933
		internal int m_iCount;

		// Token: 0x04001EFE RID: 7934
		internal int m_iOpenScopeCount;

		// Token: 0x04001EFF RID: 7935
		internal const int InitialSize = 16;

		// Token: 0x04001F00 RID: 7936
		internal LocalSymInfo[] m_localSymInfos;
	}
}
