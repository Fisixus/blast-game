using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.Enum;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Factories
{
    public class BoosterFactory : ObjectFactory<Booster>, IBoosterFactory
    {
        [field: SerializeField]
        [SerializedDictionary("Booster Type", "Booster Data")]
        public SerializedDictionary<BoosterType, BoosterDataSO> BoosterDataDict { get; private set; }

        private List<Booster> _allBoosters;
        
        public override void Bind()
        {
            base.Bind();
            Pool.Initialize(ObjPrefab, ParentTr, 8); // Initialize the pool
            _allBoosters = new List<Booster>(8);
        }
        
        public override void DestroyObj(Booster emptyBooster)
        {
            base.DestroyObj(emptyBooster);
            emptyBooster.SetAttributes(-Vector2Int.one, BoosterType.None, true);
            _allBoosters.Remove(emptyBooster);
        }

        public override Booster CreateObj()
        {
            var booster = base.CreateObj();
            _allBoosters.Add(booster);
            return booster;
        }

        public void DestroyAllBoosters()
        {
            var boostersToDestroy = new List<Booster>(_allBoosters);
            base.DestroyObjs(boostersToDestroy);
            _allBoosters.Clear();
        }

        public Booster GenerateBooster(BoosterType boosterType, Vector2Int coord)
        {
            var booster = CreateObj();
            //TODO:booster.GetComponent<BoosterEffect>().PlayBoosterCreationParticle();
            booster.SetAttributes(coord, boosterType, false);
            booster.ApplyBoosterData(BoosterDataDict[boosterType]);
            return booster;
        }
    }
}
