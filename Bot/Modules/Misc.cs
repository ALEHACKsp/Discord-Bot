using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using DPP_Bot.Core.DB;

namespace DPP_Bot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        //private readonly DbMisc _dbMisc = new DbMisc();

        private readonly DbComandos _dbComandos = new DbComandos();

        //[Command("testar")]
        //public async Task Testar()
        //{

        //}

        //  Oi

        [Command("Oi")]
        [Summary("Dá oi.")]
        [Alias("Olá")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Oi()
        {
            await Context.Channel.SendMessageAsync($"Olá {Context.User.Mention}");
        }

        //  Comandos
        [Command("Comandos", RunMode = RunMode.Async)]
        [Alias("Comando")]
        [Summary("Lista de comandos.")]
        public async Task Ajuda()
        {
            var pv = await Context.User.GetOrCreateDMChannelAsync();

            var embed = new EmbedBuilder();
            embed.WithTitle("Esses são os meus comandos:");
            embed.WithDescription(_dbComandos.StringAjuda);

            try
            {
                await pv.SendMessageAsync("", false, embed);

                await Context.Message.DeleteAsync();
                const int delay = 5000;
                var m = await ReplyAsync($":mailbox_with_mail: Enviei no seu privado {Context.User.Mention}");
                await Task.Delay(delay);
                await m.DeleteAsync();
            }
            catch (Exception)
            {
                await Context.Channel.SendMessageAsync($"Não consegui lhe enviar a mensagem no privado, então mandei por aqui {Context.User.Mention}", false, embed);
            }

        }



        //  Ping
        [Command("Ping")]
        [Summary("Tempo de resposta em MS.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Ping()
        {
            int pingstart = DateTime.Now.Millisecond;
            var msg = await ReplyAsync("Iniciando ping");
            var pingend = DateTime.Now.Millisecond;
            //await msg.ModifyAsync(x => { x.Content = $"Pong! `{pingstart} {pingend} {Context.Client.Latency} ms` :ping_pong:"; });
            await msg.ModifyAsync(x => { x.Content = $"Pong! `{pingend - pingstart} ms` :ping_pong:"; });
        }


        //  Votar
        [Command("Votar")]
        [Summary("Abro uma votação, com opções de Sim e Não.")]
        [RequireBotPermission(ChannelPermission.AddReactions)]
        public async Task NovoVoto([Remainder] string secondPart = null)
        {
            if (secondPart == null)
            {
                await Context.Channel.SendMessageAsync("Você precisa me dizer o motivo da votação.");
                return;
            }
            if (secondPart.Length >= 200)
            {
                await Context.Channel.SendMessageAsync("Perdão, porém seu voto não deve ter mais de 200 caracteres");
                return;
            }

            var embed = new EmbedBuilder();
            embed.WithColor(new Color(126, 211, 33));
            embed.WithTitle("VOTE AQUI!");
            embed.WithDescription(secondPart);
            embed.WithFooter($"Votação criada por: {Context.User.Username}");

            RestUserMessage msg = await Context.Channel.SendMessageAsync("", embed: embed);
            await msg.AddReactionAsync(new Emoji("✅"));
            await msg.AddReactionAsync(new Emoji("❌"));

            await Context.Message.DeleteAsync();

        }

        //  Membros
        [Command("Membros")]
        [Summary("Diz quantos membros temos")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task TotalMembros()
        {
            await Context.Channel.SendMessageAsync($"Atualmente contamos com `{Context.Guild.MemberCount}` membros");
        }

        //  Avatar
        [Command("Avatar")]
        [Summary("Pega o avatar do usuário mencionado")]
        [Alias("av")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Avatar(IGuildUser user = null)
        {

            if (user == null)
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Aqui está o seu avatar.");
                embed.WithImageUrl(Context.User.GetAvatarUrl(ImageFormat.Auto, 512));
                embed.WithColor(new Color(74, 144, 226));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle($"Aqui está o avatar de {user}");
                embed.WithImageUrl(user.GetAvatarUrl(ImageFormat.Auto, 512));
                embed.WithColor(new Color(74, 144, 226));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
        }

        [Command("userinfo")]
        [Summary("Mostra informações sobre o usuário")]
        [Alias("info")]
        internal async Task UserInfo(IGuildUser user = null)
        {
            if (user==null)
            {
                await ReplyAsync("Por favor, mencione alguém");
            }
            else
            {
                Random rand = new Random();
                var application = await Context.Client.GetApplicationInfoAsync();
                var thumbnailurl = user.GetAvatarUrl();
                var date = $"{user.CreatedAt.Day}/{user.CreatedAt.Month}/{user.CreatedAt.Year}";
                var auth = new EmbedAuthorBuilder()
                {
                    Name = user.Username,
                    IconUrl = thumbnailurl
                };
                var embed = new EmbedBuilder()
                {
                    Color = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)),
                    Author = auth
                };
                var us = user as SocketGuildUser;
                var username = us.Username;
                var discr = us.Discriminator;
                var id = us.Id;
                var stat = us.Status;
                var cc = us.JoinedAt;
                var game = us.Game;
                var nick = us.Nickname;
                embed.Description = $"Username: **{username}**\n" +
                                      $"Discriminator: **{discr}**\n" +
                                      $"User ID: **{id}**\n" +
                                      $"Nickname: **{nick}**\n" +
                                      $"Criado em: **{date}**\n" +
                                      $"Status: {stat}\n" +
                                      $"Entrou no server em: **{cc}**\n" +
                                      $"Jogando: **{game}**";

                await ReplyAsync("", false, embed.Build());

            }
        }

        //[Command("Server")]
        //public async Task ServerInfo()
        //{
        //    var emb = new EmbedBuilder();
        //    emb.WithTitle("Informações do servidor:");
        //    emb.AddField($"👑 Dono", $"{Context.Guild.Owner.Mention}");
        //    emb.AddField($"🖂 Canais", $"{Context.Guild.TextChannels.Count}");
        //    emb.AddField($"🔊 Audio", $"{Context.Guild.Channels.Count}");
        //    emb.AddField($"🙎 Membros", $"{Context.Guild.MemberCount}");
        //    emb.AddField($"📜 Cargos", $"{Context.Guild.Roles.Count}");

        //    await ReplyAsync("", false, emb);
        //}

        //[Command("Conectar")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //public async Task JoinChannel(IVoiceChannel channel = null)
        //{
        //    // Pega o canal de audio
        //    channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
        //    if (channel == null) { await Context.Channel.SendMessageAsync($"{Context.User.Mention}, você precisa estar em um canal de voz"); return; }

        //    // Para o próximo passo com a transmissão de áudio, você gostaria de passar este Audio Client para um serviço.
        //    //var audioClient = 
        //    await channel.ConnectAsync();
        //}

    }
}
