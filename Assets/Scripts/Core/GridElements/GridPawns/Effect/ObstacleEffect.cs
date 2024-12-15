using UnityEngine;

namespace Core.GridElements.GridPawns.Effect
{
    public class ObstacleEffect : BaseGridObjectEffect
    {
        public float PlayBlastParticle(ParticleSystem blastParticle)
        {
            blastParticle.Play();
            return blastParticle.main.duration;
        }
    }
}