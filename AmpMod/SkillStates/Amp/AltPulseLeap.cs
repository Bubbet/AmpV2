﻿using RoR2;
using UnityEngine;
using EntityStates;
using R2API;
using AmpMod.SkillStates.SkillComponents;
using KinematicCharacterController;
using UnityEngine.Networking;

namespace AmpMod.SkillStates
{
    class AltPulseLeap : BaseSkillState
    {

        private BlastAttack boostBlast;
        private string launchSound = Modules.StaticValues.boltExitString;
        private float launchDamage = Modules.StaticValues.boostDamageCoefficient;
        private GameObject launchEffect;
        private ChildLocator childLocator;
        private float smallHopVelocity = 12f;
        private Vector3 lungeVelocity;
        private Vector3 initialLaunchVelocity;
        private float blastRadius = 11f;
        private float flyDuration = .15f; //.4f;
        private float exitAnimDuration = .65f; //.75f
        private float exitDuration = 1.2f;//1f;
        private Transform modelTransform;
        private bool hasResetRot;
        private bool hasFlown;
        private float speedCoefficient = 20f; //25f;
        private bool hasFired = false;
        public static AnimationCurve speedCoefficientCurve = EntityStates.Mage.FlyUpState.speedCoefficientCurve;
        private Vector3 aimDirection;
        private Transform leftMuzzleTransform;
        private Transform swordTipTransform;
        private bool isFlying;
        private Animator animator;
        private Vector3 flyDirection;
        private float upSpeed;
        private Transform inputSpace;
        private GameObject muzzlePrefab = Modules.Assets.pulseMuzzleEffect;
        private Rotator rotator;
        private GameObject blinkPrefab = Modules.Assets.pulseBlastEffect;
        private float lungeSpeed = 40f;

        public override void OnEnter()
        {

            base.OnEnter();



            if (base.isAuthority)
            {

                FireLaunchBlast();

                this.modelTransform = base.GetModelTransform();
                animator = base.GetModelAnimator();
                this.childLocator = this.modelTransform.GetComponent<ChildLocator>();

                if (!this.childLocator)
                {
                    Debug.Log("No ChildLocator found");
                    
                }
                   if (this.modelTransform && this.childLocator)
                    {
                        Transform leftTransform = this.childLocator.FindChild("HandL");
                        Transform swordTransform = this.childLocator.FindChild("SwordTip");

                        this.leftMuzzleTransform = UnityEngine.Object.Instantiate<GameObject>(Modules.Assets.pulseMuzzleEffect, leftTransform).transform;
                        this.swordTipTransform = UnityEngine.Object.Instantiate<GameObject>(Modules.Assets.pulseMuzzleEffect, swordTransform).transform;

                    } 


                base.characterMotor.Motor.ForceUnground();
                base.characterMotor.disableAirControlUntilCollision = true;
                //this.punchVelocity = BaseSwingChargedFist.CalculateLungeVelocity(base.characterMotor.velocity, base.GetAimRay().direction, this.charge, this.minLungeSpeed, this.maxLungeSpeed);
                this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));

                this.inputSpace = new GameObject("inputSpace").transform;
                this.inputSpace.position = Vector3.zero;
                this.inputSpace.rotation = Quaternion.identity;
                this.inputSpace.localScale = Vector3.one;

                upSpeed = animator.GetFloat("upSpeed");

                animator.SetBool("isFlying", true);


                this.rotator = this.modelTransform.Find("metarig").GetComponent<Rotator>();
                if (!rotator) this.rotator = this.modelTransform.Find("metarig").gameObject.AddComponent<Rotator>();
                rotator.ResetRotation(0f);

                aimDirection = base.GetAimRay().direction;

                


                this.rotator.SetRotation(Quaternion.LookRotation(CreateForwardRotation(), Vector3.up), flyDuration);
                base.PlayAnimation("FullBody, Override", "PulseLeap", "BaseSkill.playbackRate", flyDuration);
                //base.PlayCrossfade("FullBody, Override", "PulseLeap",  flyDuration);
            }


            Transform modelTransform = base.GetModelTransform();
            childLocator = modelTransform.GetComponent<ChildLocator>();

