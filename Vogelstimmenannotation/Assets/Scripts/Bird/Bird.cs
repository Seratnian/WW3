using System;
using System.Collections;
using UnityEngine;
using WW3.Database;

namespace WW3.GameWorld
{
    [RequireComponent(typeof(AudioSource))]
    public class Bird : MonoBehaviour
    {
        private static SoundDatabaseHandler _soundDatabase;
        private string _audioClipUrl;
        private AudioSource _audioSource;
        public bool Ready { private set; get; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _soundDatabase = TestScripts.SoundDatabase.SoundDatabaseHandler;
        }

        private void OnBallHit()
        {
            throw new NotImplementedException();

            //SendMessageUpwards("StoreBird");
        }

        private void OnCollisionEnter(Collision other)
        {
            //check if ball
            OnBallHit();
        }

        #region Initialization

        public void LoadSoundAndSetGameObjectTo(bool isActive)
        {
            StartCoroutine(LoadSoundFile(isActive));
        }

        private IEnumerator LoadSoundFile(bool isActive)
        {
            Ready = false;

            _audioClipUrl = _soundDatabase.GetAudioClipUrl();
            WWW download = new WWW(_audioClipUrl);

            while (!download.isDone)
            {
                Debug.Log(download.progress);
                yield return new WaitForEndOfFrame();
            }

            _audioSource.clip = download.GetAudioClip(false, false);
            _audioSource.spatialBlend = 1;
            gameObject.SetActive(isActive);
            gameObject.name = _audioClipUrl;
            download.Dispose();

            Ready = true;
        }

        public void PlaySound(bool play)
        {
            if (Ready)
                if (play)
                    StartCoroutine(PlaySoundWhenReady());
                else
                    _audioSource.Stop();
        }

        private IEnumerator PlaySoundWhenReady()
        {
            while (!Ready)
                yield return new WaitForEndOfFrame();

            _audioSource.Play();
        }

        #endregion
    }
}