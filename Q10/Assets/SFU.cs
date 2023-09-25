using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFU : MonoBehaviour
{
    public GameObject Stadium;
    public GameObject SUB;
    public GameObject Library;
    public GameObject ASB;
    public GameObject BusLoop;
    public GameObject FitnessCentre;
    public GameObject AQPond;
    public GameObject TimHortons;
    public GameObject Residences;
    public GameObject ParkingLot;

    private GameObject[] Points = new GameObject[10];
    private LineRenderer lRStadium;
    private LineRenderer lRSUB;
    private LineRenderer lRLibrary;
    private LineRenderer lRASB;
    private LineRenderer lRBusLoop;
    private LineRenderer lRFitnessCentre;
    private LineRenderer lRAQPond;
    private LineRenderer lRTimHortons;
    private LineRenderer lRResidences;
    private LineRenderer lRParkingLot;

    private LineRenderer[] lRArr;

    // Start is called before the first frame update
    void Start()
    {
        Points[0] = Stadium;
        Points[1] = SUB;
        Points[2] = Library;
        Points[3] = ASB;
        Points[4] = BusLoop;
        Points[5] = FitnessCentre;
        Points[6] = AQPond;
        Points[7] = TimHortons;
        Points[8] = Residences;
        Points[9] = ParkingLot;

        // Initialize the lRArr array with the correct size
        lRArr = new LineRenderer[Points.Length];

        // Initialize individual LineRenderer variables
        lRStadium = Stadium.GetComponent<LineRenderer>();
        lRSUB = SUB.GetComponent<LineRenderer>();
        lRLibrary = Library.GetComponent<LineRenderer>();
        lRASB = ASB.GetComponent<LineRenderer>();
        lRBusLoop = BusLoop.GetComponent<LineRenderer>();
        lRFitnessCentre = FitnessCentre.GetComponent<LineRenderer>();
        lRAQPond = AQPond.GetComponent<LineRenderer>();
        lRTimHortons = TimHortons.GetComponent<LineRenderer>();
        lRResidences = Residences.GetComponent<LineRenderer>();
        lRParkingLot = ParkingLot.GetComponent<LineRenderer>();

        // Assign the individual LineRenderer components to the lRArr array
        lRArr[0] = lRStadium;
        lRArr[1] = lRSUB;
        lRArr[2] = lRLibrary;
        lRArr[3] = lRASB;
        lRArr[4] = lRBusLoop;
        lRArr[5] = lRFitnessCentre;
        lRArr[6] = lRAQPond;
        lRArr[7] = lRTimHortons;
        lRArr[8] = lRResidences;
        lRArr[9] = lRParkingLot;

        // Set up the LineRenderer properties
        for (int i = 0; i < Points.Length - 1; i++)
        {
            lRArr[i].positionCount = 2;
            lRArr[i].SetPosition(0, new Vector3(Points[i].transform.position.x, Points[i].transform.position.y + 1f, Points[i].transform.position.z));
            lRArr[i].SetPosition(1, new Vector3(Points[i + 1].transform.position.x, Points[i + 1].transform.position.y + 1f, Points[i + 1].transform.position.z));
            lRArr[i].SetWidth(0.5f, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ColourChange(int node)
    {
        if (node < 0 || node >= Points.Length)
        {
            Debug.LogError("Invalid node index.");
            return;
        }
        Debug.Log($"node is {node}");

        Color colour = new Color(Random.value, Random.value, Random.value);

        lRArr[node].material.color = colour;
        if (node != 0) lRArr[node-1].material.color = colour;

    }
}
