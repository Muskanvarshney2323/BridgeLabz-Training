namespace MealPlanGenerator
{
    /// <summary>
    /// High-Protein meal plan
    /// </summary>
    public class HighProteinMeal : IMealPlan
    {
        public double ProteinTarget { get; set; } = 150; // grams

        public string GetMealType()
        {
            return "High-Protein";
        }

        public string[] GetRestrictedIngredients()
        {
            return new string[] { }; // No restricted ingredients
        }

        public double GetCalorieTarget()
        {
            return 2500; // calories per day
        }
    }
}
