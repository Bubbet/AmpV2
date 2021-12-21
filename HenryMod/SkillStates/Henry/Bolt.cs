﻿using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;


namespace HenryMod.SkillStates
{

	public class Bolt : BaseSkillState
	{
		

		//copied volcanic egg code
		public override void OnEnter()
		{

			Ray aimRay = this.GetAimRay();

			//uncomment line below to change the object used as a vehicle to the one within the mod's asset bundle; it's glitchy as fuck and needs to be fixed so we can actually make some changes
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("BoltVehicle"), aimRay.origin, Quaternion.LookRotation(aimRay.direction));
			gameObject.AddComponent<BoltVehicle>();
			gameObject.GetComponent<VehicleSeat>().AssignPassenger(base.gameObject);
			gameObject.AddComponent<HitBox>();

			//declares object that will be used as a vehicle; in this case, the "fireballvehicle" from risk of rain 2. this uses the fireballvehicle from the game's asset bundle, so the skill works as a copy of the volcanic egg essentially right now	
			//GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/NetworkedObjects/FireballVehicle"), aimRay.origin, Quaternion.LookRotation(aimRay.direction));



			//assigns the player to the seat of the vehicle



			CharacterBody characterBody = this.characterBody;
			NetworkUser networkUser;
			if (characterBody == null)
			{
				networkUser = null;
			}
			else
			{
				CharacterMaster master = characterBody.master;
				if (master == null)
				{
					networkUser = null;
				}
				else
				{
					PlayerCharacterMasterController playerCharacterMasterController = master.playerCharacterMasterController;
					networkUser = ((playerCharacterMasterController != null) ? playerCharacterMasterController.networkUser : null);
				}
			}
			NetworkUser networkUser2 = networkUser;
			if (networkUser2)
			{
				NetworkServer.SpawnWithClientAuthority(gameObject, networkUser2.gameObject);
			}
			else
			{
				NetworkServer.Spawn(gameObject);
			}
			
		}


	}

}