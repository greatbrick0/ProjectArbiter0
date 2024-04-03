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

	public struct EnemyC_75a6438d17923ed4bbf57e0d1b70a8fa_EnemySyncInit__char_46_SetReferences_59c44945_06c3_4fe7_81df_2a7111dcfc20 : IEntityCommand
	{
		public string enemySpawner;

		public MessageTarget Routing => MessageTarget.All;
		public uint GetComponentType() => Definition.InternalEnemyC_75a6438d17923ed4bbf57e0d1b70a8fa_EnemySyncInit__char_46_SetReferences_59c44945_06c3_4fe7_81df_2a7111dcfc20;

		public EnemyC_75a6438d17923ed4bbf57e0d1b70a8fa_EnemySyncInit__char_46_SetReferences_59c44945_06c3_4fe7_81df_2a7111dcfc20
		(
			string dataenemySpawner
		)
		{
			enemySpawner = dataenemySpawner;
		}

		public static void Serialize(EnemyC_75a6438d17923ed4bbf57e0d1b70a8fa_EnemySyncInit__char_46_SetReferences_59c44945_06c3_4fe7_81df_2a7111dcfc20 commandData, IOutProtocolBitStream bitStream)
		{
			bitStream.WriteShortString(commandData.enemySpawner);
		}

		public static EnemyC_75a6438d17923ed4bbf57e0d1b70a8fa_EnemySyncInit__char_46_SetReferences_59c44945_06c3_4fe7_81df_2a7111dcfc20 Deserialize(IInProtocolBitStream bitStream)
		{
			var dataenemySpawner = bitStream.ReadShortString();

			return new EnemyC_75a6438d17923ed4bbf57e0d1b70a8fa_EnemySyncInit__char_46_SetReferences_59c44945_06c3_4fe7_81df_2a7111dcfc20
			(
				dataenemySpawner
			){};
		}
	}
}
