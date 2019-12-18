using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphDrawer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            string function = textBox1.Text;

            Pen pen = new Pen(Color.Red);
            Graphics g = panel1.CreateGraphics();
            /*
            string outCome = mul(detirative("4x"), "3x^2+5") + "-" 
                + mul("4x", detirative("3x^2+5") + "/" 
                + mul("3x^2+5", "3x^2+5"));
            //MessageBox.Show(outCome);
            
            string tri = "4x^2+3x/5x^1";
            string[] tArr = tri.Split('/');
            string t1 = mul(detirative(tArr[0]), tArr[1]);
            MessageBox.Show("det1: " + detirative(tArr[0]) + " | mul: " + tArr[1] + " | Equals: " + t1);
            string t2 = mul(tArr[0], detirative(tArr[1]));
            string t3 = mul(tArr[1], tArr[1]);
            */

            // string newT = t1 + " - " + t2 + " / " + t3;
            //MessageBox.Show(newT);



            //Draw(retPolinom("4x^3+3x^2+5"));

            if (function.Contains('/'))
            {
                divFunction(function);
                if(function.Contains("cos") || function.Contains("sin"))
                {

                }
            }
            else
            {
                if (function.Contains("ln"))
                {
                    lnFunction(function);
                }
                else
                {
                    if (function.Contains("cos") || function.Contains("sin"))
                    {
                        if (function.Contains("cos"))
                        {
                            TrigonometricFunction(function,"cos");

                        }
                        if (function.Contains("sin"))
                        {
                            TrigonometricFunction(function, "sin");
                        }

                    }
                    else
                    {
                        polinomFunction(function, Color.Red);
                        string det = detirative(function);
                        char[] cDArr = det.ToCharArray();
                        det = det.Substring(0, det.Length - 1);

                        label2.Visible = true;
                        label2.Text = "Det: " + det;
                        polinomFunction(det, Color.White);

                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        ///////////////////try return value////////////////////
        public void Draw(double[,] arr)
        {
            Pen pen = new Pen(Color.Red);
            Graphics g = panel1.CreateGraphics();

            for (int j = 0; j < 998; j++)
            {
                //Draw the lines between each point to the next on based on the function values
                Console.WriteLine(j);
                Console.WriteLine(arr.Length);
                g.DrawLine(pen, (int)(180 + arr[j, 1] * 10), (int)(170 - arr[j, 0]), (int)(180 + (arr[j + 1, 1] * 10)), (int)(170 - arr[j + 1, 0]));

            }
        }


        public static string retPolinomVal(string function,double i)
        {
           
                double sum = 0;                             //This varieble will present the f(x) value

                string[] sArr = function.Split('+');        //The first node of the syntax tree (+)
                foreach (string s in sArr)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        sum += Convert.ToDouble(n);         //If the node is scallar - add it to the f(x) value
                    }

                    if (s.Contains('x'))
                    {
                        if (s.Contains('^'))                //Split the node to coefficient and exponent
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            sum += adv * (Math.Pow(i, up));             // add the value to f(x)
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            sum += Convert.ToDouble(s1[0]) * i;
                        }
                    }


                }

               
                Console.WriteLine("f(" + i + ") = " + sum);     //Output the outcome to the console           
            return sum.ToString();
        }


        public void divTrigoFunction(string function, string action)
        {
            Pen pen = new Pen(Color.Red);
            Graphics g = panel1.CreateGraphics();


            double[,] arr = new double[1000, 2];
            int indx = 0;

            double sum1 = 0;
            double sum2 = 0;
            string[] sArr = function.Split('/');

            //nominator//
            string tExp = "";
            string remTE = "";

            int i1 = function.IndexOf('(');
            int i2 = function.IndexOf(')');
            int lngth = i2 - i1;


            tExp = function.Substring(i1 + 1, lngth - 1);
            if (i1 - 4 >= 0)
                remTE = function.Substring(0, i1 - 4) + function.Substring(i2 + 1, function.Length - 1 - i2);
            else
                remTE = function.Substring(0, i1 - 3) + function.Substring(i2 + 1, function.Length - 1 - i2);


            //
            // MessageBox.Show(tExp);
            //MessageBox.Show(remTE);

            for (double i = -5; i < 4.98; i += 0.01)
            {
                double lnSum = 0;
                double sum = 0;

                string[] sArr1 = tExp.Split('+');
                foreach (string s in sArr1)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        lnSum += Convert.ToDouble(n);
                    }
                    if (s.Contains('x'))
                    {
                        if (s.Contains('^'))
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            lnSum += adv * (Math.Pow(i, up));
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            lnSum += Convert.ToDouble(s1[0]) * i;
                        }
                    }


                }
                if (action == "cos")
                    lnSum = Math.Cos(lnSum);
                if (action == "sin")
                    lnSum = Math.Sin(lnSum);

                if (remTE != null)
                {

                }
                string[] sArr2 = remTE.Split('+');
                foreach (string s in sArr2)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        sum += Convert.ToDouble(n);
                    }
                    if (s.Contains('x'))
                    {
                        if (s.Contains('^'))
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            sum += adv * (Math.Pow(i, up));
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            sum += Convert.ToDouble(s1[0]) * i;
                        }
                    }
                }


            }

        }
            ///////////////////////end try///////////////////////
            public static string divDerivative(string function)
        {
            string[] split = function.Split('/');
            string nominator = split[0];
            string denominator = split[1];

            return   mul(detirative(nominator), denominator) + "-"
               + mul(nominator, detirative(denominator) + "/"
               + mul(denominator, denominator));
        }

        public void polinomFunction(string function,Color color)
        {
            Pen pen = new Pen(color);
            Graphics g = panel1.CreateGraphics();

            double[,] arr = new double[1000,2];             //the array will store the values of x and f(x)

            int indx = 0;

            for (double i = -5; i < 4.98; i += 0.01)        //The for loop that puts values instead of X 
            {
                double sum = 0;                             //This varieble will present the f(x) value

                string[] sArr = function.Split('+');        //The first node of the syntax tree (+)
                foreach (string s in sArr)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        sum += Convert.ToDouble(n);         //If the node is scallar - add it to the f(x) value
                    }

                    if (s.Contains('x'))                    
                    {
                        if (s.Contains('^'))                //Split the node to coefficient and exponent
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            sum += adv * (Math.Pow(i, up));             // add the value to f(x)
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            sum += Convert.ToDouble(s1[0]) * i;
                        }
                    }

                    
                }

                arr[indx,0] = sum;
                arr[indx, 1] = i;
                
                Console.WriteLine("f(" + i + ") = " + sum);     //Output the outcome to the console

                indx++;

            }

            for (int j = 0; j <998; j++)
            {
                                                            //Draw the lines between each point to the next on based on the function values
                Console.WriteLine(j);
                Console.WriteLine(arr.Length);
                g.DrawLine(pen, (int)(180 + arr[j,1] * 10), (int)(170 - arr[j, 0]), (int)(180 + (arr[j+1,1] * 10)), (int)(170 - arr[j+1, 0]));

            }


            
        }

        private void divFunction(string function)
        {
            Pen pen = new Pen(Color.Red);
            Graphics g = panel1.CreateGraphics();


            double[,] arr = new double[1000, 2];
            int indx = 0;

            string[] temp = function.Split('/');
            double a = getAs(temp[1]);
            label1.Visible = true;
            label1.Text = "X = " + a.ToString();
           // MessageBox.Show(a.ToString());

            int indxEr =0;

            for (double i = -5; i < 4.98; i += 0.01)
            {
                if (i > a - 0.1 && i < a + 0.1)
                {
                    indxEr = indx;
                    i += 0.01;
                    arr[indx, 1] = 0;
                }
                else
                {
                    double sum1 = 0;
                    double sum2 = 0;
                    string[] sArr = function.Split('/');

                    //mone//
                    string[] sArr1 = sArr[0].Split('+');
                    foreach (string s in sArr1)
                    {
                        int n;
                        if (int.TryParse(s, out n))
                        {
                            sum1 += Convert.ToDouble(n);
                        }

                        if (s.Contains('x'))
                        {
                            if (s.Contains('^'))
                            {
                                string[] s1 = s.Split('x');
                                double adv = Convert.ToDouble(s1[0]);
                                string[] s2 = s1[s1.Length - 1].Split('^');
                                double up = Convert.ToInt32(s2[s2.Length - 1]);

                                sum1 += adv * (Math.Pow(i, up));
                            }
                            else
                            {
                                string[] s1 = s.Split('x');
                                sum1 += Convert.ToDouble(s1[0]) * i;
                            }
                        }
                    }

                    //mechane//
                    string[] sArr2 = sArr[1].Split('+');
                    foreach (string s in sArr2)
                    {
                        int n;
                        if (int.TryParse(s, out n))
                        {
                            sum2 += Convert.ToDouble(n);
                        }

                        if (s.Contains('x'))
                        {
                            if (s.Contains('^'))
                            {
                                string[] s1 = s.Split('x');
                                double adv = Convert.ToDouble(s1[0]);
                                string[] s2 = s1[s1.Length - 1].Split('^');
                                double up = Convert.ToInt32(s2[s2.Length - 1]);

                                sum2 += adv * (Math.Pow(i, up));
                            }
                            else
                            {
                                string[] s1 = s.Split('x');
                                sum2 += Convert.ToDouble(s1[0]) * i;
                            }
                        }
                    }

                    arr[indx, 0] = sum1 / sum2;
                    arr[indx, 1] = i;


                    Console.WriteLine("f(" + i + ") = " + arr[indx, 0]);
                }
                indx++;


            }

            for (int j = 1; j < 998; j++)
            {
                if (arr[j,1] == 0 || arr[j+1,1] == 0)
                {
                    j++;
                    //MessageBox.Show("stop");
                }
                else
                {
                    try
                    {
                        g.DrawLine(pen, (int)(180 + arr[j, 1] * 30), (int)(170 - arr[j, 0] * 30), (int)(180 + (arr[j + 1, 1] * 30)), (int)(170 - arr[j + 1, 0] * 30));

                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Skiped");
                    }
                }
            }

            

        }

        public static double getAs(string function)
        {
            double sum = 0;
            double temp = 0;

            string[] sArr = function.Split('+');

            foreach (var s in sArr)
            {
                int n;

                if (int.TryParse(s, out n))
                {
                    sum += Convert.ToDouble(n);
                }

                if (s.Contains('x'))
                {

                    string[] s1 = s.Split('x');
                    temp += Convert.ToDouble(s1[0]);
                }
            }

            return -sum / temp;
        }

        public void lnFunction(string function)
        {

            Pen pen = new Pen(Color.Red);
            Graphics g = panel1.CreateGraphics();

            double[,] arr = new double[1000, 2];

            string lnExp = "";                          //Wiil be the content inside the ln exp.
            int indx = 0;                       
            string remLn = "";                          //Will be the content outside the ln

            int i1 = function.IndexOf('(');             //Find the starting indx of the ln to call the Substring function
            int i2 = function.IndexOf(')');             //Find the end of the ln exp. to the Substring function
            int lngth = i2 - i1;
           

            lnExp = function.Substring(i1+1,lngth-1);   //Cut the ln content
            if(i1-3>=0)
                remLn = function.Substring(0, i1 - 3) + function.Substring(i2 + 1, function.Length - 1 - i2);
            else
                remLn = function.Substring(0, i1 - 2) + function.Substring(i2 + 1, function.Length - 1 - i2);


            
            for (double i = -5; i < 4.98; i += 0.01)        //Same process as the last function (Polinom)
            {
                double lnSum = 0;
                double sum = 0;

                string[] sArr1 = lnExp.Split('+');
                foreach (string s in sArr1)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        lnSum += Convert.ToDouble(n);
                    }
                    if (s.Contains('x'))
                    {
                        if (s.Contains('^'))
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            lnSum += adv * (Math.Pow(i, up));
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            lnSum += Convert.ToDouble(s1[0]) * i;
                        }
                    }


                }

                lnSum = Math.Log(lnSum);

                if (remLn != null)
                {

                }
                string[] sArr2 = remLn.Split('+');
                foreach (string s in sArr2)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        sum += Convert.ToDouble(n);
                    }
                    if (s.Contains('x'))
                    {
                        if (s.Contains('^'))
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            sum += adv * (Math.Pow(i, up));
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            sum += Convert.ToDouble(s1[0]) * i;
                        }
                    }
                }

                arr[indx, 0] = sum + lnSum;
                arr[indx, 1] = i;

                Console.WriteLine("f(" + i + ") = " + (sum+lnSum));


                indx++;

               
            }

            for (int j = 0; j < 998; j++)
            {
                try
                {
                    Console.WriteLine(j);
                    Console.WriteLine(arr.Length);
                    g.DrawLine(pen, (int)(180 + arr[j, 1] * 30), (int)(170 - arr[j, 0] *30), (int)(180 + (arr[j + 1, 1] * 30)), (int)(170 - arr[j + 1, 0] *30));

                }
                catch (Exception)
                {

                    
                }
             
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.Black);
        }

        public static string detirative(string function)
        {
            string det = "";

            string[] sArr = function.Split('+');             //Splits the polinom and build the tree's first nodes
            foreach (string s in sArr)
            {
                if (s.Contains('x'))        
                {
                    if (s.Contains('^'))
                    {
                        string[] s1 = s.Split('x');
                        double adv = Convert.ToDouble(s1[0]);                   //Same process from the kast functions
                        string[] s2 = s1[s1.Length - 1].Split('^');
                        double up = Convert.ToInt32(s2[s2.Length - 1]);

                        adv = adv * up;
                        up--;                                 //The (x^n)' derivative rules 
                        det += adv + "x^" + up +"+";
                    }
                    else
                    {
                        string[] s1 = s.Split('x');
                        det += s1[0] + "+";
                    }
                }
               
            }

            return det;
        }

        public static string mul(string first, string second)    
        {
            //function gets two strings - the two expresssion  
            
            string[] sArr1 = first.Split('+');          //splits the 1st expression - first node of the syntax tree (+)
            string[] sArr2 = second.Split('+');         //splits the 2nd expression 

            string outCome = "";                        //will be the final string
            double sum = 0;                             //convert to to the string

            foreach (var s in sArr2)                    //loop over the 1st expression - over all the nodes in the tree
            {
                double num = 0;
                int adv = 0;
                 
                if (s.Contains('x'))
                {
                    if (s.Contains('^'))                  //Split the node to coefficient and exponent
                    {
                        string[] s1 = s.Split('x');
                        num = Convert.ToDouble(s1[0]);
                        string[] s2 = s1[s1.Length - 1].Split('^');         
                        adv = Convert.ToInt32(s2[s2.Length - 1]);
                    }
                    else
                    {
                        string[] s1 = s.Split('x');
                        num = Convert.ToDouble(s1[0]);
                        adv = 1;
                    }
                }
                else                            //if the node is scalar - convert it to double
                {
                    if(s!="")                       
                    {
                        num = Convert.ToDouble(s);
                        //MessageBox.Show("Works ! " + s);
                    }
                }


                foreach (var s2 in sArr1)       //loop over the second expression 
                {
                    double num1 = 0;
                    int adv1 = 0;
                   // MessageBox.Show(s2);
                    if (s2.Contains('x'))       //same process as the first
                    {
                        if (s2.Contains('^'))
                        {
                            string[] s1 = s2.Split('x');
                            num1 = Convert.ToDouble(s1[0]);
                            string[] s21 = s1[s1.Length - 1].Split('^');
                            adv1 = Convert.ToInt32(s21[s21.Length - 1]);
                        }
                        else
                        {
                            string[] s1 = s2.Split('x');
                            num1 = Convert.ToDouble(s1[0]);
                            adv1 = 1;
                        }

                                                                //based on multiplying laws - (ax^n)*(bx^f) = (a*b)x^(n+f)
                        double temp = num * num1;               //multiple the coeficents   
                        double temp1 = adv + adv1;              //add the exponents
                        outCome += temp + "x^" + temp1 + "+";
                    }
                    else
                    {
                        if (s2 != "")
                        {
                            num1 = Convert.ToDouble(s2);
                            sum += num * num1;
                            outCome += sum + "x" + "^" + adv1;
                        }
                    }



                }



            }

            Console.WriteLine(outCome);
            return outCome;
        }

        public void TrigonometricFunction(string function,string action)
        {

            Pen pen = new Pen(Color.Red);
            Graphics g = panel1.CreateGraphics();

            double[,] arr = new double[1000, 2];

            string tExp = "";
            int indx = 0;
            string remTE = "";

            int i1 = function.IndexOf('(');
            int i2 = function.IndexOf(')');
            int lngth = i2 - i1;


            tExp = function.Substring(i1 + 1, lngth - 1);
            if (i1 - 4 >= 0)
                remTE = function.Substring(0, i1 - 4) + function.Substring(i2 + 1, function.Length - 1 - i2);
            else
                remTE = function.Substring(0, i1 - 3) + function.Substring(i2 + 1, function.Length - 1 - i2);


            //
           // MessageBox.Show(tExp);
            //MessageBox.Show(remTE);

            for (double i = -5; i < 4.98; i += 0.01)
            {
                double lnSum = 0;
                double sum = 0;

                string[] sArr1 = tExp.Split('+');
                foreach (string s in sArr1)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        lnSum += Convert.ToDouble(n);
                    }
                    if (s.Contains('x'))
                    {
                        if (s.Contains('^'))
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            lnSum += adv * (Math.Pow(i, up));
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            lnSum += Convert.ToDouble(s1[0]) * i;
                        }
                    }


                }
                if(action == "cos")
                     lnSum = Math.Cos(lnSum);
                if (action == "sin")
                    lnSum = Math.Sin(lnSum);

                if (remTE != null)
                {

                }
                string[] sArr2 = remTE.Split('+');
                foreach (string s in sArr2)
                {
                    int n;
                    if (int.TryParse(s, out n))
                    {
                        sum += Convert.ToDouble(n);
                    }
                    if (s.Contains('x'))
                    {
                        if (s.Contains('^'))
                        {
                            string[] s1 = s.Split('x');
                            double adv = Convert.ToDouble(s1[0]);
                            string[] s2 = s1[s1.Length - 1].Split('^');
                            double up = Convert.ToInt32(s2[s2.Length - 1]);

                            sum += adv * (Math.Pow(i, up));
                        }
                        else
                        {
                            string[] s1 = s.Split('x');
                            sum += Convert.ToDouble(s1[0]) * i;
                        }
                    }
                }

                arr[indx, 0] = sum + lnSum;
                arr[indx, 1] = i;

                Console.WriteLine("f(" + i + ") = " + (sum + lnSum));

           

                indx++;


            }

            for (int j = 0; j < 998; j++)
            {
                try
                {
                    Console.WriteLine(j);
                    Console.WriteLine(arr.Length);
                    g.DrawLine(pen, (int)(180 + arr[j, 1] * 30), (int)(170 - arr[j, 0] *30), (int)(180 + (arr[j + 1, 1] * 30)), (int)(170 - arr[j + 1, 0] *30));

                }
                catch (Exception)
                {


                }

            }

        }
    }
}
