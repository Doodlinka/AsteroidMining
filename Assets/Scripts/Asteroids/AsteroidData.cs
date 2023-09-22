using UnityEngine;

/* TODO: make it readonly to anyone but its asteroid?
    because it can be changed, but won't affect the asteroid
    all of these have to be set through the rock
*/
public struct AsteroidData
{
    
    public Vector3 Position;
    public Vector2 Velocity;
    [Range(1,3)] public int Size;
    public bool HasOre;


    public AsteroidData(int size, Vector3 position, Vector2 velocity, bool ore) {
        Size = size;
        Velocity = velocity;
        Position = position;
        HasOre = ore;
    }

}
