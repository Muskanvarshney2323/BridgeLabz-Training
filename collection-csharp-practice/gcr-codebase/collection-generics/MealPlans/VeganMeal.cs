namespace MealPlanGenerator
{
    /// <summary>
    /// Vegan meal plan
    /// </summary>
    public class VeganMeal : IMealPlan
    {
        public double ProteinTarget { get; set; } = 60; // grams

        public string GetMealType()
        {
            return "Vegan";
        }

        public string[] GetRestrictedIngredients()
        {
            return new string[] { "Meat", "Fish", "Poultry", "Dairy", "Eggs", "Honey" };
        }

        public double GetCalorieTarget()
        {
            return 2100; // calories per day
        }
    }
}
