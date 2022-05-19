using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace serialporttest
{
    class Program
    {
        static Func<string, bool> _check = x =>
        {
            if (x.Contains( "456"))
                return true;
            return false;
        };


        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Enter your data :");
                string indata = Console.ReadLine();

                var task = checkAsync(indata);

                Console.WriteLine(task.Result);
            }
        }

        static async Task<string> checkAsync(string message)
        {
            string res;

            Console.WriteLine("Start Check Message");
            var task = Task.Run(() => check(message, _check));
            Console.WriteLine("Waiting Check Message");

            if (await Task.WhenAny(task,Task.Delay(2000)) == task)
            {
                res = task.Result ? "Ok":"No";
            }
            else
            {
                res = "Timeout";
            }

            return res;
        }

        static bool check(string message,Func<string,bool> func)
        {
            Thread.Sleep(1000);
            if (func(message))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
