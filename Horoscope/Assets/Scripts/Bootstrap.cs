using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bootstrap : MonoBehaviour
{
    public Mesh ParticleMesh;
    public Material ParticleMaterial;

    public Vector3 Root;
    public float InitialMinSpeed = 20f;
    public float InitialMaxSpeed = 80f;
    
    private EntityManager _entityManager;
    private EntityArchetype _archetype;

    public int Count { get; private set; }

    public void Initialize()
    {
        this._entityManager = World.Active.GetOrCreateManager<EntityManager>();

        this._archetype = this._entityManager.CreateArchetype(
            ComponentType.Create<MeshInstanceRenderer>(),
            ComponentType.Create<TransformMatrix>(),
            ComponentType.Create<Position>(),
            ComponentType.Create<Rotation>(),
            ComponentType.Create<ParticleData>());

        this.Count = 0;
    }

    public void CreateParticle(int num)
    {
        for (var i = 0; i < num; ++i)
        {
            var particle = this._entityManager.CreateEntity(this._archetype);

            this._entityManager.SetSharedComponentData(particle, new MeshInstanceRenderer()
            {
                mesh = ParticleMesh,
                material = ParticleMaterial,
            });

            this._entityManager.SetComponentData(particle, new TransformMatrix());

            var dx = Random.Range(-1f, 1f);
            var dy = Random.Range(-1f, 1f);
            var dz = Random.Range(-1f, 1f);
            var direction = new Vector3(dx, dy, dz).normalized;

            this._entityManager.SetComponentData(particle, new Position()
            {
                Value = new float3(this.Root.x, this.Root.y, this.Root.z),
            });

            this._entityManager.SetComponentData(particle, new Rotation()
            {
                Value = new quaternion(direction.x, direction.y, direction.z, 1f),
            });

            var initialSpeed = Random.Range(this.InitialMinSpeed, this.InitialMaxSpeed);
            var ax = Random.Range(-1f, 1f);
            var ay = Random.Range(-1f, 1f);
            var az = Random.Range(-1f, 1f);
            var axis = new Vector3(ax, ay, az).normalized;
            this._entityManager.SetComponentData(particle, new ParticleData()
            {
                Speed = initialSpeed,
                InitialSpeed = initialSpeed,
                Axis = axis,
            });
        }

        this.Count += num;
    }
}
