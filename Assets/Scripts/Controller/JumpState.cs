using UnityEngine;

public class JumpState : Playerstate
{
    public JumpState(FPCharacterController player) : base(player) { }

    public override void Update()
    {
        player.SetMovement(player.MoveSpeed);

        if (player.IsGrounded)
        {
            player.ChangeState(new IdleState(player));
        }
    }
}