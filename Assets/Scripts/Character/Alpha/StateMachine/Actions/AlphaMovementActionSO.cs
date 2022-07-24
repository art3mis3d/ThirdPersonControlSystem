using Characters.Alpha1;
using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AlphaMovement", menuName = "State Machines/Actions/Alpha/Alpha Movement")]
public class AlphaMovementActionSO : StateActionSO<AlphaMovementAction>
{
}

public class AlphaMovementAction : StateAction
{
    //Component references
    private Protagonist _protagonistScript;
    private CharacterController _characterController;

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Protagonist>();
        _characterController = stateMachine.GetComponent<CharacterController>();
    }

    public override void OnUpdate()
    {
        _characterController.Move(_protagonistScript.movementVector * Time.deltaTime);
        _protagonistScript.movementVector = _characterController.velocity;
    }
}
