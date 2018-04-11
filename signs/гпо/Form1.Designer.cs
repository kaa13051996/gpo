namespace гпо
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Step1 = new System.Windows.Forms.Label();
            this.Step2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonDo = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.radioButtonMed = new System.Windows.Forms.RadioButton();
            this.radioButtonGap = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonDetection = new System.Windows.Forms.RadioButton();
            this.radioButtonEmbedding = new System.Windows.Forms.RadioButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Step7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.Step6 = new System.Windows.Forms.Label();
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.textBox_num2 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_num = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAddStego = new System.Windows.Forms.Button();
            this.checkBox_gistogram = new System.Windows.Forms.CheckBox();
            this.checkBox_error = new System.Windows.Forms.CheckBox();
            this.checkBox_signs = new System.Windows.Forms.CheckBox();
            this.checkBoxOriginal = new System.Windows.Forms.CheckBox();
            this.checkBoxStego = new System.Windows.Forms.CheckBox();
            this.checkBox_last_bits = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(836, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.закрытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("оПрограммеToolStripMenuItem.Image")));
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // закрытьToolStripMenuItem
            // 
            this.закрытьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("закрытьToolStripMenuItem.Image")));
            this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.закрытьToolStripMenuItem.Text = "Закрыть";
            this.закрытьToolStripMenuItem.Click += new System.EventHandler(this.закрытьToolStripMenuItem_Click);
            // 
            // Step1
            // 
            this.Step1.AutoSize = true;
            this.Step1.Location = new System.Drawing.Point(18, 138);
            this.Step1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Step1.Name = "Step1";
            this.Step1.Size = new System.Drawing.Size(267, 40);
            this.Step1.TabIndex = 2;
            this.Step1.Text = "Шаг 1:\r\nДобавьте исходное изображение.";
            // 
            // Step2
            // 
            this.Step2.AutoSize = true;
            this.Step2.Location = new System.Drawing.Point(298, 138);
            this.Step2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Step2.Name = "Step2";
            this.Step2.Size = new System.Drawing.Size(256, 60);
            this.Step2.TabIndex = 3;
            this.Step2.Text = "Шаг 2:\r\nВведите информацию, которую \r\nхотите скрыть.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(297, 223);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(244, 306);
            this.textBox1.TabIndex = 5;
            // 
            // buttonDo
            // 
            this.buttonDo.Location = new System.Drawing.Point(708, 55);
            this.buttonDo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDo.Name = "buttonDo";
            this.buttonDo.Size = new System.Drawing.Size(117, 60);
            this.buttonDo.TabIndex = 8;
            this.buttonDo.Text = "Выполнить";
            this.buttonDo.UseVisualStyleBackColor = true;
            this.buttonDo.Click += new System.EventHandler(this.buttonDo_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // radioButtonMed
            // 
            this.radioButtonMed.AutoSize = true;
            this.radioButtonMed.Location = new System.Drawing.Point(9, 34);
            this.radioButtonMed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonMed.Name = "radioButtonMed";
            this.radioButtonMed.Size = new System.Drawing.Size(70, 24);
            this.radioButtonMed.TabIndex = 10;
            this.radioButtonMed.TabStop = true;
            this.radioButtonMed.Text = "MED";
            this.radioButtonMed.UseVisualStyleBackColor = true;
            // 
            // radioButtonGap
            // 
            this.radioButtonGap.AutoSize = true;
            this.radioButtonGap.Location = new System.Drawing.Point(122, 34);
            this.radioButtonGap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonGap.Name = "radioButtonGap";
            this.radioButtonGap.Size = new System.Drawing.Size(68, 24);
            this.radioButtonGap.TabIndex = 11;
            this.radioButtonGap.TabStop = true;
            this.radioButtonGap.Text = "GAP";
            this.radioButtonGap.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonGap);
            this.groupBox1.Controls.Add(this.radioButtonMed);
            this.groupBox1.Location = new System.Drawing.Point(471, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(228, 75);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите метод:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonDetection);
            this.groupBox2.Controls.Add(this.radioButtonEmbedding);
            this.groupBox2.Location = new System.Drawing.Point(18, 42);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(444, 75);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выберите действие:";
            // 
            // radioButtonDetection
            // 
            this.radioButtonDetection.AutoSize = true;
            this.radioButtonDetection.Location = new System.Drawing.Point(234, 31);
            this.radioButtonDetection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonDetection.Name = "radioButtonDetection";
            this.radioButtonDetection.Size = new System.Drawing.Size(204, 24);
            this.radioButtonDetection.TabIndex = 1;
            this.radioButtonDetection.TabStop = true;
            this.radioButtonDetection.Text = "Считать информацию";
            this.radioButtonDetection.UseVisualStyleBackColor = true;
            this.radioButtonDetection.CheckedChanged += new System.EventHandler(this.radioButtonDetection_CheckedChanged);
            // 
            // radioButtonEmbedding
            // 
            this.radioButtonEmbedding.AutoSize = true;
            this.radioButtonEmbedding.Location = new System.Drawing.Point(10, 31);
            this.radioButtonEmbedding.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonEmbedding.Name = "radioButtonEmbedding";
            this.radioButtonEmbedding.Size = new System.Drawing.Size(212, 24);
            this.radioButtonEmbedding.TabIndex = 0;
            this.radioButtonEmbedding.TabStop = true;
            this.radioButtonEmbedding.Text = "Встроить информацию";
            this.radioButtonEmbedding.UseVisualStyleBackColor = true;
            this.radioButtonEmbedding.CheckedChanged += new System.EventHandler(this.radioButtonEmbedding_CheckedChanged);
            // 
            // Step7
            // 
            this.Step7.AutoSize = true;
            this.Step7.Location = new System.Drawing.Point(282, 138);
            this.Step7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Step7.Name = "Step7";
            this.Step7.Size = new System.Drawing.Size(270, 60);
            this.Step7.TabIndex = 20;
            this.Step7.Text = "Шаг 2:\r\nВыберите число для встраивания\r\nи нажмите извлечь:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(386, 543);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 60);
            this.button2.TabIndex = 21;
            this.button2.Text = "Извлечь";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Step6
            // 
            this.Step6.AutoSize = true;
            this.Step6.Location = new System.Drawing.Point(18, 138);
            this.Step6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Step6.Name = "Step6";
            this.Step6.Size = new System.Drawing.Size(213, 60);
            this.Step6.TabIndex = 19;
            this.Step6.Text = "Шаг 1:\r\nДобавьте изображение с\r\nвстроенной информацией.";
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(16, 223);
            this.pictureBoxOriginal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(242, 308);
            this.pictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginal.TabIndex = 6;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // textBox_num2
            // 
            this.textBox_num2.Location = new System.Drawing.Point(9, 52);
            this.textBox_num2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_num2.Name = "textBox_num2";
            this.textBox_num2.Size = new System.Drawing.Size(142, 26);
            this.textBox_num2.TabIndex = 23;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_num2);
            this.groupBox4.Location = new System.Drawing.Point(554, 122);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(164, 92);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Число для встраивания:";
            // 
            // textBox_num
            // 
            this.textBox_num.Location = new System.Drawing.Point(9, 29);
            this.textBox_num.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_num.Name = "textBox_num";
            this.textBox_num.Size = new System.Drawing.Size(216, 26);
            this.textBox_num.TabIndex = 23;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_num);
            this.groupBox3.Location = new System.Drawing.Point(585, 129);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(240, 77);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Число для встраивания:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(282, 223);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(433, 306);
            this.textBox2.TabIndex = 18;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(587, 223);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(237, 306);
            this.textBox3.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(593, 534);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // buttonAddStego
            // 
            this.buttonAddStego.Location = new System.Drawing.Point(38, 543);
            this.buttonAddStego.Name = "buttonAddStego";
            this.buttonAddStego.Size = new System.Drawing.Size(202, 45);
            this.buttonAddStego.TabIndex = 28;
            this.buttonAddStego.Text = "Добавить картинку";
            this.buttonAddStego.UseVisualStyleBackColor = true;
            this.buttonAddStego.Click += new System.EventHandler(this.buttonAddStego_Click);
            // 
            // checkBox_gistogram
            // 
            this.checkBox_gistogram.AutoSize = true;
            this.checkBox_gistogram.Location = new System.Drawing.Point(15, 543);
            this.checkBox_gistogram.Name = "checkBox_gistogram";
            this.checkBox_gistogram.Size = new System.Drawing.Size(171, 24);
            this.checkBox_gistogram.TabIndex = 29;
            this.checkBox_gistogram.Text = "По изображениям";
            this.checkBox_gistogram.UseVisualStyleBackColor = true;
            // 
            // checkBox_error
            // 
            this.checkBox_error.AutoSize = true;
            this.checkBox_error.Location = new System.Drawing.Point(192, 543);
            this.checkBox_error.Name = "checkBox_error";
            this.checkBox_error.Size = new System.Drawing.Size(239, 24);
            this.checkBox_error.TabIndex = 30;
            this.checkBox_error.Text = "По ошибкам предсказаний";
            this.checkBox_error.UseVisualStyleBackColor = true;
            // 
            // checkBox_signs
            // 
            this.checkBox_signs.AutoSize = true;
            this.checkBox_signs.Location = new System.Drawing.Point(433, 543);
            this.checkBox_signs.Name = "checkBox_signs";
            this.checkBox_signs.Size = new System.Drawing.Size(108, 24);
            this.checkBox_signs.TabIndex = 31;
            this.checkBox_signs.Text = "Признаки";
            this.checkBox_signs.UseVisualStyleBackColor = true;
            // 
            // checkBoxOriginal
            // 
            this.checkBoxOriginal.AutoSize = true;
            this.checkBoxOriginal.Location = new System.Drawing.Point(12, 590);
            this.checkBoxOriginal.Name = "checkBoxOriginal";
            this.checkBoxOriginal.Size = new System.Drawing.Size(170, 24);
            this.checkBoxOriginal.TabIndex = 32;
            this.checkBoxOriginal.Text = "Вывод оригинала";
            this.checkBoxOriginal.UseVisualStyleBackColor = true;
            // 
            // checkBoxStego
            // 
            this.checkBoxStego.AutoSize = true;
            this.checkBoxStego.Location = new System.Drawing.Point(188, 590);
            this.checkBoxStego.Name = "checkBoxStego";
            this.checkBoxStego.Size = new System.Drawing.Size(132, 24);
            this.checkBoxStego.TabIndex = 33;
            this.checkBoxStego.Text = "Вывод стего";
            this.checkBoxStego.UseVisualStyleBackColor = true;
            // 
            // checkBox_last_bits
            // 
            this.checkBox_last_bits.AutoSize = true;
            this.checkBox_last_bits.Location = new System.Drawing.Point(327, 589);
            this.checkBox_last_bits.Name = "checkBox_last_bits";
            this.checkBox_last_bits.Size = new System.Drawing.Size(163, 24);
            this.checkBox_last_bits.TabIndex = 34;
            this.checkBox_last_bits.Text = "Последние биты";
            this.checkBox_last_bits.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 626);
            this.Controls.Add(this.checkBox_last_bits);
            this.Controls.Add(this.checkBoxStego);
            this.Controls.Add(this.checkBoxOriginal);
            this.Controls.Add(this.checkBox_signs);
            this.Controls.Add(this.checkBox_error);
            this.Controls.Add(this.checkBox_gistogram);
            this.Controls.Add(this.buttonAddStego);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Step7);
            this.Controls.Add(this.Step6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDo);
            this.Controls.Add(this.pictureBoxOriginal);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Step2);
            this.Controls.Add(this.Step1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Программа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
        private System.Windows.Forms.Label Step1;
        private System.Windows.Forms.Label Step2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonDo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton radioButtonMed;
        private System.Windows.Forms.RadioButton radioButtonGap;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonDetection;
        private System.Windows.Forms.RadioButton radioButtonEmbedding;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label Step7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label Step6;
        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.TextBox textBox_num2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox_num;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddStego;
        private System.Windows.Forms.CheckBox checkBox_gistogram;
        private System.Windows.Forms.CheckBox checkBox_error;
        private System.Windows.Forms.CheckBox checkBox_signs;
        private System.Windows.Forms.CheckBox checkBoxOriginal;
        private System.Windows.Forms.CheckBox checkBoxStego;
        private System.Windows.Forms.CheckBox checkBox_last_bits;
    }
}

