using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct ParticleData : IComponentData
{
    public float Speed;
    public float InitialSpeed;
    public float3 Axis;
}
