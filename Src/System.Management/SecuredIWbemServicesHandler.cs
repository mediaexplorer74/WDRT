using System;
using System.Runtime.InteropServices;

namespace System.Management
{
	// Token: 0x02000043 RID: 67
	internal class SecuredIWbemServicesHandler
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000D354 File Offset: 0x0000B554
		internal SecuredIWbemServicesHandler(ManagementScope theScope, IWbemServices pWbemServiecs)
		{
			this.scope = theScope;
			this.pWbemServiecsSecurityHelper = pWbemServiecs;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D36C File Offset: 0x0000B56C
		internal int OpenNamespace_(string strNamespace, int lFlags, ref IWbemServices ppWorkingNamespace, IntPtr ppCallResult)
		{
			return -2147217396;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D380 File Offset: 0x0000B580
		internal int CancelAsyncCall_(IWbemObjectSink pSink)
		{
			return this.pWbemServiecsSecurityHelper.CancelAsyncCall_(pSink);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		internal int QueryObjectSink_(int lFlags, ref IWbemObjectSink ppResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.QueryObjectSink_(lFlags, out ppResponseHandler);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		internal int GetObject_(string strObjectPath, int lFlags, IWbemContext pCtx, ref IWbemClassObjectFreeThreaded ppObject, IntPtr ppCallResult)
		{
			int num = -2147217407;
			if (ppCallResult != IntPtr.Zero)
			{
				num = this.pWbemServiecsSecurityHelper.GetObject_(strObjectPath, lFlags, pCtx, out ppObject, ppCallResult);
			}
			return num;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D404 File Offset: 0x0000B604
		internal int GetObjectAsync_(string strObjectPath, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.GetObjectAsync_(strObjectPath, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D42C File Offset: 0x0000B62C
		internal int PutClass_(IWbemClassObjectFreeThreaded pObject, int lFlags, IWbemContext pCtx, IntPtr ppCallResult)
		{
			int num = -2147217407;
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				num = WmiNetUtilsHelper.PutClassWmi_f(pObject, lFlags, pCtx, ppCallResult, (int)this.scope.Options.Authentication, (int)this.scope.Options.Impersonation, this.pWbemServiecsSecurityHelper, this.scope.Options.Username, password, this.scope.Options.Authority);
				Marshal.ZeroFreeBSTR(password);
			}
			return num;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		internal int PutClassAsync_(IWbemClassObjectFreeThreaded pObject, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.PutClassAsync_(pObject, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		internal int DeleteClass_(string strClass, int lFlags, IWbemContext pCtx, IntPtr ppCallResult)
		{
			int num = -2147217407;
			if (ppCallResult != IntPtr.Zero)
			{
				num = this.pWbemServiecsSecurityHelper.DeleteClass_(strClass, lFlags, pCtx, ppCallResult);
			}
			return num;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D520 File Offset: 0x0000B720
		internal int DeleteClassAsync_(string strClass, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.DeleteClassAsync_(strClass, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D548 File Offset: 0x0000B748
		internal int CreateClassEnum_(string strSuperClass, int lFlags, IWbemContext pCtx, ref IEnumWbemClassObject ppEnum)
		{
			int num = -2147217407;
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				num = WmiNetUtilsHelper.CreateClassEnumWmi_f(strSuperClass, lFlags, pCtx, out ppEnum, (int)this.scope.Options.Authentication, (int)this.scope.Options.Impersonation, this.pWbemServiecsSecurityHelper, this.scope.Options.Username, password, this.scope.Options.Authority);
				Marshal.ZeroFreeBSTR(password);
			}
			return num;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		internal int CreateClassEnumAsync_(string strSuperClass, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.CreateClassEnumAsync_(strSuperClass, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		internal int PutInstance_(IWbemClassObjectFreeThreaded pInst, int lFlags, IWbemContext pCtx, IntPtr ppCallResult)
		{
			int num = -2147217407;
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				num = WmiNetUtilsHelper.PutInstanceWmi_f(pInst, lFlags, pCtx, ppCallResult, (int)this.scope.Options.Authentication, (int)this.scope.Options.Impersonation, this.pWbemServiecsSecurityHelper, this.scope.Options.Username, password, this.scope.Options.Authority);
				Marshal.ZeroFreeBSTR(password);
			}
			return num;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D68C File Offset: 0x0000B88C
		internal int PutInstanceAsync_(IWbemClassObjectFreeThreaded pInst, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.PutInstanceAsync_(pInst, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
		internal int DeleteInstance_(string strObjectPath, int lFlags, IWbemContext pCtx, IntPtr ppCallResult)
		{
			int num = -2147217407;
			if (ppCallResult != IntPtr.Zero)
			{
				num = this.pWbemServiecsSecurityHelper.DeleteInstance_(strObjectPath, lFlags, pCtx, ppCallResult);
			}
			return num;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		internal int DeleteInstanceAsync_(string strObjectPath, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.DeleteInstanceAsync_(strObjectPath, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000D718 File Offset: 0x0000B918
		internal int CreateInstanceEnum_(string strFilter, int lFlags, IWbemContext pCtx, ref IEnumWbemClassObject ppEnum)
		{
			int num = -2147217407;
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				num = WmiNetUtilsHelper.CreateInstanceEnumWmi_f(strFilter, lFlags, pCtx, out ppEnum, (int)this.scope.Options.Authentication, (int)this.scope.Options.Impersonation, this.pWbemServiecsSecurityHelper, this.scope.Options.Username, password, this.scope.Options.Authority);
				Marshal.ZeroFreeBSTR(password);
			}
			return num;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		internal int CreateInstanceEnumAsync_(string strFilter, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.CreateInstanceEnumAsync_(strFilter, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		internal int ExecQuery_(string strQueryLanguage, string strQuery, int lFlags, IWbemContext pCtx, ref IEnumWbemClassObject ppEnum)
		{
			int num = -2147217407;
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				num = WmiNetUtilsHelper.ExecQueryWmi_f(strQueryLanguage, strQuery, lFlags, pCtx, out ppEnum, (int)this.scope.Options.Authentication, (int)this.scope.Options.Impersonation, this.pWbemServiecsSecurityHelper, this.scope.Options.Username, password, this.scope.Options.Authority);
				Marshal.ZeroFreeBSTR(password);
			}
			return num;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000D858 File Offset: 0x0000BA58
		internal int ExecQueryAsync_(string strQueryLanguage, string strQuery, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.ExecQueryAsync_(strQueryLanguage, strQuery, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000D880 File Offset: 0x0000BA80
		internal int ExecNotificationQuery_(string strQueryLanguage, string strQuery, int lFlags, IWbemContext pCtx, ref IEnumWbemClassObject ppEnum)
		{
			int num = -2147217407;
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				num = WmiNetUtilsHelper.ExecNotificationQueryWmi_f(strQueryLanguage, strQuery, lFlags, pCtx, out ppEnum, (int)this.scope.Options.Authentication, (int)this.scope.Options.Impersonation, this.pWbemServiecsSecurityHelper, this.scope.Options.Username, password, this.scope.Options.Authority);
				Marshal.ZeroFreeBSTR(password);
			}
			return num;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000D90C File Offset: 0x0000BB0C
		internal int ExecNotificationQueryAsync_(string strQueryLanguage, string strQuery, int lFlags, IWbemContext pCtx, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.ExecNotificationQueryAsync_(strQueryLanguage, strQuery, lFlags, pCtx, pResponseHandler);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000D934 File Offset: 0x0000BB34
		internal int ExecMethod_(string strObjectPath, string strMethodName, int lFlags, IWbemContext pCtx, IWbemClassObjectFreeThreaded pInParams, ref IWbemClassObjectFreeThreaded ppOutParams, IntPtr ppCallResult)
		{
			int num = -2147217407;
			if (ppCallResult != IntPtr.Zero)
			{
				num = this.pWbemServiecsSecurityHelper.ExecMethod_(strObjectPath, strMethodName, lFlags, pCtx, pInParams, out ppOutParams, ppCallResult);
			}
			return num;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000D978 File Offset: 0x0000BB78
		internal int ExecMethodAsync_(string strObjectPath, string strMethodName, int lFlags, IWbemContext pCtx, IWbemClassObjectFreeThreaded pInParams, IWbemObjectSink pResponseHandler)
		{
			return this.pWbemServiecsSecurityHelper.ExecMethodAsync_(strObjectPath, strMethodName, lFlags, pCtx, pInParams, pResponseHandler);
		}

		// Token: 0x040001BF RID: 447
		private IWbemServices pWbemServiecsSecurityHelper;

		// Token: 0x040001C0 RID: 448
		private ManagementScope scope;
	}
}
