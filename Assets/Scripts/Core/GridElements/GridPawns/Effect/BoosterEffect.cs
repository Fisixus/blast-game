using UnityEngine;

namespace Core.GridElements.GridPawns.Effect
{
    public class BoosterEffect : BaseGridObjectEffect
    {
        [SerializeField] private ParticleSystem m_BoosterCreationParticle;

        public void PlayBoosterCreationParticle()
        {
            m_BoosterCreationParticle.Play();
        }
    }
}
