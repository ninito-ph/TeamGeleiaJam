using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawnpool Data", menuName = "Other/Spawnpool", order = 0)]
public class SpawnpoolData : ScriptableObject
{
    #region Field Declarations
    [Header("Spawnpool Parameters")]
    [Tooltip("The objects in the spawnpool. For every object, there must be a weight at a corresponding index in the weight pool.")]
    [SerializeField] private GameObject[] objectPool;
    [Tooltip("The weights in the spawnpool. For every weight, there must be an object at a corresponding index in the object pool.")]
    [SerializeField] private int[] weightPool;

    public (GameObject item, int weight)[] ObjectWeightTuples
    {
        get
        {
            // If the safety check passes
            if (SafetyCheck() == true)
            {
                (GameObject item, int weight)[] objectWeightTuples = new (GameObject item, int weight)[weightPool.Length];

                for (int index = 0; index < weightPool.Length - 1; index++)
                {
                    objectWeightTuples[index] = (objectPool[index], weightPool[index]);
                }

                return objectWeightTuples;
            }
            else
            {
                Debug.LogError("There are not as many weights as there are objects! There must be a one-to-one correspondence!");
                return null;
            }
        }
    }
    #endregion

    #region Custom Methods
    // A check to see if objects and weights are balanced
    private bool SafetyCheck()
    {
        // A simple check to see if there are as many objects as there are weights.
        if (objectPool.Length == weightPool.Length)
        {
            return true;
        }
        else
        {
            Debug.LogError("Number of objects (" + objectPool.Length + ") is not the same as the number of weights (" + weightPool.Length + ")!");
            return false;
        }
    }
    #endregion
}