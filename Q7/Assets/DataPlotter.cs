using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.LinearAlgebra;

public class DataPlotter : MonoBehaviour
{

    // Name of the input file, no extension
    public string inputfile;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    // Indices for columns to be assigned
    public int columnX = 3;
    public int columnY = 6;
    public int columnZ = 15;

    // Full column names
    public string xName;
    public string yName;
    public string zName;

    // The prefab for the data points that will be instantiated
    public GameObject PointPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject PointHolder;
    private LineRenderer lineRenderer;

    private GameObject[] calPoints = new GameObject[77];
    private GameObject[] sodPoints = new GameObject[77];
    private GameObject[] ratPoints = new GameObject[77];
    public LineRenderer lineRendererCalSod;
    public LineRenderer lineRendererCalRat;
    public LineRenderer lineRendererSodRat;



    // Use this for initialization
    void Start()
    {

        // Set pointlist to results of function Reader with argument inputfile
        pointList = CSVReader.Read(inputfile);

        //Log to console
        List<object> calories = pointList.Select(x => x["calories"]).ToList();
        List<object> sodium = pointList.Select(x => x["sodium"]).ToList();
        List<object> rating = pointList.Select(x => x["rating"]).ToList();
        List<int> caloriesAsInt = new List<int>();
        List<int> sodiumAsInt = new List<int>();
        List<int> ratingAsInt = new List<int>();


       


        foreach (var obj in calories)
        {
            // Cast each object element to an integer
            int intValue = Convert.ToInt32(obj);

            // Add the integer to the new list
            caloriesAsInt.Add(intValue);
        }

        foreach (var obj in sodium)
        {
            // Cast each object element to an integer
            int intValue = Convert.ToInt32(obj);

            // Add the integer to the new list
            sodiumAsInt.Add(intValue);
        }



        foreach (var obj in rating)
        {
            // Cast each object element to an integer
            int intValue = Convert.ToInt32(obj);

            // Add the integer to the new list
            ratingAsInt.Add(intValue);
        }

       

        int[] caloriesArray = caloriesAsInt.ToArray();
        int[] sodiumArray = sodiumAsInt.ToArray();
        int[] ratingArray = ratingAsInt.ToArray();

        // Create a 2D array with the three data columns
        int[][] data = { caloriesArray, sodiumArray, ratingArray };

        // Compute the covariance matrix using MathNet.Numerics.LinearAlgebra
        //Debug.Log($"calories: {caloriesAsInt.Count}, sodium: {ratingAsInt.Count}");

        List<string> columnList = new List<string>(pointList[1].Keys);

        List<double> caloriesAsDouble = caloriesAsInt.Select(Convert.ToDouble).ToList();
        List<double> sodiumAsDouble = sodiumAsInt.Select(Convert.ToDouble).ToList();
        List<double> ratingAsDouble = ratingAsInt.Select(Convert.ToDouble).ToList();
        IEnumerable<double> caloriesEnum = caloriesAsDouble;
        IEnumerable<double> sodiumEnum = sodiumAsDouble;
        IEnumerable<double> ratingEnum = ratingAsDouble;


        var meanCal = caloriesEnum.Mean();
        var varCal = caloriesEnum.Variance();
        var meanSod = sodiumEnum.Mean();
        var varSod = sodiumEnum.Variance();
        var meanRat = ratingEnum.Mean();
        var varRat = ratingEnum.Variance();


        double corrCalSod = Correlation.Pearson(caloriesAsDouble, sodiumAsDouble);
        //Debug.Log("Correlation between calories and sodium: " + corrCalSod);

        double corrCalRat = Correlation.Pearson(caloriesAsDouble, ratingAsDouble);
        //Debug.Log("Correlation between calories and rating: " + corrCalRat);

        double corrSodRat = Correlation.Pearson(sodiumAsDouble, ratingAsDouble);
        //Debug.Log("Correlation between sodium and rating: " + corrSodRat);


        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];


        var arr = covarianceMatrix(caloriesAsInt, sodiumAsInt, ratingAsInt);
      

