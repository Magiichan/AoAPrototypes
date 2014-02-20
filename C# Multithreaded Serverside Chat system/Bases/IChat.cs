using GuildChatPrototype.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildChatPrototype.Bases
{
	public interface IChat<ListenerType>
	{
		ChatChannel<ListenerType> Chat { get; }
	}
}
