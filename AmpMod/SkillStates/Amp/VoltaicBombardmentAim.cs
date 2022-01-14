﻿using System;
using RoR2;
using UnityEngine;
using AmpMod.SkillStates.Amp;


namespace AmpMod.SkillStates
{
	
	public class VoltaicBombardmentAim : BaseLightningAim
	{
        private GameObject chargeEffect;

        public override void OnEnter()
        {
            this.chargeEffectPrefab = null;
            this.chargeSoundString = "Play_mage_m2_charge";
            this.lightningRadius = 15f;

            base.OnEnter();

            /*ChildLocator childLocator = base.GetModelChildLocator();
            if (childLocator)
            {
                this.chargeEffect = childLocator.FindChild("HealAimEffect").gameObject;
                this.chargeEffect.SetActive(true);
            }*/
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();

            /*if (this.chargeEffect)
            {
                this.chargeEffect.SetActive(false);
            }*/
        }

        protected override VoltaicBombardmentFire GetNextState()
        {
            return new VoltaicBombardmentFire();
        }
    }
}
