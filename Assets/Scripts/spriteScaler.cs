using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteScaler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprRend;
    private Vector2 cameraStartDimensions;
    private Vector2 spriteStartDimensions;

    // Start is called before the first frame update
    void Start()
    {
        // what are the start sprite dimensions
        spriteStartDimensions = new Vector2(sprRend.size.x, sprRend.size.y);

        UpdateSpritDimensions();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // if the dimensions change then update sprites
        if (cameraStartDimensions != new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight)) {
            UpdateSpritDimensions();
        }
    }

    public void UpdateSpritDimensions()
    {
        // update sprites dimensions
        // how much bigger is the width than the height
        float widthScale = Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
        // set new width assuming height never changes and is correct
        sprRend.size = new Vector2(spriteStartDimensions.y * widthScale, spriteStartDimensions.y);

        // update current dimensions
        // what are the start camera dimensions
        cameraStartDimensions = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
    }
}
