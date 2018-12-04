﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class account_mypage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["islogin"].ToString() == "false")
        {
            Response.Redirect("~/account/login.aspx");
        }
        else
        {
            string connectionString = @"server=(local)\SQLExpress;Integrated Security=true;database=db_user";
            SqlConnection Con = new SqlConnection(connectionString);

            // SQL COMMAND OBJECT를 만들고  SQL COMMAND 넣기
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "SELECT name, id, email, pw, mileage" +
                " FROM db_user WHERE id = '" + Application["id"].ToString() + "'";

            try
            {


                Con.Open();
                // ExecuteReader()는 SELECT 문을 사용하여 RECORD를 가져올때 사용
                SqlDataReader reader = Cmd.ExecuteReader();

                // SqlDataReader의 기본 위치는 첫 번째 레코드 앞, Read( )를 호출하여 하나의 레코드 읽기 
                // read.Read( )의 반환 값 형식: Boolean, 행이 더 있으면 true이고, 그렇지 않으면 false입니다. 


                while (reader.Read())
                {
                    TextBox1.Text = reader["name"].ToString().Trim();
                    TextBox2.Text = reader["id"].ToString().Trim();
                    TextBox3.Text = reader["email"].ToString().Trim();
                    Label1.Text = reader["mileage"].ToString().Trim();
                }
                reader.Close();
                Con.Close();
            }
            catch { }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string connectionString = @"server=(local)\SQLExpress;Integrated Security=true;database=db_user";//서버 연결
        SqlConnection Con = new SqlConnection(connectionString);

        // SQL COMMAND OBJECT를 만들고  SQL COMMAND 넣기
        SqlCommand Cmd = new SqlCommand();
        Cmd.Connection = Con;
        //새 비밀번호를 DB에 업데이트
        Cmd.CommandText = "UPDATE  db_user SET  name= '" + TextBox1.Text + "' WHERE id = '" + Application["id"].ToString().Trim() + "'";

        try
        {
            Con.Open();

            // ExecuteNonQuery()문은 CREATE, ALTER, DROP, INSERT, UPDATE, DELETE 문을 수행할때 사용
            int rowsAffected = Cmd.ExecuteNonQuery();//편집구문에따라 영향받는 줄은 몇줄인가?


            if (rowsAffected == 1)//영향받는 줄은 1줄
            {
                //Response.Redirect("pwchanged.aspx");//비밀번호 변경후로 이동
            }

            Con.Close();
        }
        catch { }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Application["islogin"].ToString() == "false")
        {
            Response.Redirect("~/account/login.aspx");
        }
        else
        {

            string connectionString = @"server=(local)\SQLExpress;Integrated Security=true;database=db_user";
            SqlConnection Con = new SqlConnection(connectionString);

            // SQL COMMAND OBJECT를 만들고  SQL COMMAND 넣기
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "UPDATE  db_user SET email= '" + TextBox3.Text + "' WHERE email = '" + Application["email"].ToString().Trim() + "'";

            try
            {
                Con.Open();


                int rowsAffected = Cmd.ExecuteNonQuery();

                Con.Close();

                if (rowsAffected == 1)
                {
                    Response.Redirect("pwchanged.aspx");
                }

                Con.Close();
            }
            catch { }
        }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
       
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        string connectionString = @"server=(local)\SQLExpress;Integrated Security=true;database=db_user";
        SqlConnection Con = new SqlConnection(connectionString);
        // SPQ 선언

        // SQL COMMAND OBJECT를 만들고  SQL COMMAND 넣기
        SqlCommand Cmd = new SqlCommand();
        Cmd.Connection = Con;
        Con.Open();
        try
        {
            // SQL COMMAND 수행하기
            
            Cmd.CommandText = "DELETE FROM db_user WHERE id = \'" + Application["id"].ToString().Trim() + "\'";
            // 리턴 값은 영향을 받은 ROW의 갯수
            int rowsAffected = Cmd.ExecuteNonQuery();
            if (rowsAffected == 1)
            {
                Application["islogin"] = "false";
                Application["name"] = "";
                Application["id"] = "";
                Application["email"] = "";
                Response.Redirect("~/home.aspx");
            }
            Con.Close();
        }
        catch { }
    }
}