using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace FractalEngine
{
    class FraktalDrawer
    {
        private Dictionary<string, object> drawObject = new Dictionary<string, object>();
        private List<Dictionary<string, object>> drawTable = new List<Dictionary<string, object>>();

        private Dictionary<string, object> coordinator = new Dictionary<string, object>();
        private List<Dictionary<string, object>> coordinates = new List<Dictionary<string, object>>();


        private PointF pivot;

        public Color PenColor = Color.FromArgb(0, 0, 0);
        public int PenThick = 1;

        private Graphics g;
        private Pen drawer;

        Calculator calculator;
        SaveParser saveParser = new SaveParser();

        private bool resized = false;
        private bool moved = false;

        private float resize = 1;
        private PointF move = new PointF(0, 0);

        double stepsTH = 0;
        List<Dictionary<string, object>> cordsTH = new List<Dictionary<string, object>>();
        List<Dictionary<string, object>> drawingsTH = new List<Dictionary<string, object>>();


        public FraktalDrawer(Panel panel)
        {
            drawer = new Pen(PenColor);
            drawer.Width = PenThick;

            g = panel.CreateGraphics();
            pivot = new PointF(0, 0);
            calculator = new Calculator();
        }

        public String sendPackedSaveData()
        {
            String DATA = "";

            DATA += saveParser.parseDrawsToSaveString(drawTable);

            DATA += "\n\n***\n";

            DATA += saveParser.parseCordsToSaveString(coordinates);

            return DATA;
        }

        public void unpackLoadData(String data)
        {
            drawTable = saveParser.parseDrawsFromLoadString(data);
            coordinates = saveParser.parseCordsFromLoadString(data);        
        }

        public List<Dictionary<string, object>> getDrawings() { return drawTable; }
        public List<Dictionary<string, object>> getCords() { return coordinates; }

        public void solveDrawing(Dictionary<string, object> element)
        {

            drawer.Color = (Color) element["PEN_COLOR"];
            drawer.Width = (int)element["PEN_THICK"];

            if ((string)element["DRAW"] == "LINE")
            {
                g.DrawLine(drawer, (PointF)element["A"], (PointF)element["C"]);
            }
            else if ((string)element["DRAW"] == "RECT")
            {
                PointF A = (PointF)element["A"];
                PointF B = (PointF)element["B"];
                PointF C = (PointF)element["C"];
                PointF D = (PointF)element["D"];

                PointF[] lines = new PointF[] { A, B, C, D, A };

                g.DrawLines(drawer, lines);
            }
            else if ((string)element["DRAW"] == "ELLIPT")
            {
                PointF A = (PointF)element["A"];
                PointF B = (PointF)element["B"];
                PointF C = (PointF)element["C"];
                PointF D = (PointF)element["D"];

                PointF[] curves = new PointF[] { A, B, C, D };

                g.DrawClosedCurve(drawer, curves);
            }
            else if ((string)element["DRAW"] == "TRIANG")
            {
                g.DrawLine(drawer, (PointF)element["A"], (PointF)element["B"]);
                g.DrawLine(drawer, (PointF)element["B"], (PointF)element["C"]);
                g.DrawLine(drawer, (PointF)element["C"], (PointF)element["A"]);
            }
            else if ((string)element["DRAW"] == "CURVE")
            {
                PointF A = (PointF)element["A"];
                PointF B = (PointF)element["B"];
                PointF C = (PointF)element["C"];

                PointF[] curves = new PointF[] { A, B, C };
                g.DrawCurve(drawer, curves);
            }
        }



        public void add(String type, PointF A, PointF B, PointF C, PointF D)
        {
            drawObject = new Dictionary<string, object>();

            drawObject.Add("DRAW", type);
            drawObject.Add("A", A);
            drawObject.Add("B", B);
            drawObject.Add("C", C);
            drawObject.Add("D", D);
            drawObject.Add("PEN_THICK", PenThick);
            drawObject.Add("PEN_COLOR", PenColor);
            drawTable.Add(drawObject);
        }

        public void add(String type, PointF A, PointF B, PointF C)
        {   
            drawObject = new Dictionary<string, object>();

            drawObject.Add("DRAW", type);
            drawObject.Add("A", A);
            drawObject.Add("B", B);
            drawObject.Add("C", C);
            drawObject.Add("D", new PointF(0, 0));
            drawObject.Add("PEN_THICK", PenThick);
            drawObject.Add("PEN_COLOR", PenColor);
            drawTable.Add(drawObject);          
        }
        public void stateDraws()
        {
            drawObject = new Dictionary<string, object>();

            drawObject.Add("DRAW", "EMPTY");
            drawObject.Add("A", new PointF(0, 0));
            drawObject.Add("B", new PointF(0, 0));
            drawObject.Add("C", new PointF(0, 0));
            drawObject.Add("D", new PointF(0, 0));
            drawObject.Add("PEN_THICK", 1);
            drawObject.Add("PEN_COLOR", new Color());
            drawTable.Add(drawObject);

            coordinator = new Dictionary<string, object>();

            coordinator.Add("TASK", "EMPTY");
            coordinator.Add("VAL", 0);
            coordinator.Add("PIVOT", new Point(0, 0));

            coordinates.Add(coordinator);
        }

        public void draw()
        {
            int last = drawTable.Count - 1;
            solveDrawing(drawTable[last]);    
        }

        public void redrawAll()
        {

            g.Clear(Color.FromArgb(80, 80, 80));

            for (int i=0; i<drawTable.Count; i++)
            {
                solveDrawing(drawTable[i]);
            } 
        }

        public void deleteEverything()
        {
            drawTable.Clear();
            coordinates.Clear();
        }

        public void removeLast()
        {
            if(drawTable.Count > 0) drawTable.RemoveAt(drawTable.Count - 1);
        }

        public void setPivot(int x, int y, PictureBox pivotImg)
        {
            pivot.X= x;
            pivot.Y = y;
            pivotImg.Location = new Point(x, y);
        }


        public void refreshPen()
        {
            drawer = new Pen(PenColor);
            drawer.Width = PenThick;
        }

        public void moveBase(float x, float y, RichTextBox operations)
        {            
            coordinator = new Dictionary<string, object>();

            coordinator.Add("TASK", "MOVE");
            coordinator.Add("OFF_X", x);
            coordinator.Add("OFF_Y", y);

            operations.Text += "\n> MOVE ( " + x + " , " + y + " )";
            coordinates.Add(coordinator);         
        }

        public void rotateBase(double deg, RichTextBox operations)
        {
            coordinator = new Dictionary<string, object>();

            coordinator.Add("TASK", "ROTATE");
            coordinator.Add("VAL", deg);
            coordinator.Add("PIVOT", pivot);

            operations.Text += "\n> ROTATE ( " + deg + " )";
            coordinates.Add(coordinator);
        }

        public void resizeBase(float value, RichTextBox operations)
        {
            coordinator = new Dictionary<string, object>();

            coordinator.Add("TASK", "RESIZE");
            coordinator.Add("VAL", value);
            coordinator.Add("PIVOT", pivot);

            operations.Text += "\n> RESIZE ( " + value + " )";
            coordinates.Add(coordinator);
        }

        public void duplicateBase(RichTextBox operations)
        {
            coordinator = new Dictionary<string, object>();

            coordinator.Add("TASK", "DUPLICATE");
            coordinator.Add("PIVOT", pivot);

            operations.Text += "\n> DUPLICATE( _ )";
            coordinates.Add(coordinator);
        }

        public void resetCords(RichTextBox operations)
        {
            coordinates.Clear();
            operations.Text = "";
        }

        public void removeLastCord(RichTextBox operations)
        {
            if ((coordinates.Count - 1) >= 0) { coordinates.RemoveAt(coordinates.Count - 1); }
            operations.Text = "";

            for(int i=0; i<coordinates.Count; i++)
            {
                operations.Text += "\n> " + coordinates[i]["TASK"] + "( ";
                if((string)coordinates[i]["TASK"] == "MOVE")
                {
                   operations.Text += coordinates[i]["OFF_X"] + " , " + coordinates[i]["OFF_Y"] + " )";
                }
                else if((string)coordinates[i]["TASK"] != "DUPLICATE"){ operations.Text += coordinates[i]["VAL"] + " )"; }
                else { operations.Text += " _ )";  }
            }
        }


        public void preview()
        {
            redrawAll();

            for (int i=0; i<drawTable.Count; i++)
            {
                Dictionary<string, object> changed = new Dictionary<string, object>();

                changed.Add("DRAW", drawTable[i]["DRAW"]);
                changed.Add("A", drawTable[i]["A"]);
                changed.Add("B", drawTable[i]["B"]);
                changed.Add("C", drawTable[i]["C"]);
                changed.Add("D", drawTable[i]["D"]);

                changed.Add("PEN_COLOR", Color.Pink);
                changed.Add("PEN_THICK", drawTable[i]["PEN_THICK"]);


                for (int j = 0; j < coordinates.Count; j++)
                {

                    if ((string)coordinates[j]["TASK"] == "MOVE")
                    {
                        float OFFX = (float)coordinates[j]["OFF_X"];
                        float OFFY = (float)coordinates[j]["OFF_Y"];

                        changed["A"] = calculator.Position((PointF)changed["A"], OFFX, OFFY);
                        changed["B"] = calculator.Position((PointF)changed["B"], OFFX, OFFY);
                        changed["C"] = calculator.Position((PointF)changed["C"], OFFX, OFFY);
                        changed["D"] = calculator.Position((PointF)changed["D"], OFFX, OFFY);
                    } 
                    else if((string)coordinates[j]["TASK"] == "RESIZE")
                    {                 
                        float VAL = (float)coordinates[j]["VAL"];

                        PointF pvt = (PointF)coordinates[j]["PIVOT"];

                        changed["A"] = calculator.Size((PointF)changed["A"], VAL, pvt);
                        changed["B"] = calculator.Size((PointF)changed["B"], VAL, pvt);
                        changed["C"] = calculator.Size((PointF)changed["C"], VAL, pvt);
                        changed["D"] = calculator.Size((PointF)changed["D"], VAL, pvt);
                    }
                    else if((string)coordinates[j]["TASK"] == "ROTATE")
                    {
                        double ROT = ((double)coordinates[j]["VAL"] * (Math.PI / 180));

                        PointF pvt = (PointF)coordinates[j]["PIVOT"];

                        changed["A"] = calculator.Rotation((PointF)changed["A"], ROT, pvt);
                        changed["B"] = calculator.Rotation((PointF)changed["B"], ROT, pvt);
                        changed["C"] = calculator.Rotation((PointF)changed["C"], ROT, pvt);
                        changed["D"] = calculator.Rotation((PointF)changed["D"], ROT, pvt);
                    }
                    else if((string)coordinates[j]["TASK"] == "DUPLICATE")
                    {
                        solveDrawing(changed);
                    }
                }
                solveDrawing(changed);
            }
        }


        private void startRenderThread()
        {
            jumpIn(stepsTH, cordsTH, drawingsTH);
        }

        private void jumpIn(double steps, List<Dictionary<string, object>> cords, List<Dictionary<string, object>> drawings)
        {
            move = new PointF(0, 0);
            resize = 1;

            if (steps > 0)
            {
                List<Dictionary<string, object>> recordinatedDrawTable = new List<Dictionary<string, object>>();

                List<Dictionary<string, object>> synchronizedCords = new List<Dictionary<string, object>>();


                Console.WriteLine("| RENDERING STEP NUMBER " + steps);

                for (int i = 0; i < drawings.Count; i++)
                {
                    Dictionary<string, object> render = new Dictionary<string, object>();

                    render.Add("DRAW", drawings[i]["DRAW"]);
                    render.Add("A", drawings[i]["A"]);
                    render.Add("B", drawings[i]["B"]);
                    render.Add("C", drawings[i]["C"]);
                    render.Add("D", drawings[i]["D"]);

                    render.Add("PEN_COLOR", drawings[i]["PEN_COLOR"]);
                    render.Add("PEN_THICK", drawings[i]["PEN_THICK"]);

                    Console.WriteLine(" | NEW RENDER OBJECT CONTAINS " + drawings[i]["DRAW"]);

                    for (int j = 0; j < cords.Count; j++)
                    {

                        if ((string)cords[j]["TASK"] == "MOVE")
                        {
                            float OFFX = (float)cords[j]["OFF_X"];
                            float OFFY = (float)cords[j]["OFF_Y"];

                            render["A"] = calculator.Position((PointF)render["A"], OFFX, OFFY);
                            render["B"] = calculator.Position((PointF)render["B"], OFFX, OFFY);
                            render["C"] = calculator.Position((PointF)render["C"], OFFX, OFFY);
                            render["D"] = calculator.Position((PointF)render["D"], OFFX, OFFY);

                            Console.WriteLine("\n   | OPERATION MOVE");
                            Console.WriteLine("     | OFFX: " + OFFX);
                            Console.WriteLine("     | OFFY: " + OFFY);

                            if (!moved) { move.X += OFFX; move.Y += OFFY; }

                        }
                        else if ((string)cords[j]["TASK"] == "RESIZE")
                        {
                            float VAL = (float)cords[j]["VAL"];

                            PointF pvt = (PointF)cords[j]["PIVOT"];

                            render["A"] = calculator.Size((PointF)render["A"], VAL, pvt);
                            render["B"] = calculator.Size((PointF)render["B"], VAL, pvt);
                            render["C"] = calculator.Size((PointF)render["C"], VAL, pvt);
                            render["D"] = calculator.Size((PointF)render["D"], VAL, pvt);

                            if (!resized) resize *= VAL;

                            Console.WriteLine("\n   | OPERATION RESIZE");
                            Console.WriteLine("     | VAL: " + VAL);

                        }
                        else if ((string)cords[j]["TASK"] == "ROTATE")
                        {
                            double ROT = ((double)cords[j]["VAL"] * (Math.PI / 180));

                            PointF pvt = (PointF)cords[j]["PIVOT"];

                            render["A"] = calculator.Rotation((PointF)render["A"], ROT, pvt);
                            render["B"] = calculator.Rotation((PointF)render["B"], ROT, pvt);
                            render["C"] = calculator.Rotation((PointF)render["C"], ROT, pvt);
                            render["D"] = calculator.Rotation((PointF)render["D"], ROT, pvt);

                            Console.WriteLine("\n   | OPERATION ROTATE");
                            Console.WriteLine("     | VAL: " + (double)cords[j]["VAL"]);

                        }
                        else if ((string)cords[j]["TASK"] == "DUPLICATE")
                        {

                            solveDrawing(render);
                            List<Dictionary<string, object>> duplicationCords = calculator.newCords(cords, resize, move);
                            List<Dictionary<string, object>> duplicationDrawings = new List<Dictionary<string, object>>();

                            duplicationDrawings.Add(render);

                            renderFraktal(steps - 1, duplicationCords, duplicationDrawings);
                        }
                    }

                    resized = true;
                    moved = true;

                    solveDrawing(render);
                    // if (progress.Value < progress.Maximum) progress.Value += 1;
                    recordinatedDrawTable.Add(render);
                }

                synchronizedCords = calculator.newCords(cords, resize, move);
                renderFraktal(steps - 1, synchronizedCords, recordinatedDrawTable);
            }
        }

        public void renderFraktal(double stp, List<Dictionary<string, object>> crd, List<Dictionary<string, object>> drw)
        {
            stepsTH = stp;
            cordsTH = crd;
            drawingsTH = drw;

            jumpIn(stp, crd, drw);

           // Thread render = new Thread(startRenderThread);
           // render.Start();
        }
    }
}
