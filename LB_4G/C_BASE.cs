using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace LB_4G
{

    public partial class C_BASE
    {
        public C_4G[] Mes_antennes;
        public C_REGIONS[] Mes_Regions;

        public List<C_DEPS> Les_Deps;
       public List<C_VILLE> Les_Villes;


        public C_BASE()
        {
            string Data_Json = File.ReadAllText("Emetteurs_Reduits_2023.json");

            Mes_antennes = JsonSerializer.Deserialize<C_4G[]>(Data_Json);

            //C_4G[] freeAntennes = Array.FindAll(Mes_antennes, antenne => antenne.Adm == "FREE");

            Data_Json = File.ReadAllText("regions.json");

            Mes_Regions = JsonSerializer.Deserialize<C_REGIONS[]>(Data_Json);

            Data_Json = File.ReadAllText("departments.json");

            Les_Deps = JsonSerializer.Deserialize<List<C_DEPS>>(Data_Json);

            Data_Json = File.ReadAllText("cities.json");

            Les_Villes = JsonSerializer.Deserialize<List<C_VILLE>>(Data_Json);

          
        }
        public List<C_4G> Get_Just_Free(string P_CODE)
        {

            var FREE_ANTENNE = new List<C_4G>();

            for (int i = 0; i < Mes_antennes.Length; i++) {

                if ((Mes_antennes[i].Adm) == "FREE MOBILE" && Mes_antennes[i].CP == P_CODE) {
                    FREE_ANTENNE.Add(Mes_antennes[i]);
                }
            }

            return FREE_ANTENNE;
        }
        public List<C_4G> Get_Just_Orange(string P_CODE)
        {

            var Orange_Antenne = new List<C_4G>();

            for (int i = 0; i < Mes_antennes.Length; i++) {

                if ((Mes_antennes[i].Adm) == "ORANGE" && Mes_antennes[i].CP == P_CODE) {
                    Orange_Antenne.Add(Mes_antennes[i]);
                }
            }

            return Orange_Antenne;
        }
        public List<C_4G> Get_Just_SFR(string P_CODE)
        {
            
            var SFR_Antenne = new List<C_4G>();

            for (int i = 0; i < Mes_antennes.Length; i++) {

                if ((Mes_antennes[i].Adm) =="SFR" && Mes_antennes[i].CP == P_CODE) {
                    SFR_Antenne.Add(Mes_antennes[i]);
                }
            }
            return SFR_Antenne;
        }
        public  List<C_4G> Get_Just_BOUYGUES(string P_CODE)
        {

            var BOUYGUES_Antenne = new List<C_4G>();

            for (int i = 0; i < Mes_antennes.Length; i++) {

                if ((Mes_antennes[i].Adm) == "BOUYGUES TELECOM" && Mes_antennes[i].CP == P_CODE) {
                    BOUYGUES_Antenne.Add(Mes_antennes[i]);
                }
            }
            return BOUYGUES_Antenne;
        }
        public C_4G[] Get_All_Antennes()
        {
            return Mes_antennes;
        }
        public List<C_VILLE> Get_All_Villes_By_Code_Deps(string P_Code)
        {
            List<C_VILLE> Villes_trouves = new List<C_VILLE>();

            foreach (var une_ville in Les_Villes) {
                if (une_ville.department_code == P_Code) Villes_trouves.Add(une_ville);
            }
            return Villes_trouves;
        }
        

        public List<C_4G> Get_All_Antennes_By_Villes(string P_Code)
        {
            List<C_4G> Antennes_Trouve = new List<C_4G>();


            foreach (var une_ant in Mes_antennes) {
                if (une_ant.CP == P_Code) { Antennes_Trouve.Add(une_ant); }
            }

            return Antennes_Trouve;
        }

        public C_REGIONS[] Get_All_Regions()
        {
            return Mes_Regions;
        }

        public List<C_DEPS> Get_All_Deps_By_Code_Regions(string P_Code)
        {
            List<C_DEPS> Departement_Trouves = new List<C_DEPS>();

            foreach (var Un_dpt in Les_Deps) {
                if (Un_dpt.region_code == P_Code) { Departement_Trouves.Add(Un_dpt); }
            }
            return Departement_Trouves;
        }

       
    }
}
