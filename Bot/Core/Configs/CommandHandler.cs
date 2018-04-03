using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DPP_Bot.Core.Services;

namespace DPP_Bot.Core.Configs
{
    class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        private readonly AutoMod _autoMod = new AutoMod();

        //Inicializa
        public async Task InitializeAsync(DiscordSocketClient client)                                                       // Declara
        {
            _client = client;
            _service = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,    // Define o nível do log
                DefaultRunMode = RunMode.Async  // Força todos os comandos rodarem de forma assíncronica
            });
            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAssync;
        }

        //Manipulador de comandos aguardável
        private async Task HandleCommandAssync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return;                                                                      //  Se mensagem for nula... Retornar, do contrário, prosseguir...
            var context = new SocketCommandContext(_client, msg);                                                           //  Contexto da mensagem
            if (context.User.IsBot) return;                                                                                 //  Se for um bot, retorna

            var userAccounts = UserAccounts.UserAccounts.GetAccount(context.User);                                          //  Var usuário
            if (userAccounts.IsMuted)                                                                                       //  Verifica se o usuário está mutado
            {
                await context.Message.DeleteAsync();                                                                        //  Caso esteja,
                return;                                                                                                     //  Retorna do método atual
            }
            if (s.ToString().Contains("discordapp.com/invite/"))                                                            //  Verifica se contém convite do discord
            {
                await _autoMod.AutoModInvite(context);                                                                      //  Caso conhtenha, o contexto apra o método de AutoModInvite
                return;                                                                                                     //  Retorna do método atual
            }


            int argPos = 0;                                                                                                     //  Determina posição do prefixo
            if (msg.HasStringPrefix(Config.Bot.CmdPrefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos)) //  Lê as msg's, e verifica se tem o prefixo configurado ou menção ao bot
            {
                var result = await _service.ExecuteAsync(context, argPos);
                if (!result.IsSuccess)                                                                                          //  Caso o comando não obtiver sucesso
                {
                    if (result.Error == CommandError.UnknownCommand)                                                            //  E o erro for comando não reconhecido
                    {
                        var emb = EmbedHandler.CriarEmbed("Erro",
                            $"{context.User.Mention} O comando usado por você, não é reconhecido.\n" +
                            $"Tente usar `{Config.Bot.CmdPrefix}Comandos` para receber a lista de comandos reconhecíveis.", EmbedHandler.EmbedMessageType.Error,true);
                        await context.Channel.SendMessageAsync("",false,emb);  //  Envia essa mensagem
                        return;                                                                                                 //  Retorna do método
                    }

                    await context.Channel.SendMessageAsync($"{context.User.Mention}: {result.ErrorReason}");                    //  Comando reconhecido, porém teve erro. É enviada a razão do erro
                }
                Log.CmdLog(context, s);                                                                                         //  Se tudo ocorreu corretamente, envia para o LOG do servidor e do CMD
            }

        }
    }
}
