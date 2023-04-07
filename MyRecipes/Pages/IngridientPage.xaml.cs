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
    /// Interaction logic for IngridientPage.xaml
    /// </summary>
    public partial class IngridientPage : Page
    {
        private decimal sum;

        public int TotalNumberPages // общее количество страниц 
        {
            get { return (int)GetValue(TotalNumberPagesProperty); }
            set { SetValue(TotalNumberPagesProperty, value); }
        }

        public static readonly DependencyProperty TotalNumberPagesProperty =
            DependencyProperty.Register("TotalNumberPages", typeof(int), typeof(IngridientPage));

        public int NumberPage // номер страницы на которой находится пользователь
        {
            get { return (int)GetValue(NumberPageProperty); }
            set { SetValue(NumberPageProperty, value); }
        }

        public static readonly DependencyProperty NumberPageProperty =
            DependencyProperty.Register("NumberPage", typeof(int), typeof(IngridientPage));

        public IEnumerable<Ingredient> Ingredient // все записи
        {
            get { return (IEnumerable<Ingredient>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Ingredient", typeof(IEnumerable<Ingredient>), typeof(IngridientPage));

        private int CountEntriestOnPage = 5; // количество записей на одной странице

        public int CountIngredient
        {
            get { return (int)GetValue(CountIngredientProperty); }
            set { SetValue(CountIngredientProperty, value); }
        }

        public static readonly DependencyProperty CountIngredientProperty =
           DependencyProperty.Register("CountIngredient", typeof(int), typeof(MainWindow));

        private IEnumerable<Ingredient> TestIEnumerableIngredients;

        public IngridientPage()
        {
            NumberPage = 1;
            TotalNumberPages = 0;

            Ingredient = Connection.db.Ingredient.ToList();
            TestIEnumerableIngredients = Ingredient;

            ValidateCountIngridient();
            ValidateTotalCountPage();
            PageProcessing();

            InitializeComponent();

            Navigation.main.MenuInMain.Visibility = Visibility.Collapsed;
            Navigation.main.IngTitle.Visibility = Visibility.Visible;
            Navigation.main.CountIngTb.Text = $"{Connection.db.Ingredient.ToList().Count()} наименований";

            var ings = Connection.db.Ingredient.ToList();

            foreach (var item in ings)
            {
                sum += item.Cost / (decimal)item.CostForCount * (decimal)item.AvailableCount;
            }
            Navigation.main.SumIngTb.Text = $"Запасов в холодильнике на сумму: {sum:N0}";
        }

        private void AddMenuBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new AddEditIngPage(new Ingredient()));
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var sel = (sender as TextBlock).DataContext as Ingredient;
            Navigation.NextPage(new AddEditIngPage(sel));
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var sel = (sender as TextBlock).DataContext as Ingredient;
            Connection.db.Ingredient.Remove(sel);
            Connection.db.SaveChanges();
            NumberPage = 1;
            TotalNumberPages = 0;

            Ingredient = Connection.db.Ingredient.ToList();
            TestIEnumerableIngredients = Ingredient;

            ValidateCountIngridient();
            ValidateTotalCountPage();
            PageProcessing();

            InitializeComponent();
        }
        #region Кнопки перехода по страницам 
        private void CountEntriestComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateTotalCountPage();

            if (NumberPage >= TotalNumberPages)
                NumberPage = TotalNumberPages;

            PageProcessing();
        }

        private void FirstListBTN_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 1;

            PageProcessing();
        }

        private void BackListBTN_Click(object sender, RoutedEventArgs e)
        {
            if ((NumberPage - 1) <= 0)
                return;

            NumberPage--;

            PageProcessing();
        }

        private void NextListBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TotalNumberPages <= NumberPage)
                return;

            NumberPage++;

            PageProcessing();
        }

        private void LastListBTN_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = TotalNumberPages;

            PageProcessing();
        }

        private void OneBtn_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 1;

            PageProcessing();
        }

        private void TwoBtn_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 2;

            PageProcessing();
        }

        private void ThreeBtn_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 3;

            PageProcessing();
        }

        private void FourBtn_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 4;

            PageProcessing();
        }
        private void FifeBtn_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 5;

            PageProcessing();
        }
        #endregion

        #region Методы для страниц
        private void PageProcessing()
        {
            Ingredient = TestIEnumerableIngredients;

            Ingredient = Ingredient.Cast<Ingredient>()
                                   .Skip((NumberPage - 1) * CountEntriestOnPage)
                                   .Take(CountEntriestOnPage);
        }

        private void ValidateCountIngridient()
        {
            CountIngredient = TestIEnumerableIngredients.Count();
        }

        private void ValidateTotalCountPage()
        {
            TotalNumberPages = (int)Math.Ceiling(Convert.ToDouble(TestIEnumerableIngredients.Cast<Ingredient>().Count()) / Convert.ToDouble(CountEntriestOnPage));
        }
        #endregion
    }
}
