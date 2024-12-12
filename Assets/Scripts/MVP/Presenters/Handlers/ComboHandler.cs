using System;
using System.Collections.Generic;
using System.Linq;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Combo;
using Core.GridElements.GridPawns.Effect;
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
        
        public Combo MergeBoosters(List<Booster> boosters, Vector2Int clickedPos)
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
                    return _comboFactory.GenerateCombo(comboType, clickedPos);
                }
            }

            // If no combo type is matched, return null or handle accordingly
            return null;
        }
        

        public void AnimateComboCreation(Item centerItem, List<BaseGridObject> boosters, Action<Item> onComplete)
        {
            const float durationPerAnimation = 0.2f;
            const float offsetMultiplier = 0.15f;
            const int highSortingOrder = 100;
            const int defaultSortingOrder = 0;

            foreach (var matchedBooster in boosters)
            {
                AnimateComboCreation(centerItem, matchedBooster, durationPerAnimation, offsetMultiplier,
                    highSortingOrder);
            }

            // Wait for the animations to complete before resetting and invoking callback
            UTask.Wait(durationPerAnimation * 2 + 0.05f).Do(() =>
            {
                boosters.ForEach(item => { item.SetSortingOrder(defaultSortingOrder); });
                onComplete?.Invoke(centerItem);
            });
        }
        private void AnimateComboCreation(Item centerItem, BaseGridObject matchedItem, float duration, float offsetMultiplier,
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
