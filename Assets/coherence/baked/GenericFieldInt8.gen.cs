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

	public struct GenericFieldInt8 : ICoherenceComponentData
	{
		public int number;

		public override string ToString()
		{
			return $"GenericFieldInt8(number: {number})";
		}

		public uint GetComponentType() => Definition.InternalGenericFieldInt8;

		public const int order = 0;

		public uint FieldsMask => 0b00000000000000000000000000000001;

		public int GetComponentOrder() => order;
		public bool IsSendOrdered() { return false; }

		public AbsoluteSimulationFrame Frame;
	
		private static readonly int _number_Min = -2147483648;
		private static readonly int _number_Max = 2147483647;

		public void SetSimulationFrame(AbsoluteSimulationFrame frame)
		{
			Frame = frame;
		}

		public AbsoluteSimulationFrame GetSimulationFrame() => Frame;

		public ICoherenceComponentData MergeWith(ICoherenceComponentData data, uint mask)
		{
			var other = (GenericFieldInt8)data;
			if ((mask & 0x01) != 0)
			{
				Frame = other.Frame;
				number = other.number;
			}
			mask >>= 1;
			return this;
		}

		public uint DiffWith(ICoherenceComponentData data)
		{
			throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");

		}

		public static uint Serialize(GenericFieldInt8 data, uint mask, IOutProtocolBitStream bitStream)
		{
			if (bitStream.WriteMask((mask & 0x01) != 0))
			{
				Coherence.Utils.Bounds.Check(data.number, _number_Min, _number_Max, "GenericFieldInt8.number");
				data.number = Coherence.Utils.Bounds.Clamp(data.number, _number_Min, _number_Max);
				var fieldValue = data.number;

				bitStream.WriteIntegerRange(fieldValue, 32, -2147483648);
			}
			mask >>= 1;

			return mask;
		}

		public static (GenericFieldInt8, uint) Deserialize(InProtocolBitStream bitStream)
		{
			var mask = (uint)0;
			var val = new GenericFieldInt8();
	
			if (bitStream.ReadMask())
			{
				val.number = bitStream.ReadIntegerRange(32, -2147483648);
				mask |= 0b00000000000000000000000000000001;
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
			var last = lastSent as GenericFieldInt8?;
	
		}
	}
}