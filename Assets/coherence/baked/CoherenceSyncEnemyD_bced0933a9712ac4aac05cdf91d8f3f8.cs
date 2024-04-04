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

	public class Binding_bced0933a9712ac4aac05cdf91d8f3f8_9e894389_6baf_4253_9772_c2f917e4844d : PositionBinding
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

	public class Binding_bced0933a9712ac4aac05cdf91d8f3f8_f8e80bb8_4737_4910_b90d_b2e0ea0526bc : RotationBinding
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

	public class Binding_bced0933a9712ac4aac05cdf91d8f3f8_e0165f82_b8dd_4491_b410_2c9d1d91e2d0 : IntBinding
	{
		private global::EnemyHealth CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::EnemyHealth)UnityComponent;
		}
		public override string CoherenceComponentName => "EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemyHealth_6193124714125256054";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override int Value
		{
			get { return (System.Int32)(CastedUnityComponent.health); }
			set { CastedUnityComponent.health = (System.Int32)(value); }
		}

		protected override int ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemyHealth_6193124714125256054)coherenceComponent).health;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemyHealth_6193124714125256054)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.health = Value;
			}
			else 
			{
				update.health = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemyHealth_6193124714125256054();
		}
	}


	[Preserve]
	public class CoherenceSyncEnemyD_bced0933a9712ac4aac05cdf91d8f3f8 : CoherenceSyncBaked
	{
		private SerializeEntityID entityId;
		private Logger logger = Log.GetLogger<CoherenceSyncEnemyD_bced0933a9712ac4aac05cdf91d8f3f8>();

		// Cached targets for commands		
		private global::EnemySyncInit EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290_CommandTarget;

		private IClient client;
		private CoherenceBridge bridge;

		private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
		{
			["9e894389-6baf-4253-9772-c2f917e4844d"] = new Binding_bced0933a9712ac4aac05cdf91d8f3f8_9e894389_6baf_4253_9772_c2f917e4844d(),
			["f8e80bb8-4737-4910-b90d-b2e0ea0526bc"] = new Binding_bced0933a9712ac4aac05cdf91d8f3f8_f8e80bb8_4737_4910_b90d_b2e0ea0526bc(),
			["e0165f82-b8dd-4491-b410-2c9d1d91e2d0"] = new Binding_bced0933a9712ac4aac05cdf91d8f3f8_e0165f82_b8dd_4491_b410_2c9d1d91e2d0(),
		};

		private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings =
			new Dictionary<string, Action<CommandBinding, CommandsHandler>>();

		public CoherenceSyncEnemyD_bced0933a9712ac4aac05cdf91d8f3f8()
		{
			bakedCommandBindings.Add("722794ab-8faa-4e48-b5da-23968464a290", BakeCommandBinding_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290);
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
		private void BakeCommandBinding_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290_CommandTarget = (global::EnemySyncInit)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("EnemySyncInit.SetReferences", "(System.String)",
				SendCommand_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290, ReceiveLocalCommand_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290, MessageTarget.All, EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290_CommandTarget,false);
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
			this.logger = logger.With<CoherenceSyncEnemyD_bced0933a9712ac4aac05cdf91d8f3f8>();
			this.bridge = bridge;
			this.entityId = entityId;
			this.client = client;
		}
		void SendCommand_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290(MessageTarget target, object[] args)
		{
			var command = new EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290();
			int i = 0;
			command.enemySpawner = (string)((System.String)args[i++]);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290(MessageTarget target, object[] args)
		{
			var command = new EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290();
			int i = 0;
			command.enemySpawner = (string)((System.String)args[i++]);
			ReceiveCommand_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290(command);
		}

		void ReceiveCommand_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290(EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290 command)
		{
			var target = EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290_CommandTarget;
			target.SetReferences((System.String)(command.enemySpawner));
		}

		public override void ReceiveCommand(IEntityCommand command)
		{
			switch(command)
			{
				case EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290 castedCommand:
					ReceiveCommand_EnemyD_bced0933a9712ac4aac05cdf91d8f3f8_EnemySyncInit__char_46_SetReferences_722794ab_8faa_4e48_b5da_23968464a290(castedCommand);
					break;
				default:
					logger.Warning($"[CoherenceSyncEnemyD_bced0933a9712ac4aac05cdf91d8f3f8] Unhandled command: {command.GetType()}.");
					break;
			}
		}
	}
}
