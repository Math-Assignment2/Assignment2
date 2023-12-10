using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class collision : MonoBehaviour
{
    public GameObject circle;
    public GameObject capsule;
    public GameObject square;

    bool CheckCollisionCircleCapsule(Vector3 circlePosition, float circleRadius, Vector3 capsulePosition,
        float capsuleRadius, Vector3 direction, float halfHeight, out Vector3 mtv)
    {
        Vector3 top, bot;
        CapsulePoints(capsulePosition, direction, halfHeight, out top, out bot);

        Vector3 projectedPoint = ProjectPointLine(circlePosition, top, bot);

        return CheckCollisionCircles(circlePosition, circleRadius, projectedPoint, capsuleRadius, out mtv);
    }

    void CapsulePoints(Vector3 position, Vector3 direction, float halfHeight, out Vector3 top, out Vector3 bot)
    {
        top = position + direction * halfHeight;
        bot = position - direction * halfHeight;
    }

    Vector3 ProjectPointLine(Vector3 P, Vector3 A, Vector3 B)
    {
        Vector3 AB = B - A;
        Vector3 AP = P - A;
        float t = Vector3.Dot(AB, AP) / Vector3.Dot(AB, AB);
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        return A + AB * t;
    }

    bool CheckCollisionCircles(Vector3 position1, float radius1, Vector3 position2, float radius2, out Vector3 mtv)
    {
        float distance = (position1 - position2).magnitude;
        Vector3 direction = (position1 - position2).normalized;
        float radiiSum = radius1 + radius2;

        bool collision = distance < radiiSum;
        if (collision)
        {
            float depth = radiiSum - distance;
            mtv = direction * depth;
        }
        else
        {
            mtv = Vector3.zero;
        }
        return collision;
    }

    bool CheckCollisionSquareCircle(Vector2 squarePosition, Vector2 squareExtents, Vector2 circlePosition, float circleRadius, out Vector2 mtv)
    {
        Vector2 nearest = circlePosition;
        nearest.x = Mathf.Clamp(nearest.x, squarePosition.x - squareExtents.x, squarePosition.x + squareExtents.x);
        nearest.y = Mathf.Clamp(nearest.y, squarePosition.y - squareExtents.y, squarePosition.y + squareExtents.y);

        bool collision = Vector2.Distance(nearest, circlePosition) <= circleRadius;
        mtv = collision ? (nearest - circlePosition).normalized * (circleRadius - Vector2.Distance(nearest, circlePosition)) : Vector2.zero;
        return collision;
    }

    bool CheckCollisionSquareCapsule(Vector2 squarePosition, Vector2 squareExtents,
            Vector3 capsulePosition, float capsuleRadius, Vector3 direction, float halfHeight, out Vector3 mtv)
    {
        Vector3 top, bot;
        CapsulePoints(capsulePosition, direction, halfHeight, out top, out bot);

        Vector2 closestPoint = ClosestPointSquareToLine(squarePosition, squareExtents, top, bot);

        float sqrCapsuleRadius = capsuleRadius * capsuleRadius;
        float sqrDistance = ((Vector3)closestPoint - capsulePosition).sqrMagnitude;

        bool collision = sqrDistance <= sqrCapsuleRadius;
        mtv = collision ? ((Vector3)closestPoint - capsulePosition).normalized * (capsuleRadius - Mathf.Sqrt(sqrDistance)) : Vector3.zero;
        return collision;
    }

    Vector2 ClosestPointSquareToLine(Vector2 rectanglePosition, Vector2 rectangleExtents, Vector3 A, Vector3 B)
    {
        Vector2 closest = rectanglePosition;

        closest.x = Mathf.Clamp(closest.x, Mathf.Min(A.x, B.x) - rectangleExtents.x, Mathf.Max(A.x, B.x) + rectangleExtents.x);
        closest.y = Mathf.Clamp(closest.y, Mathf.Min(A.y, B.y) - rectangleExtents.y, Mathf.Max(A.y, B.y) + rectangleExtents.y);

        return closest;
    }


    void Update()
    {
        Vector3 circlePosition = circle.transform.position;
        float circleRadius = circle.transform.localScale.x * 0.5f;

        Vector3 capsulePosition = capsule.transform.position;
        Vector3 capsuleDirection = capsule.transform.up;
        float capsuleHalfHeight = capsule.transform.localScale.y * 0.5f;
        float capsuleRadius = capsule.transform.localScale.x * 0.5f;

        Vector3 squarePosition = square.transform.position;
        Vector2 squareExtents = new Vector2(square.transform.localScale.x * 0.5f, square.transform.localScale.y * 0.5f);

        bool circleCapsuleCollision = CheckCollisionCircleCapsule(circlePosition, circleRadius, capsulePosition,
            capsuleRadius, capsuleDirection.normalized, capsuleHalfHeight, out _);

        bool squareCapsuleCollision = CheckCollisionSquareCapsule(squarePosition, squareExtents, capsulePosition,
                    capsuleRadius, capsuleDirection.normalized, capsuleHalfHeight, out _);


        bool squareCircleCollision = CheckCollisionSquareCircle(squarePosition, squareExtents, circlePosition,
            circleRadius, out _);

        circle.GetComponent<SpriteRenderer>().color = circleCapsuleCollision || squareCircleCollision ? Color.green : Color.red;
        square.GetComponent<SpriteRenderer>().color = squareCircleCollision ? Color.green : Color.red;
        capsule.GetComponent<SpriteRenderer>().color = circleCapsuleCollision || squareCapsuleCollision || squareCapsuleCollision ? Color.green : Color.red;
    }
}


