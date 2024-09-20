using System;

public class Player
{
    public Guid ID { get; private set; }
    public int Score { get; private set; } = 0;
    public bool IsJumping { get; set; } = false;
    public float JumpForce { get; private set; } = 10f;
    public float CurrentGravity { get; private set; } = 1f;
    public float CurrentGravityChangeCooldown { get; set; } = 0f;
    public const float GravityChangeCooldownDuration = 10f;
    public bool IsDashing { get; set; } = false;
    public float CurrentDashingCooldown { get; set; } = 0f;
    public const float DashingCooldownDuration = 6f;
    public bool IsDead { get; private set; } = false;

    public Player()
    {
        ID = new Guid();
    }

    public void Die()
    {
        IsDead = true;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void ChangeGravity(float gravity)
    {
        CurrentGravity = gravity;
        
        if (gravity != 1f)
        {
            CurrentGravityChangeCooldown = GravityChangeCooldownDuration;
        }
    }

    public void Dash()
    {
        IsDashing = true;
        CurrentDashingCooldown = DashingCooldownDuration;
    }
}
