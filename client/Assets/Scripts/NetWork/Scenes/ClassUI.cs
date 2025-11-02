using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeReference]
    GameObject Anim;
    void Start()
    {

    }

    public void ShowAnim()
    {
        Anim.SetActive(true);
    }

    public void HideAnim()
    {
        Anim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
