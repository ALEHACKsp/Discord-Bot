using System;

namespace DPP_Bot.Core.UserAccounts
{
    class UserAccount
    {
        public ulong ID { get; set; }

        public uint Points { get; set; }

        public uint XP { get; set; }

        public uint LevelNumber
        {
            get
            {
                return (uint)Math.Sqrt(XP / 50);
            }
            // Sistema de níveis que nunca foi implementado
        }

        public bool IsMuted { get; set; }

        public uint NumberOfWarnings { get; set; }
    }
}
