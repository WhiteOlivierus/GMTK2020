using UnityEngine;

public class CameraController
{
    private readonly Transform transform;
    private readonly int angleStep;
    private readonly float rotationSpeed;

    private bool done = true;
    internal bool active = true;

    private float targetAngle = 0;
    private int currentTargetAngle = 0;
    private int direction = 0;

    public CameraController(Transform transform, int angleStep = 90, float rotationSpeed = 1f)
    {
        this.transform = transform;
        this.angleStep = angleStep;
        this.rotationSpeed = rotationSpeed;
    }

    public void SetDirection(int value)
    {
        if (!active ||
            !PlayerData.currentNavigationPoint.canTurn ||
            !done)
            return;

        done = false;
        direction = value;

        targetAngle = Mathf.RoundToInt(angleStep);

        currentTargetAngle = (int)WrapAngle((int)transform.rotation.eulerAngles.y) + ((int)targetAngle * direction);

        Debug.Log($"{nameof(CameraController)}: Next target angle is {currentTargetAngle}");

        if (PlayerData.currentNavigationPoint.fullCircle)
            return;

        if (currentTargetAngle > CurrentNavigationPoint.maxAngleRight ||
            currentTargetAngle < CurrentNavigationPoint.maxAngleLeft)
            EndTurn();
    }


    public void Update()
    {
        if (!active ||
            done)
            return;

        Turn();
    }

    private void EndTurn()
    {
        done = true;
        direction = 0;
        targetAngle = 0;
    }

    private void Turn()
    {
        Rotate(direction);

        if (targetAngle > 0)
            return;

        transform.eulerAngles = (new Vector3(0, currentTargetAngle, 0));

        EndTurn();
    }

    private void Rotate(int direction)
    {
        int rotation = angleStep * direction;

        Vector3 rotationVector = new Vector3(0, rotation, 0);

        Vector3 eulers = rotationVector * (rotationSpeed * Time.deltaTime);

        targetAngle -= Mathf.Abs(eulers.y);

        transform.Rotate(eulers);
    }

    private PlayerData PlayerData => PlayerData.Instance;
    private NavigationPointRoot CurrentNavigationPoint => PlayerData.currentNavigationPoint;

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle == 180 ? -180 : angle;
    }
}
