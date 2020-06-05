using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TreesLab
{
 
        public class Node
        {
        Hashtable hashtable;
        Node parent;
        int type;
        string data;
        public enum Operation
            {
                Operator, Setter, Data, Empty
            }
            public Node(Hashtable hashtable, Operation op, string data, Node parent)
            {
            this.type = (int)op;
            this.hashtable = hashtable;
            this.parent = parent;
            this.data = data;
            Nodes = new List<Node>();
            }
           public List<Node> Nodes { get; set; }

            public string Visit()
            {
                switch (this.type)
                {
                    case (int)Operation.Operator:
                        double a = Convert.ToDouble(Nodes[0].Visit());
                        double b = Convert.ToDouble(Nodes[1].Visit());
                        switch (data)
                        {
                            case "+":
                                return (a + b).ToString();

                            case "-":
                                return (a - b).ToString();

                            case "/":
                                return (a / b).ToString();

                            case "*":
                                return (a * b).ToString();

                            default:
                                return (Math.Pow(a, b)).ToString();
                        }
                    case (int)Operation.Setter:
                        string s = Nodes[0].Visit();
                        if (hashtable.ContainsKey(s))
                        {
                            hashtable[s] = Nodes[1].Visit();
                        }
                        else
                        {
                            hashtable.Add(s, Nodes[1].Visit());
                        }
                        return "";
                    case (int)Operation.Empty:

                        for (int i = 0; i < Nodes.Count - 1; i++)
                        {
                            Nodes[i].Visit();
                        }
                        return Nodes[Nodes.Count - 1].Visit();
                    case (int)Operation.Data:
                        {
                            if (hashtable.ContainsKey(data))
                            {
                                return hashtable[data].ToString();
                            }
                            else
                            {
                                return data;
                            }
                        }
                    default:
                        return "";
                }
            }   

        public void BuildTree(string problem)
        {
            Node pointer = this;
            string[] tokens = problem.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int i = tokens.Length-1; i >= 0; i--)
            {
                switch (tokens[i])
                {
                    case "+": case "*": case "-": case "/": case "^":
                        Node op = new Node(hashtable, Operation.Operator, tokens[i], pointer);
                        if (pointer.parent != null)
                        {
                            pointer.Nodes.Insert(0, op);
                        }
                        else
                        {
                            pointer.Nodes.Add(op);
                        }
                        pointer = op;
                        break;
                    default:
                        pointer.Nodes.Insert(0,new Node(hashtable, Operation.Data, tokens[i], pointer));
                        break;
                }
                while (pointer.Nodes.Count > 1 && pointer.parent != null)
                {
                    pointer = pointer.parent;
                }
            }
        }
       
        
        }
    
}