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


            }
            catch (Exception P_Erreure) {
                MessageBox.Show(P_Erreure.Message, "ERREUR !", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

        }

        private void LST_REGIONS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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


               


                var ville_select = (C_VILLE)e.AddedItems[0];
                var Mes_Antennes = La_Base.Get_All_Antennes_By_Villes(ville_select.zip_code);

                LST_4G.Items.Clear();
                La_Carte.Children.Clear();



                foreach (var Une_Ant in Mes_Antennes) {
                    LST_4G.Items.Add(Une_Ant);


                    if (Une_Ant.XY.Length >= 2) {
                        float latitude = Une_Ant.XY[0];
                        float longitude = Une_Ant.XY[1];

                        Pushpin Mon_Point = new Pushpin();
                        Mon_Point.Location = new Location(latitude, longitude);
                        La_Carte.Children.Add(Mon_Point);
                    }
                }


               

                   
                



            }
        }

        private void Mode_Click(object sender, RoutedEventArgs e)
        {
        }



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
