using Core.Factories.Interface;
using Core.GridElements.GridPawns;
using UnityEngine;
using UTasks;

namespace MVP.Presenters.Handlers.Effects
{
    public class BombEffectHandler
    {
        private readonly IBombEffectFactory _bombEffectFactory;


        public BombEffectHandler(IBombEffectFactory factory)
        {
            _bombEffectFactory = factory;
        }

        public void PlayBombParticle(Booster booster, float size)
        {
            var bombEffect = _bombEffectFactory.CreateObj();
            
            var defaultSize = bombEffect.transform.localScale;
            bombEffect.transform.localScale *= size;
            
            var duration = bombEffect.PlayParticle();
            bombEffect.transform.position = booster.transform.position;
            UTask.Wait(duration).Do(() =>
            {
                bombEffect.transform.localScale = defaultSize;
                _bombEffectFactory.DestroyObj(bombEffect);
            });
        }
    }
}