using MyRecipes.DataBase;
using MyRecipes.Pages;
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

namespace MyRecipes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation.main = this;
            MainFrame.Navigate(new DishesPage());
        }

        private void DishesBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new DishesPage());
        }

        private void IngridientBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new IngridientPage());
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            IEnumerable<Dish> filter = Connection.db.Dish;
            if (CategoryCB.SelectedIndex > 0)
            {
                filter = filter.Where(x => x.CategoryId == CategoryCB.SelectedIndex);
            }
            if (SearchTB.Text.Trim().Length > 0)
            {
                filter = filter.Where(x => x.Name.ToLower().StartsWith(SearchTB.Text.Trim().ToLower()));
            }
            Navigation.dishesPage.DishesList.ItemsSource = filter.ToList();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
