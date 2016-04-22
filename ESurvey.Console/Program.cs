using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.BL.Concrete;
using ESurvey.DAL.Concrate;
using System;
using ESurvey.Entity;

namespace ESurvey.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            var man  = new SurveyAccessManager();

            var token1 = SurveyAccessManager.GenerateToken("admindapekw", "asldsapdldd", "127.16sdladl8.0.1", "cas", DateTime.Now.Ticks);
            var token2 = SurveyAccessManager.GenerateToken("asdsdadass", "asdd", "127.168.0.1", "chrome", DateTime.Now.Ticks);

            System.Console.WriteLine(token1);
            System.Console.WriteLine(token2);
            
            System.Console.WriteLine(token1.Length);
            System.Console.WriteLine(token2.Length);

            System.Console.ReadKey();
        }
    }
}