            Util.PlaySound(launchSound, base.gameObject);

        }



        public void AltHandleMovements(Vector3 flyVector)
        {
            base.characterMotor.velocity = Vector3.zero;
            base.characterMotor.rootMotion += flyVector * (this.moveSpeedStat * this.speedCoefficient * Time.fixedDeltaTime);
        }



        public static Vector3 CalculateLungeVelocity(Vector3 currentVelocity, Vector3 aimDirection, float lungeSpeed)
        {
            currentVelocity = ((Vector3.Dot(currentVelocity, aimDirection) < 0f) ? Vector3.zero : Vector3.Project(currentVelocity, aimDirection));
            return currentVelocity + aimDirection * lungeSpeed;
        }

        //create blastAttack on launch
        public void FireLaunchBlast()
        {
            if (base.isAuthority)
            {

                boostBlast = new BlastAttack
                {
                    attacker = base.gameObject,
                    baseDamage = this.launchDamage * base.characterBody.damage,
                    baseForce = 0f,
                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                    crit = base.characterBody.RollCrit(),
                    damageColorIndex = DamageColorIndex.Item,
                    damageType = DamageType.Generic,
                    falloffModel = BlastAttack.FalloffModel.None,
                    inflictor = base.gameObject,
                    position = base.transform.position,
                    procChainMask = default(ProcChainMask),
                    procCoefficient = 1f,
                    radius = this.blastRadius,
                    teamIndex = base.characterBody.teamComponent.teamIndex

                };

                boostBlast.AddModdedDamageType(Modules.DamageTypes.applyCharge);
                boostBlast.Fire();
               // Debug.Log("Firing launch blast");
            }
            
        }

        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(aimDirection);
            effectData.origin = origin;
            effectData.scale = .3f;
            EffectManager.SpawnEffect(blinkPrefab, effectData, false);
        }

       
        private Vector3 CreateForwardRotation()
        {
            Vector3 aimDir = base.GetAimRay().direction;
            Vector3 moveDir = this.inputBank.moveVector;

            var aimOrientation = new Vector3(aimDir.x, 0f, aimDir.z);
            aimOrientation = Vector3.Normalize(aimOrientation);
            this.inputSpace.rotation = Quaternion.LookRotation(aimOrientation, Vector3.up);

            aimDir = this.inputSpace.InverseTransformDirection(aimDir);
            moveDir = this.inputSpace.InverseTransformDirection(moveDir);


            Vector3 forward;
            /*if (moveDir.sqrMagnitude != 0)
            {
                forward = aimDir * moveDir.z;
                forward.x = moveDir.x;

            }
            else
            {
                forward = aimDir;
            } */

            forward = aimDir;
            forward.y += this.inputBank.jump.down ? 2f : 0f;
            forward = Vector3.Normalize(forward);

            Vector3 direction = this.inputSpace.TransformDirection(forward);
            return direction;
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.isAuthority)
            {
                if (base.characterMotor && !hasFired)
                {
                    hasFired = true;
                    //FireLaunchBlast();
                    //test following line
                    base.characterMotor.velocity = Vector3.zero;
                    base.SmallHop(base.characterMotor, this.smallHopVelocity);
                    
                    //Debug.Log("firing");


                    //remove fall damage check
                   /* if (!base.characterBody.bodyFlags.HasFlag(CharacterBody.BodyFlags.IgnoreFallDamage))
                    {
                        base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
                        base.characterMotor.onHitGroundServer += this.CharacterMotor_onHitGround;
                        Debug.Log("removing fall dmg");
                    } */


                }

                if (fixedAge <= flyDuration)
                {
                    AltHandleMovements(aimDirection);
                }
                //HandleMovements(aimDirection);
                
                //base.characterMotor.velocity = lungeVelocity;

            }

            if (fixedAge >= flyDuration)
            {
                if (NetworkServer.active && !base.characterBody.bodyFlags.HasFlag(CharacterBody.BodyFlags.IgnoreFallDamage))
                {
                    base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
                    base.characterMotor.onHitGroundServer += this.CharacterMotor_onHitGround;
                }
            }

            if (base.isAuthority && fixedAge >= flyDuration & !hasFlown)
            {
                if (this.inputSpace)
                {
                    UnityEngine.Object.Destroy(this.inputSpace.gameObject);
                }



                base.characterMotor.disableAirControlUntilCollision = false;
                this.rotator.ResetRotation(0.3f);
                hasResetRot = true;
                base.characterMotor.velocity.y = 0f;
                this.PlayAnimation("FullBody, Override", "PulseExit", "BaseSkill.playbackRate", exitAnimDuration);//this.exitDuration);
                hasFlown = true;
                animator.SetBool("isMoving", false);

            }
            if (base.isAuthority && fixedAge >= flyDuration + exitDuration && hasFlown)
            {
                this.outer.SetNextStateToMain();
            }
        }

        //hook to charactermotor to remove fall damage
        private void CharacterMotor_onHitGround(ref CharacterMotor.HitGroundInfo hitGroundInfo)
        {
            if (base.characterBody.bodyFlags.HasFlag(CharacterBody.BodyFlags.IgnoreFallDamage))
            {
                base.characterBody.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;
            }

            base.characterMotor.onHitGroundServer -= this.CharacterMotor_onHitGround;
        }


        public override void OnExit()
        {

            base.OnExit();
            if (base.isAuthority)
            {
                if (!hasResetRot)
                {
                    if (base.characterMotor)
                    {
                        base.characterMotor.disableAirControlUntilCollision = false;
                    }
                    if (this.rotator)
                    {
                        this.rotator.ResetRotation(0.5f);
                    }


                    hasResetRot = true;
                    this.PlayAnimation("FullBody, Override", "PulseExit", "BaseSkill.playbackRate", exitAnimDuration);
                    animator.SetBool("isMoving", false);

                }

                animator.SetBool("isFlying", false);
            }
            
           

            


            //base.PlayAnimation("Body", "AscendDescend");
        }



    }
}
