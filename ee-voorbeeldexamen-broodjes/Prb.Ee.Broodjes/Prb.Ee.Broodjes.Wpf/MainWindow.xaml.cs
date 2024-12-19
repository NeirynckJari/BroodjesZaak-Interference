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

namespace Prb.Ee.Broodjes.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum BreadType { Wit, Grijs, Volkoren };
        enum Topping { Hesp, Kaas, Groentjes, GeenBeleg };

        Dictionary<string, List<Enum>> order = new Dictionary<string, List<Enum>>();
        Dictionary<Enum, decimal> prices = new Dictionary<Enum, decimal>();

        public MainWindow()
        {
            InitializeComponent();
            SetPrices();
            PopulateComboBox();
        }

        void SetPrices()
        {
            prices.Clear();
            prices.Add(BreadType.Wit, 0.5M);
            prices.Add(BreadType.Grijs, 0.6M);
            prices.Add(BreadType.Volkoren, 0.7M);

            prices.Add(Topping.Hesp, 1.0M);
            prices.Add(Topping.Kaas, 0.7M);
            prices.Add(Topping.Groentjes, 2.0M);
            prices.Add(Topping.GeenBeleg, 1.0M);
        }

        void PopulateComboBox()
        {
            cmbBreadType.Items.Clear();
            foreach (BreadType type in Enum.GetValues<BreadType>())
            {
                cmbBreadType.Items.Add(type);
            }
            cmbBreadType.SelectedIndex = 0;
        }

        void CheckAndAddToppings(string name)
        {
            if (chkHesp.IsChecked == false && chkKaas.IsChecked == false && chkGroentjes.IsChecked == false)
            {
                order[name].Add(Topping.GeenBeleg);
            }
            else
            {
                if (chkHesp.IsChecked == true)
                {
                    order[name].Add(Topping.Hesp);
                }
                else if (chkKaas.IsChecked == true)
                {
                    order[name].Add(Topping.Kaas);
                }
                else if (chkGroentjes.IsChecked == true)
                {
                    order[name].Add(Topping.Groentjes);
                }
            }
        }

        void AddOrder(string name, BreadType breadType)
        {
            order.Add(name, new List<Enum>());
            order[name].Add(breadType);
            CheckAndAddToppings(name);
        }

        void UpdateOverviewListBox(string customerName, Dictionary<string, List<Enum>> order)
        {
            lstOverview.Items.Clear();
            lstOverview.Items.Add("Favoriete klant");
            lstOverview.Items.Add($"{customerName}");
            if (order.ContainsKey(customerName))
            {
                lstOverview.Items.Add($"{order[customerName][0]} broodje");
            }
            lstOverview.Items.Add("Gekozen Toppings:");

            //het probleem bevind zich hier onder, in mijn logica (momenteel) lijkt mij dit OK. Voor iedere Enum Topping in order met die customername
            //display de Toppings die we eerder toevoegde met CheckAndAddToppings
            //als je GEEN topping aanvinkt zal hij op basis van de combobox index toch toppings gaan toevoegen

            if (order.ContainsKey(customerName))
            {
                foreach (Topping topping in order[customerName]) 
                {
                    lstOverview.Items.Add(topping);
                }
            }
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            string customerName = txtName.Text;
            BreadType type = (BreadType)cmbBreadType.SelectedItem;
            AddOrder(customerName, type);
            UpdateOverviewListBox(customerName, order);
        }

        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            lstOverview.Items.Clear();
        }
    }
}
