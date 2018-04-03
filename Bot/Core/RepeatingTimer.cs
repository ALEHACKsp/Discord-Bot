using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using DPP_Bot.Core.Configs;

namespace DPP_Bot.Core
{
    internal static class RepeatingTimer
    {
        private static Timer _loopingTimer;
        //private static Timer _loopmsgTimer;
        private static SocketTextChannel _channel;

        internal static Task StartTimer()
        {
            _channel = Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog);

            //_loopmsgTimer = new Timer()
            //{
            //    Interval = 5000,
            //    AutoReset = true,
            //    Enabled = true
            //};
            //_loopmsgTimer.Elapsed += _loopmsgTimer_Elapsed;

            // Isso só foi implementado pra reiniciar a aplicação de hora em hora
            // Fizemos isso por causa da falta de memória da VPS na época...
            _loopingTimer = new Timer()
            {
                Interval = 3600000,
                AutoReset = false,
                Enabled = true
            };
            _loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        //private static void _loopmsgTimer_Elapsed(object sender, ElapsedEventArgs e)
        //{

        //}

        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            try
            {
                try
                {
                    await _channel.SendMessageAsync($"👥 {DateTime.Now:HH:mm:ss tt} - Membros na ultima hora: ```{Global.MembrosNaUltimaHora}```");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                await _channel.SendMessageAsync($"🔄 {DateTime.Now:HH:mm:ss tt} - Reiniciando...");

                await Global.Client.LogoutAsync();
                var fileName = Assembly.GetExecutingAssembly().Location;
                Process.Start(fileName);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}