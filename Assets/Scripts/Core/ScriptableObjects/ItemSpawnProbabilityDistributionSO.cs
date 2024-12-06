using AYellowpaper.SerializedCollections;
using Core.Enum;
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ProbabilityDistribution_00", menuName = "Scriptable Objects/New Distribution")]
    public class ItemSpawnProbabilityDistributionSO : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("Item Type", "Ratio")]
        private SerializedDictionary<ItemType, float> m_ItemDataDict;

        [SerializeField] private float m_TotalProbability;

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Optionally show warnings in the editor if total probability is low or zero
            CalculateTotalProbability();
            if (m_TotalProbability <= 0)
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
            m_TotalProbability = 0;
            foreach (var value in m_ItemDataDict.Values) m_TotalProbability += value;
        }

        // Method to randomly select an ItemType based on their probabilities
        public ItemType PickRandomItemType()
        {
            if (m_TotalProbability <= 0)
            {
                Debug.LogWarning("Total probability is zero. Ensure the dictionary has valid values.");
                return ItemType.None; // Fallback item type
            }

            var randomValue = Random.Range(0, m_TotalProbability);
            float cumulative = 0;

            foreach (var pair in m_ItemDataDict)
            {
                cumulative += pair.Value; // Add current item's probability
                if (randomValue < cumulative) return pair.Key; // Return the item type that fits the random value
            }

            // Fallback
            return ItemType.None;
        }
    }
}