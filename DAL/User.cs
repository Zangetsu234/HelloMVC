using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
    //static void Main(string[] args)
    //    {
    //        try
    //        {
    //            List<string> list = new List<string>();
    //            Console.WriteLine(list[0]);
    //            Console.ReadKey();
    //        }
    //        catch(Exception e)
    //        {
    //            Console.WriteLine("Error\n" + e.Message);
    //            Console.Read();
    //        }
    //    }