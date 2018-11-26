﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 中毒状态
/// </summary>
public class PoisoningState : BattleState{

    //============================================
    // 提供给外部调用的接口
    public Damage damage = Damage.Zero;     // 中毒时每间隔1秒受到的伤害
    public GameObject effect = null;        // 中毒时的特效

    private Damage nowDamage = Damage.Zero;
    private GameObject effectObject = null;

    protected override void OnEnter(CharacterMono stateHolder) {
        base.OnEnter(stateHolder);
        if (effect != null && effectObject == null) {
            // 创建临时特效对象
            effectObject = TransientGameObjectFactory.AcquireObject(EffectConditonalType.BattleState, templateObject: effect, battleState: this, target: stateHolder);
            effectObject.transform.position = stateHolder.transform.position;
            effectObject.transform.SetParent(stateHolder.transform);
        }
    }

    protected override void OnUpdate(CharacterMono stateHolder) {
        base.OnUpdate(stateHolder);

        nowDamage += damage * Time.smoothDeltaTime;
        if (nowDamage.TotalDamage >= 1) {
            stateHolder.characterModel.Damaged(nowDamage);
            nowDamage = Damage.Zero;
        }
    }
}

