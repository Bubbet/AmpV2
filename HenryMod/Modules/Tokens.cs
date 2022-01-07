﻿using R2API;
using System;

namespace HenryMod.Modules
{//need to fix the charactername and description not showing up
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            #region Henry
            string prefix = HenryPlugin.developerPrefix + "_BATTLEMAGE_BODY_";

            #region Skins
            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Default");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Descriptions
            LanguageAPI.Add(prefix + "NAME", Modules.StaticValues.characterName);
            LanguageAPI.Add(prefix + "SUBTITLE", Modules.StaticValues.characterSubtitle);
            LanguageAPI.Add(prefix + "DESCRIPTION", Modules.StaticValues.descriptionText);
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", Modules.StaticValues.characterOutro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", Modules.StaticValues.characterOutroFailure);
            #endregion

            #region Passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Charge");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "The battlemage's electrifying attacks build up stacks of 'charge' on an enemy. When 3 stacks are reached, the electric charge accumulated on the enemy is released in a devastating burst of static electricity that damages the target and all enemies near it for <style=cIsDamage>400% damage</style>.");
            #endregion

            #region Primary
            LanguageAPI.Add(prefix + "PRIMARY_SLASH_NAME", "Stormblade");
            LanguageAPI.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Helpers.agilePrefix + $"Slash continuously with your electrified sword for <style=cIsDamage>{100f * StaticValues.stormbladeDamageCoefficient}% damage</style>. Strikes have a <style=cIsDamage>{StaticValues.stormbladeChargeProcCoefficient}%</style> chance of applying charge.");
            #endregion

            #region Secondary
            LanguageAPI.Add(prefix + "SECONDARY_FERROSHOT_NAME", "Lorentz Cannon");
            LanguageAPI.Add(prefix + "SECONDARY_FERROSHOT_DESCRIPTION", Helpers.agilePrefix + $"Use electromagnetic fields to fire an array of <style=cIsDamage>6</style> iron sand bullets, each dealing <style=cIsDamage>{100f * StaticValues.ferroshotDamageCoefficient}% damage</style>.");
            #endregion

            #region Utility
            LanguageAPI.Add(prefix + "UTILITY_DASH_NAME", "Bolt");
            LanguageAPI.Add(prefix + "UTILITY_DASH_DESCRIPTION",  $"Transform into electricity, gaining invulnerability, free movement, and <style=cIsUtility>500% movement speed</style> for 2 seconds.");
            #endregion

            #region Special
            LanguageAPI.Add(prefix + "SPECIAL_CHAIN_NAME", "Fulmination");
            LanguageAPI.Add(prefix + "SPECIAL_CHAIN_DESCRIPTION", Helpers.agilePrefix + $"Fire a chaining stream of electricity that deals <style=cIsDamage>{100f * StaticValues.fulminationTotalDamageCoefficient}% damage</style> and has a <style=cIsDamage>{StaticValues.fulminationChargeProcCoefficient}%</style> chance of applying a stack of charge.");
            #endregion

            #region Special
            LanguageAPI.Add(prefix + "SPECIAL_LIGHTNING_NAME", "Voltaic Bombardment");
            LanguageAPI.Add(prefix + "SPECIAL_LIGHTNING_DESCRIPTION", Helpers.agilePrefix + $"Summon a lightning bolt that strikes the targeted area, damaging all enemies in the vicinity for <style=cIsDamage>{100f * StaticValues.lightningStrikeCoefficient}% damage</style>.");
            #endregion

            #region Achievements
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Henry: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESC", "As Henry, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Henry: Mastery");
            #endregion
            #endregion
        }
    }
}

