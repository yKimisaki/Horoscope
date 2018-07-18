using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(MeshInstanceRendererSystem))]
public class ParticleMovementComponentSystem : JobComponentSystem
{
    private struct Group
    {
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Directions;
        public ComponentDataArray<ParticleData> Particles;
        
        public readonly int Length;
    }

    private struct ParticleMovementJob : IJobParallelFor
    {
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Directions;
        public ComponentDataArray<ParticleData> Particles;

        public void Execute(int i)
        {
            var position = this.Positions[i];

            var direction = this.Directions[i];
            var dx = direction.Value.value.x;
            var dy = direction.Value.value.y;
            var dz = direction.Value.value.z;

            var particle = this.Particles[i];
            var speed = particle.Speed;
            var initialSpeed = particle.InitialSpeed;
            var axis = particle.Axis;

            this.Positions[i] = new Position()
            {
                Value = position.Value + new float3(dx, dy, dz) * speed,
            };
            this.Particles[i] = new ParticleData()
            {
                Speed = speed * 0.9f,
            };
        }
    }

    [Inject]
    private Group _group;

    protected override JobHandle OnUpdate(JobHandle jobHandle)
    {
        var job = new ParticleMovementJob()
        {
            Positions = this._group.Positions,
            Directions = this._group.Directions,
            Particles = this._group.Particles,
        };

        var handle = job.Schedule(this._group.Length, 32, jobHandle);

        return handle;
    }
}
