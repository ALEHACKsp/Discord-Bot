using System.IO;
using Newtonsoft.Json;

namespace DPP_Bot.Core.Configs
{
    class Config
    {
        //Declara os arquivos
        private const string ConfigFolder = "Resources";
        private const string ConfigFile = "Data.json";

        public static BotConfig Bot;
        static Config()
        {
            //Verifica a existência do diretório
            if (!Directory.Exists(ConfigFolder)) Directory.CreateDirectory(ConfigFolder);
            //Verifica a existência do arquivo
            if (!File.Exists(ConfigFolder + "/" + ConfigFile))
                //Se o arquivo não existir, então ele cria
            {
                Bot = new BotConfig();
                //Arquivo json
                string json = JsonConvert.SerializeObject(Bot, Formatting.Indented);
                //Escreve o arquivo
                File.WriteAllText(ConfigFolder + "/" + ConfigFile, json);
            }
            //Se já existir, então ele lê
            else
            {
                string json = File.ReadAllText(ConfigFolder + "/" + ConfigFile);
                Bot = JsonConvert.DeserializeObject<BotConfig>(json);

            }
        }

        //Gera a estrutura do arquivo
        public struct BotConfig
        {
            public string Token { get; set; }
            public string CmdPrefix { get; set; }
            public string Game { get; set; }
            public ulong IdChatGeral { get; set; }
            public ulong IdChatLog { get; set; }
            public ulong IdChatConvites { get; set; }
            public ulong IdServer { get; set; }
            public string WcEmbTitle { get; set; }
            public string WcEmbDescription { get; set; }
            public string WcEmbImgUrl { get; set; }
            public string PvWcEmbTitle { get; set; }
            public string PvWcEmbDescription { get; set; }
            public string PvWcEmbFooter { get; set; }
            public string PvWcField1Title { get; set; }
            public string PvWcField1Description { get; set; }
            public string PvWcField2Title { get; set; }
            public string PvWcField2Description { get; set; }
            public string PvWcField3Title { get; set; }
            public string PvWcField3Description { get; set; }
        }
    }
}
