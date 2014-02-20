using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildChatPrototype.Chat
{
	public abstract class Message
	{
		public abstract void Handle();
	}
}
