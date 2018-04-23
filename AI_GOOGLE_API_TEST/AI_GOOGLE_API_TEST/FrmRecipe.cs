using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_GOOGLE_API_TEST
{
    public partial class FrmRecipe : Form
    {
        public FrmRecipe(Food food)
        {
            InitializeComponent();
            TxtFoodName.Text = food.Name;
            TxtDirections.Text = food.Instructions;
            TxtIng.Text = food.Ingredients;
            Name = food.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
