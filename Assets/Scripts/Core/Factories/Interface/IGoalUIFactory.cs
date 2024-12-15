using AYellowpaper.SerializedCollections;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.UI;

namespace Core.Factories.Interface
{
    public interface IGoalUIFactory : IFactory<GoalUI>
    {
        public SerializedDictionary<ObstacleType, GoalUIDataSO> GoalUIDataDict { get; }
    }
}