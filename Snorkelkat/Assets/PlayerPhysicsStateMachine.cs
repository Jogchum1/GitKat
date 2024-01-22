using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsStateMachine : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private PhysicsMaterial2D normalMaterial;
    [SerializeField] private PhysicsMaterial2D slipperyMaterial;
    [SerializeField] private PhysicsMaterial2D noFrictionMaterial;

    public enum State
    {
        Normal,
        Slippery,
        NoFriction,
    }

    public State state;

    IEnumerator NormalState()
    {
        Debug.Log("Normal: Enter");
        gameManager.playerRigidbody2D.sharedMaterial = normalMaterial;
        while (state == State.Normal)
        {
            yield return 0;
        }
        Debug.Log("Normal: Exit");
        NextState();
    }

    IEnumerator SlipperyState()
    {
        Debug.Log("Slippery: Enter");
        gameManager.playerRigidbody2D.sharedMaterial = slipperyMaterial;
        while (state == State.Slippery)
        {
            yield return 0;
        }
        Debug.Log("Slippery: Exit");
        NextState();
    }

    IEnumerator NoFrictionState()
    {
        Debug.Log("NoFriction: Enter");
        gameManager.playerRigidbody2D.sharedMaterial = noFrictionMaterial;
        while (state == State.NoFriction)
        {
            yield return 0;
        }
        Debug.Log("NoFriction: Exit");
        NextState();
    }

    void Start()
    {
        gameManager = GameManager.instance;
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