        //Loop through Pointlist
        for (var i = 0; i < pointList.Count; i++)
        {
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i][xName]);
            float y = System.Convert.ToSingle(pointList[i][yName]);
            float z = System.Convert.ToSingle(pointList[i][zName]);

            //instantiate the prefab with coordinates defined above
            Instantiate(PointPrefab, new Vector3(x, y, 0), Quaternion.identity);

            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                    PointPrefab,
                    new Vector3(x, y, 0),
                    Quaternion.identity);
            calPoints[i] = dataPoint;

            // Make child of PointHolder object, to keep points within container in hierarchy
            dataPoint.transform.parent = PointHolder.transform;

            Renderer render = dataPoint.GetComponent<Renderer>();
            render.material.color = Color.red;

            // Assigns original values to dataPointName
            string dataPointName =
                pointList[i][xName] + " "
                + pointList[i][yName] + " "
                + pointList[i][zName];

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;


        }

        for (var i = 0; i < pointList.Count; i++)
        {
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i][xName]);
            float y = System.Convert.ToSingle(pointList[i][zName]);
            //float z = System.Convert.ToSingle(pointList[i][zName]);

            //instantiate the prefab with coordinates defined above
            Instantiate(PointPrefab, new Vector3(x, y, 0), Quaternion.identity);

            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                    PointPrefab,
                    new Vector3(x, y, 0),
                    Quaternion.identity);
            sodPoints[i] = dataPoint;  

            // Make child of PointHolder object, to keep points within container in hierarchy
            dataPoint.transform.parent = PointHolder.transform;
            Renderer render = dataPoint.GetComponent<Renderer>();
            render.material.color = Color.blue;

            // Assigns original values to dataPointName
            string dataPointName =
                pointList[i][xName] + " "
                + pointList[i][yName] + " "
                + pointList[i][zName];

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;


        }

        for (var i = 0; i < pointList.Count; i++)
        {
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i][yName]);
            float y = System.Convert.ToSingle(pointList[i][zName]);
            //float z = System.Convert.ToSingle(pointList[i][zName]);

            //instantiate the prefab with coordinates defined above
            Instantiate(PointPrefab, new Vector3(x, y, 0), Quaternion.identity);

            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                    PointPrefab,
                    new Vector3(x, y, 0),
                    Quaternion.identity);
            ratPoints[i] = dataPoint;

            // Make child of PointHolder object, to keep points within container in hierarchy
            dataPoint.transform.parent = PointHolder.transform;
            Renderer render = dataPoint.GetComponent<Renderer>();
            render.material.color = Color.green;

            // Assigns original values to dataPointName
            string dataPointName =
                pointList[i][xName] + " "
                + pointList[i][yName] + " "
                + pointList[i][zName];

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;


        }

        // Set the number of positions in the LineRenderer to draw the regression line
        var slopeCalSod = corrCalSod * (meanSod / meanCal);

        var interceptCalSod = meanSod - slopeCalSod * meanCal;
        // Calculate the y values for the regression line based on the computed slope and intercept
        float minYCalSod = (float)(caloriesAsDouble.Min() * slopeCalSod + interceptCalSod);
        float maxYCalSod = (float)(caloriesAsDouble.Maximum() * slopeCalSod + interceptCalSod);

        // Set the positions for the regression line
        float minXCalSod = calPoints[0].transform.position.x;
        float maxXCalSod = calPoints[0].transform.position.x;

        for (int i = 1; i < calPoints.Length; i++)
        {
            float x = calPoints[i].transform.position.x;
            minXCalSod = Mathf.Min(minXCalSod, x);
            maxXCalSod = Mathf.Max(maxXCalSod, x);
        }

        lineRendererCalSod = transform.GetChild(0).GetComponent<LineRenderer>(); // Set the reference to the first LineRenderer
        lineRendererCalSod.positionCount = 2;
        lineRendererCalSod.SetPosition(0, new Vector3(minXCalSod, minYCalSod, 0f));
        lineRendererCalSod.SetPosition(1, new Vector3(maxXCalSod, maxYCalSod, 0f));


        var slopeCalRat = corrCalRat * (meanRat / meanCal);

        var interceptCalRat = meanRat - slopeCalRat * meanCal;
        // Calculate the y values for the regression line based on the computed slope and intercept
        float minYCalRat = (float)(caloriesAsDouble.Min() * slopeCalRat + interceptCalRat);
        float maxYCalRat = (float)(caloriesAsDouble.Maximum() * slopeCalRat + interceptCalRat);

        // Set the positions for the regression line
        float minXCalRat = calPoints[0].transform.position.x;
        float maxXCalRat = calPoints[0].transform.position.x;

        for (int i = 1; i < calPoints.Length; i++)
        {
            float x = calPoints[i].transform.position.x;
            minXCalRat = Mathf.Min(minXCalRat, x);
            maxXCalRat = Mathf.Max(maxXCalRat, x);
        }

        lineRendererCalRat = transform.GetChild(1).GetComponent<LineRenderer>(); // Set the reference to the second LineRenderer
        lineRendererCalRat.positionCount = 2;
        lineRendererCalRat.SetPosition(0, new Vector3(minXCalRat, minYCalRat, 0f));
        lineRendererCalRat.SetPosition(1, new Vector3(maxXCalRat, maxYCalRat, 0f));



        var slopeSodRat = corrSodRat * (meanRat / meanSod);

        var interceptSodRat = meanRat - slopeSodRat * meanSod;
        // Calculate the y values for the regression line based on the computed slope and intercept
        float minYSodRat = (float)(sodiumAsDouble.Min() * slopeSodRat + interceptSodRat);
        float maxYSodRat = (float)(sodiumAsDouble.Maximum() * slopeSodRat + interceptSodRat);

        // Set the positions for the regression line
        float minXSodRat = ratPoints[0].transform.position.x;
        float maxXSodRat = ratPoints[0].transform.position.x;

        for (int i = 1; i < sodPoints.Length; i++)
        {
            float x = ratPoints[i].transform.position.x;
            minXSodRat = Mathf.Min(minXSodRat, x);
            maxXSodRat = Mathf.Max(maxXSodRat, x);
        }

        lineRendererSodRat = transform.GetChild(2).GetComponent<LineRenderer>(); // Set the reference to the second LineRenderer
        lineRendererSodRat.positionCount = 2;
        lineRendererSodRat.SetPosition(0, new Vector3(minXSodRat, minYSodRat, 0f));
        lineRendererSodRat.SetPosition(1, new Vector3(maxXSodRat, maxYSodRat, 0f));

    }



    public double[,] covarianceMatrix(List<int> calories, List<int> sodium, List<int> ratings)
    {
        //Calculate covariance matrix using covariance formula
        double[,] covMat = new double[3, 3];
        double n = Convert.ToDouble(calories.Count);
        double calAvg = Convert.ToDouble(calories.Average());
        double sodAvg = Convert.ToDouble(sodium.Average());
        double ratAvg = Convert.ToDouble(ratings.Average());
        
        covMat[0,0] = calories.Average(v => Math.Pow(v - calAvg, 2));
        covMat[1, 1] = sodium.Average(v => Math.Pow(v - sodAvg, 2));
        covMat[2, 2] = ratings.Average(v => Math.Pow(v - ratAvg, 2));



        for (int i = 0; i < calories.Count; i++)
        {
            covMat[0, 1] += (calories[i] - calAvg) * (sodium[i] - sodAvg);
            covMat[0,2] += (calories[i] - calAvg) * (ratings[i] - ratAvg);
            covMat[1, 2] += (sodium[i] - sodAvg) * (ratings[i] - ratAvg);
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                covMat[i,j] /= n;
            }
        }
        covMat[1, 0] = covMat[0, 1];
        covMat[2, 0] = covMat[0, 2];
        covMat[2,1] = covMat[1,2];


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Debug.Log($"{i},{j} : {covMat[i, j]}"); 
            }
        }

        return covMat;
    }



}