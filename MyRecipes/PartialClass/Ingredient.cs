using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyRecipes.DataBase
{
    partial class Ingredient
    {
        public string CostOFUnit
        {
            get
            {
                return $"{Cost:N0} руб. за {CostForCount} {Unit.Name}";
            }
        }
        public double Price => (double)Cost / CostForCount;

        public SolidColorBrush PriceColor
        {
            get
            {
                if (Cost <= 60)
                {
                    return Brushes.MediumSpringGreen;
                }
                else if (Cost <= 200)
                {
                    return Brushes.LightSkyBlue;
                }
                else
                {
                    return Brushes.Red;
                }
            }
        }
    }
}
