using UnityEngine;

namespace Code.SoundSystem {
    [RequireComponent(typeof(AudioSource))]
    public class SFXPlayer : MonoBehaviour {
        [SerializeField] private AudioSource audioSource;

        private void Reset() {
            audioSource ??= GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip) {
            audioSource.PlayOneShot(clip);
        }
    }
}