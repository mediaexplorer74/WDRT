using System;
using ComponentAce.Compression.Archiver;

namespace ComponentAce.Compression.Exception
{
	// Token: 0x02000035 RID: 53
	internal class ExceptionBuilder
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0001668C File Offset: 0x0001568C
		internal static Exception Exception(ErrorCode errorCode, Exception innerException)
		{
			string text = ArchiverConst.ErrorMessages[(int)errorCode];
			string text2 = text;
			return new ArchiverException(text2, errorCode, null, innerException);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000166AC File Offset: 0x000156AC
		internal static Exception Exception(ErrorCode errorCode)
		{
			string text = ArchiverConst.ErrorMessages[(int)errorCode];
			string text2 = text;
			return new ArchiverException(text2, errorCode, null, null);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000166CC File Offset: 0x000156CC
		internal static Exception Exception(ErrorCode errorCode, object[] args, Exception innerException)
		{
			string text = ArchiverConst.ErrorMessages[(int)errorCode];
			string text2 = string.Format(text, args);
			return new ArchiverException(text2, errorCode, args, innerException);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000166F4 File Offset: 0x000156F4
		internal static Exception Exception(ErrorCode errorCode, object[] args)
		{
			string text = ArchiverConst.ErrorMessages[(int)errorCode];
			string text2 = string.Format(text, args);
			return new ArchiverException(text2, errorCode, args, null);
		}
	}
}
