using Characters.Alpha1;
using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsMovingCondition", menuName = "State Machines/Conditions/Alpha/Is Moving Condition")]
public class IsAlphaMovingConditionSO : StateConditionSO<IsAlphaMovingCondition>
{
    public float threshold = .01f;
}

public class IsAlphaMovingCondition : Condition
{
    private Protagonist _protagonistScript;
    private IsAlphaMovingConditionSO _originSO => (IsAlphaMovingConditionSO)OriginSO; // The SO this Condition spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Protagonist>();
    }

    protected override bool Statement()
    {
        Vector3 movementVector = _protagonistScript.movementInput;
        movementVector.y = 0f;
        return movementVector.sqrMagnitude > _originSO.threshold;
    }
}
