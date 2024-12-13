using System;
using System.Collections.Generic;
using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using Core.GridElements.GridPawns.Effect;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
                switch (gridObject)
                {
                    case Item item:
                        PlayBlastParticle(item).Forget();
                        break;
                    case Obstacle obstacle:
                        PlayBlastParticle(obstacle).Forget();
                        break;
                }
            }
        }

        private async UniTask PlayBlastParticle(Item item)
        {
            if (!_itemBlastEffectFactory.BlastEffectDataDict.TryGetValue(item.ItemType, out var blastEffectData))
                return;

            var particle = _itemBlastEffectFactory.CreateObj();
            particle.SetTextureSheetSprites(blastEffectData.TextureAnimationSprites);
            particle.transform.position = item.transform.position;

            var itemView = item.GetComponent<ItemEffect>();
            var duration = itemView.PlayBlastParticle(particle.BlastParticleSystem);
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            _itemBlastEffectFactory.DestroyObj(particle);
        }
        
        private async UniTask PlayBlastParticle(Obstacle obstacle)
        {
            if (!_obstacleBlastEffectFactory.BlastEffectDataDict.TryGetValue(obstacle.ObstacleType, out var blastEffectData))
                return;
            Debug.Log(obstacle);
            var particle = _obstacleBlastEffectFactory.CreateObj();
            particle.SetTextureSheetSprites(blastEffectData.TextureAnimationSprites);
            particle.transform.position = obstacle.transform.position;

            var obstacleView = obstacle.GetComponent<ObstacleEffect>();
            var duration = obstacleView.PlayBlastParticle(particle.BlastParticleSystem);
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            _itemBlastEffectFactory.DestroyObj(particle);
        }
    }
}
