
using System.Reflection;
using UnityEngine;

 
[RequireComponent(typeof(UnityEngine.Rendering.Universal.ShadowCaster2D))]
[ExecuteInEditMode]
public class ShadowCaster2DController : MonoBehaviour
{
    [SerializeField] private bool setOnAwake;
    [HideInInspector, SerializeField] private UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster;
    [HideInInspector, SerializeField] EdgeCollider2D edgeCollider;
    [HideInInspector, SerializeField] PolygonCollider2D polyCollider;
 
    // Shadow caster fields to change
    private readonly FieldInfo _shapePathField;
    private readonly FieldInfo _shapeHash;
 
    private ShadowCaster2DController()
    {
        _shapeHash = typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
        _shapePathField = typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
    }
 
    private void Awake()
    {
        shadowCaster = GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        polyCollider = GetComponent<PolygonCollider2D>();
 
        if (!setOnAwake) { return; }
 
        if (edgeCollider != null)
        {
            UpdateShadowFromPoints(edgeCollider.points);
        }
        else if (polyCollider != null)
        {
            UpdateShadowFromPoints(polyCollider.points);
        }
    }
 
    /// <summary>
    /// Updates the shadow based on the objects own collider
    /// </summary>
    public void UpdateFromCollider()
    {
        if (edgeCollider != null)
        {
            UpdateShadowFromPoints(edgeCollider.points);
        }
        else if (polyCollider != null)
        {
            UpdateShadowFromPoints(polyCollider.points);
        }
    }
 
    /// <summary>
    /// Updates the shadow from an array of Vector3 points
    /// </summary>
    /// <param name="points"></param>
    public void UpdateShadowFromPoints(Vector3[] points)
    {
        // Set the shadow path
        _shapePathField.SetValue(shadowCaster, points);
 
        unchecked
        {
            // I have no idea what im doing with hashcodes but other examples are done like this
            int hashCode = (int)2166136261 ^ _shapePathField.GetHashCode();
            hashCode = hashCode * 16777619 ^ (points.GetHashCode());
 
            // Set the shapes hash to a random value which forces it to update the mesh in the next frame
            _shapeHash.SetValue(shadowCaster, hashCode);
        }
    }
 
    /// <summary>
    /// Updates the shadow from an array of Vector2 points
    /// </summary>
    /// <param name="points"></param>
    public void UpdateShadowFromPoints(Vector2[] points)
    {
        // Set the shadow path
        _shapePathField.SetValue(shadowCaster, Vector2ToVector3(points));
 
        unchecked
        {
            // I have no idea what im doing with hashcodes but other examples are done like this
            int hashCode = (int)2166136261 ^ _shapePathField.GetHashCode();
            hashCode = hashCode * 16777619 ^ (points.GetHashCode());
 
            // Set the shapes hash to a random value which forces it to update the mesh in the next frame
            _shapeHash.SetValue(shadowCaster, hashCode);
        }
    }
 
    /// <summary>
    /// Converts an array of Vector2 to an array of Vector3
    /// </summary>
    /// <param name="points"></param>
    private Vector3[] Vector2ToVector3(Vector2[] vector2s)
    {
        Vector3[] vector3s = new Vector3[vector2s.Length];
 
        for (int i = 0; i < vector2s.Length; i++)
        {
            vector3s[i] = vector2s[i];
        }
 
        return vector3s;
    }
}
 