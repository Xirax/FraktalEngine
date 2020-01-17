using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalEngine
{
    class SaveParser
    {

        private int svg = 0;

        public SaveParser() { }

        public String parseDrawsToSaveString(List<Dictionary<String, object>> list)
        {
            String save = "";

            save += (list.Count - 1);

            for(int i=0; i<(list.Count - 1); i++)
            {
                save += "\n[ " + i + " ]" + " | " + list[i]["DRAW"];
                save += " | " + list[i]["A"] + " | " + list[i]["B"] + " | " + list[i]["C"] + " | " + list[i]["D"];
                save += " | " + list[i]["PEN_THICK"] + " | " + list[i]["PEN_COLOR"];
            }

            return save;
        }

        public String parseCordsToSaveString(List<Dictionary<String, object>> list)
        {
            String save = "#";

            save += (list.Count - 1);

            for (int i = 0; i < list.Count; i++)
            {
                save += "\n[ " + i + " ]" + " | " + list[i]["TASK"];

                if (list[i]["TASK"].ToString() == "MOVE") save += " | " + list[i]["OFF_X"] + " | " + list[i]["OFF_Y"];

                else if (list[i]["TASK"].ToString() != "DUPLICATE") save += " | " + list[i]["VAL"] + " | " + list[i]["PIVOT"];

                else save += " | " + list[i]["PIVOT"];
            }

            return save;
        }

        public List<Dictionary<String, object>> parseDrawsFromLoadString(String load)
        {
            List<Dictionary<String, object>> draws = new List<Dictionary<string, object>>();

            int k = 0, ctn;
            String ctnS = "";


            while(load[k] != '\n') { ctnS += load[k]; k++; }

            int.TryParse(ctnS, out ctn);


            int w = k;


            for (int i=0; i<ctn; i++)
            {
                Dictionary<String, object> loadObject = new Dictionary<string, object>();

                String drawType = "";

                String numS = "";
                float X, Y;
           
                PointF[] TAB = new PointF[4];

                while (load[w] != '|') w++;
                w+= 2;

                while(load[w] != ' ')
                {
                    drawType += load[w];
                    w++;
                }

                for (int p=0; p<4; p++)
                {
                    while (w < load.Length && load[w] != '{') w++; w += 3;
                    while (w < load.Length && load[w] != ',') { numS += load[w]; w++; }
                    X = float.Parse(numS);
                    numS = "";

                    while (w < load.Length && load[w] != '=') w++; w++;
                    while (w < load.Length && load[w] != '}') { numS += load[w]; w++; }

                    Y = float.Parse(numS);
                    numS = "";

                    TAB[p] = new PointF(X, Y);

                }

                while (w < load.Length && load[w] != '|') w++;
                w += 2;

                int thickness;
                String thickS = "";

                while(w < load.Length && load[w] != ' ') { thickS += load[w]; w++; }

                int.TryParse(thickS, out thickness);

                int R, G, B;
                String[] rgbs = new string[3];

                while (w < load.Length && load[w] != '=') w++; w++;

                for (int rgb=0; rgb<3; rgb++)
                {
                    while (w < load.Length && load[w] != '=') w++;
                    w++;

                    

                    while (w < load.Length && load[w] != ',') { rgbs[rgb] += load[w]; w++; }                  
                }

                int.TryParse(rgbs[0], out R);
                int.TryParse(rgbs[1], out G);
                int.TryParse(rgbs[2], out B);

                


                if (drawType != "EMPTY")
                {
                    Console.WriteLine("DRAWING: " + drawType + "   |   A: " + TAB[0] + "   |   B: " + TAB[1] + "   |   C: " + TAB[2] + "   |   D: " + TAB[3]);
                    Console.WriteLine("   >THICK: " + thickness + "   |   COLOR: " + Color.FromArgb(R, G, B) + " (" + R + " , " + G + " , " + B + ")\n");
                    loadObject.Add("DRAW", drawType);
                    loadObject.Add("A", TAB[0]);
                    loadObject.Add("B", TAB[1]);
                    loadObject.Add("C", TAB[2]);
                    loadObject.Add("D", TAB[3]);
                    loadObject.Add("PEN_THICK", thickness);
                    loadObject.Add("PEN_COLOR", Color.FromArgb(R, G, B));
                    draws.Add(loadObject);
                }

                svg = w-10;
            }


            Console.WriteLine("\n\n --- DRAWS SIZE: " + draws.Count);
            Console.WriteLine("\n\n --- Ended on: " + svg);

            return draws;
        }

        public List<Dictionary<String, object>> parseCordsFromLoadString(String load)
        {
            List<Dictionary<String, object>> cords = new List<Dictionary<string, object>>();

            int ctn;
            String ctnS = "";

            int w = 0;

            while (w < load.Length && load[w] != '*') w++;
            Console.WriteLine("THIS SIGN IS ON: " + w + "(" + load[w] + ")");

            while (w < load.Length && load[w] != '#') w++; w++;
            

            while (w < load.Length && load[w] != '\n') { Console.WriteLine("THE CHARACTER IS: " + load[w]); ctnS += load[w]; w++; }

            int.TryParse(ctnS, out ctn);

            

            Console.WriteLine("\nCORDS  TO LOAD: " + ctn);

            for (int i = 0; i < ctn; i++)
            {

                Console.WriteLine("\nITERATION [" + i + "]");

                Dictionary<String, object> loadObject = new Dictionary<string, object>();

                String taskType = "";
         
                while (w < load.Length && load[w] != '|') w++;
                w += 2;

                while (w < load.Length && load[w] != ' ') { taskType += load[w]; w++; }

                while (w < load.Length && load[w] != '|') w++;
                w += 2;
             
                if (taskType == "MOVE")
                {
                    float X, Y;
                    String XS = "", YS = "";

                    while (w < load.Length && load[w] != ' ') { XS += load[w]; w++; }
                    while (w < load.Length && load[w] != '|') w++;
                    w += 2;

                    while (w < load.Length && load[w] != '\n') { YS += load[w]; w++; }

                    float.TryParse(XS, out X);
                    float.TryParse(YS, out Y);

                    loadObject.Add("TASK", taskType);
                    loadObject.Add("OFF_X", X);
                    loadObject.Add("OFF_Y", Y);

                    Console.WriteLine("   > TASK: " + taskType + "   |   OFF_X: " + X + "   |   OFF_Y: " + Y);

                }
                else if (taskType == "ROTATE" || taskType == "RESIZE")
                {
                    float val;
                    float pivX, pivY;
                    String valS = "", pivXS = "", pivYS = "";

                    while (w < load.Length && load[w] != ' ') { valS += load[w]; w++; }
                    while (w < load.Length && load[w] != '=') w++; w++;

                    while (w < load.Length && load[w] != ',') { pivXS += load[w]; w++; }

                    while (w < load.Length && load[w] != '=') w++; w++;

                    while (w < load.Length && load[w] != '}') { pivYS += load[w]; w++; }

                    float.TryParse(valS, out val);
                    float.TryParse(pivXS, out pivX);
                    float.TryParse(pivYS, out pivY);

                    

                    loadObject.Add("TASK", taskType);
                    loadObject.Add("VAL", val);
                    loadObject.Add("PIVOT", new PointF(pivX, pivY));

                    Console.WriteLine("   > TASK: " + taskType + "   |   VAL: " + val + "   |   PIVOT " + new PointF(pivX, pivY));
                }
                else if(taskType == "DUPLICATE")
                {
                    float pivX, pivY;

                    String pivXS = "", pivYS = "";

                    while (w < load.Length && load[w] != '=') w++; w++;

                    while (w < load.Length && load[w] != ',') { pivXS += load[w]; w++; }

                    while (w < load.Length && load[w] != '=') w++; w++;

                    while (w < load.Length && load[w] != '}') { pivYS += load[w]; w++; }

                    float.TryParse(pivXS, out pivX);
                    float.TryParse(pivYS, out pivY);

                    loadObject.Add("TASK", taskType);
                    loadObject.Add("PIVOT", new PointF(pivX, pivY));

                    Console.WriteLine("   > TASK: " + taskType + "   |   PIVOT " + new PointF(pivX, pivY));
                }

                
                cords.Add(loadObject);
            }

            Console.WriteLine("\n --- CORDS SIZE: " + cords.Count);
            return cords;
            
        }

    }
}
