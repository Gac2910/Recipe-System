using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipesSystem
{
    class Prompt
    {
        public static string ShowDialog(string text, string promptTitle)
        {
            // create new instance of Form and set some properties
            Form prompt = new Form()
            {
                Width = 400,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = promptTitle,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = System.Drawing.Color.White
            };
            // create components and set properties
            Label textLabel = new Label()
            {
                Left = 50,
                Top = 20,
                Text = text,
                Width = 200
            };
            TextBox textBox = new TextBox()
            {
                Left = 50,
                Top = 50,
                Width = 250
            };
            Button confirmationBtn = new Button()
            {
                Text = "Ok",
                Left = 50,
                Width = 100,
                Top = 80,
                DialogResult = DialogResult.OK
            };
            // use += to assign function to click event
            confirmationBtn.Click += (sender, e) => { prompt.Close(); };
            // add the components to the prompt
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmationBtn);
            prompt.AcceptButton = confirmationBtn;
            // return the value of the textbox if dialog result is OK
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
