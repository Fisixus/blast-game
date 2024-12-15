using UnityEngine;

namespace Core.GridElements.Data.Effects
{
    [CreateAssetMenu(fileName = "BlastEffectData_00", menuName = "Grid Object Effects/New BlastEffectData")]
    public class BlastEffectDataSO : ScriptableObject
    {
        [field: SerializeField] public Sprite[] TextureAnimationSprites { get; private set; }
    }
}