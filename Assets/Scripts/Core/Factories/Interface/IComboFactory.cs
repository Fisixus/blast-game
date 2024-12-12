using AYellowpaper.SerializedCollections;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Combo;
using UnityEngine;

namespace Core.Factories.Interface
{
    public interface IComboFactory: IFactory<Combo>
    {
        public SerializedDictionary<BoosterType, ComboDataSO> ComboDataDict { get; }
    }
}
