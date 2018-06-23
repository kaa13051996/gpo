using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace гпо
{
    public partial class MainForm : Form
    {       
        public int count = 1; //количество столбцов в гистограмме

        //эксель
        private Excel.Application excelapp;
        private Excel.Sheets excelsheets;
        private Excel.Worksheet excelworksheet;
        private Excel.Range excelcells;
        private Excel.Workbooks excelappworkbooks;
        private Excel.Workbook excelappworkbook;

        //значения по умолчанию при запуске
        public MainForm() 
        {
            InitializeComponent();

            radioButtonEmbedding.Checked = true;
            radioButtonMed.Checked = true;
            textBox_num2.Text = "0";
            textBox3.Text = "Имена отработанных файлов: ";
            label1.Text = "";
        }       

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show("Вы  хотите  закрыть  программу?",
"Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
DialogResult.Yes;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C)ТУСУР, ГПО 2016\nРазработчики:\n- Крупина (724);\n- Тирская (724),\n- Осин (724).", "О  программе",
MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //внешний вид
        private void radioButtonEmbedding_CheckedChanged(object sender, EventArgs e)
        {
            this.Width = 573;
            this.Height = 446;
            pictureBoxOriginal.Image = null;
            pictureBoxOriginal.Invalidate();
            //pictureBoxFinal3.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = false;
            textBox3.Visible = true;
            textBox_num.Visible = true;
            textBox_num2.Visible = false;
            Step6.Visible = false;
            Step7.Visible = false;
            Step1.Visible = true;
            Step2.Visible = true;
            buttonAddStego.Visible = false;
            button2.Visible = false;
            buttonDo.Visible = true;
            groupBox3.Visible = true;
            groupBox4.Visible = false;
            checkBox_gistogram.Visible = true;
            checkBox_error.Visible = true;
            checkBox_signs.Visible = true;
        }
        private void radioButtonDetection_CheckedChanged(object sender, EventArgs e)
        {
            this.Width = 499;
            this.Height = 440;
            pictureBoxOriginal.Image = null;
            pictureBoxOriginal.Invalidate();
            //pictureBoxFinal3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = true;
            textBox3.Visible = false;
            textBox_num.Visible = false;
            textBox_num2.Visible = true;
            Step6.Visible = true;
            Step7.Visible = true;
            Step1.Visible = false;
            Step2.Visible = false;
            button2.Visible = true;
            buttonDo.Visible = false;
            buttonAddStego.Visible = true;
            groupBox3.Visible = false;
            groupBox4.Visible = true;
            checkBox_gistogram.Visible = false;
            checkBox_error.Visible = false;
            checkBox_signs.Visible = false;
        }

        private void buttonDo_Click(object sender, EventArgs e)
        {
            if (radioButtonEmbedding.Checked == true)
            {
                //папка с исходными изображениями
                string[] dirs = Directory.GetFiles(@"D:\study\gpo\OriginalImage");
                textBox3.Text = "Имена отработанных файлов:";

                //открыть эксель на запись
                excelapp = new Excel.Application();

                //создаем листы
                if (checkBox_gistogram.Checked == true || checkBox_error.Checked == true) excelapp.SheetsInNewWorkbook = dirs.Length; 
                if (checkBox_signs.Checked == true) excelapp.SheetsInNewWorkbook = 2;
                if (checkBoxOriginal.Checked == true || checkBoxStego.Checked == true || checkBox_last_bits.Checked == true) excelapp.SheetsInNewWorkbook = dirs.Length;

                excelapp.Workbooks.Add(Type.Missing);                
                excelappworkbooks = excelapp.Workbooks;                
                excelappworkbook = excelappworkbooks[1];                
                excelappworkbook.Saved = true;
                excelsheets = excelappworkbook.Worksheets;                

                int count_image = 0; //счетчик изображений в папке                              

                ////вывод в эксель name и номера признака
                if (checkBox_signs.Checked == true)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        excelworksheet = (Excel.Worksheet)excelsheets.get_Item(j);
                        for (int i = 1; i < 11; i++)
                        {
                            excelcells = (Excel.Range)excelworksheet.Cells[1, i];
                            if (i == 1) excelcells.Value2 = "name";
                            else
                            {
                                if (i > 4) excelcells.Value2 = i; //4 признак не рассчитываем
                                else excelcells.Value2 = i - 1;
                            }
                        }
                    }
                }
                File.Delete("D:\\study\\gpo\\БД изображений\\standard_test_images\\new_file.txt"); //удаление файла 
                while (count_image < dirs.Length)
                {          
                    ////вывод в эксель имен изображений
                    if (checkBox_signs.Checked == true)
                    {
                        for (int j = 1; j < 3; j++)
                        {
                            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(j);
                            excelcells = (Excel.Range)excelworksheet.Cells[count_image + 2, 1];
                            excelcells.Value2 = Path.GetFileName(dirs[count_image]);
                        }
                    }

                    pictureBoxOriginal.Image = null; 
                    pictureBoxOriginal.Image = new Bitmap(dirs[count_image]);

                    //параметры изображения
                    Bitmap bmp = new Bitmap(pictureBoxOriginal.Image);
                    int height = bmp.Height;
                    int width = bmp.Width;                    

                    //последовательность действий
                    //разбить картинку на пиксели
                    float[,] blue = Original_picture(bmp, height, width);

                    //младшие 3 бита
                    
                    //if (checkBox_last_bits.Checked == true)
                    //{
                        string[,] last_bits = Last_Bits(blue, height, width);
                    LastBitsPicture(last_bits, height, width, Path.GetFileName(dirs[count_image]), 1234, "original");
                    float[] arr_float = new float[height*width];
                        int k = 0;
                        for (int i = 0; i < height; i++)
                            for (int j = 0; j < width; j++)
                            {
                                arr_float[k] = Convert.ToSingle(last_bits[j, i]);
                                k++;
                            }
                        Array.Sort(arr_float); //сортировка
                        float[,] hist_last_bits = histogram(arr_float, height, width);
                        hist_last_bits = Last_Bits_pct(hist_last_bits, hist_last_bits.GetLength(1));
                        //excelworksheet = (Excel.Worksheet)excelsheets.get_Item(count_image + 1);
                        excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                        exportExcelMatrix(hist_last_bits, hist_last_bits.GetLength(1), hist_last_bits.GetLength(0), (count_image*10) + 1);
                        //StreamWriter sw = new StreamWriter("D:\\Test.txt");
                        //ExportExcelLastBits(last_bits, height, width, sw);
                        //sw.Close();       
                    //}
                    

                    if (checkBoxOriginal.Checked == true)
                    {
                        excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                        exportExcelImage(blue, height, width, 0);
                    }

                    if (checkBox_signs.Checked == true)
                    {
                        //матрица смежности !!!
                        float[] blueCloneOriginal = Clone(blue, height, width); //запись в одномерный массив
                        Array.Sort(blueCloneOriginal); //сортировка
                        float[,] histogramBlueOriginal = histogram(blueCloneOriginal, height, width); //гистограмма
                        float[,] matrix_adjacency = matrixAdj(histogramBlueOriginal, blue, height, width); //матрица смежности
                        //exportExcelMatrix(matrix_adjacency, matrix_adjacency.GetLength(0), matrix_adjacency.GetLength(1), blue.GetLength(1) + 1); //matrix_adjacency.GetLength(0) = height

                        excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);

                        //признаки !!!
                        double[] tableSigns = signs(matrix_adjacency);
                        exportExcelMatrix(tableSigns, tableSigns.GetLength(0), count_image + 1); //matrix_adjacency.GetLength(0) = height
                    }

                    if (checkBox_gistogram.Checked == true)
                    {
                        excelworksheet = (Excel.Worksheet)excelsheets.get_Item(count_image + 1);
                        excelcells = (Excel.Range)excelworksheet.Cells[1, 1];
                        excelcells.Value2 = Path.GetFileName(dirs[count_image]);
                        float[] blueClone_original = Clone(blue, height, width); //запись в одномерный массив
                        Array.Sort(blueClone_original); //сортировка
                        float[,] histogramBlue_original = histogram(blueClone_original, height, width); //гистограмма
                        float sum = 0;
                        for (int i = 0; i < histogramBlue_original.Length/2; i++)
                        {
                            sum += histogramBlue_original[1, i];
                        }
                        exportExcelHis(histogramBlue_original, 1, sum);
                    }
                    
                    //выбор метода предсказания
                    if (radioButtonMed.Checked == true)
                    {
                        float[,] X = MED(blue, height, width); //предсказатель
                        width = width - 1; height = height - 1;

                        ////экспорт матрицы предсказаний
                        //excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                        //exportExcelMatrix(X, height, width, 0);

                        float[,] error = errorsMed(blue, X, height, width); //ошибки предсказания    
                                                
                        //экспорт матрицы ошибок предсакзаний
                        //exportExcelMatrix(error, height, width, height*2-14);                                       

                        float[] blueClone = Clone(error, height, width); //запись в одномерный массив
                        Array.Sort(blueClone); //сортировка
                        float[,] histogramBlue = histogram(blueClone, height, width); //гистограмма
                        //var sw = new StreamWriter(@"D:\gpo\histograms.csv", true, Encoding.Default);
                        //using (sw)
                        //{
                        //    sw.WriteLine();
                        //    for (int i = 0; i < histogramBlue.GetLength(1); i++)
                        //    {
                        //        sw.WriteLine(histogramBlue[0, i] + ";" + histogramBlue[1, i]);                              
                        //    }                                                     
                        //}
                        
                        
                        if (checkBox_error.Checked == true)
                        {
                            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(count_image + 1);
                            excelcells = (Excel.Range)excelworksheet.Cells[1, 1];
                            excelcells.Value2 = Path.GetFileName(dirs[count_image]);
                            float sum = 0;
                            for (int i = 0; i < histogramBlue.Length / 2; i++)
                            {
                                sum += histogramBlue[1, i];
                            }
                            exportExcelHis(histogramBlue, 1, sum);
                        }

                        double numEmbed = searchNumEmbed(histogramBlue); //поиск числа для встраивания (или вручную)                                        

                        error = Shift(error, numEmbed, height, width);//сдвиг на 1, если больше numEmbed+1
                        error = Build(error, numEmbed, height, width); //встраивание
                        float [,] blue_stego = PictureFinalMED(error, height, width, blue, X); //восстановление  
                        height++; width++;

                        //младшие 3 бита
                        string[,] last_bits_stego = Last_Bits(blue_stego, height, width);
                        LastBitsPicture(last_bits_stego, height, width, Path.GetFileName(dirs[count_image]), numEmbed, "stego");
                        //if (checkBox_last_bits.Checked == true)
                        //{
                            float[] arr_float_stego = new float[height * width];
                            int k_stego = 0;                            
                            for (int i = 0; i < height; i++)
                                for (int j = 0; j < width; j++)
                                {
                                    arr_float[k_stego] = Convert.ToSingle(last_bits_stego[j, i]);
                                    k_stego++;                                    
                                }
                            Array.Sort(arr_float); //сортировка
                            float[,] hist_last_bits_stego = histogram(arr_float, height, width);
                            hist_last_bits_stego = Last_Bits_pct(hist_last_bits_stego, hist_last_bits.GetLength(1));
                            //excelworksheet = (Excel.Worksheet)excelsheets.get_Item(count_image + 1);                        
                            //exportExcelMatrix(hist_last_bits_stego, hist_last_bits.GetLength(1), hist_last_bits.GetLength(0), hist_last_bits.GetLength(1) + 1);
                            exportExcelMatrix2(hist_last_bits_stego, hist_last_bits.GetLength(1), hist_last_bits.GetLength(0), (count_image * 10) + 1);
                        //StreamWriter sw2 = new StreamWriter("D:\\Test2.txt");
                        //ExportExcelLastBits(last_bits_stego, height, width, sw2);
                        //sw2.Close();
                        //}

                        //RMSE
                        double var_RMSE = RMSE(hist_last_bits, hist_last_bits_stego, hist_last_bits.GetLength(1));
                        excelcells = (Excel.Range)excelworksheet.Cells[(count_image * 10) + 1, 4];
                        excelcells.Value2 = "RMSE";
                        excelcells = (Excel.Range)excelworksheet.Cells[(count_image * 10) + 2, 4];
                        excelcells.Value2 = var_RMSE;
                        
                        File.AppendAllText("D:\\study\\gpo\\БД изображений\\standard_test_images\\new_file.txt", var_RMSE.ToString() + Environment.NewLine);

                        RecoveryImage(blue_stego, height, width, bmp, Path.GetFileNameWithoutExtension(dirs[count_image]), numEmbed);

                        //экспорт стегоизображения
                        if (checkBoxStego.Checked == true)
                        {
                            //exportExcelImage(blue_stego, height, width, height + 1 + matrix_adjacency.GetLength(0) + 1 + tableSigns.GetLength(0) + 1);
                            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                            exportExcelImage(blue_stego, height, width, 0);
                        }


                        //ГИСТОГРАММА ДЛЯ СТЕГОИЗОБРАЖЕНИЯ
                        if (checkBox_gistogram.Checked == true)
                        {
                            float[] blueClone_stego = Clone(blue_stego, height, width); //запись в одномерный массив
                            Array.Sort(blueClone_stego); //сортировка
                            float[,] histogramBlue_stego = histogram(blueClone_stego, height, width); //гистограмма

                            float sum = 0;
                            for (int i = 0; i < histogramBlue_stego.Length / 2; i++)
                            {
                                sum += histogramBlue_stego[1, i];
                            }
                            exportExcelHis(histogramBlue_stego, 4, sum);
                        }                     

                        if (checkBox_signs.Checked == true)
                        {
                            //матрица смежности !!!
                            float[] blueCloneStego = Clone(blue_stego, height, width); //запись в одномерный массив
                            Array.Sort(blueCloneStego); //сортировка
                            float[,] histogramBlueStego = histogram(blueCloneStego, height, width); //гистограмма
                            float[,] matrix_adjacency_stego = matrixAdj(histogramBlueStego, blue_stego, height, width);
                            //exportExcelMatrix(matrix_adjacency_stego, matrix_adjacency_stego.GetLength(0), matrix_adjacency_stego.GetLength(1), 2 * height + matrix_adjacency.GetLength(0) + tableSigns.GetLength(0) + 4); //matrix_adjacency_stego.GetLength(0) = height
                            
                            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(2);
                            double[] tableSigns_stego = signs(matrix_adjacency_stego);
                            exportExcelMatrix(tableSigns_stego, tableSigns_stego.GetLength(0), count_image + 1); //matrix_adjacency.GetLength(0) = height
                        }                                

                        if (checkBox_error.Checked == true)
                        {
                            float[,] X_stego = MED(blue_stego, height, width); //предсказатель
                            width = width - 1; height = height - 1;

                            //экспорт матрицы предсказаний
                            //exportExcelMatrix(X, height, width, 0);

                            float[,] error_stego = errorsMed(blue_stego, X_stego, height, width); //ошибки предсказания    

                            //экспорт матрицы ошибок предсакзаний
                            //exportExcelMatrix(error, height, width, height*2-14);                                       

                            float[] blueClone_stego = Clone(error_stego, height, width); //запись в одномерный массив
                            Array.Sort(blueClone_stego); //сортировка
                            float[,] histogramBlue_stego = histogram(blueClone_stego, height, width); //гистограмма
                            float sum = 0;
                            for (int i = 0; i < histogramBlue_stego.Length / 2; i++)
                            {
                                sum += histogramBlue_stego[1, i];
                            }
                            exportExcelHis(histogramBlue_stego, 4, sum);
                        }                                     
                        
                        textBox3.Text += "\r\n" + Path.GetFileName(dirs[count_image]);
                        count_image++;
                        label1.Text = count_image.ToString() + "/" + dirs.Length.ToString();
                    }
                    else
                    {
                        float[,] X = GAP(blue, height, width); //предсказатель                        
                        width = width - 3; height = height - 2;

                        //excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                        //exportExcelMatrix(X, height, width, 0);
                        
                        float[,] error = errorsGap(blue, X, height, width);

                        //excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                        //exportExcelMatrix(error, height, width, 0);

                        float[] blueClone = Clone(error, height, width); //запись в одномерный массив
                        Array.Sort(blueClone); //сортировка
                        float[,] histogramBlue = histogram(blueClone, height, width); //гистограмма

                        if (checkBox_error.Checked == true)
                        {
                            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(count_image + 1);
                            excelcells = (Excel.Range)excelworksheet.Cells[1, 1];
                            excelcells.Value2 = Path.GetFileName(dirs[count_image]);
                            float sum = 0;
                            for (int i = 0; i < histogramBlue.Length / 2; i++)
                            {
                                sum += histogramBlue[1, i];
                            }
                            exportExcelHis(histogramBlue, 1, sum);
                        }

                        double numEmbed = searchNumEmbed(histogramBlue); //поиск числа для встраивания (или вручную)
                        error = Shift(error, numEmbed, height, width);//сдвиг на 1, если больше numEmbed+1
                        error = Build(error, numEmbed, height, width); //встраивание

                        //excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                        //exportExcelMatrix(error, height, width, 0);

                        float[,] blue_stego = PictureFinalGAP(error, height, width, blue, X); //восстановление  
                        height+=2; width+=3;
                        RecoveryImage(blue_stego, height, width, bmp, Path.GetFileNameWithoutExtension(dirs[count_image]), numEmbed);

                        //экспорт стегоизображения
                        //excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                        //exportExcelImage(blue_stego, height, width, 0);

                        if (checkBox_gistogram.Checked == true)
                        {
                            float[] blueClone_stego = Clone(blue_stego, height, width); //запись в одномерный массив
                            Array.Sort(blueClone_stego); //сортировка
                            float[,] histogramBlue_stego = histogram(blueClone_stego, height, width); //гистограмма

                            float sum = 0;
                            for (int i = 0; i < histogramBlue_stego.Length / 2; i++)
                            {
                                sum += histogramBlue_stego[1, i];
                            }
                            exportExcelHis(histogramBlue_stego, 4, sum);
                        }

                        if (checkBox_signs.Checked == true)
                        {
                            //матрица смежности !!!
                            float[] blueCloneStego = Clone(blue_stego, height, width); //запись в одномерный массив
                            Array.Sort(blueCloneStego); //сортировка
                            float[,] histogramBlueStego = histogram(blueCloneStego, height, width); //гистограмма
                            float[,] matrix_adjacency_stego = matrixAdj(histogramBlueStego, blue_stego, height, width);
                            //exportExcelMatrix(matrix_adjacency_stego, matrix_adjacency_stego.GetLength(0), matrix_adjacency_stego.GetLength(1), 2 * height + matrix_adjacency.GetLength(0) + tableSigns.GetLength(0) + 4); //matrix_adjacency_stego.GetLength(0) = height

                            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(2);
                            double[] tableSigns_stego = signs(matrix_adjacency_stego);
                            exportExcelMatrix(tableSigns_stego, tableSigns_stego.GetLength(0), count_image + 1); //matrix_adjacency.GetLength(0) = height
                        }

                        if (checkBox_error.Checked == true)
                        {
                            float[,] X_stego = GAP(blue_stego, height, width); //предсказатель
                            width = width - 3; height = height - 2;

                            //экспорт матрицы предсказаний
                            //exportExcelMatrix(X, height, width, 0);

                            float[,] error_stego = errorsGap(blue_stego, X_stego, height, width); //ошибки предсказания    

                            //экспорт матрицы ошибок предсакзаний
                            //exportExcelMatrix(error, height, width, height*2-14);                                       

                            float[] blueClone_stego = Clone(error_stego, height, width); //запись в одномерный массив
                            Array.Sort(blueClone_stego); //сортировка
                            float[,] histogramBlue_stego = histogram(blueClone_stego, height, width); //гистограмма
                            float sum = 0;
                            for (int i = 0; i < histogramBlue_stego.Length / 2; i++)
                            {
                                sum += histogramBlue_stego[1, i];
                            }
                            exportExcelHis(histogramBlue_stego, 4, sum);
                        }

                        textBox3.Text += "\r\n" + Path.GetFileName(dirs[count_image]);
                        count_image++;
                        label1.Text = count_image.ToString() + "/" + dirs.Length.ToString();
                    }                    
                }
                
                excelapp.Visible = true; //открыть эксель с результатами
            }
            else
            {
                MessageBox.Show("Извлечение информации в переработке");
            }            
        }

        private float[,] Original_picture(Bitmap bmp, int height, int width)
        {
            float[,] blue = new float[width, height];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    blue[i, j] = pixel.B;
                    if (blue[i, j] == 255) blue[i, j] = 254;

                    //запись значений в эксель
                    //if (radioButtonEmbedding.Checked == true)
                    //{
                    //    excelcells = (Excel.Range)excelworksheet.Cells[j + 1, i + 1];
                    //    excelcells.Value2 = blue[i, j];
                    //}
                    //else continue;
                }
            }           

            return (blue);
        }
        private float[,] Stego_picture(Bitmap bmp, int height, int width)
        {
            float[,] blue = new float[width, height];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    blue[i, j] = pixel.B;                  
                }
            }
            return (blue);
        }

        private float[,] MED(float [,] blue, int height, int width)
        {
            float[,] X = new float[width - 1, height - 1];

            // C = myArr[i - 1, j - 1, l]
            // B = myArr[i, j - 1, l]
            // A = myArr[i - 1, j, l]
            // X[i, j, l]

            for (int j = 1; j < height; j++)
                for (int i = 1; i < width; i++)
                {
                    if (blue[i - 1, j - 1] >= max(blue[i - 1, j], blue[i, j - 1])) //if c>=max(a,b)
                    {
                        X[i - 1, j - 1] = min(blue[i - 1, j], blue[i, j - 1]);  //x = min(a,b)
                        //запись значений в эксель
                        //excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + height, i + 1];
                        //excelcells.Value2 = X[i - 1, j - 1];
                    }
                        
                    else
                    {
                        if (blue[i - 1, j - 1] <= min(blue[i - 1, j], blue[i, j - 1])) //if c<=min(a,b)
                        {
                            X[i - 1, j - 1] = max(blue[i - 1, j], blue[i, j - 1]);  //x = max(a,b)
                            //запись значений в эксель
                            //excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + height, i + 1];
                            //excelcells.Value2 = X[i - 1, j - 1];
                        }                            
                        else 
                        {
                            X[i - 1, j - 1] = blue[i - 1, j] + blue[i, j - 1] - blue[i - 1, j - 1]; //else x = a+b-c
                            //запись значений в эксель
                            //excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + height, i + 1];
                            //excelcells.Value2 = X[i - 1, j - 1];
                        } 
                    }
                }
            
            return (X);
        }
        //макс и мин для MED
        private float max(float A, float B)
        {
            if (A > B) return (A); else return (B);
        }
        private float min(float A, float B)
        {
            if (A < B) return (A); else return (B);
        }
        private float[,] GAP(float[,] blue, int height, int width)
        {           
            float[,] X = new float[width - 3, height - 2];

        //C = myArr[i - 1, j - 1, l]
        //B = myArr[i, j - 1, l]
        //A = myArr[i - 1, j, l]
        //D = myArr[i + 1, j - 1, l]
        //E = myArr[i - 2, j, l]
        //F = myArr[i, j - 2, l]
        //G = myArr[i + 1, j - 2, l]
        //X[i, j, l]

        float[,] dv = new float[width - 3, height - 2];
        float[,] dh = new float[width - 3, height - 2];
            for (int j = 2; j<height; j++)
                for (int i = 2; i<width - 1; i++)
                {
                    dh[i - 2, j - 2] = Math.Abs(blue[i - 1, j] - blue[i - 2, j]) + Math.Abs(blue[i, j - 1] - blue[i - 1, j - 1]) + Math.Abs(blue[i, j - 1] - blue[i + 1, j - 1]);
                    dv[i - 2, j - 2] = Math.Abs(blue[i - 1, j] - blue[i - 1, j - 1]) + Math.Abs(blue[i, j - 1] - blue[i, j - 2]) + Math.Abs(blue[i + 1, j - 1] - blue[i + 1, j - 2]);
                }

            for (int j = 2; j<height; j++)
                for (int i = 2; i<width - 1; i++)
                {                       
                        if (dv[i - 2, j - 2] - dh[i - 2, j - 2] > 80) //sharp horizontal edge
                            X[i - 2, j - 2] = blue[i - 1, j];
                        else
                        {
                            if (dv[i - 2, j - 2] - dh[i - 2, j - 2] < -80)
                            X[i - 2, j - 2] = blue[i, j - 1]; //sharp vertical edge
                            else
                            {                                
                                if (dv[i - 2, j - 2] - dh[i - 2, j - 2] > 32)
                                X[i - 2, j - 2] = Convert.ToSingle((((Convert.ToSingle(blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i - 1, j])) / 2; //horizontal edge
                                else
                                {
                                    if (dv[i - 2, j - 2] - dh[i - 2, j - 2] > 8)
                                    X[i - 2, j - 2] = Convert.ToSingle((3 * (Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i - 1, j])) / 4; //weak horizontal edge
                                    else
                                    {
                                        if (dv[i - 2, j - 2] - dh[i - 2, j - 2] < -32)
                                            X[i - 2, j - 2] = Convert.ToSingle(((Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i, j - 1])) / 2; //vertical edge
                                        else
                                        {
                                            if (dv[i - 2, j - 2] - dh[i - 2, j - 2] < -8)
                                                X[i - 2, j - 2] = Convert.ToSingle((3 * (Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i, j - 1])) / 4; //weak vertical edge
                                            else X[i - 2, j - 2] = (Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4); //smooth area
                                        }                                    
                                    }                                    
                                }                                               
                            }
                        }                  
                }
            width = width - 3;
            height = height - 2;
            return (X);
        }

        private float[,] errorsMed(float[,] blue, float[,] X, int height, int width)
        {
            float[,] error = new float[width, height];
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    error[i, j] = blue[i + 1, j + 1] - X[i, j];                    
                }

            ////гистограмма ошибок предсказания оригинального
            //float[] original_gista = Clone(error, height, width);
            //Array.Sort(original_gista); //сортировка
            //float[,] histogramOriginal = histogram(original_gista, height, width); //гистограмма         

            //for (int i = 0; i < 2; i++)
            //    for (int j = 0; j < histogramOriginal.Length / 2; j++)
            //    {
            //        excelcells = (Excel.Range)excelworksheet.Cells[i + height + 2, j + 1];
            //        if (i == 0) excelcells.Value2 = histogramOriginal[i, j];
            //        else excelcells.Value2 = histogramOriginal[i, j] / blue.Length;
            //    }

            return (error);
        }
        private float[,] errorsGap(float[,] blue, float[,] X, int height, int width)
        {
            float[,] error = new float[width, height];
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    error[i, j] = blue[i + 2, j + 2] - X[i, j];                    
                }
            return (error);
        }

        private float[] Clone(float[,] error, int height, int width)
        {
            float[] blueClone = new float[width*height];
            int b = 0;

            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    blueClone[b] = error[i, j];
                    b++;
                }
            return (blueClone);
        }

        private float[,] histogram(float[] blueClone, int height, int width)
        {
            count = 1;
            //Длина двумерного массива
            for (int m = 0; m < width * height - 1; m++)
            {
                if (blueClone[m] != blueClone[m + 1]) count++;
            };
            //шапка двумерного массива
            float[] Color = new float[count];
            int n = 1;
            Color[0] = blueClone[0];
            for (int m = 1; m < width * height; m++)
            {
                if (blueClone[m - 1] != blueClone[m])
                {
                    Color[n] = blueClone[m]; n++;
                }
                else continue;
            };
            //Запись всё в двумерный массив
            int N = 1;
            float[,] Histogram = new float[2, count];
            for (int j = 0; j < 2; j++)
                for (int i = 0; i < count; i++)
                {
                    if (j == 0) Histogram[j, i] = Color[i]; //заполнение шапки из массива Color
                    else
                    {
                        for (int q = 0; q < width * height - 1; q++)
                            if (blueClone[q] == blueClone[q + 1]) { N++; Histogram[j, i] = N; }
                            else
                            {
                                if (q == width * height - 2)
                                { Histogram[j, i] = N; Histogram[j, i + 1] = 1; i++; }    //если в конце 2 разных числа, например, 0 и 122, чтобы в предпоследнюю ушло количество 0, а в последнюю записать 1     
                                else
                                {
                                    Histogram[j, i] = N;
                                    N = 1;
                                    i++;
                                }
                            }
                    }
                }
            Array.Clear(Color, 0, count);
            return (Histogram);
        }

        private double searchNumEmbed(float[,] histogramBlue)
        {
            int sum = 0;
            double chislo = 0; //запоминает то число, в которое встраивает
            if (String.IsNullOrEmpty(textBox_num.Text.ToString())) //автоматически находит число
            {
                for (int j = 1; j < 2; j++)
                    for (int i = 0; i < count; i++)
                    {//нашли хотя бы 1 число -> выходим из цикла
                        if (histogramBlue[1, i] >= 16 + textBox1.Text.Length * 8) { chislo = histogramBlue[0, i]; sum++; break; } //проверка на количество пикселей для встраивания и чтобы был не белый
                        else continue;
                    }
            }
            else //пользователь задает число
            {
                try
                {
                    chislo = Convert.ToDouble(textBox_num.Text.ToString());
                    
                    for (int j = 1; j < 2; j++)
                        for (int i = 0; i < count; i++)
                        {
                            //нашли число,которое ввели -> выходим из цикла
                            //if (histogramBlue[0, i] == chislo && histogramBlue[1, i] >= 16 + textBox1.Text.Length * 8 && histogramBlue[0, i] != 3000) //проверка на количество пикселей для встраивания и чтобы был не белый
                            if (histogramBlue[0, i] == chislo && histogramBlue[0, i] != 3000)
                            {
                                //генерация строки заданной длины
                                int generate_text = Convert.ToInt32(Math.Floor((histogramBlue[1, i] - 16) / 8));
                                if (generate_text > 65535) generate_text = 65535;
                                textBox1.Text = RandomString(generate_text);
                                sum++; break;
                            } 
                            else
                            {
                                if (i == count - 1) throw new Exception();
                                continue;
                            }
                        }
                }
                catch
                {
                    MessageBox.Show("В такое значение встроить нельзя! Измените значение числа для встраивания!", "Ошибка",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            if (sum == 0)
            {
                MessageBox.Show("Картинка слишком мала или много белого цвета, чтобы встроить данный текст! Либо измените картинку, либо уменьшите текст", "Ошибка",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            else
            {
                //Array.Clear(histogramBlue, 0, count * 2);
                return (chislo);
            }
        }

        private float[,] Shift(float[,] error, double numEmbed, int height, int width)
        {
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    if (error[i, j] >= numEmbed + 1) error[i, j] = error[i, j] + 1;                    
                }
            return (error);
        }

        private float[,] Build(float[,] error, double numEmbed, int height, int width)
        {
            byte[] strBytes = Encoding.ASCII.GetBytes(textBox1.Text.ToString());
            BitArray bytes = new BitArray(strBytes);
            //ищем размер
            string BinaryCode = Convert.ToString(textBox1.Text.Length, 2); //переводим в ДСС                       
            string number16 = BinaryCode.ToString().PadLeft(16, '0'); //заполняем нулями
            int[] f = number16.Select(ch => int.Parse(ch.ToString())).ToArray(); //массив в int

            //определяет конечный+1 элемент записи размера, т.е. начало записи текста
            //int countWidth = 0; int countHeight = 0;
            //счетчик строки размера
            int count = 0;
            int count2 = 0;

            //записываем текст в виде 8-битного кода
            bool[,] mass = new bool[textBox1.Text.Length, 8]; //строку в биты

            for (int k = 0; k < textBox1.Text.Length * 8; k++)
                for (int i = 0; i < textBox1.Text.Length; i++) //строки
                    for (int j = 7; j >= 0; j--) //столбцы
                    {
                        mass[i, j] = bytes.Get(k); k++;
                    }

            int[] ascii = new int[textBox1.Text.Length * 8];
            for (int count3 = 0; count3 < textBox1.Text.Length * 8; count3++)
                for (int i = 0; i < textBox1.Text.Length; i++)
                    for (int j = 0; j < 8; j++) //столбцы
                    {
                        ascii[count3] = Convert.ToInt16(mass[i, j]);
                        count3++;
                    }

            //записываем в массив

            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    if (count2 == textBox1.Text.Length * 8)
                    {
                        goto exit;
                    }
                    if (error[i, j] == numEmbed && count < 16)
                    {
                        error[i, j] = error[i, j] + f[count]; count++;                        
                    }
                    else
                    {
                        if (error[i, j] == numEmbed && count >= 16)
                        {
                            error[i, j] = error[i, j] + Convert.ToInt16(ascii[count2]); count2++;                            
                        }
                        else continue;
                    }
                }

            exit:       

            Array.Clear(strBytes, 0, strBytes.Length);
            Array.Clear(f, 0, 16);
            Array.Clear(ascii, 0, textBox1.Text.Length * 8);            
            return (error);
        }

        private float[,] PictureFinalMED(float[,] error, int height, int width, float[,] blue, float[,] X)
        {
            width = width + 1; height = height + 1;
            Bitmap picture = new Bitmap(width, height);
            float[,] mass = new float[width, height];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (i == 0 || j == 0)
                        mass[i, j] = blue[i, j];
                    else mass[i, j] = Convert.ToInt32(error[i - 1, j - 1] + X[i - 1, j - 1]);                    
                }
            }        
                        
            //pictureBoxFinal3.Image = picture;            
            return (mass);
        }

        private float[,] PictureFinalGAP(float[,] error, int height, int width, float[,] blue, float[,] X)
        {
            width = width + 3; height = height + 2;
            Bitmap picture = new Bitmap(width, height);
            float[,] mass = new float[width, height];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (i < 2 || j < 2 || i == width - 1) mass[i, j] = blue[i, j];
                    else mass[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);

                    //////запись значений в эксель
                    ////excelcells = (Excel.Range)excelworksheet.Cells[j + 2 * height + 2, i + 1];
                    //excelcells = (Excel.Range)excelworksheet.Cells[j + height + 2, i + 2];
                    //excelcells.Value2 = mass[i, j];
                    //excelcells = (Excel.Range)excelworksheet.Cells[j + height * 2 + 3, i + 2];
                    //if (bmp.GetPixel(i, j).B == 255) excelcells.Value2 = bmp.GetPixel(i, j).B - 1 - mass[i, j];
                    //else excelcells.Value2 = bmp.GetPixel(i, j).B - mass[i, j];
                }
            }

            //for (int j = 0; j < height; j++)
            //{
            //    for (int i = 0; i < width; i++)
            //    {
            //        picture.SetPixel(i, j, Color.FromArgb(bmp.GetPixel(i, j).R, bmp.GetPixel(i, j).G, mass[i, j]));
            //    }
            //}

            return (mass);
        }

        private void RecoveryImage(float [,] blue_stego, int height, int width, Bitmap bmp, string name, double numEmbed)
        {
            Bitmap picture = new Bitmap(width, height);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    picture.SetPixel(i, j, Color.FromArgb(bmp.GetPixel(i, j).R, bmp.GetPixel(i, j).G, Convert.ToInt32(blue_stego[i, j])));
                }
            }

            picture.Save("D:\\study\\gpo\\StegoImage\\" + name + "_stego_" + numEmbed + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }

        //вставляем картинку
        string fileImage = "";
        private void buttonAddStego_Click(object sender, EventArgs e)
        {
            pictureBoxOriginal.Image = null;
            pictureBoxOriginal.Invalidate();
            
            textBox2.Text = "";
            

            openFileDialog1.Title = "Укажите файл для фото";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileImage = openFileDialog1.FileName;
                try
                {
                    pictureBoxOriginal.Image = new
                    Bitmap(openFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("Выбран не тот формат файла", "Ошибка",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else fileImage = "";
        }

        //извлечение
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBoxOriginal.Image);
            int height = bmp.Height;
            int width = bmp.Width;
            int number = Convert.ToInt32(textBox_num2.Text.ToString()); //число, из которого извлекаем

            float[,] blue = Stego_picture(bmp, height, width);

            //открыть эксель на запись
            excelapp = new Excel.Application();
            excelapp.SheetsInNewWorkbook = 1;
            excelapp.Workbooks.Add(Type.Missing);
            excelappworkbooks = excelapp.Workbooks;
            excelappworkbook = excelappworkbooks[1];
            excelappworkbook.Saved = true;
            excelsheets = excelappworkbook.Worksheets;
            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);

            //exportExcelImage(blue, height, width, 0);
            

            if (radioButtonMed.Checked == true) //MED
            {
                float[,] X = new float[width - 1, height - 1]; //предсказанные пиксели
                float[,] error = new float[width - 1, height - 1]; //ошибки предсказания
                int[] mass = new int[16];
                int size = 0;

                int k = 0;
                for (int j = 1; j < height; j++)
                    for (int i = 1; i < width; i++)
                    {
                        if (k == 16) //считаем размер
                        {
                            size = SizeDetermination(mass);
                            Array.Resize(ref mass, 16 + size * 8);
                        }
                        if (k == 16 + size * 8) goto end; //выход из цикла, чтобы дальше не смотрел
                        
            
                        if (blue[i - 1, j - 1] >= max(blue[i - 1, j], blue[i, j - 1])) //if c>=max(a,b)
                        {
                            X[i - 1, j - 1] = min(blue[i - 1, j], blue[i, j - 1]);  //x = min(a,b)
                            //excelcells = (Excel.Range)excelworksheet.Cells[j, i];
                            //excelcells.Value2 = X[i - 1, j - 1];
                            error[i - 1, j - 1] = blue[i, j] - X[i - 1, j - 1];

                            if (error[i - 1, j - 1] == number) //встроен 0
                            {
                                mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] + X[i - 1, j - 1]);  //I = I'+e (0)
                            }
                            else
                            {
                                if (error[i - 1, j - 1] == number + 1) //встроена 1
                                {
                                    mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] - 1 + X[i - 1, j - 1]); // (1)
                                }
                                else
                                {
                                    if (error[i - 1, j - 1] > number + 1)
                                        { blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] - 1 + X[i - 1, j - 1]); } // (2 и более)
                                    else blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] + X[i - 1, j - 1]); // (-1 и менее)
                                }
                            }
                        }

                        else
                        {
                            if (blue[i - 1, j - 1] <= min(blue[i - 1, j], blue[i, j - 1])) //if c<=min(a,b)
                            {
                                X[i - 1, j - 1] = max(blue[i - 1, j], blue[i, j - 1]);  //x = max(a,b)
                                //excelcells = (Excel.Range)excelworksheet.Cells[j, i];
                                //excelcells.Value2 = X[i - 1, j - 1];
                                error[i - 1, j - 1] = blue[i, j] - X[i - 1, j - 1];

                                if (error[i - 1, j - 1] == number) //встроен 0
                                {
                                    mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] + X[i - 1, j - 1]);  //I = I'+e (0)
                                }
                                else
                                {
                                    if (error[i - 1, j - 1] == number + 1) //встроена 1
                                    {
                                        mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] - 1 + X[i - 1, j - 1]); // (1)
                                    }
                                    else
                                    {
                                        if (error[i - 1, j - 1] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] - 1 + X[i - 1, j - 1]); } // (2 и более)
                                        else blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] + X[i - 1, j - 1]); // (-1 и менее)
                                    }
                                }//excelcells.Value2 = X[i - 1, j - 1];
                            }
                            else
                            {
                                X[i - 1, j - 1] = blue[i - 1, j] + blue[i, j - 1] - blue[i - 1, j - 1]; //else x = a+b-c
                                //excelcells = (Excel.Range)excelworksheet.Cells[j, i];
                                //excelcells.Value2 = X[i - 1, j - 1];
                                error[i - 1, j - 1] = blue[i, j] - X[i - 1, j - 1];

                                if (error[i - 1, j - 1] == number) //встроен 0
                                {
                                    mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] + X[i - 1, j - 1]);  //I = I'+e (0)
                                }
                                else
                                {
                                    if (error[i - 1, j - 1] == number + 1) //встроена 1
                                    {
                                        mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] - 1 + X[i - 1, j - 1]); // (1)
                                    }
                                    else
                                    {
                                        if (error[i - 1, j - 1] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] - 1 + X[i - 1, j - 1]); } // (2 и более)
                                        else blue[i, j] = Convert.ToInt32(error[i - 1, j - 1] + X[i - 1, j - 1]); // (-1 и менее)
                                    }
                                }
                            }
                        }
                    }
                
                end:
                excelapp.Visible = true; //открыть эксель с результатами
                //побуквенное извлечение информации
                string[] text = new string[size];
                int count = 16;
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        text[i] += mass[count]; //8 бит в одну строку
                        count++;
                    }

                byte[] letter = new byte[size]; //массив с ASCII символами
                for (int i = 0; i < size; i++)
                {
                    letter[i] = Convert.ToByte(text[i], 2); //ASCII код : 2сс -> 10сс
                }

                string message = Encoding.ASCII.GetString(letter);
                textBox2.Text = message.ToString();
                

            }
            else //GAP
            {
                float[,] X = new float[width - 3, height - 2]; //предсказанные пиксели
                float[,] error = new float[width - 3, height - 2]; //ошибки предсказания
                
                float[] mass = new float[16]; //для размера, потом изменится
                int k = 0; //счётчик для mass
                float[] result = new float[3]; //временный для хранения текущих значений mass, k, myarr
                int size = 2; //чтобы выйти из цикла, а то while долго, да и size изменится

                float[,] dv = new float[width - 3, height - 2];
                float[,] dh = new float[width - 3, height - 2];
                
                for (int j = 2; j < height; j++)
                    for (int i = 2; i < width - 1; i++)
                    {
                        dh[i - 2, j - 2] = Math.Abs(blue[i - 1, j] - blue[i - 2, j]) + Math.Abs(blue[i, j - 1] - blue[i - 1, j - 1]) + Math.Abs(blue[i, j - 1] - blue[i + 1, j - 1]);
                        dv[i - 2, j - 2] = Math.Abs(blue[i - 1, j] - blue[i - 1, j - 1]) + Math.Abs(blue[i, j - 1] - blue[i, j - 2]) + Math.Abs(blue[i + 1, j - 1] - blue[i + 1, j - 2]);
                    }


                for (int j = 2; j < height; j++)
                    for (int i = 2; i < width - 1; i++)
                    {
                        if (k == 16 + size * 8) break; //выход из цикла, чтобы дальше не смотрел
                        if (k == 16) // размер
                        {
                            size = SizeDetermination(mass);
                            Array.Resize(ref mass, 16 + size * 8);
                        }
                        if (dv[i - 2, j - 2] - dh[i - 2, j - 2] > 80) //sharp horizontal edge
                        {
                            X[i - 2, j - 2] = blue[i - 1, j];

                            //result = Detection(blue[i, j], X[i - 2, j - 2], number, mass[k], k);
                            //mass[k] = result[0]; k = Convert.ToInt32(result[1]); blue[i, j] = Convert.ToInt32(result[2]);


                            error[i - 2, j - 2] = blue[i, j] - X[i - 2, j - 2];

                            if (error[i - 2, j - 2] == number) //встроен 0
                            {
                                mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);  //I = I'+e (0)
                            }
                            else
                            {
                                if (error[i - 2, j - 2] == number + 1) //встроена 1
                                {
                                    mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); // (1)
                                }
                                else
                                {
                                    if (error[i - 2, j - 2] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); } // (2 и более)
                                    else blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]); // (-1 и менее)
                                }
                            }

                        }
                        else
                        {
                            if (dv[i - 2, j - 2] - dh[i - 2, j - 2] < -80)
                            {
                                X[i - 2, j - 2] = blue[i, j - 1]; //sharp vertical edge


                                //result = Detection(blue[i, j], X[i - 2, j - 2], number, mass[k], k);
                                //mass[k] = result[0]; k = Convert.ToInt32(result[1]); blue[i, j] = Convert.ToInt32(result[2]);


                                error[i - 2, j - 2] = blue[i, j] - X[i - 2, j - 2];

                                if (error[i - 2, j - 2] == number) //встроен 0
                                {
                                    mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);  //I = I'+e (0)
                                }
                                else
                                {
                                    if (error[i - 2, j - 2] == number + 1) //встроена 1
                                    {
                                        mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); // (1)
                                    }
                                    else
                                    {
                                        if (error[i - 2, j - 2] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); } // (2 и более)
                                        else blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]); // (-1 и менее)
                                    }
                                }

                            }
                            else
                            {
                                //X[i - 2, j - 2] = (myArr[i - 1, j, 2] + myArr[i, j - 1, 2]) / 2 + (myArr[i + 1, j - 1, 2] - myArr[i - 1, j - 1, 2]) / 4; //smooth area

                                if (dv[i - 2, j - 2] - dh[i - 2, j - 2] > 32)
                                {
                                    X[i - 2, j - 2] = Convert.ToSingle(((Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i - 1, j])) / 2; //horizontal edge


                                    //result = Detection(blue[i, j], X[i - 2, j - 2], number, mass[k], k);
                                    //mass[k] = result[0]; k = Convert.ToInt32(result[1]); blue[i, j] = Convert.ToInt32(result[2]);

                                    error[i - 2, j - 2] = blue[i, j] - X[i - 2, j - 2];

                                    if (error[i - 2, j - 2] == number) //встроен 0
                                    {
                                        mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);  //I = I'+e (0)
                                    }
                                    else
                                    {
                                        if (error[i - 2, j - 2] == number + 1) //встроена 1
                                        {
                                            mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); // (1)
                                        }
                                        else
                                        {
                                            if (error[i - 2, j - 2] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); } // (2 и более)
                                            else blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]); // (-1 и менее)
                                        }
                                    }

                                }
                                else
                                {
                                    if (dv[i - 2, j - 2] - dh[i - 2, j - 2] > 8)
                                    {
                                        X[i - 2, j - 2] = Convert.ToSingle((3 * (Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i - 1, j])) / 4; //weak horizontal edge


                                        //result = Detection(blue[i, j], X[i - 2, j - 2], number, mass[k], k);
                                        //mass[k] = result[0]; k = Convert.ToInt32(result[1]); blue[i, j] = Convert.ToInt32(result[2]);


                                        error[i - 2, j - 2] = blue[i, j] - X[i - 2, j - 2];

                                        if (error[i - 2, j - 2] == number) //встроен 0
                                        {
                                            mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);  //I = I'+e (0)
                                        }
                                        else
                                        {
                                            if (error[i - 2, j - 2] == number + 1) //встроена 1
                                            {
                                                mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); // (1)
                                            }
                                            else
                                            {
                                                if (error[i - 2, j - 2] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); } // (2 и более)
                                                else blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]); // (-1 и менее)
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (dv[i - 2, j - 2] - dh[i - 2, j - 2] < -32)
                                        {
                                            X[i - 2, j - 2] = Convert.ToSingle(((Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i, j - 1])) / 2; //vertical edge


                                            //result = Detection(blue[i, j], X[i - 2, j - 2], number, mass[k], k);
                                            //mass[k] = result[0]; k = Convert.ToInt32(result[1]); blue[i, j] = Convert.ToInt32(result[2]);

                                            error[i - 2, j - 2] = blue[i, j] - X[i - 2, j - 2];

                                            if (error[i - 2, j - 2] == number) //встроен 0
                                            {
                                                mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);  //I = I'+e (0)
                                            }
                                            else
                                            {
                                                if (error[i - 2, j - 2] == number + 1) //встроена 1
                                                {
                                                    mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); // (1)
                                                }
                                                else
                                                {
                                                    if (error[i - 2, j - 2] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); } // (2 и более)
                                                    else blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]); // (-1 и менее)
                                                }
                                            }

                                        }
                                        else
                                        {
                                            if (dv[i - 2, j - 2] - dh[i - 2, j - 2] < -8)
                                            {
                                                X[i - 2, j - 2] = Convert.ToSingle((3 * (Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4) + blue[i, j - 1])) / 4; //weak vertical edge



                                                //result = Detection(blue[i, j], X[i - 2, j - 2], number, mass[k], k);
                                                //mass[k] = result[0]; k = Convert.ToInt32(result[1]); blue[i, j] = Convert.ToInt32(result[2]);

                                                error[i - 2, j - 2] = blue[i, j] - X[i - 2, j - 2];

                                                if (error[i - 2, j - 2] == number) //встроен 0
                                                {
                                                    mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);  //I = I'+e (0)
                                                }
                                                else
                                                {
                                                    if (error[i - 2, j - 2] == number + 1) //встроена 1
                                                    {
                                                        mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); // (1)
                                                    }
                                                    else
                                                    {
                                                        if (error[i - 2, j - 2] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); } // (2 и более)
                                                        else blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]); // (-1 и менее)
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                X[i - 2, j - 2] = (Convert.ToSingle((blue[i - 1, j] + blue[i, j - 1])) / 2 + Convert.ToSingle((blue[i + 1, j - 1] - blue[i - 1, j - 1])) / 4);
                                                error[i - 2, j - 2] = blue[i, j] - X[i - 2, j - 2];

                                                if (error[i - 2, j - 2] == number) //встроен 0
                                                {
                                                    mass[k] = 0; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]);  //I = I'+e (0)
                                                }
                                                else
                                                {
                                                    if (error[i - 2, j - 2] == number + 1) //встроена 1
                                                    {
                                                        mass[k] = 1; k++; blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); // (1)
                                                    }
                                                    else
                                                    {
                                                        if (error[i - 2, j - 2] > number + 1) { blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] - 1 + X[i - 2, j - 2]); } // (2 и более)
                                                        else blue[i, j] = Convert.ToInt32(error[i - 2, j - 2] + X[i - 2, j - 2]); // (-1 и менее)
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                

                //побуквенное извлечение информации
                string[] text = new string[size];
                int count = 16;
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        text[i] += mass[count]; //8 бит в одну строку
                        count++;
                    }
                byte[] letter = new byte[size]; //массив с ASCII символами
                for (int i = 0; i < size; i++)
                {
                    letter[i] = Convert.ToByte(text[i], 2); //ASCII код : 2сс -> 10сс
                }
                string message = Encoding.ASCII.GetString(letter);
                textBox2.Text = message.ToString();
              
            }
        }
        
        //извлечение размера
        private int SizeDetermination(int[] mass)
        {
            string size_str = "";
            for (int i = 0; i < 16; i++)
            {
                size_str += mass[i]; //первые 16 0 и 1 в одну строку
            }
            int size = Convert.ToInt32(size_str, 2); //размер в 10 сс
            return size;
        }       
        private int SizeDetermination(float[] mass)
        {
            string size_str = "";
            for (int i = 0; i < 16; i++)
            {
                size_str += mass[i]; //первые 16 0 и 1 в одну строку
            }
            int size = Convert.ToInt32(size_str, 2); //размер в 10 сс
            return size;
        }

        //извлечение (для GAP)
        private float[] Detection(float myArr, float X, float number, float mass, int k)
        {
            float[] result = new float[3];
            float error = myArr - X;
            if (error == number)
            {
                mass = 0; k++; myArr = Convert.ToInt32(error + X);  //I = I'+e (0)
            }
            else
            {
                if (error == number + 1)
                {
                    mass = 1; k++; myArr = Convert.ToInt32(error - 1 + X); // (1)
                }
                else
                {
                    if (error > number + 1) { myArr = Convert.ToInt32(error - 1 + X); } // (2 и более)
                    else myArr = Convert.ToInt32(error + X); // (-1 и менее)
                }
            }
            result[0] = mass;
            result[1] = k;
            result[2] = myArr;
            return (result);
        }

        private void exportExcelHis(float [,] mass, int par, float sum)
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < mass.Length / 2; j++)
                {
                    excelcells = (Excel.Range)excelworksheet.Cells[i + par + 1, j + 1];
                    if (i == 0) excelcells.Value2 = mass[i, j];
                    else excelcells.Value2 = mass[i, j]/sum;
                }            
        }

        private void exportExcelMatrix(float[,] mass, int height, int width, int par)
        {
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + par, i + 1];
                    excelcells.Value2 = mass[i, j];
                }
        }

        private void exportExcelMatrix2(float[,] mass, int height, int width, int par)
        {
            for (int j = 0; j < height; j++)
            {
                excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + par, 3];
                excelcells.Value2 = mass[1, j];
            }            
                                   
        }

        private void exportExcelMatrix(double[] mass, int width, int par)
        {            
            for (int j = 0; j < 1; j++)
                for (int i = 0; i < width; i++)
                {
                    excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + par, i + 2];
                    excelcells.Value2 = mass[i];
                }
        }

        private void exportExcelImage(float[,] mass, int height, int width, int par)
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {             
                    ////запись значений в эксель                    
                    excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + par, i + 1];
                    excelcells.Value2 = mass[i, j];                    
                }
            }
        }

        private float[,] matrixAdj(float [,] histogram, float [,] massOriginal, int height, int width)
        {
            float[,] matrix_adjacency = new float[histogram.Length / 2 + 1, histogram.Length / 2 + 1];
            int alfa = 0, d = 1;            
            
            for (int j = 0; j < histogram.Length / 2 + 1; j++)
                for (int i = 0; i < histogram.Length / 2 + 1; i++)
                {
                    if (i == 0 && j == 0) matrix_adjacency[0, 0] = 3000;
                    else
                    {
                        if (j == 0 && i != 0)
                        {
                            matrix_adjacency[0, i] = histogram[0, i - 1];
                            //excelcells = (Excel.Range)excelworksheet.Cells[1 + height + 1, i + 1]; ////////////
                            //excelcells.Value2 = matrix_adjacency[0, i];
                        }
                        else
                        {
                            if (i == 0 && j != 0)
                            {
                                matrix_adjacency[j, 0] = histogram[0, j - 1];
                                //excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + height + 1, 1]; ////////////
                                //excelcells.Value2 = matrix_adjacency[j, 0];
                            }                            
                        }                        
                    }                    
                }
            
            float[] capHistogram = new float[histogram.Length / 2];
            for (int i = 0; i < histogram.Length / 2; i++)
            {
                capHistogram[i] = histogram[0, i];
            }
                        
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width - 1; i++)
                {
                    //float a = massOriginal[i, j];
                    //float b = massOriginal[i + 1, j];
                    int index_h = Array.IndexOf(capHistogram, massOriginal[i, j]);
                    int index_w = Array.IndexOf(capHistogram, massOriginal[i + 1, j]);
                    matrix_adjacency[index_h + 1, index_w + 1] += 1;                    
                }
            }

           
            return (matrix_adjacency);
        }

        private double [] signs(float[,] matrix_adjacency)
        {
            double energy = 0; // Энергия T1
            double entropy = 0; //Энтропия Т2
            double uniformity = 0; // Однородность Т3
            double middle_i = 0; // Среднее по i Т5
            double middle_j = 0; // Среднее по j Т6
            double dispersion_i = 0; // дисперсия по i T7
            double dispersion_j = 0; // Дисперсия по j T8
            double covariance = 0; // Ковариация T9
            double correlation = 0; // Корреляция Т10

            for (int i = 1; i < matrix_adjacency.GetLength(0); i++)
                for (int j = 1; j < matrix_adjacency.GetLength(1); j++)
                {
                    energy += Math.Pow(Convert.ToDouble(matrix_adjacency[i, j]), 2);
                    if (matrix_adjacency[i, j] == 0) continue;
                    else entropy += Math.Log(Convert.ToDouble(matrix_adjacency[i, j]), 2) * matrix_adjacency[i, j];
                    uniformity += matrix_adjacency[i, j] / (1 + Math.Abs(matrix_adjacency[i, 0] - matrix_adjacency[0, j]));
                    middle_i += matrix_adjacency[i, j] * matrix_adjacency[i, 0];
                    middle_j += matrix_adjacency[i, j] * matrix_adjacency[0, j];
                }

            for (int i = 1; i < matrix_adjacency.GetLength(0); i++)
                for (int j = 1; j < matrix_adjacency.GetLength(1); j++)
                {
                    dispersion_i += Math.Pow(matrix_adjacency[i, 0] - middle_i, 2) * matrix_adjacency[i, j];
                    dispersion_j += Math.Pow(matrix_adjacency[0, j] - middle_j, 2) * matrix_adjacency[i, j];
                    covariance += (matrix_adjacency[i, 0] - middle_i) * (matrix_adjacency[0, j] - middle_j) * matrix_adjacency[i, j];                    
                }

            for (int i = 1; i < matrix_adjacency.GetLength(0); i++)
                for (int j = 1; j < matrix_adjacency.GetLength(1); j++)
                {
                    correlation += (matrix_adjacency[i, 0] - middle_i) * (matrix_adjacency[0, j] - middle_j) * matrix_adjacency[i, j] / dispersion_i;
                }


            //double[,] table_signs = { {1, 2, 3, 5, 6, 7, 8, 9, 10}, { energy, entropy, uniformity, middle_i, middle_j, dispersion_i, dispersion_j, covariance, correlation}};
            double[] table_signs = { energy, entropy, uniformity, middle_i, middle_j, dispersion_i, dispersion_j, covariance, correlation };

            return table_signs;        
                    
        }

        //генерация строки заданной длины
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                //Генерируем число являющееся латинским символом в юникоде
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                ch = Convert.ToChar(Convert.ToInt32(119));//w
                //Конструируем строку со случайно сгенерированными символами
                builder.Append(ch);
            }
            return builder.ToString();
        }

        private string[,] Last_Bits(float[,] blue, int height, int width)
        {
            string[,] last_bits = new string[width, height];            
            int quantity_bits = 3; //сколько нужно последних бит

            for (int j = 0; j < height; j++)
                for (int l = 0; l < width; l++)
                {
                    BitArray b = new BitArray(new byte[] { (byte)blue[l, j] });
                    int[] bits = b.Cast<bool>().Select(bit => bit ? 1 : 0).ToArray(); //каждое байтовое число в массив int
                    Array.Resize(ref bits, quantity_bits); //отсекаем ненужное
                    int[] r_bits = new int[quantity_bits];
                    for (int i = quantity_bits - 1; i >= 0; i--)
                    {
                        r_bits[quantity_bits - 1 - i] = bits[i]; //т.к. биты в обратном порядке, то записываем с конца
                        last_bits[l, j] += r_bits[quantity_bits - 1 - i]; //склеиваем в 1 строку для байтового числа
                    }
                }           
            return last_bits;
        }

        private void ExportExcelLastBits(string [,] last_bits, int height, int width, StreamWriter sw)
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    sw.Write(last_bits[i, j] + "\t");
                    ////запись значений в эксель                    
                    //excelcells = (Excel.Range)excelworksheet.Cells[j + 1 + par, i + 1];
                    //excelcells.Value2 = last_bits[i, j];
                }
                sw.Write("\n");
            }
        }

        private float[,] Last_Bits_pct(float[,] histogram, int width)
        {
            float sum = 0;
            for (int i = 0; i < width; i++)
            {
                sum += histogram[1, i];
            }

            for (int i = 0; i < width; i++)
            {
                histogram[1, i] = histogram[1, i] / sum * 100;
            }
            return histogram;
        }

        private double RMSE(float[,] histogram, float[,] histogram_stego, int quantity) //высота гистограммы, а не рисунка
        {
            double[] arr = new double[quantity];
            double sum = 0;
            for (int i = 0; i < quantity; i++)
            {
                arr[i] = Math.Pow(histogram_stego[1, i] - histogram[1, i], 2);
                sum += arr[i];
            }        
            double MSE = sum / quantity;
            double var_RMSE = Math.Sqrt(MSE);
            return var_RMSE;
        }

        private void LastBitsPicture(string [,] last_bits, int height, int width, string name, double numEmbed, string par)
        {
            Bitmap picture = new Bitmap(width, height);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (Convert.ToInt32(last_bits[i,j]) == 0) picture.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    else picture.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                }
            }

            picture.Save("D:\\study\\gpo\\БД изображений\\standard_test_images" + name + "_"+ par + "_" + numEmbed + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
