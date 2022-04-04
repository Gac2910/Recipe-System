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
    public partial class RecipesIngredientsInput : Form
    {
        // one constructor without parameters for the Insert operation
        public RecipesIngredientsInput()
        {
            InitializeComponent();
        }
        // another constructor with parameters for the Update operation
        public RecipesIngredientsInput(int fk_RecipeID, int fk_IngredientID, int quantity)
        {
            InitializeComponent();
            this.fk_RecipeID = fk_RecipeID;
            this.fk_IngredientID = fk_IngredientID;
            this.quantity = quantity;
        }

        // these public fields are used in the SqlQueries class
        public int fk_RecipeID, fk_IngredientID, quantity;
        private void BtnCancel_Click(object sender, EventArgs e) => Close();

        private void BtnOk_Click(object sender, EventArgs e)
        {
            fk_RecipeID = int.Parse(TxtBoxRecipeID.Text);
            fk_IngredientID = int.Parse(TxtBoxIngredientID.Text);
            quantity = int.Parse(TxtBoxQuantity.Text);
        }
        public void FillInputFields()
        {
            TxtBoxRecipeID.Text = fk_RecipeID.ToString();
            TxtBoxIngredientID.Text = fk_IngredientID.ToString();
            TxtBoxQuantity.Text = quantity.ToString();
        }
    }
}
