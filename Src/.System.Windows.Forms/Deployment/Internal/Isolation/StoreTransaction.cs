using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000061 RID: 97
	internal class StoreTransaction : IDisposable
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00007AAE File Offset: 0x00005CAE
		public void Add(StoreOperationInstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007AC2 File Offset: 0x00005CC2
		public void Add(StoreOperationPinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007AD6 File Offset: 0x00005CD6
		public void Add(StoreOperationSetCanonicalizationContext o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007AEA File Offset: 0x00005CEA
		public void Add(StoreOperationSetDeploymentMetadata o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007AFE File Offset: 0x00005CFE
		public void Add(StoreOperationStageComponent o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007B12 File Offset: 0x00005D12
		public void Add(StoreOperationStageComponentFile o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007B26 File Offset: 0x00005D26
		public void Add(StoreOperationUninstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007B3A File Offset: 0x00005D3A
		public void Add(StoreOperationUnpinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007B4E File Offset: 0x00005D4E
		public void Add(StoreOperationScavenge o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007B64 File Offset: 0x00005D64
		~StoreTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007B94 File Offset: 0x00005D94
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007BA0 File Offset: 0x00005DA0
		[SecuritySafeCritical]
		private void Dispose(bool fDisposing)
		{
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
			StoreTransactionOperation[] storeOps = this._storeOps;
			this._storeOps = null;
			if (storeOps != null)
			{
				for (int num = 0; num != storeOps.Length; num++)
				{
					StoreTransactionOperation storeTransactionOperation = storeOps[num];
					if (storeTransactionOperation.Data.DataPtr != IntPtr.Zero)
					{
						switch (storeTransactionOperation.Operation)
						{
						case StoreTransactionOperationType.SetCanonicalizationContext:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetCanonicalizationContext));
							break;
						case StoreTransactionOperationType.StageComponent:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponent));
							break;
						case StoreTransactionOperationType.PinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationPinDeployment));
							break;
						case StoreTransactionOperationType.UnpinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUnpinDeployment));
							break;
						case StoreTransactionOperationType.StageComponentFile:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponentFile));
							break;
						case StoreTransactionOperationType.InstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationInstallDeployment));
							break;
						case StoreTransactionOperationType.UninstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUninstallDeployment));
							break;
						case StoreTransactionOperationType.SetDeploymentMetadata:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetDeploymentMetadata));
							break;
						case StoreTransactionOperationType.Scavenge:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationScavenge));
							break;
						}
						Marshal.FreeCoTaskMem(storeTransactionOperation.Data.DataPtr);
					}
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007D63 File Offset: 0x00005F63
		public StoreTransactionOperation[] Operations
		{
			get
			{
				if (this._storeOps == null)
				{
					this._storeOps = this.GenerateStoreOpsList();
				}
				return this._storeOps;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007D80 File Offset: 0x00005F80
		[SecuritySafeCritical]
		private StoreTransactionOperation[] GenerateStoreOpsList()
		{
			StoreTransactionOperation[] array = new StoreTransactionOperation[this._list.Count];
			for (int num = 0; num != this._list.Count; num++)
			{
				object obj = this._list[num];
				Type type = obj.GetType();
				array[num].Data.DataPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(obj));
				Marshal.StructureToPtr(obj, array[num].Data.DataPtr, false);
				if (type == typeof(StoreOperationSetCanonicalizationContext))
				{
					array[num].Operation = StoreTransactionOperationType.SetCanonicalizationContext;
				}
				else if (type == typeof(StoreOperationStageComponent))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponent;
				}
				else if (type == typeof(StoreOperationPinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.PinDeployment;
				}
				else if (type == typeof(StoreOperationUnpinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UnpinDeployment;
				}
				else if (type == typeof(StoreOperationStageComponentFile))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponentFile;
				}
				else if (type == typeof(StoreOperationInstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.InstallDeployment;
				}
				else if (type == typeof(StoreOperationUninstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UninstallDeployment;
				}
				else if (type == typeof(StoreOperationSetDeploymentMetadata))
				{
					array[num].Operation = StoreTransactionOperationType.SetDeploymentMetadata;
				}
				else
				{
					if (!(type == typeof(StoreOperationScavenge)))
					{
						throw new Exception("How did you get here?");
					}
					array[num].Operation = StoreTransactionOperationType.Scavenge;
				}
			}
			return array;
		}

		// Token: 0x04000199 RID: 409
		private ArrayList _list = new ArrayList();

		// Token: 0x0400019A RID: 410
		private StoreTransactionOperation[] _storeOps;
	}
}
