using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Disable : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public float fadeDuration = 2.0f;
    private bool fadeingInProgress = true;
    public MeshRenderer[] MeshRendChildArray;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Car Disable")) && boxCollider != null)
        {
            StartCoroutine(FadeMaterialsCoroutine(1f, 0f));
            StartCoroutine(DelayedActiveflase());
        }
    }
    private System.Collections.IEnumerator DelayedActiveflase()
    {
       
        yield return new WaitForSeconds(2f);
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        CarEngine aiCarEngine = GetComponent<CarEngine>();
        aiCarEngine.ResetCurrentNode();

        Traffic_Vehicle TraficCar = GetComponent<Traffic_Vehicle>();
        TraficCar.TrafficMode = false;
        TraficCar.ResetCurrentNode();

        gameObject.SetActive(false);
    }

  
    private IEnumerator FadeMaterialsCoroutine(float startAlpha, float targetAlpha)
    {
        fadeingInProgress = true;
        float elapsedTime = 0f;
        Color startColor = new Color(1f, 1f, 1f, startAlpha);
        Color targetColor = new Color(1f, 1f, 1f, targetAlpha);

        while (elapsedTime < fadeDuration)
        {

            elapsedTime += Time.deltaTime;
            float percentage = Mathf.Clamp01(elapsedTime / fadeDuration);
            foreach (MeshRenderer meshRenderer in MeshRendChildArray)
            {
                foreach (Material material in meshRenderer.materials)
                {
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_Zwrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetFloat("_Mode", 3.0f);
                    material.color = Color.Lerp(startColor, targetColor, percentage);
                }
            }
            yield return null;
        }

        foreach (MeshRenderer meshRenderer in MeshRendChildArray)
        {
            foreach (Material material in meshRenderer.materials)
            {
                material.color = targetColor;
                material.SetOverrideTag("Render Type", "");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_Zwrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;

            }
        }
        fadeingInProgress = false;
    }
}
