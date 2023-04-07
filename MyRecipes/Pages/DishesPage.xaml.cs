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
    /// Interaction logic for DishesPage.xaml
    /// </summary>
    public partial class DishesPage : Page
    {
        public DishesPage()
        {
            InitializeComponent();
            Navigation.dishesPage = this;
            Navigation.main.IngTitle.Visibility = Visibility.Collapsed;
            Navigation.main.MenuInMain.Visibility = Visibility.Visible;
            Navigation.main.TitleTB.Text = "Списко блюд";
            DishesList.ItemsSource = Connection.db.Dish.ToList();
            Category category = new Category();
            category.Name = "Все категории";
            List<Category> categories = Connection.db.Category.ToList();
            categories.Insert(0, category);
            Navigation.main.CategoryCB.ItemsSource = categories;
        }
    }
}
