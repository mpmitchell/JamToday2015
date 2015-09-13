using UnityEngine;

public class SoundAndMusic : MonoBehaviour {
    [SerializeField] private AudioSource musicSource_;
    [SerializeField] private AudioSource soundSource_;

    private void Awake() {
        DontDestroyOnLoad(this);
    }
    
    public void ToggleMusic(bool toggle) {
        musicSource_.mute = toggle;
    }

    public void ToggleSound(bool toggle) {
        soundSource_.mute = toggle;
    }

    public void PlaySound(AudioClip clip) {
            musicSource_.PlayOneShot(clip);
    }
}
