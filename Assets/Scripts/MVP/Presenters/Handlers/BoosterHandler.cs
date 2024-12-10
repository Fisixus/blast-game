
using System;
using System.Collections.Generic;
using System.Linq;
using Core.GridElements.Enums;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Effect;
using DG.Tweening;
using MVP.Presenters.Handlers.Strategies.Interface;
using UTasks;
using Random = UnityEngine.Random;

namespace MVP.Presenters.Handlers
{
    public class BoosterHandler
    {
        private BaseGridObject[,] _grid;
        private readonly Dictionary<BoosterType, IBoosterStrategy> _boosterStrategies = new();
        
        public BoosterHandler(IEnumerable<IBoosterStrategy> boosterStrategies)
        {
            // Register strategies
            foreach (var strategy in boosterStrategies)
            {
                _boosterStrategies[strategy.BoosterType] = strategy;
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
                // Handle randomness for horizontal/vertical rockets
                if (strategy.BoosterType is BoosterType.RocketHorizontal or BoosterType.RocketVertical)
                {
                    return (BoosterType)Random.Range((int)BoosterType.RocketHorizontal,
                        (int)BoosterType.RocketVertical + 1);
                }

                return strategy.BoosterType;
            }

            return BoosterType.None;
        }

        public void AnimateBoosterCreation(Item centerItem, List<BaseGridObject> items, Action<Item> onComplete)
        {
            const float durationPerAnimation = 0.2f;
            const float offsetMultiplier = 0.15f;
            const int highSortingOrder = 100;
            const int defaultSortingOrder = 0;

            foreach (var matchedItem in items)
            {
                AnimateItemsGathering(centerItem, matchedItem, durationPerAnimation, offsetMultiplier,
                    highSortingOrder);
            }

            // Wait for the animations to complete before resetting and invoking callback
            UTask.Wait(durationPerAnimation * 2 + 0.05f).Do(() =>
            {
                items.ForEach(item => { item.SetSortingOrder(defaultSortingOrder); });
                onComplete?.Invoke(centerItem);
            });
        }
        private void AnimateItemsGathering(Item centerItem, BaseGridObject matchedItem, float duration, float offsetMultiplier,
            int sortingOrder)
        {
            var direction = (matchedItem.transform.position - centerItem.transform.position).normalized;
            var effect = matchedItem.GetComponent<BaseGridObjectEffect>();
            matchedItem.SetSortingOrder(sortingOrder);
            // Calculate the intermediate and final positions
            var intermediatePosition = matchedItem.transform.position + direction * offsetMultiplier;
            var finalPosition = centerItem.transform.position;

            // Animate to intermediate position first
            effect.Shift(intermediatePosition, duration, Ease.OutQuad)
                .OnComplete(() =>
                {
                    // Animate to final position after the first animation completes
                    effect.Shift(finalPosition, duration, Ease.InCubic);
                });
        }
        
    }
}
