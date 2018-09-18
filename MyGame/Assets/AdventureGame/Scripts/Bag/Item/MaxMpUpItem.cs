using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMpUpItem : BagItem
{
    public int upAmount = 10;
    private void Awake()
    {
        coolTime = 3f;
    }
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
        if (!ItemCoolEnd()) return;
        Tools.PlayFollowingParticletByName("MpPotionItem", GameManager.player.transform);
        Tools.PlaySoundByName("drink", GameManager.player.transform);
        // if (Tools.GetParticleSystemByName(itemData.itemType.ToString()) == null) Debug.Log("??");
        GameManager.player.MaxMPUp(upAmount);
        useUpdate();
    }
}
