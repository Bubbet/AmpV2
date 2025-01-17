﻿using System;
using UnityEngine;

namespace AmpMod.Modules
{
    //static values for use in skillstates and other references for easy adjustment
    internal static class StaticValues
    {

        public const string characterName = "Amp";
        public const string characterSubtitle = "Electromagnetic Warrior";
        public const string characterOutro = "..and so he left, with an undeniable freedom.";
        public const string characterOutroFailure = "..and so he vanished, imprisoned for eternity.";
        public const string chargeDesc = "<style=cKeywordName>Charged</style>" + "<style=cSub>Applies one stack of <color=#0091ff>charge</color> to an enemy. On three stacks, create a static electricity explosion dealing <style=cIsDamage>400% damage</style>.";
        public const string doubleChargeDesc = "<style=cKeywordName>Double Charged</style>" + "<style=cSub>Applies two stacks of <color=#0091ff>charge</color> to an enemy.</style>";

        internal static string descriptionText = "Amp is an agile melee-range hybrid that focuses on dealing heavy AOE damage with his electromagnetic attacks.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Modified Shockblade can be used in tandem with most of Amp's non-utility abilities, so keep slashing!" + Environment.NewLine + Environment.NewLine
             + "< ! > Each of Lorentz Cannon's projectiles counts as its own, so use it to rack up status effects fast." + Environment.NewLine + Environment.NewLine
             + "< ! > If you can control it, Surge can be used to deal an easy 400% damage by running back and forth into enemies to quickly charge them." + Environment.NewLine + Environment.NewLine
             + "< ! > Voltaic Bombardment hits much higher than the indicator shows, so use it to instantly take down weak flying enemies." + Environment.NewLine + Environment.NewLine;

        internal static string characterLore = "How long has it been? He pondered, and quickly decided it did not matter. Time had become irrelevant; all he knew was his duty. To find. To capture. To fight. To kill.\n\n"
            + "Electrocute.\n\n"
            + "An orphan, they said. Saved from a life spent in darkened alleyways, the only caveat being that he work for them without question. They called themselves scientists, visionaries, leaders. He called them slavers. For any disobedience was swiftly treated with high-voltage punishment.\n\n"
            + "Electrocute.\n\n"
            + "A new mission, they told him. Obtain an artifact from a desolate planet, said to contain the final remnant of a dead god's power. The details were left out; his safety wasn't a primary concern for them. If he died, they would simply use another. But when he touched the artifact, sparks flew. Images of a faraway planet, lush and teeming with life, flashed into his mind. He stumbled back, confused. What did this mean? What was this artifact? He knew if he followed the message, he’d find the answers. But first, there was something he must do. \n\n"
            + "Electrocute.\n\n"
            + "The trail back to headquarters was stained with blood. He didn't have to go back. With his new power, he could've escaped and never returned. But he wanted vengeance.\n\n"
            + "Electrocute.\n\n"
            + "A trail of charred corpses behind him, he breaks into the boardroom. Desperate businessmen fire their sidearms, but the bullets freeze mid-air, refusing to hit their target. The last thing they see is a terrifying, magnificent blue light beginning to emanate from their slave-turned-assassin.\n\n"
            + "Electrocute.";




        [Header("Charge Values")]
        internal const int chargeMaxStacks = 3;
        internal const float chargeDuration = 7;
        internal const float chargeDamageCoefficient = 4f;

        [Header("Stormblade Values")]
        internal const float stormbladeDamageCoefficient = 1.6f;
        internal const float stormbladeChargeProcCoefficient = 100f;

        [Header("Lorentz Cannon Values")]
        internal const float ferroshotDamageCoefficient = 1.3f;
        
        [Header("Plasma Slash Values")]
        internal const float spinSlashDamageCoefficient = 7f;
        internal const float fireTrailTickDamageCoefficient = 1.5f;
        internal const float fireBeamDamageCoefficient = 5f;

        [Header("Magnetic Vortex Values")]
        internal const float vortexDamageCoefficient = 1.5f;
        internal const float vortexExplosionCoefficient = 4f;

        [Header("Bolt Values")]
        internal static float boltBlastDamageCoefficient = 3f;
        internal const float boltOverlapDamageCoefficient = 1.5f;

        [Header("Pulse Leap Values")]
        internal const float boostDamageCoefficient = 4f;

        [Header("Fulmination Values")]
        internal const float fulminationDamageCoefficient = 1.1f;
        internal const float fulminationTotalDamageCoefficient = 22f;
        internal const float fulminationChargeProcCoefficient = 25f;

        [Header("Voltaic Bombardment Values")]
        internal const float lightningStrikeCoefficient = 14f;
        internal const float overChargeDuration = 5f;
        internal const float overchargeMoveSpeed = 2.5f;
        internal const float overchargeAttackSpeed = .3f;

        [Header("Bulwark of Storms Values")]



        [Header("Charge Sound Strings")]
        internal const string chargeExplosionString = "PlayChargeExplosion";

        [Header("Stormblade Sound Strings")]
        internal const string stormbladeSwing1String = "PlayStormbladeSwing1";
        internal const string stormbladeSwing2String = "PlayStormbladeSwing2";
        internal const string stormbladeHit1String = "PlayStormbladeHit1";
        internal const string stormbladeHit2String = "PlayStormbladeHit2";
        internal const string stormbladeHit3String = "PlayStormbladeHit3";
        internal const string stormbladeHit4String = "PlayStormbladeHit4";

        [Header("Ferroshot/Gauss Cannon Sound Strings")]
        internal const string ferroshotPrepString = "PlayFerroshotCreate";
        internal const string ferroshotLaunchString = "PlayFerroshotLaunch";
        internal const string ferroshotLaunchAlterString = "PlayFerroshotLaunchAlter";
        internal const string ferroshotPrepAlterString = "PlayFerroshotCreateAlter";

        [Header("Magnetic Vortex Sound Strings")]
        internal const string vortexChargeString = "PlayVortexCharge";
        internal const string vortexLoopString = "PlayVortexLoop";
        internal const string vortexShootString = "PlayVortexShoot";
        internal const string vortexFlightLoopString = "PlayVortexFlightLoop";
        internal const string vortexSpawnString = "PlayVortexSpawn";
        internal const string vortexFlightLoopStringAlt = "PlayVortexFlightLoopAlter2";
        internal const string vortexExplosionString = "PlayVortexExplosion";

        [Header("Plasma Slash Sound Strings")]
        internal const string heatSwingString = "PlayHeatShockSwing";
        internal const string heatHitString = "PlayHeatShockHit";
        internal const string heatChargeString = "PlayHeatShockCharge";
        internal const string plasmaExplosionString = "PlayPlasmaExplosion";

        [Header("Bolt Sound Strings")]
        internal const string boltState2SecString = "PlayBoltState2sec";
        internal const string boltStateFullString = "PlayBoltStateFull";
        internal const string boltEnterString = "PlayBoltEnter";
        internal const string boltExitString = "PlayBoltExit";
        internal const string boltState2SecWindString = "PlayBoltStateWind";
        internal const string boltState2SecAlterString = "PlayBoltState2SecAlter";

        [Header("Fulmination Sound Strings")]
        internal const string fulminationEnterString = "PlayFulminationEnter";
        internal const string fulminationStateString = "PlayFulminationState";
        internal const string fulminationExitString = "PlayFulminationExit";
        internal const string fulminationExitAlterString = "PlayFulminationExitAlter";

        [Header("Bulwark of Storms Sound Strings")]
        internal const string wormChargeString = "PlayWormCharge";
        internal const string wormSummonString = "PlayWormSummon";


    }
}