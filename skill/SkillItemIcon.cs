using UnityEngine;
using System.Collections;

public class SkillItemIcon : UIDragDropItem
{
    private int skillId;

    protected override void OnDragDropStart()  //在克隆icon时调用
    {
        base.OnDragDropStart();

        skillId = transform.parent.GetComponent<SkillItem>().id;
        transform.parent = transform.root;
        GetComponent<UISprite>().depth = 3;
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);

        if (surface != null && surface.tag == Tags.shortCut)  //当技能拖到快捷栏上时
        {
            surface.GetComponent<ShortCutGrid>().SetSkill(skillId);
        }
    }
}
