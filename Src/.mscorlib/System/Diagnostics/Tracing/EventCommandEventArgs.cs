using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the arguments for the <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> callback.</summary>
	// Token: 0x02000421 RID: 1057
	[__DynamicallyInvokable]
	public class EventCommandEventArgs : EventArgs
	{
		/// <summary>Gets the command for the callback.</summary>
		/// <returns>The callback command.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000CFA7E File Offset: 0x000CDC7E
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x000CFA86 File Offset: 0x000CDC86
		[__DynamicallyInvokable]
		public EventCommand Command
		{
			[__DynamicallyInvokable]
			get;
			internal set; }

		/// <summary>Gets the array of arguments for the callback.</summary>
		/// <returns>An array of callback arguments.</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000CFA8F File Offset: 0x000CDC8F
		// (set) Token: 0x06003522 RID: 13602 RVA: 0x000CFA97 File Offset: 0x000CDC97
		[__DynamicallyInvokable]
		public IDictionary<string, string> Arguments
		{
			[__DynamicallyInvokable]
			get;
			internal set; }

		/// <summary>Enables the event that has the specified identifier.</summary>
		/// <param name="eventId">The identifier of the event to enable.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="eventId" /> is in range; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003523 RID: 13603 RVA: 0x000CFAA0 File Offset: 0x000CDCA0
		[__DynamicallyInvokable]
		public bool EnableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, true);
		}

		/// <summary>Disables the event that have the specified identifier.</summary>
		/// <param name="eventId">The identifier of the event to disable.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="eventId" /> is in range; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003524 RID: 13604 RVA: 0x000CFACF File Offset: 0x000CDCCF
		[__DynamicallyInvokable]
		public bool DisableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, false);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000CFB00 File Offset: 0x000CDD00
		internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.Command = command;
			this.Arguments = arguments;
			this.eventSource = eventSource;
			this.listener = listener;
			this.perEventSourceSessionId = perEventSourceSessionId;
			this.etwSessionId = etwSessionId;
			this.enable = enable;
			this.level = level;
			this.matchAnyKeyword = matchAnyKeyword;
		}

		// Token: 0x04001788 RID: 6024
		internal EventSource eventSource;

		// Token: 0x04001789 RID: 6025
		internal EventDispatcher dispatcher;

		// Token: 0x0400178A RID: 6026
		internal EventListener listener;

		// Token: 0x0400178B RID: 6027
		internal int perEventSourceSessionId;

		// Token: 0x0400178C RID: 6028
		internal int etwSessionId;

		// Token: 0x0400178D RID: 6029
		internal bool enable;

		// Token: 0x0400178E RID: 6030
		internal EventLevel level;

		// Token: 0x0400178F RID: 6031
		internal EventKeywords matchAnyKeyword;

		// Token: 0x04001790 RID: 6032
		internal EventCommandEventArgs nextCommand;
	}
}
