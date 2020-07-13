using UnityEngine;

public class CameraController
{
    private readonly Transform transform;
    private readonly int angleStep;
    private readonly float rotationSpeed;

    private bool done = true;
    internal bool active = true;

    private float targetAngle = 0;
    private int currentAngle = 0;
    private int direction = 0;

    public CameraController(Transform transform, int angleStep = 90, float rotationSpeed = 1f)
    {
        this.transform = transform;
        this.angleStep = angleStep;
        this.rotationSpeed = rotationSpeed;
        Calibrate();
    }

    public void Calibrate() => currentAngle = 0;

    public void SetDirection(int value)
    {
        if (!active ||
            !CurrentNavigationPoint.canTurn ||
            !done)
            return;

        if (value == 0)
        {
            SceneNavigation.Instance.Back();
            return;
        }

        done = false;

        direction = value;

        if (CurrentNavigationPoint.halfTurn)
            targetAngle = Mathf.RoundToInt(180);
        else
            targetAngle = Mathf.RoundToInt(angleStep);

        currentAngle += (int)WrapAngle(targetAngle * direction);

        if (!CurrentNavigationPoint.fullCircle && !CurrentNavigationPoint.halfTurn)
            if (currentAngle > CurrentNavigationPoint.maxAngleRight ||
                currentAngle < CurrentNavigationPoint.maxAngleLeft)
            {
                currentAngle -= (int)WrapAngle(targetAngle * direction);
                done = true;
                EndTurn();
                return;
            }

        Debug.Log($"{nameof(CameraController)}: Next target angle is {currentAngle} {targetAngle} {direction}");
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

        //transform.eulerAngles = (new Vector3(0, Mathf.Round(transform.rotation.eulerAngles.y), 0));
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

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle == 180 ? -180 : angle;
    }

    private PlayerData PlayerData => PlayerData.Instance;
    private NavigationRoot CurrentNavigationPoint => SceneNavigation.Instance.currentNavigationPoint;
}
