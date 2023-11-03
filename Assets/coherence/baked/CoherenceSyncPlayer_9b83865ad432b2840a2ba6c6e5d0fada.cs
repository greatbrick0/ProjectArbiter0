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

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_3d22ccd7_07c4_4bda_b0b6_5862e909674f : PositionBinding
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

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_2c785a2d_c548_42de_8412_2740b17abb15 : RotationBinding
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

	public class Binding_9b83865ad432b2840a2ba6c6e5d0fada_949f846f_8e45_4bea_9851_d48e7073913c : ScaleBinding
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


	[Preserve]
	public class CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada : CoherenceSyncBaked
	{
		private SerializeEntityID entityId;
		private Logger logger = Log.GetLogger<CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada>();

		// Cached targets for commands		
		private global::WeaponHolder Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42_CommandTarget;		
		private global::WeaponHolder Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8_CommandTarget;		
		private global::WeaponHolder Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52_CommandTarget;

		private IClient client;
		private CoherenceBridge bridge;

		private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
		{
			["3d22ccd7-07c4-4bda-b0b6-5862e909674f"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_3d22ccd7_07c4_4bda_b0b6_5862e909674f(),
			["2c785a2d-c548-42de-8412-2740b17abb15"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_2c785a2d_c548_42de_8412_2740b17abb15(),
			["949f846f-8e45-4bea-9851-d48e7073913c"] = new Binding_9b83865ad432b2840a2ba6c6e5d0fada_949f846f_8e45_4bea_9851_d48e7073913c(),
		};

		private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings =
			new Dictionary<string, Action<CommandBinding, CommandsHandler>>();

		public CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada()
		{
			bakedCommandBindings.Add("be751543-a7a1-4c84-9276-d6ac1b2f8d42", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42);
			bakedCommandBindings.Add("1f35b1c6-c080-4ac3-8ecc-9a03c2b39ed8", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8);
			bakedCommandBindings.Add("d5156e8b-8a71-4680-94af-0028207abe52", BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52);
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
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42_CommandTarget = (global::WeaponHolder)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("WeaponHolder.EndInput", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8_CommandTarget = (global::WeaponHolder)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("WeaponHolder.Shoot", "(UnityEngine.Vector3UnityEngine.Vector3)",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8_CommandTarget,false);
		}
		private void BakeCommandBinding_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52_CommandTarget = (global::WeaponHolder)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("WeaponHolder.StartInput", "()",
				SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52, ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52, MessageTarget.All, Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52_CommandTarget,false);
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
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42(Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42_CommandTarget;
			target.EndInput();
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8();
			int i = 0;
			command.straight = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.originPos = (Vector3)((UnityEngine.Vector3)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8();
			int i = 0;
			command.straight = (Vector3)((UnityEngine.Vector3)args[i++]);
			command.originPos = (Vector3)((UnityEngine.Vector3)args[i++]);
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8(Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8_CommandTarget;
			target.Shoot((UnityEngine.Vector3)(command.straight),(UnityEngine.Vector3)(command.originPos));
		}
		void SendCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52(MessageTarget target, object[] args)
		{
			var command = new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52();
			ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52(command);
		}

		void ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52(Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52 command)
		{
			var target = Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52_CommandTarget;
			target.StartInput();
		}

		public override void ReceiveCommand(IEntityCommand command)
		{
			switch(command)
			{
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_EndInput_be751543_a7a1_4c84_9276_d6ac1b2f8d42(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8(castedCommand);
					break;
				case Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52 castedCommand:
					ReceiveCommand_Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_StartInput_d5156e8b_8a71_4680_94af_0028207abe52(castedCommand);
					break;
				default:
					logger.Warning($"[CoherenceSyncPlayer_9b83865ad432b2840a2ba6c6e5d0fada] Unhandled command: {command.GetType()}.");
					break;
			}
		}
	}
}
