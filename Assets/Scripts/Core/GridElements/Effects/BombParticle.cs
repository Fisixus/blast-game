using UnityEngine;

namespace Core.GridElements.Effects
{
    public class BombParticle: MonoBehaviour
    {
        [field: SerializeField] public ParticleSystem BombParticleSystem { get; private set; }

        public float PlayParticle()
        {
            BombParticleSystem.Play();
            return BombParticleSystem.main.duration;
        }
        
        public void SetBombSize(float size)
        {
        }
    }
}