// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
	using Coherence.ProtocolDef;
	using Coherence.Serializer;
	using Coherence.Brook;
	using UnityEngine;
	using Coherence.Entity;

	public struct Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyHealth__char_46_Die_3a86c700_23a2_4566_a6c0_96acf7d95428 : IEntityCommand
	{

		public MessageTarget Routing => MessageTarget.All;
		public uint GetComponentType() => Definition.InternalEnemy_7365a46dbef03be41b6173e6147d68d1_EnemyHealth__char_46_Die_3a86c700_23a2_4566_a6c0_96acf7d95428;

		public static void Serialize(Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyHealth__char_46_Die_3a86c700_23a2_4566_a6c0_96acf7d95428 commandData, IOutProtocolBitStream bitStream)
		{
		}

		public static Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyHealth__char_46_Die_3a86c700_23a2_4566_a6c0_96acf7d95428 Deserialize(IInProtocolBitStream bitStream)
		{

			return new Enemy_7365a46dbef03be41b6173e6147d68d1_EnemyHealth__char_46_Die_3a86c700_23a2_4566_a6c0_96acf7d95428
			(
			){};
		}
	}
}
