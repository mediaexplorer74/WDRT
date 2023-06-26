using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AD RID: 2477
	[SecurityCritical]
	internal class ComEventsSink : NativeMethods.IDispatch, ICustomQueryInterface
	{
		// Token: 0x0600633A RID: 25402 RVA: 0x001536D2 File Offset: 0x001518D2
		internal ComEventsSink(object rcw, Guid iid)
		{
			this._iidSourceItf = iid;
			this.Advise(rcw);
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x001536E8 File Offset: 0x001518E8
		internal static ComEventsSink Find(ComEventsSink sinks, ref Guid iid)
		{
			ComEventsSink comEventsSink = sinks;
			while (comEventsSink != null && comEventsSink._iidSourceItf != iid)
			{
				comEventsSink = comEventsSink._next;
			}
			return comEventsSink;
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x00153717 File Offset: 0x00151917
		internal static ComEventsSink Add(ComEventsSink sinks, ComEventsSink sink)
		{
			sink._next = sinks;
			return sink;
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x00153721 File Offset: 0x00151921
		[SecurityCritical]
		internal static ComEventsSink RemoveAll(ComEventsSink sinks)
		{
			while (sinks != null)
			{
				sinks.Unadvise();
				sinks = sinks._next;
			}
			return null;
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x00153738 File Offset: 0x00151938
		[SecurityCritical]
		internal static ComEventsSink Remove(ComEventsSink sinks, ComEventsSink sink)
		{
			if (sink == sinks)
			{
				sinks = sinks._next;
			}
			else
			{
				ComEventsSink comEventsSink = sinks;
				while (comEventsSink != null && comEventsSink._next != sink)
				{
					comEventsSink = comEventsSink._next;
				}
				if (comEventsSink != null)
				{
					comEventsSink._next = sink._next;
				}
			}
			sink.Unadvise();
			return sinks;
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x00153780 File Offset: 0x00151980
		public ComEventsMethod RemoveMethod(ComEventsMethod method)
		{
			this._methods = ComEventsMethod.Remove(this._methods, method);
			return this._methods;
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0015379A File Offset: 0x0015199A
		public ComEventsMethod FindMethod(int dispid)
		{
			return ComEventsMethod.Find(this._methods, dispid);
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x001537A8 File Offset: 0x001519A8
		public ComEventsMethod AddMethod(int dispid)
		{
			ComEventsMethod comEventsMethod = new ComEventsMethod(dispid);
			this._methods = ComEventsMethod.Add(this._methods, comEventsMethod);
			return comEventsMethod;
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x001537CF File Offset: 0x001519CF
		[SecurityCritical]
		void NativeMethods.IDispatch.GetTypeInfoCount(out uint pctinfo)
		{
			pctinfo = 0U;
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x001537D4 File Offset: 0x001519D4
		[SecurityCritical]
		void NativeMethods.IDispatch.GetTypeInfo(uint iTInfo, int lcid, out IntPtr info)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x001537DB File Offset: 0x001519DB
		[SecurityCritical]
		void NativeMethods.IDispatch.GetIDsOfNames(ref Guid iid, string[] names, uint cNames, int lcid, int[] rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x001537E4 File Offset: 0x001519E4
		private unsafe static Variant* GetVariant(Variant* pSrc)
		{
			if (pSrc->VariantType == (VarEnum)16396)
			{
				Variant* ptr = (Variant*)(void*)pSrc->AsByRefVariant;
				if ((ptr->VariantType & (VarEnum)20479) == (VarEnum)16396)
				{
					return ptr;
				}
			}
			return pSrc;
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x00153820 File Offset: 0x00151A20
		[SecurityCritical]
		unsafe void NativeMethods.IDispatch.Invoke(int dispid, ref Guid riid, int lcid, INVOKEKIND wFlags, ref DISPPARAMS pDispParams, IntPtr pvarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ComEventsMethod comEventsMethod = this.FindMethod(dispid);
			if (comEventsMethod == null)
			{
				return;
			}
			object[] array = new object[pDispParams.cArgs];
			int[] array2 = new int[pDispParams.cArgs];
			bool[] array3 = new bool[pDispParams.cArgs];
			Variant* ptr = (Variant*)(void*)pDispParams.rgvarg;
			int* ptr2 = (int*)(void*)pDispParams.rgdispidNamedArgs;
			int i;
			int num;
			for (i = 0; i < pDispParams.cNamedArgs; i++)
			{
				num = ptr2[i];
				Variant* variant = ComEventsSink.GetVariant(ptr + i);
				array[num] = variant->ToObject();
				array3[num] = true;
				if (variant->IsByRef)
				{
					array2[num] = i;
				}
				else
				{
					array2[num] = -1;
				}
			}
			num = 0;
			while (i < pDispParams.cArgs)
			{
				while (array3[num])
				{
					num++;
				}
				Variant* variant2 = ComEventsSink.GetVariant(ptr + (pDispParams.cArgs - 1 - i));
				array[num] = variant2->ToObject();
				if (variant2->IsByRef)
				{
					array2[num] = pDispParams.cArgs - 1 - i;
				}
				else
				{
					array2[num] = -1;
				}
				num++;
				i++;
			}
			object obj = comEventsMethod.Invoke(array);
			if (pvarResult != IntPtr.Zero)
			{
				Marshal.GetNativeVariantForObject(obj, pvarResult);
			}
			for (i = 0; i < pDispParams.cArgs; i++)
			{
				int num2 = array2[i];
				if (num2 != -1)
				{
					ComEventsSink.GetVariant(ptr + num2)->CopyFromIndirect(array[i]);
				}
			}
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x001539A8 File Offset: 0x00151BA8
		[SecurityCritical]
		CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			if (iid == this._iidSourceItf || iid == typeof(NativeMethods.IDispatch).GUID)
			{
				ppv = Marshal.GetComInterfaceForObject(this, typeof(NativeMethods.IDispatch), CustomQueryInterfaceMode.Ignore);
				return CustomQueryInterfaceResult.Handled;
			}
			if (iid == ComEventsSink.IID_IManagedObject)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			return CustomQueryInterfaceResult.NotHandled;
		}

		// Token: 0x06006348 RID: 25416 RVA: 0x00153A18 File Offset: 0x00151C18
		private void Advise(object rcw)
		{
			IConnectionPointContainer connectionPointContainer = (IConnectionPointContainer)rcw;
			IConnectionPoint connectionPoint;
			connectionPointContainer.FindConnectionPoint(ref this._iidSourceItf, out connectionPoint);
			connectionPoint.Advise(this, out this._cookie);
			this._connectionPoint = connectionPoint;
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x00153A50 File Offset: 0x00151C50
		[SecurityCritical]
		private void Unadvise()
		{
			try
			{
				this._connectionPoint.Unadvise(this._cookie);
				Marshal.ReleaseComObject(this._connectionPoint);
			}
			catch (Exception)
			{
			}
			finally
			{
				this._connectionPoint = null;
			}
		}

		// Token: 0x04002CC2 RID: 11458
		private Guid _iidSourceItf;

		// Token: 0x04002CC3 RID: 11459
		private IConnectionPoint _connectionPoint;

		// Token: 0x04002CC4 RID: 11460
		private int _cookie;

		// Token: 0x04002CC5 RID: 11461
		private ComEventsMethod _methods;

		// Token: 0x04002CC6 RID: 11462
		private ComEventsSink _next;

		// Token: 0x04002CC7 RID: 11463
		private const VarEnum VT_BYREF_VARIANT = (VarEnum)16396;

		// Token: 0x04002CC8 RID: 11464
		private const VarEnum VT_TYPEMASK = (VarEnum)4095;

		// Token: 0x04002CC9 RID: 11465
		private const VarEnum VT_BYREF_TYPEMASK = (VarEnum)20479;

		// Token: 0x04002CCA RID: 11466
		private static Guid IID_IManagedObject = new Guid("{C3FCC19E-A970-11D2-8B5A-00A0C9B7C9C4}");
	}
}
