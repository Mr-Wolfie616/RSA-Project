using UnityEngine;

public class IdleState : Playerstate
{
    public IdleState(FPCharacterController player) : base(player) {}

    public override void Enter()
    {
        player.SetStanding();
    }

    public override void Update()
    {
        player.SetMovement(0f);

        if (!player.IsGrounded)
        {
            player.ChangeState(new JumpState(player));
            return;
        }

        if (player.Input.Crouch)
        {
            player.ChangeState(new CrouchState(player));
            return;
        }

        if (player.Input.Move.y < 0)
        {
            player.ChangeState(new BackwardState(player));
            return;
        }

        if (player.Input.Move.y > 0.1 && player.Input.Sprint)
        {
            player.ChangeState(new SprintState(player));
            return;
        }

        if (player.Input.Move.sqrMagnitude > 0.1f)
        {
            player.ChangeState(new WalkState(player));
        }
    }
}