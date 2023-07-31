﻿using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RP.SOI.DotNet.Utils;

public static class DBUtl
{
    /* DB_SQL property to return the final SQL string
       after substitutions to assist debugging 2022 PK */
    public static string DB_Message { get; set; } = "";
    public static string DB_SQL { get; set; } = "";

    static readonly string? env =
       Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static readonly IConfiguration config =
       new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .AddJsonFile($"appsettings.{env}.json", optional: true)
          .Build()
          .GetSection("ConnectionStrings");

    private static readonly string DB_CONNECTION = config.GetValue<string>("MyDbConnection") ?? "";

    public static List<dynamic> GetList(string sql, params object[] list)
    {
        return GetTable(sql, list).ToDynamic();
    }

    public static List<ModelClass> GetList<ModelClass>(string sql, params object[] list)
    {
        return GetTable(sql, list).ToStatic<ModelClass>();
    }

    private static List<DTO> ToStatic<DTO>(this DataTable dt)
    {
        var list = new List<DTO>();
        foreach (DataRow row in dt.Rows)
        {
            DTO obj = (DTO)Activator.CreateInstance(typeof(DTO))!;
            foreach (DataColumn column in dt.Columns)
            {
                PropertyInfo? Prop = obj.GetType().GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance);
                if (row[column] == DBNull.Value)
                    Prop?.SetValue(obj, null);
                else
                {
                    //Debug.WriteLine(row[column].GetType() + " " + Prop?.PropertyType); 
                    if (row[column].GetType() == Prop?.PropertyType)
                        Prop?.SetValue(obj, row[column]);
                }
            }
            list.Add(obj);
        }
        return list;
    }

    private static List<dynamic> ToDynamic(this DataTable dt)
    {
        var dynamicDt = new List<dynamic>();
        foreach (DataRow row in dt.Rows)
        {
            dynamic dyn = new ExpandoObject();
            foreach (DataColumn column in dt.Columns)
            {
                var dic = (IDictionary<string, object>)dyn;
                dic[column.ColumnName] = row[column];
            }
            dynamicDt.Add(dyn);
        }
        return dynamicDt;
    }

    public static DataTable GetTable(string sql, params object?[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] is string parameter)
            {
                list[i] = parameter?.EscQuote(); // Prevent SQL injection
            }
        }

        DB_Message = "";
        DB_SQL = string.Format(sql, list);
        DataTable dt = new();
        using MySqlConnection dbConn = new(DB_CONNECTION);
        using MySqlDataAdapter dAdptr = new(DB_SQL, dbConn);

        try
        {
            dAdptr.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            DB_Message = ex.Message;
            return dt;
        }
    }

    public static int ExecSQL(string sql, params object?[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] is string parameter)
            {
                list[i] = parameter?.EscQuote(); // Prevent SQL injection
            }
        }

        DB_Message = "";
        DB_SQL = string.Format(sql, list);

        int rowsAffected = 0;
        using (MySqlConnection dbConn = new(DB_CONNECTION))
        using (MySqlCommand command = new MySqlCommand(sql, dbConn))
        {
            try
            {
                dbConn.Open();
                command.CommandText = DB_SQL;
                rowsAffected = command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                DB_Message = ex.Message;
                rowsAffected = -1;
            }
        }
        return rowsAffected;
    }

    // Following line is the expression body equivalent of EscQuote. Either may be used.
    //public static string EscQuote(this string line) => line?.Replace("\"", "\"\"")!;

    public static string EscQuote(this string line)
    {
        return line?.Replace("'", "''")!;
    }

}