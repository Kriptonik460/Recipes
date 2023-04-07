using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyRecipes.DataBase
{
    class Navigation
    {
        public static MainWindow main;
        public static Pages.DishesPage dishesPage;

        public static void NextPage(Page page)
        {
            main.MainFrame.Navigate(page);
        }
    }
}
