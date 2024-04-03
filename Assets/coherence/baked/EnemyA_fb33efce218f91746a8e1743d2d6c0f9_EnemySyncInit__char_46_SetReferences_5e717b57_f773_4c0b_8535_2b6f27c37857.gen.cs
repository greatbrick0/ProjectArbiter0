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

	public struct EnemyA_fb33efce218f91746a8e1743d2d6c0f9_EnemySyncInit__char_46_SetReferences_5e717b57_f773_4c0b_8535_2b6f27c37857 : IEntityCommand
	{
		public string enemySpawner;

		public MessageTarget Routing => MessageTarget.All;
		public uint GetComponentType() => Definition.InternalEnemyA_fb33efce218f91746a8e1743d2d6c0f9_EnemySyncInit__char_46_SetReferences_5e717b57_f773_4c0b_8535_2b6f27c37857;

		public EnemyA_fb33efce218f91746a8e1743d2d6c0f9_EnemySyncInit__char_46_SetReferences_5e717b57_f773_4c0b_8535_2b6f27c37857
		(
			string dataenemySpawner
		)
		{
			enemySpawner = dataenemySpawner;
		}

		public static void Serialize(EnemyA_fb33efce218f91746a8e1743d2d6c0f9_EnemySyncInit__char_46_SetReferences_5e717b57_f773_4c0b_8535_2b6f27c37857 commandData, IOutProtocolBitStream bitStream)
		{
			bitStream.WriteShortString(commandData.enemySpawner);
		}

		public static EnemyA_fb33efce218f91746a8e1743d2d6c0f9_EnemySyncInit__char_46_SetReferences_5e717b57_f773_4c0b_8535_2b6f27c37857 Deserialize(IInProtocolBitStream bitStream)
		{
			var dataenemySpawner = bitStream.ReadShortString();

			return new EnemyA_fb33efce218f91746a8e1743d2d6c0f9_EnemySyncInit__char_46_SetReferences_5e717b57_f773_4c0b_8535_2b6f27c37857
			(
				dataenemySpawner
			){};
		}
	}
}
