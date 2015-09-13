using UnityEngine;
using System.Collections;

public class SoundAndMusicProxy : MonoBehaviour {
    private SoundAndMusic soundAndMusic_;

    private void Start() {
        GameObject gameObject = GameObject.Find("Sound & Music");

        if (gameObject) {
            soundAndMusic_ = gameObject.GetComponent<SoundAndMusic>();
        }
    }

    public void ToggleMusic(bool toggle) {
        soundAndMusic_.ToggleMusic(toggle);
    }

    public void ToggleSound(bool toggle) {
        soundAndMusic_.ToggleSound(toggle);
    }

    public void PlaySound(AudioClip clip) {
        soundAndMusic_.PlaySound(clip);
    }
}
