namespace MealPlanGenerator
{
    /// <summary>
    /// Vegetarian meal plan
    /// </summary>
    public class VegetarianMeal : IMealPlan
    {
        public double ProteinTarget { get; set; } = 50; // grams

        public string GetMealType()
        {
            return "Vegetarian";
        }

        public string[] GetRestrictedIngredients()
        {
            return new string[] { "Meat", "Fish", "Poultry" };
        }

        public double GetCalorieTarget()
        {
            return 2000; // calories per day
        }
    }
}
