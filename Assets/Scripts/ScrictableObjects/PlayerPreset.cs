// maebleme2

using Ebleme.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Ebleme.ScrictableObjects
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Ebleme/PlayerPreset", order = 0)]
    public class PlayerPreset : SingletonScriptableObject<PlayerPreset>
    {
        public AssetReference playerAsset;
        public string Id;
        public float moveSpeed;
        public float sprintSpeed;
        public float jumpPower;
        
        // public  Player GetPlayerPrefab()
        // {
        //     Addressables.LoadAssetAsync<Player>(playerAsset)
        //         .Completed += handle =>
        //     {
        //         return handle.Result;
        //     };
        //     
        // }
    }
}