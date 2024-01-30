// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Coherence.Toolkit;
	using Coherence.Toolkit.Bindings;
	using Coherence.Entity;
	using Coherence.ProtocolDef;
	using Coherence.Brook;
	using Coherence.Toolkit.Bindings.ValueBindings;
	using Coherence.Toolkit.Bindings.TransformBindings;
	using Coherence.Connection;
	using Coherence.Log;
	using Logger = Coherence.Log.Logger;
	using UnityEngine.Scripting;

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_696f6b8e_6989_42d0_b2dc_00169501f2c9 : PositionBinding
	{
		public override string CoherenceComponentName => "WorldPosition";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Vector3 Value
		{
			get { return (UnityEngine.Vector3)(coherenceSync.coherencePosition); }
			set { coherenceSync.coherencePosition = (UnityEngine.Vector3)(value); }
		}

		protected override Vector3 ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((WorldPosition)coherenceComponent).value;
			if (!coherenceSync.HasParentWithCoherenceSync)
            {
                value += floatingOriginDelta;
            }
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (WorldPosition)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.value = Value;
			}
			else 
			{
				update.value = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new WorldPosition();
		}
	}

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_7602e59e_d135_46a9_bdc7_9ce8a0b4ed43 : RotationBinding
	{
		public override string CoherenceComponentName => "WorldOrientation";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Quaternion Value
		{
			get { return (UnityEngine.Quaternion)(coherenceSync.coherenceRotation); }
			set { coherenceSync.coherenceRotation = (UnityEngine.Quaternion)(value); }
		}

		protected override Quaternion ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((WorldOrientation)coherenceComponent).value;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (WorldOrientation)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.value = Value;
			}
			else 
			{
				update.value = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new WorldOrientation();
		}
	}

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_3590a821_8003_423e_af9d_f66e982ae4b9 : ScaleBinding
	{
		public override string CoherenceComponentName => "GenericScale";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Vector3 Value
		{
			get { return (UnityEngine.Vector3)(coherenceSync.coherenceLocalScale); }
			set { coherenceSync.coherenceLocalScale = (UnityEngine.Vector3)(value); }
		}

		protected override Vector3 ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((GenericScale)coherenceComponent).value;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (GenericScale)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.value = Value;
			}
			else 
			{
				update.value = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new GenericScale();
		}
	}

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_d8a93994_7659_4885_81c6_597a5c36db8a : IntBinding
	{
		private global::PlayerHealth CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::PlayerHealth)UnityComponent;
		}
		public override string CoherenceComponentName => "Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override int Value
		{
			get { return (System.Int32)(CastedUnityComponent.mainHealth); }
			set { CastedUnityComponent.mainHealth = (System.Int32)(value); }
		}

		protected override int ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999)coherenceComponent).mainHealth;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.mainHealth = Value;
			}
			else 
			{
				update.mainHealth = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999();
		}
	}

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_65b62278_76e2_4c3b_8ecb_03fb31197ca7 : BoolBinding
	{
		private global::PlayerHealth CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::PlayerHealth)UnityComponent;
		}
		public override string CoherenceComponentName => "Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999";

		public override uint FieldMask => 0b00000000000000000000000000000010;

		public override bool Value
		{
			get { return (System.Boolean)(CastedUnityComponent.playerDead); }
			set { CastedUnityComponent.playerDead = (System.Boolean)(value); }
		}

		protected override bool ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999)coherenceComponent).playerDead;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.playerDead = Value;
			}
			else 
			{
				update.playerDead = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth_7902894017888744999();
		}
	}


	[Preserve]
	public class CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada : CoherenceSyncBaked
	{
		private SerializeEntityID entityId;
		private Logger logger = Log.GetLogger<CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada>();

		// Cached targets for commands		
		private global::IceSpikesTestSpell Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d_CommandTarget;		
		private global::PlayerHealth Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e_CommandTarget;		
		private global::PlayerSpellIceDash Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e_CommandTarget;		
		private global::PlayerSpellIceDash Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514_CommandTarget;		
		private global::PlayerSpellIceWeaponEnhancement Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d_CommandTarget;		
		private global::PlayerSpellIceWeaponEnhancement Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60_CommandTarget;		
		private global::WeaponHolder Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960_CommandTarget;

		private IClient client;
		private CoherenceBridge bridge;

		private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
		{
			["696f6b8e-6989-42d0-b2dc-00169501f2c9"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_696f6b8e_6989_42d0_b2dc_00169501f2c9(),
			["7602e59e-d135-46a9-bdc7-9ce8a0b4ed43"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_7602e59e_d135_46a9_bdc7_9ce8a0b4ed43(),
			["3590a821-8003-423e-af9d-f66e982ae4b9"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_3590a821_8003_423e_af9d_f66e982ae4b9(),
			["d8a93994-7659-4885-81c6-597a5c36db8a"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_d8a93994_7659_4885_81c6_597a5c36db8a(),
			["65b62278-76e2-4c3b-8ecb-03fb31197ca7"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_65b62278_76e2_4c3b_8ecb_03fb31197ca7(),
		};

		private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings =
			new Dictionary<string, Action<CommandBinding, CommandsHandler>>();

		public CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada()
		{
			bakedCommandBindings.Add("9e7896fc-a8a5-4139-991e-5a6a7a91c14d", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d);
			bakedCommandBindings.Add("8e8dcd6f-8f34-45e3-8311-df59eaf4787e", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e);
			bakedCommandBindings.Add("c9d1c20a-c487-4558-8ff5-21571fa3ca1e", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e);
			bakedCommandBindings.Add("41d26fe1-5f49-44a8-885c-7a6c1958d514", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514);
			bakedCommandBindings.Add("c41bab82-6099-432e-9766-0df6ba37ab5d", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d);
			bakedCommandBindings.Add("24b474a5-887c-40b7-ba0d-bda692d74c60", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60);
			bakedCommandBindings.Add("21bd6c41-31e7-4e15-88e2-e887d233b960", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960);
		}

		public override Binding BakeValueBinding(Binding valueBinding)
		{
			if (bakedValueBindings.TryGetValue(valueBinding.guid, out var bakedBinding))
			{
				valueBinding.CloneTo(bakedBinding);
				return bakedBinding;
			}

			return null;
		}

		public override void BakeCommandBinding(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			if (bakedCommandBindings.TryGetValue(commandBinding.guid, out var commandBindingBaker))
			{
				commandBindingBaker.Invoke(commandBinding, commandsHandler);
			}
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d_CommandTarget = (global::IceSpikesTestSpell)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("IceSpikesTestSpell.CastSpikes", "(System.Boolean)",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e_CommandTarget = (global::PlayerHealth)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("PlayerHealth.PlayerDown", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e_CommandTarget = (global::PlayerSpellIceDash)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("PlayerSpellIceDash.CreateVFX", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514_CommandTarget = (global::PlayerSpellIceDash)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("PlayerSpellIceDash.DashFinished", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d_CommandTarget = (global::PlayerSpellIceWeaponEnhancement)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("PlayerSpellIceWeaponEnhancement.ApplyEnhancement", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60_CommandTarget = (global::PlayerSpellIceWeaponEnhancement)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("PlayerSpellIceWeaponEnhancement.RemoveEnhancement", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960_CommandTarget = (global::WeaponHolder)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("WeaponHolder.Shoot", "(UnityEngine.Vector3UnityEngine.Vector3)",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960_CommandTarget,false);
		}

		public override List<ICoherenceComponentData> CreateEntity(bool usesLodsAtRuntime, string archetypeName)
		{
			if (!usesLodsAtRuntime)
			{
				return null;
			}

			if (Archetypes.IndexForName.TryGetValue(archetypeName, out int archetypeIndex))
			{
				var components = new List<ICoherenceComponentData>()
				{
					new ArchetypeComponent
					{
						index = archetypeIndex
					}
				};

				return components;
			}
	
			logger.Warning($"Unable to find archetype {archetypeName} in dictionary. Please, bake manually (coherence > Bake)");

			return null;
		}

		public override void Dispose()
		{
		}

		public override void Initialize(SerializeEntityID entityId, CoherenceBridge bridge, IClient client, CoherenceInput input, Logger logger)
		{
			this.logger = logger.With<CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada>();
			this.bridge = bridge;
			this.entityId = entityId;
			this.client = client;
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d();
			int i = 0;
			command.demonic = (bool)((System.Boolean)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d();
			int i = 0;
			command.demonic = (bool)((System.Boolean)args[i++]);
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d(Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d_CommandTarget;
			target.CastSpikes((System.Boolean)(command.demonic));
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e(Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e_CommandTarget;
			target.PlayerDown();
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e(Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e_CommandTarget;
			target.CreateVFX();
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514(Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514_CommandTarget;
			target.DashFinished();
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d(Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d_CommandTarget;
			target.ApplyEnhancement();
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60(Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60_CommandTarget;
			target.RemoveEnhancement();
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960();
			int i = 0;
			command.straight = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.originPos = (Vector3)((UnityEngine.Vector3)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960();
			int i = 0;
			command.straight = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.originPos = (Vector3)((UnityEngine.Vector3)args[i++]);
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960(Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960_CommandTarget;
			target.Shoot((UnityEngine.Vector3)(command.straight),(UnityEngine.Vector3)(command.originPos));
		}

		public override void ReceiveCommand(IEntityCommand command)
		{
			switch(command)
			{
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_IceSpikesTestSpell__char_46_CastSpikes_9e7896fc_a8a5_4139_991e_5a6a7a91c14d(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8e8dcd6f_8f34_45e3_8311_df59eaf4787e(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_CreateVFX_c9d1c20a_c487_4558_8ff5_21571fa3ca1e(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceDash__char_46_DashFinished_41d26fe1_5f49_44a8_885c_7a6c1958d514(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_ApplyEnhancement_c41bab82_6099_432e_9766_0df6ba37ab5d(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerSpellIceWeaponEnhancement__char_46_RemoveEnhancement_24b474a5_887c_40b7_ba0d_bda692d74c60(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_21bd6c41_31e7_4e15_88e2_e887d233b960(castedCommand);
					break;
				default:
					logger.Warning($"[CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada] Unhandled command: {command.GetType()}.");
					break;
			}
		}
	}
}
