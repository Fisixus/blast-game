using System;
using System.Collections.Generic;
using System.Linq;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Combo;
using Core.GridElements.GridPawns.Effect;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UTasks;

namespace MVP.Presenters.Handlers
{
    public class ComboHandler
    {
        private readonly IComboFactory _comboFactory;
        
        public ComboHandler(IComboFactory comboFactory)
        {
            _comboFactory = comboFactory;
        }
        
        public ComboType MergeBoosters(List<Booster> boosters)
        {
            if (boosters == null || !boosters.Any())
                throw new ArgumentException("Boosters collection cannot be null or empty.");

            foreach (var kv in _comboFactory.ComboDataDict)
            {
                var comboType = kv.Key;
                var requiredIngredients = kv.Value.BoosterIngredients;

                // Check if all required ingredients are present in the provided boosters
                if (requiredIngredients.All(ingredient => boosters.Any(booster => booster.BoosterType == ingredient)))
                {
                    // Return a new Booster of the selected combo type
                    return comboType;
                }
            }

            // If no combo type is matched, return null or handle accordingly
            return ComboType.None;
        }
        

        public async UniTask AnimateComboCreationAsync(Booster centerBooster, IEnumerable<BaseGridObject> boosters)
        {
            const float durationPerAnimation = 0.2f;
            const float offsetMultiplier = 0.15f;
            const int highSortingOrder = 100;
            const int defaultSortingOrder = 0;

            var matchedObjs = boosters.ToList();

            // Animate each booster
            var animationTasks = matchedObjs.Select(matchedBooster =>
                AnimateComboCreationAsync(centerBooster, matchedBooster, durationPerAnimation, offsetMultiplier, highSortingOrder)
            ).ToList();

            // Wait for all animations to complete
            await UniTask.WhenAll(animationTasks);

            // Reset sorting orders after animations
            matchedObjs.ForEach(item => { item.SetSortingOrder(defaultSortingOrder); });
        }

        private async UniTask AnimateComboCreationAsync(Booster centerBooster, BaseGridObject matchedItem, float duration, float offsetMultiplier, int sortingOrder)
        {
            var direction = (matchedItem.transform.position - centerBooster.transform.position).normalized;
            var effect = matchedItem.GetComponent<BaseGridObjectEffect>();
            matchedItem.SetSortingOrder(sortingOrder);

            // Calculate the intermediate and final positions
            var intermediatePosition = matchedItem.transform.position + direction * offsetMultiplier;
            var finalPosition = centerBooster.transform.position;

            // Animate to intermediate position
            await effect.ShiftAsync(intermediatePosition, duration, Ease.OutQuad);

            // Animate to final position
            await effect.ShiftAsync(finalPosition, duration, Ease.InCubic);
        }

    }
}
