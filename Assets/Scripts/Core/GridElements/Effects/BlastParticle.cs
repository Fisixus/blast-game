using UnityEngine;

namespace Core.GridElements.Effects
{
    public class BlastParticle: MonoBehaviour
    {
        [field: SerializeField] public ParticleSystem BlastParticleSystem { get; private set; }
        
        public void SetTextureSheetSprites(Sprite[] animationSprites)
        {
            var textureSheetAnimation = BlastParticleSystem.textureSheetAnimation;
            textureSheetAnimation.enabled = true;
            
            // Clear existing sprites by setting empty sprites
            for (int i = 0; i < textureSheetAnimation.spriteCount; i++)
            {
                textureSheetAnimation.SetSprite(i, null);
            }
            for (int i = 0; i < animationSprites.Length; i++)
            {
                textureSheetAnimation.SetSprite(i, animationSprites[i]);
            }
        }
    }
}
