using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipesSystem
{
    public partial class RecipesInput : Form
    {
        // one constructor without parameters for the Insert operation
        public RecipesInput()
        {
            InitializeComponent();
        }
        // another constructor with parameters for the Update operation
        public RecipesInput(int categoryID, string name, string description, string difficulty, string image, string price)
        {
            InitializeComponent();
            this.categoryID = categoryID;
            this.name = name;
            this.description = description;
            this.difficulty = difficulty;
            this.image = image;
            this.price = price;
        }
        // these public fields are used in the SqlQueries class
        public int categoryID;
        public string name, description, difficulty, price, image;

        private string imagePath;

        private void BtnCancel_Click(object sender, EventArgs e) => Close();
        private void BtnOk_Click_1(object sender, EventArgs e)
        {
            categoryID = int.Parse(TxtBoxCategoryID.Text);
            name = TxtBoxName.Text;
            description = TxtBoxDescription.Text;
            difficulty = comboBox1.Text;
            price = TxtBoxPrice.Text;
        }
        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            if (OpenImageDialog.ShowDialog() == DialogResult.OK)
            {
                // gets fileName and removes everything but the image name
                pictureBox1.Image = Image.FromFile(OpenImageDialog.FileName);
                image = OpenImageDialog.FileName;
                int picIndex = image.LastIndexOf(@"\");
                image = (image.Substring(picIndex)).Remove(0, 1);
                // this creates the image path
                string fileFullPath = Path.Combine(Application.StartupPath, @"Images\");
                imagePath = fileFullPath + image;
                // if the directory does not exist then create it
                if (!Directory.Exists(fileFullPath))
                {
                    Directory.CreateDirectory(fileFullPath);
                }
                // save the image using the image path
                pictureBox1.Image.Save(imagePath);

                // when getting the image from the database to display, the image in the directory needs to be used to display
            }
        }
        public void FillInputFields()
        {
            TxtBoxCategoryID.Text = categoryID.ToString();
            TxtBoxName.Text = name;
            TxtBoxDescription.Text = description;
            comboBox1.Text = difficulty;
            TxtBoxPrice.Text = price;
        }
    }
}
