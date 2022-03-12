﻿using EntityStates;
using RoR2;
using RoR2.Audio;
using System;
using UnityEngine;
using RoR2.Projectile;
using UnityEngine.Networking;
using RoR2.Orbs;
using RoR2.Skills;
using AmpMod.SkillStates.BaseStates;
using AmpMod.SkillStates.Amp; 

namespace AmpMod.SkillStates
{
    // Token: 0x02000B12 RID: 2834
    public abstract class BaseLightningAim : BaseSkillState
    {
            protected abstract VoltaicBombardmentFire GetNextState();
            public GameObject chargeEffectPrefab;
            public string chargeSoundString;
            public GameObject crosshairOverridePrefab;
            public float lightningRadius = .2f;
            private GameObject defaultCrosshairPrefab;
            private uint loopSoundInstanceId;

        private float reticleScale = 7.5f;

            private float duration { get; set; }
            private Animator animator { get; set; }
            private ChildLocator childLocator { get; set; }
            private GameObject chargeEffectInstance { get; set; }
            private GameObject areaIndicatorInstance { get; set; }

            public override void OnEnter()
            {
            base.OnEnter();
                this.animator = base.GetModelAnimator();
                this.childLocator = base.GetModelChildLocator();

                if (this.childLocator)
                {
                    Transform transform = this.childLocator.FindChild("HandL");

                    if (transform && this.chargeEffectPrefab)
                    {
                        this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
                        this.chargeEffectInstance.transform.parent = transform;
                        ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
                        ObjectScaleCurve component2 = this.chargeEffectInstance.GetComponent<ObjectScaleCurve>();
                        if (component)
                        {
                            component.newDuration = this.duration;
                        }
                        if (component2)
                        {
                            component2.timeMax = this.duration;
                        }
                    }
                }

                base.PlayAnimation("Gesture, Override", "ChargeSpell", "Spell.playbackRate", 0.4f);
                this.loopSoundInstanceId = Util.PlayAttackSpeedSound(this.chargeSoundString, base.gameObject, this.attackSpeedStat);
                this.defaultCrosshairPrefab = base.characterBody._defaultCrosshairPrefab;

                if (base.cameraTargetParams)
                {
                    //base.cameraTargetParams.aimMode = CameraTargetParams.AimType.OverTheShoulder;
                }

                if (this.crosshairOverridePrefab)
                {
                    base.characterBody._defaultCrosshairPrefab = this.crosshairOverridePrefab;
                }

            if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
                {
                    this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab);
                    this.areaIndicatorInstance.transform.localScale = new Vector3(reticleScale, reticleScale, reticleScale);
                }
            }

            private void UpdateAreaIndicator()
            {
                if (this.areaIndicatorInstance)
                {
                    float maxDistance = 200f;

                    Ray aimRay = base.GetAimRay();
                    RaycastHit raycastHit;
                    if (Physics.Raycast(aimRay, out raycastHit, maxDistance, LayerIndex.CommonMasks.bullet))
                    {
                        this.areaIndicatorInstance.transform.position = raycastHit.point;
                        this.areaIndicatorInstance.transform.up = raycastHit.normal;
                    }
                    else
                    {
                        this.areaIndicatorInstance.transform.position = aimRay.GetPoint(maxDistance);
                        this.areaIndicatorInstance.transform.up = -aimRay.direction;
                    }
                }
            }

            public override void OnExit()
            {
            if (this.crosshairOverridePrefab)
            {
                base.characterBody._defaultCrosshairPrefab = this.defaultCrosshairPrefab;
            }
            else
                {
                    base.characterBody.hideCrosshair = false;
                }

                if (this.areaIndicatorInstance)
                {
                    EntityState.Destroy(this.areaIndicatorInstance.gameObject);
                }

                AkSoundEngine.StopPlayingID(this.loopSoundInstanceId);

                if (!this.outer.destroying)
                {
                    base.PlayAnimation("Gesture, Override", "BufferEmpty");
                }

                if (base.cameraTargetParams)
                {
                    //base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Standard);
                }

            EntityState.Destroy(this.chargeEffectInstance);
                base.OnExit();
            }
        public virtual void HandlePrimaryAttack()
        {
            
            this.outer.SetNextStateToMain();
        }

        public override void FixedUpdate()
            {
                base.FixedUpdate();
                //base.characterBody.isSprinting = false;
                base.StartAimMode(0.5f, false);

                if (base.isAuthority && !this.IsKeyDownAuthority()) //base.inputBank.skill1.justPressed)
                {
                    VoltaicBombardmentFire nextState = this.GetNextState();
                    if (this.areaIndicatorInstance)
                    {
                        nextState.boltPosition = this.areaIndicatorInstance.transform.position;
                        nextState.lightningRotation = this.areaIndicatorInstance.transform.rotation;
                    }
                    else
                    {
                        nextState.boltPosition = base.transform.position;
                        nextState.lightningRotation = this.areaIndicatorInstance.transform.rotation;
                    }
                    this.outer.SetNextState(nextState);
                }
            }

            public override void Update()
            {
                base.Update();
                this.UpdateAreaIndicator();
            }

            public override InterruptPriority GetMinimumInterruptPriority()
            {
                return InterruptPriority.PrioritySkill;
            }
        }
    }


