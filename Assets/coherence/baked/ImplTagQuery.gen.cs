// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Toolkit
{
	using UnityEngine;
	using global::Coherence.Generated;
	using Coherence.Entity;

	public class CoherenceTagQueryImpl : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			Impl.UpdateTagQuery = UpdateTagQuery;
			Impl.RemoveTagQuery = RemoveTagQuery;
		}

		private static void UpdateTagQuery(IClient client, SerializeEntityID liveQuery, string tag)
		{
			var components = new ICoherenceComponentData[]
			{
				new TagQuery { tag = tag }
			};

			var masks = new uint[]
			{
				0b01,
			};

			client.UpdateComponents(liveQuery, components, masks);
		}

		private static void RemoveTagQuery(IClient client, SerializeEntityID liveQuery)
		{
			client.RemoveComponents(liveQuery, new []{Definition.InternalTagQuery});
		}
	}
}
