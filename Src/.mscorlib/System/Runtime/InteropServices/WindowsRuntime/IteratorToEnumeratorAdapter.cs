using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EE RID: 2542
	internal sealed class IteratorToEnumeratorAdapter<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x060064CF RID: 25807 RVA: 0x001588D4 File Offset: 0x00156AD4
		internal IteratorToEnumeratorAdapter(IIterator<T> iterator)
		{
			this.m_iterator = iterator;
			this.m_hadCurrent = true;
			this.m_isInitialized = false;
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060064D0 RID: 25808 RVA: 0x001588F1 File Offset: 0x00156AF1
		public T Current
		{
			get
			{
				if (!this.m_isInitialized)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
				}
				if (!this.m_hadCurrent)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
				}
				return this.m_current;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x00158917 File Offset: 0x00156B17
		object IEnumerator.Current
		{
			get
			{
				if (!this.m_isInitialized)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
				}
				if (!this.m_hadCurrent)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
				}
				return this.m_current;
			}
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x00158944 File Offset: 0x00156B44
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			if (!this.m_hadCurrent)
			{
				return false;
			}
			try
			{
				if (!this.m_isInitialized)
				{
					this.m_hadCurrent = this.m_iterator.HasCurrent;
					this.m_isInitialized = true;
				}
				else
				{
					this.m_hadCurrent = this.m_iterator.MoveNext();
				}
				if (this.m_hadCurrent)
				{
					this.m_current = this.m_iterator.Current;
				}
			}
			catch (Exception ex)
			{
				if (Marshal.GetHRForException(ex) != -2147483636)
				{
					throw;
				}
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
			}
			return this.m_hadCurrent;
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x001589DC File Offset: 0x00156BDC
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x001589E3 File Offset: 0x00156BE3
		public void Dispose()
		{
		}

		// Token: 0x04002D00 RID: 11520
		private IIterator<T> m_iterator;

		// Token: 0x04002D01 RID: 11521
		private bool m_hadCurrent;

		// Token: 0x04002D02 RID: 11522
		private T m_current;

		// Token: 0x04002D03 RID: 11523
		private bool m_isInitialized;
	}
}
