public interface IFitnessLeaderboard
{
    void AddUser(string name, int steps);
    void UpdateSteps(string name, int steps);
    void DisplayLeaderboard();
}
