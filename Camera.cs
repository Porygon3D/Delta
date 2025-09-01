using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class Camera
{
    private Vector3 position;
    private float yaw;
    private float pitch;
    private int viewportWidth;
    private int viewportHeight;
    private float nearClip = 10.0f;
    private float farClip = 100000.0f;

    public Vector3 Position => position;

    public float Yaw => yaw;

    public float Pitch => pitch;

    public Matrix ViewMatrix
    {
        get
        {
            Vector3 cameraDirection = Vector3.Transform(Vector3.Forward, Matrix.CreateFromYawPitchRoll(yaw, pitch, 0));
            Vector3 cameraTarget = position + cameraDirection;
            return Matrix.CreateLookAt(position, cameraTarget, Vector3.Up);
        }
    }

    public Matrix ProjectionMatrix { get; private set; }

    public Camera(Vector3 startPosition, float startYaw, float startPitch, int viewportWidth, int viewportHeight)
    {
        position = startPosition;
        yaw = startYaw;
        this.viewportWidth = viewportWidth;
        this.viewportHeight = viewportHeight;
        this.nearClip = nearClip;
        this.farClip = farClip;

        ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.ToRadians(45),
            viewportWidth / (float)viewportHeight,
            nearClip,
            farClip
        );
    }
}