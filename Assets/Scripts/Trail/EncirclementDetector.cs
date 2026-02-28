#if false
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
        public static bool IsPointInPolygon(Vector2 point, List<Vector2> polygon);
        
        /// <summary>
        /// Find all generators encircled by a trail
        /// </summary>
        /// <param name="trail">Trail points forming potential polygon</param>
        /// <param name="generators">List of generators to check</param>
        /// <returns>List of encircled generators</returns>
        public static List<Generators.IStoneGenerator> FindEncircledGenerators(
            List<Vector2> trail, 
            List<Generators.IStoneGenerator> generators);
        
        /// <summary>
        /// Check if trail forms a closed loop (head near tail)
        /// </summary>
        /// <param name="trail">Trail points</param>
        /// <param name="closureDistance">Maximum distance for closure</param>
        /// <returns>True if trail is closed</returns>
        public static bool IsTrailClosed(List<Vector2> trail, float closureDistance = 1.0f);
        
        /// <summary>
        /// Get the polygon formed by a closed trail
        /// </summary>
        /// <param name="trail">Trail points</param>
        /// <returns>Simplified polygon vertices, or null if trail not closed</returns>
        public static List<Vector2> GetTrailPolygon(List<Vector2> trail);
        
        /// <summary>
        /// Simplify a trail by removing redundant points
        /// </summary>
        /// <param name="trail">Original trail points</param>
        /// <param name="tolerance">Distance tolerance for simplification</param>
        /// <returns>Simplified trail</returns>
        public static List<Vector2> SimplifyTrail(List<Vector2> trail, float tolerance = 0.1f);
        
        /// <summary>
        /// Calculate area of polygon formed by trail
        /// </summary>
        /// <param name="polygon">Polygon vertices</param>
        /// <returns>Area of polygon</returns>
        public static float CalculatePolygonArea(List<Vector2> polygon);
        
        /// <summary>
        /// Check if two line segments intersect
        /// </summary>
        /// <param name="p1">First segment start</param>
        /// <param name="p2">First segment end</param>
        /// <param name="p3">Second segment start</param>
        /// <param name="p4">Second segment end</param>
        /// <returns>True if segments intersect</returns>
        public static bool DoSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4);
        
        /// <summary>
        /// Find self-intersections in trail
        /// </summary>
        /// <param name="trail">Trail points</param>
        /// <returns>List of intersection points</returns>
        public static List<Vector2> FindTrailIntersections(List<Vector2> trail);
    }
}
#endif
