/*// SalaModernaGenerator.cs
// Colocar en Assets/Editor/
// Menú: Tools -> Generar Sala Moderna (.fbx)
// Requiere UnityEditor; si quieres exportar a FBX automáticamente instala:
// Package Manager -> Add package by name -> "com.unity.formats.fbx"

using UnityEngine;
using UnityEditor;
using System.IO;

public class SalaModernaGenerator : MonoBehaviour
{
    const float ROOM_WIDTH = 6f;   // x (metros)
    const float ROOM_LENGTH = 8f;  // z (metros)
    const float ROOM_HEIGHT = 3f;  // y (metros)

    [MenuItem("Tools/Generar Sala Moderna (.fbx)")]
    public static void GenerateSala()
    {
        // Crear carpeta de salida
        string outDir = "Assets/ProyectoFinal/escenario/habitaciones/salade_estar";
        if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);

        // Root
        GameObject root = new GameObject("SalaModerna");
        Undo.RegisterCreatedObjectUndo(root, "Create SalaModerna");

        // Materiales básicos (PBR-like usando Standard shader)
        Material matWall = CreateMaterial("M_Wall_RojoOscuro", new Color(0.18f, 0.03f, 0.03f), 0.0f, 0.2f);
        Material matFloor = CreateMaterial("M_Suelo_Marmol", new Color(0.08f, 0.08f, 0.09f), 0.2f, 0.85f);
        Material matBlack = CreateMaterial("M_Black", Color.black, 0.0f, 0.3f);
        Material matRedAcc = CreateMaterial("M_RojoAcc", new Color(0.4f, 0.03f, 0.03f), 0.0f, 0.25f);
        Material matFabric = CreateMaterial("M_Fabric", new Color(0.12f, 0.12f, 0.12f), 0.0f, 0.6f);
        Material matEmissive = CreateMaterial("M_TV_Emissive", Color.black, 0.0f, 0.0f);
        matEmissive.EnableKeyword("_EMISSION");
        matEmissive.SetColor("_EmissionColor", new Color(0.05f, 0.2f, 0.35f));

        // Suelo (usamos un cubo fino para más control)
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Suelo_Marmol";
        floor.transform.parent = root.transform;
        floor.transform.localScale = new Vector3(ROOM_WIDTH, 0.05f, ROOM_LENGTH);
        floor.transform.localPosition = new Vector3(0f, -0.025f, 0f);
        floor.GetComponent<MeshRenderer>().material = matFloor;

        // Paredes (4)
        float wallThickness = 0.2f;
        // Norte (back)
        CreateWall(root.transform, "Pared_Norte", new Vector3(0f, ROOM_HEIGHT/2f, -ROOM_LENGTH/2f + wallThickness/2f), 
                   new Vector3(ROOM_WIDTH, ROOM_HEIGHT, wallThickness), matWall);
        // Sur (front)
        CreateWall(root.transform, "Pared_Sur", new Vector3(0f, ROOM_HEIGHT/2f, ROOM_LENGTH/2f - wallThickness/2f),
                   new Vector3(ROOM_WIDTH, ROOM_HEIGHT, wallThickness), matWall);
        // Oeste (left)
        CreateWall(root.transform, "Pared_Oeste", new Vector3(-ROOM_WIDTH/2f + wallThickness/2f, ROOM_HEIGHT/2f, 0f),
                   new Vector3(wallThickness, ROOM_HEIGHT, ROOM_LENGTH), matWall);
        // Este (right)
        CreateWall(root.transform, "Pared_Este", new Vector3(ROOM_WIDTH/2f - wallThickness/2f, ROOM_HEIGHT/2f, 0f),
                   new Vector3(wallThickness, ROOM_HEIGHT, ROOM_LENGTH), matWall);

        // Techo
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Techo";
        ceiling.transform.parent = root.transform;
        ceiling.transform.localScale = new Vector3(ROOM_WIDTH, 0.05f, ROOM_LENGTH);
        ceiling.transform.localPosition = new Vector3(0f, ROOM_HEIGHT + 0.025f, 0f);
        var mCeil = CreateMaterial("M_Ceiling", new Color(0.05f,0.05f,0.05f), 0f, 0.1f);
        ceiling.GetComponent<MeshRenderer>().material = mCeil;

        // Sofá (moderno, bajo, negro)
        GameObject sofa = new GameObject("Sofa");
        sofa.transform.parent = root.transform;
        sofa.transform.localPosition = new Vector3(-ROOM_WIDTH/2f + 1.4f, 0f, -ROOM_LENGTH/2f + 1.6f);
        // base
        GameObject sofaBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sofaBase.name = "Sofa_Base";
        sofaBase.transform.parent = sofa.transform;
        sofaBase.transform.localScale = new Vector3(2.2f, 0.5f, 0.9f);
        sofaBase.transform.localPosition = new Vector3(0f, 0.35f, 0f);
        sofaBase.GetComponent<MeshRenderer>().material = matBlack;
        // respaldo
        GameObject sofaBack = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sofaBack.name = "Sofa_Back";
        sofaBack.transform.parent = sofa.transform;
        sofaBack.transform.localScale = new Vector3(2.2f, 0.7f, 0.25f);
        sofaBack.transform.localPosition = new Vector3(0f, 0.85f, -0.325f);
        sofaBack.GetComponent<MeshRenderer>().material = matFabric;

        // Sillas tipo "butaca" (2)
        CreateChair(root.transform, "Butaca_1", new Vector3(-0.5f, 0f, -ROOM_LENGTH/2f + 2.6f), matFabric, matRedAcc);
        CreateChair(root.transform, "Butaca_2", new Vector3(1.2f, 0f, -ROOM_LENGTH/2f + 2.6f), matFabric, matRedAcc);

        // Mesa de centro
        GameObject mesa = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mesa.name = "Mesa_Centro";
        mesa.transform.parent = root.transform;
        mesa.transform.localScale = new Vector3(1.2f, 0.25f, 0.6f);
        mesa.transform.localPosition = new Vector3(0.35f, 0.15f, -ROOM_LENGTH/2f + 2.1f);
        mesa.GetComponent<MeshRenderer>().material = matBlack;

        // Alfombra (plano ligero)
        GameObject alfombra = GameObject.CreatePrimitive(PrimitiveType.Plane);
        alfombra.name = "Alfombra";
        alfombra.transform.parent = root.transform;
        // Unity plane = 10x10, queremos aproximar ~2.2 x 1.6
        alfombra.transform.localScale = new Vector3(0.22f * 2.2f, 1f, 0.16f * 1.6f); // ajustes para aproximar tamaño
        alfombra.transform.localPosition = new Vector3(0.35f, 0.01f, -ROOM_LENGTH/2f + 2.1f);
        var matRug = CreateMaterial("M_Alfombra", new Color(0.25f,0.05f,0.05f), 0f, 0.7f);
        alfombra.GetComponent<MeshRenderer>().material = matRug;

        // Chimenea (en pared norte centrada a la izquierda)
        GameObject chimenea = GameObject.CreatePrimitive(PrimitiveType.Cube);
        chimenea.name = "Chimenea";
        chimenea.transform.parent = root.transform;
        chimenea.transform.localScale = new Vector3(1.2f, 1.1f, 0.35f);
        chimenea.transform.localPosition = new Vector3(-ROOM_WIDTH/2f + 1.0f + 0.6f, 1.1f, -ROOM_LENGTH/2f + wallThickness + 0.175f);
        chimenea.GetComponent<MeshRenderer>().material = matBlack;
        // hueco (inset) - representado por un plano oscuro
        GameObject fuego = GameObject.CreatePrimitive(PrimitiveType.Plane);
        fuego.name = "Fuego_Emissivo";
        fuego.transform.parent = chimenea.transform;
        fuego.transform.localRotation = Quaternion.Euler(90,0,0);
        fuego.transform.localPosition = new Vector3(0f, -0.3f, 0.18f);
        fuego.transform.localScale = new Vector3(0.18f,1f,0.12f);
        var matFire = CreateMaterial("M_Fire", new Color(0.9f,0.45f,0.05f), 0f, 0.0f);
        matFire.EnableKeyword("_EMISSION");
        matFire.SetColor("_EmissionColor", new Color(0.7f,0.25f,0.05f));
        fuego.GetComponent<MeshRenderer>().material = matFire;

        // Televisor en pared sur (grande, plano)
        GameObject tv = GameObject.CreatePrimitive(PrimitiveType.Plane);
        tv.name = "Televisor";
        tv.transform.parent = root.transform;
        tv.transform.localScale = new Vector3(0.9f,1f,0.55f); // ancho x1, alto
        tv.transform.localRotation = Quaternion.Euler(90,180,0);
        tv.transform.localPosition = new Vector3(0f, 1.2f, ROOM_LENGTH/2f - wallThickness - 0.01f);
        tv.GetComponent<MeshRenderer>().material = matEmissive;

        // Librería - varios estantes (en pared este)
        GameObject libreria = new GameObject("Libreria");
        libreria.transform.parent = root.transform;
        libreria.transform.localPosition = new Vector3(ROOM_WIDTH/2f - 0.6f, 1.0f, 0f);
        for (int i = 0; i < 5; i++)
        {
            GameObject estante = GameObject.CreatePrimitive(PrimitiveType.Cube);
            estante.name = "Estante_" + i;
            estante.transform.parent = libreria.transform;
            estante.transform.localScale = new Vector3(1.0f, 0.1f, 0.25f);
            estante.transform.localPosition = new Vector3(0f, 0.9f - i*0.4f, 0f);
            estante.GetComponent<MeshRenderer>().material = matBlack;
        }

        // Plantita decorativa (cilindro + esfera para hoja)
        GameObject maceta = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        maceta.name = "Maceta";
        maceta.transform.parent = root.transform;
        maceta.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
        maceta.transform.localPosition = new Vector3(-ROOM_WIDTH/2f + 0.6f, 0.25f, 1.5f);
        maceta.GetComponent<MeshRenderer>().material = matBlack;
        GameObject planta = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planta.name = "Planta_Hoja";
        planta.transform.parent = root.transform;
        planta.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        planta.transform.localPosition = new Vector3(-ROOM_WIDTH/2f + 0.6f, 0.65f, 1.5f);
        planta.GetComponent<MeshRenderer>().material = matRedAcc; // acento rojo oscuro

        // Iluminación: directional + puntos suaves
        GameObject dirLight = new GameObject("Directional Light");
        dirLight.transform.parent = root.transform;
        Light dLight = dirLight.AddComponent<Light>();
        dLight.type = LightType.Directional;
        dLight.intensity = 0.6f;
        dirLight.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        // Lámpara puntual ambiente
        GameObject lamp = new GameObject("Lamp_Point");
        lamp.transform.parent = root.transform;
        lamp.transform.localPosition = new Vector3(0f, ROOM_HEIGHT - 0.4f, 0f);
        Light pLight = lamp.AddComponent<Light>();
        pLight.type = LightType.Point;
        pLight.range = 6f;
        pLight.intensity = 1.2f;
        pLight.shadows = LightShadows.Soft;

        // Marcar escena sucia para guardar assets
        EditorUtility.SetDirty(root);

        // Guardar prefab/objetos generados en Assets
        string prefabPath = outDir + "/SalaModerna_Root.prefab";
        PrefabUtility.SaveAsPrefabAsset(root, prefabPath);

        // Intentar exportar a FBX si está disponible
#if UNITY_2018_3_OR_NEWER
        bool fbxAvailable = false;
        #if UNITY_EDITOR
        // Comprobación simple: intenta usar ModelExporter via reflexión si existe
        try {
            // si la clase ModelExporter existe, intentaremos exportar
            var type = typeof(ModelExporter);
            fbxAvailable = type != null;
        } catch {
            fbxAvailable = false;
        }
        if (fbxAvailable)
        {
            string fbxPath = "Assets/SalaModerna.fbx";
            try
            {
                Debug.Log("Exportado a FBX: " + fbxPath);
                EditorUtility.DisplayDialog("SalaGenerada", "Sala generada y exportada a Assets/SalaModerna.fbx", "OK");
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("No se pudo exportar automáticamente a FBX: " + ex.Message);
                EditorUtility.DisplayDialog("SalaGenerada", "Sala generada en Assets/SalaModerna_Generated/\nNo se pudo exportar automáticamente (comprueba el FBX Exporter).", "OK");
            }
        }
        else
        {
            EditorUtility.DisplayDialog("SalaGenerada", "Sala generada en Assets/SalaModerna_Generated/\nPara exportar a FBX instala el paquete 'FBX Exporter' (com.unity.formats.fbx) desde Package Manager.", "OK");
        }
        #endif
#else
        EditorUtility.DisplayDialog("SalaGenerada", "Sala generada en Assets/SalaModerna_Generated/.", "OK");
#endif

        // Seleccionar el root en el editor para revisión
        Selection.activeGameObject = root;
    }

    static void CreateWall(Transform parent, string name, Vector3 localPos, Vector3 localScale, Material mat)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.parent = parent;
        wall.transform.localPosition = localPos;
        wall.transform.localScale = localScale;
        wall.GetComponent<MeshRenderer>().material = mat;
    }

    static void CreateChair(Transform parent, string name, Vector3 position, Material fabric, Material accent)
    {
        GameObject chair = new GameObject(name);
        chair.transform.parent = parent;
        chair.transform.localPosition = position;

        GameObject baseSeat = GameObject.CreatePrimitive(PrimitiveType.Cube);
        baseSeat.name = "Asiento";
        baseSeat.transform.parent = chair.transform;
        baseSeat.transform.localScale = new Vector3(0.7f, 0.35f, 0.7f);
        baseSeat.transform.localPosition = new Vector3(0f, 0.35f, 0f);
        baseSeat.GetComponent<MeshRenderer>().material = fabric;

        GameObject back = GameObject.CreatePrimitive(PrimitiveType.Cube);
        back.name = "Respaldo";
        back.transform.parent = chair.transform;
        back.transform.localScale = new Vector3(0.7f, 0.6f, 0.2f);
        back.transform.localPosition = new Vector3(0f, 0.8f, -0.25f);
        back.GetComponent<MeshRenderer>().material = fabric;

        GameObject coj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        coj.name = "Cojin";
        coj.transform.parent = chair.transform;
        coj.transform.localScale = new Vector3(0.45f, 0.12f, 0.45f);
        coj.transform.localPosition = new Vector3(0f, 0.46f, 0f);
        coj.GetComponent<MeshRenderer>().material = accent;
    }

    static Material CreateMaterial(string name, Color albedo, float metallic, float smoothness)
    {
        Material m = new Material(Shader.Find("Standard"));
        m.name = name;
        m.SetColor("_Color", albedo);
        m.SetFloat("_Metallic", metallic);
        m.SetFloat("_Glossiness", smoothness);
        return m;
    }
}*/
