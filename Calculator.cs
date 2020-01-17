using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FractalEngine
{
    class Calculator
    {


        public Calculator() { }


        public PointF Position(PointF P, float X, float Y)
        {
            P.X += X;
            P.Y += Y;

            return P;
        }

        public PointF Size(PointF P, float val, PointF pvt)
        {
            P.X -= pvt.X;
            P.Y -= pvt.Y;

            P.X *= val;
            P.Y *= val;

            P.X += pvt.X;
            P.Y += pvt.Y;

            return P;
        }

        public PointF Rotation(PointF P, double rot, PointF pvt)
        {
            P.X -= pvt.X;
            P.Y -= pvt.Y;

            float TX, TY;

            TX = (float)(P.X * Math.Cos(rot) - (P.Y * Math.Sin(rot)));
            TY = (float)(P.Y * Math.Cos(rot) + (P.X * Math.Sin(rot)));

            P.X = TX + pvt.X;
            P.Y = TY + pvt.Y;

            return P;
        }

        public List<Dictionary<string, object>> newCords(List<Dictionary<string, object>> copy, float rs, PointF m)
        {

            Dictionary<string, object> simpleCord = new Dictionary<string, object>();
            List<Dictionary<string, object>> NEW_CORDS = new List<Dictionary<string, object>>();

            for (int c = 0; c < copy.Count; c++)
            {
                simpleCord.Add("TASK", copy[c]["TASK"]);

                if ((string)copy[c]["TASK"] == "MOVE")
                {
                    simpleCord.Add("OFF_X", (float)copy[c]["OFF_X"] * rs);
                    simpleCord.Add("OFF_Y", (float)copy[c]["OFF_Y"] * rs);
                }
                else if ((string)copy[c]["TASK"] != "DUPLICATE")
                {
                    PointF pvt;

                    pvt = (PointF)copy[c]["PIVOT"];
                    pvt.X += m.X;
                    pvt.Y += m.Y;

                    simpleCord.Add("PIVOT", pvt);

                    if ((string)copy[c]["TASK"] == "ROTATE") { simpleCord.Add("VAL", (double)copy[c]["VAL"]); }
                    else { simpleCord.Add("VAL", (float)copy[c]["VAL"]); }
                }

                NEW_CORDS.Add(simpleCord);
                simpleCord = new Dictionary<string, object>();
            }

            return NEW_CORDS;

        }


    }
}
