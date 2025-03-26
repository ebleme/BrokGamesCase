// Copyright (C) 2015-2021 gamevanilla - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace Ebleme.Utility {

    /// <summary>
    /// This component goes together with a button object and contains
    /// the audio clips to play when the player rolls over and presses it.
    /// </summary>
    public class ButtonSounds : MonoBehaviour {
        [SerializeField] private AudioClip _pressedSound;

        // public void PlayPressedSound() {
        //     if (_pressedSound != null) {
        //         GeneralServicesManager.Sounds.PlayOneShotSFX(_pressedSound);
        //     }
        //     else
        //     {
        //         GeneralServicesManager.Sounds.PlayButtonClickSfx();
        //     }
        // }
    }
}
