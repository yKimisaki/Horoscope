using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(MeshInstanceRendererSystem))]
public class ParticleMovementComponentSystem : ComponentSystem
{
    private struct Group
    {
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Directions;
        public ComponentDataArray<ParticleData> Particles;
        
        public readonly int Length;
    }

    [Inject]
    private Group _group;

    protected override void OnUpdate()
    {
        for(var i = 0; i < this._group.Length; ++i)
        {
            var position = this._group.Positions[i];

            var direction = this._group.Directions[i];
            var dx = direction.Value.value.x;
            var dy = direction.Value.value.y;
            var dz = direction.Value.value.z;

            var particle = this._group.Particles[i];
            var speed = particle.Speed;
            var initialSpeed = particle.InitialSpeed;
            var axis = particle.Axis;

            this._group.Positions[i] = new Position()
            {
                Value = position.Value + new float3(dx, dy, dz) * speed,
            };
            this._group.Particles[i] = new ParticleData()
            {
                Speed = speed * 0.9f,
            };
        }
    }
}
