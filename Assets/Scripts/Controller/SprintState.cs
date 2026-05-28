using UnityEngine;

public class SprintState : Playerstate
{
    public SprintState(FPCharacterController player) : base(player) { }

    public override void Enter()
    {
        player.SetStanding();
    }

    public override void Update()
    {
        player.SetMovement(player.SprintSpeed);

        if (!player.IsGrounded)
        {
            player.ChangeState(new JumpState(player));
            return;
        }

        if (!player.Input.Sprint || player.Input.Move.y <= 0.1f)
        {
            player.ChangeState(new WalkState(player));
        }
    }
}