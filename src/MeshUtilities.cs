using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUtilities : MonoBehaviour {

    public static Mesh MakeUnitQuad() {
        Mesh m = new Mesh();
        //make verts, bottom left first
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-0.5f, -0.5f, 0); //bottom left
        vertices[1] = new Vector3(0.5f, -0.5f, 0); //bottom right
        vertices[2] = new Vector3(-0.5f, 0.5f, 0); //top left
        vertices[3] = new Vector3(0.5f, 0.5f, 0); //top right
        m.vertices = vertices;

        //make tris
        int[] tris = new int[6];
        //lower left tri
        tris[0] = 0;
        tris[1] = 2;
        tris[2] = 1;
        //upper right tri
        tris[3] = 2;
        tris[4] = 3;
        tris[5] = 1;
        m.triangles = tris;

        //make normals
        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        m.normals = normals;

        //make uvs
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        m.uv = uv;

        return m;
    }

    public static Mesh QuadFromRect(Rect r) {
        Mesh m = new Mesh();
        //make verts, bottom left first
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0, 0, 0); //bottom left
        vertices[1] = new Vector3(r.width, 0, 0); //bottom right
        vertices[2] = new Vector3(0, r.height, 0); //top left
        vertices[3] = new Vector3(r.width, r.height, 0); //top right
        m.vertices = vertices;

        //make tris
        int[] tris = new int[6];
        //lower left tri
        tris[0] = 0;
        tris[1] = 2;
        tris[2] = 1;
        //upper right tri
        tris[3] = 2;
        tris[4] = 3;
        tris[5] = 1;
        m.triangles = tris;

        //make normals
        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        m.normals = normals;

        //make uvs
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        m.uv = uv;

        return m;
    }
}
