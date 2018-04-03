using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DPP_Bot.Core;
using DPP_Bot.Core.Configs;
using DPP_Bot.Core.Services;

namespace DPP_Bot
{
    class Program : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;
        private readonly BoasVindas _boasVindas = new BoasVindas();

        static void Main()
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (string.IsNullOrEmpty(Config.Bot.Token))                 // Verifica se o valor do token foi inserido no arquivo de configuração
            {
                Console.WriteLine("Por favor, escreva o Token no arquivo de configuração dentro da pasta Resources");
                Console.ReadKey();
                return;         
            }

            _client = new DiscordSocketClient(new DiscordSocketConfig   // Cria o soket de cliente, e configura o nível do log
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 1000
            });

            _client.Log += _Log;                                        // Chama o evento para escrever o log
            _client.UserJoined += _boasVindas.NovoMembro;                           // Chama o evento quando usuário novo entrar
            _client.Ready += RepeatingTimer.StartTimer;
            _client.Ready += _client_Ready;

            var selfRoles = new SelfRoles();                            // Delcara Classe SelfRoles
            _client.ReactionAdded += selfRoles.AoAddReact;              // Chama o evento quando adicionar reação
            _client.ReactionRemoved += selfRoles.AoRemoverReact;        // Chama o evento quando remove reação

            await _client.LoginAsync(TokenType.Bot, Config.Bot.Token);  // Credenciais de login
            await _client.SetGameAsync(Config.Bot.Game);                // Define o jogo, que pode ser modificado no arquivo Data.json
            await _client.StartAsync();                                 // Inicia conexão

            Global.Client = _client;
            _handler = new CommandHandler();                            // Declara o pegador de comandos

            await _handler.InitializeAsync(_client);                    // Inicia o pegador de comandos

            await Task.Delay(-1);                                       // Previne o console de fechar
        }

        internal async Task _client_Ready()
        {
            var canal = (SocketTextChannel)_client.GetChannel(Config.Bot.IdChatLog);
            await canal.SendMessageAsync($"✅ {DateTime.Now:HH:mm:ss tt} - Online e operando!");
        }


        private Task _Log(LogMessage msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    return Task.CompletedTask;
                //Console.ForegroundColor = ConsoleColor.Yellow;
                //break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            } // Cor para o log do servidor


            Console.Write($"{DateTime.Now:HH:mm:ss tt} [{msg.Severity,8}] {msg.Source,7}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(msg.Message + "\n");

            return Task.CompletedTask;
        }                            // Escreve o log no console
    }
}
