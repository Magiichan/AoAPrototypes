using GuildChatPrototype.Chat;
using GuildChatPrototype.PlayerStuff;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuildChatPrototype.Threading
{
	public class ChatThread
	{
		private ReaderWriterLockSlim MessageQueueLock = new ReaderWriterLockSlim();

		//Collection of unprocessed messages
		private List<ChatMessage<Player>> Messages;

		//While this is true the thread spins and handles chat messages.
		public bool SpinThread;

		public ChatThread()
		{
			this.SpinThread = true;
			Messages = new List<ChatMessage<Player>>(100);
		}

		public void ThreadedChatMessageHandler()
		{
			ChatMessage<Player>[] cachedChatMessageArray;

			while(SpinThread)
			{
				//Get an array of Messages. This is O(n) where n is the count of messages
				//Still more efficient than handling chat in the main thread where n will be much larger
				//This will also prevent the main thread from being block when it tries to add new messages.

				//* WARNING: This is technically not thread safe. But it'll increase speed and the worst case is
				// stale data for an iteration
				if(this.Messages.Count > 0)
				{
					this.MessageQueueLock.EnterWriteLock();
					try
					{
						cachedChatMessageArray = this.Messages.ToArray();
						this.Messages.Clear();
					}
					finally
					{
						this.MessageQueueLock.ExitWriteLock();
					}

					//For wouldn't really give preformance gain here.
					foreach (ChatMessage<Player> message in cachedChatMessageArray)
					{
						//Handle the message by calling the Message method Handle()
						message.Handle();
					}
				}
			}
		}

		//Queue the work item but lock the data first
		public void QueueMessageItem(ChatMessage<Player> message)
		{
			this.MessageQueueLock.EnterWriteLock();
			try
			{
				this.Messages.Add(message);
			}
			finally
			{
				this.MessageQueueLock.ExitWriteLock();
			}
		}
	}
}
