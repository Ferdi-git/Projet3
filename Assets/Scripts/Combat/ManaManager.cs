using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ManaManager : MonoBehaviour
{

    [SerializeField] SOEventVisuelEffect visuelEffect;
    [SerializeField] SOEventTrail eventTrail;
    [SerializeField] Transform posMana;
    [SerializeField, ColorUsage(true, true)] Color glowManaColor;


    private void OnEnable()
    {
        visuelEffect.EffectGainMana += PlayerGetMana;
    }

    private void OnDisable()
    {
        visuelEffect.EffectGainMana -= PlayerGetMana;

    }


    private void PlayerGetMana(VisuelAttakData data)
    {
        StartCoroutine(GetMana(data));
    }
    private IEnumerator GetMana(VisuelAttakData data)
    {
        bool ended = false;
        Action trailEvent = () => ended = true;
        eventTrail.InvokeCreateTrail(new EventTrailData()
        {
            pos1 = data.posAttacker,
            pos2 = posMana.position,
            height = 1,
            trailTime = 0.15f - 0.005f ,
            glowColor = glowManaColor,
            eventEndTrail = trailEvent,
        });
        yield return new WaitUntil(() => ended);

        data.eventEndVisuel.Invoke();
    }


}
