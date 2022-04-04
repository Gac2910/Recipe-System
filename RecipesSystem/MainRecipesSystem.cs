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
    public partial class MainRecipesSystem : Form
    {
        public MainRecipesSystem()
        {
            InitializeComponent();
        }

        private int selectedID;
        private string activeTable;

        // these 2 functions set and reset the value of selectedID
        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectedID = int.Parse(DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) => selectedID = 0;

        // load recipes table by default
        private void MainRecipesSystem_Load(object sender, EventArgs e) => recipesToolStripMenuItem_Click(sender, e);

        // this switch is used to "refresh" the active table after doing an edit to it
        private void RefreshActiveTable(object sender, EventArgs e)
        {
            switch (activeTable)
            {
                case "Recipes":
                    recipesToolStripMenuItem_Click(sender, e);
                    break;
                case "Categories":
                    categoriesToolStripMenuItem_Click(sender, e);
                    break;
                case "Ingredients":
                    ingredientsToolStripMenuItem_Click(sender, e);
                    break;
                case "RecipesIngredients":
                    recipeIngredientsToolStripMenuItem_Click(sender, e);
                    break;
            }
        }

        // -------------------- Menu Strip Buttons For "TABLE SELECT" --------------------
        private void recipesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayTable(SqlQueries.GetTable("SELECT * FROM Recipes"));
            activeTable = "Recipes";
        }
        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayTable(SqlQueries.GetTable("SELECT * FROM Categories"));
            activeTable = "Categories";
        }
        private void ingredientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayTable(SqlQueries.GetTable("SELECT * FROM Ingredients"));
            activeTable = "Ingredients";
        }
        private void recipeIngredientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayTable(SqlQueries.GetTable("SELECT * FROM RecipesIngredients"));
            activeTable = "RecipesIngredients";
        }
        private void DisplayTable(DataTable table)
        {
            DataGridView1.DataSource = table;
            selectedID = 0;
        }

        // -------------------- Menu Strip Buttons For "OPTIONS" --------------------
        private void closeToolStripMenuItem_Click(object sender, EventArgs e) => Close();
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlQueries.InsertRecord(activeTable);
            RefreshActiveTable(sender, e);
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlQueries.DeleteRecord(activeTable, selectedID);
            RefreshActiveTable(sender, e);
        }
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlQueries.UpdateRecord(activeTable, selectedID, DataGridView1);
            RefreshActiveTable(sender, e);
        }

        // -------------------- Menu Strip Buttons For "SEARCH BY" --------------------
        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Enter name of recipe to search for.", "Search Recipes");
            if (string.IsNullOrEmpty(name)) return;
            DisplayTable(SqlQueries.GetTable($"SELECT * FROM Recipes WHERE Name LIKE '%{name}%';"));
        }
        private void difficultyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string difficulty = Prompt.ShowDialog("Enter difficulty of recipe to search for.", "Search Recipes");
            if (string.IsNullOrEmpty(difficulty)) return;
            DisplayTable(SqlQueries.GetTable($"SELECT * FROM Recipes WHERE Difficulty LIKE '%{difficulty}%';"));
        }
        private void descriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string description = Prompt.ShowDialog("Enter description of recipe to search for.", "Search Recipes");
            if (string.IsNullOrEmpty(description)) return;
            DisplayTable(SqlQueries.GetTable($"SELECT * FROM Recipes WHERE Description LIKE '%{description}%';"));
        }

        // -------------------- Menu Strip Buttons For "Reports" --------------------
        private void allRecipesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports form = new Reports();
            form.ShowDialog();
        }
    }
}