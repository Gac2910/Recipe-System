using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipesSystem
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            DataTable table = SqlQueries.GetTable("SELECT Name, Description, Difficulty, Price FROM Recipes;");
            DataGridViewReport.DataSource = table;
            SqlQueries.DisplayReport(DataGridViewReport);
        }
    }
}
