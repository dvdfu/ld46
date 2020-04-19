using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    Dictionary<string, AudioSource> sources;

    public static void Play(AudioClip clip) {
        AudioSource source = instance.ClipToSource(clip);
        source.Play();
    }

    void Awake() {
        if (instance == null) instance = this;
        sources = new Dictionary<string, AudioSource>();
    }

    AudioSource ClipToSource(AudioClip clip) {
        string name = clip.name;
        AudioSource source;
        if (sources.ContainsKey(name)) {
            sources.TryGetValue(name, out source);
        } else {
            source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = 0.5f;
            sources.Add(name, source);
        }
        return source;
    }
}