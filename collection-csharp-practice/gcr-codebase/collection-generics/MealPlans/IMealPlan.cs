namespace MealPlanGenerator
{
    /// <summary>
    /// Interface for meal plan types
    /// Demonstrates: Interfaces and Generic Constraints
    /// </summary>
    public interface IMealPlan
    {
        string GetMealType();
        string[] GetRestrictedIngredients();
        double GetCalorieTarget();
    }
}
