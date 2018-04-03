//using System;
//using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using DPP_Bot.Core.DB;

namespace DPP_Bot.Modules
{
    public class Runas : ModuleBase<SocketCommandContext>
    {
        private readonly Campeoes _campeoes = new Campeoes();

        [Command("Runas")]
        [Summary("v")]
        [Alias("R")]
        public async Task Runa(string champ)
        {
            champ = champ.ToLower().Trim();

            switch (champ)
            {
                case "wukong":
                    champ = champ.Replace("wukong", "monkeyking");
                    break;
                case "bardo":
                    champ = champ.Replace("bardo", "bard");
                    break;
                case "tk":
                    champ = champ.Replace("tk", "tahmkench");
                    break;
                case "ww":
                    champ = champ.Replace("ww", "warwick");
                    break;
                case "xin":
                    champ = champ.Replace("xin", "xinzhao");
                    break;
                case "cait":
                    champ = champ.Replace("cait", "caitlyn");
                    break;
                case "cho":
                    champ = champ.Replace("cho", "chogath");
                    break;
                case "cho'gath":
                    champ = champ.Replace("cho'gath", "chogath");
                    break;
                case "mundo":
                    champ = champ.Replace("mundo", "drmundo");
                    break;
                case "ez":
                    champ = champ.Replace("ez", "ezreal");
                    break;
                case "fiddle":
                    champ = champ.Replace("fiddle", "fiddlesticks");
                    break;
                case "gp":
                    champ = champ.Replace("gp", "gangplank");
                    break;
                case "heimer":
                    champ = champ.Replace("heimer", "heimerdinger");
                    break;
                case "kassa":
                    champ = champ.Replace("kassa", "kassadin");
                    break;
                case "kat":
                    champ = champ.Replace("kat", "katarina");
                    break;
                case "kha":
                    champ = champ.Replace("kha", "khazix");
                    break;
                case "kog":
                    champ = champ.Replace("kog", "khazix");
                    break;
                case "lb":
                    champ = champ.Replace("lb", "leblanc");
                    break;
                case "lee":
                    champ = champ.Replace("lee", "leesin");
                    break;
                case "malp":
                    champ = champ.Replace("malp", "malphite");
                    break;
                case "malza":
                    champ = champ.Replace("malza", "malza");
                    break;
                case "master":
                    champ = champ.Replace("master", "masteryi");
                    break;
                case "yi":
                    champ = champ.Replace("yi", "masteryi");
                    break;
                case "mf":
                    champ = champ.Replace("mf", "missfortune");
                    break;
                case "mord":
                    champ = champ.Replace("mord", "mordekaiser");
                    break;
                case "morg":
                    champ = champ.Replace("morg", "morgana");
                    break;
                case "naut":
                    champ = champ.Replace("naut", "nautilus");
                    break;
                case "nida":
                    champ = champ.Replace("nida", "nidalee");
                    break;
                case "noc":
                    champ = champ.Replace("noc", "nocturne");
                    break;
                case "ori":
                    champ = champ.Replace("ori", "oriana");
                    break;
                case "pant":
                    champ = champ.Replace("pant", "pantheon");
                    break;
                case "rek":
                    champ = champ.Replace("rek", "reksai");
                    break;
                case "renek":
                    champ = champ.Replace("renek", "renekton");
                    break;
                case "seju":
                    champ = champ.Replace("seju", "sejuani");
                    break;
                case "shy":
                    champ = champ.Replace("shy", "shyvana");
                    break;
                case "capeta":
                    champ = champ.Replace("capeta", "teemo");
                    break;
                case "trist":
                    champ = champ.Replace("trist", "tristana");
                    break;
                case "trynda":
                    champ = champ.Replace("trynda", "tryndamere");
                    break;
                case "tf":
                    champ = champ.Replace("tf", "twistedfate");
                    break;
                case "tw":
                    champ = champ.Replace("tw", "twitch");
                    break;
                case "vlad":
                    champ = champ.Replace("vlad", "vladimir");
                    break;
                case "voli":
                    champ = champ.Replace("voli", "volibear");
                    break;
            }

            // Verifica se o campeão está na array
            if (_campeoes.Nomes.Contains(champ))
            {
                var embed = new EmbedBuilder();
                embed.WithTitle($"Runa: {champ}");
                embed.WithColor(140, 0, 0);
                embed.WithDescription($"{Context.User.Mention} Aí está, runas para `{champ}`");
                embed.WithThumbnailUrl($"http://opgg-static.akamaized.net/images/lol/champion/{champ}.png?image=w_160&v=1");
                embed.WithFooter($"Solicitado por {Context.User.Username}#{Context.User.Discriminator}", Context.User.GetAvatarUrl());

                await Context.Channel.SendMessageAsync(Context.User.Mention, false, embed);
                await Context.Channel.SendFileAsync($"Resources/Runa/{champ.ToLower()}.png");

                //await Context.Channel.SendFileAsync($"Resources/Runas/{champ}.png", $"{Context.User.Mention} Aí está, runas para `{champ}`");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Acho que você digitou o nome do campeão de forma errada...");
            }
        }

        // Não terminei isso...
        //[Command("Atualizar Runas")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //public async Task AtualizarRunas()
        //{
        //    if (Environment.MachineName == "DESKTOP-PKIN6P4")
        //    {
        //        if (!Directory.Exists("Resources/Runas/")) Directory.CreateDirectory("Resources/Runas/");
        //        await Context.Channel.SendMessageAsync(
        //            $"{Context.User.Mention} Iniciando atualização... Por favor aguarde isso deve demorar");

        //        //await Context.Channel.SendMessageAsync($"{Context.User.Mention}, runas atualizadas");
        //    }
        //    else
        //    {
        //        await Context.Channel.SendMessageAsync($"Desculpe {Context.User.Mention}, porém não é possível fazer isso agora.");
        //    }

        //}


    }
}

