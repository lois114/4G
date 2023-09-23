using LB_4G;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using System.Reflection;

namespace IHM_RESEAU
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string CLE_MAP = "AjiQzqXyD4elNI_EzQW-U3wxfWVoUS6f7X6jhMvNL9047jfEopnKmelQrgstm_5K";
        C_BASE La_Base;
        public MainWindow()
        {
            InitializeComponent();
            try {
                La_Base = new C_BASE();

                var Les_Regions = La_Base.Get_All_Regions();

                foreach (var Une_Region in Les_Regions) {
                    LST_REGIONS.Items.Add(Une_Region);
                }

                La_Carte.CredentialsProvider = new ApplicationIdCredentialsProvider(CLE_MAP);
                La_Carte.Mode = new RoadMode();
                La_Carte.Center = new Location(43.837009, 4.370130);
                La_Carte.ZoomLevel = 12;

                //Pushpin point = new Pushpin();
                //point.Location = new Location(43.837009, 4.370130);
                //La_Carte.Children.Add(point);
                SFR_B.IsEnabled = false;
                ORANGE_B.IsEnabled = false;
                BOUYGUES_B.IsEnabled = false;
                FREE_B.IsEnabled = false;
                ALL_B.IsEnabled = false;

            }
            catch (Exception P_Erreure) {
                MessageBox.Show(P_Erreure.Message, "ERREUR !", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

        }

        private void LST_REGIONS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LST_4G.Items.Clear();
            LST_VILLE.Items.Clear();
            if (e.AddedItems.Count > 0) {
                var Region_Selectionne = (C_REGIONS)e.AddedItems[0];
                var Mes_Departements = La_Base.Get_All_Deps_By_Code_Regions(Region_Selectionne.code);
                LST_DEP.Items.Clear();
                foreach (var Un_Departement in Mes_Departements) {
                    LST_DEP.Items.Add(Un_Departement);
                }
            }
        }

        private void LST_DEP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LST_4G.Items.Clear();
           
            if (e.AddedItems.Count > 0) {
                var Dep_select = (C_DEPS)e.AddedItems[0];
                var Mes_Villes = La_Base.Get_All_Villes_By_Code_Deps(Dep_select.code);
                LST_VILLE.Items.Clear();
                foreach (var Une_ville in Mes_Villes) {
                    LST_VILLE.Items.Add(Une_ville);
                }
            }
        }

        

        private void LST_VILLE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0) {

                Pushpin Ville = new Pushpin();
                float Lat = (e.AddedItems[0] as C_VILLE).gps_lat;
                float Lng = (e.AddedItems[0] as C_VILLE).gps_lng;
                Ville.Location = new Location(Lat, Lng);
                La_Carte.Center = new Location(Lat, Lng);
                La_Carte.Children.Add(Ville);


                SFR_B.IsEnabled = true;
                ORANGE_B.IsEnabled = true;
                BOUYGUES_B.IsEnabled = true;
                FREE_B.IsEnabled = true;
                ALL_B.IsEnabled = true;

                var ville_select = (C_VILLE)e.AddedItems[0];
                var Mes_Antennes = La_Base.Get_All_Antennes_By_Villes(ville_select.zip_code);

                LST_4G.Items.Clear();
                La_Carte.Children.Clear();


                int index = 0;
                foreach (var Une_Ant in Mes_Antennes) {
                    LST_4G.Items.Add(Une_Ant);
                   

                    if (Une_Ant.XY.Length >= 2) {
                        float latitude = Une_Ant.XY[0];
                        float longitude = Une_Ant.XY[1];

                        Pushpin Mon_Point = new Pushpin();
                        Mon_Point.Location = new Location(latitude, longitude);
                        Choix_Couleur_Pin(Mon_Point, Mes_Antennes,index);
                        La_Carte.Children.Add(Mon_Point);

                        index++;
                    }
                }


               

                   
                



            }
        }

      

        private void Mode_Aerial_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked == true) {
                
                La_Carte.Mode = new AerialMode();

               
            } else La_Carte.Mode = new RoadMode();
            ;
        }


     



        private void Choix_Couleur_Pin(Pushpin P_Point, List<C_4G> P_Liste_Emetteurs, int P_Index)
        {
            if (P_Liste_Emetteurs[P_Index].Adm == "SFR") {
                P_Point.Background = new SolidColorBrush(Colors.DarkGreen);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "ORANGE") {
                P_Point.Background = new SolidColorBrush(Colors.Orange);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "DIGICEL") {
                P_Point.Background = new SolidColorBrush(Colors.White);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "FREE MOBILE") {
                P_Point.Background = new SolidColorBrush(Colors.Red);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "SPM TELECOM") {
                P_Point.Background = new SolidColorBrush(Colors.WhiteSmoke);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "PMT/VODAFONE") {
                P_Point.Background = new SolidColorBrush(Colors.Gray);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "DAUPHIN TELECOM") {
                P_Point.Background = new SolidColorBrush(Colors.CadetBlue);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "BOUYGUES TELECOM") {
                P_Point.Background = new SolidColorBrush(Colors.Blue);
            }
            if (P_Liste_Emetteurs[P_Index].Adm == "OUTREMER TELECOM") {
                P_Point.Background = new SolidColorBrush(Colors.Violet);
            }
        }

       

        private void SFR_B_Click(object sender, RoutedEventArgs e)
        {
            C_VILLE Laville = (C_VILLE)LST_VILLE.SelectedItem;

            LST_4G.Items.Clear();
            var SFR = La_Base.Get_Just_SFR(Laville.zip_code);
            La_Carte.Children.Clear();
            int index = 0;
            foreach (var Une_Ant in SFR) {

                LST_4G.Items.Add(Une_Ant);
               
                if (Une_Ant.XY.Length >= 2) {
                    float latitude = Une_Ant.XY[0];
                    float longitude = Une_Ant.XY[1];
                   
                    Pushpin Mon_Point = new Pushpin();
                    Choix_Couleur_Pin(Mon_Point, SFR, index);
                    Mon_Point.Location = new Location(latitude, longitude);

                    La_Carte.Children.Add(Mon_Point);
                }
                    index++;
            }
        }

        private void ORANGE_B_Click(object sender, RoutedEventArgs e)
        {
            C_VILLE Laville = (C_VILLE)LST_VILLE.SelectedItem;
            La_Carte.Children.Clear();
            LST_4G.Items.Clear();
            var Orange = La_Base.Get_Just_Orange(Laville.zip_code);
            int index = 0;
            foreach (var Une_Ant in Orange) {

                LST_4G.Items.Add(Une_Ant);
                     if (Une_Ant.XY.Length >= 2) {
                    float latitude = Une_Ant.XY[0];
                    float longitude = Une_Ant.XY[1];

                    Pushpin Mon_Point = new Pushpin();
                    Choix_Couleur_Pin(Mon_Point, Orange, index);
                    Mon_Point.Location = new Location(latitude, longitude);

                    La_Carte.Children.Add(Mon_Point);
                }
                     index++;
            }
        }

      

        private void BOUYGUES_B_Click(object sender, RoutedEventArgs e)
        {
            C_VILLE Laville = (C_VILLE)LST_VILLE.SelectedItem;
            La_Carte.Children.Clear();
            LST_4G.Items.Clear();
            var Bouygues = La_Base.Get_Just_BOUYGUES(Laville.zip_code);
            int index = 0;

            foreach (var Une_Ant in Bouygues) {

                LST_4G.Items.Add(Une_Ant);
                if (Une_Ant.XY.Length >= 2) {
                    float latitude = Une_Ant.XY[0];
                    float longitude = Une_Ant.XY[1];

                    Pushpin Mon_Point = new Pushpin();
                    Choix_Couleur_Pin(Mon_Point, Bouygues, index);

                    Mon_Point.Location = new Location(latitude, longitude);

                    La_Carte.Children.Add(Mon_Point);
                }
                index++;
            }
        }

        private void FREE_B_Click(object sender, RoutedEventArgs e)
        {
            C_VILLE Laville = (C_VILLE)LST_VILLE.SelectedItem;
            La_Carte.Children.Clear();
            LST_4G.Items.Clear();
            var FREE = La_Base.Get_Just_Free(Laville.zip_code);
            int index = 0;
            foreach (var Une_Ant in FREE) {

                LST_4G.Items.Add(Une_Ant);
                if (Une_Ant.XY.Length >= 2) {
                    float latitude = Une_Ant.XY[0];
                    float longitude = Une_Ant.XY[1];

                    Pushpin Mon_Point = new Pushpin();
                    Choix_Couleur_Pin(Mon_Point, FREE, index);

                    Mon_Point.Location = new Location(latitude, longitude);

                    La_Carte.Children.Add(Mon_Point);
                }
                index++;
            }
        }

        private void ALL_B_Click(object sender, RoutedEventArgs e)
        {
            C_VILLE Laville = (C_VILLE)LST_VILLE.SelectedItem;
            La_Carte.Children.Clear();
            LST_4G.Items.Clear();
            var Mes_Antennes = La_Base.Get_All_Antennes_By_Villes(Laville.zip_code);
            int index = 0;
            foreach (var Une_Ant in Mes_Antennes) {

                LST_4G.Items.Add(Une_Ant);
                if (Une_Ant.XY.Length >= 2) {
                    float latitude = Une_Ant.XY[0];
                    float longitude = Une_Ant.XY[1];

                    Pushpin Mon_Point = new Pushpin();
                    Mon_Point.Location = new Location(latitude, longitude);
                    Choix_Couleur_Pin(Mon_Point, Mes_Antennes, index);
                    La_Carte.Children.Add(Mon_Point);

                    index++;
                }
            }
        }

        // TEST CHECK BOX 
        //private void SFR_Click_1(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as CheckBox).IsChecked == true) {

        //        C_VILLE Laville = (C_VILLE)LST_VILLE.SelectedItem;

        //        LST_4G.Items.Clear();
        //        var SFR = La_Base.Get_Just_SFR(Laville.zip_code);
        //        foreach (var Une_Ant in SFR) {

        //            LST_4G.Items.Add(Une_Ant);
        //        }

        //    } else {

        //        C_VILLE Laville = (C_VILLE)LST_VILLE.SelectedItem;
        //        var mes_antennes = La_Base.Get_All_Antennes_By_Villes(Laville.zip_code);
        //        foreach (var Une_Ant in mes_antennes) {

        //            LST_4G.Items.Add(Une_Ant);
        //        }




        //    }
        //}


        //private void LST_4G_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count > 0) {

        //        var Antenne_select = (C_4G)e.AddedItems[0];

        //        gps_lat.Content = $"LAT  {Antenne_select.XY[0]}";
        //        gps_long.Content = $"LNG {Antenne_select.XY[1]}";




        //    }
        //}
    }
}
