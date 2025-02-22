using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CarregarHistoria : MonoBehaviour {

    string servidor = "";
    string proximo = "";
    bool historiaRolando = false;

    public string imagemPath = "imagem.png";
    public string textoPath = "texto.txt";
    public string proximoPath = "proximo.txt";

    public InputField serverInput;
    public Text texto;
    public Image imagem;

    public GameObject startScreen, endScreen;

    /*
        Nesse caso criei um formato de pastas para manter a progressão da história.
        A variavel 'servidor' contém o link base do host onde as pastas estão localizadas.
        Cada pasta possui uma imagem 'imagemPath', um texto 'textoPath' e um arquivo 'proximoPath' que contém o nome da próxima pasta.
        O método 'CarregarProximo' carrega os dados da próxima pasta e atualiza as variáveis 'imagem', 'texto' e 'proximo'.
        Se não houver um arquivo 'proximoPath' ou se o servidor não for informado, acaba a história.

        Ex:

        host:
            inicio/
                imagem.png
                texto.txt
                proximo.txt [contém '2']
            2/
                imagem.png
                texto.txt
                proximo.txt [contém '3']
            3/
                imagem.png
                texto.txt
    */

    void Start() {
        startScreen.SetActive(true);
        endScreen.SetActive(false);
    }

    public void CarregarProximo() {
        if (proximo == "" || servidor == "") {
            if (historiaRolando) FimHistoria();
            else VoltarInicio();
            return;
        }

        historiaRolando = true;

        string basePath = servidor + proximo + "/";
        string imagemUrl = basePath + imagemPath;
        string textoUrl = basePath + textoPath;
        string proximoUrl = basePath + proximoPath;

        StartCoroutine(CarregarImagem(imagemUrl));
        StartCoroutine(CarregarTexto(textoUrl));
        StartCoroutine(CarregarProximo(proximoUrl));
    }

    IEnumerator CarregarImagem(string path) {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(path);

        yield return imageRequest.SendWebRequest();
        if (imageRequest.result == UnityWebRequest.Result.Success) {
            Texture2D tex = ((DownloadHandlerTexture)imageRequest.downloadHandler).texture;
            Rect rect = new Rect(0,0,tex.width,tex.height);
            Vector2 center = new Vector2(tex.width / 2.0f, tex.height / 2.0f);
            Sprite sprite = Sprite.Create(tex, rect, center);
            imagem.sprite = sprite;
            imagem.color = Color.white;
        } else {
            Debug.Log("Erro ao carregar imagem: " + imageRequest.error);
            imagem.sprite = null;
            imagem.color = Color.clear;
        }
    }

    IEnumerator CarregarTexto(string path) {
        UnityWebRequest textRequest = UnityWebRequest.Get(path);

        yield return textRequest.SendWebRequest();
        if (textRequest.result == UnityWebRequest.Result.Success) {
            texto.text = textRequest.downloadHandler.text;
        } else {
            Debug.Log("Erro ao carregar texto: " + textRequest.error);
            texto.text = "";
        }
    }

    IEnumerator CarregarProximo(string path) {
        UnityWebRequest textRequest = UnityWebRequest.Get(path);

        yield return textRequest.SendWebRequest();
        if (textRequest.result == UnityWebRequest.Result.Success) {
            proximo = textRequest.downloadHandler.text;
        }
        else proximo = "";
    }

    public void SetarServidor() {
        servidor = serverInput.text;
        proximo = "inicio";

        if (!servidor.EndsWith("/")) servidor += "/";

        startScreen.SetActive(false);
        CarregarProximo();
    }

    public void VoltarInicio() {
        proximo = "";
        endScreen.SetActive(false);
        startScreen.SetActive(true);
    }

    public void FimHistoria() {
        endScreen.SetActive(true);
        historiaRolando = false;
    }
}
