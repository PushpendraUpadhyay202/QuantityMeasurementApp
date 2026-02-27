using QuantityMeasurementMain.BusinessLayer;
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
                System.Console.WriteLine("1.check Lengths Equality \n0.Exit");
                System.Console.WriteLine("Enter your choice");
                
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("invalid input");
                    continue;
                }
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        System.Console.Write("Enter first  input:");
                        double input1=double.Parse(Console.ReadLine());
                        System.Console.Write("unit  for first input :");
                        if (!Enum.TryParse(Console.ReadLine(), true, out Length.LengthUnit unit1))
                        {
                            Console.WriteLine("Invalid unit");
                            return;
                        }
                        System.Console.Write("Enter second  Input:");
                        double input2=double.Parse(Console.ReadLine());
                        System.Console.Write("unit  for second input :");
                        if (!Enum.TryParse(Console.ReadLine(), true, out Length.LengthUnit unit2))
                        {
                            Console.WriteLine("Invalid unit");
                            return;
                        }
                        System.Console.WriteLine($"{input1} {unit1} and {input2} {unit2} are equal: {LengthEqualityUtility.CheckLengthEquality(input1,unit1,input2,unit2)}"); 
                        break;
                }
            }
        }
    } 
}