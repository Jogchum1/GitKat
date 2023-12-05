using Unity.VisualScripting;
using UnityEngine;

public abstract class ModAbility : ScriptableObject
{
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Rigidbody2D playerRigidbody2D;
    [HideInInspector]
    public PlayerMovement playerMovement;

    public void InstantiateAbility(GameManager gm)
    {
        gameManager = GameManager.instance;
        player = gameManager.player;
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public abstract void ActivateAbility();
    public abstract void DeactivateAbility();

    public virtual void UpdateAbility()
    {

    }
}