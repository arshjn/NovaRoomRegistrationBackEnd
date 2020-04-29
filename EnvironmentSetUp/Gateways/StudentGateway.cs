using EnvironmentSetUp.Models;
using System;
using System.Collections.Generic;
using MySql.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace EnvironmentSetUp.Gateways
{
    public class StudentGateway
    {
        public Student GetStudent(int Id)
        {
            string SQLQuery = "Select * from Student where NNumber = " + Id;
            Student output = new Student();
            using (MySqlConnection connection = new MySqlConnection(SQL_String.GetRDSConnectionString()))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQLQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            output.FirstName = reader["FirstName"].ToString();
                            output.LastName = reader["LastName"].ToString();
                            output.RazorsEdge = Convert.ToBoolean(reader["RazorsEdge"]);
                            output.Athlete = Convert.ToBoolean(reader["Athlete"]);
                            output.Sex = reader["Sex"].ToString();
                            output.Level = reader["Level"].ToString();
                            output.NNumber = Id;
                        }
                    }
                }

            }
            return output;
            /*
            using (var ctx = new NovaRoomRegistrationTestEntities())
            {
                
                try
                {
                    return ctx.Student.SqlQuery("Select * from Student where NNumber = " + Id).FirstOrDefault();
                }
                catch (Exception e)
                {
                    Student se = new Student();
                    se.FirstName = Convert.ToString(e);
                    return (se);
                }
            }*/
        }

        public List<BuildingFloor> GetBuildingFloors (int Id)
        {
            const string fm = "Freshman", sm = "Sophomore";

            Student student = GetStudent(Id);
            List < BuildingFloor > list = new List<BuildingFloor>();
            BuildingFloor bf = new BuildingFloor();  //Container [will be cloned and added to list]
            //Checking for Valid Student
            if (student.NNumber == 0)
                return null;

            switch (student.Level)
            {
                case fm:
                    if (student.RazorsEdge == false)
                    {
                        //Goodwin
                        bf.BuildingName = "GDW";
                        bf.FloorNum = 1;
                        list.Add(bf);
                        
                        if (student.Athlete == true)
                        {
                            bf = new BuildingFloor();
                            bf.BuildingName = "GDW";
                            bf.FloorNum = 2;
                            list.Add(bf);
                            return list;
                        }
                        bf = new BuildingFloor();
                        bf.BuildingName = "GDW";
                        bf.FloorNum = 3;
                        list.Add(bf);
                        /*bf = new BuildingFloor();
                        bf.BuildingName = "GDW";
                        bf.FloorNum = 4;
                        list.Add(bf);*/
                        
                        //Commons
                        for (int i = 1; i <= 2; i++)
                        {
                            //if (i != 4)
                            {
                                bf = new BuildingFloor();
                                bf.BuildingName = "COM";
                                bf.FloorNum = i;
                                list.Add(bf);
                            }
                        }
                    }
                    else if (student.RazorsEdge == true)
                    {
                        bf.BuildingName = "COM";
                        bf.FloorNum = 3;
                        list.Add(bf);
                    }
                    break;
                case sm:
                    if (student.RazorsEdge == true)
                    {
                        bf.BuildingName = "COM";
                        bf.FloorNum = 3;
                        list.Add(bf);
                    }
                    else if (student.RazorsEdge == false)
                    {
                        if (student.Athlete == true)
                        {
                            bf.BuildingName = "CLC";
                            bf.FloorNum = 2;
                            list.Add(bf);
                            return list;
                        }
                            //CLC
                            for (int i = 1; i <= 3;i++)
                        {
                            if (i == 2)
                                continue;
                            bf = new BuildingFloor();
                            bf.BuildingName = "CLC";
                            bf.FloorNum = i;
                            list.Add(bf);
                        }
                        //Founders
                        for (int i = 1; i <= 3; i++)
                        {

                            bf = new BuildingFloor();
                            bf.BuildingName = "FDR";
                            bf.FloorNum = i;
                            list.Add(bf);
                        }
                        //Farquar
                        for (int i = 1; i <= 3; i++)
                        {

                            bf = new BuildingFloor();
                            bf.BuildingName = "FAR";
                            bf.FloorNum = i;
                            list.Add(bf);
                        }
                        //Vettel
                        for (int i = 1; i <= 3; i++)
                        {

                            bf = new BuildingFloor();
                            bf.BuildingName = "VET";
                            bf.FloorNum = i;
                            list.Add(bf);
                        }
                    }
                    break;
                
                default:
                    //Junior or Senior
                    if (student.RazorsEdge == true)
                    {
                        bf.BuildingName = "MKH";
                        bf.FloorNum = 1;
                        list.Add(bf);
                        bf = new BuildingFloor();
                        bf.BuildingName = "MKH";
                        bf.FloorNum = 2;
                        list.Add(bf);
                    }
                    else if (student.RazorsEdge == false)
                    {
                        if (student.Athlete == true)
                        {
                            bf.BuildingName = "MKH";
                            bf.FloorNum = 2;
                            list.Add(bf);
                            return list;
                        }
                        //Mako Hall
                        
                            bf = new BuildingFloor();
                            bf.BuildingName = "MKH";
                            bf.FloorNum = 3;
                            list.Add(bf);

                        //Rolling Hills A
                        for (int i = 1; i <= 3; i++)
                        {

                            bf = new BuildingFloor();
                            bf.BuildingName = "RHA";
                            bf.FloorNum = i;
                            list.Add(bf);
                        }
                        //Rolling Hills C
                        for (int i = 1; i <= 3; i++)
                        {

                            bf = new BuildingFloor();
                            bf.BuildingName = "RHC";
                            bf.FloorNum = i;
                            list.Add(bf);
                        }
                    }
                    break;
            }

            return list;
        }

    }


}