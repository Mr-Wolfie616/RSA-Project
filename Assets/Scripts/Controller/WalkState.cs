using UnityEngine;

public class WalkState : Playerstate
{
    public WalkState(FPCharacterController player) : base(player) {}

    public override void Enter()
    {
        player.SetStanding();
    }

    public override void Update()
    {
        player.SetMovement(player.MoveSpeed);

        if (!player.IsGrounded)
        {
            player.ChangeState(new JumpState(player));
            return;
        }

          if (player.Input.Move.sqrMagnitude < 0.01f)
        {
            player.ChangeState(new IdleState(player));
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

        if (player.Input.Move.y > 0.1f && player.Input.Sprint)
        {
            player.ChangeState(new SprintState(player));
        }
    }
}