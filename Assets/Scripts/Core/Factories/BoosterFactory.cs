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

        private List<Booster> _allBoosters;
        
        public override void PreInitialize()
        {
            Pool = new ObjectPool<Booster>(ObjPrefab, ParentTr, 8);
            _allBoosters = new List<Booster>(8);
        }
        
        public override void DestroyObj(Booster emptyBooster)
        {
            base.DestroyObj(emptyBooster);
            emptyBooster.SetAttributes(-Vector2Int.one, BoosterType.None);
            _allBoosters.Remove(emptyBooster);
        }

        public override Booster CreateObj()
        {
            var booster = base.CreateObj();
            _allBoosters.Add(booster);
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

        public void DestroyAllBoosters()
        {
            var boostersToDestroy = new List<Booster>(_allBoosters);
            base.DestroyObjs(boostersToDestroy);
            _allBoosters.Clear();
        }
        
    }
}
