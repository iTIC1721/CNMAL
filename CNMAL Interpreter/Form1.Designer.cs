namespace CNMAL_Interpreter {
	partial class Form1 {
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent() {
			this.inputBox = new System.Windows.Forms.TextBox();
			this.OutputBox = new System.Windows.Forms.TextBox();
			this.resetButton = new System.Windows.Forms.Button();
			this.compileButton = new System.Windows.Forms.Button();
			this.LineCountLabel = new System.Windows.Forms.Label();
			this.letterCountLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// inputBox
			// 
			this.inputBox.Location = new System.Drawing.Point(12, 41);
			this.inputBox.Multiline = true;
			this.inputBox.Name = "inputBox";
			this.inputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.inputBox.Size = new System.Drawing.Size(760, 355);
			this.inputBox.TabIndex = 0;
			this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged);
			// 
			// OutputBox
			// 
			this.OutputBox.Location = new System.Drawing.Point(12, 403);
			this.OutputBox.Multiline = true;
			this.OutputBox.Name = "OutputBox";
			this.OutputBox.ReadOnly = true;
			this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.OutputBox.Size = new System.Drawing.Size(760, 146);
			this.OutputBox.TabIndex = 1;
			// 
			// resetButton
			// 
			this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.resetButton.Location = new System.Drawing.Point(12, 12);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(75, 23);
			this.resetButton.TabIndex = 2;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// compileButton
			// 
			this.compileButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.compileButton.Location = new System.Drawing.Point(697, 12);
			this.compileButton.Name = "compileButton";
			this.compileButton.Size = new System.Drawing.Size(75, 23);
			this.compileButton.TabIndex = 3;
			this.compileButton.Text = "Compile";
			this.compileButton.UseVisualStyleBackColor = true;
			this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
			// 
			// LineCountLabel
			// 
			this.LineCountLabel.AutoSize = true;
			this.LineCountLabel.Location = new System.Drawing.Point(93, 9);
			this.LineCountLabel.Name = "LineCountLabel";
			this.LineCountLabel.Size = new System.Drawing.Size(0, 12);
			this.LineCountLabel.TabIndex = 4;
			this.LineCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// letterCountLabel
			// 
			this.letterCountLabel.AutoSize = true;
			this.letterCountLabel.Location = new System.Drawing.Point(93, 23);
			this.letterCountLabel.Name = "letterCountLabel";
			this.letterCountLabel.Size = new System.Drawing.Size(0, 12);
			this.letterCountLabel.TabIndex = 5;
			this.letterCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.letterCountLabel);
			this.Controls.Add(this.LineCountLabel);
			this.Controls.Add(this.compileButton);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.OutputBox);
			this.Controls.Add(this.inputBox);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 600);
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CNMAL";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.TextBox OutputBox;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button compileButton;
		private System.Windows.Forms.Label LineCountLabel;
		private System.Windows.Forms.Label letterCountLabel;
	}
}

