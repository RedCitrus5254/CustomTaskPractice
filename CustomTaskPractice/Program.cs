namespace CustomTaskPractice
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main()
        {
            var timerTaskRes = await GetIntAsync();

            Console.WriteLine($"res: {timerTaskRes}");
            
            async TimerTask<int> GetIntAsync()
            {
                Console.WriteLine("start getIni()");
                await Task.Delay(100);
                return 5;
            }
        }

        
    }
}