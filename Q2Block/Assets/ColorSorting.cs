using System.Collections;
using UnityEngine;

public class ColorSorting : MonoBehaviour
{
    public GameObject blockPrefab;
    public float swapDuration;
    public float swapDelay;
    public float stackSpacing;

    private Color[] colors;
    private GameObject[] blocks;
    private bool isSorting;
    private int firstCol;
    private int secondCol;
    private int thirdCol;
    private int fourthCol;





    public void SetColors(int first, int second, int third, int fourth)
    {
        Debug.Log("Set Col");

        Debug.Log($" {firstCol}, {secondCol}, {thirdCol}, {fourthCol}");
        colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, new Color(1.0f, 0.64f, 0.0f), new Color(1.0f, 0.0f, 1.0f), new Color(1.0f, 0.76f, 0.8f) };
        firstCol = first;
        secondCol = second;   
        thirdCol = third;
        fourthCol = fourth;
    }
    public void Start()
    {
        Debug.Log("Color Sort Start");

        // Sort and stack the blocks based on the colors
    }
    public void StartSorting()
    {
        // Sort and stack the blocks based on the colors
        StartCoroutine(SortAndStackBlocks());
    }
    private IEnumerator SortAndStackBlocks()
    {
        isSorting = true;

        // Instantiate the blocks with colors
        blocks = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            GameObject block = Instantiate(blockPrefab, transform.position + Vector3.up * (i * stackSpacing), Quaternion.identity);
            blocks[i] = block;
        }
            Renderer renderer0 = blocks[0].GetComponent<Renderer>();
            renderer0.material.color = colors[firstCol];

            Renderer renderer1 = blocks[1].GetComponent<Renderer>();
            renderer1.material.color = colors[secondCol];

            Renderer renderer2 = blocks[2].GetComponent<Renderer>();
            renderer2.material.color = colors[thirdCol];

            Renderer renderer3 = blocks[3].GetComponent<Renderer>();
            renderer3.material.color = colors[fourthCol];

        int n = blocks.Length;
        while (isSorting)
        {
            isSorting = false;
            for (int i = 0; i < n - 1; i++)
            {
                // Compare the R (Red) value of adjacent blocks
                for (int j = 0; j < n - 1; j++)
                {
                    float r1 = blocks[j].GetComponent<Renderer>().material.color.r;
                    float r2 = blocks[j + 1].GetComponent<Renderer>().material.color.r;

                    if (r1 > r2)
                    {
                        Debug.Log($"{j}: {r1} > {j + 1}: {r2}");

                        // Swap the positions of the blocks
                        yield return StartCoroutine(SwapBlocks(blocks[j], blocks[j + 1]));
                        GameObject temp = blocks[j];
                        blocks[j] = blocks[j + 1];
                        blocks[j + 1] = temp;
                        isSorting = true;

                    }
                }
                if (isSorting == false)
                    break;
            }
            n--;
        }

        Debug.Log("Sorting complete!");
    }

    private IEnumerator SwapBlocks(GameObject blockA, GameObject blockB)
    {
        Rigidbody rbA = blockA.GetComponent<Rigidbody>();
        Rigidbody rbB = blockB.GetComponent<Rigidbody>();

        // Disable collisions during the swap
        Physics.IgnoreCollision(blockA.GetComponent<Collider>(), blockB.GetComponent<Collider>(), true);

        // Make the blocks kinematic
        rbA.isKinematic = true;
        rbB.isKinematic = true;

        Vector3 initialPosA = blockA.transform.position;
        Vector3 initialPosB = blockB.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < swapDuration)
        {
            float t = elapsedTime / swapDuration;
            rbA.MovePosition(Vector3.Lerp(initialPosA, initialPosB, t));
            rbB.MovePosition(Vector3.Lerp(initialPosB, initialPosA, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the final position for the blocks
        rbA.MovePosition(initialPosB);
        rbB.MovePosition(initialPosA);

        // Enable collisions after the swap
        Physics.IgnoreCollision(blockA.GetComponent<Collider>(), blockB.GetComponent<Collider>(), false);

        // Delay after each swap
        yield return new WaitForSeconds(swapDelay);

        // Make the blocks kinematic again
        rbA.isKinematic = true;
        rbB.isKinematic = true;
    }
}
