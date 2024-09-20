using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource AudioSource;
    public Player Player { get; private set; } = new Player();
    public Animator Animator;
    public AudioClip JumpSound;
    public AudioClip GroundHitSound;
    public TextMeshProUGUI ScoreLabel;
    public Image DashCover;
    public Image GravityPotionEffectImage;
    public GameObject GameOverPanel;
    public TextMeshProUGUI GameOverScoreLabel;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        GameOverPanel.SetActive(false);
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        ScoreLabel.text = $"Score: {Player.Score}";

        if (Player.CurrentGravityChangeCooldown > 0f)
        {
            Player.CurrentGravityChangeCooldown -= Time.unscaledDeltaTime;
            GravityPotionEffectImage.fillAmount = (Player.CurrentGravityChangeCooldown / Player.GravityChangeCooldownDuration);
        }
        else
        {
            Player.CurrentGravityChangeCooldown = 0f;
            GravityPotionEffectImage.fillAmount = 0;
            EndGravityPotion();
        }

        if (Player.CurrentDashingCooldown > 0f)
        {
            Player.CurrentDashingCooldown -= Time.unscaledDeltaTime;
            DashCover.fillAmount = (Player.CurrentDashingCooldown / Player.DashingCooldownDuration);
        }
        else
        {
            Player.CurrentDashingCooldown = 0f;
            DashCover.fillAmount = 0;
        }
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Dash();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "DeathZone":
                Die();
                break;
            case "GravityPotion":
                TakeGravityPotion();
                Destroy(collision.gameObject);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Platform":
                EndJump();
                EndDash();
                break;
            case "Enemy":
                Die();
                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Platform":
                Player.IsJumping = true;
                Animator.SetBool("IsJumping", true);
                break;
        }
    }

    private void Jump()
    {
        if (!Player.IsJumping && !Player.IsDead)
        {
            var rigidbody = this.GetComponent<Rigidbody2D>();

            rigidbody.velocity = new Vector2(0, Player.JumpForce);
            Player.IsJumping = true;
            Animator.SetBool("IsJumping", true);

            AudioSource.clip = JumpSound;
            AudioSource.Play();
        }
    }

    private void EndJump()
    {
        if (Player.IsJumping)
        {
            Player.IsJumping = false;
            Animator.SetBool("IsJumping", false);

            AudioSource.clip = GroundHitSound;
            AudioSource.Play();
        }
    }

    private void Dash()
    {
        if (Player.IsJumping && !Player.IsDashing && Player.CurrentDashingCooldown <= 0f && !Player.IsDead)
        {
            Player.Dash();
            Animator.SetBool("IsDashing", true);
            DashCover.fillAmount = 1;

            var rigidbody = this.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0, 4f);
        }
    }

    private void EndDash()
    {
        if (Player.IsDashing)
        {
            Player.IsDashing = false;
            Animator.SetBool("IsDashing", false);
        }
    }

    private void TakeGravityPotion()
    {
        var rigidbody = this.GetComponent<Rigidbody2D>();

        Player.ChangeGravity(0.8f);
        GravityPotionEffectImage.fillAmount = 1;
        rigidbody.gravityScale = Player.CurrentGravity;
    }

    private void EndGravityPotion()
    {
        var rigidbody = this.GetComponent<Rigidbody2D>();

        Player.ChangeGravity(1f);
        rigidbody.gravityScale = Player.CurrentGravity;
    }

    public void Die()
    {
        Player.Die();
        // Lose the player's collider
        this.gameObject.GetComponent<Collider2D>().isTrigger = true;

        GameOverPanel.SetActive(true);
        GameOverScoreLabel.text = $"YOUR SCORE: {Player.Score}";
        Cursor.visible = true;

        GameSettings.UpdateSceneryHighestScore(Player.Score);
        SaveSys.SaveData();
    }
}
