// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
	using Coherence.ProtocolDef;
	using Coherence.Serializer;
	using Coherence.SimulationFrame;
	using Coherence.Entity;
	using Coherence.Utils;
	using Coherence.Brook;
	using Coherence.Toolkit;
	using UnityEngine;

	public struct Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147 : ICoherenceComponentData
	{
		public Quaternion rotation;
		public Vector3 position;

		public override string ToString()
		{
			return $"Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147(rotation: {rotation}, position: {position})";
		}

		public uint GetComponentType() => Definition.InternalPlayer_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147;

		public const int order = 0;

		public uint FieldsMask => 0b00000000000000000000000000000011;

		public int GetComponentOrder() => order;
		public bool IsSendOrdered() { return false; }

		public AbsoluteSimulationFrame Frame;
	

		public void SetSimulationFrame(AbsoluteSimulationFrame frame)
		{
			Frame = frame;
		}

		public AbsoluteSimulationFrame GetSimulationFrame() => Frame;

		public ICoherenceComponentData MergeWith(ICoherenceComponentData data, uint mask)
		{
			var other = (Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147)data;
			if ((mask & 0x01) != 0)
			{
				Frame = other.Frame;
				rotation = other.rotation;
			}
			mask >>= 1;
			if ((mask & 0x01) != 0)
			{
				Frame = other.Frame;
				position = other.position;
			}
			mask >>= 1;
			return this;
		}

		public uint DiffWith(ICoherenceComponentData data)
		{
			throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");

		}

		public static uint Serialize(Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147 data, uint mask, IOutProtocolBitStream bitStream)
		{
			if (bitStream.WriteMask((mask & 0x01) != 0))
			{
				var fieldValue = (data.rotation.ToCoreQuaternion());

				bitStream.WriteQuaternion(fieldValue, 32);
			}
			mask >>= 1;
			if (bitStream.WriteMask((mask & 0x01) != 0))
			{
				var fieldValue = (data.position.ToCoreVector3());

				bitStream.WriteVector3(fieldValue, FloatMeta.NoCompression());
			}
			mask >>= 1;

			return mask;
		}

		public static (Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147, uint) Deserialize(InProtocolBitStream bitStream)
		{
			var mask = (uint)0;
			var val = new Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147();
	
			if (bitStream.ReadMask())
			{
				val.rotation = (bitStream.ReadQuaternion(32)).ToUnityQuaternion();
				mask |= 0b00000000000000000000000000000001;
			}
			if (bitStream.ReadMask())
			{
				val.position = (bitStream.ReadVector3(FloatMeta.NoCompression())).ToUnityVector3();
				mask |= 0b00000000000000000000000000000010;
			}
			return (val, mask);
		}

		/// <summary>
		/// Resets byte array references to the local array instance that is kept in the lastSentData.
		/// If the array content has changed but remains of same length, the new content is copied into the local array instance.
		/// If the array length has changed, the array is cloned and overwrites the local instance.
		/// If the array has not changed, the reference is reset to the local array instance.
		/// Otherwise, changes to other fields on the component might cause the local array instance reference to become permanently lost.
		/// </summary>
		public void ResetByteArrays(ICoherenceComponentData lastSent, uint mask)
		{
			var last = lastSent as Player_9b83865ad432b2840a2ba6c6e5d0fada_UnityEngine__char_46_Transform_3402058366864678147?;
	
		}
	}
}