using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DPP_Bot.Core.Data;
using DPP_Bot.Core.DB;

namespace DPP_Bot.Modules
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        private readonly DbMisc _dbMisc = new DbMisc();

        //private readonly DbComandos _dbComandos = new DbComandos();
        [Command("Palmeiras")]
        [Summary("E o mundial?")]
        internal async Task Palmeiras()
        {
            await ReplyAsync("Não tem mundial");
        }
        //  Gif aleatório baseado na tag
        [Command("Gif", RunMode = RunMode.Async)]
        [Summary("Responde um aleatório baseado na tag")]
        public async Task Gif([Remainder] string pesquisa = null)
        {
            if (pesquisa == null)
            {
                await Context.Message.DeleteAsync();
                var m = await Context.Channel.SendMessageAsync($"{Context.User.Mention} você deve me dizer um parâmetro de pesquisa");
                await Task.Delay(2000);
                await m.DeleteAsync();
                return;
            }
            if (pesquisa.Contains("fingerjob") || pesquisa.Contains("blowjob") || pesquisa.Contains("masturb") || pesquisa.Contains("siririca") || pesquisa.Contains("punheta") || pesquisa.Contains("xana") || pesquisa.Contains("boob") || pesquisa.Contains("buceta") || pesquisa.Contains("ppk") || pesquisa.Contains("ass") || pesquisa.Contains("porn") || pesquisa.Contains("porno") || pesquisa.Contains("pornô") || pesquisa.Contains("sexo") || pesquisa.Contains("sex") || pesquisa.Contains("anal") || pesquisa.Contains("cu") || pesquisa.Contains("peitos") || pesquisa.Contains("bunda"))
            {
                await Context.Message.DeleteAsync();
                var m = await Context.Channel.SendMessageAsync($"Que feio {Context.User.Mention}... Não irei lhe mostrar esse tipo de coisa");
                await Task.Delay(2000);
                await m.DeleteAsync();
                return;
            }
            var query = WebUtility.UrlEncode(pesquisa);
            if (query == null) return;

            using (var http = new HttpClient())
            {
                var response = await http.GetStringAsync("http://api.giphy.com/v1/gifs/search?api_key= " + "<Insira a Key da API aqui>" + $"&lang=pt&q={Uri.EscapeUriString(query)}").ConfigureAwait(false);
                var data = JsonConvert.DeserializeObject<GiphyData>(response);
                var r = new Random();
                if (data.data.Count == 0)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Não encontrei nada para esse parâmetro.");
                    return;
                }
                var randomData = data.data[r.Next(data.data.Count)];
                int index = randomData.images.original.url.IndexOf('?');
                string url = (index == -1 ? randomData.images.original.url : randomData.images.original.url.Remove(index));

                var builder = new EmbedBuilder();
                builder.WithImageUrl(url);
                builder.WithTitle($"Aqui está o seu gif: {pesquisa}");
                builder.WithColor(new Color(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));

                var m = await Context.Channel.SendMessageAsync(Context.User.Mention, false, builder);
                await m.AddReactionAsync(new Emoji("👍"));
                await m.AddReactionAsync(new Emoji("😂"));
                await m.AddReactionAsync(new Emoji("😂"));
                await m.AddReactionAsync(new Emoji("😯"));
                await m.AddReactionAsync(new Emoji("😭"));
                await m.AddReactionAsync(new Emoji("😡"));

                // Fiz um sistema de reactionss igual o do facebook mesmo kkkkk
            }

        }

        //  Neko
        [Command("Neko")]
        [Summary("Responde um gato aleatório.")]
        public async Task Neko()
        {

            string json;
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://nekos.life/api/neko");
            }

            var jsonData = JsonConvert.DeserializeObject<dynamic>(json);
            string imageUrl = jsonData.neko;

            var builder = new EmbedBuilder();
            builder.WithImageUrl(imageUrl);
            builder.WithTitle("Aí está o seu gatinho");
            var rand = new Random();
            builder.WithColor(new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)));

            await Context.Channel.SendMessageAsync(Context.User.Mention, false, builder);
        }

        //  Gato
        [Command("Gato")]
        [Summary("Responde um gato aleatório.")]
        public async Task Gato()
        {

            string json;
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("http://random.cat/meow");
            }

            var jsonData = JsonConvert.DeserializeObject<dynamic>(json);
            string imageUrl = jsonData.file;

            var builder = new EmbedBuilder();
            builder.WithImageUrl(imageUrl);
            builder.WithTitle("Aí está o seu gatinho");
            var rand = new Random();
            builder.WithColor(new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)));

            await Context.Channel.SendMessageAsync(Context.User.Mention, false, builder);
        }

        //  Cachorro
        [Command("Cachorro")]
        [Alias("dog")]
        public async Task Cachorro()
        {

            string json;
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://random.dog/woof.json");
            }

            var jsonData = JsonConvert.DeserializeObject<dynamic>(json);
            string imageUrl = jsonData.url;

            if (imageUrl == null || imageUrl.ToLower().EndsWith("mp4"))
            {
                await Cachorro();
            }

            var builder = new EmbedBuilder();
            builder.WithImageUrl(imageUrl);
            builder.WithTitle("Aí está o seu cachorrinho");
            var rand = new Random();
            builder.WithColor(new Color(rand.Next(0,255), rand.Next(0, 255), rand.Next(0, 255)));

            await Context.Channel.SendMessageAsync(Context.User.Mention, false, builder);
        }

        //  Siu ou não
        [Command("Sim ou Não")]
        [Alias("Sim ou Nao")]
        [Summary("Dá uma resposta Sim ou Não")]
        public async Task GetYesNo([Remainder] string secondPart = null)
        {

            if (secondPart == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Parce que faltou o complento da sua pergunta...");
                return;
            }


            WebClient wc = new WebClient();
            string json = wc.DownloadString("https://yesno.wtf/api/");
            var response = JsonConvert.DeserializeObject<dynamic>(json);

            string imageUrl = response.image;
            string answer = response.answer;

            string resposta;

            var builder = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder()
            };
            builder.WithImageUrl(imageUrl);

            if (answer == "yes")
            {
                builder.WithColor(Color.Green);
                resposta = "Sim";
            }
            else if (answer == "no")
            {
                builder.WithColor(Color.Red);
                resposta = "Não";
            }
            else
            {
                builder.WithColor(Color.Orange);
                resposta = "Não sei";
            }

            builder.AddField("Eu diria...", resposta);

            await Context.Channel.SendMessageAsync("", false, builder);
        }

        //  Repetir
        [Command("Repetir")]
        [Summary("Repito algo.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Repetir([Remainder] string msg = null)
        {

            if (msg == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Você precisa me dizer algo para repetir");
                return;
            }

            var embed = new EmbedBuilder();
            embed.WithTitle($"O {Context.User.Username} me pediu para repetir isso");
            embed.WithDescription(msg);
            embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
            embed.WithColor(new Color(74, 144, 226));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        //  Escolha
        [Command("Escolha")]
        [Summary("Escolho algo, dentro das opções fornecidas.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task EscolhaUm([Remainder] string message = null)
        {
            if (message == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Parece que você não me deu nenhuma opção...");
                return;
            }

            string[] options = message.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle($"Pensei um pouco {Context.User.Username}");
            embed.WithDescription($"{selection}, eu acho que é a melhor escolha");
            embed.WithColor(new Color(74, 144, 226));
            embed.WithThumbnailUrl("https://image.prntscr.com/image/lHMDXsqySVOmRGdpU1T5DA.png");
            embed.WithFooter($"{Context.User.Username}, pediu para mim escolher");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        //  Eu te amo
        [Command("Eu te amo")]
        [Summary("Envia mensagem de Te Amo.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task EuTeAmo()
        {
            var pv = await Context.User.GetOrCreateDMChannelAsync();
            await Context.Channel.SendMessageAsync(":blush:");
            await pv.SendMessageAsync($"Eu também te amo {Context.User.Username}...\nSó não conta pra ninguém");
        }

        //  Bom Dia
        [Command("Bom Dia")]
        [Summary("Envia mensagem de bom dia.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task BomDia([Remainder] string secondpart = null)
        {
            await Context.Channel.SendMessageAsync($"Bom Dia {Context.User.Mention} {secondpart}");
        }

        //  Boa Noite
        [Command("Boa Noite")]
        [Summary("Envia mensagem de boa noite.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task BoaNoite([Remainder] string secondpart = null)
        {
            await Context.Channel.SendMessageAsync($"Boa Noite {Context.User.Mention} {secondpart}:full_moon_with_face: ");
        }

        //  Sugestão
        [Command("Sugestão")]
        [Summary("Sugiro algo para a pessoa.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Sugestao([Remainder] string message = null)
        {
            if (message == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Você deve me dizer sobre o que eu vou dar a sugestão.");
                return;
            }

            string[] options =
            {
                "sim",
                "não",
                "talvez",
                "lógico, com certeza",
                "certamente",
                "posso dar resposta em troco de bala?",
                "certamente não",
                "nope"
            };

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];
            string neko = _dbMisc.Neko[r.Next(0, _dbMisc.Neko.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle($"Pensei um pouco {Context.User.Username}");
            embed.WithDescription($"Acho que, {selection}...");
            embed.WithColor(new Color(74, 144, 226));
            embed.WithThumbnailUrl(neko);

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        //  Aquela Carinha
        [Command("Aquela carinha")]
        [Summary("Aquela carinha...")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task AquelaCarinha()
        {
            await Context.Channel.SendMessageAsync("( ͡° ͜ʖ ͡°)");
        }

        // Eu tirei iso porque o servidor não era meu, e sei lá... se quiser usar, só tirar o comentário
        //  Bolsonaro
        //[Command("Bolsonaro")]
        //[Summary("Frases icônicas do bolsonaro.")]
        //[RequireBotPermission(ChannelPermission.SendMessages)]
        //public async Task Bolsonaro()
        //{
        //    Random r = new Random();
        //    string fraseSelecionada = _dbMisc.BolsonaroFrases[r.Next(0, _dbMisc.BolsonaroFrases.Length)];
        //    string avatarSelecionado = _dbMisc.BolsonaroAvatares[r.Next(0, _dbMisc.BolsonaroAvatares.Length)];


        //    var embed = new EmbedBuilder();
        //    embed.WithTitle("Bolsonaro:");
        //    embed.WithDescription(fraseSelecionada.Replace("{user}", Context.User.Mention));

        //    embed.WithColor(new Color(74, 144, 226));
        //    embed.WithThumbnailUrl(avatarSelecionado);

        //    await Context.Channel.SendMessageAsync("", false, embed);
        //}

        //  Abraçar
        [Command("Abraçar")]
        [Summary("Manda um abraço.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Abraçar(IGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Você deve mencionar alguém");
                return;
            }

            Random r = new Random();
            string abraços = _dbMisc.Abraços[r.Next(0, _dbMisc.Abraços.Length)];
            string frase = _dbMisc.FrasesFofas[r.Next(0, _dbMisc.FrasesFofas.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username} deu um abraço em {user.Username}");
            embed.WithDescription(frase);
            embed.WithColor(new Color(74, 144, 226));
            embed.WithImageUrl(abraços);
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync($"{Context.User.Mention} {user.Mention}", false, embed);
        }

        //  Beijar
        [Command("Beijar")]
        [Summary("Manda um Beijar.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Beijar(IGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Você deve mencionar alguém");
                return;
            }

            Random r = new Random();
            string abraços = _dbMisc.Beijos[r.Next(0, _dbMisc.Beijos.Length)];
            string frase = _dbMisc.FrasesFofas[r.Next(0, _dbMisc.FrasesFofas.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username} deu um beijo em {user.Username}");
            embed.WithDescription(frase);
            embed.WithColor(new Color(74, 144, 226));
            embed.WithImageUrl(abraços);

            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync($"{Context.User.Mention} {user.Mention}", false, embed);
        }

        //  Swag
        [Command("swag", RunMode = RunMode.Async)]
        [Summary("Swags no chat")]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task Swag()
        {
            var msg = await ReplyAsync("( ͡° ͜ʖ ͡°)>⌐■-■");
            await Task.Delay(1500);
            await msg.ModifyAsync(x => { x.Content = "( ͡⌐■ ͜ʖ ͡-■)"; });
        }

        //  Knuckles
        [Command("Knuckles")]
        [Summary("Knuckles")]
        public async Task Knuckles()
        {
            string[] options =
            {
                "Do you know the wae?",
                "Do you know the wae my bruda?",
                "You are the queen?",
                "You are not the queen!",
                "Hummmm, you smell of ebola",
                "Show me the wae",
                "Show me the wae my bruda",
                "I know the wae ma bruda, i can show you the wae"
            };

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Uganda knuckles:");
            embed.WithDescription(selection);
            embed.WithColor(new Color(74, 144, 226));
            embed.WithThumbnailUrl("https://i.imgur.com/oVgpEuD.png");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("Dados"), Alias("Dado"), Summary("Rola um dado")]
        public async Task RollDice()
        {
            Random r = new Random();
            var emb = new EmbedBuilder();

            emb.WithTitle("Dados");
            emb.WithDescription($"🎲 Rolado: {r.Next(1, 7)}");
            emb.WithColor(new Color(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));
            await ReplyAsync("", false, emb);
        }
    }
}
