using AYellowpaper.SerializedCollections;
using Core.GridElements.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.ProbabilityData
{
    [CreateAssetMenu(fileName = "ProbabilityDistribution_00", menuName = "Probabilities/New Item Distribution")]
    public class ItemSpawnProbabilityDistributionSO : ScriptableObject
    {
        [FormerlySerializedAs("m_ItemDataDict")] [SerializeField] [SerializedDictionary("Item Type", "Ratio")]
        private SerializedDictionary<ItemType, float> _itemDataDict;

        [FormerlySerializedAs("m_TotalProbability")] [SerializeField] private float _totalProbability;

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Optionally show warnings in the editor if total probability is low or zero
            CalculateTotalProbability();
            if (_totalProbability <= 0)
                Debug.LogWarning("Total probability is zero. Add valid percentages for item spawning.");
        }
#endif

        private void OnEnable()
        {
            CalculateTotalProbability();
        }

        // Calculate the total probability for cumulative calculation
        private void CalculateTotalProbability()
        {
            _totalProbability = 0;
            foreach (var value in _itemDataDict.Values) _totalProbability += value;
        }

        // Method to randomly select an ItemType based on their probabilities
        public ItemType PickRandomItemType()
        {
            if (_totalProbability <= 0)
            {
                Debug.LogWarning("Total probability is zero. Ensure the dictionary has valid values.");
                return ItemType.None; // Fallback item type
            }

            var randomValue = Random.Range(0, _totalProbability);
            float cumulative = 0;

            foreach (var pair in _itemDataDict)
            {
                cumulative += pair.Value; // Add current item's probability
                if (randomValue < cumulative) return pair.Key; // Return the item type that fits the random value
            }

            // Fallback
            return ItemType.None;
        }
    }
}