using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorConfig", menuName = "LevelChanger/Door Config")]
public class DoorConfig : ScriptableObject
{
    public int levelToLoad;
    public Vector3 spawnPosition;
    // Другие данные, которые вам могут понадобится
}