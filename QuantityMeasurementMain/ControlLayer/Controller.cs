using QuantityMeasurementMain.ModelLayer;

namespace QuantityMeasurementMain.ControlLayer
{
    public class Controller
    {
        public static void Menu()
        {
            System.Console.WriteLine("Welcomme to Quantity Measurement App");
            while (true)
            {
                System.Console.WriteLine("1.Check Feet Equality \n2.Check Inche Equality \n0.Exit");
                System.Console.WriteLine("Enter your choice");
                int choice=int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        System.Console.Write("Enter first feet input:");
                        double input1=double.Parse(Console.ReadLine());
                        System.Console.Write("Enter second feet Input:");
                        double input2=double.Parse(Console.ReadLine());
                        Feet feet1=new Feet(input1);
                        Feet feet2=new Feet(input2);
                        System.Console.WriteLine($"Input {feet1.Value} and {feet2.Value} are equal : {feet1.Equals(feet2)}");
                        break;
                    case 2:
                        System.Console.Write("Enter first feet input:");
                        double inchInput1=double.Parse(Console.ReadLine());
                        System.Console.Write("Enter second feet Input:");
                        double inchInput2=double.Parse(Console.ReadLine());
                        Inch inch1=new Inch(inchInput1);
                        Inch inch2=new Inch(inchInput2);
                        System.Console.WriteLine($"Input {inch1.Value} and {inch2.Value} are equal : {inch1.Equals(inch2)}");
                        break;
                }
            }
        }
    } 
}