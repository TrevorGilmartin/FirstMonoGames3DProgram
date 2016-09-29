using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DModellingCodingExercise
{
    class SampleModel
    {
        public Matrix[] boneTransforms { get; set; }
        public Model model { get; set; }
        public int vertexCount { get; set; }
        public Matrix world { get; set; }

        private string assetID;

        SampleModel()
        {

        }

        public SampleModel(string asset, Vector3 location, float scale)
        {
            assetID = asset;

            world = Matrix.Identity * Matrix.CreateScale(scale) *
                Matrix.CreateTranslation(location);
        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>(@"Models\" + assetID);

            boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    vertexCount += part.NumVertices;
        }

        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = view;
                    effect.Projection = projection;
                    effect.World = world;
                }
                mesh.Draw();
            }
        }
    }
}
