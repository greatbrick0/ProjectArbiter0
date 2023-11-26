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

	public struct Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8 : IEntityCommand
	{
		public Vector3 straight;
		public Vector3 originPos;

		public MessageTarget Routing => MessageTarget.All;
		public uint GetComponentType() => Definition.InternalPlayer_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8;

		public Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8
		(
			Vector3 datastraight,
			Vector3 dataoriginPos
		)
		{
			straight = datastraight;
			originPos = dataoriginPos;
		}

		public static void Serialize(Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8 commandData, IOutProtocolBitStream bitStream)
		{
			var converted_straight = commandData.straight.ToCoreVector3();
			bitStream.WriteVector3(converted_straight, FloatMeta.NoCompression());
			var converted_originPos = commandData.originPos.ToCoreVector3();
			bitStream.WriteVector3(converted_originPos, FloatMeta.NoCompression());
		}

		public static Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8 Deserialize(IInProtocolBitStream bitStream)
		{
			var converted_straight = bitStream.ReadVector3(FloatMeta.NoCompression());
			var datastraight = converted_straight.ToUnityVector3();
			var converted_originPos = bitStream.ReadVector3(FloatMeta.NoCompression());
			var dataoriginPos = converted_originPos.ToUnityVector3();

			return new Player_9b83865ad432b2840a2ba6c6e5d0fada_WeaponHolder__char_46_Shoot_1f35b1c6_c080_4ac3_8ecc_9a03c2b39ed8
			(
				datastraight,
				dataoriginPos
			){};
		}
	}
}
