using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.DataBase
{
    partial class Dish
    {
        public decimal CostDish
        {
            get
            {
                var allIngridients = CookingStage.SelectMany(x => x.IngredientOfStage).ToList();
                decimal totalSum = allIngridients.Sum(x => (decimal)x.Quantity * ((decimal)x.Ingredient.CostForCount / (decimal)x.Ingredient.Cost));
                decimal price = totalSum / ServingQuantity;

                return price;
            }
        }
    }
}
