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

        public void PlayBombParticle(Booster booster)
        {
            //if (!_bombEffectFactory.BombEffectDataDict.TryGetValue(item.ItemType, out var blastEffectData))
                //return;

            var bombEffect = _bombEffectFactory.CreateObj();
            //TODO:SetSize
            var duration = bombEffect.PlayParticle();
            bombEffect.transform.position = booster.transform.position;
            UTask.Wait(duration).Do(() => { _bombEffectFactory.DestroyObj(bombEffect); });
        }
    }
}