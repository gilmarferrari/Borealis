using UnityEngine;

public class Platform
{
    public Vector3 Position { get; private set; }
    public PlatformSize Size { get; private set; }
    public float Speed { get; private set; }

    public Platform(Vector3 initialPosition, float speed, PlatformSize? previousPlatformSize)
    {
        var random = Random.Range(0, 2);

        Position = initialPosition;
        Speed = speed;

        if (previousPlatformSize != null)
        {
            Size = previousPlatformSize == PlatformSize.Large ? PlatformSize.Medium : (PlatformSize)random;
        }
        else
        {
            Size = PlatformSize.Large;
        }
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        Position = newPosition;
    }
}

public enum PlatformSize
{
    Medium,
    Large,
}
