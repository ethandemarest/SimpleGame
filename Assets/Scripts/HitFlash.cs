using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    SpriteRenderer sp;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(SpriteFlash());
    }
    public IEnumerator SpriteFlash()
    {
        sp.material.shader = shaderGUItext;
        sp.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sp.material.shader = shaderSpritesDefault;
        sp.color = Color.white;
    }

}
