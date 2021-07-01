using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelPart[] _parts;
    public LevelObject[] levels;
    public const int platformDefaultLength = 6;

    private void Start()
    {

    }

    /// <summary>
    /// Method to get a part from '_parts' by 'LevelType'
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public LevelPart GetPart(LevelType type)
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i]._levelPart == type)
            {
                return _parts[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Method to instantiate a certain part in a parent and to a location
    /// </summary>
    /// <param name="side"></param>
    /// <param name="pos"></param>
    /// <param name="prefab"></param>
    public void GenerateLevelPart(Transform side, Vector3 pos, GameObject prefab)
    {
        GameObject part = Instantiate(prefab);
        part.transform.SetParent(side);
        part.transform.localPosition = pos;
    }
    public void GenerateLevel(LevelObject lvl)
    {
        //Get an empty platform part
        LevelPart emptyPart = GetPart(LevelType.Empty);

        //Get an empty platform prefab
        GameObject emptyPlatform = emptyPart._levelPrefab;

        //Set current z to negative, so we add a platform behind the character, behind 0
        float currentZ = -emptyPart._levelPartLength;

        //Set height change to 0
        float heightChange = 0;

        //Add two empty platforms, 1 behind the player, one in front of it
        for (int i = 0; i < 2; i++)
        {
            //Instantiate empty platform
            GenerateLevelPart(lvl._levelParent.transform, new Vector3(0, 0, currentZ), emptyPlatform);
            //Increase 'currentZ' by part's length
            currentZ += emptyPart._levelPartLength;
        }

        //Loop through each part from 'lvl'
        for (int i = 0; i < lvl._parts.Length; i++)
        {
            //Get part object
            LevelPart part = GetPart(lvl._parts[i]);
            //Get prefab from current part
            GameObject prefab = part._levelPrefab;
            //Instantiate current part prefab
            GenerateLevelPart(lvl._levelParent.transform, new Vector3(0, heightChange, currentZ), prefab);
            //Add height change, some objects will have value 0, so it adds nothing
            heightChange += part._levelPartHeightChange;
            //Increase 'currentZ'
            currentZ += part._levelPartLength;
            //Generate an empty platform between them, so you don't have an obstacle immediatly after another obstacle
            GenerateLevelPart(lvl._levelParent.transform, new Vector3(0, heightChange, currentZ), emptyPlatform);
            //Increase this value again
            currentZ += platformDefaultLength;
        }

        //Get the finish platform
        GameObject finishPlatform = GetPart(LevelType.Finish)._levelPrefab;
        //Instantiate the finish platform
        GenerateLevelPart(lvl._levelParent.transform.parent, new Vector3(0, heightChange, currentZ), finishPlatform);

        GameManager.Instance.EndPos = new Vector3(0, heightChange, currentZ);
        GameManager.Instance.InitialDistanceToEnd = Vector3.Distance(FindObjectOfType<PlayerManager>().transform.position, GameManager.Instance.EndPos);
    }

    /// <summary>
    /// Generate level by int index
    /// </summary>
    /// <param name="level"></param>
    public void GenerateLevel(int level)
    {
        if(level > levels.Length)
        {
            level = Random.Range(0, levels.Length);
        }
        GenerateLevel(levels[level]);
    }
}
