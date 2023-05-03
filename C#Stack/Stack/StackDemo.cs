using System;
using Stack;

namespace StackDemo 
{
    class Program {
    
        // Main Method
        static public void Main(String[] args)
        {
            var ss1=Stack.Stack.Empty().Push("Hello").Push("World").Push("!!");

            ss1.ToString();
        }
    }
}
