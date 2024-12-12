
using System;
using System.Collections.Generic;
using System.Linq;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Effect;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MVP.Presenters.Handlers.Strategies.Interface;
using UnityEngine;
using UTasks;
using Random = UnityEngine.Random;

namespace MVP.Presenters.Handlers
{
    public class BoosterHandler
    {
        private BaseGridObject[,] _grid;
        private readonly Dictionary<Enum, IBoosterStrategy> _boosterStrategies = new();
        
        public BoosterHandler(IEnumerable<IBoosterStrategy> boosterStrategies)
        {
            // Register strategies
            foreach (var strategy in boosterStrategies)
            {
                _boosterStrategies[strategy.Type] = strategy;
            }
        }
        
        public void Initialize(BaseGridObject[,] grid)
        {
            _grid = grid;
        }
        
        public BoosterType IsBoosterCreatable(List<BaseGridObject> matchedObjs)
        {
            foreach (var strategy in _boosterStrategies.Values.Where(strategy =>
                         strategy.CanCreateBooster(matchedObjs.Count)))
            {
                return (BoosterType)strategy.Type;
            }

            return BoosterType.None;
        }

        public async UniTask AnimateBoosterCreationAsync(Item centerItem, List<BaseGridObject> items)
        {
            const float durationPerAnimation = 0.2f;
            const float offsetMultiplier = 0.15f;
            const int highSortingOrder = 100;
            const int defaultSortingOrder = 0;
        
            // Create a list of tasks to animate all items
            var animationTasks = items.Select(matchedItem =>
                AnimateItemsGatheringAsync(centerItem, matchedItem, durationPerAnimation, offsetMultiplier, highSortingOrder)
            ).ToList();
        
            // Wait for all animations to complete
            await UniTask.WhenAll(animationTasks);
        
            // Reset sorting orders after animations
            items.ForEach(item => item.SetSortingOrder(defaultSortingOrder));
        }
        
        private async UniTask AnimateItemsGatheringAsync(Item centerItem, BaseGridObject matchedItem, float duration, float offsetMultiplier, int sortingOrder)
        {
            var direction = (matchedItem.transform.position - centerItem.transform.position).normalized;
            var effect = matchedItem.GetComponent<BaseGridObjectEffect>();
            matchedItem.SetSortingOrder(sortingOrder);
        
            // Calculate the intermediate and final positions
            var intermediatePosition = matchedItem.transform.position + direction * offsetMultiplier;
            var finalPosition = centerItem.transform.position;
        
            // Animate to intermediate position
            await effect.ShiftAsync(intermediatePosition, duration, Ease.OutQuad);
        
            // Animate to final position
            await effect.ShiftAsync(finalPosition, duration, Ease.InCubic);
        }
        
        public async UniTask<List<BaseGridObject>> ApplyBoostAsync(BaseGridObject finalBooster)
        {
            var effectedGridObjects = new List<BaseGridObject>();
            await ProcessBoostersAsync(finalBooster, effectedGridObjects);
            return effectedGridObjects;
        }

        private async UniTask ProcessBoostersAsync(BaseGridObject finalBooster, List<BaseGridObject> effectedGridObjects)
        {
            if (finalBooster == null) return;
            if (!_boosterStrategies.TryGetValue(finalBooster.Type, out var strategy))
                return;

            strategy.PlayExplosionEffect(finalBooster);
            AddToAffectedGridObjects(effectedGridObjects, finalBooster);
            finalBooster.gameObject.SetActive(false);

            var foundGridObjects = strategy.FindAffectedItems(_grid, finalBooster);

            // List to handle tasks for processing nested boosters
            List<UniTask> boosterTasks = new();

            foreach (var gridObject in foundGridObjects)
            {
                bool isAddable = AddToAffectedGridObjects(effectedGridObjects, gridObject);
                var waitTime = strategy.GetWaitTime();

                // Delay to simulate the interval between actions
                await UniTask.Delay(TimeSpan.FromSeconds(waitTime), DelayType.DeltaTime);

                // Skip if already added to the affected list
                if (!isAddable) continue;

                gridObject.gameObject.SetActive(false);
                switch (gridObject)
                {
                    case Booster booster:
                        // Process nested boosters asynchronously
                        boosterTasks.Add(ProcessBoostersAsync(booster, effectedGridObjects));
                        break;
                    case Obstacle obstacle:
                        //TODO:m_GoalHandler.UpdateGoal(obstacle);
                        break;
                }
            }

            // Await all booster tasks in parallel
            await UniTask.WhenAll(boosterTasks);
        }

        // Adds an object to the affected list if not already present; returns false if already added
        private bool AddToAffectedGridObjects(List<BaseGridObject> effectedGridObjects, BaseGridObject baseGridObject)
        {
            if (effectedGridObjects.Contains(baseGridObject)) return false;
            effectedGridObjects.Add(baseGridObject);
            return true;
        }
        
    }
}
