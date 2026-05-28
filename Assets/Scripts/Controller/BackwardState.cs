using UnityEngine;

public class BackwardState : Playerstate
{
    public BackwardState(FPCharacterController player) : base(player) { }

    public override void Enter()
    {
        player.SetStanding();
    }

    public override void Update()
    {
        player.SetMovement(player.BackwardSpeed);

        if (!player.IsGrounded)
        {
            player.ChangeState(new JumpState(player));
            return;
        }

        if (player.Input.Move.y >= 0)
        {
            player.ChangeState(new WalkState(player));
        }
    }
}