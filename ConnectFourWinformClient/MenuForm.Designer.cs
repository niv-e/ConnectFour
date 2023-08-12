namespace ConnectFourWinformClient
{
    partial class MenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            PlayerIdTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            GuidToRestoreTextBox = new TextBox();
            button2 = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(452, 141);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PlayerIdTextBox
            // 
            PlayerIdTextBox.Location = new Point(338, 142);
            PlayerIdTextBox.Name = "PlayerIdTextBox";
            PlayerIdTextBox.Size = new Size(94, 27);
            PlayerIdTextBox.TabIndex = 1;
            PlayerIdTextBox.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(251, 145);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 2;
            label1.Text = "Player Id";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(145, 303);
            label2.Name = "label2";
            label2.Size = new Size(118, 20);
            label2.TabIndex = 5;
            label2.Text = "Game Session Id";
            label2.Click += label2_Click;
            // 
            // GuidToRestoreTextBox
            // 
            GuidToRestoreTextBox.Location = new Point(269, 300);
            GuidToRestoreTextBox.Name = "GuidToRestoreTextBox";
            GuidToRestoreTextBox.Size = new Size(239, 27);
            GuidToRestoreTextBox.TabIndex = 4;
            GuidToRestoreTextBox.Text = "0";
            // 
            // button2
            // 
            button2.Location = new Point(514, 300);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "Restore";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(316, 258);
            label3.Name = "label3";
            label3.Size = new Size(144, 28);
            label3.TabIndex = 6;
            label3.Text = "Restore Game";
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(GuidToRestoreTextBox);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(PlayerIdTextBox);
            Controls.Add(button1);
            Name = "MenuForm";
            Text = "Form1";
            Load += MenuForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox PlayerIdTextBox;
        private Label label1;
        private Label label2;
        private TextBox GuidToRestoreTextBox;
        private Button button2;
        private Label label3;
    }
}