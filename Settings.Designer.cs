namespace PickASlice
{
    partial class Settings
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
            btnOrca = new Button();
            btnCreality = new Button();
            btnElegoo = new Button();
            btnAnycubic = new Button();
            btnQidi = new Button();
            btnSettings = new Button();
            btnBambu = new Button();
            SuspendLayout();
            // 
            // btnOrca
            // 
            btnOrca.Location = new Point(79, 44);
            btnOrca.Name = "btnOrca";
            btnOrca.Size = new Size(75, 23);
            btnOrca.TabIndex = 0;
            btnOrca.Text = "OrcaSlicer";
            btnOrca.UseVisualStyleBackColor = true;
            btnOrca.Click += btnOrca_Click;
            // 
            // btnCreality
            // 
            btnCreality.Location = new Point(79, 73);
            btnCreality.Name = "btnCreality";
            btnCreality.Size = new Size(75, 23);
            btnCreality.TabIndex = 1;
            btnCreality.Text = "Creality Print";
            btnCreality.UseVisualStyleBackColor = true;
            btnCreality.Click += btnCreality_Click;
            // 
            // btnElegoo
            // 
            btnElegoo.Location = new Point(79, 102);
            btnElegoo.Name = "btnElegoo";
            btnElegoo.Size = new Size(75, 23);
            btnElegoo.TabIndex = 2;
            btnElegoo.Text = "Elegoo ";
            btnElegoo.UseVisualStyleBackColor = true;
            btnElegoo.Click += btnElegoo_Click;
            // 
            // btnAnycubic
            // 
            btnAnycubic.Location = new Point(79, 131);
            btnAnycubic.Name = "btnAnycubic";
            btnAnycubic.Size = new Size(75, 23);
            btnAnycubic.TabIndex = 3;
            btnAnycubic.Text = "AnyCubic";
            btnAnycubic.UseVisualStyleBackColor = true;
            btnAnycubic.Click += btnAnycubic_Click;
            // 
            // btnQidi
            // 
            btnQidi.Location = new Point(79, 160);
            btnQidi.Name = "btnQidi";
            btnQidi.Size = new Size(75, 23);
            btnQidi.TabIndex = 4;
            btnQidi.Text = "Qidi Studio";
            btnQidi.UseVisualStyleBackColor = true;
            btnQidi.Click += btnQidi_Click;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(79, 219);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(75, 23);
            btnSettings.TabIndex = 5;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnBambu
            // 
            btnBambu.Location = new Point(79, 189);
            btnBambu.Name = "btnBambu";
            btnBambu.Size = new Size(75, 23);
            btnBambu.TabIndex = 6;
            btnBambu.Text = "Bambu Studio";
            btnBambu.UseVisualStyleBackColor = true;
            btnBambu.Click += btnBambuClick;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(250, 290);
            Controls.Add(btnBambu);
            Controls.Add(btnSettings);
            Controls.Add(btnQidi);
            Controls.Add(btnAnycubic);
            Controls.Add(btnElegoo);
            Controls.Add(btnCreality);
            Controls.Add(btnOrca);
            Name = "Settings";
            Text = "Pick A Slice";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOrca;
        private Button btnCreality;
        private Button btnElegoo;
        private Button btnAnycubic;
        private Button btnQidi;
        private Button btnSettings;
        private Button btnBambu;
    }
}
