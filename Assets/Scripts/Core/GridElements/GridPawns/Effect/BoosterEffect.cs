using UnityEngine;

namespace Core.GridElements.GridPawns.Effect
{
    public class BoosterEffect : BaseGridObjectEffect
    {
        [SerializeField] private ParticleSystem _boosterCreationParticle;

        public void PlayBoosterCreationParticle()
        {
            _boosterCreationParticle.Play();
        }
    }
}