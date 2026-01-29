using System.Collections.Generic;
using UnityEngine;

namespace TestGPUInstancing
{
    public class RenderMashA : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;

        private int MeshCount = 1023 * 4;
        private List<Matrix4x4[]> _batches;

        private void Start()
        {
            _batches = new List<Matrix4x4[]>();
            var matrices = new Matrix4x4[1023];

            for (int i = 0; i < MeshCount; i++)
            {
                if (i % 1023 == 0)
                {
                    matrices = new Matrix4x4[1023];
                    _batches.Add(matrices);
                }

                var pos = new Vector3(
                    UnityEngine.Random.Range(-10f, 10f),
                    UnityEngine.Random.Range(-10f, 10f),
                    UnityEngine.Random.Range(-10f, 10f)
                );

                matrices[i % 1023] = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
            }
        }

        private void Update()
        {
            foreach (var batch in _batches)
            {
                Graphics.DrawMeshInstanced(_mesh, 0, _material, batch, 1023);
            }
        }
    }
}
