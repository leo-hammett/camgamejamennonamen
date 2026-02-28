
using UnityEngine;
using System.Collections.Generic;

namespace InGraved.Trail
{
    /// <summary>
    /// Utility class for detecting encirclement by trail
    /// </summary>
    public static class EncirclementDetector
    {
        /// <summary>
        /// Check if a point is inside a polygon using ray casting algorithm
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <param name="polygon">Polygon vertices</param>
        /// <returns>True if point is inside polygon</returns>
        public static bool IsPointInPolygon(Vector2 point, List<Vector2> polygon)
        {
            if (polygon == null || polygon.Count < 3) return false;
            
            bool inside = false;
            int j = polygon.Count - 1;
            
            for (int i = 0; i < polygon.Count; i++)
            {
                if ((polygon[i].y > point.y) != (polygon[j].y > point.y) &&
                    point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / 
                    (polygon[j].y - polygon[i].y) + polygon[i].x)
                {
                    inside = !inside;
                }
                j = i;
            }
            
            return inside;
        }
        
        /// <summary>
        /// Find all generators encircled by a trail
        /// </summary>
        /// <param name="trail">Trail points forming potential polygon</param>
        /// <param name="generators">List of generators to check</param>
        /// <returns>List of encircled generators</returns>
        public static List<Generators.IStoneGenerator> FindEncircledGenerators(
            List<Vector2> trail, 
            List<Generators.IStoneGenerator> generators)
        {
            var encircled = new List<Generators.IStoneGenerator>();
            if (!IsTrailClosed(trail)) return encircled;
            
            foreach (var gen in generators)
            {
                if (gen.IsAlive && IsPointInPolygon(gen.Position, trail))
                {
                    encircled.Add(gen);
                }
            }
            
            return encircled;
        }
        
        /// <summary>
        /// Check if trail forms a closed loop (head near tail)
        /// </summary>
        /// <param name="trail">Trail points</param>
        /// <param name="closureDistance">Maximum distance for closure</param>
        /// <returns>True if trail is closed</returns>
        public static bool IsTrailClosed(List<Vector2> trail, float closureDistance = 1.0f)
        {
            if (trail == null || trail.Count < 3) return false;
            
            float distance = Vector2.Distance(trail[0], trail[trail.Count - 1]);
            return distance <= closureDistance;
        }
        
        /// <summary>
        /// Get the polygon formed by a closed trail
        /// </summary>
        /// <param name="trail">Trail points</param>
        /// <returns>Simplified polygon vertices, or null if trail not closed</returns>
        public static List<Vector2> GetTrailPolygon(List<Vector2> trail)
        {
            if (!IsTrailClosed(trail)) return null;
            return new List<Vector2>(trail);
        }
        
        /// <summary>
        /// Simplify a trail by removing redundant points
        /// </summary>
        /// <param name="trail">Original trail points</param>
        /// <param name="tolerance">Distance tolerance for simplification</param>
        /// <returns>Simplified trail</returns>
        public static List<Vector2> SimplifyTrail(List<Vector2> trail, float tolerance = 0.1f)
        {
            // TODO: Implement Douglas-Peucker or similar algorithm
            return new List<Vector2>(trail);
        }
        
        /// <summary>
        /// Calculate area of polygon formed by trail
        /// </summary>
        /// <param name="polygon">Polygon vertices</param>
        /// <returns>Area of polygon</returns>
        public static float CalculatePolygonArea(List<Vector2> polygon)
        {
            if (polygon == null || polygon.Count < 3) return 0f;
            
            float area = 0f;
            for (int i = 0; i < polygon.Count; i++)
            {
                int j = (i + 1) % polygon.Count;
                area += polygon[i].x * polygon[j].y;
                area -= polygon[j].x * polygon[i].y;
            }
            
            return Mathf.Abs(area) / 2f;
        }
        
        /// <summary>
        /// Check if two line segments intersect
        /// </summary>
        /// <param name="p1">First segment start</param>
        /// <param name="p2">First segment end</param>
        /// <param name="p3">Second segment start</param>
        /// <param name="p4">Second segment end</param>
        /// <returns>True if segments intersect</returns>
        public static bool DoSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            Vector2 d1 = p2 - p1;
            Vector2 d2 = p4 - p3;
            float cross = d1.x * d2.y - d1.y * d2.x;
            
            if (Mathf.Abs(cross) < 1e-6f) return false; // Parallel
            
            Vector2 diff = p3 - p1;
            float t = (diff.x * d2.y - diff.y * d2.x) / cross;
            float u = (diff.x * d1.y - diff.y * d1.x) / cross;
            
            return t > 0.01f && t < 0.99f && u > 0.01f && u < 0.99f;
        }
        
        /// <summary>
        /// Find self-intersections in trail
        /// </summary>
        /// <param name="trail">Trail points</param>
        /// <returns>List of intersection points</returns>
        public static List<Vector2> FindTrailIntersections(List<Vector2> trail)
        {
            var intersections = new List<Vector2>();
            // TODO: Implement intersection detection
            return intersections;
        }
    }
}

