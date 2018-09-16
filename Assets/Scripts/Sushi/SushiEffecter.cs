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
        [SerializeField] List<AudioClip> taberuSushiClips;
        [SerializeField] List<AudioClip> dropDishClips;

        void Start()
        {
            core.OnLaneSushiClick.Subscribe(_ =>
            {
                PlaySound(takeSushiClips);
            }).AddTo(this);
            
            core.OnTableSushiClick.Subscribe(sushiLife =>
            {
                PlaySound(taberuSushiClips);
            }).AddTo(this);
            
            core.OnEmptySushiClick.Subscribe(_ =>
            {
                PlaySound(dropDishClips);
            }).AddTo(this);
        }

        void PlaySound(List<AudioClip> clips)
        {
            var selectedClip = clips.Random();
            var pitch = Random.Range(0.95f, 1.05f);
            
            audioSource.clip = selectedClip;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }
}
