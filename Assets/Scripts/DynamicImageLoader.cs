using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DynamicImageLoader : MonoBehaviour
{
    private const string LinkedInImageUrl = "https://media.licdn.com/dms/image/v2/D4E03AQEFpZFIaytRug/profile-displayphoto-shrink_800_800/profile-displayphoto-shrink_800_800/0/1709329525216?e=1734566400&v=beta&t=8DxGYvo7nfWOJWMpfdJwIMfbVpuDAlhcBg0PSuMqNlA";
    private Vector2 desiredSize = new Vector2(100, 100); // 期望的图片尺寸
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 禁用所有其他渲染器
        DisableOtherRenderers();

        // 获取或添加 SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        // 设置排序层级，确保在Tilemap之上
        spriteRenderer.sortingOrder = 10; // 可以根据需要调整这个值

        // 确保Tilemap的Z轴位置为0
        transform.position = new Vector3(transform.position.x, transform.position.y, -2); // 将Sprite的Z轴设置为-1

        StartCoroutine(LoadImageFromUrl());
    }

    void DisableOtherRenderers()
    {
        // 禁用所有其他类型的渲染器
        Renderer[] renderers = GetComponents<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (!(renderer is SpriteRenderer))
            {
                renderer.enabled = false;
            }
        }

        // 如果有子对象，也禁用它们的渲染器
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in childRenderers)
        {
            if (renderer.gameObject != this.gameObject)
            {
                renderer.enabled = false;
            }
        }
    }

    IEnumerator LoadImageFromUrl()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(LinkedInImageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D originalTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Texture2D resizedTexture = ResizeTexture(originalTexture, (int)desiredSize.x, (int)desiredSize.y);
            Sprite sprite = Sprite.Create(resizedTexture, new Rect(0, 0, resizedTexture.width, resizedTexture.height), new Vector2(0.5f, 0.5f));
            
            spriteRenderer.sprite = sprite;
            
            // 调整游戏对象的缩放以匹配新的图片尺寸
            float aspectRatio = (float)resizedTexture.width / resizedTexture.height;
            transform.localScale = new Vector3(aspectRatio, 1, 1);
        }
        else
        {
            Debug.LogError($"图片加载失败: {request.error}");
            Debug.LogError($"尝试加载的URL: {LinkedInImageUrl}");
            // 可以在这里添加一些错误处理逻辑，比如加载一个默认图片
        }
    }

    Texture2D ResizeTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }
}
