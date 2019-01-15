namespace WindowsFormsApp1
{
	partial class Login
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox_user = new System.Windows.Forms.TextBox();
			this.textBox_pass = new System.Windows.Forms.TextBox();
			this.login_button = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox_user
			// 
			this.textBox_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox_user.Location = new System.Drawing.Point(12, 124);
			this.textBox_user.Multiline = true;
			this.textBox_user.Name = "textBox_user";
			this.textBox_user.Size = new System.Drawing.Size(202, 25);
			this.textBox_user.TabIndex = 0;
			// 
			// textBox_pass
			// 
			this.textBox_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox_pass.Location = new System.Drawing.Point(12, 179);
			this.textBox_pass.Multiline = true;
			this.textBox_pass.Name = "textBox_pass";
			this.textBox_pass.PasswordChar = '*';
			this.textBox_pass.Size = new System.Drawing.Size(202, 25);
			this.textBox_pass.TabIndex = 1;
			// 
			// login_button
			// 
			this.login_button.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.login_button.FlatAppearance.BorderSize = 0;
			this.login_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.login_button.Location = new System.Drawing.Point(-1, 242);
			this.login_button.Name = "login_button";
			this.login_button.Size = new System.Drawing.Size(230, 36);
			this.login_button.TabIndex = 2;
			this.login_button.Text = "Login";
			this.login_button.UseVisualStyleBackColor = true;
			this.login_button.Click += new System.EventHandler(this.login_button_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(9, 99);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "User";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(9, 155);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Password";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(49, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(124, 84);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// Login
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
			this.ClientSize = new System.Drawing.Size(228, 277);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.login_button);
			this.Controls.Add(this.textBox_pass);
			this.Controls.Add(this.textBox_user);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(244, 316);
			this.MinimumSize = new System.Drawing.Size(244, 316);
			this.Name = "Login";
			this.Text = "Login";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_user;
		private System.Windows.Forms.TextBox textBox_pass;
		private System.Windows.Forms.Button login_button;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

