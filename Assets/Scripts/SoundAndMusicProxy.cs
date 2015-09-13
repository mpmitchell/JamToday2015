using UnityEngine;
using System.Collections;

public class SoundAndMusicProxy : MonoBehaviour {
    [SerializeField] private GameObject soundAndMusic_;

    private SoundAndMusic audioSource_;

    private void Start() {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Sound & Music");

        if (gameObject) {
            audioSource_ = gameObject.GetComponent<SoundAndMusic>();
        } else {
            audioSource_ = Instantiate(soundAndMusic_).GetComponent<SoundAndMusic>();
        }
    }

    public void ToggleMusic(bool toggle) {
        audioSource_.ToggleMusic(toggle);
    }

    public void ToggleSound(bool toggle) {
        audioSource_.ToggleSound(toggle);
    }

    public void PlaySound(AudioClip clip) {
        audioSource_.PlaySound(clip);
    }
}
