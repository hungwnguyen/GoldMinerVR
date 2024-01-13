using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class VisionCone : MonoBehaviour
    {
        [SerializeField] private Material _visionConeMaterial;
        [SerializeField] private float _visionRange;
        [Tooltip("Angle in degrees")]
        [SerializeField] public float _visionAngle;
        [SerializeField] private int _visionConeResolution = 120;
        public Mesh VisionConeMesh { get; private set; }
        public MeshFilter MeshFilter { get; private set; }
        public MeshRenderer MeshRendered { get; private set; }

        void Start()
        {
            transform.position = Screen.Instance.PlayerRect.center;
            MeshRendered = GetComponent<MeshRenderer>();
            MeshRendered.material = _visionConeMaterial;
            MeshFilter = GetComponent<MeshFilter>();
            VisionConeMesh = new Mesh();
        }

        void Update()
        {
            DrawVisionCone();
        }

        void DrawVisionCone()
        {
            int[] triangles = new int[(_visionConeResolution - 1) * 3];
            Vector3[] Vertices = new Vector3[_visionConeResolution + 1];
            Vertices[0] = Vector3.zero;
            float Currentangle = -_visionAngle / 2;
            float angleIncrement = _visionAngle / (_visionConeResolution - 1);
            float Sine;
            float Cosine;

            for (int i = 0; i < _visionConeResolution; i++)
            {
                Sine = Mathf.Sin(Mathf.Deg2Rad * Currentangle);
                Cosine = Mathf.Cos(Mathf.Deg2Rad * Currentangle);
                Vector3 VertForward = (Vector3.up * Cosine) + (Vector3.right * Sine);
                Vertices[i + 1] = VertForward * _visionRange;
                Currentangle += angleIncrement;
            }

            for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
            {
                triangles[i] = 0;
                triangles[i + 1] = j + 1;
                triangles[i + 2] = j + 2;
            }

            VisionConeMesh.Clear();
            VisionConeMesh.vertices = Vertices;
            VisionConeMesh.triangles = triangles;
            MeshFilter.mesh = VisionConeMesh;
        }
    }
}
