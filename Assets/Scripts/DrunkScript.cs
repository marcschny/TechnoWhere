using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class DrunkScript : MonoBehaviour
{
    private Volume volume;
    private LensDistortion lensDistortion;
    private ColorAdjustments colorAdjustments;

    public float lensDistortionSpeed = 0.1f;
    public float hueShiftSpeed = 0.5f;

    public Vector2 targetCenter;

    public Vector2 maxCenter = new Vector2(1f, 1f);

    public Vector2 minCenter = new Vector2(0.1f, 0.1f);

    public Vector2 currentCenter;
    public Vector2 defaultCenter = new(0.5f, 0.5f);

    public float defaultHueShift = 0f;
    public float currentHueShift;
    public float targetHueShift;


    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out lensDistortion);
        volume.profile.TryGet(out colorAdjustments);

        targetCenter = GetNewTargetCenter();
        currentCenter = lensDistortion.center.value;

        currentHueShift = colorAdjustments.hueShift.value;
        targetHueShift = 180f;
    }

    void FixedUpdate()
    {
        ModifyLensDistortion();

        ModifyColors();
    }

    private void ModifyColors()
    {
        currentHueShift = Mathf.MoveTowards(currentHueShift, targetHueShift, hueShiftSpeed);
        colorAdjustments.hueShift.Override(currentHueShift);

        if (currentHueShift < -179f)
        {
            targetHueShift = 180f;
        }
        if (currentHueShift > 179f)
        {
            targetHueShift = -180;
        }
    }


    private void ModifyLensDistortion()
    {
        currentCenter = Vector2.MoveTowards(currentCenter, targetCenter, lensDistortionSpeed * Time.deltaTime);
        lensDistortion.center.Override(currentCenter);

        if (Vector2.Distance(currentCenter, targetCenter) < 0.002f)
        {
            targetCenter = GetNewTargetCenter();
        }
    }

    private Vector2 GetNewTargetCenter()
    {
        return new Vector2(Random.Range(minCenter.x, maxCenter.x), Random.Range(minCenter.y, maxCenter.y));
    }

    public void SetToDefault()
    {
        lensDistortion.center.Override(defaultCenter);
        colorAdjustments.hueShift.Override(defaultHueShift);


    }
}