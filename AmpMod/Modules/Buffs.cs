﻿using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace AmpMod.Modules
{
    public static class Buffs
    {
        // armor buff gained during roll
        internal static BuffDef armorBuff;
        internal static BuffDef chargeDebuff;
        internal static BuffDef chargeBuildup;
        internal static BuffDef invulnerableBuff;


        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        internal static void RegisterBuffs()
        {
            armorBuff = AddNewBuff("HenryArmorBuff", Resources.Load<Sprite>("Textures/BuffIcons/texBuffGenericShield"), Color.white, false, false);
            //charge debuff
            chargeBuildup= AddNewBuff("AmpChargeBuildup", Resources.Load<Sprite>("None"), Color.cyan, true, true);
            //invulnerability for use in bolt; using this instead of built-in invincibility buffs to avoid conflicts
            invulnerableBuff = AddNewBuff("AmpInvulnerability", Resources.Load<Sprite>("None"), Color.black, false, false);
        }

        // simple helper method
        internal static BuffDef AddNewBuff(string buffName, Sprite buffIcon, Color buffColor, bool canStack, bool isDebuff)
        {
            BuffDef buffDef = ScriptableObject.CreateInstance<BuffDef>();
            buffDef.name = buffName;
            buffDef.buffColor = buffColor;
            buffDef.canStack = canStack;
            buffDef.isDebuff = isDebuff;
            buffDef.eliteDef = null;
            buffDef.iconSprite = buffIcon;

            buffDefs.Add(buffDef);

            return buffDef;
        }
    }
}