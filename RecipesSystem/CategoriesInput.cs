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
    public partial class CategoriesInput : Form
    {
        // one constructor without parameters for the Insert operation
        public CategoriesInput()
        {
            InitializeComponent();
        }
        // another constructor with parameters for the Update operation
        public CategoriesInput(string name, string description, string personInCharge)
        {
            InitializeComponent();
            this.name = name;
            this.description = description;
            this.personInCharge = personInCharge;
        }

        // these public fields are used in the SqlQueries class
        public string name, description, personInCharge;

        private void BtnCancel_Click(object sender, EventArgs e) => Close();

        private void BtnOk_Click(object sender, EventArgs e)
        {
            name = TxtBoxName.Text;
            description = TxtBoxDescription.Text;
            personInCharge = TxtBoxPersonInCharge.Text;
        }
        public void FillInputFields()
        {
            TxtBoxName.Text = name;
            TxtBoxDescription.Text = description;
            TxtBoxPersonInCharge.Text = personInCharge;
        }
    }
}
