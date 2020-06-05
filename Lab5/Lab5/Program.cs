﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TreesLab
{
    class Program
    {
        //                          123*(a/(43-t))-5*b^(f/e^k)-45/f
        //                          (15^a-b*3)/((4^(d-c))*c-a*d)

        static void Main(string[] args)
        {
            string problem = Console.ReadLine();
            string s = "notNullString";
            Hashtable table = new Hashtable();
            Tree tree = new Tree(table);
            Node pointer = tree.topNode;
            while (s != "")
            {
                s = Console.ReadLine();
                if (s != "")
                {
                    s = s.Replace("=", " ");
                    s = s.Replace(";", " ");
                    string[] parts = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Node setter = new Node(table, Node.Operation.Setter, "=", tree.topNode);
                    Node a = new Node(table, Node.Operation.Data, parts[0], setter);
                    Node b = new Node(table, Node.Operation.Data, parts[1], setter);
                    setter.Nodes.Add(a);
                    setter.Nodes.Add(b);
                    pointer.Nodes.Add(setter);
                }
            }
            problem = CreatePostfixNotation(problem);
            Console.WriteLine(problem);
            tree.topNode.BuildTree(problem);
            Console.WriteLine(tree.Calculate());
            Console.WriteLine(table["a"]);
            Console.WriteLine(table["b"]);
            Console.WriteLine(table["c"]);
            Console.WriteLine(table["d"]);
        }
        public static string CreatePostfixNotation(string input)
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
