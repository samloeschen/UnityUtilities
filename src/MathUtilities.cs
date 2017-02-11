using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public static class MathUtilities {
    //pre-allocate rectangle arrays
    public static Vector2[] rectP1 = new Vector2[4];
    public static Vector2[] rectP2 = new Vector2[4];
    public static void LineBoxIntersect(Vector2 point1, Vector2 point2, Rect rect, Vector2[] intersect) {
        int next = 0;
        //order is clockwise from bottom left -> left edge, top edge, right edge, bottom edge

        rectP1[0] = new Vector2(rect.xMin, rect.yMin); //bottom left
        rectP1[1] = new Vector2(rect.xMin, rect.yMax); //top left
        rectP1[2] = new Vector2(rect.xMax, rect.yMax); //top right
        rectP1[3] = new Vector2(rect.xMax, rect.yMin); // bottom right

        rectP2[0] = new Vector2(rect.xMin, rect.yMax); //top left
        rectP2[1] = new Vector2(rect.xMax, rect.yMax); //top right
        rectP2[2] = new Vector2(rect.xMax, rect.yMin); //bottom right
        rectP2[3] = new Vector2(rect.xMin, rect.yMin); //bottom left

        for (int i = 0; i < 4; i++) {
            Vector2 test;
            if (LineIntersect(point1, point2, rectP1[i], rectP2[i], out test)) {
                intersect[next] = test;
                next++;
            }
        }
    }

    public static bool LineIntersect(Vector2 a0, Vector2 a1, Vector2 b0, Vector2 b1, out Vector2 intersectionPoint) {
        intersectionPoint = Vector2.zero;

        if (a0 == b0 || a0 == b1 || a1 == b0 || a1 == b1)
            return false;

        float x1 = a0.x;
        float y1 = a0.y;
        float x2 = a1.x;
        float y2 = a1.y;
        float x3 = b0.x;
        float y3 = b0.y;
        float x4 = b1.x;
        float y4 = b1.y;

        //AABB early exit
        if (Mathf.Max(x1, x2) < Mathf.Min(x3, x4) || Mathf.Max(x3, x4) < Mathf.Min(x1, x2))
            return false;

        if (Mathf.Max(y1, y2) < Mathf.Min(y3, y4) || Mathf.Max(y3, y4) < Mathf.Min(y1, y2))
            return false;

        float ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3));
        float ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3));
        float denom = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
        if (Mathf.Abs(denom) < Mathf.Epsilon) {
            //Lines are too close to parallel to call
            return false;
        }
        ua /= denom;
        ub /= denom;

        if ((0 < ua) && (ua < 1) && (0 < ub) && (ub < 1)) {
            intersectionPoint.x = (x1 + ua * (x2 - x1));
            intersectionPoint.y = (y1 + ua * (y2 - y1));
            return true;
        }
        return false;
    }

    public static bool PointInRect(Vector2 point, Rect rect) {
        if (point.x > rect.min.x &&
            point.x < rect.max.x &&
            point.y < rect.min.y &&
            point.y > rect.max.y) {
            return true;
        } else {
            return false;
        }
    }

    public static bool CircleLineCheck(Vector2 point1, Vector2 point2, Vector2 circleOrigin, float circleRadius) {
        Vector2 localPoint1 = point1 - circleOrigin;
        Vector2 localPoint2 = point2 - circleOrigin;
        Vector2 lineOffset = localPoint2 - localPoint1;

        float a = (lineOffset.x * lineOffset.x) + (lineOffset.y * lineOffset.y);
        float b = 2f * ((lineOffset.x * localPoint1.x) + (lineOffset.y * localPoint1.y));
        float c = (localPoint1.x * localPoint1.x) + (localPoint1.y * localPoint1.y) - (circleRadius * circleRadius);
        float disc = (b * b) - (4f * a * c);

        return (disc > 0f);
    }

    public static void CircleLineCheckPointsNonAlloc(Vector2 point1, Vector2 point2, Vector2 circleOrigin, float circleRadius, ref Vector2[] intersections) {
        Vector2 localPoint1 = point1 - circleOrigin;
        Vector2 localPoint2 = point2 - circleOrigin;
        Vector2 lineOffset = localPoint2 - localPoint1;

        float a = (lineOffset.x * lineOffset.x) + (lineOffset.y * lineOffset.y);
        float b = 2f * ((lineOffset.x * localPoint1.x) + (lineOffset.y * localPoint1.y));
        float c = (localPoint1.x * localPoint1.x) + (localPoint1.y * localPoint1.y) - (circleRadius * circleRadius);
        float disc = (b * b) - (4f * a * c);

        if (disc > 0f) {

            disc = Mathf.Sqrt(disc);

            float t1 = (-b - disc) / (2 * a);
            float t2 = (-b + disc) / (2 * a);

            if (t1 >= 0 && t2 >= 0) {
                //total intersection, populate both points                
                intersections = new Vector2[2];
                intersections[0] = point1 + ((point2 - point1) * t1);
                intersections[1] = point1 + ((point2 - point1) * t2);
            } else {
                if (t1 >= 0) {
                    intersections[0] = point1 + ((point2 - point1) * t1);
                } else if (t2 >= 0) {
                    intersections[0] = point1 + ((point2 - point1) * t2);
                }
            }
        }
        return;
    }
   
}
