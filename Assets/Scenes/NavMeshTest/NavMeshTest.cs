using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    public Vector3 mouseWorldPos;

    public List<Vector3> selectedPositions;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Physics.Raycast(ray, out RaycastHit raycastHit);
        //mouseWorldPos = raycastHit.point;

        RaycastHit2D raycastHit = Physics2D.GetRayIntersection(ray);
        bool hasHitSomething = raycastHit.collider != null;
        if (hasHitSomething) mouseWorldPos = raycastHit.point;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //float[] distances = { 2.5F, 5F, 7.5F };
            float[] distances = { 1F, 2F, 3F };
            int[] amounts = { 4, 8, 12 };
            float[] rotOffsets = { 0, 22.5F, 0 };
            selectedPositions = GetPositionListAround(mouseWorldPos, distances, amounts, rotOffsets);
            OrderUnits();
        }
    }

    private List<Vector3> GetPositionListAround(Vector3 center, float[] distances, int[] amounts, float[] rotOffsets)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < distances.Length; i++)
        {
            //TODO: randomize values?
            result.AddRange(GetPositionListAround(center, distances[i], amounts[i], rotOffsets[i]));
        }
        return result;
    }

    private List<Vector3> GetPositionListAround(Vector3 center, float distance, int amount, float rotOffset)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < amount; i++)
        {
            float angle = i * (360F / amount);
            angle += rotOffset;
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 pos = center + (dir * distance);
            result.Add(pos);
        }
        return result;
    }

    private Vector3 ApplyRotationToVector(Vector3 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
        //return Quaternion.Euler(0, angle, 0) * vector;
    }

    private void OrderUnits()
    {
        NavMeshAgent[] agentArray = FindObjectsOfType<NavMeshAgent>();
        //TODO: this randomization becomes unnecessary if is done beforehand
        for (int i = 0; i < agentArray.Length; i++)
        {
            NavMeshAgent agent = agentArray[i];
            int rng = Random.Range(0, i);
            agentArray[i] = agentArray[rng];
            agentArray[rng] = agent;
        }

        List<NavMeshAgent> agentList = new List<NavMeshAgent>(agentArray);
        for (int i = 0; i < agentList.Count; i++)
        {
            NavMeshAgent agent = agentList[i];
            agent.SetDestination(selectedPositions[i]);
        }
    }
}
