using Kinemation.AdvancedLookComponent.Runtime;
using UnityEngine;

public class LookAtWithLimits : MonoBehaviour
{
    public Transform target;
    public float horizontalRotationValue;
    public float verticalRotationValue;
    public LookComponent lookComponent;
    public float aimX, aimY;

    private void Update()
    {
        ComputeHorizontalRotation();
        ComputeVerticalRotation();

        Debug.Log($"AimY: {aimY}, AimX: {aimX}");
        lookComponent.SetAimTargetRotation(new Vector2(aimX, aimY));
    }

    private void ComputeHorizontalRotation()
    {
        Vector3 directionToTarget = (target.position - transform.position).WithY(0).normalized;
        Vector3 expectedForward = transform.parent.TransformDirection(Vector3.forward).WithY(0).normalized;

        horizontalRotationValue = Vector3.SignedAngle(expectedForward, directionToTarget, Vector3.up);
        horizontalRotationValue = ClampAngle(horizontalRotationValue, -180f, 180f);

        if (horizontalRotationValue > -90 && horizontalRotationValue < 90)
            aimY = horizontalRotationValue;
    }

    private void ComputeVerticalRotation()
    {
        Vector3 directionToTarget = target.position - transform.position;
        float targetPitchAngle = Mathf.Asin(directionToTarget.y / directionToTarget.magnitude) * Mathf.Rad2Deg;

        float parentPitchAngle = transform.parent.eulerAngles.x;
        parentPitchAngle = ClampAngle(parentPitchAngle, -180f, 180f);

        verticalRotationValue = targetPitchAngle - parentPitchAngle;
        verticalRotationValue = ClampAngle(verticalRotationValue, -90f, 90f);

        if (verticalRotationValue > -90 && verticalRotationValue < 90)
            aimX = verticalRotationValue;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > max)
            angle -= 360f;
        else if (angle < min)
            angle += 360f;
        return angle;
    }
}

public static class Vector3Extensions
{
    public static Vector3 WithY(this Vector3 vector, float newYValue)
    {
        vector.y = newYValue;
        return vector;
    }
}
