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

	public struct TurretEnemy_05434230e22f555459cd21106aeb67e2_EnemySyncInit__char_46_SetReferences_0fdf290c_9c68_42c4_b1fa_8ba86e34a1ba : IEntityCommand
	{
		public string enemySpawner;

		public MessageTarget Routing => MessageTarget.All;
		public uint GetComponentType() => Definition.InternalTurretEnemy_05434230e22f555459cd21106aeb67e2_EnemySyncInit__char_46_SetReferences_0fdf290c_9c68_42c4_b1fa_8ba86e34a1ba;

		public TurretEnemy_05434230e22f555459cd21106aeb67e2_EnemySyncInit__char_46_SetReferences_0fdf290c_9c68_42c4_b1fa_8ba86e34a1ba
		(
			string dataenemySpawner
		)
		{
			enemySpawner = dataenemySpawner;
		}

		public static void Serialize(TurretEnemy_05434230e22f555459cd21106aeb67e2_EnemySyncInit__char_46_SetReferences_0fdf290c_9c68_42c4_b1fa_8ba86e34a1ba commandData, IOutProtocolBitStream bitStream)
		{
			bitStream.WriteShortString(commandData.enemySpawner);
		}

		public static TurretEnemy_05434230e22f555459cd21106aeb67e2_EnemySyncInit__char_46_SetReferences_0fdf290c_9c68_42c4_b1fa_8ba86e34a1ba Deserialize(IInProtocolBitStream bitStream)
		{
			var dataenemySpawner = bitStream.ReadShortString();

			return new TurretEnemy_05434230e22f555459cd21106aeb67e2_EnemySyncInit__char_46_SetReferences_0fdf290c_9c68_42c4_b1fa_8ba86e34a1ba
			(
				dataenemySpawner
			){};
		}
	}
}
