using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarSelectorUI : MonoBehaviour
{
    [SerializeField]
    private ModelManager _modelManager;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private float _padding = 10;

    // Todo: Reemplazar este sistema por un sistema de eventos que se hookee
    //       a la carga de los modelos desde su provider "ModelManager"
    void createClickeableItems()
    {
        int x = 0;
        int y = 0;

        foreach (Model model in _modelManager.Models)
        {
            GameObject modelItem = Instantiate(_prefab);
            RectTransform rect = (RectTransform)modelItem.transform;
            string thumbnail = Path.Combine(model.GetPath(), model.GetConfig().Files.Icon);

            modelItem.transform.SetParent(this.transform);

            float width = rect.rect.width;
            float height = rect.rect.height;

            float paddingX = ((x + 1) * _padding) + (x * width);
            float paddingY = ((y + 1) * _padding) + (y * height);

            modelItem.transform.localPosition = new Vector3(-paddingX, -paddingY, 0);
            modelItem.GetComponent<RawImage>().texture = IMG2Sprite.instance.LoadTexture(thumbnail);

            TextMeshProUGUI[] texts = modelItem.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts)
            {
                Debug.Log(text.text);
                text.text = text.text
                    .Replace("{name}", model.GetConfig().Name)
                    .Replace("{loader}", model.GetConfig().Loader)
                    .Replace("{name}", model.GetConfig().Version.ToString())
                ;
            }

            if (x == 2)
            {
                x = 0;
                y++;
            }
            else
            {
                x++;
            }

            Debug.Log(paddingX + " - " + paddingY);
        }
    }

    private bool _firstFrame = true;

    // Update is called once per frame
    void Update()
    {
        if (_firstFrame)
        {
            createClickeableItems();
            _firstFrame = false;
        }
    }
}
