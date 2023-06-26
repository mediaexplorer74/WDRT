using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000432 RID: 1074
	[FriendAccessAllowed]
	[EventSource(Guid = "8E9F5090-2D75-4d03-8A81-E5AFBF85DAF1", Name = "System.Diagnostics.Eventing.FrameworkEventSource")]
	internal sealed class FrameworkEventSource : EventSource
	{
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060035A9 RID: 13737 RVA: 0x000D294E File Offset: 0x000D0B4E
		public static bool IsInitialized
		{
			get
			{
				return FrameworkEventSource.Log != null;
			}
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000D2958 File Offset: 0x000D0B58
		private FrameworkEventSource()
			: base(new Guid(2392805520U, 11637, 19715, 138, 129, 229, 175, 191, 133, 218, 241), "System.Diagnostics.Eventing.FrameworkEventSource")
		{
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000D29AC File Offset: 0x000D0BAC
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3, bool arg4)
		{
			if (base.IsEnabled())
			{
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg3)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)ptr);
					ptr2[2].Size = (arg3.Length + 1) * 2;
					ptr2[3].DataPointer = (IntPtr)((void*)(&arg4));
					ptr2[3].Size = 4;
					base.WriteEventCore(eventId, 4, ptr2);
				}
			}
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000D2A8C File Offset: 0x000D0C8C
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3)
		{
			if (base.IsEnabled())
			{
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg3)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)ptr);
					ptr2[2].Size = (arg3.Length + 1) * 2;
					base.WriteEventCore(eventId, 3, ptr2);
				}
			}
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000D2B40 File Offset: 0x000D0D40
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, string arg2, bool arg3, bool arg4)
		{
			if (base.IsEnabled())
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					ptr2[2].DataPointer = (IntPtr)((void*)(&arg3));
					ptr2[2].Size = 4;
					ptr2[3].DataPointer = (IntPtr)((void*)(&arg4));
					ptr2[3].Size = 4;
					base.WriteEventCore(eventId, 4, ptr2);
				}
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000D2C1C File Offset: 0x000D0E1C
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, bool arg2, bool arg3)
		{
			if (base.IsEnabled())
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				base.WriteEventCore(eventId, 3, ptr);
			}
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000D2CA8 File Offset: 0x000D0EA8
		[NonEvent]
		[SecuritySafeCritical]
		private unsafe void WriteEvent(int eventId, long arg1, bool arg2, bool arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&arg4));
				ptr[3].Size = 4;
				base.WriteEventCore(eventId, 4, ptr);
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000D2D5F File Offset: 0x000D0F5F
		[Event(1, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerLookupStarted(string baseName, string mainAssemblyName, string cultureName)
		{
			base.WriteEvent(1, baseName, mainAssemblyName, cultureName);
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000D2D6B File Offset: 0x000D0F6B
		[Event(2, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerLookingForResourceSet(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(2, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000D2D7F File Offset: 0x000D0F7F
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerFoundResourceSetInCache(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(3, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000D2D93 File Offset: 0x000D0F93
		[Event(4, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerFoundResourceSetInCacheUnexpected(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(4, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000D2DA7 File Offset: 0x000D0FA7
		[Event(5, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerStreamFound(string baseName, string mainAssemblyName, string cultureName, string loadedAssemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(5, new object[] { baseName, mainAssemblyName, cultureName, loadedAssemblyName, resourceFileName });
			}
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000D2DD4 File Offset: 0x000D0FD4
		[Event(6, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerStreamNotFound(string baseName, string mainAssemblyName, string cultureName, string loadedAssemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(6, new object[] { baseName, mainAssemblyName, cultureName, loadedAssemblyName, resourceFileName });
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000D2E01 File Offset: 0x000D1001
		[Event(7, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerGetSatelliteAssemblySucceeded(string baseName, string mainAssemblyName, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(7, new object[] { baseName, mainAssemblyName, cultureName, assemblyName });
			}
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000D2E29 File Offset: 0x000D1029
		[Event(8, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerGetSatelliteAssemblyFailed(string baseName, string mainAssemblyName, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(8, new object[] { baseName, mainAssemblyName, cultureName, assemblyName });
			}
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000D2E51 File Offset: 0x000D1051
		[Event(9, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(string baseName, string mainAssemblyName, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(9, new object[] { baseName, mainAssemblyName, assemblyName, resourceFileName });
			}
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000D2E7A File Offset: 0x000D107A
		[Event(10, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupFailed(string baseName, string mainAssemblyName, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(10, new object[] { baseName, mainAssemblyName, assemblyName, resourceFileName });
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000D2EA3 File Offset: 0x000D10A3
		[Event(11, Level = EventLevel.Error, Keywords = (EventKeywords)1L)]
		public void ResourceManagerManifestResourceAccessDenied(string baseName, string mainAssemblyName, string assemblyName, string canonicalName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(11, new object[] { baseName, mainAssemblyName, assemblyName, canonicalName });
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000D2ECC File Offset: 0x000D10CC
		[Event(12, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourcesSufficient(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(12, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000D2EE1 File Offset: 0x000D10E1
		[Event(13, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourceAttributeMissing(string mainAssemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(13, mainAssemblyName);
			}
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000D2EF4 File Offset: 0x000D10F4
		[Event(14, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCreatingResourceSet(string baseName, string mainAssemblyName, string cultureName, string fileName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(14, new object[] { baseName, mainAssemblyName, cultureName, fileName });
			}
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000D2F1D File Offset: 0x000D111D
		[Event(15, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNotCreatingResourceSet(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(15, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000D2F32 File Offset: 0x000D1132
		[Event(16, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerLookupFailed(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(16, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000D2F47 File Offset: 0x000D1147
		[Event(17, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerReleasingResources(string baseName, string mainAssemblyName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(17, baseName, mainAssemblyName);
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000D2F5B File Offset: 0x000D115B
		[Event(18, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourcesNotFound(string baseName, string mainAssemblyName, string resName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(18, baseName, mainAssemblyName, resName);
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000D2F70 File Offset: 0x000D1170
		[Event(19, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerNeutralResourcesFound(string baseName, string mainAssemblyName, string resName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(19, baseName, mainAssemblyName, resName);
			}
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000D2F85 File Offset: 0x000D1185
		[Event(20, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerAddingCultureFromConfigFile(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(20, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000D2F9A File Offset: 0x000D119A
		[Event(21, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCultureNotFoundInConfigFile(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(21, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000D2FAF File Offset: 0x000D11AF
		[Event(22, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		public void ResourceManagerCultureFoundInConfigFile(string baseName, string mainAssemblyName, string cultureName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(22, baseName, mainAssemblyName, cultureName);
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000D2FC4 File Offset: 0x000D11C4
		[NonEvent]
		public void ResourceManagerLookupStarted(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerLookupStarted(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000D2FDC File Offset: 0x000D11DC
		[NonEvent]
		public void ResourceManagerLookingForResourceSet(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerLookingForResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000D2FF4 File Offset: 0x000D11F4
		[NonEvent]
		public void ResourceManagerFoundResourceSetInCache(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerFoundResourceSetInCache(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000D300C File Offset: 0x000D120C
		[NonEvent]
		public void ResourceManagerFoundResourceSetInCacheUnexpected(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerFoundResourceSetInCacheUnexpected(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000D3024 File Offset: 0x000D1224
		[NonEvent]
		public void ResourceManagerStreamFound(string baseName, Assembly mainAssembly, string cultureName, Assembly loadedAssembly, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerStreamFound(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, FrameworkEventSource.GetName(loadedAssembly), resourceFileName);
			}
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000D3045 File Offset: 0x000D1245
		[NonEvent]
		public void ResourceManagerStreamNotFound(string baseName, Assembly mainAssembly, string cultureName, Assembly loadedAssembly, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerStreamNotFound(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, FrameworkEventSource.GetName(loadedAssembly), resourceFileName);
			}
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000D3066 File Offset: 0x000D1266
		[NonEvent]
		public void ResourceManagerGetSatelliteAssemblySucceeded(string baseName, Assembly mainAssembly, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerGetSatelliteAssemblySucceeded(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, assemblyName);
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000D3080 File Offset: 0x000D1280
		[NonEvent]
		public void ResourceManagerGetSatelliteAssemblyFailed(string baseName, Assembly mainAssembly, string cultureName, string assemblyName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerGetSatelliteAssemblyFailed(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, assemblyName);
			}
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000D309A File Offset: 0x000D129A
		[NonEvent]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(string baseName, Assembly mainAssembly, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, resourceFileName);
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000D30B4 File Offset: 0x000D12B4
		[NonEvent]
		public void ResourceManagerCaseInsensitiveResourceStreamLookupFailed(string baseName, Assembly mainAssembly, string assemblyName, string resourceFileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCaseInsensitiveResourceStreamLookupFailed(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, resourceFileName);
			}
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000D30CE File Offset: 0x000D12CE
		[NonEvent]
		public void ResourceManagerManifestResourceAccessDenied(string baseName, Assembly mainAssembly, string assemblyName, string canonicalName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerManifestResourceAccessDenied(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, canonicalName);
			}
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000D30E8 File Offset: 0x000D12E8
		[NonEvent]
		public void ResourceManagerNeutralResourcesSufficient(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourcesSufficient(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000D3100 File Offset: 0x000D1300
		[NonEvent]
		public void ResourceManagerNeutralResourceAttributeMissing(Assembly mainAssembly)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourceAttributeMissing(FrameworkEventSource.GetName(mainAssembly));
			}
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000D3116 File Offset: 0x000D1316
		[NonEvent]
		public void ResourceManagerCreatingResourceSet(string baseName, Assembly mainAssembly, string cultureName, string fileName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCreatingResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, fileName);
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000D3130 File Offset: 0x000D1330
		[NonEvent]
		public void ResourceManagerNotCreatingResourceSet(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNotCreatingResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000D3148 File Offset: 0x000D1348
		[NonEvent]
		public void ResourceManagerLookupFailed(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerLookupFailed(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000D3160 File Offset: 0x000D1360
		[NonEvent]
		public void ResourceManagerReleasingResources(string baseName, Assembly mainAssembly)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerReleasingResources(baseName, FrameworkEventSource.GetName(mainAssembly));
			}
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000D3177 File Offset: 0x000D1377
		[NonEvent]
		public void ResourceManagerNeutralResourcesNotFound(string baseName, Assembly mainAssembly, string resName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourcesNotFound(baseName, FrameworkEventSource.GetName(mainAssembly), resName);
			}
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000D318F File Offset: 0x000D138F
		[NonEvent]
		public void ResourceManagerNeutralResourcesFound(string baseName, Assembly mainAssembly, string resName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerNeutralResourcesFound(baseName, FrameworkEventSource.GetName(mainAssembly), resName);
			}
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000D31A7 File Offset: 0x000D13A7
		[NonEvent]
		public void ResourceManagerAddingCultureFromConfigFile(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerAddingCultureFromConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000D31BF File Offset: 0x000D13BF
		[NonEvent]
		public void ResourceManagerCultureNotFoundInConfigFile(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCultureNotFoundInConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000D31D7 File Offset: 0x000D13D7
		[NonEvent]
		public void ResourceManagerCultureFoundInConfigFile(string baseName, Assembly mainAssembly, string cultureName)
		{
			if (base.IsEnabled())
			{
				this.ResourceManagerCultureFoundInConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
			}
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000D31EF File Offset: 0x000D13EF
		private static string GetName(Assembly assembly)
		{
			if (assembly == null)
			{
				return "<<NULL>>";
			}
			return assembly.FullName;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000D3206 File Offset: 0x000D1406
		[Event(30, Level = EventLevel.Verbose, Keywords = (EventKeywords)18L)]
		public void ThreadPoolEnqueueWork(long workID)
		{
			base.WriteEvent(30, workID);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000D3211 File Offset: 0x000D1411
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadPoolEnqueueWorkObject(object workID)
		{
			this.ThreadPoolEnqueueWork((long)((ulong)(*(IntPtr*)(void*)JitHelpers.UnsafeCastToStackPointer<object>(ref workID))));
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000D3227 File Offset: 0x000D1427
		[Event(31, Level = EventLevel.Verbose, Keywords = (EventKeywords)18L)]
		public void ThreadPoolDequeueWork(long workID)
		{
			base.WriteEvent(31, workID);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000D3232 File Offset: 0x000D1432
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadPoolDequeueWorkObject(object workID)
		{
			this.ThreadPoolDequeueWork((long)((ulong)(*(IntPtr*)(void*)JitHelpers.UnsafeCastToStackPointer<object>(ref workID))));
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000D3248 File Offset: 0x000D1448
		[Event(140, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)1, Opcode = EventOpcode.Start, Version = 1)]
		private void GetResponseStart(long id, string uri, bool success, bool synchronous)
		{
			this.WriteEvent(140, id, uri, success, synchronous);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000D325A File Offset: 0x000D145A
		[Event(141, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)1, Opcode = EventOpcode.Stop, Version = 1)]
		private void GetResponseStop(long id, bool success, bool synchronous, int statusCode)
		{
			this.WriteEvent(141, id, success, synchronous, statusCode);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000D326C File Offset: 0x000D146C
		[Event(142, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)2, Opcode = EventOpcode.Start, Version = 1)]
		private void GetRequestStreamStart(long id, string uri, bool success, bool synchronous)
		{
			this.WriteEvent(142, id, uri, success, synchronous);
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000D327E File Offset: 0x000D147E
		[Event(143, Level = EventLevel.Informational, Keywords = (EventKeywords)4L, ActivityOptions = EventActivityOptions.Disable, Task = (EventTask)2, Opcode = EventOpcode.Stop, Version = 1)]
		private void GetRequestStreamStop(long id, bool success, bool synchronous)
		{
			this.WriteEvent(143, id, success, synchronous);
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000D328E File Offset: 0x000D148E
		[NonEvent]
		[SecuritySafeCritical]
		public void BeginGetResponse(object id, string uri, bool success, bool synchronous)
		{
			if (base.IsEnabled())
			{
				this.GetResponseStart(FrameworkEventSource.IdForObject(id), uri, success, synchronous);
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000D32A8 File Offset: 0x000D14A8
		[NonEvent]
		[SecuritySafeCritical]
		public void EndGetResponse(object id, bool success, bool synchronous, int statusCode)
		{
			if (base.IsEnabled())
			{
				this.GetResponseStop(FrameworkEventSource.IdForObject(id), success, synchronous, statusCode);
			}
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000D32C2 File Offset: 0x000D14C2
		[NonEvent]
		[SecuritySafeCritical]
		public void BeginGetRequestStream(object id, string uri, bool success, bool synchronous)
		{
			if (base.IsEnabled())
			{
				this.GetRequestStreamStart(FrameworkEventSource.IdForObject(id), uri, success, synchronous);
			}
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000D32DC File Offset: 0x000D14DC
		[NonEvent]
		[SecuritySafeCritical]
		public void EndGetRequestStream(object id, bool success, bool synchronous)
		{
			if (base.IsEnabled())
			{
				this.GetRequestStreamStop(FrameworkEventSource.IdForObject(id), success, synchronous);
			}
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000D32F4 File Offset: 0x000D14F4
		[Event(150, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = EventOpcode.Send)]
		public void ThreadTransferSend(long id, int kind, string info, bool multiDequeues)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(150, id, kind, info, multiDequeues);
			}
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000D330E File Offset: 0x000D150E
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadTransferSendObj(object id, int kind, string info, bool multiDequeues)
		{
			this.ThreadTransferSend((long)((ulong)(*(IntPtr*)(void*)JitHelpers.UnsafeCastToStackPointer<object>(ref id))), kind, info, multiDequeues);
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000D3328 File Offset: 0x000D1528
		[Event(151, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = EventOpcode.Receive)]
		public void ThreadTransferReceive(long id, int kind, string info)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(151, id, kind, info);
			}
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000D3340 File Offset: 0x000D1540
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadTransferReceiveObj(object id, int kind, string info)
		{
			this.ThreadTransferReceive((long)((ulong)(*(IntPtr*)(void*)JitHelpers.UnsafeCastToStackPointer<object>(ref id))), kind, info);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000D3358 File Offset: 0x000D1558
		[Event(152, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = (EventOpcode)11)]
		public void ThreadTransferReceiveHandled(long id, int kind, string info)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(152, id, kind, info);
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000D3370 File Offset: 0x000D1570
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void ThreadTransferReceiveHandledObj(object id, int kind, string info)
		{
			this.ThreadTransferReceive((long)((ulong)(*(IntPtr*)(void*)JitHelpers.UnsafeCastToStackPointer<object>(ref id))), kind, info);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000D3388 File Offset: 0x000D1588
		private static long IdForObject(object obj)
		{
			return (long)obj.GetHashCode() + 9223372032559808512L;
		}

		// Token: 0x040017EE RID: 6126
		public static readonly FrameworkEventSource Log = new FrameworkEventSource();

		// Token: 0x02000B91 RID: 2961
		public static class Keywords
		{
			// Token: 0x04003520 RID: 13600
			public const EventKeywords Loader = (EventKeywords)1L;

			// Token: 0x04003521 RID: 13601
			public const EventKeywords ThreadPool = (EventKeywords)2L;

			// Token: 0x04003522 RID: 13602
			public const EventKeywords NetClient = (EventKeywords)4L;

			// Token: 0x04003523 RID: 13603
			public const EventKeywords DynamicTypeUsage = (EventKeywords)8L;

			// Token: 0x04003524 RID: 13604
			public const EventKeywords ThreadTransfer = (EventKeywords)16L;
		}

		// Token: 0x02000B92 RID: 2962
		[FriendAccessAllowed]
		public static class Tasks
		{
			// Token: 0x04003525 RID: 13605
			public const EventTask GetResponse = (EventTask)1;

			// Token: 0x04003526 RID: 13606
			public const EventTask GetRequestStream = (EventTask)2;

			// Token: 0x04003527 RID: 13607
			public const EventTask ThreadTransfer = (EventTask)3;
		}

		// Token: 0x02000B93 RID: 2963
		[FriendAccessAllowed]
		public static class Opcodes
		{
			// Token: 0x04003528 RID: 13608
			public const EventOpcode ReceiveHandled = (EventOpcode)11;
		}
	}
}
