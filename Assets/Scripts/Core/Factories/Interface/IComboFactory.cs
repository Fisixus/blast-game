using AYellowpaper.SerializedCollections;
using Core.GridElements.GridPawns.Combo;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IComboFactory: IFactory<Combo>
    {
        public SerializedDictionary<ComboType, ComboDataSO> ComboDataDict { get; }
        public Combo GenerateCombo(ComboType comboType, Vector2Int coord);
    }
}
