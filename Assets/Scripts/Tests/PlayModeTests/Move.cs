using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

public class Move
{
    private GameObject _testGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MoveWithEnumeratorPasses()
    {
        CharacterController controller;
        
        _testGameObject = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Character/Alpha.prefab"));
        controller = _testGameObject.GetComponent<CharacterController>();

        Vector3 prePos = _testGameObject.transform.position;
        controller.Move(Vector3.forward * Time.deltaTime * 4f);
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return Time.deltaTime * 4f;
        Vector3 postPos = _testGameObject.transform.position;
        Assert.AreNotEqual(postPos, prePos);
    }
}
