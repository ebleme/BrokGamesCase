// maebleme2

using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Ebleme
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;
        private readonly IPlayerProvider _provider;

        public PlayerFactory(DiContainer container, IPlayerProvider provider)
        {
            _container = container;
            _provider = provider;
        }

        public async Task<GameObject> Create(string enemyName)
        {
            var prefab = await _provider.Load(enemyName);
            return _container.InstantiatePrefab(prefab );
        }
    }
}