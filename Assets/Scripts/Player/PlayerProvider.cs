// maebleme2

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Ebleme
{
    public class PlayerProvider : IPlayerProvider
    {
        public async Task<GameObject> Load(string enemyName)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(enemyName);
            return await handle.Task;
        }
    }
}