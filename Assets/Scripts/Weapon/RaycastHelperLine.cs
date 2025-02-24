using System.Collections.Generic;
using Player;

namespace Weapon
{
    using UnityEngine;
    using System.Collections.Generic;

    public class RaycastHelperLine : MonoBehaviour
    {
        public Transform startPoint; // 指定射線的起始點
        public LayerMask obstacleLayer; // 指定障礙物的 Layer
        public float maxDistance = 10f; // 射線最大距離
        public Color lineColor = Color.red; // 虛線顏色
        public float dashLength = 0.2f; // 虛線的長度
        public float gapLength = 0.1f; // 虛線的間隔
        public bool isRightDirection = true; // true：往右發射, false：往左發射

        [SerializeField]
        private LineRenderer lineRenderer;

        void Start()
        {
            if (startPoint == null)
            {
                Debug.LogError("請指定 StartPoint（射線起點）");
                return;
            }

            // 初始化 LineRenderer
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            lineRenderer.useWorldSpace = true;
        }

        void Update()
        {
            if (startPoint != null)
            {
                DrawDashedLine();
            }
        }

        private void DrawDashedLine()
        {
            // 設定方向
            isRightDirection = !PlayerController.Instance.LeftDirection;
            Vector2 direction = isRightDirection ? Vector2.right : Vector2.left;

            // 執行 Raycast，檢測障礙物
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, direction, maxDistance, obstacleLayer);

            // 計算終點（確保 Y 軸不變）
            Vector2 endPoint;
            if (hit.collider != null)
            {
                endPoint = new Vector2(hit.point.x, startPoint.position.y);
            }
            else
            {
                endPoint = new Vector2(startPoint.position.x + (isRightDirection ? maxDistance : -maxDistance),
                    startPoint.position.y);
            }

            // 繪製虛線
            DrawDottedLine(startPoint.position, endPoint);
        }

        private void DrawDottedLine(Vector2 start, Vector2 end)
        {
            float distance = Vector2.Distance(start, end);
            Vector2 direction = (end - start).normalized;
            float currentDistance = 0f;

            List<Vector3> points = new List<Vector3>();

            while (currentDistance < distance)
            {
                Vector2 segmentStart = start + direction * currentDistance;
                Vector2 segmentEnd = start + direction * Mathf.Min(currentDistance + dashLength, distance);

                points.Add(segmentStart);
                points.Add(segmentEnd);

                currentDistance += dashLength + gapLength; // 前進到下一個虛線段
            }

            // 更新 LineRenderer 點
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }
}