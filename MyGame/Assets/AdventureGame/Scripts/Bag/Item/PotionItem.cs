using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : BagItem
{
    public int cureAmount = 2;
    public int effectNum = 0;
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
        Tools.PlayFollowingParticletByName(itemData.itemType.ToString(), GameManager.player.transform);
        // if (Tools.GetParticleSystemByName(itemData.itemType.ToString()) == null) Debug.Log("??");
        GameManager.player.Cure(cureAmount);
        useUpdate();
    }
}
