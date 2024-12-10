using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Effect;
using UTasks;

namespace MVP.Presenters.Handlers.Effects
{
    public class BlastEffectHandler
    {
        private readonly IItemBlastEffectFactory _itemBlastEffectFactory;
        private readonly IObstacleBlastEffectFactory _obstacleBlastEffectFactory;


        public BlastEffectHandler(IItemBlastEffectFactory itemBlastFactory, IObstacleBlastEffectFactory obstacleEffectFactory)
        {
            _itemBlastEffectFactory = itemBlastFactory;
            _obstacleBlastEffectFactory = obstacleEffectFactory;
        }

        public void PlayBlastParticles(List<BaseGridObject> matchedGridObjects)
        {
            foreach (var gridObject in matchedGridObjects)
            {
                if (gridObject is Item item)
                {
                    PlayBlastParticle(item);
                }
                else if (gridObject is Obstacle obstacle)
                {
                    PlayBlastParticle(obstacle);
                }
            }
        }

        public void PlayBlastParticle(Item item)
        {
            if (!_itemBlastEffectFactory.BlastEffectDataDict.TryGetValue(item.ItemType, out var blastEffectData))
                return;

            var particle = _itemBlastEffectFactory.CreateObj();
            particle.SetTextureSheetSprites(blastEffectData.TextureAnimationSprites);
            particle.transform.position = item.transform.position;

            var itemView = item.GetComponent<ItemEffect>();
            var duration = itemView.PlayBlastParticle(particle.BlastParticleSystem);
            UTask.Wait(duration).Do(() => { _itemBlastEffectFactory.DestroyObj(particle); });
        }
        
        public void PlayBlastParticle(Obstacle obstacle)
        {
            if (!_obstacleBlastEffectFactory.BlastEffectDataDict.TryGetValue(obstacle.ObstacleType, out var blastEffectData))
                return;

            var particle = _obstacleBlastEffectFactory.CreateObj();
            particle.SetTextureSheetSprites(blastEffectData.TextureAnimationSprites);
            particle.transform.position = obstacle.transform.position;

            var obstacleView = obstacle.GetComponent<ObstacleEffect>();
            var duration = obstacleView.PlayBlastParticle(particle.BlastParticleSystem);
            UTask.Wait(duration).Do(() => { _obstacleBlastEffectFactory.DestroyObj(particle); });
        }
    }
}
