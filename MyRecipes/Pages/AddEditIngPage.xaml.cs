using MyRecipes.DataBase;
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

namespace MyRecipes.Pages
{
    /// <summary>
    /// Interaction logic for AddEditIngPage.xaml
    /// </summary>
    public partial class AddEditIngPage : Page
    {
        public static Ingredient ingredient;

        public AddEditIngPage()
        {
        }

        public AddEditIngPage(Ingredient _ingridient)
        {
            InitializeComponent();
            ingredient = _ingridient;
            DataContext = ingredient;
            UnitCB.Items.Clear();
            UnitCB.ItemsSource = Connection.db.Unit.ToList();
            UnitCB.DisplayMemberPath = "Name";
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ingredient.Id == 0)
            {
                ingredient.Name = NameTB.Text;
                ingredient.Cost = decimal.Parse(CostTB.Text.Trim());
                ingredient.CostForCount = double.Parse(ToCountTB.Text);
                ingredient.UnitId = (UnitCB.SelectedItem as Unit).Id;
                ingredient.AvailableCount = int.Parse(CountInFrozTB.Text);
                Connection.db.Ingredient.Add(ingredient);
                Connection.db.SaveChanges();
                Navigation.NextPage(new IngridientPage());
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new IngridientPage());
        }
    }
}