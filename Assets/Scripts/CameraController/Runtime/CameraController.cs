using UnityEngine;

public class CameraController
{
    private readonly Transform transform;
    private readonly int anglePerStep;
    private readonly float rotationSpeed;

    private bool done = true;
    internal bool active = true;

    private float angleToTarget = 0;

    private int direction = 0;

    public CameraController(Transform transform, int anglePerStep = 90, float rotationSpeed = 1f)
    {
        this.transform = transform;
        this.anglePerStep = anglePerStep;
        this.rotationSpeed = rotationSpeed;
    }

    public void SetDirection(int value)
    {
        if (!active)
            return;

        if (!PlayerData.currentNavigationPoint.canTurn)
            return;

        if (!done)
            return;

        done = false;
        direction = value;

        angleToTarget = anglePerStep;
    }

    public void Update()
    {
        if (!active)
            return;

        if (done)
            return;

        Turn();
    }

    private void EndTurn()
    {
        done = true;
        direction = 0;
    }

    private void Turn()
    {
        Rotate(direction);

        if (angleToTarget > 0)
            return;

        EndTurn();
    }

    private void Rotate(int direction)
    {
        int rotation = anglePerStep * direction;

        Vector3 rotationVector = new Vector3(0, rotation, 0);

        Vector3 eulers = rotationVector * (rotationSpeed * Time.deltaTime);

        angleToTarget -= Mathf.Abs(eulers.y);

        transform.Rotate(eulers);
    }

    private PlayerData PlayerData => PlayerData.Instance;
}
