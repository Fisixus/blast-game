using AYellowpaper.SerializedCollections;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns.Combo;
using Core.GridObjectsData;
using UnityEngine;

namespace Core.Factories
{
    public class ComboFactory : ObjectFactory<Combo>, IComboFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Combo Type", "Combo Data")]
        public SerializedDictionary<ComboType, ComboDataSO> ComboDataDict { get; private set; }

        
        public override void PreInitialize()
        {
            Pool = new ObjectPool<Combo>(ObjPrefab, ParentTr, 2);
        }

        public Combo GenerateCombo(ComboType comboType, Vector2Int coord)
        {
            var combo = CreateObj();
            combo.SetAttributes(coord, comboType);
            combo.ApplyData(ComboDataDict[comboType]);
            return combo;
        }
    }
}