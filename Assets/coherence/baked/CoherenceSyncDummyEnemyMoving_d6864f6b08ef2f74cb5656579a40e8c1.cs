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

	public class Binding_d6864f6b08ef2f74cb5656579a40e8c1_d61d2e79_306d_42db_98c8_b0918efaa3c3 : PositionBinding
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

	public class Binding_d6864f6b08ef2f74cb5656579a40e8c1_51ccd302_3433_4538_9eb0_8f1250982b4d : RotationBinding
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


	[Preserve]
	public class CoherenceSyncDummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1 : CoherenceSyncBaked
	{
		private SerializeEntityID entityId;
		private Logger logger = Log.GetLogger<CoherenceSyncDummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1>();

		// Cached targets for commands		
		private global::EnemyHealth DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69_CommandTarget;		
		private global::EnemySyncInit DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181_CommandTarget;

		private IClient client;
		private CoherenceBridge bridge;

		private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
		{
			["d61d2e79-306d-42db-98c8-b0918efaa3c3"] = new Binding_d6864f6b08ef2f74cb5656579a40e8c1_d61d2e79_306d_42db_98c8_b0918efaa3c3(),
			["51ccd302-3433-4538-9eb0-8f1250982b4d"] = new Binding_d6864f6b08ef2f74cb5656579a40e8c1_51ccd302_3433_4538_9eb0_8f1250982b4d(),
		};

		private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings =
			new Dictionary<string, Action<CommandBinding, CommandsHandler>>();

		public CoherenceSyncDummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1()
		{
			bakedCommandBindings.Add("4858caf3-7099-4fe7-9bac-245be0dcea69", BakeCommandBinding_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69);
			bakedCommandBindings.Add("036db983-680a-4f7e-a234-076b1e65a181", BakeCommandBinding_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181);
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
		private void BakeCommandBinding_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69_CommandTarget = (global::EnemyHealth)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("EnemyHealth.Die", "()",
				SendCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69, ReceiveLocalCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69, MessageTarget.All, DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69_CommandTarget,false);
		}
		private void BakeCommandBinding_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181_CommandTarget = (global::EnemySyncInit)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("EnemySyncInit.SetReferences", "(System.String)",
				SendCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181, ReceiveLocalCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181, MessageTarget.All, DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181_CommandTarget,false);
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
			this.logger = logger.With<CoherenceSyncDummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1>();
			this.bridge = bridge;
			this.entityId = entityId;
			this.client = client;
		}
		void SendCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69(MessageTarget target, object[] args)
		{
			var command = new DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69();
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69(MessageTarget target, object[] args)
		{
			var command = new DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69();
			ReceiveCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69(command);
		}

		void ReceiveCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69(DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69 command)
		{
			var target = DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69_CommandTarget;
			target.Die();
		}
		void SendCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181(MessageTarget target, object[] args)
		{
			var command = new DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181();
			int i = 0;
			command.enemySpawner = (string)((System.String)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181(MessageTarget target, object[] args)
		{
			var command = new DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181();
			int i = 0;
			command.enemySpawner = (string)((System.String)args[i++]);
			ReceiveCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181(command);
		}

		void ReceiveCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181(DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181 command)
		{
			var target = DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181_CommandTarget;
			target.SetReferences((System.String)(command.enemySpawner));
		}

		public override void ReceiveCommand(IEntityCommand command)
		{
			switch(command)
			{
				case DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69 castedCommand:
					ReceiveCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemyHealth__char_46_Die_4858caf3_7099_4fe7_9bac_245be0dcea69(castedCommand);
					break;
				case DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181 castedCommand:
					ReceiveCommand_DummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1_EnemySyncInit__char_46_SetReferences_036db983_680a_4f7e_a234_076b1e65a181(castedCommand);
					break;
				default:
					logger.Warning($"[CoherenceSyncDummyEnemyMoving_d6864f6b08ef2f74cb5656579a40e8c1] Unhandled command: {command.GetType()}.");
					break;
			}
		}
	}
}
