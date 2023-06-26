using System;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015C RID: 348
	internal abstract class ODataReaderCoreAsync : ODataReaderCore
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0001D959 File Offset: 0x0001BB59
		protected ODataReaderCoreAsync(ODataInputContext inputContext, bool readingFeed, IODataReaderWriterListener listener)
			: base(inputContext, readingFeed, listener)
		{
		}

		// Token: 0x06000985 RID: 2437
		protected abstract Task<bool> ReadAtStartImplementationAsync();

		// Token: 0x06000986 RID: 2438
		protected abstract Task<bool> ReadAtFeedStartImplementationAsync();

		// Token: 0x06000987 RID: 2439
		protected abstract Task<bool> ReadAtFeedEndImplementationAsync();

		// Token: 0x06000988 RID: 2440
		protected abstract Task<bool> ReadAtEntryStartImplementationAsync();

		// Token: 0x06000989 RID: 2441
		protected abstract Task<bool> ReadAtEntryEndImplementationAsync();

		// Token: 0x0600098A RID: 2442
		protected abstract Task<bool> ReadAtNavigationLinkStartImplementationAsync();

		// Token: 0x0600098B RID: 2443
		protected abstract Task<bool> ReadAtNavigationLinkEndImplementationAsync();

		// Token: 0x0600098C RID: 2444
		protected abstract Task<bool> ReadAtEntityReferenceLinkAsync();

		// Token: 0x0600098D RID: 2445 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		protected override Task<bool> ReadAsynchronously()
		{
			Task<bool> task;
			switch (this.State)
			{
			case ODataReaderState.Start:
				task = this.ReadAtStartImplementationAsync();
				break;
			case ODataReaderState.FeedStart:
				task = this.ReadAtFeedStartImplementationAsync();
				break;
			case ODataReaderState.FeedEnd:
				task = this.ReadAtFeedEndImplementationAsync();
				break;
			case ODataReaderState.EntryStart:
				task = TaskUtils.GetTaskForSynchronousOperation(delegate
				{
					base.IncreaseEntryDepth();
				}).FollowOnSuccessWithTask((Task t) => this.ReadAtEntryStartImplementationAsync());
				break;
			case ODataReaderState.EntryEnd:
				task = TaskUtils.GetTaskForSynchronousOperation(delegate
				{
					base.DecreaseEntryDepth();
				}).FollowOnSuccessWithTask((Task t) => this.ReadAtEntryEndImplementationAsync());
				break;
			case ODataReaderState.NavigationLinkStart:
				task = this.ReadAtNavigationLinkStartImplementationAsync();
				break;
			case ODataReaderState.NavigationLinkEnd:
				task = this.ReadAtNavigationLinkEndImplementationAsync();
				break;
			case ODataReaderState.EntityReferenceLink:
				task = this.ReadAtEntityReferenceLinkAsync();
				break;
			case ODataReaderState.Exception:
			case ODataReaderState.Completed:
				task = TaskUtils.GetFaultedTask<bool>(new ODataException(Strings.ODataReaderCore_NoReadCallsAllowed(this.State)));
				break;
			default:
				task = TaskUtils.GetFaultedTask<bool>(new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCoreAsync_ReadAsynchronously)));
				break;
			}
			return task.FollowOnSuccessWith(delegate(Task<bool> t)
			{
				if ((this.State == ODataReaderState.EntryStart || this.State == ODataReaderState.EntryEnd) && this.Item != null)
				{
					ReaderValidationUtils.ValidateEntry(base.CurrentEntry);
				}
				return t.Result;
			});
		}
	}
}
