using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Masters;
using Sushi;
using UniRx;

public class UIGacha : SingletonMonoBehaviour<UIGacha>
{
    static readonly int GachaLayer = 9;

    Dictionary<int, SushiCore> sushis = new Dictionary<int, SushiCore>();

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip startClip;
    [SerializeField]
    AudioClip winClip;
    [SerializeField]
    AudioClip loseClip;
    
    void Start()
    {
        sushis = Master.Instance.SushiMaster.Values
            .ToDictionary(x => x.Code, x => CreateSushi(x));
    }

    SushiCore CreateSushi(SushiMaster master)
    {
        var sushi = master.SushiCreate(Vector3.zero, Quaternion.identity, transform);
        sushi.transform.localPosition = Vector3.zero;
        sushi.gameObject.SetLayerRecursively(GachaLayer);
        sushi.gameObject.SetActive(false);
        sushi.ChangeStateToGacha();
        return sushi;
    }

    public IObservable<Unit> DirectionAsObservable(int sushiCode, bool isNewSushi)
    {
        return Observable.FromCoroutine(() => Direction(sushiCode, isNewSushi));
    }
    
    IEnumerator Direction(int sushiCode, bool isNewSushi)
    {
        yield return LotteryProgress();
        yield return Result(sushiCode, isNewSushi);
    }

    IEnumerator LotteryProgress()
    {
        audioSource.PlayOneShot(startClip);

        for (int i = 0; i < 2; i++)
        {
            foreach (var it in sushis.Values)
            {
                it.gameObject.SetActive(true);

                yield return new WaitForSeconds(0.1f);

                it.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Result(int sushiCode, bool isNewSushi)
    {
        audioSource.PlayOneShot(isNewSushi ? winClip : loseClip);

        var resultSushi = sushis[sushiCode];

        for (var i = 0; i < 10; i++)
        {
            resultSushi.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(0.1f);
            
            resultSushi.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
