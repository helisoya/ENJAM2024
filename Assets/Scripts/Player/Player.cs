using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a player
/// </summary>
public class Player : MonoBehaviour
{
    public int ID { get; private set; }
    public int GUIID { get; private set; }

    [Header("Infos")]
    [SerializeField] private float stunLength = 3;
    [SerializeField] private int amountLostOnStun = 2;
    private int score;
    private bool stuned;
    private float stunStart;

    [Header("Components")]
    [SerializeField] private PlayerMovements movements;
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private PlayerInterraction interraction;
    [SerializeField] private Animator animator;

    [Header("Collisions")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float collisionCooldown = 0.5f;
    private Gamepad pad;
    private float collisionStart;
    private bool collided;


    void Start()
    {
        collided = false;
        stuned = false;
        pad = playerInput.GetDevice<Gamepad>();
        if (pad != null) pad.SetMotorSpeeds(0f, 0f);

        ID = GameManager.instance.RegisterPlayer(this);
        GUIID = GameGUI.instance.AddNewPlayerGUI(ID);
    }

    /// <summary>
    /// Gets the player's score
    /// </summary>
    /// <returns>The player's score</returns>
    public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// Adds score
    /// </summary>
    /// <param name="scoreAmount">The score to add</param>
    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        GameGUI.instance.SetPlayerCandyCount(ID, score);
    }

    /// <summary>
    /// Stuns the player and returns the amount of money stolen if needed
    /// </summary>
    /// <param name="stealMoney">Should money be stolen ?</param>
    /// <returns>The amount of money stolen</returns>
    public int Stun(bool stealMoney = false)
    {
        stunStart = Time.time;
        stuned = true;
        movements.SetVelocity(Vector2.zero);
        SetAnimationTrigger("Damage");
        OnCollisionEnter2D(null);

        if (stealMoney)
        {
            int amountStolen = Mathf.Min(amountLostOnStun, score);
            AddScore(-amountStolen);
            return amountStolen;
        }

        return 0;
    }

    /// <summary>
    /// Sets the animator's trigger
    /// </summary>
    /// <param name="triggerName">The trigger's name</param>
    public void SetAnimationTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    void Update()
    {
        if (collided && Time.time - collisionStart >= collisionCooldown)
        {
            collided = false;
            if (pad != null) pad.SetMotorSpeeds(0f, 0f);
        }

        if (stuned && Time.time - stunStart >= stunLength)
        {
            stuned = false;
        }
    }

    void OnMove(InputValue input)
    {
        if (!stuned && GameManager.instance.InGame)
        {
            movements.SetVelocity(input.Get<Vector2>());
        }
    }

    void OnAttack(InputValue input)
    {
        if (!stuned && GameManager.instance.InGame)
        {
            attack.TryAttack();
        }
    }

    void OnInterract(InputValue input)
    {
        if (!stuned && GameManager.instance.InGame)
        {
            interraction.TryInterract();
        }
    }

    void OnInterract1(InputValue input)
    {
        if (input.isPressed && GameManager.instance.InGame)
        {
            GameGUI.instance.TogglePauseMenu();
        }
    }

    void OnReadyUp(InputValue input)
    {
        if (input.isPressed && !GameManager.instance.InGame)
        {
            GameManager.instance.ReadyUp(ID);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.instance.InGame) return;

        collisionStart = Time.time;
        collided = true;
        if (pad != null) pad.SetMotorSpeeds(0.2f, 0.2f);
    }

}
