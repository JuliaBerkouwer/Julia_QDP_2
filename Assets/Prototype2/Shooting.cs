using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public GameObject projectile;

    public float multiplierForce;
    public KeyCode shootKey = KeyCode.Space;
    public Vector2 velocity;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    public Vector2 cooldownRange;
    public Vector2 forceRange;
    public Color[] bulletColors;

    private Vector2 range;
    private bool canShoot = true;
    private float cooldown = 1f;
    private Vector2 hueValues;

    private void Start()
    {
        float hue;
        float saturation;
        float value;

        hueValues = new Vector2();

        Color.RGBToHSV(bulletColors[0], out hue, out saturation, out value);
        hueValues.x = hue;
        Color.RGBToHSV(bulletColors[1], out hue, out saturation, out value);
        hueValues.y = hue;

        range.y = GameObject.Find("Ground").transform.localScale.x / 2f;
    }

    public void Shoot(Vector2 direction)
    {
        if (Input.GetKeyDown(shootKey) && canShoot)
        {
            Vector2 position = (Vector2)transform.position + (offset * direction) * transform.localScale.x;
            GameObject go = (GameObject)Instantiate(projectile, position, Quaternion.identity);

            float stepValue = Remap(Mathf.Abs(transform.position.x), range.x, range.y, 0, 1);
            float hueValue = Mathf.Lerp(hueValues.x, hueValues.y, stepValue);
            Color bulletColor = Color.HSVToRGB(hueValue, 1, 1);
            go.GetComponent<SpriteRenderer>().color = bulletColor;

            float force = Remap(Mathf.Abs(transform.position.x), range.x, range.y, forceRange.x, forceRange.y);
            Vector2 forceVector = force * direction * multiplierForce;
            go.GetComponent<Bullet>().velocity = forceVector;

            StartCoroutine(CanShoot());
        }
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        cooldown = Remap(Mathf.Abs(transform.position.x), range.x, range.y, cooldownRange.x, cooldownRange.y);
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    public float Remap(float input, float minOld, float maxOld, float minNew, float maxNew)
    {
        return (input - minOld) / (maxOld - minOld) * (maxNew - minNew) + minNew;
    }
}