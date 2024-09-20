using System;

public class LaserGun
{
    public Guid ID { get; set; }
    public const float Damage = 10f;
    public const float FireRate = 0.42f;
    public float CurrentOverheat { get; set; } = 0f;
    public const float MaxOverheat = 12f;
    public const float MinOverheatToFire = 10f;
}
