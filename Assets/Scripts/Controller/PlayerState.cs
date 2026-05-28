using UnityEngine;

public abstract class Playerstate
{
    protected FPCharacterController player;

    protected Playerstate(FPCharacterController player)
    {
        this.player = player;
    }

    public virtual void Enter() {}
    public virtual void Update() {}
    public virtual void Exit() {}
}