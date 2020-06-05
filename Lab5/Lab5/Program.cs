using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TreesLab
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        static string CreatePostfixNotation(string input)
        {
            input = input.Replace("+", " + ");
            input = input.Replace("-", " - ");
            input = input.Replace("/", " / ");
            input = input.Replace("*", " * ");
            input = input.Replace("^", " ^ ");
            input = input.Replace("(", " ( ");
            input = input.Replace(")", " ) ");
            bool nextIsNegative = false;
            string[] OpAndOp = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Stack<string> stack = new Stack<string>();
            string result = "";
            for (int i = 0; i < OpAndOp.Length; i++)
            {
                switch (OpAndOp[i])
                {
                    case "/":
                        result += " ";
                        while (stack.Count > 0)
                        {
                            if (stack.Peek() == "^" || stack.Peek() == "/" || stack.Peek() == "*" || stack.Peek() == "!")
                            {
                                result += " ";
                                result += stack.Pop().ToString();
                                result += " ";
                            }
                            else
                            {
                                break;
                            }
                        }
                        stack.Push(OpAndOp[i]);
                        break;
                    case "*":
                        result += " ";
                        while (stack.Count > 0)
                        {
                            if (stack.Peek() == "^" || stack.Peek() == "/" || stack.Peek() == "*" || stack.Peek() == "!")
                            {
                                result += " ";
                                result += stack.Pop().ToString();
                                result += " ";
                            }
                            else
                            {
                                break;
                            }
                        }
                        stack.Push(OpAndOp[i]);
                        break;
                    case "^":
                        result += " ";
                        stack.Push(OpAndOp[i]);
                        break;
                    case "-":
                        if (OpAndOp[i - 1] == "(")
                        {
                            nextIsNegative = true;
                        }
                        else
                        {
                            nextIsNegative = false;
                            result += " ";
                            while (stack.Count > 0)
                            {
                                if (stack.Peek() == "^" || stack.Peek() == "/" || stack.Peek() == "*" || stack.Peek() == "-" || stack.Peek() == "+" || stack.Peek() == "!")
                                {
                                    result += " ";
                                    result += stack.Pop().ToString();
                                    result += " ";
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(OpAndOp[i]);
                        }
                        break;
                    case "+":
                        result += " ";
                        while (stack.Count > 0)
                        {
                            if (stack.Peek() == "^" || stack.Peek() == "/" || stack.Peek() == "*" || stack.Peek() == "-" || stack.Peek() == "+" || stack.Peek() == "!")
                            {
                                result += " ";
                                result += stack.Pop().ToString(); result += " ";
                            }
                            else
                            {
                                break;
                            }
                        }
                        stack.Push(OpAndOp[i]);
                        break;
                    case "(":
                        stack.Push(OpAndOp[i]);
                        break;
                    case ")":
                        while (stack.Peek() != "(")
                        {
                            result += " ";
                            result += stack.Pop().ToString();
                        }
                        stack.Pop();
                        break;
                    default:
                        if (nextIsNegative)
                        {
                            result += "-";
                            nextIsNegative = false;
                        }
                        result += OpAndOp[i].ToString();
                        break;
                }
            }
            while (stack.Count > 0)
            {
                result += " ";
                result += stack.Pop().ToString();
            }
            return result;
        }

    }
}