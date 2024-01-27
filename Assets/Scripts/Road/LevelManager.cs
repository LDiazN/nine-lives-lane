using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float Speed = 5;


    [SerializeField] List<Points> allTheLevelBlock = new List<Points>();
    [SerializeField] List<Points> currentLevelBlocks = new List<Points>();
    [SerializeField] Transform levelStartPosition;
    [SerializeField] Transform parentRoad;

    protected virtual void Start()
    {
        if (parentRoad.position != Vector3.zero)
        {
            parentRoad.position = Vector3.zero;
        }
        for (int i = 0; i < 5; i++)
        {
            AddLevelBlock();
        }
        StartCoroutine(repeat());
    }
    private void Update()
    {
        parentRoad.position = new Vector3(parentRoad.position.x + 5 * Time.deltaTime, 0, 0);

    }
    IEnumerator repeat()
    {
        while (true)
        {
            AddLevelBlock();
            yield return new WaitForSeconds(5f);
        }
    }
    public void AddLevelBlock()
    {
        int RandomIdx = Random.Range(0, allTheLevelBlock.Count);
        Points block;
        Vector3 spawnPosition = Vector3.zero;
        if (currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlock[0]);
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlock[RandomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
        }
        block.transform.SetParent(parentRoad, false);

        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x, 0, 0);

        block.transform.position = correction;

        currentLevelBlocks.Add(block);
    }


}