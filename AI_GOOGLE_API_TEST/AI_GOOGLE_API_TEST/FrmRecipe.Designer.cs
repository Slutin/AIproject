namespace AI_GOOGLE_API_TEST
{
    partial class FrmRecipe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFoodName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtIng = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtDirections = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // TxtFoodName
            // 
            this.TxtFoodName.Location = new System.Drawing.Point(78, 6);
            this.TxtFoodName.Name = "TxtFoodName";
            this.TxtFoodName.Size = new System.Drawing.Size(682, 20);
            this.TxtFoodName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ingredients:";
            // 
            // TxtIng
            // 
            this.TxtIng.Location = new System.Drawing.Point(78, 32);
            this.TxtIng.Multiline = true;
            this.TxtIng.Name = "TxtIng";
            this.TxtIng.Size = new System.Drawing.Size(682, 84);
            this.TxtIng.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Directions:";
            // 
            // TxtDirections
            // 
            this.TxtDirections.Location = new System.Drawing.Point(78, 122);
            this.TxtDirections.Multiline = true;
            this.TxtDirections.Name = "TxtDirections";
            this.TxtDirections.Size = new System.Drawing.Size(682, 171);
            this.TxtDirections.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(633, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 341);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TxtDirections);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtIng);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtFoodName);
            this.Controls.Add(this.label1);
            this.Name = "FrmRecipe";
            this.Text = "FrmRecipe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtFoodName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtIng;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtDirections;
        private System.Windows.Forms.Button button1;
    }
}