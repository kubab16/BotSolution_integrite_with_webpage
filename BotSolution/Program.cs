using BotSolution.Bot;
using System;
using System.Threading.Tasks;

namespace BotSolution
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RunBot.start();
        }
    }
}