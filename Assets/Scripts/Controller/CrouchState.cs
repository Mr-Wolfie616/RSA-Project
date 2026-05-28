using UnityEngine;

public class CrouchState : Playerstate
{
    public CrouchState(FPCharacterController player) : base(player) { }

    public override void Enter()
    {
        player.SetCrouching();
    }

    public override void Update()
    {
        player.SetMovement(player.CrouchSpeed);

        if (!player.IsGrounded)
        {
            player.ChangeState(new JumpState(player));
            return;
        }

        if (!player.Input.Crouch)
        {
            player.ChangeState(new IdleState(player));
        }
    }

    public override void Exit()
    {
        player.SetStanding();
    }
}