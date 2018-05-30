using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHUANG
{
    public partial class Form2 : Form
    {
        public Form1 f1;

        private int[] hRad =  new int[12];
        private int[] dRad =  new int[12];
        private double lat;
        private double dip;

        private int[] result = new int[12];
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 f):this()
        {
            f1 = f;
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible == true && DialogResult.No == MessageBox.Show("您确定要退出吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
            }
        }
        private void Form2_OnFormClosed(object sender, FormClosedEventArgs e)
        {
             Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            f1.f3.Show();
        }
        //初始表创建函数
        private void DataCreate()
        {
            
            DataTable dt = new DataTable();  

            DataColumn dc0 = new DataColumn("INPUT", Type.GetType("System.String"), "");
            dt.Columns.Add(dc0);
            for (int i = 0; i < 12; i ++ )
            {
                DataColumn dc = new DataColumn("Mon" + (i+1), Type.GetType("System.Int32"), "");
                dt.Columns.Add(dc);
            }   
             DataRow dr1 = dt.NewRow();//在dt中新建一行 
             dr1["INPUT"] = "水平总辐射";

             DataRow dr2 = dt.NewRow();//在dt中新建一行 
             dr2["INPUT"] = "散辐射";

             for (int i = 0; i < 12; i++)
             {
                 dr1["Mon" + (i + 1)] = 0;
                 dr2["Mon" + (i + 1)] = 0;
             }
             dt.Rows.Add(dr1);//向dt中添加一行  
             dt.Rows.Add(dr2);//向dt中添加一行  


             this.dataGridView1.DataSource = dt;
            for(int i = 1;i <=12;i ++)
            {
                dataGridView1.Columns[i].Width = 50;
            }
            
        }
        //读取输入函数
        private void DataRead()
        {
            DataTable dt = (DataTable)this.dataGridView1.DataSource;
            for(int i = 0; i < 12;i ++)
            {
                hRad[i] = Convert.ToInt32(dt.Rows[0][i + 1]);
                dRad[i] = Convert.ToInt32(dt.Rows[1][i + 1]);
            }
            try
            {
              lat = Convert.ToDouble(this.textBox1.Text);
              dip = Convert.ToDouble(this.textBox2.Text);
            }
            catch(Exception e)
            {
                MessageBox.Show("FBI WARNING:输入错误","WARNING");
            }
        }
        //显示结果函数
        private void ShowResult()
        {
            DataSet ds = new DataSet("dsText");
            DataTable dt = new DataTable("tableText");
            ds.Tables.Add(dt);

            DataColumn dc0 = new DataColumn("OUTPUT", Type.GetType("System.String"), "");
            dt.Columns.Add(dc0);
            for (int i = 0; i < 12; i++)
            {
                DataColumn dc = new DataColumn("Mon" + (i + 1), Type.GetType("System.Int32"), "");
                dt.Columns.Add(dc);
            }
            DataRow dr1 = dt.NewRow();//在dt中新建一行 
            dr1["OUTPUT"] = "斜面辐射";
            for (int i = 0; i < 12;i ++ )
            {
                dr1["Mon" + (i + 1)] = result[i];
            }
                dt.Rows.Add(dr1);//向dt中添加一行  
  

            this.dataGridView2.DataSource = ds.Tables[0];

            for (int i = 1; i <= 12; i++)
            {
                dataGridView2.Columns[i].Width = 50;
            }
        }
        //计算过程
        private void Calculate()
        {
            for (int i = 0; i < 12; i++)
            {
                result[i] = 0;
            }
        }

       private void Form2_Load(object sender, EventArgs e)
       {
           DataCreate();    
       }

       private void Cal_Click(object sender, EventArgs e)
       {
           DataRead();
           Calculate();
           ShowResult();
       }
    }
}
