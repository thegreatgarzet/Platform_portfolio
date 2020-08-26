using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab: MonoBehaviour
{
    public List<GameObject> tabelas;
    public List<Button> botoestabela;
    public GameObject tabelaAtiva;
    public Button botaoTabelaAtivada;
    public void AtivaTab()
    {
        foreach (GameObject tabs in tabelas)
        {
            tabs.SetActive(false);
        }
        tabelaAtiva.SetActive(true);
        botaoTabelaAtivada.Select();
        foreach (Button button in botoestabela)
        {
            //button.enabled = false;
        }
    }

}
