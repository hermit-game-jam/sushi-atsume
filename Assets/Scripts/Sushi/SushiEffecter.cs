using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Sushi
{
    public class SushiEffecter : MonoBehaviour
    {
        [SerializeField] SushiCore core;

        [SerializeField] AudioSource audioSource;

        [SerializeField] List<AudioClip> takeSushiClips;
        [SerializeField] List<AudioClip> takeSushiFailedClips;
        [SerializeField] List<AudioClip> taberuSushiClips;
        [SerializeField] List<AudioClip> dropDishClips;

        void Start()
        {
            var globalPitchOffset = Random.Range(-0.125f, 0.125f);
            
            core.OnLaneSushiClick.Subscribe(putSucceed =>
            {
                var clips = putSucceed ? takeSushiClips : takeSushiFailedClips;
                var pitchRange = putSucceed ? 0.08f : 0;
                PlaySound(clips, pitchRange);
            }).AddTo(this);
            
            core.OnTableSushiClick.Subscribe(sushiLife =>
            {
                var t = SushiCore.SushiMaxLife - sushiLife;
                var pitchOffset = t * 0.059f + globalPitchOffset;
                PlaySound(taberuSushiClips, 0.0285f, pitchOffset);
            }).AddTo(this);
            
            core.OnEmptySushiClick.Subscribe(_ =>
            {
                PlaySound(dropDishClips, 0.05f, 0, true);
            }).AddTo(this);
        }

        void PlaySound(List<AudioClip> clips, float pitchRange = 0.05f, float pitchOffset = 0, bool playOutSide = false)
        {
            var selectedClip = clips.Random();
            var pitch = Random.Range(1 - pitchRange, 1 + pitchRange);

            var a = playOutSide ? CreateOutsideAudioSource(selectedClip) : audioSource;
            
            a.clip = selectedClip;
            a.pitch = pitch + pitchOffset;
            a.Play();
        }
        
        // AudioSource
        // public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [DefaultValue("1.0F")] float volume)
        // より拝借
        AudioSource CreateOutsideAudioSource(AudioClip clip)
        {
            var go = new GameObject("One shot audio");
            var a = go.AddComponent<AudioSource>();
            Destroy(go, clip.length * ((double) Time.timeScale >= 0.00999999977648258 ? Time.timeScale : 0.01f));
            return a;
        }
    }
}
