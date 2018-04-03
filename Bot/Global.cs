using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DPP_Bot
{
    class Global
    {
        internal static DiscordSocketClient Client { get; set; }
        internal static int MembrosNaUltimaHora { get; set; }
    }
}
