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
       // if (Tools.GetParticleSystemByName(itemData.itemType.ToString()) == null) Debug.Log("??");
        ParticleSystem go = Instantiate(Tools.GetParticleSystemGameObjectByName(itemData.itemType.ToString()).GetComponent<ParticleSystem>()) as ParticleSystem;
        go.GetComponent<ParticleSys>().SetFollowingTarget(GameManager.player.transform);
        go.Play();
        GameManager.player.Cure(cureAmount);
        itemData.itemNum--;
        updateText();
    }
}
