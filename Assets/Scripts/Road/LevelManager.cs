using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float Speed = 5;


    [SerializeField] List<Points> allTheLevelBlock = new List<Points>();
    [SerializeField] List<Points> currentLevelBlocks = new List<Points>();
    [SerializeField] Transform parentRoad;
    [SerializeField] float TimeToSpawn = 2;
    [SerializeField] int InitialRoads = 5;
   

    protected virtual void Start()
    {
        if (parentRoad.position != Vector3.zero)
        {
            parentRoad.position = Vector3.zero;
        }
        for (int i = 0; i < InitialRoads; i++)
        {
            AddLevelBlock();
        }
        StartCoroutine(repeat());
    }
    private void Update()
    {
        parentRoad.position += new Vector3( 0, 0, -Speed * Time.deltaTime);

    }
    IEnumerator repeat()
    {
        while (true)
        {
            AddLevelBlock();
            //yield return new WaitForSeconds(TimeToSpawn);
            float respawnTime = RespawnTime();

            if (Mathf.Approximately(0, respawnTime))
                yield return new WaitForSeconds(respawnTime);

            yield return new WaitForSeconds(RespawnTime());


            Points p = currentLevelBlocks[0];
            currentLevelBlocks.RemoveAt(0);
            Destroy(p.gameObject);
        }
    }

    void AddLevelBlock()
    {
        int RandomIdx = Random.Range(0, allTheLevelBlock.Count);
        Points block;
        Vector3 spawnPosition = Vector3.zero;
        if (currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlock[0]);
            spawnPosition = parentRoad.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlock[RandomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
        }
        block.transform.SetParent(parentRoad, false);

        float blockLength = Vector3.Distance(block.startPoint.localPosition, block.endPoint.localPosition);

        block.transform.position = spawnPosition + new Vector3(0, 0, blockLength / 2);
        block.transform.position = new Vector3(block.transform.position.x, 0, block.transform.position.z);

        currentLevelBlocks.Add(block);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(20, 0.5f, 20));
    }

    float ChunksPerSecond()
    {
        float sizeOfChunk = SizeOfChunk();
        if (Mathf.Approximately(0, sizeOfChunk))
            return 0;

        return Speed / sizeOfChunk;
    }

    float SizeOfChunk()
    {
        if (currentLevelBlocks.Count == 0)
        {
            return 0;
        }

        Points firstChunk = currentLevelBlocks[0];

        return Vector3.Distance(firstChunk.startPoint.position, firstChunk.endPoint.position);
    }

    float RespawnTime()
    {
        return 1 / ChunksPerSecond();
    }

}