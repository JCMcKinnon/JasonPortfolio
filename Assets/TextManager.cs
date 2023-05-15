using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextManager : MonoBehaviour
{
    TMP_TextInfo info;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        info = text.textInfo;
    }

    void Update()
    {
        
        if (gameObject.activeSelf && StateManager.isPaused)
        {
            text.ForceMeshUpdate();

            var vert = info.meshInfo[0].vertices;

            DOTween.To(() => vert[0].y, y => vert[0].y = y, 20, 1).OnUpdate(() => text.UpdateVertexData());
        }
        //text.UpdateVertexData();

    }
    private void OnEnable()
    {
        info = text.textInfo;

        var vert = info.meshInfo[0].vertices;
        text.ForceMeshUpdate();

        text.UpdateVertexData();
    }
}
