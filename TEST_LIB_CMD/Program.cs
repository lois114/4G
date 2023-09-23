using LB_4G;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_LIB_CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try {
                C_BASE La_Base = new C_BASE();

                C_4G[] Mes_Antennes = La_Base.Get_All_Antennes();

                //Console.WriteLine(Mes_Antennes.Length);

                //foreach (var une_antenne in Mes_Antennes) {
                //    Console.WriteLine(une_antenne);
                //}

              

            } 
            catch (Exception e) {

                Console.WriteLine("erreur" + e);
            }
        }
    }
}
