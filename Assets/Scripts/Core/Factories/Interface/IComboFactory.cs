using AYellowpaper.SerializedCollections;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IComboFactory : IFactory<Combo>
    {
        public SerializedDictionary<ComboType, ComboDataSO> ComboDataDict { get; }
        public Combo GenerateCombo(ComboType comboType, Vector2Int coord);
    }
}