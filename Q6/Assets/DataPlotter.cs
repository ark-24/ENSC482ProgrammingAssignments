using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathNet.Numerics; //https://gamedev.stackexchange.com/questions/182801/how-to-make-mathnet-numerics-work-with-unity
using MathNet.Numerics.Statistics;
using MathNet.Numerics.LinearAlgebra;
//inspiration: https://sites.psu.edu/bdssblog/2017/04/06/basic-data-visualization-in-unity-scatterplot-creation/
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
    private string xName;
    private string yName;
    private string zName;

    // The prefab for the data points that will be instantiated
    public GameObject PointPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject PointHolder;

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

      

        List<string> columnList = new List<string>(pointList[1].Keys);

        List<double> caloriesAsDouble = caloriesAsInt.Select(Convert.ToDouble).ToList();
        List<double> sodiumAsDouble = sodiumAsInt.Select(Convert.ToDouble).ToList();
        List<double> ratingAsDouble = ratingAsInt.Select(Convert.ToDouble).ToList();

        double corrCalSod = Correlation.Pearson(caloriesAsDouble, sodiumAsDouble);

        double corrCalRat = Correlation.Pearson(caloriesAsDouble, ratingAsDouble);

        double corrSodRat = Correlation.Pearson(sodiumAsDouble, ratingAsDouble);



        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];


        var arr = covarianceMatrix(caloriesAsInt, sodiumAsInt, ratingAsInt);
        // Compute the covariance matrix using MathNet.Numerics.LinearAlgebra
       

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

    }

    public double[,] covarianceMatrix(List<int> calories, List<int> sodium, List<int> ratings)
    {
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

        return covMat;
    }



}