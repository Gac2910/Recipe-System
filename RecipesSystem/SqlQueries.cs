using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace RecipesSystem
{
	class SqlQueries
	{
		// school database
		private static SqlConnection sqlConnect = new SqlConnection("Data Source=ZCM-851704219\\SQLEXPRESS01;Initial Catalog=Restaurant10;Integrated Security=True");
		// home database
		//private static SqlConnection sqlConnect = new SqlConnection("Data Source=LAPTOP-UP6DGQ4Q\\SQLEXPRESS;Initial Catalog=Restaurant10HomeDB;Integrated Security=True");
		// ----------------------- GET TABLE -----------------------
		public static DataTable GetTable(string commandTxt)
		{
			try
			{
				sqlConnect.Open();
				SqlCommand command = sqlConnect.CreateCommand();
				command.CommandText = commandTxt;
				command.ExecuteNonQuery();
				DataTable table = new DataTable();
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				adapter.Fill(table);
				return table;
			}
			catch (SqlException ex)
			{
				MessageBox.Show(ex.Message, "Error Getting table", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
			finally
			{
				sqlConnect.Close();

			}
		}
		// ----------------------- INSERT RECORD -----------------------
		public static void InsertRecord(string activeTable)
		{
			// to try to reduce repetition, im using a dynamic variable and created an instance depending on the active table
			dynamic form;
			switch (activeTable)
			{
				case "Recipes":
					form = new RecipesInput();
					break;
				case "Categories":
					form = new CategoriesInput();
					break;
				case "Ingredients":
					form = new IngredientsInput();
					break;
				case "RecipesIngredients":
					form = new RecipesIngredientsInput();
					break;
				default: throw new ArgumentException("Argument Exception when creating form instance.");
			}
			DialogResult result = form.ShowDialog();
			if (result != DialogResult.OK) return;
			try
			{
				sqlConnect.Open();
				SqlCommand command = sqlConnect.CreateCommand();
				// this switch sets the command text depending on the active table
				switch (activeTable)
				{
					case "Recipes":
						command.CommandText = string.Format(
							"INSERT INTO Recipes " +
							"VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}');",
							form.categoryID,
							form.name,
							form.description,
							form.difficulty,
							form.price,
							form.image);
						break;
					case "Categories":
						command.CommandText = string.Format(
							"INSERT INTO Categories " +
							"VALUES('{0}', '{1}', '{2}');",
							form.name,
							form.description,
							form.personInCharge);
						break;
					case "Ingredients":
						command.CommandText = string.Format(
							"INSERT INTO Ingredients " +
							"VALUES('{0}', {1}, '{2}');",
							form.name,
							form.amountInWarehouse,
							form.unitsOfMeasurement);
						break;
					case "RecipesIngredients":
						command.CommandText = string.Format(
							"INSERT INTO RecipesIngredients " +
							"VALUES({0}, {1}, {2});",
							form.fk_RecipeID,
							form.fk_IngredientID,
							form.quantity);
						break;
					default: throw new ArgumentException("Argument Exception when creating commandText.");
				}
				command.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				MessageBox.Show(ex.Message, "Error Inserting Record.", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			finally
			{
				sqlConnect.Close();
			}
		}
		// ----------------------- UPDATE RECORD -----------------------
		public static void UpdateRecord(string activeTable, int ID, DataGridView DataGridView1)
		{
			// validation
			if (ID == 0)
			{
				MessageBox.Show("Please select a record.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			dynamic form = 0;
			string commandTxt = "";
			switch (activeTable)
			{
				case "Recipes":
					int categoryID = (int)DataGridView1.SelectedRows[0].Cells[1].Value;
					string nameR = DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
					string descriptionR = DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
					string difficulty = DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
					string price = DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
					string image = DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
					form = new RecipesInput(categoryID, nameR, descriptionR, difficulty, image, price);
					commandTxt =
						"UPDATE Recipes " +
						"SET Fk_CategoryID = {0}, Name = '{1}', Description = '{2}', Difficulty = '{3}', Price = '{4}', Photo = '{5}' " +
						"WHERE RecipeID = {6};";
					break;
				case "Categories":
					string nameC = DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
					string descriptionC = DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
					string personInCharge = DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
					form = new CategoriesInput(nameC, descriptionC, personInCharge);
					commandTxt =
						"UPDATE Categories " +
						"SET Name = '{0}', Description = '{1}', PersonInCharge = '{2}' " +
						"WHERE CategoryID = {3};";
					break;
				case "Ingredients":
					string nameI = DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
					int amountInWarehouse = (int)DataGridView1.SelectedRows[0].Cells[2].Value;
					string unitsOfMeasurement = DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
					form = new IngredientsInput(nameI, amountInWarehouse, unitsOfMeasurement);
					commandTxt =
						"UPDATE Ingredients " +
						"SET Name = '{0}', AmountInWarehouse = {1}, UnitOfMeasurement = '{2}' " +
						"WHERE IngredientID = {3};";
					break;
				case "RecipesIngredients":
					int fk_RecipeID = (int)DataGridView1.SelectedRows[0].Cells[1].Value;
					int fk_IngredientID = (int)DataGridView1.SelectedRows[0].Cells[2].Value;
					int quantity = (int)DataGridView1.SelectedRows[0].Cells[3].Value;
					form = new RecipesIngredientsInput(fk_RecipeID, fk_IngredientID, quantity);
					commandTxt =
						"UPDATE RecipesIngredients " +
						"SET Fk_RecipeID = '{0}', Fk_IngredientID = {1}, Quantity = '{2}' " +
						"WHERE RecipeIngredientID = {3};";
					break;
			}
			form.FillInputFields();
			DialogResult result = form.ShowDialog();
			if (result != DialogResult.OK) return;
			try
			{
				sqlConnect.Open();
				SqlCommand command = sqlConnect.CreateCommand();
				switch (activeTable)
				{
					case "Recipes":
						commandTxt = string.Format(commandTxt, form.categoryID, form.name, form.description, form.difficulty, form.price, form.image, ID);
						break;
					case "Categories":
						commandTxt = string.Format(commandTxt, form.name, form.description, form.personInCharge, ID);
						break;
					case "Ingredients":
						commandTxt = string.Format(commandTxt, form.name, form.amountInWarehouse, form.unitsOfMeasurement, ID);
						break;
					case "RecipesIngredients":
						commandTxt = string.Format(commandTxt, form.fk_RecipeID, form.fk_IngredientID, form.quantity, ID);
						break;
					default: throw new ArgumentException("Argument Exception when creating commandText.");
				}
				command.CommandText = commandTxt;
				command.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				MessageBox.Show(ex.Message, "Error Updating Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				sqlConnect.Close();
			}
		}
		// ----------------------- DELETE RECORD -----------------------
		public static void DeleteRecord(string activeTable, int ID)
		{
			// validation
			if (ID == 0)
			{
				MessageBox.Show("Please select a record.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (result != DialogResult.Yes) return;
			// this switch defines the active column name depending on the active table
			string activeColumn;
			switch (activeTable)
			{
				case "Recipes":
					activeColumn = "RecipeID";
					break;
				case "Categories":
					activeColumn = "CategoryID";
					break;
				case "Ingredients":
					activeColumn = "IngredientID";
					break;
				case "RecipesIngredients":
					activeColumn = "RecipeIngredientID";
					break;
				default: throw new ArgumentException("Argument Exception when defining activeColumn.");
			}
			// delete using 3 variables: activeTable, activeColumn, ID
			try
			{
				sqlConnect.Open();
				SqlCommand command = sqlConnect.CreateCommand();
				command.CommandText = string.Format("DELETE FROM {0} WHERE {1} = {2}", activeTable, activeColumn, ID);
				command.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				MessageBox.Show(ex.Message, "Error Deleting Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				sqlConnect.Close();
			}
		}

		// ----------------------- DISPLAY REPORT -----------------------
		public static void DisplayReport(DataGridView dataGridView)
		{
			// read pictures from recipe table
			string getImagesCommand = "SELECT Name, Description, Difficulty, Price, Photo FROM Recipes;";
			DataTable imageTable = GetTable(getImagesCommand);

			// get number of columns
			int lastColumnNum = dataGridView.ColumnCount;

			// add an image column at position 0
			DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
			imageColumn.DataPropertyName = "Data";
			imageColumn.HeaderText = "Picture";
			imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
			dataGridView.Columns.Insert(lastColumnNum, imageColumn);
			dataGridView.RowTemplate.Height = 64;
			dataGridView.Columns[lastColumnNum].Width = 200;

			string saveFileFolder = @"Images\";
			string fileFullPath = Path.Combine(Application.StartupPath, saveFileFolder);

			imageTable.Columns.Add("Data", Type.GetType("System.Byte[]"));

			foreach (DataRow row in imageTable.Rows)
			{

				string picPath = fileFullPath + @"\" + row["Photo"].ToString();
				if (!File.Exists(picPath)) continue;
				row["Data"] = File.ReadAllBytes(picPath);
			}
			dataGridView.DataSource = imageTable;
		}
	}
}
