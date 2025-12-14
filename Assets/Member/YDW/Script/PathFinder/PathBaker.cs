using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Member.YDW.Script.PathFinder
{
    public class PathBaker : MonoBehaviour
    {
        
        [SerializeField] private PathBakeEventSO bakeEventSO;
        [SerializeField] private Tilemap groundMap;
        [SerializeField] private Tilemap obstacleMap;
        [SerializeField] private BakedDataSO bakedData;

        [SerializeField] private bool isDrawGizmo = true;
        [SerializeField] private bool isConnerCheck = true;
        [SerializeField] private Color nodeColor, edgeColor;
        
        private Vector2Int buildingPos; //빌딩 위치 리스트

        private void Awake()
        {
            bakeEventSO.OnEvent += BakeMapDataRunning;
        }

        private void BakeMapDataRunning(RunTimeBakeEvent obj)
        {
            switch (obj.runTimeBakeEventType)
            {
                case RunTimeBakeEventType.Delete:
                    ReGeneratePos(obj.buildingPosition,obj.buildingSize);
                    break;
                case RunTimeBakeEventType.Set:
                    DeletePos(obj.buildingPosition,obj.buildingSize);
                    break;
            }
        }

        private void DeletePos(Vector2Int buildingPosition, Vector2Int buildingSize)
        {
            foreach (Vector3Int points in GetBounds(buildingPosition, buildingSize))
            {
                if (bakedData.TryGetNode(points, out NodeData nodeData))
                {
                    bakedData.RunTimeRemovePoint(nodeData);
                    DeleteNeighbors(nodeData);
                }
            }
        }

        private void DeleteNeighbors(NodeData nodeData)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    
                    Vector3Int neighborPos = nodeData.cellPosition + new Vector3Int(x, y, 0);
                    if (bakedData.TryGetNode(neighborPos, out NodeData adjacentNode))
                    { 
                        adjacentNode.neighbors.RemoveAll(link => link.endCellPositon == nodeData.cellPosition );
                    }
                }
            }

        }

        private void GenerateNeighbors(NodeData nodeData)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if(x == 0 && y == 0) continue;
                    Vector3Int nextPoint = new Vector3Int(x, y) + nodeData.cellPosition;
                    if (bakedData.TryGetNode(nextPoint, out NodeData abjacentNode)) 
                    {
                        Debug.Log($"Generating neighbors ; {nodeData.worldPosition}, {nodeData.cellPosition}");
                        if (CheckCorner(nextPoint, nodeData.cellPosition))
                        {
                            nodeData.AddNeighbors(abjacentNode); 
                            abjacentNode.AddNeighbors(nodeData);
                        }
                    }
                    else
                        Debug.Log($"Failed to generate neighbors : {nextPoint} ");
                }
            }
        }
            

        private void ReGeneratePos(Vector2Int buildingPosition, Vector2Int buildingSize)
        {
            foreach (Vector3Int pos in GetBounds(buildingPosition, buildingSize))
            {
                Vector3 worldPosition = groundMap.GetCellCenterWorld(pos);
                bakedData.RunTimeAddPoint(worldPosition,pos);
                bakedData.TryGetNode(pos, out NodeData nodeData);
                GenerateNeighbors(nodeData);
            }
            
        }

        private List<Vector2Int> GetBounds(Vector2Int buildingPosition, Vector2Int buildingSize)
        {
            List<Vector2Int> bounds = new();
            for (int i = 0; i < buildingSize.x; i++) {
                for (int j = 0; j < buildingSize.y; j++) {
                    Vector2Int cellPos = buildingPosition + new Vector2Int(i, j);
                    bounds.Add(cellPos);
                }
            }
            return bounds;
        }

        private bool CheckNeighbors(NodeData targetNode, Vector3Int generateCell)
        {
            //True면 해당 셀 데이터가 존재한다. 
            //False면 존재하지 않는다.
            for (int i = 0; i < targetNode.neighbors.Count; i++)
            {
                if (targetNode.neighbors[i].endCellPositon == generateCell) //이웃에 해당 데이터가 존재함을 의미하므로.
                {
                    return true;
                }

            }
            return false;
        }
        

        //런타임 베이크 필요.
        [ContextMenu("Bake map Data")]
        private void BakeMapData()
        {
            Debug.Assert(groundMap != null && obstacleMap != null, "Target tilemap are null or empty");
            WritePointData();
            RecordNeighbors();
            SaveIfUnityEditor();
        }
        private void SaveIfUnityEditor()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(bakedData);
            AssetDatabase.SaveAssets();
#endif

        }

        private void RecordNeighbors()
        {
            foreach (NodeData nodeData in bakedData.points)
            {
                nodeData.neighbors.Clear();
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if(x == 0 && y == 0) continue;
                        Vector3Int nextPoint = new Vector3Int(x, y) + nodeData.cellPosition;
                        if (bakedData.TryGetNode(nextPoint, out NodeData abjacentNode)) 
                        {
                            if (CheckCorner(nextPoint, nodeData.cellPosition))
                            {
                                nodeData.AddNeighbors(abjacentNode);
                            }
                        }
                    }
                }
            }
        }

        private bool CheckCorner(Vector3Int nextPoint, Vector3Int currentPoint)
        {
            if(isConnerCheck == false) return true;

            return CanMovePosition(new Vector3Int(nextPoint.x, currentPoint.y))
                   && CanMovePosition(new Vector3Int(currentPoint.x, nextPoint.y));
        }

        private void WritePointData()
        {
            bakedData.ClearPoints();
            groundMap.CompressBounds();

            BoundsInt mapBound = groundMap.cellBounds;

            for (int x = mapBound.xMin; x < mapBound.xMax; x++)
            {
                for (int y = mapBound.yMin; y < mapBound.yMax; y++)
                {
                    Vector3Int targetCell = new Vector3Int(x, y);
                    if (CanMovePosition(targetCell))
                    {
                        AddPoint(targetCell);
                    }
                }
            }
        }


        private bool CanMovePosition(Vector3Int targetCell )
        {
            bool hasObstacle = obstacleMap.HasTile(targetCell);
            bool hasGround = groundMap.HasTile(targetCell);
            return hasGround && hasObstacle == false;
        }
        private void AddPoint(Vector3Int targetCell)
        {
            Vector3 worldPosition = groundMap.GetCellCenterWorld(targetCell);
            bakedData.AddPoint(worldPosition, targetCell);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(isDrawGizmo == false || bakedData == null) return;
            

            foreach (NodeData nodeData in bakedData.points)
            {
                Gizmos.color = nodeColor;
                Gizmos.DrawWireSphere(nodeData.worldPosition, 0.15f);

                foreach (LinkData link in nodeData.neighbors)
                {
                    Gizmos.color = edgeColor;
                    DrawArrowGizmo(link.startPosition, link.endPosition);
                }
            }
        }

        private void DrawArrowGizmo(Vector3 start, Vector3 end)
        {
            Vector3 direction = (end - start).normalized;
            Vector3 arrowStart = end - direction * 0.25f;
            Vector3 arrowEnd = end - direction.normalized * 0.15f;
            const float arrowSize = 0.05f;

            Vector3 pointA = arrowStart + (Quaternion.Euler(0,0,-90f) * direction) * arrowSize;
            Vector3 pointB = arrowStart + (Quaternion.Euler(0, 0, 90f) * direction) * arrowSize;

            Gizmos.DrawLine(start, arrowStart);
            Gizmos.DrawLine(pointA,arrowEnd);
            Gizmos.DrawLine(pointB,arrowEnd);
            Gizmos.DrawLine(pointA,pointB);

        }



#endif

    }
}
