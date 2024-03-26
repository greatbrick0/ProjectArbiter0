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

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_4f88e77b_cc25_44e0_945c_3463b7c14b3d : BoolBinding
	{
		private global::PlayerAnimation CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::PlayerAnimation)UnityComponent;
		}
		public override string CoherenceComponentName => "Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerAnimation_8127347517799184014";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override bool Value
		{
			get { return (System.Boolean)(CastedUnityComponent.walking); }
			set { CastedUnityComponent.walking = (System.Boolean)(value); }
		}

		protected override bool ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerAnimation_8127347517799184014)coherenceComponent).walking;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerAnimation_8127347517799184014)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.walking = Value;
			}
			else 
			{
				update.walking = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerAnimation_8127347517799184014();
		}
	}

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_67b8021e_626b_4a0c_987b_0a90e1741376 : BoolAnimatorParameterBinding
	{
		private global::UnityEngine.Animator CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Animator)UnityComponent;
		}
		public override string CoherenceComponentName => "Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Animator_8450259073051062080";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override bool Value
		{
			get { return (CastedUnityComponent.GetBool(CastedDescriptor.ParameterHash)); }
			set { CastedUnityComponent.SetBool(CastedDescriptor.ParameterHash, (value)); }
		}

		protected override bool ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Animator_8450259073051062080)coherenceComponent).walking;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Animator_8450259073051062080)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.walking = Value;
			}
			else 
			{
				update.walking = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Animator_8450259073051062080();
		}
	}


	[Preserve]
	public class CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada : CoherenceSyncBaked
	{
		private SerializeEntityID entityId;
		private Logger logger = Log.GetLogger<CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada>();

		// Cached targets for commands		
		private global::AbilityInputSystem Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da_CommandTarget;		
		private global::PlayerHealth Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1_CommandTarget;		
		private global::SanitySystem Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096_CommandTarget;		
		private global::WeaponHolder Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d_CommandTarget;

		private IClient client;
		private CoherenceBridge bridge;

		private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
		{
			["696f6b8e-6989-42d0-b2dc-00169501f2c9"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_696f6b8e_6989_42d0_b2dc_00169501f2c9(),
			["7602e59e-d135-46a9-bdc7-9ce8a0b4ed43"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_7602e59e_d135_46a9_bdc7_9ce8a0b4ed43(),
			["3590a821-8003-423e-af9d-f66e982ae4b9"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_3590a821_8003_423e_af9d_f66e982ae4b9(),
			["4f88e77b-cc25-44e0-945c-3463b7c14b3d"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_4f88e77b_cc25_44e0_945c_3463b7c14b3d(),
			["67b8021e-626b-4a0c-987b-0a90e1741376"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_67b8021e_626b_4a0c_987b_0a90e1741376(),
		};

		private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings =
			new Dictionary<string, Action<CommandBinding, CommandsHandler>>();

		public CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada()
		{
			bakedCommandBindings.Add("09631ba5-b8ad-4b21-8720-2dcef6abf6da", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da);
			bakedCommandBindings.Add("8ed7af65-8faa-4df4-a8bc-d05c5ec8b2a1", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1);
			bakedCommandBindings.Add("b60d8eb5-ad38-4c3c-ac1f-824f2ecab096", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096);
			bakedCommandBindings.Add("4580daad-388d-444f-92c3-72b98a4d593d", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d);
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
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da_CommandTarget = (global::AbilityInputSystem)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("AbilityInputSystem.SendCastToAbility", "(System.Int32System.Boolean)",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1_CommandTarget = (global::PlayerHealth)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("PlayerHealth.PlayerDown", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096_CommandTarget = (global::SanitySystem)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("SanitySystem.ShowAura", "(System.Boolean)",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d_CommandTarget = (global::WeaponHolder)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("WeaponHolder.Shoot", "(UnityEngine.Vector3UnityEngine.Vector3UnityEngine.Vector3)",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d_CommandTarget,false);
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
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da();
			int i = 0;
			command.tier = (int)((System.Int32)args[i++]);
			command.doDemon = (bool)((System.Boolean)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da();
			int i = 0;
			command.tier = (int)((System.Int32)args[i++]);
			command.doDemon = (bool)((System.Boolean)args[i++]);
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da(Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da_CommandTarget;
			target.SendCastToAbility((System.Int32)(command.tier),(System.Boolean)(command.doDemon));
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1(Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1_CommandTarget;
			target.PlayerDown();
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096();
			int i = 0;
			command.useAura = (bool)((System.Boolean)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096();
			int i = 0;
			command.useAura = (bool)((System.Boolean)args[i++]);
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096(Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096_CommandTarget;
			target.ShowAura((System.Boolean)(command.useAura));
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d();
			int i = 0;
			command.straight = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.up = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.originPos = (Vector3)((UnityEngine.Vector3)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d();
			int i = 0;
			command.straight = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.up = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.originPos = (Vector3)((UnityEngine.Vector3)args[i++]);
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d(Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d_CommandTarget;
			target.Shoot((UnityEngine.Vector3)(command.straight),(UnityEngine.Vector3)(command.up),(UnityEngine.Vector3)(command.originPos));
		}

		public override void ReceiveCommand(IEntityCommand command)
		{
			switch(command)
			{
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_AbilityInputSystem__char_46_SendCastToAbility_09631ba5_b8ad_4b21_8720_2dcef6abf6da(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_PlayerHealth__char_46_PlayerDown_8ed7af65_8faa_4df4_a8bc_d05c5ec8b2a1(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_SanitySystem__char_46_ShowAura_b60d8eb5_ad38_4c3c_ac1f_824f2ecab096(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_4580daad_388d_444f_92c3_72b98a4d593d(castedCommand);
					break;
				default:
					logger.Warning($"[CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada] Unhandled command: {command.GetType()}.");
					break;
			}
		}
	}
}
