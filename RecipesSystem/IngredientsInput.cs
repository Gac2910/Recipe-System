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
    public partial class IngredientsInput : Form
    {
        // one constructor without parameters for the Insert operation
        public IngredientsInput()
        {
            InitializeComponent();
        }
        // another constructor with parameters for the Update operation
        public IngredientsInput(string name, int amountInWarehouse, string unitsOfMeasurement)
        {
            InitializeComponent();
            this.name = name;
            this.amountInWarehouse = amountInWarehouse;
            this.unitsOfMeasurement = unitsOfMeasurement;
        }

        // these public fields are used in the SqlQueries class
        public int amountInWarehouse;
        public string name, unitsOfMeasurement;
        private void BtnCancel_Click(object sender, EventArgs e) => Close();

        private void BtnOk_Click(object sender, EventArgs e)
        {
            name = TxtBoxName.Text;
            amountInWarehouse = int.Parse(TxtBoxWarehouse.Text);
            unitsOfMeasurement = TxtBoxMeasurement.Text;
        }
        public void FillInputFields()
        {
            TxtBoxName.Text = name;
            TxtBoxWarehouse.Text = amountInWarehouse.ToString();
            TxtBoxMeasurement.Text = unitsOfMeasurement;
        }

    }
}
