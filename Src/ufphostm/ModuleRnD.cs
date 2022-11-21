using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
//using CppImplementationDetails;
//using CrtImplementationDetails;
using ATL;
using FlashingPlatform1;
using Microsoft.Windows.Flashing.Platform;
//using RAII;
using std;

// Token: 0x02000001 RID: 1
internal class Module
{
	// Token: 0x06000001 RID: 1 RVA: 0x00012424 File Offset: 0x00011824
	internal unsafe static void Term(CAtlComModule* A_0)
	{
        /*
		if (*A_0 != 0)
		{
			_ATL_OBJMAP_ENTRY30** ptr = *(A_0 + 8);
			if (ptr < *(A_0 + 12))
			{
				do
				{
					uint num = (uint)(*(int*)ptr);
					if (num != 0U)
					{
						_ATL_OBJMAP_ENTRY30* ptr2 = num;
						uint num2 = (uint)(*(int*)(ptr2 + 16 / sizeof(_ATL_OBJMAP_ENTRY30)));
						if (num2 != 0U)
						{
							uint num3 = num2;
							object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvStdcall)(System.IntPtr), num3, *(*num3 + 8));
						}
						*(int*)(ptr2 + 16 / sizeof(_ATL_OBJMAP_ENTRY30)) = 0;
					}
					ptr += 4 / sizeof(_ATL_OBJMAP_ENTRY30*);
				}
				while (ptr < *(A_0 + 12));
			}
			<Module>.DeleteCriticalSection(A_0 + 16);
			*A_0 = 0;
			
		}
		*/
    }//Term end
  
	// CUTTED     
}
