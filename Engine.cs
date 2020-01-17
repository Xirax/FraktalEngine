using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FractalEngine
{
    public partial class Engine : Form
    {
        Color ButtonTextColor = Color.FromArgb(255, 255, 128);
        Color ButtonBackColor = Color.SlateGray;

        String drawType = "";
        String controlType = "";

        double duplicates = 0;

        FraktalDrawer fraktalDrawer;

        public int thickness;
        public Color color;

        bool showOnce = false;
        bool triangle = false;

        void disableTriangle()
        {
            startX.Text = "X:";
            startY.Text = "Y: ";
            triangleX.Visible = false;
            triangleY.Visible = false;
            triangleXinput.Visible = false;
            triangleYinput.Visible = false;
        }

        void buttonsToDark()
        {
            squareButton.Visible = true;
            lineButton.Visible = true;
            triangleButton.Visible = true;
            circleButton.Visible = true;
            curveButton.Visible = true;
        }

        void controlsToDark()
        {
            moveControl.Visible = true;
            resizeControl.Visible = true;
            rotateControl.Visible = true;
            pivotControl.Visible = true;
            duplicateControl.Visible = true;
        }

        public Engine()
        {

            InitializeComponent();
            fraktalDrawer = new FraktalDrawer(drawPanel);
        }


        private void renderButton_Click(object sender, EventArgs e)
        {
            int steps;
            int.TryParse(param1input.Text, out steps);

            fraktalDrawer.renderFraktal(steps, fraktalDrawer.getCords(), fraktalDrawer.getDrawings());
        }


        private void squareButton_Click(object sender, EventArgs e)
        {
            buttonsToDark();
            squareButton.Visible = false;

            drawType = "RECT";

            if (!showOnce)
            {
                startX.Visible = true;
                startY.Visible = true;
                endX.Visible = true;
                endY.Visible = true;

                startXinput.Visible = true;
                startYinput.Visible = true;
                endXinput.Visible = true;
                endYinput.Visible = true;
                acceptButton.Visible = true;
            }

            if (triangle) { disableTriangle(); triangle = false; }
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            buttonsToDark();
            lineButton.Visible = false;

            drawType = "LINE";
            endX.Text = "END X:";
            endY.Text = "END Y:";

            if (!showOnce)
            {
                startX.Visible = true;
                startY.Visible = true;
                endX.Visible = true;
                endY.Visible = true;

                startXinput.Visible = true;
                startYinput.Visible = true;
                endXinput.Visible = true;
                endYinput.Visible = true;
                acceptButton.Visible = true;
            }

            if (triangle) { disableTriangle(); triangle = false; }
        }

        private void triangleButton_Click(object sender, EventArgs e)
        {
            buttonsToDark();
            triangleButton.Visible = false;

            drawType = "TRIANG";
            startX.Text = "AX:";
            startY.Text = "AY:";

            endX.Text = "BX:";
            endY.Text = "BY:";

            triangleX.Text = "CX:";
            triangleY.Text = "CY";

            if (!showOnce)
            {
                startX.Visible = true;
                startY.Visible = true;
                endX.Visible = true;
                endY.Visible = true;

                startXinput.Visible = true;
                startYinput.Visible = true;
                endXinput.Visible = true;
                endYinput.Visible = true;
                acceptButton.Visible = true;
            }

            triangleX.Visible = true;
            triangleY.Visible = true;
            triangleXinput.Visible = true;
            triangleYinput.Visible = true;
            triangle = true;
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            buttonsToDark();
            circleButton.Visible = false;

            drawType = "ELLIPT";
            endX.Text = "Width:";
            endY.Text = "Height: ";




            if (!showOnce)
            {
                startX.Visible = true;
                startY.Visible = true;
                endX.Visible = true;
                endY.Visible = true;

                startXinput.Visible = true;
                startYinput.Visible = true;
                endXinput.Visible = true;
                endYinput.Visible = true;
                acceptButton.Visible = true;
            }


            if (triangle) { disableTriangle(); triangle = false; }
        }

        private void radioPoints_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPoints.Checked && drawType != "TRIANG" && drawType != "CURVE")
            {
                endX.Text = "END X:";
                endY.Text = "END Y:";
            }
            else if (drawType != "LINE" && drawType != "TRIANG" && drawType != "CURVE")
            {
                endX.Text = "Width:";
                endY.Text = "Height:";
            }
            else if (radioPoints.Checked && drawType == "TRIANG")
            {
                startX.Text = "AX:";
                startY.Text = "AY:";
                endX.Text = "BX";

                triangleX.Text = "CX:";
                triangleY.Text = "CY:";

                endY.Visible = true;
                endYinput.Visible = true;

                triangleX.Visible = true;
                triangleXinput.Visible = true;

                triangleY.Visible = true;
                triangleYinput.Visible = true;
            }
            else if (!radioPoints.Checked && drawType == "TRIANG")
            {
                startX.Text = "HX:";
                startY.Text = "HY:";

                endX.Text = "| BC |:";

                endY.Visible = false;
                endYinput.Visible = false;

                triangleX.Visible = false;
                triangleXinput.Visible = false;

                triangleY.Visible = false;
                triangleYinput.Visible = false;
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            float startX, endX;
            float startY, endY;
            float triangCX, triangCY;

            float.TryParse(startXinput.Text, out startX);
            float.TryParse(startYinput.Text, out startY);

            float.TryParse(endXinput.Text, out endX);
            float.TryParse(endYinput.Text, out endY);

            if (!radioPoints.Checked && drawType != "LINE")
            {
                endX += startX;
                endY += startY;
            }


            if (drawType != "TRIANG")
            {

                PointF A = new PointF(startX, startY);
                PointF B = new PointF(endX, startY);
                PointF C = new PointF(endX, endY);
                PointF D = new PointF(startX, endY);
                fraktalDrawer.add(drawType, A, B, C, D);
            }
            else
            {

                float.TryParse(triangleXinput.Text, out triangCX);
                float.TryParse(triangleYinput.Text, out triangCY);

                PointF A = new PointF(startX, startY);
                PointF B = new PointF(endX, endY);
                PointF C = new PointF(triangCX, triangCY);
                fraktalDrawer.add(drawType, A, B, C);
            }

            fraktalDrawer.draw();
        }

        private void moveControl_Click(object sender, EventArgs e)
        {
            controlsToDark();
            moveControl.Visible = false;

            controlType = "MOVE";

            parameter1.Text = "X:";
            parameter2.Text = "Y:";

            parameter1.Visible = true;
            parameter2.Visible = true;
            param1input.Visible = true;
            param2input.Visible = true;
            acceptButton2.Visible = true;
        }

        private void resizeControl_Click(object sender, EventArgs e)
        {
            controlsToDark();
            resizeControl.Visible = false;

            controlType = "RESIZE";

            parameter1.Text = "Multiplayer:";

            parameter1.Visible = true;
            parameter2.Visible = false;
            param1input.Visible = true;
            param2input.Visible = false;
            acceptButton2.Visible = true;
        }

        private void rotateControl_Click(object sender, EventArgs e)
        {
            controlsToDark();
            rotateControl.Visible = false;

            controlType = "ROTATE";

            parameter1.Text = "Angle: ";

            parameter1.Visible = true;
            parameter2.Visible = false;
            param1input.Visible = true;
            param2input.Visible = false;
            acceptButton2.Visible = true;
        }

        private void pivotControl_Click(object sender, EventArgs e)
        {
            controlsToDark();
            pivotControl.Visible = false;

            controlType = "PIVOT";

            parameter1.Text = "Pivot X:";
            parameter2.Text = "Pivot Y:";

            parameter1.Visible = true;
            parameter2.Visible = true;
            param1input.Visible = true;
            param2input.Visible = true;
            acceptButton2.Visible = true;
        }

        private void duplicateControl_Click(object sender, EventArgs e)
        {
            duplicates++;

            controlsToDark();
            duplicateControl.Visible = false;

            parameter1.Visible = false;
            parameter2.Visible = false;
            param1input.Visible = false;
            param2input.Visible = false;

            acceptButton2.Visible = false;

            fraktalDrawer.duplicateBase(operationList);
        }

        private void acceptButton2_Click(object sender, EventArgs e)
        {
            if (controlType == "MOVE")
            {
                float x, y;

                float.TryParse(param1input.Text, out x);
                float.TryParse(param2input.Text, out y);

                fraktalDrawer.moveBase(x, y, operationList);
            }
            else if (controlType == "RESIZE")
            {
                float res;

                float.TryParse(param1input.Text, out res);
                fraktalDrawer.resizeBase(res, operationList);
            }
            else if (controlType == "ROTATE")
            {
                double deg;

                double.TryParse(param1input.Text, out deg);

                fraktalDrawer.rotateBase(deg, operationList);
            }
            else if (controlType == "PIVOT")
            {
                int x, y;
                int.TryParse(param1input.Text, out x);
                int.TryParse(param2input.Text, out y);

                fraktalDrawer.setPivot(x, y, pivotImage);
            }

            if (controlType != "PIVOT") fraktalDrawer.preview();
        }

        private void renderButton_Click_1(object sender, EventArgs e)
        {
            int steps;
            String input;

            input = Interaction.InputBox("Pass how much steps you want to render", "Render Steps", "5");

            int.TryParse(input, out steps);

            fraktalDrawer.redrawAll();
            fraktalDrawer.renderFraktal(steps, fraktalDrawer.getCords(), fraktalDrawer.getDrawings());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            fraktalDrawer.removeLastCord(operationList);
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fraktalDrawer.removeLast();
            fraktalDrawer.redrawAll();
        }

        private void penThicknessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (InputControl control = new InputControl())
            {
                //control.Show();
                if (control.ShowDialog() == DialogResult.OK)
                {
                    int tmpThick;
                    int.TryParse(control.Value, out tmpThick);

                    fraktalDrawer.PenThick = tmpThick;
                    fraktalDrawer.refreshPen();
                }
            }
        }

        private void penColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (ColorControl control = new ColorControl())
            {
                if (control.ShowDialog() == DialogResult.OK)
                {
                    fraktalDrawer.PenColor = control.Value;
                    fraktalDrawer.refreshPen();
                }
            }
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            fraktalDrawer.resetCords(operationList);
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Stream saveStream;
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "fes files (*.fes)|*.fes|All files (*.*)|*.*";
            saveDialog.FilterIndex = 2;
            saveDialog.RestoreDirectory = true;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if ((saveStream = saveDialog.OpenFile()) != null)
                {
                    fraktalDrawer.stateDraws();

                    String DATA = fraktalDrawer.sendPackedSaveData();

                    fraktalDrawer.removeLast();

                    using (StreamWriter output = new StreamWriter(saveStream))
                    {
                        output.WriteLine(DATA);
                    }

                    saveStream.Close();
                }
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Stream loadStream;
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "fes files (*.fes)|*.fes|All files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            openDialog.RestoreDirectory = true;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if ((loadStream = openDialog.OpenFile()) != null)
                {
                    String DATA;




                    using (StreamReader input = new StreamReader(loadStream))
                    {
                        DATA = input.ReadToEnd();
                    }

                    fraktalDrawer.unpackLoadData(DATA);
                    fraktalDrawer.redrawAll();



                    for (int i = 0; i < fraktalDrawer.getCords().Count; i++)
                    {

                        operationList.Text += "\n> " + fraktalDrawer.getCords()[i]["TASK"] + " ( ";

                        if ((String)fraktalDrawer.getCords()[i]["TASK"] == "MOVE")
                        {
                            operationList.Text += fraktalDrawer.getCords()[i]["OFF_X"] + " , ";
                            operationList.Text += fraktalDrawer.getCords()[i]["OFF_Y"] + " )";
                        }
                        else if ((String)fraktalDrawer.getCords()[i]["TASK"] != "DUPLICATE")
                        {
                            operationList.Text += fraktalDrawer.getCords()[i]["VAL"] + " )";
                        }
                        else
                        {
                            operationList.Text += " _ )";
                        }
                    }

                }
            }
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fraktalDrawer.deleteEverything();
            fraktalDrawer.redrawAll();
            operationList.Text = "";
        }

        private void curveButton_Click(object sender, EventArgs e)
        {
            buttonsToDark();
            curveButton.Visible = false;

            drawType = "CURVE";
            startX.Text = "AX:";
            startY.Text = "AY:";

            endX.Text = "END X:";
            endY.Text = "END Y:";

            triangleX.Text = "SX:";
            triangleY.Text = "SY:";

            if (!showOnce)
            {
                startX.Visible = true;
                startY.Visible = true;
                endX.Visible = true;
                endY.Visible = true;

                startXinput.Visible = true;
                startYinput.Visible = true;
                endXinput.Visible = true;
                endYinput.Visible = true;
                acceptButton.Visible = true;
            }

            triangleX.Visible = true;
            triangleY.Visible = true;
            triangleXinput.Visible = true;
            triangleYinput.Visible = true;
            triangle = true;
        }
    }
}
