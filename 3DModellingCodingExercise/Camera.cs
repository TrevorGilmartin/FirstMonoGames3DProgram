using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModellingCodingExercise
{
    public class Camera : GameComponent
    {
        Vector3 cameraLocation;
        Vector3 cameraTarget;
        float fieldOfView = 45;
        float aspectRatio;
        float nearPlane = 0.1f;
        float farPlane = 1000;

        Matrix cameraRotation;
        Matrix viewMatrix;
        Matrix projectionMatrix;

        public Matrix View { get { return viewMatrix; } }
        public Matrix Projection { get { return projectionMatrix; } }

        public Camera(Game game, Vector3 location, Vector3 target)
            :base(game)
        {
            this.cameraLocation = location;
            this.cameraTarget = target;

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            aspectRatio = Game.GraphicsDevice.DisplayMode.AspectRatio;

            cameraRotation = Matrix.CreateRotationY(0);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), aspectRatio, nearPlane, farPlane);

            UpdateViewMatrix();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateViewMatrix();

            base.Update(gameTime);
        }

        private void UpdateViewMatrix()
        {
            var lookAtLocation = cameraLocation + cameraTarget;
            viewMatrix = Matrix.CreateLookAt(cameraLocation, lookAtLocation, Vector3.Up);
        }


    }
}
