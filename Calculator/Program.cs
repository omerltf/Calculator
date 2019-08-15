using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    enum ArgumentType {
        String,
        Number,
        DateTime,
        TimeSpan
    }

    enum OperatorType {
        Unknown,
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Modulus
    }
    class Program {
        public static OperatorType GetOperatorType(string message) {
            OperatorType operatorType = OperatorType.Unknown;
            while (true) {
                Console.Write("{0}: ", message);
                ConsoleKeyInfo selectedOption = Console.ReadKey();
                Console.WriteLine();
                char tempChar = selectedOption.KeyChar;

                switch (tempChar) {
                    case '+':
                        operatorType = OperatorType.Addition;
                        return operatorType;

                    case '-':
                        operatorType = OperatorType.Subtraction;
                        return operatorType;

                    case '*':
                        operatorType = OperatorType.Multiplication;
                        return operatorType;

                    case '/':
                        operatorType = OperatorType.Division;
                        return operatorType;

                    case '%':
                        operatorType = OperatorType.Modulus;
                        return operatorType;
                    default:
                        Console.WriteLine("Incorrect input. Please try again.");
                        continue;
                }
            }
        }

        public static string GetArgument(string message) {
            while (true) {
                Console.Write("{0}", message);
                Console.WriteLine();
                string tempString = Console.ReadLine();
                if (string.IsNullOrEmpty(tempString)) {
                    Console.WriteLine("Incorrect input. Please try again.");
                    continue;
                }
                else {
                    Console.Clear();
                    return tempString;
                }
            }
        }

        public static ArgumentType GetArgumentType(string argument) {
            ArgumentType argumentType = ArgumentType.String;
            double number;
            DateTime dateTime;
            TimeSpan timeSpan;
            if (double.TryParse(argument, out number)) {
                argumentType = ArgumentType.Number;
                return argumentType;
            }
            else if (TimeSpan.TryParse(argument, out timeSpan)) {
                argumentType = ArgumentType.TimeSpan;
                return argumentType;
            }
            else if (DateTime.TryParse(argument, out dateTime)) {
                argumentType = ArgumentType.DateTime;
                return argumentType;
            }
            else {
                argumentType = ArgumentType.String;
                return argumentType;
            }
        }

        static void Main(string[] args) {
            Console.WriteLine("Welcome to the Calculator app!");
            Console.ReadLine();
            string argumentOne;
            string argumentTwo;
            ArgumentType argument1Type = ArgumentType.String;
            ArgumentType argument2Type = ArgumentType.String;
            argumentOne = GetArgument("Please input first argument: ");
            argumentTwo = GetArgument("Please input second argument: ");
            argument1Type = GetArgumentType(argumentOne);
            argument2Type = GetArgumentType(argumentTwo);

            Console.WriteLine("Argument 1 is {0}", argument1Type);
            Console.WriteLine("Argument 2 is {0}", argument2Type);
            Console.ReadKey();

            OperatorType operatorType = OperatorType.Unknown;
            operatorType = GetOperatorType("Please input your operator");
            Console.WriteLine("Your operator is {0}", operatorType);
            Console.ReadKey();

            Calculator(argumentOne, argument1Type, argumentTwo, argument2Type, operatorType);
        }

        public static void Calculator(string argumentOne, ArgumentType arg1, string argumentTwo, ArgumentType arg2, OperatorType op) {
            if (arg1 == arg2) {
                if (op == OperatorType.Subtraction) {
                    switch (arg1) {
                        case ArgumentType.Number:
                            try {
                                int calculation = int.Parse(argumentOne) - int.Parse(argumentTwo);
                                Console.WriteLine("{0} - {1} = {2}", argumentOne, argumentTwo, calculation);
                                Console.ReadKey();
                                break;
                            }
                            catch (OverflowException) {
                                Console.WriteLine("Overflow occured");
                                Console.ReadKey();
                                break;
                            }
                        case ArgumentType.String:
                            string result = argumentOne.Replace(argumentTwo, "");
                            Console.WriteLine("{0} - {1} = {2}", argumentOne, argumentTwo, result);
                            Console.ReadKey();
                            break;
                        case ArgumentType.DateTime:
                            TimeSpan dateTime = DateTime.Parse(argumentOne) - DateTime.Parse(argumentTwo);
                            Console.WriteLine("{0} - {1} = {2}", argumentOne, argumentTwo, dateTime);
                            Console.ReadKey();
                            break;
                    }

                }
                else if (op == OperatorType.Addition) {
                    switch (arg1) {
                        case ArgumentType.Number:
                            try {
                                int calculation = int.Parse(argumentOne) + int.Parse(argumentTwo);
                                Console.WriteLine("{0} + {1} = {2}", argumentOne, argumentTwo, calculation);
                                Console.ReadKey();
                                break;
                            }
                            catch (OverflowException) {
                                Console.WriteLine("Overflow occured");
                                Console.ReadKey();
                                break;
                            }
                        case ArgumentType.String:
                            string result = argumentOne + argumentTwo;
                            Console.WriteLine("{0} + {1} = {2}", argumentOne, argumentTwo, result);
                            Console.ReadKey();
                            break;
                    }
                }
                Console.WriteLine("Unknown Operation. Goodbye!");
                Console.ReadKey();
            }
            else {
                switch (op) {
                    case OperatorType.Addition:
                        if ((arg1 == ArgumentType.DateTime) && (arg2 == ArgumentType.TimeSpan)) {
                            DateTime dateTime= DateTime.Parse(argumentOne) + TimeSpan.Parse(argumentTwo);
                            Console.WriteLine("{0} - {1} = {2}", argumentOne, argumentTwo, dateTime);
                            Console.ReadKey();
                        }
                        break;
                    case OperatorType.Subtraction:
                        if ((arg1 == ArgumentType.DateTime) && (arg2 == ArgumentType.TimeSpan)) {
                            DateTime timeSpan = DateTime.Parse(argumentOne) - TimeSpan.Parse(argumentTwo);
                            Console.WriteLine("{0} - {1} = {2}", argumentOne, argumentTwo, timeSpan);
                            Console.ReadKey();
                        }
                        break;
                    case OperatorType.Multiplication:
                        if ((arg1 == ArgumentType.String) && (arg2 == ArgumentType.Number)) {
                            int tempInt = int.Parse(argumentTwo);
                            string originalArgument = argumentOne;
                            for (int count=0; count<tempInt-1; count++) {
                                originalArgument += argumentOne;
                            }
                            Console.WriteLine("{0} * {1} = {2}", argumentOne, argumentTwo, originalArgument);
                            Console.ReadKey();
                        }
                        break;
                }
                Console.WriteLine("Unknown Operation. Goodbye!");
                Console.ReadKey();
            }

        }
    }
}
