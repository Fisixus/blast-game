using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Factories.Interface;
using Core.Factories.Pools;
using Core.GridElements.Data;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Effect;
using UnityEngine;

namespace Core.Factories
{
    public class BoosterFactory : ObjectFactory<Booster>, IBoosterFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Booster Type", "Booster Data")]
        public SerializedDictionary<BoosterType, BoosterDataSO> BoosterDataDict { get; private set; }

        
        public override void PreInitialize()
        {
            Pool = new ObjectPool<Booster>(ObjPrefab, ParentTr, 8);
        }
        
        public override void DestroyObj(Booster emptyBooster)
        {
            base.DestroyObj(emptyBooster);
            emptyBooster.SetAttributes(-Vector2Int.one, BoosterType.None);
        }

        public override Booster CreateObj()
        {
            var booster = base.CreateObj();
            return booster;
        }

        public Booster GenerateBooster(BoosterType boosterType, Vector2Int coord, bool isEffectOn = false)
        {
            var booster = CreateObj();
            if(isEffectOn)
                booster.GetComponent<BoosterEffect>().PlayBoosterCreationParticle();
            booster.SetAttributes(coord, boosterType);
            booster.ApplyData(BoosterDataDict[boosterType]);
            return booster;
        }
        
        
    }
}
