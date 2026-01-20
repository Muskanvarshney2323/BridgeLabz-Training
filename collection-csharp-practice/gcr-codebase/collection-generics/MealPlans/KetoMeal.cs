namespace MealPlanGenerator
{
    /// <summary>
    /// Keto meal plan
    /// </summary>
    public class KetoMeal : IMealPlan
    {
        public double ProteinTarget { get; set; } = 100; // grams
        public double FatTarget { get; set; } = 150; // grams

        public string GetMealType()
        {
            return "Keto";
        }

        public string[] GetRestrictedIngredients()
        {
            return new string[] { "Rice", "Bread", "Pasta", "Sugar", "Fruits (High Carb)" };
        }

        public double GetCalorieTarget()
        {
            return 1800; // calories per day
        }
    }
}
