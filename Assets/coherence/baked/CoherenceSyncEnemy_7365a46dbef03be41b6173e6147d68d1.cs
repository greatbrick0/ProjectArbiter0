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

	public class Binding_7365a46dbef03be41b6173e6147d68d1_60e5ca40_27c3_4173_bf68_8b9cb3bd0b0c : PositionBinding
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

	public class Binding_7365a46dbef03be41b6173e6147d68d1_a9a952bb_dcc8_4176_9348_3a2bf706041a : RotationBinding
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

	public class Binding_7365a46dbef03be41b6173e6147d68d1_4a26bfeb_4c31_4585_a3ef_e53e62fabdf9 : BoolBinding
	{
		private global::EnemyAnimation CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::EnemyAnimation)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override bool Value
		{
			get { return (System.Boolean)(CastedUnityComponent.walking); }
			set { CastedUnityComponent.walking = (System.Boolean)(value); }
		}

		protected override bool ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866)coherenceComponent).walking;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866)coherenceComponent;
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
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_a5302f1f_2dec_46fe_8d89_11f98fdb6f9e : FloatBinding
	{
		private global::EnemyAnimation CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::EnemyAnimation)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866";

		public override uint FieldMask => 0b00000000000000000000000000000010;

		public override float Value
		{
			get { return (System.Single)(CastedUnityComponent.directionAngle); }
			set { CastedUnityComponent.directionAngle = (System.Single)(value); }
		}

		protected override float ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866)coherenceComponent).directionAngle;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.directionAngle = Value;
			}
			else 
			{
				update.directionAngle = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyAnimation_6956384395590712866();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_8575dfe7_65b2_4c10_8eb5_7bc8a9416699 : BoolAnimatorParameterBinding
	{
		private global::UnityEngine.Animator CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Animator)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override bool Value
		{
			get { return (CastedUnityComponent.GetBool(CastedDescriptor.ParameterHash)); }
			set { CastedUnityComponent.SetBool(CastedDescriptor.ParameterHash, (value)); }
		}

		protected override bool ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039)coherenceComponent).Walking;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.Walking = Value;
			}
			else 
			{
				update.Walking = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_77c2b974_07dc_44f7_b79e_03f1a391d022 : FloatAnimatorParameterBinding
	{
		private global::UnityEngine.Animator CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Animator)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039";

		public override uint FieldMask => 0b00000000000000000000000000000010;

		public override float Value
		{
			get { return (CastedUnityComponent.GetFloat(CastedDescriptor.ParameterHash)); }
			set { CastedUnityComponent.SetFloat(CastedDescriptor.ParameterHash, (value)); }
		}

		protected override float ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039)coherenceComponent).Angle;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.Angle = Value;
			}
			else 
			{
				update.Angle = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Animator_3583843158289549039();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_f24e2e34_9299_4e0b_baa6_04e982931162 : DeepPositionBinding
	{
		private global::UnityEngine.Transform CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Vector3 Value
		{
			get { return (UnityEngine.Vector3)(CastedUnityComponent.localPosition); }
			set { CastedUnityComponent.localPosition = (UnityEngine.Vector3)(value); }
		}

		protected override Vector3 ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910)coherenceComponent).position;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.position = Value;
			}
			else 
			{
				update.position = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_9ae6dfbf_47b4_4197_a41d_11e4dd8c1f80 : DeepRotationBinding
	{
		private global::UnityEngine.Transform CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910";

		public override uint FieldMask => 0b00000000000000000000000000000010;

		public override Quaternion Value
		{
			get { return (UnityEngine.Quaternion)(CastedUnityComponent.localRotation); }
			set { CastedUnityComponent.localRotation = (UnityEngine.Quaternion)(value); }
		}

		protected override Quaternion ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910)coherenceComponent).rotation;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.rotation = Value;
			}
			else 
			{
				update.rotation = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_2950839971944629910();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_881917c2_b0ab_4081_961c_576f97318917 : DeepPositionBinding
	{
		private global::UnityEngine.Transform CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Vector3 Value
		{
			get { return (UnityEngine.Vector3)(CastedUnityComponent.localPosition); }
			set { CastedUnityComponent.localPosition = (UnityEngine.Vector3)(value); }
		}

		protected override Vector3 ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147)coherenceComponent).position;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.position = Value;
			}
			else 
			{
				update.position = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_3157e208_ad9f_4265_af7e_e3f798be6897 : DeepRotationBinding
	{
		private global::UnityEngine.Transform CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147";

		public override uint FieldMask => 0b00000000000000000000000000000010;

		public override Quaternion Value
		{
			get { return (UnityEngine.Quaternion)(CastedUnityComponent.localRotation); }
			set { CastedUnityComponent.localRotation = (UnityEngine.Quaternion)(value); }
		}

		protected override Quaternion ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147)coherenceComponent).rotation;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.rotation = Value;
			}
			else 
			{
				update.rotation = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_3402058366864678147();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_00710810_469a_45eb_95ad_2840243b5cc5 : DeepPositionBinding
	{
		private global::UnityEngine.Transform CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301";

		public override uint FieldMask => 0b00000000000000000000000000000001;

		public override Vector3 Value
		{
			get { return (UnityEngine.Vector3)(CastedUnityComponent.localPosition); }
			set { CastedUnityComponent.localPosition = (UnityEngine.Vector3)(value); }
		}

		protected override Vector3 ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301)coherenceComponent).position;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.position = Value;
			}
			else 
			{
				update.position = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301();
		}
	}

	public class Binding_7365a46dbef03be41b6173e6147d68d1_22fd91b6_3a83_4c7a_ac68_13a1028116f8 : DeepRotationBinding
	{
		private global::UnityEngine.Transform CastedUnityComponent;

		protected override void OnBindingCloned()
		{
			CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
		}
		public override string CoherenceComponentName => "Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301";

		public override uint FieldMask => 0b00000000000000000000000000000010;

		public override Quaternion Value
		{
			get { return (UnityEngine.Quaternion)(CastedUnityComponent.localRotation); }
			set { CastedUnityComponent.localRotation = (UnityEngine.Quaternion)(value); }
		}

		protected override Quaternion ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
		{
			var value = ((Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301)coherenceComponent).rotation;
			return value;
		}
		
		public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, double time)
		{
			var update = (Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301)coherenceComponent;
			if (RuntimeInterpolationSettings.IsInterpolationNone) 
			{
				update.rotation = Value;
			}
			else 
			{
				update.rotation = GetInterpolatedAt(time);
			}
			return update;
		}

		public override ICoherenceComponentData CreateComponentData()
		{
			return new Enemy_7365a46dbef03be41b6173e6147d68d1_UnityEngine__char_46_Transform_7710182724903923301();
		}
	}


	[Preserve]
	public class CoherenceSyncEnemy_7365a46dbef03be41b6173e6147d68d1 : CoherenceSyncBaked
	{
		private SerializeEntityID entityId;
		private Logger logger = Log.GetLogger<CoherenceSyncEnemy_7365a46dbef03be41b6173e6147d68d1>();

		// Cached targets for commands		
		private global::EnemySyncInit Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209_CommandTarget;

		private IClient client;
		private CoherenceBridge bridge;

		private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
		{
			["60e5ca40-27c3-4173-bf68-8b9cb3bd0b0c"] = new Binding_7365a46dbef03be41b6173e6147d68d1_60e5ca40_27c3_4173_bf68_8b9cb3bd0b0c(),
			["a9a952bb-dcc8-4176-9348-3a2bf706041a"] = new Binding_7365a46dbef03be41b6173e6147d68d1_a9a952bb_dcc8_4176_9348_3a2bf706041a(),
			["4a26bfeb-4c31-4585-a3ef-e53e62fabdf9"] = new Binding_7365a46dbef03be41b6173e6147d68d1_4a26bfeb_4c31_4585_a3ef_e53e62fabdf9(),
			["a5302f1f-2dec-46fe-8d89-11f98fdb6f9e"] = new Binding_7365a46dbef03be41b6173e6147d68d1_a5302f1f_2dec_46fe_8d89_11f98fdb6f9e(),
			["8575dfe7-65b2-4c10-8eb5-7bc8a9416699"] = new Binding_7365a46dbef03be41b6173e6147d68d1_8575dfe7_65b2_4c10_8eb5_7bc8a9416699(),
			["77c2b974-07dc-44f7-b79e-03f1a391d022"] = new Binding_7365a46dbef03be41b6173e6147d68d1_77c2b974_07dc_44f7_b79e_03f1a391d022(),
			["f24e2e34-9299-4e0b-baa6-04e982931162"] = new Binding_7365a46dbef03be41b6173e6147d68d1_f24e2e34_9299_4e0b_baa6_04e982931162(),
			["9ae6dfbf-47b4-4197-a41d-11e4dd8c1f80"] = new Binding_7365a46dbef03be41b6173e6147d68d1_9ae6dfbf_47b4_4197_a41d_11e4dd8c1f80(),
			["881917c2-b0ab-4081-961c-576f97318917"] = new Binding_7365a46dbef03be41b6173e6147d68d1_881917c2_b0ab_4081_961c_576f97318917(),
			["3157e208-ad9f-4265-af7e-e3f798be6897"] = new Binding_7365a46dbef03be41b6173e6147d68d1_3157e208_ad9f_4265_af7e_e3f798be6897(),
			["00710810-469a-45eb-95ad-2840243b5cc5"] = new Binding_7365a46dbef03be41b6173e6147d68d1_00710810_469a_45eb_95ad_2840243b5cc5(),
			["22fd91b6-3a83-4c7a-ac68-13a1028116f8"] = new Binding_7365a46dbef03be41b6173e6147d68d1_22fd91b6_3a83_4c7a_ac68_13a1028116f8(),
		};

		private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings =
			new Dictionary<string, Action<CommandBinding, CommandsHandler>>();

		public CoherenceSyncEnemy_7365a46dbef03be41b6173e6147d68d1()
		{
			bakedCommandBindings.Add("88fa8c05-3997-4a41-aedd-c5155114f209", BakeCommandBinding_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209);
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
		private void BakeCommandBinding_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209(CommandBinding commandBinding, CommandsHandler commandsHandler)
		{
			Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209_CommandTarget = (global::EnemySyncInit)commandBinding.UnityComponent;
			commandsHandler.AddBakedCommand("EnemySyncInit.SetReferences", "(UnityEngine.GameObject)",
				SendCommand_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209, ReceiveLocalCommand_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209, MessageTarget.All, Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209_CommandTarget,false);
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
			this.logger = logger.With<CoherenceSyncEnemy_7365a46dbef03be41b6173e6147d68d1>();
			this.bridge = bridge;
			this.entityId = entityId;
			this.client = client;
		}
		void SendCommand_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209(MessageTarget target, object[] args)
		{
			var command = new Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209();
			int i = 0;
			command.enemySpawner = (SerializeEntityID)bridge.UnityObjectToEntityId(args[i++] as GameObject);
			client.SendCommand(command, target, entityId);
		}

		void ReceiveLocalCommand_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209(MessageTarget target, object[] args)
		{
			var command = new Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209();
			int i = 0;
			command.enemySpawner = (SerializeEntityID)bridge.UnityObjectToEntityId(args[i++] as GameObject);
			ReceiveCommand_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209(command);
		}

		void ReceiveCommand_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209(Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209 command)
		{
			var target = Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209_CommandTarget;
			target.SetReferences(bridge.EntityIdToGameObject(command.enemySpawner));
		}

		public override void ReceiveCommand(IEntityCommand command)
		{
			switch(command)
			{
				case Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209 castedCommand:
					ReceiveCommand_Enemy_7365a46dbef03be41b6173e6147d68d1_EnemySyncInit__char_46_SetReferences_88fa8c05_3997_4a41_aedd_c5155114f209(castedCommand);
					break;
				default:
					logger.Warning($"[CoherenceSyncEnemy_7365a46dbef03be41b6173e6147d68d1] Unhandled command: {command.GetType()}.");
					break;
			}
		}
	}
}
