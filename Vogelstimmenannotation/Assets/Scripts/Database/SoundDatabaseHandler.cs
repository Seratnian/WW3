using UnityEngine;

namespace WW3.Database
{
    public interface SoundDatabaseHandler
    {
        string GetAudioClipUrl();
        void IdentifyAudioclip(WWW clipUrl, string name);
    }
}