// maebleme2

using System;
using System.Threading.Tasks;
using Ebleme.ScrictableObjects;
using UnityEngine;
using Zenject;

namespace Ebleme
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Inject]
        private IPlayerFactory playerFactory;


        public Player CurrentPlayer { get; private set; }
        public PlayerPreset CurrentPlayerPreset { get; private set; }

        public async Task Spawn(PlayerPreset playerPreset, Action onLoaded)
        {
            var pos = Vector3.zero;
            if (CurrentPlayer != null)
            {
                pos = CurrentPlayer.transform.position;
                Destroy(CurrentPlayer.gameObject);
            }

            var newPlayer = await playerFactory.Create(playerPreset.Id);
            newPlayer.transform.position = pos;

            CurrentPlayerPreset = playerPreset;
            CurrentPlayer = newPlayer.GetComponent<Player>();

            GameManager.Instance.CurrentPlayer = CurrentPlayer;
            GameManager.Instance.CurrentPlayerPreset= CurrentPlayerPreset;
            
            
            CurrentPlayer.Set(CurrentPlayerPreset);

            GameManager.Instance.SetCinemachineFollowTarget(CurrentPlayer.CameraFollowPoint);
            GameManager.Instance.SetCursorState(true);
            onLoaded?.Invoke();
        }
    }
}