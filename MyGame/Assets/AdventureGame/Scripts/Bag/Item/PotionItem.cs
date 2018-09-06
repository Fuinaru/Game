using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : BagItem
{
    public int cureAmount = 2;
    public int effectNum = 0;
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
    public override void useItem()
    {

        ParticleSystem go = Instantiate(MyGameVariable.particleEffects[0]) as ParticleSystem;
        go.transform.SetParent(GameManager.player.transform);
        go.transform.localPosition = Vector3.zero;
        go.Play();
        GameManager.player.Cure(cureAmount);
        itemData.itemNum--;
        updateText();
    }
}
